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
    public partial class CustomRadio : UserControl
    {
        public CustomRadio()
        {
            InitializeComponent();
        }

        public static CustomRadio currentradio;
        #region Properties

        public string _titre;
        private string _radio;
        private bool _checked;
        public static int _id;


        [Category("Custom Props")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int _ids;
        public int Ids
        {
            get { return _ids; }
            set { _ids = value; _id = value; }
        }

        [Category("Custom Props")]
        public string Montant
        {
            get { return _titre; }
            set { _titre = value; lblMontant.Text = value; }
        }

        [Category("Custom Props")]
        public string Titre
        {
            get { return _radio; }
            set { _radio = value; lblRadio.Text = value; }
        }

        [Category("Custom Props")]
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; radioButton.Checked = value; }
        }

        #endregion

        

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lblRadio_Click(object sender, EventArgs e)
        {
            this.Checked = true;
        }

        private void PictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(152, 152, 242);
        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(4, 10, 97);
        }

        public static string profile;
        public static string id;
        public void CustomRadio_Click(object sender, EventArgs e)
        {
            if (this != null)
            {
                DisableButton();

                currentradio = this;
                currentradio.Checked = true;
                currentradio.Id = this.Id;
                id = this.Id.ToString();
                Ids = this.Ids;
                Id = Ids;
                profile = this.Titre;
            }
        }

        public static void DisableButton()
        {
            if (currentradio != null)
            {
                currentradio.Checked = false;
            }
        }
    }
}
