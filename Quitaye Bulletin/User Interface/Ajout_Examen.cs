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
    public partial class Ajout_Examen : Form
    {
        public string ok;
        string mycontrng = LogIn.mycontrng;

        public Ajout_Examen()
        {
            InitializeComponent();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAjouterClasse_Click(object sender, EventArgs e)
        {
            if (LogIn.expiré == false)
            {
                if (txtNom.Text != "" && cycle != "" && cycle != null)
                {
                    using (var donnée = new QuitayeContext())
                    {
                        nom = txtNom.Text;
                        AddExamenAsync(donnée);
                        ok = "Oui";
                        Alert.SShow("Examen ajoutée avec succès.", Alert.AlertType.Sucess);
                    }
                }
            }
            
        }
        private static string nom;

        public static Task AddExamenAsync(QuitayeContext donnée)
        {
            return Task.Factory.StartNew(() => AddExament());
        }
        public static async void AddExament()
        {
            using(var donnée = new QuitayeContext())
            {
                var année_Scolaire = new Models.Context.tbl_examen();
                année_Scolaire.Nom = nom;
                année_Scolaire.Auteur = Principales.profile;
                année_Scolaire.Date_Enregistrement = DateTime.Now;
                année_Scolaire.Cycle = cycle;
                donnée.tbl_examen.Add(année_Scolaire);
                await donnée.SaveChangesAsync();
            }
            
        }

       static string cycle;

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cycle = ((RadioButton)sender).Text;
        }

    }
}
