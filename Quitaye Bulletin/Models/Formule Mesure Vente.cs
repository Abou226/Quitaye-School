using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class Formule_Mesure_Vente
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _mesure;

        public string Formule
        {
            get { return _mesure; }
            set { _mesure = value; }
        }

        private decimal _petit;

        public decimal Petit
        {
            get { return _petit; }
            set { _petit = value; }
        }

        private decimal _moyen;

        public decimal Moyen
        {
            get { return _moyen; }
            set { _moyen = value; }
        }

        private decimal _grand;

        public decimal Grand
        {
            get { return _grand; }
            set { _grand = value; }
        }

        private decimal _large;

        public decimal Large
        {
            get { return _large; }
            set { _large = value; }
        }

        private decimal _hyper_large;

        public decimal Hyper_Large
        {
            get { return _hyper_large; }
            set { _hyper_large = value; }
        }

    }
}
