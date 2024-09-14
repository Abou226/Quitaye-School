using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Ajouter_Element : Form
    {
        public Ajouter_Element(string _type, string product_id = null, string barcode = null)
        {
            InitializeComponent();
            btnFermer.Click += BtnFermer_Click;
            btnAjouter.Click += BtnAjouter_Click;
            txtNomination.Text = barcode;
            type = _type;
            product_Id = product_id;
            Barcode = barcode;
            if (type == "Marque" || type == "Catégorie" || type == "Taille" || type == "Account")
                lblTitre.Text = "Nouvelle " + type;
            else if (type == "Usage" || type == "Mode" || type == "Type" || type == "Code")
                lblTitre.Text = "Nouveau " + type;
            else if(type == "Catégorie de Payement")
            {
                lblTitre.Text = $"{_type}";
            }
            lblNomination.Text = type + " :";
            txtNomination.KeyPress += TxtNomination_KeyPress;
        }

        private void TxtNomination_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 45 || e.KeyChar == 44)
            {
                e.Handled = true;
            }
            //if (!char.IsDigit(e.KeyChar) && e.KeyChar != 46 && e.KeyChar != 8)
            //{
            //    e.Handled = true;
            //}
        }

        public  string ok;

        private async void BtnAjouter_Click(object sender, EventArgs e)
        {
            if(txtNomination.Text.Length > 0)
            {
                n = txtNomination.Text;
                var result = await AddDataAsync();
                if (result)
                {
                    ok = "Oui";
                    txtNomination.Clear();
                    Alert.SShow(type + " ajouté avec succès.", Alert.AlertType.Sucess);
                }
            }
        }

        public static string type;
        private readonly string product_Id;

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        static string n;

        public string Barcode { get; }

        
        public async Task<bool> AddDataAsync()
        {
            using(var donnée = new QuitayeContext())
            {
                if (type == "Marque")
                {
                    int id = 1;
                    var pr = (from d in donnée.tbl_marque select new { Id = d.Id }).Take(1);
                    if(pr.Count() != 0)
                    {
                        var prds = (from d in donnée.tbl_marque orderby d.Id descending select new { Id = d.Id }).First();
                        id = prds.Id + 1;
                    }

                    var m = new Models.Context.tbl_marque();
                    m.Nom = n;
                    m.Auteur = Principales.profile;
                    m.Date = DateTime.Now;
                    m.Id = id;
                    donnée.tbl_marque.Add(m);
                    await donnée.SaveChangesAsync();
                }else if(type == "Type")
                {
                    int id = 1;
                    var pr = (from d in donnée.tbl_type select new { Id = d.Id }).Take(1);
                    if (pr.Count() != 0)
                    {
                        var prds = (from d in donnée.tbl_type orderby d.Id descending select new { Id = d.Id }).First();
                        id = prds.Id + 1;
                    }

                    var m = new Models.Context.tbl_type();
                    m.Type = n;
                    m.Auteur = Principales.profile;
                    m.Date_Ajout = DateTime.Now;
                    m.Id = id;
                    donnée.tbl_type.Add(m);
                    await donnée.SaveChangesAsync();
                }else if(type == "Code")
                {
                    int id = 1;
                    var pr = (from d in donnée.tbl_multi_barcode orderby d.Id descending select d.Id).FirstOrDefault();
                    if (pr != 0)
                    {
                        id = pr + 1;
                    }
                    var check_multi = donnée.tbl_multi_barcode.Where(x => x.Barcode == txtNomination.Text).FirstOrDefault();
                    if (check_multi == null)
                    {
                        var m = new Models.Context.tbl_multi_barcode();
                        m.Barcode = n;
                        m.Product_Id = Convert.ToInt32(product_Id);
                        m.Id = id;
                        donnée.tbl_multi_barcode.Add(m);
                        await donnée.SaveChangesAsync();
                    }
                    else
                    {
                        Alert.SShow("Ce code barre est déjà attribué à un produit.", Alert.AlertType.Warning);
                        return false;
                    }
                }
                else if(type == "Mode")
                {
                    int id = 1;
                    var pr = (from d in donnée.tbl_mode_payement select new { Id = d.Id }).Take(1);
                    if (pr.Count() != 0)
                    {
                        var prds = (from d in donnée.tbl_mode_payement orderby d.Id descending select new { Id = d.Id }).First();
                        id = prds.Id + 1;
                    }

                    var m = new Models.Context.tbl_mode_payement();
                    m.Mode = n;
                    m.Auteur = Principales.profile;
                    m.Date = DateTime.Now;
                    m.Id = id;
                    donnée.tbl_mode_payement.Add(m);
                    await donnée.SaveChangesAsync();
                }
                else if (type == "Catégorie")
                {
                    int id = 1;
                    var pr = (from d in donnée.tbl_catégorie select d).Take(1);
                    if (pr.Count() != 0)
                    {
                        var prds = (from d in donnée.tbl_catégorie orderby d.Id descending select d).First();
                        id = prds.Id + 1;
                    }

                    var m = new Models.Context.tbl_catégorie();
                    m.Catégorie = n;
                    m.Auteur = Principales.profile;
                    m.Date_Ajout = DateTime.Now;
                    m.Id = id;
                    donnée.tbl_catégorie.Add(m);
                    await donnée.SaveChangesAsync();
                }
                else if(type == "Catégorie de Payement")
                {
                    //int id = 1;
                    //var pr = (from d in donnée.tbl_payement_catégorie orderby d.Id descending select d.Id).FirstOrDefault();
                    //id = pr + 1;

                    //var payement_catégorie = donnée.tbl_payement_catégorie
                    //                        .Add(new Models.Context.tbl_payement_catégorie()
                    //                        {
                    //                            Nom = n,
                    //                            Id = id,
                                                
                    //                        });
                    await donnée.SaveChangesAsync();
                }
                else if (type == "Usage")
                {
                    //int id = 1;
                    //var pr = (from d in donnée.tbl_usage select d).Take(1);
                    //if (pr.Count() != 0)
                    //{
                    //    var prds = (from d in donnée.tbl_usage orderby d.Id descending select d).First();
                    //    id = prds.Id + 1;
                    //}

                    //var m = new Models.Context.tbl_usage();
                    //m.Usage = n;
                    //m.Auteur = Principales.profile;
                    //m.Date_Ajout = DateTime.Now;
                    //m.Id = id;
                    //donnée.tbl_usage.Add(m);
                    //await donnée.SaveChangesAsync();

                }
                else if(type == "Taille")
                {
                    int id = 1;
                    var pr = (from d in donnée.tbl_taille select d).Take(1);
                    if (pr.Count() != 0)
                    {
                        var prds = (from d in donnée.tbl_taille orderby d.Id descending select d).First();
                        id = prds.Id + 1;
                    }

                    var m = new Models.Context.tbl_taille();
                    m.Taille = n;
                    m.Auteur = Principales.profile;
                    m.Date_Ajout = DateTime.Now;
                    m.Id = id;
                    donnée.tbl_taille.Add(m);
                    await donnée.SaveChangesAsync();
                }
                else if (type == "Account")
                {
                    //int id = 1;
                    //var pr = (from d in donnée.tbl_account_list select d).Take(1);
                    //if (pr.Count() != 0)
                    //{
                    //    var prds = (from d in donnée.tbl_account_list orderby d.Id descending select d).First();
                    //    id = prds.Id + 1;
                    //}

                    //var m = new Models.Context.tbl_account_list();
                    //m.Name = n;
                    
                    //m.Id = id;
                    //donnée.tbl_account_list.Add(m);
                    //await donnée.SaveChangesAsync();
                }
                return true;
            }
        }
    }
}
