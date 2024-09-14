using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class Info_Moyenne
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private decimal _moyenne;

        public decimal Moyenne
        {
            get { return _moyenne; }
            set { _moyenne = value; }
        }

        private string _matricule;

        public string Matricule
        {
            get { return _matricule; }
            set { _matricule = value; }
        }

    }
}
