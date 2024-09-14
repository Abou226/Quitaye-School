using Quitaye_School.Models.Context;
using Quitaye_School.User_Interface;
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
    public partial class PopVente : Form
    {
        string mycontrng = LogIn.mycontrng;
        public PopVente()
        {
            InitializeComponent();
        }
         
        public string ok;
        private void btnFermer_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btnValidé_Click(object sender, EventArgs e)
        {
            if (LogIn.expiré == false)
            {
                using (var donnée = new QuitayeContext())
                {
                    var s = (from d in donnée.tbl_vente where d.Id == venteInfo1.Ref select d).First();
                    Validation_Vente validation = new Validation_Vente(s.Mesure);
                    validation.txtPrix.Text = s.Montant.ToString();
                    validation.txtQuantité.Text = s.Quantité.ToString();
                    validation.panel1.Visible = true;
                    validation.panel2.Visible = true;
                    validation.panel3.Visible = true;
                    validation.panel4.Visible = true;
                    Validation_Vente.id = s.Id;
                    validation.ShowDialog();
                    if (validation.ok == "Oui")
                    {
                        venteInfo1.Montant = Convert.ToDecimal(s.Montant);
                        venteInfo1.Quantité = Convert.ToDecimal(s.Quantité);
                        lblEtat.Text = "Etat : Attente";
                        btnValidé.Visible = false;
                        ok = "Oui";
                    }
                }
            }
        }
    }
}
