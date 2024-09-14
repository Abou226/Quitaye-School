using PrintAction;
using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using Quitaye_Medical.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Ravitaillement : Form
    {
        private string name;
        private Timer loadTime = new Timer();
        public Ravitaillement()
        {
            InitializeComponent();
            btnTransfère.Click += BtnTransfère_Click;
            loadTime.Enabled = false;
            loadTime.Interval = 10;
            if (Principales.type_compte != "Administrateur")
            {
                cbxDetachementEnvoie.Enabled = false;
                btnTransfère.Enabled = false;
            }
            loadTime.Start();
            loadTime.Tick += LoadTime_Tick;
            dataGridView1.CellClick += DataGridView1_CellClick;
            btnAjouter.Click += BtnAjouter_Click;
            startDate.ValueChanged += StartDate_ValueChanged;
            btnPdf.Click += btnPdf_Click;
            btnExcel.Click += btnExcel_Click;
            EndDate.ValueChanged += StartDate_ValueChanged;
            txtsearch.TextChanged += TxtSearch_TextChanged;
            
        }

        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtsearch.Text != "")
                await CallSearch(txtsearch.Text);
            else
                await CallData();
        }

        private async Task CallSearch(string search)
        {
            MyTable result = await FillSearchAsync(txtsearch.Text);
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = result.Table;
            dataGridView1.Columns[0].Visible = false;
            lblMontant.Text = "Quantité : " + result.Quantité.ToString("N0");
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
            try
            {
                AddColumns.Addcolumn(dataGridView1);
                dataGridView1.Columns["Edit"].Visible = false;
                result = null;
            }
            catch (Exception ex)
            {
                result = null;
            }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
        }

        private Task<MyTable> FillSearchAsync(string search) => Task.Factory.StartNew((() => FillSearch(search)));

        private MyTable FillSearch(string search)
        {
            using (var donnée = new QuitayeContext())
            {
                MyTable myTable = new MyTable();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id", typeof(int));
                dataTable.Columns.Add("Code_Barre");
                dataTable.Columns.Add("Désignation");
                dataTable.Columns.Add("Quantité", typeof(Decimal));
                dataTable.Columns.Add("Mesure");
                dataTable.Columns.Add("Filiale");
                dataTable.Columns.Add("Date", typeof(DateTime));
                dataTable.Columns.Add("Auteur");

                var source = (from d in donnée.tbl_ravitaillement 
                              where (d.Date.Value.Date <= EndDate.Value && d.Date.Value >= startDate.Value.Date) 
                              && (d.Taille.Contains(search) 
                              || d.Marque.Contains(search) || d.Code_Barre.Equals(search))
                              join p in donnée.tbl_produits on d.Code_Barre equals p.Barcode into joinedTable
                              from f in joinedTable.DefaultIfEmpty()
                              select new 
                              {
                                Id = d.Id,
                                Marque = d.Marque,
                                Code = d.Code_Barre,
                                Catégorie = d.Catégorie,
                                Taille = d.Taille,
                                Quantité = d.Quantité,
                                Filiale = d.Fiaile,
                                Date = d.Date,
                                Auteur = d.Auteur,
                                Mesure = d.Mesure,
                                Type = f.Type
                            });
                myTable.Quantité = Convert.ToDecimal(source.Sum(x => x.Quantité));
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Code;
                    row[2] = data.Marque + "-" + data.Taille + ", " + data.Catégorie;
                    row[3] = data.Quantité;
                    row[4] = data.Mesure;
                    row[5] = data.Filiale;
                    row[6] = data.Date;
                    row[7] = data.Auteur;
                    dataTable.Rows.Add(row);
                }
                myTable.Table = dataTable;
                return myTable;
            }
        }

        private async void StartDate_ValueChanged(object sender, EventArgs e) => await CallData();

        private async void BtnAjouter_Click(object sender, EventArgs e)
        {
            Ajout_Filiale détachement = new Ajout_Filiale();
            détachement.ShowDialog();
            if (!(détachement.ok == "Oui"))
            {
                détachement = null;
            }
            else
            {
                await CallTask();
                détachement = null;
            }
        }

        private async void btnExcel_Click(object sender, EventArgs e)
        {
            //Print.PrintExcelFile(dataGridView1, "Ravitaillement Filiale", name, "Quitaye School");
            var file = "C:/Quitaye School/Ravitaillement Filiale " + startDate.Value.Date.ToString("dd-MM-yyyy") + EndDate.Value.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            //PrintAction.Print.PrintExcelFile(dataGridView2, "Rapport Commane(s)", name, "Quitaye School");
            Alert.SShow("Génération Barcode en cours.. Veillez-patientez !", Alert.AlertType.Info);
            await Task.Run(() => Quitaye_School.Models.Docuement.ExportToExcel(dataGridView1, file));
            System.Diagnostics.Process.Start(file);
            Alert.SShow("Génération effectué avec succès.", Alert.AlertType.Sucess);
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            string[] strArray = new string[6];
            strArray[0] = "Ravitaillement Filiale ";
            DateTime date1 = startDate.Value;
            date1 = date1.Date;
            strArray[1] = date1.ToString("dd-MM-yyyy");
            strArray[2] = " ";
            DateTime date2 = EndDate.Value;
            date2 = date2.Date;
            strArray[3] = date2.ToString("dd-MM-yyyy");
            strArray[4] = " ";
            strArray[5] = DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            name = string.Concat(strArray);
            Print.PrintPdfFile(dataGridView1, name, "Ravitaillement Filiale", nameof(Ravitaillement), "Stock Ravitaillé", LogIn.mycontrng, "Quitaye School", true);
        }

        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Num_Transfère") 
                || !dataGridView1.Columns[e.ColumnIndex].Name.Equals("Sup"))
                return;
            MsgBox msg = new MsgBox();
            msg.show("Voulez-vous supprimer cet element ?", "Suppression", 
                MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
            int num = (int)msg.ShowDialog();
            if (msg.clicked == "Non")
                return;
            if (msg.clicked == "Oui")
            {
                if (await SuppressionTransfèreAsync(id))
                {
                    Alert.SShow("Element supprimé avec succès.", Alert.AlertType.Sucess);
                    await CallData();
                }
                else
                    Alert.SShow("Element non supprimé", Alert.AlertType.Info);
            }
            msg = (MsgBox)null;
        }

        private async Task<bool> SuppressionTransfèreAsync(int id)
        {
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_ravitaillement.Where(d => d.Id == id).Select(d => new
                {
                    Id = d.Id,
                    Date = d.Date
                });
                if (source.Count() == 0 || !(source.First().Date.Value.AddDays(24.0) >= DateTime.Now))
                    return false;

                
                var dese = financeDataContext.tbl_ravitaillement.Where(d => d.Id == id).First();
                financeDataContext.tbl_ravitaillement.Remove(dese);
                var tblMesureVente = financeDataContext.tbl_mesure_vente
                        .Where(d => d.Nom == dese.Mesure).First();
                int? niveau1 = tblMesureVente.Niveau;
                
                var stock = financeDataContext.tbl_stock_produits_vente.Where(d => d.Product_Id == dese.Product_Id 
                && d.Detachement == dese.Fiaile).First();
                var formuleMesureVente = financeDataContext
                        .tbl_formule_mesure_vente.Where(d => d.Id == stock.Formule).First();
                if (niveau1.GetValueOrDefault() == 1 & niveau1.HasValue)
                {
                    decimal quantité2 = Convert.ToDecimal(dese.Quantité);
                    stock.Quantité -= quantité2;
                    
                }else
                {
                    if(niveau1 == 2 & niveau1.HasValue)
                    {
                        decimal num5 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Moyen);
                        var quantité3 = stock.Quantité;
                        var new_qté = Convert.ToDecimal(dese.Quantité) * num5;
                        stock.Quantité -= new_qté;
                    }else if(niveau1 == 3 & niveau1.HasValue)
                    {
                        decimal num5 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Grand);
                        var quantité3 = stock.Quantité;
                        var new_qté = Convert.ToDecimal(dese.Quantité) * num5;
                        stock.Quantité -= new_qté;
                    }
                    else if(niveau1 == 4 & niveau1.HasValue)
                    {
                        decimal num5 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Large);
                        var quantité3 = stock.Quantité;
                        var new_qté = Convert.ToDecimal(dese.Quantité) * num5;
                        stock.Quantité -= new_qté;
                    }
                    else if(niveau1 == 5 && niveau1.HasValue)
                    {
                        decimal num5 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Hyper_Large);
                        var quantité3 = stock.Quantité;
                        var new_qté = Convert.ToDecimal(dese.Quantité) * num5;
                        stock.Quantité -= new_qté;
                    }
                }
                
                if(stock.Quantité < 0)
                {
                    stock.Quantité = 0;
                }
                var historique = financeDataContext
                    .tbl_historique_valeur_stock.Where(x => x.Code_Barre == stock.Code_Barre
                    && x.Filiale == stock.Detachement
                    && DbFunctions.TruncateTime(x.Date.Value) == DbFunctions.TruncateTime(dese.Date.Value)).FirstOrDefault();
                if (historique != null)
                {
                    historique.Quantité -= stock.Quantité;
                    if(historique.Quantité  <= 0)
                    {
                        historique.Quantité = 0;
                    }
                }
                var hist = await financeDataContext
                    .tbl_historique_expiration.Where(x => x.Id_Opération == dese.Id 
                    && x.Type == "Ravitaillement" && DbFunctions.TruncateTime(x.Date.Value) == DbFunctions.TruncateTime(dese.Date.Value)).ToListAsync();

                foreach (var item in hist)
                {
                    var expiration = financeDataContext.tbl_expiration.Where(x => DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(item.Date_Expiration)
                    && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(item.Date)
                    && x.Code_Barre == stock.Code_Barre).FirstOrDefault();
                    financeDataContext.tbl_expiration.Remove(expiration);
                }

                financeDataContext.tbl_historique_expiration.RemoveRange(hist);

                await financeDataContext.SaveChangesAsync();
                return true;
            }
        }

        private async void BtnTransfère_Click(object sender, EventArgs e)
        {
            if (!(cbxDetachementEnvoie.Text != ""))
                return;
            RavitaillementStock transfère = new RavitaillementStock(cbxDetachementEnvoie.Text);
            int num = (int)transfère.ShowDialog();
            if (transfère.ok == "Oui")
                await CallTask();
            transfère = (RavitaillementStock)null;
        }

        private void BtnFermer_Click(object sender, EventArgs e) => Close();

        private async void LoadTime_Tick(object sender, EventArgs e)
        {
            loadTime.Stop();
            await CallTask();
        }

        private async Task CallTask()
        {
            Task<MyTable> filldata = FillDataAsync();
            Task<DataTable> fillenvoie = FillCbxDétachementAsync();
            Task<DataTable> fillereception = FillCbxDétachementAsync();
            List<Task> taskList = new List<Task>()
      {
         filldata,
         fillenvoie,
         fillereception
      };
            while (taskList.Count > 0)
            {
                Task finishedTask = await Task.WhenAny((IEnumerable<Task>)taskList);
                if (finishedTask == fillenvoie)
                {
                    cbxDetachementEnvoie.DataSource = fillenvoie.Result;
                    cbxDetachementEnvoie.DisplayMember = "Nom";
                    cbxDetachementEnvoie.ValueMember = "Id";
                    cbxDetachementEnvoie.Text = null;
                    cbxDetachementEnvoie.Visible = true;
                }
                else if (finishedTask == filldata)
                {
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = filldata.Result.Table;
                    try
                    {
                        AddColumns.Addcolumn(dataGridView1);
                        dataGridView1.Columns["edit"].Visible = false;
                    }
                    catch (Exception ex1)
                    {
                        Exception ex = ex1;
                    }
                }
                taskList.Remove(finishedTask);
                finishedTask = null;
            }
            filldata = null;
            fillenvoie = (Task<DataTable>)null;
            fillereception = (Task<DataTable>)null;
            taskList = null;
        }

        private async Task CallData()
        {
            MyTable result = await FillDataAsync();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = result.Table;
            try
            {
                AddColumns.Addcolumn(dataGridView1);
                dataGridView1.Columns["edit"].Visible = false;
                result = null;
            }
            catch (Exception ex1)
            {
                Exception ex = ex1;
                result = null;
            }
        }

        private Task<MyTable> FillDataAsync() => Task.Factory.StartNew((() => FillData()));

        private MyTable FillData()
        {
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
            try
            {
                MyTable myTable = new MyTable();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id", typeof(int));
                dataTable.Columns.Add("Code_Barre");
                dataTable.Columns.Add("Désignation");
                dataTable.Columns.Add("Quantité", typeof(Decimal));
                dataTable.Columns.Add("Mesure");
                dataTable.Columns.Add("Filiale");
                dataTable.Columns.Add("Date", typeof(DateTime));
                dataTable.Columns.Add("Auteur");
                using (var financeDataContext = new QuitayeContext())
                {
                    var source = (from d in financeDataContext.tbl_ravitaillement
                                  where DbFunctions.TruncateTime(d.Date) >= DbFunctions.TruncateTime(startDate.Value)
                                && DbFunctions.TruncateTime(d.Date) <= DbFunctions.TruncateTime(EndDate.Value)
                                  join p in financeDataContext.tbl_produits
                                  on d.Product_Id equals p.Id into joinedTable
                                  from f in joinedTable.DefaultIfEmpty()
                                  select new
                                  {
                                      Id = d.Id,
                                      Marque = d.Marque,
                                      Code = d.Code_Barre,
                                      Catégorie = d.Catégorie,
                                      Taille = d.Taille,
                                      Quantité = d.Quantité,
                                      Filiale = d.Fiaile,
                                      Date = d.Date,
                                      Auteur = d.Auteur,
                                      Mesure = d.Mesure,
                                      Type = f.Type,
                                  });
                    myTable.Quantité = Convert.ToDecimal(source.Sum(x => x.Quantité));
                    foreach (var data in source)
                    {
                        DataRow row = dataTable.NewRow();
                        row[0] = data.Id;
                        row[1] = data.Code;
                        if (data.Type == null)
                            row[2] = data.Marque + "-" + data.Taille + ", " + data.Catégorie;
                        else row[2] = data.Marque + "-" + data.Taille + ", " + data.Catégorie + "_" + data.Type;
                        row[3] = data.Quantité;
                        row[4] = data.Mesure;
                        row[5] = data.Filiale;
                        row[6] = data.Date;
                        row[7] = data.Auteur;
                        dataTable.Rows.Add(row);
                    }
                    myTable.Table = dataTable;
                }
                return myTable;
            }
            catch (Exception ex)
            {
                return null;
            }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
        }

        private Task<DataTable> FillCbxDétachementAsync() => Task.Factory.StartNew(() => FillCbxDétachement());

        private DataTable FillCbxDétachement()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Nom");
            using (var donnée = new QuitayeContext())
            {
                var source = from d in donnée.tbl_filiale
                             orderby d.Nom 
                             select new {Id = d.Id, Nom = d.Nom};
               
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Nom;
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }
    }
}
