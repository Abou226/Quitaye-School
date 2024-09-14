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
    public partial class Details_Staff : Form
    {
        public Details_Staff(int i)
        {
            InitializeComponent();

            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            id = i;
            //timer1.Start();
            if (Principales.type_compte == "Administrateur")
            {
                btnEdit.Visible = true;
                btnScolarité.Visible = true;
                btnBulletin.Visible = true;
            }
            int width = SystemInformation.VirtualScreen.Width;
            int height = SystemInformation.VirtualScreen.Height;

            if (width < 1024)
            {
                this.Width = 1000;
                this.Height = 620;
            }
            else if (width > 1300)
            {
                this.Width = 1345;
                this.Height = 680;
            }
            FillData(i);
        }

        //public int id;
        public string cycle;
        public string classes;
        public string prenom;
        public string nom;
        public string genre;
        public string matricule;
        public string salaire;

        public int id;

        public void FillData(int i)
        {
            try
            {
                using (var donnée = new QuitayeContext())
                {
                    var eleve = (from d in donnée.tbl_staff where d.Id == i select d).First();

                    lblContact1.Text = eleve.Contact;
                    //lblContact2.Text = eleve.Contact2;
                    lblNom.Text = eleve.Nom;
                    lblNom.Text = eleve.Nom;

                    lblNom_Complet.Text = eleve.Nom;
                    lblAdresse.Text = eleve.Adresse;

                    lblGenre.Text = eleve.Genre;
                    lblRole.Text = eleve.Role;
                    //if (eleve.Image != null)
                    //    btnImage.Image = ByteArrayToImage(eleve.Image.ToArray());

                    var se = from d in donnée.tbl_payement where d.N_Matricule == id.ToString() && d.Type == "Salaire" && d.Cycle == "Staff" && d.Année_Scolaire == Principales.annéescolaire select d;
                    lblMontantScolarité.Text = "Montant Payée : " + Convert.ToDecimal(se.Sum(x => x.Montant)).ToString("N0") + " FCFA";
                    {
                        //var scolarité = (from d in donnée.tbl_classe where d.Nom == classes select d).First();
                        //var payée = from d in donnée.tbl_payement where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == matricule && d.Type == "Scolarité" select d;
                        //if (eleve.Scolarité == null)
                        //    lblMontantScolarité.Text = "Montant Scolarité : " + Convert.ToDecimal(scolarité.Scolarité).ToString("N0") + " FCFA";
                        //else lblMontantScolarité.Text = "Montant Scolarité : " + Convert.ToDecimal(eleve.Scolarité).ToString("N0") + " FCFA";
                        //lblMontantPayée.Text = "Montant Payée : " + Convert.ToDecimal(payée.Sum(x => x.Montant)).ToString("N0") + " FCFA";
                        //decimal payé = Convert.ToDecimal(payée.Sum(x => x.Montant));
                        //if (payé == 0)
                        //{
                        //    if (eleve.Scolarité == null)
                        //    {
                        //        lblReste.Text = "Reste : " + Convert.ToDecimal(scolarité.Scolarité).ToString("N0") + " FCFA";
                        //        lblTranche2.Text = "Tranche 2 : " + Convert.ToDecimal(scolarité.Tranche_2).ToString("N0") + " FCFA";
                        //        lblTranche1.Text = "Tranche 1 : " + Convert.ToDecimal(scolarité.Tranche_1).ToString("N0") + " FCFA";
                        //        lblTranche3.Text = "Tranche 3 : " + Convert.ToDecimal(scolarité.Tranche_3).ToString("N0") + " FCFA";
                        //    }
                        //    else
                        //    {
                        //        decimal scol = Convert.ToDecimal(eleve.Scolarité) / 3;
                        //        lblReste.Text = "Reste : " + Convert.ToDecimal(eleve.Scolarité).ToString("N0") + " FCFA";
                        //        lblTranche2.Text = "Tranche 2 : " + Math.Round(scol, 1).ToString("N0") + " FCFA";
                        //        lblTranche1.Text = "Tranche 1 : " + Math.Round(scol, 1).ToString("N0") + " FCFA";
                        //        lblTranche3.Text = "Tranche 3 : " + Math.Round(scol, 1).ToString("N0") + " FCFA";
                        //    }
                        //}
                        //else
                        //{
                        //    if (eleve.Scolarité == null)
                        //    {

                        //        lblReste.Text = "Reste : " + Convert.ToDecimal(scolarité.Scolarité - payée.Sum(x => x.Montant)).ToString("N0") + " FCFA";
                        //        decimal tranche1 = Convert.ToDecimal(scolarité.Tranche_1);
                        //        decimal tranche2 = Convert.ToDecimal(tranche1 + scolarité.Tranche_2);
                        //        decimal tranche3 = Convert.ToDecimal(tranche2 + scolarité.Tranche_3);

                        //        if (eleve.Scolarité == null)
                        //        {
                        //            if (payé >= tranche1)
                        //            {
                        //                lblTranche1.Text = "Tranche 1 : Payée";

                        //            }
                        //            else
                        //            {
                        //                lblTranche1.Text = "Tranche 1 : " + (tranche1 - payé).ToString("N0") + " FCFA";

                        //            }

                        //            if (payé >= tranche2)
                        //            {

                        //                lblTranche2.Text = "Tranche 2 : Payée";

                        //            }
                        //            else if (payé <= tranche1)
                        //            {
                        //                lblTranche2.Text = "Tranche 2 : " + Convert.ToDecimal(scolarité.Tranche_2).ToString("N0") + " FCFA";
                        //            }
                        //            else
                        //            {
                        //                lblTranche2.Text = "Tranche 2 : " + (tranche2 - payé).ToString("N0") + " FCFA";
                        //            }

                        //            if (payé >= tranche3)
                        //            {

                        //                lblTranche3.Text = "Tranche 3 : Payée";

                        //            }
                        //            else if (payé <= tranche2)
                        //            {
                        //                lblTranche3.Text = "Tranche 3 : " + Convert.ToDecimal(scolarité.Tranche_3).ToString("N0") + " FCFA";
                        //            }
                        //            else
                        //            {
                        //                lblTranche3.Text = "Tranche 3 : " + (tranche3 - payé).ToString("N0") + " FCFA";
                        //            }
                        //        }
                        //    }

                        //    else

                        //    {
                        //        lblReste.Text = "Reste : " + Convert.ToDecimal(eleve.Scolarité - payée.Sum(x => x.Montant)).ToString("N0") + " FCFA";

                        //        decimal scol = Convert.ToDecimal(eleve.Scolarité) / 3;
                        //        decimal tranche1 = Math.Round(scol);
                        //        decimal tranche2 = Math.Round(scol) * 2;
                        //        decimal tranche3 = Math.Round(scol) * 3;
                        //        if (payé >= tranche1)
                        //        {
                        //            lblTranche1.Text = "Tranche 1 : Payée";

                        //        }
                        //        else
                        //        {
                        //            lblTranche1.Text = "Tranche 1 : " + (tranche1 - payé).ToString("N0") + " FCFA";

                        //        }

                        //        if (payé >= tranche2)
                        //        {

                        //            lblTranche2.Text = "Tranche 2 : Payée";

                        //        }
                        //        else if (payé <= tranche1)
                        //        {
                        //            lblTranche2.Text = "Tranche 2 : " + Math.Round(scol, 1).ToString("N0") + " FCFA";
                        //        }
                        //        else
                        //        {
                        //            lblTranche2.Text = "Tranche 2 : " + (tranche2 - payé).ToString("N0") + " FCFA";
                        //        }

                        //        if (payé >= tranche3)
                        //        {

                        //            lblTranche3.Text = "Tranche 3 : Payée";

                        //        }
                        //        else if (payé <= tranche2)
                        //        {
                        //            lblTranche3.Text = "Tranche 3 : " + Math.Round(scol, 1).ToString("N0") + " FCFA";
                        //        }
                        //        else
                        //        {
                        //            lblTranche3.Text = "Tranche 3 : " + (tranche3 - payé).ToString("N0") + " FCFA";
                        //        }
                        //    }
                        //}
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

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnScolarité_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                try
                {
                    using (var donnée = new QuitayeContext())
                    {
                        var s = from d in donnée.tbl_payement where d.N_Matricule == id.ToString() && d.Type == "Salaire" && d.Cycle == "Staff" && d.Année_Scolaire == Principales.annéescolaire select d;
                        decimal montant = 0;
                        if (s.Count() > 0)
                        {
                            montant = Convert.ToDecimal(s.Sum(x => x.Montant));
                        }
                        var ens = (from d in donnée.tbl_staff where d.Id == id select d).First();

                        decimal scolarit = 0;
                        Payer_Salaire scolarité = new Payer_Salaire();
                        scolarité.staff = true;
                        scolarité.payée = scolarit;
                        scolarité.nom = ens.Nom;
                        scolarité.genre = ens.Genre;
                        scolarité.id = id;
                        scolarité.lblMontant.Text = "Montant Payée : " + montant.ToString("N0") + " FCFA";
                        scolarité.Location = ((FontAwesome.Sharp.IconButton)sender).Location;
                        scolarité.ShowDialog();
                        if (scolarité.ok == "Oui")
                        {
                            FillData(id);
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

        private void btnAbsence_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                using (var donnée = new QuitayeContext())
                {
                    var re = (from d in donnée.tbl_staff where d.Id == id select d).First();

                    Edit_Staff edit = new Edit_Staff();
                    edit.id = id;
                    edit.txtAdresse.Text = re.Adresse;
                    edit.txtContact.Text = re.Contact;
                    edit.cbxGenre.Text = re.Genre;
                    edit.cbxRole.Text = re.Role;
                    edit.txtNom.Text = re.Nom;
                    //edit.txtSalaire.Text = re.Salaire.ToString();
                    edit.ShowDialog();
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
