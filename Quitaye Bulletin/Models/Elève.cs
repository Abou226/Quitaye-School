using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class Elève
    {
        private int _id;

        private string _adresse;

        private string _email;

        private decimal _montant_inscription;

        private string _nom;

        private string _prenom;

        private string _nom_complet;

        private string _genre;

        private string _père;

        private string _mère;

        private string _contact1;

        private string _contact2;

        private DateTime? _naisance;

        private string _nationalité;

        private string _classe;

        private string _type_scolarité;

        private decimal _montant_scolarité;

        private string _matricule;

        private string _transport;

        private string _assurance;

        private string _cantine;

        private DateTime? date_Inscription;

        private Image image;

        private decimal tranche1;

        private decimal tranche2;

        private decimal tranche3;

        private decimal montant_payé;

        public string Adresse
        {
            get
            {
                return _adresse;
            }
            set
            {
                _adresse = value;
            }
        }

        public string Assurance
        {
            get
            {
                return _assurance;
            }
            set
            {
                _assurance = value;
            }
        }

        public string Cantine
        {
            get
            {
                return _cantine;
            }
            set
            {
                _cantine = value;
            }
        }

        public string Classe
        {
            get
            {
                return _classe;
            }
            set
            {
                _classe = value;
            }
        }

        public string Contact1
        {
            get
            {
                return _contact1;
            }
            set
            {
                _contact1 = value;
            }
        }

        public string Contact2
        {
            get
            {
                return _contact2;
            }
            set
            {
                _contact2 = value;
            }
        }

        public DateTime? Date_Inscription
        {
            get
            {
                return date_Inscription;
            }
            set
            {
                date_Inscription = value;
            }
        }

        public DateTime? Date_Naissance
        {
            get
            {
                return _naisance;
            }
            set
            {
                _naisance = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        public string Genre
        {
            get
            {
                return _genre;
            }
            set
            {
                _genre = value;
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
            }
        }

        public string Matricule
        {
            get
            {
                return _matricule;
            }
            set
            {
                _matricule = value;
            }
        }

        public string Mère
        {
            get
            {
                return _mère;
            }
            set
            {
                _mère = value;
            }
        }

        public decimal Montant_Inscription
        {
            get
            {
                return _montant_inscription;
            }
            set
            {
                _montant_inscription = value;
            }
        }

        public decimal Montant_Payé
        {
            get
            {
                return montant_payé;
            }
            set
            {
                montant_payé = value;
            }
        }

        public decimal Montant_Scolarité
        {
            get
            {
                return _montant_scolarité;
            }
            set
            {
                _montant_scolarité = value;
            }
        }

        public string Nationalité
        {
            get
            {
                return _nationalité;
            }
            set
            {
                _nationalité = value;
            }
        }

        public string Nom
        {
            get
            {
                return _nom;
            }
            set
            {
                _nom = value;
            }
        }

        public string Nom_Complet
        {
            get
            {
                return _nom_complet;
            }
            set
            {
                _nom_complet = value;
            }
        }

        public string Père
        {
            get
            {
                return _père;
            }
            set
            {
                _père = value;
            }
        }

        public string Prenom
        {
            get
            {
                return _prenom;
            }
            set
            {
                _prenom = value;
            }
        }

        public decimal Tranche1
        {
            get
            {
                return tranche1;
            }
            set
            {
                tranche1 = value;
            }
        }

        public decimal Tranche2
        {
            get
            {
                return tranche2;
            }
            set
            {
                tranche2 = value;
            }
        }

        public decimal Tranche3
        {
            get
            {
                return tranche3;
            }
            set
            {
                tranche3 = value;
            }
        }

        public string Transport
        {
            get
            {
                return _transport;
            }
            set
            {
                _transport = value;
            }
        }

        public string Type_Scolarité
        {
            get
            {
                return _type_scolarité;
            }
            set
            {
                _type_scolarité = value;
            }
        }

        private string _année_scolaire;

        public string Année_Scolaire
        {
            get { return _année_scolaire; }
            set { _année_scolaire = value; }
        }

        private string _année_suivante;

        public string Année_Suivante
        {
            get { return _année_suivante; }
            set { _année_suivante = value; }
        }

        private DateTime? _date_operation;

        public DateTime? Date_Operation
        {
            get { return _date_operation; }
            set { _date_operation = value; }
        }

        private string _auteur;

        public string Auteur
        {
            get { return _auteur; }
            set { _auteur = value; }
        }

        private string _classe_actuelle;

        public string Classe_Actuelle
        {
            get { return _classe_actuelle; }
            set { _classe_actuelle = value; }
        }

    }
}
