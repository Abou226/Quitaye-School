using Quitaye_School.Models.Context;
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
    public partial class Edit_Staff : Form
    {
        public Edit_Staff()
        {
            InitializeComponent();
            FillCbx();
        }

        private void FillCbx()
        {
            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_responsabilité orderby d.Responsabilité ascending select d;
                cbxRole.DataSource = s;
                cbxRole.DisplayMember = "Responsabilité";
                cbxRole.ValueMember = "Id";
                cbxRole.Text = null;
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int id;
        private async void btnAjouter_Click(object sender, EventArgs e)
        {
            if (LogIn.expiré == false)
            {
                try
                {
                    if (txtAdresse.Text != "" && txtContact.Text != "" && txtNom.Text != "" && cbxRole.Text != "" && cbxGenre.Text != "")
                    {
                        using (var donnée = new QuitayeContext())
                        {

                            var s = (from d in donnée.tbl_staff where d.Id == id select d).First();

                            s.Role = cbxRole.Text;
                            s.Nom = txtNom.Text;
                            s.Genre = cbxGenre.Text;
                            s.Contact = txtContact.Text;
                            s.Adresse = txtAdresse.Text;
                            //s.Salaire = Convert.ToDecimal(txtSalaire.Text);
                            await donnée.SaveChangesAsync();
                            ClearData();
                            Alert.SShow("Employée modifié avec succès !", Alert.AlertType.Sucess);
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
                
            }else
            {
                if(LogIn.trial == true)
                {
                    Alert.SShow("Periode d'essay arrivé terme. Veillez-passez à la version payante !", Alert.AlertType.Info);
                }else Alert.SShow("Veillez renouveller votre abonnement !", Alert.AlertType.Info);
            }
        }

        private void ClearData()
        {
            txtNom.Clear();
            txtAdresse.Clear();
            txtContact.Clear();
            cbxGenre.Text = null;
            cbxRole.Text = null;
            txtSalaire.Clear();
        }
    }
}
