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
    public partial class Custom_Passage : UserControl
    {
        public Custom_Passage()
        {
            InitializeComponent();
        }

        #region Properties

        private string _matricule;
        private string _nom;
        //private string _noteclasse;
        //private string _notecompo;
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

        private string _classe;
        [Category("Custom Props")]
        public string Classe
        {
            get { return _classe; }
            set { _classe = value; lblClasse.Text = value; }
        }

        
        [Category("Custom Props")]
        public bool Checked
        {
            get { return check.Checked; }
            set { check.Checked = value; }
        }


        //private string _moyenne;
        //[Category("Custom Props")]
        //public string Moyenne
        //{
        //    get { return _moyenne; }
        //    set { _moyenne = value; lblMoyenne.Text = value; }
        //}

        #endregion


    }
}
