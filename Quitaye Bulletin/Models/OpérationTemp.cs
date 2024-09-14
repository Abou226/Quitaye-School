using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class OpérationTemp
    {
        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int product_id;

        public int Product_Id
        {
            get { return product_id; }
            set { product_id = value; }
        }



        private string _fournisseur;

        public string Fournisseur
        {
            get { return _fournisseur; }
            set { _fournisseur = value; }
        }

        private string _mesure;

        public string Mesure
        {
            get { return _mesure; }
            set { _mesure = value; }
        }


        private string _payement;

        public string Payement
        {
            get { return _payement; }
            set { _payement = value; }
        }


        private int _id_Client;

        public int Id_Client
        {
            get { return _id_Client; }
            set { _id_Client = value; }
        }


        private string _bon_commande;

        public string Bon_Commande
        {
            get { return _bon_commande; }
            set { _bon_commande = value; }
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


        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private DateTime date_expiration;

        public DateTime Date_Expiration
        {
            get { return date_expiration; }
            set { date_expiration = value; }
        }


        private bool _saved;

        public bool Saved
        {
            get { return _saved; }
            set { _saved = value; }
        }

        private string _code_barre;

        public string Code_Barre
        {
            get { return _code_barre; }
            set { _code_barre = value; }
        }

        private string _client;

        public string Client
        {
            get { return _client; }
            set { _client = value; }
        }

        private string _model;

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        private string _detachement;

        public string Detachement
        {
            get { return _detachement; }
            set { _detachement = value; }
        }



        private string _num_client;

        public string Num_Client
        {
            get { return _num_client; }
            set { _num_client = value; }
        }

        private string _gateau;

        public string Marque
        {
            get { return _gateau; }
            set { _gateau = value; }
        }

        private string _taille;

        public string Taille
        {
            get { return _taille; }
            set { _taille = value; }
        }

        private string _usage;

        public string Usage
        {
            get { return _usage; }
            set { _usage = value; }
        }


        private string _catégorie;

        public string Catégorie
        {
            get { return _catégorie; }
            set { _catégorie = value; }
        }
        private int _qté;

        public int Quantité
        {
            get { return _qté; }
            set { _qté = value; }
        }

        private decimal _prix;

        public decimal Prix_Unité
        {
            get { return _prix; }
            set { _prix = value; }
        }

        private decimal _montant;

        public decimal Montant
        {
            get { return _montant; }
            set { _montant = value; }
        }

        private string _filiale;

        public string Filiale
        {
            get { return _filiale; }
            set { _filiale = value; }
        }


    }
}
