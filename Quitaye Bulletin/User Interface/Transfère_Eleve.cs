using FontAwesome.Sharp;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Transfère_Eleve : Form
    {
        public Transfère_Eleve()
        {
            InitializeComponent();
        }

        string mycontrng = LogIn.mycontrng;
        
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

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnValider_Click(object sender, EventArgs e)
        {
            if (txtMontant.Text != "")
            {
                try
                {
                    MsgBox msg = new MsgBox();
                    msg.show("Voulez-vous valider ce transfère ?", "Attente Confirmation", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                    msg.ShowDialog();
                    if (msg.clicked == "Non")
                        return;
                    else if (msg.clicked == "Oui")
                    {
                        using (var donnée = new QuitayeContext())
                        {
                            var s = from d in donnée.tbl_payement orderby d.Id descending select d;
                            var ses = (from d in donnée.tbl_inscription where d.N_Matricule == matricule select d).First();
                            var jour = from d in donnée.tbl_journal_comptable select d;
                            if (s.Count() > 0)
                            {
                                var ss = (from d in donnée.tbl_payement orderby d.Id descending select d).First();

                                if (filePath != "")
                                {
                                    byte[] file;
                                    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                                    {
                                        using (var reader = new BinaryReader(stream))
                                        {
                                            file = reader.ReadBytes((int)stream.Length);
                                        }
                                    }
                                    var payement = new Models.Context.tbl_payement();
                                    payement.Id = (ss.Id) + 1;
                                    payement.Auteur = Principales.profile;
                                    payement.Année_Scolaire = Principales.annéescolaire;
                                    payement.Classe = classes;
                                    payement.Montant = Convert.ToDecimal(txtMontant.Text);
                                    payement.Prenom = prenom;
                                    payement.Nom = nom;
                                    payement.N_Matricule = matricule;
                                    payement.Date_Payement = DateOp.Value.Date;
                                    payement.Date_Enregistrement = DateTime.Now;
                                    payement.Commentaire = txtCommentaire.Text;
                                    payement.Fichier = file;
                                    payement.Nom_Fichier = filename;
                                    payement.Cycle = cycle;
                                    payement.Genre = genre;
                                    payement.Type = "Transfère";

                                    donnée.tbl_payement.Add(payement);
                                    await donnée.SaveChangesAsync();
                                    ok = "Oui";
                                }
                                else
                                {
                                    var payement = new Models.Context.tbl_payement();
                                    payement.Id = (ss.Id) + 1;
                                    payement.Auteur = Principales.profile;
                                    payement.Année_Scolaire = Principales.annéescolaire;
                                    payement.Classe = classes;
                                    payement.Montant = Convert.ToDecimal(txtMontant.Text);
                                    payement.Prenom = prenom;
                                    payement.Nom = nom;
                                    payement.N_Matricule = matricule;
                                    payement.Date_Payement = DateOp.Value.Date;
                                    payement.Date_Enregistrement = DateTime.Now;
                                    payement.Commentaire = txtCommentaire.Text;
                                    payement.Cycle = cycle;
                                    payement.Genre = genre;
                                    payement.Type = "Transfère";

                                    donnée.tbl_payement.Add(payement);
                                    await donnée.SaveChangesAsync();
                                    ok = "Oui";

                                }

                                var active = donnée.tbl_inscription.SingleOrDefault(x => x.Classe == classes && x.Année_Scolaire == Principales.annéescolaire && x.N_Matricule == matricule);
                                active.Active = "Non";
                                active.Date_Desactivation = DateTime.Now;
                                active.Motif_Desactivation = "Transfère";
                                await donnée.SaveChangesAsync();

                                if (jour.Count() > 0)
                                {
                                    var j = (from d in donnée.tbl_journal_comptable orderby d.Id descending select d).First();

                                    int id = Convert.ToInt32(j.Id) + 1;

                                    //var dn = (from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte }).First();
                                    //var don = from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte };
                                    // decimal mont = Convert.ToDecimal(don.Sum(x => x.Montant));
                                    decimal montant = 0;
                                    //if (mont != 0)
                                    montant = Convert.ToDecimal(txtMontant.Text);
                                    var V = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("V") orderby d.Ref_Pièces descending select d;
                                    if (V.Count() > 0)
                                    {
                                        var di = (from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("V") orderby d.Ref_Pièces descending select d).First();
                                        string[] arry = di.Ref_Pièces.Split('V');
                                        int reference = (Convert.ToInt32(arry[1]) * 1) + 1;

                                        //var f = (from d in donnée.tbl_Compte_Comptable where d.Compte == dn.Compte select d).First();
                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = id;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = DateTime.Now;
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = montant;
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal);


                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal1.Id = id + 1;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = DateTime.Now;
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = montant;
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        int reference = 1;

                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = id;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = DateTime.Now;
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = montant;
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal);
                                        //donnée.SaveChangesAsync();

                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal1.Id = id + 1;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = DateTime.Now;
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = montant;
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                    }
                                }
                                else
                                {
                                    //var dn = (from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte }).First();
                                    //var don = from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte };
                                    //decimal mont = Convert.ToDecimal(don.Sum(x => x.Montant));
                                    decimal montant = 0;
                                    //if (mont != 0)
                                    montant = Convert.ToDecimal(txtMontant.Text);
                                    var V = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("V") orderby d.Ref_Pièces descending select d;
                                    if (V.Count() > 0)
                                    {
                                        var di = (from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("V") orderby d.Ref_Pièces descending select d).First();
                                        string[] arry = di.Ref_Pièces.Split('V');
                                        int reference = (Convert.ToInt32(arry[1]) * 1) + 1;

                                        //var f = (from d in donnée.tbl_Compte_Comptable where d.Compte == dn.Compte select d).First();
                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = 1;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = DateTime.Now;
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = montant;
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal);


                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal.Id = 2;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = DateTime.Now;
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = montant;
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        int reference = 1;

                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = 1;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = DateTime.Now;
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = montant;
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal);
                                        //donnée.SaveChangesAsync();

                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal1.Id = 2;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = DateTime.Now;
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = montant;
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                    }
                                }

                                txtMontant.Clear();
                                var se = from d in donnée.tbl_payement where d.N_Matricule == matricule && d.Type == "Scolarité" select d;
                                lblReste.Text = "Reste : " + Convert.ToDecimal(scolarité - se.Sum(x => x.Montant));
                                Alert.SShow("Payement et transfère validé avec succès.", Alert.AlertType.Sucess);
                            }
                            else
                            {
                                byte[] file;
                                if (filePath != "")
                                {
                                    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                                    {
                                        using (var reader = new BinaryReader(stream))
                                        {
                                            file = reader.ReadBytes((int)stream.Length);
                                        }
                                    }

                                    var payement = new Models.Context.tbl_payement();
                                    payement.Id = 1;
                                    payement.Auteur = Principales.profile;
                                    payement.Année_Scolaire = Principales.annéescolaire;
                                    payement.Classe = classes;
                                    payement.Montant = Convert.ToDecimal(txtMontant.Text);
                                    payement.Prenom = prenom;
                                    payement.Nom = nom;
                                    payement.N_Matricule = matricule;
                                    payement.Date_Payement = DateOp.Value.Date;
                                    payement.Date_Enregistrement = DateTime.Now;
                                    payement.Commentaire = txtCommentaire.Text;
                                    payement.Genre = genre;
                                    payement.Fichier = file;
                                    payement.Nom_Fichier = filename;
                                    payement.Cycle = cycle;
                                    payement.Type = "Transfère";

                                    donnée.tbl_payement.Add(payement);
                                    await donnée.SaveChangesAsync();

                                    ok = "Oui";
                                }
                                else
                                {
                                    var payement = new Models.Context.tbl_payement();
                                    payement.Id = 1;
                                    payement.Auteur = Principales.profile;
                                    payement.Année_Scolaire = Principales.annéescolaire;
                                    payement.Classe = classes;
                                    payement.Montant = Convert.ToDecimal(txtMontant.Text);
                                    payement.Prenom = prenom;
                                    payement.Nom = nom;
                                    payement.N_Matricule = matricule;
                                    payement.Date_Payement = DateOp.Value.Date;
                                    payement.Date_Enregistrement = DateTime.Now;
                                    payement.Commentaire = txtCommentaire.Text;
                                    payement.Cycle = cycle;
                                    payement.Genre = genre;
                                    payement.Type = "Transfère";

                                    donnée.tbl_payement.Add(payement);
                                    await donnée.SaveChangesAsync();

                                    ok = "Oui";
                                }

                                var active = donnée.tbl_inscription.SingleOrDefault(x => x.Classe == classes && x.Année_Scolaire == Principales.annéescolaire && x.N_Matricule == matricule);
                                active.Active = "Non";
                                active.Date_Desactivation = DateTime.Now;
                                active.Motif_Desactivation = "Transfère";
                                await donnée.SaveChangesAsync();

                                if (jour.Count() > 0)
                                {
                                    var j = (from d in donnée.tbl_journal_comptable orderby d.Id descending select d).First();

                                    int id = Convert.ToInt32(j.Id) + 1;

                                    //var dn = (from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte }).First();
                                    //var don = from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte };
                                    //decimal mont = Convert.ToDecimal(don.Sum(x => x.Montant));
                                    decimal montant = 0;
                                    //if (mont != 0)
                                    montant = Convert.ToDecimal(txtMontant.Text);
                                    var V = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("V") orderby d.Ref_Pièces descending select d;
                                    if (V.Count() > 0)
                                    {
                                        var di = (from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("V") orderby d.Ref_Pièces descending select d).First();
                                        string[] arry = di.Ref_Pièces.Split('V');
                                        int reference = (Convert.ToInt32(arry[1]) * 1) + 1;

                                        //var f = (from d in donnée.tbl_Compte_Comptable where d.Compte == dn.Compte select d).First();
                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = id;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = DateTime.Now;
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = montant;
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal);

                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal1.Id = id + 1;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = DateTime.Now;
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = montant;
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        int reference = 1;

                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = id;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = DateTime.Now;
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = montant;
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal);
                                        //donnée.SaveChangesAsync();

                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal1.Id = id + 1;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = DateTime.Now;
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = montant;
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                    }
                                }
                                else
                                {
                                    //var dn = (from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte }).First();
                                    //var don = from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte };
                                    //decimal mont = Convert.ToDecimal(don.Sum(x => x.Montant));
                                    decimal montant = 0;
                                    //if (mont != 0)
                                    montant = Convert.ToDecimal(txtMontant.Text);
                                    var V = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("V") orderby d.Ref_Pièces descending select d;
                                    if (V.Count() > 0)
                                    {
                                        var di = (from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("V") orderby d.Ref_Pièces descending select d).First();
                                        string[] arry = di.Ref_Pièces.Split('V');
                                        int reference = (Convert.ToInt32(arry[1]) * 1) + 1;

                                        //var f = (from d in donnée.tbl_Compte_Comptable where d.Compte == dn.Compte select d).First();
                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = 1;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = DateTime.Now;
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = montant;
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal);

                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal.Id = 2;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = DateTime.Now;
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = montant;
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                    }
                                    else
                                    {
                                        int reference = 1;

                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = 1;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = DateTime.Now;
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = montant;
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal);
                                        //donnée.SaveChangesAsync();

                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal1.Id = 2;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = DateTime.Now;
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = montant;
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference;
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                    }
                                }
                                var se = from d in donnée.tbl_payement where d.N_Matricule == matricule && d.Type == "Scolarité" select d;
                                lblReste.Text = "Reste : " + Convert.ToDecimal(scolarité - se.Sum(x => x.Montant));
                                txtMontant.Clear();

                                Alert.SShow("Payement et transfère validés avec succès.", Alert.AlertType.Sucess);
                            }
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
                            MsgBox msgs = new MsgBox();
                            msgs.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                            msgs.ShowDialog();
                        }
                        else
                        {
                            MsgBox msgs = new MsgBox();
                            msgs.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                            msgs.ShowDialog();
                        }
                    }
                }
                
            }
            else
            {
                Alert.SShow("Veillez remplir tous les zones de textes.", Alert.AlertType.Warning);
            }
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            IconButton p = sender as IconButton;

            if (p != null)
            {
                file.Filter = "(*.pdf; *.xls;*.docs)| *.pdf; *.xls; *.docs";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    filePath = file.FileName;
                    filename = Path.GetFileName(filePath);
                    //p.Image = Image.FromFile(file.FileName);
                }
            }
        }

        private void txtMontant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 46 && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}
