using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Note_Individuelle : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Note_Individuelle()
        {
            InitializeComponent();
            //cbxClasse.Visible = false;
            //lblClasse.Visible = false;
            FillClasse();
        }

        private void FillClasse()
        {
            if (Principales.type_compte == "Administrateur" || Principales.departement == "Finance/Comptabilité")
            {
                using (var donnée = new QuitayeContext())
                {
                    var s = (from d in donnée.tbl_classe orderby d.Nom select d).ToList();
                    cbxClasse.DataSource = s;
                    cbxClasse.DisplayMember = "Nom";
                    cbxClasse.ValueMember = "Id";
                    cbxClasse.Text = null;
                }
            }
            else
            {

                if (Principales.role == "Agent")
                {
                    using (var donnée = new QuitayeContext())
                    {
                        var s = (from d in donnée.tbl_classe where d.Nom == Principales.classes orderby d.Nom select d).ToList();
                        cbxClasse.DataSource = s;
                        cbxClasse.DisplayMember = "Nom";
                        cbxClasse.ValueMember = "Id";
                        cbxClasse.Text = null;
                    }
                }
                else if (Principales.role == "Responsable")
                {
                    using (var donnée = new QuitayeContext())
                    {
                        var s = (from d in donnée.tbl_classe where d.Cycle == Principales.auth1 || d.Cycle == Principales.auth2 || d.Cycle == Principales.auth3 || d.Cycle == Principales.auth4 orderby d.Nom select d).ToList();
                        cbxClasse.DataSource = s;
                        cbxClasse.DisplayMember = "Nom";
                        cbxClasse.ValueMember = "Id";
                        cbxClasse.Text = null;
                    }
                }
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void cbxClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDG();
        }

        public void FillDG()
        {
            using (var donnée = new QuitayeContext())
            {
                var don = from d in donnée.tbl_inscription
                          where d.Année_Scolaire == Principales.annéescolaire && d.Classe == cbxClasse.Text
                          orderby d.Id descending
                          select new
                          {
                              Id = d.Id,
                              N_Matricule = d.N_Matricule,
                              Prenom = d.Prenom,
                              Nom = d.Nom,
                              Date_Naissance = d.Date_Naissance,
                              Genre = d.Genre,
                              Nom_Père = d.Nom_Père,
                              Nom_Mère = d.Nom_Mère,
                              Classe = d.Classe,
                              Date_Inscription = d.Date_Inscription,
                          };
                if (don.Count() == 0)
                {
                    dataGridView1.Columns.Clear();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Tableau vide");
                    DataRow dr = dt.NewRow();
                    dr[0] = "Aucune donnée disponible dans le tableau";
                    dt.Rows.Add(dr);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = don;
                    dataGridView1.Columns[0].Visible = false;

                    DossierColumn delete = new DossierColumn();
                    delete.HeaderText = "Note";
                    delete.Name = "Dossier";

                    dataGridView1.Columns.Add(delete);
                    dataGridView1.Columns["Dossier"].Width = 50;
                    
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 10)
            {
                using (var donnée = new QuitayeContext())
                {
                    string id = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    var eleve = (from d in donnée.tbl_inscription where d.N_Matricule == id select d).First();

                    var clas = (from d in donnée.tbl_classe where d.Nom == eleve.Classe select d).First();
                    Ajout_Note détails_Elèves = new Ajout_Note();
                    détails_Elèves.lblTitre.Text = "Note " + eleve.Nom_Complet;
                    Ajout_Note.matricule = id;
                    Ajout_Note.eleve = eleve.Nom_Complet;
                    Ajout_Note.classes = eleve.Classe;
                    Ajout_Note.cycle = clas.Cycle;
                    Ajout_Note.genre = eleve.Genre;
                    Ajout_Note.nom = eleve.Nom;
                    if(clas.Cycle == "Premier Cycle")
                    {
                        détails_Elèves.label4.Visible = false;
                        détails_Elèves.txtNoteClass.Visible = false;
                    }
                    Ajout_Note.prenom = eleve.Prenom;
                    détails_Elèves.ShowDialog();
                }
            }
        }
    }
}
