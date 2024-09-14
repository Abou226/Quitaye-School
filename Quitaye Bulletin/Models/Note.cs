using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class Note
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private decimal _note_classe;

        public decimal Note_Classe
        {
            get { return _note_classe; }
            set { _note_classe = value; }
        }

        private decimal _note_compo;

        public decimal Note_Compo
        {
            get { return _note_compo; }
            set { _note_compo = value; }
        }

        private string _classe;

        public string classe
        {
            get { return _classe; }
            set { _classe = value; }
        }

        private string _matière;

        public string Matière
        {
            get { return _matière; }
            set { _matière = value; }
        }

        private int _coeff;

        public int Coeff
        {
            get { return _coeff; }
            set { _coeff = value; }
        }



        private string _examen;

        public string Examen
        {
            get { return _examen; }
            set { _examen = value; }
        }

        private string _matricule;

        public string Matricule
        {
            get { return _matricule; }
            set { _matricule = value; }
        }
    }
}
