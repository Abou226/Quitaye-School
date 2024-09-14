using FontAwesome.Sharp;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Notifier_Absence : Form
    {
        public Notifier_Absence()
        {
            InitializeComponent();
        }

        public string ok;
        private string filePath = "";
        private string filename;
        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            IconButton p = sender as IconButton;

            if (p != null)
            {
                file.Filter = "(*.pdf; *.xls;*.docs)| *.pdf; *.xls; *.docs";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    filePath = file.FileName;
                    filename = Path.GetFileName(filePath);
                    //p.Image = Image.FromFile(file.FileName);
                }
            }
        }

        public static string nom_complet;
        public static string matricule;
        public static string cycle;
        public static string classe;
        public static string genre;
        private static string commentaire;
        public static DateTime DateOperation;

        private async void btnValider_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                using (var donnée = new QuitayeContext())
                {
                    commentaire = txtCommentaire.Text;
                    DateOperation = DateOp.Value.Date;
                    await AddAbsenceAsync(donnée);
                    ok = "Oui";
                    ClearData();
                    Alert.SShow("Absence Enregistrée avec succès !", Alert.AlertType.Sucess);
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

        public static Task AddAbsenceAsync(QuitayeContext donnée)
        {
            return Task.Factory.StartNew(() => AddAbsence(donnée));
        }
        private static async void AddAbsence(QuitayeContext donnée)
        {
            var er = from d in donnée.tbl_notifier_absence select d;
            int id = 0;
            if (er.Count() != 0)
            {
                var ers = (from d in donnée.tbl_notifier_absence orderby d.Id descending select d).First();
                id = ers.Id;
            }

            var tbl = new Models.Context.tbl_notifier_absence();
            tbl.Id = id + 1;
            tbl.Auteur = Principales.profile;
            tbl.Date_Ajout = DateTime.Now;
            tbl.Année_Scolaire = Principales.annéescolaire;
            tbl.Personne = nom_complet;
            tbl.N_Matricule = matricule;
            tbl.Classe = classe;
            tbl.Cycle = cycle;
            tbl.Date_Absence = DateOperation;
            tbl.Commentaire = commentaire;
            tbl.Genre = genre;
            donnée.tbl_notifier_absence.Add(tbl);
            await donnée.SaveChangesAsync();
        }

        private void ClearData()
        {
            txtCommentaire.Clear();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
