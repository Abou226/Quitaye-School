using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Balance_Initiale : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Balance_Initiale(string name)
        {
            InitializeComponent();
            lblFormName.Text = name;
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
            temp = 1;
        }

        int temp = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (temp == 1)
            {
                FillDG();
                FillCbxCompte();
                FillCbx();
                temp = 0;
            }
        }

        Timer timer = new Timer();

        private void FillDG()
        {
            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("ST")
                        orderby d.Id descending
                        select new
                        {
                            Id = d.Id,
                            Libelle = d.Libelle,
                            Compte = d.Compte,
                            Ref_Pièce = d.Ref_Pièces,
                            Débit = d.Débit,
                            Crédit = d.Crédit,
                            Date_Ajout = d.Date,
                            Description = d.Commentaire,
                            Auteur = d.Auteur,
                        };

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = s;
                DeleteColumn delete = new DeleteColumn();
                delete.HeaderText = "Sup";
                delete.Name = "Delete";

                //EditColumn edit = new EditColumn();
                //edit.HeaderText = "Edit";
                //edit.Name = "Edit";
                //edit.Width = 20;

                //dataGridView1.Columns.Add(edit);
                dataGridView1.Columns.Add(delete);
                //dataGridView1.Columns["Edit"].Width = 20;
                dataGridView1.Columns["Delete"].Width = 30;
            }
        }

        public static string ok;
        //int id;

        private async void btnAjouter_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                if (txtMontant.Text != "" && cbxDébit.Text != "" && cbxCrédit.Text != "")
                {
                    using (var donnée = new QuitayeContext())
                    {
                        var res = from d in donnée.tbl_journal_comptable select d;
                        int i = 1;
                        if (res.Count() != 0)
                        {
                            var re = (from d in donnée.tbl_journal_comptable orderby d.Id descending select d).First();
                            i = re.Id + 1;
                        }
                        var bc = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("ST") select d;
                        string refpièce = "";
                        if (bc.Count() != 0)
                        {
                            var bcs = (from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("ST") orderby d.Id descending select d).First();
                            refpièce = bcs.Ref_Pièces;
                        }
                        string result = Regex.Replace(txtMontant.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                        var débit = cbxDébit.Text.Split('-');
                        var crédit = cbxCrédit.Text.Split('-');
                        string[] refesplit;
                        int reference = 1;
                        if (refpièce != "")
                        {
                            refesplit = refpièce.Split('T');
                            reference = Convert.ToInt32(refesplit[1]) + 1;
                        }



                        var tbl = new Models.Context.tbl_journal_comptable();
                        tbl.Id = i;
                        tbl.Auteur = Principales.profile;
                        tbl.Compte = débit[0];

                        tbl.Débit = Convert.ToDecimal(result);
                        //journal.Date = DatePayement.Value.Date;
                        tbl.Date_Enregistrement = DateTime.Now;
                        tbl.Libelle = débit[1];
                        tbl.Commentaire = txtDescription.Text;

                        //if (filePath != null || filePath != "")
                        //{
                        //    journal.Nom_Fichier = filename;
                        //    journal.Fichier = file;
                        //}
                        tbl.Ref_Pièces = "ST" + reference;
                        donnée.tbl_journal_comptable.Add(tbl);
                        await donnée.SaveChangesAsync();

                        var tbl1 = new Models.Context.tbl_journal_comptable();
                        tbl1.Id = i + 1;
                        tbl1.Auteur = Principales.profile;
                        tbl1.Compte = crédit[0];

                        tbl1.Crédit = Convert.ToDecimal(result);
                        //journal.Date = DatePayement.Value.Date;
                        tbl1.Date_Enregistrement = DateTime.Now;
                        tbl1.Libelle = crédit[1];
                        tbl1.Commentaire = txtDescription.Text;

                        //if (filePath != null || filePath != "")
                        //{
                        //    journal.Nom_Fichier = filename;
                        //    journal.Fichier = file;
                        //}
                        tbl1.Ref_Pièces = "ST" + (reference);
                        donnée.tbl_journal_comptable.Add(tbl1);
                        await donnée.SaveChangesAsync();
                        ClearData();
                        FillDG();
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

        private void ClearData()
        {
            txtDescription.Clear();
            txtMontant.Clear();
            cbxCrédit.Text = null;
            cbxDébit.Text = null;
        }

        private void FillCbxCompte()
        {
            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_Compte_Comptable where d.Type == null && d.Compte.ToString().StartsWith("5") orderby d.Compte ascending  select d;
                cbxDébit.DataSource = s;
                cbxDébit.DisplayMember = "Nom_Compte";
                cbxDébit.ValueMember = "Id";
                cbxDébit.Text = null;
            }
        }

        private void FillCbx()
        {
            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_Compte_Comptable where d.Type == null && d.Compte.ToString().StartsWith("5") orderby d.Compte ascending select d;
                cbxCrédit.DataSource = s;
                cbxCrédit.DisplayMember = "Nom_Compte";
                cbxCrédit.ValueMember = "Id";
                cbxCrédit.Text = null;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 9)
            {
                using(var donnée = new QuitayeContext())
                {
                    string reference = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    MsgBox msg = new MsgBox();
                    msg.show("Voulez-vous annulée cette écriture ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                    msg.ShowDialog();
                    if (msg.clicked == "Non")
                        return;
                    else if (msg.clicked == "Oui")
                    {
                        var s = from d in donnée.tbl_journal_comptable where d.Ref_Pièces == reference select new { Id = d.Id };
                        //dataGridView3.DataSource = s;

                        if (s.Count() != 0)
                        {
                            foreach (var item in s)
                            {
                                var ss = (from d in donnée.tbl_journal_comptable where d.Id == Convert.ToInt32(item.Id) select d).First();
                                var bc = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith("ST") orderby d.Id descending select d;
                                var refidcom = from d in donnée.tbl_journal_comptable orderby d.Id descending select d;
                                int idcomp = 1;
                                if (refidcom.Count() > 0)
                                {
                                    var refid = (from d in donnée.tbl_journal_comptable orderby d.Id descending  select d).First();
                                    idcomp = Convert.ToInt32(refid.Id) + 1;
                                }
                                ss.Active = "Non";
                                if (bc.Count() > 0)
                                {
                                    var bcs = (from d in donnée.tbl_journal_comptable where d.Id == Convert.ToInt32(item.Id) orderby d.Id descending select d).First();
                                    //string[] arry = bcs.Ref_Pièces.Split('F');

                                    string compte = bcs.Compte;
                                    string[] refesplit = bcs.Ref_Pièces.Split('T');
                                    int referenc = Convert.ToInt32(refesplit[1]) + 1;

                                    var journal = new Models.Context.tbl_journal_comptable();
                                    journal.Id = idcomp;
                                    journal.Auteur = Principales.profile;
                                    journal.Compte = compte;
                                    if (bcs.Débit == null)
                                    {
                                        journal.Crédit = -ss.Crédit;
                                    }
                                    else
                                        journal.Débit = -ss.Débit;
                                    //journal.Date = DatePayement.Value.Date;
                                    journal.Date_Enregistrement = DateTime.Now;
                                    journal.Libelle = ss.Libelle;
                                    string commentaire = "Annulation Balance Initial " + ss.Ref_Pièces;
                                    journal.Commentaire = commentaire;
                                    //journal.Ref_Payement = txtRef.Text;
                                    journal.Ref_Pièces = "ST" + referenc;
                                    journal.Active = "Non";
                                    donnée.tbl_journal_comptable.Add(journal);
                                    donnée.SaveChangesAsync();
                                }
                            }
                            Alert.SShow("Annulation effectué avec succès.", Alert.AlertType.Sucess);
                            FillDG();
                        }
                    }
                }
                
            }
        }
    }
}
