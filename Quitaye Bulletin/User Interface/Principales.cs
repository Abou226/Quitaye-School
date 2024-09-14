using FontAwesome.Sharp;
using Quitaye_School.Models.Context;
using Quitaye_School.User_Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Quitaye_School
{
    
    public partial class Principales : Form
    {
        public static string profile;
        public static string role;
        public static string type_compte;
        public static string departement;
        public static string filiale;
        private string mycontrng = LogIn.mycontrng;
        private IconButton currentbtn;
        private Form currentChildForm;
        private Timer times = new Timer();
        public bool deroulecaisse;
        public bool deroulerstaff;
        private Timer timerPrincipale = new Timer();
        private Timer timercompta = new Timer();
        private Timer timerstaff = new Timer();
        private Timer timercaisse = new Timer();
        private Timer timercours = new Timer();
        public bool deroule_compta;
        public static string annéescolaire;
        public static string entreprise;
        private int temp;
        private Timer timerCompta = new Timer();
        private Timer timernote = new Timer();
        public static string auth1;
        public static string auth2;
        public static string auth3;
        public static string auth4;
        public static string classes;
        public static int id;
        public bool compta_deroule;
        public bool cours_deroule;
        private Timer timer = new Timer();
        public bool derouler_eleve;
        public bool derouler_note;
        public bool derouler_enseignant;

        public Principales()
        {
            InitializeComponent();
            if (!LogIn.trial)
            {
                if (LogIn.expiré)
                    lblEssay.Text = "Abonnement arrivé à terme !";
                else
                    lblEssay.Text = "Version Pro jusqu'au " + LogIn.date.Date.ToString("dd/MM/yyyy") + " !";
            }
            else if (LogIn.expiré)
                lblEssay.Text = "Periode essaie expiré !";
            else
                lblEssay.Text = "Periode essaie " + LogIn.days.ToString() + " jours !";
            lblEntreprise.Text = "Etablissement : " + Principales.entreprise;
            int width = SystemInformation.VirtualScreen.Width;
            int height = SystemInformation.VirtualScreen.Height;
            //if (width < 1024)
            //{
            //    Width = 1000;
            //    Height = 640;
            //}
            //else if (width > 1300)
            //{
            //    Width = 1345;
            //    Height = 680;
            //}
            leftborderbtn = new Panel();
            leftborderbtn.Size = new Size(7, 50);
            panel2.Controls.Add(leftborderbtn);
            TimerCommunauté.Start();
            TimerNote.Start();
            TimerEleve.Start();
            TimerComptabilité.Start();
            TimerEnseignant.Start();
            Text = string.Empty;
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
            timerCompta.Enabled = false;
            timerCompta.Interval = 10;
            timerCompta.Start();
            timerCompta.Tick += TimerComptabilité_Tick;
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            cbxClasse.DataSource = (object)SelectCatégorie();
            cbxClasse.DisplayMember = "Nom";
            cbxClasse.ValueMember = "Id";
            btnResultatFinance.Click += BtnResultatFinance_Click;
            temp = 1;
            timer1.Start();
            btnAchat.Click += BtnAchat_Click;
            timerPrincipale.Enabled = false;
            timerPrincipale.Interval = 10;
            timerPrincipale.Start();
            timerPrincipale.Tick += TimerPrincipale_Tick;
            timercompta.Enabled = false;
            timercompta.Interval = 10;
            timercompta.Start();
            timercompta.Tick += Timercompta_Tick;
            timerstaff.Enabled = false;
            timerstaff.Interval = 10;
            timerstaff.Start();
            timerstaff.Tick += Timerstaff_Tick;
            timercaisse.Enabled = false;
            timercaisse.Interval = 10;
            timercaisse.Start();
            timercaisse.Tick += Timercaisse_Tick;
            times.Enabled = false;
            times.Interval = 10;
            times.Start();
            times.Tick += Times_Tick;
            timercours.Enabled = false;
            timercours.Interval = 10;
            timercours.Start();
            timercours.Tick += Timercours_Tick;
            Tables();
            btnRapportAutrePayement.Click += BtnRapportAutrePayement_Click;
            btnListPassage.Click += BtnListPassage_Click;
            btnEditGroup.Click += BtnEditGroup_Click;
            btnAjustemtentPassage.Click += BtnAjustemtentPassage_Click;
            btnRapportVente.Click += BtnRapportVente_Click;
            btnRapportAchat.Click += BtnRapportAchat_Click;
            timerAchat_Vente.Enabled = false;
            timerAchat_Vente.Interval = 10;
            timerAchat_Vente.Start();
            timerAchat_Vente.Tick += TimerAchat_Vente_Tick;
            btnAchatVente.Click += BtnAchatVente_Click;
            btnProduit.Click += BtnProduit_Click;
            produitTimer.Enabled = false;
            produitTimer.Interval = 10;
            produitTimer.Start();
            produitTimer.Tick += ProduitTimer_Tick;
            btnInventaire.Click += BtnInventaire_Click;
            btnNiveauRentabilité.Click += BtnNiveauRentabilité_Click;
            btnRavitaillementStock.Click += BtnRavitaillementStock_Click;
            btnSituationExpiration.Click += BtnSituationExpiration_Click;
            btnPayementAchat.Click += BtnPayementAchat_Click;
            btbSoldeCompte.Click += BtnSoldeCompte_Click; ;
            btnPretRemboursement.Click += BtnPretRemboursement_Click;
            btnVirement.Click += BtnVirement_Click;
            btnTransactionFournisseur.Click += BtnTransactionFournisseur_Click;
            btnEmploiDuTempsClasse.Click += BtnEmploiDuTempsClasse_Click;
            btnEmploiDuTempsProfs.Click += BtnEmploiDuTempsProfs_Click;
        }

        private void BtnEmploiDuTempsProfs_Click(object sender, EventArgs e)
        {
            ChildForm(new List_Employ_Du_Temps(0, false));
        }

        private void BtnEmploiDuTempsClasse_Click(object sender, EventArgs e)
        {
            
        }

        private void Timercours_Tick(object sender, EventArgs e)
        {
            if (cours_deroule)
            {
                panelCours.Height += 10;
                if (!(panelCours.Size == panelCours.MaximumSize))
                    return;
                timercours.Stop();
                cours_deroule = false;
            }
            else
            {
                panelCours.Height -= 10;
                if (panelCours.Size == panelCours.MinimumSize)
                {
                    timercours.Stop();
                    cours_deroule = true;
                }
            }
        }

        private void BtnSoldeCompte_Click(object sender, EventArgs e)
        {
            ChildForm(new Solde_Compte());
        }

        private void BtnTransactionFournisseur_Click(object sender, EventArgs e)
        {
            ChildForm(new Transaction_Fournisseur());
        }

        private void BtnVirement_Click(object sender, EventArgs e)
        {
            ChildForm(new Virement_Interne());
        }

        private void BtnPretRemboursement_Click(object sender, EventArgs e)
        {
            ChildForm(new Pret_Remboursement());
        }

        private void BtnPayementAchat_Click(object sender, EventArgs e)
        {
            ChildForm(new Rapport_Payement());
        }

        private void BtnSituationExpiration_Click(object sender, EventArgs e)
        {
            ChildForm(new Situation_Expiration());
        }

        private void BtnRavitaillementStock_Click(object sender, EventArgs e)
        {
            ChildForm(new Ravitaillement());
        }

        private void BtnNiveauRentabilité_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnInventaire_Click(object sender, EventArgs e)
        {
            ChildForm(new Boutique());
        }

        public bool deroule_produit;
        private void ProduitTimer_Tick(object sender, EventArgs e)
        {
            if (deroule_produit)
            {
                panelProduit.Height += 10;
                if (!(panelProduit.Size == panelProduit.MaximumSize))
                    return;
                produitTimer.Stop();
                deroule_produit = false;
            }
            else
            {
                panelProduit.Height -= 10;
                if (panelProduit.Size == panelProduit.MinimumSize)
                {
                    produitTimer.Stop();
                    deroule_produit = true;
                }
            }
        }

        Timer produitTimer = new Timer();

        private void BtnProduit_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Principales.RGBColors.color3);
            produitTimer.Start();
        }
        
        private void BtnAchatVente_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Principales.RGBColors.color3);
            timerAchat_Vente.Start();
        }

        private void TimerAchat_Vente_Tick(object sender, EventArgs e)
        {
            if (deroule_achat_vente)
            {
                panelAchatVente.Height += 10;
                if (!(panelAchatVente.Size == panelAchatVente.MaximumSize))
                    return;
                timerAchat_Vente.Stop();
                deroule_achat_vente = false;
            }
            else
            {
                panelAchatVente.Height -= 10;
                if (panelAchatVente.Size == panelAchatVente.MinimumSize)
                {
                    timerAchat_Vente.Stop();
                    deroule_achat_vente = true;
                }
            }
        }

        Timer timerAchat_Vente = new Timer();
        bool deroule_achat_vente;
        private void BtnAchat_Click(object sender, EventArgs e)
        {
            
            ChildForm(new AchatVente());
        }

        private void BtnRapportAchat_Click(object sender, EventArgs e)
        {
            ChildForm(new Rapport_Achat());
        }

        private void BtnRapportVente_Click(object sender, EventArgs e)
        {
            ChildForm(new Rapport_Vente());
        }

        

        private void BtnAjustemtentPassage_Click(object sender, EventArgs e) => ChildForm(new Re_Passage_Elève());

        private void BtnEditGroup_Click(object sender, EventArgs e) => ChildForm(new Edit_Group_Classe());

        private void BtnListPassage_Click(object sender, EventArgs e) => ChildForm(new List_Passage());

        private void BtnRapportAutrePayement_Click(object sender, EventArgs e) => ChildForm(new Rapport_Autre_Payement());

        private async void Tables()
        {
            await Task.Delay(4000);
            Task t = new Task(new Action(PayementUpdate));
            t.Start();
            await t;
            t = null;
        }

        private async void PayementUpdate()
        {
            using (var ecoleDataContext = new QuitayeContext())
            {
                var queryable1 = ecoleDataContext.tbl_inscription.Select(d => new
                {
                    Matricule = d.N_Matricule,
                    Année = d.Année_Scolaire,
                    Prenom = d.Prenom,
                    Nom = d.Nom,
                    Classe = d.Classe,
                    Cycle = d.Cycle
                }).ToList();
                var queryable2 = ecoleDataContext.tbl_année_scolaire.Select(d => d).ToList();
                foreach (var data in queryable1)
                {
                    var items = data;
                    foreach (var tblAnnéeScolaire in queryable2)
                    {
                        var item = tblAnnéeScolaire;
                        if (ecoleDataContext.tbl_payement.Where((d => d.N_Matricule == items.Matricule && d.Année_Scolaire == item.Nom)).Count() == 0)
                        {
                            var source = ecoleDataContext.tbl_payement.OrderByDescending((d => d.Id)).Select(d => new
                            {
                                Id = d.Id
                            }).ToList().Take(1);
                            int num = 1;
                            if (source.Count() != 0)
                                num = source.First().Id;
                            ecoleDataContext.tbl_payement.Add(new Models.Context.tbl_payement()
                            {
                                Id = num + 1,
                                Montant = new Decimal?(0M),
                                N_Matricule = items.Matricule,
                                Prenom = items.Prenom,
                                Nom = items.Nom,
                                Classe = items.Classe,
                                Cycle = items.Cycle,
                                Année_Scolaire = item.Nom
                            });
                            await ecoleDataContext.SaveChangesAsync();
                        }
                    }
                }
            }
        }

        private void BtnResultatFinance_Click(object sender, EventArgs e) => ChildForm((Form)new Resultat_Finance());

        private void Times_Tick(object sender, EventArgs e)
        {
            times.Stop();
            lblEssay.Visible = true;
            if (LogIn.enligne)
            {
                if (!LogIn.trial)
                {
                    if (LogIn.days > 5 || LogIn.days < 0)
                        return;
                    Alert.SShow("Votre abonnement reste " + LogIn.days.ToString() + " jours. Veillez vous réabonner, pour continuer vos opérations !", Alert.AlertType.Info);
                }
                else
                {
                    if (LogIn.expiré)
                        lblEssay.Text = "Periode essaie expiré !";
                    else
                        lblEssay.Text = "Periode essaie " + LogIn.days.ToString() + " jours !";
                    if (LogIn.days <= 5 && LogIn.days >= 0)
                        Alert.SShow("Votre periode d'essaie reste " + LogIn.days.ToString() + " jours. Veillez vous réabonner, pour continuer vos opérations !", Alert.AlertType.Info);
                }
            }
            else
                lblEssay.Text = "Version Hors Ligne !";
        }

        private void Timercaisse_Tick(object sender, EventArgs e)
        {
            if (deroulecaisse)
            {
                panelCaisse.Height += 10;
                if (!(panelCaisse.Size == panelCaisse.MaximumSize))
                    return;
                timercaisse.Stop();
                deroulecaisse = false;
            }
            else
            {
                panelCaisse.Height -= 10;
                if (panelCaisse.Size == panelCaisse.MinimumSize)
                {
                    timercaisse.Stop();
                    deroulecaisse = true;
                }
            }
        }

        private void Timerstaff_Tick(object sender, EventArgs e)
        {
            if (deroulerstaff)
            {
                panelStaff.Height += 10;
                if (!(panelStaff.Size == panelStaff.MaximumSize))
                    return;
                timerstaff.Stop();
                deroulerstaff = false;
            }
            else
            {
                panelStaff.Height -= 10;
                if (panelStaff.Size == panelStaff.MinimumSize)
                {
                    timerstaff.Stop();
                    deroulerstaff = true;
                }
            }
        }

        private void TimerPrincipale_Tick(object sender, EventArgs e)
        {
            timerPrincipale.Stop();
            SetAuthorization();
        }

        private void Timercompta_Tick(object sender, EventArgs e)
        {
            if (deroule_compta)
            {
                panelComptabilité.Height += 10;
                if (!(panelComptabilité.Size == panelComptabilité.MaximumSize))
                    return;
                timercompta.Stop();
                deroule_compta = false;
            }
            else
            {
                panelComptabilité.Height -= 10;
                if (panelComptabilité.Size == panelComptabilité.MinimumSize)
                {
                    timercompta.Stop();
                    deroule_compta = true;
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (derouler_eleve)
            {
                panelEleve.Height += 10;
                if (!(panelEleve.Size == panelEleve.MaximumSize))
                    return;
                timer.Stop();
                derouler_eleve = false;
            }
            else
            {
                panelEleve.Height -= 10;
                if (panelEleve.Size == panelEleve.MinimumSize)
                {
                    timer.Stop();
                    derouler_eleve = true;
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 132)
            {
                base.WndProc(ref m);
                if ((int)m.Result != 1)
                    return;
                Point client = PointToClient(new Point(m.LParam.ToInt32()));
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

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.Style |= 131072;
                return createParams;
            }
        }

        private void SetAuthorization()
        {
            if (Principales.type_compte == "Administrateur")
            {
                panelMenu.Visible = true;
                btnDashboard.Visible = true;
                panelAchatVente.Visible = true;
                panelEleve.Visible = true;
                panelDesktop.Visible = true;
                panelProduit.Visible = true;
                panelEnseignant.Visible = true;
                panelComptabilité.Visible = true;
                panelNote.Visible = true;
                panelFinance.Visible = true;
                panelCours.Visible = true;
                panelStatistique.Visible = true;
                panelParamettre.Visible = true;
                panelCaisse.Visible = true;
                panelStaff.Visible = true;
            }
            else if (Principales.departement == "Pédagogie")
            {
                panelEnseignant.Visible = true;
                panelCours.Visible= true;
                panelNote.Visible = true;
            }
            else if (Principales.departement == "Finance/Comptabilité")
            {
                panelFinance.Visible = true;
                panelAchatVente.Visible = true;
                panelComptabilité.Visible = true;
                panelEleve.Visible = true;
                panelCaisse.Visible = true;
                panelStaff.Visible = true;
            }
            else if (Principales.departement == "Administration")
            {
                panelEleve.Visible = true;
                panelStaff.Visible = true;
                panelNote.Visible = true;
                panelCours.Visible = true;
            }
        }

        public DataTable SelectCatégorie()
        {
            SqlConnection connection = new SqlConnection(mycontrng);
            DataTable dataTable = new DataTable();
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SELECT * FROM tbl_année_scolaire ORDER BY Nom DESC", connection));
                connection.Open();
                sqlDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MsgBox msgBox = new MsgBox();
                msgBox.show(ex.Message, "Error", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                int num = (int)msgBox.ShowDialog();
            }
            finally
            {
                connection.Close();
            }
            return dataTable;
        }

        public void ActivateButton(object sender, Color color)
        {
            if (sender == null)
                return;
            DisableButton();
            currentbtn = (IconButton)sender;
            currentbtn.BackColor = Color.FromArgb(37, 36, 81);
            currentbtn.ForeColor = color;
            currentbtn.TextAlign = ContentAlignment.MiddleCenter;
            currentbtn.IconColor = color;
            currentbtn.TextImageRelation = TextImageRelation.TextBeforeImage;
            currentbtn.ImageAlign = ContentAlignment.MiddleRight;
            iconCurrentChildForm.IconChar = currentbtn.IconChar;
            iconCurrentChildForm.IconColor = currentbtn.IconColor;
            lblTitleChildForm.Text = currentbtn.Text;
            int y = currentbtn.Location.Y;
            int x = currentbtn.Location.X;
            leftborderbtn.BackColor = color;
            leftborderbtn.Visible = true;
            leftborderbtn.BringToFront();
        }

        public void DisableButton()
        {
            if (currentbtn == null)
                return;
            currentbtn.BackColor = Color.FromArgb(4, 10, 97);
            currentbtn.ForeColor = Color.LightBlue;
            currentbtn.TextAlign = ContentAlignment.MiddleLeft;
            currentbtn.IconColor = Color.LightBlue;
            currentbtn.TextImageRelation = TextImageRelation.ImageBeforeText;
            currentbtn.ImageAlign = ContentAlignment.MiddleLeft;
        }

        private void ChildForm(Form form)
        {
            if (currentChildForm != null)
                currentChildForm.Close();
            currentChildForm = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add((Control)form);
            panelDesktop.Tag = (object)form;
            form.BringToFront();
            form.Show();
            lblTitleChildForm.Text = form.Text;
        }

        private void btnFermer_Click(object sender, EventArgs e) => Application.Exit();

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void btnMinimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Principales.RGBColors.color5);
            ChildForm((Form)new Tableau_Bord());
        }

        private void btnStatistique_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Principales.RGBColors.color7);
            ChildForm((Form)new Statistique());
        }

        private void btnInscription_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Principales.RGBColors.color6);
            timer.Start();
        }

        private void btnCours_Click(object sender, EventArgs e) => ActivateButton(sender, Principales.RGBColors.color1);

        private void btnScolarité_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Principales.RGBColors.color3);
            timerCompta.Start();
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Principales.RGBColors.color4);
            TimerNote.Start();
        }

        private void btnEnseignant_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Principales.RGBColors.color2);
            TimerEnseignant.Start();
        }

        private void TimerComptabilité_Tick(object sender, EventArgs e)
        {
            if (compta_deroule)
            {
                panelFinance.Height += 10;
                if (!(panelFinance.Size == panelFinance.MaximumSize))
                    return;
                timerCompta.Stop();
                compta_deroule = false;
            }
            else
            {
                panelFinance.Height -= 10;
                if (panelFinance.Size == panelFinance.MinimumSize)
                {
                    timerCompta.Stop();
                    compta_deroule = true;
                }
            }
        }

        private void btnInscription_Click_1(object sender, EventArgs e) => ChildForm((Form)new Inscription());

        private void btnAjouter_Click(object sender, EventArgs e)
        {
        }

        public static void AjoutInitial()
        {
            int num = (int)new Ajout_Initiale().ShowDialog();
        }

        public void InsertEnterData()
        {
        }

        public void SelectEnterData()
        {
            try
            {
                List<AnnéeScolaire> annéeScolaireList = new List<AnnéeScolaire>();
                using (FileStream fileStream = new FileStream(Environment.CurrentDirectory + "\\AnnéeScolaire.xml", FileMode.Open, FileAccess.Read))
                    annéeScolaireList = new XmlSerializer(typeof(List<AnnéeScolaire>)).Deserialize((Stream)fileStream) as List<AnnéeScolaire>;
                dataGridView1.DataSource = (object)annéeScolaireList;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void cbxAnnéScolaire_SelectedIndexChanged(object sender, EventArgs e)
        {
            InsertEnterData();
            Principales.annéescolaire = cbxClasse.Text;
        }

        private void btnAnnéeScolaire_Click(object sender, EventArgs e)
        {
            Ajouter_Année ajouterAnnée = new Ajouter_Année();
            int num = (int)ajouterAnnée.ShowDialog();
            if (!(ajouterAnnée.ok == "Oui"))
                return;
            cbxClasse.DataSource = (object)SelectCatégorie();
            cbxClasse.DisplayMember = "Nom";
            cbxClasse.ValueMember = "Id";
            Principales.annéescolaire = cbxClasse.Text;
        }

        private void btnParamettrageCompta_Click(object sender, EventArgs e) => ChildForm((Form)new Paramettrage_Comptabilité());

        private void btnHome_Click(object sender, EventArgs e) => Reset();

        private void Reset()
        {
            DisableButton();
            leftborderbtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.LightBlue;
            lblTitleChildForm.Text = "Home";
            if (currentChildForm == null)
                return;
            currentChildForm.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (temp != 1)
                return;
            timer1.Stop();
            Principales.annéescolaire = cbxClasse.Text;
            if (Principales.type_compte == "Administrateur")
                btnUsers.Visible = true;
            else
                btnUsers.Visible = false;
            using (QuitayeContext ecoleDataContext = new QuitayeContext())
            {
                if (ecoleDataContext.tbl_année_scolaire.OrderBy((d => d.Nom)).Count() == 0)
                    btnAnnéeScolaire_Click(null, e);
            }
            temp = 0;
        }

        private void btnEleveInscrit_Click(object sender, EventArgs e) => ChildForm(new List_Elèves());

        private void TimerNote_Tick(object sender, EventArgs e)
        {
            if (derouler_note)
            {
                panelNote.Height += 10;
                if (!(panelNote.Size == panelNote.MaximumSize))
                    return;
                TimerNote.Stop();
                derouler_note = false;
            }
            else
            {
                panelNote.Height -= 10;
                if (panelNote.Size == panelNote.MinimumSize)
                {
                    TimerNote.Stop();
                    derouler_note = true;
                }
            }
        }

        private void btnNoteIndividuelle_Click(object sender, EventArgs e) => ChildForm((Form)new Note_Individuelle());

        private void btnNoteGroupé_Click(object sender, EventArgs e) => ChildForm((Form)new Note_Groupé());

        private void btnNoteDetails_Click(object sender, EventArgs e) => ChildForm((Form)new Détails_Note());

        private void btnTBBCompta_Click(object sender, EventArgs e) => ChildForm((Form)new Tableau_Bord_Compta());

        private void btnSaisie_Click(object sender, EventArgs e) => ChildForm((Form)new Dépenses_Recette());

        private void TimerEnseignant_Tick(object sender, EventArgs e)
        {
            if (derouler_enseignant)
            {
                panelEnseignant.Height += 10;
                if (!(panelEnseignant.Size == panelEnseignant.MaximumSize))
                    return;
                TimerEnseignant.Stop();
                derouler_enseignant = false;
            }
            else
            {
                panelEnseignant.Height -= 10;
                if (panelEnseignant.Size == panelEnseignant.MinimumSize)
                {
                    TimerEnseignant.Stop();
                    derouler_enseignant = true;
                }
            }
        }

        private void btnAjoutEnseignant_Click(object sender, EventArgs e) => ChildForm((Form)new Enseignants());

        private void btnUsers_Click(object sender, EventArgs e)
        {
            int num = (int)new Les_Utilisateurs().ShowDialog();
        }

        private void btnMotdePasse_Click(object sender, EventArgs e)
        {
            EditMotdepasse editMotdepasse = new EditMotdepasse();
            editMotdepasse.btnConnecter.Text = "VERIFIER";
            int num = (int)editMotdepasse.ShowDialog();
        }

        private void btnPayementScolarité_Click(object sender, EventArgs e) => ChildForm((Form)new Payement_Scolarité());

        private void btnPayementTransfère_Click(object sender, EventArgs e) => ChildForm((Form)new Payement_Transfère());

        private void btnPayementInscription_Click(object sender, EventArgs e) => ChildForm((Form)new Payement_Inscription());

        private void btnTranférer_Click(object sender, EventArgs e) => ChildForm((Form)new Payement_Transfère());

        private void btnParent_Click(object sender, EventArgs e)
        {
        }

        private void btnParamettre_Click(object sender, EventArgs e) => ChildForm((Form)new Paramettre());

        private void btnListEnseignant_Click(object sender, EventArgs e) => ChildForm((Form)new List_Enseignant());

        private void btnElèveList_Click(object sender, EventArgs e) => ChildForm((Form)new List_Elèves());

        private void btnRegistreBulletin_Click(object sender, EventArgs e) => ChildForm((Form)new Registre_Bulletin());

        private void btnFeedBack_Click(object sender, EventArgs e)
        {
            int num = (int)new FeedBack().ShowDialog();
        }

        private void btnFinanceCompta_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Principales.RGBColors.color4);
            timercompta.Start();
        }

        private void btnDocument_Click(object sender, EventArgs e) => ChildForm((Form)new Document_Comptable());

        private void btnAjouterOperation_Click(object sender, EventArgs e) => ChildForm((Form)new List_Opérations());

        private void btnCaisse_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Principales.RGBColors.color8);
            timercaisse.Start();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Principales.RGBColors.color9);
            timerstaff.Start();
        }

        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
            public static Color color7 = Color.FromArgb(188, 21, 234);
            public static Color color8 = Color.FromArgb(13, 21, 254);
            public static Color color9 = Color.FromArgb(138, 51, 204);
        }

        private void btnSoldeCompte_Click(object sender, EventArgs e) => ChildForm(new Solde_Compte());

        private void btnListEmployée_Click(object sender, EventArgs e) => ChildForm(new List_Employée());

        private void btnParamettreStaff_Click(object sender, EventArgs e) => ChildForm(new Paramettre_Staff());

        private void btnParamettreEleve_Click(object sender, EventArgs e) => ChildForm(new Paramettre_Elève());

        private void btnPassageElève_Click(object sender, EventArgs e) => ChildForm(new Passage_Elève());

        private void btnParamettreNote_Click(object sender, EventArgs e) => ChildForm(new Paramettre_Pedagogie());

        private void Principales_Load(object sender, EventArgs e)
        {

        }
    }
}
