using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Validation_Vente : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Validation_Vente(string _mesure)
        {
            InitializeComponent();
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            mesure = _mesure;
        }

        string mesure;

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallMesure(mesure);
        }

        Timer loadTimer = new Timer();

        private async Task CallMesure(string mesure)
        {
            var result = await FillMesureAsync();
            cbxMesure.DataSource = result;
            cbxMesure.DisplayMember = "Mesure";
            cbxMesure.ValueMember = "Id";
            cbxMesure.Text = mesure;
        }
        private Task<MesureTable> FillMesureAsync()
        {
            return Task.Factory.StartNew(() => FillMesure());
        }
        private MesureTable FillMesure()
        {
            MesureTable mesureTable = new MesureTable();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Mesure");

            using (var donnée = new QuitayeContext())
            {
                var mesure = from d in donnée.tbl_mesure_vente orderby d.Nom select new { Id = d.Niveau, Nom = d.Nom, Default = d.Default, Type = d.Type };
                if (mesure.Count() != 0)
                {

                    foreach (var item in mesure)
                    {
                        RowsList rows = new RowsList();
                        DataRow dr = dt.NewRow();
                        dr[0] = item.Id;
                        dr[1] = item.Nom;

                        if (item.Default == "Oui")
                        {
                            rows.Default = true;
                            rows.Mesure = item.Nom;
                            rows.Type = item.Type;
                        }
                        else rows.Default = false;
                        mesureTable.RowsLists = new List<RowsList>();
                        mesureTable.RowsLists.Add(rows);
                        dt.Rows.Add(dr);
                    }
                }


                mesureTable.Table = dt;
            }
            return mesureTable;
        }


        public static int id;
        public string ok;

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static string prix, qté;
        private async void btnValidé_Click(object sender, EventArgs e)
        {
            qté = txtQuantité.Text;
            prix = txtPrix.Text;
            var result = await ValidationVenteAsync(cbxMesure.Text);
            if (result)
            {
                Alert.SShow("Vente validé avec succès.", Alert.AlertType.Sucess);
                ok = "Oui";
                Close();
            }
        }
        

        public static async Task<bool> ValidationVenteAsync(string mesure)
        {
            using(var donnée = new QuitayeContext())
            {
                var don = (from d in donnée.tbl_vente where d.Id == id select d).First();
                decimal exQ = Convert.ToDecimal(don.Quantité);
                don.Montant = Convert.ToDecimal(prix);
                don.Quantité = Convert.ToDecimal(qté);
                don.Type = "Attente";
                don.Date_Payement = DateTime.Now;
                don.Date_Action = DateTime.Now;
                don.Auteur_Payement = Principales.profile;
                await donnée.SaveChangesAsync();
                var ms = (from d in donnée.tbl_mesure_vente where d.Nom == mesure select d).First();
                var stock = (from d in donnée.tbl_stock_produits_vente where d.Code_Barre == don.Barcode select d).First();
                var formu = (from d in donnée.tbl_formule_mesure_vente where d.Id == stock.Formule select d).First();
                if (exQ < Convert.ToDecimal(don.Quantité))
                {
                    decimal diff = Convert.ToDecimal(don.Quantité) - exQ;
                    don.Quantité -= exQ;
                    
                    if (ms.Niveau == 1)
                    {
                         stock.Quantité += diff;
                    }
                    else if (ms.Niveau == 2)
                    {
                        decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Moyen);
                        stock.Quantité += diff * unité;
                    }
                    else if (ms.Niveau == 3)
                    {
                        decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Grand);
                        stock.Quantité += diff * unité;
                    }
                    else if (ms.Niveau == 4)
                    {
                        decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Large);
                        stock.Quantité += diff * unité;
                    }
                    else if (ms.Niveau == 5)
                    {
                        decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Hyper_Large);
                        stock.Quantité += diff * unité;
                    }

                }
                else if (exQ > Convert.ToDecimal(don.Quantité))
                {
                    decimal diff = exQ - Convert.ToDecimal(don.Quantité);
                    var p = (from d in donnée.tbl_produits where d.Barcode == don.Barcode select d).First();
                    don.Quantité += diff;

                    if (ms.Niveau == 1)
                    {
                        stock.Quantité -= diff;
                    }
                    else if (ms.Niveau == 2)
                    {
                        decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Moyen);
                        stock.Quantité -= diff * unité;
                    }
                    else if (ms.Niveau == 3)
                    {
                        decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Grand);
                        stock.Quantité -= diff * unité;
                    }
                    else if (ms.Niveau == 4)
                    {
                        decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Large);
                        stock.Quantité -= diff * unité;
                    }
                    else if (ms.Niveau == 5)
                    {
                        decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Hyper_Large);
                        stock.Quantité -= diff * unité;
                    }
                }

                await donnée.SaveChangesAsync();
                return true;
            }
            
        }

        private void txtPrix_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar != 46 && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}
