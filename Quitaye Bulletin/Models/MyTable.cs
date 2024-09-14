using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class MyTable
    {
        private int _id;

        private DataTable myVar;

        private string _client;

        private decimal _montant_achat;

        private decimal _total_benefice;

        private decimal _montant_vente;

        private int _id_Client;

        private string _num_client;

        private decimal _montant;

        private decimal _quantité;

#pragma warning disable CS0169 // Le champ 'MyTable._total_quantité' n'est jamais utilisé
        private int _total_quantité;
#pragma warning restore CS0169 // Le champ 'MyTable._total_quantité' n'est jamais utilisé

        public string Client
        {
            get
            {
                return _client;
            }
            set
            {
                _client = value;
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public int Id_Client
        {
            get
            {
                return _id_Client;
            }
            set
            {
                _id_Client = value;
            }
        }

        public decimal Montant
        {
            get
            {
                return _montant;
            }
            set
            {
                _montant = value;
            }
        }

        public decimal Montant_Achat
        {
            get
            {
                return _montant_achat;
            }
            set
            {
                _montant_achat = value;
            }
        }

        public decimal Montant_Vente
        {
            get
            {
                return _montant_vente;
            }
            set
            {
                _montant_vente = value;
            }
        }

        public string Num_Client
        {
            get
            {
                return _num_client;
            }
            set
            {
                _num_client = value;
            }
        }

        public decimal Quantité
        {
            get
            {
                return _quantité;
            }
            set
            {
                _quantité = value;
            }
        }


        public int Total_Quantité
        {
            get { return total_quantité; }
            set { total_quantité = value; }
        }


        public DataTable Table
        {
            get
            {
                return myVar;
            }
            set
            {
                myVar = value;
            }
        }

        private (DataTable, DataTable) tables;

        public (DataTable, DataTable) Tables
        {
            get { return tables; }
            set { tables = value; }
        }

        private List<VenteList> venteLists;

        public List<VenteList> VenteLists
        {
            get { return venteLists; }
            set { venteLists = value; }
        }

        public decimal Total_Benefice
        {
            get
            {
                return _total_benefice;
            }
            set
            {
                _total_benefice = value;
            }
        }

        private int total_quantité;

        private decimal effectif;

        public decimal Effectif
        {
            get { return effectif; }
            set { effectif = value; }
        }


        private decimal fille;

        public decimal Fille
        {
            get { return fille; }
            set { fille = value; }
        }

        private decimal garçon;

        public decimal Garçon
        {
            get { return garçon; }
            set { garçon = value; }
        }


        private decimal montant_fille;

        public decimal Montant_Fille
        {
            get { return montant_fille; }
            set { montant_fille = value; }
        }


        private decimal montant_garçon;

        public decimal Montant_Garçon
        {
            get { return montant_garçon; }
            set { montant_garçon = value; }
        }


        //public int Total_Quantité
        //{
        //    get { return total_quantité; }
        //    set { total_quantité = value; }
        //}

    }
}
