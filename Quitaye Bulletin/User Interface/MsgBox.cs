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
    public partial class MsgBox : Form
    {
        public MsgBox()
        {
            InitializeComponent();
        }

        public enum MsgBoxIcon
        {
            Information = 0, Succes = 1, Question = 2, Error = 3, Warning = 4
        };


        public enum MsgBoxButton
        {
            OK = 0, OuiNon = 1
        }
        public string clicked;
        public void show(string msg, string titre, MsgBoxButton button, MsgBoxIcon icon)
        {
            lblTitre.Text = titre;
            lblMessage.Text = msg;

            if (MsgBoxButton.OK == button)
            {
                btnNon.Visible = false;
                btnOui.Visible = false;
                btnOk.Visible = true;

            }
            else if (MsgBoxButton.OuiNon == button)
            {
                btnOk.Visible = false;
                btnNon.Visible = true;
                btnOui.Visible = true;

            }

            if (MsgBoxIcon.Error == icon)
            {
                btnIcon.Image = Properties.Resources.icons8_cancel_50px;
            }
            else if (MsgBoxIcon.Information == icon)
            {
                btnIcon.IconChar = FontAwesome.Sharp.IconChar.InfoCircle;
            }
            else if (MsgBoxIcon.Question == icon)
            {
                btnIcon.IconChar = FontAwesome.Sharp.IconChar.QuestionCircle;
            }
            else if (MsgBoxIcon.Warning == icon)
            {
                btnIcon.Image = Properties.Resources.icons8___warning_sign_50px;
            }
            else if (MsgBoxIcon.Succes == icon)
            {
                btnIcon.Image = Properties.Resources.icons8_ok_50px;
            }
        }

        private void BtnOui_Click(object sender, EventArgs e)
        {
            clicked = "Oui";
            this.Close();
        }

        private void BtnNon_Click(object sender, EventArgs e)
        {
            clicked = "Non";
            this.Close();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            clicked = "Ok";
            this.Close();
        }
        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
