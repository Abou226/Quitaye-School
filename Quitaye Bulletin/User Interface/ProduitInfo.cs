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
    public partial class ProduitInfo : UserControl
    {
        public ProduitInfo()
        {
            InitializeComponent();
        }

        #region Properties
        private string _titre;

        private Image image;

        private string _catégorie;
        private string _usage;
        private decimal _montant;
        private int _stock_min;
        private int _stock_max;
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


        [Category("Custom Props")]
        public string Catégorie
        {
            get { return _catégorie; }
            set { _catégorie = value; lblCatégorie.Text = "Catégorie : " + value; }
        }

        [Category("Custom Props")]
        public string Code_Barre
        {
            get { return _usage; }
            set { _usage = value; lblUsage.Text = "Code Barre : [" + value + "]"; }
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
            set { _montant = value; lblMontant.Text = "Montant_Es : " + value.ToString("N0") + " FCFA"; }
        }

        [Category("Custom Props")]
        public int Stock_min
        {
            get { return _stock_min; }
            set { _stock_min = value; lblStkmin.Text = "Stok_min : " + value + " Pièces"; }
        }

        private string __usage;

        public string Usage
        {
            get { return __usage; }
            set { __usage = value; }
        }


        [Category("Custom Props")]
        public int Stock_max
        {
            get { return _stock_max; }
            set { _stock_max = value; lblStkmax.Text = "Stock_max : " + value + " Pièces"; }
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
            set { image = value; if (value != null)
                    btnImage.Image = value;
                else btnImage.IconChar = FontAwesome.Sharp.IconChar.Image; }
        }

        #endregion
    }
}
