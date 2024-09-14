using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class FeedBack : Form
    {
        public FeedBack()
        {
            InitializeComponent();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtcommentaire_Click(object sender, EventArgs e)
        {
            if (txtcommentaire.Text.StartsWith("Donnez plus de details dans votre feedback cela nous aidera à mieux améliore vot"))
            {
                txtcommentaire.Clear();
            }
        }

        private void btnEnvoyer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtcommentaire.Text.StartsWith("Donnez plus de details dans votre feedback cela nous aidera à mieux améliore vot") && txtcommentaire.Text != "")
                {
                    using (var donnée = new DonnéeEcoleDataContext(GetConnectionsStrings.GetSConnectionString()))
                    {
                        int id = 1;
                        var ss = from d in donnée.tbl_feedback orderby d.Id descending select d;
                        if (ss.Count() != 0)
                        {
                            var s = (from d in donnée.tbl_feedback orderby d.Id descending select d).First();
                            id = s.Id + 1;
                        }

                        var feed = new tbl_feedback();
                        feed.Id = id;
                        feed.Entreprise = Principales.entreprise;
                        feed.Email_Entreprise = Program.EntrepriseEmail();
                        feed.Date = DateTime.Now;
                        feed.Commentaire = txtcommentaire.Text;
                        feed.Utilisateur = Principales.profile;
                        feed.Logiciel = "Quitaye School";

                        donnée.tbl_feedback.InsertOnSubmit(feed);
                        donnée.SubmitChanges();

                        txtcommentaire.Clear();
                        Alert.SShow("Feed Back envoyé avec succès. Merci pour votre contribution !!", Alert.AlertType.Sucess);
                    }
                }
            }
            catch (Exception ex)
            {
                var w32ex = ex as SqlException;
                if (w32ex == null)
                {
                    w32ex = ex.InnerException as SqlException;
                    int codes = w32ex.ErrorCode;
                }
                if (w32ex != null)
                {
                    int codes = w32ex.ErrorCode;
                    // do stuff

                    if (codes == -2146232060)
                    {
                        MsgBox msg = new MsgBox();
                        msg.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        msg.ShowDialog();
                    }
                    else
                    {
                        MsgBox msg = new MsgBox();
                        msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        msg.ShowDialog();
                    }
                }
            }
            
        }
    }
}
