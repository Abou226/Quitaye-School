using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class Partenaires
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _classe;

        public string Classe
        {
            get { return _classe; }
            set { _classe = value; }
        }


        private int id_partenaire;

        private string _matricule;

        public string Matricule
        {
            get { return _matricule; }
            set { _matricule = value; }
        }


        private string _prenom;

        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }

        public int Id_Partenaire
        {
            get { return id_partenaire; }
            set { id_partenaire = value; }
        }

        private string _nom;

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        private string _genre;

        public string Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        private string _adresse;

        public string Adresse
        {
            get { return _adresse; }
            set { _adresse = value; }
        }

        private string _telephone;

        public string Telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }


        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

    }
}
