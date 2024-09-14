using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class VenteList
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int prod_id;

        public int Product_Id
        {
            get { return prod_id; }
            set { prod_id = value; }
        }


        private string _id_Client;

        public string Id_Client
        {
            get { return _id_Client; }
            set { _id_Client = value; }
        }


        private string _model;

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public string Num_Vente { get; set; }

        private string _marque;

        public string Marque
        {
            get { return _marque; }
            set { _marque = value; }
        }

        private string _code_barre;

        public string Code_Barre
        {
            get { return _code_barre; }
            set { _code_barre = value; }
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

        private string _source;

        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        private string désignation;

        public string Désignation
        {
            get { return désignation; }
            set { désignation = value; }
        }


        private string _payement;

        public string Payement
        {
            get { return _payement; }
            set { _payement = value; }
        }

        private string _catégorie;

        public string Catégorie
        {
            get { return _catégorie; }
            set { _catégorie = value; }
        }

        private string _mode_payement;

        public string Mode_Payement
        {
            get { return _mode_payement; }
            set { _mode_payement = value; }
        }


        private string _auteur;

        public string Auteur
        {
            get { return _auteur; }
            set { _auteur = value; }
        }

        private DateTime? dateTime;

        public DateTime? Date
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        private DateTime? date_expiration;

        public DateTime? Date_Expiration
        {
            get { return date_expiration; }
            set { date_expiration = value; }
        }


        private decimal? _qté;

        public decimal? Quantité
        {
            get { return _qté; }
            set { _qté = value; }
        }

        private string _filiale;

        public string Filiale
        {
            get { return _filiale; }
            set { _filiale = value; }
        }


        private decimal? _prix_unitaire;

        public decimal? Prix_Unitaire
        {
            get { return _prix_unitaire; }
            set { _prix_unitaire = value; }
        }

        private decimal? montant;

        public decimal? Montant
        {
            get { return montant; }
            set { montant = value; }
        }

        private decimal? _prix_unité;

        public decimal? Prix_Petit
        {
            get { return _prix_unité; }
            set { _prix_unité = value; }
        }

        private decimal? _prix_secondaire;

        public decimal? Prix_Moyen
        {
            get { return _prix_secondaire; }
            set { _prix_secondaire = value; }
        }

        private decimal? _prix_tertiaire;

        public decimal? Prix_Grand
        {
            get { return _prix_tertiaire; }
            set { _prix_tertiaire = value; }
        }

        private decimal? _large;

        public decimal? Prix_Large
        {
            get { return _large; }
            set { _large = value; }
        }


        private decimal? _hyper_large;

        public decimal? Prix_Hyper_Large
        {
            get { return _hyper_large; }
            set { _hyper_large = value; }
        }

        private string _client;

        public string Client
        {
            get { return _client; }
            set { _client = value; }
        }

        private decimal _montant_payée;

        public decimal Montant_Payée
        {
            get { return _montant_payée; }
            set { _montant_payée = value; }
        }

        private decimal? _reduction;

        public decimal? Reduction
        {
            get { return _reduction; }
            set { _reduction = value; }
        }


        private string _num_client;

        public string Num_Client
        {
            get { return _num_client; }
            set { _num_client = value; }
        }

        private string _num_client2;

        public string Num_client2
        {
            get { return _num_client2; }
            set { _num_client2 = value; }
        }

        private string _genre;

        public string Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

    }
}
