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
    public partial class List_Enseignant : Form
    {
        string mycontrng = LogIn.mycontrng;
        public List_Enseignant()
        {
            InitializeComponent();
            //FillCbx();
            timer.Enabled = false;
            timer.Interval = 1000;
            timer.Start();
            timer.Tick += Timer_Tick;
            timerser.Enabled = false;
            timerser.Interval = 10;
            timerser.Start();
            timerser.Tick += Timerser_Tick;
        }

        private void Timerser_Tick(object sender, EventArgs e)
        {
            classes = cbxClasse.Text;
            name = "Effectif Enseignant " + classes + " " + Principales.annéescolaire + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            //temp = 0;
        }

        Timer timerser = new Timer();

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            FillCbx();
            check = 1;
           // FillCbx();
            classe = cbxClasse.Text;
        }

        private void FillCbx()
        {
            using(var donnée = new QuitayeContext())
            {
                var s = (from d in donnée.tbl_classe select d).ToList();
                cbxClasse.DataSource = s;
                cbxClasse.DisplayMember = "Nom";
                cbxClasse.ValueMember = "Nom";
                cbxClasse.Text = null;

                if (dataGridView1.Rows.Count == 0)
                {
                    dataGridView1.Columns.Clear();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Tableau vide");
                    DataRow dr = dt.NewRow();
                    dr[0] = "Aucune donnée disponible dans le tableau";
                    dt.Rows.Add(dr);
                    dataGridView1.DataSource = dt;
                }
            }
        }
        private async void CallData()
        {
            var result = await FillDGAsync();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = result;
            dataGridView1.Columns[0].Visible = false;

            DossierColumn dossier = new DossierColumn();
            dossier.HeaderText = "Détails";
            dossier.Name = "Dossier";

            dataGridView1.Columns.Add(dossier);
            dataGridView1.Columns["Dossier"].Width = 40;

            using(var donnée = new QuitayeContext())
            {
                {
                    //var fille = from d in donnée.tbl_enseignant where d. == Principales.annéescolaire && d.Classe == cbxClasse.Text && d.Genre == "Feminin" && d.Active == "Oui" select d;
                    //lblFille.Text = "Fille : " + fille.Count();
                    //var garçon = from d in donnée.tbl_enseignant where d.Année_Scolaire == Principales.annéescolaire && d.Classe == cbxClasse.Text && d.Genre == "Masculin" && d.Active == "Oui" select d;
                    //lblGarçon.Text = "Garçon : " + garçon.Count();
                    //lblEffectif.Text = "Effectif Total : " + don.Count();
                    //lblEffectif.Visible = true;
                    //lblFille.Visible = true;
                    //lblGarçon.Visible = true;
                }
            }
        }

        public Task<DataTable> FillDGAsync()
        {
            return Task.Factory.StartNew(() => FillDG());
        }

        string classe;
        public DataTable FillDG()
        {
            using (var donnée = new QuitayeContext())
            {
                //var don = from d in donnée.tbl_enseignant
                //          where d.Active == "Oui" || d.Active == null
                //          orderby d.Prenom ascending
                //          orderby d.Nom ascending
                //          select new
                //          {
                //              Id = d.Id,
                //              Prenom = d.Prenom,
                //              Nom = d.Nom,
                //              Date_Naissance = d.Date_Naissance,
                //              Genre = d.Genre,
                //              Contact = d.Contact1,
                //              Contact2 = d.Contact2,
                //          };

                var don = (from d in donnée.tbl_matiere
                           where d.Classe == classe && d.Année_Scolaire == Principales.annéescolaire
                           orderby d.Nom ascending
                           select new
                           {
                               Id = d.Id,
                               Classe = d.Classe,
                               Enseignant = d.Enseignant,
                               Matière = d.Nom,
                           }).ToList();

                DataTable dt = new DataTable("tbled");
                //var matier = from d in donnée.tbl_matiere where d.Classe == cbxClasse.Text select d;
                dataGridView3.DataSource = don;
                dt.Columns.Add("Id");
                dt.Columns.Add("Prenom");
                dt.Columns.Add("Nom");
                dt.Columns.Add("Date_Naissance");
                dt.Columns.Add("Genre");
                dt.Columns.Add("Contact1");
                dt.Columns.Add("Contact2");
                dt.Columns.Add("Matière");

                foreach (var item in don)
                {
                    var p = from d in donnée.tbl_enseignant where d.Id == item.Enseignant && (d.Active == "Oui" || d.Active == null) select d;
                    if (p.Count() != 0)
                    {
                        var pp = (from d in donnée.tbl_enseignant where d.Id == item.Enseignant && (d.Active == "Oui" || d.Active == null) select d).First();

                        DataRow dr = dt.NewRow();
                        dr[0] = pp.Id;
                        dr[1] = pp.Prenom;
                        dr[2] = pp.Nom;
                        dr[3] = pp.Date_Naissance.Value.Date.ToString("dd/MM/yyyy");
                        dr[4] = pp.Genre;
                        dr[5] = pp.Contact1;
                        dr[6] = pp.Contact2;
                        dr[7] = item.Matière;
                        dt.Rows.Add(dr);
                    }
                }

                return dt;

            }
        }

        Timer timer = new Timer();
        int check = 0;
        private void cbxClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (check != 0)
            {
                classe = cbxClasse.Text;
                CallData();
            }
                
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 8)
            {
                using (var donnée = new QuitayeContext())
                {
                    string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    var eleve = (from d in donnée.tbl_enseignant where d.Id == Convert.ToInt32(id) select d).First();

                    //var clas = (from d in donnée.tbl_classe where d.Nom == eleve.Classe select d).First();
                    Détails_Enseignant détails_Elèves = new Détails_Enseignant(Convert.ToInt32(id));
                    //détails_Elèves.id = Convert.ToInt32(id);
                    détails_Elèves.matricule = id.ToString();
                    détails_Elèves.prenom = eleve.Prenom;
                    détails_Elèves.nom = eleve.Nom;
                    détails_Elèves.genre = eleve.Genre;
                    détails_Elèves.cycle = "Enseignant";
                    détails_Elèves.lblTitre.Text = "Détails " + eleve.Nom_Complet;
                    //détails_Elèves.id = Convert.ToInt32(id);
                    détails_Elèves.ShowDialog();
                }
            }
        }

        private void btnClasse_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                Classe cla = new Classe();
                cla.ShowDialog();
                if (cla.ok == "Oui")
                {
                    FillCbx();
                }
            }
        }

        private static string name;
        public string classes;

        private void btnExcel_Click(object sender, EventArgs e)
        {
            PrintAction.Print.PrintExcelFile(dataGridView1, "Enseignant " + classes, name, "Quitaye School");
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            PrintAction.Print.PrintPdfFile(dataGridView2, name, "Enseignant " + classes, "Année Scolaire", Principales.annéescolaire, mycontrng, "Quitaye School", true);
        }
    }
}
