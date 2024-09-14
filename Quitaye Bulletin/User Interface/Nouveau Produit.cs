using FontAwesome.Sharp;
using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using Quitaye_School.User_Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Quitaye_School.User_Interface
{
    public partial class Nouveau_Produit : Form
    {
        private N_Produit produit = new N_Produit();
        private int id;
        private Timer loadTimer = new Timer();
        private string filepath = "";
        private string filename = "";
        private byte[] ImageByteArray;
        public string ok;
        private List<Custom_Prix> list_prix = new List<Custom_Prix>();
        private List<Custom_Prix> list_prix_grossiste = new List<Custom_Prix>();
        private List<Custom_Prix> list_prix_achat = new List<Custom_Prix>();
        private bool callus = true;
        private bool callcat = true;
        private bool calltai = true;
        private bool callmar = true;
        private bool callcode = true;
        private bool calltype = true;
        public Nouveau_Produit(N_Produit _produit = null)
        {
            InitializeComponent();
            list_prix = new List<Custom_Prix>();
            if (_produit != null)
            {
                id = _produit.Id;
                produit = _produit;
                btnAddCode.Visible = true;
                if (!string.IsNullOrEmpty(produit.Code_Barre)) 
                    cbxCode.Enabled = false;
            }
            btnFermer.Click += BtnFermer_Click;
            btnAjouter.Click += BtnAjouter_Click;
            btnImage.DoubleClick += BtnImage_DoubleClick;
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            btnAddCatégorie.Click += BtnAddCatégorie_Click;
            btnAddMarque.Click += BtnAddMarque_Click;
            btnAddTaille.Click += BtnAddTaille_Click;
            btnAddCode.Click += BtnAddCode_Click;
            btnAddUsage.Click += BtnAddUsage_Click;
            btnAddFormule.Click += BtnAddFormule_Click;
            txtmin.TextChanged += Txtmin_TextChanged;
            txtmax.TextChanged += Txtmax_TextChanged;
            txtStock.TextChanged += TxtStock_TextChanged;
            btnCodeBarre.Click += BtnCodeBarre_Click;
            txtmax.KeyPress += Txt_KeyPress;
            txtStock.KeyPress += Txt_KeyPress;
            txtmin.KeyPress += Txt_KeyPress;
            //cbxCode.KeyPress += CbxCode_KeyPress;
            btnAddType.Click += BtnAddType_Click;            
        }

        private void CbxCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private async void BtnAddCode_Click(object sender, EventArgs e)
        {
            Ajouter_Element element = new Ajouter_Element("Code", Product_Id.ToString(), cbxCode.Text);
            int num = (int)element.ShowDialog();
            if (!(element.ok == "Oui"))
            {
                element = (Ajouter_Element)null;
            }
            else
            {
                await CallCodeAsync(Product_Id.ToString());
                element = (Ajouter_Element)null;
            }
        }

        private async void BtnAddType_Click(object sender, EventArgs e)
        {
            Ajouter_Element element = new Ajouter_Element("Type");
            int num = (int)element.ShowDialog();
            if (!(element.ok == "Oui"))
            {
                element = (Ajouter_Element)null;
            }
            else
            {
                await CallTypelAsync();
                element = (Ajouter_Element)null;
            }
        }

        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == '-' || e.KeyChar == '.')
                return;
            e.Handled = true;
        }

        private void BtnCodeBarre_Click(object sender, EventArgs e)
        {
            //Code_Barre codeBarre = new Code_Barre();
            //if (cbxCode.Text != "")
            //{
            //    codeBarre.txtcode.Text = cbxCode.Text;
            //    codeBarre.txtcode.ReadOnly = true;
            //}
            //int num = (int)codeBarre.ShowDialog();
            //codeBarre.Dispose();
            //if (!(cbxCode.Text != ""))
            //    return;
            //cbxCode.Enabled = false;
        }

        private async void BtnAddFormule_Click(object sender, EventArgs e)
        {
            Ajouter_Formule_Vente formule = new Ajouter_Formule_Vente();
            int num = (int)formule.ShowDialog();
            if (!(formule.ok == "Oui"))
            {
                formule = null;
            }
            else
            {
                await CallFormule();
                formule = null;
            }
        }

        private async Task CallPrix()
        {
            List<Custom_Prix> customPrixList1 = await List_Prix_Async();
            list_prix = customPrixList1;
            customPrixList1 = null;
            flowLayoutPanel1.Controls.Clear();
            foreach (Custom_Prix item in list_prix)
            {
                if (item.Type == "Petit")
                    item.Prix_Unité = produit.Prix_Petit;
                else if (item.Type == "Moyen")
                    item.Prix_Unité = produit.Prix_Moyen;
                else if (item.Type == "Grand")
                    item.Prix_Unité = produit.Prix_Grand;
                else if (item.Type == "Large")
                    item.Prix_Unité = produit.Prix_Large;
                else if (item.Type == "Hyper Large")
                    item.Prix_Unité = produit.Prix_Hyper_Large;
                item.Width = flowLayoutPanel1.Width - 25;
                flowLayoutPanel1.Controls.Add(item);
            }
            List<Custom_Prix> customPrixList2 = await List_Prix_Async();
            list_prix_achat = customPrixList2;
            customPrixList2 = null;
            flowLayoutPanel3.Controls.Clear();
            foreach (Custom_Prix item in list_prix_achat)
            {
                if (item.Type == "Petit")
                    item.Prix_Unité = produit.Prix_Achat_Petit;
                else if (item.Type == "Moyen")
                    item.Prix_Unité = produit.Prix_Achat_Moyen;
                else if (item.Type == "Grand")
                    item.Prix_Unité = produit.Prix_Achat_Grand;
                else if (item.Type == "Large")
                    item.Prix_Unité = produit.Prix_Achat_Large;
                else if (item.Type == "Hyper Large")
                    item.Prix_Unité = produit.Prix_Achat_Hyper_Large;
                item.Width = flowLayoutPanel3.Width - 25;
                flowLayoutPanel3.Controls.Add((Control)item);
            }
            List<Custom_Prix> customPrixList3 = await List_Prix_Async();
            list_prix_grossiste = customPrixList3;
            customPrixList3 = null;
            flowLayoutPanel2.Controls.Clear();
            foreach (Custom_Prix item in list_prix_grossiste)
            {
                if (item.Type == "Petit")
                    item.Prix_Unité = produit.Prix_Petit_Grossiste;
                else if (item.Type == "Moyen")
                    item.Prix_Unité = produit.Prix_Moyen_Grossiste;
                else if (item.Type == "Grand")
                    item.Prix_Unité = produit.Prix_Grand_Grossiste;
                else if (item.Type == "Large")
                    item.Prix_Unité = produit.Prix_Large_Grossiste;
                else if (item.Type == "Hyper Large")
                    item.Prix_Unité = produit.Prix_Hyper_Large_Grossiste;
                item.Width = flowLayoutPanel2.Width - 25;
                flowLayoutPanel2.Controls.Add((Control)item);
            }
        }

        private Task<List<Custom_Prix>> List_Prix_Async() => Task.Factory.StartNew(() => List_Prix());

        private List<Custom_Prix> List_Prix()
        {
            var customPrixList = new List<Custom_Prix>();
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_mesure_vente.OrderBy((d => d.Niveau)).Select(d => new
                {
                    Id = d.Id,
                    Type = d.Type,
                    Nom = d.Nom
                });
                if (source.Count() != 0)
                {
                    foreach (var data in source)
                        customPrixList.Add(new Custom_Prix()
                        {
                            Id = data.Id,
                            Type = data.Type,
                            Mesure = data.Nom
                        });
                }
            }
            return customPrixList;
        }

        private void TxtStock_TextChanged(object sender, EventArgs e)
        {
            if (!(txtStock.Text != ""))
                return;
            txtStock.Text = Convert.ToDecimal(txtStock.Text).ToString("N0");
            txtStock.SelectionStart = txtStock.Text.Length;
        }

        private void Txtmax_TextChanged(object sender, EventArgs e)
        {
            if (!(txtmax.Text != ""))
                return;
            txtmax.Text = Convert.ToDecimal(txtmax.Text).ToString("N0");
            txtmax.SelectionStart = txtmax.Text.Length;
        }

        private void Txtmin_TextChanged(object sender, EventArgs e)
        {
            if (!(txtmin.Text != ""))
                return;
            txtmin.Text = Convert.ToDecimal(txtmin.Text).ToString("N0");
            txtmin.SelectionStart = txtmin.Text.Length;
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            if (produit != null)
            {
                string nom = produit.Nom;
                await CallTask(produit);
            }
            else
                await CallTask((N_Produit)null);
            await CallPrix();
        }

        private async void BtnAddTaille_Click(object sender, EventArgs e)
        {
            Ajouter_Element element = new Ajouter_Element("Taille");
            int num = (int)element.ShowDialog();
            if (!(element.ok == "Oui"))
            {
                element = (Ajouter_Element)null;
            }
            else
            {
                await CallTaillAsync();
                element = (Ajouter_Element)null;
            }
        }

        private async void BtnAddUsage_Click(object sender, EventArgs e)
        {
            Ajouter_Mesure_Vente element = new Ajouter_Mesure_Vente();
            int num = (int)element.ShowDialog();
            if (!(element.ok == "Oui"))
            {
                element = (Ajouter_Mesure_Vente)null;
            }
            else
            {
                await CallUsageAsync();
                await CallPrix();
                element = (Ajouter_Mesure_Vente)null;
            }
        }

        private async void BtnAddCatégorie_Click(object sender, EventArgs e)
        {
            Ajouter_Element element = new Ajouter_Element("Catégorie");
            int num = (int)element.ShowDialog();
            if (!(element.ok == "Oui"))
            {
                element = (Ajouter_Element)null;
            }
            else
            {
                await CallCatégorieAsync();
                element = (Ajouter_Element)null;
            }
        }

        private async void BtnAddMarque_Click(object sender, EventArgs e)
        {
            Ajouter_Element element = new Ajouter_Element("Marque");
            int num = (int)element.ShowDialog();
            if (!(element.ok == "Oui"))
            {
                element = (Ajouter_Element)null;
            }
            else
            {
                await CallMarqueAsync();
                element = (Ajouter_Element)null;
            }
        }

        private void BtnImage_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (!(sender is IconPictureBox iconPictureBox))
                return;
            openFileDialog.Filter = "(*.jpg; *.jpeg;*bmp)| *.jpg; *.jpeg; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filepath = openFileDialog.FileName;
                iconPictureBox.Image = Image.FromFile(openFileDialog.FileName);
                filename = Path.GetFileName(openFileDialog.FileName);
            }
        }

        private async void BtnAjouter_Click(object sender, EventArgs e)
        {
            if (!(cbxCode.Text != "") || !(cbxCatégorie.Text != "") || !(cbxNom.Text != "")  || !(cbxFormuleStockage.Text != ""))
                return;
            if (btnAjouter.IconChar == IconChar.Add)
            {
                string min = Regex.Replace(txtmin.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                string max = Regex.Replace(txtmax.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                N_Produit produit = new N_Produit();
                produit.Catégorie = cbxCatégorie.Text;
                if (!string.IsNullOrEmpty(cbxTaille.Text))
                    produit.Taille = cbxTaille.Text;
                produit.Code_Barre = cbxCode.Text.ToUpper();
                produit.Stock_Max = 0;
                produit.Stock_Min = 0;
                produit.Nom = cbxNom.Text;
                produit.Quantité = 0;
                produit.Mesure = cbxUsage.Text;
                produit.Date = DateTime.Now;
                produit.Auteur = Principales.profile;
                if(!string.IsNullOrEmpty(cbxType.Text))
                produit.Type = cbxType.Text;
                var prs = list_prix.Where((d => d.Prix_Unité != 0M));
                if (prs.Count() != 0)
                {
                    int list_count = list_prix_achat.Count;
                    foreach (Custom_Prix item in list_prix.Take(list_count))
                    {
                        if (item.Type == "Petit")
                            produit.Prix_Petit = item.Prix_Unité;
                        else if (item.Type == "Moyen")
                            produit.Prix_Moyen = item.Prix_Unité;
                        else if (item.Type == "Grand")
                            produit.Prix_Grand = item.Prix_Unité;
                        else if (item.Type == "Large")
                            produit.Prix_Large = item.Prix_Unité;
                        else if (item.Type == "Hyper Large")
                            produit.Prix_Hyper_Large = item.Prix_Unité;
                    }
                    foreach (Custom_Prix item in list_prix_grossiste)
                    {
                        if (item.Type == "Petit")
                            produit.Prix_Petit_Grossiste = item.Prix_Unité;
                        else if (item.Type == "Moyen")
                            produit.Prix_Moyen_Grossiste = item.Prix_Unité;
                        else if (item.Type == "Grand")
                            produit.Prix_Grand_Grossiste = item.Prix_Unité;
                        else if (item.Type == "Large")
                            produit.Prix_Large_Grossiste = item.Prix_Unité;
                        else if (item.Type == "Hyper Large")
                            produit.Prix_Hyper_Large_Grossiste = item.Prix_Unité;
                    }
                    foreach (Custom_Prix item in list_prix_achat)
                    {
                        if (item.Type == "Petit")
                            produit.Prix_Achat_Petit = item.Prix_Unité;
                        else if (item.Type == "Moyen")
                            produit.Prix_Achat_Moyen = item.Prix_Unité;
                        else if (item.Type == "Grand")
                            produit.Prix_Achat_Grand = item.Prix_Unité;
                        else if (item.Type == "Large")
                            produit.Prix_Achat_Large = item.Prix_Unité;
                        else if (item.Type == "Hyper Large")
                            produit.Prix_Achat_Hyper_Large = item.Prix_Unité;
                    }
                    if (filepath != "")
                    {
                        Bitmap temp = new Bitmap(btnImage.Image);
                        MemoryStream strm = new MemoryStream();
                        if (filename.EndsWith(".jpg") || filename.EndsWith(".jpeg"))
                            temp.Save(strm, ImageFormat.Jpeg);
                        else if (filename.EndsWith(".png"))
                            temp.Save(strm, ImageFormat.Png);
                        else if (filename.EndsWith(".bmp"))
                            temp.Save(strm, ImageFormat.Bmp);
                        else
                            temp.Save(strm, ImageFormat.Jpeg);
                        ImageByteArray = strm.ToArray();
                        produit.Image_Byte = ImageByteArray;
                        temp = null;
                        strm = null;
                    }
                    else
                        produit.Image_Byte = null;
                    produit.Mesure = cbxUsage.Text;
                    produit.Formule = Convert.ToInt32(cbxFormuleStockage.SelectedValue);
                    if (await AddProductAsync(produit))
                    {
                        Alert.SShow("Produit Ajouter avec succès.", Alert.AlertType.Sucess);
                        cbxCode.Text = null;
                        ok = "Oui";
                        filepath = "";
                        filename = "";
                        btnImage.IconChar = IconChar.Image;
                        txtmax.Clear();
                        cbxType.Text = null;
                        txtmin.Clear();
                        cbxNom.Text = null;
                        cbxTaille.Text = null;
                        cbxUsage.Text = null;
                        cbxFormuleStockage.Text = null;
                        cbxCatégorie.Text = null;
                        txtStock.Clear();
                    }
                    else Alert.SShow("Un produit avec le même code barre existe déjà dans l'inventaire.", Alert.AlertType.Info);
                }
                min = null;
                max = null;
                produit = null;
                prs = null;
            }
            else if (btnAjouter.IconChar == IconChar.Edit)
            {
                string min = Regex.Replace(txtmin.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                string max = Regex.Replace(txtmax.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                string qté = Regex.Replace(txtStock.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                N_Produit produit = new N_Produit();
                produit.Catégorie = cbxCatégorie.Text;
                if (!string.IsNullOrEmpty(cbxTaille.Text))
                    produit.Taille = cbxTaille.Text;
                else produit.Taille = null;
                produit.Code_Barre = cbxCode.Text.ToUpper();
                produit.Stock_Max = Convert.ToDecimal(max);
                produit.Stock_Min = Convert.ToDecimal(min);
                produit.Nom = cbxNom.Text;
                produit.Quantité = Convert.ToDecimal(qté);
                produit.Mesure = cbxUsage.Text;
                produit.Date = DateTime.Now;
                produit.Id = id;
                produit.Auteur = Principales.profile;
                var prs = list_prix.Where((d => d.Prix_Unité != 0M));
                if (prs.Count() != 0)
                {
                    int list_count = list_prix_achat.Count;
                    foreach (Custom_Prix item in list_prix.Take(list_count))
                    {
                        if (item.Type == "Petit")
                            produit.Prix_Petit = item.Prix_Unité;
                        else if (item.Type == "Moyen")
                            produit.Prix_Moyen = item.Prix_Unité;
                        else if (item.Type == "Grand")
                            produit.Prix_Grand = item.Prix_Unité;
                        else if (item.Type == "Large")
                            produit.Prix_Large = item.Prix_Unité;
                        else if (item.Type == "Hyper Large")
                            produit.Prix_Hyper_Large = item.Prix_Unité;
                    }
                    foreach (Custom_Prix item in list_prix_achat)
                    {
                        if (item.Type == "Petit")
                            produit.Prix_Achat_Petit = item.Prix_Unité;
                        else if (item.Type == "Moyen")
                            produit.Prix_Achat_Moyen = item.Prix_Unité;
                        else if (item.Type == "Grand")
                            produit.Prix_Achat_Grand = item.Prix_Unité;
                        else if (item.Type == "Large")
                            produit.Prix_Achat_Large = item.Prix_Unité;
                        else if (item.Type == "Hyper Large")
                            produit.Prix_Achat_Hyper_Large = item.Prix_Unité;
                    }
                    foreach (Custom_Prix item in list_prix_grossiste)
                    {
                        if (item.Type == "Petit")
                            produit.Prix_Petit_Grossiste = item.Prix_Unité;
                        else if (item.Type == "Moyen")
                            produit.Prix_Moyen_Grossiste = item.Prix_Unité;
                        else if (item.Type == "Grand")
                            produit.Prix_Grand_Grossiste = item.Prix_Unité;
                        else if (item.Type == "Large")
                            produit.Prix_Large_Grossiste = item.Prix_Unité;
                        else if (item.Type == "Hyper Large")
                            produit.Prix_Hyper_Large_Grossiste = item.Prix_Unité;
                    }
                    if (filepath != "")
                    {
                        Bitmap temp = new Bitmap(btnImage.Image);
                        MemoryStream strm = new MemoryStream();
                        temp.Save(strm, ImageFormat.Jpeg);
                        ImageByteArray = strm.ToArray();
                        produit.Image_Byte = ImageByteArray;
                        temp = null;
                        strm = null;
                    }
                    else
                        produit.Image_Byte = null;
                    produit.Mesure = cbxUsage.Text;
                    if (!string.IsNullOrEmpty(cbxType.Text))
                        produit.Type = cbxType.Text;
                    produit.Formule = Convert.ToInt32(cbxFormuleStockage.SelectedValue);
                    if (await EditProductAsync(produit))
                    {
                        Alert.SShow("Produit modifiée avec succès.", Alert.AlertType.Sucess);
                        cbxCode.Text = null;
                        ok = "Oui";
                        filepath = "";
                        filename = "";
                        btnImage.IconChar = IconChar.Image;
                        btnAjouter.IconChar = IconChar.Add;
                        btnAjouter.Text = "Ajouter";
                        txtmax.Clear();
                        cbxType.Text = null;
                        txtmin.Clear();
                        cbxNom.Text = null;
                        cbxTaille.Text = null;
                        cbxUsage.Text = null;
                        cbxFormuleStockage.Text = null;
                        cbxCatégorie.Text = null;
                        txtStock.Clear();
                    }
                }
                min = null;
                max = null;
                qté = null;
                produit = null;
                prs = null;
            }
        }

        private async Task CallTask(N_Produit produit)
        {
            var marque = FillCbxMarqueAsync();
            if(produit != null)
            {
                Product_Id = produit.Product_Id;
            }
            var code = FillCodeAsync(Product_Id);
            var catégorie = FillCbxCatégorieAsync();
            var usage = FillCbxUsageAsync();
            var taille = FillCbxTailleAsync();
            var mesurelist = MesureListAsync();
            var formule = FormuleListAsync();
            var type = FillCbxTypeAsync();
            List<Task> taskList = new List<Task>()
            {
                marque,
                code,
                catégorie,
                type,
                usage,
                taille,
                mesurelist,
                formule
            };
            while (taskList.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(taskList);
                if (finishedTask == marque)
                {
                    cbxNom.DataSource = marque.Result;
                    cbxNom.DisplayMember = "Marque";
                    cbxNom.ValueMember = "Id";
                    if(produit.Nom != null)
                    cbxNom.Text = produit.Nom.Trim();
                    else cbxNom.Text = produit.Nom;
                    cbxCode.Text = produit.Code_Barre;
                    Decimal num = produit.Stock_Max;
                    string str1 = num.ToString("N0");
                    txtmax.Text = str1;
                    num = produit.Stock_Min;
                    string str2 = num.ToString("N0");
                    txtmin.Text = str2;
                    num = produit.Quantité;
                    string str3 = num.ToString("N0");
                    txtStock.Text = str3;
                    if (produit.Image != null)
                        btnImage.Image = produit.Image;
                    else
                        btnImage.IconChar = IconChar.Image;
                    using (var donnée = new QuitayeContext())
                    {
                        if(produit.Nom != null)
                        {
                            var checktype = donnée.tbl_marque.Where(x => x.Nom == produit.Nom.Trim()).FirstOrDefault();
                            if (checktype == null)
                            {
                                int catégory_id = 1;
                                var chd = donnée.tbl_marque.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                                catégory_id = chd + 1;
                                var category = new Models.Context.tbl_marque();
                                category.Nom = produit.Nom;
                                category.Date = DateTime.Now;
                                category.Auteur = Principales.profile;
                                category.Id = catégory_id;
                                donnée.tbl_marque.Add(category);
                                await donnée.SaveChangesAsync();
                                await CallTypelAsync();
                                if (produit.Nom != null)
                                    cbxNom.Text = produit.Nom.Trim();
                            }
                        }
                        
                    }
                }else if(finishedTask == code)
                {
                    cbxCode.DataSource = code.Result;
                    cbxCode.DisplayMember = "Code";
                    cbxCode.ValueMember = "Id";
                    cbxCode.Text = null;
                    if (produit != null)
                        cbxCode.Text = produit.Code_Barre;
                    if(code.Result.Rows.Count > 1) 
                    {
                        cbxCode.Enabled = true;
                        cbxCode.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                }
                else if(finishedTask == type)
                {
                    cbxType.DataSource = type.Result;
                    cbxType.DisplayMember = "Type";
                    cbxType.ValueMember = "Id";
                    cbxType.Text = produit.Type;
                    using(var donnée = new QuitayeContext())
                    {
                        var checktype = donnée.tbl_type.Where(x => x.Type == produit.Type).FirstOrDefault();
                        if (checktype == null)
                        {
                            int catégory_id = 1;
                            var chd = donnée.tbl_type.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                            catégory_id = chd + 1;
                            var category = new Models.Context.tbl_type();
                            category.Type = produit.Type;
                            category.Date_Ajout = DateTime.Now;
                            category.Auteur = Principales.profile;
                            category.Id = catégory_id;
                            donnée.tbl_type.Add(category);
                            await donnée.SaveChangesAsync();
                            await CallTypelAsync();
                            cbxType.Text = produit.Type;
                        }
                    }
                }
                else if (finishedTask == catégorie)
                {
                    cbxCatégorie.DataSource = catégorie.Result;
                    cbxCatégorie.DisplayMember = "Catégorie";
                    cbxCatégorie.ValueMember = "Id";
                    cbxCatégorie.Text = produit.Catégorie;
                }
                else if (finishedTask == usage)
                {
                    cbxUsage.DataSource = usage.Result;
                    cbxUsage.DisplayMember = "Usage";
                    cbxUsage.ValueMember = "Id";
                    cbxUsage.Text = produit.Mesure;
                }
                else if (finishedTask == formule)
                {
                    cbxFormuleStockage.DataSource = formule.Result;
                    cbxFormuleStockage.DisplayMember = "Formule";
                    cbxFormuleStockage.ValueMember = "Id";
                    cbxFormuleStockage.Text = produit.N_Formule;
                }
                else if (finishedTask == taille)
                {
                    cbxTaille.DataSource = taille.Result;
                    cbxTaille.DisplayMember = "Taille";
                    cbxTaille.ValueMember = "Id";
                    cbxTaille.Text = produit.Taille;
                }
                else if (finishedTask == mesurelist)
                {
                    foreach (Mesure item in mesurelist.Result)
                    {
                        list_prix.Add(new Custom_Prix()
                        {
                            Type = item.Type
                        });
                    }
                        
                    foreach (Mesure item in mesurelist.Result)
                    {
                        list_prix_grossiste.Add(new Custom_Prix()
                        {
                            Type = item.Type
                        });
                    }
                        
                    foreach (Mesure item in mesurelist.Result)
                    {
                        if (item.Nom.Contains("Petit"))
                            list_prix_achat.Add(new Custom_Prix()
                            {
                                Type = item.Type,
                                Prix_Unité = produit.Prix_Achat_Petit
                            });
                        else if (item.Nom.Contains("Grand"))
                            list_prix_achat.Add(new Custom_Prix()
                            {
                                Type = item.Type,
                                Prix_Unité = produit.Prix_Achat_Grand
                            });
                        else if (item.Nom.Contains("Moyen"))
                            list_prix_achat.Add(new Custom_Prix()
                            {
                                Type = item.Type,
                                Prix_Unité = produit.Prix_Achat_Moyen
                            });
                        else if (item.Nom.Contains("Hyper_Large"))
                            list_prix_achat.Add(new Custom_Prix()
                            {
                                Type = item.Type,
                                Prix_Unité = produit.Prix_Achat_Hyper_Large
                            });
                        else if (item.Nom.Equals("Large"))
                            list_prix_achat.Add(new Custom_Prix()
                            {
                                Type = item.Type,
                                Prix_Unité = produit.Prix_Achat_Large
                            });
                    }
                }
                taskList.Remove(finishedTask);
                finishedTask = (Task)null;
            }
            cbxNom.Text = produit.Nom;
            marque = (Task<DataTable>)null;
            catégorie = (Task<DataTable>)null;
            usage = (Task<DataTable>)null;
            taille = (Task<DataTable>)null;
            mesurelist = (Task<List<Mesure>>)null;
            formule = (Task<List<Formule_Mesure_Vente>>)null;
            taskList = (List<Task>)null;
        }

        private async Task CallTaillAsync()
        {
            var result = await FillCbxTailleAsync();
            cbxTaille.DataSource = result;
            cbxTaille.DisplayMember = "Taille";
            cbxTaille.ValueMember = "Id";
            cbxTaille.Text = null;
            calltai = false;
            result = (DataTable)null;
        }

        private async Task CallTypelAsync()
        {
            var result = await FillCbxTypeAsync();
            cbxType.DataSource = result;
            cbxType.DisplayMember = "Type";
            cbxType.ValueMember = "Id";
            cbxType.Text = null;
            calltai = false;
            result = null;
        }

        private Task<DataTable> FillCbxTailleAsync() => Task.Factory.StartNew<DataTable>((Func<DataTable>)(() => fillCbxTaille()));

        private DataTable fillCbxTaille()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Taille");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_taille.OrderBy(d => d.Taille).Select(d => new
                {
                    Id = d.Id,
                    Taille = d.Taille
                });
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

        private async Task CallUsageAsync()
        {
            DataTable result = await FillCbxUsageAsync();
            cbxUsage.DataSource = result;
            cbxUsage.DisplayMember = "Usage";
            cbxUsage.ValueMember = "Id";
            cbxUsage.Text = null;
            callus = false;
            result = (DataTable)null;
        }

        private Task<DataTable> FillCbxUsageAsync() => Task.Factory.StartNew<DataTable>((Func<DataTable>)(() => FillCbxUsage()));

        private DataTable FillCbxUsage()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Usage");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext
                    .tbl_mesure_vente.OrderBy(d => d.Nom).Select(d => new
                {
                    Id = d.Id,
                    Usage = d.Nom
                });
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Usage;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        private async Task CallCatégorieAsync()
        {
            DataTable result = await FillCbxCatégorieAsync();
            cbxCatégorie.DataSource = result;
            cbxCatégorie.DisplayMember = "Catégorie";
            cbxCatégorie.ValueMember = "Id";
            cbxCatégorie.Text = null;
            callcat = false;
            result = (DataTable)null;
        }

        private void CallCatégorie()
        {
            cbxCatégorie.DataSource = FillCbxCatégorie();
            cbxCatégorie.DisplayMember = "Catégorie";
            cbxCatégorie.ValueMember = "Id";
            cbxCatégorie.Text = null;
            callcat = false;
        }

        private Task<List<Mesure>> MesureListAsync() => Task.Factory.StartNew<List<Mesure>>((Func<List<Mesure>>)(() => MesureList()));

        private List<Mesure> MesureList()
        {
            List<Mesure> mesureList = new List<Mesure>();
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_mesure_vente.OrderBy(d => d.Type).Select(d => new
                {
                    Id = d.Id,
                    Nom = d.Nom,
                    Type = d.Type
                });
                foreach (var data in source)
                    mesureList.Add(new Mesure()
                    {
                        Id = data.Id,
                        Type = data.Type,
                        Nom = data.Nom
                    });
            }
            return mesureList;
        }

        private async Task CallFormule()
        {
            List<Formule_Mesure_Vente> result = await FormuleListAsync();
            cbxFormuleStockage.DataSource = result;
            cbxFormuleStockage.DisplayMember = "Formule";
            cbxFormuleStockage.ValueMember = "Id";
            cbxFormuleStockage.Text = null;
            result = (List<Formule_Mesure_Vente>)null;
        }

        private Task<List<Formule_Mesure_Vente>> FormuleListAsync() 
            => Task.Factory.StartNew((() => FormuleList()));

        private List<Formule_Mesure_Vente> FormuleList()
        {
            var formuleMesureVenteList = new List<Formule_Mesure_Vente>();
            using (var financeDataContext = new QuitayeContext())
            {
                var formuleMesureVentes = financeDataContext.tbl_formule_mesure_vente.Where(x => x.Petit != 0).Select(d => new
                {
                    Id = d.Id,
                    Nom = d.Formule
                });
                foreach (var data in formuleMesureVentes)
                    formuleMesureVenteList.Add(new Formule_Mesure_Vente()
                    {
                        Id = Convert.ToInt32(data.Id),
                        Formule = data.Nom
                    });
            }
            return formuleMesureVenteList;
        }

        private Task<DataTable> FillCbxCatégorieAsync() 
            => Task.Factory.StartNew((() => FillCbxCatégorie()));

        private DataTable FillCbxCatégorie()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Catégorie");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_catégorie.OrderBy(d => d.Catégorie).Select(d => new
                {
                    Id = d.Id,
                    Catégorie = d.Catégorie
                });
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


        //private Task<DataTable> FillMulti_BarcodeAsync()
        //    => Task.Factory.StartNew((() => FillMulti_Barcode()));

        //private DataTable FillMulti_Barcode()
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable.Columns.Add("Id");
        //    dataTable.Columns.Add("Name");
        //    using (var donnée = new QuitayeContext())
        //    {
        //        var source = donnée.tbl_catégorie.OrderBy(d => d.Catégorie).Select(d => new
        //        {
        //            Id = d.Id,
        //            Catégorie = d.Catégorie
        //        });
        //        foreach (var data in source)
        //        {
        //            DataRow row = dataTable.NewRow();
        //            row[0] = data.Id;
        //            row[1] = data.Catégorie;
        //            dataTable.Rows.Add(row);
        //        }
        //        return dataTable;
        //    }
        //}

        private async Task CallMarqueAsync()
        {
            var result = await FillCbxMarqueAsync();
            cbxNom.DataSource = result;
            cbxNom.DisplayMember = "Marque";
            cbxNom.ValueMember = "Id";
            cbxNom.Text = null;
            callmar = false;
            result = (DataTable)null;
            
        }

        private async Task CallCodeAsync(string product_id = null)
        {
            var result = await FillCodeAsync(Convert.ToInt32(product_id));
            cbxCode.DataSource = result;
            cbxCode.DisplayMember = "Code";
            cbxCode.ValueMember = "Id";
            cbxCode.Text = null;
            if(result.Rows.Count > 1)
            {
                cbxCode.DropDownStyle = ComboBoxStyle.DropDownList;
                cbxCode.Enabled = true;
            }
            callcode = false;
            result = (DataTable)null;
        }

        private Task<DataTable> FillCbxMarqueAsync() => Task.Factory.StartNew(() => FillCbxMarque());

        private DataTable FillCbxMarque()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Marque");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_marque
                    .OrderBy(d => d.Nom).Select(d => new
                {
                    Id = d.Id,
                    Marque = d.Nom
                });
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Marque;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        private Task<DataTable> FillCodeAsync(int product_id = 0) => Task.Factory.StartNew(() => FillCode(product_id));
        public int Product_Id { get; set; }
        private DataTable FillCode(int product_id = 0)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Code");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_multi_barcode
                    .Where(d => d.Product_Id == product_id && product_id != 0).Select(d => new
                    {
                        Id = d.Id,
                        Code = d.Barcode
                    });
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Code;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        private void BtnFermer_Click(object sender, EventArgs e) => Close();

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


        private async Task<bool> AddProductAsync(N_Produit produit)
        {
            bool flag = false;
            using (var financeDataContext = new QuitayeContext())
            {
                var source1 = financeDataContext.tbl_produits.Select(d => new
                {
                    Id = d.Id
                }).Take(1);
                int num1 = 1;
                if (source1.Count() != 0)
                {
                    var data = financeDataContext.tbl_produits.OrderByDescending(d => d.Id).Select(d => new
                    {
                        Id = d.Id
                    }).First();
                    num1 = Convert.ToInt32(data.Id) + 1;
                }
                var check = financeDataContext.tbl_produits.Where(x => x.Barcode == produit.Code_Barre).FirstOrDefault();
                if (check == null)
                {
                    var entity1 = new Models.Context.tbl_produits();
                    entity1.Id = num1;
                    entity1.Nom = produit.Nom;
                    entity1.Catégorie = produit.Catégorie;
                    entity1.Prix_Petit = new Decimal?(produit.Prix_Petit);
                    entity1.Prix_Moyen = new Decimal?(produit.Prix_Moyen);
                    entity1.Prix_Grand = new Decimal?(produit.Prix_Grand);
                    entity1.Prix_Large = new Decimal?(produit.Prix_Large);
                    entity1.Prix_Hyper_Large = new Decimal?(produit.Prix_Hyper_Large);
                    entity1.Prix_Achat_Petit = new Decimal?(produit.Prix_Achat_Petit);
                    entity1.Prix_Achat_Moyen = new Decimal?(produit.Prix_Achat_Moyen);
                    entity1.Prix_Achat_Grand = new Decimal?(produit.Prix_Achat_Grand);
                    entity1.Prix_Achat_Large = new Decimal?(produit.Prix_Achat_Large);
                    entity1.Prix_Achat_Hyper_Large = new Decimal?(produit.Prix_Achat_Hyper_Large);
                    entity1.Formule_Stockage = new int?(produit.Formule);
                    entity1.Mesure = produit.Mesure;
                    entity1.Taille = produit.Taille;
                    entity1.Type = produit.Type;
                    entity1.Prix_Achat = new Decimal?(produit.Prix_Achat);
                    entity1.Stock_min = new int?(Convert.ToInt32(produit.Stock_Min));
                    entity1.Stock_max = new int?(Convert.ToInt32(produit.Stock_Max));
                    entity1.Prix_Petit_Grossiste = new Decimal?(produit.Prix_Petit_Grossiste);
                    entity1.Prix_Moyen_Grossiste = new Decimal?(produit.Prix_Moyen_Grossiste);
                    entity1.Prix_Grand_Grossiste = new Decimal?(produit.Prix_Grand_Grossiste);
                    entity1.Prix_Large_Grossiste = new Decimal?(produit.Prix_Large_Grossiste);
                    entity1.Prix_Hyper_Large_Grossiste = new Decimal?(produit.Prix_Hyper_Large_Grossiste);
                    entity1.Auteur = Principales.profile;
                    if (filepath != "")
                        entity1.Nom_Image = filename;
                    entity1.Barcode = produit.Code_Barre;
                    entity1.Image = produit.Image_Byte;
                    entity1.Date_Ajout = new DateTime?(DateTime.Now);
                    financeDataContext.tbl_produits.Add(entity1);
                    var source2 = financeDataContext.tbl_stock_produits_vente.Select(d => new
                    {
                        Id = d.Id
                    }).Take(1);
                    int num2 = 1;
                    if (source2.Count() != 0)
                    {
                        var data = financeDataContext.tbl_stock_produits_vente.OrderByDescending((d => d.Id)).Select(d => new
                        {
                            Id = d.Id
                        }).First();
                        num2 = Convert.ToInt32(data.Id) + 1;
                    }
                    var entity2 = new Models.Context.tbl_stock_produits_vente();
                    entity2.Id = num2;
                    entity2.Marque = produit.Nom;
                    entity2.Code_Barre = produit.Code_Barre;
                    entity2.Taille = produit.Taille;
                    entity2.Catégorie = produit.Catégorie;
                    entity2.Type = produit.Type;
                    entity2.Detachement = "Siège";
                    entity2.Product_Id = entity1.Id;
                    var tblMesureVente = financeDataContext.tbl_mesure_vente.Where(d => d.Nom == produit.Mesure).First();
                    var formuleMesureVente = financeDataContext.tbl_formule_mesure_vente.Where(d => d.Id == produit.Formule).First();
                    int? niveau = tblMesureVente.Niveau;
                    int num3 = 1;
                    if (niveau.GetValueOrDefault() == num3 & niveau.HasValue)
                    {
                        entity2.Quantité = new Decimal?(produit.Quantité);
                    }
                    else
                    {
                        niveau = tblMesureVente.Niveau;
                        int num4 = 2;
                        if (niveau.GetValueOrDefault() == num4 & niveau.HasValue)
                        {
                            Decimal num5 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Moyen);
                            entity2.Quantité = new Decimal?(Convert.ToDecimal(produit.Quantité) * num5);
                        }
                        else
                        {
                            niveau = tblMesureVente.Niveau;
                            int num6 = 3;
                            if (niveau.GetValueOrDefault() == num6 & niveau.HasValue)
                            {
                                Decimal num7 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Grand);
                                entity2.Quantité = new Decimal?(Convert.ToDecimal(produit.Quantité) * num7);
                            }
                            else
                            {
                                niveau = tblMesureVente.Niveau;
                                int num8 = 4;
                                if (niveau.GetValueOrDefault() == num8 & niveau.HasValue)
                                {
                                    Decimal num9 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Large);
                                    entity2.Quantité = new Decimal?(Convert.ToDecimal(produit.Quantité) * num9);
                                }
                                else
                                {
                                    niveau = tblMesureVente.Niveau;
                                    int num10 = 5;
                                    if (niveau.GetValueOrDefault() == num10 & niveau.HasValue)
                                    {
                                        Decimal num11 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Hyper_Large);
                                        entity2.Quantité = new Decimal?(Convert.ToDecimal(produit.Quantité) * num11);
                                    }
                                }
                            }
                        }
                    }
                    var hist = 1;
                    var dsv = (from d in financeDataContext.tbl_historique_valeur_stock
                               orderby d.Id descending
                               select d).FirstOrDefault();
                    if (dsv != null)
                        hist = Convert.ToInt32(dsv.Id) + 1;
                    var historique = new Models.Context.tbl_historique_valeur_stock();
                    historique.Code_Barre = produit.Code_Barre;
                    historique.Filiale = "Siège";
                    historique.Date = DateTimeOffset.Now;
                    historique.Prix_Grand = produit.Prix_Grand;
                    historique.Prix_Petit = produit.Prix_Petit;
                    historique.Prix_Moyen = produit.Prix_Moyen;
                    historique.Prix_Large = produit.Prix_Large;
                    historique.Prix_Hyper_Large = Convert.ToDecimal(produit.Prix_Hyper_Large);
                    historique.Quantité = produit.Quantité;
                    historique.Id = hist;
                    financeDataContext.tbl_historique_valeur_stock.Add(historique);
                    var multi_id = 1;
                    var check_multi = financeDataContext.tbl_multi_barcode.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                    if (check_multi != 0)
                        multi_id = check_multi + 1;
                    var multi_barcode = financeDataContext.tbl_multi_barcode.Add(new tbl_multi_barcode()
                    {
                        Id = multi_id,
                        Barcode = produit.Code_Barre,
                        Product_Id = entity1.Id,
                    });
                    entity2.Formule = new int?(produit.Formule);
                    financeDataContext.tbl_stock_produits_vente.Add(entity2);
                    await financeDataContext.SaveChangesAsync();
                    flag = true;
                }
                else flag = false;
            }
            return flag;
        }

        private async Task<bool> EditProductAsync(N_Produit produit)
        {
            bool flag = false;
            using (var financeDataContext = new QuitayeContext())
            {
                var check = financeDataContext.tbl_stock_produits_vente.Where(x => x.Code_Barre == produit.Code_Barre
                && x.Id != produit.Id).FirstOrDefault();
                

                var ps = financeDataContext.tbl_stock_produits_vente
                    .Where(d => d.Id == produit.Id).Select(d => new
                {
                    Barcode = d.Code_Barre,
                    Filiale = d.Detachement
                }).First();
                if(check !=  null)
                {
                    if(ps.Filiale == check.Detachement) { return false; }
                }
                var tblProduit = financeDataContext
                    .tbl_produits.Where(d => d.Barcode == ps.Barcode).First();
                tblProduit.Nom = produit.Nom;
                tblProduit.Type = produit.Type;
                tblProduit.Catégorie = produit.Catégorie;
                tblProduit.Prix_Petit = new Decimal?(produit.Prix_Petit);
                tblProduit.Prix_Moyen = new Decimal?(produit.Prix_Moyen);
                tblProduit.Prix_Grand = new Decimal?(produit.Prix_Grand);
                tblProduit.Prix_Large = new Decimal?(produit.Prix_Large);
                tblProduit.Prix_Hyper_Large = new Decimal?(produit.Prix_Hyper_Large);
                tblProduit.Prix_Achat_Petit = new Decimal?(produit.Prix_Achat_Petit);
                tblProduit.Prix_Achat_Moyen = new Decimal?(produit.Prix_Achat_Moyen);
                tblProduit.Prix_Achat_Grand = new Decimal?(produit.Prix_Achat_Grand);
                tblProduit.Prix_Achat_Large = new Decimal?(produit.Prix_Achat_Large);
                tblProduit.Prix_Achat_Hyper_Large = new Decimal?(produit.Prix_Achat_Hyper_Large);
                tblProduit.Prix_Petit_Grossiste = new Decimal?(produit.Prix_Petit_Grossiste);
                tblProduit.Prix_Moyen_Grossiste = new Decimal?(produit.Prix_Moyen_Grossiste);
                tblProduit.Prix_Grand_Grossiste = new Decimal?(produit.Prix_Grand_Grossiste);
                tblProduit.Prix_Large_Grossiste = new Decimal?(produit.Prix_Large_Grossiste);
                tblProduit.Prix_Achat = new Decimal?(produit.Prix_Achat);
                tblProduit.Prix_Hyper_Large_Grossiste = new Decimal?(produit.Prix_Hyper_Large_Grossiste);
                tblProduit.Formule_Stockage = new int?(produit.Formule);
                tblProduit.Mesure = produit.Mesure;
                tblProduit.Taille = produit.Taille;
                tblProduit.Auteur = Principales.profile;
                if (filepath != "")
                {
                    tblProduit.Nom_Image = filename;
                    tblProduit.Image = produit.Image_Byte;
                }
                tblProduit.Barcode = produit.Code_Barre;
                tblProduit.Type = produit.Type;
                var stockProduitsVente = financeDataContext.tbl_stock_produits_vente
                    .Where(d => d.Id == produit.Id).First();
                stockProduitsVente.Marque = produit.Nom;
                stockProduitsVente.Code_Barre = produit.Code_Barre;
                stockProduitsVente.Taille = produit.Taille;
                stockProduitsVente.Catégorie = produit.Catégorie;
                stockProduitsVente.Type = produit.Type;
                var tblMesureVente = financeDataContext.tbl_mesure_vente
                    .Where(d => d.Nom == produit.Mesure).First();
                var formuleMesureVente = financeDataContext.tbl_formule_mesure_vente
                    .Where(d => d.Id == produit.Formule).First();
                int? niveau1 = tblMesureVente.Niveau;
                int num1 = 1;
                if (niveau1.GetValueOrDefault() == num1 & niveau1.HasValue)
                {
                    stockProduitsVente.Quantité = new Decimal?(produit.Quantité);
                }
                else
                {
                    int? niveau2 = tblMesureVente.Niveau;
                    int num2 = 2;
                    if (niveau2.GetValueOrDefault() == num2 & niveau2.HasValue)
                    {
                        Decimal num3 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Moyen);
                        stockProduitsVente.Quantité = new Decimal?(Convert.ToDecimal(produit.Quantité) * num3);
                    }
                    else
                    {
                        niveau2 = tblMesureVente.Niveau;
                        int num4 = 3;
                        if (niveau2.GetValueOrDefault() == num4 & niveau2.HasValue)
                        {
                            Decimal num5 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Grand);
                            stockProduitsVente.Quantité = new Decimal?(Convert.ToDecimal(produit.Quantité) * num5);
                        }
                        else
                        {
                            niveau2 = tblMesureVente.Niveau;
                            int num6 = 4;
                            if (niveau2.GetValueOrDefault() == num6 & niveau2.HasValue)
                            {
                                Decimal num7 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Large);
                                stockProduitsVente.Quantité = new Decimal?(Convert.ToDecimal(produit.Quantité) * num7);
                            }
                            else
                            {
                                niveau2 = tblMesureVente.Niveau;
                                int num8 = 5;
                                if (niveau2.GetValueOrDefault() == num8 & niveau2.HasValue)
                                {
                                    Decimal num9 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Hyper_Large);
                                    stockProduitsVente.Quantité = new Decimal?(Convert.ToDecimal(produit.Quantité) * num9);
                                }
                            }
                        }
                    }
                }
                stockProduitsVente.Formule = new int?(produit.Formule);
                await financeDataContext.SaveChangesAsync();
                flag = true;
            }
            return flag;
        }
    }
}
