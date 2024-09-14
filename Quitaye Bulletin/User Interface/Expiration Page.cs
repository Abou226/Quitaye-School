using Quitaye_School.Models.Context;
using Quitaye_Medical.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Expiration_Page : Form
    {
        public Timer LoadTimer { get; set; }
        public List<string> Code_Barre { get; set; }
        public string Filiale { get; set; }
        public Expiration_Page(List<string> code_barre, string filiale = null)
        {
            InitializeComponent();
            LoadTimer = new Timer();
            LoadTimer.Enabled = false;
            LoadTimer.Interval = 10;
            LoadTimer.Start();
            LoadTimer.Tick += LoadTimer_Tick;
            Code_Barre = code_barre;
            Filiale = filiale;
            btnFermer.Click += BtnFermer_Click;
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task CallTask()
        {
            var result = await FillDataAsync();
            if (result != null)
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = result;
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                try
                {
                    dataGridView1.Columns["Id"].Visible = false;
                }
                catch (Exception ex)
                {

                }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
            }
        }

        private Task<DataTable> FillDataAsync() => Task.Factory.StartNew(() => FillData());
        private DataTable FillData()
        {
            using(var donnée = new QuitayeContext())
            {
                var data = donnée.tbl_expiration
                    .Where(x => Code_Barre.Contains(x.Code_Barre) && x.Filiale == Filiale)
                    .OrderBy(x => x.Date_Expiration).ToList();
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("Code_Barre");
                dt.Columns.Add("Reste");
                dt.Columns.Add("Date_Expiration");
                dt.Columns.Add("Date");
                dt.Columns.Add("Mesure");

                foreach (var item in data)
                {
                    var row = dt.NewRow();
                    row["Id"] = item.Id;
                    row["Code_Barre"] = item.Code_Barre;
                    row["Reste"] = item.Reste;
                    row["Date_Expiration"] = item.Date_Expiration.Value.ToString("dd/MM/yyyy");
                    row["Date"] = item.Date.Value.ToString("dd/MM/yyyy");
                    row["Mesure"] = item.Mesure;

                    dt.Rows.Add(row);
                }

                return dt;
            }
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            LoadTimer.Stop();
            await CallTask();
        }
    }
}
