using PrintAction;
using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using Quitaye_Medical.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Virement_Interne : Form
    {
        public string name;
        public Virement_Interne()
        {
            InitializeComponent();
            btnNouvelleOpération.Click += BtnNouvelleOpération_Click;
            startDate.Value = DateTime.Now.AddDays(-6);
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            dataGridView1.CellClick += DataGridView1_CellClick;
            startDate.ValueChanged += StartDate_ValueChanged;
            EndDate.ValueChanged += StartDate_ValueChanged;
            btnExcel.Click += BtnExcel_Click;
            btnPdf.Click += BtnPdf_Click;
        }

        private async void BtnExcel_Click(object sender, EventArgs e)
        {
            var file = $"C:/Quitaye School/Rapport Virement Interne {startDate.Value.Date.ToString("dd-MM-yyyy")}_{EndDate.Value.Date.ToString("dd-MM-yyyy")}_{DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")}";
            //PrintAction.Print.PrintExcelFile(dataGridView2, "Rapport Commane(s)", name, "Quitaye School");
            Alert.SShow("Génération Barcode en cours.. Veillez-patientez !", Alert.AlertType.Info);
            await Task.Run(() => Quitaye_School.Models.Docuement.ExportToExcel(dataGridView1, file));
            System.Diagnostics.Process.Start(file);
            Alert.SShow("Génération effectué avec succès.", Alert.AlertType.Sucess);
        }

        private void BtnPdf_Click(object sender, EventArgs e)
        {
            string[] strArray = new string[6];
            strArray[0] = "Rapport Virement Interne ";
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
            Print.PrintPdfFile(dataGridView1, name, "Rapport Virement Interne", "Opération(s)", "Inter-Compte", LogIn.mycontrng, "Quitaye School", false);
        }

        private async void StartDate_ValueChanged(object sender, EventArgs e)
        {
            await CallData();
        }

        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Principales.type_compte.Contains("Administrateur") || Principales.role == "Administrateur")
            {
                if (e.ColumnIndex >= 2)
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Sup") == true)
                    {
                        MsgBox msg = new MsgBox();
                        msg.show("Voulez-vous supprimer cet élément ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                        msg.ShowDialog();
                        if (msg.clicked == "Non")
                            return;
                        else if (msg.clicked == "Oui")
                        {
                            if (await DeleteDataAsync(id))
                            {
                                Alert.SShow("Element supprimé avec succès.", Alert.AlertType.Sucess);
                                await CallData();
                            }
                        }
                    }
                }
            }
        }

        private async Task<bool> DeleteDataAsync(int id)
        {
            using (var donnée = new QuitayeContext())
            {
                var der = (from d in donnée.tbl_payement where d.Id == id select d).First();
                donnée.tbl_payement.Remove(der);
                await donnée.SaveChangesAsync();
                return true;
            }
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallData();
        }

        Timer loadTimer = new Timer();

        private async void BtnNouvelleOpération_Click(object sender, EventArgs e)
        {
            Nouveau_Virement virement = new Nouveau_Virement();
            virement.ShowDialog();
            if (virement.ok == "Oui")
            {
                await CallData();
            }
        }

        private async Task CallData()
        {
            var result = await FillDataAsync();
            dataGridView1.Columns.Clear();
            if (result.Table.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
            }
            else
            {
                try
                {
                    dataGridView1.DataSource = result.Table;
                    AddColumns.Addcolumn(dataGridView1);
                    dataGridView1.Columns["Edit"].Visible = false;
                }
                catch (Exception)
                {

                }
            }
        }
        private Task<MyTable> FillDataAsync()
        {
            return Task.Factory.StartNew(() => FillData());
        }
        private MyTable FillData()
        {
            MyTable table = new MyTable();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Désignation");
            dt.Columns.Add("Mode_Opération");
            dt.Columns.Add("Montant", typeof(decimal));
            dt.Columns.Add("Num_Opération");
            dt.Columns.Add("Type");
            dt.Columns.Add("Nature");
            dt.Columns.Add("Date");
            dt.Columns.Add("Date_Enregistrement");
            dt.Columns.Add("Auteur");
            using (var donnée = new QuitayeContext())
            {
                var des = from d in donnée.tbl_payement
                          where (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value)) && (d.Nature == "Virement")
                          orderby d.Id descending
                          select new
                          {
                              Id = d.Id,
                              Désignation = d.Commentaire,
                              Mode_Payement = d.Mode_Payement,
                              Montant = d.Montant,
                              Num_Opération = d.Num_Opération,
                              Date = d.Date_Payement,
                              Type = d.Type,
                              Nature = d.Nature,
                              Date_Enregistrement = d.Date_Enregistrement,
                              Auteur = d.Auteur,
                              Date_Echance = d.Date_Echeance,
                          };
                table.Montant = Convert.ToDecimal(des.Where(x => x.Type == "Encaissement").Sum(x => x.Montant));
                table.Quantité = Convert.ToDecimal(des.Where(x => x.Type == "Décaissement").Sum(x => x.Montant));

                foreach (var item in des)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Désignation;
                    dr[2] = item.Mode_Payement;
                    dr[3] = item.Montant;
                    dr[4] = item.Num_Opération;
                    dr[5] = item.Type;
                    dr[6] = item.Nature;

                    dr[7] = item.Date.Value.ToString("dd/MM/yyyy");
                    dr[8] = item.Date_Enregistrement.Value.ToString("dd/MM/yyyy hh:mm:ss");
                    dr[9] = item.Auteur;

                    dt.Rows.Add(dr);
                }

                table.Table = dt;
                return table;
            }
        }
    }

}
