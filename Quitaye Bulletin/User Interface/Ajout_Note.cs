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
    public partial class Ajout_Note : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Ajout_Note()
        {
            InitializeComponent();
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
          //cbxMat.Text = null;
            
        }

        public Ajout_Note(Note _note)
        {
            InitializeComponent();
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
            //cbxMat.Text = null;
            note = _note;
        }

        Note note = new Note();
        int control = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            CallCbxExamen(note);
            if (cycle == "Premier Cycle")
            {
                label5.Text = "Note Compo / 10:";
                
            }
            if(cycle != "Premier Cycle")
            {
                label4.Visible = true;
                txtNoteClass.Visible = true;
            }
                
            CallCbxMatière(note);
            cbxExamen.Text = exam;
            cbxMat.Text = matière;
        }

      public static string matricule;
      public static string eleve;
      public static string classes;
      public static string matière;
      public static string exam;
      public static string cycle;
      public static string ok;
      public static string nom;
      public static string prenom;
      public static string genre;

        Timer timer = new Timer();
        
        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNote_TextChanged(object sender, EventArgs e)
        {
            if(txtNoteClass.Text != "" && Convert.ToInt32(txtNoteClass.Text) <= 20)
            {

            }else if (txtNoteClass.Text != "" && Convert.ToInt32(txtNoteClass.Text) > 20)
            {
                txtNoteClass.Text = "20";
            }
        }

        private void btnExamen_Click(object sender, EventArgs e)
        {
            Ajout_Examen examen = new Ajout_Examen();
            examen.ShowDialog();
            if (examen.ok == "Oui")
            {
                CallCbxExamen(null);
            }
        }


        async void CallCbxExamen(Note _note)
        {
            var result = await FillCbxExamenAsync();
            cbxExamen.DataSource = result;
            cbxExamen.DisplayMember = "Nom";
            cbxExamen.ValueMember = "Id";
            cbxExamen.Text = _note.Examen;
        }
        private Task<DataTable> FillCbxExamenAsync()
        {
            return Task.Factory.StartNew(() => FillCbxExamen());
        }

        private DataTable  FillCbxExamen()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Nom");
            using (var donnée = new QuitayeContext())
            {
                var s = (from d in donnée.tbl_examen where d.Cycle == cycle select d).ToList();
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

        private void btnMatière_Click(object sender, EventArgs e)
        {
            Ajout_Matière matière = new Ajout_Matière();
            matière.ShowDialog();
            if(matière.ok == "Oui")
            {
                CallCbxMatière(null);
            }
        }

        async void CallCbxMatière(Note _note)
        {
            var result = await FillCbxMatièreAsync();
            cbxMat.DataSource = result;
            cbxMat.DisplayMember = "Nom";
            cbxMat.ValueMember = "Id";
            cbxMat.Text = _note.Matière;
            control = 1;
            txtCoeff.Text = _note.Coeff.ToString();
        }
        Task<DataTable> FillCbxMatièreAsync()
        {
            return Task.Factory.StartNew(() => FillCbxMatière());
        }
        private DataTable FillCbxMatière()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Nom");
            using (var donnée = new QuitayeContext())
            {
                try
                {
                    var s = from d in donnée.tbl_matiere where d.Cycle == cycle && d.Classe == classes select d;
                    foreach (var item in s)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = item.Id;
                        dr[1] = item.Nom;
                        dt.Rows.Add(dr);
                    }

                    
                    
                }catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
            }
            return dt;
        }

        private void txtNote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 46 && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        public int id;
        private async void btnValider_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                if (id <= 0)
                {
                    if (cycle != "Premier Cycle")
                    {
                        if (txtCoeff.Text != "" && cbxExamen.Text != "" && cbxMat.Text != "" && txtNoteClass.Text != "" && txtNoteCompo.Text != "")
                        {
                            this.Cursor = Cursors.WaitCursor;
                            using (var donnée = new QuitayeContext())
                            {
                                SetParams();
                               await AddNoteCycleSuperieurAsync(donnée);
                                ClearData();
                                ok = "Oui";
                                Alert.SShow("Note ajouté avec succès.", Alert.AlertType.Sucess);
                            }
                            this.Cursor = Cursors.Default;
                        }
                    }
                    else
                    {
                        if (txtCoeff.Text != "" && cbxExamen.Text != "" && cbxMat.Text != "" && txtNoteCompo.Text != "")
                        {
                            this.Cursor = Cursors.WaitCursor;
                            SetParams();
                           await AddNotePremierCycleAsync();
                            ClearData();
                            ok = "Oui";
                            Alert.SShow("Note ajouté avec succès.", Alert.AlertType.Sucess);
                            this.Cursor = Cursors.Default;
                        }
                    }
                }
                else
                {
                    if (cycle != "Premier Cycle")
                    {
                        if (txtCoeff.Text != "" && cbxExamen.Text != "" && cbxMat.Text != "" && txtNoteClass.Text != "" && txtNoteCompo.Text != "")
                        {
                            this.Cursor = Cursors.WaitCursor;
                            using (var donnée = new QuitayeContext())
                            {
                                var ses = (from d in donnée.tbl_Users where d.Id == Principales.id select d).First();
                                var note = (from d in donnée.tbl_note where d.Id == id select d).First();
                                //note.Id = Convert.ToInt32(ss.Id) + 1;
                                note.Matière = cbxMat.Text;
                                note.Examen = cbxExamen.Text;
                                note.Note_Classe = Convert.ToInt32(txtNoteClass.Text);
                                note.Note_Compo = Convert.ToInt32(txtNoteCompo.Text);
                                note.Coeff = Convert.ToInt32(txtCoeff.Text);
                                note.Date = NaissanceDate.Value;
                                note.Classe = classes;
                                note.Cycle = cycle;
                                note.N_Matricule = matricule;
                                note.Auteur = Principales.profile;
                                note.Année_Scolaire = Principales.annéescolaire;
                                note.Prenom = prenom;
                                note.Enseignant = ses.Id;
                                note.Nom = nom;
                                note.Genre = genre;
                                await donnée.SaveChangesAsync();
                                ClearData();
                                ok = "Oui";
                                Alert.SShow("Note modifiée avec succès.", Alert.AlertType.Sucess);
                            }
                            this.Cursor = Cursors.Default;
                            this.Close();
                        }
                    }
                    else
                    {
                        if (txtCoeff.Text != "" && cbxExamen.Text != "" && cbxMat.Text != "" && txtNoteCompo.Text != "")
                        {
                            this.Cursor = Cursors.WaitCursor;
                            using (var donnée = new QuitayeContext())
                            {
                                var ses = (from d in donnée.tbl_Users where d.Id == Principales.id select d).First();
                                var note = (from d in donnée.tbl_note where d.Id == id select d).First();
                                //note.Id = Convert.ToInt32(ss.Id) + 1;
                                note.Matière = cbxMat.Text;
                                note.Examen = cbxExamen.Text;
                                note.Note_Compo = Convert.ToInt32(txtNoteCompo.Text);
                                note.Coeff = Convert.ToInt32(txtCoeff.Text);
                                note.Date = NaissanceDate.Value;
                                note.Classe = classes;
                                note.Cycle = cycle;
                                note.Enseignant = ses.Id;
                                note.N_Matricule = matricule;
                                note.Auteur = Principales.profile;
                                note.Année_Scolaire = Principales.annéescolaire;
                                note.Prenom = prenom;
                                note.Nom = nom;
                                note.Genre = genre;
                                await donnée.SaveChangesAsync();
                                ClearData();
                                ok = "Oui";
                                Alert.SShow("Note modifiée avec succès.", Alert.AlertType.Sucess);
                            }
                            this.Cursor = Cursors.Default;
                            this.Close();
                        }
                    }
                }
            }
        }

        void SetParams()
        {
            mat = cbxMat.Text;
            examen = cbxExamen.Text;
            DateNaissance = NaissanceDate.Value.Date;
            noteclasse = txtNoteClass.Text;
            notecompo = txtNoteCompo.Text;
            coeff = txtCoeff.Text;
        }

        public Task AddNotePremierCycleAsync()
        {
            return Task.Factory.StartNew(() => AddNotePremierCycle());
        }
        private static async void AddNotePremierCycle()
        {
            using (var donnée = new QuitayeContext())
            {
                var s = (from d in donnée.tbl_note orderby d.Id select d).ToList();
                int ide = 1;
                if (s.Count() > 0)
                {
                    var ss = (from d in donnée.tbl_note orderby d.Id descending select d).First();
                    ide = Convert.ToInt32(ss.Id) + 1;
                }
                var note = new Models.Context.tbl_note();
                note.Id = ide;
                note.Matière = mat;
                note.Examen = examen;
                note.Note_Compo = Convert.ToInt32(notecompo);
                note.Coeff = Convert.ToInt32(coeff);
                note.Date = DateNaissance;
                note.Date_Enregistrement = DateTime.Now;
                note.Classe = classes;
                note.Cycle = cycle;
                note.N_Matricule = matricule;
                note.Auteur = Principales.profile;
                note.Année_Scolaire = Principales.annéescolaire;
                note.Prenom = prenom;
                note.Nom = nom;
                note.Genre = genre;
                donnée.tbl_note.Add(note);
                await donnée.SaveChangesAsync();
            }
        }

        static string mat, examen, noteclasse, notecompo, coeff;
        static DateTime DateNaissance;
        public static Task AddNoteCycleSuperieurAsync(QuitayeContext donnée)
        {
            return Task.Factory.StartNew(() => AddNoteCycleSuperieur());
        }
        private static async void AddNoteCycleSuperieur()
        {
            using(var donnée = new QuitayeContext())
            {
                var s = (from d in donnée.tbl_note orderby d.Id select d).ToList();
                int ide = 1;
                if (s.Count() > 0)
                {
                    var ss = (from d in donnée.tbl_note orderby d.Id descending select d).First();
                    ide = ss.Id + 1;
                }
                var note = new Models.Context.tbl_note();
                note.Id = ide;
                note.Matière = mat;
                note.Examen = examen;
                note.Note_Classe = Convert.ToInt32(noteclasse);
                note.Note_Compo = Convert.ToInt32(notecompo);
                note.Coeff = Convert.ToInt32(coeff);
                note.Date = DateNaissance;
                note.Date_Enregistrement = DateTime.Now;
                note.Classe = classes;
                note.Cycle = cycle;
                note.N_Matricule = matricule;
                note.Auteur = Principales.profile;
                note.Année_Scolaire = Principales.annéescolaire;
                note.Prenom = prenom;
                note.Nom = nom;
                note.Genre = genre;
                donnée.tbl_note.Add(note);
                await donnée.SaveChangesAsync();
            }
            
        }

        public void ClearData()
        {
            txtNoteClass.Clear();
            txtCoeff.Clear();
            cbxExamen.Text = null;
            cbxMat.Text = null;
            txtNoteCompo.Clear();
            mat = null;
            notecompo = null;
            noteclasse = null;
            coeff = null;
            
        }

        private void cbxMatière_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cbxMat.SelectedValue);
            using(var donnée = new QuitayeContext())
            {
                if(cbxMat.Text != "")
                {
                    var s = (from d in donnée.tbl_matiere where d.Id == id select d).First();
                    txtCoeff.Text = s.Coefficient.ToString();
                }
            }
        }

        int max = 0;
        private void txtNoteCompo_TextChanged(object sender, EventArgs e)
        {
            if (cycle == "Premier Cycle")
                max = 10;
            else max = 40;
            if (txtNoteCompo.Text != "" && Convert.ToInt32(txtNoteCompo.Text) <= max)
            {

            }
            else if (txtNoteCompo.Text != "" && Convert.ToInt32(txtNoteCompo.Text) > max)
            {
                txtNoteCompo.Text = max.ToString();
            }
        }

        private void cbxMat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(control != 0)
            {
                int id = Convert.ToInt32(cbxMat.SelectedValue);
                using (var donnée = new QuitayeContext())
                {
                    if (cbxMat.Text != "")
                    {
                        var s = (from d in donnée.tbl_matiere where d.Id == id select d).First();
                        txtCoeff.Text = s.Coefficient.ToString();
                    }
                }
            }
        }

        private void cbxExamen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
