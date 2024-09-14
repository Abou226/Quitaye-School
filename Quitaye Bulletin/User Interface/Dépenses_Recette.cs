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
    public partial class Dépenses_Recette : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Dépenses_Recette()
        {
            InitializeComponent();

            //txtDesignation.Focus();
            SetSql();

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
                try
                {
                    FillDG();
                    FillCbx();
                    if (dataGridView1.Columns.Count < 2)
                    {
                        using (var donnée = new QuitayeContext())
                        {
                            var s = from d in donnée.tbl_journal_comptable where d.Validé == "Non" && d.Ref_Pièces.StartsWith(cbxJournal.Text) select d;
                            if (s.Count() != 0)
                            {
                                cbxJournal.Text = null;
                                Alert.SShow("Cet journal est en cours d'utilisation !", Alert.AlertType.Info);
                            }
                        }
                    }
                    temp = 0;
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

        private void SetSql()
        {
            try
            {
                using (var donnée = new QuitayeContext())
                {
                    var s = from d in donnée.tbl_list_journaux orderby d.Prefix select d;
                    cbxJournal.DataSource = s;
                    cbxJournal.DisplayMember = "Prefix";
                    cbxJournal.ValueMember = "Id";

                    var ss = from d in donnée.tbl_Compte_Comptable where d.Type == null orderby d.Compte select d;
                    cbxCompte.DataSource = ss;
                    cbxCompte.DisplayMember = "Compte";
                    cbxCompte.ValueMember = "Id";
                    cbxCompte.Text = null;
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

        Timer timer = new Timer();

        ///public static string type;

        private void comboBox_DropDown(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            cbo.PreviewKeyDown += new PreviewKeyDownEventHandler(comboBox_PreviewKeyDown);
        }

        private void comboBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            cbo.PreviewKeyDown -= comboBox_PreviewKeyDown;
            if (cbo.DroppedDown) cbo.Focus();
        }

        private void FillCbx()
        {
            try
            {
                using (var donnée = new QuitayeContext())
                {
                    if (cbxJournal.Text.Length > 0)
                    {
                        var journ = (from d in donnée.tbl_list_journaux where d.Prefix == cbxJournal.Text select d).First();

                        if (journ.Type == "Vente")
                        {
                            var s = from d in donnée.tbl_Compte_Comptable where d.Type == journ.Type orderby d.Préfix select d;
                            cbxCompteTier.DataSource = s;
                            cbxCompteTier.DisplayMember = "Préfix";
                            cbxCompteTier.ValueMember = "Id";
                        }
                        else
                        {
                            var s = from d in donnée.tbl_Compte_Comptable where d.Type == "Fournisseur" || d.Type == "Client" orderby d.Préfix select d;
                            cbxCompteTier.DataSource = s;
                            cbxCompteTier.DisplayMember = "Préfix";
                            cbxCompteTier.ValueMember = "Id";
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

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Count > 2)
            {
                try
                {
                    using (var donnée = new QuitayeContext())
                    {
                        var montant = from d in donnée.tbl_journal_comptable where d.Validé == "Non" && d.Auteur == Principales.profile select d;
                        if (Convert.ToDecimal(montant.Sum(x => x.Crédit)) == Convert.ToDecimal(montant.Sum(x => x.Débit)))
                        {
                            SetValidé();
                            FillDG();
                            Alert.SShow("Ecriture validé avec succès.", Alert.AlertType.Sucess);
                        }
                        else
                        {
                            Alert.SShow("Ecriture incomplet, veillez completer !!", Alert.AlertType.Sucess);
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

        private async void SetValidé()
        {
            try
            {
                using (var donnée = new QuitayeContext())
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        var s = (from d in donnée.tbl_journal_comptable where d.Id == Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) select d).First();
                        s.Validé = "Oui";
                        await donnée.SaveChangesAsync();
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

        //String filePath = "";
        public string filename;
        //Byte[] ImageByteArray;
        public int id;
        public string exten;

        public string sq;
        private void FillDG()
        {
            try
            {
                using (var donnée = new QuitayeContext())
                {
                    var s = from d in donnée.tbl_journal_comptable
                            where d.Validé == "Non" && d.Auteur == Principales.profile
                            orderby d.Id ascending
                            select new
                            {
                                Id = d.Id,
                                Date = d.Date,
                                N_Facture = d.N_Facture,
                                Ref_Pièce = d.Ref_Pièces,
                                Compte = d.Compte,
                                Compte_Tier = d.Compte_Tier,
                                Libelle_Ecriture = d.Commentaire,
                                Débit = d.Débit,
                                Crédit = d.Crédit,
                            };
                    if (s.Count() == 0)
                    {
                        dataGridView1.Columns.Clear();
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Tableau vide");
                        DataRow dr = dt.NewRow();
                        dr[0] = "Aucune donnée disponible dans le tableau";
                        dt.Rows.Add(dr);
                        dataGridView1.DataSource = dt;
                        cbxJournal.Enabled = true;
                        lblRef.Text = "Référence Pièce :";

                        lblMontantCrédit.Text = "Crédit : 0";
                        lblMontantDébit.Text = "Débit : 0";
                    }
                    else
                    {
                        dataGridView1.Columns.Clear();
                        dataGridView1.DataSource = s;
                        dataGridView1.Columns[0].Visible = false;
                        cbxJournal.Enabled = false;
                        var se = (from d in donnée.tbl_journal_comptable where d.Validé == "Non" && d.Auteur == Principales.profile select d).First();
                        Date_Operation.Value = Convert.ToDateTime(se.Date);
                        lblRef.Text = "Référence Pièce :" + se.Ref_Pièces;
                        char[] ca = new char[se.Ref_Pièces.Length];
                        ca = se.Ref_Pièces.ToArray();
                        char c = ca[se.Ref_Pièces.Length - 1];
                        string[] arr = se.Ref_Pièces.Split(c);
                        cbxJournal.Text = arr[0];

                        var montant = from d in donnée.tbl_journal_comptable where d.Validé == "Non" && d.Auteur == Principales.profile select d;
                        lblMontantCrédit.Text = "Crédit : " + Convert.ToDecimal(montant.Sum(x => x.Crédit)).ToString("N0");
                        lblMontantDébit.Text = "Débit : " + Convert.ToDecimal(montant.Sum(x => x.Débit)).ToString("N0");

                        EditColumn edit = new EditColumn();
                        edit.HeaderText = "Edit";
                        edit.Name = "Edit";
                        edit.Width = 20;

                        DeleteColumn edit1 = new DeleteColumn();
                        edit1.HeaderText = "Sup";
                        edit1.Name = "Sup";
                        edit1.Width = 20;

                        dataGridView1.Columns.Add(edit);
                        dataGridView1.Columns.Add(edit1);
                        dataGridView1.Columns["Edit"].Width = 20;
                        dataGridView1.Columns["Sup"].Width = 30;
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

        private async void EditData()
        {
            try
            {
                using (var donnée = new QuitayeContext())
                {
                    var s = (from d in donnée.tbl_journal_comptable where d.Id == id select d).First();
                    s.N_Facture = txtFacture.Text;
                    s.Compte_Tier = cbxCompteTier.Text;
                    s.Compte = cbxCompte.Text;
                    s.Date = Date_Operation.Value;
                    if (txtCrédit.Text != "")
                        s.Débit = null;
                    else
                    {
                        string result = Regex.Replace(txtDébit.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                        s.Débit = Convert.ToInt32(result);
                    }

                    if (txtDébit.Text != "")
                        s.Crédit = null;
                    else
                    {
                        string result = Regex.Replace(txtCrédit.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                        s.Crédit = Convert.ToInt32(result);
                    }
                    await donnée.SaveChangesAsync();
                    FillDG();
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

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if ((txtCrédit.Text != "" || txtDébit.Text != "") && cbxCompte.Text != "")
            {
                if(LogIn.expiré == false)
                {
                    if (btnAjouter.IconChar == FontAwesome.Sharp.IconChar.Plus)
                    {
                        InserData();
                    }
                    else
                    {
                        EditData();
                    }
                }
            }
        }

        string pièce;
        private async void InserData()
        {
            try
            {
                using (var donnée = new QuitayeContext())
                {
                    var tbl = new tbl_journal_comptable();

                    //char[] ca = new char[cbxJournal.Text.Length];
                    //ca = cbxJournal.Text.ToArray();
                    //char c = ca[cbxJournal.Text.Length - 1];

                    string prefix = null;

                    prefix = cbxJournal.Text;

                    if (cbxJournal.Text != "")
                    {

                    }
                    else
                    {
                        MsgBox msg = new MsgBox();
                        msg.show("Veillez-ajouter vos différents journaux !!", "Information", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Information);
                        msg.ShowDialog();
                        return;
                    }

                    if (dataGridView1.Columns.Count >= 2)
                    {
                        var bc = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith(prefix) select d;
                        if (bc.Count() > 0)
                        {
                            var bcs = (from d in donnée.tbl_journal_comptable where d.Validé == "Non" && d.Auteur == Principales.profile select d).First();


                            var journ = (from d in donnée.tbl_list_journaux where d.Prefix == cbxJournal.Text select d).First();
                            pièce = bcs.Ref_Pièces;
                            int id = 1;

                            var jj = (from d in donnée.tbl_journal_comptable orderby d.Id descending select d).First();

                            id = jj.Id + 1;

                            var co = (from d in donnée.tbl_Compte_Comptable where d.Compte == cbxCompte.Text select d).First();

                            var journal = new Models.Context.tbl_journal_comptable();
                            journal.Id = id;
                            journal.N_Facture = bcs.N_Facture;
                            journal.Date = Date_Operation.Value;
                            journal.Compte = cbxCompte.Text;
                            journal.Compte_Tier = cbxCompteTier.Text;
                            journal.Commentaire = bcs.Commentaire;
                            if (txtCrédit.Text != "")
                                journal.Débit = null;
                            else
                            {
                                string result = Regex.Replace(txtDébit.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                                journal.Débit = Convert.ToInt32(result);
                            }

                            if (txtDébit.Text != "")
                                journal.Crédit = null;
                            else
                            {
                                string result = Regex.Replace(txtCrédit.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                                journal.Crédit = Convert.ToInt32(result);
                            }
                            journal.Auteur = Principales.profile;
                            journal.Date_Enregistrement = DateTime.Now;
                            journal.Libelle = co.Catégorie;
                            journal.Ref_Pièces = pièce;
                            journal.Type = journ.Type;
                            journal.Validé = "Non";
                            donnée.tbl_journal_comptable.Add(journal);
                            await donnée.SaveChangesAsync();

                            FillDG();
                        }
                    }
                    else
                    {
                        var bc = from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith(prefix) select d;
                        if (bc.Count() > 0)
                        {
                            var bcs = (from d in donnée.tbl_journal_comptable where d.Ref_Pièces.StartsWith(prefix) orderby d.Id descending select d).First();
                            string[] arry = bcs.Ref_Pièces.Split('-');
                            int reference = (Convert.ToInt32(arry[1]) * 1) + 1;

                            pièce = cbxJournal.Text + "-" + reference;
                            int id = 1;
                            var journ = (from d in donnée.tbl_list_journaux where d.Prefix == cbxJournal.Text select d).First();
                            var jj = (from d in donnée.tbl_journal_comptable orderby d.Id descending select d).First();

                            id = jj.Id + 1;

                            var co = (from d in donnée.tbl_Compte_Comptable where d.Compte == cbxCompte.Text select d).First();

                            var journal = new Models.Context.tbl_journal_comptable();
                            journal.Id = id;
                            journal.N_Facture = txtFacture.Text;
                            journal.Date = Date_Operation.Value;
                            journal.Compte = cbxCompte.Text;
                            journal.Compte_Tier = cbxCompteTier.Text;
                            journal.Commentaire = txtCommentaire.Text;
                            if (txtCrédit.Text != "")
                                journal.Débit = null;
                            else
                            {
                                string result = Regex.Replace(txtDébit.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                                journal.Débit = Convert.ToInt32(result);
                            }

                            if (txtDébit.Text != "")
                                journal.Crédit = null;
                            else
                            {
                                string result = Regex.Replace(txtCrédit.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                                journal.Crédit = Convert.ToInt32(result);
                            }
                            journal.Auteur = Principales.profile;
                            journal.Date_Enregistrement = DateTime.Now;
                            journal.Libelle = co.Catégorie;
                            journal.Ref_Pièces = pièce;
                            journal.Validé = "Non";
                            journal.Type = journ.Type;
                            donnée.tbl_journal_comptable.Add(journal);
                            await donnée.SaveChangesAsync();
                            FillDG();
                        }
                        else
                        {
                            var j = from d in donnée.tbl_journal_comptable select d;
                            int id = 1;
                            if (j.Count() != 0)
                            {
                                var jj = (from d in donnée.tbl_journal_comptable orderby d.Id descending select d).First();
                                id = jj.Id + 1;
                            }
                            var journ = (from d in donnée.tbl_list_journaux where d.Prefix == cbxJournal.Text select d).First();
                            pièce = cbxJournal.Text + "-" + 1;
                            var co = (from d in donnée.tbl_Compte_Comptable where d.Compte == cbxCompte.Text select d).First();

                            var journal = new Models.Context.tbl_journal_comptable();
                            journal.Id = id;
                            journal.N_Facture = txtFacture.Text;
                            journal.Date = Date_Operation.Value;
                            journal.Date_Enregistrement = DateTime.Now;
                            journal.Compte = cbxCompte.Text;
                            journal.Compte_Tier = cbxCompteTier.Text;
                            journal.Commentaire = txtCommentaire.Text;
                            if (txtCrédit.Text != "")
                                journal.Débit = null;
                            else
                            {
                                string result = Regex.Replace(txtDébit.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                                journal.Débit = Convert.ToInt32(result);
                            }

                            if (txtDébit.Text != "")
                                journal.Crédit = null;
                            else
                            {
                                string result = Regex.Replace(txtCrédit.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                                journal.Crédit = Convert.ToInt32(result);
                            }
                            journal.Auteur = Principales.profile;
                            journal.Date_Enregistrement = DateTime.Now;
                            journal.Libelle = co.Catégorie;
                            journal.Ref_Pièces = pièce;
                            journal.Validé = "Non";
                            journal.Type = journ.Type;
                            donnée.tbl_journal_comptable.Add(journal);
                            await donnée.SaveChangesAsync();

                            FillDG();
                        }
                    }
                    ClearData();
                    lblRef.Text = "Référence Pièce :" + pièce;
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

        private void ClearData()
        {
            txtCommentaire.Clear();
            cbxCompte.Text = null;
            txtCrédit.Clear();
            txtDébit.Clear();
            cbxCompteTier.Text = null;
        }



        private void txtDébit_TextChanged(object sender, EventArgs e)
        {
            txtCrédit.Clear();
            if (txtDébit.Text != "")
            {
                txtDébit.Text = Convert.ToDecimal(txtDébit.Text).ToString("N0");
                txtDébit.SelectionStart = txtDébit.Text.Length;
            }
        }

        private void txtCrédit_TextChanged(object sender, EventArgs e)
        {
            txtDébit.Clear();
            if (txtCrédit.Text != "")
            {
                txtCrédit.Text = Convert.ToDecimal(txtCrédit.Text).ToString("N0");
                txtCrédit.SelectionStart = txtCrédit.Text.Length;
            }
        }
        //int id;

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                try
                {
                    id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    using (var donnée = new QuitayeContext())
                    {
                        var s = (from d in donnée.tbl_journal_comptable where d.Id == id select d).First();
                        txtCommentaire.Text = s.Commentaire;
                        cbxCompte.Text = s.Compte;
                        cbxCompteTier.Text = s.Compte_Tier;
                        txtCrédit.Text = s.Crédit.ToString();
                        txtDébit.Text = s.Débit.ToString();
                        txtFacture.Text = s.N_Facture;
                        Date_Operation.Value = s.Date.Value;
                        btnAjouter.IconChar = FontAwesome.Sharp.IconChar.Edit;
                        btnAjouter.Text = "Modifier";
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
            else if (e.ColumnIndex == 10)
            {
                try
                {
                    using (var donnée = new QuitayeContext())
                    {
                        MsgBox msg = new MsgBox();
                        msg.show("Voulez-vous supprimer cet élément ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                        msg.ShowDialog();
                        if (msg.clicked == "Non")
                            return;
                        else if (msg.clicked == "Oui")
                        {
                            var s = (from d in donnée.tbl_journal_comptable where d.Id == Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value) select d).First();
                            donnée.tbl_journal_comptable.Remove(s);
                            await donnée.SaveChangesAsync();
                            Alert.SShow("Ecriture supprimée avec succès.", Alert.AlertType.Sucess);
                            FillDG();
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

        private void txtDébit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46 && e.KeyChar != 45)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                btnAjouter_Click(null, e);
            }
        }

        private void txtCompteTier_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCompteTier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Tab))
            {
                using (var donnée = new QuitayeContext())
                {
                    var s = from d in donnée.tbl_Compte_Comptable where d.Type != null && d.Préfix == cbxCompteTier.Text select d;
                    if (s.Count() != 0)
                    {

                    }
                }
            }
        }

        string compte = "";
        private void txtCompte_TextChanged(object sender, EventArgs e)
        {
            if (cbxCompte.Text.Length > 0)
            {
                try
                {
                    using (var donnée = new QuitayeContext())
                    {
                        var s = from d in donnée.tbl_Compte_Comptable where d.Compte == cbxCompte.Text select d;
                        if (s.Count() > 0)
                        {
                            var ss = (from d in donnée.tbl_Compte_Comptable where d.Compte == cbxCompte.Text select d).First();
                            lblcomptegénéral.Text = "Compte : " + ss.Catégorie;
                            compte = ss.Catégorie;
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
            else lblcomptegénéral.Text = "Compte : ";
        }

        private void cbxCompteTier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (var donnée = new QuitayeContext())
                {
                    var s = from d in donnée.tbl_Compte_Comptable where d.Préfix == cbxCompteTier.Text select d;
                    if (s.Count() > 0)
                    {
                        var ss = (from d in donnée.tbl_Compte_Comptable where d.Préfix == cbxCompteTier.Text select d).First();
                        cbxCompte.Text = ss.Compte;
                    }
                    //else txtCompte.Clear();
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

        private void cbxJournal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Count < 2)
            {
                try
                {
                    using (var donnée = new QuitayeContext())
                    {
                        var s = from d in donnée.tbl_journal_comptable where d.Validé == "Non" && d.Ref_Pièces.StartsWith(cbxJournal.Text) select d;
                        if (s.Count() != 0)
                        {
                            cbxJournal.Text = null;
                            Alert.SShow("Cet journal est en cours d'utilisation !", Alert.AlertType.Info);
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

        private void cbxCompteTier_KeyDown(object sender, KeyEventArgs e)
        {
            if ((sender as ComboBox).DroppedDown)
                (sender as ComboBox).DroppedDown = false;
        }

        private void txtCommentaire_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (dataGridView1.Columns.Count > 2)
                {
                    decimal montant = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        montant += Convert.ToDecimal(dataGridView1.Rows[i].Cells[7].Value);
                    }
                    txtCrédit.Focus();
                    txtCrédit.Text = montant.ToString("N0");
                }
            }
        }

        private void txtCommentaire_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblcomptegénéral_Click(object sender, EventArgs e)
        {
            if (txtCommentaire.Focus() == true)
            {
                txtCommentaire.Text = compte;
            }
            else if (txtFacture.Focus() == true)
            {
                txtFacture.Text = compte;
            }
        }

        private void txtCompte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void btnAjouterCatégorie_Click(object sender, EventArgs e)
        {
            Compte_Comptable compte = new Compte_Comptable();
            compte.ShowDialog();
            if (Compte_Comptable.ok == "Oui")
            {
                SetSql();
            }
        }
    }
}
