using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class Pret_Argent
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _preteur;

        public string Preteur
        {
            get { return _preteur; }
            set { _preteur = value; }
        }

        private string _pretant;

        public string Pretant
        {
            get { return _pretant; }
            set { _pretant = value; }
        }

        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private decimal _montant;

        public decimal Montant
        {
            get { return _montant; }
            set { _montant = value; }
        }

        private int _id_preteur;

        public int Id_Preteur
        {
            get { return _id_preteur; }
            set { _id_preteur = value; }
        }



        private string _mode_payement;

        public string Mode_Payement
        {
            get { return _mode_payement; }
            set { _mode_payement = value; }
        }


        private DateTime date;

        public DateTime Date_Echeance
        {
            get { return date; }
            set { date = value; }
        }

        private string _catégorie;

        public string Catégorie
        {
            get { return _catégorie; }
            set { _catégorie = value; }
        }

        private string _reference;

        public string Reference
        {
            get { return _reference; }
            set { _reference = value; }
        }

    }
}
