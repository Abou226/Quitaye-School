using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Custom_Solde : UserControl
    {
        public Custom_Solde()
        {
            InitializeComponent();
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _titre;

        public string Titre
        {
            get { return _titre; }
            set { _titre = value; lblTitre.Text = value; }
        }

        private static string _titre1;

        public static string Titre1
        {
            get { return _titre1; }
            set { _titre1 = value;  }
        }


        private string _solde;

        public string Solde
        {
            get { return _solde; }
            set { _solde = value; lblSolde.Text = value; }
        }

        private static DateTime dateTime;

        public static DateTime StartDate
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        private static DateTime _endDate;

        public static DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        private static int _day;

        public static int Days
        {
            get { return _day; }
            set { _day = value; }
        }

        private object dataGridView;

        public object Data
        {
            get { return dataGridView; }
            set { dataGridView = value; }
        }


        private void btnValider_Click(object sender, EventArgs e)
        {
            Solde_Compte.operations = Titre;
                //Data = tbl;
                Solde_Compte.refresh = true;
        }
    }
}
