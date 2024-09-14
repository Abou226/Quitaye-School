using FontAwesome.Sharp;
using Microsoft.Office.Interop.Excel;
using Quitaye_School.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;
using Quitaye_School.Models.Context;

namespace Quitaye_School.User_Interface
{
    public partial class Détails_Elèves : Form
    {
        private string mycontrng = LogIn.mycontrng;
        private Timer timerrefresh = new Timer();
        public string matricule;
        public string classes;
        public string prenom;
        public string nom;
        public string cycle;
        public string genre;
        private int temp;
        public string ok;
        public static bool refresh;
        public Détails_Elèves()
        {
            InitializeComponent();
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            temp = 1;
            timer1.Start();
            if (Principales.type_compte == "Administrateur")
            {
                btnEdit.Visible = true;
                btnScolarité.Visible = true;
                btnTranfère.Visible = true;
                btnDesactiver.Visible = true;
                btnAutreOpération.Visible = true;
            }
            if (Principales.departement == "Finance/Comptablité")
            {
                btnScolarité.Visible = true;
                btnAutreOpération.Visible = true;
            }
            if (Principales.departement == "Pédagogie" && Principales.classes == classes)
            {
                btnNote.Visible = true;
                btnBulletin.Visible = true;
            }
            System.Drawing.Rectangle virtualScreen = SystemInformation.VirtualScreen;
            int width = virtualScreen.Width;
            virtualScreen = SystemInformation.VirtualScreen;
            int height = virtualScreen.Height;
            if (width < 1024)
            {
                Width = 1000;
                Height = 620;
            }
            else if (width > 1300 && Width >= 1300)
            {
                Width = 1345;
                Height = 680;
            }
            timerrefresh.Enabled = false;
            timerrefresh.Interval = 10;
            timerrefresh.Start();
            timerrefresh.Tick += new EventHandler(Timerrefresh_Tick);
            btnAutreOpération.Click += new EventHandler(BtnAutreOpération_Click);
            btnInscription.Click += btnInscription_Click;
            btnAutreVente.Click += BtnAutreVente_Click;
        }

        private void BtnAutreVente_Click(object sender, EventArgs e)
        {
            var achat_vente = new AchatVente(matricule, true, false, false);
            achat_vente.ShowDialog();
        }

        private void BtnAutreOpération_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ecoleDataContext = new QuitayeContext())
                {
                    var source1 = ecoleDataContext.tbl_payement.Where((d => d.N_Matricule == matricule && d.Type == "Scolarité"));
                    Decimal num1 = 0M;
                    if (source1.Count() > 0)
                        num1 = Convert.ToDecimal(source1.Sum((x => x.Montant)));
                    var source2 = ecoleDataContext.tbl_scolarité.Where((d => d.Année_Scolaire == Principales.annéescolaire && d.Classe == classes));
                    Decimal num2;
                    if (source2.Count() != 0)
                        num2 = Convert.ToDecimal(source2.First().Montant);
                    else
                        num2 = Convert.ToDecimal(ecoleDataContext.tbl_inscription.Where((d => d.N_Matricule == matricule && d.Année_Scolaire == Principales.annéescolaire && d.Classe == classes)).First().Scolarité);
                    var source3 = ecoleDataContext.tbl_année_scolaire.OrderByDescending((d => d.Nom));
                    if (Principales.annéescolaire == source3.First().Nom)
                    {
                        var tblClasse = ecoleDataContext.tbl_classe.Where(d => d.Nom == classes).First();
                        Decimal num3 = !(num2 == 0M) ? Convert.ToDecimal(num2) : Convert.ToDecimal(tblClasse.Scolarité);
                        Autre_Payement autrePayement = new Autre_Payement();
                        autrePayement.lblReste.Text = "Reste : " + (num3 - num1).ToString();
                        autrePayement.matricule = matricule;
                        autrePayement.prenom = prenom;
                        autrePayement.nom = nom;
                        autrePayement.classes = classes;
                        autrePayement.cycle = cycle;
                        autrePayement.genre = genre;
                        autrePayement.Location = ((Control)sender).Location;
                        int num4 = (int)autrePayement.ShowDialog();
                        if (!(autrePayement.ok == "Oui"))
                            return;
                        FillData();
                    }
                    else
                    {
                        var tblScolarité = ecoleDataContext.tbl_scolarité.Where((d => d.Classe == classes && d.Année_Scolaire == Principales.annéescolaire)).First();
                        Decimal num5 = !(num2 == 0M) ? Convert.ToDecimal(num2) : Convert.ToDecimal(tblScolarité.Montant);
                        Autre_Payement autrePayement = new Autre_Payement();
                        autrePayement.lblReste.Text = "Reste : " + (num5 - num1).ToString();
                        autrePayement.matricule = matricule;
                        autrePayement.prenom = prenom;
                        autrePayement.nom = nom;
                        autrePayement.classes = classes;
                        autrePayement.cycle = cycle;
                        autrePayement.genre = genre;
                        autrePayement.Location = ((Control)sender).Location;
                        int num6 = (int)autrePayement.ShowDialog();
                        if (autrePayement.ok == "Oui")
                            FillData();
                    }
                }
            }
            catch (Exception ex)
            {
                if (!(ex is SqlException sqlException))
                {
                    sqlException = ex.InnerException as SqlException;
                    int errorCode = sqlException.ErrorCode;
                }
                if (sqlException == null)
                    return;
                if (sqlException.ErrorCode == -2146232060)
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                else
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
            }
        }

        private void Timerrefresh_Tick(object sender, EventArgs e)
        {
            if (!Détails_Elèves.refresh)
                return;
            timerrefresh.Stop();
            FillData();
            Détails_Elèves.refresh = false;
            timerrefresh.Start();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 132)
            {
                base.WndProc(ref m);
                if ((int)m.Result != 1)
                    return;
                System.Drawing.Point client = PointToClient(new System.Drawing.Point(m.LParam.ToInt32()));
                if (client.Y <= 10)
                {
                    if (client.X <= 10)
                    {
                        m.Result = (IntPtr)13;
                    }
                    else
                    {
                        int x = client.X;
                        int num = Size.Width - 10;
                        m.Result = x >= num ? (IntPtr)14 : (IntPtr)12;
                    }
                }
                else
                {
                    int y = client.Y;
                    Size size = Size;
                    int num1 = size.Height - 10;
                    if (y <= num1)
                    {
                        if (client.X <= 10)
                        {
                            m.Result = (IntPtr)10;
                        }
                        else
                        {
                            int x = client.X;
                            size = Size;
                            int num2 = size.Width - 10;
                            m.Result = x >= num2 ? (IntPtr)11 : (IntPtr)2;
                        }
                    }
                    else if (client.X <= 10)
                    {
                        m.Result = (IntPtr)16;
                    }
                    else
                    {
                        int x = client.X;
                        size = Size;
                        int num3 = size.Width - 10;
                        m.Result = x >= num3 ? (IntPtr)17 : (IntPtr)15;
                    }
                }
            }
            else
                base.WndProc(ref m);
        }

        private void btnFermer_Click(object sender, EventArgs e) => Close();

        private void btnScolarité_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ecoleDataContext = new QuitayeContext())
                {
                    var source1 = ecoleDataContext
                        .tbl_payement.Where(d => d.N_Matricule == matricule 
                    && d.Type == "Scolarité" 
                    && d.Année_Scolaire == Principales.annéescolaire);
                    Decimal num1 = 0M;
                    if (source1.Count() > 0)
                        num1 = Convert.ToDecimal(source1.Sum((x => x.Montant)));
                    var années = Principales.annéescolaire.Split('-');
                    var année = Convert.ToInt32(années[0]);
                    var source2 = ecoleDataContext
                        .tbl_scolarité.Where((d => d.Classe == classes
                        && d.Date.Value.Year < année));
                    Decimal num2;
                    if (source2.Count() != 0)
                        num2 = Convert.ToDecimal(source2.First().Montant);
                    else
                        num2 = Convert.ToDecimal(ecoleDataContext.tbl_inscription
                            .Where(d => d.N_Matricule == matricule 
                            && d.Année_Scolaire == Principales.annéescolaire
                            && d.Classe == classes).First().Scolarité);
                    var source3 = ecoleDataContext.tbl_année_scolaire.OrderByDescending(d => d.Nom);
                    if (Principales.annéescolaire == source3.First().Nom)
                    {
                        var tblClasse = ecoleDataContext.tbl_classe.Where(d => d.Nom == classes).First();
                        var num3 = !(num2 == 0M) ? Convert.ToDecimal(num2) : Convert.ToDecimal(tblClasse.Scolarité);
                        var scolaritéIndividuelle = new Scolarité_Individuelle();
                        scolaritéIndividuelle.lblScolarité.Text = "Montant à payer : " + num3.ToString("N0");
                        scolaritéIndividuelle.lblReste.Text = "Reste : " + (num3 - num1).ToString();
                        scolaritéIndividuelle.matricule = matricule;
                        scolaritéIndividuelle.prenom = prenom;
                        scolaritéIndividuelle.nom = nom;
                        scolaritéIndividuelle.classes = classes;
                        scolaritéIndividuelle.cycle = cycle;
                        scolaritéIndividuelle.genre = genre;
                        scolaritéIndividuelle.scolarité = num3;
                        scolaritéIndividuelle.Location = ((Control)sender).Location;
                        scolaritéIndividuelle.ShowDialog();
                        if ((scolaritéIndividuelle.ok == "Oui"))
                            return;
                        FillData();
                    }
                    else
                    {
                        var tblScolarité = ecoleDataContext.tbl_scolarité.Where(d => d.Classe == classes && d.Date.Value.Year < année).First();
                        decimal num5 = !(num2 == 0M) ? Convert.ToDecimal(num2) : Convert.ToDecimal(tblScolarité.Montant);
                        var scolaritéIndividuelle = new Scolarité_Individuelle();
                        scolaritéIndividuelle.lblScolarité.Text = "Montant à payer : " + num5.ToString("N0");
                        scolaritéIndividuelle.lblReste.Text = "Reste : " + (num5 - num1).ToString();
                        scolaritéIndividuelle.matricule = matricule;
                        scolaritéIndividuelle.prenom = prenom;
                        scolaritéIndividuelle.nom = nom;
                        scolaritéIndividuelle.classes = classes;
                        scolaritéIndividuelle.cycle = cycle;
                        scolaritéIndividuelle.genre = genre;
                        scolaritéIndividuelle.scolarité = num5;
                        scolaritéIndividuelle.Location = ((Control)sender).Location;
                        scolaritéIndividuelle.ShowDialog();
                        if (scolaritéIndividuelle.ok == "Oui")
                            FillData();
                    }
                }
            }
            catch (Exception ex)
            {
                if (!(ex is SqlException sqlException))
                {
                    sqlException = ex.InnerException as SqlException;
                    int errorCode = sqlException.ErrorCode;
                }
                if (sqlException == null)
                    return;
                if (sqlException.ErrorCode == -2146232060)
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                else
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
            }
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            try
            {
                using (QuitayeContext ecoleDataContext = new QuitayeContext())
                {
                    string id = matricule;
                    var eleve = ecoleDataContext.tbl_inscription.Where((d => d.N_Matricule == id && d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui")).First();
                    var tblClasse = ecoleDataContext.tbl_classe.Where((d => d.Nom == eleve.Classe)).First();
                    Ajout_Note ajoutNote = new Ajout_Note();
                    ajoutNote.lblTitre.Text = "Note " + eleve.Nom_Complet;
                    Ajout_Note.matricule = id;
                    Ajout_Note.eleve = eleve.Nom_Complet;
                    Ajout_Note.classes = eleve.Classe;
                    Ajout_Note.cycle = tblClasse.Cycle;
                    Ajout_Note.genre = eleve.Genre;
                    Ajout_Note.nom = eleve.Nom;
                    Ajout_Note.prenom = eleve.Prenom;
                    if (cycle == "Premier Cycle")
                    {
                        ajoutNote.label4.Visible = false;
                        ajoutNote.txtNoteClass.Visible = false;
                    }
                    ajoutNote.Location = ((Control)sender).Location;
                    int num = (int)ajoutNote.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                if (!(ex is SqlException sqlException))
                {
                    sqlException = ex.InnerException as SqlException;
                    int errorCode = sqlException.ErrorCode;
                }
                if (sqlException == null)
                    return;
                if (sqlException.ErrorCode == -2146232060)
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                else
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
            }
        }

        private void Data()
        {
            try
            {
                using (QuitayeContext ecoleDataContext = new QuitayeContext())
                {
                    var tblInscription = ecoleDataContext.tbl_inscription.Where((d => d.N_Matricule == matricule && d.Active == "Oui")).FirstOrDefault();
                    lblContact1.Text = tblInscription.Contact_1;
                    lblContact2.Text = tblInscription.Contact_2;
                    lblNom.Text = tblInscription.Nom;
                    lblPrenom.Text = tblInscription.Prenom;
                    lblPère.Text = tblInscription.Nom_Père;
                    lblNom_Complet.Text = tblInscription.Nom_Complet;
                    lblGenre.Text = tblInscription.Genre;
                    lblMère.Text = tblInscription.Nom_Mère;
                    lblDateNaissance.Text = tblInscription.Date_Naissance.Value.Date.ToString("dd/MM/yyyy");
                    if (tblInscription.Image != null)
                        btnImage.Image = ByteArrayToImage(tblInscription.Image.ToArray());
                    lblClasse.Text = tblInscription.Classe;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void FillData()
        {
            try
            {
                using (var ecoleDataContext = new QuitayeContext())
                {
                    var source1 = ecoleDataContext.tbl_année_scolaire.OrderByDescending((d => d.Nom));
                    if (Principales.annéescolaire == source1.First().Nom)
                    {
                        var tblInscription = ecoleDataContext.tbl_inscription
                            .Where(d => d.N_Matricule == matricule && d.Active == "Oui").First();
                        FillAbsence();
                        Decimal num1 = 0M;
                        var tblScolarité1 = ecoleDataContext
                            .tbl_scolarité.Where(d => d.Année_Scolaire == Principales.annéescolaire 
                            && d.Classe == classes).OrderByDescending(d => d.Id).FirstOrDefault();
                        Decimal num2;
                        if (tblScolarité1 != null)
                            num2 = Convert.ToDecimal(tblScolarité1.Montant);
                        else
                            num2 = Convert.ToDecimal(ecoleDataContext.tbl_scolarité
                                .Where(d => d.Classe == classes).FirstOrDefault().Montant);
                        num1 = num2;
                        num1 = Convert.ToDecimal(ecoleDataContext.tbl_inscription
                            .Where(d => d.Année_Scolaire == Principales.annéescolaire 
                            && d.Classe == classes).First().Scolarité);
                        lblContact1.Text = tblInscription.Contact_1;
                        lblContact2.Text = tblInscription.Contact_2;
                        lblNom.Text = tblInscription.Nom;
                        lblPrenom.Text = tblInscription.Prenom;
                        lblPère.Text = tblInscription.Nom_Père;
                        lblNom_Complet.Text = tblInscription.Nom_Complet;
                        lblGenre.Text = tblInscription.Genre;
                        lblMère.Text = tblInscription.Nom_Mère;
                        lblDateNaissance.Text = tblInscription.Date_Naissance.Value.Date.ToString("dd/MM/yyyy");
                        if (tblInscription.Image != null)
                            btnImage.Image = ByteArrayToImage(tblInscription.Image.ToArray());
                        lblClasse.Text = tblInscription.Classe;
                        var source2 = ecoleDataContext.tbl_scolarité.Where(d => d.Année_Scolaire == Principales.annéescolaire 
                        && d.Classe == classes);
                        var source3 = ecoleDataContext.tbl_payement.Where(d => d.Année_Scolaire == Principales.annéescolaire 
                        && d.N_Matricule == matricule && d.Type == "Scolarité");
                        decimal? nullable1 = tblInscription.Scolarité;
                        if (!nullable1.HasValue)
                        {
                            if (source2.Count() != 0)
                            {
                                lblMontantScolarité.Text = "Montant Scolarité : " + Convert.ToDecimal(source2.First().Montant).ToString("N0") + " FCFA";
                            }
                            else
                            {
                                Decimal num3;
                                if (tblScolarité1 != null)
                                    num3 = Convert.ToDecimal(tblScolarité1.Montant);
                                else
                                    num3 = Convert.ToDecimal(ecoleDataContext.tbl_scolarité.Where((d => d.Classe == classes)).OrderByDescending((d => d.Id)).FirstOrDefault().Montant);
                                lblMontantScolarité.Text = "Montant Scolarité : " + num3.ToString("N0") + " FCFA";
                            }
                        }
                        else
                            lblMontantScolarité.Text = "Montant Scolarité : " + Convert.ToDecimal(tblInscription.Scolarité).ToString("N0") + " FCFA";
                        lblMontantPayée.Text = "Montant Payée : " + Convert.ToDecimal(source3.Sum((x => x.Montant))).ToString("N0") + " FCFA";
                        Decimal num4 = Convert.ToDecimal(source3.Sum((x => x.Montant)));
                        if (num4 == 0M)
                        {
                            nullable1 = tblInscription.Scolarité;
                            if (!nullable1.HasValue)
                            {
                                if (tblInscription.Type_Scolarité == "Gratuit")
                                {
                                    
                                    int num5 = 0;
                                    string str1 = "Reste : " + num5.ToString() + " FCFA";
                                    lblReste.Text = str1;
                                    
                                    num5 = 0;
                                    string str2 = "Tranche 2 : " + num5.ToString() + " FCFA";
                                    lblTranche2.Text = str2;
                                    
                                    num5 = 0;
                                    string str3 = "Tranche 1 : " + num5.ToString() + " FCFA";
                                    lblTranche1.Text = str3;
                                   
                                    num5 = 0;
                                    string str4 = "Tranche 3 : " + num5.ToString() + " FCFA";
                                    lblTranche3.Text = str4;
                                }
                                else if (source2.Count() != 0)
                                {
                                    Decimal num6;
                                    if (tblScolarité1 != null)
                                        num6 = Convert.ToDecimal(tblScolarité1.Montant);
                                    else
                                        num6 = Convert.ToDecimal(ecoleDataContext.tbl_scolarité.Where((d => d.Classe == classes)).OrderByDescending((d => d.Id)).FirstOrDefault().Montant);
                                    Decimal num7 = num6;
                                    
                                    Decimal num8 = Convert.ToDecimal(num7);
                                    string str5 = "Reste : " + num8.ToString("N0") + " FCFA";
                                    lblReste.Text = str5;
                                    
                                    num8 = Convert.ToDecimal(source2.First().Tranche_2);
                                    string str6 = "Tranche 2 : " + num8.ToString("N0") + " FCFA";
                                    lblTranche2.Text = str6;
                                    lblTranche1.Text = "Tranche 1 : " + Convert.ToDecimal(source2.First().Tranche_1).ToString("N0") + " FCFA";
                                    lblTranche3.Text = "Tranche 3 : " + Convert.ToDecimal(source2.First().Tranche_3).ToString("N0") + " FCFA";
                                }
                                else
                                {
                                    var tblScolarité2 = new Models.Context.tbl_scolarité();
                                    if (tblScolarité1 == null)
                                        tblScolarité2 = ecoleDataContext.tbl_scolarité.Where((d => d.Classe == classes)).OrderByDescending((d => d.Id)).FirstOrDefault();
                                    else
                                        tblScolarité2 = ecoleDataContext.tbl_scolarité.Where((d => d.Classe == classes && d.Année_Scolaire == Principales.annéescolaire)).FirstOrDefault();
                                    
                                    Decimal num9 = Convert.ToDecimal(tblScolarité2.Montant);
                                    string str7 = "Reste : " + num9.ToString("N0") + " FCFA";
                                    lblReste.Text = str7;
                                    
                                    num9 = Convert.ToDecimal(tblScolarité2.Tranche_2);
                                    string str8 = "Tranche 2 : " + num9.ToString("N0") + " FCFA";
                                    lblTranche2.Text = str8;
                                    
                                    Decimal num10 = Convert.ToDecimal(tblScolarité2.Tranche_1);
                                    string str9 = "Tranche 1 : " + num10.ToString("N0") + " FCFA";
                                    lblTranche1.Text = str9;
                                    
                                    num10 = Convert.ToDecimal(tblScolarité2.Tranche_3);
                                    string str10 = "Tranche 3 : " + num10.ToString("N0") + " FCFA";
                                    lblTranche3.Text = str10;
                                }
                            }
                            else
                            {
                                Decimal d = Convert.ToDecimal(tblInscription.Scolarité) / 3M;
                                lblReste.Text = "Reste : " + Convert.ToDecimal(tblInscription.Scolarité).ToString("N0") + " FCFA";
                                lblTranche2.Text = "Tranche 2 : " + Math.Round(d, 1).ToString("N0") + " FCFA";
                                lblTranche1.Text = "Tranche 1 : " + Math.Round(d, 1).ToString("N0") + " FCFA";
                                lblTranche3.Text = "Tranche 3 : " + Math.Round(d, 1).ToString("N0") + " FCFA";
                            }
                        }
                        else
                        {
                            nullable1 = tblInscription.Scolarité;
                            if (!nullable1.HasValue)
                            {
                                if (source2.Count() != 0)
                                {
                                    
                                    nullable1 = source2.First().Montant;
                                    decimal? nullable2 = source3.Sum((x => x.Montant));
                                    string str11 = "Reste : " + Convert.ToDecimal((nullable1.HasValue & nullable2.HasValue ? new decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new decimal?())).ToString("N0") + " FCFA";
                                    lblReste.Text = str11;
                                    Decimal num11 = Convert.ToDecimal(source2.First().Tranche_1);
                                    Decimal num12 = num11;
                                    decimal? nullable3 = source2.First().Tranche_2;
                                    decimal? nullable4;
                                    if (!nullable3.HasValue)
                                    {
                                        nullable1 = new decimal?();
                                        nullable4 = nullable1;
                                    }
                                    else
                                        nullable4 = new decimal?(num12 + nullable3.GetValueOrDefault());
                                    Decimal num13 = Convert.ToDecimal(nullable4);
                                    Decimal num14 = num13;
                                    nullable3 = source2.First().Tranche_3;
                                    decimal? nullable5;
                                    if (!nullable3.HasValue)
                                    {
                                        nullable1 = new decimal?();
                                        nullable5 = nullable1;
                                    }
                                    else
                                        nullable5 = new decimal?(num14 + nullable3.GetValueOrDefault());
                                    Decimal num15 = Convert.ToDecimal(nullable5);
                                    nullable3 = tblInscription.Scolarité;
                                    if (!nullable3.HasValue)
                                    {
                                        Decimal num16;
                                        if (num4 >= num11)
                                        {
                                            lblTranche1.Text = "Tranche 1 : Payée";
                                        }
                                        else
                                        {
                                            
                                            num16 = num11 - num4;
                                            string str12 = "Tranche 1 : " + num16.ToString("N0") + " FCFA";
                                            lblTranche1.Text = str12;
                                        }
                                        if (num4 >= num13)
                                            lblTranche2.Text = "Tranche 2 : Payée";
                                        else if (num4 <= num11)
                                        {
                                            
                                            num16 = Convert.ToDecimal(source2.First().Tranche_2);
                                            string str13 = "Tranche 2 : " + num16.ToString("N0") + " FCFA";
                                            lblTranche2.Text = str13;
                                        }
                                        else
                                        {
                                            
                                            num16 = num13 - num4;
                                            string str14 = "Tranche 2 : " + num16.ToString("N0") + " FCFA";
                                            lblTranche2.Text = str14;
                                        }
                                        if (num4 >= num15)
                                            lblTranche3.Text = "Tranche 3 : Payée";
                                        else if (num4 <= num13)
                                        {
                                            
                                            num16 = Convert.ToDecimal(source2.First().Tranche_3);
                                            string str15 = "Tranche 3 : " + num16.ToString("N0") + " FCFA";
                                            lblTranche3.Text = str15;
                                        }
                                        else
                                        {
                                            
                                            num16 = num15 - num4;
                                            string str16 = "Tranche 3 : " + num16.ToString("N0") + " FCFA";
                                            lblTranche3.Text = str16;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                
                                decimal? scolarité = tblInscription.Scolarité;
                                nullable1 = source3.Sum((x => x.Montant));
                                string str17 = "Reste : " + Convert.ToDecimal((scolarité.HasValue & nullable1.HasValue ? new decimal?(scolarité.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new decimal?())).ToString("N0") + " FCFA";
                                lblReste.Text = str17;
                                Decimal d = Convert.ToDecimal(tblInscription.Scolarité) / 3M;
                                Decimal num17 = Math.Round(d);
                                Decimal num18 = Math.Round(d) * 2M;
                                Decimal num19 = Math.Round(d) * 3M;
                                Decimal num20;
                                if (num4 >= num17)
                                {
                                    lblTranche1.Text = "Tranche 1 : Payée";
                                }
                                else
                                {
                                    
                                    num20 = num17 - num4;
                                    string str18 = "Tranche 1 : " + num20.ToString("N0") + " FCFA";
                                    lblTranche1.Text = str18;
                                }
                                if (num4 >= num18)
                                    lblTranche2.Text = "Tranche 2 : Payée";
                                else if (num4 <= num17)
                                {
                                    
                                    num20 = Math.Round(d, 1);
                                    string str19 = "Tranche 2 : " + num20.ToString("N0") + " FCFA";
                                    lblTranche2.Text = str19;
                                }
                                else
                                {
                                    
                                    num20 = num18 - num4;
                                    string str20 = "Tranche 2 : " + num20.ToString("N0") + " FCFA";
                                    lblTranche2.Text = str20;
                                }
                                if (num4 >= num19)
                                    lblTranche3.Text = "Tranche 3 : Payée";
                                else if (num4 <= num18)
                                {
                                    
                                    num20 = Math.Round(d, 1);
                                    string str21 = "Tranche 3 : " + num20.ToString("N0") + " FCFA";
                                    lblTranche3.Text = str21;
                                }
                                else
                                {
                                    
                                    num20 = num19 - num4;
                                    string str22 = "Tranche 3 : " + num20.ToString("N0") + " FCFA";
                                    lblTranche3.Text = str22;
                                }
                            }
                        }
                    }
                    else
                    {
                        var tblInscription = ecoleDataContext.tbl_inscription.Where((d => d.N_Matricule == matricule && d.Active == "Oui")).First();
                        var source4 = ecoleDataContext.tbl_historiqueeffectif.Where((d => d.N_Matricule == matricule && d.Active == "Oui" && d.N_Matricule == matricule && d.Année_Scolaire == Principales.annéescolaire));
                        FillAbsence();
                        lblContact1.Text = tblInscription.Contact_1;
                        lblContact2.Text = tblInscription.Contact_2;
                        lblNom.Text = tblInscription.Nom;
                        lblPrenom.Text = tblInscription.Prenom;
                        lblPère.Text = tblInscription.Nom_Père;
                        lblNom_Complet.Text = tblInscription.Nom_Complet;
                        lblGenre.Text = tblInscription.Genre;
                        lblMère.Text = tblInscription.Nom_Mère;
                        lblDateNaissance.Text = tblInscription.Date_Naissance.Value.Date.ToString("dd/MM/yyyy");
                        if (tblInscription.Image != null)
                            btnImage.Image = ByteArrayToImage(tblInscription.Image.ToArray());
                        lblClasse.Text = tblInscription.Classe;
                        if (source4.Count() != 0)
                        {
                            var historiqueeffectif = source4.First();
                            var années = Principales.annéescolaire.Split('-');
                            var année = 2020;
                            if(années .Length > 0)
                            {
                                année = Convert.ToInt32(années[0]);
                            }
                            var tblScolarité3 = ecoleDataContext.tbl_scolarité.Where((d => d.Date.Value.Year <= année  && d.Classe == classes)).OrderByDescending(x => x.Date).FirstOrDefault();
                            Decimal num21 = 0M;
                            if (tblScolarité3 != null)
                                num21 = Convert.ToDecimal(tblScolarité3.Montant);
                            else
                                num21 = Convert.ToDecimal(ecoleDataContext.tbl_inscription.Where((d => d.Année_Scolaire == Principales.annéescolaire && d.Classe == classes)).First().Scolarité);
                            var tblScolarité4 = ecoleDataContext.tbl_scolarité.Where((d => d.Année_Scolaire == Principales.annéescolaire && d.Classe == classes)).FirstOrDefault();
                            var source5 = ecoleDataContext.tbl_payement.Where((d => d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == matricule && d.Type == "Scolarité")).ToList();
                            Decimal num22;
                            if (historiqueeffectif.Type_Scolarité == "Normal")
                            {
                                if (tblScolarité4 != null)
                                {
                                    Label montantScolarité = lblMontantScolarité;
                                    num22 = Convert.ToDecimal(tblScolarité4.Montant);
                                    string str = "Montant Scolarité : " + num22.ToString("N0") + " FCFA";
                                    montantScolarité.Text = str;
                                }
                                else
                                    lblMontantScolarité.Text = "Montant Scolarité : " + Convert.ToDecimal(ecoleDataContext.tbl_scolarité.Where((d => d.Classe == classes && d.Date.Value.Year <= année)).OrderByDescending(x => x.Date).First().Montant).ToString("N0") + " FCFA";
                            }
                            else
                                lblMontantScolarité.Text = "Montant Scolarité : " + Convert.ToDecimal(historiqueeffectif.Scolarité).ToString("N0") + " FCFA";
                            
                            num22 = Convert.ToDecimal(source5.Sum((x => x.Montant)));
                            string str23 = "Montant Payée : " + num22.ToString("N0") + " FCFA";
                            lblMontantPayée.Text = str23;
                            Decimal num23 = Convert.ToDecimal(source5.Sum((x => x.Montant)));
                            if (num23 == 0M)
                            {
                                if (!historiqueeffectif.Scolarité.HasValue)
                                {
                                    if (historiqueeffectif.Type_Scolarité == "Gratuit")
                                    {
                                        
                                        int num24 = 0;
                                        string str24 = "Reste : " + num24.ToString() + " FCFA";
                                        lblReste.Text = str24;
                                        
                                        num24 = 0;
                                        string str25 = "Tranche 2 : " + num24.ToString() + " FCFA";
                                        lblTranche2.Text = str25;
                                        
                                        num24 = 0;
                                        string str26 = "Tranche 1 : " + num24.ToString() + " FCFA";
                                        lblTranche1.Text = str26;
                                        
                                        num24 = 0;
                                        string str27 = "Tranche 3 : " + num24.ToString() + " FCFA";
                                        lblTranche3.Text = str27;
                                    }
                                    else if (tblScolarité4 != null)
                                    {
                                        
                                        num22 = Convert.ToDecimal(tblScolarité4.Montant);
                                        string str28 = "Reste : " + num22.ToString("N0") + " FCFA";
                                        lblReste.Text = str28;
                                        
                                        num22 = Convert.ToDecimal(tblScolarité4.Tranche_2);
                                        string str29 = "Tranche 2 : " + num22.ToString("N0") + " FCFA";
                                        lblTranche2.Text = str29;
                                        
                                        num22 = Convert.ToDecimal(tblScolarité4.Tranche_1);
                                        string str30 = "Tranche 1 : " + num22.ToString("N0") + " FCFA";
                                        lblTranche1.Text = str30;
                                        
                                        num22 = Convert.ToDecimal(tblScolarité4.Tranche_3);
                                        string str31 = "Tranche 3 : " + num22.ToString("N0") + " FCFA";
                                        lblTranche3.Text = str31;
                                    }
                                    else
                                    {
                                        var tblScolarité5 = ecoleDataContext.tbl_scolarité.Where((d => d.Classe == classes && d.Date.Value.Year <= année)).OrderByDescending(x => x.Date).First();
                                        
                                        num22 = Convert.ToDecimal(tblScolarité5.Montant);
                                        string str32 = "Reste : " + num22.ToString("N0") + " FCFA";
                                        lblReste.Text = str32;
                                        
                                        num22 = Convert.ToDecimal(tblScolarité5.Tranche_2);
                                        string str33 = "Tranche 2 : " + num22.ToString("N0") + " FCFA";
                                        lblTranche2.Text = str33;
                                        
                                        num22 = Convert.ToDecimal(tblScolarité5.Tranche_1);
                                        string str34 = "Tranche 1 : " + num22.ToString("N0") + " FCFA";
                                        lblTranche1.Text = str34;
                                        
                                        num22 = Convert.ToDecimal(tblScolarité5.Tranche_3);
                                        string str35 = "Tranche 3 : " + num22.ToString("N0") + " FCFA";
                                        lblTranche3.Text = str35;
                                    }
                                }
                                else
                                {
                                    Decimal d = Convert.ToDecimal(historiqueeffectif.Scolarité) / 3M;
                                    
                                    num22 = Convert.ToDecimal(historiqueeffectif.Scolarité);
                                    string str36 = "Reste : " + num22.ToString("N0") + " FCFA";
                                    lblReste.Text = str36;
                                    
                                    num22 = Math.Round(d, 1);
                                    string str37 = "Tranche 2 : " + num22.ToString("N0") + " FCFA";
                                    lblTranche2.Text = str37;
                                    
                                    num22 = Math.Round(d, 1);
                                    string str38 = "Tranche 1 : " + num22.ToString("N0") + " FCFA";
                                    lblTranche1.Text = str38;
                                    
                                    num22 = Math.Round(d, 1);
                                    string str39 = "Tranche 3 : " + num22.ToString("N0") + " FCFA";
                                    lblTranche3.Text = str39;
                                }
                            }
                            else if (!historiqueeffectif.Scolarité.HasValue)
                            {
                                if (tblScolarité4 != null)
                                {
                                    
                                    decimal? nullable6 = tblScolarité4.Montant;
                                    decimal? nullable7 = source5.Sum((x => x.Montant));
                                    num22 = Convert.ToDecimal((nullable6.HasValue & nullable7.HasValue ? new decimal?(nullable6.GetValueOrDefault() - nullable7.GetValueOrDefault()) : new decimal?()));
                                    string str40 = "Reste : " + num22.ToString("N0") + " FCFA";
                                    lblReste.Text = str40;
                                    Decimal num25 = Convert.ToDecimal(tblScolarité4.Tranche_1);
                                    num22 = num25;
                                    decimal? nullable8 = tblScolarité4.Tranche_2;
                                    decimal? nullable9;
                                    if (!nullable8.HasValue)
                                    {
                                        nullable6 = new decimal?();
                                        nullable9 = nullable6;
                                    }
                                    else
                                        nullable9 = new decimal?(num22 + nullable8.GetValueOrDefault());
                                    Decimal num26 = Convert.ToDecimal(nullable9);
                                    num22 = num26;
                                    nullable8 = tblScolarité4.Tranche_3;
                                    decimal? nullable10;
                                    if (!nullable8.HasValue)
                                    {
                                        nullable6 = new decimal?();
                                        nullable10 = nullable6;
                                    }
                                    else
                                        nullable10 = new decimal?(num22 + nullable8.GetValueOrDefault());
                                    Decimal num27 = Convert.ToDecimal(nullable10);
                                    nullable8 = historiqueeffectif.Scolarité;
                                    if (!nullable8.HasValue)
                                    {
                                        if (num23 >= num25)
                                        {
                                            lblTranche1.Text = "Tranche 1 : Payée";
                                        }
                                        else
                                        {
                                            
                                            num22 = num25 - num23;
                                            string str41 = "Tranche 1 : " + num22.ToString("N0") + " FCFA";
                                            lblTranche1.Text = str41;
                                        }
                                        if (num23 >= num26)
                                            lblTranche2.Text = "Tranche 2 : Payée";
                                        else if (num23 <= num25)
                                        {
                                            
                                            num22 = Convert.ToDecimal(tblScolarité4.Tranche_2);
                                            string str42 = "Tranche 2 : " + num22.ToString("N0") + " FCFA";
                                            lblTranche2.Text = str42;
                                        }
                                        else
                                        {
                                            
                                            num22 = num26 - num23;
                                            string str43 = "Tranche 2 : " + num22.ToString("N0") + " FCFA";
                                            lblTranche2.Text = str43;
                                        }
                                        if (num23 >= num27)
                                            lblTranche3.Text = "Tranche 3 : Payée";
                                        else if (num23 <= num26)
                                        {
                                            
                                            num22 = Convert.ToDecimal(tblScolarité4.Tranche_3);
                                            string str44 = "Tranche 3 : " + num22.ToString("N0") + " FCFA";
                                            lblTranche3.Text = str44;
                                        }
                                        else
                                        {
                                            
                                            num22 = num27 - num23;
                                            string str45 = "Tranche 3 : " + num22.ToString("N0") + " FCFA";
                                            lblTranche3.Text = str45;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (historiqueeffectif.Type_Scolarité == "Normal")
                                {
                                    
                                    decimal? montant = tblScolarité3.Montant;
                                    decimal? nullable = source5.Sum((x => x.Montant));
                                    num22 = Convert.ToDecimal((montant.HasValue & nullable.HasValue ? new decimal?(montant.GetValueOrDefault() - nullable.GetValueOrDefault()) : new decimal?()));
                                    string str46 = "Reste : " + num22.ToString("N0") + " FCFA";
                                    lblReste.Text = str46;
                                }
                                else if (historiqueeffectif.Type_Scolarité == "Avec Rémise")
                                {
                                    
                                    decimal? scolarité = historiqueeffectif.Scolarité;
                                    decimal? nullable = source5.Sum((x => x.Montant));
                                    num22 = Convert.ToDecimal((scolarité.HasValue & nullable.HasValue ? new decimal?(scolarité.GetValueOrDefault() - nullable.GetValueOrDefault()) : new decimal?()));
                                    string str47 = "Reste : " + num22.ToString("N0") + " FCFA";
                                    lblReste.Text = str47;
                                }
                                Decimal d = Convert.ToDecimal(historiqueeffectif.Scolarité) / 3M;
                                Decimal num28 = Math.Round(d);
                                Decimal num29 = Math.Round(d) * 2M;
                                Decimal num30 = Math.Round(d) * 3M;
                                if (num23 >= num28)
                                {
                                    lblTranche1.Text = "Tranche 1 : Payée";
                                }
                                else
                                {
                                    
                                    num22 = num28 - num23;
                                    string str48 = "Tranche 1 : " + num22.ToString("N0") + " FCFA";
                                    lblTranche1.Text = str48;
                                }
                                if (num23 >= num29)
                                    lblTranche2.Text = "Tranche 2 : Payée";
                                else if (num23 <= num28)
                                {
                                    
                                    num22 = Math.Round(d, 1);
                                    string str49 = "Tranche 2 : " + num22.ToString("N0") + " FCFA";
                                    lblTranche2.Text = str49;
                                }
                                else
                                {
                                    
                                    num22 = num29 - num23;
                                    string str50 = "Tranche 2 : " + num22.ToString("N0") + " FCFA";
                                    lblTranche2.Text = str50;
                                }
                                if (num23 >= num30)
                                    lblTranche3.Text = "Tranche 3 : Payée";
                                else if (num23 <= num29)
                                {
                                    
                                    num22 = Math.Round(d, 1);
                                    string str51 = "Tranche 3 : " + num22.ToString("N0") + " FCFA";
                                    lblTranche3.Text = str51;
                                }
                                else
                                {
                                    
                                    num22 = num30 - num23;
                                    string str52 = "Tranche 3 : " + num22.ToString("N0") + " FCFA";
                                    lblTranche3.Text = str52;
                                }
                            }
                        }
                        else
                        {
                            var années = Principales.annéescolaire.Split('-');
                            var année = Convert.ToInt32(années[0]);
                            var source6 = ecoleDataContext.tbl_scolarité.Where((d => d.Date.Value.Year < année && d.Classe == classes));
                            var source7 = ecoleDataContext.tbl_payement.Where((d => d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == matricule && d.Type == "Scolarité"));
                            if (!tblInscription.Scolarité.HasValue)
                            {
                                if (source6.Count() != 0)
                                    lblMontantScolarité.Text = "Montant Scolarité : " + Convert.ToDecimal(source6.First().Montant).ToString("N0") + " FCFA";
                                else
                                    lblMontantScolarité.Text = "Montant Scolarité : " + Convert.ToDecimal(ecoleDataContext.tbl_scolarité.Where((d => d.Classe == classes && d.Date.Value.Year < année)).First().Montant).ToString("N0") + " FCFA";
                            }
                            else
                                lblMontantScolarité.Text = "Montant Scolarité : " + Convert.ToDecimal(tblInscription.Scolarité).ToString("N0") + " FCFA";
                            lblMontantPayée.Text = "Montant Payée : " + Convert.ToDecimal(source7.Sum((x => x.Montant))).ToString("N0") + " FCFA";
                            Decimal num31 = Convert.ToDecimal(source7.Sum((x => x.Montant)));
                            if (num31 == 0M)
                            {
                                if (!tblInscription.Scolarité.HasValue)
                                {
                                    if (tblInscription.Type_Scolarité == "Gratuit")
                                    {
                                        
                                        int num32 = 0;
                                        string str53 = "Reste : " + num32.ToString() + " FCFA";
                                        lblReste.Text = str53;
                                        
                                        num32 = 0;
                                        string str54 = "Tranche 2 : " + num32.ToString() + " FCFA";
                                        lblTranche2.Text = str54;
                                        
                                        num32 = 0;
                                        string str55 = "Tranche 1 : " + num32.ToString() + " FCFA";
                                        lblTranche1.Text = str55;
                                        
                                        num32 = 0;
                                        string str56 = "Tranche 3 : " + num32.ToString() + " FCFA";
                                        lblTranche3.Text = str56;
                                    }
                                    else if (source6.Count() != 0)
                                    {
                                        lblReste.Text = "Reste : " + Convert.ToDecimal(source6.First().Montant).ToString("N0") + " FCFA";
                                        lblTranche2.Text = "Tranche 2 : " + Convert.ToDecimal(source6.First().Tranche_2).ToString("N0") + " FCFA";
                                        lblTranche1.Text = "Tranche 1 : " + Convert.ToDecimal(source6.First().Tranche_1).ToString("N0") + " FCFA";
                                        lblTranche3.Text = "Tranche 3 : " + Convert.ToDecimal(source6.First().Tranche_3).ToString("N0") + " FCFA";
                                    }
                                    else
                                    {
                                        var tblClasse = ecoleDataContext.tbl_classe.Where((d => d.Nom == classes)).First();
                                        lblReste.Text = "Reste : " + Convert.ToDecimal(tblClasse.Scolarité).ToString("N0") + " FCFA";
                                        lblTranche2.Text = "Tranche 2 : " + Convert.ToDecimal(tblClasse.Tranche_2).ToString("N0") + " FCFA";
                                        lblTranche1.Text = "Tranche 1 : " + Convert.ToDecimal(tblClasse.Tranche_1).ToString("N0") + " FCFA";
                                        lblTranche3.Text = "Tranche 3 : " + Convert.ToDecimal(tblClasse.Tranche_3).ToString("N0") + " FCFA";
                                    }
                                }
                                else
                                {
                                    Decimal d = Convert.ToDecimal(tblInscription.Scolarité) / 3M;
                                    lblReste.Text = "Reste : " + Convert.ToDecimal(tblInscription.Scolarité).ToString("N0") + " FCFA";
                                    lblTranche2.Text = "Tranche 2 : " + Math.Round(d, 1).ToString("N0") + " FCFA";
                                    lblTranche1.Text = "Tranche 1 : " + Math.Round(d, 1).ToString("N0") + " FCFA";
                                    lblTranche3.Text = "Tranche 3 : " + Math.Round(d, 1).ToString("N0") + " FCFA";
                                }
                            }
                            else if (!tblInscription.Scolarité.HasValue)
                            {
                                if (source6.Count() != 0)
                                {
                                    
                                    decimal? nullable11 = source6.First().Montant;
                                    decimal? nullable12 = source7.Sum((x => x.Montant));
                                    string str57 = "Reste : " + Convert.ToDecimal((nullable11.HasValue & nullable12.HasValue ? new decimal?(nullable11.GetValueOrDefault() - nullable12.GetValueOrDefault()) : new decimal?())).ToString("N0") + " FCFA";
                                    lblReste.Text = str57;
                                    Decimal num33 = Convert.ToDecimal(source6.First().Tranche_1);
                                    Decimal num34 = num33;
                                    decimal? nullable13 = source6.First().Tranche_2;
                                    decimal? nullable14;
                                    if (!nullable13.HasValue)
                                    {
                                        nullable11 = new decimal?();
                                        nullable14 = nullable11;
                                    }
                                    else
                                        nullable14 = new decimal?(num34 + nullable13.GetValueOrDefault());
                                    Decimal num35 = Convert.ToDecimal(nullable14);
                                    Decimal num36 = num35;
                                    nullable13 = source6.First().Tranche_3;
                                    decimal? nullable15;
                                    if (!nullable13.HasValue)
                                    {
                                        nullable11 = new decimal?();
                                        nullable15 = nullable11;
                                    }
                                    else
                                        nullable15 = new decimal?(num36 + nullable13.GetValueOrDefault());
                                    Decimal num37 = Convert.ToDecimal(nullable15);
                                    nullable13 = tblInscription.Scolarité;
                                    if (!nullable13.HasValue)
                                    {
                                        Decimal num38;
                                        if (num31 >= num33)
                                        {
                                            lblTranche1.Text = "Tranche 1 : Payée";
                                        }
                                        else
                                        {
                                            
                                            num38 = num33 - num31;
                                            string str58 = "Tranche 1 : " + num38.ToString("N0") + " FCFA";
                                            lblTranche1.Text = str58;
                                        }
                                        if (num31 >= num35)
                                            lblTranche2.Text = "Tranche 2 : Payée";
                                        else if (num31 <= num33)
                                        {
                                            
                                            num38 = Convert.ToDecimal(source6.First().Tranche_2);
                                            string str59 = "Tranche 2 : " + num38.ToString("N0") + " FCFA";
                                            lblTranche2.Text = str59;
                                        }
                                        else
                                        {
                                            
                                            num38 = num35 - num31;
                                            string str60 = "Tranche 2 : " + num38.ToString("N0") + " FCFA";
                                            lblTranche2.Text = str60;
                                        }
                                        if (num31 >= num37)
                                            lblTranche3.Text = "Tranche 3 : Payée";
                                        else if (num31 <= num35)
                                        {
                                            
                                            num38 = Convert.ToDecimal(source6.First().Tranche_3);
                                            string str61 = "Tranche 3 : " + num38.ToString("N0") + " FCFA";
                                            lblTranche3.Text = str61;
                                        }
                                        else
                                        {
                                            
                                            num38 = num37 - num31;
                                            string str62 = "Tranche 3 : " + num38.ToString("N0") + " FCFA";
                                            lblTranche3.Text = str62;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                
                                decimal? scolarité = tblInscription.Scolarité;
                                decimal? nullable = source7.Sum((x => x.Montant));
                                string str63 = "Reste : " + Convert.ToDecimal((scolarité.HasValue & nullable.HasValue ? new decimal?(scolarité.GetValueOrDefault() - nullable.GetValueOrDefault()) : new decimal?())).ToString("N0") + " FCFA";
                                lblReste.Text = str63;
                                Decimal d = Convert.ToDecimal(tblInscription.Scolarité) / 3M;
                                Decimal num39 = Math.Round(d);
                                Decimal num40 = Math.Round(d) * 2M;
                                Decimal num41 = Math.Round(d) * 3M;
                                Decimal num42;
                                if (num31 >= num39)
                                {
                                    lblTranche1.Text = "Tranche 1 : Payée";
                                }
                                else
                                {
                                    
                                    num42 = num39 - num31;
                                    string str64 = "Tranche 1 : " + num42.ToString("N0") + " FCFA";
                                    lblTranche1.Text = str64;
                                }
                                if (num31 >= num40)
                                    lblTranche2.Text = "Tranche 2 : Payée";
                                else if (num31 <= num39)
                                {
                                    
                                    num42 = Math.Round(d, 1);
                                    string str65 = "Tranche 2 : " + num42.ToString("N0") + " FCFA";
                                    lblTranche2.Text = str65;
                                }
                                else
                                {
                                    
                                    num42 = num40 - num31;
                                    string str66 = "Tranche 2 : " + num42.ToString("N0") + " FCFA";
                                    lblTranche2.Text = str66;
                                }
                                if (num31 >= num41)
                                    lblTranche3.Text = "Tranche 3 : Payée";
                                else if (num31 <= num40)
                                {
                                    
                                    num42 = Math.Round(d, 1);
                                    string str67 = "Tranche 3 : " + num42.ToString("N0") + " FCFA";
                                    lblTranche3.Text = str67;
                                }
                                else
                                {
                                    
                                    num42 = num41 - num31;
                                    string str68 = "Tranche 3 : " + num42.ToString("N0") + " FCFA";
                                    lblTranche3.Text = str68;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogControl.Write(ex.Message + "-" + ex.StackTrace);
                if (!(ex is SqlException sqlException))
                {
                    sqlException = ex.InnerException as SqlException;
                    int errorCode = sqlException.ErrorCode;
                }
                if (sqlException == null)
                    return;
                if (sqlException.ErrorCode == -2146232060)
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                else
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
            }
        }

        private void RefreshData()
        {
        }

        public Task<Elève> FillEleveAsync() => Task.Factory.StartNew((() => FillEleve()));

        public Elève FillEleve()
        {
            Elève elève = new Elève();
            using (var donnée = new QuitayeContext())
            {
                var source = donnée.tbl_payement.Where((d => d.N_Matricule == matricule && d.Année_Scolaire == Principales.annéescolaire && d.Type == "Scolarité")).Select(d => new
                {
                    Montant = d.Montant
                });
                var details_elève = donnée.tbl_inscription.Where((d => d.N_Matricule == matricule)).FirstOrDefault();
                var scolarité = donnée.tbl_scolarité.Where((d => d.Classe == classes && d.Année_Scolaire == Principales.annéescolaire)).FirstOrDefault();
                elève.Montant_Payé = Convert.ToDecimal(source.Sum(x => x.Montant));
                elève.Image = ByteArrayToImage(details_elève.Image.ToArray());
                elève.Prenom = details_elève.Prenom;
                elève.Nom = details_elève.Nom;
                elève.Mère = details_elève.Nom_Mère;
                elève.Père = details_elève.Nom_Père;
                elève.Date_Naissance = Convert.ToDateTime(details_elève.Date_Naissance);
                elève.Contact1 = details_elève.Contact_1;
                elève.Contact2 = details_elève.Contact_2;
                var data = donnée.tbl_année_scolaire.OrderBy((d => d.Nom)).Select(d => new
                {
                    Nom = d.Nom
                }).FirstOrDefault();
                if (data != null && data.Nom == Principales.annéescolaire)
                    ScolaritéAnnéeRecent(elève, donnée, details_elève, scolarité);
                else if (data != null && data.Nom != Principales.annéescolaire)
                    ScolaritéAnnéePassé(elève, donnée, scolarité);
            }
            return elève;
        }

        private void ScolaritéAnnéeRecent(
          Elève elève,
          QuitayeContext donnée,
          Models.Context.tbl_inscription details_elève,
          Models.Context.tbl_scolarité scolarité)
        {
            if (details_elève.Type_Scolarité == "Normal")
            {
                if (scolarité != null)
                {
                    elève.Montant_Scolarité = Convert.ToDecimal(scolarité.Montant);
                    elève.Tranche1 = Convert.ToDecimal(scolarité.Tranche_1);
                    elève.Tranche2 = Convert.ToDecimal(scolarité.Tranche_2);
                    elève.Tranche3 = Convert.ToDecimal(scolarité.Tranche_3);
                }
                else
                {
                    var tblScolarité = donnée.tbl_scolarité.Where((d => d.Classe == classes)).OrderByDescending((d => d.Id)).FirstOrDefault();
                    elève.Montant_Scolarité = Convert.ToDecimal(tblScolarité.Montant);
                    elève.Tranche1 = Convert.ToDecimal(tblScolarité.Tranche_1);
                    elève.Tranche2 = Convert.ToDecimal(tblScolarité.Tranche_2);
                    elève.Tranche3 = Convert.ToDecimal(tblScolarité.Tranche_3);
                }
            }
            else if (details_elève.Type_Scolarité == "Gratuit")
            {
                elève.Montant_Scolarité = 0M;
                int num = 0;
                elève.Tranche3 = (Decimal)num;
                elève.Tranche2 = (Decimal)num;
                elève.Tranche1 = (Decimal)num;
            }
            else
            {
                if (!(details_elève.Type_Scolarité == "Avec Remise"))
                    return;
                elève.Montant_Scolarité = Convert.ToDecimal(details_elève.Scolarité);
                Decimal num = Math.Round(elève.Montant_Scolarité / 3M, 0);
                elève.Tranche3 = num;
                elève.Tranche2 = num;
                elève.Tranche1 = num;
            }
        }

        private void ScolaritéAnnéePassé(
          Elève elève,
          QuitayeContext donnée,
          Models.Context.tbl_scolarité scolarité)
        {
            var historiqueeffectif = donnée.tbl_historiqueeffectif
                .Where((d => d.N_Matricule == matricule && d.Année_Scolaire == Principales.annéescolaire)).OrderByDescending(d => d.Id).FirstOrDefault();
            if (historiqueeffectif.Type_Scolarité == "Normal")
            {
                if (scolarité != null)
                {
                    elève.Montant_Scolarité = Convert.ToDecimal(scolarité.Montant);
                    elève.Tranche1 = Convert.ToDecimal(scolarité.Tranche_1);
                    elève.Tranche2 = Convert.ToDecimal(scolarité.Tranche_2);
                    elève.Tranche3 = Convert.ToDecimal(scolarité.Tranche_3);
                }
                else
                {
                    var tblScolarité = donnée.tbl_scolarité.Where((d => d.Classe == classes)).OrderByDescending((d => d.Id)).FirstOrDefault();
                    elève.Montant_Scolarité = Convert.ToDecimal(tblScolarité.Montant);
                    elève.Tranche1 = Convert.ToDecimal(tblScolarité.Tranche_1);
                    elève.Tranche2 = Convert.ToDecimal(tblScolarité.Tranche_2);
                    elève.Tranche3 = Convert.ToDecimal(tblScolarité.Tranche_3);
                }
            }
            else if (historiqueeffectif.Type_Scolarité == "Gratuit")
            {
                elève.Montant_Scolarité = 0M;
                int num = 0;
                elève.Tranche3 = (Decimal)num;
                elève.Tranche2 = (Decimal)num;
                elève.Tranche1 = (Decimal)num;
            }
            else
            {
                if (!(historiqueeffectif.Type_Scolarité == "Avec Remise"))
                    return;
                elève.Montant_Scolarité = Convert.ToDecimal(historiqueeffectif.Scolarité);
                Decimal num = Math.Round(elève.Montant_Scolarité / 3M, 0);
                elève.Tranche3 = num;
                elève.Tranche2 = num;
                elève.Tranche1 = num;
            }
        }
        
        private void FillAbsence()
        {
            try
            {
                using (QuitayeContext ecoleDataContext = new QuitayeContext())
                {
                    var queryable = ecoleDataContext.tbl_notifier_absence.Where((d => d.N_Matricule == matricule && d.Année_Scolaire == Principales.annéescolaire && d.Classe == classes && d.Cycle == cycle))
                        .OrderByDescending((d => d.Id)).Select(d => new
                    {
                        Id = d.Id,
                        Date = d.Date_Absence,
                        Commentaire = d.Commentaire
                    });
                    var customAbsenceList1 = new List<Custom_Absence>();
                    foreach (var data in queryable)
                    {
                        var customAbsenceList2 = customAbsenceList1;
                        var customAbsence = new Custom_Absence();
                        customAbsence.Id = data.Id;
                        customAbsence.Titre = data.Commentaire;
                        DateTime date = data.Date.Value;
                        date = date.Date;
                        customAbsence.Date = date.ToString("dd/MM/yyyyy");
                        customAbsenceList2.Add(customAbsence);
                    }
                    panelAbsence.Controls.Clear();
                    foreach (Control control in customAbsenceList1)
                        panelAbsence.Controls.Add(control);
                }
            }
            catch (Exception ex)
            {
                if (!(ex is SqlException sqlException))
                {
                    sqlException = ex.InnerException as SqlException;
                    int errorCode = sqlException.ErrorCode;
                }
                if (sqlException == null)
                    return;
                if (sqlException.ErrorCode == -2146232060)
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                else
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
            }
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArrayIn))
                return Image.FromStream((Stream)memoryStream);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (temp != 1)
                return;
            timer1.Stop();
            FillData();
            temp = 0;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                using (QuitayeContext ecoleDataContext = new QuitayeContext())
                {
                    var tblInscription = ecoleDataContext.tbl_inscription.Where((d => d.N_Matricule == matricule && d.Classe == classes)).First();
                    var formulaireInscription = new Formulaire_Inscription();
                    formulaireInscription.Size = new Size(892, 563);
                    formulaireInscription.txtAdresse.Text = tblInscription.Adresse;
                    formulaireInscription.txtContact1.Text = tblInscription.Contact_1;
                    formulaireInscription.txtContact2.Text = tblInscription.Contact_2;
                    formulaireInscription.txtEmail.Text = tblInscription.Email;
                    formulaireInscription.txtMatriculte.Text = tblInscription.N_Matricule;
                    formulaireInscription.txtPrenom.Text = tblInscription.Prenom;
                    formulaireInscription.txtNom.Text = tblInscription.Nom;
                    formulaireInscription.cbxMere.Text = tblInscription.Nom_Mère;
                    formulaireInscription.txtNationalité.Text = tblInscription.Nationalité;
                    formulaireInscription.cbxPère.Text = tblInscription.Nom_Père;
                    formulaireInscription.cbxClasse.Text = tblInscription.Classe;
                    formulaireInscription.cbxTypeScolarité.Text = tblInscription.Type_Scolarité;
                    if (tblInscription.Type_Scolarité == "Avec Rémise")
                    {
                        formulaireInscription.txtScolarité.Visible = true;
                        formulaireInscription.txtScolarité.Text = tblInscription.Scolarité.ToString();
                    }
                    Formulaire_Inscription.classe = tblInscription.Classe;
                    Formulaire_Inscription.matricule = tblInscription.N_Matricule;
                    formulaireInscription.Matricule = tblInscription.N_Matricule;
                    formulaireInscription.Mère = tblInscription.Nom_Mère;
                    formulaireInscription.Père = tblInscription.Nom_Père;
                    formulaireInscription.Classe = tblInscription.Classe;
                    formulaireInscription.btnPicture.Image = ByteArrayToImage(tblInscription.Image.ToArray());
                    formulaireInscription.btnImage.Visible = false;
                    formulaireInscription.NaissanceDate.Value = tblInscription.Date_Naissance.Value.Date;
                    Formulaire_Inscription.genre = tblInscription.Genre;
                    Formulaire_Inscription.exgenre = tblInscription.Genre;
                    if (tblInscription.Genre == "Masculin")
                        formulaireInscription.radioButton1.Checked = true;
                    else
                        formulaireInscription.radioButton2.Checked = true;
                    formulaireInscription.btnAjouter.Text = "Modifier";
                    formulaireInscription.btnAjouter.IconChar = IconChar.Edit;
                    formulaireInscription.lblTitre.Text = "Modification Inscription";
                    int num = (int)formulaireInscription.ShowDialog();
                    if (!(Formulaire_Inscription.ok == "Oui"))
                        return;
                    FillData();
                }
            }
            catch (Exception ex)
            {
                if (!(ex is SqlException sqlException))
                {
                    sqlException = ex.InnerException as SqlException;
                    int errorCode = sqlException.ErrorCode;
                }
                if (sqlException == null)
                    return;
                if (sqlException.ErrorCode == -2146232060)
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                else
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
            }
        }

        private void btnTranfère_Click(object sender, EventArgs e)
        {
            try
            {
                using (QuitayeContext ecoleDataContext = new QuitayeContext())
                {
                    var source = ecoleDataContext.tbl_payement.Where((d => d.N_Matricule == matricule && d.Type == "Scolarité"));
                    Decimal num1 = 0M;
                    if (source.Count() > 0)
                        num1 = Convert.ToDecimal(source.Sum((x => x.Montant)));
                    var tblInscription = ecoleDataContext.tbl_inscription.Where((d => d.Classe == classes && d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == matricule)).FirstOrDefault();
                    var tblScolarité = ecoleDataContext.tbl_scolarité.Where((d => d.Année_Scolaire == Principales.annéescolaire && d.Classe == classes)).FirstOrDefault();
                    Decimal num2;
                    if (tblScolarité != null)
                        num2 = Convert.ToDecimal(tblScolarité.Montant);
                    else
                        num2 = Convert.ToDecimal(ecoleDataContext.tbl_scolarité.Where((d => d.Classe == classes)).FirstOrDefault().Montant);
                    Decimal num3 = num2;
                    var transfèreEleve = new Transfère_Eleve();
                    transfèreEleve.lblTitre.Text = "Transfère (" + tblInscription.Nom_Complet + ")";
                    transfèreEleve.lblScolarité.Text = "Montant à payer : " + num3.ToString("N0");
                    transfèreEleve.lblReste.Text = "Reste : " + (num3 - num1).ToString();
                    transfèreEleve.matricule = matricule;
                    transfèreEleve.prenom = prenom;
                    transfèreEleve.nom = nom;
                    transfèreEleve.classes = classes;
                    transfèreEleve.cycle = cycle;
                    transfèreEleve.genre = genre;
                    transfèreEleve.scolarité = num3;
                    transfèreEleve.Location = new System.Drawing.Point(((Control)sender).Location.X - transfèreEleve.Width, ((Control)sender).Location.Y);
                    int num4 = (int)transfèreEleve.ShowDialog();
                    if (!(transfèreEleve.ok == "Oui"))
                        return;
                    ok = "Oui";
                    Close();
                }
            }
            catch (Exception ex)
            {
                if (!(ex is SqlException sqlException))
                {
                    sqlException = ex.InnerException as SqlException;
                    int errorCode = sqlException.ErrorCode;
                }
                if (sqlException == null)
                    return;
                if (sqlException.ErrorCode == -2146232060)
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                else
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
            }
        }

        private void btnBulletin_Click(object sender, EventArgs e)
        {
            Bulletin bulletin = new Bulletin();
            bulletin.matricule = matricule;
            bulletin.cycle = cycle;
            bulletin.clase = classes;
            bulletin.etat = "Normal";
            bulletin.lblTitre.Text = "Bulletin Scolaire " + prenom + " " + nom;
            int num = (int)bulletin.ShowDialog();
        }

        private async void btnDesactiver_Click(object sender, EventArgs e)
        {
            try
            {
                MsgBox msgBox = new MsgBox();
                msgBox.show("Voulez-vous desactivé cet élève/étudiant ?", "Desactivation", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                int num = (int)msgBox.ShowDialog();
                if (msgBox.clicked == "Non" || !(msgBox.clicked == "Oui"))
                    return;
                using (QuitayeContext ecoleDataContext = new QuitayeContext())
                {
                    var tblInscription = ecoleDataContext.tbl_inscription.Where((d => d.N_Matricule == matricule)).First();
                    tblInscription.Active = "Non";
                    tblInscription.Motif_Desactivation = "Desactivation";
                    tblInscription.Date_Desactivation = new DateTime?(DateTime.Now);
                    await ecoleDataContext.SaveChangesAsync();
                    string str = "";
                    if (tblInscription.Genre == "Feminin")
                        str = "Elle";
                    else if (tblInscription.Genre == "Masculin")
                        str = "Il";
                    ok = "Oui";
                    Alert.SShow(prenom + " " + nom + " a été desactivé avec succès. " + str + " ne sera plus visible parmi vos élève(s) et étudiant(s) !!", Alert.AlertType.Sucess);
                }
            }
            catch (Exception ex)
            {
                if (!(ex is SqlException sqlException))
                {
                    sqlException = ex.InnerException as SqlException;
                    int errorCode = sqlException.ErrorCode;
                }
                if (sqlException == null)
                    return;
                if (sqlException.ErrorCode == -2146232060)
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                else
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
            }
        }

        private void btnAbsence_Click(object sender, EventArgs e)
        {
            try
            {
                Notifier_Absence notifierAbsence = new Notifier_Absence();
                Notifier_Absence.cycle = cycle;
                Notifier_Absence.classe = classes;
                Notifier_Absence.nom_complet = prenom + " " + nom;
                Notifier_Absence.genre = genre;
                Notifier_Absence.matricule = matricule;
                int num = (int)notifierAbsence.ShowDialog();
                if (!(notifierAbsence.ok == "Oui"))
                    return;
                FillData();
            }
            catch (Exception ex)
            {
                if (!(ex is SqlException sqlException))
                {
                    sqlException = ex.InnerException as SqlException;
                    int errorCode = sqlException.ErrorCode;
                }
                if (sqlException == null)
                    return;
                if (sqlException.ErrorCode == -2146232060)
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                else
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
            }
        }

        private void btnInscription_Click(object sender, EventArgs e)
        {
            int num = (int)new Inscription_Individuel()
            {
                matricule = matricule
            }.ShowDialog();
        }
    }
}
