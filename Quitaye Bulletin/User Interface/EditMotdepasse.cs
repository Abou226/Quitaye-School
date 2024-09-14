using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class EditMotdepasse : Form
    {
        public EditMotdepasse()
        {
            InitializeComponent();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string mycontrng = LogIn.mycontrng;

        public void SearchPassword()
        {
            using (var donnée = new QuitayeContext())
            {
                try
                {
                    if (txtPassword.Text != "" && txtUser.Text != "")
                    {
                        this.Cursor = Cursors.WaitCursor;
                        var s = from d in donnée.tbl_Users
                                where d.Password == txtPassword.Text && d.Username == txtUser.Text
                                select d;

                        if (s.Count() > 0)
                        {

                            var f = (from d in donnée.tbl_Users where d.Password == txtPassword.Text && d.Username == txtUser.Text select d).First();
                            // Principales.profile = f.Prenom + " " + f.Nom;
                            //Principales.role = f.Role;
                            EditMotdepasse edit = new EditMotdepasse();
                            edit.label2.Text = "Bienvenu " + f.Prenom + " " + f.Nom;
                            edit.label2.Visible = true;
                            edit.label4.Text = "Nouveau Nom Utilisateur ";
                            edit.label5.Text = "Nouveau Mot de passe";
                            edit.btnConnecter.Text = "MODIFIER";
                            edit.btnConnecter.IconChar = FontAwesome.Sharp.IconChar.Edit;
                            edit.id = f.Id;
                            edit.label3.Visible = true;
                            edit.textBox1.Visible = true;
                            edit.ShowDialog();
                            this.Close();
                            this.Cursor = Cursors.Default;
                        }
                        else
                        {
                            lblAvertiss.Visible = true;
                            lblAvertiss.Text = "Mot de passe ou nom d'utilisateur incorrect.";
                            this.Cursor = Cursors.Default;
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public int id;

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (btnConnecter.Text == "VERIFIER" && txtPassword.Text != "" && txtUser.Text != "")
            {
                SearchPassword();
            }
            else if(textBox1.Text != "" && txtUser.Text != "" && txtPassword.Text != "")
            {
                SetPassword();
            }
        }

        public async void SetPassword()
        {
            using (var donnée = new QuitayeContext())
            {
                var users = donnée.tbl_Users.SingleOrDefault(x => x.Id == id);
                if (textBox1.Text == txtPassword.Text)
                {
                    users.Username = txtUser.Text;
                    users.Password = txtPassword.Text;
                    await donnée.SaveChangesAsync();
                    
                        Alert.SShow("Votre mot de passe et nom d'utilisateur ont été modifié avec succès. Les modification prendront effet au demarrage.", Alert.AlertType.Sucess);
                    
                    this.Close();
                }
                else
                {
                    textBox1.ForeColor = Color.Red;
                    txtPassword.ForeColor = Color.Red;
                }
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 )
            {
                e.Handled = true;
                btnAjouter_Click(null, e);
            }
        }
    }
}
