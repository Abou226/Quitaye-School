using FontAwesome.Sharp;
using Microsoft.SqlServer.Management.Trace;
using PrintAction;
using Quitaye_School.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using DataGrid = Quitaye_School.Models.DataGrid;
using System.Windows.Forms.Layout;
using Quitaye_School.Models.Context;
using System.Data.Entity;

namespace Quitaye_School.User_Interface
{
    public partial class Formulaire_Inscription : Form
    {
        private string mycontrng = LogIn.mycontrng;
        private Timer loadTimer;
        private Timer timer = new Timer();
        public static string ok;
        public static string genre;
        public static string filePath;
        private static byte[] ImageByteArray;
        public int id;
        public static string classe;
        public static string matricule;
        public static string exgenre;
        private static Panel PCantine;
        private static Panel PTransport;
        private static Panel PAsurance;
        private static Image PImage;
        public bool derouler;
        public string Père { get; set; }

        public string Mère { get; set; }

        public string Matricule { get; set; }

        public string Classe { get; set; }

        public Formulaire_Inscription()
        {
            InitializeComponent();
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
            cbxMere.SelectedIndexChanged += CbxMere_SelectedIndexChanged;
            cbxPère.SelectedIndexChanged += CbxMere_SelectedIndexChanged;
            loadTimer = new Timer();
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            btnAddMere.Click += btnAjoutPère_Click;
            btnAddPere.Click += btnAjoutPère_Click;
            btnfermerNew.Click += btnFermer_Click;
        }

       
        private void BtnAddPere_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnAddMere_Click(object sender, EventArgs e)
        {
            
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallTasks();
        }

        private async Task CallTasks()
        {
            Task<DataTable> mere = FillCbxMereAsync();
            Task<DataTable> pere = FillCbxAsync();
            Task<DataTable> classe = FillCbxClasseAsync();
            List<Task> taskList = new List<Task>()
      {
         pere,
         mere,
         classe
      };
            while (taskList.Count > 0)
            {
                Task current = await Task.WhenAny(taskList);
                if (current == mere)
                {
                    DataGrid.FillCbxAsync(cbxMere, mere.Result, "Nom");
                    cbxMere.Text = Mère;
                }
                else if (current == pere)
                {
                    DataGrid.FillCbxAsync(cbxPère, pere.Result, "Nom");
                    cbxPère.Text = Père;
                }
                else if (current == classe)
                {
                    DataGrid.FillCbxAsync(cbxClasse, classe.Result, "Nom");
                    cbxClasse.Text = Classe;
                }
                txtMatriculte.Text = Matricule;
                taskList.Remove(current);
                current = null;
            }
            mere = null;
            pere = null;
            classe = null;
            taskList = null;
        }

        private void CbxMere_SelectedIndexChanged(object sender, EventArgs e) => txtPrenom_TextChanged(null, e);

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (derouler)
            {
                panelOption.Height += 10;
                if (!(panelOption.Size == panelOption.MaximumSize))
                    return;
                timer.Stop();
                derouler = false;
            }
            else
            {
                panelOption.Height -= 10;
                if (panelOption.Size == panelOption.MinimumSize)
                {
                    timer.Stop();
                    derouler = true;
                }
            }
        }

        private void btnFermer_Click(object sender, EventArgs e) => Close();

        private Task<DataTable> FillCbxAsync() => Task.Factory.StartNew(() => FillCbx());

        private DataTable FillCbx()
        {
            using (var ecoleDataContext = new QuitayeContext())
            {
                var queryable = ecoleDataContext
                    .tbl_parent_elèves.Where(d => d.Genre == "Homme")
                    .OrderByDescending(d => d.Id).Select(d => new
                {
                    Id = d.Id,
                    Nom = d.Nom_Contact
                });
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Nom");
                foreach (var data in queryable)
                {
                    DataRow row = dataTable.NewRow();
                    row["Id"] = data.Id;
                    row["Nom"] = (data.Nom ?? "");
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        private Task<DataTable> FillCbxClasseAsync() => Task.Factory.StartNew(() => FillCbxClasse());

        private DataTable FillCbxClasse()
        {
            using (var ecoleDataContext = new QuitayeContext())
            {
                var queryable = ecoleDataContext.tbl_classe
                    .OrderBy(d => d.Nom).Select(d => new
                {
                    Id = d.Id,
                    Nom = d.Nom
                });
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Nom");
                foreach (var data in queryable)
                {
                    DataRow row = dataTable.NewRow();
                    row["Id"] = data.Id;
                    row["Nom"] = (data.Nom ?? "");
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        private Task<DataTable> FillCbxMereAsync() 
            => Task.Factory.StartNew(() => FillCbxMere());

        private DataTable FillCbxMere()
        {
            using (var ecoleDataContext = new QuitayeContext())
            {
                var queryable = ecoleDataContext.tbl_parent_elèves
                    .Where(d => d.Genre == "Femme")
                    .OrderByDescending(d => d.Id).Select(d => new
                {
                    Id = d.Id,
                    Nom = d.Nom_Contact
                });
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Nom");
                foreach (var data in queryable)
                {
                    DataRow row = dataTable.NewRow();
                    row["Id"] = data.Id;
                    row["Nom"] = (data.Nom ?? "");
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        private async void btnInscription_Click(object sender, EventArgs e)
        {
            if (LogIn.expiré)
                return;
            try
            {
                if (txtNom.Text != "" && Formulaire_Inscription.genre != "" && txtPrenom.Text != "" && cbxMere.Text != "" && txtContact1.Text != "" && txtNationalité.Text != "" && cbxPère.Text != "" && txtMatriculte.Text != "" && cbxClasse.Text != "" && cbxTypeScolarité.Text != "")
                {
                    if (txtScolarité.Visible && txtScolarité.Text == "")
                        Alert.SShow("Veillez entré le montant de la scolarité.", Alert.AlertType.Warning);
                    else if (btnAjouter.Text == "Ajouter")
                    {
                        MsgBox msg = new MsgBox();
                        msg.show("Voulez-vous continuer cette inscription", "Inscription", 
                            MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                        int num = (int)msg.ShowDialog();
                        if (msg.clicked == "Non")
                            return;
                        if (msg.clicked == "Oui")
                        {
                            Elève elève = new Elève();
                            elève.Prenom = txtPrenom.Text;
                            elève.Nom = txtNom.Text;
                            elève.Mère = cbxMere.Text;
                            elève.Père = cbxPère.Text;
                            elève.Type_Scolarité = cbxTypeScolarité.Text;
                            if (txtScolarité.Text != "")
                                elève.Montant_Scolarité = Convert.ToDecimal(txtScolarité.Text);
                            elève.Classe = cbxClasse.Text;
                            elève.Contact1 = txtContact1.Text;
                            elève.Contact2 = txtContact2.Text;
                            elève.Email = txtEmail.Text;
                            elève.Genre = Formulaire_Inscription.genre;
                            elève.Adresse = txtAdresse.Text;
                            elève.Matricule = txtMatriculte.Text;
                            elève.Nationalité = txtNationalité.Text;
                            if (txtInscription.Text != "")
                                elève.Montant_Inscription = Convert.ToDecimal(txtInscription.Text);
                            elève.Date_Naissance = NaissanceDate.Value;
                            Formulaire_Inscription.classe = elève.Classe;
                            Formulaire_Inscription.matricule = elève.Matricule;
                            Formulaire_Inscription.PCantine = panelCantine;
                            Formulaire_Inscription.PTransport = panelTransport;
                            Formulaire_Inscription.PAsurance = panelAssurance;
                            Formulaire_Inscription.PImage = btnImage.Image;
                            Alert.SShow("Opération enclencher avec succès.", Alert.AlertType.Info);
                            if (await InscriptionAsync(elève))
                            {
                                Alert.SShow("Inscription " + elève.Prenom + " " + elève.Nom + " effectué avec succès.", Alert.AlertType.Sucess);
                                if (!string.IsNullOrEmpty(txtInscription.Text))
                                {
                                    await CallRecu();
                                    string name = "Reçu Payement " + txtPrenom.Text + " " + txtNom.Text + " N° Matricule " + Formulaire_Inscription.matricule + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
                                    Detail_Facture facture = await FactureAsync(Formulaire_Inscription.matricule);
                                    Print.PrintRecuPdfFile(dataGridView1, name, "Année_Scolaire " + Principales.annéescolaire, "Payement ", txtPrenom.Text + " " + txtNom.Text + " N°" + Formulaire_Inscription.matricule, LogIn.mycontrng, "Quitaye School", false, facture, true);
                                    name = null;
                                    facture = null;
                                }
                                ClearData();
                                Formulaire_Inscription.PCantine = null;
                                Formulaire_Inscription.PTransport = null;
                                Formulaire_Inscription.PAsurance = null;
                                Formulaire_Inscription.PImage = (Image)null;
                            }
                            elève = (Elève)null;
                        }
                        msg = null;
                    }
                    else
                    {
                        MsgBox msg = new MsgBox();
                        msg.show("Voulez-vous continuer cette modification", "Inscription", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                        int num = (int)msg.ShowDialog();
                        if (msg.clicked == "Non")
                            return;
                        if (msg.clicked == "Oui")
                        {
                            Elève elève = new Elève();
                            elève.Prenom = txtPrenom.Text;
                            elève.Nom = txtNom.Text;
                            elève.Mère = cbxMere.Text;
                            elève.Père = cbxPère.Text;
                            elève.Type_Scolarité = cbxTypeScolarité.Text;
                            if (!string.IsNullOrWhiteSpace(txtScolarité.Text))
                                elève.Montant_Scolarité = Convert.ToDecimal(txtScolarité.Text);
                            elève.Classe = cbxClasse.Text;
                            elève.Contact1 = txtContact1.Text;
                            elève.Contact2 = txtContact2.Text;
                            elève.Email = txtEmail.Text;
                            elève.Genre = Formulaire_Inscription.genre;
                            elève.Adresse = txtAdresse.Text;
                            elève.Matricule = txtMatriculte.Text;
                            elève.Nationalité = txtNationalité.Text;
                            if (txtInscription.Text != "")
                                elève.Montant_Inscription = Convert.ToDecimal(txtInscription.Text);
                            elève.Date_Naissance = NaissanceDate.Value.Date;
                            Formulaire_Inscription.classe = elève.Classe;
                            elève.Nom_Complet = elève.Prenom + " " + elève.Nom;
                            Formulaire_Inscription.matricule = elève.Matricule;
                            Formulaire_Inscription.PCantine = panelCantine;
                            Formulaire_Inscription.PTransport = panelTransport;
                            Formulaire_Inscription.PAsurance = panelAssurance;
                            Formulaire_Inscription.PImage = btnPicture.Image;
                            Alert.SShow("Opération enclencher avec succès.", Alert.AlertType.Info);
                            if (await ModifInscriptionAsync(elève))
                            {
                                Alert.SShow("Modification effectué avec succès.", Alert.AlertType.Sucess);
                                Formulaire_Inscription.PCantine = null;
                                Formulaire_Inscription.PTransport = null;
                                Formulaire_Inscription.PAsurance = null;
                                Formulaire_Inscription.PImage = (Image)null;
                                Close();
                            }
                            elève = (Elève)null;
                        }
                        msg = null;
                    }
                }
                else
                    Alert.SShow("Veillez remplir tous les zones de textes.", Alert.AlertType.Warning);
            }
            catch (Exception ex)
            {
                if (!(ex is SqlException w32ex))
                {
                    w32ex = ex.InnerException as SqlException;
                    int codes = w32ex.ErrorCode;
                }
                if (w32ex != null)
                {
                    int codes = w32ex.ErrorCode;
                    if (codes == -2146232060)
                    {
                        MsgBox msg = new MsgBox();
                        msg.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        int num = (int)msg.ShowDialog();
                        msg = null;
                    }
                    else
                    {
                        MsgBox msg = new MsgBox();
                        msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        int num = (int)msg.ShowDialog();
                        msg = null;
                    }
                }
                w32ex = null;
            }
        }

        private Task<Detail_Facture> FactureAsync(string matricule) 
            => Task.Factory.StartNew(() => Facture(matricule));

        private Detail_Facture Facture(string matricule)
        {
            Detail_Facture detailFacture = new Detail_Facture();
            using (var ecoleDataContext = new QuitayeContext())
            {
                var source = ecoleDataContext.tbl_payement
                    .Where(d => d.Année_Scolaire == Principales.annéescolaire 
                    && d.N_Matricule == matricule && d.Type == "Inscription").GroupBy(d => new
                {
                    Date = DbFunctions.TruncateTime(d.Date_Payement.Value)
                }).OrderByDescending (gr => gr.Key.Date).Select(gr => new
                {
                    Date = gr.Key.Date,
                    Montant = gr.Sum((x => x.Montant))
                });
                detailFacture.MontantPayée = Convert.ToDecimal(source.Sum(x => x.Montant));
                detailFacture.MontantHT = Convert.ToDecimal(source.Sum(x => x.Montant));
                detailFacture.MontantTTC = Convert.ToDecimal(source.Sum(x => x.Montant));
                detailFacture.Type_Operation = "Inscription";
                //detailFacture.PayementJour = Convert.ToDecimal(detailFacture.MontantPayée);
            }
            return detailFacture;
        }

        private static async Task<bool> ModifInscriptionAsync(Elève elève)
        {
            using (var ecoleDataContext = new QuitayeContext())
            {
                var tblScolarité = ecoleDataContext.tbl_scolarité
                    .Where(d => d.Classe == elève.Classe 
                    && d.Année_Scolaire == Principales.annéescolaire).First();
                var eleve = ecoleDataContext.tbl_inscription
                    .Where(d => d.Année_Scolaire == Principales.annéescolaire 
                    && d.Classe == elève.Classe && d.N_Matricule == elève.Matricule);
                if (eleve.Count() != 0)
                {
                    var tblInscription = ecoleDataContext
                        .tbl_inscription.SingleOrDefault(x => x.Id == eleve.First().Id);
                    tblInscription.Adresse = elève.Adresse;
                    tblInscription.Année_Scolaire = Principales.annéescolaire;
                    tblInscription.Classe = elève.Classe;
                    tblInscription.Contact_1 = elève.Contact1;
                    tblInscription.Contact_2 = elève.Contact2;
                    tblInscription.Date_Naissance = elève.Date_Naissance;
                    tblInscription.Email = elève.Email;
                    tblInscription.Genre = elève.Genre;
                    foreach (var control in Formulaire_Inscription.PCantine.Controls)
                    {
                        if (((RadioButton)control).Checked)
                            tblInscription.Cantine = ((Control)control).Text;
                    }
                    foreach (var control in Formulaire_Inscription.PTransport.Controls)
                    {
                        if (((RadioButton)control).Checked)
                            tblInscription.Transport = ((Control)control).Text;
                    }
                    foreach (var control in Formulaire_Inscription.PAsurance.Controls)
                    {
                        if (((RadioButton)control).Checked)
                            tblInscription.Assurance = ((Control)control).Text;
                    }
                    tblInscription.Nom = elève.Nom;
                    tblInscription.Prenom = elève.Prenom;
                    tblInscription.Nom_Père = elève.Père;
                    tblInscription.Nom_Mère = elève.Mère;
                    tblInscription.Nom_Complet = elève.Prenom + " " + elève.Nom;
                    tblInscription.Nationalité = elève.Nationalité;
                    tblInscription.Active = "Oui";
                    tblInscription.Cycle = tblScolarité.Cycle;
                    tblInscription.Scolarité = !(elève.Type_Scolarité == "Avec Rémise") ? new Decimal?() : new Decimal?(elève.Montant_Scolarité);
                    tblInscription.Type_Scolarité = elève.Type_Scolarité;
                    tblInscription.Nom_Matricule = tblInscription.Nom_Complet + "(" + tblInscription.N_Matricule + ")";
                    if (Formulaire_Inscription.filePath != "")
                    {
                        Image image = (Image)new Bitmap(Formulaire_Inscription.PImage);
                        MemoryStream memoryStream = new MemoryStream();
                        image.Save((Stream)memoryStream, ImageFormat.Jpeg);
                        Formulaire_Inscription.ImageByteArray = memoryStream.ToArray();
                        tblInscription.Image = Formulaire_Inscription.ImageByteArray;
                    }
                    await ecoleDataContext.SaveChangesAsync();
                    if (Formulaire_Inscription.exgenre != elève.Genre)
                    {
                        var source = ecoleDataContext.tbl_payement
                            .Where(d => d.N_Matricule == elève.Matricule 
                        && d.Année_Scolaire == Principales.annéescolaire).Select(d => new
                        {
                            Id = d.Id
                        });
                        foreach (var data in source)
                        {
                            int id = Convert.ToInt32(data.Id);
                            ecoleDataContext.tbl_payement.SingleOrDefault((x => x.Id == id)).Genre = elève.Genre;
                            await ecoleDataContext.SaveChangesAsync();
                        }
                    }
                    Formulaire_Inscription.ok = "Oui";
                }
                return true;
            }
        }

        
        private static async Task<bool> InscriptionAsync(Elève elève)
        {
            using (var ecoleDataContext = new QuitayeContext())
            {
                var data = ecoleDataContext.tbl_classe
                    .Where(d => d.Nom == elève.Classe).Select(d => new
                {
                    Nom = d.Nom,
                    Cycle = d.Cycle
                }).First();
                if (ecoleDataContext.tbl_inscription.OrderByDescending(d => d.Id).Count() > 0)
                {
                    var tblInscription = ecoleDataContext.tbl_inscription.OrderByDescending(d => d.Id).First();
                    var inscription = new Models.Context.tbl_inscription();
                    inscription.Id = Convert.ToInt32(tblInscription.Id) + 1;
                    inscription.Adresse = elève.Adresse;
                    inscription.Année_Scolaire = Principales.annéescolaire;
                    inscription.Classe = elève.Classe;
                    inscription.Contact_1 = elève.Contact1;
                    inscription.Contact_2 = elève.Contact2;
                    inscription.Date_Naissance = elève.Date_Naissance;
                    inscription.Email = elève.Email;
                    foreach (var control in Formulaire_Inscription.PCantine.Controls)
                    {
                        if (((RadioButton)control).Checked)
                            inscription.Cantine = ((Control)control).Text;
                    }
                    foreach (var control in Formulaire_Inscription.PTransport.Controls)
                    {
                        if (((RadioButton)control).Checked)
                            inscription.Transport = ((Control)control).Text;
                    }
                    foreach (var control in Formulaire_Inscription.PAsurance.Controls)
                    {
                        if (((RadioButton)control).Checked)
                            inscription.Assurance = ((Control)control).Text;
                    }
                    inscription.Genre = elève.Genre;
                    inscription.Nom = elève.Nom;
                    inscription.Prenom = elève.Prenom;
                    inscription.Nom_Père = elève.Père;
                    inscription.Nom_Mère = elève.Mère;
                    inscription.Nom_Complet = elève.Prenom + " " + elève.Nom;
                    inscription.Auteur = Principales.profile;
                    inscription.Date_Inscription = new DateTime?(DateTime.Now);
                    inscription.Nationalité = elève.Nationalité;
                    inscription.N_Matricule = elève.Matricule;
                    inscription.Cycle = data.Cycle;
                    inscription.Active = "Oui";
                    inscription.Scolarité = !(elève.Montant_Scolarité != 0M) ? new Decimal?() : new Decimal?(elève.Montant_Scolarité);
                    inscription.Type_Scolarité = elève.Type_Scolarité;
                    inscription.Nom_Matricule = inscription.Nom_Complet + "(" + inscription.N_Matricule + ")";
                    if (Formulaire_Inscription.filePath != "")
                    {
                        Image image = (Image)new Bitmap(Formulaire_Inscription.PImage);
                        MemoryStream memoryStream = new MemoryStream();
                        image.Save((Stream)memoryStream, ImageFormat.Jpeg);
                        Formulaire_Inscription.ImageByteArray = memoryStream.ToArray();
                        inscription.Image = Formulaire_Inscription.ImageByteArray;
                    }
                    else
                        inscription.Image = null;
                    if (ecoleDataContext.tbl_inscription.Where((d => d.N_Matricule == inscription.N_Matricule)).Select(d => new
                    {
                        Id = d.Id
                    }).ToList().Count == 0)
                    {
                        ecoleDataContext.tbl_inscription.Add(inscription);
                    }
                    else
                    {
                        string str = DateTime.Now.ToString("ffff");
                        inscription.N_Matricule = inscription.N_Matricule + "_" + str;
                        ecoleDataContext.tbl_inscription.Add(inscription);
                    }
                    await ecoleDataContext.SaveChangesAsync();
                    if (ecoleDataContext.tbl_payement.Select(d => d).Count() > 0)
                    {
                        var tblPayement = ecoleDataContext
                            .tbl_payement.OrderByDescending(d => d.Id).First();
                        ecoleDataContext.tbl_payement.Add(new Models.Context.tbl_payement()
                        {
                            Id = tblPayement.Id + 1,
                            Auteur = Principales.profile,
                            Année_Scolaire = Principales.annéescolaire,
                            Classe = elève.Classe,
                            Montant = new Decimal?(0M),
                            Prenom = elève.Prenom,
                            Nom = elève.Nom,
                            N_Matricule = elève.Matricule,
                            Date_Payement = new DateTime?(DateTime.Now),
                            Date_Enregistrement = new DateTime?(DateTime.Now),
                            Commentaire = "",
                            Type = "Scolarité",
                            Cycle = data.Cycle,
                            Genre = elève.Genre
                        });
                        await ecoleDataContext.SaveChangesAsync();
                        if (elève.Montant_Inscription != 0M)
                        {
                            ecoleDataContext.tbl_payement.Add(new Models.Context.tbl_payement()
                            {
                                Id = tblPayement.Id + 2,
                                Auteur = Principales.profile,
                                Année_Scolaire = Principales.annéescolaire,
                                Classe = elève.Classe,
                                Montant = new Decimal?(elève.Montant_Inscription),
                                Prenom = elève.Prenom,
                                Nom = elève.Nom,
                                N_Matricule = elève.Matricule,
                                Date_Payement = new DateTime?(DateTime.Now),
                                Date_Enregistrement = new DateTime?(DateTime.Now),
                                Commentaire = nameof(Inscription),
                                Type = nameof(Inscription),
                                Cycle = data.Cycle,
                                Genre = elève.Genre
                            });
                            await ecoleDataContext.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        ecoleDataContext.tbl_payement.Add(new Models.Context.tbl_payement()
                        {
                            Id = 1,
                            Auteur = Principales.profile,
                            Année_Scolaire = Principales.annéescolaire,
                            Classe = elève.Classe,
                            Montant = new Decimal?(0M),
                            Prenom = elève.Prenom,
                            Nom = elève.Nom,
                            N_Matricule = elève.Matricule,
                            Date_Payement = new DateTime?(DateTime.Now),
                            Date_Enregistrement = new DateTime?(DateTime.Now),
                            Commentaire = "",
                            Type = "Scolarité",
                            Cycle = data.Cycle,
                            Genre = elève.Genre
                        });
                        await ecoleDataContext.SaveChangesAsync();
                        if (elève.Montant_Scolarité != 0M)
                        {
                            ecoleDataContext.tbl_payement.Add(new Models.Context.tbl_payement()
                            {
                                Id = 2,
                                Auteur = Principales.profile,
                                Année_Scolaire = Principales.annéescolaire,
                                Classe = elève.Classe,
                                Montant = new Decimal?(elève.Montant_Scolarité),
                                Prenom = elève.Prenom,
                                Nom = elève.Nom,
                                N_Matricule = elève.Matricule,
                                Date_Payement = new DateTime?(DateTime.Now),
                                Date_Enregistrement = new DateTime?(DateTime.Now),
                                Commentaire = "",
                                Type = nameof(Inscription),
                                Cycle = data.Cycle,
                                Genre = elève.Genre
                            });
                            await ecoleDataContext.SaveChangesAsync();
                        }
                    }
                    if (ecoleDataContext.tbl_Compte_Comptable.Select(d => d).Count() > 0)
                    {
                        var tblCompteComptable = ecoleDataContext
                            .tbl_Compte_Comptable.OrderByDescending(d => d.Id).First();
                        var entity = new Models.Context.tbl_Compte_Comptable()
                        {
                            Id = tblCompteComptable.Id + 1,
                            Auteur = Principales.profile,
                            Catégorie = "411100-Collectif Client",
                            Compte = "411100",
                            Compte_Aux = inscription.Nom_Complet,
                            Date_Ajout = new DateTime?(DateTime.Now),
                            Préfix = inscription.N_Matricule
                        };
                        entity.Nom_Compte = entity.Compte + "-" + inscription.Nom_Complet;
                        entity.Type = "Client";
                        ecoleDataContext.tbl_Compte_Comptable.Add(entity);
                        await ecoleDataContext.SaveChangesAsync();
                    }
                    else
                    {
                        var entity = new Models.Context.tbl_Compte_Comptable()
                        {
                            Id = 1,
                            Auteur = Principales.profile,
                            Catégorie = "411100-Collectif Client",
                            Compte = "411100",
                            Compte_Aux = inscription.Nom_Complet,
                            Date_Ajout = new DateTime?(DateTime.Now),
                            Préfix = inscription.N_Matricule
                        };
                        entity.Nom_Compte = entity.Compte + "-" + inscription.Nom_Complet;
                        entity.Type = "Client";
                        ecoleDataContext.tbl_Compte_Comptable.Add(entity);
                        await ecoleDataContext.SaveChangesAsync();
                    }
                    Formulaire_Inscription.ok = "Oui";
                }
                else
                {
                    var entity1 = new Models.Context.tbl_inscription();
                    entity1.Id = 1;
                    entity1.Adresse = elève.Adresse;
                    entity1.Année_Scolaire = Principales.annéescolaire;
                    entity1.Classe = elève.Classe;
                    entity1.Contact_1 = elève.Contact1;
                    entity1.Contact_2 = elève.Contact2;
                    entity1.Date_Naissance = elève.Date_Naissance;
                    entity1.Email = elève.Email;
                    entity1.Genre = elève.Genre;
                    entity1.Nom = elève.Nom;
                    foreach (var control in Formulaire_Inscription.PCantine.Controls)
                    {
                        if (((RadioButton)control).Checked)
                            entity1.Cantine = ((Control)control).Text;
                    }
                    foreach (var control in Formulaire_Inscription.PTransport.Controls)
                    {
                        if (((RadioButton)control).Checked)
                            entity1.Transport = ((Control)control).Text;
                    }
                    foreach (var control in Formulaire_Inscription.PAsurance.Controls)
                    {
                        if (((RadioButton)control).Checked)
                            entity1.Assurance = ((Control)control).Text;
                    }
                    entity1.Prenom = elève.Prenom;
                    entity1.Nom_Père = elève.Père;
                    entity1.Nom_Mère = elève.Mère;
                    entity1.Nom_Complet = elève.Prenom + " " + elève.Nom;
                    entity1.Auteur = Principales.profile;
                    entity1.Date_Inscription = new DateTime?(DateTime.Now);
                    entity1.Nationalité = elève.Nationalité;
                    entity1.N_Matricule = elève.Matricule;
                    entity1.Cycle = data.Cycle;
                    entity1.Active = "Oui";
                    entity1.Scolarité = !(elève.Montant_Scolarité != 0M) ? new Decimal?() : new Decimal?(elève.Montant_Scolarité);
                    entity1.Type_Scolarité = elève.Type_Scolarité;
                    entity1.Nom_Matricule = entity1.Nom_Complet + "(" + entity1.N_Matricule + ")";
                    if (Formulaire_Inscription.filePath != "")
                    {
                        Image image = (Image)new Bitmap(Formulaire_Inscription.PImage);
                        MemoryStream memoryStream = new MemoryStream();
                        image.Save((Stream)memoryStream, ImageFormat.Jpeg);
                        Formulaire_Inscription.ImageByteArray = memoryStream.ToArray();
                        entity1.Image = Formulaire_Inscription.ImageByteArray;
                    }
                    else
                        entity1.Image = null;
                    ecoleDataContext.tbl_inscription.Add(entity1);
                    await ecoleDataContext.SaveChangesAsync();
                    ecoleDataContext.tbl_payement.Add(new Models.Context.tbl_payement()
                    {
                        Id = 1,
                        Auteur = Principales.profile,
                        Année_Scolaire = Principales.annéescolaire,
                        Classe = elève.Classe,
                        Montant = new Decimal?(0M),
                        Prenom = elève.Prenom,
                        Nom = elève.Nom,
                        N_Matricule = elève.Matricule,
                        Date_Payement = new DateTime?(DateTime.Now),
                        Date_Enregistrement = new DateTime?(DateTime.Now),
                        Commentaire = "",
                        Type = "Scolarité",
                        Cycle = data.Cycle,
                        Genre = elève.Genre
                    });
                    await ecoleDataContext.SaveChangesAsync();
                    if (elève.Montant_Inscription != 0M)
                    {
                        ecoleDataContext.tbl_payement.Add(new Models.Context.tbl_payement()
                        {
                            Id = 2,
                            Auteur = Principales.profile,
                            Année_Scolaire = Principales.annéescolaire,
                            Classe = elève.Classe,
                            Montant = new Decimal?(elève.Montant_Inscription),
                            Prenom = elève.Prenom,
                            Nom = elève.Nom,
                            N_Matricule = elève.Matricule,
                            Date_Payement = new DateTime?(DateTime.Now),
                            Date_Enregistrement = new DateTime?(DateTime.Now),
                            Commentaire = nameof(Inscription),
                            Type = nameof(Inscription),
                            Cycle = data.Cycle,
                            Genre = elève.Genre
                        });
                        await ecoleDataContext.SaveChangesAsync();
                    }
                    if (ecoleDataContext.tbl_Compte_Comptable.Select(d => d).Count() > 0)
                    {
                        var tblCompteComptable = ecoleDataContext
                            .tbl_Compte_Comptable.OrderByDescending(d => d.Id).First();
                        var entity2 = new Models.Context.tbl_Compte_Comptable()
                        {
                            Id = tblCompteComptable.Id + 1,
                            Auteur = Principales.profile,
                            Catégorie = "411100-Collectif Client",
                            Compte = "411100",
                            Compte_Aux = entity1.Nom_Complet,
                            Date_Ajout = new DateTime?(DateTime.Now),
                            Préfix = entity1.N_Matricule
                        };
                        entity2.Nom_Compte = entity2.Compte + "-" + entity1.Nom_Complet;
                        entity2.Type = "Client";
                        ecoleDataContext.tbl_Compte_Comptable.Add(entity2);
                        await ecoleDataContext.SaveChangesAsync();
                    }
                    else
                    {
                        var entity3 = new Models.Context.tbl_Compte_Comptable()
                        {
                            Id = 1,
                            Auteur = Principales.profile,
                            Catégorie = "411100-Collectif Client",
                            Compte = "411100",
                            Compte_Aux = entity1.Nom_Complet,
                            Date_Ajout = new DateTime?(DateTime.Now),
                            Préfix = entity1.N_Matricule
                        };
                        entity3.Nom_Compte = entity3.Compte + "-" + entity1.Nom_Complet;
                        entity3.Type = "Client";
                        ecoleDataContext.tbl_Compte_Comptable.Add(entity3);
                        await ecoleDataContext.SaveChangesAsync();
                    }
                    Formulaire_Inscription.ok = "Oui";
                }
                return true;
            }
        }

        private async Task CallRecu()
        {
            DataTable result = await Formulaire_Inscription.FillRecuAsync(Formulaire_Inscription.matricule);
            dataGridView1.DataSource = result;
            result = null;
        }

        public static Task<DataTable> FillRecuAsync(string _matricule) => Task.Factory.StartNew(() => Formulaire_Inscription.FillRecu(_matricule));

        private static DataTable FillRecu(string _matricule)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Désignation");
            dataTable.Columns.Add("Montant");
            dataTable.Columns.Add("Date");
            using (var ecoleDataContext = new QuitayeContext())
            {
                var source = ecoleDataContext.tbl_payement
                    .Where(d => d.N_Matricule == _matricule 
                    && d.Année_Scolaire == Principales.annéescolaire)
                    .OrderByDescending(d => d.Id).Select(d => new
                {
                    Id = d.Id,
                    Montant = d.Montant,
                    Commentaire = d.Commentaire,
                    Date = d.Date_Payement
                });
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Commentaire;
                    row[2] = data.Montant;
                    DataRow dataRow = row;
                    DateTime date = data.Date.Value;
                    date = date.Date;
                    string str = date.ToString("dd/MM/yyy");
                    dataRow[3] = str;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        private void btnImage_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (((Control)sender).Name == "btnPicture")
            {
                if (!(sender is PictureBox pictureBox))
                    return;
                openFileDialog.Filter = "(*.jpg; *.jpeg;*.bmp)| *.jpg; *.jpeg; *.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Formulaire_Inscription.filePath = openFileDialog.FileName;
                    pictureBox.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
            else if (sender is IconPictureBox iconPictureBox)
            {
                openFileDialog.Filter = "(*.jpg; *.jpeg;*bmp)| *.jpg; *.jpeg; *.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Formulaire_Inscription.filePath = openFileDialog.FileName;
                    iconPictureBox.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
        }

        public void ClearData()
        {
            txtAdresse.Clear();
            txtContact1.Clear();
            txtContact2.Clear();
            txtEmail.Clear();
            txtMatriculte.Clear();
            cbxMere.Text = null;
            txtNationalité.Clear();
            txtNom.Clear();
            txtPrenom.Clear();
            cbxPère.Text = null;
            cbxClasse.Text = null;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            Formulaire_Inscription.genre = null;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Formulaire_Inscription.genre = "Masculin";
            txtPrenom_TextChanged(null, e);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Formulaire_Inscription.genre = "Feminin";
            txtPrenom_TextChanged(null, e);
        }

        private async void btnAjouterClasse_Click(object sender, EventArgs e)
        {
            Quitaye_School.User_Interface.Classe cla = new Quitaye_School.User_Interface.Classe();
            int num = (int)cla.ShowDialog();
            if (!(cla.ok == "Oui"))
            {
                cla = (Quitaye_School.User_Interface.Classe)null;
            }
            else
            {
                DataTable result = await FillCbxClasseAsync();
                DataGrid.FillCbxAsync(cbxClasse, result, "Nom");
                result = null;
                cla = (Quitaye_School.User_Interface.Classe)null;
            }
        }

        private void txtPrenom_TextChanged(object sender, EventArgs e)
        {
            if (txtNom.Text != "" && txtPrenom.Text != "" && cbxPère.Text != "" && cbxMere.Text != "" && Formulaire_Inscription.genre != "")
            {
                string str1 = NaissanceDate.Value.Month.ToString();
                string str2 = NaissanceDate.Value.ToString("yy");
                string str3 = NaissanceDate.Value.ToString("dd");
                char ch1 = txtPrenom.Text[0];
                char ch2 = txtNom.Text[0];
                char ch3 = cbxPère.Text[0];
                char ch4 = cbxMere.Text[0];
                if (Formulaire_Inscription.genre == "Masculin")
                    txtMatriculte.Text = str3 + "M" + str2 + ch1.ToString().ToUpper() + ch2.ToString().ToUpper() + str1 + ch3.ToString().ToUpper() + ch4.ToString().ToUpper();
                else
                    txtMatriculte.Text = str3 + "F" + str2 + ch1.ToString().ToUpper() + ch2.ToString().ToUpper() + str1 + ch3.ToString().ToUpper() + ch4.ToString().ToUpper();
            }
            else
                txtMatriculte.Clear();
        }

        private void btnAjouterFormule_Click(object sender, EventArgs e)
        {
        }

        private void cbxTypeScolarité_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTypeScolarité.Text == "Avec Rémise")
            {
                txtScolarité.Visible = true;
                lblRémise.Visible = true;
            }
            else
            {
                txtScolarité.Visible = false;
                lblRémise.Visible = false;
            }
            if (string.IsNullOrEmpty(cbxClasse.Text) || !(btnAjouter.Text != "Ajouter"))
                return;
            using (QuitayeContext ecoleDataContext = new QuitayeContext())
                ecoleDataContext.tbl_inscription.Where((d => d.Année_Scolaire == Principales.annéescolaire && d.Classe == cbxClasse.Text && d.N_Matricule == txtMatriculte.Text)).FirstOrDefault();
        }

        private void txtRémise_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '\b')
                return;
            e.Handled = true;
        }

        private async void btnAjoutPère_Click(object sender, EventArgs e)
        {
            if (LogIn.expiré)
                return;
            Ajout_Parent p = new Ajout_Parent();
            int num = (int)p.ShowDialog();
            if (Ajout_Parent.ok == "Oui")
            {
                using (new QuitayeContext())
                {
                    if (((Control)sender).Name == "btnAddPere")
                    {
                        DataTable result = await FillCbxAsync();
                        DataGrid.FillCbxAsync(cbxPère, result, "Nom");
                        result = null;
                    }
                    else if (((Control)sender).Name == "btnAddMere")
                    {
                        DataTable result = await FillCbxMereAsync();
                        DataGrid.FillCbxAsync(cbxMere, result, "Nom");
                        result = null;
                    }
                    Ajout_Parent.ok = null;
                }
            }
            p = (Ajout_Parent)null;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) => timer.Start();
    }
}
    

