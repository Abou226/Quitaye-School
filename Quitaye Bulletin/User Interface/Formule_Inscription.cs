using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Formule_Inscription : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Formule_Inscription()
        {
            InitializeComponent();
            timer1.Start();
            comboBox1.DataSource = SelectCatégorie();
            comboBox1.DisplayMember = "Nom_Compte";
            comboBox1.ValueMember = "Id";
        }

        public string ok;

        private void ch_Gratuit_CheckStateChanged(object sender, EventArgs e)
        {
            if (ch_Gratuit.CheckState == CheckState.Checked)
            {
                txtMontant.Clear();
                txtMontant.Enabled = false;
            }
            else txtMontant.Enabled = true;
        }

        public DataTable SelectCatégorie()
        {
            SqlConnection conn = new SqlConnection(mycontrng);

            DataTable dt = new DataTable();
            try
            {

                string sql = "SELECT * FROM tbl_Compte_Comptable WHERE Compte LIKE '3%' OR Compte LIKE '5%' OR Compte LIKE '4%' OR Compte LIKE '7%' OR Compte LIKE ' %'";

                //string sql = "SELECT * FROM tbl_Compte_Comptable";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                conn.Open();
                ad.Fill(dt);

            }
            catch (Exception ex)
            {
                MsgBox msg = new MsgBox();
                msg.show(ex.Message, "Error", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                msg.ShowDialog();
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       



        private async void btnAjouterFormule_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                using (var donnée = new QuitayeContext())
                {
                    if (txtFormule.Text != "" && (txtMontant.Text != "" || ch_Gratuit.Checked != false))
                    {
                        var formule = new Models.Context.tbl_formule_inscription();
                        formule.Auteur = Principales.profile;
                        formule.Date_Ajout = DateTime.Now;
                        formule.Formule = txtFormule.Text;
                        if (txtMontant.Text != "" && ch_Gratuit.CheckState == CheckState.Unchecked)
                        {
                            formule.Montant = Convert.ToDecimal(txtMontant.Text);
                            formule.Gratuit = "Non";
                        }

                        else if (ch_Gratuit.CheckState == CheckState.Checked)
                        {
                            formule.Gratuit = "Oui";
                            formule.Montant = 0;
                        }

                        if (checkFacture.Checked == true)
                        {
                            if (comboBox1.Text != "")
                            {
                                string[] arry = comboBox1.Text.Split('-');
                                formule.Compte = Convert.ToInt32(arry[0]) * 1;
                            }
                        }
                        donnée.tbl_formule_inscription.Add(formule);
                        await donnée.SaveChangesAsync();

                        Alert.SShow("Formule ajoutée avec succès.", Alert.AlertType.Sucess);
                        ok = "Oui";
                    }
                    else
                    {
                        Alert.SShow("Veillez remplir tous les zones de textes.", Alert.AlertType.Warning);
                    }
                }
            }
            else
            {
                if (LogIn.trial == true)
                {
                    Alert.SShow("Periode d'essay arrivé terme. Veillez-passez à la version payante !", Alert.AlertType.Info);
                }
                else Alert.SShow("Veillez renouveller votre abonnement !", Alert.AlertType.Info);
            }

        }

        public bool facture_derouler;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (facture_derouler)
            {
                panelFacture.Height += 10;
                panelFacture.Visible = true;
                if (panelFacture.Size == panelFacture.MaximumSize)
                {
                    timer1.Stop();
                    facture_derouler = false;
                }
            }
            else
            {
                panelFacture.Height -= 10;

                if (panelFacture.Size == panelFacture.MinimumSize)
                {
                    panelFacture.Visible = false;
                    timer1.Stop();
                    facture_derouler = true;
                }
            }
        }

        private void checkFacture_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
