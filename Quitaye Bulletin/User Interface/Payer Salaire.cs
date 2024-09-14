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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Payer_Salaire : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Payer_Salaire()
        {
            InitializeComponent();
            if (Principales.role == "Administrateur")
                CheckNegatif.Visible = true;
        }

        public string reste;
        public string prenom;
        public string nom;
        public int id;
        public string genre;
        private string filePath = "";
        private string filename;
        public string ok;
        public decimal payée;

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool staff = false;

        string operation;

        private async void btnValider_Click(object sender, EventArgs e)
        {
            if (txtMontant.Text != "" && operation != "")
            {
                MsgBox msg = new MsgBox();
                msg.show("Voulez-vous valider cette opération ?", "Attente Confirmation", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                msg.ShowDialog();
                if (msg.clicked == "Non")
                    return;
                else if (msg.clicked == "Oui")
                {
                    try
                    {
                        if (staff)
                        {
                            using (var donnée = new QuitayeContext())
                            {
                                var s = from d in donnée.tbl_payement orderby d.Id descending select d;
                                var ses = (from d in donnée.tbl_staff where d.Id == id select d).First();
                                var jour = from d in donnée.tbl_journal_comptable select d;
                                string result = Regex.Replace(txtMontant.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
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
                                        //payement.Classe = classes;
                                        if (CheckNegatif.Checked == true)
                                        {
                                            payement.Montant = -+Convert.ToDecimal(result);
                                        }
                                        else payement.Montant = Convert.ToDecimal(result);
                                        payement.Opération = operation;
                                        payement.Prenom = prenom;
                                        payement.Nom = nom;
                                        payement.N_Matricule = id.ToString();
                                        payement.Date_Payement = DateOp.Value.Date;
                                        payement.Date_Enregistrement = DateTime.Now;
                                        payement.Commentaire = txtCommentaire.Text;
                                        payement.Fichier = file;
                                        payement.Nom_Fichier = filename;
                                        //payement.Cycle = cycle;
                                        payement.Genre = genre;
                                        if (staff == false)
                                        {
                                            payement.Cycle = "Enseignant";
                                        }
                                        else payement.Cycle = "Staff";
                                        payement.Type = "Salaire";

                                        donnée.tbl_payement.Add(payement);
                                        donnée.SaveChangesAsync();
                                        var dee = from d in donnée.tbl_payement where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == id.ToString() select d;

                                        ok = "Oui";
                                    }
                                    else
                                    {
                                        var payement = new Models.Context.tbl_payement();
                                        payement.Id = (ss.Id) + 1;
                                        payement.Auteur = Principales.profile;
                                        payement.Année_Scolaire = Principales.annéescolaire;
                                        //payement.Classe = classes;
                                        if (CheckNegatif.Checked == true)
                                        {
                                            payement.Montant = -+Convert.ToDecimal(result);
                                        }
                                        else payement.Montant = Convert.ToDecimal(result);
                                        payement.Prenom = prenom;
                                        payement.Nom = nom;
                                        payement.N_Matricule = id.ToString();
                                        payement.Date_Payement = DateOp.Value.Date;
                                        payement.Date_Enregistrement = DateTime.Now;
                                        payement.Commentaire = txtCommentaire.Text;
                                        //payement.Cycle = cycle;
                                        payement.Genre = genre;
                                        if (staff == false)
                                        {
                                            payement.Cycle = "Enseignant";
                                        }
                                        else payement.Cycle = "Staff";
                                        payement.Type = "Salaire";

                                        donnée.tbl_payement.Add(payement);
                                        await donnée.SaveChangesAsync();

                                        var dee = from d in donnée.tbl_payement where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == id.ToString() && d.Type == "Salaire" select d;

                                        ok = "Oui";

                                    }



                                    if (jour.Count() > 0)
                                    {
                                        var j = (from d in donnée.tbl_journal_comptable orderby d.Id descending select d).First();

                                        int id = Convert.ToInt32(j.Id) + 1;

                                        //var dn = (from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte }).First();
                                        //var don = from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte };
                                        // decimal mont = Convert.ToDecimal(don.Sum(x => x.Montant));
                                        decimal montant = 0;
                                        //if (mont != 0)
                                        montant = Convert.ToDecimal(result);
                                        var V = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("STAF") orderby d.Ref_Pièces descending select d;
                                        if (V.Count() > 0)
                                        {
                                            var di = (from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("STAF") orderby d.Ref_Pièces descending select d).First();
                                            string[] arry = di.Ref_Pièces.Split('F');
                                            int reference = (Convert.ToInt32(arry[1]) * 1) + 1;

                                            //var f = (from d in donnée.tbl_Compte_Comptable where d.Compte == dn.Compte select d).First();
                                            var journal = new Models.Context.tbl_journal_comptable();
                                            journal.Id = id;
                                            journal.Auteur = Principales.profile;
                                            journal.Compte = "570100";
                                            journal.Date = DateTime.Now;
                                            journal.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "STAF" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            await donnée.SaveChangesAsync();


                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal1.Id = id + 1;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "STAF" + reference;
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
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "STAF" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            //donnée.SaveChangesAsync();

                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal1.Id = id + 1;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "STAF" + reference;
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
                                        montant = Convert.ToDecimal(result);
                                        var V = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("STAFF") orderby d.Ref_Pièces descending select d;
                                        if (V.Count() > 0)
                                        {
                                            var di = (from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("STAF") orderby d.Ref_Pièces descending select d).First();
                                            string[] arry = di.Ref_Pièces.Split('F');
                                            int reference = (Convert.ToInt32(arry[1]) * 1) + 1;

                                            //var f = (from d in donnée.tbl_Compte_Comptable where d.Compte == dn.Compte select d).First();
                                            var journal = new Models.Context.tbl_journal_comptable();
                                            journal.Id = 1;
                                            journal.Auteur = Principales.profile;
                                            journal.Compte = "570100";
                                            journal.Date = DateTime.Now;
                                            journal.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "STAF" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            await donnée.SaveChangesAsync();

                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal.Id = 2;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "STAF" + reference;
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
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "STAF" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            //donnée.SaveChangesAsync();

                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal1.Id = 2;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "STAF" + reference;
                                            donnée.tbl_journal_comptable.Add(journal1);
                                            await donnée.SaveChangesAsync();
                                        }
                                    }

                                    txtMontant.Clear();

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
                                        //payement.Classe = classes;
                                        if (CheckNegatif.Checked == true)
                                        {
                                            payement.Montant = -+Convert.ToDecimal(result);
                                        }
                                        else payement.Montant = Convert.ToDecimal(result);
                                        payement.Prenom = prenom;
                                        payement.Nom = nom;
                                        payement.N_Matricule = id.ToString();
                                        payement.Date_Payement = DateOp.Value.Date;
                                        payement.Date_Enregistrement = DateTime.Now;
                                        payement.Commentaire = txtCommentaire.Text;
                                        payement.Genre = genre;
                                        payement.Fichier = file;
                                        payement.Opération = operation;
                                        payement.Nom_Fichier = filename;
                                        //payement.Cycle = cycle;
                                        payement.Type = "Salaire";

                                        donnée.tbl_payement.Add(payement);
                                        donnée.SaveChangesAsync();

                                        var dee = from d in donnée.tbl_payement where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == id.ToString() select d;


                                        ok = "Oui";
                                    }
                                    else
                                    {
                                        var payement = new Models.Context.tbl_payement();
                                        payement.Id = 1;
                                        payement.Auteur = Principales.profile;
                                        payement.Année_Scolaire = Principales.annéescolaire;
                                        //payement.Classe = classes;
                                        if (CheckNegatif.Checked == true)
                                        {
                                            payement.Montant = -+Convert.ToDecimal(result);
                                        }
                                        else payement.Montant = Convert.ToDecimal(result);
                                        payement.Prenom = prenom;
                                        payement.Nom = nom;
                                        payement.N_Matricule = id.ToString();
                                        payement.Date_Payement = DateOp.Value.Date;
                                        payement.Date_Enregistrement = DateTime.Now;
                                        payement.Commentaire = txtCommentaire.Text;
                                        if (staff == false)
                                        {
                                            payement.Cycle = "Enseignant";
                                        }
                                        else payement.Cycle = "Staff";
                                        //payement.Cycle = cycle;
                                        payement.Genre = genre;
                                        payement.Type = "Salaire";
                                        donnée.tbl_payement.Add(payement);
                                        donnée.SaveChangesAsync();

                                        var dee = from d in donnée.tbl_payement where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == id.ToString() select d;

                                    }


                                    if (jour.Count() > 0)
                                    {
                                        var j = (from d in donnée.tbl_journal_comptable orderby d.Id descending select d).First();

                                        int id = Convert.ToInt32(j.Id) + 1;

                                        //var dn = (from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte }).First();
                                        //var don = from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte };
                                        //decimal mont = Convert.ToDecimal(don.Sum(x => x.Montant));
                                        decimal montant = 0;
                                        //if (mont != 0)
                                        montant = Convert.ToDecimal(result);
                                        var V = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("STAF") orderby d.Ref_Pièces descending select d;
                                        if (V.Count() > 0)
                                        {
                                            var di = (from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("STAF") orderby d.Ref_Pièces descending select d).First();
                                            string[] arry = di.Ref_Pièces.Split('F');
                                            int reference = (Convert.ToInt32(arry[1]) * 1) + 1;

                                            //var f = (from d in donnée.tbl_Compte_Comptable where d.Compte == dn.Compte select d).First();
                                            var journal = new Models.Context.tbl_journal_comptable();
                                            journal.Id = id;
                                            journal.Auteur = Principales.profile;
                                            journal.Compte = "570100";
                                            journal.Date = DateTime.Now;
                                            journal.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "STAF" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            donnée.SaveChangesAsync();


                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal1.Id = id + 1;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "STAF" + reference;
                                            donnée.tbl_journal_comptable.Add(journal1);
                                            donnée.SaveChangesAsync();
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
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "STAF" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            //donnée.SaveChangesAsync();

                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal1.Id = id + 1;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "STAF" + reference;
                                            donnée.tbl_journal_comptable.Add(journal1);
                                            donnée.SaveChangesAsync();
                                        }
                                    }
                                    else
                                    {
                                        //var dn = (from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte }).First();
                                        //var don = from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte };
                                        //decimal mont = Convert.ToDecimal(don.Sum(x => x.Montant));
                                        decimal montant = 0;
                                        //if (mont != 0)
                                        montant = Convert.ToDecimal(result);
                                        var V = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("STAF") orderby d.Ref_Pièces descending select d;
                                        if (V.Count() > 0)
                                        {
                                            var di = (from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("STAF") orderby d.Ref_Pièces descending select d).First();
                                            string[] arry = di.Ref_Pièces.Split('F');
                                            int reference = (Convert.ToInt32(arry[1]) * 1) + 1;

                                            //var f = (from d in donnée.tbl_Compte_Comptable where d.Compte == dn.Compte select d).First();
                                            var journal = new Models.Context.tbl_journal_comptable();
                                            journal.Id = 1;
                                            journal.Auteur = Principales.profile;
                                            journal.Compte = "570100";
                                            journal.Date = DateTime.Now;
                                            journal.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "STAF" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            await donnée.SaveChangesAsync();


                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal.Id = 2;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "STAF" + reference;
                                            donnée.tbl_journal_comptable.Add(journal1);
                                            donnée.SaveChangesAsync();
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
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "STAF" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            //donnée.SaveChangesAsync();

                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal1.Id = 2;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "STAF" + reference;
                                            donnée.tbl_journal_comptable.Add(journal1);
                                            await donnée.SaveChangesAsync();
                                        }
                                    }


                                }
                                var se = from d in donnée.tbl_payement where d.N_Matricule == id.ToString() && d.Type == "Salaire" && d.Cycle == "Staff" && d.Année_Scolaire == Principales.annéescolaire select d;
                                lblMontant.Text = "Montant Payée : " + Convert.ToDecimal(se.Sum(x => x.Montant)).ToString("N0") + " FCFA";
                                txtMontant.Clear();
                                Alert.SShow("Salaire validé avec succès.", Alert.AlertType.Sucess);
                            }
                        }
                        else
                        {
                            using (var donnée = new QuitayeContext())
                            {
                                string result = Regex.Replace(txtMontant.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                                var s = from d in donnée.tbl_payement orderby d.Id descending select d;
                                var ses = (from d in donnée.tbl_enseignant where d.Id == id select d).First();
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
                                        //payement.Classe = classes;
                                        if (CheckNegatif.Checked == true)
                                        {
                                            payement.Montant = -+Convert.ToDecimal(result);
                                        }
                                        else payement.Montant = Convert.ToDecimal(result);
                                        payement.Opération = operation;
                                        payement.Prenom = prenom;
                                        payement.Nom = nom;
                                        payement.N_Matricule = id.ToString();
                                        payement.Date_Payement = DateOp.Value.Date;
                                        payement.Date_Enregistrement = DateTime.Now;
                                        payement.Commentaire = txtCommentaire.Text;
                                        payement.Fichier = file;
                                        payement.Nom_Fichier = filename;
                                        //payement.Cycle = cycle;
                                        payement.Genre = genre;
                                        if (staff == false)
                                        {
                                            payement.Cycle = "Enseignant";
                                        }
                                        else payement.Cycle = "Staff";
                                        payement.Type = "Salaire";

                                        donnée.tbl_payement.Add(payement);
                                        await donnée.SaveChangesAsync();
                                        var dee = from d in donnée.tbl_payement where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == id.ToString() select d;

                                        ok = "Oui";
                                    }
                                    else
                                    {
                                        var payement = new Models.Context.tbl_payement();
                                        payement.Id = (ss.Id) + 1;
                                        payement.Auteur = Principales.profile;
                                        payement.Année_Scolaire = Principales.annéescolaire;
                                        //payement.Classe = classes;
                                        if (CheckNegatif.Checked == true)
                                        {
                                            payement.Montant = -+Convert.ToDecimal(result);
                                        }
                                        else payement.Montant = Convert.ToDecimal(result);
                                        payement.Prenom = prenom;
                                        payement.Nom = nom;
                                        payement.N_Matricule = id.ToString();
                                        payement.Date_Payement = DateOp.Value.Date;
                                        payement.Date_Enregistrement = DateTime.Now;
                                        payement.Commentaire = txtCommentaire.Text;
                                        //payement.Cycle = cycle;
                                        payement.Genre = genre;
                                        if (staff == false)
                                        {
                                            payement.Cycle = "Enseignant";
                                        }
                                        else payement.Cycle = "Staff";
                                        payement.Type = "Salaire";

                                        donnée.tbl_payement.Add(payement);
                                        await donnée.SaveChangesAsync();

                                        var dee = from d in donnée.tbl_payement where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == id.ToString() && d.Type == "Salaire" select d;

                                        ok = "Oui";

                                    }



                                    if (jour.Count() > 0)
                                    {
                                        var j = (from d in donnée.tbl_journal_comptable orderby d.Id descending select d).First();

                                        int id = Convert.ToInt32(j.Id) + 1;

                                        //var dn = (from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte }).First();
                                        //var don = from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte };
                                        // decimal mont = Convert.ToDecimal(don.Sum(x => x.Montant));
                                        decimal montant = 0;
                                        //if (mont != 0)
                                        montant = Convert.ToDecimal(result);
                                        var V = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("ENS") orderby d.Ref_Pièces descending select d;
                                        if (V.Count() > 0)
                                        {
                                            var di = (from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("ENS") orderby d.Ref_Pièces descending select d).First();
                                            string[] arry = di.Ref_Pièces.Split('S');
                                            int reference = (Convert.ToInt32(arry[1]) * 1) + 1;

                                            //var f = (from d in donnée.tbl_Compte_Comptable where d.Compte == dn.Compte select d).First();
                                            var journal = new Models.Context.tbl_journal_comptable();
                                            journal.Id = id;
                                            journal.Auteur = Principales.profile;
                                            journal.Compte = "570100";
                                            journal.Date = DateTime.Now;
                                            journal.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "ENS" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            await donnée.SaveChangesAsync();


                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal1.Id = id + 1;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom_Complet;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "ENS" + reference;
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
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "ENS" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            //donnée.SaveChangesAsync();

                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal1.Id = id + 1;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom_Complet;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "ENS" + reference;
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
                                        montant = Convert.ToDecimal(result);
                                        var V = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("ENS") orderby d.Ref_Pièces descending select d;
                                        if (V.Count() > 0)
                                        {
                                            var di = (from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("STAF") orderby d.Ref_Pièces descending select d).First();
                                            string[] arry = di.Ref_Pièces.Split('S');
                                            int reference = (Convert.ToInt32(arry[1]) * 1) + 1;

                                            //var f = (from d in donnée.tbl_Compte_Comptable where d.Compte == dn.Compte select d).First();
                                            var journal = new Models.Context.tbl_journal_comptable();
                                            journal.Id = 1;
                                            journal.Auteur = Principales.profile;
                                            journal.Compte = "570100";
                                            journal.Date = DateTime.Now;
                                            journal.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "ENS" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            await donnée.SaveChangesAsync();


                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal.Id = 2;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom_Complet;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "ENS" + reference;
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
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "ENS" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            //donnée.SaveChangesAsync();

                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal1.Id = 2;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom_Complet;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "ENS" + reference;
                                            donnée.tbl_journal_comptable.Add(journal1);
                                            await donnée.SaveChangesAsync();
                                        }
                                    }

                                    txtMontant.Clear();

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
                                        //payement.Classe = classes;
                                        if (CheckNegatif.Checked == true)
                                        {
                                            payement.Montant = -+Convert.ToDecimal(result);
                                        }
                                        else payement.Montant = Convert.ToDecimal(result);
                                        payement.Prenom = prenom;
                                        payement.Nom = nom;
                                        payement.N_Matricule = id.ToString();
                                        payement.Date_Payement = DateOp.Value.Date;
                                        payement.Date_Enregistrement = DateTime.Now;
                                        payement.Commentaire = txtCommentaire.Text;
                                        payement.Genre = genre;
                                        payement.Fichier = file;
                                        payement.Opération = operation;
                                        payement.Nom_Fichier = filename;
                                        //payement.Cycle = cycle;
                                        payement.Type = "Salaire";

                                        donnée.tbl_payement.Add(payement);
                                        await donnée.SaveChangesAsync();

                                        var dee = from d in donnée.tbl_payement where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == id.ToString() select d;


                                        ok = "Oui";
                                    }
                                    else
                                    {
                                        var payement = new Models.Context.tbl_payement();
                                        payement.Id = 1;
                                        payement.Auteur = Principales.profile;
                                        payement.Année_Scolaire = Principales.annéescolaire;
                                        //payement.Classe = classes;
                                        if (CheckNegatif.Checked == true)
                                        {
                                            payement.Montant = -+Convert.ToDecimal(result);
                                        }
                                        else payement.Montant = Convert.ToDecimal(result);
                                        payement.Prenom = prenom;
                                        payement.Nom = nom;
                                        payement.N_Matricule = id.ToString();
                                        payement.Date_Payement = DateOp.Value.Date;
                                        payement.Date_Enregistrement = DateTime.Now;
                                        payement.Commentaire = txtCommentaire.Text;
                                        if (staff == false)
                                        {
                                            payement.Cycle = "Enseignant";
                                        }
                                        else payement.Cycle = "Staff";
                                        //payement.Cycle = cycle;
                                        payement.Genre = genre;
                                        payement.Type = "Salaire";
                                        donnée.tbl_payement.Add(payement);
                                        await donnée.SaveChangesAsync();

                                        var dee = from d in donnée.tbl_payement where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == id.ToString() select d;

                                    }


                                    if (jour.Count() > 0)
                                    {
                                        var j = (from d in donnée.tbl_journal_comptable orderby d.Id descending select d).First();

                                        int id = Convert.ToInt32(j.Id) + 1;

                                        //var dn = (from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte }).First();
                                        //var don = from d in donnée.tbl_formule_inscription where d.Id == CustomRadio._id select new { Montant = d.Montant, Compte = d.Compte };
                                        //decimal mont = Convert.ToDecimal(don.Sum(x => x.Montant));
                                        decimal montant = 0;
                                        //if (mont != 0)
                                        montant = Convert.ToDecimal(result);
                                        var V = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("ENS") orderby d.Ref_Pièces descending select d;
                                        if (V.Count() > 0)
                                        {
                                            var di = (from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("ENS") orderby d.Ref_Pièces descending select d).First();
                                            string[] arry = di.Ref_Pièces.Split('S');
                                            int reference = (Convert.ToInt32(arry[1]) * 1) + 1;

                                            //var f = (from d in donnée.tbl_Compte_Comptable where d.Compte == dn.Compte select d).First();
                                            var journal = new Models.Context.tbl_journal_comptable();
                                            journal.Id = id;
                                            journal.Auteur = Principales.profile;
                                            journal.Compte = "570100";
                                            journal.Date = DateTime.Now;
                                            journal.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "ENS" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            await donnée.SaveChangesAsync();


                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal1.Id = id + 1;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom_Complet;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "ENS" + reference;
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
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "ENS" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            //donnée.SaveChangesAsync();

                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal1.Id = id + 1;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom_Complet;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "ENS" + reference;
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
                                        montant = Convert.ToDecimal(result);
                                        var V = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("ENS") orderby d.Ref_Pièces descending select d;
                                        if (V.Count() > 0)
                                        {
                                            var di = (from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("ENS") orderby d.Ref_Pièces descending select d).First();
                                            string[] arry = di.Ref_Pièces.Split('S');
                                            int reference = (Convert.ToInt32(arry[1]) * 1) + 1;

                                            //var f = (from d in donnée.tbl_Compte_Comptable where d.Compte == dn.Compte select d).First();
                                            var journal = new Models.Context.tbl_journal_comptable();
                                            journal.Id = 1;
                                            journal.Auteur = Principales.profile;
                                            journal.Compte = "570100";
                                            journal.Date = DateTime.Now;
                                            journal.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "ENS" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            donnée.SaveChangesAsync();


                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal.Id = 2;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom_Complet;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "ENS" + reference;
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
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal.Crédit = -montant;
                                            }
                                            else
                                                journal.Crédit = montant;
                                            journal.Libelle = "Caisse";
                                            journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                            journal.Ref_Pièces = "ENS" + reference;
                                            donnée.tbl_journal_comptable.Add(journal);
                                            //donnée.SaveChangesAsync();

                                            var journal1 = new Models.Context.tbl_journal_comptable();
                                            journal1.Id = 2;
                                            journal1.Auteur = Principales.profile;
                                            journal1.Compte = "411E" + id.ToString();
                                            journal1.Date = DateTime.Now;
                                            journal1.Date_Enregistrement = DateTime.Now;
                                            if (CheckNegatif.Checked == true)
                                            {
                                                journal1.Débit = -montant;
                                            }
                                            else
                                                journal1.Débit = montant;
                                            journal1.Libelle = ses.Nom_Complet;
                                            journal1.Commentaire = txtCommentaire.Text;
                                            journal1.Ref_Pièces = "ENS" + reference;
                                            donnée.tbl_journal_comptable.Add(journal1);
                                            await donnée.SaveChangesAsync();
                                        }
                                    }
                                }
                                var se = from d in donnée.tbl_payement where d.N_Matricule == id.ToString() && d.Type == "Salaire" && (d.Cycle == "Enseignant" || d.Cycle == null) && d.Année_Scolaire == Principales.annéescolaire select d;
                                lblMontant.Text = "Montant Payée : " + Convert.ToDecimal(se.Sum(x => x.Montant)).ToString("N0") + " FCFA";
                                txtMontant.Clear();
                                Alert.SShow("Salaire validé avec succès.", Alert.AlertType.Sucess);
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
                                msg.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                                msg.ShowDialog();
                            }
                            else
                            {
                                msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                                msg.ShowDialog();
                            }
                        }
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

        private void txtMontant_TextChanged(object sender, EventArgs e)
        {
            if (txtMontant.Text != "")
            {
                txtMontant.Text = Convert.ToDecimal(txtMontant.Text).ToString("N0");
                txtMontant.SelectionStart = txtMontant.Text.Length;
            }
        }
    }
}
