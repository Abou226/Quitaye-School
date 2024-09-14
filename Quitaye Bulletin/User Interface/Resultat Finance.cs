using PrintAction;
using Quitaye_School.Models.Context;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Resultat_Finance : Form
    {
        private string mycontrng = LogIn.mycontrng;
        private Timer loadTimer = new Timer();
        private string name;
        private Timer timer = new Timer();
        private TimeSpan ts;
        public Resultat_Finance()
        {
            InitializeComponent();
            startDate.Value = DateTime.Today.AddDays(-30.0);
            DateTime dateTime = EndDate.Value;
            DateTime date1 = dateTime.Date;
            dateTime = startDate.Value;
            DateTime date2 = dateTime.Date;
            ts = date1 - date2;
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            btnExcel.Click += btnExcel_Click;
            btnPdf.Click += btnPdf_Click;
            EndDate.ValueChanged += EndDate_ValueChanged;
            startDate.ValueChanged += EndDate_ValueChanged;
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            DateTime dateTime = EndDate.Value;
            DateTime date1 = dateTime.Date;
            dateTime = startDate.Value;
            DateTime date2 = dateTime.Date;
            ts = date1 - date2;
            await CallFill(ts.Days);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            string[] strArray = new string[6];
            strArray[0] = "Resultat Financier ";
            DateTime date1 = startDate.Value;
            date1 = date1.Date;
            strArray[1] = date1.ToString("dd-MM-yyyy");
            strArray[2] = " ";
            DateTime date2 = EndDate.Value;
            date2 = date2.Date;
            strArray[3] = date2.ToString("dd-MM-yyyy");
            strArray[4] = " ";
            strArray[5] = DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            name = string.Concat(strArray);
        }

        public void SetData(int days)
        {
            using (var ecoleDataContext = new QuitayeContext())
            {
                var source = ecoleDataContext.tbl_payement.Where((d => DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(EndDate.Value.Date.AddDays(-days)) && DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(startDate.Value.Date.AddDays(days)) && d.Type != "Dépenses")).Select(d => new
                {
                    Id = d.Id,
                    Montant = d.Montant
                });
                decimal num1 = Convert.ToDecimal(ecoleDataContext.tbl_payement.Where((d => DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(EndDate.Value.Date.AddDays(-days)) && DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(startDate.Value.Date.AddDays(days)) && d.Type == "Dépenses")).Select(d => new
                {
                    Id = d.Id,
                    Montant = d.Montant
                }).Sum(x => x.Montant));
                Convert.ToDecimal(source.Sum(x => x.Montant));
                lblTotalRev.Text = "Total Encaissement : " + Convert.ToDecimal(source.Sum(x => x.Montant)).ToString("N0") + " FCFA";
                lblTotalDép.Text = "Total Décaissmeent : " + Convert.ToDecimal(num1).ToString("N0") + " FCFA";
                lblDep.Text = "Autres Dépenses : " + num1.ToString("N0") + " FCFA";
                
                decimal? nullable = source.Sum(x => x.Montant);
                decimal num2 = num1;
                string str = "Résultat : " + Convert.ToDecimal((nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - num2) : new Decimal?())).ToString("N0") + " FCFA";
                lblBénéfice.Text = str;
                lblBénéfice.Visible = true;
                lblCrédit.Visible = true;
                lblTotalRev.Visible = true;
                lblTotalDép.Visible = true;
            }
        }

        private async Task CallFill(int days)
        {
            DataTable result = await FillDGAsync(days);
            dataGridView1.Columns.Clear();
            try
            {
                dataGridView1.DataSource = result;
                SetData(days);
                result = null;
            }
            catch (Exception ex)
            {
                result = null;
            }
        }

        public Task<DataTable> FillDGAsync(int days) => Task.Factory.StartNew(() => Filldata(days));

        public DataTable Filldata(int days)
        {
            using (QuitayeContext ecoleDataContext = new QuitayeContext())
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Date");
                dataTable.Columns.Add("Montant_Encaissé");
                dataTable.Columns.Add("Montant_Décaissé");
                dataTable.Columns.Add("Résultat");
                for (int i = 0; i <= days; i++)
                {
                    var day = startDate.Value.AddDays(i);
                    var source1 = (ecoleDataContext.tbl_payement.Where((d => DbFunctions.TruncateTime(d.Date_Payement.Value) == DbFunctions.TruncateTime(day) 
                    && d.Type != "Dépenses")).GroupBy(d => new
                    {
                        Ref = DbFunctions.TruncateTime(d.Date_Payement.Value)
                    }).Select(gr => new
                    {
                        Ref = gr.Key.Ref,
                        Qty = gr.Sum((n => n.Montant))
                    })).ToList();
                    var source2 = ecoleDataContext.tbl_payement.Where((d => DbFunctions.TruncateTime(d.Date_Payement.Value) == DbFunctions.TruncateTime(day) && d.Type == "Dépenses")).GroupBy(d => new
                    {
                        Ref = DbFunctions.TruncateTime(d.Date_Payement.Value)
                    }).Select(gr => new
                    {
                        Ref = gr.Key.Ref,
                        Qty = gr.Sum(n => n.Montant)
                    });

                    DataRow row = dataTable.NewRow();
                    
                    row[0] = startDate.Value.Date.AddDays(i).ToString("dd-MM-yyyy");
                    row[1] = Convert.ToDecimal(source1.Sum(n => n.Qty)).ToString("N0");
                    row[2] = Convert.ToDecimal(source2.Sum(n => n.Qty)).ToString("N0");
                    row[3] = Convert.ToDecimal(Convert.ToDecimal(source1.Sum(n => n.Qty)) - Convert.ToDecimal(Convert.ToDecimal(source2.Sum(n => n.Qty)))).ToString("N0");
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e) => Print.PrintExcelFile(dataGridView1, "Resultat Financier", name, "Quitaye School");

        private void btnPdf_Click(object sender, EventArgs e) => Print.PrintPdfFile(dataGridView1, name, "Résultat Financier", "Activité Périodique", "Revenu & Dépenses", mycontrng, "Quitaye School", false);

        private async void EndDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime dateTime = EndDate.Value;
            DateTime date1 = dateTime.Date;
            dateTime = startDate.Value;
            DateTime date2 = dateTime.Date;
            ts = date1 - date2;
            await CallFill(ts.Days);
        }
    }
}
