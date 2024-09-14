using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Comparaison_Mesure : UserControl
    {
        public Comparaison_Mesure()
        {
            InitializeComponent();
            txtsearch.TextChanged += Txtsearch_TextChanged;
            txtsearch.KeyPress += Txtsearch_KeyPress;
        }

        private void Txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void Txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (txtsearch.Text != "" && txtsearch.Text.Length > 0)
                Quantité = Convert.ToDecimal(txtsearch.Text);
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _mesure;

        public string Mesure
        {
            get { return _mesure; }
            set { _mesure = value; lblMesure.Text = value + " :"; }
        }

        private decimal _qté;

        public decimal Quantité
        {
            get { return _qté; }
            set { _qté = value; txtsearch.Text = value.ToString(); }
        }

        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

    }
}
