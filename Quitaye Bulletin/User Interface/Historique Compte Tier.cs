using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Historique_Compte_Tier : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Historique_Compte_Tier(string compte, string compe, DateTime startdate, DateTime enddate, string type)
        {
            InitializeComponent();
            comp = compe;
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
            temp = 1;
            startDate.Value = startdate;
            EndDate.Value = enddate;
            typ = type;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            lblCompteTier.Text = compte;
        }
        string typ;
        int temp = 0;

        protected override void WndProc(ref Message m)
        {

            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (temp == 1)
            {
                if (typ == "Compte")
                {
                    CallFillJournalCompte(dataGridView1, comp);
                    FillDataCompte(comp);
                }
                else
                {
                    CallFillJournal(dataGridView1, comp);
                    FillData(comp);
                }

                temp = 0;
            }
        }

        Timer timer = new Timer();


        string comp = null;

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        TimeSpan ts;

        private void FillData(string code)
        {
            using (var donnée = new QuitayeContext())
            {
                var sese = from d in donnée.tbl_journal_comptable
                           where d.Compte_Tier.Contains(code) && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                           && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days) && d.Type != null
                           select d;

                débit = Convert.ToDecimal(sese.Sum(x => x.Débit));
                crédit = Convert.ToDecimal(sese.Sum(x => x.Crédit));
                if (débit == crédit)
                    lblSolde.Text = "Compte Soldé : 0";
                else if (débit > crédit)
                    lblSolde.Text = "Solde Débiteur";
                else if (crédit > débit)
                    lblSolde.Text = "Solde Créditeur";
                lblSolde.Visible = true;
                lblDébit.Text = "Débit : " + débit.ToString("N0");
                lblCrédit.Text = "Crédit : " + crédit.ToString("N0");
                lblCrédit.Visible = true;
                lblDébit.Visible = true;
            }
        }

        private void FillDataCompte(string code)
        {
            using (var donnée = new QuitayeContext())
            {
                var sese = from d in donnée.tbl_journal_comptable
                           where d.Compte.Contains(code) && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                           && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days) && d.Type != null
                           select d;

                débit = Convert.ToDecimal(sese.Sum(x => x.Débit));
                crédit = Convert.ToDecimal(sese.Sum(x => x.Crédit));
                if (débit == crédit)
                    lblSolde.Text = "Compte Soldé : 0";
                else if (débit > crédit)
                    lblSolde.Text = "Solde Débiteur";
                else if (crédit > débit)
                    lblSolde.Text = "Solde Créditeur";
                lblSolde.Visible = true;
                lblDébit.Text = "Débit : " + débit.ToString("N0");
                lblCrédit.Text = "Crédit : " + crédit.ToString("N0");
                lblCrédit.Visible = true;
                lblDébit.Visible = true;
            }
        }

        private async void CallFillJournal(DataGridView dgv, string days)
        {
            ts = EndDate.Value.Date - startDate.Value.Date;
            var result = await FillDGAsyncJournal(days);
            dgv.Columns.Clear();
            dgv.DataSource = result;
            dgv.Columns[0].Visible = false;
        }
        public Task<DataTable> FillDGAsyncJournal(string days)
        {
            return Task.Factory.StartNew(() => FilldataJournal(days));
        }

        decimal débit = 0;
        decimal crédit = 0;


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



                var achat = from d in donnée.tbl_journal_comptable
                            where d.Compte_Tier.Contains(code) && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
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




                // Databind
                return dt;
            }
        }

        private async void CallFillJournalCompte(DataGridView dgv, string days)
        {
            ts = EndDate.Value.Date - startDate.Value.Date;
            var result = await FillDGAsyncJournalCompte(days);
            dgv.Columns.Clear();
            dgv.DataSource = result;
            dgv.Columns[0].Visible = false;
        }
        public Task<DataTable> FillDGAsyncJournalCompte(string days)
        {
            return Task.Factory.StartNew(() => FilldataJournalCompte(days));
        }

        public DataTable FilldataJournalCompte(string code)
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



                var achat = from d in donnée.tbl_journal_comptable
                            where d.Compte.Contains(code) && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
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




                // Databind
                return dt;
            }
        }

        private void EndDate_ValueChanged(object sender, EventArgs e)
        {
            temp = 1;
        }
    }
}
