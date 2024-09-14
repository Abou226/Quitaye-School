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
    public partial class Rapport_Balance_Général : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Rapport_Balance_Général()
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

        //private string[] SetDate()
        //{
        //    ts = EndDate.Value.Date - startDate.Value.Date;
        //    using (var donnée = new DonnéePatissDataContext(mycontrng))
        //    {
        //        List<string> k = donnée.tbl_journal_comptable.Where(x => x.Type != null && x.Type == "Achat" || x.Type == "Vente" && x.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
        //                   && x.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)).OrderBy(x => x.Compte).Select(x => x.Compte).Distinct().ToList();
        //        return k.ToArray();

        //    }
        //}


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                    var historique = new Historique_Compte_Tier("Compte : " + s.Compte + " " + s.Catégorie, s.Compte, startDate.Value, EndDate.Value, "Compte");
                    historique.Text = "Compte Général";
                    historique.ShowDialog();
                }
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
            //coe = SetDate();
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



        public DataTable FilldataJournal()
        {
            using (var donnée = new QuitayeContext())
            {
                // Convert the Linq result into DataTable
                DataTable dt = new DataTable("tbl_inventaire");

                dt.Columns.Add("N° Compte");
                dt.Columns.Add("Intitulé Compte");
                dt.Columns.Add("Mouvement Débit");
                dt.Columns.Add("Mouvement Crédit");
                dt.Columns.Add("Solde Débiteur");
                dt.Columns.Add("Solde Créditeur");

                decimal solde = 0;


                var achat = from d in donnée.tbl_journal_comptable
                            where d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                            && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                            orderby d.Date ascending
                            group d by new
                            {
                                Compte = d.Compte,
                            } into gr
                            select new
                            {
                                Compte = gr.Key.Compte,
                                Débit = gr.Sum(x => x.Débit),
                                Crédit = gr.Sum(x => x.Crédit),
                            };


                foreach (var r in achat)
                {
                    decimal debit = 0;
                    decimal credit = 0;
                    var compte = (from d in donnée.tbl_Compte_Comptable where d.Compte == r.Compte select d).First();
                    DataRow row = dt.NewRow();
                    row[0] = compte.Compte;
                    row[1] = compte.Catégorie;
                    solde += Convert.ToDecimal(r.Débit) - Convert.ToDecimal(r.Crédit);
                    debit = Convert.ToDecimal(r.Débit);
                    credit = Convert.ToDecimal(r.Crédit);

                    if (debit > 0)
                    {
                        row[2] = debit.ToString("N0");
                    }

                    if (credit > 0)
                    {
                        row[3] = credit.ToString("N0");
                    }


                    if (debit > credit)
                    {
                        row[4] = (debit - credit).ToString("N0");

                        row[5] = "";
                    }
                    else if (credit > debit)
                    {
                        row[4] = "";
                        row[5] = (credit - debit).ToString("N0");
                    }
                    else
                    {
                        row[4] = "";
                        row[5] = "";
                    }

                    dt.Rows.Add(row);
                }
                string[] bl = { "1", "2", "3", "4", "5" };
                var bilan = from d in donnée.tbl_journal_comptable
                            where d.Type != null && (d.Compte.StartsWith("1") || d.Compte.StartsWith("2") || d.Compte.StartsWith("3") || d.Compte.StartsWith("4") || d.Compte.StartsWith("5")) && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                            && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                            select d;

                var gestion = from d in donnée.tbl_journal_comptable
                              where d.Type != null && (d.Compte.StartsWith("6") || d.Compte.StartsWith("7") || d.Compte.StartsWith("8") || d.Compte.StartsWith("9")) && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                            && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                              select d;
                decimal dbl = 0;
                decimal cbl = 0;
                dbl = Convert.ToDecimal(bilan.Sum(x => x.Débit));
                cbl = Convert.ToDecimal(bilan.Sum(x => x.Crédit));

                DataRow de = dt.NewRow();
                dt.Rows.Add(de);
                DataRow dre = dt.NewRow();
                dre[1] = "Total Compte Bilan";
                dre[2] = dbl.ToString("N0");
                dre[3] = cbl.ToString("N0");
                if (dbl > cbl)
                {
                    dre[4] = (dbl - cbl).ToString("N0");

                    dre[5] = "";
                }
                else if (cbl > dbl)
                {
                    dre[4] = "";
                    dre[5] = (cbl - dbl).ToString("N0");
                }
                else
                {
                    dre[4] = "";
                    dre[5] = "";
                }
                dt.Rows.Add(dre);

                decimal dges = 0;
                decimal cges = 0;
                dges = Convert.ToDecimal(gestion.Sum(x => x.Débit));
                cges = Convert.ToDecimal(gestion.Sum(x => x.Crédit));

                DataRow dres = dt.NewRow();
                dres[1] = "Total Compte Gestion";
                dres[2] = dges.ToString("N0");
                dres[3] = cges.ToString("N0");
                if (dges > cges)
                {
                    dres[4] = Convert.ToDecimal(dges - cges).ToString("N0");

                    dres[5] = "";
                }
                else if (cges > dges)
                {
                    dres[4] = "";
                    dres[5] = Convert.ToDecimal(cges - dges).ToString("N0");
                }
                else
                {
                    dres[4] = "";
                    dres[5] = "";
                }
                dt.Rows.Add(dres);

                DataRow dress = dt.NewRow();
                dress[1] = "Resultat Balance";
                dress[2] = Convert.ToDecimal(dbl + dges).ToString("N0");
                dress[3] = Convert.ToDecimal(cbl + cges).ToString("N0");
                if (dges + dbl > cges + cbl)
                {
                    dress[4] = Convert.ToDecimal((dges + dbl) - (cbl + cges)).ToString("N0");

                    dress[5] = "";
                }
                else if (cges + cbl > dges + dbl)
                {
                    dress[4] = "";
                    dress[5] = Convert.ToDecimal((cges + cbl) - (dbl + dges)).ToString("N0");
                }
                else
                {
                    dress[4] = "";
                    dress[5] = "";
                }
                dt.Rows.Add(dress);


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
