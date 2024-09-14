using PrintAction;
using Quitaye_School.Models.Context;
using System;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Rapport_Vente : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Rapport_Vente(bool minimize = false)
        {
            if (minimize)
            {
                Minimize = minimize;
                this.Size = new Size(1024, 763);
            }
            InitializeComponent();

            var size = this.Size;
            if (minimize)
            {
                Minimize = minimize;
                this.Size = new Size(1024, 763);
            }
            types = "Vente";
            timer1.Start();
            timer2.Start();
            startDate.Value = DateTime.Today.AddDays(-3);
            btnImprimerFacture.Click += BtnImprimerFacture_Click;
            startDate.ValueChanged += StartDate_ValueChanged;
            EndDate.ValueChanged += StartDate_ValueChanged;
            //cbxFiliale.SelectedIndexChanged += CbxFiliale_SelectedIndexChanged;
            cbxType.SelectedIndexChanged += cbxType_SelectedIndexChanged;
            btnPdf.Click += btnPDF_Click;
            btnExcel.Click += btnExcel_Click;
            dataGridView1.CellClick += dataGridView1_CellClick;
            txtsearch.TextChanged += txtsearch_TextChanged;
            btnRestore.Visible = minimize;
            btnMinimize.Visible = minimize;
            btnFermer.Visible = minimize;
            btnRestore.Click += BtnRestore_Click;
            btnMinimize.Click += BtnMinimize_Click;
            panel2.Visible = minimize;
            panel4.Visible = minimize;
            panel5.Visible = minimize;
            panel6.Visible = minimize;
            btnFermer.Visible = minimize;
            btnRestore.Visible = minimize;
            btnMinimize.Visible = minimize;
            btnFermer.Click += BtnFermer_Click;
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnMinimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;


        private void BtnRestore_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        public bool Minimize { get; set; }

        private async void CbxFiliale_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(cbxFiliale.Text != "")
            //{
            //    await CallSearch(cbxFiliale.Text);
            //}
        }

        private async void StartDate_ValueChanged(object sender, EventArgs e)
        {
            if (!Minimize)
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

        async Task CallData()
        {
            if (Minimize)
            {
                var result = await FillDataSingleAsync();
                dataGridView1.Columns.Clear();
                if (result.MyTables.Item1.Rows.Count == 0)
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
                    dataGridView1.DataSource = result.MyTables.Item1;
                    dataGridView2.DataSource = result.MyTables.Item2;
                    dataGridView3.DataSource = result.MyTables.Item1;
                    dataGridView2.DataSource = result.MyTable;
                    lblMontant.Text = $"Montant : {result.Montant.ToString("N0")} FCFA, Réduction : {result.Reduction.ToString("N0")} FCFA, Montant Net : {(result.Montant - result.Reduction).ToString("N0")} FCFA";
                    lblQty.Text = "Quantité : " + result.Qté.ToString("N0");
                    first = false;
                    //try
                    //{
                    //    if (Principales.type_compte.Contains("Administrateur"))
                    //    {
                    //        var selection = new DataGridViewCheckBoxColumn();
                    //        selection.Name = "Select";
                    //        selection.HeaderText = "Select";
                    //        selection.Width = 40;

                    //        dataGridView1.Columns.Add(selection);
                    //        AddColumns.Addcolumn(dataGridView1);
                    //    }

                    //    dataGridView1.Columns["Edit"].Visible = false;
                    //}
                    //catch (Exception)
                    //{

                    //}
                }
            }
            else
            {
                var result = await FillDataAsync();
                dataGridView1.Columns.Clear();
                if (result.MyTables.Item1.Rows.Count == 0)
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
                    dataGridView1.DataSource = result.MyTables.Item1;
                    dataGridView2.DataSource = result.MyTables.Item2;
                    dataGridView3.DataSource = result.MyTables.Item1;
                    dataGridView2.DataSource = result.MyTable;
                    lblMontant.Text = $"Montant : {result.Montant.ToString("N0")} FCFA, Réduction : {result.Reduction.ToString("N0")} FCFA, Montant Net : {(result.Montant - result.Reduction).ToString("N0")} FCFA";
                    lblQty.Text = "Quantité : " + result.Qté.ToString("N0");
                    first = false;
                    //try
                    //{
                    //    if (Principales.type_compte.Contains("Administrateur"))
                    //    {
                    //        var selection = new DataGridViewCheckBoxColumn();
                    //        selection.Name = "Select";
                    //        selection.HeaderText = "Select";
                    //        selection.Width = 40;

                    //        dataGridView1.Columns.Add(selection);
                    //        AddColumns.Addcolumn(dataGridView1);
                    //    }

                    //    dataGridView1.Columns["Edit"].Visible = false;
                    //}
                    //catch (Exception)
                    //{

                    //}
                }
            }
            
        }
        private Task<SearchedTable> FillDataAsync()
        {
            return Task.Factory.StartNew(() => FillData());
        }
        public SearchedTable FillData()
        {
            SearchedTable table = new SearchedTable();
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Num_Vente");
            dt1.Columns.Add("Code Barre");
            dt1.Columns.Add("Désignation");
            dt1.Columns.Add("Filiale");
            dt1.Columns.Add("Quantité");
            dt1.Columns.Add("Mesure");
            dt1.Columns.Add("Prix_Unité");
            dt1.Columns.Add("Montant");
            dt1.Columns.Add("Type");
            dt1.Columns.Add("Date");
            dt1.Columns.Add("Auteur");

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Num_Vente");
            dt2.Columns.Add("Code Barre");
            dt2.Columns.Add("Désignation");
            dt2.Columns.Add("Filiale");
            dt2.Columns.Add("Quantité", typeof(decimal));
            dt2.Columns.Add("Mesure");
            dt2.Columns.Add("Prix_Unité", typeof(decimal));
            dt2.Columns.Add("Montant", typeof(decimal));
            dt2.Columns.Add("Type");
            dt2.Columns.Add("Date", typeof(DateTime));
            dt2.Columns.Add("Auteur");

            using (var donnée = new QuitayeContext())
            {
                var don = (from d in donnée.tbl_vente
                          where DbFunctions.TruncateTime(d.Date_Vente) <= DbFunctions.TruncateTime(EndDate.Value) 
                          && DbFunctions.TruncateTime(d.Date_Vente) >= DbFunctions.TruncateTime(startDate.Value)
                          orderby d.Date_Vente descending
                          group d by new
                          {
                              Num_Vente = d.Num_Vente
                          } into gr
                          select new
                          {
                              Code_Barre = gr.FirstOrDefault().Barcode,
                              Marque = gr.FirstOrDefault().Produit,
                              Catégorie = gr.FirstOrDefault().Catégorie,
                              Taille = gr.FirstOrDefault().Taille,
                              Quantité = gr.Sum(x => x.Quantité),
                              Prix_Unité = gr.Sum(x => x.Prix_Unité),
                              Montant = gr.Sum(x => x.Montant),
                              Type = gr.FirstOrDefault().Type,
                              Date = gr.FirstOrDefault().Date_Vente,
                              Auteur = gr.FirstOrDefault().Auteur,
                              Num_Vente = gr.FirstOrDefault().Num_Vente,
                              Filiale = gr.FirstOrDefault().Filiale,
                              Mesure = gr.FirstOrDefault().Mesure,
                              Reduction = gr.Sum(x => x.Reduction),
                              Type_Base = donnée.tbl_stock_produits_vente.Where(x => x.Product_Id.ToString().Equals(gr.FirstOrDefault().Product_Id.ToString())).Select(x => x.Type).FirstOrDefault(),
                          }).ToList();
                table.Qté = Convert.ToDecimal(don.Sum(x => x.Quantité));
                table.Montant = Convert.ToDecimal(don.Sum(x => x.Montant));
                table.Reduction = Convert.ToDecimal(don.Sum(x => x.Reduction));

                foreach (var item in don.OrderByDescending(x => x.Date))
                {
                    DataRow dr = dt1.NewRow();
                    dr[0] = item.Num_Vente;
                    dr[1] = item.Code_Barre;
                    dr[2] = $"{item.Marque} {item.Catégorie} {item.Taille} {item.Type_Base}";
                    dr[3] = item.Filiale;
                    dr[4] = Convert.ToDecimal(item.Quantité).ToString("N0");
                    dr[5] = item.Mesure;
                    dr[6] = Convert.ToDecimal(item.Prix_Unité).ToString("N0");
                    dr[7] = Convert.ToDecimal(item.Montant).ToString("N0");
                    dr[8] = item.Type;
                    dr[9] = item.Date;
                    dr[10] = item.Auteur;

                    dt1.Rows.Add(dr);

                    DataRow dr2 = dt2.NewRow();
                    dr2[0] = item.Num_Vente;
                    dr2[1] = item.Code_Barre;
                    dr2[2] = $"{item.Marque} {item.Catégorie} {item.Taille} {item.Type_Base}";
                    dr2[3] = item.Filiale;
                    dr2[4] = Convert.ToDecimal(item.Quantité);
                    dr2[5] = item.Mesure;
                    dr2[6] = Convert.ToDecimal(item.Prix_Unité);
                    dr2[7] = Convert.ToDecimal(item.Montant);
                    dr2[8] = item.Type;
                    dr2[9] = Convert.ToDateTime(item.Date.Value);
                    dr2[10] = item.Auteur;

                    dt2.Rows.Add(dr2);
                }
                table.MyTables = (dt1, dt2);
                return table;
            }
        }

        private Task<SearchedTable> FillDataSingleAsync()
        {
            return Task.Factory.StartNew(() => FillDataSingle());
        }
        public SearchedTable FillDataSingle()
        {
            SearchedTable table = new SearchedTable();
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Num_Vente");
            dt1.Columns.Add("Code Barre");
            dt1.Columns.Add("Désignation");
            dt1.Columns.Add("Filiale");
            dt1.Columns.Add("Quantité");
            dt1.Columns.Add("Mesure");
            dt1.Columns.Add("Prix_Unité");
            dt1.Columns.Add("Montant");
            dt1.Columns.Add("Type");
            dt1.Columns.Add("Date");
            dt1.Columns.Add("Auteur");

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Num_Vente");
            dt2.Columns.Add("Code Barre");
            dt2.Columns.Add("Désignation");
            dt2.Columns.Add("Filiale");
            dt2.Columns.Add("Quantité", typeof(decimal));
            dt2.Columns.Add("Mesure");
            dt2.Columns.Add("Prix_Unité", typeof(decimal));
            dt2.Columns.Add("Montant", typeof(decimal));
            dt2.Columns.Add("Type");
            dt2.Columns.Add("Date", typeof(DateTime));
            dt2.Columns.Add("Auteur");

            using (var donnée = new QuitayeContext())
            {
                var don = (from d in donnée.tbl_vente
                          where DbFunctions.TruncateTime(d.Date_Vente) == DbFunctions.TruncateTime(DateTime.Now)
                          group d by new
                          {
                              Num_Vente = d.Num_Vente
                          } into gr
                          select new
                          {
                              Code_Barre = gr.FirstOrDefault().Barcode,
                              Marque = gr.FirstOrDefault().Produit,
                              Catégorie = gr.FirstOrDefault().Catégorie,
                              Taille = gr.FirstOrDefault().Taille,
                              Quantité = gr.Sum(x => x.Quantité),
                              Prix_Unité = gr.Sum(x => x.Prix_Unité),
                              Montant = gr.Sum(x => x.Montant),
                              Type = gr.FirstOrDefault().Type,
                              Date = gr.FirstOrDefault().Date_Vente,
                              Auteur = gr.FirstOrDefault().Auteur,
                              Num_Vente = gr.FirstOrDefault().Num_Vente,
                              Filiale = gr.FirstOrDefault().Filiale,
                              Mesure = gr.FirstOrDefault().Mesure,
                              Reduction = gr.Sum(x => x.Reduction),
                              Type_Base = donnée.tbl_stock_produits_vente.Where(x => x.Product_Id.ToString().Equals(gr.FirstOrDefault().Product_Id.ToString())).Select(x => x.Type).FirstOrDefault(),
                          }).OrderByDescending(x => x.Date).Take(1);
                table.Qté = Convert.ToDecimal(don.Sum(x => x.Quantité));
                table.Montant = Convert.ToDecimal(don.Sum(x => x.Montant));
                table.Reduction = Convert.ToDecimal(don.Sum(x => x.Reduction));

                foreach (var item in don.OrderByDescending(x => x.Date))
                {
                    DataRow dr = dt1.NewRow();
                    dr[0] = item.Num_Vente;
                    dr[1] = item.Code_Barre;
                    dr[2] = $"{item.Marque} {item.Catégorie} {item.Taille} {item.Type_Base}";
                    dr[3] = item.Filiale;
                    dr[4] = Convert.ToDecimal(item.Quantité).ToString("N0");
                    dr[5] = item.Mesure;
                    dr[6] = Convert.ToDecimal(item.Prix_Unité).ToString("N0");
                    dr[7] = Convert.ToDecimal(item.Montant).ToString("N0");
                    dr[8] = item.Type;
                    dr[9] = item.Date;
                    dr[10] = item.Auteur;

                    dt1.Rows.Add(dr);

                    DataRow dr2 = dt2.NewRow();
                    dr2[0] = item.Num_Vente;
                    dr2[1] = item.Code_Barre;
                    dr2[2] = $"{item.Marque} {item.Catégorie} {item.Taille} {item.Type_Base}";
                    dr2[3] = item.Filiale;
                    dr2[4] = Convert.ToDecimal(item.Quantité);
                    dr2[5] = item.Mesure;
                    dr2[6] = Convert.ToDecimal(item.Prix_Unité);
                    dr2[7] = Convert.ToDecimal(item.Montant);
                    dr2[8] = item.Type;
                    dr2[9] = Convert.ToDateTime(item.Date.Value);
                    dr2[10] = item.Auteur;

                    dt2.Rows.Add(dr2);
                }
                table.MyTables = (dt1, dt2);
                return table;
            }
        }

        async Task CallDataFiltre()
        {
            var result = await FillDataFiltreAsync();
            dataGridView1.Columns.Clear();
            if (result.MyTables.Item1.Rows.Count == 0)
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
                dataGridView1.DataSource = result.MyTables.Item1;
                dataGridView2.DataSource = result.MyTables.Item2;
                dataGridView3.DataSource = result.MyTables.Item1;
                dataGridView2.DataSource = result.MyTable;
                lblMontant.Text = $"Montant : {result.Montant.ToString("N0")} FCFA, Réduction : {result.Reduction.ToString("N0")} FCFA, Montant Net : {(result.Montant - result.Reduction).ToString("N0")} FCFA";
                lblQty.Text = "Quantité : " + result.Qté.ToString("N0");
                first = false;
                //try
                //{
                //    if (Principales.type_compte.Contains("Administrateur"))
                //    {
                //        var selection = new DataGridViewCheckBoxColumn();
                //        selection.Name = "Select";
                //        selection.HeaderText = "Select";
                //        selection.Width = 40;

                //        dataGridView1.Columns.Add(selection);
                //        AddColumns.Addcolumn(dataGridView1);
                //    }
                //    dataGridView1.Columns["Edit"].Visible = false;
                //}
                //catch (Exception)
                //{

                //}
            }
        }
        private Task<SearchedTable> FillDataFiltreAsync()
        {
            return Task.Factory.StartNew(() => FillDataFiltre());
        }
        public SearchedTable FillDataFiltre()
        {
            SearchedTable table = new SearchedTable();
            using (var donnée = new QuitayeContext())
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Num_Vente");
                dt.Columns.Add("Code Barre");
                dt.Columns.Add("Désignation");
                dt.Columns.Add("Filiale");
                dt.Columns.Add("Quantité");
                dt.Columns.Add("Mesure");
                dt.Columns.Add("Prix_Unité");
                dt.Columns.Add("Montant");
                dt.Columns.Add("Type");
                dt.Columns.Add("Date");
                dt.Columns.Add("Auteur");

                DataTable dt2 = new DataTable();
                dt2.Columns.Add("Num_Vente");
                dt2.Columns.Add("Code Barre");
                dt2.Columns.Add("Désignation");
                dt2.Columns.Add("Filiale");
                dt2.Columns.Add("Quantité", typeof(decimal));
                dt2.Columns.Add("Mesure");
                dt2.Columns.Add("Prix_Unité", typeof(decimal));
                dt2.Columns.Add("Montant", typeof(decimal));
                dt2.Columns.Add("Type");
                dt2.Columns.Add("Date", typeof(DateTime));
                dt2.Columns.Add("Auteur");

                var don = from d in donnée.tbl_vente
                          where DbFunctions.TruncateTime(d.Date_Vente) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Vente) >= DbFunctions.TruncateTime(startDate.Value) && d.Type == type
                          orderby d.Date_Vente descending
                          group d by new
                          {
                              Num_Vente = d.Num_Vente
                          } into gr
                          select new
                          {
                              Code_Barre = gr.FirstOrDefault().Barcode,
                              Marque = gr.FirstOrDefault().Produit,
                              Catégorie = gr.FirstOrDefault().Catégorie,
                              Taille = gr.FirstOrDefault().Taille,
                              Quantité = gr.Sum(x => x.Quantité),
                              Prix_Unité = gr.Sum(x => x.Prix_Unité),
                              Montant = gr.Sum(x => x.Montant),
                              Type = gr.FirstOrDefault().Type,
                              Date = gr.FirstOrDefault().Date_Vente,
                              Auteur = gr.FirstOrDefault().Auteur,
                              Num_Vente = gr.FirstOrDefault().Num_Vente,
                              Filiale = gr.FirstOrDefault().Filiale,
                              Mesure = gr.FirstOrDefault().Mesure,
                              Reduction = gr.Sum(x => x.Reduction),
                              Type_Base = donnée.tbl_stock_produits_vente.Where(x => x.Product_Id.ToString().Equals(gr.FirstOrDefault().Product_Id.ToString())).Select(x => x.Type).FirstOrDefault(),
                          };
                table.Qté = Convert.ToDecimal(don.Sum(x => x.Quantité));
                table.Montant = Convert.ToDecimal(don.Sum(x => x.Montant));
                table.Reduction = Convert.ToDecimal(don.Sum(x => x.Reduction));
                
                foreach (var item in don.OrderByDescending(x => x.Date))
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Num_Vente;
                    dr[1] = item.Code_Barre;
                    dr[2] = $"{item.Marque} {item.Catégorie} {item.Taille} {item.Type_Base}";
                    dr[3] = item.Filiale;
                    dr[4] = Convert.ToDecimal(item.Quantité).ToString("N0");
                    dr[5] = item.Mesure;
                    dr[6] = Convert.ToDecimal(item.Prix_Unité).ToString("N0");
                    dr[7] = Convert.ToDecimal(item.Montant).ToString("N0");
                    dr[8] = item.Type;
                    dr[9] = item.Date;
                    dr[10] = item.Auteur;

                    dt.Rows.Add(dr);

                    DataRow dr2 = dt2.NewRow();
                    dr2[0] = item.Num_Vente;
                    dr2[1] = item.Code_Barre;
                    dr2[2] = $"{item.Marque} {item.Catégorie} {item.Taille} {item.Type_Base}";
                    dr2[3] = item.Filiale;
                    dr2[4] = Convert.ToDecimal(item.Quantité);
                    dr2[5] = item.Mesure;
                    dr2[6] = Convert.ToDecimal(item.Prix_Unité);
                    dr2[7] = Convert.ToDecimal(item.Montant);
                    dr2[8] = item.Type;
                    dr2[9] = Convert.ToDateTime(item.Date.Value.Date);
                    dr2[10] = item.Auteur;

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
            //name = $"Rapport {types} {startDate.Value.Date.ToString("dd-MM-yyyy")}-{EndDate.Value.Date.ToString("dd-MM-yyyy")} {DateTime.Now.Date.ToString("dd-MM-yyyy HH.mm.ss")}.xlsx";
            //Print.PrintExcelFile(dataGridView2, "Rapport " + types, name, "Quitaye School");
            var file = "";
            if (txtsearch.Text != "")
            {
                file = $"C:/Quitaye School/Rapport {txtsearch} {startDate.Value.Date.ToString("dd-MM-yyyy")}-{EndDate.Value.Date.ToString("dd-MM-yyyy")} {DateTime.Now.Date.ToString("dd-MM-yyyy HH.mm.ss")}.xlsx";
            }
            else
            {
                file = $"C:/Quitaye School/Rapport {types} {startDate.Value.Date.ToString("dd - MM - yyyy")}-{EndDate.Value.Date.ToString("dd - MM - yyyy")} {DateTime.Now.Date.ToString("dd - MM - yyyy HH.mm.ss")}";
            }

            //PrintAction.Print.PrintExcelFile(dataGridView2, "Rapport Commane(s)", name, "Quitaye School");
            Alert.SShow("Génération Barcode en cours.. Veillez-patientez !", Alert.AlertType.Info);
            await Task.Run(() => Quitaye_School.Models.Docuement.ExportToExcel(dataGridView2, file));
            System.Diagnostics.Process.Start(file);
            Alert.SShow("Génération effectué avec succès.", Alert.AlertType.Sucess);
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            name = $"Rapport {types} {startDate.Value.Date.ToString("dd-MM-yyyy")}-{EndDate.Value.Date.ToString("dd-MM-yyyy")} {DateTime.Now.Date.ToString("dd-MM-yyyy HH.mm.ss")}.xlsx";
            Print.PrintPdfFile(dataGridView3, name, "Rapport " + types, "Opération(s)", types, mycontrng, "Quitaye School", true);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (cbxType.Text != "" && cbxType.Text != "Tout")
            {
                name = "Rapport " + types + " " + cbxType.Text + " " + startDate.Value.Date.ToString("dd-MM-yyyy") + " " + EndDate.Value.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            }
            else if(txtsearch.Text != "")
            {
                name = "Rapport " + types + " " + txtsearch.Text+ " " + startDate.Value.Date.ToString("dd-MM-yyyy") + " " + EndDate.Value.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            }   
            else
            {
                name = "Rapport " + types + " "  + startDate.Value.Date.ToString("dd-MM-yyyy") + " " + EndDate.Value.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            }
        }


       
        private async void txtsearch_TextChanged(object sender, EventArgs e)
        {
            if(txtsearch.Text != "")
            {
                await  CallSearch(txtsearch.Text);
                
            }
            else
            {
                await ShowData();
            }
        }

        async Task CallSearch(string text)
        {
            var result = await SearchAsync(text);
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = result.MyTables.Item1;
            dataGridView2.DataSource = result.MyTables.Item2;
            dataGridView3.DataSource = result.MyTables.Item1; 
            //if(result.MyTables.Item1.Columns.Count > 0) 
            //dataGridView1.Columns[0].Visible = false;
            dataGridView2.DataSource = result.MyTable;
            lblMontant.Text = $"Montant : {result.Montant.ToString("N0")} FCFA, Réduction : {result.Reduction.ToString("N0")} FCFA, Montant Net : {(result.Montant - result.Reduction).ToString("N0")} FCFA";
            lblQty.Text = "Quantité : " + result.Qté.ToString("N0");
            //try
            //{
            //    if(Principales.type_compte.Contains("Administrateur"))
            //    AddColumns.Addcolumn(dataGridView1);
            //    var selection = new DataGridViewCheckBoxColumn();
            //    selection.Name = "Select";
            //    selection.HeaderText = "Select";
            //    selection.Width = 40;

            //    dataGridView1.Columns.Add(selection);
            //    dataGridView1.Columns["Edit"].Visible = false;
            //}
            //catch (Exception)
            //{

            //}
        }
        private static string type;
        private Task<SearchedTable> SearchAsync(string text)
        {
            return Task.Factory.StartNew(() => Search(text));
        }
        public SearchedTable Search(string text)
        {
            using(var donnée = new QuitayeContext())
            {
                    SearchedTable table = new SearchedTable();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Num_Vente");
                    dt.Columns.Add("Code Barre");
                    dt.Columns.Add("Désignation");
                    dt.Columns.Add("Filiale");
                    dt.Columns.Add("Quantité");
                    dt.Columns.Add("Meusre");
                    dt.Columns.Add("Prix_Unité");
                    dt.Columns.Add("Montant");
                    dt.Columns.Add("Type");
                    dt.Columns.Add("Date");
                    dt.Columns.Add("Auteur");

                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("Num_Vente");
                    dt2.Columns.Add("Code Barre");
                    dt2.Columns.Add("Désignation");
                    dt2.Columns.Add("Filiale");
                    dt2.Columns.Add("Quantité", typeof(decimal));
                    dt2.Columns.Add("Meusre");
                    dt2.Columns.Add("Prix_Unité", typeof(decimal));
                    dt2.Columns.Add("Montant", typeof(decimal));
                    dt2.Columns.Add("Type");
                    dt2.Columns.Add("Date", typeof(DateTime));
                    dt2.Columns.Add("Auteur");

                    if (type == "" || type == "Tout" || type == null)
                    {
                        var don = from d in donnée.tbl_vente
                                  where (d.Auteur.Contains(text) || d.Client.Contains(text)
                                  || d.Num_Vente.Contains(text)
                                  || d.Catégorie.Contains(text) || d.Dept_Auteur.Contains(text)
                                  || d.Taille.Contains(text) || d.Produit.Contains(text)
                                  || d.Barcode.Contains(text) || d.Usage.Contains(text)
                                  || d.Num_Client.Contains(text) || d.Filiale.Contains(text) || d.Type.Contains(text)) 
                                  && DbFunctions.TruncateTime(d.Date_Vente) <= DbFunctions.TruncateTime(EndDate.Value)
                                  && DbFunctions.TruncateTime(d.Date_Vente) >= DbFunctions.TruncateTime(startDate.Value)
                                  orderby d.Date_Vente descending
                                  group d by new
                                  {
                                      Num_Vente = d.Num_Vente
                                  } into gr
                                  select new
                                  {
                                      Code_Barre = gr.FirstOrDefault().Barcode,
                                      Marque = gr.FirstOrDefault().Produit,
                                      Catégorie = gr.FirstOrDefault().Catégorie,
                                      Taille = gr.FirstOrDefault().Taille,
                                      Quantité = gr.Sum(x => x.Quantité),
                                      Prix_Unité = gr.Sum(x => x.Prix_Unité),
                                      Montant = gr.Sum(x => x.Montant),
                                      Type = gr.FirstOrDefault().Type,
                                      Date = gr.FirstOrDefault().Date_Vente,
                                      Auteur = gr.FirstOrDefault().Auteur,
                                      Num_Vente = gr.FirstOrDefault().Num_Vente,
                                      Filiale = gr.FirstOrDefault().Filiale,
                                      Mesure = gr.FirstOrDefault().Mesure,
                                      Reduction = gr.Sum(x => x.Reduction),
                                      Type_Base = donnée.tbl_stock_produits_vente.Where(x => x.Product_Id.ToString().Equals(gr.FirstOrDefault().Product_Id.ToString())).Select(x => x.Type).FirstOrDefault(),
                                  };
                    table.Qté = Convert.ToDecimal(don.Sum(x => x.Quantité));
                    table.Montant = Convert.ToDecimal(don.Sum(x => x.Montant));
                    table.Reduction = Convert.ToDecimal(don.Sum(x => x.Reduction));

                    foreach (var item in don.OrderByDescending(x => x.Date))
                    {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.Num_Vente;
                            dr[1] = item.Code_Barre;
                            if(!string.IsNullOrEmpty(item.Type_Base)) 
                                dr[2] = $"{item.Marque} {item.Catégorie} {item.Taille} {item.Type_Base}";
                            else dr[2] = $"{item.Marque} {item.Catégorie} {item.Taille}";
                            
                            dr[3] = item.Filiale;
                            dr[4] = Convert.ToDecimal(item.Quantité).ToString("N0");
                            dr[5] = item.Mesure;
                            dr[6] = Convert.ToDecimal(item.Prix_Unité).ToString("N0");
                            dr[7] = Convert.ToDecimal(item.Montant).ToString("N0");
                            dr[8] = item.Type;
                            dr[9] = item.Date;
                            dr[10] = item.Auteur;

                            dt.Rows.Add(dr);

                            DataRow dr2 = dt2.NewRow();
                            dr2[0] = item.Num_Vente;
                            dr2[1] = item.Code_Barre;
                        if (!string.IsNullOrEmpty(item.Type_Base))
                            dr2[2] = $"{item.Marque} {item.Catégorie} {item.Taille} {item.Type_Base}";
                        else dr2[2] = $"{item.Marque} {item.Catégorie} {item.Taille}";
                        
                            dr2[3] = item.Filiale;
                            dr2[4] = Convert.ToDecimal(item.Quantité);
                            dr2[5] = item.Mesure;
                            dr2[6] = Convert.ToDecimal(item.Prix_Unité);
                            dr2[7] = Convert.ToDecimal(item.Montant);
                            dr2[8] = item.Type;
                            dr2[9] = Convert.ToDateTime(item.Date.Value);
                            dr2[10] = item.Auteur;

                            dt2.Rows.Add(dr2);
                        }
                        table.Qté = Convert.ToDecimal(don.Sum(x => x.Quantité));
                        table.Montant = Convert.ToDecimal(don.Sum(x => x.Montant));
                        table.MyTables = (dt, dt2);
                        return table;
                    }
                    else
                    {
                        var don = from d in donnée.tbl_vente
                                  where (d.Auteur.Contains(text) || d.Client.Contains(text) || d.Num_Vente.Contains(text)
                                  || d.Catégorie.Contains(text) || d.Dept_Auteur.Contains(text)
                                  || d.Taille.Contains(text) || d.Produit.Contains(text) 
                                  || d.Barcode.Contains(text)
                                  || d.Num_Client.Contains(text) || d.Filiale.Contains(text) || d.Type.Contains(text)) 
                                  && DbFunctions.TruncateTime(d.Date_Vente) <= DbFunctions.TruncateTime(EndDate.Value)
                                 && DbFunctions.TruncateTime(d.Date_Vente) >= DbFunctions.TruncateTime(startDate.Value)
                                 && d.Type == type
                                  orderby d.Date_Vente descending
                                  group d by new
                                  {
                                      Num_Vente = d.Num_Vente
                                  } into gr
                                  select new
                                  {
                                      Code_Barre = gr.FirstOrDefault().Barcode,
                                      Marque = gr.FirstOrDefault().Produit,
                                      Catégorie = gr.FirstOrDefault().Catégorie,
                                      Taille = gr.FirstOrDefault().Taille,
                                      Quantité = gr.Sum(x => x.Quantité),
                                      Prix_Unité = gr.Sum(x => x.Prix_Unité),
                                      Montant = gr.Sum(x => x.Montant),
                                      Type = gr.FirstOrDefault().Type,
                                      Date = gr.FirstOrDefault().Date_Vente,
                                      Auteur = gr.FirstOrDefault().Auteur,
                                      Num_Vente = gr.FirstOrDefault().Num_Vente,
                                      Filiale = gr.FirstOrDefault().Filiale,
                                      Mesure = gr.FirstOrDefault().Mesure,
                                      Reduction = gr.Sum(x => x.Reduction),
                                      Type_Base = donnée.tbl_stock_produits_vente.Where(x => x.Code_Barre.Equals(gr.FirstOrDefault().Barcode)).Select(x => x.Type).FirstOrDefault(),
                                  };

                    table.Qté = Convert.ToDecimal(don.Sum(x => x.Quantité));
                    table.Montant = Convert.ToDecimal(don.Sum(x => x.Montant));
                    table.Reduction = Convert.ToDecimal(don.Sum(x => x.Reduction));

                    foreach (var item in don.OrderByDescending(x => x.Date))
                    {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.Num_Vente;
                            dr[1] = item.Code_Barre;
                            if (!string.IsNullOrEmpty(item.Type_Base))
                                dr[2] = $"{item.Marque} {item.Catégorie} {item.Taille} {item.Type_Base}";
                            else dr[2] = $"{item.Marque} {item.Catégorie} {item.Taille}";
                            
                            dr[3] = item.Filiale;
                            dr[4] = Convert.ToDecimal(item.Quantité).ToString("N0");
                            dr[5] = item.Mesure;
                            dr[6] = Convert.ToDecimal(item.Prix_Unité).ToString("N0");
                            dr[7] = Convert.ToDecimal(item.Montant).ToString("N0");
                            dr[8] = item.Type;
                            dr[9] = item.Date;
                            dr[10] = item.Auteur;

                            dt.Rows.Add(dr);

                            DataRow dr2 = dt2.NewRow();
                            dr2[0] = item.Num_Vente;
                            dr2[1] = item.Code_Barre;
                        if (!string.IsNullOrEmpty(item.Type_Base))
                            dr2[2] = $"{item.Marque} {item.Catégorie} {item.Taille} {item.Type_Base}";
                        else dr2[2] = $"{item.Marque} {item.Catégorie} {item.Taille}";
                        dr2[3] = item.Filiale;
                            dr2[4] = Convert.ToDecimal(item.Quantité);
                            dr2[5] = item.Mesure;
                            dr2[6] = Convert.ToDecimal(item.Prix_Unité);
                            dr2[7] = Convert.ToDecimal(item.Montant);
                            dr2[8] = item.Type;
                            dr2[9] = Convert.ToDateTime(item.Date.Value);
                            dr2[10] = item.Auteur;

                            dt2.Rows.Add(dr2);
                        }
                        table.Qté = Convert.ToDecimal(don.Sum(x => x.Quantité));
                        table.Montant = Convert.ToDecimal(don.Sum(x => x.Montant));
                        table.MyTables = (dt, dt2);
                        return table;
                    }
            }
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(types == "Vente")
            {
                if(dataGridView1.Columns.Count >= 2)
                if(e.ColumnIndex >= 0)
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
                                                            && d.Detachement == "Siège" select d).First();
                                            }
                                            else stock = (from d in donnée.tbl_stock_produits_vente 
                                                            where d.Code_Barre == v.Barcode 
                                                            && d.Detachement == v.Filiale select d).First();
                                            var ms = (from d in donnée.tbl_mesure_vente 
                                                        where d.Nom == v.Mesure select d).First();

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
                    else if (e.ColumnIndex == 0)
                    {
                        string num_vente = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        if (num_vente != null && num_vente != "")
                        {
                            var height = 600;
                            var width = 1169;
                            if (Minimize)
                            {
                                height = 763;
                                width = 1024;
                            }
                            Details_Vente vente = new Details_Vente(num_vente, width, height);
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
                                    if(valeur.Cloturé != "Oui")
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
                    else if (e.ColumnIndex == 1)
                    {
                        string num_vente = dataGridView1.CurrentRow.Cells[1].Value.ToString();
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


        private async Task SetCaisse()
        {
            using(var donnée = new QuitayeContext())
            {
                var vente_sans_payement = (from d in donnée.tbl_vente
                                           join pay in donnée.tbl_payement on d.Num_Vente equals pay.Num_Opération into joinedTable
                                           from p in joinedTable.DefaultIfEmpty()
                                           where p == null
                                           select new
                                           {
                                               Id = d.Id,
                                               Barcode = d.Barcode,
                                               Date_Vente = d.Date_Vente,
                                               Num_Vente = d.Num_Vente,
                                           });

                foreach (var item in vente_sans_payement)
                {
                    
                }
            }
        }

        private async Task ShowData()
        {
            if (cbxType.Text == "" || cbxType.Text == "Tout")
            {
                await CallData();
            }
            else 
            { 
                if(!Minimize)
                await CallDataFiltre();
                else await CallData();
            }
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
                if(types == "Vente")
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
                    
                    
                    if(st.Type != null)
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

}
