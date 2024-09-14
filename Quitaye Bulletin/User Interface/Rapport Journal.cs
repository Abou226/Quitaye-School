using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Rapport_Journal : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Rapport_Journal()
        {
            InitializeComponent();
            string year = DateTime.Today.Year.ToString();
            DateTime date = Convert.ToDateTime("01/01/" + year);
            startDate.Value = date;
            EndDate.Value = Convert.ToDateTime("31/12/" + year);
            FillCbx();
            temp = 1;
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        int temp = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (temp == 1)
            {
                FillCbx();
                if (cbxJournal.Text.Length > 0)
                    CallFillJournal(dataGridView1, cbxJournal.Text);
                temp = 0;
            }
        }

        Timer timer = new Timer();
        private void FillCbx()
        {
            try
            {
                using (var donnée = new QuitayeContext())
                {
                    var s = from d in donnée.tbl_list_journaux orderby d.Prefix select d;
                    cbxJournal.DataSource = s;
                    cbxJournal.DisplayMember = "Prefix";
                    cbxJournal.ValueMember = "Id";
                }
            }
            catch (Exception ex)
            {
                var w32ex = ex as SqlException;
                if (w32ex == null)
                {
                    w32ex = ex.InnerException as SqlException;
                    int code = w32ex.ErrorCode;
                }
                if (w32ex != null)
                {
                    int code = w32ex.ErrorCode;
                    // do stuff

                    if (code == -2146232060)
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

        TimeSpan ts;

        private string[] SetDate()
        {
            ts = EndDate.Value.Date - startDate.Value.Date;
            using (var donnée = new QuitayeContext())
            {
                return donnée.tbl_journal_comptable.Where(d => (d.Type == "Achat" || d.Type == "Vente") && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                           && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)).Select(x => x.Compte).OrderBy(x => x).ToArray();


            }
        }

        private void cbxJournal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxJournal.Text.Length > 0)
            {
                CallFillJournal(dataGridView1, cbxJournal.Text);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    if (dataGridView1.CurrentRow.Cells[5].Value.ToString() != "")
                    {
                        string compte = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                        Historique_Compte_Tier historique = new Historique_Compte_Tier("Compte Tier : " + compte, compte, startDate.Value, EndDate.Value, "Compte Tier");
                        historique.Text = "Compte Tier";
                        historique.ShowDialog();
                    }
                }
                else if (e.ColumnIndex == 4)
                {

                    using (var donnée = new QuitayeContext())
                    {
                        var s = (from d in donnée.tbl_Compte_Comptable where d.Compte == dataGridView1.CurrentRow.Cells[4].Value.ToString() select d).First();
                        Historique_Compte_Tier historique = new Historique_Compte_Tier("Compte : " + s.Compte + " " + s.Catégorie, s.Compte, startDate.Value, EndDate.Value, "Compte");
                        historique.Text = "Compte Général";
                        historique.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                var w32ex = ex as SqlException;
                if (w32ex == null)
                {
                    w32ex = ex.InnerException as SqlException;
                    int code = w32ex.ErrorCode;
                }
                if (w32ex != null)
                {
                    int code = w32ex.ErrorCode;
                    // do stuff

                    if (code == -2146232060)
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

        private void FillData()
        {
            try
            {
                using (var donnée = new QuitayeContext())
                {
                    var compte = (from d in donnée.tbl_list_journaux where d.Prefix == cbxJournal.Text select d).First();
                    string co = compte.Compte;
                    if (co != null)
                    {
                        var sese = from d in donnée.tbl_journal_comptable
                                   where d.Compte == co && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                   orderby d.Id descending
                                   select new
                                   {

                                       Auteur = d.Auteur,
                                       Ref = d.Ref_Pièces,
                                       Débit = d.Débit,
                                       Crédit = d.Crédit,
                                   };

                        débit = Convert.ToDecimal(sese.Sum(x => x.Débit));
                        crédit = Convert.ToDecimal(sese.Sum(x => x.Crédit));
                        lblDébit.Text = "Débit : " + débit.ToString("N0");
                        lblCrédit.Text = "Crédit : " + crédit.ToString("N0");
                        lblCrédit.Visible = true;
                        lblDébit.Visible = true;
                    }
                    else
                    {
                        lblCrédit.Visible = false;
                        lblDébit.Visible = false;
                        lblSoldeAntérieur.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                var w32ex = ex as SqlException;
                if (w32ex == null)
                {
                    w32ex = ex.InnerException as SqlException;
                    int code = w32ex.ErrorCode;
                }
                if (w32ex != null)
                {
                    int code = w32ex.ErrorCode;
                    // do stuff

                    if (code == -2146232060)
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

        private async void CallFillJournal(DataGridView dgv, string days)
        {
            ts = EndDate.Value.Date - startDate.Value.Date;
            var result = await FillDGAsyncJournal(days);
            dgv.Columns.Clear();
            dgv.DataSource = result;
            FillData();
        }
        public Task<DataTable> FillDGAsyncJournal(string days)
        {
            return Task.Factory.StartNew(() => FilldataJournal(days));
        }

        decimal débit = 0;
        decimal crédit = 0;

        string[] coe;

        public DataTable FilldataJournal(string code)
        {
            using (var donnée = new QuitayeContext())
            {
                // Convert the Linq result into DataTable
                DataTable dt = new DataTable("tbl_inventaire");
                dt.Columns.Add("Id");
                dt.Columns.Add("Date");
                dt.Columns.Add("N_Facture");
                dt.Columns.Add("Ref_Pièce");
                dt.Columns.Add("Compte");
                dt.Columns.Add("Compte_Tier");
                dt.Columns.Add("Libelle_Ecriture");
                dt.Columns.Add("Débit");
                dt.Columns.Add("Crédit");

                try
                {
                    var achat = from d in donnée.tbl_journal_comptable
                                where d.Ref_Pièces.StartsWith(code) && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                orderby d.Id descending
                                select new
                                {
                                    Id = d.Id,
                                    Date = d.Date,
                                    N_Facture = d.N_Facture,
                                    Ref_Pièce = d.Ref_Pièces,
                                    Compte = d.Compte,
                                    Compte_Tier = d.Compte_Tier,
                                    Libélle = d.Commentaire,
                                    Débit = d.Débit,
                                    Crédit = d.Crédit,
                                };

                    foreach (var r in achat)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = r.Id;

                        row[1] = r.Date; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = r.N_Facture;

                        row[3] = r.Ref_Pièce;

                        row[4] = r.Compte; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[5] = r.Compte_Tier;

                        row[6] = r.Libélle;//achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[7] = r.Débit;

                        row[8] = r.Crédit;

                        dt.Rows.Add(row);
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

                // Databind
                return dt;
            }
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            temp = 1;
        }
    }
}
