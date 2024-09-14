using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class Payement
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

        private string _mode_payement;

        private string _ref;

        public string Reference
        {
            get { return _ref; }
            set { _ref = value; }
        }

       

        public string Mode_Payement
        {
            get { return _mode_payement; }
            set { _mode_payement = value; }
        }

        private int _id_Client;

        public int Id_Client
        {
            get { return _id_Client; }
            set { _id_Client = value; }
        }

        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }


        private string _client;

        public string Client
        {
            get { return _client; }
            set { _client = value; }
        }

        private string _num_client;

        public string Num_Client
        {
            get { return _num_client; }
            set { _num_client = value; }
        }

        private decimal _reduction;

        public decimal Reduction
        {
            get { return _reduction; }
            set { _reduction = value; }
        }

        private string _num_opération;

        public string Num_Opération
        {
            get { return _num_opération; }
            set { _num_opération = value; }
        }

        private string _raison;

        public string Raison
        {
            get { return _raison; }
            set { _raison = value; }
        }


        private DateTime dateTime;

        public DateTime Date
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        private string commentaire;

        public string Commentaire
        {
            get { return commentaire; }
            set { commentaire = value; }
        }


    }
}
