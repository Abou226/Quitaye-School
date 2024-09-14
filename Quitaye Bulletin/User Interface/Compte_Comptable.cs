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
    public partial class Compte_Comptable : Form
    {
        string mycontrng = LogIn.mycontrng;
        static public string ok;
        public int id;

        public Compte_Comptable()
        {
            InitializeComponent();
        }


        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnAjouter_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                using (var donnée = new QuitayeContext())
                {
                    if (btnAjouter.Text == "Ajouter")
                    {
                        if (txtCatégorie.Text != "" && txtCompte.Text != "")
                        {
                            catégorie = txtCatégorie.Text;
                            comptes = txtCompte.Text;
                            AddCompteComptableAsync(donnée);
                            txtCompte.Clear();
                            txtCatégorie.Clear();
                            Alert.SShow("Compte Comptable ajoutée avec succès.", Alert.AlertType.Sucess);
                            ok = "Oui";
                        }
                        else
                        {
                            Alert.SShow("Veillez remplir tous les zones de textes.", Alert.AlertType.Warning);
                        }
                    }
                    else
                    {
                        if (txtCatégorie.Text != "" && txtCompte.Text != "")
                        {
                            var compte = donnée.tbl_Compte_Comptable.SingleOrDefault(x => x.Id == id);
                            compte.Auteur = Principales.profile;
                            compte.Catégorie = txtCatégorie.Text;
                            compte.Compte = txtCompte.Text;
                            compte.Nom_Compte = txtCompte.Text + "-" + txtCatégorie.Text;

                            await donnée.SaveChangesAsync();

                            MsgBox msg = new MsgBox();
                            msg.show("Compte modifié avec succès.", "Confirmation", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Succes);
                            msg.ShowDialog();
                            ok = "Oui";
                        }
                        else
                        {
                            Alert.SShow("Veillez remplir tous les zones de textes.", Alert.AlertType.Warning);
                        }
                    }
                }
            }
           
        }

        static string catégorie;
        static string comptes;
        
        public static Task AddCompteComptableAsync(QuitayeContext donnée)
        {
            return Task.Factory.StartNew(() => AddCompteComptable(donnée));
        }
        public static async void AddCompteComptable(QuitayeContext donnée)
        {
            var compte = new Models.Context.tbl_Compte_Comptable();
            compte.Auteur = Principales.profile;
            compte.Catégorie = catégorie;
            compte.Compte = comptes;
            compte.Nom_Compte = comptes + "-" + catégorie;
            compte.Date_Ajout = DateTime.Now;

            donnée.tbl_Compte_Comptable.Add(compte);
            await donnée.SaveChangesAsync();
        }
    }
}
