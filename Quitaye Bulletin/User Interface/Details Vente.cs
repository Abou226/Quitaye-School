using PrintAction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quitaye_School.Models;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using Quitaye_School.Models.Context;
using System.Data.Entity;
using Quitaye_Medical.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Windows;

namespace Quitaye_School.User_Interface
{
    public partial class Details_Vente : Form
    {

#pragma warning disable CS0169 // Le champ 'Details_Vente.type' n'est jamais utilisé
        private string type;
#pragma warning restore CS0169 // Le champ 'Details_Vente.type' n'est jamais utilisé
        public int id_client;
        private Timer loadTimer = new Timer();
        public static string ok;
        private Timer filename = new Timer();
        private static string name;
        private static string client;
        private static string contact;
        private static string num_vente;
        private SearchedTable result = new SearchedTable();

        public Details_Vente(string _num_vente, int width = 1169, int heigth = 600)
        {
            InitializeComponent();
            btnFermer.Click += BtnFermer_Click;
            Details_Vente.num_vente = _num_vente;
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            btnImprimerFacture.Click += BtnImprimerFacture_Click;
            btnAjouter.Click += BtnAjouter_Click;
            btnAddPayement.Click += BtnAddPayement_Click;
            btnValider.Click += BtnValider_Click;
            txtmontant.TextChanged += Txtmontant_TextChanged;
            txtmontant.KeyPress += Txtmontant_KeyPress;
            btnClotureEcriture.Click += BtnClotureEcriture_Click;
            btnSelectionTout.Click += BtnSelectionTout_Click;
            dataGridView1.CellClick += dataGridView1_CellClick;
            this.Size = new System.Drawing.Size(width, heigth);
            txtSearch.TextChanged += TxtSearch_TextChanged;
            btnExcel.Click += BtnExcel_Click1;
            btnRetourner.Click += BtnRetourner_Click;
            lblNum_Operation.Text = "N_Opération:" + num_vente;
        }

        private async void BtnRetourner_Click(object sender, EventArgs e)
        {
            AchatVente vente = new AchatVente(num_vente, client, contact, "Vente", true);
            vente.panel2.Visible = true;
            vente.panel6.Visible = true;
            vente.panel4.Visible = true;
            vente.panel5.Visible = true;
            vente.btnFermer.Visible = true;
            vente.ShowDialog();
            if (!(ok == "Oui"))
            {
                vente = null;
            }
            else
            {
                ok = "Oui";
                ok = null;
                await CallData();
                vente = null;
            }
        }

        private async void BtnExcel_Click1(object sender, EventArgs e)
        {
            //name = $"Rapport Vente {num_vente} {DateTime.Now.Date.ToString("dd-MM-yyyy HH.mm.ss")}.xlsx";
            //Print.PrintExcelFile(dataGridView2, "Rapport " + num_vente, name, "Quitaye School");
            string file = $"C:/Quitaye School/Rapport Vente {num_vente} {DateTime.Now.Date.ToString("dd-MM-yyyy HH.mm.ss")}.xlsx";
            //PrintAction.Print.PrintExcelFile(dataGridView2, "Rapport Commane(s)", name, "Quitaye School");
            Alert.SShow("Génération Barcode en cours.. Veillez-patientez !", Alert.AlertType.Info);
            await Task.Run(() => Quitaye_School.Models.Docuement.ExportToExcel(this.dataGridView2, file));
            System.Diagnostics.Process.Start(file);
            Alert.SShow("Génération effectué avec succès.", Alert.AlertType.Sucess);
        }

        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
                await CallSearchData(txtSearch.Text);
            else await CallData();
        }

        private async Task CallSearchData(string search)
        {
            SearchedTable searchedTable = await FillSearchDataAsync(search);
            result = searchedTable;
            searchedTable = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = result.MyTable;
            dataGridView2.DataSource = result.MyTable;

            lblMontant.Text = "Payée : " + result.Payement.ToString("N0") + " FCFA, Réduction : " + result.Reduction.ToString("N0") + ", Restant : " + (result.Montant - result.Payement).ToString("N0") + " FCFA";
            lblTitre.Text = "Client : " + result.Client;
            lblNum_Operation.Text = "N° Opération : " + num_vente;
            lblNum_Operation.Visible = true;
            lblTitre.Visible = true;
            lblMontant.Visible = true;
            try
            {
                if (Principales.type_compte.Contains("Administrateur"))
                {
                    var selection = new DataGridViewCheckBoxColumn();
                    selection.Name = "Select";
                    selection.HeaderText = "Select";
                    selection.Width = 40;

                    dataGridView1.Columns.Add(selection);
                    AddColumns.Addcolumn(dataGridView1);
                    //dataGridView1.Columns["Id"].Width = 40;
                    //dataGridView1.Columns["Barcode"].Width = 70;
                    dataGridView1.Columns["Quantité"].Width = 70;
                    dataGridView1.Columns["Prix_Unité"].Width = 90;
                    dataGridView1.Columns["Select"].Width = 50;
                    dataGridView1.Columns["Sup"].Width = 40;
                    dataGridView1.Columns["Montant"].Width = 120;
                    dataGridView1.Columns["Edit"].Visible = false;
                }
            }
            catch (Exception)
            {

            }
        }

        public static Task<SearchedTable> FillSearchDataAsync(string search) => Task.Factory.StartNew(() => FillSearchData(search));

        public static SearchedTable FillSearchData(string text)
        {
            SearchedTable searchedTable = new SearchedTable();
            DataTable dataTable = new DataTable();
            //dataTable.Columns.Add("Id", typeof(int));
            //dataTable.Columns.Add("Barcode");
            dataTable.Columns.Add("Désignation");
            dataTable.Columns.Add("Quantité");
            dataTable.Columns.Add("Prix_Unité");
            dataTable.Columns.Add("Montant");
            using (var financeDataContext = new QuitayeContext())
            {
                var source1 = from d in financeDataContext.tbl_vente
                              where d.Num_Vente == num_vente
                              join p in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals p.Product_Id into joinedTable
                              from f in joinedTable.DefaultIfEmpty()
                              orderby f.Marque
                              where (d.Catégorie.Contains(text) || d.Dept_Auteur.Contains(text)
                                  || d.Taille.Contains(text) || d.Produit.Contains(text)
                                  || d.Barcode.ToLower().Equals(text.ToLower()) || d.Usage.Contains(text)
                                  || d.Num_Client.Contains(text) || d.Filiale.Contains(text) || d.Type.Contains(text))
                              select new
                              {
                                  Id = d.Id,
                                  Montant = d.Montant,
                                  Quantité = d.Q_Unité,
                                  Barcode = d.Barcode,
                                  Catégorie = d.Catégorie,
                                  Num_Client = d.Num_Client,
                                  Prix_Unité = d.Prix_Unité,
                                  Taille = d.Taille,
                                  Produit = d.Produit,
                                  Client = d.Client,
                                  Type = f.Type,
                                  Id_Client = d.Id_Client,
                                  Reduction = d.Reduction
                              };
                var source2 = financeDataContext.tbl_payement.Where((d => d.Num_Opération == Details_Vente.num_vente)).Select(d => new
                {
                    Id = d.Id,
                    Montant = d.Montant,
                    Reduction = d.Réduction
                });
                searchedTable.Payement = Convert.ToDecimal(source2.Sum(x => x.Montant));
                searchedTable.Qté = Convert.ToDecimal(source1.Sum(x => x.Quantité));
                searchedTable.Montant = Convert.ToDecimal(source1.Sum(x => x.Montant));
                searchedTable.Reduction = Convert.ToDecimal(source1.Sum(x => x.Reduction));
                foreach (var item in source1)
                {
                    int? idClient = item.Id_Client;
                    int num1;
                    if (idClient.HasValue)
                    {
                        idClient = item.Id_Client;
                        int num2 = 0;
                        num1 = !(idClient.GetValueOrDefault() == num2 & idClient.HasValue) ? 1 : 0;
                    }
                    else
                        num1 = 0;
                    if (num1 != 0)
                    {
                        searchedTable.Id_Client = Convert.ToInt32(item.Id_Client);
                        break;
                    }
                }
                foreach (var item in source1)
                {
                    Details_Vente.client = item.Client;
                    Details_Vente.contact = item.Num_Client;
                    DataRow row = dataTable.NewRow();
                    //row["Id"] = item.Id;
                    //row["Barcode"] = item.Barcode;
                    if (!string.IsNullOrEmpty(item.Type))
                        row["Désignation"] = item.Produit + " " + item.Taille + " " + item.Catégorie + " " + item.Type;
                    else
                        row["Désignation"] = item.Produit + " " + item.Taille + " " + item.Catégorie;
                    row["Quantité"] = Convert.ToDecimal(item.Quantité).ToString("N0");
                    row["Prix_Unité"] = Convert.ToDecimal(item.Prix_Unité).ToString("N0");
                    row["Montant"] = Convert.ToDecimal(item.Montant).ToString("N0");
                    dataTable.Rows.Add(row);
                }

                var nRow = dataTable.NewRow();
                nRow["Désignation"] = "Total";
                nRow["Quantité"] = Convert.ToDecimal(source1.Sum(x => x.Quantité)).ToString("N0");
                nRow["Prix_Unité"] = Convert.ToDecimal(source1.Sum(x => x.Prix_Unité)).ToString("N0");
                nRow["Montant"] = Convert.ToDecimal(source1.Sum(x => x.Montant)).ToString("N0");
                dataTable.Rows.Add(nRow);

                var nnRow = dataTable.NewRow();
                nnRow["Désignation"] = "Réduction";
                //nnRow["Quantité"] = Convert.ToDecimal(source1.Sum(x => x.Quantité)).ToString("N0");
                //nnRow["Prix_Unité"] = Convert.ToDecimal(source1.Sum(x => x.Prix_Unité)).ToString("N0");
                nnRow["Montant"] = Convert.ToDecimal(source1.Sum(x => x.Reduction)).ToString("N0");
                dataTable.Rows.Add(nnRow);
                foreach (var data in source1)
                {
                    if (data.Client != "" && data.Client != null)
                    {
                        Details_Vente.client = data.Client;
                        searchedTable.Client = data.Client;
                        break;
                    }
                }
            }
            searchedTable.MyTable = dataTable;
            return searchedTable;
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns.Count >= 2)
                if (e.ColumnIndex >= 0)
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Sup") == true)
                    {
                        if (!string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells["Id"].Value.ToString()))
                        {
                            //if (Principales.type_compte.Contains("Administrateur"))
                            //{
                            //    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                            //    MsgBox msg = new MsgBox();
                            //    msg.show("Voulez-vous supprimer cette vente ?", "Suppression",
                            //        MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                            //    msg.ShowDialog();
                            //    if (msg.clicked == "Non")
                            //        return;
                            //    else if (msg.clicked == "Oui")
                            //    {
                            //        using (var donnée = new QuitayeContext())
                            //        {
                            //            var v = (from d in donnée.tbl_vente where d.Id == id select d).First();
                            //            if (v.Cloturé != "Oui")
                            //            {
                            //                //if(v.Date_Vente.Value.AddHours(168) >= DateTime.Now)
                            //                {
                            //                    var hist = donnée.tbl_historique_expiration
                            //                                    .Where(x => x.Id_Opération == v.Id && x.Num_Opération == v.Num_Vente).ToList();

                            //                    foreach (var item in hist)
                            //                    {
                            //                        var expiration = donnée.tbl_expiration.Where(x => DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(item.Date_Expiration)
                            //                        && x.Code_Barre == v.Barcode).FirstOrDefault();
                            //                        expiration.Reste += item.Quantité;
                            //                    }
                            //                    donnée.tbl_vente.Remove(v);
                            //                    donnée.tbl_historique_expiration.RemoveRange(hist);

                            //                    var stock = new Models.Context.tbl_stock_produits_vente();
                            //                    if (v.Filiale == null || v.Filiale == "Siège" || v.Filiale == "")
                            //                    {
                            //                        stock = (from d in donnée.tbl_stock_produits_vente
                            //                                 where d.Code_Barre == v.Barcode
                            //                                 && d.Detachement == "Siège"
                            //                                 select d).First();
                            //                    }
                            //                    else stock = (from d in donnée.tbl_stock_produits_vente
                            //                                  where d.Code_Barre == v.Barcode
                            //                                  && d.Detachement == v.Filiale
                            //                                  select d).First();
                            //                    var ms = (from d in donnée.tbl_mesure_vente
                            //                              where d.Nom == v.Mesure
                            //                              select d).First();

                            //                    var formu = (from d in donnée.tbl_formule_mesure_vente
                            //                                 where d.Id == stock.Formule
                            //                                 select d).First();

                            //                    if (ms.Niveau == 1)
                            //                    {
                            //                        stock.Quantité += v.Quantité;
                            //                    }
                            //                    else if (ms.Niveau == 2)
                            //                    {
                            //                        decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Moyen);
                            //                        stock.Quantité += Convert.ToDecimal(v.Quantité) * unité;
                            //                    }
                            //                    else if (ms.Niveau == 3)
                            //                    {
                            //                        decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Grand);
                            //                        stock.Quantité += Convert.ToDecimal(v.Quantité) * unité;
                            //                    }
                            //                    else if (ms.Niveau == 4)
                            //                    {
                            //                        decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Large);
                            //                        stock.Quantité += Convert.ToDecimal(v.Quantité) * unité;
                            //                    }
                            //                    else if (ms.Niveau == 5)
                            //                    {
                            //                        decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Hyper_Large);
                            //                        stock.Quantité += Convert.ToDecimal(v.Quantité) * unité;
                            //                    }

                            //                    var historique = donnée.tbl_historique_valeur_stock.Where(x => x.Code_Barre == stock.Code_Barre
                            //                    && x.Filiale == stock.Detachement && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(v.Date_Vente)).FirstOrDefault();
                            //                    if (historique != null)
                            //                    {
                            //                        historique.Quantité = stock.Quantité;
                            //                    }

                            //                    var evolution = donnée.tbl_historique_evolution_stock.Where(x => x.Barcode == stock.Code_Barre
                            //                    && DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(v.Date_Expiration)
                            //                    && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(v.Date_Vente)).FirstOrDefault();
                            //                    evolution.Quantité += v.Quantité;
                            //                    var evo_list = donnée.tbl_historique_evolution_stock.Where(x => x.Barcode == stock.Code_Barre
                            //                    && DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(v.Date_Expiration)
                            //                    && DbFunctions.TruncateTime(x.Date) > DbFunctions.TruncateTime(v.Date_Vente)).ToList();
                            //                    foreach (var evo in evo_list)
                            //                    {
                            //                        evo.Quantité += v.Quantité;
                            //                    }
                            //                    await donnée.SaveChangesAsync();
                            //                    await CallData();
                            //                    Alert.SShow("Vente supprimeé avec succès.", Alert.AlertType.Sucess);
                            //                }
                            //            }
                            //            else Alert.SShow("Suppression impossible, écriture cloturée", Alert.AlertType.Info);
                            //        }
                            //    }
                            //}
                        }
                        //string auteur = dataGridView1.CurrentRow.Cells["Auteur"].Value.ToString();
                    }
                }
        }


        private void BtnSelectionTout_Click(object sender, EventArgs e)
        {
            if (btnSelectionTout.Text == "Tout Selectionner")
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    item.Cells["Select"].Value = true;
                }

                btnSelectionTout.Text = "Tout Deselectionner";
            }
            else if (btnSelectionTout.Text == "Tout Deselectionner")
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    item.Cells["Select"].Value = false;
                }

                btnSelectionTout.Text = "Tout Selectionner";
            }
        }

        private async void BtnClotureEcriture_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["Select"].Value);
                if (isSelected)
                {
                    if (!string.IsNullOrEmpty(row.Cells["Id"].Value.ToString()))
                    {
                        list.Add(Convert.ToInt32(row.Cells["Id"].Value));
                    }
                }
            }

            if (list.Count > 0)
            {
                var msg = new MsgBox();
                msg.show($"Voulez-vus réellement cloturé ce(s) {list.Count} élément(s) ?", "Cloture écriture",
                    MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                msg.ShowDialog();
                if (msg.clicked == "Oui")
                {
                    var result = await VenteRangeClotureAsync(list);
                    if (result)
                    {
                        Alert.SShow("Cloture éffectué avec succès.", Alert.AlertType.Sucess);
                    }
                }
            }
        }

        private async Task<bool> VenteSingleClotureAsync(int id)
        {
            using (var donnée = new QuitayeContext())
            {
                var item = donnée.tbl_vente.Where(x => x.Id == id).FirstOrDefault();
                item.Cloturé = "Oui";
                await donnée.SaveChangesAsync();
                return true;
            }
        }

        private async Task<bool> VenteRangeClotureAsync(List<int> ids)
        {
            using (var donnée = new QuitayeContext())
            {
                var items = donnée.tbl_vente.Where(x => ids.Contains(x.Id)).ToList();
                foreach (var item in items)
                {
                    item.Cloturé = "Oui";
                }
                await donnée.SaveChangesAsync();
                return true;
            }
        }


        private void Txtmontant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '\b')
                return;
            e.Handled = true;
        }

        private void Txtmontant_TextChanged(object sender, EventArgs e)
        {
            if (!(txtmontant.Text != "") || !(txtmontant.Text != "-"))
                return;
            txtmontant.Text = Convert.ToDecimal(txtmontant.Text).ToString("N0");
            txtmontant.SelectionStart = txtmontant.Text.Length;
        }

        private async void BtnValider_Click(object sender, EventArgs e)
        {
            if (!(txtmontant.Text != "") || !(cbxPayement.Text != ""))
                return;
            string result = Regex.Replace(txtmontant.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
            Payement payement = new Payement()
            {
                Montant = Convert.ToDecimal(result),
                Client = Details_Vente.client,
                Date = DateOpération.Value,
                Reference = txtReference.Text,
                Mode_Payement = cbxPayement.Text
            };
            payement.Client = Details_Vente.client;
            payement.Id_Client = id_client;
            payement.Num_Opération = Details_Vente.num_vente;
            payement.Type = "Encaissement";
            payement.Raison = "Client";
            if (await SavePayementAsync(payement))
            {
                txtmontant.Text = null;
                txtReference.Text = null;
                await CallData();
                Alert.SShow("Payement enregistré avec succès.", Alert.AlertType.Sucess);
            }
            result = null;
            payement = (Payement)null;
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallTask();
        }

        private async Task CallTask()
        {
            Task<MesureTable> mode = Details_Vente.FillModeAsync();
            Task<SearchedTable> data = Details_Vente.FillDataAsync();
            List<Task> tasklist = new List<Task>()
      {
         mode,
         data
      };
            while (tasklist.Count > 0)
            {
                Task finishedTask = await Task.WhenAny((IEnumerable<Task>)tasklist);
                if (finishedTask == mode)
                {
                    cbxPayement.DataSource = mode.Result.Table;
                    cbxPayement.DisplayMember = "Mode";
                    cbxPayement.ValueMember = "Id";
                    if (mode.Result.RowsLists != null && mode.Result.RowsLists.Count > 0)
                    {
                        foreach (RowsList rowsList in mode.Result.RowsLists)
                        {
                            RowsList item = rowsList;
                            if (item.Default)
                            {
                                cbxPayement.Text = item.Mesure;
                                break;
                            }
                            cbxPayement.Text = null;
                            item = (RowsList)null;
                        }
                    }
                    else
                        cbxPayement.Text = null;
                }
                else if (finishedTask == data)
                {
                    result = data.Result;

                    dataGridView1.DataSource = data.Result.MyTable;
                    dataGridView2.DataSource = data.Result.MyTable;

                    lblMontant.Text = "Payée : " + result.Payement.ToString("N0") + " FCFA, Réduction : " + result.Reduction.ToString("N0") + ", Restant : " + (result.Montant - result.Payement).ToString("N0") + " FCFA";
                    id_client = data.Result.Id_Client;
                    lblTitre.Text = "Client : " + data.Result.Client;
                    lblNum_Operation.Text = "N° Opération : " + Details_Vente.num_vente;
                    lblNum_Operation.Visible = true;
                    lblTitre.Visible = true;
                    lblMontant.Visible = true;

                    try
                    {
                        if (Principales.type_compte.Contains("Administrateur"))
                        {
                            var selection = new DataGridViewCheckBoxColumn();
                            selection.Name = "Select";
                            selection.HeaderText = "Select";
                            selection.Width = 40;

                            dataGridView1.Columns.Add(selection);
                            AddColumns.Addcolumn(dataGridView1);
                            //dataGridView1.Columns["Id"].Width = 40;
                            //dataGridView1.Columns["Barcode"].Width = 70;
                            dataGridView1.Columns["Quantité"].Width = 70;
                            dataGridView1.Columns["Prix_Unité"].Width = 90;
                            dataGridView1.Columns["Select"].Width = 50;
                            dataGridView1.Columns["Sup"].Width = 40;
                            dataGridView1.Columns["Montant"].Width = 120;
                            dataGridView1.Columns["Edit"].Visible = false;
                        }

                    }
                    catch (Exception)
                    {

                    }

                }
                tasklist.Remove(finishedTask);
                finishedTask = null;
            }
            mode = (Task<MesureTable>)null;
            data = (Task<SearchedTable>)null;
            tasklist = null;
        }

        private async void BtnAddPayement_Click(object sender, EventArgs e)
        {
            Ajouter_Element element = new Ajouter_Element("Mode");
            int num = (int)element.ShowDialog();
            if (!(element.ok == "Oui"))
            {
                element = null;
            }
            else
            {
                await CallMode();
                element = null;
            }
        }

        private async Task<bool> SavePayementAsync(Payement payement)
        {
            using (var financeDataContext = new QuitayeContext())
            {
                var source1 = financeDataContext.tbl_payement.Select(d => new
                {
                    Id = d.Id
                }).Take(1);
                int num = 1;
                if (source1.Count() != 0)
                {
                    var data = financeDataContext.tbl_payement.OrderByDescending((d => d.Id)).Select(d => new
                    {
                        Id = d.Id
                    }).First();
                    num = data.Id + 1;
                }
                
                var entity = new Models.Context.tbl_payement()
                {
                    Id = num,
                    Num_Opération = payement.Num_Opération,
                    Client = payement.Client,
                    Montant = new Decimal?(payement.Montant),
                    Num_Client = payement.Num_Client
                };
                entity.Num_Opération = payement.Num_Opération;
                entity.Date_Payement = new DateTime?(payement.Date);
                entity.Date_Enregistrement = new DateTime?(DateTime.Now);
                entity.Auteur = Principales.profile;
                entity.Réduction = new Decimal?(payement.Reduction);
                entity.Raison = "Client";
                entity.Reference = payement.Reference;
                entity.Type = payement.Type;
                if (payement.Id_Client != 0)
                    entity.Compte_Tier = payement.Id_Client.ToString();
                entity.Mode_Payement = payement.Mode_Payement;
                entity.Nature = "Payement";
                entity.Commentaire = "Payement " + Details_Vente.client + " " + payement.Num_Opération;
                financeDataContext.tbl_payement.Add(entity);
                await financeDataContext.SaveChangesAsync();
                var source2 = financeDataContext.tbl_vente.Where((d => d.Num_Vente == payement.Num_Opération)).Select(d => new
                {
                    Id = d.Id,
                    Montant = d.Montant
                });
                var source3 = financeDataContext.tbl_payement.Where((d => d.Num_Opération == payement.Num_Opération)).Select(d => new
                {
                    Id = d.Id,
                    Montant = d.Montant
                });
                decimal? nullable1 = source2.Sum(x => x.Montant);
                decimal? nullable2 = source3.Sum(x => x.Montant);
                decimal? montant = entity.Montant;
                decimal? nullable3 = nullable2.HasValue & montant.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + montant.GetValueOrDefault()) : new Decimal?();
                if (nullable1.GetValueOrDefault() <= nullable3.GetValueOrDefault() 
                    & nullable1.HasValue & nullable3.HasValue)
                {
                    foreach (var data in source2)
                    {
                        var item = data;
                        financeDataContext.tbl_vente.Where((d => d.Id == item.Id)).First().Type = "Payée";
                        await financeDataContext.SaveChangesAsync();
                    }
                }
                else
                {
                    foreach (var data in source2)
                    {
                        var item = data;
                        var tblVente = financeDataContext.tbl_vente.Where((d => d.Id == item.Id)).First();
                        tblVente.Type = !tblVente.Type.Contains("A Crédit") ? "Restant" : "A Crédit";
                        await financeDataContext.SaveChangesAsync();
                    }
                }
                return true;
            }
        }

        private async Task CallMode()
        {
            MesureTable result = await Details_Vente.FillModeAsync();
            cbxPayement.DataSource = result.Table;
            cbxPayement.DisplayMember = "Mode";
            cbxPayement.ValueMember = "Id";
            if (result.RowsLists != null && result.RowsLists.Count > 0)
            {
                foreach (RowsList rowsList in result.RowsLists)
                {
                    RowsList item = rowsList;
                    if (item.Default)
                    {
                        cbxPayement.Text = item.Mesure;
                        break;
                    }
                    cbxPayement.Text = null;
                    item = null;
                }
                result = null;
            }
            else
            {
                cbxPayement.Text = null;
                result = null;
            }
        }

        public static Task<MesureTable> FillModeAsync() => Task.Factory.StartNew((() => Details_Vente.FillMode()));

        private static MesureTable FillMode()
        {
            MesureTable mesureTable = new MesureTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Mode");
            using (var financeDataContext = new QuitayeContext())
            {
                if (Principales.type_compte.Contains("Administrateur"))
                {
                    var source = financeDataContext.tbl_mode_payement.OrderByDescending((d => d.Id)).Select(d => new
                    {
                        Id = d.Id,
                        Mode = d.Mode,
                        Default = d.Defaut
                    });
                    if (source.Count() != 0)
                    {
                        mesureTable.RowsLists = new List<RowsList>();
                        foreach (var data in source)
                        {
                            RowsList rowsList = new RowsList();
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            row[1] = data.Mode;
                            if (data.Default == "Oui")
                            {
                                rowsList.Default = true;
                                rowsList.Mesure = data.Mode;
                            }
                            else
                                rowsList.Default = false;
                            mesureTable.RowsLists.Add(rowsList);
                            dataTable.Rows.Add(row);
                        }
                    }
                }
                else
                {
                    var source = financeDataContext.tbl_mode_payement.Where((d => d.Niveau == "Utilisateur" || d.Niveau == null)).OrderByDescending((d => d.Id)).Select(d => new
                    {
                        Id = d.Id,
                        Mode = d.Mode,
                        Default = d.Defaut
                    });
                    if (source.Count() != 0)
                    {
                        mesureTable.RowsLists = new List<RowsList>();
                        foreach (var data in source)
                        {
                            RowsList rowsList = new RowsList();
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            row[1] = data.Mode;
                            if (data.Default == "Oui")
                            {
                                rowsList.Default = true;
                                rowsList.Mesure = data.Mode;
                            }
                            else
                                rowsList.Default = false;
                            mesureTable.RowsLists.Add(rowsList);
                            dataTable.Rows.Add(row);
                        }
                    }
                }
            }
            mesureTable.Table = dataTable;
            return mesureTable;
        }

        private async void BtnAjouter_Click(object sender, EventArgs e)
        {
            AchatVente vente = new AchatVente(Details_Vente.num_vente, Details_Vente.client, Details_Vente.contact, "Vente");
            vente.panel2.Visible = true;
            vente.panel6.Visible = true;
            vente.panel4.Visible = true;
            vente.panel5.Visible = true;
            vente.btnFermer.Visible = true;
            int num = (int)vente.ShowDialog();
            if (!(ok == "Oui"))
            {
                vente = null;
            }
            else
            {
                Details_Vente.ok = "Oui";
                ok = null;
                await CallData();
                vente = null;
            }
        }

        private async void BtnImprimerFacture_Click(object sender, EventArgs e)
        {
            Details_Vente.name = "Facture " + Details_Vente.client + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            var pdf = new Custom_Pdf(name, new float[] { 60f, 10f, 10f, 10f });
            await pdf.PrintFactureToPdfAsync(dataGridView2, "Facture : " + Details_Vente.num_vente, "N° Opération", Details_Vente.num_vente, new Detail_Facture()
            {
                MontantTTC = result.Montant - result.Reduction,
                Taxe = result.Montant / 100M * 18M,
                MontantHT = result.Montant-result.Reduction
            });
        }

        private void BtnFermer_Click(object sender, EventArgs e) => Close();

        private async Task CallData()
        {
            SearchedTable searchedTable = await FillDataAsync();
            result = searchedTable;
            searchedTable = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = result.MyTable;
            dataGridView2.DataSource = result.MyTable;


            lblMontant.Text = "Payée : " + result.Payement.ToString("N0") + " FCFA, Réduction : " + result.Reduction.ToString("N0") + ", Restant : " + (result.Montant - result.Payement).ToString("N0") + " FCFA";
            lblTitre.Text = "Client : " + result.Client;
            lblNum_Operation.Text = "N° Opération : " + Details_Vente.num_vente;
            lblNum_Operation.Visible = true;
            lblTitre.Visible = true;
            lblMontant.Visible = true;

            try
            {
                if (Principales.type_compte.Contains("Administrateur"))
                {
                    var selection = new DataGridViewCheckBoxColumn();
                    selection.Name = "Select";
                    selection.HeaderText = "Select";
                    selection.Width = 40;

                    dataGridView1.Columns.Add(selection);
                    AddColumns.Addcolumn(dataGridView1);
                    //dataGridView1.Columns["Id"].Width = 40;
                    //dataGridView1.Columns["Barcode"].Width = 70;
                    dataGridView1.Columns["Quantité"].Width = 70;
                    dataGridView1.Columns["Prix_Unité"].Width = 90;
                    dataGridView1.Columns["Select"].Width = 50;
                    dataGridView1.Columns["Sup"].Width = 40;
                    dataGridView1.Columns["Montant"].Width = 120;
                    dataGridView1.Columns["Edit"].Visible = false;
                }
            }
            catch (Exception)
            {

            }
        }

        public static Task<SearchedTable> FillDataAsync() => Task.Factory.StartNew((() => Details_Vente.FillData()));

        public static SearchedTable FillData()
        {
            SearchedTable searchedTable = new SearchedTable();
            DataTable dataTable = new DataTable();
            //dataTable.Columns.Add("Id", typeof(int));
            //dataTable.Columns.Add("Barcode");
            dataTable.Columns.Add("Désignation");
            dataTable.Columns.Add("Quantité");
            dataTable.Columns.Add("Prix_Unité");
            dataTable.Columns.Add("Montant");
            using (var financeDataContext = new QuitayeContext())
            {
                var source1 = from d in financeDataContext.tbl_vente
                              where d.Num_Vente == num_vente
                              join p in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals p.Product_Id into joinedTable
                              from f in joinedTable.DefaultIfEmpty()
                              orderby f.Marque
                              select new
                              {
                                  Id = d.Id,
                                  Montant = d.Montant,
                                  Quantité = d.Q_Unité,
                                  Barcode = d.Barcode,
                                  Catégorie = d.Catégorie,
                                  Num_Client = d.Num_Client,
                                  Prix_Unité = d.Prix_Unité,
                                  Taille = d.Taille,
                                  Produit = d.Produit,
                                  Client = d.Client,
                                  Type = f.Type,
                                  Id_Client = d.Id_Client,
                                  Reduction = d.Reduction
                              };
                var source2 = financeDataContext.tbl_payement.Where((d => d.Num_Opération == Details_Vente.num_vente)).Select(d => new
                {
                    Id = d.Id,
                    Montant = d.Montant,
                    Reduction = d.Réduction
                });
                searchedTable.Payement = Convert.ToDecimal(source2.Sum(x => x.Montant));
                searchedTable.Qté = Convert.ToDecimal(source1.Sum(x => x.Quantité));
                searchedTable.Montant = Convert.ToDecimal(source1.Sum(x => x.Montant));
                searchedTable.Reduction = Convert.ToDecimal(source1.Sum(x => x.Reduction));
                foreach (var item in source1)
                {
                    int? idClient = item.Id_Client;
                    int num1;
                    if (idClient.HasValue)
                    {
                        idClient = item.Id_Client;
                        int num2 = 0;
                        num1 = !(idClient.GetValueOrDefault() == num2 & idClient.HasValue) ? 1 : 0;
                    }
                    else
                        num1 = 0;
                    if (num1 != 0)
                    {
                        searchedTable.Id_Client = Convert.ToInt32(item.Id_Client);
                        break;
                    }
                }
                foreach (var item in source1)
                {
                    Details_Vente.client = item.Client;
                    Details_Vente.contact = item.Num_Client;
                    DataRow row = dataTable.NewRow();
                    //row["Id"] = item.Id;
                    //row["Barcode"] = item.Barcode;
                    if (!string.IsNullOrEmpty(item.Type))
                        row["Désignation"] = item.Produit + " " + item.Taille + " " + item.Catégorie + " " + item.Type;
                    else
                        row["Désignation"] = item.Produit + " " + item.Taille + " " + item.Catégorie;
                    row["Quantité"] = Convert.ToDecimal(item.Quantité).ToString("N0");
                    row["Prix_Unité"] = Convert.ToDecimal(item.Prix_Unité).ToString("N0");
                    row["Montant"] = Convert.ToDecimal(item.Montant).ToString("N0");
                    dataTable.Rows.Add(row);
                }
                var nRow = dataTable.NewRow();
                nRow["Désignation"] = "Montant Total";
                nRow["Quantité"] = Convert.ToDecimal(source1.Sum(x => x.Quantité)).ToString("N0");
                nRow["Prix_Unité"] = Convert.ToDecimal(source1.Sum(x => x.Prix_Unité)).ToString("N0");
                nRow["Montant"] = Convert.ToDecimal(source1.Sum(x => x.Montant)).ToString("N0");
                dataTable.Rows.Add(nRow);

                var nnRow = dataTable.NewRow();
                nnRow["Désignation"] = "Réduction";
                //nnRow["Quantité"] = Convert.ToDecimal(source1.Sum(x => x.Quantité)).ToString("N0");
                //nnRow["Prix_Unité"] = Convert.ToDecimal(source1.Sum(x => x.Prix_Unité)).ToString("N0");
                nnRow["Montant"] = Convert.ToDecimal(source1.Sum(x => x.Reduction)).ToString("N0");
                dataTable.Rows.Add(nnRow);

                foreach (var data in source1)
                {
                    if (data.Client != "" && data.Client != null)
                    {
                        Details_Vente.client = data.Client;
                        searchedTable.Client = data.Client;
                        break;
                    }
                }
            }
            searchedTable.MyTable = dataTable;
            return searchedTable;
        }
    }
}