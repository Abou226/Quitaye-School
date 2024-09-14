using FontAwesome.Sharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PrintAction;
using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using Quitaye_Medical.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace Quitaye_School.User_Interface
{
    public partial class Boutique : Form
    {
        private string mycontrng = LogIn.mycontrng;
        private Timer timer = new Timer();
        private string name;
        private Timer loadTimer = new Timer();
        public Boutique()
        {
            this.InitializeComponent();
            this.timer.Enabled = false;
            this.timer.Interval = 10;
            this.timer.Start();
            this.timer.Tick += this.Timer_Tick;
            this.loadTimer.Enabled = false;
            this.loadTimer.Interval = 10;
            this.loadTimer.Start();
            this.loadTimer.Tick += this.LoadTimer_Tick;
            this.dataGridView1.CellClick += this.DataGridView1_CellClick1;
            this.btnNouvelleInventaire.Click += this.BtnNouvelleInventaire_Click;
            this.btnInitialiserStock.Click += this.BtnInitialiserStock_Click;
            this.btnPreview.Enabled = false;
            this.btnPreview.Click += this.BtnPreview_Click;
            this.btnNext.Click += this.BtnNext_Click;
            btnAjouter.Click += btnAjouter_Click;
            btnImporterExcel.Click += BtnImporterExcel_Click;
            btnEttiquetteProduit.Click += BtnEttiquetteProduit_Click;
            dataGridView1.DoubleClick += DataGridView1_DoubleClick;
            btnExcel.Click += btnExcel_Click;
            btnPdf.Click += btnPDF_Click;
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyPress += TxtSearch_KeyPress;
            btnProduitSimple.Click += BtnProduitSimple_Click;
            cbxCatégorie.SelectedIndexChanged += CbxCatégorie_SelectedIndexChanged;
            cbxMarque.SelectedIndexChanged += CbxMarque_SelectedIndexChanged;
            btnNonPrice.Click += BtnNonPrice_Click;
            btnCheckPrice.Click += BtnCheckPrice_Click;
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            var code = dataGridView1.CurrentRow.Cells["Code_Barre"].Value.ToString();
            var filiale = dataGridView1.CurrentRow.Cells["Filiale"].Value.ToString();
            var ravitaillement = new RavitaillementStock(filiale, new List<string>() { code.Split(',').FirstOrDefault()});
            ravitaillement.ShowDialog();
        }

        private async void BtnCheckPrice_Click(object sender, EventArgs e)
        {
            //var price_check = new Price_Check();
            //price_check.ShowDialog();
        }

        private async void BtnNonPrice_Click(object sender, EventArgs e)
        {
            await CallNonPrice();
        }

        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                txtSearch_TextChanged(sender, null);
            }
        }

        private void CbxMarque_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void CbxCatégorie_SelectedIndexChanged(object sender, EventArgs e)
        {
               
        }

        private async void BtnProduitSimple_Click(object sender, EventArgs e)
        {
            //var simple = new Produit_Simple();
            //simple.ShowDialog();
            //if(simple.Ok == "Oui")
            //{
            //    await CallData();
            //}
        }

        private void BtnEttiquetteProduit_Click(object sender, EventArgs e)
        {
            var ettiquette = new Ettiquette_Produit();
            ettiquette.ShowDialog();
        }

        private async void BtnImporterExcel_Click(object sender, EventArgs e)
        {
            var import = new Page_Importation();
            import.ShowDialog();
            if (import.Ok)
            {
                await CallData();
            }
        }

        public int Pages_Count { get; set; } = 1;

        public int LastSkip { get; set; }

        public int CurrentTotal { get; set; }
        public int AllCurrentTotal { get; set; }

        public static int nTotalRow { get; set; }
        public static int AllnTotalRow { get; set; }

        public static int pTotalRow { get; set; }
        public static int AllpTotalRow { get; set; }

        public static int nSkkipedRows { get; set; }

        public static int pSkkipedRows { get; set; }

        public static int Total { get; set; }
        public static int AllTotal { get; set; }

        private async void BtnNext_Click(object sender, EventArgs e)
        {
            Next = true;
            this.Skip += 100;
            await CallData(this.Skip);
            int total_page_nb = (Boutique.nTotalRow / 100)+1;
            ++this.Pages_Count;
            //this.lblCurrentPage.Text = string.Format("{0}/{1} pages", this.Pages_Count, total_page_nb);
        }
        public bool Next { get; set; }
        private async void BtnPreview_Click(object sender, EventArgs e)
        {
            Next = false;
            this.Skip -= 100;
            await this.CallData(this.Skip);
            int total_page_nb = (Boutique.nTotalRow / 100)+1;
            --this.Pages_Count;
            //this.lblCurrentPage.Text = string.Format("{0}/{1} pages", this.Pages_Count, total_page_nb);
        }

        public int Skip { get; set; } = 0;

        private async void BtnInitialiserStock_Click(object sender, EventArgs e)
        {
            MsgBox msg = new MsgBox();
            msg.show("Voulez-vous vraiment reinitialiser le stock ?", "Reinitialisation", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Warning);
            int num1 = (int)msg.ShowDialog();
            if (msg.clicked == "Non")
                msg = (MsgBox)null;
            else if (!(msg.clicked == "Oui"))
            {
                msg = (MsgBox)null;
            }
            else
            {
                int num2 = await SaveInitialisationAsync() ? 1 : 0;
                Alert.SShow("Stock Reinitialiser avec succès.", Alert.AlertType.Sucess);
                await CallData();
                msg = null;
            }
        }

        private async Task<bool> SaveInitialisationAsync()
        {
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_stock_produits_vente
                    .Where(d => d.Quantité != 0).Select(x => new { Id = x.Id });
                foreach (var data in source)
                {
                    var item = data;
                    financeDataContext.tbl_stock_produits_vente.Where(d => d.Id == item.Id).First().Quantité = new Decimal?(0M);
                    await financeDataContext.SaveChangesAsync();
                }
                return true;
            }
        }

        private void BtnNouvelleInventaire_Click(object sender, EventArgs e)
        {
            //int num = (int)new Nouvelle_Inventaire().ShowDialog();
        }

        private async void DataGridView1_CellClick1(object sender, DataGridViewCellEventArgs e)
        {
            if (!Principales.type_compte.Contains("Administrateur"))
                return;
            if(dataGridView1.Columns.Count > 1)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                if (e.ColumnIndex >= 0)
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Edit"))
                    {
                        N_Produit result = await ProductAsync(id);
                        Nouveau_Produit nouveau = new Nouveau_Produit(result);
                        nouveau.btnAjouter.IconChar = IconChar.Edit;
                        nouveau.btnAjouter.Text = "Modifier";
                        int num = (int)nouveau.ShowDialog();
                        if (nouveau.ok == "Oui")
                            await CallData();
                        result = (N_Produit)null;
                        nouveau = (Nouveau_Produit)null;
                    }
                    else if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Sup"))
                    {
                        MsgBox msg = new MsgBox();
                        msg.show("Voulez-vous supprimer cet élément ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                        int num = (int)msg.ShowDialog();
                        if (msg.clicked == "Non")
                            return;
                        if (msg.clicked == "Oui")
                        {
                            if (await DeleteDataAsync(id).Result)
                            {
                                Alert.SShow("Element supprimé avec succès.", Alert.AlertType.Sucess);
                                await CallData();
                            }
                        }
                        msg = (MsgBox)null;
                    }
                    else if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Code_Barre"))
                    {
                        var code_barre = dataGridView1.CurrentRow.Cells["Code_Barre"].Value.ToString();
                        if (!string.IsNullOrEmpty(code_barre))
                        {
                            var filiale = dataGridView1.CurrentRow.Cells["Filiale"].Value.ToString();

                            var barcodes = new List<string>();
                            if (code_barre.Contains(","))
                            {
                                var codes = code_barre.Split(',');
                                foreach (var item in codes)
                                {
                                    barcodes.Add(item);
                                }
                            }
                            else barcodes.Add(code_barre);
                            var expiration = new Expiration_Page(barcodes, filiale);
                            expiration.ShowDialog();
                        }
                    }
                }
            }
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            this.loadTimer.Stop();
            await this.CallTask();
        }

        private async Task CallTask()
        {
            Task<DataTable> taille = this.FillCbxTailleAsync();
            Task<DataTable> catégorie = this.FillCbxCatégorieAsync();
            Task<DataTable> marque = this.FillCbxMarqueAsync();
            Task<MyTable> filldata = this.FillDataAsync();
            Task<MyTable> alldata = this.FillAllDataAsync();
            var type = FillCbxTypeAsync();
            List<Task> taskList = new List<Task>()
            {
                taille,
                catégorie,
                marque,
                filldata,
                alldata,
            };
            while (taskList.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(taskList);
                if (finishedTask == catégorie)
                {
                    this.cbxCatégorie.DataSource = catégorie.Result;
                    this.cbxCatégorie.DisplayMember = "Catégorie";
                    this.cbxCatégorie.ValueMember = "Id";
                    this.cbxCatégorie.Text = null;
                }
                else if(finishedTask == alldata)
                {
                    if (filldata.Result.Table.Rows.Count == 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Tableau Vide");
                        DataRow dr = dt.NewRow();
                        dr[0] = "Aucune donnée dans ce tableau";
                        dt.Rows.Add(dr);
                        this.dataGridView1.DataSource = alldata.Result.Table;
                        this.lblMontant.Text = "Estimatif Total Achat : " + alldata.Result.Montant_Achat.ToString("N0") + " FCFA, Estimatif Total Vente : " + filldata.Result.Montant_Vente.ToString("N0") + " FCFA";
                        dt = (DataTable)null;
                        dr = (DataRow)null;
                    }
                    else
                    {
                        this.dataGridView2.DataSource = alldata.Result.Table;
                        
                        this.lblMontant.Text = "Estimatif Total Achat : " + alldata.Result.Montant_Achat.ToString("N0") + " FCFA, Estimatif Total Vente : " + filldata.Result.Montant_Vente.ToString("N0") + " FCFA";
                        
                    }
                }
                else if (finishedTask == marque)
                {
                    this.cbxMarque.DataSource = marque.Result;
                    this.cbxMarque.DisplayMember = "Marque";
                    this.cbxMarque.ValueMember = "Id";
                    this.cbxMarque.Text = null;
                }
                else if (finishedTask == filldata)
                {
                    this.dataGridView1.Columns.Clear();
                    Boutique.nTotalRow = filldata.Result.Total_Quantité;
                    Boutique.pTotalRow = filldata.Result.Total_Quantité;
                    Boutique.Total = filldata.Result.Total_Quantité;
                    CurrentTotal += Convert.ToInt32(filldata.Result.Quantité);
                    this.LastSkip = Convert.ToInt32(filldata.Result.Quantité);
                    if (filldata.Result.Table.Rows.Count == 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Tableau Vide");
                        DataRow dr = dt.NewRow();
                        dr[0] = "Aucune donnée dans ce tableau";
                        dt.Rows.Add(dr);
                        this.dataGridView1.DataSource = dt;
                        this.btnPreview.Enabled = false;
                        this.lblMontant.Text = "Estimatif Total Achat : " + filldata.Result.Montant_Achat.ToString("N0") + " FCFA, Estimatif Total Vente : " + filldata.Result.Montant_Vente.ToString("N0") + " FCFA";
                        dt = (DataTable)null;
                        dr = (DataRow)null;
                    }
                    else
                    {
                        this.dataGridView1.DataSource = filldata.Result.Table;
                        this.dataGridView2.DataSource = filldata.Result.Table;
                        dataGridView1.Columns[0].Visible = false;
                        int total_page_nb = (Boutique.nTotalRow / 100)+1;
                        this.lblCurrentPage.Text = string.Format("{0}/{1} pages", this.Pages_Count, total_page_nb);
                        this.btnPreview.Enabled = false;
                        
                        if (Boutique.nTotalRow > this.CurrentTotal)
                            this.btnNext.Enabled = true;
                        else if (this.CurrentTotal <= Boutique.nTotalRow)
                            this.btnNext.Enabled = false;
                        this.lblMontant.Text = "Estimatif Total Achat : " + filldata.Result.Montant_Achat.ToString("N0") + " FCFA, Estimatif Total Vente : " + filldata.Result.Montant_Vente.ToString("N0") + " FCFA";
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                        try
                        {
                            AddColumns.Addcolumn(this.dataGridView1);
                        }
                        catch (Exception ex)
                        {
                        }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
                    }
                }
                else if (finishedTask == taille)
                {
                    this.cbxTaille.DataSource = taille.Result;
                    this.cbxTaille.DisplayMember = "Taille";
                    this.cbxTaille.ValueMember = "Id";
                    this.cbxTaille.Text = null;
                }
                taskList.Remove(finishedTask);
                finishedTask = (Task)null;
            }
            taille = (Task<DataTable>)null;
            catégorie = (Task<DataTable>)null;
            marque = (Task<DataTable>)null;
            filldata = (Task<MyTable>)null;
            taskList = (List<Task>)null;
        }

        private async Task CallTaill()
        {
            DataTable result = await this.FillCbxTailleAsync();
            this.cbxTaille.DataSource = result;
            this.cbxTaille.DisplayMember = "Taille";
            this.cbxTaille.ValueMember = "Id";
            this.cbxTaille.Text = null;
            result = (DataTable)null;
        }

        private Task<DataTable> FillCbxTailleAsync() => Task.Factory.StartNew((() => fillCbxTaille()));

        private DataTable fillCbxTaille()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Taille");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_taille.OrderBy(d => d.Taille).Select(d => new {Id = d.Id, Taille = d.Taille});
                
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Taille;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        private Task<DataTable> FillCbxTypeAsync() => Task.Factory.StartNew(() => this.fillCbxType());

        private DataTable fillCbxType()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Type");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_type.OrderBy(d => d.Type).Select(d => new { Id = d.Id, Type = d.Type });

                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Type;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }


       
        private async void CallCatégorie()
        {
            DataTable result = await FillCbxCatégorieAsync();
            cbxCatégorie.DataSource = result;
            cbxCatégorie.DisplayMember = "Catégorie";
            cbxCatégorie.ValueMember = "Id";
            cbxCatégorie.Text = null;
            result = (DataTable)null;
        }

        private Task<DataTable> FillCbxCatégorieAsync() => Task.Factory.StartNew<DataTable>((() => this.FillCbxCatégorie()));

        private DataTable FillCbxCatégorie()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Catégorie");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_catégorie.OrderBy(d => d.Catégorie)
                    .Select(d => new {Id = d.Id, Catégorie = d.Catégorie});
                
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Catégorie;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        private async void CallMarque()
        {
            DataTable result = await FillCbxMarqueAsync();
            cbxMarque.DataSource = result;
            cbxMarque.DisplayMember = "Model";
            cbxMarque.ValueMember = "Id";
            cbxMarque.Text = null;
            result = (DataTable)null;
        }

        private Task<DataTable> FillCbxMarqueAsync() => Task.Factory.StartNew((() => FillCbxMarque()));

        private DataTable FillCbxMarque()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Marque");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_marque.OrderBy(d => d.Nom).Select(d => new
                {
                    Id = d.Id,
                    Nom = d.Nom
                });
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Nom;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        private async void btnExcel_Click(object sender, EventArgs e)
        {
            //Print.PrintExcelFile(this.dataGridView2, "Inventaire Stock", this.name, "Quitaye School");
            Alert.SShow("Génération Fichier en cours.. Veillez-patientez !", Alert.AlertType.Info);
            var file = $"C:/Quitaye School/List Inventaire {DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")}.xlsx";
            await Task.Run(() => Quitaye_School.Models.Docuement.ExportToExcel(this.dataGridView2, file));
            System.Diagnostics.Process.Start(file);
            Alert.SShow("Génération effectué avec succès.", Alert.AlertType.Sucess);
        }

        private void btnPDF_Click(object sender, EventArgs e) 
            => Print.PrintPdfFile(this.dataGridView2, this.name, "Inventaire Stock Produit", "Décompte", "Produit", this.mycontrng, "Quitaye School", true);

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.cbxCatégorie.Text != "" || this.cbxMarque.Text != "" || this.cbxTaille.Text != "")
                this.name = "Inventaire Stock Produit " + this.cbxMarque.Text + " " + this.cbxCatégorie.Text + " " + this.cbxTaille.Text + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            else
                this.name = "Inventaire Stock Produit " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
        }

        
        private async Task<N_Produit> ProductAsync(int id)
        {
            N_Produit nProduit = new N_Produit();
            using (var financeDataContext = new QuitayeContext())
            {
                var de = financeDataContext.tbl_stock_produits_vente.Where(d => d.Id == id).First();
                var des = new Models.Context.tbl_produits();
                if (string.IsNullOrEmpty(de.Code_Barre))
                {
                  des = financeDataContext.tbl_produits.Where(d => d.Nom == de.Marque
                  && d.Catégorie == de.Catégorie
                  && d.Taille == de.Taille
                  && d.Type == de.Type).FirstOrDefault();
                } 
                else
                {
                    des = financeDataContext.tbl_produits.Where(d => d.Barcode == de.Code_Barre).FirstOrDefault();
                    if(des != null && des.Type == null && de.Type != null)
                    {
                        des.Type = de.Type;
                        await financeDataContext.SaveChangesAsync();
                        des = financeDataContext.tbl_produits.Where(d => d.Barcode == de.Code_Barre).FirstOrDefault();
                    }
                }

                if (des == null)
                {
                    var pros = await Page_Importation.NewProduct(de.Type, "1", de.Marque, de.Catégorie, de.Taille, 0, 0, "PIECE", Convert.ToInt32(de.Formule), de.Code_Barre);
                    if (string.IsNullOrEmpty(de.Code_Barre))
                    {
                        des = financeDataContext.tbl_produits.Where(d => d.Nom == de.Marque
                      && d.Catégorie == de.Catégorie
                      && d.Taille == de.Taille
                      && d.Type == de.Type).FirstOrDefault();
                    }
                    else
                    {
                        des = financeDataContext.tbl_produits.Where(d => d.Barcode == de.Code_Barre).FirstOrDefault();
                    }
                }
                 
                var formuleMesureVente = financeDataContext.tbl_formule_mesure_vente.Where(d => d.Id == de.Formule).First();
                nProduit.Catégorie = des.Catégorie;
                nProduit.Code_Barre = des.Barcode;
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                try
                {
                    if(des.Image != null)
                    nProduit.Image_Byte = des.Image.ToArray();
                }
                catch (Exception ex)
                {
                    nProduit.Image = null;
                }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
                nProduit.Taille = des.Taille;
                nProduit.Code_Barre = des.Barcode;
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                try
                {
                    if( des.Image != null)
                    nProduit.Image = this.ByteArrayToImage(des.Image.ToArray());
                }
                catch (Exception ex)
                {
                    nProduit.Image = null;
                }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
                var source = financeDataContext.tbl_mesure_vente.OrderBy((d => d.Niveau)).Take(1);
                nProduit.Formule = Convert.ToInt32(des.Formule_Stockage);
                nProduit.Prix_Petit = Convert.ToDecimal(des.Prix_Petit);
                nProduit.Prix_Moyen = Convert.ToDecimal(des.Prix_Moyen);
                nProduit.Prix_Grand = Convert.ToDecimal(des.Prix_Grand);
                nProduit.Prix_Large = Convert.ToDecimal(des.Prix_Large);
                nProduit.Prix_Hyper_Large = Convert.ToDecimal(des.Prix_Hyper_Large);
                nProduit.Prix_Achat_Petit = Convert.ToDecimal(des.Prix_Achat_Petit);
                nProduit.Prix_Achat_Moyen = Convert.ToDecimal(des.Prix_Achat_Moyen);
                nProduit.Prix_Achat_Grand = Convert.ToDecimal(des.Prix_Achat_Grand);
                nProduit.Prix_Achat_Large = Convert.ToDecimal(des.Prix_Achat_Large);
                nProduit.Prix_Achat_Hyper_Large = Convert.ToDecimal(des.Prix_Achat_Hyper_Large);
                nProduit.Prix_Petit_Grossiste = Convert.ToDecimal(des.Prix_Petit_Grossiste);
                nProduit.Prix_Moyen_Grossiste = Convert.ToDecimal(des.Prix_Moyen_Grossiste);
                nProduit.Prix_Grand_Grossiste = Convert.ToDecimal(des.Prix_Grand_Grossiste);
                nProduit.Prix_Large_Grossiste = Convert.ToDecimal(des.Prix_Large_Grossiste);
                nProduit.Prix_Hyper_Large_Grossiste = Convert.ToDecimal(des.Prix_Hyper_Large_Grossiste);
                nProduit.Nom = des.Nom;
                if (source.Count() != 0)
                    nProduit.Mesure = source.First().Nom;
                nProduit.Stock_Max = (Decimal)Convert.ToInt32(des.Stock_max);
                nProduit.Stock_Min = (Decimal)Convert.ToInt32(des.Stock_min);
                nProduit.N_Formule = formuleMesureVente.Formule;
                nProduit.Quantité = Convert.ToDecimal(de.Quantité);
                nProduit.Prix_Achat = Convert.ToDecimal(des.Prix_Achat);
                nProduit.Type = des.Type;
                nProduit.Id = id;
                nProduit.Product_Id = des.Id;
            }
            return nProduit;
        }

        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!Principales.type_compte.Contains("Administrateur"))
                return;
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Edit"))
            {
                N_Produit result = await ProductAsync(id);
                Nouveau_Produit nouveau = new Nouveau_Produit(result);
                nouveau.btnAjouter.IconChar = IconChar.Edit;
                nouveau.btnAjouter.Text = "Modifier";
                nouveau.ShowDialog();
                if (nouveau.ok == "Oui")
                    await CallData();
                result = (N_Produit)null;
                nouveau = (Nouveau_Produit)null;
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Sup"))
            {
                MsgBox msg = new MsgBox();
                msg.show("Voulez-vous supprimer cet élément ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                int num = (int)msg.ShowDialog();
                if (msg.clicked == "Non")
                    return;
                if (msg.clicked == "Oui")
                {
                    if (await this.DeleteDataAsync(id).Result)
                    {
                        Alert.SShow("Element supprimé avec succès.", Alert.AlertType.Sucess);
                        await this.CallData();
                    }
                    else
                        Alert.SShow("Suppression non effectué!", Alert.AlertType.Info);
                }
                msg = (MsgBox)null;
            }
        }

        private Task<Task<bool>> DeleteDataAsync(int id) => Task.Factory.StartNew<Task<bool>>((Func<Task<bool>>)(() => this.DeleteData(id)));

        private async Task<bool> DeleteData(int id)
        {
            using (var donnée = new QuitayeContext())
            {
                if (!Principales.type_compte.Contains("Administrateur"))
                    return false;
                try
                {
                    var sto = donnée.tbl_stock_produits_vente.Where(d => d.Id == id).First();
                    if(sto.Code_Barre != null)
                    {
                        var st = donnée.tbl_stock_produits_vente.Where(d => d.Code_Barre == sto.Code_Barre).Select(d => new
                        {
                            Id = d.Id
                        });
                        donnée.tbl_stock_produits_vente.Remove(sto);
                        if (st.Count() == 1)
                        {
                            var psf = donnée.tbl_produits.Where(d => d.Barcode == sto.Code_Barre);
                            donnée.tbl_produits.Remove(psf.First());
                            psf = null;
                        }
                        await donnée.SaveChangesAsync();
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(sto.Details)) 
                        {
                            var st = donnée.tbl_stock_produits_vente.Where(d => d.Details == sto.Details).Select(d => new
                            {
                                Id = d.Id
                            });
                            donnée.tbl_stock_produits_vente.Remove(sto);
                            if (st.Count() == 1)
                            {
                                var psf = donnée.tbl_produits.Where(d => d.Details == sto.Details);
                                donnée.tbl_produits.Remove(psf.First());
                                psf = null;
                            }
                            await donnée.SaveChangesAsync();
                        } 
                        else
                        {
                            var st = donnée.tbl_stock_produits_vente.Where(d => d.Marque == sto.Marque
                        && d.Catégorie == sto.Catégorie && d.Taille == sto.Taille && d.Type == sto.Type && d.Code_Barre == null).Select(d => new
                        {
                            Id = d.Id
                        });
                            donnée.tbl_stock_produits_vente.Remove(sto);
                            if (st.Count() == 1)
                            {
                                var psf = donnée.tbl_produits.Where(d => d.Nom == sto.Marque
                                && d.Catégorie == sto.Catégorie && d.Taille == sto.Taille
                                && d.Type == sto.Type && d.Barcode == null);
                                donnée.tbl_produits.Remove(psf.First());
                                psf = null;
                            }
                            await donnée.SaveChangesAsync();
                        }
                        
                    }
                    
                    return true;
                }
                catch (Exception ex)
                {
                    if (await MyExeption.ErrorReseauAsync(ex))
                    {
                        Alert.SShow("Erreur Réseau. Veillez verifier votre connection Internet!", Alert.AlertType.Info);
                        return false;
                    }
                }
                return true;
            }
        }

        private async void BtnNouveauProduit_Click(object sender, EventArgs e)
        {
            Nouveau_Produit produit = new Nouveau_Produit();
            int num = (int)produit.ShowDialog();
            if (!(produit.ok == "Oui"))
            {
                produit = (Nouveau_Produit)null;
            }
            else
            {
                await CallData();
                produit = (Nouveau_Produit)null;
            }
        }

        private async Task CallData(int skip = 0)
        {
            pagePanel.Visible = true;

            var data = FillDataAsync(skip);
            var all = FillAllDataAsync();

            var tasklist = new List<Task>() { data, all };

            while(tasklist.Count > 0)
            {
                var current = await Task.WhenAny(tasklist);

                if(current == data)
                {
                    var result = data.Result;
                    this.dataGridView1.Columns.Clear();
                    nTotalRow = result.Total_Quantité;
                    pTotalRow = result.Total_Quantité;
                    Total = result.Total_Quantité;
                    this.LastSkip = skip;
                    if (result.Table.Rows.Count == 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Tableau Vide");
                        DataRow dr = dt.NewRow();
                        dr[0] = "Aucune donnée dans ce tableau";
                        dt.Rows.Add(dr);
                        this.dataGridView1.DataSource = dt;
                        if (skip > 0)
                            this.btnPreview.Enabled = true;
                        else if (skip == 0)
                            this.btnPreview.Enabled = false;
                        this.lblMontant.Text = "Estimatif Total Achat : " + result.Montant_Achat.ToString("N0") + " FCFA, Estimatif Total Vente : " + result.Montant_Vente.ToString("N0") + " FCFA";
                        dt = (DataTable)null;
                        dr = (DataRow)null;
                        result = (MyTable)null;
                    }
                    else
                    {
                        this.dataGridView1.DataSource = result.Table;
                        this.dataGridView2.DataSource = result.Table;
                        dataGridView1.Columns[0].Visible = false;
                        int total_page_nb = (Boutique.nTotalRow / 100) + 1;

                        if (Next)
                        {
                            if (skip > 0)
                            {
                                this.btnPreview.Enabled = true;
                                CurrentTotal += Convert.ToInt32(result.Quantité);
                                int current_page = this.CurrentTotal / 100;
                                this.lblCurrentPage.Text = string.Format("{0}/{1} pages", current_page, total_page_nb);
                            }
                            else if (skip == 0)
                            {
                                this.btnPreview.Enabled = false;
                            }
                        }
                        else
                        {
                            CurrentTotal -= Convert.ToInt32(result.Quantité);
                            int current_page = this.CurrentTotal / 100;
                            this.lblCurrentPage.Text = string.Format("{0}/{1} pages", current_page, total_page_nb);
                        }
                        if (Boutique.nTotalRow > this.CurrentTotal)
                            this.btnNext.Enabled = true;
                        else if (this.CurrentTotal <= Boutique.nTotalRow)
                        {
                            this.btnNext.Enabled = false;
                            if(CurrentTotal == 0)
                            this.btnPreview.Enabled = false;
                        }

                        this.lblMontant.Text = "Estimatif Total Achat : " + result.Montant_Achat.ToString("N0") + " FCFA, Estimatif Total Vente : " + result.Montant_Vente.ToString("N0") + " FCFA";
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                        try
                        {
                            AddColumns.Addcolumn(this.dataGridView1);
                        }
                        catch (Exception ex)
                        {
                        }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
                    }
                }
                else if(current == all)
                {
                    var result = all.Result;
                    
                    if (result.Table.Rows.Count == 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Tableau Vide");
                        DataRow dr = dt.NewRow();
                        dr[0] = "Aucune donnée dans ce tableau";
                        dt.Rows.Add(dr);
                        
                        this.lblMontant.Text = "Estimatif Total Achat : " + result.Montant_Achat.ToString("N0") + " FCFA, Estimatif Total Vente : " + result.Montant_Vente.ToString("N0") + " FCFA";
                        dt = (DataTable)null;
                        dr = (DataRow)null;
                        result = (MyTable)null;
                    }
                    else
                    {
                        this.dataGridView2.DataSource = result.Table;
                        
                        this.lblMontant.Text = "Estimatif Total Achat : " + result.Montant_Achat.ToString("N0") + " FCFA, Estimatif Total Vente : " + result.Montant_Vente.ToString("N0") + " FCFA";
                    }
                }

                tasklist.Remove(current);
            }
        }

        private Task<MyTable> FillDataAsync(int skip = 0) => Task.Factory.StartNew(() => this.FillData(skip));

        private MyTable FillData(int skip = 0)
        {
            MyTable myTable1 = new MyTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Code_Barre");
            dataTable.Columns.Add("Désignation");
            dataTable.Columns.Add("Filiale");
            using (var financeDataContext = new QuitayeContext())
            {
                var source1 = financeDataContext.tbl_mesure_vente.OrderBy(d => d.Niveau).Select(d => new
                {
                    Mesure = d.Nom,
                    Type = d.Type,
                    Niveau = d.Niveau
                });
                foreach (var data in source1)
                    dataTable.Columns.Add("Qté_" + data.Mesure);
                if (source1.Count() == 0)
                    dataTable.Columns.Add("Quantité");
                var source2 = source1;
                using (var enumerator = source2.OrderBy(x => x.Niveau).GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        var current = enumerator.Current;
                        dataTable.Columns.Add("Prix_Achat " + current.Mesure);
                        dataTable.Columns.Add("Prix_Vente " + current.Mesure);
                        dataTable.Columns.Add("Marge " + current.Mesure);
                        dataTable.Columns.Add("Estimatif Achat Total");
                        dataTable.Columns.Add("Estimatif Vente Total");
                    }
                }

                var result = (from d in financeDataContext.tbl_stock_produits_vente
                                  //join mul in financeDataContext.tbl_multi_barcode on d.Product_Id equals mul.Product_Id into firstJoinedTable
                                  //from m in firstJoinedTable.DefaultIfEmpty()
                              join p in financeDataContext.tbl_produits on d.Product_Id equals p.Id into joinedTable
                              from f in joinedTable.DefaultIfEmpty()
                              join ar in financeDataContext.tbl_arrivée on d.Product_Id equals ar.Product_Id into joinedTable1
                              from a in joinedTable1.OrderByDescending(x => x.Date_Arrivée).DefaultIfEmpty()
                              orderby f.Nom
                              where d.Product_Id != 0
                              select new
                              {
                                  Id = d.Id,
                                  Nom = d.Marque,
                                  Catégorie = d.Catégorie,
                                  Taille = d.Taille,
                                  Quantité = d.Quantité,
                                  Formule = d.Formule,
                                  Prix_Achat = a.Prix / a.Quantité,
                                  Prix_Achat_Petit = f.Prix_Achat_Petit,
                                  Prix_Vente = f.Prix_Petit,
                                  Type = d.Type,
                                  Code_Barre = d.Code_Barre,
                                  Product_Id = d.Product_Id,
                                  Filiale = d.Detachement,
                                  Désignation = d.Marque + " " + d.Catégorie + " " + d.Taille
                              }).Distinct().OrderBy(x => x.Désignation).Skip(skip).Take(100).ToList();
                var selected = result.Select(x => new
                {
                    Id = x.Id,
                    Nom = x.Nom,
                    Catégorie = x.Catégorie,
                    Taille = x.Taille,
                    Quantité = x.Quantité,
                    Formule = x.Formule,
                    Prix_Achat = x.Prix_Achat,
                    Code_Barre = string.Join(",", (from s in financeDataContext.tbl_multi_barcode
                                                   where s.Product_Id == x.Product_Id
                                                   select s.Barcode)),
                    Prix_Achat_Petit = x.Prix_Achat_Petit,
                    Prix_Vente = x.Prix_Vente,
                    Type = x.Type,
                    Filiale = x.Filiale
                }).ToList();
                //int count = financeDataContext.tbl_stock_produits_vente.Count();

                foreach (var item in selected)
                {
                    var formuleMesureVente = financeDataContext.tbl_formule_mesure_vente.Where(d => d.Id == item.Formule).First();
                    var row = dataTable.NewRow();
                    row[0] = item.Id;
                    row[1] = item.Code_Barre;
                    //if (!string.IsNullOrEmpty(item.Details))
                    //{
                    //    row[2] = item.Details;
                    //}
                    //else
                    {
                        if (!string.IsNullOrEmpty(item.Type))
                            row[2] = $"{item.Nom} {item.Catégorie} {item.Taille}-{item.Type}";
                        else
                            row[2] = $"{item.Nom} {item.Catégorie} {item.Taille}";
                    }
                    row[3] = item.Filiale;
                    int num1 = 4;
                    decimal? nullable1;
                    decimal num2;
                    foreach (var data2 in source1)
                    {
                        if (data2.Type == "Petit")
                            row[num1++] = item.Quantité;
                        else if (data2.Type == "Moyen")
                        {
                            nullable1 = formuleMesureVente.Moyen;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                decimal num3 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Moyen);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num3, 2);
                            }
                        }
                        else if (data2.Type == "Grand")
                        {
                            nullable1 = formuleMesureVente.Grand;
                            num2 = 0M;
                            int num4;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                nullable1 = formuleMesureVente.Moyen;
                                num4 = nullable1.HasValue ? 1 : 0;
                            }
                            else
                                num4 = 0;
                            if (num4 != 0)
                            {
                                decimal num5 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Grand);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num5, 3);
                            }
                        }
                        else if (data2.Type == "Large")
                        {
                            nullable1 = formuleMesureVente.Large;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                decimal num6 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Large);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num6, 4);
                            }
                        }
                        else if (data2.Type == "Hyper Large")
                        {
                            nullable1 = formuleMesureVente.Hyper_Large;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                decimal num7 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Hyper_Large);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num7, 5);
                            }
                        }
                    }
                    if (source1.Count() == 0)
                        row[num1++] = 0;
                    decimal? prixAchat = item.Prix_Achat;
                    decimal num8 = 0M;
                    decimal num9 = !(prixAchat.GetValueOrDefault() == num8 & prixAchat.HasValue) ? Convert.ToDecimal(item.Prix_Achat) : Convert.ToDecimal(item.Prix_Achat_Petit);
                    
                    int columnIndex1 = num1;
                    int num10 = columnIndex1 + 1;
                    num2 = Convert.ToDecimal(num9);
                    string str1 = num2.ToString("N0");
                    row[columnIndex1] = str1;
                    DataRow dataRow2 = row;
                    int columnIndex2 = num10;
                    int num11 = columnIndex2 + 1;
                    num2 = Convert.ToDecimal(item.Prix_Vente);
                    string str2 = num2.ToString("N0");
                    dataRow2[columnIndex2] = str2;
                    DataRow dataRow3 = row;
                    int columnIndex3 = num11;
                    int num12 = columnIndex3 + 1;
                    nullable1 = item.Prix_Vente;
                    num2 = num9;
                    num2 = Convert.ToDecimal((nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num2) : new Decimal?()));
                    string str3 = num2.ToString("N0");
                    dataRow3[columnIndex3] = str3;
                    DataRow dataRow4 = row;
                    int columnIndex4 = num12;
                    int num13 = columnIndex4 + 1;
                    num2 = num9;
                    nullable1 = item.Quantité;
                    num2 = Convert.ToDecimal((nullable1.HasValue ? new Decimal?(num2 * nullable1.GetValueOrDefault()) : new Decimal?()));
                    string str4 = num2.ToString("N0");
                    dataRow4[columnIndex4] = str4;
                    DataRow dataRow5 = row;
                    int columnIndex5 = num13;
                    int num14 = columnIndex5 + 1;
                    nullable1 = item.Prix_Vente;
                    decimal? nullable2 = item.Quantité;
                    num2 = Convert.ToDecimal((nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?()));
                    string str5 = num2.ToString("N0");
                    dataRow5[columnIndex5] = str5;
                    MyTable myTable2 = myTable1;
                    decimal montantVente = myTable2.Montant_Vente;
                    nullable2 = item.Prix_Vente;
                    nullable1 = item.Quantité;
                    decimal num15 = Convert.ToDecimal((nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?()));
                    myTable2.Montant_Vente = montantVente + num15;
                    MyTable myTable3 = myTable1;
                    decimal montantAchat = myTable3.Montant_Achat;
                    num2 = num9;
                    nullable1 = item.Quantité;
                    decimal? nullable3;
                    if (!nullable1.HasValue)
                    {
                        nullable2 = new Decimal?();
                        nullable3 = nullable2;
                    }
                    else
                        nullable3 = new Decimal?(num2 * nullable1.GetValueOrDefault());
                    Decimal num16 = Convert.ToDecimal(nullable3);
                    myTable3.Montant_Achat = montantAchat + num16;
                    dataTable.Rows.Add(row);
                }

                var num = (from d in financeDataContext.tbl_stock_produits_vente
                           select new
                            {
                                Id = d.Id
                            });
                myTable1.Total_Quantité = Convert.ToInt32(num.Count());
                myTable1.Quantité = Convert.ToDecimal(result.Count());
                myTable1.Table = dataTable;
                return myTable1;
            }
        }

        private async Task CallNonPrice()
        {
            var data = await FillDataNonPriceAsync();
            var result = data;
            this.dataGridView1.Columns.Clear();
            nTotalRow = result.Total_Quantité;
            pTotalRow = result.Total_Quantité;
            Total = result.Total_Quantité;
            
            if (result.Table.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau";
                dt.Rows.Add(dr);
                this.dataGridView1.DataSource = dt;
               
                this.lblMontant.Text = "Estimatif Total Achat : " + result.Montant_Achat.ToString("N0") + " FCFA, Estimatif Total Vente : " + result.Montant_Vente.ToString("N0") + " FCFA";
                dt = (DataTable)null;
                dr = (DataRow)null;
                result = (MyTable)null;
            }
            else
            {
                this.dataGridView1.DataSource = result.Table;
                this.dataGridView2.DataSource = result.Table;
                int total_page_nb = (Boutique.nTotalRow / 50) + 1;
                int current_page = this.CurrentTotal / 50;
                //this.lblCurrentPage.Text = string.Format("{0}/{1} pages", current_page, total_page_nb);
                CurrentTotal += Convert.ToInt32(result.Quantité);
                
                if (Boutique.nTotalRow > this.CurrentTotal)
                    this.btnNext.Enabled = true;
                else if (this.CurrentTotal <= Boutique.nTotalRow)
                {
                    this.btnNext.Enabled = false;
                    if (CurrentTotal == 0)
                        this.btnPreview.Enabled = false;
                }

                this.lblMontant.Text = "Estimatif Total Achat : " + result.Montant_Achat.ToString("N0") + " FCFA, Estimatif Total Vente : " + result.Montant_Vente.ToString("N0") + " FCFA";
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                try
                {
                    AddColumns.Addcolumn(this.dataGridView1);
                }
                catch (Exception ex)
                {
                }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
            }
        }
        private Task<MyTable> FillDataNonPriceAsync(int skip = 0) => Task.Factory.StartNew(() => this.FillDataNonPrice());

        private MyTable FillDataNonPrice(int skip = 0)
        {
            MyTable myTable1 = new MyTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Code_Barre");
            dataTable.Columns.Add("Désignation");
            dataTable.Columns.Add("Filiale");
            using (var financeDataContext = new QuitayeContext())
            {
                var source1 = financeDataContext.tbl_mesure_vente.OrderBy(d => d.Niveau).Select(d => new
                {
                    Mesure = d.Nom,
                    Type = d.Type,
                    Niveau = d.Niveau
                });
                foreach (var data in source1)
                    dataTable.Columns.Add("Qté_" + data.Mesure);
                if (source1.Count() == 0)
                    dataTable.Columns.Add("Quantité");
                var source2 = source1;
                using (var enumerator = source2.OrderBy(x => x.Niveau).GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        var current = enumerator.Current;
                        dataTable.Columns.Add("Prix_Achat " + current.Mesure);
                        dataTable.Columns.Add("Prix_Vente " + current.Mesure);
                        dataTable.Columns.Add("Marge " + current.Mesure);
                        dataTable.Columns.Add("Estimatif Achat Total");
                        dataTable.Columns.Add("Estimatif Vente Total");
                    }
                }
                var result = (from d in financeDataContext.tbl_stock_produits_vente
                              join p in financeDataContext.tbl_produits on d.Product_Id equals p.Id into joinedTable
                              from f in joinedTable.DefaultIfEmpty()
                              join ar in financeDataContext.tbl_arrivée on d.Product_Id equals ar.Product_Id into joinedTable1
                              from a in joinedTable1.DefaultIfEmpty()
                              where f.Prix_Petit != 0 && d.Product_Id != 0
                              orderby f.Nom
                              select new
                              {
                                  Id = d.Id,
                                  Nom = d.Marque,
                                  Catégorie = d.Catégorie,
                                  Taille = d.Taille,
                                  Quantité = d.Quantité,
                                  Formule = d.Formule,
                                  Prix_Achat = a.Prix / a.Quantité,
                                  Prix_Achat_Petit = f.Prix_Achat_Petit,
                                  Prix_Vente = f.Prix_Petit,
                                  Type = d.Type,
                                  Code_Barre = d.Code_Barre,
                                  Product_Id = d.Product_Id,
                                  Filiale = d.Detachement,
                                  Désignation = d.Marque + " " + d.Catégorie + " " + d.Taille + " " + d.Type
                              }).Distinct().OrderBy(x => x.Code_Barre).ThenBy(x => x.Désignation).ToList().Select(x => new
                              {
                                  Id = x.Id,
                                  Nom = x.Nom,
                                  Catégorie = x.Catégorie,
                                  Taille = x.Taille,
                                  Quantité = x.Quantité,
                                  Formule = x.Formule,
                                  Prix_Achat = x.Prix_Achat,
                                  Code_Barre = string.Join(",", (from s in financeDataContext.tbl_multi_barcode
                                                                 where s.Product_Id == x.Product_Id
                                                                 select s.Barcode)),
                                  Prix_Achat_Petit = x.Prix_Achat_Petit,
                                  Prix_Vente = x.Prix_Vente,
                                  Type = x.Type,
                                  Filiale = x.Filiale
                              }).ToList();

                //int count = financeDataContext.tbl_stock_produits_vente.Count();

                foreach (var item in result.OrderBy(x => x.Nom).ThenBy(x => x.Prix_Vente))
                {
                    var formuleMesureVente = financeDataContext.tbl_formule_mesure_vente.Where(d => d.Id == item.Formule).First();
                    var row = dataTable.NewRow();
                    row[0] = item.Id;
                    row[1] = item.Code_Barre;
                    //if (!string.IsNullOrEmpty(item.Details))
                    //{
                    //    row[2] = item.Details;
                    //}
                    //else
                    {
                        if (!string.IsNullOrEmpty(item.Type))
                            row[2] = $"{item.Nom} {item.Catégorie} {item.Taille}-{item.Type}";
                        else
                            row[2] = $"{item.Nom} {item.Catégorie} {item.Taille}";
                    }
                    row[3] = item.Filiale;
                    int num1 = 4;
                    decimal? nullable1;
                    decimal num2;
                    foreach (var data2 in source1)
                    {
                        if (data2.Type == "Petit")
                            row[num1++] = item.Quantité;
                        else if (data2.Type == "Moyen")
                        {
                            nullable1 = formuleMesureVente.Moyen;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                decimal num3 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Moyen);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num3, 2);
                            }
                        }
                        else if (data2.Type == "Grand")
                        {
                            nullable1 = formuleMesureVente.Grand;
                            num2 = 0M;
                            int num4;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                nullable1 = formuleMesureVente.Moyen;
                                num4 = nullable1.HasValue ? 1 : 0;
                            }
                            else
                                num4 = 0;
                            if (num4 != 0)
                            {
                                decimal num5 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Grand);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num5, 3);
                            }
                        }
                        else if (data2.Type == "Large")
                        {
                            nullable1 = formuleMesureVente.Large;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                decimal num6 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Large);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num6, 4);
                            }
                        }
                        else if (data2.Type == "Hyper Large")
                        {
                            nullable1 = formuleMesureVente.Hyper_Large;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                decimal num7 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Hyper_Large);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num7, 5);
                            }
                        }
                    }
                    if (source1.Count() == 0)
                        row[num1++] = 0;
                    decimal? prixAchat = item.Prix_Achat;
                    decimal num8 = 0M;
                    decimal num9 = !(prixAchat.GetValueOrDefault() == num8 & prixAchat.HasValue) ? Convert.ToDecimal(item.Prix_Achat) : Convert.ToDecimal(item.Prix_Achat_Petit);

                    int columnIndex1 = num1;
                    int num10 = columnIndex1 + 1;
                    num2 = Convert.ToDecimal(num9);
                    string str1 = num2.ToString("N0");
                    row[columnIndex1] = str1;
                    DataRow dataRow2 = row;
                    int columnIndex2 = num10;
                    int num11 = columnIndex2 + 1;
                    num2 = Convert.ToDecimal(item.Prix_Vente);
                    string str2 = num2.ToString("N0");
                    dataRow2[columnIndex2] = str2;
                    DataRow dataRow3 = row;
                    int columnIndex3 = num11;
                    int num12 = columnIndex3 + 1;
                    nullable1 = item.Prix_Vente;
                    num2 = num9;
                    num2 = Convert.ToDecimal((nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num2) : new Decimal?()));
                    string str3 = num2.ToString("N0");
                    dataRow3[columnIndex3] = str3;
                    DataRow dataRow4 = row;
                    int columnIndex4 = num12;
                    int num13 = columnIndex4 + 1;
                    num2 = num9;
                    nullable1 = item.Quantité;
                    num2 = Convert.ToDecimal((nullable1.HasValue ? new Decimal?(num2 * nullable1.GetValueOrDefault()) : new Decimal?()));
                    string str4 = num2.ToString("N0");
                    dataRow4[columnIndex4] = str4;
                    DataRow dataRow5 = row;
                    int columnIndex5 = num13;
                    int num14 = columnIndex5 + 1;
                    nullable1 = item.Prix_Vente;
                    decimal? nullable2 = item.Quantité;
                    num2 = Convert.ToDecimal((nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?()));
                    string str5 = num2.ToString("N0");
                    dataRow5[columnIndex5] = str5;
                    MyTable myTable2 = myTable1;
                    decimal montantVente = myTable2.Montant_Vente;
                    nullable2 = item.Prix_Vente;
                    nullable1 = item.Quantité;
                    decimal num15 = Convert.ToDecimal((nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?()));
                    myTable2.Montant_Vente = montantVente + num15;
                    MyTable myTable3 = myTable1;
                    decimal montantAchat = myTable3.Montant_Achat;
                    num2 = num9;
                    nullable1 = item.Quantité;
                    decimal? nullable3;
                    if (!nullable1.HasValue)
                    {
                        nullable2 = new Decimal?();
                        nullable3 = nullable2;
                    }
                    else
                        nullable3 = new Decimal?(num2 * nullable1.GetValueOrDefault());
                    Decimal num16 = Convert.ToDecimal(nullable3);
                    myTable3.Montant_Achat = montantAchat + num16;
                    dataTable.Rows.Add(row);
                }

                var num = (from d in financeDataContext.tbl_stock_produits_vente
                           select new
                           {
                               Id = d.Id
                           });
                myTable1.Total_Quantité = Convert.ToInt32(num.Count());
                myTable1.Quantité = Convert.ToDecimal(result.Count());
                myTable1.Table = dataTable;
                return myTable1;
            }
        }

        private Task<MyTable> FillAllDataAsync() => Task.Factory.StartNew(() => this.FillAllData());

        private MyTable FillAllData()
        {
            MyTable myTable1 = new MyTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Code_Barre");
            dataTable.Columns.Add("Désignation");
            dataTable.Columns.Add("Filiale");
            using (var financeDataContext = new QuitayeContext())
            {
                var source1 = financeDataContext.tbl_mesure_vente.OrderBy(d => d.Niveau).Select(d => new
                {
                    Mesure = d.Nom,
                    Type = d.Type,
                    Niveau = d.Niveau
                });
                foreach (var data in source1)
                    dataTable.Columns.Add("Qté_" + data.Mesure);
                if (source1.Count() == 0)
                    dataTable.Columns.Add("Quantité");
                var source2 = source1;
                using (var enumerator = source2.OrderBy(x => x.Niveau).GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        var current = enumerator.Current;
                        dataTable.Columns.Add("Prix_Achat " + current.Mesure);
                        dataTable.Columns.Add("Prix_Vente " + current.Mesure);
                        dataTable.Columns.Add("Marge " + current.Mesure);
                        dataTable.Columns.Add("Estimatif Achat Total");
                        dataTable.Columns.Add("Estimatif Vente Total");
                    }
                }
                var result = (from d in financeDataContext.tbl_stock_produits_vente
                              join p in financeDataContext.tbl_produits on d.Product_Id equals p.Id into joinedTable
                              from f in joinedTable.DefaultIfEmpty()
                              join ar in financeDataContext.tbl_arrivée on d.Product_Id equals ar.Product_Id into joinedTable1
                              from a in joinedTable1.DefaultIfEmpty()
                              orderby f.Nom
                              where d.Product_Id != 0
                              select new
                              {
                                  Id = d.Id,
                                  Nom = d.Marque,
                                  Catégorie = d.Catégorie,
                                  Taille = d.Taille,
                                  Quantité = d.Quantité,
                                  Formule = d.Formule,
                                  Prix_Achat = a.Prix / a.Quantité,
                                  Prix_Achat_Petit = f.Prix_Achat_Petit,
                                  Prix_Vente = f.Prix_Petit,
                                  Type = d.Type,
                                  Code_Barre = d.Code_Barre,
                                  Product_Id = d.Product_Id,
                                  Filiale = d.Detachement,
                                  Désignation = d.Marque + " " + d.Catégorie + " " + d.Taille + " " + d.Type
                              }).Distinct().OrderBy(x => x.Désignation).ToList().Select(x => new
                              {
                                  Id = x.Id,
                                  Nom = x.Nom,
                                  Catégorie = x.Catégorie,
                                  Taille = x.Taille,
                                  Quantité = x.Quantité,
                                  Formule = x.Formule,
                                  Prix_Achat = x.Prix_Achat,
                                  Code_Barre = string.Join(",", (from s in financeDataContext.tbl_multi_barcode
                                                                 where s.Product_Id == x.Product_Id
                                                                 select s.Barcode)),
                                  Prix_Achat_Petit = x.Prix_Achat_Petit,
                                  Prix_Vente = x.Prix_Vente,
                                  Type = x.Type,
                                  Filiale = x.Filiale
                              }).ToList();

                //int count = financeDataContext.tbl_stock_produits_vente.Count();

                foreach (var item in result)
                {
                    var formuleMesureVente = financeDataContext.tbl_formule_mesure_vente.Where(d => d.Id == item.Formule).First();
                    var row = dataTable.NewRow();
                    row[0] = item.Id;
                    row[1] = item.Code_Barre;
                    //if (!string.IsNullOrEmpty(item.Details))
                    //{
                    //    row[2] = item.Details;
                    //}
                    //else
                    {
                        if (!string.IsNullOrEmpty(item.Type))
                            row[2] = $"{item.Nom} {item.Catégorie} {item.Taille}-{item.Type}";
                        else
                            row[2] = $"{item.Nom} {item.Catégorie} {item.Taille}";
                    }
                    row[3] = item.Filiale;
                    int num1 = 4;
                    decimal? nullable1;
                    decimal num2;
                    foreach (var data2 in source1)
                    {
                        if (data2.Type == "Petit")
                            row[num1++] = item.Quantité;
                        else if (data2.Type == "Moyen")
                        {
                            nullable1 = formuleMesureVente.Moyen;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                decimal num3 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Moyen);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num3, 2);
                            }
                        }
                        else if (data2.Type == "Grand")
                        {
                            nullable1 = formuleMesureVente.Grand;
                            num2 = 0M;
                            int num4;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                nullable1 = formuleMesureVente.Moyen;
                                num4 = nullable1.HasValue ? 1 : 0;
                            }
                            else
                                num4 = 0;
                            if (num4 != 0)
                            {
                                decimal num5 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Grand);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num5, 3);
                            }
                        }
                        else if (data2.Type == "Large")
                        {
                            nullable1 = formuleMesureVente.Large;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                decimal num6 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Large);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num6, 4);
                            }
                        }
                        else if (data2.Type == "Hyper Large")
                        {
                            nullable1 = formuleMesureVente.Hyper_Large;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                decimal num7 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Hyper_Large);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num7, 5);
                            }
                        }
                    }
                    if (source1.Count() == 0)
                        row[num1++] = 0;
                    decimal? prixAchat = item.Prix_Achat;
                    decimal num8 = 0M;
                    decimal num9 = !(prixAchat.GetValueOrDefault() == num8 & prixAchat.HasValue) ? Convert.ToDecimal(item.Prix_Achat) : Convert.ToDecimal(item.Prix_Achat_Petit);

                    int columnIndex1 = num1;
                    int num10 = columnIndex1 + 1;
                    num2 = Convert.ToDecimal(num9);
                    string str1 = num2.ToString("N0");
                    row[columnIndex1] = str1;
                    DataRow dataRow2 = row;
                    int columnIndex2 = num10;
                    int num11 = columnIndex2 + 1;
                    num2 = Convert.ToDecimal(item.Prix_Vente);
                    string str2 = num2.ToString("N0");
                    dataRow2[columnIndex2] = str2;
                    DataRow dataRow3 = row;
                    int columnIndex3 = num11;
                    int num12 = columnIndex3 + 1;
                    nullable1 = item.Prix_Vente;
                    num2 = num9;
                    num2 = Convert.ToDecimal((nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num2) : new Decimal?()));
                    string str3 = num2.ToString("N0");
                    dataRow3[columnIndex3] = str3;
                    DataRow dataRow4 = row;
                    int columnIndex4 = num12;
                    int num13 = columnIndex4 + 1;
                    num2 = num9;
                    nullable1 = item.Quantité;
                    num2 = Convert.ToDecimal((nullable1.HasValue ? new Decimal?(num2 * nullable1.GetValueOrDefault()) : new Decimal?()));
                    string str4 = num2.ToString("N0");
                    dataRow4[columnIndex4] = str4;
                    DataRow dataRow5 = row;
                    int columnIndex5 = num13;
                    int num14 = columnIndex5 + 1;
                    nullable1 = item.Prix_Vente;
                    decimal? nullable2 = item.Quantité;
                    num2 = Convert.ToDecimal((nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?()));
                    string str5 = num2.ToString("N0");
                    dataRow5[columnIndex5] = str5;
                    MyTable myTable2 = myTable1;
                    decimal montantVente = myTable2.Montant_Vente;
                    nullable2 = item.Prix_Vente;
                    nullable1 = item.Quantité;
                    decimal num15 = Convert.ToDecimal((nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?()));
                    myTable2.Montant_Vente = montantVente + num15;
                    MyTable myTable3 = myTable1;
                    decimal montantAchat = myTable3.Montant_Achat;
                    num2 = num9;
                    nullable1 = item.Quantité;
                    decimal? nullable3;
                    if (!nullable1.HasValue)
                    {
                        nullable2 = new Decimal?();
                        nullable3 = nullable2;
                    }
                    else
                        nullable3 = new Decimal?(num2 * nullable1.GetValueOrDefault());
                    Decimal num16 = Convert.ToDecimal(nullable3);
                    myTable3.Montant_Achat = montantAchat + num16;
                    dataTable.Rows.Add(row);
                }

                var num = (from d in financeDataContext.tbl_stock_produits_vente
                           select new
                           {
                               Id = d.Id
                           });
                myTable1.Total_Quantité = Convert.ToInt32(num.Count());
                myTable1.Quantité = Convert.ToDecimal(result.Count());
                myTable1.Table = dataTable;
                return myTable1;
            }
        }


        private async Task CallSearch(int skip = 0)
        {
            var data = FillDataSearchAsync(txtSearch.Text, 0);
            var all = FillAllDataSearchAsync(txtSearch.Text);

            var tasklist = new List<Task>() { data, all };

            while (tasklist.Count > 0)
            {
                var current = await Task.WhenAny(tasklist);

                if (current == data)
                {
                    var result = data.Result;
                    this.dataGridView1.Columns.Clear();
                    //Boutique.nTotalRow = result.Total_Quantité;
                    //Boutique.pTotalRow = result.Total_Quantité;
                    //Boutique.Total = result.Total_Quantité;
                    //this.LastSkip = skip;
                    if (result.Table.Rows.Count == 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Tableau Vide");
                        DataRow dr = dt.NewRow();
                        dr[0] = "Aucune donnée dans ce tableau";
                        dt.Rows.Add(dr);
                        this.dataGridView1.DataSource = dt;
                        if (skip > 0)
                            this.btnPreview.Enabled = true;
                        else if (skip == 0)
                            this.btnPreview.Enabled = false;
                        this.lblMontant.Text = "Estimatif Total Achat : " + result.Montant_Achat.ToString("N0") + " FCFA, Estimatif Total Vente : " + result.Montant_Vente.ToString("N0") + " FCFA";
                        dt = (DataTable)null;
                        dr = (DataRow)null;
                        result = (MyTable)null;
                    }
                    else
                    {
                        this.dataGridView1.DataSource = result.Table;
                        this.dataGridView2.DataSource = result.Table;
                        pagePanel.Visible = false;
                        //int total_page_nb = (Boutique.nTotalRow / 50) + 1;
                        //int current_page = this.CurrentTotal / 50;
                        //this.lblCurrentPage.Text = string.Format("{0}/{1} pages", current_page, total_page_nb);
                        //CurrentTotal += Convert.ToInt32(result.Quantité);
                        //if (skip > 0)
                        //{
                        //    this.btnPreview.Enabled = true;
                        //}
                        //else if (skip == 0)
                        //{
                        //    this.btnPreview.Enabled = false;
                        //}
                        //if (Boutique.nTotalRow > this.CurrentTotal)
                        //    this.btnNext.Enabled = true;
                        //else if (this.CurrentTotal <= Boutique.nTotalRow)
                        //{
                        //    this.btnNext.Enabled = false;
                        //    if (CurrentTotal == 0)
                        //        this.btnPreview.Enabled = false;
                        //}

                        this.lblMontant.Text = "Estimatif Total Achat : " + result.Montant_Achat.ToString("N0") + " FCFA, Estimatif Total Vente : " + result.Montant_Vente.ToString("N0") + " FCFA";
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                        try
                        {
                            AddColumns.Addcolumn(this.dataGridView1);
                        }
                        catch (Exception ex)
                        {
                        }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
                    }
                }
                else if (current == all)
                {
                    var result = all.Result;
                    if (result.Table.Rows.Count == 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Tableau Vide");
                        DataRow dr = dt.NewRow();
                        dr[0] = "Aucune donnée dans ce tableau";
                        dt.Rows.Add(dr);
                        this.dataGridView2.DataSource = result.Table;
                        
                        this.lblMontant.Text = "Estimatif Total Achat : " + result.Montant_Achat.ToString("N0") + " FCFA, Estimatif Total Vente : " + result.Montant_Vente.ToString("N0") + " FCFA";
                        dt = (DataTable)null;
                        dr = (DataRow)null;
                        result = (MyTable)null;
                    }
                    else
                    {
                        this.dataGridView2.DataSource = result.Table;
                        
                        this.lblMontant.Text = "Estimatif Total Achat : " + result.Montant_Achat.ToString("N0") + " FCFA, Estimatif Total Vente : " + result.Montant_Vente.ToString("N0") + " FCFA";
                        
                        result = (MyTable)null;
                    }
                }

                tasklist.Remove(current);
            }
        }

        private Task<MyTable> FillDataSearchAsync(string nom, int skip = 0) => Task.Factory.StartNew((() => this.FillDataSearch(nom, skip)));

        private MyTable FillDataSearch(string nom, int skip = 0)
        {
            MyTable myTable1 = new MyTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Code_Barre");
            dataTable.Columns.Add("Désignation");
            dataTable.Columns.Add("Filiale");

            using (var financeDataContext = new QuitayeContext())
            {
                var source1 = financeDataContext.tbl_mesure_vente.OrderBy(d => d.Niveau).Select(d => new
                {
                    Mesure = d.Nom,
                    Type = d.Type,
                    Niveau = d.Niveau
                });
                foreach (var data in source1)
                    dataTable.Columns.Add("Qté_" + data.Mesure);
                if (source1.Count() == 0)
                    dataTable.Columns.Add("Quantité");
                var source2 = source1;
                using (var enumerator = source2.OrderBy(x => x.Niveau).GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        var current = enumerator.Current;
                        dataTable.Columns.Add("Prix_Achat " + current.Mesure);
                        dataTable.Columns.Add("Prix_Vente " + current.Mesure);
                        dataTable.Columns.Add("Marge " + current.Mesure);
                        dataTable.Columns.Add("Estimatif Achat Total");
                        dataTable.Columns.Add("Estimatif Vente Total");
                    }
                }

                var result = (from d in financeDataContext.tbl_stock_produits_vente
                              join mul in financeDataContext.tbl_multi_barcode on d.Product_Id equals mul.Product_Id into multi_barcode
                              from m in multi_barcode.DefaultIfEmpty()
                              where
                              d.Detachement.Contains(nom)
                            || d.Marque.Contains(nom) || d.Catégorie.Contains(nom)
                            || m.Barcode.ToLower().Equals(nom.ToLower()) || d.Taille.Contains(nom) 
                            
                            || d.Type.Contains(nom) && d.Product_Id != 0
                              join p in financeDataContext.tbl_produits on d.Product_Id equals p.Id into joinedTable
                              from f in joinedTable.DefaultIfEmpty()
                              join ar in financeDataContext.tbl_arrivée on d.Product_Id equals ar.Product_Id into joinedTable1
                              from a in joinedTable1.DefaultIfEmpty()
                              orderby f.Nom
                              where d.Product_Id != 0
                              select new
                              {
                                  Id = d.Id,
                                  Nom = d.Marque,
                                  Catégorie = d.Catégorie,
                                  Taille = d.Taille,
                                  Quantité = d.Quantité,
                                  Formule = d.Formule,
                                  Prix_Achat = a.Prix / a.Quantité,
                                  Prix_Achat_Petit = f.Prix_Achat_Petit,
                                  Prix_Vente = f.Prix_Petit,
                                  Type = d.Type,
                                  Code_Barre = d.Code_Barre,
                                  Product_Id = d.Product_Id,
                                  Filiale = d.Detachement,
                                  Désignation = d.Marque+" "+d.Catégorie+" "+d.Taille+ " "+d.Type
                              }).Distinct().OrderBy(x => x.Désignation).ToList().Select(x => new
                              {
                                  Id = x.Id,
                                  Nom = x.Nom,
                                  Catégorie = x.Catégorie,
                                  Taille = x.Taille,
                                  Quantité = x.Quantité,
                                  Formule = x.Formule,
                                  Prix_Achat = x.Prix_Achat,
                                  Code_Barre = string.Join(",", (from s in financeDataContext.tbl_multi_barcode
                                                                 where s.Product_Id == x.Product_Id
                                                                 select s.Barcode)),
                                  Prix_Achat_Petit = x.Prix_Achat_Petit,
                                  Prix_Vente = x.Prix_Vente,
                                  Type = x.Type,
                                  Filiale = x.Filiale
                              }).ToList();

                foreach (var data1 in result)
                {
                    var item = data1;
                    var formuleMesureVente = financeDataContext.tbl_formule_mesure_vente
                        .Where((d => (int?)d.Id == item.Formule)).First();
                    DataRow row = dataTable.NewRow();
                    row[0] = item.Id;
                    row[1] = item.Code_Barre;
                    //if (!string.IsNullOrEmpty(item.Details))
                    //{
                    //    row[2] = item.Details;
                    //}
                    //else
                    {
                        if (!string.IsNullOrEmpty(item.Type))
                            row[2] = $"{item.Nom} {item.Catégorie} {item.Taille}-{item.Type}";
                        else
                            row[2] = $"{item.Nom} {item.Catégorie} {item.Taille}";
                    }
                    row[3] = item.Filiale;
                    int num1 = 4;
                    Decimal? nullable1;
                    Decimal num2;
                    foreach (var data2 in source1)
                    {
                        if (data2.Type == "Petit")
                            row[num1++] = item.Quantité;
                        else if (data2.Type == "Moyen")
                        {
                            nullable1 = formuleMesureVente.Moyen;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                Decimal num3 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Moyen);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num3, 2);
                            }
                        }
                        else if (data2.Type == "Grand")
                        {
                            nullable1 = formuleMesureVente.Grand;
                            num2 = 0M;
                            int num4;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                nullable1 = formuleMesureVente.Moyen;
                                num4 = nullable1.HasValue ? 1 : 0;
                            }
                            else
                                num4 = 0;
                            if (num4 != 0)
                            {
                                Decimal num5 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Grand);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num5, 3);
                            }
                        }
                        else if (data2.Type == "Large")
                        {
                            nullable1 = formuleMesureVente.Large;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                Decimal num6 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Large);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num6, 4);
                            }
                        }
                        else if (data2.Type == "Hyper Large")
                        {
                            nullable1 = formuleMesureVente.Hyper_Large;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                Decimal num7 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Hyper_Large);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num7, 5);
                            }
                        }
                    }
                    if (source1.Count() == 0)
                        row[num1++] = 0;
                    Decimal? prixAchat = item.Prix_Achat;
                    Decimal num8 = 0M;
                    Decimal num9 = !(prixAchat.GetValueOrDefault() == num8 & prixAchat.HasValue) ? Convert.ToDecimal(item.Prix_Achat) : Convert.ToDecimal(item.Prix_Achat_Petit);
                    DataRow dataRow1 = row;
                    int columnIndex1 = num1;
                    int num10 = columnIndex1 + 1;
                    num2 = Convert.ToDecimal(num9);
                    string str1 = num2.ToString("N0");
                    dataRow1[columnIndex1] = str1;
                    DataRow dataRow2 = row;
                    int columnIndex2 = num10;
                    int num11 = columnIndex2 + 1;
                    num2 = Convert.ToDecimal(item.Prix_Vente);
                    string str2 = num2.ToString("N0");
                    dataRow2[columnIndex2] = str2;
                    DataRow dataRow3 = row;
                    int columnIndex3 = num11;
                    int num12 = columnIndex3 + 1;
                    Decimal? prixVente = item.Prix_Vente;
                    num2 = num9;
                    num2 = Convert.ToDecimal((prixVente.HasValue ? new Decimal?(prixVente.GetValueOrDefault() - num2) : new Decimal?()));
                    string str3 = num2.ToString("N0");
                    dataRow3[columnIndex3] = str3;
                    DataRow dataRow4 = row;
                    int columnIndex4 = num12;
                    int num13 = columnIndex4 + 1;
                    num2 = num9;
                    nullable1 = item.Quantité;
                    num2 = Convert.ToDecimal((nullable1.HasValue ? new Decimal?(num2 * nullable1.GetValueOrDefault()) : new Decimal?()));
                    string str4 = num2.ToString("N0");
                    dataRow4[columnIndex4] = str4;
                    DataRow dataRow5 = row;
                    int columnIndex5 = num13;
                    int num14 = columnIndex5 + 1;
                    nullable1 = item.Prix_Vente;
                    Decimal? nullable2 = item.Quantité;
                    num2 = Convert.ToDecimal((nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?()));
                    string str5 = num2.ToString("N0");
                    dataRow5[columnIndex5] = str5;
                    MyTable myTable2 = myTable1;
                    Decimal montantVente = myTable2.Montant_Vente;
                    nullable2 = item.Prix_Vente;
                    nullable1 = item.Quantité;
                    Decimal num15 = Convert.ToDecimal((nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?()));
                    myTable2.Montant_Vente = montantVente + num15;
                    MyTable myTable3 = myTable1;
                    Decimal montantAchat = myTable3.Montant_Achat;
                    num2 = num9;
                    nullable1 = item.Quantité;
                    Decimal? nullable3;
                    if (!nullable1.HasValue)
                    {
                        nullable2 = new Decimal?();
                        nullable3 = nullable2;
                    }
                    else
                        nullable3 = new Decimal?(num2 * nullable1.GetValueOrDefault());
                    Decimal num16 = Convert.ToDecimal(nullable3);
                    myTable3.Montant_Achat = montantAchat + num16;
                    dataTable.Rows.Add(row);
                }

                var num = (from d in financeDataContext.tbl_stock_produits_vente
                           select new
                           {
                               Id = d.Id
                           });
                myTable1.Total_Quantité = Convert.ToInt32(num.Count());
                myTable1.Quantité = (Decimal)Convert.ToInt32(result.Count());
                myTable1.Table = dataTable;
                return myTable1;
            }
        }


        private Task<MyTable> FillAllDataSearchAsync(string nom) => Task.Factory.StartNew((() => this.FillAllDataSearch(nom)));

        private MyTable FillAllDataSearch(string nom)
        {
            MyTable myTable1 = new MyTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Code_Barre");
            dataTable.Columns.Add("Désignation");
            dataTable.Columns.Add("Filiale");

            using (var financeDataContext = new QuitayeContext())
            {
                var source1 = financeDataContext.tbl_mesure_vente.OrderBy(d => d.Niveau).Select(d => new
                {
                    Mesure = d.Nom,
                    Type = d.Type,
                    Niveau = d.Niveau
                });
                foreach (var data in source1)
                    dataTable.Columns.Add("Qté_" + data.Mesure);
                if (source1.Count() == 0)
                    dataTable.Columns.Add("Quantité");
                var source2 = source1;
                using (var enumerator = source2.OrderBy(x => x.Niveau).GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        var current = enumerator.Current;
                        dataTable.Columns.Add("Prix_Achat " + current.Mesure);
                        dataTable.Columns.Add("Prix_Vente " + current.Mesure);
                        dataTable.Columns.Add("Marge " + current.Mesure);
                        dataTable.Columns.Add("Estimatif Achat Total");
                        dataTable.Columns.Add("Estimatif Vente Total");
                    }
                }

                var result = (from d in financeDataContext.tbl_stock_produits_vente
                              where d.Detachement.Contains(nom)
                               || d.Marque.Contains(nom) || d.Catégorie.Contains(nom)
                               || d.Code_Barre.ToLower().Equals(nom.ToLower()) || d.Taille.Contains(nom) || d.Type.Contains(nom)
                              join p in financeDataContext.tbl_produits on d.Product_Id equals p.Id into joinedTable
                              from f in joinedTable.DefaultIfEmpty()
                              join ar in financeDataContext.tbl_arrivée on d.Product_Id equals ar.Product_Id into joinedTable1
                              from a in joinedTable1.DefaultIfEmpty()
                              orderby f.Nom
                              where d.Product_Id != 0
                              select new
                              {
                                  Id = d.Id,
                                  Nom = d.Marque,
                                  Catégorie = d.Catégorie,
                                  Taille = d.Taille,
                                  Quantité = d.Quantité,
                                  Formule = d.Formule,
                                  Prix_Achat = a.Prix / a.Quantité,
                                  Prix_Achat_Petit = f.Prix_Achat_Petit,
                                  Prix_Vente = f.Prix_Petit,
                                  Type = d.Type,
                                  Code_Barre = d.Code_Barre,
                                  Product_Id = d.Product_Id,
                                  Filiale = d.Detachement,
                                  Désignation = d.Marque + " " + d.Catégorie + " " + d.Taille + " " + d.Type
                              }).Distinct().OrderBy(x => x.Désignation).ToList().Select(x => new
                              {
                                  Id = x.Id,
                                  Nom = x.Nom,
                                  Catégorie = x.Catégorie,
                                  Taille = x.Taille,
                                  Quantité = x.Quantité,
                                  Formule = x.Formule,
                                  Prix_Achat = x.Prix_Achat,
                                  Code_Barre = string.Join(",", (from s in financeDataContext.tbl_multi_barcode
                                                                 where s.Product_Id == x.Product_Id
                                                                 select s.Barcode)),
                                  Prix_Achat_Petit = x.Prix_Achat_Petit,
                                  Prix_Vente = x.Prix_Vente,
                                  Type = x.Type,
                                  Filiale = x.Filiale
                              }).ToList();

                foreach (var data1 in result)
                {
                    var item = data1;
                    var formuleMesureVente = financeDataContext.tbl_formule_mesure_vente
                        .Where((d => (int?)d.Id == item.Formule)).First();
                    DataRow row = dataTable.NewRow();
                    row[0] = item.Id;
                    row[1] = item.Code_Barre;
                    //if (!string.IsNullOrEmpty(item.Details))
                    //{
                    //    row[2] = item.Details;
                    //}else
                    {
                        if (!string.IsNullOrEmpty(item.Type))
                            row[2] = $"{item.Nom} {item.Catégorie} {item.Taille}-{item.Type}";
                        else
                            row[2] = $"{item.Nom} {item.Catégorie} {item.Taille}";
                    }
                    
                    row[3] = item.Filiale;
                    int num1 = 4;
                    Decimal? nullable1;
                    Decimal num2;
                    foreach (var data2 in source1)
                    {
                        if (data2.Type == "Petit")
                            row[num1++] = item.Quantité;
                        else if (data2.Type == "Moyen")
                        {
                            nullable1 = formuleMesureVente.Moyen;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                Decimal num3 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Moyen);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num3, 2);
                            }
                        }
                        else if (data2.Type == "Grand")
                        {
                            nullable1 = formuleMesureVente.Grand;
                            num2 = 0M;
                            int num4;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                nullable1 = formuleMesureVente.Moyen;
                                num4 = nullable1.HasValue ? 1 : 0;
                            }
                            else
                                num4 = 0;
                            if (num4 != 0)
                            {
                                Decimal num5 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Grand);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num5, 3);
                            }
                        }
                        else if (data2.Type == "Large")
                        {
                            nullable1 = formuleMesureVente.Large;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                Decimal num6 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Large);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num6, 4);
                            }
                        }
                        else if (data2.Type == "Hyper Large")
                        {
                            nullable1 = formuleMesureVente.Hyper_Large;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                Decimal num7 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Hyper_Large);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Quantité) / num7, 5);
                            }
                        }
                    }
                    if (source1.Count() == 0)
                        row[num1++] = 0;
                    Decimal? prixAchat = item.Prix_Achat;
                    Decimal num8 = 0M;
                    Decimal num9 = !(prixAchat.GetValueOrDefault() == num8 & prixAchat.HasValue) ? Convert.ToDecimal(item.Prix_Achat) : Convert.ToDecimal(item.Prix_Achat_Petit);
                    DataRow dataRow1 = row;
                    int columnIndex1 = num1;
                    int num10 = columnIndex1 + 1;
                    num2 = Convert.ToDecimal(num9);
                    string str1 = num2.ToString("N0");
                    dataRow1[columnIndex1] = str1;
                    DataRow dataRow2 = row;
                    int columnIndex2 = num10;
                    int num11 = columnIndex2 + 1;
                    num2 = Convert.ToDecimal(item.Prix_Vente);
                    string str2 = num2.ToString("N0");
                    dataRow2[columnIndex2] = str2;
                    DataRow dataRow3 = row;
                    int columnIndex3 = num11;
                    int num12 = columnIndex3 + 1;
                    Decimal? prixVente = item.Prix_Vente;
                    num2 = num9;
                    num2 = Convert.ToDecimal((prixVente.HasValue ? new Decimal?(prixVente.GetValueOrDefault() - num2) : new Decimal?()));
                    string str3 = num2.ToString("N0");
                    dataRow3[columnIndex3] = str3;
                    DataRow dataRow4 = row;
                    int columnIndex4 = num12;
                    int num13 = columnIndex4 + 1;
                    num2 = num9;
                    nullable1 = item.Quantité;
                    num2 = Convert.ToDecimal((nullable1.HasValue ? new Decimal?(num2 * nullable1.GetValueOrDefault()) : new Decimal?()));
                    string str4 = num2.ToString("N0");
                    dataRow4[columnIndex4] = str4;
                    DataRow dataRow5 = row;
                    int columnIndex5 = num13;
                    int num14 = columnIndex5 + 1;
                    nullable1 = item.Prix_Vente;
                    Decimal? nullable2 = item.Quantité;
                    num2 = Convert.ToDecimal((nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?()));
                    string str5 = num2.ToString("N0");
                    dataRow5[columnIndex5] = str5;
                    MyTable myTable2 = myTable1;
                    Decimal montantVente = myTable2.Montant_Vente;
                    nullable2 = item.Prix_Vente;
                    nullable1 = item.Quantité;
                    Decimal num15 = Convert.ToDecimal((nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?()));
                    myTable2.Montant_Vente = montantVente + num15;
                    MyTable myTable3 = myTable1;
                    Decimal montantAchat = myTable3.Montant_Achat;
                    num2 = num9;
                    nullable1 = item.Quantité;
                    Decimal? nullable3;
                    if (!nullable1.HasValue)
                    {
                        nullable2 = new Decimal?();
                        nullable3 = nullable2;
                    }
                    else
                        nullable3 = new Decimal?(num2 * nullable1.GetValueOrDefault());
                    Decimal num16 = Convert.ToDecimal(nullable3);
                    myTable3.Montant_Achat = montantAchat + num16;
                    dataTable.Rows.Add(row);
                }

                var num = (from d in financeDataContext.tbl_stock_produits_vente
                           select new
                           {
                               Id = d.Id
                           });
                myTable1.Total_Quantité = Convert.ToInt32(num.Count());
                myTable1.Quantité = (Decimal)Convert.ToInt32(result.Count());
                myTable1.Table = dataTable;
                return myTable1;
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                ;
        }

        private async void btnAjouter_Click(object sender, EventArgs e)
        {
            if (LogIn.expiré)
                return;
            Nouveau_Produit NOv = new Nouveau_Produit();
            NOv.ShowDialog();
            if (NOv.ok == "Oui")
                await this.CallData();
            NOv = (Nouveau_Produit)null;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        public System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArrayIn))
                return Image.FromStream((Stream)memoryStream);
        }

        public async Task PopUp()
        {
            using (var donnée = new QuitayeContext())
            {
                
                var sto = donnée.tbl_stock_produits_vente.Where(x => x.Id == Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value)).First();
                var st = donnée.tbl_produits.Where(d => d.Barcode == sto.Code_Barre).First();
                PopUps pop = new PopUps();
                pop.text = nameof(Boutique);
                pop.produitInfo2.Titre = sto.Marque;
                pop.produitInfo2.Catégorie = sto.Catégorie;
                pop.produitInfo2.Taille = st.Taille;
                pop.produitInfo2.Code_Barre = st.Barcode;
                pop.produitInfo2.Usage = st.Usage;
                pop.produitInfo2.Prix_Unité = Convert.ToDecimal(st.Prix_Petit);
                pop.produitInfo2.Quantité = Convert.ToDecimal(sto.Quantité);
                pop.produitInfo2.Montant = Convert.ToDecimal(sto.Quantité) * Convert.ToDecimal(st.Prix_Petit);
                pop.produitInfo2.Stock_min = Convert.ToInt32(st.Stock_min);
                pop.produitInfo2.Stock_max = Convert.ToInt32(st.Stock_max);
                pop.produitInfo2.Ref = (Decimal)Convert.ToInt32(st.Id);
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                try
                {
                    byte[] img = st.Image.ToArray();
                    pop.produitInfo2.Icon = this.ByteArrayToImage(st.Image.ToArray());
                    img = (byte[])null;
                }
                catch (Exception ex)
                {
                    pop.produitInfo2.Icon = null;
                }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
                pop.panel1.Visible = true;
                pop.panel2.Visible = true;
                pop.panel3.Visible = true;
                pop.panel4.Visible = true;
                pop.Location = Cursor.Position;
                int num = (int)pop.ShowDialog();
                if (pop.ok == "Oui")
                    await CallData();
                st = null;
                pop = (PopUps)null;
            }
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
                await CallSearch();
            else
                await CallData();
        }

        private async void dataGridView1_RowHeaderMouseClick(
          object sender,
          DataGridViewCellMouseEventArgs e)
        {
            await this.PopUp();
        }

        private void Boutique_Load(object sender, EventArgs e)
        {

        }
    }
}
