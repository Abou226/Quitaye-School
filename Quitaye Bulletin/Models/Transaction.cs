using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class Transaction
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        private string reference;

        public string Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _num_ref;

        public string Num_Ref
        {
            get { return _num_ref; }
            set { _num_ref = value; }
        }


        private decimal _montant;

        public decimal Montant
        {
            get { return _montant; }
            set { _montant = value; }
        }

        private string _source;

        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private string _fournisseur;

        public string Fournisseur
        {
            get { return _fournisseur; }
            set { _fournisseur = value; }
        }

        private decimal _solde;

        public decimal Solde
        {
            get { return _solde; }
            set { _solde = value; }
        }


    }
}
