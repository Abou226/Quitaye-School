using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Quitaye_School.User_Interface
{
    public partial class Custom_Bulletin : UserControl
    {
        string mycontrng = LogIn.mycontrng;
        public Custom_Bulletin(string cycles)
        {
            InitializeComponent();
            cycle = cycles;
        }

        #region Properties

        private string cycle;
        private string _matricule;
        private string _nom_complet;
        //private string _note;
        private string _classe;
        private string _examen;
        private int _id;
        private string _nom;
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
            set { _matricule = value; lblMatricule.Text = "N°_Matricule : [" + value+"]"; }
        }



        [Category("Custom Props")]
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
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

        private string _ecole;

        public string Ecole
        {
            get { return _ecole; }
            set { _ecole = value; lbEcole.Text = "Ecole : " + value; }
        }

        private string _annéeScolaire;

        public string Année_Scolaire
        {
            get { return _annéeScolaire; }
            set { _annéeScolaire = value; lblAnnéeScolaire.Text = "Année Scolaire : " + value; }
        }

        public string Classe
        {
            get { return _classe; }
            set { _classe = value; lblClasse.Text = "Classe : " + value; }
        }

        public string Examen
        {
            get { return _examen; }
            set { _examen = value; lblExamen.Text = "Examen : " + value; }
        }

        [Category("Custom Props")]
        public string Nom_Complet
        {
            get { return _nom_complet; }
            set { _nom_complet = value; lblNom.Text = "Nom : "+ value; }
        }

        //private DataGridView _data;
        [Category("Custom Props")]
        public object DataSource
        {
            get { return dataGridView1.DataSource; }
            set { dataGridView1.DataSource = value; }
        }

        public int ColumnOneWidth
        {
            get { return dataGridView1.Columns[0].Width; }
            set { dataGridView1.Columns[0].Width = value; }
        }

        public int ColumnLastWidth
        {
            get { return dataGridView1.Columns[6].Width; }
            set { dataGridView1.Columns[6].Width = value; }
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

        private void btnPDF_Click(object sender, EventArgs e)
        {
            PrintAction.Print.PrintPdfFile(dataGridView1, Nom_Complet + " " + Classe + " " + Examen + " " + Année_Scolaire+ " "+ DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss"), "Bulletin Scolaire", Classe, Examen, mycontrng, "Quitaye School", false);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            PrintAction.Print.PrintExcelFile(dataGridView1, "Bulletin Scolaire " + Nom_Complet, Nom_Complet + " " + Classe + " " + Examen + " " + Année_Scolaire+ " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss"), "Quitaye School");
        }
    }
}
