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
    public partial class Note_Groupé : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Note_Groupé()
        {
            InitializeComponent();

            FillExamen();

            FillMatière();

            FillClasse();
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            checkmat = 1;
            checkcbx = 1;
        }

        CustomNote[] listItems;
        CustomNoteAdvance[] listItemAd;

        private void FillExamen()
        {
            using(var donnée = new QuitayeContext())
            {
                var s = (from d in donnée.tbl_examen where d.Cycle == cycle select d).ToList();
                cbxExamen.DataSource = s;
                cbxExamen.DisplayMember = "Nom";
                cbxExamen.ValueMember = "Id";
                cbxExamen.Text = null;
            }
        }

        private void FillMatière()
        {
            using(var donnée = new QuitayeContext())
            {
                var s = (from d in donnée.tbl_matiere where d.Cycle == cycle select d).ToList();
                cbxMatière.DataSource = s;
                cbxMatière.DisplayMember = "Nom";
                cbxMatière.ValueMember = "Id";
                cbxMatière.Text = null;
            }
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

        Timer timer = new Timer();
       

        

        private void btnAjoutClasse_Click(object sender, EventArgs e)
        {
            Classe cla = new Classe();
            cla.ShowDialog();
            if (cla.ok == "Oui")
            {
                FillClasse();
            }
        }

        private void btnExamen_Click(object sender, EventArgs e)
        {
            Ajout_Examen examen = new Ajout_Examen();
            examen.ShowDialog();
            if (examen.ok == "Oui")
            {
                FillExamen();
            }
        }

        private void btnMatière_Click(object sender, EventArgs e)
        {
            Ajout_Matière matière = new Ajout_Matière();
            matière.ShowDialog();
            if (matière.ok == "Oui")
            {
                FillMatière();
            }
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                if (btnValider.Text == "Afficher Elèves")
                {
                    if (txtCoeff.Text != "" && cbxClasse.Text != "" && cbxExamen.Text != "" && cbxMatière.Text != "")
                    {
                        using (var donnée = new QuitayeContext())
                        {
                            var don = (from d in donnée.tbl_inscription
                                      where d.Année_Scolaire == Principales.annéescolaire
                                      && d.Classe == cbxClasse.Text && d.Active == "Oui"
                                      orderby d.Nom
                                      orderby d.Prenom
                                      select new
                                      {
                                          Id = d.Id,
                                          Matricule = d.N_Matricule,
                                          Nom = d.Nom_Complet,
                                          Noms = d.Nom,
                                          Prenom = d.Prenom,
                                          Genre = d.Genre,
                                      }).ToList();
                            var s = (from d in donnée.tbl_classe where d.Nom == cbxClasse.Text select new { Id = d.Cycle, }).First();
                            cycle = s.Id;
                            dataGridView1.DataSource = don;

                            if (don.Count() > 0)
                            {
                                if (cycle == "Premier Cycle")
                                {
                                    PopulateItems(dataGridView1);
                                }
                                else PopulateItemsSecondCycle(dataGridView1);
                            }
                        }
                    }
                    else
                    {
                        Alert.SShow("Veillez remplir tous les zones de textes.", Alert.AlertType.Warning);
                    }
                }
                else
                {
                    txtCoeff.Enabled = true;
                    cbxMatière.Enabled = true;
                    cbxExamen.Enabled = true;
                    cbxMatière.Enabled = true;
                    cbxClasse.Enabled = true;
                    dataGridView1.DataSource = null;
                    flowLayoutPanel1.Controls.Clear();
                    btnValider.Text = "Afficher Elèves";
                    btnValider.IconChar = FontAwesome.Sharp.IconChar.Check;
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

        public void PopulateItems(DataGridView dgv)
        {
            flowLayoutPanel1.Controls.Clear();
            listItems = new CustomNote[dgv.Rows.Count];

            using (var donné = new QuitayeContext())
            {
                for (int i = 0; i < listItems.Length; i++)
                {
                    var st = (from s in donné.tbl_inscription where s.Id == Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) select s).First();
                    listItems[i] = new CustomNote();
                    listItems[i].Id = st.Id;
                    listItems[i].Nom = "Nom : "+ st.Nom_Complet;
                    listItems[i].Matricule = st.N_Matricule;
                    //listItems[i].lblMaticule.Text = "N°_Matricule : "+st.N_Matricule;
                    listItems[i].Genre = st.Genre;
                    listItems[i].Noms = st.Nom;
                    listItems[i].Prenom = st.Prenom;

                    listItems[i].Width = flowLayoutPanel1.Width - 25;

                    if (flowLayoutPanel1.Controls.Count < 0)
                    {
                        flowLayoutPanel1.Controls.Clear();
                    }
                    else
                    {
                        flowLayoutPanel1.Controls.Add(listItems[i]);
                    }
                }

                txtCoeff.Enabled = false;
                cbxClasse.Enabled = false;
                cbxExamen.Enabled = false;
                cbxMatière.Enabled = false;

                btnValider.Text = "Reprendre";
                btnValider.IconChar = FontAwesome.Sharp.IconChar.Redo;
            }
        }
        public void PopulateItemsSecondCycle(DataGridView dgv)
        {
            flowLayoutPanel1.Controls.Clear();
            listItemAd = new CustomNoteAdvance[dgv.Rows.Count];

            using (var donné = new QuitayeContext())
            {
                for (int i = 0; i < listItemAd.Length; i++)
                {
                    var st = (from s in donné.tbl_inscription where s.Id == Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) select s).First();
                    listItemAd[i] = new CustomNoteAdvance();
                    listItemAd[i].Id = st.Id;
                    listItemAd[i].Nom = "Nom : " + st.Nom_Complet;
                    listItemAd[i].Matricule = st.N_Matricule;
                    //listItemAd[i].lblMaticule.Text = "N°_Matricule : " + st.N_Matricule;
                    listItemAd[i].Genre = st.Genre;
                    listItemAd[i].Noms = st.Nom;
                    listItemAd[i].Prenom = st.Prenom;

                    listItemAd[i].Width = flowLayoutPanel1.Width - 25;

                    if (flowLayoutPanel1.Controls.Count < 0)
                    {
                        flowLayoutPanel1.Controls.Clear();
                    }
                    else
                    {
                        flowLayoutPanel1.Controls.Add(listItemAd[i]);
                    }
                }

                txtCoeff.Enabled = false;
                cbxClasse.Enabled = false;
                cbxExamen.Enabled = false;
                cbxMatière.Enabled = false;

                btnValider.Text = "Reprendre";
                btnValider.IconChar = FontAwesome.Sharp.IconChar.Redo;
            }
        }

        public string cycle;

        private async void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                using(var donnée = new QuitayeContext())
                {
                    for(int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        
                        var s = (from d in donnée.tbl_note orderby d.Id select d).ToList();
                        var classe = (from d in donnée.tbl_classe 
                                      where d.Id == Convert.ToInt32(cbxClasse.SelectedValue) select d).First();
                        var ens = (from d in donnée.tbl_Users 
                                   where d.Id == Principales.id select d).First();
                        if (cycle == "Premier Cycle")
                        {
                            if (listItems[i].Note != null)
                            {
                                if (s.Count() > 0)
                                {
                                    var ss = (from d in donnée.tbl_note orderby d.Id descending select d).First();
                                    
                                    var note = new Models.Context.tbl_note();
                                    note.Id = Convert.ToInt32(ss.Id) + 1;
                                    note.Matière = cbxMatière.Text;
                                    note.Examen = cbxExamen.Text;
                                    note.Note_Compo = Convert.ToInt32(listItems[i].Note);
                                    note.Coeff = Convert.ToInt32(txtCoeff.Text);
                                    note.Date = NaissanceDate.Value;
                                    note.Date_Enregistrement = DateTime.Now;
                                    note.Classe = cbxClasse.Text;
                                    note.Cycle = classe.Cycle;
                                    note.N_Matricule = listItems[i].Matricule;
                                    note.Auteur = Principales.profile;
                                    note.Année_Scolaire = Principales.annéescolaire;
                                    note.Prenom = listItems[i].Prenom;
                                    note.Nom = listItems[i].Noms;
                                    note.Genre = listItems[i].Genre;
                                    note.Enseignant = ens.Id;
                                    donnée.tbl_note.Add(note);
                                    await donnée.SaveChangesAsync();
                                }
                                else
                                {
                                    var note = new Models.Context.tbl_note();
                                    note.Id = 1;
                                    note.Matière = cbxMatière.Text;
                                    note.Examen = cbxExamen.Text;
                                    note.Note_Compo = Convert.ToInt32(listItems[i].Note);
                                    note.Coeff = Convert.ToInt32(txtCoeff.Text);
                                    note.Date = NaissanceDate.Value;
                                    note.Date_Enregistrement = DateTime.Now;
                                    note.Classe = cbxClasse.Text;
                                    note.Cycle = classe.Cycle;
                                    note.N_Matricule = listItems[i].Matricule;
                                    note.Auteur = Principales.profile;
                                    note.Année_Scolaire = Principales.annéescolaire;
                                    note.Prenom = listItems[i].Prenom;
                                    note.Nom = listItems[i].Noms;
                                    note.Genre = listItems[i].Genre;
                                    note.Enseignant = ens.Id;
                                    donnée.tbl_note.Add(note);
                                    await donnée.SaveChangesAsync();
                                }
                            }
                        } 
                        else if (cycle == "Second Cycle" || cycle == "Lycée")
                        {
                            if (listItemAd[i].NoteClasse != null && listItemAd[i].NoteCompo != "")
                            {
                                if (s.Count() > 0)
                                {
                                    var ss = (from d in donnée.tbl_note orderby d.Id descending select d).First();
                                    var note = new Models.Context.tbl_note();
                                    note.Id = Convert.ToInt32(ss.Id) + 1;
                                    note.Matière = cbxMatière.Text;
                                    note.Examen = cbxExamen.Text;
                                    note.Note_Compo = Convert.ToInt32(listItemAd[i].NoteCompo);
                                    note.Note_Classe = Convert.ToInt32(listItemAd[i].NoteClasse);
                                    note.Coeff = Convert.ToInt32(txtCoeff.Text);
                                    note.Date = NaissanceDate.Value;
                                    note.Date_Enregistrement = DateTime.Now;
                                    note.Classe = cbxClasse.Text;
                                    note.Cycle = classe.Cycle;
                                    note.N_Matricule = listItemAd[i].Matricule;
                                    note.Auteur = Principales.profile;
                                    note.Année_Scolaire = Principales.annéescolaire;
                                    note.Prenom = listItemAd[i].Prenom;
                                    note.Nom = listItemAd[i].Noms;
                                    note.Genre = listItemAd[i].Genre;
                                    note.Enseignant = ens.Id;

                                    donnée.tbl_note.Add(note);
                                    await donnée.SaveChangesAsync();
                                }
                                else
                                {
                                    var note = new Models.Context.tbl_note();
                                    note.Id = 1;
                                    note.Matière = cbxMatière.Text;
                                    note.Examen = cbxExamen.Text;
                                    note.Note_Compo = Convert.ToInt32(listItemAd[i].NoteCompo);
                                    note.Note_Classe = Convert.ToInt32(listItemAd[i].NoteClasse);
                                    note.Coeff = Convert.ToInt32(txtCoeff.Text);
                                    note.Date = NaissanceDate.Value;
                                    note.Date_Enregistrement = DateTime.Now;
                                    note.Classe = cbxClasse.Text;
                                    note.Cycle = classe.Cycle;
                                    note.N_Matricule = listItemAd[i].Matricule;
                                    note.Auteur = Principales.profile;
                                    note.Année_Scolaire = Principales.annéescolaire;
                                    note.Prenom = listItemAd[i].Prenom;
                                    note.Nom = listItemAd[i].Noms;
                                    note.Genre = listItemAd[i].Genre;
                                    note.Enseignant = ens.Id;

                                    donnée.tbl_note.Add(note);
                                    await donnée.SaveChangesAsync();
                                }
                            }
                        }
                    }

                    flowLayoutPanel1.Controls.Clear();
                    dataGridView1.DataSource = null;

                    cbxClasse.Enabled = true;
                    txtCoeff.Enabled = true;
                    cbxMatière.Enabled = true;
                    cbxExamen.Enabled = true;
                    cbxMatière.Enabled = true;
                    dataGridView1.DataSource = null;
                    flowLayoutPanel1.Controls.Clear();
                    btnValider.Text = "Assignation";
                    btnValider.IconChar = FontAwesome.Sharp.IconChar.Check;

                    
                    Alert.SShow("Note ajouté avec succès.", Alert.AlertType.Sucess);
                    
                }
            }
        }

        int checkcbx = 0;
        int checkmat = 0;
        private void cbxClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkcbx != 0)
            {
                int id = Convert.ToInt32(cbxClasse.SelectedValue);
                using (var donnée = new QuitayeContext())
                {
                    var se = (from d in donnée.tbl_classe where d.Id == id select d).First();
                    cycle = se.Cycle;
                    if (cbxClasse.Text != "")
                    {
                        var re = (from d in donnée.tbl_examen where d.Cycle == se.Cycle select d).ToList();
                        if (re.Count() != 0)
                        {
                            var s = ((from d in donnée.tbl_examen where d.Cycle == se.Cycle select d).ToList()).First();

                            FillExamen();
                            FillMatière();
                        }
                        //var r = (from din )
                        //txtCoeff.Text = s.Coefficient.ToString();
                    }
                }
            }
        }

        private void cbxMatière_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(checkmat != 0)
            {
                int id = Convert.ToInt32(cbxMatière.SelectedValue);
                using (var donnée = new QuitayeContext())
                {
                    if (cbxMatière.Text != "")
                    {
                        var s = (from d in donnée.tbl_matiere where d.Id == id select d).First();
                        txtCoeff.Text = s.Coefficient.ToString();
                    }
                }
            }
        }

        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            
        }

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count != 0)
            {
                flowLayoutPanel1.Controls.Clear();
                if (cycle == "Premier Cycle")
                {
                    PopulateItems(dataGridView1);
                }
                else PopulateItemsSecondCycle(dataGridView1);
            }
        }

        private void cbxExamen_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
