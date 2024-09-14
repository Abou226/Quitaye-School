using FontAwesome.Sharp;
using PrintAction;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Autre_Payement : Form
    {
        private string mycontrng = LogIn.mycontrng;
        public Decimal montant_operation;
        public string classes;
        public string prenom;
        public string nom;
        public string type_operation;
        public string matricule;
        public string cycle;
        public string genre;
        private string filePath = "";
        private string filename;
        public string ok;
        private string name;
        private string operation = (string)null;
        public Autre_Payement()
        {
            InitializeComponent();
            if (Principales.type_compte == "Administrateur")
                CheckNegatif.Visible = true;
            cbxTypeOpération.SelectedIndexChanged += CbxTypeOpération_SelectedIndexChanged;
            btnFermer.Click += btnFermer_Click;
            btnValider.Click += btnValider_Click;
            txtMontant.TextChanged += txtMontant_TextChanged;
            txtMontant.KeyPress += txtMontant_KeyPress;
        }

        private async void CbxTypeOpération_SelectedIndexChanged(object sender, EventArgs e) => await Reste();

        private async Task Reste()
        {
            if (!(cbxTypeOpération.Text != ""))
                return;
            type_operation = cbxTypeOpération.Text;
            Decimal montant = await GetOperationMontantAsync(type_operation);
            lblReste.Text = "Reste : " + montant.ToString("N0");
            lblReste.Visible = true;
        }

        private Task<Decimal> GetOperationMontantAsync(string operation) => Task.Factory.StartNew<Decimal>((Func<Decimal>)(() => GetOperationMontant(operation)));

        private Decimal GetOperationMontant(string operation)
        {
            using (var ecoleDataContext = new QuitayeContext())
            {
                var source1 = ecoleDataContext.tbl_payement.Where((d => d.N_Matricule == matricule && d.Année_Scolaire == Principales.annéescolaire && d.Type == type_operation)).Select(d => new
                {
                    Montant = d.Montant
                });
                var source2 = ecoleDataContext.tbl_inscription.Where((d => d.N_Matricule == matricule)).Select(d => new
                {
                    Transport = d.Transport,
                    Assurance = d.Assurance,
                    Cantine = d.Cantine
                });
                var source3 = ecoleDataContext.tbl_tarif_accessoire.Where((d => d.Nom == operation)).Select(d => new
                {
                    Mensuel = d.Tarif_Mensuel,
                    Annuel = d.Tarif_Annuel,
                    Journalier = d.Tarif_Journalier
                });
                if (source3.Count() == 0)
                    return 0M;
                if (operation == "Transport")
                {
                    string transport = source2.First().Transport;
                    if (string.IsNullOrWhiteSpace(source2.First().Transport))
                        return 0M;
                    if (source2.First().Transport.Contains("Annuel"))
                        return Convert.ToDecimal(source3.First().Annuel) - Convert.ToDecimal(source1.Sum(x => x.Montant));
                    if (source2.First().Transport.Contains("Mensuel"))
                        return Convert.ToDecimal(source3.First().Mensuel) - Convert.ToDecimal(source1.Sum(x => x.Montant));
                    return Convert.ToDecimal(source3.First().Journalier) - Convert.ToDecimal(source1.Sum(x => x.Montant));
                }
                if (operation == "Cantine")
                {
                    string cantine = source2.First().Cantine;
                    if (string.IsNullOrWhiteSpace(source2.First().Cantine))
                        return 0M;
                    if (source2.First().Cantine.Contains("Annuel"))
                        return Convert.ToDecimal(source3.First().Annuel) - Convert.ToDecimal(source1.Sum(x => x.Montant));
                    if (source2.First().Cantine.Contains("Mensuel"))
                        return Convert.ToDecimal(source3.First().Mensuel) - Convert.ToDecimal(source1.Sum(x => x.Montant));
                    return Convert.ToDecimal(source3.First().Journalier) - Convert.ToDecimal(source1.Sum(x => x.Montant));
                }
                if (!(operation == "Assurance"))
                    return 0M;
                string assurance = source2.First().Assurance;
                if (string.IsNullOrWhiteSpace(source2.First().Assurance))
                    return 0M;
                if (source2.First().Assurance.Contains("Annuel"))
                    return Convert.ToDecimal(source3.First().Annuel) - Convert.ToDecimal(source1.Sum(x => x.Montant));
                if (source2.First().Assurance.Contains("Mensuel"))
                    return Convert.ToDecimal(source3.First().Mensuel) - Convert.ToDecimal(source1.Sum(x => x.Montant));
                return Convert.ToDecimal(source3.First().Journalier) - Convert.ToDecimal(source1.Sum(x => x.Montant));
            }
        }

        private void btnFermer_Click(object sender, EventArgs e) => Close();

        private async Task CallRecu()
        {
            DataTable result = await Autre_Payement.FillRecuAsync(matricule);
            dataGridView1.DataSource = result;
            result = (DataTable)null;
        }

        public static Task<DataTable> FillRecuAsync(string _matricule) => Task.Factory.StartNew<DataTable>((Func<DataTable>)(() => Autre_Payement.FillRecu(_matricule)));

        private static DataTable FillRecu(string _matricule)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Désignation");
            dataTable.Columns.Add("Montant");
            dataTable.Columns.Add("Date", typeof(DateTime));
            using (QuitayeContext ecoleDataContext = new QuitayeContext())
            {
                var source = ecoleDataContext.tbl_payement.Where((d => d.N_Matricule == _matricule 
                && d.Année_Scolaire == Principales.annéescolaire))
                    .OrderByDescending((d => d.Id)).Select( d => new
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
                    row[3] = Convert.ToDateTime(data.Date.Value.ToString("dd/MM/yyy"));
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
                    if (!(txtMontant.Text != "") || !(operation != "") || !(cbxTypeOpération.Text != ""))
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
                            var s = donnée.tbl_payement.OrderByDescending((d => d.Id)).Select(d => new
                            {
                                Id = d.Id
                            });
                            var ses = donnée.tbl_inscription.Where((d => d.N_Matricule == matricule)).Select(d => new
                            {
                                Id = d.Id,
                                Scolarité = d.Scolarité,
                                Nom_Complet = d.Nom_Complet
                            }).First();
                            var jour = donnée.tbl_journal_comptable.Select(d => new
                            {
                                Id = d.Id
                            });
                            string result = Regex.Replace(txtMontant.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                            DateTime now;
                            if (s.Count() > 0)
                            {
                                var ss = s.First();
                                if (filePath != "")
                                {
                                    byte[] file;
                                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                                    {
                                        using (BinaryReader reader = new BinaryReader((Stream)stream))
                                            file = reader.ReadBytes((int)stream.Length);
                                    }
                                    var payement = new Models.Context.tbl_payement();
                                    payement.Id = ss.Id + 1;
                                    payement.Auteur = Principales.profile;
                                    payement.Année_Scolaire = Principales.annéescolaire;
                                    payement.Classe = classes;
                                    payement.Montant = !CheckNegatif.Checked ? new Decimal?(Convert.ToDecimal(result)) : new Decimal?(-Convert.ToDecimal(result));
                                    payement.Prenom = prenom;
                                    payement.Nom = nom;
                                    payement.N_Matricule = matricule;
                                    payement.Date_Payement = new DateTime?(DateOp.Value.Date);
                                    payement.Date_Enregistrement = new DateTime?(DateTime.Now);
                                    payement.Commentaire = txtCommentaire.Text;
                                    payement.Fichier = file;
                                    payement.Nom_Fichier = filename;
                                    payement.Cycle = cycle;
                                    payement.Genre = genre;
                                    payement.Type = cbxTypeOpération.Text;
                                    payement.Opération = operation;
                                    donnée.tbl_payement.Add(payement);
                                    await donnée.SaveChangesAsync();
                                    ok = "Oui";
                                    file = (byte[])null;
                                    payement = null;
                                }
                                else
                                {
                                    var payement = new Models.Context.tbl_payement();
                                    payement.Id = ss.Id + 1;
                                    payement.Auteur = Principales.profile;
                                    payement.Année_Scolaire = Principales.annéescolaire;
                                    payement.Classe = classes;
                                    payement.Montant = !CheckNegatif.Checked ? new Decimal?(Convert.ToDecimal(result)) : new Decimal?(-Convert.ToDecimal(result));
                                    payement.Prenom = prenom;
                                    payement.Nom = nom;
                                    payement.Opération = operation;
                                    payement.N_Matricule = matricule;
                                    var tblPayement = payement;
                                    now = DateOp.Value;
                                    DateTime? nullable = new DateTime?(now.Date);
                                    tblPayement.Date_Payement = nullable;
                                    payement.Date_Enregistrement = new DateTime?(DateTime.Now);
                                    payement.Commentaire = txtCommentaire.Text;
                                    payement.Cycle = cycle;
                                    payement.Genre = genre;
                                    payement.Type = cbxTypeOpération.Text;
                                    donnée.tbl_payement.Add(payement);
                                    await donnée.SaveChangesAsync();
                                    payement = null;
                                }
                                ss = null;
                            }
                            else
                            {
                                byte[] file;
                                if (filePath != "")
                                {
                                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                                    {
                                        using (BinaryReader reader = new BinaryReader((Stream)stream))
                                            file = reader.ReadBytes((int)stream.Length);
                                    }
                                    var payement = new Models.Context.tbl_payement();
                                    payement.Id = 1;
                                    payement.Auteur = Principales.profile;
                                    payement.Année_Scolaire = Principales.annéescolaire;
                                    payement.Classe = classes;
                                    payement.Montant = !CheckNegatif.Checked ? new Decimal?(Convert.ToDecimal(result)) : new Decimal?(-Convert.ToDecimal(result));
                                    payement.Prenom = prenom;
                                    payement.Nom = nom;
                                    payement.Opération = operation;
                                    payement.N_Matricule = matricule;
                                    var tblPayement = payement;
                                    now = DateOp.Value;
                                    DateTime? nullable = new DateTime?(now.Date);
                                    tblPayement.Date_Payement = nullable;
                                    payement.Date_Enregistrement = new DateTime?(DateTime.Now);
                                    payement.Commentaire = txtCommentaire.Text;
                                    payement.Genre = genre;
                                    payement.Fichier = file;
                                    payement.Nom_Fichier = filename;
                                    payement.Cycle = cycle;
                                    payement.Type = cbxTypeOpération.Text;
                                    donnée.tbl_payement.Add(payement);
                                    await donnée.SaveChangesAsync();
                                    ok = "Oui";
                                    payement = null;
                                }
                                else
                                {
                                    var payement = new Models.Context.tbl_payement();
                                    payement.Id = 1;
                                    payement.Auteur = Principales.profile;
                                    payement.Année_Scolaire = Principales.annéescolaire;
                                    payement.Classe = classes;
                                    payement.Montant = !CheckNegatif.Checked ? new Decimal?(Convert.ToDecimal(result)) : new Decimal?(-Convert.ToDecimal(result));
                                    payement.Prenom = prenom;
                                    payement.Opération = operation;
                                    payement.Nom = nom;
                                    payement.N_Matricule = matricule;
                                    var tblPayement = payement;
                                    now = DateOp.Value;
                                    DateTime? nullable = new DateTime?(now.Date);
                                    tblPayement.Date_Payement = nullable;
                                    payement.Date_Enregistrement = new DateTime?(DateTime.Now);
                                    payement.Commentaire = txtCommentaire.Text;
                                    payement.Cycle = cycle;
                                    payement.Genre = genre;
                                    payement.Type = cbxTypeOpération.Text;
                                    donnée.tbl_payement.Add(payement);
                                    await donnée.SaveChangesAsync();
                                    payement = null;
                                }
                                var se = donnée.tbl_payement.Where((d => d.N_Matricule == matricule && d.Type == type_operation));
                                
                                Decimal montantOperation = montant_operation;
                                Decimal? nullable1 = se.Sum((x => x.Montant));
                                string str = "Reste : " + Convert.ToDecimal((nullable1.HasValue ? new Decimal?(montantOperation - nullable1.GetValueOrDefault()) : new Decimal?())).ToString();
                                lblReste.Text = str;
                                txtMontant.Clear();
                                Alert.SShow("Payement validé avec succès.", Alert.AlertType.Sucess);
                                file = (byte[])null;
                                se = null;
                            }
                            await CallRecu();
                            string[] strArray = new string[8]
                            {
                "Reçu Payement ",
                prenom,
                " ",
                nom,
                " N° Matricule ",
                matricule,
                " ",
                null
                            };
                            now = DateTime.Now;
                            strArray[7] = now.ToString("dd-MM-yyyy HH.mm.ss");
                            name = string.Concat(strArray);
                            Detail_Facture facture = await FactureAsync(matricule);
                            Print.PrintRecuPdfFile(dataGridView1, name, "Année_Scolaire " + Principales.annéescolaire, "Payement ", prenom + " " + nom + " N°" + matricule, LogIn.mycontrng, "Quitaye School", false, facture, true);
                            s = null;
                            ses = null;
                            jour = null;
                            result = (string)null;
                            facture = (Detail_Facture)null;
                        }
                    }
                    msg = (MsgBox)null;
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
                            msg = (MsgBox)null;
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

        private Task<Detail_Facture> FactureAsync(string matricule) => Task.Factory.StartNew<Detail_Facture>((Func<Detail_Facture>)(() => Facture(matricule)));

        private Detail_Facture Facture(string matricule)
        {
            Detail_Facture detailFacture = new Detail_Facture();
            using (QuitayeContext ecoleDataContext = new QuitayeContext())
            {
                var source1 = ecoleDataContext.tbl_inscription.Where((d => d.N_Matricule == matricule)).Select(d => new
                {
                    Type = d.Type_Scolarité,
                    Scolarité = d.Scolarité,
                    Classe = d.Classe,
                    Cycle = d.Cycle,
                    Transport = d.Transport,
                    Assurance = d.Assurance,
                    Cantine = d.Cantine
                }).Take(1);
                if (source1.Count() != 0)
                {
                    var data = source1.First();
                    var source2 = ecoleDataContext.tbl_tarif_accessoire.Where((d => d.Nom == type_operation));
                    if (type_operation.Contains("Transport"))
                    {
                        if (!string.IsNullOrWhiteSpace(data.Transport))
                        {
                            if (data.Transport.Contains("Annuel"))
                                detailFacture.Scolarité = Convert.ToDecimal(source2.First().Tarif_Annuel);
                            else if (data.Transport.Contains("Mensuel"))
                                detailFacture.Scolarité = Convert.ToDecimal(source2.First().Tarif_Mensuel);
                            else if (data.Transport.Contains("Journa"))
                                detailFacture.Scolarité = Convert.ToDecimal(source2.First().Tarif_Journalier);
                        }
                    }
                    else if (type_operation.Contains("Assurance"))
                    {
                        if (!string.IsNullOrWhiteSpace(data.Assurance))
                        {
                            if (data.Assurance.Contains("Annuel"))
                                detailFacture.Scolarité = Convert.ToDecimal(source2.First().Tarif_Annuel);
                            else if (data.Assurance.Contains("Mensuel"))
                                detailFacture.Scolarité = Convert.ToDecimal(source2.First().Tarif_Mensuel);
                            else if (data.Assurance.Contains("Journa"))
                                detailFacture.Scolarité = Convert.ToDecimal(source2.First().Tarif_Journalier);
                        }
                    }
                    else if (type_operation.Contains("Cantine") 
                        && !string.IsNullOrWhiteSpace(data.Cantine))
                    {
                        if (data.Cantine.Contains("Annuel"))
                            detailFacture.Scolarité = Convert.ToDecimal(source2.First().Tarif_Annuel);
                        else if (data.Cantine.Contains("Mensuel"))
                            detailFacture.Scolarité = Convert.ToDecimal(source2.First().Tarif_Mensuel);
                        else if (data.Cantine.Contains("Journa"))
                            detailFacture.Scolarité = Convert.ToDecimal(source2.First().Tarif_Journalier);
                    }
                }
                var source3 = ecoleDataContext.tbl_payement
                    .Where(d => d.Année_Scolaire == Principales.annéescolaire 
                    && d.N_Matricule == matricule && d.Type == type_operation).GroupBy(d => new
                {
                    Date = DbFunctions.TruncateTime(d.Date_Payement.Value)
                }).OrderByDescending (gr => gr.Key.Date).Select(gr => new
                {
                    Date = gr.Key.Date,
                    Montant = gr.Sum(x => x.Montant)
                });
                detailFacture.MontantPayée = Convert.ToDecimal(source3.Sum(x => x.Montant));
                detailFacture.Restant = detailFacture.Scolarité - detailFacture.MontantPayée;
                detailFacture.Type_Operation = type_operation;
                detailFacture.PayementJour = Convert.ToDecimal(source3.First().Montant);
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
