using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quitaye_School.Models;
using FontAwesome.Sharp;
using System.Text.RegularExpressions;
using Bunifu.Framework.UI;
using PrintAction;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Quitaye_Medical.Models;
using Quitaye_School.Models.Context;
using System.Data.Entity;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Windows.Media;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using TextBox = System.Windows.Forms.TextBox;
using Application = System.Windows.Forms.Application;
using System.Runtime.InteropServices;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.SqlServer.Management.Smo;
using Quitaye_School.Models;
using Quitaye_School.User_Interface;

namespace Quitaye_School.User_Interface
{
    public partial class AchatVente : Form
    {
        public static string ok;
        private Timer loadTimer = new Timer();
        private string mycontrng = LogIn.mycontrng;
        private Timer timer = new Timer();
        public string mvalue = "1";
        public static string types;
        public static string codes;
        public static string reduction;
        public static string soustotal;
        public static string nettotal;
        public static bool checke; 
        private ProductObject product = new ProductObject();
        private List<ProductObject> product_list = new List<ProductObject>();
        private List<Quitaye_School.Models.VenteList> vente = new List<Quitaye_School.Models.VenteList>();
        private bool first = true;
        private static bool check = false;
        private static bool restant = false;
        private static string filepath;
        private static string filename;
        public static string num_vente;
        public static string num_achat;
        public string Matricule { get; set; }

        public static string num_damaged;

        public bool Minimize { get; set; }
        public bool Close_All { get; set; }
        public bool Returned { get; }

        public AchatVente(string client_matricule = null, bool minimize = false, bool returned = false, bool close_all = true)
        {
            InitializeComponent();
            if (!LogIn.type_compte.Contains("Administrateur"))
            {
                cbxType.Items.Clear();
                cbxType.Items.Add("Vente");
            }
            Close_All = close_all;
            BarcodeList = new List<string>();
            FirstLoad = true;
            ProductList = new List<Simple_Cbx_Item>();
            cbxType.SelectedIndex = 0;
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            Matricule = client_matricule;
            loadTimer.Tick += LoadTimer_Tick;
            cbxCode.SelectedIndexChanged += CbxNom_SelectedIndexChanged;
            cbxSearch.SelectedIndexChanged += CbxSearch_SelectedIndexChanged;
            txtQuantité.TextChanged += TxtQuantité_TextChanged;
            iconButton1.Click += btnFournisseur_Click;
            txtQuantité.KeyPress += TxtQuantité_KeyPress;
            cbxSearch.TextChanged += CbxSearch_TextChanged;
            cbxMesure.SelectedIndexChanged += TxtQuantité_TextChanged;
            txtPrixUnité.KeyPress += TxtMontant_KeyPress;
            cbxType.SelectedIndexChanged += CbxType_SelectedIndexChanged;
            btnAjouter.Click += BtnAjouter_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            cbxModePrix.SelectedIndexChanged += CbxModePrix_SelectedIndexChanged;
            btnEnregistrer.Click += BtnEnregistrer_Click;
            dataGridView1.CellClick += DataGridView1_CellClick;
            btnFile.Click += BtnFile_Click;
            cbxCode.KeyPress += CbxCode_KeyPress;
            txtMontant.KeyPress += TxtMontant_KeyPress;
            btnImprimerTicket.Click += BtnImprimerTicket_Click;
            btnAddPayement.Click += BtnAddPayement_Click;
            txtmontantpayer.TextChanged += Txtmontantpayer_TextChanged;
            txtmontantpayer.KeyPress += Txtmontantpayer_KeyPress;
            cbxCode.TextChanged += CbxCode_TextChanged;
            btnAddClient.Click += BtnAddClient_Click;
            btnAddFiliale.Click += BtnAddFiliale_Click;
            txtQuantité.KeyDown += TxtQuantité_KeyDown;
            txtSousTotal.TextChanged += TxtSousTotal_TextChanged;
            txtRéduction.TextChanged += TxtRéduction_TextChanged;
            txtRéduction.KeyPress += TxtRéduction_KeyPress;
            iconButton5.Click += BtnAddPayement_Click;
            iconButton2.Click += BtnImprimerTicket_Click;
            iconButton3.Click += BtnFile_Click;
            btnMinimize.Visible = minimize;
            btnFermer.Visible = minimize;
            btnRestore.Visible = minimize;
            btnFermer.Click += BtnFermer_Click;
            Minimize = minimize;
            Returned = returned;
            panel2.Visible = minimize;
            panel6.Visible = minimize;
            panel4.Visible = minimize;
            panel5.Visible = minimize;
            btnSaveCode.Click += BtnSaveCode_Click;
            btnMinimize.Click += btnMinimize_Click;
            btnClotureJournée.Click += BtnClotureJournée_Click;
            if (minimize)
            {
                btnSaveCode.Visible = false;
                txtNewCode.Visible = false;
            }
            txtPrixUnité.TextChanged += TxtPrixUnité_TextChanged;
            int width = SystemInformation.VirtualScreen.Width;
            int height = SystemInformation.VirtualScreen.Height;
            if (width <= 1024)
            {
                Width = 1010;
                Height = 620;
            }
            else
            {
                if (width <= 1300)
                    return;
                Width = 1345;
                Height = 680;
            }
            btnReturn.Click += BtnReturn_Click;
            btnRestore.Click += BtnRestore_Click;
            //cbxSearch.KeyDown += CbxSearch_KeyDown;
            //cbxSearch.KeyUp += CbxSearch_KeyUp;
            //cbxSearch.
            cbxPendingList.SelectedIndexChanged += CbxPendingList_SelectedIndexChanged;
            btnDeleteAll.Click += BtnDeleteAll_Click;
            //btnVenteEnregistrées.Click += BtnVenteEnregistrées_Click;
            btnPending.Click += BtnPending_Click;
            //txt
            btnReprintTicket.Click += BtnReprintTicket_Click;
        }

        

        private async void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbxType.Text))
            {
                if (cbxType.Text == "Vente")
                {
                    foreach (var item in vente)
                    {
                        await DeleteVenteAsync(item.Id);
                    }
                    await CallTaskSecond(Active_Pending);
                }else if(cbxType.Text == "Achat")
                {
                    foreach (var item in vente)
                    {
                        await DeleteAchatAsync(item.Id);
                    }
                    await CallTaskSecond(Active_Pending);
                }
            }
        }

        private void BtnReprintTicket_Click(object sender, EventArgs e)
        {
            //var reprint = new Reprint_Ticket();
            //reprint.ShowDialog();
            //cbxCode.Focus();
        }
        
        private void BtnReturn_Click(object sender, EventArgs e)
        {
            var achat_vente = new AchatVente(Matricule, true, true, false);
            achat_vente.ShowDialog();
        }

        private async void BtnPending_Click(object sender, EventArgs e)
        {
            using(var donnée = new QuitayeContext())
            {
                var pending = donnée.tbl_vente_temp.OrderByDescending(x => x.Pending).Select(x => x.Pending).FirstOrDefault();

                foreach (var item in vente)
                {
                    var v = donnée.tbl_vente_temp.Where(x => x.Id == item.Id && x.Pending == null).FirstOrDefault();
                    if(v != null)
                    {
                        v.Pending = (Convert.ToInt32(pending) + 1).ToString();
                        await donnée.SaveChangesAsync();
                    }
                }
                Active_Pending = null;
                await CallTaskSecond();
            }
            cbxCode.Focus();
        }

        private async void CbxPendingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbxPendingList.Text) && !cbxPendingList.Text.Contains("System"))
            {
                if (!FirstPendingLoad)
                {
                    if(string.IsNullOrEmpty(cbxPendingList.SelectedValue.ToString()))
                    {
                        Active_Pending = null;
                        await CallTaskSecond();
                    }
                    else
                    {
                        Active_Pending = cbxPendingList.SelectedValue.ToString();
                        await CallTaskSecond(Active_Pending);
                    }
                    

                }
            }
        }
        
        private void BtnVenteEnregistrées_Click(object sender, EventArgs e)
        {
            var ventes = new Rapport_Vente(true);
            ventes.ShowDialog();
            cbxCode.Focus();
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void BtnClotureJournée_Click(object sender, EventArgs e)
        {
            var entreprise = new Models.Info_Entreprise();
            var entre_details = new List<tbl_entreprise_autres_details>();
            var list = new List<Models.VenteList>();
            decimal montant_net_payé = 0;
            decimal montant_retourné = 0;
            decimal réduction = 0;
            decimal montant_total = 0;
            decimal depenses = 0;
            using (var donnée = new QuitayeContext())
                {
                    var des = donnée.tbl_entreprise.Where(d => d.Id == 1).Select(d => new
                    {
                        Id = d.Id,
                        Nom = d.Nom,
                        Email = d.Email,
                        Telephone = d.Téléphone,
                        Adresse = d.Adresse,
                        Date_Ouverture = d.Date_Ouverture
                    }).First();

                    entre_details = donnée.tbl_entreprise_autres_details.ToList();

                    entreprise.Nom = des.Nom;
                    entreprise.Adresse = des.Adresse;
                    entreprise.Telephone = des.Telephone;
                    entreprise.Email = des.Email;
                    list = donnée.tbl_vente.Where(x => DbFunctions.TruncateTime(x.Date_Vente) == DbFunctions.TruncateTime(DateTime.Now))
                    .Select(x => new Models.VenteList() { Montant = x.Montant, Quantité = x.Quantité, Num_Client = x.Num_Vente, Reduction = x.Reduction }).ToList();
                var num_vente_list = new List<string>();
                var total_depenses = donnée.tbl_payement.Where(x => DbFunctions.TruncateTime(x.Date_Payement) == DbFunctions.TruncateTime(DateTime.Today)
                && x.Type == "Décaissement" && (x.Nature == "Payement" || x.Nature == "Dépense")).ToList();
                depenses = Convert.ToDecimal(total_depenses.Sum(x => x.Montant));
                foreach (var vente in list)
                {
                    num_vente_list.Add(vente.Num_Client);
                }
                //var res = donnée.tbl_payement.Where(x => DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(DateTime.Now) 
                //&& num_vente_list.Contains(x.Num_Opération)).Select(x => new { Réduction = x.Réduction }).ToList();
                réduction = Convert.ToDecimal(list.Sum(x => x.Reduction));
                des = null;
                }
            montant_total = Convert.ToDecimal(list.Sum(x => x.Montant));
            var ticket = new Ticket(entreprise, list, new Sales_Details()
            {
                Montant_Total = montant_total,
                Montant_Net_Payé = montant_net_payé,
                Réduction = réduction,
                Montant_Rétourné = montant_retourné,
                Total_Dépenses = depenses
            }, entre_details);
            ticket.print_Bilan();
            //bool result = await Print_TiketAsync(entreprise, list, new Sales_Details() { Montant_Total = montant_total, Montant_Net_Payé = montant_net_payé, Réduction = réduction, Montant_Rétourné =  montant_retourné, });
            entreprise = null;
            list = null;
            cbxCode.Focus();
        }

        private async void CbxCode_TextChanged(object sender, EventArgs e)
        {
            string search = cbxCode.Text;
            var result = await Task.Run(() => DoesBarcodeExist(search));
            if (!result)
            {
                txtNom.Text = null;
                txtPrixUnité.Text = null;
                txtMontant.Text = null;
            }
        }

        private async Task<bool> DoesBarcodeExist(string search)
        {
            if (BarcodeList.Count > 0)
            {
                var code = BarcodeList.Where(x => x.Equals(search)).FirstOrDefault();
                if (!string.IsNullOrEmpty(code))
                {
                    await Task.Delay(2);
                    return true;
                }
                else
                {
                    await Task.Delay(2);
                    return false;
                }
            }
            else return false;
        }

        private void CbxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void CbxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        public List<Simple_Cbx_Item> ProductList { get; set; }
        public bool FirstLoad { get; set; }
        public bool FirstPendingLoad { get; set; }
        [DllImport("user32.dll")]
        private static extern bool ShowCursor(bool bShow);
        private async void CbxSearch_TextChanged(object sender, EventArgs e)
        {
            if (!FirstLoad)
            {
                FillCbxProduit_Details(cbxSearch.Text);
                cbxSearch.SelectionStart = cbxSearch.Text.Length;
                cbxSearch.SelectionLength = 0;
                cbxSearch.DroppedDown = true;
            }
            var cursor = Cursor.Current;
            if (cursor == null)
            {
                this.Cursor = Cursors.Default;
            }
            else
            {

            }
        }

        private void FillCbxProduit_Details(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var items = ProductList.Where(x => x.Name.ToLower().Contains(search.ToLower())).Select(x => x).ToList();
                cbxSearch.Items.Clear();
                foreach (var item in items)
                {
                    cbxSearch.Items.Add(item.Name);
                }
            }
            else
            {
                cbxSearch.Items.Clear();
                foreach (var item in ProductList)
                {
                    cbxSearch.Items.Add(item.Name);
                }
                //cbxSearch.DataSource = ProductList;
                //cbxSearch.DisplayMember = "Name";
                //cbxSearch.ValueMember = "Id";
                //cbxSearch.Text = null;
            }
        }

        private async void btnFournisseur_Click(object sender, EventArgs e)
        {
            var fournisseur = new Fournisseur();
            fournisseur.ShowDialog();
            if(fournisseur.Ok == "Oui")
            {
                await CallCbxFournisseur();
            }
        }

        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if(cbxType.Text == "Vente")
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    await CallSearch(txtSearch.Text);
                }else
                {
                    await CallTaskSecond();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    await CallSearchAcha(txtSearch.Text);
                }
                else
                {
                    await CallTaskSecond();
                }
            }
        }

        private async void TxtMontant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsBarcodeScannerInput(e))
            {
                e.Handled = true; // Prevent the textbox from accepting the input
                cbxCode.Focus();
            }
            else
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
                    e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    e.Handled = true;
                    if (cbxType.Text == "Vente")
                    {
                        await VenteProduit();
                    }
                    else if(cbxType.Text =="Achat")
                    {
                        await AchatGadget();
                    }
                    else if(cbxType.Text == "Expense Damaged")
                    {
                        await DamagedExpense();
                    }
                }
                else
                {

                }
            }
        }

        private void TxtPrixUnité_TextChanged(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(txtPrixUnité.Text))
                && !string.IsNullOrEmpty(txtQuantité.Text))
            {
                if (!string.IsNullOrEmpty(txtPrixUnité.Text))
                {
                    var result = Prix_Unité_MontantTotal(Convert.ToDecimal(txtPrixUnité.Text), Convert.ToDecimal(txtQuantité.Text));
                    txtMontant.Text = result.Item2.ToString("N0");
                }
            }
        }

        private (decimal, decimal) Prix_Unité_MontantTotal(decimal prix, decimal qté, bool unité = true)
        {
            return (prix, prix * qté);
        }

        private async void BtnSaveCode_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewCode.Text)&& product != null && product.Code_Barre == null)
            {
                var result = await SaveChangesAsync(txtNewCode.Text);
                if (result.Item1)
                {
                    product.Code_Barre = txtNewCode.Text;
                    Alert.SShow("Attribution effectuée avec succès.", Alert.AlertType.Sucess);
                    await CallCode();
                    cbxCode.Text = txtNewCode.Text;
                    txtNewCode.Text = null;
                }
                else
                {
                    Alert.SShow($"Erreur: {result.Item2}", Alert.AlertType.Warning);
                    txtNewCode.Text = null;
                }
            }
        }

        

        private async Task<(bool, string)> SaveChangesAsync(string code)
        {
            using (var donnée = new QuitayeContext())
            {
                var prd = (from d in donnée.tbl_produits 
                           where d.Barcode.Equals(code) select new { Id = d.Id }).FirstOrDefault();
                if (prd == null)
                {
                    var item = (from d in donnée.tbl_stock_produits_vente where d.Id == product.Id select d).FirstOrDefault();
                    if (item != null)
                    {
                        item.Code_Barre = code;
                        var stock = (from d in donnée.tbl_stock_produits_vente
                                     where d.Marque == item.Marque && d.Catégorie == item.Catégorie
                                     && d.Taille == item.Taille && d.Type == item.Type
                                     select d);
                        foreach (var itemd in stock)
                        {
                            itemd.Code_Barre = code;
                        }

                        var pro = (from d in donnée.tbl_produits
                                   where d.Nom == item.Marque
                                   && d.Catégorie == item.Catégorie
                                   && d.Taille == item.Taille
                                   && d.Type == item.Type
                                   select d).ToList();
                        if (pro.Count == 0)
                        {
                            //var pr = await Page_Importation.NewProduct(item.Type, "1", item.Marque, item.Catégorie, item.Taille, 0, 0, "PIECE", Convert.ToInt32(item.Formule));
                            //pro = (from d in donnée.tbl_produits
                            //       where d.Nom == item.Marque
                            //       && d.Catégorie == item.Catégorie
                            //       && d.Taille == item.Taille
                            //       && d.Type == item.Type
                            //       select d).ToList();
                            //pro.FirstOrDefault().Barcode = code;
                        }

                        await donnée.SaveChangesAsync();
                        return (true, "Success");
                    }
                    else return (false, "Produit non existant.");
                }
                else return (false, "Code barre existe déja, Veillez modifier ce code barre");
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;


        private void TxtQuantité_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                // Jump to the specific control
                txtmontantpayer.Focus();
            }
        }

        private async void BtnAddFiliale_Click(object sender, EventArgs e)
        {
            Ajout_Filiale ajout = new Ajout_Filiale();
            int num = (int)ajout.ShowDialog();
            if (!(ajout.ok == "Oui"))
            {
                ajout = null;
            }
            else
            {
                await CallFiliale();
                ajout = null;
            }
        }

        private async Task CallFiliale()
        {
            DataTable result = await FillFilialeAsync();
            cbxFiliale.DataSource = result;
            cbxFiliale.DisplayMember = "Nom";
            cbxFiliale.ValueMember = "Id";
            cbxFiliale.Text = null;
            result = null;
        }

        private Task<DataTable> FillFilialeAsync() => Task.Factory.StartNew<DataTable>((() => FillFiliale()));

        private DataTable FillFiliale()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Nom");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = from d in financeDataContext.tbl_filiale orderby d.Nom select new
                {
                    Id = d.Id,
                    Nom = d.Nom
                };
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

        private async void CbxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (first || cbxSearch.Text.Length <= 0 
                || !(cbxSearch.Text != ""))
                return;
            List<string> red = new List<string>();
            string[] array1 = cbxSearch.Text.Split('-');
            if (array1.Length >= 2)
            {
                string[] array2 = array1[1].Split(',');
                List<string> le = new List<string>();
                le.Add(array1[0]);
                le.Add(array2[0]);
                var serd = array2[1].Split('_');
                le.Add(serd[0]);
                if(cbxSearch.Text.Contains("_"))
                {
                    string[] array3 = cbxSearch.Text.Split('_');
                    le.Add(array3[1]);
                }
                
                if (LogIn.type_compte.Contains("Administrateur") && cbxFiliale.Text != "")
                {
                    var productObject = await SearchObjectAsync(le, cbxFiliale.Text);
                    product = productObject;
                    productObject = null;
                }
                else
                {
                    var productObject = await SearchObjectAsync(le, LogIn.filiale);
                    product = productObject;
                    productObject = null;
                }
                
                if (!string.IsNullOrEmpty(product.Type))
                    txtNom.Text = product.Marque + " " + product.Taille + " " + product.Catégorie + " " + product.Type;
                else
                    txtNom.Text = product.Marque + " " + product.Taille + " " + product.Catégorie;
                txtStock.Text = Convert.ToDecimal(product.Stock).ToString("N0");
                cbxCode.Text = product.Code_Barre;

                if(product.Code_Barre == null)
                {
                    txtNewCode.Focus();
                    Alert.SShow("Cet élément n'a pas de code barre. Veillez attribuer un code barre.", Alert.AlertType.Info);
                    
                }else 
                txtQuantité.Focus();
                array2 = null;
                le = null;
            }
            red = null;
            array1 = null;
        }

        public static Task<ProductObject> SearchObjectAsync(
          List<string> list,
          string _filiale)
        {
            return Task.Factory.StartNew(() => SearchProduit(list, _filiale));
        }

        public static ProductObject SearchProduit(List<string> list, string _filiale)
        {
            ProductObject productObject = new ProductObject();
            string nom = list[0];
            string catégorie = list[2].Trim();
            string taille = list[1];
            string type = null;
            if(list.Count == 4)
            type = list[3];

            if (taille.EndsWith("p"))
                taille = taille.Split('p')[0];

            if (string.IsNullOrEmpty(taille))
                taille = null;

            if (string.IsNullOrEmpty(type))
                type = null;
            using (var financeDataContext = new QuitayeContext())
            {
                productObject = (financeDataContext.tbl_stock_produits_vente.Where(d => d.Marque.ToLower().Equals(nom.ToLower())
                 && d.Catégorie.ToLower().Equals(catégorie.ToLower()) && d.Taille == taille
                 && d.Type == type
                 && d.Detachement == _filiale).Select(d => new ProductObject()
                 {
                     Id = d.Id,
                     Taille = d.Taille,
                     Type = d.Type,
                     Catégorie = d.Catégorie,
                     Marque = d.Marque,
                     Stock = d.Quantité,
                     Code_Barre = d.Code_Barre
                 })).FirstOrDefault();
                if(productObject == null && type == null)
                {
                    productObject = (financeDataContext.tbl_stock_produits_vente.Where(d => d.Marque.ToLower().Equals(nom.ToLower())
                 && d.Catégorie.ToLower().Equals(catégorie.ToLower()) && d.Taille == taille
                 && d.Type == ""
                 && d.Detachement == _filiale).Select(d => new ProductObject()
                 {
                     Id = d.Id,
                     Taille = d.Taille,
                     Type = d.Type,
                     Catégorie = d.Catégorie,
                     Marque = d.Marque,
                     Stock = d.Quantité,
                     Code_Barre = d.Code_Barre
                 })).FirstOrDefault();
                }
            }
            return productObject;
        }

        public static Task<ProductObject> SearchObjectAsync(List<string> list) => Task.Factory.StartNew(() => SearchProduit(list));

        public static ProductObject SearchProduit(List<string> list)
        {
            ProductObject productObject = new ProductObject();
            string nom = list[0];
            string catégorie = list[2].Trim();
            string taille = list[1];
            string type = null;
            if (list.Count == 4)
                type = list[3];

            if (taille.EndsWith("p"))
                taille = taille.Split('p')[0];

            if (string.IsNullOrEmpty(taille))
                taille = null;

            if (string.IsNullOrEmpty(type))
                type = null;
            using (var financeDataContext = new QuitayeContext())
            {
                productObject = (financeDataContext.tbl_stock_produits_vente.Where(d => d.Marque.ToLower().Equals(nom.ToLower())
                 && d.Catégorie.ToLower().Equals(catégorie.ToLower()) && d.Taille == taille
                 && d.Type == type).Select(d => new ProductObject()
                 {
                     Id = d.Id,
                     Taille = d.Taille,
                     Type = d.Type,
                     Catégorie = d.Catégorie,
                     Marque = d.Marque,
                     Stock = d.Quantité,
                     Code_Barre = d.Code_Barre
                 })).FirstOrDefault();
                if(productObject == null && type == null)
                {
                    productObject = (financeDataContext.tbl_stock_produits_vente.Where(d => d.Marque.ToLower().Equals(nom.ToLower())
                 && d.Catégorie.ToLower().Equals(catégorie.ToLower()) && d.Taille == taille
                 && d.Type == "").Select(d => new ProductObject()
                 {
                     Id = d.Id,
                     Taille = d.Taille,
                     Type = d.Type,
                     Catégorie = d.Catégorie,
                     Marque = d.Marque,
                     Stock = d.Quantité,
                     Code_Barre = d.Code_Barre
                 })).FirstOrDefault();
                }
            }
            return productObject;
        }

        private Task<bool> Print_TiketAsync(
          Models.Info_Entreprise entreprise,
          List<Models.VenteList> list,
          Sales_Details sales_Details)
        {
            return Task.Factory.StartNew((() => Print_Tiket(entreprise, list, sales_Details)));
        }

        private bool Print_Tiket(
          Models.Info_Entreprise entreprise,
          List<Models.VenteList> list,
          Sales_Details sales_Details)
        {
            //BarcodePrinter.PrintReceipt(entreprise, list, sales_Details);
            
            return true;
        }

        public string default_client  { get; set; }
        public AchatVente(string _num_operation, string _client, string _contact, string _type, bool returned = false)
          : this()
        {
            Returned = returned;
            if (_type == "Achat")
            {
                num_achat = _num_operation;
                cbxType.SelectedIndex = 1;
                cbxFournisseurs.Text = _client;
                default_client = _client;
            }
            else if (_type == "Vente")
            {
                num_vente = _num_operation;
                cbxType.SelectedIndex = 0;
                cbxClient.Text = _client;
                default_client = _client;
            }
            cbxType.Text = _type;
            txtNum.Text = _contact;
            btnFermer.Click += BtnFermer_Click;
            loadTimer.Tick += LoadTimer_Tick;

        }

        private void LoadTimer_Tick1(object sender, EventArgs e)
        {

        }

        private async void BtnAddClient_Click(object sender, EventArgs e)
        {
            //Info_Client client = new Info_Client();
            //int num = (int)client.ShowDialog();
            //if (!(client.ok == "Oui"))
            //{
            //    client = null;
            //}
            //else
            //{
            //    await CallClient();
            //    client = null;
            //}
        }

        private void CbxClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (first)
#pragma warning disable CS0642 // Possibilité d'instruction vide erronée
                ;
#pragma warning restore CS0642 // Possibilité d'instruction vide erronée
        }

        private void Txtmontantpayer_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the input is coming from a barcode scanner
            if (IsBarcodeScannerInput(e))
            {
                e.Handled = true; // Prevent the textbox from accepting the input
                cbxCode.Focus();
            }
            else
            {
                if (e.KeyChar == '\r')
                {
                    BtnImprimerTicket_Click(null, null);
                    return;
                }

                if (char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '\b')
                    return;
                e.Handled = true;
            }
        }

        private bool IsBarcodeScannerInput(KeyPressEventArgs e)
        {
            const int WM_KEYDOWN = 0x100;

            // Check if the input is a keyboard event and not a control character
            if (e.KeyChar >= 32 && e.KeyChar <= 126 && e.KeyChar != 127)
            {
                // Check if the input is part of a keyboard sequence
                if ((int)e.KeyChar == WM_KEYDOWN)
                {
                    return true; // Input is likely coming from a barcode scanner
                }
            }

            return false; // Input is from a regular keyboard
        }
        private void CbxModePrix_SelectedIndexChanged(object sender, EventArgs e) => TxtQuantité_TextChanged(null, e);

        private async void BtnImprimerTicket_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Count > 1)
            {
                PrintTicket = true;
                
                //bool result = await Print_TiketAsync(entreprise, list, new Sales_Details() { Montant_Total = montant_total, Montant_Net_Payé = montant_net_payé, Réduction = réduction, Montant_Rétourné =  montant_retourné, });
                BtnEnregistrer_Click(null, null);
                
                //PrintTicket = false;
            }
            else
                Alert.SShow("Il aucune donnée à imprimer!", Alert.AlertType.Sucess);
        }

        public void PrintReceip(out Models.Info_Entreprise entreprise, out List<Models.VenteList> list)
        {
            entreprise = new Models.Info_Entreprise();
            var entre_details = new List<tbl_entreprise_autres_details>();
            using (var donnée = new QuitayeContext())
            {
                var des = donnée.tbl_entreprise.Where(d => d.Id == 1).Select(d => new
                {
                    Id = d.Id,
                    Nom = d.Nom,
                    Email = d.Email,
                    Telephone = d.Téléphone,
                    Adresse = d.Adresse,
                    Date_Ouverture = d.Date_Ouverture

                }).First();

                entre_details = donnée.tbl_entreprise_autres_details.ToList();

                entreprise.Nom = des.Nom;
                entreprise.Adresse = des.Adresse;
                entreprise.Telephone = des.Telephone;
                entreprise.Email = des.Email;
                des = null;
            }
            list = new List<Models.VenteList>();
            foreach (Quitaye_School.Models.VenteList item in vente)
                list.Add(new Models.VenteList()
                {
                    Id = item.Id,
                    Marque = item.Marque,
                    Catégorie = item.Catégorie,
                    Taille = item.Taille,
                    Quantité = item.Quantité,
                    Prix_Unitaire = item.Montant / item.Quantité,
                    Montant = item.Montant,
                    Mesure = item.Mesure,
                    Type = item.Type
                });
            decimal montant_net_payé = 0;
            decimal montant_retourné = 0;
            decimal réduction = 0;
            decimal montant_total = 0;
            montant_total = Convert.ToDecimal(list.Sum(x => x.Montant));

            if (!string.IsNullOrEmpty(txtmontantpayer.Text))
            {
                montant_net_payé = Convert.ToDecimal(txtmontantpayer.Text);
                montant_retourné = montant_net_payé - montant_total;
            }

            if (!string.IsNullOrEmpty(txtRéduction.Text))
            {
                réduction = Convert.ToDecimal(txtRéduction.Text);
                montant_retourné = montant_net_payé - (montant_total - réduction);
            }

            if (string.IsNullOrEmpty(txtmontantpayer.Text))
            {
                montant_net_payé = 0;
                montant_retourné = 0;
            }


            var ticket = new Ticket(entreprise, list, new Sales_Details()
            {
                Montant_Total = montant_total,
                Montant_Net_Payé = montant_net_payé,
                Réduction = réduction,
                Montant_Rétourné = montant_retourné,
                Num_Vente = num_vente,
                Ticket_Date = DateTime.Now
            }, entre_details);
            ticket.print_Receip();
        }

        private void BtnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (!(sender is IconButton))
                return;
            openFileDialog.Filter = "(*.pdf; *.xls;*.docs; *.png; *.jpg; *.jpeg)| *.pdf; *.xls; *.docs; *.png; *.jpg; *.jpeg";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filepath = openFileDialog.FileName;
                filename = Path.GetFileName(filepath);
            }
        }

        private async void CbxCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
                return;
            e.Handled = true;

            await CodeSearch();
            //BtnAjouter_Click(null, (EventArgs)e);
        }

        private async void TxtQuantité_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsBarcodeScannerInput(e))
            {
                e.Handled = true; // Prevent the textbox from accepting the input
                cbxCode.Focus();
            }else
            {
                if (e.KeyChar == 12)
                {
                    // Jump to the specific control
                    txtmontantpayer.Focus();
                }
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
                    e.Handled = true;
                if (e.KeyChar != '\r')
                    return;
                e.Handled = true;
                if (cbxType.Text == "Vente")
                {
                    await VenteProduit();
                }
                else if(cbxType.Text =="Achat")
                {
                    await AchatGadget();
                }
                else if(cbxType.Text =="Expense Damaged")
                {
                    await DamagedExpense();
                }
            }
        }

        private async void TxtQuantité_TextChanged(object sender, EventArgs e)
        {
            
            if (txtQuantité.Text != "" && txtQuantité.Text.Length > 0 && cbxMesure.Text != "" && cbxMesure.Text.Length > 0 && cbxCode.Text.Length > 0)
            {
                if (product == null)
                    return;
                bool g = false;
                if (cbxModePrix.Text == "Grossiste")
                    g = true;
                string type = "Vente";
                if (cbxType.Text != "")
                    type = cbxType.Text;
                var result = await PrixAsync(Convert.ToDecimal(txtQuantité.Text), cbxMesure.Text, cbxCode.Text, type, g);
                txtMontant.Text = result.Item1.ToString("N0");
                txtPrixUnité.Text = result.Item2.ToString("N0");
                type = null;
            }
            else
            {
                txtMontant.Text = "0";
                txtPrixUnité.Text = "0";
            }
        }

        private Task<(decimal, decimal)> PrixAsync(
          decimal qty,
          string mesure,
          string code,
          string type_operation,
          bool grossiste)
        {
            return Task.Factory.StartNew(() => Prix(qty, mesure, code, type_operation, grossiste));
        }

        private (decimal, decimal) Prix(
          Decimal qty,
          string mesure,
          string code,
          string type_operation,
          bool grossiste)
        {
            decimal montant = 0M;
            decimal prix_unité = 0M;
            using (var financeDataContext = new QuitayeContext())
            {
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                try
                {
                    if (mesure != "" && code != "")
                    {
                        var mesu = financeDataContext.tbl_mesure_vente
                            .Where(d => d.Nom == mesure).Select(d => new
                            {
                                Type = d.Type
                            });
                        if (mesu.Count() != 0)
                        {
                            var data = financeDataContext.tbl_mesure_vente.
                                Where(d => d.Nom == mesure).Select(d => new
                            {
                                Type = d.Type
                            }).First();
                            string type = data.Type;
                            var source = (from d in financeDataContext.tbl_produits
                                          join mul in financeDataContext.tbl_multi_barcode 
                                          on d.Id equals mul.Product_Id into joinedTable
                                          from m in joinedTable.DefaultIfEmpty()
                                          where m.Barcode == code
                                          join arr in financeDataContext.tbl_arrivée
                                          on d.Id equals arr.Product_Id into joinedTablearr
                                          from ar in joinedTablearr.OrderByDescending(x => x.Date_Arrivée).DefaultIfEmpty()
                                          select new
                                          {
                                              Id = d.Id,
                                              Prix_Petit = d.Prix_Petit,
                                              Prix_Moyen = d.Prix_Moyen,
                                              Prix_Grand = d.Prix_Grand,
                                              Prix_Large = d.Prix_Large,
                                              Prix_Hyper_Large = d.Prix_Hyper_Large,
                                              Prix_Petit_Grossiste = d.Prix_Petit_Grossiste,
                                              Prix_Moyen_Grossiste = d.Prix_Moyen_Grossiste,
                                              Prix_Grand_Grossiste = d.Prix_Grand_Grossiste,
                                              Prix_Large_Grossiste = d.Prix_Large_Grossiste,
                                              Prix_Hyper_Large_Grossiste = d.Prix_Hyper_Large_Grossiste,
                                              Prix_Achat_Petit = ar.Prix / ar.Quantité,
                                              Prix_Achat_Moyen = d.Prix_Achat_Moyen,
                                              Prix_Achat_Grand = d.Prix_Achat_Grand,
                                              Prix_Achat_Large = d.Prix_Achat_Large,
                                              Prix_Achat_Hyper_Large = d.Prix_Achat_Hyper_Large
                                          }).FirstOrDefault();

                            //if(source.FirstOrDefault() != null)
                            //{
                            //    var da = financeDataContext.tbl_arrivée.Where(x => x.Product_Id == source.FirstOrDefault().Id).OrderByDescending(x => x.Id).FirstOrDefault();
                            //    var prix = Convert.ToDecimal(da.Prix / da.Quantité);
                            //    //source.FirstOrDefault().Prix_Achat_Petit = prix;
                            //}
                            if(source != null)
                            {
                                if (type_operation == "Vente")
                                {
                                    if (grossiste)
                                    {
                                        if (type == "Petit")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Petit_Grossiste) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Petit_Grossiste);
                                            product.Prix_Petit_Grossiste = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Moyen")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Moyen_Grossiste) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Moyen_Grossiste);
                                            product.Prix_Moyen_Grossiste = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Grand")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Grand_Grossiste) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Grand_Grossiste);
                                            product.Prix_Grand_Grossiste = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Large")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Large_Grossiste) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Large_Grossiste);
                                            product.Prix_Large_Grossiste = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Hyper Large")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Hyper_Large_Grossiste) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Hyper_Large_Grossiste);
                                            product.Prix_Hyper_Large_Grossiste = montant;
                                            product.Mesure = type;
                                        }
                                    }
                                    else
                                    {
                                        if (type == "Petit")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Petit) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Petit);
                                            product.Prix_Petit = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Moyen")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Moyen) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Moyen);
                                            product.Prix_Moyen = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Grand")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Grand) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Grand);

                                            product.Prix_Grand = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Large")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Large) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Large);

                                            product.Prix_Large = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Hyper Large")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Hyper_Large) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Hyper_Large);

                                            product.Prix_Hyper_Large = montant;
                                            product.Mesure = type;
                                        }
                                    }

                                    return (montant, prix_unité);
                                }
                                else if (type_operation == "Achat")
                                {
                                    if (type == "Petit")
                                    {
                                        montant = Convert.ToDecimal(source.Prix_Achat_Petit) * qty;
                                        prix_unité = Convert.ToDecimal(source.Prix_Achat_Petit);
                                        product.Prix_Achat_Petit = montant;
                                        product.Mesure = type;
                                    }
                                    else if (type == "Moyen")
                                    {
                                        montant = Convert.ToDecimal(source.Prix_Achat_Moyen) * qty;
                                        prix_unité = Convert.ToDecimal(source.Prix_Achat_Moyen);
                                        product.Prix_Achat_Moyen = montant;
                                        product.Mesure = type;
                                    }
                                    else if (type == "Grand")
                                    {
                                        montant = Convert.ToDecimal(source.Prix_Achat_Grand) * qty;
                                        prix_unité = Convert.ToDecimal(source.Prix_Achat_Grand);
                                        product.Prix_Achat_Grand = montant;
                                        product.Mesure = type;
                                    }
                                    else if (type == "Large")
                                    {
                                        montant = Convert.ToDecimal(source.Prix_Achat_Large) * qty;
                                        prix_unité = Convert.ToDecimal(source.Prix_Achat_Large);
                                        product.Prix_Achat_Large = montant;
                                        product.Mesure = type;
                                    }
                                    else if (type == "Hyper Large")
                                    {
                                        montant = Convert.ToDecimal(source.Prix_Achat_Hyper_Large) * qty;
                                        prix_unité = Convert.ToDecimal(source.Prix_Achat_Hyper_Large);
                                        product.Prix_Achat_Hyper_Large = montant;
                                        product.Mesure = type;
                                    }
                                }
                                else if (type_operation == "Expense Damaged")
                                {
                                    if (grossiste)
                                    {
                                        if (type == "Petit")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Petit_Grossiste) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Petit_Grossiste);
                                            product.Prix_Petit_Grossiste = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Moyen")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Moyen_Grossiste) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Moyen_Grossiste);
                                            product.Prix_Moyen_Grossiste = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Grand")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Grand_Grossiste) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Grand_Grossiste);
                                            product.Prix_Grand_Grossiste = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Large")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Large_Grossiste) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Large_Grossiste);
                                            product.Prix_Large_Grossiste = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Hyper Large")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Hyper_Large_Grossiste) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Hyper_Large_Grossiste);
                                            product.Prix_Hyper_Large_Grossiste = montant;
                                            product.Mesure = type;
                                        }
                                    }
                                    else
                                    {
                                        if (type == "Petit")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Achat_Petit) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Achat_Petit);
                                            product.Prix_Petit = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Moyen")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Moyen) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Moyen);
                                            product.Prix_Moyen = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Grand")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Grand) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Grand);

                                            product.Prix_Grand = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Large")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Large) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Large);

                                            product.Prix_Large = montant;
                                            product.Mesure = type;
                                        }
                                        else if (type == "Hyper Large")
                                        {
                                            montant = Convert.ToDecimal(source.Prix_Hyper_Large) * qty;
                                            prix_unité = Convert.ToDecimal(source.Prix_Hyper_Large);

                                            product.Prix_Hyper_Large = montant;
                                            product.Mesure = type;
                                        }
                                    }

                                    return (montant, prix_unité);
                                }
                            }
                            
                        }
                        return (montant, prix_unité);
                    }

                    if (financeDataContext.tbl_mesure_vente.Where(d => d.Type == "Petit").Select(d => new
                    {
                        Type = d.Type
                    }).Count() != 0)
                    {
                        var data1 = financeDataContext.tbl_mesure_vente
                            .Where(d => d.Type == "Petit").Select(d => new
                        {
                            Type = d.Type
                        }).First();
                        string type = data1.Type;
                        var data2 = (from d in financeDataContext.tbl_produits
                                    join mul in financeDataContext.tbl_multi_barcode
                                    on d.Id equals mul.Product_Id into joinedTable
                                    from m in joinedTable.DefaultIfEmpty()
                                     where m.Barcode == code
                                     join arr in financeDataContext.tbl_arrivée
                                    on d.Id equals arr.Product_Id into joinedTablearr
                                    from ar in joinedTablearr.OrderByDescending(x => x.Date_Arrivée).DefaultIfEmpty() select
                            new
                        {
                            Prix_Petit = d.Prix_Petit,
                            Prix_Moyen = d.Prix_Moyen,
                            Prix_Grand = d.Prix_Grand,
                            Prix_Large = d.Prix_Large,
                            Prix_Hyper_Large = d.Prix_Hyper_Large,
                                Prix_Achat_Petit = ar.Prix / ar.Quantité,

                                Prix_Petit_Grossiste = d.Prix_Petit_Grossiste,
                            Prix_Moyen_Grossiste = d.Prix_Moyen_Grossiste,
                            Prix_Grand_Grossiste = d.Prix_Grand_Grossiste,
                            Prix_Large_Grossiste = d.Prix_Large_Grossiste,
                            Prix_Hyper_Large_Grossiste = d.Prix_Hyper_Large_Grossiste
                        }).FirstOrDefault();
                        if(data2 != null)
                        {
                            if (grossiste)
                            {
                                if (type == "Petit")
                                {
                                    montant = Convert.ToDecimal(data2.Prix_Petit_Grossiste) * qty;
                                    prix_unité = Convert.ToDecimal(data2.Prix_Petit_Grossiste);
                                    product.Prix_Petit_Grossiste = montant;
                                    product.Mesure = type;
                                }
                                else if (type == "Moyen")
                                {
                                    montant = Convert.ToDecimal(data2.Prix_Moyen_Grossiste) * qty;
                                    prix_unité = Convert.ToDecimal(data2.Prix_Moyen_Grossiste);
                                    product.Prix_Moyen_Grossiste = montant;
                                    product.Mesure = type;
                                }
                                else if (type == "Grand")
                                {
                                    montant = Convert.ToDecimal(data2.Prix_Grand_Grossiste) * qty;
                                    prix_unité = Convert.ToDecimal(data2.Prix_Grand_Grossiste);
                                    product.Prix_Grand_Grossiste = montant;
                                    product.Mesure = type;
                                }
                                else if (type == "Large")
                                {
                                    montant = Convert.ToDecimal(data2.Prix_Large_Grossiste) * qty;
                                    prix_unité = Convert.ToDecimal(data2.Prix_Large_Grossiste);
                                    product.Prix_Large_Grossiste = montant;
                                    product.Mesure = type;
                                }
                                else if (type == "Hyper Large")
                                {
                                    montant = Convert.ToDecimal(data2.Prix_Hyper_Large_Grossiste) * qty;
                                    prix_unité = Convert.ToDecimal(data2.Prix_Hyper_Large_Grossiste);
                                    product.Prix_Hyper_Large_Grossiste = montant;
                                    product.Mesure = type;
                                }
                            }
                            else if (type == "Petit")
                            {
                                montant = Convert.ToDecimal(data2.Prix_Petit) * qty;
                                prix_unité = Convert.ToDecimal(data2.Prix_Petit);
                                product.Prix_Petit = montant;
                                product.Mesure = type;
                            }
                            else if (type == "Moyen")
                            {
                                montant = Convert.ToDecimal(data2.Prix_Moyen) * qty;
                                prix_unité = Convert.ToDecimal(data2.Prix_Moyen);
                                product.Prix_Moyen = montant;
                                product.Mesure = type;
                            }
                            else if (type == "Grand")
                            {
                                montant = Convert.ToDecimal(data2.Prix_Grand) * qty;
                                prix_unité = Convert.ToDecimal(data2.Prix_Grand);
                                product.Prix_Grand = montant;
                                product.Mesure = type;
                            }
                            else if (type == "Large")
                            {
                                montant = Convert.ToDecimal(data2.Prix_Large) * qty;
                                prix_unité = Convert.ToDecimal(data2.Prix_Large);
                                product.Prix_Large = montant;
                                product.Mesure = type;
                            }
                            else if (type == "Hyper Large")
                            {
                                montant = Convert.ToDecimal(data2.Prix_Hyper_Large) * qty;
                                prix_unité = Convert.ToDecimal(data2.Prix_Hyper_Large);
                                product.Prix_Hyper_Large = montant;
                                product.Mesure = type;
                            }
                        }
                    }
                    return (montant, prix_unité);
                }
                catch (Exception ex)
                {
                    return (montant, prix_unité);
                }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
            }
        }

        private async void TxtCodeBarre_TextChanged(object sender, EventArgs e) => await CodeSearch();

        private async Task CodeSearch()
        {
            if (!(cbxCode.Text != "") || cbxCode.Text.Length <= 0)
                return;
            if (LogIn.type_compte.Contains("Administrateur") && cbxFiliale.Text != "")
            {
                var productObject = await SearchProduct.SearchCodeAsync(cbxCode.Text, cbxFiliale.Text);
                product = productObject;
                productObject = null;
            }
            else
            {
                if (!(LogIn.type_compte == "Utilisateur") && (LogIn.filiale == "" || LogIn.filiale == null))
                    Alert.SShow("Veillez selectionner une filiale spécifique.", Alert.AlertType.Info);
                product = new ProductObject();
            }
            if (LogIn.type_compte == "Utilisateur")
            {
                ProductObject productObject = await SearchProduct.SearchCodeAsync(cbxCode.Text);
                product = productObject;
                productObject = null;
            }
            if (product != null)
            {
                if(product.Marque != null && product.Catégorie != null && cbxCode.Text != null)
                {
                    product.Code_Barre = cbxCode.Text;
                    if (!string.IsNullOrEmpty(product.Type))
                        txtNom.Text = product.Marque + " " + product.Taille + " " + product.Catégorie + " " + product.Type;
                    else
                        txtNom.Text = product.Marque + " " + product.Taille + " " + product.Catégorie;
                    decimal num = Convert.ToDecimal(product.Stock);
                    string str1 = num.ToString("N0");
                    txtStock.Text = str1;
                    int niveau = Convert.ToInt32(cbxMesure.SelectedValue);
                    if (txtQuantité.Text != "")
                    {
                        switch (niveau)
                        {
                            case 1:
                                System.Windows.Forms.TextBox txtPrix1 = txtMontant;
                                num = Convert.ToDecimal(product.Prix_Petit) * Convert.ToDecimal(txtQuantité.Text);
                                string str2 = num.ToString("N0");
                                txtPrixUnité.Text = Convert.ToDecimal(product.Prix_Petit).ToString("N0");
                                txtPrix1.Text = str2;
                                break;
                            case 2:
                                TextBox txtPrix2 = txtMontant;
                                num = Convert.ToDecimal(product.Prix_Moyen) * Convert.ToDecimal(txtQuantité.Text);
                                string str3 = num.ToString("N0");
                                txtPrixUnité.Text = Convert.ToDecimal(product.Prix_Moyen).ToString("N0");
                                txtPrix2.Text = str3;
                                break;
                            case 3:
                                TextBox txtPrix3 = txtMontant;
                                num = Convert.ToDecimal(product.Prix_Grand) * Convert.ToDecimal(txtQuantité.Text);
                                string str4 = num.ToString("N0");
                                txtPrixUnité.Text = Convert.ToDecimal(product.Prix_Grand).ToString("N0");
                                txtPrix3.Text = str4;
                                break;
                            case 4:
                                TextBox txtPrix4 = txtMontant;
                                num = Convert.ToDecimal(product.Prix_Large) * Convert.ToDecimal(txtQuantité.Text);
                                string str5 = num.ToString("N0");
                                txtPrixUnité.Text = Convert.ToDecimal(product.Prix_Large).ToString("N0");
                                txtPrix4.Text = str5;
                                break;
                            case 5:
                                TextBox txtPrix5 = txtMontant;
                                num = Convert.ToDecimal(product.Prix_Hyper_Large) * Convert.ToDecimal(txtQuantité.Text);
                                string str6 = num.ToString("N0");
                                txtPrixUnité.Text = Convert.ToDecimal(product.Prix_Hyper_Large).ToString("N0");
                                txtPrix5.Text = str6;
                                break;
                        }
                        if (cbxMesure.SelectedValue == null)
                        {
                            txtPrixUnité.Text = Convert.ToDecimal(product.Prix_Petit).ToString("N0");
                            txtMontant.Text = Convert.ToDecimal(product.Prix_Petit * Convert.ToInt32(txtQuantité.Text)).ToString("N0");
                        }
                    }
                }else
                {
                    txtNom.Text = null;

                }
                
            }
            else
            {
                txtNom.Text = null;
                txtStock.Text = null;

            }
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            num_vente = null;
            num_achat = null;
            if (Minimize && Close_All)
            {
                Application.Exit();
            }
            else this.Close();
               
        }

        private async void CbxNom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (first)
                return;
            await CodeSearch();
            txtQuantité.Focus();
            TxtQuantité_TextChanged(null, e);
            if (cbxType.Text == "Vente")
            {
                if (product != null && !string.IsNullOrEmpty(product.Code_Barre))
                {
                    await VenteProduit();
                }
            }
            else if(cbxType.Text == "Expense Damaged")
            {
                if (product != null && !string.IsNullOrEmpty(product.Code_Barre))
                {
                    await DamagedExpense();
                }
            }
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallTask();
            cbxCode.Focus();
        }

        private async void CallCbx()
        {
            DataTable result = await FillProduitCbxAsync();
            cbxCode.DataSource = result;
            cbxCode.DisplayMember = "Code";
            cbxCode.ValueMember = "Id";
            cbxCode.Text = null;
            txtNom.Clear();
            result = null;
        }

        public static Task<DataTable> FillProduitCbxAsync() => Task.Factory.StartNew(() => FillProduitCbx());

        public static DataTable FillProduitCbx()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Code");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = from d in financeDataContext.tbl_stock_produits_vente 
                             where d.Quantité > 0 select new
                {
                    Id = d.Id,
                    Code = d.Code_Barre
                };
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Code;
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }

        private async Task CallCbxFournisseur()
        {
            DataTable result = await FillCbxFournisseurAsync();
            cbxFournisseurs.DataSource = result;
            cbxFournisseurs.DisplayMember = "Nom";
            cbxFournisseurs.ValueMember = "Id";
            cbxFournisseurs.Text = null;
            result = null;
        }

        private async Task CallPendings()
        {
            var result = await FillPendingListAsync();
            cbxPendingList.DataSource = result;
            cbxPendingList.DisplayMember = "Nom";
            cbxPendingList.ValueMember = "Id";
            cbxPendingList.Text = null;
            result = null;
        }

        public static Task<DataTable> FillCbxFournisseurAsync() => Task.Factory.StartNew(() => fillCbxfournisseur());

        private static DataTable fillCbxfournisseur()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Nom");
            using (var financeDataContext = new QuitayeContext())
            {
                var tblFournisseurs = from d in financeDataContext.tbl_fournisseurs orderby d.Nom select new
                {
                    Id = d.Id,
                    Nom = d.Nom
                };
                foreach (var data in tblFournisseurs)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Nom;
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }

        public static Task<DataTable> FillPendingListAsync() => Task.Factory.StartNew(() => FillPending());

        private static DataTable FillPending()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Nom");
            using (var donnée = new QuitayeContext())
            {
                var pendings = (from d in donnée.tbl_vente_temp 
                                where d.Auteur == LogIn.profile && DbFunctions.TruncateTime(d.Date_Vente) == DbFunctions.TruncateTime(DateTime.Today)
                                orderby d.Pending
                                group d by new
                                {
                                    Pending = d.Pending,
                                }into gr
                                select new
                                {
                                    Id = gr.Key.Pending,
                                    Pending = gr.Key.Pending,
                                    Montant = gr.Sum(x => x.Montant),
                                }).ToList();
                foreach (var data in pendings)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = "Pending"+ data.Pending+"...."+ Convert.ToDecimal(data.Montant).ToString("N0");
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }

        private async void CallCbxBon()
        {
            DataTable result = await FillCbxBonAsync();
            cbxBonCommande.DataSource = result;
            cbxBonCommande.DisplayMember = "Bon_Commande";
            cbxBonCommande.ValueMember = "Id";
            cbxBonCommande.Text = null;
            result = null;
        }

        public static Task<DataTable> FillCbxBonAsync() => Task.Factory.StartNew<DataTable>((() => fillCbxBon()));

        private static DataTable fillCbxBon()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Bon_Commande");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = from d in financeDataContext.tbl_bon orderby d.Id descending select new
                {
                    Id = d.Id,
                    Bon_Commande = d.Bon_Commande
                };
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Bon_Commande;
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }

        private Task<N_Produit> ProductAsync(int id) => Task.Factory.StartNew(() => Product(id));

        private N_Produit Product(int id)
        {
            N_Produit nProduit = new N_Produit();
            using (var financeDataContext = new QuitayeContext())
            {
                var des = financeDataContext.tbl_produits.Where((d => d.Id == id)).First();
                var formuleMesureVente = financeDataContext.tbl_formule_mesure_vente.Where(d => (int?)d.Id == des.Formule_Stockage).First();
                nProduit.Id = des.Id;
                nProduit.Catégorie = des.Catégorie;
                nProduit.Code_Barre = des.Barcode;
                nProduit.Image_Byte = des.Image.ToArray();
                nProduit.Taille = des.Taille;
                nProduit.Code_Barre = des.Barcode;
                nProduit.Formule = Convert.ToInt32(des.Formule_Stockage);
                nProduit.Prix_Petit = Convert.ToDecimal(des.Prix_Petit);
                nProduit.Prix_Moyen = Convert.ToDecimal(des.Prix_Moyen);
                nProduit.Prix_Grand = Convert.ToDecimal(des.Prix_Grand);
                nProduit.Prix_Large = Convert.ToDecimal(des.Prix_Large);
                nProduit.Prix_Hyper_Large = Convert.ToDecimal(des.Prix_Hyper_Large);
                nProduit.Nom = des.Nom;
                nProduit.Mesure = des.Mesure;
                nProduit.Stock_Max = (Decimal)Convert.ToInt32(des.Stock_max);
                nProduit.Stock_Min = (Decimal)Convert.ToInt32(des.Stock_min);
                nProduit.N_Formule = formuleMesureVente.Formule;
            }
            return nProduit;
        }
        public string Active_Pending { get; set; }
        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns.Count <= 1)
                return;
            var sd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (!string.IsNullOrEmpty(sd))
            {
                int id = Convert.ToInt32(sd);
                if (!dataGridView1.Columns[e.ColumnIndex].Name.Equals("Edit") && dataGridView1.Columns[e.ColumnIndex].Name.Equals("Sup"))
                {
                    if (cbxType.Text == "Vente")
                    {
                        if (await DeleteVenteAsync(id))
                        {
                            if (dataGridView1.Rows.Count <= 2)
                                Active_Pending = null;
                            Alert.SShow("Element supprimé avec succès.", Alert.AlertType.Sucess);
                            await CallTaskSecond(Active_Pending);
                        }
                    }
                    else if (await DeleteAchatAsync(id))
                    {
                        Alert.SShow("Element supprimé avec succès.", Alert.AlertType.Sucess);
                        await CallTaskSecond();
                    }
                }
            }
            
        }

        private async Task<bool> DeleteVenteAsync(int id)
        {
            using (var financeDataContext = new QuitayeContext())
            {
                var entity = financeDataContext.tbl_vente_temp.Where(d => d.Id == id).First();
                financeDataContext.tbl_vente_temp.Remove(entity);
                await financeDataContext.SaveChangesAsync();
                return true;
            }
        }

        private async Task<bool> DeleteAchatAsync(int id)
        {
            using (var financeDataContext = new QuitayeContext())
            {
                var entity = financeDataContext.tbl_arrivée_temp.Where((d => d.Id == id)).First();
                financeDataContext.tbl_arrivée_temp.Remove(entity);
                await financeDataContext.SaveChangesAsync();
                return true;
            }
        }

        private async void BtnAjouter_Click(object sender, EventArgs e)
        {
            if (LogIn.expiré || !(cbxCode.Text != "") 
                || !(txtNom.Text != "") || !(txtStock.Text != "") 
                || !(txtQuantité.Text != ""))
                return;
            using (var donnée = new QuitayeContext())
            {
                var entre = donnée.tbl_entreprise.Where((d => d.Id == 1)).First();
                if (cbxType.Text == "Vente" && double.Parse(txtStock.Text) >= double.Parse(txtQuantité.Text))
                {
                    //if (entre.Info_Client == "Oui")
                    //{
                    //    if (txtNom.Text != "" && txtNum.Text != "")
                    //    {
                    //        if (cbxType.Text == "Vente")
                    //            await VenteProduit();
                    //    }
                    //    else
                    //        Alert.SShow("Veillez saisr le nom et numéro du client !!", Alert.AlertType.Info);
                    //}
                    //else
                        await VenteProduit();
                }
                else if (cbxType.Text == "Achat")
                {
                    await AchatGadget();
                    cbxCode.Focus();
                }
                else if(cbxType.Text == "Expense Damaged")
                {
                    await DamagedExpense();
                    cbxCode.Focus();
                }
                else
                {
                    int num = (int)MessageBox.Show("Le stock est inférieur à la demande. Veillez passer une commande de cet article !!", "Stock Inférieur", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                entre = null;
            }
        }

        private async Task<bool> AddStockTempAsync(OpérationTemp vente)
        {
            bool flag = false;
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_arrivée_temp.Select(d => new
                {
                    Id = d.Id
                }).Take(1);
                int num = 1;
                if (source.Count() != 0)
                {
                    var data = financeDataContext.tbl_arrivée_temp
                        .OrderByDescending(d => d.Id).Select(d => new
                    {
                        Id = d.Id
                    }).First();
                    num = data.Id + 1;
                }

                var prod_id = financeDataContext.tbl_multi_barcode.Where(x => x.Barcode == vente.Code_Barre).Select(x => x.Product_Id).FirstOrDefault();
                var entity = new Models.Context.tbl_arrivée_temp()
                {
                    Prix = new Decimal?(vente.Montant),
                    Id = num,
                    Fournisseur = vente.Client,
                    Quantité = new Decimal?((Decimal)vente.Quantité),
                    Nom = vente.Marque,
                    Catégorie = vente.Catégorie,
                    Taille = vente.Taille,
                    Mesure = vente.Mesure,
                    Auteur = LogIn.profile,
                    Date_Arrivée = new DateTime?(DateTime.Now),
                    Id_Fournisseur = vente.Id_Client.ToString(),
                    Barcode = vente.Code_Barre,
                    Bon_Commande = vente.Bon_Commande,
                    Date_Expiration = vente.Date_Expiration,
                    Product_Id = prod_id,
                };
                entity.Fournisseur = vente.Fournisseur;
                financeDataContext.tbl_arrivée_temp.Add(entity);
                await financeDataContext.SaveChangesAsync();
                flag = true;
            }
            return flag;
        }

        private async Task AchatGadget()
        {
            if (product != null && !string.IsNullOrEmpty(product.Code_Barre))
            {
                string montant = txtMontant.Text;
                try
                {
                    if (Convert.ToDecimal(montant) == 0)
                    {
                        Alert.SShow("Prix non établis pour ce produit.", Alert.AlertType.Warning);
                        return;
                    }
                }
                catch (Exception)
                {
                    Alert.SShow("Prix non établis pour ce produit.", Alert.AlertType.Warning);
                    return;
                }
                OpérationTemp vente = new OpérationTemp();
                vente.Marque = product.Marque;
                vente.Taille = product.Taille;
                vente.Catégorie = product.Catégorie;
                vente.Mesure = cbxMesure.Text;
                if(Returned)
                vente.Quantité = -Convert.ToInt32(txtQuantité.Text);
                else vente.Quantité = Convert.ToInt32(txtQuantité.Text);
                if (product.Mesure == "Petit")
                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Petit);
                else if (product.Mesure == "Moyen")
                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Moyen);
                else if (product.Mesure == "Grand")
                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Grand);
                else if (product.Mesure == "Large")
                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Large);
                else if (product.Mesure == "Hyper Large")
                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Hyper_Large);
                if(Returned)
                vente.Montant = -Convert.ToDecimal(txtMontant.Text);
                else vente.Montant = Convert.ToDecimal(txtMontant.Text);

                vente.Prix_Unité = vente.Montant / Convert.ToDecimal(txtQuantité.Text);

                vente.Client = cbxClient.Text;
                vente.Num_Client = txtNum.Text;
                vente.Model = product.Type;
                vente.Code_Barre = product.Code_Barre;
                vente.Fournisseur = cbxFournisseurs.Text;
                vente.Id_Client = Convert.ToInt32(cbxFournisseurs.SelectedValue);
                vente.Bon_Commande = cbxBonCommande.Text;
                vente.Date_Expiration = Date_Expiration.Value.Date;
                if(!await CheckIfArticleExistInPurchaseInvoiceAsync(vente))
                {
                    bool result = await AddStockTempAsync(vente);
                    if (!result)
                    {
                        vente = (OpérationTemp)null;
                    }
                    else
                    {
                        ClearTemp();
                        await CallTaskSecond();
                        product = (ProductObject)null;
                        vente = (OpérationTemp)null;
                    }
                }else
                {
                    Alert.SShow("Cet article existe déjà dans la liste, Veillez-verifier", Alert.AlertType.Warning);
                }
            }
        }

        public async Task CallAcha()
        {
            MyTable result = await FillAchatAsync();
            if (result.Table.Rows.Count == 0)
            {
                dataGridView1.Columns.Clear();
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée disponible dans le tableau";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                dt = null;
                dr = null;
                result = null;
            }
            else
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = result.Table;
                first = false;
                dataGridView1.Columns[0].Visible = false;
                txtSousTotal.Text = result.Montant.ToString("N0");
                try
                {
                    AddColumns.Addcolumn(dataGridView1);
                    dataGridView1.Columns["Edit"].Visible = false;
                }
                catch (Exception ex)
                {

                }
                txtSousTotal.Text = result.Montant.ToString("N0");
                result = null;
            }
        }

        private Task<MyTable> FillAchatAsync() => Task.Factory.StartNew((() => FillAchat()));

        private MyTable FillAchat()
        {
            MyTable myTable = new MyTable();
            using (var financeDataContext = new QuitayeContext())
            {
                if (Returned)
                {
                    var source = (from d in financeDataContext.tbl_arrivée_temp
                                  where d.Auteur == LogIn.profile
                                  join st in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into pros
                                  from s in pros.DefaultIfEmpty()
                                  where d.Quantité < 0
                                  orderby d.Id descending
                                  select new
                                  {
                                      Id = d.Id,
                                      Marque = d.Nom,
                                      Catégorie = d.Catégorie,
                                      Taille = d.Taille,
                                      Quantité = d.Quantité,
                                      Mesure = d.Mesure,
                                      Montant = d.Prix,
                                      Type = s.Type
                                  }).ToList();
                    myTable.Montant = Convert.ToDecimal(source.Sum(x => x.Montant));
                    DataTable dataTable = new DataTable("tbl_achat");
                    try
                    {
                        dataTable.Columns.Add("Id");
                        dataTable.Columns.Add("Désignation");
                        dataTable.Columns.Add("Mesure");
                        dataTable.Columns.Add("Quantité");
                        dataTable.Columns.Add("Montant");
                        foreach (var data in source)
                        {
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            if (string.IsNullOrEmpty(data.Type))
                                row[1] = data.Marque + " " + data.Catégorie + " " + data.Taille;
                            else row[1] = data.Marque + " " + data.Catégorie + " " + data.Taille + "-" + data.Type;
                            row[2] = data.Mesure;
                            row[3] = data.Quantité;
                            row[4] = data.Montant;
                            dataTable.Rows.Add(row);
                        }

                        if (source.Count() > 0)
                        {
                            var dr = dataTable.NewRow();
                            dr[2] = "Total";
                            dr[3] = Convert.ToDecimal(source.Sum(x => x.Quantité)).ToString("N0");
                            dr[4] = Convert.ToDecimal(source.Sum(x => x.Montant)).ToString("N0");

                            dataTable.Rows.Add(dr);
                        }
                        myTable.Table = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MsgBox msgBox = new MsgBox();
                        msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        msgBox.ShowDialog();
                    }
                }
                else
                {
                    var source = (from d in financeDataContext.tbl_arrivée_temp
                                  where d.Auteur == LogIn.profile
                                  join st in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into pros
                                  from s in pros.DefaultIfEmpty()
                                  where d.Quantité > 0
                                  orderby d.Id descending
                                  select new
                                  {
                                      Id = d.Id,
                                      Marque = d.Nom,
                                      Catégorie = d.Catégorie,
                                      Taille = d.Taille,
                                      Quantité = d.Quantité,
                                      Mesure = d.Mesure,
                                      Montant = d.Prix,
                                      Type = s.Type
                                  }).ToList();
                    myTable.Montant = Convert.ToDecimal(source.Sum(x => x.Montant));
                    DataTable dataTable = new DataTable("tbl_achat");
                    try
                    {
                        dataTable.Columns.Add("Id");
                        dataTable.Columns.Add("Désignation");
                        dataTable.Columns.Add("Mesure");
                        dataTable.Columns.Add("Quantité");
                        dataTable.Columns.Add("Montant");
                        foreach (var data in source)
                        {
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            if (string.IsNullOrEmpty(data.Type))
                                row[1] = data.Marque + " " + data.Catégorie + " " + data.Taille;
                            else row[1] = data.Marque + " " + data.Catégorie + " " + data.Taille + "-" + data.Type;
                            row[2] = data.Mesure;
                            row[3] = data.Quantité;
                            row[4] = data.Montant;
                            dataTable.Rows.Add(row);
                        }

                        if (source.Count() > 0)
                        {
                            var dr = dataTable.NewRow();
                            dr[2] = "Total";
                            dr[3] = Convert.ToDecimal(source.Sum(x => x.Quantité)).ToString("N0");
                            dr[4] = Convert.ToDecimal(source.Sum(x => x.Montant)).ToString("N0");

                            dataTable.Rows.Add(dr);
                        }
                        myTable.Table = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MsgBox msgBox = new MsgBox();
                        msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        msgBox.ShowDialog();
                    }
                }
                                
                return myTable;
            }
        }

        public async Task CallSearchAcha(string search)
        {
            MyTable result = await FillSearchAchatAsync(search);
            if (result.Table.Rows.Count == 0)
            {
                dataGridView1.Columns.Clear();
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée disponible dans le tableau";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                dt = null;
                dr = null;
                result = null;
            }
            else
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = result.Table;
                first = false;
                dataGridView1.Columns[0].Visible = false;
                txtSousTotal.Text = result.Montant.ToString("N0");
                try
                {
                    AddColumns.Addcolumn(dataGridView1);
                    dataGridView1.Columns["Edit"].Visible = false;
                }
                catch (Exception ex)
                {
                }
                txtSousTotal.Text = result.Montant.ToString("N0");
                result = null;
            }
        }

        private Task<MyTable> FillSearchAchatAsync(string search) => Task.Factory.StartNew((() => FillSearchAchat(search)));

        private MyTable FillSearchAchat(string search)
        {
            MyTable myTable = new MyTable();
            using (var financeDataContext = new QuitayeContext())
            {
                if (Returned)
                {
                    var source = (from d in financeDataContext.tbl_arrivée_temp
                                  where d.Auteur == LogIn.profile
                                  join st in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into pros
                                  from s in pros.DefaultIfEmpty()
                                  orderby d.Id descending
                                  where d.Quantité < 0 && (d.Barcode == search || d.Nom.Contains(search) || d.Taille.Contains(search)
                                  || d.Catégorie.Contains(search) || s.Type.Contains(search))
                                  select new
                                  {
                                      Id = d.Id,
                                      Marque = d.Nom,
                                      Catégorie = d.Catégorie,
                                      Taille = d.Taille,
                                      Quantité = d.Quantité,
                                      Mesure = d.Mesure,
                                      Montant = d.Prix,
                                      Type = s.Type
                                  });

                    myTable.Montant = Convert.ToDecimal(source.Sum(x => x.Montant));
                    DataTable dataTable = new DataTable("tbl_achat");
                    try
                    {
                        dataTable.Columns.Add("Id");
                        dataTable.Columns.Add("Désignation");
                        dataTable.Columns.Add("Mesure");
                        dataTable.Columns.Add("Quantité");
                        dataTable.Columns.Add("Montant");
                        foreach (var data in source)
                        {
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            if (string.IsNullOrEmpty(data.Type))
                                row[1] = data.Marque + " " + data.Catégorie + " " + data.Taille;
                            else row[1] = data.Marque + " " + data.Catégorie + " " + data.Taille + "-" + data.Type;
                            row[2] = data.Mesure;
                            row[3] = data.Quantité;
                            row[4] = data.Montant;
                            dataTable.Rows.Add(row);
                        }

                        if(source.Count() > 0)
                        {
                            var dr = dataTable.NewRow();
                            dr[2] = "Total";
                            dr[3] = Convert.ToDecimal(source.Sum(x => x.Quantité)).ToString("N0");
                            dr[4] = Convert.ToDecimal(source.Sum(x => x.Montant)).ToString("N0");

                            dataTable.Rows.Add(dr);
                        }
                        
                        myTable.Table = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MsgBox msgBox = new MsgBox();
                        msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        msgBox.ShowDialog();
                    }
                }
                else
                {
                    var source = (from d in financeDataContext.tbl_arrivée_temp
                                  where d.Auteur == LogIn.profile
                                  join st in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into pros
                                  from s in pros.DefaultIfEmpty()
                                  orderby d.Id
                                  where d.Quantité > 0 && (d.Barcode == search || d.Nom.Contains(search) || d.Taille.Contains(search)
                                  || d.Catégorie.Contains(search) || s.Type.Contains(search))
                                  select new
                                  {
                                      Id = d.Id,
                                      Marque = d.Nom,
                                      Catégorie = d.Catégorie,
                                      Taille = d.Taille,
                                      Quantité = d.Quantité,
                                      Mesure = d.Mesure,
                                      Montant = d.Prix,
                                      Type = s.Type
                                  });

                    myTable.Montant = Convert.ToDecimal(source.Sum(x => x.Montant));
                    DataTable dataTable = new DataTable("tbl_achat");
                    try
                    {
                        dataTable.Columns.Add("Id");
                        dataTable.Columns.Add("Désignation");
                        dataTable.Columns.Add("Mesure");
                        dataTable.Columns.Add("Quantité");
                        dataTable.Columns.Add("Montant");
                        foreach (var data in source)
                        {
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            if (string.IsNullOrEmpty(data.Type))
                                row[1] = (data.Marque + " " + data.Catégorie + " " + data.Taille);
                            else row[1] = data.Marque + " " + data.Catégorie + " " + data.Taille + "-" + data.Type;
                            row[2] = data.Mesure;
                            row[3] = data.Quantité;
                            row[4] = data.Montant;
                            dataTable.Rows.Add(row);
                        }

                        if (source.Count() > 0)
                        {
                            var dr = dataTable.NewRow();
                            dr[2] = "Total";
                            dr[3] = Convert.ToDecimal(source.Sum(x => x.Quantité)).ToString("N0");
                            dr[4] = Convert.ToDecimal(source.Sum(x => x.Montant)).ToString("N0");

                            dataTable.Rows.Add(dr);
                        }
                        myTable.Table = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MsgBox msgBox = new MsgBox();
                        msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        msgBox.ShowDialog();
                    }
                }

                return myTable;
            }
        }


        public async Task CallFill(string pending = null)
        {
            MyTable result = await FillDGAsync(pending);
            if (result.Table.Rows.Count == 0)
            {
                dataGridView1.Columns.Clear();
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée disponible dans le tableau";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                dt = null;
                dr = null;
                result = null;
            }
            else
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = result.Table;
                first = false;
                dataGridView1.Columns[0].Visible = false;
                txtSousTotal.Text = result.Montant.ToString("N0");
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                try
                {
                    AddColumns.Addcolumn(dataGridView1);
                    dataGridView1.Columns["Edit"].Visible = false;
                }
                catch (Exception ex)
                {
                }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
                txtSousTotal.Text = result.Montant.ToString("N0");
                result = null;
            }
        }

        public async Task CallSearch(string search)
        {
            MyTable result = await FillDGSearchAsync(search);
            if (result.Table.Rows.Count == 0)
            {
                dataGridView1.Columns.Clear();
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée disponible dans le tableau";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                dt = null;
                dr = null;
                result = null;
            }
            else
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = result.Table;
                first = false;
                dataGridView1.Columns[0].Visible = false;
                txtSousTotal.Text = result.Montant.ToString("N0");
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                try
                {
                    AddColumns.Addcolumn(dataGridView1);
                    dataGridView1.Columns["Edit"].Visible = false;
                }
                catch (Exception ex)
                {
                }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
                txtSousTotal.Text = result.Montant.ToString("N0");
                result = null;
            }
        }

        private Task<MyTable> FillDGAsync(object pending = null) => Task.Factory.StartNew((() => Filldata(pending)));

        private MyTable Filldata(object pending = null)
        {
            MyTable myTable = new MyTable();
            using (var financeDataContext = new QuitayeContext())
            {
                if (Returned)
                {
                    var source =    (from d in financeDataContext.tbl_vente_temp
                                    where d.Auteur == LogIn.profile && d.Quantité < 0 && d.Pending == pending
                                    && DbFunctions.TruncateTime(d.Date_Vente) == DbFunctions.TruncateTime(DateTime.Today)
                                    join st in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into pros
                                    from s in pros.DefaultIfEmpty()
                                    orderby d.Id descending
                                    select new
                                    {
                                        Id = d.Id,
                                        Barcode = d.Barcode,
                                        Produit = d.Produit,
                                        Catégorie = d.Catégorie,
                                        Taille = d.Taille,
                                        Quantité = d.Quantité,
                                        Montant = d.Montant,
                                        Prix_Unité = d.Prix_Unité,
                                        Type = s.Type
                                    });
                    myTable.Montant = Convert.ToDecimal(source.Sum(x => x.Montant));
                    DataTable dataTable = new DataTable("tbl_vente");
                    try
                    {
                        dataTable.Columns.Add("Id");
                        dataTable.Columns.Add("Désignation");
                        dataTable.Columns.Add("Quantité");
                        dataTable.Columns.Add("Prix_Unité");
                        dataTable.Columns.Add("Montant");
                        foreach (var data in source)
                        {
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            if (!string.IsNullOrEmpty(data.Type))
                                row[1] = data.Produit + " " + data.Catégorie + " " + data.Taille + "-" + data.Type;
                            else row[1] = data.Produit + " " + data.Catégorie + " " + data.Taille;
                            row[2] = data.Quantité;
                            row[3] = data.Prix_Unité;
                            row[4] = data.Montant;
                            dataTable.Rows.Add(row);
                        }

                        if (source.Count() > 0)
                        {
                            var dr = dataTable.NewRow();
                            dr[1] = "Total";
                            dr[2] = Convert.ToDecimal(source.Sum(x => x.Quantité)).ToString("N0");
                            dr[3] = Convert.ToDecimal(source.Sum(x => x.Prix_Unité)).ToString("N0");
                            dr[4] = Convert.ToDecimal(source.Sum(x => x.Montant)).ToString("N0");

                            dataTable.Rows.Add(dr);
                        }
                        myTable.Table = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MsgBox msgBox = new MsgBox();
                 
                        msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        msgBox.ShowDialog();
                    }
                }
                else
                {
                    var source = (from d in financeDataContext.tbl_vente_temp
                                  join st in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into pros
                                  from s in pros.DefaultIfEmpty()
                                  where d.Auteur == LogIn.profile && d.Quantité > 0 && d.Pending == pending 
                                  && DbFunctions.TruncateTime(d.Date_Vente.Value) == DbFunctions.TruncateTime(DateTime.Today)
                                  orderby d.Id descending
                                  select new
                                  {
                                      Id = d.Id,
                                      Barcode = d.Barcode,
                                      Produit = d.Produit,
                                      Catégorie = d.Catégorie,
                                      Taille = d.Taille,
                                      Quantité = d.Quantité,
                                      Montant = d.Montant,
                                      Prix_Unité = d.Prix_Unité,
                                      Type = s.Type
                                  });
                    myTable.Montant = Convert.ToDecimal(source.Sum(x => x.Montant));
                    DataTable dataTable = new DataTable("tbl_vente");
                    try
                    {
                        dataTable.Columns.Add("Id");
                        dataTable.Columns.Add("Désignation");
                        dataTable.Columns.Add("Quantité");
                        dataTable.Columns.Add("Prix_Unité");
                        dataTable.Columns.Add("Montant");
                        foreach (var data in source)
                        {
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            if (!string.IsNullOrEmpty(data.Type))
                                row[1] = data.Produit + " " + data.Taille + " " + data.Catégorie + "-" + data.Type;
                            else row[1] = data.Produit + " " + data.Taille + " " + data.Catégorie;
                            row[2] = data.Quantité;
                            row[3] = data.Prix_Unité;
                            row[4] = data.Montant;
                            dataTable.Rows.Add(row);
                        }

                        if (source.Count() > 0)
                        {
                            var dr = dataTable.NewRow();
                            dr[1] = "Total";
                            dr[2] = Convert.ToDecimal(source.Sum(x => x.Quantité)).ToString("N0");
                            dr[3] = Convert.ToDecimal(source.Sum(x => x.Prix_Unité)).ToString("N0");
                            dr[4] = Convert.ToDecimal(source.Sum(x => x.Montant)).ToString("N0");

                            dataTable.Rows.Add(dr);
                        }
                        myTable.Table = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MsgBox msgBox = new MsgBox();
                        msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        msgBox.ShowDialog();
                    }
                }
                return myTable;
            }
        }

        private Task<MyTable> FillDGDamagedAsync(object pending = null) => Task.Factory.StartNew((() => FilldataDamaged(pending)));

        private MyTable FilldataDamaged(object pending = null)
        {
            MyTable myTable = new MyTable();
            using (var financeDataContext = new QuitayeContext())
            {
                if (Returned)
                {
                    var source = (from d in financeDataContext.tbl_damaged_expense_temp
                                  where d.Auteur == LogIn.profile && d.Quantité < 0 && d.Pending == pending
                                  && DbFunctions.TruncateTime(d.Date_Damaged) == DbFunctions.TruncateTime(DateTime.Today)
                                  join st in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into pros
                                  from s in pros.DefaultIfEmpty()
                                  orderby d.Id descending
                                  select new
                                  {
                                      Id = d.Id,
                                      Barcode = d.Barcode,
                                      Produit = d.Produit,
                                      Catégorie = d.Catégorie,
                                      Taille = d.Taille,
                                      Quantité = d.Quantité,
                                      Montant = d.Montant,
                                      Prix_Unité = d.Prix_Unité,
                                      Type = s.Type
                                  });
                    myTable.Montant = Convert.ToDecimal(source.Sum(x => x.Montant));
                    DataTable dataTable = new DataTable("tbl_vente");
                    try
                    {
                        dataTable.Columns.Add("Id");
                        dataTable.Columns.Add("Désignation");
                        dataTable.Columns.Add("Quantité");
                        dataTable.Columns.Add("Prix_Unité");
                        dataTable.Columns.Add("Montant");
                        foreach (var data in source)
                        {
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            if (!string.IsNullOrEmpty(data.Type))
                                row[1] = data.Produit + " " + data.Catégorie + " " + data.Taille + "-" + data.Type;
                            else row[1] = data.Produit + " " + data.Catégorie + " " + data.Taille;
                            row[2] = data.Quantité;
                            row[3] = data.Prix_Unité;
                            row[4] = data.Montant;
                            dataTable.Rows.Add(row);
                        }

                        if (source.Count() > 0)
                        {
                            var dr = dataTable.NewRow();
                            dr[1] = "Total";
                            dr[2] = Convert.ToDecimal(source.Sum(x => x.Quantité)).ToString("N0");
                            dr[3] = Convert.ToDecimal(source.Sum(x => x.Prix_Unité)).ToString("N0");
                            dr[4] = Convert.ToDecimal(source.Sum(x => x.Montant)).ToString("N0");

                            dataTable.Rows.Add(dr);
                        }
                        myTable.Table = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MsgBox msgBox = new MsgBox();

                        msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        msgBox.ShowDialog();
                    }
                }
                else
                {
                    var source = (from d in financeDataContext.tbl_damaged_expense_temp
                                  join st in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into pros
                                  from s in pros.DefaultIfEmpty()
                                  where d.Auteur == LogIn.profile && d.Quantité > 0 && d.Pending == pending
                                  && DbFunctions.TruncateTime(d.Date_Damaged.Value) == DbFunctions.TruncateTime(DateTime.Today)
                                  orderby d.Id descending
                                  select new
                                  {
                                      Id = d.Id,
                                      Barcode = d.Barcode,
                                      Produit = d.Produit,
                                      Catégorie = d.Catégorie,
                                      Taille = d.Taille,
                                      Quantité = d.Quantité,
                                      Montant = d.Montant,
                                      Prix_Unité = d.Prix_Unité,
                                      Type = s.Type
                                  });
                    myTable.Montant = Convert.ToDecimal(source.Sum(x => x.Montant));
                    DataTable dataTable = new DataTable("tbl_vente");
                    try
                    {
                        dataTable.Columns.Add("Id");
                        dataTable.Columns.Add("Désignation");
                        dataTable.Columns.Add("Quantité");
                        dataTable.Columns.Add("Prix_Unité");
                        dataTable.Columns.Add("Montant");
                        foreach (var data in source)
                        {
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            if (!string.IsNullOrEmpty(data.Type))
                                row[1] = data.Produit + " " + data.Taille + " " + data.Catégorie + "-" + data.Type;
                            else row[1] = data.Produit + " " + data.Taille + " " + data.Catégorie;
                            row[2] = data.Quantité;
                            row[3] = data.Prix_Unité;
                            row[4] = data.Montant;
                            dataTable.Rows.Add(row);
                        }

                        if (source.Count() > 0)
                        {
                            var dr = dataTable.NewRow();
                            dr[1] = "Total";
                            dr[2] = Convert.ToDecimal(source.Sum(x => x.Quantité)).ToString("N0");
                            dr[3] = Convert.ToDecimal(source.Sum(x => x.Prix_Unité)).ToString("N0");
                            dr[4] = Convert.ToDecimal(source.Sum(x => x.Montant)).ToString("N0");

                            dataTable.Rows.Add(dr);
                        }
                        myTable.Table = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MsgBox msgBox = new MsgBox();
                        msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        msgBox.ShowDialog();
                    }
                }
                return myTable;
            }
        }

        private Task<MyTable> FillDGSearchAsync(string search) => Task.Factory.StartNew((() => FillSearchdata(search)));

        private MyTable FillSearchdata(string search)
        {
            MyTable myTable = new MyTable();
            using (var financeDataContext = new QuitayeContext())
            {
                if (Returned)
                {
                    var source =    (from d in financeDataContext.tbl_vente_temp
                                    join st in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into pros
                                    from s in pros.DefaultIfEmpty()
                                    where d.Auteur == LogIn.profile
                                    && DbFunctions.TruncateTime(d.Date_Vente) == DbFunctions.TruncateTime(DateTime.Today)
                                    && d.Quantité < 0 && (d.Barcode == search || d.Produit.Contains(search) || d.Taille.Contains(search)
                                    || d.Catégorie.Contains(search) || d.Type_Base.Contains(search))
                                    select new
                                    {
                                        Id = d.Id,
                                        Barcode = d.Barcode,
                                        Produit = d.Produit,
                                        Catégorie = d.Catégorie,
                                        Taille = d.Taille,
                                        Quantité = d.Quantité,
                                        Montant = d.Montant,
                                        Prix_Unité = d.Prix_Unité,
                                        Type = s.Type
                                    });
                    myTable.Montant = Convert.ToDecimal(source.Sum(x => x.Montant));
                    DataTable dataTable = new DataTable("tbl_vente");
                    try
                    {
                        dataTable.Columns.Add("Id");
                        dataTable.Columns.Add("Désignation");
                        dataTable.Columns.Add("Quantité");
                        dataTable.Columns.Add("Prix_Unité");
                        dataTable.Columns.Add("Montant");
                        foreach (var data in source)
                        {
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            if (!string.IsNullOrEmpty(data.Type))
                                row[1] = data.Produit + " " + data.Catégorie + " " + data.Taille + "-" + data.Type;
                            else row[1] = data.Produit + " " + data.Catégorie + " " + data.Taille;
                            row[2] = data.Quantité;
                            row[3] = data.Prix_Unité;
                            row[4] = data.Montant;
                            dataTable.Rows.Add(row);
                        }

                        if (source.Count() > 0)
                        {
                            var dr = dataTable.NewRow();
                            dr[1] = "Total";
                            dr[2] = Convert.ToDecimal(source.Sum(x => x.Quantité)).ToString("N0");
                            dr[3] = Convert.ToDecimal(source.Sum(x => x.Prix_Unité)).ToString("N0");
                            dr[4] = Convert.ToDecimal(source.Sum(x => x.Montant)).ToString("N0");

                            dataTable.Rows.Add(dr);
                        }
                            
                        myTable.Table = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MsgBox msgBox = new MsgBox();
                        msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        msgBox.ShowDialog();
                    }
                }
                else
                {
                    var source = (from d in financeDataContext.tbl_vente_temp
                                  join st in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into pros
                                  from s in pros.DefaultIfEmpty()
                                  where d.Auteur == LogIn.profile && d.Quantité > 0 && 
                                  (d.Barcode == search || d.Produit.Contains(search) 
                                  || d.Taille.Contains(search)

                                  || d.Catégorie.Contains(search) || d.Type_Base.Contains(search)) && DbFunctions.TruncateTime(d.Date_Vente) == DbFunctions.TruncateTime(DateTime.Today)

                                  select new
                                  {
                                      Id = d.Id,
                                      Barcode = d.Barcode,
                                      Produit = d.Produit,
                                      Catégorie = d.Catégorie,
                                      Taille = d.Taille,
                                      Quantité = d.Quantité,
                                      Montant = d.Montant,
                                      Prix_Unité = d.Prix_Unité,
                                      Type = s.Type
                                  });
                    myTable.Montant = Convert.ToDecimal(source.Sum(x => x.Montant));
                    DataTable dataTable = new DataTable("tbl_vente");
                    try
                    {
                        dataTable.Columns.Add("Id");
                        dataTable.Columns.Add("Désignation");
                        dataTable.Columns.Add("Quantité");
                        dataTable.Columns.Add("Prix_Unité");
                        dataTable.Columns.Add("Montant");
                        foreach (var data in source)
                        {
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            if (!string.IsNullOrEmpty(data.Type))
                                row[1] = data.Produit + " " + data.Catégorie + " " + data.Taille + "-" + data.Type;
                            else row[1] = data.Produit + " " + data.Catégorie + " " + data.Taille;
                            row[2] = data.Quantité;
                            row[3] = data.Prix_Unité;
                            row[4] = data.Montant;
                            dataTable.Rows.Add(row);
                        }
                        if(source.Count() > 0)
                        {
                            var dr = dataTable.NewRow();
                            dr[1] = "Total";
                            dr[2] = Convert.ToDecimal(source.Sum(x => x.Quantité)).ToString("N0");
                            dr[3] = Convert.ToDecimal(source.Sum(x => x.Prix_Unité)).ToString("N0");
                            dr[4] = Convert.ToDecimal(source.Sum(x => x.Montant)).ToString("N0");

                            dataTable.Rows.Add(dr);
                        }
                        
                        myTable.Table = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MsgBox msgBox = new MsgBox();
                        msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        msgBox.ShowDialog();
                    }
                }
                
                return myTable;
            }
        }

        private Task<MyTable> FillDGSearchDamagedAsync(string search) => Task.Factory.StartNew((() => FillSearchdataDamaged(search)));

        private MyTable FillSearchdataDamaged(string search)
        {
            MyTable myTable = new MyTable();
            using (var financeDataContext = new QuitayeContext())
            {
                if (Returned)
                {
                    var source = (from d in financeDataContext.tbl_damaged_expense_temp
                                  join st in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into pros
                                  from s in pros.DefaultIfEmpty()
                                  where d.Auteur == LogIn.profile
                                  && DbFunctions.TruncateTime(d.Date_Damaged) == DbFunctions.TruncateTime(DateTime.Today)
                                  && d.Quantité < 0 && (d.Barcode == search || d.Produit.Contains(search) || d.Taille.Contains(search)
                                  || d.Catégorie.Contains(search) || d.Type_Base.Contains(search))
                                  select new
                                  {
                                      Id = d.Id,
                                      Barcode = d.Barcode,
                                      Produit = d.Produit,
                                      Catégorie = d.Catégorie,
                                      Taille = d.Taille,
                                      Quantité = d.Quantité,
                                      Montant = d.Montant,
                                      Prix_Unité = d.Prix_Unité,
                                      Type = s.Type
                                  });
                    myTable.Montant = Convert.ToDecimal(source.Sum(x => x.Montant));
                    DataTable dataTable = new DataTable("tbl_vente");
                    try
                    {
                        dataTable.Columns.Add("Id");
                        dataTable.Columns.Add("Désignation");
                        dataTable.Columns.Add("Quantité");
                        dataTable.Columns.Add("Prix_Unité");
                        dataTable.Columns.Add("Montant");
                        foreach (var data in source)
                        {
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            if (!string.IsNullOrEmpty(data.Type))
                                row[1] = data.Produit + " " + data.Catégorie + " " + data.Taille + "-" + data.Type;
                            else row[1] = data.Produit + " " + data.Catégorie + " " + data.Taille;
                            row[2] = data.Quantité;
                            row[3] = data.Prix_Unité;
                            row[4] = data.Montant;
                            dataTable.Rows.Add(row);
                        }

                        if (source.Count() > 0)
                        {
                            var dr = dataTable.NewRow();
                            dr[1] = "Total";
                            dr[2] = Convert.ToDecimal(source.Sum(x => x.Quantité)).ToString("N0");
                            dr[3] = Convert.ToDecimal(source.Sum(x => x.Prix_Unité)).ToString("N0");
                            dr[4] = Convert.ToDecimal(source.Sum(x => x.Montant)).ToString("N0");

                            dataTable.Rows.Add(dr);
                        }

                        myTable.Table = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MsgBox msgBox = new MsgBox();
                        msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        msgBox.ShowDialog();
                    }
                }
                else
                {
                    var source = (from d in financeDataContext.tbl_damaged_expense_temp
                                  join st in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into pros
                                  from s in pros.DefaultIfEmpty()
                                  where d.Auteur == LogIn.profile && d.Quantité > 0 &&
                                  (d.Barcode == search || d.Produit.Contains(search)
                                  || d.Taille.Contains(search)

                                  || d.Catégorie.Contains(search) || d.Type_Base.Contains(search)) && DbFunctions.TruncateTime(d.Date_Damaged) == DbFunctions.TruncateTime(DateTime.Today)

                                  select new
                                  {
                                      Id = d.Id,
                                      Barcode = d.Barcode,
                                      Produit = d.Produit,
                                      Catégorie = d.Catégorie,
                                      Taille = d.Taille,
                                      Quantité = d.Quantité,
                                      Montant = d.Montant,
                                      Prix_Unité = d.Prix_Unité,
                                      Type = s.Type
                                  });
                    myTable.Montant = Convert.ToDecimal(source.Sum(x => x.Montant));
                    DataTable dataTable = new DataTable("tbl_vente");
                    try
                    {
                        dataTable.Columns.Add("Id");
                        dataTable.Columns.Add("Désignation");
                        dataTable.Columns.Add("Quantité");
                        dataTable.Columns.Add("Prix_Unité");
                        dataTable.Columns.Add("Montant");
                        foreach (var data in source)
                        {
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            if (!string.IsNullOrEmpty(data.Type))
                                row[1] = data.Produit + " " + data.Catégorie + " " + data.Taille + "-" + data.Type;
                            else row[1] = data.Produit + " " + data.Catégorie + " " + data.Taille;
                            row[2] = data.Quantité;
                            row[3] = data.Prix_Unité;
                            row[4] = data.Montant;
                            dataTable.Rows.Add(row);
                        }
                        if (source.Count() > 0)
                        {
                            var dr = dataTable.NewRow();
                            dr[1] = "Total";
                            dr[2] = Convert.ToDecimal(source.Sum(x => x.Quantité)).ToString("N0");
                            dr[3] = Convert.ToDecimal(source.Sum(x => x.Prix_Unité)).ToString("N0");
                            dr[4] = Convert.ToDecimal(source.Sum(x => x.Montant)).ToString("N0");

                            dataTable.Rows.Add(dr);
                        }

                        myTable.Table = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MsgBox msgBox = new MsgBox();
                        msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        msgBox.ShowDialog();
                    }
                }

                return myTable;
            }
        }


        public Decimal RefreshSubTotal()
        {
            Decimal num = 0M;
            if (types == "Achat")
            {
                using (var financeDataContext = new QuitayeContext())
                {
                    var source = from s in financeDataContext.tbl_arrivée_temp where s.Auteur == LogIn.profile select new
                    {
                        Prix = s.Prix
                    };
                    num = num + Convert.ToDecimal(source.Sum(x => x.Prix));
                }
            }
            else
            {
                using (var financeDataContext = new QuitayeContext())
                {
                    var source = financeDataContext.tbl_vente_temp
                        .Where((s => s.Auteur == LogIn.profile)).Select(s => new
                    {
                        Montant = s.Montant
                    });
                    num = num + Convert.ToDecimal(source.Sum(x => x.Montant));
                }
            }
            return num;
        }

        public async Task ShowData() => await CallTaskSecond();

        private void ClearData()
        {
            cbxCode.Text = null;
            cbxCode.Text = null;
            txtMontant.Clear();
            txtStock.Clear();
        }

        private void Calcule()
        {
            double souttotal = double.Parse(txtSousTotal.Text);
            if (txtRéduction.Text != "")
            {
                double reduction = double.Parse(txtRéduction.Text);
                if (Returned)
                {
                    reduction = -reduction;
                }
                txtNetTotal.Text = (souttotal - reduction).ToString("N0");
            }
            else
                txtNetTotal.Text = double.Parse(txtSousTotal.Text).ToString("N0");
        }

        private Task<bool> ShouldPrintTicketAsync() => Task.Factory.StartNew(() => ShouldPrintTicket());
        private bool ShouldPrintTicket()
        {
            using(var donnée = new QuitayeContext())
            {
                var item = donnée.tbl_operation_default.Where(x => x.Nom == "Ticket" 
                && x.Default == "Oui").FirstOrDefault();
                if (item != null || PrintTicket)
                    return true;
                else return false;
            }
        }

        public bool PrintTicket { get; set; }
        private async void BtnEnregistrer_Click(object sender, EventArgs e)
        {
            if (cbxType.Text == "Vente")
            {
                await SaveAllSales();
                await ShowData();
                //Alert.SShow(cbxType.Text + "(s) enregistrée(s) avec succès.", Alert.AlertType.Sucess);
                ClearCalculations();
                ClearData();
                ok = "Oui";
            }
            else if(cbxType.Text == "Achat")
            {
                if (!(cbxType.Text == "Achat"))
                    return;
                await SaveAchat();
                ok = "Oui";
            }
            else if(cbxType.Text == "Expense Damaged")
            {
                await SaveAllDamaged();
                await ShowData();
                //Alert.SShow(cbxType.Text + "(s) enregistrée(s) avec succès.", Alert.AlertType.Sucess);
                ClearCalculations();
                ClearData();
                ok = "Oui";
            }

            cbxCode.Focus();
            if (string.IsNullOrEmpty(txtQuantité.Text))
                txtQuantité.Text = "1";
        }

        private void ClearCalculations()
        {
            txtSousTotal.Text = "0";
            txtRéduction.Text = "0";
            txtNetTotal.Text = "0";
            txtmontantpayer.Text = "0";
            txtAretourner.Text = "0";
            cbxClient.Text = null;
            txtNum.Clear();
        }

        private async void CbxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxType.Text == "Achat")
            {
                txtMontant.ReadOnly = false;
                CallCbxBon();
                cbxFournisseurs.Visible = true;
                tableLayoutPanel10.Visible = false;
                label31.Visible = true;
                Date_Expiration.Visible = true;
                txtRéduction.ReadOnly = true;
                txtNum.Visible = false;
                cbxBonCommande.Visible = true;
                lblNum.Visible = false;
                lblBC.Visible = true;
                lblfour.Visible = true;
                lblclient.Visible = false;
                btnFile.Visible = true;
                await ShowData();
                TxtQuantité_TextChanged(null, e);
            }
            else if(cbxType.Text =="Vente" || cbxType.Text == "Expense Damaged")
            {
                
                cbxFournisseurs.Visible = false;
                tableLayoutPanel10.Visible = true;
                txtNum.Visible = true;
                cbxBonCommande.Visible = false;
                lblNum.Visible = true;
                lblBC.Visible = false;
                lblfour.Visible = false;
                label31.Visible = false;
                Date_Expiration.Visible = false;
                lblclient.Visible = true;
                btnFile.Visible = false;
                await ShowData();
            }
        }

        private void TxtRéduction_TextChanged(object sender, EventArgs e) => Calcule();

        private async Task CallClient()
        {
            List<Partenaires> result = await ClientAsync();
            cbxClient.DataSource = result.OrderBy((x => x.Nom)).ToList();
            cbxClient.DisplayMember = "Nom";
            cbxClient.ValueMember = "Id";
            cbxClient.Text = null;
            result = null;
        }

        private Task<List<Partenaires>> ClientAsync() => Task.Factory.StartNew((() => Client()));

        private List<Partenaires> Client()
        {
            List<Partenaires> partenairesList = new List<Partenaires>();
            using (var financeDataContext = new QuitayeContext())
            {
                var queryable = financeDataContext.tbl_inscription.Where(x => x.Active == "Oui").Distinct().OrderBy(x => x.Nom).ThenBy(x => x.Prenom).Select(d => new
                {
                    Id = d.Id,
                    Prenom = d.Prenom,
                    Matricule = d.N_Matricule,
                    Nom = d.Nom, 
                    Classe = d.Classe,
                });
                int num = 1;
                foreach (var data in queryable)
                {
                    partenairesList.Add(new Partenaires()
                    {
                        Id = num,
                        Id_Partenaire = data.Id,
                        Matricule = data.Matricule,
                        Classe = data.Classe,
                        Nom = data.Prenom + " " + data.Nom + "("+data.Classe+")",
                        Type = nameof(Client)
                    });
                    ++num;
                }
                return partenairesList;
            }
        }

        private void Txtmontantpayer_TextChanged(object sender, EventArgs e)
        {
            if (txtmontantpayer.Text != "" && txtmontantpayer.Text != "-")
            {
                txtmontantpayer.Text = Convert.ToDecimal(txtmontantpayer.Text).ToString("N0");
                txtmontantpayer.SelectionStart = txtmontantpayer.Text.Length;
            }
            double num1 = 0;
            if (!string.IsNullOrEmpty(txtNetTotal.Text))
                num1 = double.Parse(txtNetTotal.Text);
            string s = Regex.Replace(txtmontantpayer.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
            if (!(s != ""))
                return;
            double num2 = double.Parse(s);
            if (num2 > num1)
                txtAretourner.Text = (num2 - num1).ToString("N0") + " FCFA";
            else
                txtAretourner.Text = "";
        }

        private void TxtSousTotal_TextChanged(object sender, EventArgs e) => Calcule();

        private void TxtRéduction_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsBarcodeScannerInput(e))
            {
                e.Handled = true;
                // Prevent the textbox from accepting the input
                cbxCode.Focus();
            }
            else
            {
                if (e.KeyChar == '\r')
                {
                    txtmontantpayer.Focus();
                    return;
                }
                if (char.IsDigit(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '.')
                    return;
                e.Handled = true;
            }
        }

        private Task<(DataTable, List<Simple_Cbx_Item>)> FillCbxProduitAsync() => Task.Factory.StartNew(() => FillCbxProduit());

        private (DataTable, List<Simple_Cbx_Item>) FillCbxProduit()
        {
            var list = new List<Simple_Cbx_Item>();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Nom");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_stock_produits_vente.Select(d => new
                            {
                                Marque = d.Marque,
                                Catégorie = d.Catégorie,
                                Taille = d.Taille,
                                Type = d.Type,
                                Id = d.Id,
                            }).Distinct().OrderBy(x => x.Marque)
                            .ThenBy(x => x.Taille);

                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    if(data.Type == null)
                    {
                        if(data.Taille == null)
                        {
                            row[1] = data.Marque + "-, " + data.Catégorie;
                            list.Add(new Simple_Cbx_Item() { Id = data.Id, Name = data.Marque + "-, " + data.Catégorie });
                        }
                        else 
                        { 
                            row[1] = data.Marque + "-" + data.Taille.ToString() + ", " + data.Catégorie;
                            list.Add(new Simple_Cbx_Item() { Id = data.Id, Name = data.Marque + "-" + data.Taille.ToString() + ", " + data.Catégorie });

                        }
                    }
                    else
                    {
                        if(data.Taille == null)
                        {
                            row[1] = data.Marque + "-, " + data.Catégorie + "_" + data.Type;
                            list.Add(new Simple_Cbx_Item() { Id = data.Id, Name = data.Marque + "-, " + data.Catégorie + "_" + data.Type });
                        }
                        else
                        {
                            row[1] = data.Marque + "-" + data.Taille.ToString() + ", " + data.Catégorie + "_" + data.Type;
                            list.Add(new Simple_Cbx_Item() { Id = data.Id, Name = data.Marque + "-" + data.Taille.ToString() + ", " + data.Catégorie + "_" + data.Type });
                        }
                    }
                    
                    dataTable.Rows.Add(row);
                }
            }
            return (dataTable, list);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        private async void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        {
        }

        private async void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
                return;
            e.Handled = true;
            if (cbxCode.Text != "" && cbxCode.Text.Length > 0 && product != null)
                await VenteProduit();
        }

        private async Task VenteProduit()
        {
            if (LogIn.type_compte.Contains("Administrateur") && cbxFiliale.Text != "")
            {
                if (string.IsNullOrEmpty(txtQuantité.Text))
                    txtQuantité.Text = "1";
                if (txtQuantité.Text != "")
                {
                    if (product != null && !string.IsNullOrEmpty(product.Code_Barre))
                    {
                        OpérationTemp vente = new OpérationTemp();
                        if (Returned)
                        {
                            vente.Quantité = -Convert.ToInt32(txtQuantité.Text);
                            vente.Montant = -Convert.ToDecimal(txtMontant.Text);

                        }
                        else
                        {
                            vente.Quantité = Convert.ToInt32(txtQuantité.Text);
                            vente.Montant = Convert.ToDecimal(txtMontant.Text);
                            if (product.Stock < (Decimal)Convert.ToInt32(txtQuantité.Text))
                            {
                                Alert.SShow("Désolé, Stock insuffisant", Alert.AlertType.Info);
                                return;
                            }
                        }

                        {
                            string montant = txtMontant.Text;
                            try
                            {
                                if (Convert.ToDecimal(montant) == 0)
                                {
                                    Alert.SShow("Prix non établis pour ce produit.", Alert.AlertType.Warning);
                                    return;
                                }
                            }
                            catch (Exception)
                            {
                                Alert.SShow("Prix non établis pour ce produit.", Alert.AlertType.Warning);
                                return;
                            }

                            
                            vente.Marque = product.Marque;
                            vente.Taille = product.Taille;
                            vente.Payement = !checkCrédit.Checked ? "Payée" : "A Crédit";
                            vente.Catégorie = product.Catégorie;
                            vente.Id_Client = Convert.ToInt32(cbxClient.SelectedValue);
                            //vente.Quantité = Convert.ToInt32(txtQuantité.Text);
                            if (product.Mesure == "Petit" && cbxModePrix.Text == "Grossiste")
                                vente.Prix_Unité = Convert.ToDecimal(product.Prix_Petit_Grossiste);
                            else if (product.Mesure == "Petit" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                                vente.Prix_Unité = Convert.ToDecimal(product.Prix_Petit);
                            if (product.Mesure == "Moyen" && cbxModePrix.Text == "Grossiste")
                                vente.Prix_Unité = Convert.ToDecimal(product.Prix_Moyen_Grossiste);
                            else if (product.Mesure == "Moyen" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                                vente.Prix_Unité = Convert.ToDecimal(product.Prix_Moyen);
                            if (product.Mesure == "Grand" && cbxModePrix.Text == "Grossiste")
                                vente.Prix_Unité = Convert.ToDecimal(product.Prix_Grand_Grossiste);
                            else if (product.Mesure == "Grand" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                                vente.Prix_Unité = Convert.ToDecimal(product.Prix_Grand);
                            if (product.Mesure == "Large" && cbxModePrix.Text == "Grossiste")
                                vente.Prix_Unité = Convert.ToDecimal(product.Prix_Large_Grossiste);
                            else if (product.Mesure == "Large" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                                vente.Prix_Unité = Convert.ToDecimal(product.Prix_Large);
                            if (product.Mesure == "Hyper Large" && cbxModePrix.Text == "Grossiste")
                                vente.Prix_Unité = Convert.ToDecimal(product.Prix_Hyper_Large_Grossiste);
                            else if (product.Mesure == "Hyper Large" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                                vente.Prix_Unité = Convert.ToDecimal(product.Prix_Hyper_Large);
                            vente.Prix_Unité = vente.Montant/ Convert.ToDecimal(txtQuantité.Text);
                            vente.Client = cbxClient.Text;
                            vente.Num_Client = txtNum.Text;
                            vente.Model = product.Type;
                            vente.Filiale = cbxFiliale.Text;
                            vente.Code_Barre = product.Code_Barre;
                            vente.Mesure = cbxMesure.Text;
                            //if (!await CheckIfArticleExistInSaleInvoiceAsync(vente))
                            {
                                if (await AddTempAsync(vente))
                                {
                                    product = (ProductObject)null;
                                    ClearTemp();
                                    await CallTaskSecond(Active_Pending);
                                    cbxCode.Focus();
                                }
                                vente = (OpérationTemp)null;
                            }
                            //else Alert.SShow("Cet article existe déjà dans la liste, Veillez-verifier", Alert.AlertType.Warning);
                        }
                        
                    }
                }
                else
                    Alert.SShow("Veillez entrez une quantité", Alert.AlertType.Info);
            }
            else
            {
                if (!(LogIn.type_compte == "Utilisateur"))
                    return;
                if (string.IsNullOrEmpty(txtQuantité.Text))
                    txtQuantité.Text = "1";
                if (txtQuantité.Text != "")
                {
                    if (product != null && !string.IsNullOrEmpty(product.Code_Barre))
                    {
                        if(!Returned)
                        {
                            if (product.Stock >= (Decimal)Convert.ToInt32(txtQuantité.Text))
                            {
                                string montant = txtMontant.Text;
                                try
                                {
                                    if (Convert.ToDecimal(montant) == 0)
                                    {
                                        Alert.SShow("Prix non établis pour ce produit.", Alert.AlertType.Warning);
                                        return;
                                    }
                                }
                                catch (Exception)
                                {
                                    Alert.SShow("Prix non établis pour ce produit.", Alert.AlertType.Warning);
                                    return;
                                }

                                OpérationTemp vente = new OpérationTemp();
                                vente.Marque = product.Marque;
                                vente.Taille = product.Taille;
                                vente.Payement = !checkCrédit.Checked ? "Payée" : "A Crédit";
                                vente.Catégorie = product.Catégorie;
                                vente.Id_Client = Convert.ToInt32(cbxClient.SelectedValue);
                                vente.Quantité = Convert.ToInt32(txtQuantité.Text);
                                if (product.Mesure == "Petit" && cbxModePrix.Text == "Grossiste")
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Petit_Grossiste);
                                else if (product.Mesure == "Petit" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Petit);
                                if (product.Mesure == "Moyen" && cbxModePrix.Text == "Grossiste")
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Moyen_Grossiste);
                                else if (product.Mesure == "Moyen" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Moyen);
                                if (product.Mesure == "Grand" && cbxModePrix.Text == "Grossiste")
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Grand_Grossiste);
                                else if (product.Mesure == "Grand" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Grand);
                                if (product.Mesure == "Large" && cbxModePrix.Text == "Grossiste")
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Large_Grossiste);
                                else if (product.Mesure == "Large" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Large);
                                if (product.Mesure == "Hyper Large" && cbxModePrix.Text == "Grossiste")
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Hyper_Large_Grossiste);
                                else if (product.Mesure == "Hyper Large" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Hyper_Large);
                                vente.Montant = Convert.ToDecimal(txtMontant.Text);
                                vente.Prix_Unité = (vente.Montant / Convert.ToDecimal(txtQuantité.Text));
                                vente.Client = cbxClient.Text;
                                vente.Num_Client = txtNum.Text;
                                vente.Model = product.Type;
                                vente.Filiale = LogIn.filiale;
                                vente.Code_Barre = product.Code_Barre;
                                vente.Mesure = cbxMesure.Text;
                                // if (!await CheckIfArticleExistInSaleInvoiceAsync(vente))
                                {
                                    if (await AddTempAsync(vente))
                                    {
                                        product = (ProductObject)null;
                                        ClearTemp();
                                        await CallTaskSecond(Active_Pending);
                                        cbxCode.Focus();
                                    }
                                    vente = (OpérationTemp)null;
                                }
                                //else Alert.SShow("Cet article existe déjà dans la liste, Veillez-verifier", Alert.AlertType.Warning);
                            }
                            else
                                Alert.SShow("Désolé, Stock insuffisant", Alert.AlertType.Info);
                        }else
                        {
                            //if (product.Stock >= (Decimal)Convert.ToInt32(txtQuantité.Text))
                            {
                                string montant = txtMontant.Text;
                                try
                                {
                                    if (Convert.ToDecimal(montant) == 0)
                                    {
                                        Alert.SShow("Prix non établis pour ce produit.", Alert.AlertType.Warning);
                                        return;
                                    }
                                }
                                catch (Exception)
                                {
                                    Alert.SShow("Prix non établis pour ce produit.", Alert.AlertType.Warning);
                                    return;
                                }

                                OpérationTemp vente = new OpérationTemp();
                                vente.Marque = product.Marque;
                                vente.Taille = product.Taille;
                                vente.Payement = !checkCrédit.Checked ? "Payée" : "A Crédit";
                                vente.Catégorie = product.Catégorie;
                                vente.Id_Client = Convert.ToInt32(cbxClient.SelectedValue);
                                vente.Quantité = -Convert.ToInt32(txtQuantité.Text);
                                if (product.Mesure == "Petit" && cbxModePrix.Text == "Grossiste")
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Petit_Grossiste);
                                else if (product.Mesure == "Petit" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Petit);
                                if (product.Mesure == "Moyen" && cbxModePrix.Text == "Grossiste")
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Moyen_Grossiste);
                                else if (product.Mesure == "Moyen" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Moyen);
                                if (product.Mesure == "Grand" && cbxModePrix.Text == "Grossiste")
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Grand_Grossiste);
                                else if (product.Mesure == "Grand" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Grand);
                                if (product.Mesure == "Large" && cbxModePrix.Text == "Grossiste")
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Large_Grossiste);
                                else if (product.Mesure == "Large" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Large);
                                if (product.Mesure == "Hyper Large" && cbxModePrix.Text == "Grossiste")
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Hyper_Large_Grossiste);
                                else if (product.Mesure == "Hyper Large" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                                    vente.Prix_Unité = Convert.ToDecimal(product.Prix_Hyper_Large);
                                vente.Montant = -Convert.ToDecimal(txtMontant.Text);
                                vente.Prix_Unité = vente.Montant / Convert.ToDecimal(txtQuantité.Text);
                                vente.Client = cbxClient.Text;
                                vente.Num_Client = txtNum.Text;
                                vente.Model = product.Type;
                                vente.Filiale = LogIn.filiale;
                                vente.Code_Barre = product.Code_Barre;
                                vente.Mesure = cbxMesure.Text;
                                // if (!await CheckIfArticleExistInSaleInvoiceAsync(vente))
                                {
                                    if (await AddTempAsync(vente))
                                    {
                                        product = (ProductObject)null;
                                        ClearTemp();
                                        await CallTaskSecond(Active_Pending);
                                        cbxCode.Focus();
                                    }
                                    vente = (OpérationTemp)null;
                                }
                                //else Alert.SShow("Cet article existe déjà dans la liste, Veillez-verifier", Alert.AlertType.Warning);
                            }
                            //else
                              //  Alert.SShow("Désolé, Stock insuffisant", Alert.AlertType.Info);
                        }
                        
                    }
                }
                else
                    Alert.SShow("Veillez entrez une quantité", Alert.AlertType.Info);
            }
            if (string.IsNullOrEmpty(txtQuantité.Text))
                txtQuantité.Text = "1";
        }

        private async Task DamagedExpense()
        {
            if (string.IsNullOrEmpty(txtQuantité.Text))
                txtQuantité.Text = "1";
            if (txtQuantité.Text != "")
            {
                if (product != null && !string.IsNullOrEmpty(product.Code_Barre))
                {
                    OpérationTemp vente = new OpérationTemp();
                    if (Returned)
                    {
                        vente.Quantité = -Convert.ToInt32(txtQuantité.Text);
                        vente.Montant = -Convert.ToDecimal(txtMontant.Text);
                    }
                    else
                    {
                        vente.Quantité = Convert.ToInt32(txtQuantité.Text);
                        vente.Montant = Convert.ToDecimal(txtMontant.Text);
                        if (product.Stock < (Decimal)Convert.ToInt32(txtQuantité.Text))
                        {
                            Alert.SShow("Désolé, Stock insuffisant", Alert.AlertType.Info);
                            return;
                        }
                    }

                    {
                        string montant = txtMontant.Text;
                        try
                        {
                            if (Convert.ToDecimal(montant) == 0)
                            {
                                Alert.SShow("Prix non établis pour ce produit.", Alert.AlertType.Warning);
                                return;
                            }
                        }
                        catch (Exception)
                        {
                            Alert.SShow("Prix non établis pour ce produit.", Alert.AlertType.Warning);
                            return;
                        }


                        vente.Marque = product.Marque;
                        vente.Taille = product.Taille;
                        vente.Payement = !checkCrédit.Checked ? "Payée" : "A Crédit";
                        vente.Catégorie = product.Catégorie;
                        vente.Id_Client = Convert.ToInt32(cbxClient.SelectedValue);
                        //vente.Quantité = Convert.ToInt32(txtQuantité.Text);
                        if (product.Mesure == "Petit" && cbxModePrix.Text == "Grossiste")
                            vente.Prix_Unité = Convert.ToDecimal(product.Prix_Petit_Grossiste);
                        else if (product.Mesure == "Petit" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                            vente.Prix_Unité = Convert.ToDecimal(product.Prix_Petit);
                        if (product.Mesure == "Moyen" && cbxModePrix.Text == "Grossiste")
                            vente.Prix_Unité = Convert.ToDecimal(product.Prix_Moyen_Grossiste);
                        else if (product.Mesure == "Moyen" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                            vente.Prix_Unité = Convert.ToDecimal(product.Prix_Moyen);
                        if (product.Mesure == "Grand" && cbxModePrix.Text == "Grossiste")
                            vente.Prix_Unité = Convert.ToDecimal(product.Prix_Grand_Grossiste);
                        else if (product.Mesure == "Grand" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                            vente.Prix_Unité = Convert.ToDecimal(product.Prix_Grand);
                        if (product.Mesure == "Large" && cbxModePrix.Text == "Grossiste")
                            vente.Prix_Unité = Convert.ToDecimal(product.Prix_Large_Grossiste);
                        else if (product.Mesure == "Large" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                            vente.Prix_Unité = Convert.ToDecimal(product.Prix_Large);
                        if (product.Mesure == "Hyper Large" && cbxModePrix.Text == "Grossiste")
                            vente.Prix_Unité = Convert.ToDecimal(product.Prix_Hyper_Large_Grossiste);
                        else if (product.Mesure == "Hyper Large" && (cbxModePrix.Text == "" || cbxModePrix.Text == "Detaillant"))
                            vente.Prix_Unité = Convert.ToDecimal(product.Prix_Hyper_Large);
                        vente.Prix_Unité = vente.Montant / Convert.ToDecimal(txtQuantité.Text);
                        vente.Client = cbxClient.Text;
                        vente.Num_Client = txtNum.Text;
                        vente.Model = product.Type;
                        vente.Filiale = cbxFiliale.Text;
                        vente.Code_Barre = product.Code_Barre;
                        vente.Mesure = cbxMesure.Text;
                        //if (!await CheckIfArticleExistInSaleInvoiceAsync(vente))
                        {
                            if (await AddTempDamagedAsync(vente))
                            {
                                product = (ProductObject)null;
                                ClearTemp();
                                await CallTaskSecond(Active_Pending);
                                cbxCode.Focus();
                            }
                            vente = (OpérationTemp)null;
                        }
                        //else Alert.SShow("Cet article existe déjà dans la liste, Veillez-verifier", Alert.AlertType.Warning);
                    }

                }
            }
            else
                Alert.SShow("Veillez entrez une quantité", Alert.AlertType.Info);
        }
        private void ClearTemp()
        {
            txtNom.Text = null;
            cbxCode.Text = null;
            txtQuantité.Text = "1";
            txtStock.Text = null;
        }

        private Task<List<Quitaye_School.Models.VenteList>> VenteListsAsync(object pending = null) => Task.Factory.StartNew(() => VenteList(pending));

        private List<Quitaye_School.Models.VenteList> VenteList(object pending = null)
        {
            List<Quitaye_School.Models.VenteList> venteListList = new List<Quitaye_School.Models.VenteList>();
            using (var financeDataContext = new QuitayeContext())
            {
                var result = new List<Models.VenteList>();
                if (Returned)
                {
                   result = (from d in financeDataContext.tbl_vente_temp
                     where d.Auteur == LogIn.profile
                    && DbFunctions.TruncateTime(d.Date_Vente) == DbFunctions.TruncateTime(DateTime.Today)
                    join pr in financeDataContext.tbl_produits on d.Barcode equals pr.Barcode into pro
                    from p in pro.DefaultIfEmpty()
                    join st in financeDataContext.tbl_stock_produits_vente on d.Barcode equals st.Code_Barre into pros
                    from s in pros.DefaultIfEmpty()
                    where d.Quantité < 0 && d.Pending == pending
                    orderby d.Id descending
                    select new Quitaye_School.Models.VenteList()
                    {
                        Id = d.Id,
                        Montant = d.Montant,
                        Prix_Unitaire = d.Prix_Unité,
                        Auteur = d.Auteur,
                        Client = d.Client,
                        Date = d.Date_Vente,
                        Quantité = d.Quantité,
                        Marque = d.Produit,
                        Catégorie = d.Catégorie,
                        Taille = d.Taille,
                        Mesure = d.Mesure,
                        Code_Barre = d.Barcode,
                        Num_Client = d.Num_Client,
                        Id_Client = d.Id_Client.ToString(),
                        Filiale = d.Filiale,
                        Prix_Petit = p.Prix_Petit,
                        Prix_Moyen = p.Prix_Moyen,
                        Prix_Grand = p.Prix_Grand,
                        Prix_Large = p.Prix_Large,
                        Prix_Hyper_Large = p.Prix_Hyper_Large,
                        Type = s.Type,
                        Product_Id = p.Id
                    }).ToList();
                }else
                {
                    result = (from d in financeDataContext.tbl_vente_temp
                    where d.Auteur == LogIn.profile 
                    && DbFunctions.TruncateTime(d.Date_Vente) == DbFunctions.TruncateTime(DateTime.Today)
                    join pr in financeDataContext.tbl_produits on d.Product_Id equals pr.Id into pro
                    from p in pro.DefaultIfEmpty()
                    join st in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into pros
                    from s in pros.DefaultIfEmpty()
                    where d.Quantité > 0 && d.Pending == pending
                    orderby d.Id descending
                    select new Quitaye_School.Models.VenteList()
                     {
                         Id = d.Id,
                         Montant = d.Montant,
                         Prix_Unitaire = d.Prix_Unité,
                         Auteur = d.Auteur,
                         Client = d.Client,
                         Date = d.Date_Vente,
                         Quantité = d.Quantité,
                         Marque = d.Produit,
                         Catégorie = d.Catégorie,
                         Taille = d.Taille,
                         Mesure = d.Mesure,
                         Code_Barre = d.Barcode,
                         Num_Client = d.Num_Client,
                         Id_Client = d.Id_Client.ToString(),
                         Filiale = d.Filiale,
                         Prix_Petit = p.Prix_Petit,
                         Prix_Moyen = p.Prix_Moyen,
                         Prix_Grand = p.Prix_Grand,
                         Prix_Large = p.Prix_Large,
                         Prix_Hyper_Large = p.Prix_Hyper_Large,
                         Type = s.Type,
                         Product_Id = p.Id
                     }).ToList();
                }
                return result;
            }
        }

        private Task<List<Quitaye_School.Models.VenteList>> DamagedListAsync(object pending = null) => Task.Factory.StartNew(() => DamagedList(pending));

        private List<Quitaye_School.Models.VenteList> DamagedList(object pending = null)
        {
            List<Quitaye_School.Models.VenteList> venteListList = new List<Quitaye_School.Models.VenteList>();
            using (var financeDataContext = new QuitayeContext())
            {
                var result = new List<Models.VenteList>();
                if (Returned)
                {
                    result = (from d in financeDataContext.tbl_damaged_expense_temp
                              where d.Auteur == LogIn.profile
                             && DbFunctions.TruncateTime(d.Date_Damaged) == DbFunctions.TruncateTime(DateTime.Today)
                              join pr in financeDataContext.tbl_produits on d.Barcode equals pr.Barcode into pro
                              from p in pro.DefaultIfEmpty()
                              join st in financeDataContext.tbl_stock_produits_vente on d.Barcode equals st.Code_Barre into pros
                              from s in pros.DefaultIfEmpty()
                              where d.Quantité < 0 && d.Pending == pending
                              orderby d.Id descending
                              select new Quitaye_School.Models.VenteList()
                              {
                                  Id = d.Id,
                                  Montant = d.Montant,
                                  Prix_Unitaire = d.Prix_Unité,
                                  Auteur = d.Auteur,
                                  Client = d.Client,
                                  Date = d.Date_Damaged,
                                  Quantité = d.Quantité,
                                  Marque = d.Produit,
                                  Catégorie = d.Catégorie,
                                  Taille = d.Taille,
                                  Mesure = d.Mesure,
                                  Code_Barre = d.Barcode,
                                  Num_Client = d.Num_Client,
                                  Id_Client = d.Id_Client.ToString(),
                                  Filiale = d.Filiale,
                                  Prix_Petit = p.Prix_Petit,
                                  Prix_Moyen = p.Prix_Moyen,
                                  Prix_Grand = p.Prix_Grand,
                                  Prix_Large = p.Prix_Large,
                                  Prix_Hyper_Large = p.Prix_Hyper_Large,
                                  Type = s.Type,
                                  Product_Id = p.Id
                              }).ToList();
                }
                else
                {
                    result = (from d in financeDataContext.tbl_damaged_expense_temp
                              where d.Auteur == LogIn.profile
                              && DbFunctions.TruncateTime(d.Date_Damaged) == DbFunctions.TruncateTime(DateTime.Today)
                              join pr in financeDataContext.tbl_produits on d.Product_Id equals pr.Id into pro
                              from p in pro.DefaultIfEmpty()
                              join st in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into pros
                              from s in pros.DefaultIfEmpty()
                              where d.Quantité > 0 && d.Pending == pending
                              orderby d.Id descending
                              select new Quitaye_School.Models.VenteList()
                              {
                                  Id = d.Id,
                                  Montant = d.Montant,
                                  Prix_Unitaire = d.Prix_Unité,
                                  Auteur = d.Auteur,
                                  Client = d.Client,
                                  Date = d.Date_Damaged,
                                  Quantité = d.Quantité,
                                  Marque = d.Produit,
                                  Catégorie = d.Catégorie,
                                  Taille = d.Taille,
                                  Mesure = d.Mesure,
                                  Code_Barre = d.Barcode,
                                  Num_Client = d.Num_Client,
                                  Id_Client = d.Id_Client.ToString(),
                                  Filiale = d.Filiale,
                                  Prix_Petit = p.Prix_Petit,
                                  Prix_Moyen = p.Prix_Moyen,
                                  Prix_Grand = p.Prix_Grand,
                                  Prix_Large = p.Prix_Large,
                                  Prix_Hyper_Large = p.Prix_Hyper_Large,
                                  Type = s.Type,
                                  Product_Id = p.Id
                              }).ToList();
                }
                return result;
            }
        }


        private async Task CallTaskSecond(object pending = null)
        {
            Task<MyTable> filldata = null;
            var ventelist = (Task<List<Quitaye_School.Models.VenteList>>)null;
            var pendings = FillPendingListAsync();
            if (cbxType.Text == "Achat")
            {
                filldata = FillAchatAsync();
                ventelist = ArrivageListAsync();
            }else if(cbxType.Text == "Vente")
            {
                //if (cbxType.Text == "Vente")
                {
                    filldata = FillDGAsync(pending);
                    ventelist = VenteListsAsync(pending);
                }
            }
            else if(cbxType.Text == "Expense Damaged")
            {
                //if (cbxType.Text == "Vente")
                {
                    filldata = FillDGDamagedAsync(pending);
                    ventelist = DamagedListAsync(pending);
                }
            }

            List<Task> taskList = new List<Task>()
            {
                filldata,
                ventelist,
                pendings
            };
            while (taskList.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(taskList);
                if (finishedTask == ventelist)
                    vente = ventelist.Result;
                else if (finishedTask == filldata)
                {
                    if (filldata.Result.Table.Rows.Count == 0)
                    {
                        dataGridView1.Columns.Clear();
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Tableau vide");
                        DataRow dr = dt.NewRow();
                        dr[0] = "Aucune donnée disponible dans le tableau";
                        dt.Rows.Add(dr);
                        dataGridView1.DataSource = dt;
                        txtSousTotal.Text = filldata.Result.Montant.ToString("N0");
                        Active_Pending = null;
                        dt = null;
                        dr = null;
                    }
                    else 
                    {
                        dataGridView1.Columns.Clear();
                        dataGridView1.DataSource = filldata.Result.Table;
                        first = false;
                        dataGridView1.Columns[0].Visible = false;
                        txtSousTotal.Text = filldata.Result.Montant.ToString("N0");
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                        try
                        {
                            AddColumns.Addcolumn(dataGridView1);
                            dataGridView1.Columns["Edit"].Visible = false;
                            dataGridView1.Columns["Id"].Width = 40;
                            dataGridView1.Columns["Désignation"].Width = 350;
                            dataGridView1.Columns["Quantité"].Width = 60;
                            dataGridView1.Columns["Sup"].Width = 40;
                            dataGridView1.Columns["Montant"].Width = 80;
                        }
                        catch (Exception ex)
                        {
                        }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
                        txtSousTotal.Text = filldata.Result.Montant.ToString("N0");
                    }
                }
                else if(finishedTask == pendings)
                {
                    FirstPendingLoad = true;
                    cbxPendingList.DataSource = pendings.Result;
                    cbxPendingList.DisplayMember = "Nom";
                    cbxPendingList.ValueMember = "Id";
                    cbxPendingList.Text = null;
                    FirstPendingLoad = false;
                }
                cbxCode.Focus();
                taskList.Remove(finishedTask);
                //finishedTask = null;
            }
            //filldata = null;
            //ventelist = (Task<List<Quitaye_School.Models.VenteList>>)null;
            //taskList = null;
        }

        public List<string> BarcodeList { get; set; }
        private async Task CallCode()
        {
            var result = await FillCodeAsync();
            BarcodeList = new List<string>();
            BarcodeList = result.Item2;
            cbxCode.DataSource = result.Item1;
            cbxCode.DisplayMember = "Code_Barre";
            cbxCode.ValueMember = "Id";
            cbxCode.Text = null;
            first = false;
            cbxCode.Visible = true;
            result = (null, null);
        }

        private Task<(DataTable, List<string>)> FillCodeAsync() => Task.Factory.StartNew(() => FillCode());

        private (DataTable, List<string>) FillCode()
        {
            var list = new List<string>();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Code_Barre");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = (from d in financeDataContext.tbl_multi_barcode 
                             where d.Barcode != null orderby d.Barcode select new
                {
                    Id = d.Id,
                    Code_Barre = d.Barcode
                }).Distinct().ToList();
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Code_Barre;
                    dataTable.Rows.Add(row);
                    list.Add(data.Code_Barre);
                }
            }
            return (dataTable, list);
        }

        private async Task<bool> Prix_Unité_Read_OnlyAsync()
        {
            using(var donnée = new QuitayeContext())
            {
                if (LogIn.type_compte.Contains("Administrateur"))
                    return false;

                var read = await donnée.tbl_operation_default
                    .Where(x => x.Nom == "Prix_Unité")
                    .FirstOrDefaultAsync();
                if (read == null)
                    return false;
                else
                {
                    if (read.Default == "Oui")
                    {
                        return true;
                    }
                    else return false;
                }
            }
        }

        private async Task CallTask()
        {
            Task<MyTable> filldata;
            var mesure = FillMesureAsync();
            Task<List<Models.VenteList>> ventelist;
            var four = FillCbxFournisseurAsync();
            var pendings = FillPendingListAsync();
            var prix_read_only = Prix_Unité_Read_OnlyAsync();
            var bon = FillCbxBonAsync();
            var defaul = FillDefaultAsync();
            var code = FillCodeAsync();
            var fillcbx = FillCbxProduitAsync();
            var mode = FillModeAsync();
            var filiale = FillFilialeAsync();
            var client = ClientAsync();
            var taskList = new List<Task>()
            {
                mesure,
                code,
                four,
                bon,
                fillcbx,
                defaul,
                mode,
                client,
                filiale,
                prix_read_only,
                pendings
            };
            if(cbxType.Text == "Vente")
            {
                filldata = FillDGAsync();
                taskList.Add(filldata);
                ventelist = VenteListsAsync();
                taskList.Add(ventelist);
            }else
            {
                ventelist = ArrivageListAsync();
                filldata = FillAchatAsync();
                taskList.Add(ventelist);
                taskList.Add(filldata);

            }
            while (taskList.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(taskList);
                if (finishedTask == ventelist)
                {
                    vente = ventelist.Result;
                    first = false;
                }
                else if(finishedTask == prix_read_only)
                {
                    txtPrixUnité.ReadOnly = prix_read_only.Result;
                }
                else if (finishedTask == filiale)
                {
                    cbxFiliale.DataSource = filiale.Result;
                    cbxFiliale.DisplayMember = "Nom";
                    cbxFiliale.ValueMember = "Id";
                    if (LogIn.filiale == "" || LogIn.filiale == null)
                        cbxFiliale.Text = null;
                    else
                        cbxFiliale.Text = LogIn.filiale;
                    if (LogIn.type_compte.Contains("Administrateur"))
                    {
                        cbxFiliale.Visible = true;
                        lblFiliale.Visible = true;
                        btnAddFiliale.Visible = true;
                    }
                }
                else if(finishedTask == pendings)
                {
                    FirstPendingLoad = true;
                    cbxPendingList.DataSource = pendings.Result;
                    cbxPendingList.DisplayMember = "Nom";
                    cbxPendingList.ValueMember = "Id";
                    cbxPendingList.Text = null;
                    FirstPendingLoad = false;
                }
                else if (finishedTask == fillcbx)
                {
                    //cbxSearch.DataSource = fillcbx.Result.Item2;
                    //cbxSearch.DisplayMember = "Name";
                    //cbxSearch.ValueMember = "Id";
                    //cbxSearch.Text = null;
                    cbxSearch.Items.Clear();
                    ProductList = fillcbx.Result.Item2;
                    foreach (var item in ProductList)
                    {
                        cbxSearch.Items.Add(item.Name);
                    }
                    cbxSearch.Visible = true;
                    FirstLoad = false;
                    first = false;
                }
                else if (finishedTask == client)
                {
                    cbxClient.DataSource = client.Result.OrderBy(x => x.Nom).ToList();
                    cbxClient.DisplayMember = "Nom";
                    cbxClient.ValueMember = "Id";
                    cbxClient.Text = null;
                    cbxClient.Text = default_client;
                    cbxClient.Visible = true;
                    var set_client = client.Result.Where(x => x.Matricule == Matricule).FirstOrDefault();
                    if(set_client != null)
                    cbxClient.Text = set_client.Nom;
                }
                else if (finishedTask == four)
                {
                    cbxFournisseurs.DataSource = four.Result;
                    cbxFournisseurs.DisplayMember = "Nom";
                    cbxFournisseurs.ValueMember = "Id";
                    cbxFournisseurs.Text = null;
                    if(product != null)
                    {
                        if(cbxType.Text == "Achat")
                        {
                            cbxFournisseurs.Text = default_client;
                        }
                    }
                }
                else if (finishedTask == mode)
                {
                    bool ps = await AddColAsync("tbl_mode_payement", "Defaut", "Nvarchar(50)");
                    cbxPayement.DataSource = mode.Result.Table;
                    cbxPayement.DisplayMember = "Mode";
                    cbxPayement.ValueMember = "Id";
                    first = false;
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
                else if (finishedTask == bon)
                {
                    cbxBonCommande.DataSource = bon.Result;
                    cbxBonCommande.DisplayMember = "Bon_Commande";
                    cbxBonCommande.ValueMember = "Id";
                    cbxBonCommande.Text = null;
                }
                else if (finishedTask == code)
                {
                    cbxCode.DataSource = code.Result.Item1;
                    cbxCode.DisplayMember = "Code_Barre";
                    cbxCode.ValueMember = "Id";
                    cbxCode.Text = null;
                    cbxCode.Visible = true;
                    BarcodeList = new List<string>();
                    BarcodeList = code.Result.Item2;
                }
                else if (finishedTask == filldata)
                {
                    if (filldata.Result.Table.Rows.Count == 0)
                    {
                        dataGridView1.Columns.Clear();
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Tableau vide");
                        DataRow dr = dt.NewRow();
                        dr[0] = "Aucune donnée disponible dans le tableau";
                        dt.Rows.Add(dr);
                        dataGridView1.DataSource = dt;
                        dt = null;
                        dr = null;
                    }
                    else
                    {
                        dataGridView1.Columns.Clear();
                        dataGridView1.DataSource = filldata.Result.Table;
                        first = false;
                        dataGridView1.Columns[0].Visible = false;
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                        try
                        {
                            AddColumns.Addcolumn(dataGridView1);
                            dataGridView1.Columns["Edit"].Visible = false;
                            dataGridView1.Columns["Id"].Width = 40;
                            dataGridView1.Columns["Désignation"].Width = 350;
                            dataGridView1.Columns["Quantité"].Width = 60;
                            dataGridView1.Columns["Sup"].Width = 40;
                            dataGridView1.Columns["Montant"].Width = 80;
                        }
                        catch (Exception ex)
                        {
                        }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
                        txtSousTotal.Text = filldata.Result.Montant.ToString("N0");
                    }
                    cbxCode.Focus();
                }
                else if (finishedTask == mesure)
                {
                    cbxMesure.DataSource = mesure.Result.Table;
                    cbxMesure.DisplayMember = "Mesure";
                    cbxMesure.ValueMember = "Id";
                    first = false;
                    if (mesure.Result.RowsLists != null && mesure.Result.RowsLists.Count > 0)
                    {
                        foreach (RowsList rowsList in mesure.Result.RowsLists)
                        {
                            RowsList item = rowsList;
                            if (item.Default)
                            {
                                cbxMesure.Text = item.Mesure;
                                break;
                            }
                            cbxMesure.Text = null;
                            item = (RowsList)null;
                        }
                    }
                    else
                        cbxMesure.Text = null;
                    cbxCode.Focus();
                }
                else if (finishedTask == defaul)
                {
                    cbxModePrix.DataSource = defaul.Result.Table;
                    cbxModePrix.DisplayMember = "Default";
                    cbxModePrix.ValueMember = "Id";
                    first = false;
                    if (defaul.Result.RowsLists != null && defaul.Result.RowsLists.Count > 0)
                    {
                        foreach (RowsList rowsList in defaul.Result.RowsLists)
                        {
                            RowsList item = rowsList;
                            if (item.Default)
                            {
                                cbxModePrix.Text = item.Mesure;
                                break;
                            }
                            cbxModePrix.Text = null;
                            item = (RowsList)null;
                        }
                    }
                    else
                        cbxModePrix.Text = null;
                    cbxCode.Focus();
                }
                taskList.Remove(finishedTask);
                //finishedTask = null;
            }
            filldata = null;
            mesure = (Task<MesureTable>)null;
            ventelist = (Task<List<Quitaye_School.Models.VenteList>>)null;
            four = (Task<DataTable>)null;
            bon = (Task<DataTable>)null;
            defaul = (Task<MesureTable>)null;
            code = (Task<(DataTable, List<string>)>)null;
            fillcbx = (Task<(DataTable, List<Simple_Cbx_Item>)>)null;
            mode = (Task<MesureTable>)null;
            filiale = (Task<DataTable>)null;
            client = (Task<List<Partenaires>>)null;
            pendings = null;
            taskList = null;
        }

        private Task<bool> AddColAsync(string table, string column, string type) => Task.Factory.StartNew<bool>((() => AddCol(table, column, type)));

        private bool AddCol(string table, string column, string type)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = LogIn.mycontrng;
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand("ALTER TABLE  [dbo].[" + table + "] ADD  " + column + " " + type + "  NULL ", connection);
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
            connection.Close();
            return true;
        }

        private async Task<bool> SavePayementAsync(Payement payement)
        {
            using (var financeDataContext = new QuitayeContext())
            {
                var source1 = financeDataContext.tbl_payement.Select(d => new
                {
                    Id = d.Id
                }).Take(1);
                int num1 = 1;
                if (source1.Count() != 0)
                {
                    var data = financeDataContext.tbl_payement.OrderByDescending((d => d.Id)).Select(d => new
                    {
                        Id = d.Id
                    }).First();
                    num1 = data.Id + 1;
                }
                var source2 = financeDataContext.tbl_payement.Where((d => d.Num_Opération == payement.Num_Opération)).Select(d => new
                {
                    Id = d.Id,
                    Montant = d.Montant
                });
                var entity = new Models.Context.tbl_payement()
                {
                    Id = num1,
                    Num_Opération = payement.Num_Opération,
                    Client = payement.Client,
                    Montant = new Decimal?(payement.Montant),
                    Num_Client = payement.Num_Client,
                    Reference = payement.Reference
                };
                entity.Num_Opération = payement.Num_Opération;
                entity.Type = payement.Type;
                entity.Date_Payement = new DateTime?(payement.Date);
                entity.Date_Enregistrement = new DateTime?(DateTime.Now);
                entity.Auteur = LogIn.profile;
                entity.Réduction = new Decimal?(payement.Reduction);
                entity.Raison = payement.Raison;
                entity.Mode_Payement = payement.Mode_Payement;
                entity.Compte_Tier = payement.Id_Client.ToString();
                entity.Raison = payement.Raison;
                entity.Nature = "Payement";
                if (string.IsNullOrEmpty(payement.Commentaire))
                    entity.Commentaire = "Payement " + payement.Client + " " + payement.Num_Opération;
                else entity.Commentaire = payement.Commentaire;
                financeDataContext.tbl_payement.Add(entity);
                await financeDataContext.SaveChangesAsync();
                Decimal num2 = Convert.ToDecimal(vente.Sum((x => x.Montant)) - payement.Reduction);
                Convert.ToDecimal(source2.Sum(x => x.Montant));
                if (payement.Type == "Encaissement")
                {
                    var queryable = financeDataContext.tbl_vente.Where((d => d.Num_Vente == payement.Num_Opération));
                    Decimal? montant = entity.Montant;
                    Decimal num3 = num2;
                    if (montant.GetValueOrDefault() >= num3 & montant.HasValue)
                    {
                        foreach (var tblVente in queryable)
                        {
                            var item = tblVente;
                            financeDataContext.tbl_vente.Where((d => d.Id == item.Id)).First().Type = "Payée";
                            await financeDataContext.SaveChangesAsync();
                        }
                    }
                }
                return true;
            }
        }

        private async Task CallMode()
        {
            MesureTable result = await FillModeAsync();
            cbxPayement.DataSource = result.Table;
            cbxPayement.DisplayMember = "Mode";
            cbxPayement.ValueMember = "Id";
            first = false;
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
                    item = (RowsList)null;
                }
                result = (MesureTable)null;
            }
            else
            {
                cbxPayement.Text = null;
                result = (MesureTable)null;
            }
        }

        public static Task<MesureTable> FillModeAsync() => Task.Factory.StartNew<MesureTable>((Func<MesureTable>)(() => FillMode()));

        private static MesureTable FillMode()
        {
            MesureTable mesureTable = new MesureTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Mode");
            using (var financeDataContext = new QuitayeContext())
            {
                if (LogIn.role == "Administrateur")
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

        private Task<MesureTable> FillMesureAsync() => Task.Factory.StartNew<MesureTable>(() => FillMesure());

        private MesureTable FillMesure()
        {
            MesureTable mesureTable = new MesureTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Mesure");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_mesure_vente.OrderBy((d => d.Nom)).Select(d => new
                {
                    Id = d.Niveau,
                    Nom = d.Nom,
                    Default = d.Default,
                    Type = d.Type
                }).ToList();
                if (source.Count() != 0)
                {
                    mesureTable.RowsLists = new List<RowsList>();
                    foreach (var data in source)
                    {
                        RowsList rowsList = new RowsList();
                        DataRow row = dataTable.NewRow();
                        row[0] = data.Id;
                        row[1] = data.Nom;
                        if (data.Default == "Oui")
                        {
                            rowsList.Default = true;
                            rowsList.Mesure = data.Nom;
                            rowsList.Type = data.Type;
                        }
                        else
                            rowsList.Default = false;
                        mesureTable.RowsLists.Add(rowsList);
                        dataTable.Rows.Add(row);
                    }
                }
                mesureTable.Table = dataTable;
            }
            return mesureTable;
        }

        private Task<MesureTable> FillDefaultAsync() => Task.Factory.StartNew((() => FillDefault()));

        private MesureTable FillDefault()
        {
            MesureTable mesureTable = new MesureTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Default");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_operation_default.Where(x => x.Nom.StartsWith("Gros") || x.Nom.StartsWith("Detai")).OrderBy(d => d.Nom).Select(d => new
                {
                    Id = d.Id,
                    Nom = d.Nom,
                    Default = d.Default
                });
                if (source.Count() != 0)
                {
                    mesureTable.RowsLists = new List<RowsList>();
                    foreach (var data in source)
                    {
                        RowsList rowsList = new RowsList();
                        DataRow row = dataTable.NewRow();
                        row[0] = data.Id;
                        row[1] = data.Nom;
                        if (data.Default == "Oui")
                        {
                            rowsList.Default = true;
                            rowsList.Mesure = data.Nom;
                        }
                        else
                            rowsList.Default = false;
                        mesureTable.RowsLists.Add(rowsList);
                        dataTable.Rows.Add(row);
                    }
                }
                mesureTable.Table = dataTable;
            }
            return mesureTable;
        }

        public Task<bool> CheckIfArticleExistInSaleInvoiceAsync(OpérationTemp opération) 
            => Task.Factory.StartNew(() => CheckIfArticleExistInSaleInvoice(opération));
        public bool CheckIfArticleExistInSaleInvoice(OpérationTemp opération)
        {
            using(var donnée = new QuitayeContext())
            {
                if (string.IsNullOrEmpty(num_vente))
                {
                    var data = (from d in donnée.tbl_vente_temp
                               where d.Barcode == opération.Code_Barre 
                               select d).ToList();
                    if (data.Count == 0)
                        return false;
                    else return true;
                }else
                {
                    var data = (from d in donnée.tbl_vente
                               where d.Barcode == opération.Code_Barre && d.Num_Vente == num_vente
                               select d).ToList();

                    var deser = (from d in donnée.tbl_vente_temp
                                 where d.Barcode == opération.Code_Barre
                                 select d).ToList();

                    
                    if (data.Count == 0 && deser.Count == 0)
                        return false;
                    else return true;
                }
               
            }
        }

        public Task<bool> CheckIfArticleExistInPurchaseInvoiceAsync(OpérationTemp opération)
            => Task.Factory.StartNew(() => CheckIfArticleExistInPurchaseInvoice(opération));
        public bool CheckIfArticleExistInPurchaseInvoice(OpérationTemp opération)
        {
            using (var donnée = new QuitayeContext())
            {
                if (Returned)
                {
                    if (string.IsNullOrEmpty(num_achat))
                    {
                        var data = (from d in donnée.tbl_arrivée_temp
                                    where d.Barcode == opération.Code_Barre && d.Quantité == opération.Quantité
                                    select d).ToList();
                        if (data.Count == 0)
                            return false;
                        else return true;
                    }
                    else
                    {
                        //var deser = (from d in donnée.tbl_arrivée_temp
                        //             where d.Barcode == opération.Code_Barre && d.Quantité == opération.Quantité
                        //             select d).ToList();

                        //var data = (from d in donnée.tbl_arrivée
                        //            where d.Barcode == opération.Code_Barre 
                        //            && d.Num_Achat == num_achat && d.Quantité == opération.Quantité
                        //            select d).ToList();
                        //if (data.Count == 0 && deser.Count == 0)
                            return false;
                        //else return true;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(num_achat))
                    {
                        var data = (from d in donnée.tbl_arrivée_temp
                                    where d.Barcode == opération.Code_Barre
                                    select d).ToList();
                        if (data.Count == 0)
                            return false;
                        else return true;
                    }
                    else
                    {
                        var deser = (from d in donnée.tbl_arrivée_temp
                                     where d.Barcode == opération.Code_Barre
                                     select d).ToList();

                        var data = (from d in donnée.tbl_arrivée
                                    where d.Barcode == opération.Code_Barre && d.Num_Achat == num_achat
                                    select d).ToList();
                        if (data.Count == 0 && deser.Count == 0)
                            return false;
                        else return true;
                    }
                }
                
            }
        }

        private async Task<bool> AddTempAsync(OpérationTemp vente)
        {
            bool flag = false;
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_vente_temp.Select(d => new
                {
                    Id = d.Id
                }).Take(1);
                int num = 1;
                var prod_id = financeDataContext.tbl_multi_barcode.Where(x => x.Barcode == vente.Code_Barre).Select(x => x.Product_Id).FirstOrDefault();
                if (source.Count() != 0)
                {
                    var data = financeDataContext.tbl_vente_temp.OrderByDescending(d => d.Id).Select(d => new
                    {
                        Id = d.Id
                    }).First();
                    num = data.Id + 1;
                }
                var entity = new Models.Context.tbl_vente_temp();
                entity.Montant = new Decimal?(vente.Montant);
                entity.Product_Id = prod_id;
                entity.Id = num;
                entity.Client = vente.Client;
                entity.Num_Client = vente.Num_Client;
                entity.Prix_Unité = new Decimal?(vente.Prix_Unité);
                entity.Quantité = new Decimal?((Decimal)vente.Quantité);
                entity.Produit = vente.Marque;
                entity.Id_Client = new int?(vente.Id_Client);
                entity.Catégorie = vente.Catégorie;
                entity.Taille = vente.Taille;
                entity.Mesure = vente.Mesure;
                if (LogIn.role == "Utilisateur" && LogIn.filiale != null && LogIn.filiale != "")
                    entity.Filiale = LogIn.filiale;
                else if (vente.Filiale != null && vente.Filiale != "")
                    entity.Filiale = vente.Filiale;
                entity.Auteur = LogIn.profile;
                entity.Date_Vente = new DateTime?(DateTime.Now);
                entity.Barcode = vente.Code_Barre;
                entity.Pending = Active_Pending;
                entity.Product_Id = prod_id;
                financeDataContext.tbl_vente_temp.Add(entity);
                await financeDataContext.SaveChangesAsync();
                flag = true;
            }
            return flag;
        }

        private async Task<bool> AddTempDamagedAsync(OpérationTemp vente)
        {
            bool flag = false;
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_damaged_expense_temp.Select(d => new
                {
                    Id = d.Id
                }).Take(1);
                int num = 1;
                var prod_id = financeDataContext.tbl_multi_barcode.Where(x => x.Barcode == vente.Code_Barre).Select(x => x.Product_Id).FirstOrDefault();
                if (source.Count() != 0)
                {
                    var data = financeDataContext.tbl_damaged_expense_temp.OrderByDescending(d => d.Id).Select(d => new
                    {
                        Id = d.Id
                    }).First();
                    num = data.Id + 1;
                }
                var entity = new Models.Context.tbl_damaged_expense_temp();
                entity.Montant = new Decimal?(vente.Montant);
                entity.Product_Id = prod_id;
                entity.Id = num;
                entity.Client = vente.Client;
                entity.Num_Client = vente.Num_Client;
                entity.Prix_Unité = new Decimal?(vente.Prix_Unité);
                entity.Quantité = new Decimal?((Decimal)vente.Quantité);
                entity.Produit = vente.Marque;
                entity.Id_Client = new int?(vente.Id_Client);
                entity.Catégorie = vente.Catégorie;
                entity.Taille = vente.Taille;
                entity.Mesure = vente.Mesure;
                if (LogIn.role == "Utilisateur" && LogIn.filiale != null && LogIn.filiale != "")
                    entity.Filiale = LogIn.filiale;
                else if (vente.Filiale != null && vente.Filiale != "")
                    entity.Filiale = vente.Filiale;
                entity.Auteur = LogIn.profile;
                entity.Date_Damaged = new DateTime?(DateTime.Now);
                entity.Barcode = vente.Code_Barre;
                entity.Pending = Active_Pending;
                entity.Product_Id = prod_id;
                financeDataContext.tbl_damaged_expense_temp.Add(entity);
                await financeDataContext.SaveChangesAsync();
                flag = true;
            }
            return flag;
        }

        private async Task SaveAllSales()
        {
            try
            {
                if (checkCrédit.Checked)
                    check = true;
                else if (!checkCrédit.Checked)
                    check = false;
                List<bool> listsaved = new List<bool>();
                if (num_vente == "" || num_vente == null)
                {
                    num_vente = await Numero_Cmd();
                    var ticket = await ShouldPrintTicketAsync();
                    if (ticket)
                    {
                        Models.Info_Entreprise entreprise;
                        List<Models.VenteList> list;

                        PrintReceip(out entreprise, out list);
                        entreprise = null;
                        PrintTicket = false;
                        list = null;
                    }
                    Decimal mont = Convert.ToDecimal(vente.Sum((x => x.Montant)));
                    Decimal pay = 0M;
                    if (!string.IsNullOrWhiteSpace(txtmontantpayer.Text))
                        pay = Convert.ToDecimal(txtmontantpayer.Text);
                    if (mont >= pay && !check)
                        restant = true;
                    List<bool> boolList = new List<bool>();
                    if (!string.IsNullOrEmpty(txtRéduction.Text))
                    {
                        if(Returned)
                        {
                            string reduct = Regex.Replace(txtRéduction.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                            if (!string.IsNullOrEmpty(reduct) && Convert.ToDecimal(reduct) > 0M)
                            {
                                vente.FirstOrDefault().Reduction = -Convert.ToDecimal(reduct);
                            }
                        }else
                        {
                            string reduct = Regex.Replace(txtRéduction.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                            if (!string.IsNullOrEmpty(reduct) && Convert.ToDecimal(reduct) > 0M)
                            {
                                vente.FirstOrDefault().Reduction = Convert.ToDecimal(reduct);
                            }
                        }
                        
                    }
                    
                    if(cbxType.Text  == "Vente")
                    {
                        if (!string.IsNullOrEmpty(cbxClient.Text))
                        {
                            foreach (var item in vente)
                            {
                                item.Client = cbxClient.Text;
                                item.Id_Client = cbxClient.SelectedValue.ToString();
                            }
                        }
                    }else if(cbxType.Text =="Achat")
                    {
                        if (!string.IsNullOrEmpty(cbxFournisseurs.Text))
                        {
                            foreach (var item in vente)
                            {
                                item.Client = cbxFournisseurs.Text;
                                item.Id_Client = cbxFournisseurs.SelectedValue.ToString();
                            }
                        }
                    }else if(cbxType.Text =="Damaged Expense")
                    {

                    }
                    
                    foreach (Quitaye_School.Models.VenteList item in vente)
                    {
                        bool result = await SaveSingleAsync(item);
                        boolList.Add(result);
                    }
                    bool failed = false;
                    foreach (bool item in boolList)
                    {
                        if (!item)
                            failed = true;
                    }
                    if (failed)
                    {
                        Alert.SShow("Certains opération n'ont pas pu être enregistré. Stock Insuffisant", Alert.AlertType.Info);
                    }
                    else
                    {
                        var ses = vente.Select(d => new
                        {
                            Id_Client = d.Id_Client
                        });
                        string reduct = Regex.Replace(txtRéduction.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                        string montant = Regex.Replace(txtmontantpayer.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                        if ((montant != "" && cbxPayement.Text != "") || (!checkCrédit.Checked))
                        {
                            var new_montant = montant;
                            if (string.IsNullOrEmpty(new_montant))
                                new_montant = vente.Sum(x => x.Montant).ToString();
                            Payement payement = new Payement();
                            if (Convert.ToDecimal(new_montant) > vente.Sum((x => x.Montant)))
                                montant = Convert.ToDecimal(vente.Sum((x => x.Montant))).ToString();
                            if (Returned)
                            {
                                if (!string.IsNullOrEmpty(reduct) && Convert.ToDecimal(reduct) > 0M)
                                {
                                    var red = Convert.ToDecimal(reduct);
                                    red = -red;
                                    new_montant = (Convert.ToDecimal(new_montant) - Convert.ToDecimal(red)).ToString();
                                    payement.Reduction = Convert.ToDecimal(red);
                                }
                            }else
                            {
                                if (!string.IsNullOrEmpty(reduct) && Convert.ToDecimal(reduct) > 0M)
                                {
                                    new_montant = (Convert.ToDecimal(new_montant) - Convert.ToDecimal(reduct)).ToString();
                                    payement.Reduction = Convert.ToDecimal(reduct);
                                }
                            }
                           
                            payement.Montant = Convert.ToDecimal(new_montant);
                            payement.Client = vente.First().Client;
                            payement.Date = DateTime.Today;
                            payement.Reference = txtReference.Text;
                            payement.Num_Opération = num_vente;
                            payement.Mode_Payement = cbxPayement.Text;
                            payement.Type = "Encaissement";
                            payement.Raison = "Client";
                            if (ses.Count() != 0)
                                payement.Id_Client = Convert.ToInt32(ses.First().Id_Client);
                            if (await SavePayementAsync(payement))
                            {
                                txtmontantpayer.Text = null;
                                txtReference.Text = null;
                                txtRéduction.Text = null;
                            }
                            payement = (Payement)null;
                        }
                        Alert.SShow("Vente enregistré avec succès.", Alert.AlertType.Sucess);
                        num_vente = null;
                        ses = null;
                        montant = null;
                        reduct = null;
                    }
                }
                else
                {
                    var ticket = await ShouldPrintTicketAsync();
                    if (ticket)
                    {
                        Models.Info_Entreprise entreprise;
                        List<Models.VenteList> list;

                        PrintReceip(out entreprise, out list);
                        entreprise = null;
                        list = null;
                        PrintTicket = false;
                    }
                    List<bool> boolList = new List<bool>();
                    string montant = Regex.Replace(txtmontantpayer.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                    string reduct = Regex.Replace(txtRéduction.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                    if (Returned)
                    {
                        var red = Convert.ToDecimal(reduct);
                        red = -red;
                        if (!string.IsNullOrEmpty(reduct) && Convert.ToDecimal(reduct) > 0M)
                        {
                            montant = (Convert.ToDecimal(montant) - red).ToString();
                            vente.FirstOrDefault().Reduction = Convert.ToDecimal(red);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(reduct) && Convert.ToDecimal(reduct) > 0M)
                        {
                            montant = (Convert.ToDecimal(montant) - Convert.ToDecimal(reduct)).ToString();
                            vente.FirstOrDefault().Reduction = Convert.ToDecimal(reduct);
                        }
                    }
                    
                    foreach (Quitaye_School.Models.VenteList item in vente)
                    {
                        bool result = await SaveSingleAsync(item);
                        boolList.Add(result);
                    }
                    bool failed = false;
                    foreach (bool item in boolList)
                    {
                        if (!item)
                            failed = true;
                    }
                    if (failed)
                        Alert.SShow("Certains opération n'ont pas pu être enregistré. Stock Insuffisant", Alert.AlertType.Info);
                    else
                        Alert.SShow("Vente enregistré avec succès.", Alert.AlertType.Sucess);
                    num_vente = null;
                    Close();
                }
                //Alert.SShow("Vente enregistré avec succès.", Alert.AlertType.Sucess);
                listsaved = (List<bool>)null;
            }
            catch (Exception ex)
            {
                MsgBox msg = new MsgBox();
                msg.show(ex.Message, "Error:", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Question);
                msg.ShowDialog();
            }
        }

        private async Task SaveAllDamaged()
        {
            try
            {
                if (checkCrédit.Checked)
                    check = true;
                else if (!checkCrédit.Checked)
                    check = false;
                List<bool> listsaved = new List<bool>();
                if (num_damaged == "" || num_damaged == null)
                {
                    num_damaged = await Numero_Damaged();
                    var ticket = await ShouldPrintTicketAsync();
                    if (ticket)
                    {
                        Models.Info_Entreprise entreprise;
                        List<Models.VenteList> list;

                        PrintReceip(out entreprise, out list);
                        entreprise = null;
                        PrintTicket = false;
                        list = null;
                    }
                    Decimal mont = Convert.ToDecimal(vente.Sum((x => x.Montant)));
                    Decimal pay = 0M;
                    if (!string.IsNullOrWhiteSpace(txtmontantpayer.Text))
                        pay = Convert.ToDecimal(txtmontantpayer.Text);
                    if (mont >= pay && !check)
                        restant = true;
                    List<bool> boolList = new List<bool>();
                    if (!string.IsNullOrEmpty(txtRéduction.Text))
                    {
                        if (Returned)
                        {
                            string reduct = Regex.Replace(txtRéduction.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                            if (!string.IsNullOrEmpty(reduct) && Convert.ToDecimal(reduct) > 0M)
                            {
                                vente.FirstOrDefault().Reduction = -Convert.ToDecimal(reduct);
                            }
                        }
                        else
                        {
                            string reduct = Regex.Replace(txtRéduction.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                            if (!string.IsNullOrEmpty(reduct) && Convert.ToDecimal(reduct) > 0M)
                            {
                                vente.FirstOrDefault().Reduction = Convert.ToDecimal(reduct);
                            }
                        }
                    }

                    if (cbxType.Text == "Vente")
                    {
                        if (!string.IsNullOrEmpty(cbxClient.Text))
                        {
                            foreach (var item in vente)
                            {
                                item.Client = cbxClient.Text;
                                item.Id_Client = cbxClient.SelectedValue.ToString();
                            }
                        }
                    }
                    else if (cbxType.Text == "Achat")
                    {
                        if (!string.IsNullOrEmpty(cbxFournisseurs.Text))
                        {
                            foreach (var item in vente)
                            {
                                item.Client = cbxFournisseurs.Text;
                                item.Id_Client = cbxFournisseurs.SelectedValue.ToString();
                            }
                        }
                    }
                    else if (cbxType.Text == "Damaged Expense")
                    {

                    }

                    foreach (Quitaye_School.Models.VenteList item in vente)
                    {
                        bool result = await SaveSingleDamagedAsync(item);
                        boolList.Add(result);
                    }
                    bool failed = false;
                    foreach (bool item in boolList)
                    {
                        if (!item)
                            failed = true;
                    }
                    if (failed)
                    {
                        Alert.SShow("Certains opération n'ont pas pu être enregistré. Stock Insuffisant", Alert.AlertType.Info);
                    }
                    else
                    {
                        var ses = vente.Select(d => new
                        {
                            Id_Client = d.Id_Client
                        });
                        string reduct = Regex.Replace(txtRéduction.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                        string montant = Regex.Replace(txtmontantpayer.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                        if ((montant != "" && cbxPayement.Text != "") || (!checkCrédit.Checked))
                        {
                            var new_montant = montant;
                            if (string.IsNullOrEmpty(new_montant))
                                new_montant = vente.Sum(x => x.Montant).ToString();
                            Payement payement = new Payement();
                            if (Convert.ToDecimal(new_montant) > vente.Sum((x => x.Montant)))
                                montant = Convert.ToDecimal(vente.Sum((x => x.Montant))).ToString();
                            if (Returned)
                            {
                                if (!string.IsNullOrEmpty(reduct) && Convert.ToDecimal(reduct) > 0M)
                                {
                                    var red = Convert.ToDecimal(reduct);
                                    red = -red;
                                    new_montant = (Convert.ToDecimal(new_montant) - Convert.ToDecimal(red)).ToString();
                                    payement.Reduction = Convert.ToDecimal(red);
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(reduct) && Convert.ToDecimal(reduct) > 0M)
                                {
                                    new_montant = (Convert.ToDecimal(new_montant) - Convert.ToDecimal(reduct)).ToString();
                                    payement.Reduction = Convert.ToDecimal(reduct);
                                }
                            }

                            payement.Montant = Convert.ToDecimal(new_montant);
                            payement.Client = vente.First().Client;
                            payement.Date = DateTime.Today;
                            payement.Reference = txtReference.Text;
                            payement.Num_Opération = num_damaged;
                            
                            payement.Mode_Payement = cbxPayement.Text;
                            payement.Type = "Décaissement";
                            payement.Raison = "Client";
                            payement.Commentaire = cbxType.Text;
                            if (ses.Count() != 0)
                                payement.Id_Client = Convert.ToInt32(ses.First().Id_Client);
                            if (await SavePayementAsync(payement))
                            {
                                txtmontantpayer.Text = null;
                                txtReference.Text = null;
                                txtRéduction.Text = null;
                            }
                            payement = (Payement)null;
                        }
                        //Alert.SShow("Vente enregistré avec succès.", Alert.AlertType.Sucess);
                        num_damaged = null;
                        ses = null;
                        montant = null;
                        reduct = null;
                    }
                }
                else
                {
                    var ticket = await ShouldPrintTicketAsync();
                    if (ticket)
                    {
                        Models.Info_Entreprise entreprise;
                        List<Models.VenteList> list;

                        PrintReceip(out entreprise, out list);
                        entreprise = null;
                        list = null;
                        PrintTicket = false;
                    }
                    List<bool> boolList = new List<bool>();
                    string montant = Regex.Replace(txtmontantpayer.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                    string reduct = Regex.Replace(txtRéduction.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                    if (Returned)
                    {
                        var red = Convert.ToDecimal(reduct);
                        red = -red;
                        if (!string.IsNullOrEmpty(reduct) && Convert.ToDecimal(reduct) > 0M)
                        {
                            montant = (Convert.ToDecimal(montant) - red).ToString();
                            vente.FirstOrDefault().Reduction = Convert.ToDecimal(red);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(reduct) && Convert.ToDecimal(reduct) > 0M)
                        {
                            montant = (Convert.ToDecimal(montant) - Convert.ToDecimal(reduct)).ToString();
                            vente.FirstOrDefault().Reduction = Convert.ToDecimal(reduct);
                        }
                    }

                    foreach (Quitaye_School.Models.VenteList item in vente)
                    {
                        bool result = await SaveSingleAsync(item);
                        boolList.Add(result);
                    }
                    bool failed = false;
                    foreach (bool item in boolList)
                    {
                        if (!item)
                            failed = true;
                    }
                    if (failed)
                        Alert.SShow("Certains opération n'ont pas pu être enregistré. Stock Insuffisant", Alert.AlertType.Info);
                    else
                        Alert.SShow("Opération enregistré avec succès.", Alert.AlertType.Sucess);
                    num_damaged = null;
                    Close();
                }
                //Alert.SShow("Vente enregistré avec succès.", Alert.AlertType.Sucess);
                listsaved = (List<bool>)null;
            }
            catch (Exception ex)
            {
                MsgBox msg = new MsgBox();
                msg.show(ex.Message, "Error:", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Question);
                msg.ShowDialog();
            }
        }

        public static async Task<string> Numero_AchatAsync()
        {
            using (var financeDataContext = new QuitayeContext())
            {
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                try
                {
                    var source1 = financeDataContext.tbl_num_achat.Select(d => new
                    {
                        Id = d.Id
                    }).Take(1);
                    int num = 1;
                    if (source1.Count() != 0)
                    {
                        var source2 = financeDataContext.tbl_num_achat.Where((d => d.Date.Value.Month == DateTime.Now.Month 
                        && d.Date.Value.Year == DateTime.Now.Year)).OrderByDescending((d => d.Id)).Select(d => new
                        {
                            Id = d.Id
                        }).Take(1);
                        if (source2.Count() != 0)
                        {
                            var data = source2.First();
                            num = data.Id + 1;
                        }
                        else
                        {
                            var data = financeDataContext.tbl_num_achat.OrderByDescending((d => d.Id)).Select(d => new
                            {
                                Id = d.Id
                            }).First();
                            num = Convert.ToInt32(data.Id) + 1;
                        }
                    }
                    var entity = new Models.Context. tbl_num_achat();
                    string str1 = num >= 10 ? (num < 10 || num >= 100 ? (num < 100 || num >= 1000 ? (num < 1000 || num >= 10000 ? num.ToString() : "0" + num.ToString()) : "00" + num.ToString()) : "000" + num.ToString()) : "0000" + num.ToString();
                    string str2 = DateTime.Now.ToString("MM");
                    string str3 = DateTime.Now.ToString("yy");
                    entity.Id = num;
                    entity.Order_Id = new int?(num);
                    entity.Order = str2 + str3 + "-ACH." + str1;
                    entity.Date = new DateTime?(DateTime.Now);
                    financeDataContext.tbl_num_achat.Add(entity);
                    await financeDataContext.SaveChangesAsync();
                    num_achat = entity.Order;
                }
                catch (Exception ex)
                {
                }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
                return num_achat;
            }
        }

        public static Task<bool> Num_OpérationAsync(string bon_commande) => Task.Factory.StartNew<bool>((() => Num_Opération(bon_commande)));

        private static bool Num_Opération(string bon_commande)
        {
            using (var financeDataContext = new QuitayeContext())
            {
                //int num = 1;
                //if (financeDataContext.tbl_fichier_gestions.Select(d => new
                //{
                //    Id = d.Id
                //}).Take(1).Count() != 0)
                //{
                //    var data = financeDataContext.tbl_fichier_gestions.OrderByDescending(d => d.Id).Select(d => new
                //    {
                //        Id = d.Id
                //    }).First();
                //    num = data.Id + 1;
                //}
                //if (!string.IsNullOrWhiteSpace(filepath))
                //{
                //    byte[] numArray;
                //    using (var input = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                //    {
                //        using (var binaryReader = new BinaryReader(input))
                //            numArray = binaryReader.ReadBytes((int)input.Length);
                //    }
                //    financeDataContext.tbl_fichier_gestions.InsertOnSubmit(new tbl_fichier_gestion()
                //    {
                //        Id = num,
                //        Type = "Achat",
                //        Nom = "Bon Commande : " + bon_commande,
                //        Num_Opération = num_achat,
                //        Auteur = LogIn.profile,
                //        Date = DateTime.Now,
                //        Fichier = (Binary)numArray,
                //        Nom_Fichier = filename
                //    });
                //    financeDataContext.SubmitChanges();
                //}
                return true;
            }
        }


        private async Task SaveAchat()
        {
            if (num_achat == "" || num_achat == null)
            {
                num_achat = await Numero_AchatAsync();
                foreach (Quitaye_School.Models.VenteList item in vente)
                {
                    bool result = await SaveSingleStockAsync(item).Result;
                    if (!result)
#pragma warning disable CS0642 // Possibilité d'instruction vide erronée
                        ;
#pragma warning restore CS0642 // Possibilité d'instruction vide erronée
                }
                int num = await Num_OpérationAsync(cbxBonCommande.Text) ? 1 : 0;
            }
            else
            {
                foreach (Quitaye_School.Models.VenteList item in vente)
                {
                    bool result = await SaveSingleStockAsync(item).Result;
                    if (!result)
#pragma warning disable CS0642 // Possibilité d'instruction vide erronée
                        ;
#pragma warning restore CS0642 // Possibilité d'instruction vide erronée
                }
            }
            var ses = vente.Select(d => new
            {
                Id_Client = d.Id_Client
            });
            string results = Regex.Replace(txtmontantpayer.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
            string reduct = Regex.Replace(txtRéduction.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
            if (results != "" && cbxPayement.Text != "")
            {
                Payement payement = new Payement();
                if (Convert.ToDecimal(results) > vente.Sum<Quitaye_School.Models.VenteList>((x => x.Montant)))
                    results = Convert.ToDecimal(vente.Sum<Quitaye_School.Models.VenteList>((x => x.Montant))).ToString();
                payement.Montant = Convert.ToDecimal(results);
                payement.Client = vente.FirstOrDefault().Client;
                payement.Date = DateTime.Today;
                payement.Reference = txtReference.Text;
                payement.Num_Opération = num_achat;
                payement.Mode_Payement = cbxPayement.Text;
                payement.Type = "Décaissement";
                payement.Raison = "Fournisseur";
                if (ses.Count() != 0)
                    payement.Id_Client = Convert.ToInt32(ses.First().Id_Client);
                if (txtRéduction.Text != "")
                    payement.Reduction = Convert.ToDecimal(txtRéduction.Text);
                if (await SavePayementAsync(payement))
                {
                    txtmontantpayer.Text = null;
                    txtReference.Text = null;
                }
                payement = (Payement)null;
            }
            //Alert.SShow("Opération enregistré avec succès.", Alert.AlertType.Sucess);
            num_vente = null;
            num_achat = "";
            num_damaged = null;
            filepath = "";
            Alert.SShow("Achat enregistrée avec succès.", Alert.AlertType.Sucess);
            filepath = "";
            await ShowData();
            ClearCalculations();
            ClearData();
            ses = null;
            results = null;
            reduct = null;
        }

        private Task<List<Quitaye_School.Models.VenteList>> ArrivageListAsync() => Task.Factory.StartNew<List<Quitaye_School.Models.VenteList>>((Func<List<Quitaye_School.Models.VenteList>>)(() => ArrivageList()));

        private List<Quitaye_School.Models.VenteList> ArrivageList()
        {
            List<Quitaye_School.Models.VenteList> venteListList = new List<Quitaye_School.Models.VenteList>();
            using (var financeDataContext = new QuitayeContext())
            {
                var result = new List<Models.VenteList>();
                if (Returned)
                {
                    result = (from d in financeDataContext.tbl_arrivée_temp
                              where d.Auteur == LogIn.profile
                              join pr in financeDataContext.tbl_produits on d.Product_Id equals pr.Id into pro
                              from p in pro.DefaultIfEmpty()
                              join st in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into pros
                              from s in pros.DefaultIfEmpty()
                              where d.Quantité < 0
                              orderby d.Id descending
                              select new Quitaye_School.Models.VenteList()
                              {
                                  Id = d.Id,
                                  Marque = d.Nom,
                                  Id_Client = d.Id_Fournisseur,
                                  Catégorie = d.Catégorie,
                                  Taille = d.Taille,
                                  Quantité = d.Quantité,
                                  Montant = d.Prix,
                                  Date = d.Date_Arrivée,
                                  Code_Barre = d.Barcode,
                                  Client = d.Fournisseur,
                                  Num_Client = d.Bon_Commande,
                                  Mesure = d.Mesure,
                                  Date_Expiration = d.Date_Expiration,
                                  Prix_Petit = p.Prix_Petit,
                                  Prix_Moyen = p.Prix_Moyen,
                                  Prix_Grand = p.Prix_Grand,
                                  Prix_Large = p.Prix_Large,
                                  Prix_Hyper_Large = p.Prix_Hyper_Large,
                                  Type = s.Type,
                                  Product_Id = p.Id
                              }).ToList();
                }
                else
                {
                   result = (from d in financeDataContext.tbl_arrivée_temp
                     where d.Auteur == LogIn.profile
                     join pr in financeDataContext.tbl_produits on d.Barcode equals pr.Barcode into pro
                     from p in pro.DefaultIfEmpty()
                     join st in financeDataContext.tbl_stock_produits_vente on d.Barcode equals st.Code_Barre into pros
                     from s in pros.DefaultIfEmpty()
                     where d.Quantité > 0
                             orderby d.Id descending
                             select new Quitaye_School.Models.VenteList()
                     {
                         Id = d.Id,
                         Marque = d.Nom,
                         Id_Client = d.Id_Fournisseur,
                         Catégorie = d.Catégorie,
                         Taille = d.Taille,
                         Quantité = d.Quantité,
                         Montant = d.Prix,
                         Date = d.Date_Arrivée,
                         Code_Barre = d.Barcode,
                         Client = d.Fournisseur,
                         Num_Client = d.Bon_Commande,
                         Mesure = d.Mesure,
                         Date_Expiration = d.Date_Expiration,
                         Prix_Petit = p.Prix_Petit,
                         Prix_Moyen = p.Prix_Moyen,
                         Prix_Grand = p.Prix_Grand,
                         Prix_Large = p.Prix_Large,
                         Prix_Hyper_Large = p.Prix_Hyper_Large,
                         Type = s.Type,
                         Product_Id = p.Id
                     }).ToList();
                }
                return result;
            }
        }

        private Task<Task<bool>> SaveSingleStockAsync(Quitaye_School.Models.VenteList vente) => Task.Factory.StartNew(() => SaveSingleStock(vente));
        public async Task<bool> SaveSingleStock(Quitaye_School.Models.VenteList vente)
        {
            bool saved = false;
            using (var donnée = new QuitayeContext())
            {
                try
                {
                    var der = donnée.tbl_arrivée.Select(d => new
                    {
                        Id = d.Id
                    }).Take(1);
                    int id = 1;
                    if (der.Count() != 0)
                    {
                        var de = donnée.tbl_arrivée.OrderByDescending(d => d.Id).Select(d => new
                        {
                            Id = d.Id
                        }).First();
                        id = de.Id + 1;
                        de = null;
                    }

                    var date = DateTime.Now;
                    var tbl = new Models.Context.tbl_arrivée();
                    tbl.Id = id;
                    tbl.Prix = vente.Montant;
                    tbl.Fournisseur = vente.Client;
                    tbl.Bon_Commande = vente.Num_Client;
                    tbl.Nom = vente.Marque;
                    tbl.Catégorie = vente.Catégorie;
                    tbl.Quantité = new Decimal?((Decimal)vente.Quantité);
                    tbl.Taille = vente.Taille;
                    tbl.Auteur = LogIn.profile;
                    tbl.Barcode = vente.Code_Barre;
                    tbl.Date_Arrivée = vente.Date;
                    tbl.Id_Fournisseur = vente.Id_Client.ToString();
                    tbl.Num_Achat = num_achat;
                    tbl.Date_Action = vente.Date;
                    tbl.Mesure = vente.Mesure;
                    tbl.Date_Expiration = vente.Date_Expiration;
                    tbl.Product_Id = vente.Product_Id;

                    var check_expiration = donnée.tbl_expiration.Where(x => x.Code_Barre == vente.Code_Barre && x.Date_Expiration == vente.Date_Expiration).FirstOrDefault();
                    if(check_expiration == null)
                    {
                        var expiration = donnée.tbl_expiration.Add(new tbl_expiration()
                        {
                            Code_Barre = vente.Code_Barre,
                            Date = vente.Date,
                            Date_Expiration = vente.Date_Expiration,
                            Mesure = vente.Mesure,
                            Quantité = vente.Quantité,
                            Reste = vente.Quantité,
                            Filiale = "Siège",
                            Product_Id = vente.Product_Id
                        });
                    }else
                    {
                        check_expiration.Quantité += vente.Quantité;
                        check_expiration.Reste += vente.Quantité;
                    }

                    

                    var hist_id = 1;
                    var histg = donnée.tbl_historique_expiration.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                    hist_id = histg + 1;
                    var new_hist = new tbl_historique_expiration();
                    new_hist.Id = hist_id;
                    new_hist.Barcode = vente.Code_Barre;
                    new_hist.Filiale = "Siège";
                    new_hist.Num_Opération = tbl.Num_Achat;
                    new_hist.Quantité = vente.Quantité;
                    new_hist.Type = "Achat";
                    new_hist.Id_Opération = tbl.Id;
                    new_hist.Date = vente.Date;
                    new_hist.Date_Expiration = vente.Date_Expiration;
                    new_hist.Auteur = LogIn.profile;
                    new_hist.Product_Id = vente.Product_Id;
                    donnée.tbl_historique_expiration.Add(new_hist);

                    var ev_id = 1;
                    var evolution = donnée.tbl_historique_evolution_stock.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                    ev_id = evolution + 1;
                    var new_evo = new tbl_historique_evolution_stock();
                    new_evo.Id = ev_id;
                    new_evo.Barcode = vente.Code_Barre;
                    new_evo.Quantité = vente.Quantité;
                    new_evo.Date = vente.Date;
                    new_evo.Date_Achat = vente.Date;
                    new_evo.Mesure = vente.Mesure;
                    new_evo.Prix_Achat_Petit = Math.Round(Convert.ToDecimal(vente.Montant/vente.Quantité), 2);
                    new_evo.Date_Expiration = vente.Date_Expiration;
                    new_evo.Auteur = LogIn.profile;
                    new_evo.Product_Id = vente.Product_Id;
                    donnée.tbl_historique_evolution_stock.Add(new_evo);

                    var ms = donnée.tbl_mesure_vente.Where((d => d.Nom == vente.Mesure)).First();
                    var stock = (from d in donnée.tbl_stock_produits_vente
                                 join mul in donnée.tbl_multi_barcode 
                                 on d.Product_Id equals mul.Product_Id into multi_barcode_join
                                 from m in multi_barcode_join.DefaultIfEmpty()
                                 where m.Barcode == vente.Code_Barre && (d.Detachement == null || d.Detachement == "Siège")
                                 select d).First();
                    var formu = donnée.tbl_formule_mesure_vente.Where(d => (int?)d.Id == stock.Formule).First();
                    decimal unité = 0M;
                    int? niveau = ms.Niveau;
                    int num1 = 1;
                    if (niveau.GetValueOrDefault() == num1 & niveau.HasValue)
                    {
                        decimal? quantité1 = stock.Quantité;
                        decimal quantité2 = (Decimal)vente.Quantité;
                        stock.Quantité = quantité1.HasValue ? new Decimal?(quantité1.GetValueOrDefault() + quantité2) : new Decimal?();
                    }
                    else
                    {
                        niveau = ms.Niveau;
                        int num2 = 2;
                        if (niveau.GetValueOrDefault() == num2 & niveau.HasValue)
                        {
                            unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Moyen);
                            decimal? quantité = stock.Quantité;
                            decimal num3 = Convert.ToDecimal(vente.Quantité) * unité;
                            stock.Quantité = quantité.HasValue ? new Decimal?(quantité.GetValueOrDefault() + num3) : new Decimal?();
                        }
                        else
                        {
                            niveau = ms.Niveau;
                            int num4 = 3;
                            if (niveau.GetValueOrDefault() == num4 & niveau.HasValue)
                            {
                                unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Grand);
                                decimal? quantité = stock.Quantité;
                                decimal num5 = Convert.ToDecimal(vente.Quantité) * unité;
                                stock.Quantité = quantité.HasValue ? new Decimal?(quantité.GetValueOrDefault() + num5) : new Decimal?();
                            }
                            else
                            {
                                niveau = ms.Niveau;
                                int num6 = 4;
                                if (niveau.GetValueOrDefault() == num6 & niveau.HasValue)
                                {
                                    unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Large);
                                    decimal? quantité = stock.Quantité;
                                    decimal num7 = Convert.ToDecimal(vente.Quantité) * unité;
                                    stock.Quantité = quantité.HasValue ? new Decimal?(quantité.GetValueOrDefault() + num7) : new Decimal?();
                                }
                                else
                                {
                                    niveau = ms.Niveau;
                                    int num8 = 5;
                                    if (niveau.GetValueOrDefault() == num8 & niveau.HasValue)
                                    {
                                        unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Hyper_Large);
                                        decimal? quantité = stock.Quantité;
                                        decimal num9 = Convert.ToDecimal(vente.Quantité) * unité;
                                        stock.Quantité = quantité.HasValue ? new Decimal?(quantité.GetValueOrDefault() + num9) : new Decimal?();
                                    }
                                }
                            }
                        }
                    }

                    var historique = donnée.tbl_historique_valeur_stock.Where(x => x.Code_Barre == stock.Code_Barre
                    && x.Filiale == stock.Detachement && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(DateTime.Now))
                    .AsEnumerable().FirstOrDefault();
                    if (historique != null )
                    {
                        historique.Quantité = stock.Quantité;
                    }
                    else
                    {
                        var hist = 1;
                        var dsv = (from d in donnée.tbl_historique_valeur_stock
                                   orderby d.Id descending
                                   select d).FirstOrDefault();
                        if (dsv != null)
                            hist = Convert.ToInt32(dsv.Id) + 1;
                        var h = new Models.Context.tbl_historique_valeur_stock();
                        h.Code_Barre = vente.Code_Barre;
                        h.Filiale = "Siège";
                        h.Date = DateTimeOffset.Now;
                        h.Prix_Grand = vente.Prix_Grand;
                        h.Prix_Petit = vente.Prix_Petit;
                        h.Prix_Moyen = vente.Prix_Moyen;
                        h.Prix_Large = vente.Prix_Large;
                        h.Prix_Hyper_Large = Convert.ToDecimal(vente.Prix_Hyper_Large);
                        h.Quantité = stock.Quantité;
                        h.Id = hist;
                        h.Product_Id = vente.Product_Id;
                        donnée.tbl_historique_valeur_stock.Add(h);
                    }

                    tbl.Q_Unité = !(unité != 0M) ? new Decimal?((Decimal)vente.Quantité) : new Decimal?((Decimal)vente.Quantité * unité);
                    donnée.tbl_arrivée.Add(tbl);
                    var del = donnée.tbl_arrivée_temp.Where((d => d.Id == vente.Id)).First();
                    donnée.tbl_arrivée_temp.Remove(del);
                    await donnée.SaveChangesAsync();
                    saved = true;
                    der = null;
                    tbl = null;
                    ms = null;
                    formu = null;
                    del = null;
                }
                catch (Exception ex)
                {
                    if (await MyExeption.ErrorReseauAsync(ex))
                        Alert.SShow("Erreur Réseau. Veillez verifier votre connection Internet !", Alert.AlertType.Info);
                    return false;
                }
            }
            return saved;
        }

        //private static Task<bool> Num_OpérationAsync(string bon_commande)
        //{
        //    using (var financeDataContext = new QuitayeContext())
        //    {
        //        int num = 1;
        //        if (financeDataContext.tbl_fichier_gestions.Select(d => new
        //        {
        //            Id = d.Id
        //        }).Take(1).Count() != 0)
        //        {
        //            var data = financeDataContext.tbl_fichier_gestions.OrderByDescending(d => d.Id).Select(d => new
        //            {
        //                Id = d.Id
        //            }).First();
        //            num = data.Id + 1;
        //        }
        //        if (!string.IsNullOrWhiteSpace(filepath))
        //        {
        //            byte[] numArray;
        //            using (var input = new FileStream(filepath, FileMode.Open, FileAccess.Read))
        //            {
        //                using (var binaryReader = new BinaryReader(input))
        //                    numArray = binaryReader.ReadBytes((int)input.Length);
        //            }
        //            //financeDataContext.tbl_fichier_gestions.InsertOnSubmit(new tbl_fichier_gestion()
        //            //{
        //            //    Id = num,
        //            //    Type = "Achat",
        //            //    Nom = "Bon Commande : " + bon_commande,
        //            //    Num_Opération = num_achat,
        //            //    Auteur = LogIn.profile,
        //            //    Date = DateTime.Now,
        //            //    Fichier = (Binary)numArray,
        //            //    Nom_Fichier = filename
        //            //});
        //            await financeDataContext.SaveChangesAsync();
        //        }
        //        return true;
        //    }
        //}

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public static async Task<string> Numero_Cmd()
        {
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_num_vente.Select(d => new
                {
                    Id = d.Id
                }).Take(1);
                int num = 1;
                if (source.Count() != 0)
                    num = Convert.ToInt32(financeDataContext.tbl_num_vente.OrderByDescending((d => d.Id)).Take(1).First().Id) + 1;
                var entity = new Models.Context.tbl_num_vente();
                string str1 = num >= 10 ? (num < 10 || num >= 100 ? (num < 100 || num >= 1000 ? (num < 1000 
                    || num >= 10000 ? num.ToString() : "0" + num.ToString()) : "00" + num.ToString()) : "000" + num.ToString()) : "0000" + num.ToString();
                string str2 = DateTime.Now.ToString("MM");
                string str3 = DateTime.Now.ToString("yy");
                entity.Id = num;
                entity.OrderId = new int?(num);
                entity.Order = str2 + str3 + "-VTE." + str1;
                entity.Date = new DateTime?(DateTime.Now);
                financeDataContext.tbl_num_vente.Add(entity);
                await financeDataContext.SaveChangesAsync();
                num_vente = entity.Order;
                return num_vente;
            }
        }

        public static async Task<string> Numero_Damaged()
        {
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_num_damaged.Select(d => new
                {
                    Id = d.Id
                }).Take(1);
                int num = 1;
                if (source.Count() != 0)
                    num = Convert.ToInt32(financeDataContext.tbl_num_damaged.OrderByDescending((d => d.Id)).Take(1).First().Id) + 1;
                var entity = new Models.Context.tbl_num_damaged();
                string str1 = num >= 10 ? (num < 10 || num >= 100 ? (num < 100 || num >= 1000 ? (num < 1000
                    || num >= 10000 ? num.ToString() : "0" + num.ToString()) : "00" + num.ToString()) : "000" + num.ToString()) : "0000" + num.ToString();
                string str2 = DateTime.Now.ToString("MM");
                string str3 = DateTime.Now.ToString("yy");
                entity.Id = num;
                entity.OrderId = new int?(num);
                entity.Order = str2 + str3 + "-DMG." + str1;
                entity.Date = new DateTime?(DateTime.Now);
                financeDataContext.tbl_num_damaged.Add(entity);
                await financeDataContext.SaveChangesAsync();
                num_damaged = entity.Order;
                return num_damaged;
            }
        }

        public static async Task<bool> SaveSingleAsync(Quitaye_School.Models.VenteList vente)
        {
            using (var donnée = new QuitayeContext())
            {
                try
                {
                    var der = donnée.tbl_vente.OrderByDescending(d => d.Id).Select(d => new
                    {
                        Id = d.Id
                    }).Take(1);
                    int id = 1;
                    if (der.Count() != 0)
                    {
                        var ders = der.First();
                        id = ders.Id + 1;
                        ders = null;
                    }
                    var tbl = new Models.Context.tbl_vente()
                    {
                        Id = id,
                        Type = vente.Model,
                        Auteur = vente.Auteur,
                        Date_Vente = vente.Date,
                        Quantité = new Decimal?((Decimal)vente.Quantité),
                        Num_Vente = num_vente,
                        Id_Client = Convert.ToInt32(vente.Id_Client),
                        Montant = vente.Montant,
                        Prix_Unité = vente.Prix_Unitaire,
                        Produit = vente.Marque,
                        Catégorie = vente.Catégorie,
                        Reduction = vente.Reduction
                    };
                    tbl.Id_Client = Convert.ToInt32(vente.Id_Client);
                    tbl.Type = !check ? "Payée" : "A Crédit";
                    //if (restant)
                    //    tbl.Type = "Restant";
                    tbl.Taille = vente.Taille;
                    tbl.Filiale = vente.Filiale;
                    tbl.Mesure = vente.Mesure;
                    tbl.Client = vente.Client;
                    tbl.Mode_Payement = vente.Mode_Payement;
                    tbl.Num_Client = vente.Num_Client;
                    tbl.Date_Action = new DateTime?(DateTime.Now);
                    tbl.Barcode = vente.Code_Barre;
                    tbl.Product_Id = vente.Product_Id;
                    var ms = donnée.tbl_mesure_vente.Where((d => d.Nom == vente.Mesure)).First();
                    var stock = new Models.Context.tbl_stock_produits_vente();
                    if (tbl.Filiale != "" && tbl.Filiale != null)
                        stock = (from d in donnée.tbl_stock_produits_vente 
                                 join mul in donnée.tbl_multi_barcode on d.Product_Id equals mul.Product_Id into multi_barcode
                                 from m in multi_barcode.DefaultIfEmpty()
                                 where m.Barcode == vente.Code_Barre
                        && d.Detachement == tbl.Filiale select d).First();
                    else
                        stock = (from d in donnée.tbl_stock_produits_vente 
                                 join mul in donnée.tbl_multi_barcode on d.Product_Id equals mul.Product_Id into multi_barcode_join 
                                 from m in multi_barcode_join.DefaultIfEmpty()
                                 where m.Barcode == vente.Code_Barre 
                        && (d.Detachement == null || d.Detachement == "Siège") select d).First();
                    var formu = donnée.tbl_formule_mesure_vente.Where(d => (int?)d.Id == stock.Formule).First();
                    Decimal unité = 0M;
                    int? niveau1 = ms.Niveau;
                    int num1 = 1;

                    var expiration = await donnée.tbl_expiration
                        .Where(x => x.Code_Barre == vente.Code_Barre)
                        .OrderBy(x => x.Date_Expiration).Select(d => d).ToListAsync();

                    decimal? qté = vente.Quantité;
                    decimal default_reste = 0;
                    var hist_id = 1;
                    if (expiration.Count > 0)
                    foreach (var item in expiration)
                    {
                        if (qté > 0)
                        {
                            if(qté >= item.Reste)
                            {
                                if (hist_id == 1)
                                {
                                    var hist = donnée.tbl_historique_expiration
                                    .OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                                    hist_id = hist + 1;
                                }
                                var new_hist = new tbl_historique_expiration();
                                new_hist.Id = hist_id;
                                new_hist.Barcode = item.Code_Barre;
                                new_hist.Filiale = item.Filiale;
                                new_hist.Quantité = item.Reste;
                                new_hist.Type = "Vente";
                                new_hist.Id_Opération = tbl.Id;
                                new_hist.Num_Opération = tbl.Num_Vente;
                                new_hist.Date = DateTime.Now;
                                new_hist.Date_Expiration = item.Date_Expiration;
                                new_hist.Auteur = LogIn.profile;
                                new_hist.Product_Id = vente.Product_Id;
                                donnée.tbl_historique_expiration.Add(new_hist);
                                    tbl.Date_Expiration = item.Date_Expiration;
                                qté -= item.Reste;
                                item.Reste = 0;
                                 hist_id += 1;
                                }
                            else
                            {
                                if(hist_id == 1)
                                {
                                    var hist = donnée.tbl_historique_expiration
                                    .OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                                    hist_id = hist + 1;
                                }
                               
                                var new_hist = new tbl_historique_expiration();
                                new_hist.Id = hist_id;
                                new_hist.Barcode = item.Code_Barre;
                                new_hist.Filiale = item.Filiale;
                                new_hist.Num_Opération = tbl.Num_Vente;
                                new_hist.Quantité = qté;
                                new_hist.Type = "Vente";
                                new_hist.Id_Opération = tbl.Id;
                                new_hist.Date = DateTime.Now;
                                new_hist.Date_Expiration = item.Date_Expiration;
                                new_hist.Auteur = LogIn.profile;
                                new_hist.Product_Id = vente.Product_Id;
                                donnée.tbl_historique_expiration.Add(new_hist);
                                    tbl.Date_Expiration = item.Date_Expiration;
                                    item.Reste -= qté;
                                qté = 0;
                                    hist_id += 1;
                            }
                                default_reste = Convert.ToDecimal(item.Reste);
                        }
                        else break;
                    }

                    if (niveau1.GetValueOrDefault() == num1 & niveau1.HasValue)
                    {
                        decimal? quantité1 = stock.Quantité;
                        decimal quantité2 = (Decimal)vente.Quantité;
                        if (!(quantité1.GetValueOrDefault() >= quantité2 & quantité1.HasValue))
                            return false;
                        var stockProduitsVente = stock;
                        Decimal? quantité3 = stockProduitsVente.Quantité;
                        Decimal quantité4 = (Decimal)vente.Quantité;
                        stockProduitsVente.Quantité = quantité3.HasValue ? new Decimal?(quantité3.GetValueOrDefault() - quantité4) : new Decimal?();
                    }
                    else
                    {
                        int? niveau2 = ms.Niveau;
                        int num2 = 2;
                        if (niveau2.GetValueOrDefault() == num2 & niveau2.HasValue)
                        {
                            unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Moyen);
                            Decimal? quantité5 = stock.Quantité;
                            Decimal num3 = Convert.ToDecimal(vente.Quantité) * unité;
                            if (!(quantité5.GetValueOrDefault() >= num3 & quantité5.HasValue))
                                return false;
                            var stockProduitsVente = stock;
                            Decimal? quantité6 = stockProduitsVente.Quantité;
                            Decimal num4 = Convert.ToDecimal(vente.Quantité) * unité;
                            stockProduitsVente.Quantité = quantité6.HasValue ? new Decimal?(quantité6.GetValueOrDefault() - num4) : new Decimal?();
                        }
                        else
                        {
                            niveau2 = ms.Niveau;
                            int num5 = 3;
                            if (niveau2.GetValueOrDefault() == num5 & niveau2.HasValue)
                            {
                                unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Grand);
                                Decimal? quantité7 = stock.Quantité;
                                Decimal num6 = Convert.ToDecimal(vente.Quantité) * unité;
                                if (!(quantité7.GetValueOrDefault() >= num6 & quantité7.HasValue))
                                    return false;
                                var stockProduitsVente = stock;
                                Decimal? quantité8 = stockProduitsVente.Quantité;
                                Decimal num7 = Convert.ToDecimal(vente.Quantité) * unité;
                                stockProduitsVente.Quantité = quantité8.HasValue ? new Decimal?(quantité8.GetValueOrDefault() - num7) : new Decimal?();
                            }
                            else
                            {
                                niveau2 = ms.Niveau;
                                int num8 = 4;
                                if (niveau2.GetValueOrDefault() == num8 & niveau2.HasValue)
                                {
                                    unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Large);
                                    Decimal? quantité9 = stock.Quantité;
                                    Decimal num9 = Convert.ToDecimal(vente.Quantité) * unité;
                                    if (!(quantité9.GetValueOrDefault() >= num9 & quantité9.HasValue))
                                        return false;
                                    var stockProduitsVente = stock;
                                    Decimal? quantité10 = stockProduitsVente.Quantité;
                                    Decimal num10 = Convert.ToDecimal(vente.Quantité) * unité;
                                    stockProduitsVente.Quantité = quantité10.HasValue ? new Decimal?(quantité10.GetValueOrDefault() - num10) : new Decimal?();
                                }
                                else
                                {
                                    niveau2 = ms.Niveau;
                                    int num11 = 5;
                                    if (niveau2.GetValueOrDefault() == num11 & niveau2.HasValue)
                                    {
                                        unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Hyper_Large);
                                        Decimal? quantité11 = stock.Quantité;
                                        Decimal num12 = Convert.ToDecimal(vente.Quantité) * unité;
                                        if (!(quantité11.GetValueOrDefault() >= num12 & quantité11.HasValue))
                                            return false;
                                        var stockProduitsVente = stock;
                                        Decimal? quantité12 = stockProduitsVente.Quantité;
                                        Decimal num13 = Convert.ToDecimal(vente.Quantité) * unité;
                                        stockProduitsVente.Quantité = quantité12.HasValue ? new Decimal?(quantité12.GetValueOrDefault() - num13) : new Decimal?();
                                    }
                                }
                            }
                        }
                    }

                    var historique = donnée.tbl_historique_valeur_stock.Where(x => x.Code_Barre == stock.Code_Barre
                    && x.Filiale == stock.Detachement && x.Date.HasValue
                    && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();
                    if (historique != null)
                    {
                        historique.Quantité = stock.Quantité;
                    }
                    else
                    {
                        var hist = 1;
                        var dsv = (from d in donnée.tbl_historique_valeur_stock
                                   orderby d.Id descending
                                   select d).FirstOrDefault();
                        if (dsv != null)
                            hist = Convert.ToInt32(dsv.Id) + 1;
                        var h = new Models.Context.tbl_historique_valeur_stock();
                        var deser = donnée.tbl_arrivée.Where(x => x.Barcode == stock.Code_Barre).OrderByDescending(x => x.Date_Arrivée).FirstOrDefault();
                        if(deser != null)
                        {
                            h.Code_Barre = vente.Code_Barre;
                            h.Filiale = vente.Filiale;
                            h.Date = DateTimeOffset.Now;
                            h.Prix_Petit = Math.Round(Convert.ToDecimal(deser.Prix/deser.Quantité), 2);
                           
                            h.Quantité = stock.Quantité;
                            h.Id = hist;
                            h.Product_Id = vente.Product_Id;
                            donnée.tbl_historique_valeur_stock.Add(h);
                        }
                        
                        
                    }

                    var evolution = donnée.tbl_historique_evolution_stock.Where(x => x.Barcode == tbl.Barcode && x.Quantité > 0
                    && DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(tbl.Date_Expiration) 
                    && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();
                    if(evolution != null)
                    {
                        evolution.Quantité = default_reste;
                    }
                    else
                    {
                        var ev_id = 1;
                        var evolutio = donnée.tbl_historique_evolution_stock.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                        ev_id = evolutio + 1;
                        var achats = donnée.tbl_arrivée.Where(x => x.Barcode == vente.Code_Barre
                        && DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(tbl.Date_Expiration))
                            .OrderBy(x => x.Date_Arrivée).ToList();
                        var achat = new Models.Context.tbl_arrivée();
                        if(achats.Count > 0)
                        {
                            achat = donnée.tbl_arrivée.Where(x => x.Barcode == vente.Code_Barre
                                    && DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(tbl.Date_Expiration))
                                    .OrderByDescending(x => x.Date_Arrivée).FirstOrDefault();
                        }

                        var check_ev = donnée.tbl_historique_evolution_stock.Where(x => x.Date_Achat == achat.Date_Arrivée 
                        && x.Quantité > 0).OrderByDescending(x => x.Date).FirstOrDefault();
                        if(check_ev  != null)
                        {
                            //achat = donnée.tbl_arrivée.Where(x => x.Barcode == vente.Code_Barre && x.Date_Arrivée != check_ev.Date_Achat
                            //&& DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(tbl.Date_Expiration)).OrderBy(x => x.Date_Arrivée).FirstOrDefault();
                        }

                        var new_evo = new tbl_historique_evolution_stock();
                        new_evo.Id = ev_id;
                        new_evo.Barcode = vente.Code_Barre;
                        new_evo.Quantité = default_reste;
                        new_evo.Date = vente.Date;
                        new_evo.Date_Achat = achat.Date_Arrivée;
                        new_evo.Mesure = vente.Mesure;
                        new_evo.Prix_Achat_Petit = Math.Round(Convert.ToDecimal(achat.Prix / achat.Quantité), 2);
                        new_evo.Date_Expiration = achat.Date_Expiration;
                        new_evo.Auteur = LogIn.profile;
                        new_evo.Product_Id = vente.Product_Id;
                        donnée.tbl_historique_evolution_stock.Add(new_evo);
                    }
                    tbl.Q_Unité = !(unité != 0M) ? new Decimal?((Decimal)vente.Quantité) : new Decimal?((Decimal)vente.Quantité * unité);
                    donnée.tbl_vente.Add(tbl);
                    var del = donnée.tbl_vente_temp.Where(d => d.Id == vente.Id).FirstOrDefault();
                    if(del != null)
                    donnée.tbl_vente_temp.Remove(del);
                    await donnée.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    if (await MyExeption.ErrorReseauAsync(ex))
                        Alert.SShow("Erreur Réseau. Veillez verifier votre connection Internet !", Alert.AlertType.Info);
                    return false;
                }
            }
        }

        public static async Task<bool> SaveSingleDamagedAsync(Quitaye_School.Models.VenteList vente)
        {
            using (var donnée = new QuitayeContext())
            {
                try
                {
                    var der = donnée.tbl_damaged_expense.OrderByDescending(d => d.Id).Select(d => new
                    {
                        Id = d.Id
                    }).Take(1);
                    int id = 1;
                    if (der.Count() != 0)
                    {
                        var ders = der.First();
                        id = ders.Id + 1;
                        ders = null;
                    }
                    var tbl = new Models.Context.tbl_damaged_expense()
                    {
                        Id = id,
                        Type = vente.Model,
                        Auteur = vente.Auteur,
                        Date_Damaged = vente.Date,
                        Quantité = new Decimal?((Decimal)vente.Quantité),
                        Num_Damaged = num_damaged,
                        Id_Client = Convert.ToInt32(vente.Id_Client),
                        Montant = vente.Montant,
                        Prix_Unité = vente.Prix_Unitaire,
                        Produit = vente.Marque,
                        Catégorie = vente.Catégorie,
                        Reduction = vente.Reduction
                    };
                    tbl.Id_Client = Convert.ToInt32(vente.Id_Client);
                    tbl.Type = !check ? "Payée" : "A Crédit";
                    //if (restant)
                    //    tbl.Type = "Restant";
                    tbl.Taille = vente.Taille;
                    tbl.Filiale = vente.Filiale;
                    tbl.Mesure = vente.Mesure;
                    tbl.Client = vente.Client;
                    tbl.Mode_Payement = vente.Mode_Payement;
                    tbl.Num_Client = vente.Num_Client;
                    tbl.Date_Action = new DateTime?(DateTime.Now);
                    tbl.Barcode = vente.Code_Barre;
                    tbl.Product_Id = vente.Product_Id;
                    var ms = donnée.tbl_mesure_vente.Where(d => d.Nom == vente.Mesure).First();
                    var stock = new Models.Context.tbl_stock_produits_vente();
                    if (tbl.Filiale != "" && tbl.Filiale != null)
                        stock = (from d in donnée.tbl_stock_produits_vente
                                 join mul in donnée.tbl_multi_barcode on d.Product_Id equals mul.Product_Id into multi_barcode
                                 from m in multi_barcode.DefaultIfEmpty()
                                 where m.Barcode == vente.Code_Barre
                        && d.Detachement == tbl.Filiale
                                 select d).First();
                    else
                        stock = (from d in donnée.tbl_stock_produits_vente
                                 join mul in donnée.tbl_multi_barcode on d.Product_Id equals mul.Product_Id into multi_barcode_join
                                 from m in multi_barcode_join.DefaultIfEmpty()
                                 where m.Barcode == vente.Code_Barre
                        && (d.Detachement == null || d.Detachement == "Siège")
                                 select d).First();
                    var formu = donnée.tbl_formule_mesure_vente.Where(d => (int?)d.Id == stock.Formule).First();
                    Decimal unité = 0M;
                    int? niveau1 = ms.Niveau;
                    int num1 = 1;

                    var expiration = await donnée.tbl_expiration
                        .Where(x => x.Code_Barre == vente.Code_Barre)
                        .OrderBy(x => x.Date_Expiration).Select(d => d).ToListAsync();

                    decimal? qté = vente.Quantité;
                    decimal default_reste = 0;
                    var hist_id = 1;
                    if (expiration.Count > 0)
                        foreach (var item in expiration)
                        {
                            if (qté > 0)
                            {
                                if (qté >= item.Reste)
                                {
                                    if (hist_id == 1)
                                    {
                                        var hist = donnée.tbl_historique_expiration
                                        .OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                                        hist_id = hist + 1;
                                    }
                                    var new_hist = new tbl_historique_expiration();
                                    new_hist.Id = hist_id;
                                    new_hist.Barcode = item.Code_Barre;
                                    new_hist.Filiale = item.Filiale;
                                    new_hist.Quantité = item.Reste;
                                    new_hist.Type = "Vente";
                                    new_hist.Id_Opération = tbl.Id;
                                    new_hist.Num_Opération = tbl.Num_Damaged;
                                    new_hist.Date = DateTime.Now;
                                    new_hist.Date_Expiration = item.Date_Expiration;
                                    new_hist.Auteur = LogIn.profile;
                                    new_hist.Product_Id = vente.Product_Id;
                                    donnée.tbl_historique_expiration.Add(new_hist);
                                    tbl.Date_Expiration = item.Date_Expiration;
                                    qté -= item.Reste;
                                    item.Reste = 0;
                                    hist_id += 1;
                                }
                                else
                                {
                                    if (hist_id == 1)
                                    {
                                        var hist = donnée.tbl_historique_expiration
                                        .OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                                        hist_id = hist + 1;
                                    }

                                    var new_hist = new tbl_historique_expiration();
                                    new_hist.Id = hist_id;
                                    new_hist.Barcode = item.Code_Barre;
                                    new_hist.Filiale = item.Filiale;
                                    new_hist.Num_Opération = tbl.Num_Damaged;
                                    new_hist.Quantité = qté;
                                    new_hist.Type = "Vente";
                                    new_hist.Id_Opération = tbl.Id;
                                    new_hist.Date = DateTime.Now;
                                    new_hist.Date_Expiration = item.Date_Expiration;
                                    new_hist.Auteur = LogIn.profile;
                                    new_hist.Product_Id = vente.Product_Id;
                                    donnée.tbl_historique_expiration.Add(new_hist);
                                    tbl.Date_Expiration = item.Date_Expiration;
                                    item.Reste -= qté;
                                    qté = 0;
                                    hist_id += 1;
                                }
                                default_reste = Convert.ToDecimal(item.Reste);
                            }
                            else break;
                        }

                    if (niveau1.GetValueOrDefault() == num1 & niveau1.HasValue)
                    {
                        decimal? quantité1 = stock.Quantité;
                        decimal quantité2 = (Decimal)vente.Quantité;
                        if (!(quantité1.GetValueOrDefault() >= quantité2 & quantité1.HasValue))
                            return false;
                        var stockProduitsVente = stock;
                        Decimal? quantité3 = stockProduitsVente.Quantité;
                        Decimal quantité4 = (Decimal)vente.Quantité;
                        stockProduitsVente.Quantité = quantité3.HasValue ? new Decimal?(quantité3.GetValueOrDefault() - quantité4) : new Decimal?();
                    }
                    else
                    {
                        int? niveau2 = ms.Niveau;
                        int num2 = 2;
                        if (niveau2.GetValueOrDefault() == num2 & niveau2.HasValue)
                        {
                            unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Moyen);
                            Decimal? quantité5 = stock.Quantité;
                            Decimal num3 = Convert.ToDecimal(vente.Quantité) * unité;
                            if (!(quantité5.GetValueOrDefault() >= num3 & quantité5.HasValue))
                                return false;
                            var stockProduitsVente = stock;
                            Decimal? quantité6 = stockProduitsVente.Quantité;
                            Decimal num4 = Convert.ToDecimal(vente.Quantité) * unité;
                            stockProduitsVente.Quantité = quantité6.HasValue ? new Decimal?(quantité6.GetValueOrDefault() - num4) : new Decimal?();
                        }
                        else
                        {
                            niveau2 = ms.Niveau;
                            int num5 = 3;
                            if (niveau2.GetValueOrDefault() == num5 & niveau2.HasValue)
                            {
                                unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Grand);
                                Decimal? quantité7 = stock.Quantité;
                                Decimal num6 = Convert.ToDecimal(vente.Quantité) * unité;
                                if (!(quantité7.GetValueOrDefault() >= num6 & quantité7.HasValue))
                                    return false;
                                var stockProduitsVente = stock;
                                Decimal? quantité8 = stockProduitsVente.Quantité;
                                Decimal num7 = Convert.ToDecimal(vente.Quantité) * unité;
                                stockProduitsVente.Quantité = quantité8.HasValue ? new Decimal?(quantité8.GetValueOrDefault() - num7) : new Decimal?();
                            }
                            else
                            {
                                niveau2 = ms.Niveau;
                                int num8 = 4;
                                if (niveau2.GetValueOrDefault() == num8 & niveau2.HasValue)
                                {
                                    unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Large);
                                    Decimal? quantité9 = stock.Quantité;
                                    Decimal num9 = Convert.ToDecimal(vente.Quantité) * unité;
                                    if (!(quantité9.GetValueOrDefault() >= num9 & quantité9.HasValue))
                                        return false;
                                    var stockProduitsVente = stock;
                                    Decimal? quantité10 = stockProduitsVente.Quantité;
                                    Decimal num10 = Convert.ToDecimal(vente.Quantité) * unité;
                                    stockProduitsVente.Quantité = quantité10.HasValue ? new Decimal?(quantité10.GetValueOrDefault() - num10) : new Decimal?();
                                }
                                else
                                {
                                    niveau2 = ms.Niveau;
                                    int num11 = 5;
                                    if (niveau2.GetValueOrDefault() == num11 & niveau2.HasValue)
                                    {
                                        unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Hyper_Large);
                                        Decimal? quantité11 = stock.Quantité;
                                        Decimal num12 = Convert.ToDecimal(vente.Quantité) * unité;
                                        if (!(quantité11.GetValueOrDefault() >= num12 & quantité11.HasValue))
                                            return false;
                                        var stockProduitsVente = stock;
                                        Decimal? quantité12 = stockProduitsVente.Quantité;
                                        Decimal num13 = Convert.ToDecimal(vente.Quantité) * unité;
                                        stockProduitsVente.Quantité = quantité12.HasValue ? new Decimal?(quantité12.GetValueOrDefault() - num13) : new Decimal?();
                                    }
                                }
                            }
                        }
                    }

                    var historique = donnée.tbl_historique_valeur_stock.Where(x => x.Code_Barre == stock.Code_Barre
                    && x.Filiale == stock.Detachement && x.Date.HasValue
                    && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();
                    if (historique != null)
                    {
                        historique.Quantité = stock.Quantité;
                    }
                    else
                    {
                        var hist = 1;
                        var dsv = (from d in donnée.tbl_historique_valeur_stock
                                   orderby d.Id descending
                                   select d).FirstOrDefault();
                        if (dsv != null)
                            hist = Convert.ToInt32(dsv.Id) + 1;
                        var h = new Models.Context.tbl_historique_valeur_stock();
                        var deser = donnée.tbl_arrivée.Where(x => x.Barcode == stock.Code_Barre).OrderByDescending(x => x.Date_Arrivée).FirstOrDefault();
                        if (deser != null)
                        {
                            h.Code_Barre = vente.Code_Barre;
                            h.Filiale = vente.Filiale;
                            h.Date = DateTimeOffset.Now;
                            h.Prix_Petit = Math.Round(Convert.ToDecimal(deser.Prix / deser.Quantité), 2);

                            h.Quantité = stock.Quantité;
                            h.Id = hist;
                            h.Product_Id = vente.Product_Id;
                            donnée.tbl_historique_valeur_stock.Add(h);
                        }


                    }

                    var evolution = donnée.tbl_historique_evolution_stock.Where(x => x.Barcode == tbl.Barcode && x.Quantité > 0
                    && DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(tbl.Date_Expiration)
                    && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();
                    if (evolution != null)
                    {
                        evolution.Quantité = default_reste;
                    }
                    else
                    {
                        var ev_id = 1;
                        var evolutio = donnée.tbl_historique_evolution_stock.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                        ev_id = evolutio + 1;
                        var achats = donnée.tbl_arrivée.Where(x => x.Barcode == vente.Code_Barre
                        && DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(tbl.Date_Expiration))
                            .OrderBy(x => x.Date_Arrivée).ToList();
                        var achat = new Models.Context.tbl_arrivée();
                        if (achats.Count > 0)
                        {
                            achat = donnée.tbl_arrivée.Where(x => x.Barcode == vente.Code_Barre
                                    && DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(tbl.Date_Expiration))
                                    .OrderByDescending(x => x.Date_Arrivée).FirstOrDefault();
                        }

                        var check_ev = donnée.tbl_historique_evolution_stock.Where(x => x.Date_Achat == achat.Date_Arrivée
                        && x.Quantité > 0).OrderByDescending(x => x.Date).FirstOrDefault();
                        if (check_ev != null)
                        {
                            //achat = donnée.tbl_arrivée.Where(x => x.Barcode == vente.Code_Barre && x.Date_Arrivée != check_ev.Date_Achat
                            //&& DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(tbl.Date_Expiration)).OrderBy(x => x.Date_Arrivée).FirstOrDefault();
                        }

                        var new_evo = new tbl_historique_evolution_stock();
                        new_evo.Id = ev_id;
                        new_evo.Barcode = vente.Code_Barre;
                        new_evo.Quantité = default_reste;
                        new_evo.Date = vente.Date;
                        new_evo.Date_Achat = achat.Date_Arrivée;
                        new_evo.Mesure = vente.Mesure;
                        new_evo.Prix_Achat_Petit = Math.Round(Convert.ToDecimal(achat.Prix / achat.Quantité), 2);
                        new_evo.Date_Expiration = achat.Date_Expiration;
                        new_evo.Auteur = LogIn.profile;
                        new_evo.Product_Id = vente.Product_Id;
                        donnée.tbl_historique_evolution_stock.Add(new_evo);
                    }
                    tbl.Q_Unité = !(unité != 0M) ? new Decimal?((Decimal)vente.Quantité) : new Decimal?((Decimal)vente.Quantité * unité);
                    donnée.tbl_damaged_expense.Add(tbl);
                    var del = donnée.tbl_damaged_expense_temp.Where(d => d.Id == vente.Id).FirstOrDefault();
                    if (del != null)
                        donnée.tbl_damaged_expense_temp.Remove(del);
                    await donnée.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    if (await MyExeption.ErrorReseauAsync(ex))
                        Alert.SShow("Erreur Réseau. Veillez verifier votre connection Internet !", Alert.AlertType.Info);
                    return false;
                }
            }
        }


        private void btnImprimer_Click(object sender, EventArgs e)
        {
            //Code_Barre codeBarre = new Code_Barre();
            //if (cbxCode.Text != "")
            //{
            //    codeBarre.txtcode.Text = cbxCode.Text;
            //    codeBarre.txtcode.ReadOnly = true;
            //}
            //codeBarre.ShowDialog();
            //codeBarre.Dispose();
        }
    }
}
