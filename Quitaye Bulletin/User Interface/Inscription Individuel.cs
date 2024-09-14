using FontAwesome.Sharp;
using PrintAction;
using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Inscription_Individuel : Form
    {
        private string mycontrng = LogIn.mycontrng;
        public string reste;
        public decimal scolarité;
        public string classes;
        public string prenom;
        public string nom;
        public string matricule;
        public string cycle;
        public string genre;
        private string filePath = "";
        private string filename;
        public string ok;
        private string name;
        private string operation = null;

        public Inscription_Individuel()
        {
            InitializeComponent();
            //if (!(Principales.type_compte == "Administrateur"))
            //    return;
            //CheckNegatif.Visible = true;
            btnFermer.Click += btnFermer_Click;
            btnValider.Click += btnValider_Click;
        }

        private void btnFermer_Click(object sender, EventArgs e) => Close();

        private async Task CallRecu()
        {
            DataTable result = await FillRecuAsync(matricule);
            dataGridView1.DataSource = result;
            result = null;
        }

        public static Task<DataTable> FillRecuAsync(string _matricule) => Task.Factory.StartNew(() => FillRecu(_matricule));

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
                    && d.Année_Scolaire == Principales.annéescolaire && d.Type == "Inscription")
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
                    row[3] = data.Date.Value.ToString("dd/MM/yyy");
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        private async void btnValider_Click(object sender, EventArgs e)
        {
            if (!LogIn.expiré)
            {
                try
                {
                    if (!(txtMontant.Text != "") || !(operation != ""))
                        return;
                    MsgBox msg = new MsgBox();
                    msg.show("Voulez-vous valider cette opération ?", "Attente Confirmation", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                    int num = (int)msg.ShowDialog();
                    if (msg.clicked == "Non")
                        return;
                    if (msg.clicked == "Oui")
                    {
                        using (var donnée = new QuitayeContext())
                        {
                            var ses = donnée.tbl_inscription
                                .Where(d => d.N_Matricule == matricule).Select(d => new
                            {
                                Id = d.Id,
                                Scolarité = d.Scolarité,
                                Cycle = d.Cycle,
                                Genre = d.Genre,
                                Prenom = d.Prenom,
                                Nom = d.Nom,
                                Matricule = d.N_Matricule,
                                Nom_Complet = d.Nom_Complet,
                                Classe = d.Classe
                            }).First();
                            var jour = donnée.tbl_journal_comptable.Select(d => new
                            {
                                Id = d.Id
                            });
                            var pay = donnée.tbl_payement
                                .OrderByDescending(d => d.Id).Select(d => new
                            {
                                Id = d.Id
                            }).Take(1);
                            if (pay.Count() > 0)
                            {
                                var p = pay.FirstOrDefault();
                                var payement = new Models.Context.tbl_payement();
                                payement.Id = p.Id + 1;
                                payement.Auteur = Principales.profile;
                                payement.Année_Scolaire = Principales.annéescolaire;
                                payement.Classe = ses.Classe;
                                payement.Montant = new Decimal?(Convert.ToDecimal(txtMontant.Text));
                                payement.Prenom = ses.Prenom;
                                payement.Nom = ses.Nom;
                                payement.N_Matricule = ses.Matricule;
                                payement.Date_Payement = DateOp.Value.Date;
                                payement.Date_Enregistrement = new DateTime?(DateTime.Now);
                                payement.Commentaire = string.IsNullOrEmpty(txtCommentaire.Text) ? "Inscription" : txtCommentaire.Text;
                                payement.Opération = operation;
                                payement.Type = "Inscription";
                                payement.Cycle = ses.Cycle;
                                payement.Genre = ses.Genre;
                                donnée.tbl_payement.Add(payement);
                                await donnée.SaveChangesAsync();
                                p = null;
                                payement = null;
                            }
                            else
                            {
                                var payement = new Models.Context.tbl_payement();
                                payement.Id = 1;
                                payement.Auteur = Principales.profile;
                                payement.Année_Scolaire = Principales.annéescolaire;
                                payement.Classe = ses.Classe;
                                payement.Montant = new Decimal?(Convert.ToDecimal(txtMontant.Text));
                                payement.Prenom = ses.Prenom;
                                payement.Nom = ses.Nom;
                                payement.N_Matricule = ses.Matricule;
                                payement.Date_Payement = DateOp.Value.Date;
                                payement.Date_Enregistrement = new DateTime?(DateTime.Now);
                                payement.Commentaire = string.IsNullOrEmpty(txtCommentaire.Text) ? "Inscription" : txtCommentaire.Text;
                                payement.Type = "Inscription";
                                payement.Cycle = ses.Cycle;
                                payement.Genre = ses.Genre;
                                payement.Opération = operation;
                                donnée.tbl_payement.Add(payement);
                                await donnée.SaveChangesAsync();
                                payement = null;
                            }
                            var co = donnée.tbl_Compte_Comptable.Select(d => d);
                            if (co.Count() > 0)
                            {
                                var c = donnée.tbl_Compte_Comptable
                                    .OrderByDescending(d => d.Id).First();
                                var compte = new Models.Context.tbl_Compte_Comptable()
                                {
                                    Id = c.Id + 1,
                                    Auteur = Principales.profile,
                                    Catégorie = "411100-Collectif Client",
                                    Compte = "411100",
                                    Compte_Aux = ses.Nom_Complet,
                                    Date_Ajout = new DateTime?(DateTime.Now),
                                    Préfix = ses.Matricule
                                };
                                compte.Nom_Compte = compte.Compte + "-" + ses.Nom_Complet;
                                compte.Type = "Client";
                                donnée.tbl_Compte_Comptable.Add(compte);
                                await donnée.SaveChangesAsync();
                                c = null;
                                compte = null;
                            }
                            else
                            {
                                var compte = new Models.Context.tbl_Compte_Comptable()
                                {
                                    Id = 1,
                                    Auteur = Principales.profile,
                                    Catégorie = "411100-Collectif Client",
                                    Compte = "411100",
                                    Compte_Aux = ses.Nom_Complet,
                                    Date_Ajout = new DateTime?(DateTime.Now),
                                    Préfix = ses.Matricule
                                };
                                compte.Nom_Compte = compte.Compte + "-" + ses.Nom_Complet;
                                compte.Type = "Client";
                                donnée.tbl_Compte_Comptable.Add(compte);
                                await donnée.SaveChangesAsync();
                                compte = null;
                            }
                            await CallRecu();
                            name = "Reçu Payement " + prenom + " " + nom + " N° Matricule " + matricule + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
                            Detail_Facture facture = await FactureAsync(matricule);
                            facture.Type_Operation = "Inscription";
                            Print.PrintRecuPdfFile(dataGridView1, name, "Année_Scolaire " + Principales.annéescolaire, "Payement ", prenom + " " + nom + " N°" + matricule, LogIn.mycontrng, "Quitaye School", false, facture, false);
                            ses = null;
                            jour = null;
                            pay = null;
                            co = null;
                            facture = null;
                        }
                    }
                    msg = null;
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
                            msg = (MsgBox)null;
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
            else if (LogIn.trial)
                Alert.SShow("Periode d'essay arrivé terme. Veillez-passez à la version payante !", Alert.AlertType.Info);
            else
                Alert.SShow("Veillez renouveller votre abonnement !", Alert.AlertType.Info);
        }

        private Task<Detail_Facture> FactureAsync(string matricule) => Task.Factory.StartNew(() => Facture(matricule));

        private Detail_Facture Facture(string matricule)
        {
            var detailFacture = new Detail_Facture();
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
                    Montant = gr.Sum(x => x.Montant)
                });
                detailFacture.MontantHT = Convert.ToDecimal(source.Sum(x => x.Montant));
                detailFacture.MontantPayée = Convert.ToDecimal(source.Sum(x => x.Montant));
                detailFacture.Type_Operation = "Inscription";
                detailFacture.PayementJour = Convert.ToDecimal(source.First().Montant);
            }
            return detailFacture;
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (!(sender is IconButton))
                return;
            openFileDialog.Filter = "(*.pdf; *.xls;*.docs)| *.pdf; *.xls; *.docs";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                filename = Path.GetFileName(filePath);
            }
        }

        private void txtMontant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '\b')
                return;
            e.Handled = true;
        }

        private void txtMontant_TextChanged(object sender, EventArgs e)
        {
            if (!(txtMontant.Text != ""))
                return;
            txtMontant.Text = Convert.ToDecimal(txtMontant.Text).ToString("N0");
            txtMontant.SelectionStart = txtMontant.Text.Length;
        }

        private void rEspèce_CheckedChanged(object sender, EventArgs e) => operation = ((Control)sender).Text;

    }
}
