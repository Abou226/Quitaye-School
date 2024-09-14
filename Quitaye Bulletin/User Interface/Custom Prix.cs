using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Custom_Prix : UserControl
    {
        public Custom_Prix()
        {
            InitializeComponent();
            txtPrix.TextChanged += TxtPrix_TextChanged;
            txtPrix.KeyPress += TxtPrix_KeyPress;
        }

        private void TxtPrix_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void TxtPrix_TextChanged(object sender, EventArgs e)
        {
            if (txtPrix.Text != "")
            {
                txtPrix.Text = Convert.ToDecimal(txtPrix.Text).ToString("N0");
                txtPrix.SelectionStart = txtPrix.Text.Length;
                string prix_achat = Regex.Replace(txtPrix.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                Prix_Unité = Convert.ToDecimal(prix_achat);
            }
            
        }

        private int _d;

        public int Id
        {
            get { return _d; }
            set { _d = value; }
        }

        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _mesure;

        public string Mesure
        {
            get { return _mesure; }
            set { _mesure = value; label5.Text = "Prix_" + value; }
        }


        private decimal _prix;

        public decimal Prix_Unité
        {
            get { return _prix; }
            set { _prix = value; txtPrix.Text = value.ToString("N0"); }
        }

    }
}
