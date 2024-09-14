using Quitaye_School.Models;
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
    public partial class Détails_Note : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Détails_Note()
        {
            InitializeComponent();

            FillClasse();
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            checkclass = 1;
            
        }

        Timer timer = new Timer();


        private async Task CallTask()
        {
            var examen = FillExamenAsync();
            var matière = FillMatièreAsync();

            var taskList = new List<Task> { matière, examen };

            while(taskList.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(taskList);
                if(finishedTask == matière)
                {
                    cbxMatière.DataSource = matière.Result;
                    cbxMatière.DisplayMember = "Nom";
                    cbxMatière.ValueMember = "Id";
                    cbxMatière.Text = null;
                }else if(finishedTask == examen)
                {
                    cbxExamen.DataSource = examen.Result;
                    cbxExamen.DisplayMember = "Nom";
                    cbxExamen.ValueMember = "Id";
                    cbxExamen.Text = null;
                }

                taskList.Remove(finishedTask);
            }
        }

        private async Task CallExamen()
        {
            var result = await FillExamenAsync();
            cbxExamen.DataSource = result;
            cbxExamen.DisplayMember = "Nom";
            cbxExamen.ValueMember = "Id";
            cbxExamen.Text = null;
        }
        private Task<DataTable> FillExamenAsync()
        {
            return Task.Factory.StartNew(() => FillExamen());
        }
        private DataTable FillExamen()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Nom");
            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_examen where d.Cycle == cycle 
                        select new { Id = d.Id, Nom = d.Nom};
                foreach (var item in s)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Nom;

                    dt.Rows.Add(dr);
                }

                return dt;
            }
        }

        private async Task CallMatière()
        {
            var result = await FillMatièreAsync();
            cbxMatière.DataSource = result;
            cbxMatière.DisplayMember = "Nom";
            cbxMatière.ValueMember = "Id";
            cbxMatière.Text = null;
        }
        private Task<DataTable> FillMatièreAsync()
        {
            return Task.Factory.StartNew(() => FillMatière());
        }
        private DataTable FillMatière()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Nom");
            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_matiere where d.Cycle == cycle 
                        select new { Id = d.Id, Nom = d.Nom };

                foreach (var item in s)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Nom;

                    dt.Rows.Add(dr);
                }

                return dt;
                
            }
        }

        string classe, matière, examen;
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


        private async Task CallDG()
        {
            examen = cbxExamen.Text;
            matière = cbxMatière.Text;
            classe = cbxClasse.Text;
            var result = await FillDgAsync();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = result;
            dataGridView1.Columns[0].Visible = false;
            DossierColumn dossier = new DossierColumn();
            dossier.Name = "Edit";
            dossier.HeaderText = "Edit";

            DeleteColumn delete = new DeleteColumn();
            delete.Name = "Delete";
            delete.HeaderText = "Sup";

            dataGridView1.Columns.Add(dossier);
            dataGridView1.Columns.Add(delete);
            dataGridView1.Columns["Edit"].Width = 30;
            dataGridView1.Columns["Delete"].Width = 30;
        }
        private Task<DataTable> FillDgAsync()
        {
            return Task.Factory.StartNew(() => FillDg());
        }
        public DataTable FillDg()
        {
            using(var donnée = new QuitayeContext())
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("Prenom");
                dt.Columns.Add("Nom");
                dt.Columns.Add("N_Matricule");
                dt.Columns.Add("Classe");
                dt.Columns.Add("Matière");
                dt.Columns.Add("Note_Classe");
                dt.Columns.Add("Note_Compo");
                var don = from d in donnée.tbl_note
                          where d.Classe == classe
                          && d.Matière == matière
                          && d.Examen == examen
                          && d.Année_Scolaire == Principales.annéescolaire
                          orderby d.Note_Compo descending
                          select new
                          {
                              Id = d.Id,
                              Prenom = d.Prenom,
                              Nom = d.Nom,
                              N_Matricule = d.N_Matricule,
                              Classe = d.Classe,
                              Matière = d.Matière,
                              Note = d.Note_Compo,
                          };
                foreach (var item in don)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Prenom;
                    dr[2] = item.Nom;
                    dr[3] = item.N_Matricule;
                    dr[4] = item.Classe;
                    dr[5] = item.Matière;
                    dr[6] = item.Note;

                    dt.Rows.Add(dr);
                }

                return dt;
            }
        }

        private async Task CallSecondCycle()
        {
            examen = cbxExamen.Text;
            matière = cbxMatière.Text;
            classe = cbxClasse.Text;
            var result = await FillDgSecondCycleAsync();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = result;
            dataGridView1.Columns[0].Visible = false;
            DossierColumn dossier = new DossierColumn();
            dossier.Name = "Edit";
            dossier.HeaderText = "Edit";

            DeleteColumn delete = new DeleteColumn();
            delete.Name = "Delete";
            delete.HeaderText = "Sup";

            dataGridView1.Columns.Add(dossier);
            dataGridView1.Columns.Add(delete);
            dataGridView1.Columns["Edit"].Width = 30;
            dataGridView1.Columns["Delete"].Width = 30;
        }
        private Task<DataTable> FillDgSecondCycleAsync()
        {
            return Task.Factory.StartNew(() => FillDgsecondCycle());
        }
        public DataTable FillDgsecondCycle()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Prenom");
            dt.Columns.Add("Nom");
            dt.Columns.Add("N_Matricule");
            dt.Columns.Add("Classe");
            dt.Columns.Add("Matière");
            dt.Columns.Add("Note_Classe");
            dt.Columns.Add("Note_Compo");
            using (var donnée = new QuitayeContext())
            {
                var don = from d in donnée.tbl_note
                          where d.Classe == classe
                          && d.Matière == matière
                          && d.Examen == examen
                          && d.Année_Scolaire == Principales.annéescolaire
                          orderby d.Note_Compo descending
                          orderby d.Note_Classe descending
                          select new
                          {
                              Id = d.Id,
                              Prenom = d.Prenom,
                              Nom = d.Nom,
                              N_Matricule = d.N_Matricule,
                              Classe = d.Classe,
                              Matière = d.Matière,
                              NoteClasse = d.Note_Classe,
                              NoteCompo = d.Note_Compo,
                          };

                foreach (var item in don)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Prenom;
                    dr[2] = item.Nom;
                    dr[3] = item.N_Matricule;
                    dr[4] = item.Classe;
                    dr[5] = item.Matière;
                    dr[6] = item.NoteClasse;
                    dr[7] = item.NoteCompo;

                    dt.Rows.Add(dr);
                }

                return dt;
                
            }
        }


        int checkclass = 0;
        string cycle;
        private async void cbxClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(checkclass != 0)
            {
                using (var donnée = new QuitayeContext())
                {
                    var s = (from d in donnée.tbl_classe where d.Id == Convert.ToInt32(cbxClasse.SelectedValue) select d).First();
                    cycle = s.Cycle;
                    await CallTask();
                }
            }
        }



        private void cbxExamen_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbxClasse.Text != "" && cbxExamen.Text != "" && cbxMatière.Text != "")
            //{
            //    FillDg();
            //}
        }

        private void cbxMatière_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbxClasse.Text != "" && cbxExamen.Text != "" && cbxMatière.Text != "")
            //{
            //    FillDg();
            //}
        }

        private async void btnValider_Click(object sender, EventArgs e)
        {
            if (cbxClasse.Text != "" && cbxExamen.Text != "" && cbxMatière.Text != "")
            {
                try
                {
                    if (cycle == "Premier Cycle")
                       await CallDG();
                    else await CallSecondCycle();
                }
                catch (Exception ex)
                {
                    var w32ex = ex as SqlException;
                    if (w32ex == null)
                    {
                        w32ex = ex.InnerException as SqlException;
                        int codes = w32ex.ErrorCode;
                    }
                    if (w32ex != null)
                    {
                        int codes = w32ex.ErrorCode;
                        // do stuff

                        if (codes == -2146232060)
                        {
                            MsgBox msg = new MsgBox();
                            msg.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                            msg.ShowDialog();
                        }
                        else
                        {
                            MsgBox msg = new MsgBox();
                            msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                            msg.ShowDialog();
                        }
                    }
                }
            }
            
            
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnPDF_Click(object sender, EventArgs e)
        {

        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.Columns.Count == 10)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    using (var donnée = new QuitayeContext())
                    {
                        var s = (from d in donnée.tbl_note where d.Id == id select d).First();
                        if (e.ColumnIndex == 8 && (s.Id == Principales.id || Principales.role == "Responsable"))
                        {
                            Note _note = new Note();
                            _note.Matière = s.Matière;
                            _note.Examen = s.Examen;
                            _note.Matricule = s.N_Matricule;
                            _note.Note_Classe = Convert.ToDecimal(s.Note_Classe);
                            _note.Note_Compo = Convert.ToDecimal(s.Note_Compo);
                            _note.classe = s.Classe;
                            _note.Coeff = Convert.ToInt32(s.Coeff);
                            Ajout_Note note = new Ajout_Note(_note);
                            note.id = id;
                            note.cbxExamen.Text = s.Examen;
                            Ajout_Note.classes = s.Classe;
                            Ajout_Note.matière = s.Matière;
                            Ajout_Note.exam = s.Examen;
                            note.cbxMat.Text = s.Matière;
                            note.txtCoeff.Text = s.Coeff.ToString();
                            note.txtNoteClass.Text = s.Note_Classe.ToString();
                            note.txtNoteCompo.Text = s.Note_Compo.ToString();
                            note.btnValider.Text = "Modifier";
                            note.btnValider.IconChar = FontAwesome.Sharp.IconChar.Edit;
                            Ajout_Note.cycle = s.Cycle;
                            note.NaissanceDate.Value = s.Date.Value.Date;
                            note.lblTitre.Text = "Note " + s.Prenom + " " + s.Nom;
                            Ajout_Note.matricule = s.N_Matricule;
                            Ajout_Note.genre = s.Genre;
                            Ajout_Note.eleve = s.Prenom + " " + s.Nom;
                            Ajout_Note.nom = s.Nom;
                            Ajout_Note.prenom = s.Prenom;
                            note.ShowDialog();
                            if (Ajout_Note.ok == "Oui")
                               await CallSecondCycle();
                            Ajout_Note.ok = null;
                        }
                        else if (e.ColumnIndex == 9 && (s.Id == Principales.id || Principales.role == "Responsable"))
                        {
                            MsgBox msg = new MsgBox();
                            msg.show("Voulez-vous supprimer cette note ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                            msg.ShowDialog();
                            if (msg.clicked == "Non")
                                return;
                            else if (msg.clicked == "Oui")
                            {
                                var del = (from d in donnée.tbl_note where d.Id == id select d).First();
                                donnée.tbl_note.Remove(del);
                                await donnée.SaveChangesAsync();
                                Alert.SShow("Note supprimer avec succès !", Alert.AlertType.Sucess);
                              await  CallSecondCycle();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var w32ex = ex as SqlException;
                    if (w32ex == null)
                    {
                        w32ex = ex.InnerException as SqlException;
                        int codes = w32ex.ErrorCode;
                    }
                    if (w32ex != null)
                    {
                        int codes = w32ex.ErrorCode;
                        // do stuff

                        if (codes == -2146232060)
                        {
                            MsgBox msg = new MsgBox();
                            msg.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                            msg.ShowDialog();
                        }
                        else
                        {
                            MsgBox msg = new MsgBox();
                            msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                            msg.ShowDialog();
                        }
                    }
                }
                
            }
            else if (dataGridView1.Columns.Count == 9)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    using (var donnée = new QuitayeContext())
                    {
                        var s = (from d in donnée.tbl_note where d.Id == id select d).First();
                        if (e.ColumnIndex == 7 && (s.Id == Principales.id || Principales.role == "Responsable"))
                        {
                            Note _note = new Note();
                            _note.Matière = s.Matière;
                            _note.Examen = s.Examen;
                            _note.Matricule = s.N_Matricule;
                            _note.Note_Classe = Convert.ToDecimal(s.Note_Classe);
                            _note.Note_Compo = Convert.ToDecimal(s.Note_Compo);
                            _note.classe = s.Classe;
                            _note.Coeff = Convert.ToInt32(s.Coeff);
                            Ajout_Note note = new Ajout_Note(_note);
                            note.id = id;
                            note.cbxExamen.Text = s.Examen;
                            Ajout_Note.classes = s.Classe;
                            Ajout_Note.matière = s.Matière;
                            Ajout_Note.exam = s.Examen;
                            note.cbxMat.Text = s.Matière;
                            note.txtCoeff.Text = s.Coeff.ToString();
                            note.txtNoteClass.Text = s.Note_Classe.ToString();
                            note.txtNoteCompo.Text = s.Note_Compo.ToString();
                            note.btnValider.Text = "Modifier";
                            note.btnValider.IconChar = FontAwesome.Sharp.IconChar.Edit;
                            Ajout_Note.cycle = s.Cycle;
                            note.NaissanceDate.Value = s.Date.Value.Date;
                            note.lblTitre.Text = "Note " + s.Prenom + " " + s.Nom;
                            Ajout_Note.matricule = s.N_Matricule;
                            Ajout_Note.genre = s.Genre;
                            Ajout_Note.eleve = s.Prenom + " " + s.Nom;
                            Ajout_Note.nom = s.Nom;
                            Ajout_Note.prenom = s.Prenom;
                            note.ShowDialog();
                            if (Ajout_Note.ok == "Oui")
                               await CallDG();
                            Ajout_Note.ok = null;
                        }
                        else if (e.ColumnIndex == 8 && (s.Id == Principales.id || Principales.role == "Responsable"))
                        {
                            MsgBox msg = new MsgBox();
                            msg.show("Voulez-vous supprimer cette note ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                            msg.ShowDialog();
                            if (msg.clicked == "Non")
                                return;
                            else if (msg.clicked == "Oui")
                            {
                                var del = (from d in donnée.tbl_note where d.Id == id select d).First();
                                donnée.tbl_note.Remove(del);
                                await donnée.SaveChangesAsync();
                                Alert.SShow("Note supprimer avec succès !", Alert.AlertType.Sucess);
                                await CallDG();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var w32ex = ex as SqlException;
                    if (w32ex == null)
                    {
                        w32ex = ex.InnerException as SqlException;
                        int codes = w32ex.ErrorCode;
                    }
                    if (w32ex != null)
                    {
                        int codes = w32ex.ErrorCode;
                        // do stuff

                        if (codes == -2146232060)
                        {
                            MsgBox msg = new MsgBox();
                            msg.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                            msg.ShowDialog();
                        }
                        else
                        {
                            MsgBox msg = new MsgBox();
                            msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                            msg.ShowDialog();
                        }
                    }
                }
                
            }
        }
    }
}
