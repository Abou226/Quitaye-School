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
    public partial class Alert : Form
    {
        public Alert(string message, AlertType alert)
        {
            InitializeComponent();
            lblMessage.Text = message;
            switch (alert)
            {
                case AlertType.Sucess:
                    this.BackColor = Color.Cyan;
                    pictureBox1.Image = Properties.Resources.icons8_ok_50px;
                    break;
                case AlertType.Error:
                    this.BackColor = Color.Crimson;
                    pictureBox1.Image = Properties.Resources.icons8_cancel_50px;
                    break;
                case AlertType.Info:
                    this.BackColor = Color.Gray;
                    pictureBox1.Image = Properties.Resources.icons8_info_50px;
                    break;
                case AlertType.Warning:
                    this.BackColor = Color.Goldenrod;
                    pictureBox1.Image = Properties.Resources.icons8___warning_sign_50px;
                    break;
            }
        }

        public enum AlertType
        {
            Sucess, Warning, Info, Error
        }

        public static void SShow(string message, AlertType type)
        {
            new Quitaye_School.User_Interface.Alert(message, type).Show();
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            close.Start();
        }

        private void Alert_Load(object sender, EventArgs e)
        {
            this.Top = 60;
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 60;
            show.Start();
            timerout.Start();
        }

        private void timerout_Tick(object sender, EventArgs e)
        {
            close.Start();
        }

        int interval = 0;
        private void show_Tick(object sender, EventArgs e)
        {
            if (this.Top < 60)
            {
                this.Top += interval;
                interval += 2;
            }
            else
            {
                show.Stop();
            }
        }

        private void close_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.1;
            }
            else
            {
                this.Close();
            }
        }
    }
}
