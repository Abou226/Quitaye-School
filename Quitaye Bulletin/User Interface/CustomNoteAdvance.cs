using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class CustomNoteAdvance : UserControl
    {
        public CustomNoteAdvance()
        {
            InitializeComponent();
        }

        #region Properties

        private string _matricule;
        private string _nom;
        private string _noteclasse;
        private string _notecompo;
        private int _id;
        private string _noms;
        private string _prenom;
        private string _genre;

        [Category("Custom Props")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        //public int _ids;
        //public int Ids
        //{
        //    get { return _ids; }
        //    set { _ids = value; _id = value; }
        //}

        [Category("Custom Props")]
        public string Matricule
        {
            get { return _matricule; }
            set { _matricule = value; lblMaticule.Text = "N°_Matricule : " + value; }
        }



        [Category("Custom Props")]
        public string Noms
        {
            get { return _noms; }
            set { _noms = value; }
        }

        [Category("Custom Props")]
        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }

        [Category("Custom Props")]
        public string Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        [Category("Custom Props")]
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; lblNom.Text = value; }
        }


        public string NoteClasse
        {
            get { return _noteclasse; }
            set { _noteclasse = value; txtClasse.Text = value; }
        }

        public string NoteCompo
        {
            get { return _notecompo; }
            set { _notecompo = value; txtCompo.Text = value; }
        }
        #endregion


        private void PictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(152, 152, 242);
        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(4, 10, 97);
        }

        private void txtCompo_TextChanged(object sender, EventArgs e)
        {
            NoteCompo = txtCompo.Text;
        }

        private void txtClasse_TextChanged(object sender, EventArgs e)
        {
            NoteClasse = txtClasse.Text;
        }

        private void txtCompo_Click(object sender, EventArgs e)
        {
            if (txtCompo.Text.Contains("Compo/40")) txtCompo.Clear();
        }

        private void txtClasse_Click(object sender, EventArgs e)
        {
            if (txtClasse.Text.Contains("Classe/20")) txtClasse.Clear();
        }
    }
}
