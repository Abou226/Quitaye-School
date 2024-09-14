using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class ProductObject
    {
        private int _id;
        private string _source;
        private string _code_barre;
        private decimal? _prix_unité;
        private Image image;
        private byte[] _imagebyte;
        private decimal? _prix_second;
        private decimal? _prix_tertiaire;
        private decimal? _prix_petit_grossiste;
        private decimal? _prix_moyen_grossiste;
        private decimal? _prix_achat;
        private decimal? _prix_grand_grossiste;
        private decimal? _prix_large_grossiste;
        private decimal? _prix_hyper_large_grossiste;
        private decimal? _stock;
        private decimal? _prix_large;
        private string _type;
        private decimal? _prix_hyper_large;
        private string _marque;
        private string _catégorie;
        private string _taille;
        private decimal? _quantité;
        private string _mesure;
        private string _type_mesure;
        private string _formule_stockage;
        private decimal? _montant;
        private bool _manufactué;
        private decimal? _prix_achat_petit;
        private decimal? _prix_achat_moyen;
        private decimal? _prix_achat_grand;
        private decimal? _prix_achat_large;
        private decimal? _prix_achat_hyper_large;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Source
        {
            get => _source;
            set => _source = value;
        }

        public string Code_Barre
        {
            get => _code_barre;
            set => _code_barre = value;
        }

        public decimal? Prix_Petit
        {
            get => _prix_unité;
            set => _prix_unité = value;
        }

        public Image Image
        {
            get => image;
            set => image = value;
        }

        public byte[] Image_Byte
        {
            get => _imagebyte;
            set => _imagebyte = value;
        }

        public decimal? Prix_Moyen
        {
            get => _prix_second;
            set => _prix_second = value;
        }

        public decimal? Prix_Grand
        {
            get => _prix_tertiaire;
            set => _prix_tertiaire = value;
        }

        public decimal? Prix_Petit_Grossiste
        {
            get => _prix_petit_grossiste;
            set => _prix_petit_grossiste = value;
        }

        public decimal? Prix_Moyen_Grossiste
        {
            get => _prix_moyen_grossiste;
            set => _prix_moyen_grossiste = value;
        }

        public decimal? Prix_Achat
        {
            get => _prix_achat;
            set => _prix_achat = value;
        }

        public decimal? Prix_Grand_Grossiste
        {
            get => _prix_grand_grossiste;
            set => _prix_grand_grossiste = value;
        }

        public decimal? Prix_Large_Grossiste
        {
            get => _prix_large_grossiste;
            set => _prix_large_grossiste = value;
        }

        public decimal? Prix_Hyper_Large_Grossiste
        {
            get => _prix_hyper_large_grossiste;
            set => _prix_hyper_large_grossiste = value;
        }

        public decimal? Stock
        {
            get => _stock;
            set => _stock = value;
        }

        public decimal? Prix_Large
        {
            get => _prix_large;
            set => _prix_large = value;
        }

        public string Type
        {
            get => _type;
            set => _type = value;
        }

        public decimal? Prix_Hyper_Large
        {
            get => _prix_hyper_large;
            set => _prix_hyper_large = value;
        }

        public string Marque
        {
            get => _marque;
            set => _marque = value;
        }

        public string Catégorie
        {
            get => _catégorie;
            set => _catégorie = value;
        }

        public string Taille
        {
            get => _taille;
            set => _taille = value;
        }

        public decimal? Quantité
        {
            get => _quantité;
            set => _quantité = value;
        }

        public string Mesure
        {
            get => _mesure;
            set => _mesure = value;
        }

        public string Type_Mesure
        {
            get => _type_mesure;
            set => _type_mesure = value;
        }

        public string Formule_Stockage
        {
            get => _formule_stockage;
            set => _formule_stockage = value;
        }

        public decimal? Montant
        {
            get => _montant;
            set => _montant = value;
        }

        public bool Manufacturé
        {
            get => _manufactué;
            set => _manufactué = value;
        }

        public decimal? Prix_Achat_Petit
        {
            get => _prix_achat_petit;
            set => _prix_achat_petit = value;
        }

        public decimal? Prix_Achat_Moyen
        {
            get => _prix_achat_moyen;
            set => _prix_achat_moyen = value;
        }

        public decimal? Prix_Achat_Grand
        {
            get => _prix_achat_grand;
            set => _prix_achat_grand = value;
        }

        public decimal? Prix_Achat_Large
        {
            get => _prix_achat_large;
            set => _prix_achat_large = value;
        }

        public decimal? Prix_Achat_Hyper_Large
        {
            get => _prix_achat_hyper_large;
            set => _prix_achat_hyper_large = value;
        }
    }
}
