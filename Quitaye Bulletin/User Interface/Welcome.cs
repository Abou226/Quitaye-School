using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Welcome : Form
    {
        public Welcome(string noms)
        {
            InitializeComponent();
            lblUsername.Text = noms;
        }

        private void FadeIn_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) this.Opacity += 0.05;
            bunifuCircleProgressbar1.Value += 1;

            if (bunifuCircleProgressbar1.Value == 100)
            {
                FadeIn.Stop();
                FadeOut.Start();
            }
        }

        private void FadeOut_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.05;
            if (this.Opacity == 0)
            {
                FadeOut.Stop();
                this.Close();
            }
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.0;
            FadeIn.Start();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hwd, int pa1, int pa2, int pa3);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }



    }
}
