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
    public partial class Saisie : Form
    {
        public Saisie()
        {
            InitializeComponent();
        }

        private void btnRecette_Click(object sender, EventArgs e)
        {
            Saisie_Comptable saisie = new Saisie_Comptable();
            saisie.lblTitleChildForm.Text = ((FontAwesome.Sharp.IconButton)sender).Text;
            saisie.iconCurrentChildForm.IconChar = ((FontAwesome.Sharp.IconButton)sender).IconChar;
            saisie.iconCurrentChildForm.IconColor = ((FontAwesome.Sharp.IconButton)sender).IconColor;
            saisie.ShowDialog();
        }

        private void btnDépenses_Click(object sender, EventArgs e)
        {
            Saisie_Comptable saisie = new Saisie_Comptable();
            saisie.lblTitleChildForm.Text = ((FontAwesome.Sharp.IconButton)sender).Text;
            saisie.iconCurrentChildForm.IconChar = ((FontAwesome.Sharp.IconButton)sender).IconChar;
            saisie.iconCurrentChildForm.IconColor = ((FontAwesome.Sharp.IconButton)sender).IconColor;
            saisie.ShowDialog();
        }
    }
}
