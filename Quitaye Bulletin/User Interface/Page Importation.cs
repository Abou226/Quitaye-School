using Microsoft.Office.Interop.Excel;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
#pragma warning disable CS0105 // La directive using est apparue précédemment dans cet espace de noms
using System;
#pragma warning restore CS0105 // La directive using est apparue précédemment dans cet espace de noms
#pragma warning disable CS0105 // La directive using est apparue précédemment dans cet espace de noms
using System.Collections.Generic;
#pragma warning restore CS0105 // La directive using est apparue précédemment dans cet espace de noms
#pragma warning disable CS0105 // La directive using est apparue précédemment dans cet espace de noms
using System.Data;
#pragma warning restore CS0105 // La directive using est apparue précédemment dans cet espace de noms
using System.IO;
#pragma warning disable CS0105 // La directive using est apparue précédemment dans cet espace de noms
using System.Linq;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Wordprocessing;
using Quitaye_School.Models;
using Microsoft.VisualBasic;
#pragma warning restore CS0105 // La directive using est apparue précédemment dans cet espace de noms

namespace Quitaye_School.User_Interface
{
    public partial class Page_Importation : Form
    {
        public string Mesure { get; set; }
        public Timer LoadTimer { get; set; }
        public ExcelFile File { get; set; }
        public Page_Importation()
        {
            InitializeComponent();
            btnFermer.Click += BtnFermer_Click;
            btnImporterExcel.Click += BtnImporterExcel_Click;
            btnEnregistrer.Click += BtnEnregistrer_Click;
            LoadTimer = new Timer();
            LoadTimer.Enabled = false;
            LoadTimer.Interval = 10;
            LoadTimer.Tick += LoadTimer_Tick;
            btnEditImport.Click += BtnEditImport_Click;
            LoadTimer.Start();
            btnModifier.Click += BtnModifier_Click;
            btnVentes.Click += BtnVentes_Click;
            btnSaveSales.Click += BtnSaveSales_Click;
            btnExcel.Click += BtnExcel_Click;
        }

        private async void BtnExcel_Click(object sender, EventArgs e)
        {
            Alert.SShow("Génération Fichier en cours.. Veillez-patientez !", Alert.AlertType.Info);
            var file = $"C:/Quitaye School/List Inventaire {DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")}.xlsx";
            await Task.Run(() => Quitaye_School.Models.Docuement.ExportToExcel(this.dataGridView1, file));
            System.Diagnostics.Process.Start(file);
            Alert.SShow("Génération effectué avec succès.", Alert.AlertType.Sucess);
        }

        private async void BtnSaveSales_Click(object sender, EventArgs e)
        {
            var msg = new MsgBox();
            msg.show("Voulez-vous enregistrer ces ventes ?", "Enregistrement", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
            msg.ShowDialog();
            if (msg.clicked == "Oui")
            {
                Alert.SShow("Enregistrement en cours.... Veillez-patienez !", Alert.AlertType.Info);
                var result = await SaveSalesAsync(File.Sheets.FirstOrDefault().Sales);
                if (result.Item1)
                {
                    Alert.SShow("Element(s) enregistré(s) avec succès.", Alert.AlertType.Sucess);
                    dataGridView1.DataSource = result.Item2;
                }
            }
        }

        private async void BtnVentes_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "All Files (*.*)|*.xlsx";
            fileDialog.FilterIndex = 1;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = fileDialog.FileName;
                Alert.SShow("Chargement en cours..... Veillez-patientez!", Alert.AlertType.Info);
                var table = await Task.Run(() => ImportSaleExcelAsync(selectedFile));
                File = table;
                dataGridView1.DataSource = table.Sheets.FirstOrDefault().Table;
            }
        }

        private async void BtnModifier_Click(object sender, EventArgs e)
        {
            var msg = new MsgBox();
            msg.show("Voulez-vous modifier ces éléments ?", "Modification", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
            msg.ShowDialog();
            if (msg.clicked == "Oui")
            {
                Alert.SShow("Modification en cours.... Veillez-patienez !", Alert.AlertType.Info);
                var result = await EditAsync(File.Sheets.FirstOrDefault().Produits);
                if (result)
                {
                    Alert.SShow("Element(s) modifié(s) avec succès.", Alert.AlertType.Sucess);
                }
            }
        }

        private async void BtnEditImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "All Files (*.*)|*.xlsx";
            fileDialog.FilterIndex = 1;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = fileDialog.FileName;
                Alert.SShow("Chargement en cours..... Veillez-patientez!", Alert.AlertType.Info);
                var table = await Task.Run(() => ImportExcelForEditAsync(selectedFile));
                File = table;
                dataGridView1.DataSource = table.Sheets.FirstOrDefault().Table;
            }
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            LoadTimer.Stop();
            Mesure = await GetMesureAsync();
        }

        private async void BtnEnregistrer_Click(object sender, EventArgs e)
        {
            var msg = new MsgBox();
            msg.show("Voulez-vous valider ces enregistrements ?", "Enregistrement", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
            msg.ShowDialog();
            if(msg.clicked == "Oui")
            {
                Alert.SShow("Enregistrement en cours.... Veillez-patienez !", Alert.AlertType.Info);
                var result = await SaveData(null, File.Sheets.FirstOrDefault().Produits);
                if (result)
                {
                    Alert.SShow("Element enregistré avec succès.", Alert.AlertType.Sucess);
                }
            }
        }

        private Task<string> GetMesureAsync()
        {
            return Task.Factory.StartNew(() => GetMesurePetit());
        }

        private string GetMesurePetit()
        {
            using(var donnée = new QuitayeContext())
            {
                var mesure = (from d in donnée.tbl_mesure_vente orderby d.Niveau select d.Nom).FirstOrDefault();
                return mesure;
            }
        }

        private async Task<bool> EditAsync(List<Produits> produits)
        {
            using(var donnée = new QuitayeContext())
            {
                foreach (var item in produits)
                {
                    var prod = donnée.tbl_produits.Where(x => x.Barcode == item.Barcode).FirstOrDefault();
                    if(prod != null)
                    {
                        prod.Prix_Petit = item.Prix_Unité;
                        var formule = donnée.tbl_formule_mesure_vente.Where(x => x.Id == prod.Formule_Stockage).FirstOrDefault();
                        prod.Prix_Moyen = item.Prix_Unité *formule.Petit;
                        await donnée.SaveChangesAsync();
                    }
                }
                return true;
            }
        }

        private async Task<(bool, System.Data.DataTable)> SaveSalesAsync(List<Sale> sales)
        {
            using (var donnée = new QuitayeContext())
            {
                var list_nom_reconnue = new List<VenteList>();
                var vente_list = new List<VenteList>();
                foreach (var item in sales.GroupBy(x => x.Num_Vente))
                {
                    AchatVente.num_vente = await AchatVente.Numero_Cmd();
                    vente_list = new List<VenteList>();
                    foreach (var itemd in sales.Where(x => x.Num_Vente == item.Key))
                    {
                        var stock = (from d in donnée.tbl_stock_produits_vente where d.Code_Barre == itemd.Barcode select d).FirstOrDefault();
                        if(stock != null)
                        {
                            vente_list.Add(new VenteList()
                            {
                                Catégorie = stock.Catégorie,
                                Marque = stock.Marque,
                                Code_Barre = itemd.Barcode,
                                Date = itemd.Date,
                                Mesure = "PIECE",
                                Prix_Unitaire = itemd.Prix_Unité,
                                Montant = itemd.Montant,
                                Filiale = "Siège",
                                Quantité = itemd.Quantité,
                                Taille = stock.Taille,
                                Type = stock.Type,
                                Reduction = itemd.Reduction,
                                Auteur = Principales.profile,
                                Num_Vente = itemd.Num_Vente,
                            });
                        }else
                        {
                            list_nom_reconnue.Add(new VenteList()
                            {
                                //Catégorie = stock.Catégorie,
                                //Marque = stock.Marque,
                                Code_Barre = itemd.Barcode,
                                Date = itemd.Date,
                                Mesure = "PIECE",
                                Prix_Unitaire = itemd.Prix_Unité,
                                Montant = itemd.Montant,
                                Filiale = "Siège",
                                Quantité = itemd.Quantité,
                                //Taille = stock.Taille,
                                //Type = stock.Type,
                                Reduction = itemd.Reduction,
                                Auteur = Principales.profile,
                                Num_Vente = itemd.Num_Vente
                            });
                        }
                        
                    }

                    //foreach (var items in vente_list)
                    //{
                    //    await AchatVente.SaveSingleAsync(items);
                    //}
                }

                AchatVente.num_vente = null;
                var dt = new System.Data.DataTable();
                dt.Columns.Add("Num_Vente");
                dt.Columns.Add("Barcode");
                dt.Columns.Add("Quantité");
                dt.Columns.Add("Prix_Unité");
                dt.Columns.Add("Montant");
                dt.Columns.Add("Date");
                foreach (var item in list_nom_reconnue)
                {
                    var row = dt.NewRow();
                    row[0] = item.Num_Vente;
                    row["Barcode"] = item.Code_Barre;
                    row["Quantité"] = item.Quantité;
                    row["Prix_Unité"] = item.Prix_Unitaire;
                    row["Montant"] = item.Montant;
                    row["Date"] = item.Date.Value.ToString("dd/MM/yyyy");

                    dt.Rows.Add(row);
                }


                return (true, dt);
            }
        }

        private async Task<bool> SaveData(System.Data.DataTable table, List<Produits> test = null)
        {
            using(var donnée = new QuitayeContext())
            {
                if(table != null)
                {
                    var rows = table.Rows;
                    foreach (DataRow item in rows)
                    {
                        var produit = item["Produit"].ToString();
                        var prix = Convert.ToDecimal(item["Prix_Unité"]);
                        var prix_carton = Convert.ToDecimal(item["Prix_Carton"]);
                        var marque = item["Marque"].ToString();
                        var emballage = item["Emballage"].ToString();
                        var words = produit.Split(' ');
                        var taille = words.Where(x => x.ToLower().EndsWith("ml") 
                        || x.ToLower().EndsWith("gm") 
                        || x.ToLower().EndsWith("cm")
                        || x.ToLower().EndsWith("mm")
                        || x.ToLower().EndsWith("kg")
                        || x.ToLower().EndsWith("l")).FirstOrDefault();
                        string input = produit;
                        //string pattern = @"(.+?)\s+(\b\w+\b)\s+(.+?(?=gm|ML))\s*(gm|ML)\s*-\s*(.*)";
                        //string replacement = marque;
                        //string output = Regex.Replace(input, pattern, replacement);
                        //output = Regex.Replace(input, pattern, taille);
                        var catégorie = item["Catégorie"].ToString();
                        var type = produit.Replace(marque, "");
                        if(!string.IsNullOrEmpty(taille))
                        type = type.Replace(taille, "");
                        type = type.Replace(catégorie, "");

                        type = type.TrimStart('-', ' ');

                        marque = marque.Trim();
                        catégorie = catégorie.Trim();
                        var formule_Id = 1;
                        var formule = donnée.tbl_formule_mesure_vente.Where(x => x.Formule == $"1/{emballage}").FirstOrDefault();
                        if (formule != null)
                        {
                            formule_Id = formule.Id;
                        }
                        else
                        {
                            var form = new Models.Context.tbl_formule_mesure_vente();
                            form.Formule = $"1/{emballage}";

                            form.Moyen = 1;
                            form.Petit = Convert.ToDecimal(emballage);
                            donnée.tbl_formule_mesure_vente.Add(form);
                            await donnée.SaveChangesAsync();
                            formule_Id = form.Id;
                        }

                        int pr_id = 1;
                        var prd = donnée.tbl_produits.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
#pragma warning disable CS0472 // Le résultat de l'expression est toujours le même, car une valeur de ce type n'est jamais égale à 'null'
                        if (prd != null)
                        {
                            pr_id = Convert.ToInt32(prd) + 1;
                        }
#pragma warning restore CS0472 // Le résultat de l'expression est toujours le même, car une valeur de ce type n'est jamais égale à 'null'
                        var p = new Models.Context.tbl_produits();
                        p.Id = pr_id;
                        p.Auteur = Principales.profile;
                        p.Catégorie = catégorie;
                        p.Taille = taille;
                        p.Prix_Petit = prix;
                        p.Prix_Moyen = prix_carton;
                        p.Nom = marque;
                        p.Mesure = Mesure;
                        donnée.tbl_produits.Add(p);
                        var st_id = 1;
                        var stocksd = donnée.tbl_stock_produits_vente.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
#pragma warning disable CS0472 // Le résultat de l'expression est toujours le même, car une valeur de ce type n'est jamais égale à 'null'
                        if (stocksd != null)
                        {
                            st_id = Convert.ToInt32(stocksd) + 1;
                        }
#pragma warning restore CS0472 // Le résultat de l'expression est toujours le même, car une valeur de ce type n'est jamais égale à 'null'
                        var stock = new Models.Context.tbl_stock_produits_vente();
                        stock.Catégorie = catégorie;
                        stock.Detachement = "Siège";
                        stock.Marque = marque;
                        stock.Formule = formule_Id;
                        stock.Taille = taille;
                        stock.Id = st_id;
                        stock.Quantité = 0;
                        donnée.tbl_stock_produits_vente.Add(stock);
                        await donnée.SaveChangesAsync();
                    }
                }
                Ok = true;

                foreach (Produits item in test)
                {
                    var produit = item.Details.Replace('-', ' ');
                    var prix = item.Prix_Unité;
                    var prix_carton = item.Prix_Carton;
                    var marque = item.Marque.Replace('-', ' ').Trim();
                    var emballage = item.Emballage;
                    var catégorie = item.Catégorie.Replace('-', ' ').Trim();
                    catégorie = catégorie.Replace('-', ' ');
                    catégorie = catégorie.Replace('_', ' ');
                    marque = marque.Replace('-', ' ');
                    marque = marque.Replace('_', ' ');
                    produit = produit.Replace('-', ' ');
                    produit = produit.Replace('_', ' ');
                    var words = produit.Split(' ');
                    var taille = words.Where(x => x.ToLower().EndsWith("ml") 
                    || x.ToLower().ToLower().EndsWith("gm")
                    || x.ToLower().EndsWith("cm")
                    || x.ToLower().EndsWith("mm")
                    || x.ToLower().EndsWith("kg")
                    || x.ToLower().EndsWith("l")).FirstOrDefault();
                    if (string.IsNullOrEmpty(taille))
                    {
                        words = produit.Split(' ');
                        taille = words.Where(x => x.ToLower().EndsWith("ml") 
                        || x.ToLower().ToLower().EndsWith("gm") 
                        || x.ToLower().EndsWith("cm") 
                        || x.ToLower().EndsWith("mm")
                        || x.ToLower().EndsWith("kg")
                        || x.ToLower().EndsWith("l")).FirstOrDefault();
                    }
                    // Find indices of marque and taille
                    var type = produit.Replace(marque, "");
                    if (!string.IsNullOrEmpty(taille))
                        type = type.Replace(taille, "");

                    type = type.Replace(catégorie, "");
                    type = type.TrimStart(' ');

                    if (!string.IsNullOrEmpty(type))
                        type = type.Trim();
                    if(!string.IsNullOrEmpty(taille))
                        taille = taille.Trim();
                    if(!string.IsNullOrEmpty(marque))
                        marque = marque.Trim();
                    if (!string.IsNullOrEmpty(catégorie))
                        catégorie = catégorie.Trim();
                    await NewProduct(type, emballage, marque, catégorie, taille, prix, prix_carton, Mesure);
                }
                return true;
            }
        }

        public static async Task<bool> NewProduct(string type, string emballage, string marque, string catégorie, string taille, decimal prix, decimal prix_carton, string mesure, int formuleid= 0, string barcode = null)
        {
            using(var donnée = new QuitayeContext())
            {
                var formule_Id = 1;
                if (formuleid == 0)
                {
                    var formuleString = string.Format("1/{0}", emballage);
                    var formule = donnée.tbl_formule_mesure_vente.Where(x => x.Formule == formuleString)
                        .Select(f => new { Id = f.Id });
                    if (formule.FirstOrDefault() != null)
                    {
                        formule_Id = formule.FirstOrDefault().Id;
                    }
                    else
                    {
                        var form = new Models.Context.tbl_formule_mesure_vente();
                        form.Formule = $"1/{emballage}";
                        form.Moyen = 1;
                        form.Petit = Convert.ToDecimal(emballage);
                        donnée.tbl_formule_mesure_vente.Add(form);
                        await donnée.SaveChangesAsync();
                        formule_Id = form.Id;
                    }
                }
                else
                formule_Id = formuleid;
                
                var check = donnée.tbl_produits.Where(x => x.Nom == marque
                && x.Taille == taille && x.Type == type
                && x.Catégorie == catégorie).FirstOrDefault();
                if (check == null)
                {
                    int pr_id = 1;
                    var prd = donnée.tbl_produits.OrderByDescending(x => x.Id).Select(x => x.Id).ToList().FirstOrDefault();
#pragma warning disable CS0472 // Le résultat de l'expression est toujours le même, car une valeur de ce type n'est jamais égale à 'null'
                    if (prd != null)
                    {
                        pr_id = Convert.ToInt32(prd) + 1;
                    }
#pragma warning restore CS0472 // Le résultat de l'expression est toujours le même, car une valeur de ce type n'est jamais égale à 'null'
                    var p = new Models.Context.tbl_produits();
                    p.Id = pr_id;
                    p.Auteur = Principales.profile;
                    p.Catégorie = catégorie;
                    p.Taille = taille;
                    p.Prix_Petit = prix;
                    p.Prix_Moyen = prix_carton;
                    p.Nom = marque;
                    p.Mesure = mesure;
                    p.Formule_Stockage = formule_Id;
                    p.Type = type;
                    p.Barcode = barcode;
                    donnée.tbl_produits.Add(p);
                    var st_id = 1;
                    var chdsd = donnée.tbl_stock_produits_vente.Where(x => x.Marque == marque &&  x.Catégorie == catégorie 
                    && x.Taille == taille && x.Type == type).FirstOrDefault();
                    if(chdsd == null) 
                    {
                        var stocksd = donnée.tbl_stock_produits_vente.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
#pragma warning disable CS0472 // Le résultat de l'expression est toujours le même, car une valeur de ce type n'est jamais égale à 'null'
                        if (stocksd != null)
                        {
                            st_id = Convert.ToInt32(stocksd) + 1;
                        }
#pragma warning restore CS0472 // Le résultat de l'expression est toujours le même, car une valeur de ce type n'est jamais égale à 'null'
                        var stock = new Models.Context.tbl_stock_produits_vente();
                        stock.Catégorie = catégorie;
                        stock.Detachement = "Siège";
                        stock.Marque = marque;
                        stock.Formule = formule_Id;
                        stock.Taille = taille;
                        stock.Type = type;
                        stock.Id = st_id;
                        stock.Code_Barre = barcode;
                        stock.Quantité = 0;
                        donnée.tbl_stock_produits_vente.Add(stock);
                    }
                    
                    var checkcategory = donnée.tbl_catégorie.Where(x => x.Catégorie == catégorie).FirstOrDefault();
                    if (checkcategory == null)
                    {
                        int catégory_id = 1;
                        var chd = donnée.tbl_catégorie.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                        catégory_id = chd + 1;
                        var category = new Models.Context.tbl_catégorie();
                        category.Catégorie = catégorie;
                        category.Date_Ajout = DateTime.Now;
                        category.Auteur = Principales.profile;
                        category.Id = catégory_id;
                        donnée.tbl_catégorie.Add(category);
                    }


                    var checkmarque = donnée.tbl_marque.Where(x => x.Nom == marque).FirstOrDefault();
                    if (checkmarque == null)
                    {
                        int catégory_id = 1;
                        var chd = donnée.tbl_marque.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                        catégory_id = chd + 1;
                        var category = new Models.Context.tbl_marque();
                        category.Nom = marque;
                        category.Date = DateTime.Now;
                        category.Auteur = Principales.profile;
                        category.Id = catégory_id;
                        donnée.tbl_marque.Add(category);
                    }

                    if (!string.IsNullOrEmpty(taille))
                    {
                        var checktaille = donnée.tbl_taille.Where(x => x.Taille == taille).FirstOrDefault();
                        if (checktaille == null)
                        {
                            int catégory_id = 1;
                            var chd = donnée.tbl_taille.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                            catégory_id = chd + 1;
                            var category = new Models.Context.tbl_taille();
                            category.Taille = taille;
                            category.Date_Ajout = DateTime.Now;
                            category.Auteur = Principales.profile;
                            category.Id = catégory_id;
                            donnée.tbl_taille.Add(category);
                        }
                    }

                    if (!string.IsNullOrEmpty(type))
                    {
                        var checktype = donnée.tbl_type.Where(x => x.Type == type).FirstOrDefault();
                        if (checktype == null)
                        {
                            int catégory_id = 1;
                            var chd = donnée.tbl_type.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                            catégory_id = chd + 1;
                            var category = new Models.Context.tbl_type();
                            category.Type = type;
                            category.Date_Ajout = DateTime.Now;
                            category.Auteur = Principales.profile;
                            category.Id = catégory_id;
                            donnée.tbl_type.Add(category);
                        }
                    }
                    if(!string.IsNullOrEmpty(barcode))
                    {
                        var multi_id = 1;
                        var rds = donnée.tbl_multi_barcode.OrderByDescending(x => x.Id).FirstOrDefault();
                        if (rds != null)
                        {
                            multi_id = rds.Id + 1;
                        }


                        
                        var prod_id = donnée.tbl_produits.Where(x => x.Barcode == barcode).Select(x => x.Id).FirstOrDefault();
                        if(prod_id != 0)
                        {
                            var multi = donnée.tbl_multi_barcode.Add(new tbl_multi_barcode() { Barcode = barcode, Id = multi_id, Product_Id = prod_id });
                            await donnée.SaveChangesAsync();
                            var stoc = donnée.tbl_stock_produits_vente.Where(x => x.Id == st_id).FirstOrDefault();
                            stoc.Product_Id = prod_id;
                            await donnée.SaveChangesAsync();
                        }
                        
                    }
                }
                else
                {
                    if(check.Barcode == null)
                    {
                        var cds = donnée.tbl_produits.Where(x => x.Barcode == barcode).FirstOrDefault();
                        if(cds == null)
                        {
                            check.Barcode = barcode;
                            await donnée.SaveChangesAsync();
                        }
                    }
                }
            }
            return true;
        }

        public bool Ok { get; set; }

        private async void BtnImporterExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "All Files (*.*)|*.xlsx";
            fileDialog.FilterIndex = 1;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = fileDialog.FileName;
                Alert.SShow("Chargement en cours..... Veillez-patientez!", Alert.AlertType.Info);
                var table = await Task.Run(() => ImportExcelAsync(selectedFile));
                File = table;
                dataGridView1.DataSource = table.Sheets.FirstOrDefault().Table;
            }
        }

#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        private async Task<ExcelFile> ImportExcelAsync(string filepath)
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        {
            var file = new ExcelFile();
            file.Sheets = new List<ExcelSheet>();

            using (var spreadsheetDocument = SpreadsheetDocument.Open(filepath, false))
            {
                var workbookPart = spreadsheetDocument.WorkbookPart;

                // Get the sheet names
                var sheets = workbookPart.Workbook.Sheets;
                var sheetNames = sheets.Cast<Sheet>().Select(s => s.Name.Value).ToList();

                foreach (string sheetName in sheetNames)
                {
                    var excelSheet = new ExcelSheet();
                    excelSheet.SheetName = sheetName;
                    excelSheet.ColumnNames = new List<string>();
                    excelSheet.Produits = new List<Produits>();

                    var worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheets.Cast<Sheet>().First(s => s.Name.Value == sheetName).Id);
                    var worksheet = worksheetPart.Worksheet;

                    // Get the sheet data
                    var rows = worksheet.GetFirstChild<SheetData>().Elements<Row>();

                    var dataTable = new System.Data.DataTable();

                    // Get the column names
                    foreach (Cell cell in rows.ElementAt(0))
                    {
                        string columnName = GetCellValue(spreadsheetDocument, cell);
                        excelSheet.ColumnNames.Add(columnName);
                        dataTable.Columns.Add(new DataColumn(columnName));
                    }

                    // Get the row data
                    foreach (Row row in rows.Skip(1))
                    {
                        var dataRow = dataTable.NewRow();
                        var values = new List<object>();

                        // Keep track of the last cell index that was processed
                        int lastCellIndex = 0;

                        // Loop through all the cells in the row, including the empty ones
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            // Get the cell index
                            int currentCellIndex = GetColumnIndex(cell.CellReference);

                            // Add empty strings for any empty cells between the last cell and the current cell
                            for (int i = lastCellIndex + 1; i < currentCellIndex; i++)
                            {
                                values.Add(string.Empty);
                            }

                            // Get the cell value
                            string cellValue = GetCellValue(spreadsheetDocument, cell);

                            // Add the cell value to the values list
                            values.Add(cellValue);

                            // Update the last cell index
                            lastCellIndex = currentCellIndex;
                        }

                        // Add empty strings for any remaining empty cells at the end of the row
                        for (int i = lastCellIndex + 1; i <= row.Descendants<Cell>().Count(); i++)
                        {
                            values.Add(string.Empty);
                        }

                        // Set the values of the DataRow and add it to the DataTable
                        dataRow.ItemArray = values.ToArray();
                        dataTable.Rows.Add(dataRow);
                        var emb = "1";
                        if (Convert.ToInt32(values[2].ToString()) == 0)
                            values[20] = emb;
                        excelSheet.Produits.Add(new Produits()
                        {
                            Details = values[0].ToString(),
                            Prix_Carton = Convert.ToDecimal(values[5]),
                            Marque = values[1].ToString(),
                            Prix_Unité = Convert.ToDecimal(values[4]),
                            Emballage = values[3].ToString(),
                            Catégorie = values[2].ToString()
                        });
                    }

                    excelSheet.Table = dataTable;
                    file.Sheets.Add(excelSheet);
                }
            }

            return file;
        }

        private async Task<ExcelFile> ImportExcelForEditAsync(string filepath)
        {
            var file = new ExcelFile();
            file.Sheets = new List<ExcelSheet>();

            using (var spreadsheetDocument = SpreadsheetDocument.Open(filepath, false))
            {
                var workbookPart = spreadsheetDocument.WorkbookPart;

                // Get the sheet names
                var sheets = workbookPart.Workbook.Sheets;
                var sheetNames = sheets.Cast<Sheet>().Select(s => s.Name.Value).ToList();

                foreach (string sheetName in sheetNames)
                {
                    var excelSheet = new ExcelSheet();
                    excelSheet.SheetName = sheetName;
                    excelSheet.ColumnNames = new List<string>();
                    excelSheet.Produits = new List<Produits>();

                    var worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheets.Cast<Sheet>().First(s => s.Name.Value == sheetName).Id);
                    var worksheet = worksheetPart.Worksheet;

                    // Get the sheet data
                    var rows = worksheet.GetFirstChild<SheetData>().Elements<Row>();

                    var dataTable = new System.Data.DataTable();

                    // Get the column names
                    foreach (Cell cell in rows.ElementAt(0))
                    {
                        string columnName = GetCellValue(spreadsheetDocument, cell);
                        excelSheet.ColumnNames.Add(columnName);
                        dataTable.Columns.Add(new DataColumn(columnName));
                    }

                    // Get the row data
                    foreach (Row row in rows.Skip(1))
                    {
                        var dataRow = dataTable.NewRow();
                        var values = new List<object>();

                        // Keep track of the last cell index that was processed
                        int lastCellIndex = 0;

                        // Loop through all the cells in the row, including the empty ones
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            // Get the cell index
                            int currentCellIndex = GetColumnIndex(cell.CellReference);

                            // Add empty strings for any empty cells between the last cell and the current cell
                            for (int i = lastCellIndex + 1; i < currentCellIndex; i++)
                            {
                                values.Add(string.Empty);
                            }

                            // Get the cell value
                            string cellValue = GetCellValue(spreadsheetDocument, cell);

                            // Add the cell value to the values list
                            values.Add(cellValue);

                            // Update the last cell index
                            lastCellIndex = currentCellIndex;
                        }

                        // Add empty strings for any remaining empty cells at the end of the row
                        for (int i = lastCellIndex + 1; i <= row.Descendants<Cell>().Count(); i++)
                        {
                            values.Add(string.Empty);
                        }

                        // Set the values of the DataRow and add it to the DataTable
                        dataRow.ItemArray = values.ToArray();
                        dataTable.Rows.Add(dataRow);
                        decimal prix_carton = 0;

                        //if (!string.IsNullOrEmpty(values[3].ToString()))
                        //{
                        //    prix_carton = Convert.ToDecimal(values[3]);
                        //}

                        excelSheet.Produits.Add(new Produits()
                        {
                            Barcode = values[0].ToString(),
                            Details = values[1].ToString(),
                            Prix_Carton =prix_carton,
                            Prix_Unité = Convert.ToDecimal(values[2]),
                        });
                    }

                    excelSheet.Table = dataTable;
                    file.Sheets.Add(excelSheet);
                }
            }

            return file;
        }

        private async Task<ExcelFile> ImportSaleExcelAsync(string filepath)
        {
            var file = new ExcelFile();
            file.Sheets = new List<ExcelSheet>();

            using (var spreadsheetDocument = SpreadsheetDocument.Open(filepath, false))
            {
                var workbookPart = spreadsheetDocument.WorkbookPart;

                // Get the sheet names
                var sheets = workbookPart.Workbook.Sheets;
                var sheetNames = sheets.Cast<Sheet>().Select(s => s.Name.Value).ToList();

                foreach (string sheetName in sheetNames)
                {
                    var excelSheet = new ExcelSheet();
                    excelSheet.SheetName = sheetName;
                    excelSheet.ColumnNames = new List<string>();
                    excelSheet.Sales = new List<Sale>();

                    var worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheets.Cast<Sheet>().First(s => s.Name.Value == sheetName).Id);
                    var worksheet = worksheetPart.Worksheet;

                    // Get the sheet data
                    var rows = worksheet.GetFirstChild<SheetData>().Elements<Row>();

                    var dataTable = new System.Data.DataTable();

                    // Get the column names
                    foreach (Cell cell in rows.ElementAt(0))
                    {
                        string columnName = GetCellValue(spreadsheetDocument, cell);
                        excelSheet.ColumnNames.Add(columnName);
                        dataTable.Columns.Add(new DataColumn(columnName));
                    }

                    // Get the row data
                    foreach (Row row in rows.Skip(1))
                    {
                        var dataRow = dataTable.NewRow();
                        var values = new List<object>();

                        // Keep track of the last cell index that was processed
                        int lastCellIndex = 0;

                        // Loop through all the cells in the row, including the empty ones
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            // Get the cell index
                            int currentCellIndex = GetColumnIndex(cell.CellReference);

                            // Add empty strings for any empty cells between the last cell and the current cell
                            for (int i = lastCellIndex + 1; i < currentCellIndex; i++)
                            {
                                values.Add(string.Empty);
                            }

                            // Get the cell value
                            string cellValue = GetCellValue(spreadsheetDocument, cell);

                            // Add the cell value to the values list
                            values.Add(cellValue);

                            // Update the last cell index
                            lastCellIndex = currentCellIndex;
                        }

                        // Add empty strings for any remaining empty cells at the end of the row
                        for (int i = lastCellIndex + 1; i <= row.Descendants<Cell>().Count(); i++)
                        {
                            values.Add(string.Empty);
                        }

                        // Set the values of the DataRow and add it to the DataTable
                        dataRow.ItemArray = values.ToArray();
                        dataTable.Rows.Add(dataRow);
                        decimal prix_carton = 0;

                        //if (!string.IsNullOrEmpty(values[3].ToString()))
                        //{
                        //    prix_carton = Convert.ToDecimal(values[3]);
                        //}

                        excelSheet.Sales.Add(new Sale()
                        {
                            Barcode = values[1].ToString(),
                            Num_Vente = values[0].ToString(),
                            Date = DateTime.FromOADate(Convert.ToDouble(values[5].ToString())),
                            Prix_Unité = Convert.ToDecimal(values[3]),
                            Quantité = Convert.ToDecimal(values[2]),
                            Montant = Convert.ToDecimal(values[4]),
                            Reduction = Convert.ToDecimal(values[6]),
                        });
                    }

                    excelSheet.Table = dataTable;
                    file.Sheets.Add(excelSheet);
                }
            }

            return file;
        }


        private static int GetColumnIndex(string cellReference)
        {
            // Remove the row number from the cell reference
            string columnName = Regex.Replace(cellReference.ToUpper(), @"[\d]", string.Empty);

            int columnIndex = 0;
            int factor = 1;

            // Loop through the column name from right to left and calculate the column index
            for (int i = columnName.Length - 1; i >= 0; i--)
            {
                char c = columnName[i];
                columnIndex += factor * (c - 'A' + 1);
                factor *= 26;
            }

            return columnIndex;
        }

        private static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            string value = null;
            if (cell.CellValue != null)
            {
                value = cell.CellValue.InnerXml;

                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    int index = int.Parse(value);
                    SharedStringTablePart sharedStringTablePart = document.WorkbookPart.SharedStringTablePart;
                    value = sharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(index).InnerText;
                }
            }
            return value;
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class ExcelFile
    {
        public List<ExcelSheet> Sheets { get; set; }
    }
    public class ExcelSheet
    {
        public string SheetName { get; set; }
        public List<string> ColumnNames { get; set; }
        public System.Data.DataTable Table { get; set; }
        public List<Produits> Produits { get; set; }
        public List<Sale> Sales { get; set; }
    }

    public class Produits
    {
        public string Details { get; set; }
        public string Barcode { get; set; }
        public decimal Prix_Unité { get; set; }
        public decimal Prix_Carton { get; set; }
        public string Marque { get; set; }
        public string Emballage { get; set; }
        public string Catégorie { get; set; }
    }
    public class Sale
    {
        public string Num_Vente { get; set; }
        public string Barcode { get; set; }
        public decimal Quantité { get; set; }
        public decimal Prix_Unité { get; set; }
        public decimal Montant { get; set; }
        public DateTime Date { get; set; }
        public decimal Reduction { get; set; }
    }
}
