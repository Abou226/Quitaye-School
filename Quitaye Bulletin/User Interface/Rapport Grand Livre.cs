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
    public partial class Rapport_Grand_Livre : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Rapport_Grand_Livre()
        {
            InitializeComponent();
            string year = DateTime.Today.Year.ToString();
            DateTime date = Convert.ToDateTime("01/01/" + year);
            startDate.Value = date;
            EndDate.Value = Convert.ToDateTime("31/12/" + year);

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
                CallFillJournal(dataGridView1);
                temp = 0;
            }
        }

        Timer timer = new Timer();


        TimeSpan ts;

        private string[] SetDate()
        {
            ts = EndDate.Value.Date - startDate.Value.Date;
            using (var donnée = new QuitayeContext())
            {
                List<string> k = donnée.tbl_journal_comptable.Where(x => x.Type != null && x.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                           && x.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)).OrderBy(x => x.Compte).Select(x => x.Compte).Distinct().ToList();
                return k.ToArray();
            }
        }


        private void FillData()
        {
            using (var donnée = new QuitayeContext())
            {
                //var compte = (from d in donnée.tbl_list_journaux where d.Prefix == cbxJournal.Text select d).First();
                //string co = compte.Compte;
                //if (co != null)
                //{
                //    var sese = from d in donnée.tbl_journal_comptable
                //               where d.Compte == co && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                //               && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                //               orderby d.Id descending
                //               select new
                //               {

                //                   Auteur = d.Auteur,
                //                   Ref = d.Ref_Pièces,
                //                   Débit = d.Débit,
                //                   Crédit = d.Crédit,
                //               };

                //    débit = Convert.ToDecimal(sese.Sum(x => x.Débit));
                //    crédit = Convert.ToDecimal(sese.Sum(x => x.Crédit));
                //    lblDébit.Text = "Débit : " + débit.ToString("N0");
                //    lblCrédit.Text = "Crédit : " + crédit.ToString("N0");
                //    lblCrédit.Visible = true;
                //    lblDébit.Visible = true;
                //}
                //else
                {
                    lblCrédit.Visible = false;
                    lblDébit.Visible = false;
                    lblSoldeAntérieur.Visible = false;
                }
            }
        }

        private async void CallFillJournal(DataGridView dgv)
        {
            coe = SetDate();
            ts = EndDate.Value.Date - startDate.Value.Date;
            var result = await FillDGAsyncJournal();
            dgv.Columns.Clear();
            dgv.DataSource = result;
            FillData();
        }
        public Task<DataTable> FillDGAsyncJournal()
        {
            return Task.Factory.StartNew(() => FilldataJournal());
        }

        decimal débit = 0;
        decimal crédit = 0;

        string[] coe;

        public DataTable FilldataJournal()
        {
            using (var donnée = new QuitayeContext())
            {
                // Convert the Linq result into DataTable
                DataTable dt = new DataTable("tbl_inventaire");

                dt.Columns.Add("Date");
                dt.Columns.Add("Ref_Pièce");
                dt.Columns.Add("Libelle_Ecriture");
                dt.Columns.Add("Débit");
                dt.Columns.Add("Crédit");
                dt.Columns.Add("Solde Progressif");

                decimal solde = 0;

                for (int i = 0; i < coe.Length; i++)
                {
                    var achat = from d in donnée.tbl_journal_comptable
                                where d.Compte == coe[i] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                orderby d.Date ascending
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

                    var compte = (from d in donnée.tbl_Compte_Comptable where d.Compte == coe[i] select d).First();

                    DataRow data = dt.NewRow();
                    data[0] = "";
                    dt.Rows.Add(data);
                    DataRow dr = dt.NewRow();
                    dr[0] = compte.Compte + " " + compte.Catégorie;
                    dt.Rows.Add(dr);

                    foreach (var r in achat)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = r.Date.Value.Date.ToString("dd/MM/yyyy");
                        solde += Convert.ToDecimal(r.Débit) - Convert.ToDecimal(r.Crédit);
                        row[1] = r.Ref_Pièce; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = r.Libélle;
                        if (r.Débit != null)
                            row[3] = Convert.ToDecimal(r.Débit).ToString("N0");

                        if (r.Crédit != null)
                            row[4] = Convert.ToDecimal(r.Crédit).ToString("N0"); //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[5] = solde.ToString("N0");

                        dt.Rows.Add(row);
                    }

                    DataRow dre = dt.NewRow();
                    dre[2] = "Total ";
                    if (Convert.ToDecimal(achat.Sum(x => x.Débit)) > 0)
                        dre[3] = Convert.ToDecimal(achat.Sum(x => x.Débit)).ToString("N0");
                    if (Convert.ToDecimal(achat.Sum(x => x.Crédit)) > 0)
                        dre[4] = Convert.ToDecimal(achat.Sum(x => x.Crédit)).ToString("N0");
                    dre[5] = (Convert.ToDecimal(achat.Sum(x => x.Débit)) - Convert.ToDecimal(achat.Sum(x => x.Crédit))).ToString("N0");
                    dt.Rows.Add(dre);
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
