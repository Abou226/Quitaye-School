using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class Virement
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _source;

        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        private string _destination;

        public string Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private decimal _montant;

        public decimal Montant
        {
            get { return _montant; }
            set { _montant = value; }
        }

        private byte[] bin;

        public byte[] File_Bin
        {
            get { return bin; }
            set { bin = value; }
        }

    }
}
