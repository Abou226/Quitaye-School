using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class InfoClasse
    {
        private int _id;
        private decimal _scolarité;
        private decimal _effectif;
        private decimal _fille;
        private int _garçon;
        private decimal _tranche1;
        private decimal _tranche2;
        private decimal _tranche3;
        private int _ajour;
        private decimal _retard;
        private decimal _resteScolarité;
        private decimal _scolarité_Payée;
        private string _annéeScolaire;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public decimal Scolarité
        {
            get => _scolarité;
            set => _scolarité = value;
        }

        public decimal Effectif
        {
            get => _effectif;
            set => _effectif = value;
        }

        public decimal Fille
        {
            get => _fille;
            set => _fille = value;
        }

        public int Garçon
        {
            get => _garçon;
            set => _garçon = value;
        }

        public decimal Tranche1
        {
            get => _tranche1;
            set => _tranche1 = value;
        }

        public decimal Tranche2
        {
            get => _tranche2;
            set => _tranche2 = value;
        }

        public decimal Tranche3
        {
            get => _tranche3;
            set => _tranche3 = value;
        }

        public int Ajour
        {
            get => _ajour;
            set => _ajour = value;
        }

        public decimal Retard
        {
            get => _retard;
            set => _retard = value;
        }

        public decimal Reste_Scolarité
        {
            get => _resteScolarité;
            set => _resteScolarité = value;
        }

        public decimal Scolarité_Payée
        {
            get => _scolarité_Payée;
            set => _scolarité_Payée = value;
        }

        public string Année_Scolaire
        {
            get => _annéeScolaire;
            set => _annéeScolaire = value;
        }
    }
}
