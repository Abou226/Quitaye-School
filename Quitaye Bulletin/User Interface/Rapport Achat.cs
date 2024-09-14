
using PrintAction;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Rapport_Achat : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Rapport_Achat()
        {
            InitializeComponent();
            types = "Achat";
            timer1.Start();
            timer2.Start();
            startDate.Value = DateTime.Today.AddDays(-3);
            btnImprimerFacture.Click += BtnImprimerFacture_Click;
            startDate.ValueChanged += StartDate_ValueChanged;
            EndDate.ValueChanged += StartDate_ValueChanged;
            cbxFiliale.SelectedIndexChanged += CbxFiliale_SelectedIndexChanged;
            cbxType.SelectedIndexChanged += cbxType_SelectedIndexChanged;
            btnPdf.Click += btnPDF_Click;
            btnExcel.Click += btnExcel_Click;
            dataGridView1.CellClick += dataGridView1_CellClick;
            txtsearch.TextChanged += txtsearch_TextChanged;
        }

        private async Task<bool> AchatRangeClotureAsync(List<int> ids)
        {
            using (var donnée = new QuitayeContext())
            {
                var items = donnée.tbl_arrivée.Where(x => ids.Contains(x.Id)).ToList();
                foreach (var item in items)
                {
                    item.Cloturé = "Oui";
                }
                await donnée.SaveChangesAsync();
                return true;
            }
        }

        private async void CbxFiliale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxFiliale.Text != "")
            {
                await CallSearchTask(cbxFiliale.Text);
            }
        }

        private async void StartDate_ValueChanged(object sender, EventArgs e)
        {
            await ShowData();
        }

        private void BtnImprimerFacture_Click(object sender, EventArgs e)
        {
            //if(types == "Achat")
            //{
            //    List<int> list = new List<int>();
            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        bool isSelected = Convert.ToBoolean(row.Cells["Select"].Value);
            //        object data = row.Cells[9].Value;

            //        if (data != null)
            //        {
            //            if (isSelected)
            //            {
            //                list.Add(Convert.ToInt32(row.Cells[0].Value));
            //            }
            //        }
            //    }
            //}else
            //{
            //    List<int> list = new List<int>();
            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        bool isSelected = Convert.ToBoolean(row.Cells["Select"].Value);
            //        object data = row.Cells[13].Value;

            //        if (data != null)
            //        {
            //            if (isSelected)
            //            {
            //                list.Add(Convert.ToInt32(row.Cells[0].Value));
            //            }
            //        }
            //    }
            //    if(list.Count > 0)
            //    {
            //        Impression_Facture impression = new Impression_Facture(list);
            //        impression.ShowDialog();
            //    }
            //}
        }

        private string name;
        private string types = "Vente";

        public async Task CallDataAchat()
        {
            var result = FillDataAchatAsync();
            var all_result = FillAllAsync();

            var tasklist = new List<Task>() { result, all_result };

            while(tasklist.Count  > 0)
            {
                var current = await Task.WhenAny(tasklist);

                if(current == result)
                {
                    dataGridView1.Columns.Clear();
                    if (result.Result.MyTables.Item1.Rows.Count == 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Tableau Vide");
                        DataRow dr = dt.NewRow();
                        dr[0] = "Aucune donnée dans ce tableau !";
                        dt.Rows.Add(dr);
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        dataGridView1.DataSource = result.Result.MyTables.Item1;
                        dataGridView2.DataSource = result.Result.MyTables.Item2;
                        dataGridView3.DataSource = result.Result.MyTables.Item1;
                        dataGridView2.DataSource = result.Result.MyTable;
                        lblMontant.Text = $"Montant : {result.Result.Montant.ToString("N0")} FCFA, Réduction : {result.Result.Reduction.ToString("N0")} FCFA, Montant Net : {(result.Result.Montant - result.Result.Reduction).ToString("N0")} FCFA";
                        lblQty.Text = "Quantité : " + result.Result.Qté.ToString("N0");
                        first = false;
                    }
                }
                else if(current == all_result)
                {
                    dataGridView5.DataSource = all_result.Result.MyTables.Item1;
                    dataGridView4.DataSource = all_result.Result.MyTables.Item2;
                    first = false;
                }

                tasklist.Remove(current);
            }
        }

        private Task<SearchedTable> FillDataAchatAsync()
        {
            return Task.Factory.StartNew(() => FillDataAchat());
        }

        public SearchedTable FillDataAchat()
        {
            SearchedTable table = new SearchedTable();
            using (var donnée = new QuitayeContext())
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Num_Achat");
                dt.Columns.Add("Code Barre");
                dt.Columns.Add("Désignation");
                dt.Columns.Add("Quantité");
                dt.Columns.Add("Mesure");
                dt.Columns.Add("Montant");
                dt.Columns.Add("Fournisseur");
                dt.Columns.Add("Date");
                dt.Columns.Add("Auteur");

                DataTable dt2 = new DataTable();
                dt2.Columns.Add("Num_Achat");
                dt2.Columns.Add("Code Barre");
                dt2.Columns.Add("Désignation");
                dt2.Columns.Add("Quantité", typeof(decimal));
                dt2.Columns.Add("Mesure");
                dt2.Columns.Add("Montant", typeof(decimal));
                dt2.Columns.Add("Fournisseur");
                dt2.Columns.Add("Date", typeof(DateTime));
                dt2.Columns.Add("Auteur");

                var don = from d in donnée.tbl_arrivée
                          where DbFunctions.TruncateTime(d.Date_Arrivée) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Arrivée) >= DbFunctions.TruncateTime(startDate.Value)
                          group d by d.Num_Achat into gr
                          select new
                          {
                              Num_Achat = gr.Key,                              
                              Code_Barre = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Barcode,
                              Marque = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Nom,
                              Catégorie = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Catégorie,
                              Taille = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Taille,
                              Quantité = gr.Sum(x => x.Quantité),
                              Montant = gr.Sum(x => x.Prix),
                              Date = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Date_Arrivée,
                              Fournisseur = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Fournisseur,
                              Auteur = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Auteur,
                              Mesure = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Mesure,
                              Type = donnée.tbl_stock_produits_vente.Where(x => x.Product_Id.ToString().Equals(gr.OrderByDescending(y => y.Date_Arrivée).FirstOrDefault().Product_Id.ToString())).Select(x => x.Type).FirstOrDefault(),
                              Date_Expiration = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Date_Expiration
                          };

                table.Qté = Convert.ToDecimal(don.Sum(x => x.Quantité));
                table.Montant = Convert.ToDecimal(don.Sum(x => x.Montant));
                foreach (var item in don.OrderByDescending(x => x.Date))
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Num_Achat;
                    dr[1] = item.Code_Barre;
                    if(!string.IsNullOrEmpty(item.Type))
                    dr[2] = $"{item.Marque} {item.Catégorie} {item.Taille}-{item.Type}";
                    else 
                    dr[2] = $"{item.Marque} {item.Catégorie} {item.Taille}";
                    dr[3] = Convert.ToDecimal(item.Quantité).ToString("N0");
                    dr[4] = item.Mesure;
                    dr[5] = Convert.ToDecimal(item.Montant).ToString("N0");
                    dr[6] = item.Fournisseur;
                    dr[7] = item.Date;
                    dr[8] = item.Auteur;

                    dt.Rows.Add(dr);

                    DataRow dr2 = dt2.NewRow();
                    
                    dr2[0] = item.Num_Achat;
                    dr2[1] = item.Code_Barre;
                    if (!string.IsNullOrEmpty(item.Type))
                        dr2[2] = $"{item.Marque} {item.Catégorie} {item.Taille}-{item.Type}";
                    else
                        dr2[2] = $"{item.Marque} {item.Catégorie} {item.Taille}";
                    dr2[3] = item.Quantité;
                    dr2[4] = item.Mesure;
                    dr2[5] = item.Montant;
                    dr2[6] = item.Fournisseur;
                    dr2[7] = item.Date;
                    dr2[8] = item.Auteur;

                    dt2.Rows.Add(dr2);
                }
                table.MyTables = (dt, dt2);
                return table;
            }
        }

        
        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            await ShowData();
        }

        private async void cbxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            type = cbxType.Text;
            if (!first)
                await ShowData();
        }

        bool first = true;


        private async void btnExcel_Click(object sender, EventArgs e)
        {
            MsgBox msg = new MsgBox();
            msg.show("Voulez-vous exporter avec les details d'achat ?", "Details achaht", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
            msg.ShowDialog();
            if(msg.clicked == "Oui")
            {
                Alert.SShow("Génération Fichier en cours.. Veillez-patientez !", Alert.AlertType.Info);
                var file = $"C:/Quitaye School/Rapport d'achat {startDate.Value.Date.ToString("dd-MM-yyyy")}-{EndDate.Value.Date.ToString("dd-MM-yyyy")} {DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")}.xlsx";
                await Task.Run(() => Quitaye_School.Models.Docuement.ExportToExcel(this.dataGridView5, file));
                System.Diagnostics.Process.Start(file);
                Alert.SShow("Génération effectué avec succès.", Alert.AlertType.Sucess);
            }
            else
            {
                //var file = "";
                //if (txtsearch.Text != "")
                //{
                //    file = $"C:/Quitaye School/Rapport {txtsearch.Text} {startDate.Value.Date.ToString("dd-MM-yyyy")}-{EndDate.Value.Date.ToString("dd-MM-yyyy")} {DateTime.Now.Date.ToString("dd-MM-yyyy HH.mm.ss")}.xlsx";
                //}
                //else
                //{
                //    file = $"C:/Quitaye School/Rapport {types} {startDate.Value.Date.ToString("dd-MM-yyyy")}-{EndDate.Value.Date.ToString("dd-MM-yyyy")} {DateTime.Now.Date.ToString("dd-MM-yyyy HH.mm.ss")}.xlsx";
                //}

                Alert.SShow("Génération Fichier en cours.. Veillez-patientez !", Alert.AlertType.Info);
                var file = $"C:/Quitaye School/Rapport d'achat {startDate.Value.Date.ToString("dd-MM-yyyy")}-{EndDate.Value.Date.ToString("dd-MM-yyyy")} {DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")}.xlsx";
                await Task.Run(() => Quitaye_School.Models.Docuement.ExportToExcel(this.dataGridView2, file));
                System.Diagnostics.Process.Start(file);
                Alert.SShow("Génération effectué avec succès.", Alert.AlertType.Sucess);
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            MsgBox msg = new MsgBox();
            msg.show("Voulez-vous exporter avec les details d'achat ?", "Details achaht", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
            msg.ShowDialog();
            if (msg.clicked == "Oui")
            {
                name = $"Rapport {types} {startDate.Value.Date.ToString("dd-MM-yyyy")}-{EndDate.Value.Date.ToString("dd-MM-yyyy")} {DateTime.Now.Date.ToString("dd-MM-yyyy HH.mm.ss")}.xlsx";
                Print.PrintPdfFile(dataGridView5, name, "Rapport " + types, "Opération(s)", types, mycontrng, "Quitaye School", true);
            }
            else
            {
                name = $"Rapport {types} {startDate.Value.Date.ToString("dd-MM-yyyy")}-{EndDate.Value.Date.ToString("dd-MM-yyyy")} {DateTime.Now.Date.ToString("dd-MM-yyyy HH.mm.ss")}.xlsx";
                Print.PrintPdfFile(dataGridView3, name, "Rapport " + types, "Opération(s)", types, mycontrng, "Quitaye School", true);
            }
                
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (cbxType.Text != "" && cbxType.Text != "Tout")
            {
                name = "Rapport " + types + " " + cbxType.Text + " " + startDate.Value.Date.ToString("dd-MM-yyyy") + " " + EndDate.Value.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            }
            else if (txtsearch.Text != "")
            {
                name = "Rapport " + types + " " + txtsearch.Text + " " + startDate.Value.Date.ToString("dd-MM-yyyy") + " " + EndDate.Value.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            }
            else
            {
                name = "Rapport " + types + " " + startDate.Value.Date.ToString("dd-MM-yyyy") + " " + EndDate.Value.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            }
        }

        private async void txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (txtsearch.Text != "")
            {
                await CallSearchTask(txtsearch.Text);
            }
            else
            {
                await ShowData();
            }
        }

        private async Task CallSearchTask(string txt)
        {
            var search = SearchAsync(txt);
            var search_all = FillAllAsync(txt);

            var tasklist = new List<Task>() { search, search_all };
            while(tasklist.Count > 0)
            {
                var current = await Task.WhenAny(tasklist);
                if(current == search)
                {
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = search.Result.MyTables.Item1;
                    dataGridView2.DataSource = search.Result.MyTables.Item2;
                    dataGridView3.DataSource =  search.Result.MyTables.Item1;

                    dataGridView2.DataSource = search.Result.MyTable;
                    lblMontant.Text = $"Montant : {search.Result.Montant.ToString("N0")} FCFA, Réduction : {search.Result.Reduction.ToString("N0")} FCFA, Montant Net : {(search.Result.Montant - search.Result.Reduction).ToString("N0")} FCFA";
                    lblQty.Text = "Quantité : " + search.Result.Qté.ToString("N0");
                    try
                    {
                        //if (Principales.type_compte.Contains("Administrateur"))
                        //    AddColumns.Addcolumn(dataGridView1);
                        //var selection = new DataGridViewCheckBoxColumn();
                        //selection.Name = "Select";
                        //selection.HeaderText = "Select";
                        //selection.Width = 40;

                        //dataGridView1.Columns.Add(selection);
                        //dataGridView1.Columns["Edit"].Visible = false;
                    }
                    catch (Exception)
                    {

                    }
                }
                else if(current == search_all)
                {
                    dataGridView4.DataSource = search_all.Result.MyTables.Item1;
                    dataGridView5.DataSource = search_all.Result.MyTables.Item2;
                }

                tasklist.Remove(current);
            }
        }
        async Task CallSearch(string text)
        {
            var result = await SearchAsync(text);
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = result.MyTables.Item1;
            dataGridView2.DataSource = result.MyTables.Item2;
            dataGridView3.DataSource = result.MyTables.Item1;
            
            dataGridView2.DataSource = result.MyTable;
            lblMontant.Text = $"Montant : {result.Montant.ToString("N0")} FCFA, Réduction : {result.Reduction.ToString("N0")} FCFA, Montant Net : {(result.Montant - result.Reduction).ToString("N0")} FCFA";
            lblQty.Text = "Quantité : " + result.Qté.ToString("N0");
            try
            {
                //if (Principales.type_compte.Contains("Administrateur"))
                //    AddColumns.Addcolumn(dataGridView1);
                //var selection = new DataGridViewCheckBoxColumn();
                //selection.Name = "Select";
                //selection.HeaderText = "Select";
                //selection.Width = 40;

                //dataGridView1.Columns.Add(selection);
                //dataGridView1.Columns["Edit"].Visible = false;
            }
            catch (Exception)
            {

            }
        }
        private static string type;
        private Task<SearchedTable> SearchAsync(string text)
        {
            return Task.Factory.StartNew(() => Search(text));
        }
        public SearchedTable Search(string text)
        {
            using (var donnée = new QuitayeContext())
            {
                {
                    SearchedTable table = new SearchedTable();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Num_Achat");
                    dt.Columns.Add("Code Barre");
                    dt.Columns.Add("Désignation");
                    dt.Columns.Add("Quantité");
                    dt.Columns.Add("Mesure");
                    dt.Columns.Add("Montant");
                    dt.Columns.Add("Fournisseur");
                    dt.Columns.Add("Date");
                    dt.Columns.Add("Auteur");

                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("Num_Achat");
                    dt2.Columns.Add("Code Barre");
                    dt2.Columns.Add("Désignation");
                    dt2.Columns.Add("Quantité", typeof(decimal));
                    dt2.Columns.Add("Mesure");
                    dt2.Columns.Add("Montant", typeof(decimal));
                    dt2.Columns.Add("Fournisseur");
                    dt2.Columns.Add("Date", typeof(DateTime));
                    dt2.Columns.Add("Auteur");

                    var don = from d in donnée.tbl_arrivée
                              where (d.Auteur.Contains(text) || d.Fournisseur.Contains(text)
                              || d.Num_Achat.Contains(text)
                              || d.Catégorie.Contains(text) || d.Barcode.Contains(text)
                              || d.Taille.Contains(text) || d.Nom.Contains(text) || d.Barcode.Contains(text)
                              || d.Bon_Commande.Contains(text))
                              && DbFunctions.TruncateTime(d.Date_Arrivée) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Arrivée) >= DbFunctions.TruncateTime(startDate.Value)
                              group d by d.Num_Achat into gr
                              select new
                              {
                                  Num_Achat = gr.Key,
                                  Code_Barre = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Barcode,
                                  Marque = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Nom,
                                  Catégorie = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Catégorie,
                                  Taille = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Taille,
                                  Quantité = gr.Sum(x => x.Quantité),
                                  Montant = gr.Sum(x => x.Prix),
                                  Date = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Date_Arrivée,
                                  Fournisseur = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Fournisseur,
                                  Auteur = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Auteur,
                                  Mesure = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Mesure,
                                  Type = donnée.tbl_stock_produits_vente.Where(x => x.Product_Id.ToString().Equals(gr.OrderByDescending(y => y.Date_Arrivée).FirstOrDefault().Product_Id.ToString())).Select(x => x.Type).FirstOrDefault(),
                                  Date_Expiration = gr.OrderByDescending(x => x.Date_Arrivée).FirstOrDefault().Date_Expiration
                              };

                    table.Qté = Convert.ToDecimal(don.Sum(x => x.Quantité));
                    table.Montant = Convert.ToDecimal(don.Sum(x => x.Montant));
                    foreach (var item in don.OrderByDescending(x => x.Date))
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = item.Num_Achat;
                        dr[1] = item.Code_Barre;
                        if (!string.IsNullOrEmpty(item.Type))
                            dr[2] = $"{item.Marque} {item.Catégorie} {item.Taille}-{item.Type}";
                        else
                            dr[2] = $"{item.Marque} {item.Catégorie} {item.Taille}";
                        dr[3] = Convert.ToDecimal(item.Quantité).ToString("N0");
                        dr[4] = item.Mesure;
                        dr[5] = Convert.ToDecimal(item.Montant).ToString("N0");
                        dr[6] = item.Fournisseur;
                        dr[7] = item.Date;
                        dr[8] = item.Auteur;

                        dt.Rows.Add(dr);

                        DataRow dr2 = dt2.NewRow();

                        dr2[0] = item.Num_Achat;
                        dr2[1] = item.Code_Barre;
                        if (!string.IsNullOrEmpty(item.Type))
                            dr2[2] = $"{item.Marque} {item.Catégorie} {item.Taille}-{item.Type}";
                        else
                            dr2[2] = $"{item.Marque} {item.Catégorie} {item.Taille}";
                        dr2[3] = item.Quantité;
                        dr2[4] = item.Mesure;
                        dr2[5] = item.Montant;
                        dr2[6] = item.Fournisseur;
                        dr2[7] = item.Date;
                        dr2[8] = item.Auteur;

                        dt2.Rows.Add(dr2);
                    }
                    table.MyTables = (dt, dt2);
                    return table;
                }
            }
        }
        
        private Task<SearchedTable> FillAllAsync(string search)
        {
            return Task.Factory.StartNew(() => FillAll(search));
        }
        private SearchedTable FillAll(string text)
        {
            using (var donnée = new QuitayeContext())
            {
                {
                    SearchedTable table = new SearchedTable();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Num_Achat");
                    dt.Columns.Add("Code Barre");
                    dt.Columns.Add("Désignation");
                    dt.Columns.Add("Quantité");
                    dt.Columns.Add("Mesure");
                    dt.Columns.Add("Montant");
                    dt.Columns.Add("Fournisseur");
                    dt.Columns.Add("Date");
                    dt.Columns.Add("Auteur");

                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("Num_Achat");
                    dt2.Columns.Add("Code Barre");
                    dt2.Columns.Add("Désignation");
                    dt2.Columns.Add("Quantité", typeof(decimal));
                    dt2.Columns.Add("Mesure");
                    dt2.Columns.Add("Montant", typeof(decimal));
                    dt2.Columns.Add("Fournisseur");
                    dt2.Columns.Add("Date", typeof(DateTime));
                    dt2.Columns.Add("Auteur");

                    var don = from d in donnée.tbl_arrivée
                              where (d.Auteur.Contains(text) || d.Fournisseur.Contains(text)
                              || d.Num_Achat.Contains(text)
                              || d.Catégorie.Contains(text) || d.Barcode.Contains(text)
                              || d.Taille.Contains(text) || d.Nom.Contains(text) || d.Barcode.Contains(text)
                              || d.Bon_Commande.Contains(text))
                              && DbFunctions.TruncateTime(d.Date_Arrivée) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Arrivée) >= DbFunctions.TruncateTime(startDate.Value)
                              select new
                              {
                                  Num_Achat = d.Num_Achat,
                                  Code_Barre = d.Barcode,
                                  Marque = d.Nom,
                                  Catégorie = d.Catégorie,
                                  Taille = d.Taille,
                                  Quantité = d.Quantité,
                                  Montant = d.Prix,
                                  Date = d.Date_Arrivée,
                                  Fournisseur = d.Fournisseur,
                                  Auteur = d.Auteur,
                                  Mesure = d.Mesure,
                                  Type = donnée.tbl_stock_produits_vente.Where(x => x.Product_Id.ToString().Equals(d.Product_Id.ToString())).Select(x => x.Type).FirstOrDefault(),
                                  Date_Expiration = d.Date_Expiration
                              };

                    table.Qté = Convert.ToDecimal(don.Sum(x => x.Quantité));
                    table.Montant = Convert.ToDecimal(don.Sum(x => x.Montant));
                    foreach (var item in don.OrderByDescending(x => x.Date))
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = item.Num_Achat;
                        dr[1] = item.Code_Barre;
                        if (!string.IsNullOrEmpty(item.Type))
                            dr[2] = $"{item.Marque} {item.Catégorie} {item.Taille}-{item.Type}";
                        else
                            dr[2] = $"{item.Marque} {item.Catégorie} {item.Taille}";
                        dr[3] = Convert.ToDecimal(item.Quantité).ToString("N0");
                        dr[4] = item.Mesure;
                        dr[5] = Convert.ToDecimal(item.Montant).ToString("N0");
                        dr[6] = item.Fournisseur;
                        dr[7] = item.Date;
                        dr[8] = item.Auteur;

                        dt.Rows.Add(dr);

                        DataRow dr2 = dt2.NewRow();

                        dr2[0] = item.Num_Achat;
                        dr2[1] = item.Code_Barre;
                        if (!string.IsNullOrEmpty(item.Type))
                            dr2[2] = $"{item.Marque} {item.Catégorie} {item.Taille}-{item.Type}";
                        else
                            dr2[2] = $"{item.Marque} {item.Catégorie} {item.Taille}";
                        dr2[3] = item.Quantité;
                        dr2[4] = item.Mesure;
                        dr2[5] = item.Montant;
                        dr2[6] = item.Fournisseur;
                        dr2[7] = item.Date;
                        dr2[8] = item.Auteur;

                        dt2.Rows.Add(dr2);
                    }
                    table.MyTables = (dt, dt2);
                    return table;
                }
            }
        }

        private Task<SearchedTable> FillAllAsync()
        {
            return Task.Factory.StartNew(() => FillAll());
        }
        private SearchedTable FillAll()
        {
            using (var donnée = new QuitayeContext())
            {
                {
                    SearchedTable table = new SearchedTable();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Num_Achat");
                    dt.Columns.Add("Code Barre");
                    dt.Columns.Add("Désignation");
                    dt.Columns.Add("Quantité");
                    dt.Columns.Add("Mesure");
                    dt.Columns.Add("Montant");
                    dt.Columns.Add("Fournisseur");
                    dt.Columns.Add("Date");
                    dt.Columns.Add("Auteur");

                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("Num_Achat");
                    dt2.Columns.Add("Code Barre");
                    dt2.Columns.Add("Désignation");
                    dt2.Columns.Add("Quantité", typeof(decimal));
                    dt2.Columns.Add("Mesure");
                    dt2.Columns.Add("Montant", typeof(decimal));
                    dt2.Columns.Add("Fournisseur");
                    dt2.Columns.Add("Date", typeof(DateTime));
                    dt2.Columns.Add("Auteur");

                    var don = from d in donnée.tbl_arrivée
                              where DbFunctions.TruncateTime(d.Date_Arrivée) <= DbFunctions.TruncateTime(EndDate.Value)
                              && DbFunctions.TruncateTime(d.Date_Arrivée) >= DbFunctions.TruncateTime(startDate.Value)
                              orderby d.Num_Achat
                              select new
                              {
                                  Num_Achat = d.Num_Achat,
                                  Code_Barre = d.Barcode,
                                  Marque = d.Nom,
                                  Catégorie = d.Catégorie,
                                  Taille = d.Taille,
                                  Quantité = d.Quantité,
                                  Montant = d.Prix,
                                  Date = d.Date_Arrivée,
                                  Fournisseur = d.Fournisseur,
                                  Auteur = d.Auteur,
                                  Mesure = d.Mesure,
                                  Type = donnée.tbl_stock_produits_vente.Where(x => x.Product_Id.ToString().Equals(d.Product_Id.ToString())).Select(x => x.Type).FirstOrDefault(),
                                  Date_Expiration = d.Date_Expiration
                              };

                    table.Qté = Convert.ToDecimal(don.Sum(x => x.Quantité));
                    table.Montant = Convert.ToDecimal(don.Sum(x => x.Montant));
                    var current_num = "";
                    foreach (var item in don.OrderByDescending(x => new {x.Num_Achat, x.Date}))
                    {
                        if (string.IsNullOrEmpty(current_num))
                        {
                            current_num = item.Num_Achat;
                        }
                        else
                        {
                            if(current_num == item.Num_Achat)
                            {

                            }else
                            {
                                var drn = dt.NewRow();
                                drn["Montant"] = don.Where(x => x.Num_Achat == item.Num_Achat).Sum(x => x.Montant);
                                dt.Rows.Add(drn);
                                var dre = dt2.NewRow();
                                dre["Montant"] = don.Where(x => x.Num_Achat == item.Num_Achat).Sum(x => x.Montant);
                                dt2.Rows.Add(dre);
                            }
                        }
                        current_num = item.Num_Achat;
                        DataRow dr = dt.NewRow();
                        dr[0] = item.Num_Achat;
                        dr[1] = item.Code_Barre;
                        if (!string.IsNullOrEmpty(item.Type))
                            dr[2] = $"{item.Marque} {item.Catégorie} {item.Taille}-{item.Type}";
                        else
                            dr[2] = $"{item.Marque} {item.Catégorie} {item.Taille}";
                        dr[3] = Convert.ToDecimal(item.Quantité).ToString("N0");
                        dr[4] = item.Mesure;
                        dr[5] = Convert.ToDecimal(item.Montant).ToString("N0");
                        dr[6] = item.Fournisseur;
                        dr[7] = item.Date;
                        dr[8] = item.Auteur;

                        dt.Rows.Add(dr);

                        DataRow dr2 = dt2.NewRow();

                        dr2[0] = item.Num_Achat;
                        dr2[1] = item.Code_Barre;
                        if (!string.IsNullOrEmpty(item.Type))
                            dr2[2] = $"{item.Marque} {item.Catégorie} {item.Taille}-{item.Type}";
                        else
                            dr2[2] = $"{item.Marque} {item.Catégorie} {item.Taille}";
                        dr2[3] = item.Quantité;
                        dr2[4] = item.Mesure;
                        dr2[5] = item.Montant;
                        dr2[6] = item.Fournisseur;
                        dr2[7] = item.Date;
                        dr2[8] = item.Auteur;

                        dt2.Rows.Add(dr2);
                    }

                    {
                        if(don.OrderBy(x => x.Num_Achat).FirstOrDefault() != null)
                        current_num = don.OrderBy(x => x.Num_Achat).FirstOrDefault().Num_Achat;
                    }
                    {
                        {
                            if(don.OrderBy(x => x.Num_Achat).FirstOrDefault() != null)
                            {
                                var drn = dt.NewRow();
                                drn["Montant"] = don.Where(x => x.Num_Achat == current_num).Sum(x => x.Montant);
                                dt.Rows.Add(drn);
                                var dre = dt2.NewRow();
                                dre["Montant"] = don.Where(x => x.Num_Achat == current_num).Sum(x => x.Montant);
                                dt2.Rows.Add(dre);
                            }
                        }
                    }
                    table.MyTables = (dt, dt2);
                    return table;
                }
            }
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (types == "Vente")
            {
                if (dataGridView1.Columns.Count >= 2)
                    if (e.ColumnIndex >= 0)
                    {
                        if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Sup") == true)
                        {
                            string auteur = dataGridView1.CurrentRow.Cells["Auteur"].Value.ToString();
                            if (Principales.type_compte.Contains("Administrateur") || auteur == Principales.profile)
                            {
                                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                                MsgBox msg = new MsgBox();
                                msg.show("Voulez-vous supprimer cette vente ?", "Suppression",
                                    MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                                msg.ShowDialog();
                                if (msg.clicked == "Non")
                                    return;
                                else if (msg.clicked == "Oui")
                                {
                                    using (var donnée = new QuitayeContext())
                                    {
                                        var v = (from d in donnée.tbl_vente where d.Id == id select d).First();
                                        if (v.Cloturé != "Oui")
                                        {
                                            //if(v.Date_Vente.Value.AddHours(168) >= DateTime.Now)
                                            {
                                                var hist = donnée.tbl_historique_expiration
                                                                .Where(x => x.Id_Opération == v.Id && x.Num_Opération == v.Num_Vente).ToList();

                                                foreach (var item in hist)
                                                {
                                                    var expiration = donnée.tbl_expiration.Where(x => DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(item.Date_Expiration)
                                                    && x.Code_Barre == v.Barcode).FirstOrDefault();
                                                    expiration.Reste += item.Quantité;
                                                }
                                                donnée.tbl_vente.Remove(v);
                                                donnée.tbl_historique_expiration.RemoveRange(hist);

                                                var stock = new Models.Context.tbl_stock_produits_vente();
                                                if (v.Filiale == null || v.Filiale == "Siège" || v.Filiale == "")
                                                {
                                                    stock = (from d in donnée.tbl_stock_produits_vente
                                                             where d.Code_Barre == v.Barcode
                                                             && d.Detachement == "Siège"
                                                             select d).First();
                                                }
                                                else stock = (from d in donnée.tbl_stock_produits_vente
                                                              where d.Code_Barre == v.Barcode
                                                              && d.Detachement == v.Filiale
                                                              select d).First();
                                                var ms = (from d in donnée.tbl_mesure_vente
                                                          where d.Nom == v.Mesure
                                                          select d).First();

                                                var formu = (from d in donnée.tbl_formule_mesure_vente
                                                             where d.Id == stock.Formule
                                                             select d).First();

                                                if (ms.Niveau == 1)
                                                {
                                                    stock.Quantité += v.Quantité;
                                                }
                                                else if (ms.Niveau == 2)
                                                {
                                                    decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Moyen);
                                                    stock.Quantité += Convert.ToDecimal(v.Quantité) * unité;
                                                }
                                                else if (ms.Niveau == 3)
                                                {
                                                    decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Grand);
                                                    stock.Quantité += Convert.ToDecimal(v.Quantité) * unité;
                                                }
                                                else if (ms.Niveau == 4)
                                                {
                                                    decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Large);
                                                    stock.Quantité += Convert.ToDecimal(v.Quantité) * unité;
                                                }
                                                else if (ms.Niveau == 5)
                                                {
                                                    decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Hyper_Large);
                                                    stock.Quantité += Convert.ToDecimal(v.Quantité) * unité;
                                                }

                                                var historique = donnée.tbl_historique_valeur_stock.Where(x => x.Code_Barre == stock.Code_Barre
                                                && x.Filiale == stock.Detachement && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(v.Date_Vente)).FirstOrDefault();
                                                if (historique != null)
                                                {
                                                    historique.Quantité = stock.Quantité;
                                                }
                                                await donnée.SaveChangesAsync();
                                                await ShowData();
                                                Alert.SShow("Vente supprimeé avec succès.", Alert.AlertType.Sucess);
                                            }
                                        }
                                        else Alert.SShow("Suppression impossible, écriture cloturée", Alert.AlertType.Info);
                                    }
                                }
                            }
                        }
                        else if (e.ColumnIndex == 1)
                        {
                            string num_vente = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                            if (num_vente != null && num_vente != "")
                            {
                                Details_Vente vente = new Details_Vente(num_vente);
                                vente.ShowDialog();
                                if (Details_Vente.ok == "Oui")
                                {
                                    Details_Vente.ok = null;
                                    await ShowData();
                                }
                            }
                        }
                    }
            }
            else
            {
                if (e.ColumnIndex >= 0)
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Sup") == true)
                    {
                        string auteur = dataGridView1.CurrentRow.Cells["Auteur"].Value.ToString();
                        if (Principales.type_compte.Contains("Administrateur") || auteur == Principales.profile)
                        {
                            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                            MsgBox msg = new MsgBox();
                            msg.show("Voulez-vous supprimer cet achat ?", "Suppression",
                                MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                            msg.ShowDialog();
                            if (msg.clicked == "Non")

                                return;
                            else if (msg.clicked == "Oui")
                            {
                                using (var donnée = new QuitayeContext())
                                {
                                    var valeur = await donnée.tbl_arrivée.Where(x => x.Id == id).FirstOrDefaultAsync();
                                    //if (v.Date_Arrivée.Value.AddHours(168) >= DateTime.Now)
                                    if (valeur.Cloturé != "Oui")
                                    {
                                        var hist = await donnée.tbl_historique_expiration
                                                                .Where(x => x.Id_Opération == valeur.Id
                                                                && x.Num_Opération == valeur.Num_Achat).ToListAsync();

                                        foreach (var item in hist)
                                        {
                                            var expiration = donnée.tbl_expiration.Where(x => DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(item.Date_Expiration)
                                            && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(item.Date)
                                            && x.Code_Barre == valeur.Barcode).FirstOrDefault();
                                            donnée.tbl_expiration.Remove(expiration);
                                        }

                                        donnée.tbl_historique_expiration.RemoveRange(hist);

                                        donnée.tbl_arrivée.Remove(valeur);

                                        var ms = (from d in donnée.tbl_mesure_vente where d.Nom == valeur.Mesure select d).First();
                                        var stock = (from d in donnée.tbl_stock_produits_vente where d.Code_Barre == valeur.Barcode select d).First();
                                        var formu = (from d in donnée.tbl_formule_mesure_vente where d.Id == stock.Formule select d).First();
                                        if (ms.Niveau == 1)
                                        {
                                            stock.Quantité -= valeur.Quantité;
                                        }
                                        else if (ms.Niveau == 2)
                                        {
                                            decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Moyen);
                                            stock.Quantité -= Convert.ToDecimal(valeur.Quantité) * unité;
                                        }
                                        else if (ms.Niveau == 3)
                                        {
                                            decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Grand);
                                            stock.Quantité -= Convert.ToDecimal(valeur.Quantité) * unité;
                                        }
                                        else if (ms.Niveau == 4)
                                        {
                                            decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Large);
                                            stock.Quantité -= Convert.ToDecimal(valeur.Quantité) * unité;
                                        }
                                        else if (ms.Niveau == 5)
                                        {
                                            decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Hyper_Large);
                                            stock.Quantité -= Convert.ToDecimal(valeur.Quantité) * unité;
                                        }

                                        var historique = donnée.tbl_historique_valeur_stock.Where(x => x.Code_Barre == stock.Code_Barre
                                            && x.Filiale == stock.Detachement && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(valeur.Date_Arrivée)).FirstOrDefault();
                                        if (historique != null)
                                        {
                                            historique.Quantité = stock.Quantité;
                                        }
                                        await donnée.SaveChangesAsync();
                                        await ShowData();
                                        Alert.SShow("Achat supprimeé avec succès.", Alert.AlertType.Sucess);
                                    }
                                    else
                                    {
                                        Alert.SShow("Suppression impossible, écriture cloturée.", Alert.AlertType.Info);
                                    }
                                }
                            }
                        }
                    }
                    else if (e.ColumnIndex == 0)
                    {
                        string num_vente = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        if (num_vente != null && num_vente != "")
                        {
                            Details_Achat vente = new Details_Achat(num_vente);
                            vente.ShowDialog();
                            if (Details_Achat.ok == "Oui")
                            {
                                Details_Achat.ok = null;
                                await ShowData();
                            }
                        }
                    }
                }
            }
        }

        private async Task ShowData()
        {
            await CallDataAchat();
        }
        public System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                return returnImage;
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            PopUp();
        }

        public async void PopUp()
        {
            using (var donnée = new QuitayeContext())
            {
                if (types == "Vente")
                {
                    var st = (from s in donnée.tbl_vente
                              where s.Id == Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString())
                              select s).First();

                    var b = (from d in donnée.tbl_produits
                             where d.Barcode == st.Barcode
                             select new { Id = d.Id, Image = d.Image }).First();
                    PopVente pop = new PopVente();
                    pop.venteInfo1.Ref = st.Id;

                    pop.venteInfo1.Titre = st.Produit;
                    pop.venteInfo1.Catégorie = st.Catégorie;
                    pop.venteInfo1.Taille = st.Taille;
                    pop.venteInfo1.Code_Barre = st.Barcode;
                    pop.venteInfo1.Usage = st.Usage;
                    pop.venteInfo1.Prix_Unité = Convert.ToDecimal(st.Prix_Unité);
                    pop.venteInfo1.Quantité = Convert.ToDecimal(st.Quantité);
                    pop.venteInfo1.Montant = Convert.ToDecimal(st.Montant);
                    pop.venteInfo1.Client = st.Client;
                    pop.venteInfo1.Contact = st.Num_Client;
                    pop.venteInfo1.Ref = Convert.ToInt32(st.Id);

                    try
                    {
                        byte[] img = b.Image.ToArray();
                        pop.venteInfo1.Icon = ByteArrayToImage(b.Image.ToArray());
                    }
                    catch (Exception)
                    {
                        pop.venteInfo1.Icon = null;
                    }


                    if (st.Type != null)
                        pop.lblEtat.Text = "Etat : " + st.Type;
                    if (st.Type == "A Crédit")
                        pop.btnValidé.Visible = true;
                    pop.panel1.Visible = true;
                    pop.panel2.Visible = true;
                    pop.panel3.Visible = true;
                    pop.panel4.Visible = true;
                    pop.ShowDialog();
                    if (pop.ok == "Oui")
                        await ShowData();
                }
            }
        }
    }

    public class SearchedTable
    {
        private DataTable _dt;

        public DataTable MyTable
        {
            get { return _dt; }
            set { _dt = value; }
        }

        private (DataTable, DataTable) my_tables;

        public (DataTable, DataTable) MyTables
        {
            get { return my_tables; }
            set { my_tables = value; }
        }


        private decimal _payement;

        public decimal Payement
        {
            get { return _payement; }
            set { _payement = value; }
        }

        private int _id_Client;

        public int Id_Client
        {
            get { return _id_Client; }
            set { _id_Client = value; }
        }


        private decimal _qté;

        public decimal Qté
        {
            get { return _qté; }
            set { _qté = value; }
        }

        private decimal _montant;

        public decimal Montant
        {
            get { return _montant; }
            set { _montant = value; }
        }

        private string _client;

        public string Client
        {
            get { return _client; }
            set { _client = value; }
        }

        private decimal _reduction;

        public decimal Reduction
        {
            get { return _reduction; }
            set { _reduction = value; }
        }

    }
}
