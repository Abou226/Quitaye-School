using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quitaye_School.Models.Context;

namespace Quitaye_School.User_Interface
{
    public partial class Custom_Absence : UserControl
    {
        public Custom_Absence()
        {
            InitializeComponent();
        }

        #region Properties

        //private string _matricule;
        //private string _nom;
        //private string _note;
        private int _id;
        private string _titre;
        private string _date;
        //private string _genre;

        [Category("Custom Props")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        //public int _ids;
        //public int Ids
        //{
        //    get { return _ids; }
        //    set { _ids = value; _id = value; }
        //}

        [Category("Custom Props")]
        public string Titre
        {
            get { return _titre; }
            set { _titre = value; lblTitre.Text = "Commentaire : " + value; }
        }

        [Category("Custom Props")]
        public string Date
        {
            get { return _date; }
            set { _date = value;  lblDate.Text = "Date Absence : " + value; }
        }

        #endregion

        private async void btnFermer_Click(object sender, EventArgs e)
        {
            using(var donnée = new QuitayeContext())
            {
                var se = (from d in donnée.tbl_notifier_absence where d.Id == Id select d).First();
                if(Principales.profile == se.Auteur || Principales.type_compte == "Administrateur")
                {
                    MsgBox msg = new MsgBox();
                    msg.show("Voulez-vous supprimer cette absence ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                    msg.ShowDialog();
                    if (msg.clicked == "Non")
                        return;
                    else if(msg.clicked == "Oui")
                    {
                        donnée.tbl_notifier_absence.Remove(se);
                        await donnée.SaveChangesAsync();
                        Détails_Elèves.refresh = true;
                    }
                }
            }
        }
    }
}
