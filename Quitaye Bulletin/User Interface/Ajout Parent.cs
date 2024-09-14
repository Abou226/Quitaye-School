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
    public partial class Ajout_Parent : Form
    {
        public Ajout_Parent()
        {
            InitializeComponent();
        }

        public static string ok;
        private async void btnAjouterParent_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                if (txtPrenom.Text != "" && txtNom.Text != "" && txtTelephone.Text != "" && cbxGenre.Text != "")
                {
                    if (btnAjouterParent.IconChar == FontAwesome.Sharp.IconChar.Plus)
                    {
                        SetParams();
                        await AddParentAsync();
                        ClearData();
                        Alert.SShow("Parent ajouté avec succès.", Alert.AlertType.Sucess);
                    }
                }
            }
            else
            {
                if (LogIn.trial == true)
                {
                    Alert.SShow("Periode d'essay arrivé terme. Veillez-passez à la version payante !", Alert.AlertType.Info);
                }
                else Alert.SShow("Veillez renouveller votre abonnement !", Alert.AlertType.Info);
            }
        }

        private void SetParams()
        {
            email = txtEmail.Text;
            contact = txtTelephone.Text;
            prenom = txtPrenom.Text;
            nom = txtNom.Text;
            genre = cbxGenre.Text;
            pays = txtPays.Text;
            ville = txtVille.Text;
            adresse = txtAdresse.Text;
        }

        private static string prenom, nom, genre, contact, ville, pays, adresse, email;
        public static Task AddParentAsync()
        {
            return Task.Factory.StartNew(() => AddParent());
        }
        private static async void AddParent()
        {
            using (var donnée = new QuitayeContext())
            {
                var er = from d in donnée.tbl_parent_elèves select d;
                int id = 0;
                if (er.Count() != 0)
                {
                    var ers = (from d in donnée.tbl_parent_elèves orderby d.Id descending select d).First();
                    id = ers.Id;
                }

                var s = new Models.Context.tbl_parent_elèves();
                s.Id = id + 1;
                s.Auteur = Principales.profile;
                s.Date_Ajout = DateTime.Now;
                s.Prenom = prenom;
                s.Nom = nom;
                s.Genre = genre;
                s.Contact = contact;
                s.Ville = ville;
                s.Pays = pays;
                s.Email = email;
                s.Adresse = adresse;
                ok = "Oui";
                s.Nom_Contact = s.Prenom + " " + s.Nom + " " + s.Contact;
                donnée.tbl_parent_elèves.Add(s);
                await donnée.SaveChangesAsync();
            }
        }

        private void ClearData()
        {
            txtAdresse.Clear();
            txtEmail.Clear();
            txtPrenom.Clear();
            txtNom.Clear();
            txtPays.Clear();
            txtVille.Clear();
            txtTelephone.Clear();
            cbxGenre.Text = null;
            nom = null;
            prenom = null;
            contact = null;
            ville = null;
            pays = null;
            adresse = null;
            genre = null;
            email = null;
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
