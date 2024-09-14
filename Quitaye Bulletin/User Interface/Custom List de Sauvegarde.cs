using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Quitaye_School.User_Interface
{
    public partial class Custom_List_de_Sauvegarde : UserControl
    {
        public Custom_List_de_Sauvegarde()
        {
            InitializeComponent();
            this.Click += Custom_List_de_Sauvegarde_Click;
            lblDescription.Click += Custom_List_de_Sauvegarde_Click;
            lblTitre.Click += Custom_List_de_Sauvegarde_Click;
            radioButton1.Click += Custom_List_de_Sauvegarde_Click;
        }

         
        public static void DisableButton()
        {
            if (currentobject != null)
            {
                currentobject.Checked = false;
            }
        }
        public static bool typeupdate = false;
        private void Custom_List_de_Sauvegarde_Click(object sender, EventArgs e)
        {
            if (this != null)
            {
                DisableButton();
                currentobject = this;
                currentobject.Checked = true;
                currentobject.Titre = this.Titre;
                currentobject.Description = this.Description;
                currentobject.Id = this.Id;
                typeupdate = true;
            }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private bool _checked;

        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; radioButton1.Checked = value; }
        }


        private string _nom;

        public string Titre
        {
            get { return _nom; }
            set { _nom = value; lblTitre.Text = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; lblDescription.Text = value; }
        }

        public static Custom_List_de_Sauvegarde currentobject;


    }
}
