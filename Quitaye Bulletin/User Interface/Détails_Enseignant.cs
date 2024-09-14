using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Détails_Enseignant : Form
    {
        string mycontrng = LogIn.mycontrng;
        public int id;
        public string cycle;
        public string classes;
        public string prenom;
        public string nom;
        public string genre;
        public string matricule;

        public Détails_Enseignant(int i)
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

            if (width <= 1300)
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


        protected override void WndProc(ref Message m)
        {
            //if (m.Msg == 0x84)
            //{
            //    Point pos = new Point(m.LParam.ToInt32());
            //    pos = this.PointToClient(pos);
            //    if (pos.Y < cCaption)
            //    {
            //        m.Result = (IntPtr)2;
            //        return;
            //    }
            //    if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
            //    {
            //        m.Result = (IntPtr)17;
            //        return;
            //    }
            //}
            //base.WndProc(ref m);


            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }

        public void FillData(int i)
        {
            using (var donnée = new QuitayeContext())
            {
                var eleve = (from d in donnée.tbl_enseignant where d.Id == i && (d.Active == "Oui" || d.Active == null) select d).First();

                lblContact1.Text = eleve.Contact1;
                lblContact2.Text = eleve.Contact2;
                lblNom.Text = eleve.Nom;
                lblPrenom.Text = eleve.Prenom;
               
                lblNom_Complet.Text = eleve.Nom_Complet;
                lblGenre.Text = eleve.Genre;
               
                lblDateNaissance.Text = eleve.Date_Naissance.Value.Date.ToString("dd/MM/yyyy");
                if (eleve.Image != null)
                    btnImage.Image = ByteArrayToImage(eleve.Image.ToArray());

                var se = from d in donnée.tbl_payement where d.N_Matricule == id.ToString() && d.Type == "Salaire" && (d.Cycle == "Enseignant" || d.Cycle == null) && d.Année_Scolaire == Principales.annéescolaire select d;
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

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnScolarité_Click(object sender, EventArgs e)
        {
            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_payement where d.N_Matricule == id.ToString() && d.Type == "Salaire" && (d.Cycle == "Enseignant" || d.Cycle == null) && d.Année_Scolaire == Principales.annéescolaire select d;
                decimal montant = 0;
                if (s.Count() > 0)
                {
                    montant = Convert.ToDecimal(s.Sum(x => x.Montant));
                }
                var ens = (from d in donnée.tbl_enseignant where d.Id == id select d).First();

                decimal scolarit = 0;
                Payer_Salaire scolarité = new Payer_Salaire();
                scolarité.staff = false;
                scolarité.payée = scolarit;
                scolarité.nom = ens.Nom;
                scolarité.prenom = ens.Prenom;
                scolarité.genre = ens.Genre;
                scolarité.id = id;
                scolarité.lblMontant.Text = "Montant Payée : " + montant.ToString("N0")+ " FCFA";
                scolarité.Location = ((FontAwesome.Sharp.IconButton)sender).Location;
                scolarité.ShowDialog();
                if (scolarité.ok == "Oui")
                {
                    FillData(id);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var donnée = new QuitayeContext())
            {
                var eleve = (from d in donnée.tbl_enseignant where d.Id == id  select d).First();
                Ajout_Enseignant inscription = new Ajout_Enseignant();
                //inscription.Size = new Size(802, 510);
                inscription.btnImage.Visible = true;
                inscription.txtAdresse.Text = eleve.Adresse;
                inscription.txtContact1.Text = eleve.Contact1;
                inscription.txtContact2.Text = eleve.Contact2;
                inscription.txtEmail.Text = eleve.Email;
                inscription.txtPrenom.Text = eleve.Prenom;
                inscription.txtNom.Text = eleve.Nom;
                inscription.txtNationalité.Text = eleve.Nationalité;
                inscription.btnImage.Image = ByteArrayToImage(eleve.Image.ToArray());
                //inscription.btnImage.Visible = false;
                inscription.NaissanceDate.Value = eleve.Date_Naissance.Value.Date;
                inscription.genre = eleve.Genre;
                inscription.exgenre = eleve.Genre;
                if (eleve.Genre == "Masculin")
                    inscription.radioButton1.Checked = true;
                else inscription.radioButton2.Checked = true;
                inscription.btnAjouter.Text = "Modifier";
                inscription.btnAjouter.IconChar = FontAwesome.Sharp.IconChar.Edit;
                inscription.lblTitre.Text = "Modification";
                inscription.ShowDialog();
                if (inscription.ok == "Oui")
                    FillData(id);
            }
        }

        private void btnAbsence_Click(object sender, EventArgs e)
        {
            
            Notifier_Absence absence = new Notifier_Absence();
            Notifier_Absence.cycle = cycle;
            Notifier_Absence.classe = classes;
            Notifier_Absence.nom_complet = prenom + " " + nom;
            Notifier_Absence.genre = genre;
            Notifier_Absence.matricule = matricule;
            absence.ShowDialog();
            if (absence.ok == "Oui")
            {
                FillData(id);
            }
        }
    }
}
