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
    public partial class Rapport_Compte_de_Resultat : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Rapport_Compte_de_Resultat()
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



        decimal débit = 0;
        decimal crédit = 0;

        private async void CallFillJournal(DataGridView dgv)
        {

            ts = EndDate.Value.Date - startDate.Value.Date;
            var result = await FillDGAsyncJournal();
            dgv.Columns.Clear();
            dgv.DataSource = result;
        }
        public Task<DataTable> FillDGAsyncJournal()
        {
            return Task.Factory.StartNew(() => FilldataJournal());
        }



        //private IEnumerable<string> getLatestReadings(string[] brands, string[] categories)
        //{
        //    //var donnée = new DonnéePatissDataContext(mycontrng);

        //    //var products = donnée.tbl_journal_comptable.AsQueryable();
        //    //if (brands.Any())
        //    //    products = products.Where(product => brands.Contains(product.Compte));

        //    //// Now the main query (just use products instead of context.Products)
        //    //return
        //    //    select product;

        //    //return from d in donnée.tbl_journal_comptable where brands.Contains(d.Compte) 
        //    //       select new 
        //    //       {
        //    //            Compte = d.Compte
        //    //       };
        //}



        public DataTable FilldataJournal()
        {
            using (var donnée = new QuitayeContext())
            {
                // Convert the Linq result into DataTable
                DataTable dt = new DataTable("tbl_inventaire");
                dt.Columns.Add("Ref");
                dt.Columns.Add("Designation");
                dt.Columns.Add("Année N");
                dt.Columns.Add("Année N-1");



                decimal totalProduit1 = 0;
                decimal totalProduit2 = 0;
                decimal totalcharge1 = 0;
                decimal totalcharge2 = 0;
                for (int i = 0; i < 41; i++)
                {

                    if (i == 0)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where c.Compte.StartsWith("701") && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));
                        }

                        DataRow row = dt.NewRow();

                        row[0] = "TA";

                        row[1] = "Vente de marchandises"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 1)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("702") || c.Compte.StartsWith("703") || c.Compte.StartsWith("704")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }
                        DataRow row = dt.NewRow();
                        row[0] = "TC";

                        row[1] = "Vente de produit fabriqués"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 2)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("705") || c.Compte.StartsWith("706")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }
                        DataRow row = dt.NewRow();
                        row[0] = "TD";

                        row[1] = "Traveaux, Services vendues"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 3)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("703")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }
                        DataRow row = dt.NewRow();
                        row[0] = "TE";

                        row[1] = "Production Stockée"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 4)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("702")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "TF";

                        row[1] = "Production Immobilisée"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }
                    if (i == 5)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("707")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "TH";

                        row[1] = "Produits Accessoires"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 6)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("71")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "TK";

                        row[1] = "Subvention d'exploitation"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 7)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("75")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "TL";

                        row[1] = "Autres produits"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 8)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("791") || c.Compte.StartsWith("798")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "TS";

                        row[1] = "Reprises de provisions"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 9)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("781")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "TT";

                        row[1] = "Transfère de charges"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 10)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("77") && !c.Compte.StartsWith("776")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "UA";

                        row[1] = "Revenus financiers"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }
                    if (i == 11)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("776")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "UC";

                        row[1] = "Gains de change"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 12)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("797")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "UD";

                        row[1] = "Reprises de provisions"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 13)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("787")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "UE";

                        row[1] = "Transfère de charges"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 14)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("82")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "UK";

                        row[1] = "Produits des cessions d'immobilisations"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 15)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where ((c.Compte.StartsWith("84") || c.Compte.StartsWith("88")) && !c.Compte.StartsWith("848")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "UL";

                        row[1] = "Produist H.A.O"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 16)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("86")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "UM";

                        row[1] = "Reprises H.A.O"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 17)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("848")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalProduit1 += totaldebit;
                            totalProduit2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "UN";

                        row[1] = "Transfère de charges"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 18)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("601")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "RA";

                        row[1] = "Achat de marchandises"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 19)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("6031")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "RB";

                        row[1] = "Variation Stocks Marchandises"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 20)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("602")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "RC";

                        row[1] = "Achat de matière premières et fournitures liées"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 21)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("6032")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "RD";

                        row[1] = "Variation de stocks matières premières"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 22)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("604") || c.Compte.StartsWith("605") || c.Compte.StartsWith("608")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "RE";

                        row[1] = "Autres achats"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 23)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("6033")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "RH";

                        row[1] = "Autres variations de stocks"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 24)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("61")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "RI";

                        row[1] = "Transports"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 25)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("62") || c.Compte.StartsWith("63")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "RJ";

                        row[1] = "Services extérieurs"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 26)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("64")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "RK";

                        row[1] = "Impôt et taxes"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 27)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("65")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "RL";

                        row[1] = "Autres charges"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 28)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("66")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "RP";

                        row[1] = "Charges de personnel"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 29)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("681") || c.Compte.StartsWith("691")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "RS";

                        row[1] = "Dotations aux amortissements et aux provisions"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 30)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("67") && !c.Compte.StartsWith("676")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "SA";

                        row[1] = "Frais Financiers"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 31)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("676")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "SC";

                        row[1] = "Perte de change"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 32)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("687") || c.Compte.StartsWith("697")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "SD";

                        row[1] = "Dotations aux amortissements et aux provisions"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 33)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("81")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "SK";

                        row[1] = "Valeur comptables des cessions d'immobilisations"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 34)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("83")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "SL";

                        row[1] = "Chages H.A.O"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 35)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("85")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "SM";

                        row[1] = "Dotations H.A.O"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 36)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("87")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "SQ";

                        row[1] = "Participations des travailleurs"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }

                    if (i == 37)
                    {
                        string[] all = (from c in donnée.tbl_journal_comptable
                                        where (c.Compte.StartsWith("89")) && c.Type != null && c.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                   && c.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                        select c.Compte).Distinct().ToArray();
                        decimal totaldebit = 0;
                        decimal totalcredit = 0;
                        for (int j = 0; j < all.Length; j++)
                        {
                            var ere = from d in donnée.tbl_journal_comptable
                                      where d.Compte == all[j] && d.Type != null && d.Date.Value.Date >= EndDate.Value.Date.AddDays(-ts.Days)
                                       && d.Date.Value.Date <= startDate.Value.Date.AddDays(ts.Days)
                                      select d;
                            var eres = from d in donnée.tbl_journal_comptable
                                       where d.Compte == all[j] && d.Type != null && d.Date.Value.Year >= EndDate.Value.Year - 1
                                        && d.Date.Value.Year <= startDate.Value.Year - 1
                                       select d;
                            totaldebit += Convert.ToDecimal(ere.Sum(x => x.Débit));
                            totalcredit += Convert.ToDecimal(eres.Sum(x => x.Débit));

                            totalcharge1 += totaldebit;
                            totalcharge2 += totalcredit;
                        }

                        DataRow row = dt.NewRow();
                        row[0] = "SR";

                        row[1] = "Impôt sur le résultat"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                        row[2] = totaldebit.ToString("N0");

                        row[3] = totalcredit.ToString("N0");
                        dt.Rows.Add(row);
                    }



                }

                DataRow r = dt.NewRow();
                dt.Rows.Add(r);

                DataRow rowa = dt.NewRow();
                rowa[0] = "";

                rowa[1] = "Resultat d'exploitation"; //achat.Select(x => x.Ref.First()) + " "+ vente.Select(x => x.Ref.First());

                rowa[2] = (totalProduit1 - totalcharge1).ToString("N0");

                rowa[3] = (totalProduit2 - totalcharge2).ToString("N0");
                dt.Rows.Add(rowa);

                //DataRow rowas = dt.NewRow();
                //rowas[0] = "";
                //rowas[1] = "Resultat d'exploitation";
                //rowas[2] = (totalProduit1 - totalcharge1).ToString("N0");
                //rowas[3] = (totalProduit2 - totalcharge2).ToString("N0");
                //dt.Rows.Add(rowas);

                // Databind
                return dt;
            }
        }

    }
}
