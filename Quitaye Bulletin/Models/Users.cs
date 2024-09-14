using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class Users
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _nom;

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        private string _prenom;

        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _adresse;

        public string Adresse
        {
            get { return _adresse; }
            set { _adresse = value; }
        }

        private string _role;

        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        private string _type_compte;

        public string Type_Compte
        {
            get { return _type_compte; }
            set { _type_compte = value; }
        }

        private string _departement;

        public string Departement
        {
            get { return _departement; }
            set { _departement = value; }
        }

        private string _telephone;

        public string Telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }

        private string _genre;

        public string Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }


        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _username;

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }


        private string _filiale;

        public string Filiale
        {
            get { return _filiale; }
            set { _filiale = value; }
        }


        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

    }

}
