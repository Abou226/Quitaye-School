using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class Rédévance
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private decimal _montant;

        public decimal Montant
        {
            get { return _montant; }
            set { _montant = value; }
        }

        private string _redeveur;

        public string Rédéveur
        {
            get { return _redeveur; }
            set { _redeveur = value; }
        }

        private string _rédévant;

        public string Rédévant
        {
            get { return _rédévant; }
            set { _rédévant = value; }
        }


    }
}
