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
    public partial class Ajouter_Année : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Ajouter_Année()
        {
            InitializeComponent();
        }

        public int id;

        public string ok;

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnAjouterClasse_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                if (txtNom.Text != "")
                {
                    if(btnAjouterClasse.IconChar == FontAwesome.Sharp.IconChar.Plus)
                    {
                        nom = txtNom.Text;
                        await AddAnnéeScolaireAsync();
                        ok = "Oui";
                        Alert.SShow("Année Scolaire ajouté avec succès.", Alert.AlertType.Sucess);
                    }
                    else if(btnAjouterClasse.IconChar == FontAwesome.Sharp.IconChar.Edit)
                    {
                        using(var donnée = new QuitayeContext())
                        {
                            var année_Scolaire = (from d in donnée.tbl_année_scolaire where d.Id == id select d).First();
                            année_Scolaire.Nom = txtNom.Text;
                            await donnée.SaveChangesAsync();
                        }
                            
                        ok = "Oui";
                        Alert.SShow("Année Scolaire ajouté avec succès.", Alert.AlertType.Sucess);
                    }
                }
            }
        }

        static string nom;
        public static Task AddAnnéeScolaireAsync()
        {
            return Task.Factory.StartNew(() => AddAnnéeScolaire());
        }
        public static async void AddAnnéeScolaire()
        {
            using (var donnée = new QuitayeContext())
            {
                var année_Scolaire = new Models.Context.tbl_année_scolaire();
                année_Scolaire.Nom = nom;
                année_Scolaire.Auteur = Principales.profile;
                année_Scolaire.Date = DateTime.Now;
                donnée.tbl_année_scolaire.Add(année_Scolaire);
                await donnée.SaveChangesAsync();
            }
        }
    }
}
