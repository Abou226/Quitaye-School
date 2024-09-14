using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class Mesure
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _mesure;

        public string Nom
        {
            get { return _mesure; }
            set { _mesure = value; }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private int _niveau;

        public int Niveau
        {
            get { return _niveau; }
            set { _niveau = value; }
        }

    }
}
