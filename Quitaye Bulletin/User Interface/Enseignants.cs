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
    public partial class Enseignants : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Enseignants()
        {
            InitializeComponent();
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            FillData();
        }

        private void btnInscription_Click(object sender, EventArgs e)
        {
            Ajout_Enseignant enseignant = new Ajout_Enseignant();
            enseignant.ShowDialog();
            if (enseignant.ok == "Oui")
                FillData();
        }

        Timer timer = new Timer();

        private void FillData()
        {
            using(var donnée = new QuitayeContext())
            {
                var s = (from d in donnée.tbl_enseignant where d.Active == "Oui" || d.Active == null 
                        orderby d.Id descending
                        select new
                        {
                            Id = d.Id,
                            Prenom = d.Prenom,
                            Nom = d.Nom,
                            Date_Naissance = d.Date_Naissance.Value,
                            Genre = d.Genre,
                            Adresse = d.Adresse,
                            Contact1 = d.Contact1,
                            Contact2 = d.Contact2,
                            Email = d.Email,
                            Date_Ajout = d.Date_Ajout,
                            Nationalité = d.Nationalité,
                        }).ToList();

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = s;
                dataGridView1.Columns[0].Visible = false;

                DossierColumn dossier = new DossierColumn();
                dossier.Name = "Dossier";
                dossier.HeaderText = "Détails";

                dataGridView1.Columns.Add(dossier);
                dataGridView1.Columns["Dossier"].Width = 40;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 11)
            {
                using (var donnée = new QuitayeContext())
                {
                    string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    var eleve = (from d in donnée.tbl_enseignant where d.Id == Convert.ToInt32(id) select d).First();

                    //var clas = (from d in donnée.tbl_classe where d.Nom == eleve.Classe select d).First();
                    Détails_Enseignant détails_Elèves = new Détails_Enseignant(Convert.ToInt32(id));
                    détails_Elèves.lblTitre.Text = "Détails " + eleve.Nom_Complet;
                    //détails_Elèves.id = Convert.ToInt32(id);
                    détails_Elèves.ShowDialog();
                }
            }
        }
    }
}
