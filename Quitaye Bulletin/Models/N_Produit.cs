using System;
using System.Drawing;

namespace Quitaye_School.Models
{
    public class N_Produit
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        

        private decimal _stock_min;

        public decimal Stock_Min
        {
            get { return _stock_min; }
            set { _stock_min = value; }
        }

        private decimal _prix_achat;

        public decimal Prix_Achat
        {
            get { return _prix_achat; }
            set { _prix_achat = value; }
        }

        private decimal _prix_petit_grossiste;

        public decimal Prix_Petit_Grossiste
        {
            get { return _prix_petit_grossiste; }
            set { _prix_petit_grossiste = value; }
        }

        private decimal _prix_moyen_grossiste;

        public decimal Prix_Moyen_Grossiste
        {
            get { return _prix_moyen_grossiste; }
            set { _prix_moyen_grossiste = value; }
        }

        private decimal _prix_grand_grossiste;

        public decimal Prix_Grand_Grossiste
        {
            get { return _prix_grand_grossiste; }
            set { _prix_grand_grossiste = value; }
        }

        private decimal _prix_large_grossiste;

        public decimal Prix_Large_Grossiste
        {
            get { return _prix_large_grossiste; }
            set { _prix_large_grossiste = value; }
        }

        private decimal _prix_hyper_large_grossiste;

        public decimal Prix_Hyper_Large_Grossiste
        {
            get { return _prix_hyper_large_grossiste; }
            set { _prix_hyper_large_grossiste = value; }
        }


        private decimal _stock_max;

        public decimal Stock_Max
        {
            get { return _stock_max; }
            set { _stock_max = value; }
        }


        private string _code_barre;

        public string Code_Barre
        {
            get { return _code_barre; }
            set { _code_barre = value; }
        }

        private string _nom;

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        private string _catégorie;

        public string Catégorie
        {
            get { return _catégorie; }
            set { _catégorie = value; }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }


        private string _taille;

        public string Taille
        {
            get { return _taille; }
            set { _taille = value; }
        }

        private string _mesure;

        public string Mesure
        {
            get { return _mesure; }
            set { _mesure = value; }
        }

        private string _type_mesure;

        public string Type_Mesure
        {
            get { return _type_mesure; }
            set { _type_mesure = value; }
        }

        private decimal prix_achat_petit;

        public decimal Prix_Achat_Petit
        {
            get { return prix_achat_petit; }
            set { prix_achat_petit = value; }
        }

        private decimal prix_achat_peit;

        public decimal Prix_Achat_Moyen
        {
            get { return prix_achat_peit; }
            set { prix_achat_peit = value; }
        }

        private decimal prix_achat_grand;

        public decimal Prix_Achat_Grand
        {
            get { return prix_achat_grand; }
            set { prix_achat_grand = value; }
        }

        private decimal prix_achat_large;

        public decimal Prix_Achat_Large
        {
            get { return prix_achat_large; }
            set { prix_achat_large = value; }
        }

        private decimal prix_achat_hyper_large;

        public decimal Prix_Achat_Hyper_Large
        {
            get { return prix_achat_hyper_large; }
            set { prix_achat_hyper_large = value; }
        }

        private string _auteur;

        public string Auteur
        {
            get { return _auteur; }
            set { _auteur = value; }
        }


        private DateTime dateTime;

        public DateTime Date
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        private byte[] imageByte;

        public byte[] Image_Byte
        {
            get { return imageByte; }
            set { imageByte = value; }
        }

        private Image image;

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        private decimal _qté;

        public decimal Quantité
        {
            get { return _qté; }
            set { _qté = value; }
        }

        private decimal _prix_unité;

        public decimal Prix_Petit
        {
            get { return _prix_unité; }
            set { _prix_unité = value; }
        }

        private decimal _prix_secondaire;

        public decimal Prix_Moyen
        {
            get { return _prix_secondaire; }
            set { _prix_secondaire = value; }
        }

        private decimal _prix_tertiaire;

        public decimal Prix_Grand
        {
            get { return _prix_tertiaire; }
            set { _prix_tertiaire = value; }
        }

        private decimal _large;

        public decimal Prix_Large
        {
            get { return _large; }
            set { _large = value; }
        }


        private decimal _hyper_large;

        public decimal Prix_Hyper_Large
        {
            get { return _hyper_large; }
            set { _hyper_large = value; }
        }

        private int _formule;

        public int Formule
        {
            get { return _formule; }
            set { _formule = value; }
        }

        private string _n_formule;

        public string N_Formule
        {
            get { return _n_formule; }
            set { _n_formule = value; }
        }


        private int produc_id;

        public int Product_Id
        {
            get { return produc_id; }
            set { produc_id = value; }
        }


    }
}
