using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Ajout_Matière : Form
    {
        string mycontrng = LogIn.mycontrng;
        public string ok;

        public Ajout_Matière()
        {
            InitializeComponent();
            FillCbx();
            FillClasse();
        }

        private void FillCbx()
        {
            using(var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_enseignant where d.Active == "Oui" || d.Active == null orderby d.Nom orderby d.Prenom select d;
                cbxEnseignant.DataSource = s;
                cbxEnseignant.DisplayMember = "Nom_Complet";
                cbxEnseignant.ValueMember = "Id";
                cbxEnseignant.Text = null;
            }
        }

        private void FillClasse()
        {
            using(var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_classe select d;
                cbxClasse.DataSource = s;
                cbxClasse.DisplayMember = "Nom";
                cbxClasse.ValueMember = "Id";
                cbxClasse.Text = null;
            }
        }

        private void Ajout_Matière_Load(object sender, EventArgs e)
        {

        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnAjouterClasse_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                if (txtNom.Text != "" && txtCoefficient.Text != "" && cycle != null && cycle != "" && cbxEnseignant.Text != "" && cbxClasse.Text != "")
                {
                    using (var donnée = new QuitayeContext())
                    {
                        nom = txtNom.Text;
                        coeff = txtCoefficient.Text;
                        enseignant = Convert.ToInt32(cbxEnseignant.SelectedValue);
                        classe = cbxClasse.Text;
                        await AddMatièreAsync(donnée, checkBox1.Checked);
                        txtCoefficient.Clear();
                        cbxClasse.Text = null;
                        cbxEnseignant.Text = null;
                        foreach (var item in flowLayoutPanel1.Controls)
                        {
                            if(item is RadioButton)
                            {
                                RadioButton r = (RadioButton)item;
                                r.Checked = false;
                            }
                        }
                        checkBox1.Checked = false;
                        cycle = null;
                        txtNom.Clear();
                        ok = "Oui";
                        Alert.SShow("Matière ajoutée avec succès.", Alert.AlertType.Sucess);
                    }
                }
                else
                {
                    Alert.SShow("Veillez valider tous les zones de textes.", Alert.AlertType.Info);
                }
            }
            
        }

        static string nom;
        static string classe;
        static string coeff;
        static int enseignant;
        public static Task AddMatièreAsync(QuitayeContext donnée, bool check)
        {
            return Task.Factory.StartNew(() => AddMatière(check));
        }
        public static async void AddMatière(bool check)
        {
            using(var donnée = new QuitayeContext())
            {
                var année_Scolaire = new Models.Context.tbl_matiere();
                année_Scolaire.Nom = nom;
                année_Scolaire.Auteur = Principales.profile;
                année_Scolaire.Année_Scolaire = Principales.annéescolaire;
                année_Scolaire.Date_Enregistrement = DateTime.Now;
                année_Scolaire.Coefficient = Convert.ToInt32(coeff);
                année_Scolaire.Enseignant = enseignant;
                année_Scolaire.Classe = classe;
                if (check)
                    année_Scolaire.Matière_Crutiale = "Oui";
                année_Scolaire.Cycle = cycle;
                donnée.tbl_matiere.Add(année_Scolaire);
                await donnée.SaveChangesAsync();
            }
            
        }

        static string cycle;
        
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cycle = ((RadioButton)sender).Text;
        }

        private void btnMatière_Click(object sender, EventArgs e)
        {
            Ajout_Enseignant enseignant = new Ajout_Enseignant();
            enseignant.ShowDialog();
            if (enseignant.ok == "Oui") FillCbx();
        }
    }
}
