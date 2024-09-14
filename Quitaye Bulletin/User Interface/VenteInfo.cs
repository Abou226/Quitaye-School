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
    public partial class VenteInfo : UserControl
    {
        public VenteInfo()
        {
            InitializeComponent();
        }

        #region Properties
        private string _titre;

        private Image image;

        private string _catégorie;
        private string _code_barre;
        private decimal _montant;
        private string _client;
        private string _contact;
        private decimal _quantité;
        private decimal _prix;
        private decimal _ref;


        public decimal Ref
        {
            get { return _ref; }
            set { _ref = value; lblRef.Text = "Code Barre :[" + value.ToString() + "]"; }
        }

        private string _taille;

        [Category("Custom Props")]
        public string Taille
        {
            get { return _taille; }
            set { _taille = value; lblTaille.Text = "Taille : " + value; }
        }

        private string _usage;

        [Category("Custom Props")]
        public string Usage
        {
            get { return _usage; }
            set { _usage = value; lblUsage.Text = "Usage : " + value; }
        }


        [Category("Custom Props")]
        public string Catégorie
        {
            get { return _catégorie; }
            set { _catégorie = value; lblCatégorie.Text = "Catégorie : " + value; }
        }

        [Category("Custom Props")]
        public string Code_Barre
        {
            get { return _code_barre; }
            set { _code_barre = value; lblCodeBarre.Text = "Code Barre : [" + value + "]"; }
        }

        [Category("Custom Props")]
        public decimal Quantité
        {
            get { return _quantité; }
            set { _quantité = value; lblQuantité.Text = "NB : " + value + " Pièces"; }
        }

        [Category("Custom Props")]
        public decimal Prix_Unité
        {
            get { return _prix; }
            set { _prix = value; lblPrix.Text = "Prix Unité : " + value.ToString("N0") + " FCFA"; }
        }

        [Category("Custom Props")]
        public decimal Montant
        {
            get { return _montant; }
            set { _montant = value; lblMontant.Text = "Montant : " + value.ToString("N0") + " FCFA"; }
        }

        [Category("Custom Props")]
        public string Client
        {
            get { return _client; }
            set { _client = value; lblClient.Text = "Client : " + value; }
        }

        [Category("Custom Props")]
        public string Contact
        {
            get { return _contact; }
            set { _contact = value; lblContact.Text = "Num_Client : " + value; }
        }

        [Category("Custom Props")]
        public string Titre
        {
            get { return _titre; }
            set { _titre = value; lblTitre.Text = "Marque : " + value; }
        }

        [Category("Custom Props")]
        public Image Icon
        {
            get { return image; }
            set { image = value; picPhoto.Image = value; }
        }

        #endregion
    }
}
