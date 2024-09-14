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
    public partial class Document_Comptable : Form
    {
        string mycontrng = LogIn.mycontrng;
        Form currentChildForm;
        public Document_Comptable()
        {
            InitializeComponent();


            foreach (FontAwesome.Sharp.IconButton item in flowLayoutPanel1.Controls)
            {
                if (item.Name != btnJournal.Name)
                {
                    item.ForeColor = Color.LightBlue;
                    item.IconColor = Color.LightBlue;
                }
                else
                {
                    btnJournal.ForeColor = Color.Cyan;
                    btnJournal.IconColor = Color.Cyan;
                }
            }
            panelRuban.Location = new Point(flowLayoutPanel1.Location.X + btnJournal.Location.X, flowLayoutPanel1.Location.Y + 45);
            panelRuban.Size = new Size(btnJournal.Size.Width, 3);
            ChildForm(new Rapport_Journal());
        }

        private void ChildForm(Form form)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            currentChildForm = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(form);
            panelContenedor.Tag = form;
            form.BringToFront();
            form.Show();
        }

        private void ScrollRuban(object sender)
        {
            foreach (FontAwesome.Sharp.IconButton item in flowLayoutPanel1.Controls)
            {
                if (item.Name != ((FontAwesome.Sharp.IconButton)sender).Name)
                {
                    item.ForeColor = Color.LightBlue;
                    item.IconColor = Color.LightBlue;
                }
                else
                {
                    ((FontAwesome.Sharp.IconButton)sender).ForeColor = Color.Cyan;
                    ((FontAwesome.Sharp.IconButton)sender).IconColor = Color.Cyan;
                }
            }
            panelRuban.Location = new Point(flowLayoutPanel1.Location.X + ((FontAwesome.Sharp.IconButton)sender).Location.X, flowLayoutPanel1.Location.Y + 45);
            panelRuban.Size = new Size(((FontAwesome.Sharp.IconButton)sender).Size.Width, 3);
        }



        private void btnCompteResultat_Click(object sender, EventArgs e)
        {
            ScrollRuban(sender);
            ChildForm(new Rapport_Compte_de_Resultat());
        }

        private void btnJournal_Click(object sender, EventArgs e)
        {
            ScrollRuban(sender);
            ChildForm(new Rapport_Journal());
        }

        private void btnGrandLivre_Click(object sender, EventArgs e)
        {
            ScrollRuban(sender);
            ChildForm(new Rapport_Grand_Livre());
        }

        private void btnBalanceGénéral_Click(object sender, EventArgs e)
        {
            ScrollRuban(sender);
            ChildForm(new Rapport_Balance_Général());
        }

        private void btnBilan_Click(object sender, EventArgs e)
        {
            ScrollRuban(sender);
        }

        private void btnFichierFEC_Click(object sender, EventArgs e)
        {
            ScrollRuban(sender);
        }
    }
}
