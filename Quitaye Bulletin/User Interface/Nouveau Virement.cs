using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quitaye_School.Models;
using Quitaye_School.Models.Context;

namespace Quitaye_School.User_Interface
{
    public partial class Nouveau_Virement : Form
    {
        public Nouveau_Virement()
        {
            InitializeComponent();
            btnFermer.Click += BtnFermer_Click;
            btnNouvelleOpération.Click += BtnNouvelleOpération_Click;
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            btnAddSource.Click += BtnAddSource_Click;
            btnAddDestination.Click += BtnAddDestination_Click;
            txtmontant.TextChanged += Txtmontant_TextChanged;
            txtmontant.KeyPress += Txtmontant_KeyPress;
        }

        private void Txtmontant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar != 46 && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void Txtmontant_TextChanged(object sender, EventArgs e)
        {
            if (txtmontant.Text != "" && txtmontant.Text != "-")
            {
                txtmontant.Text = Convert.ToDecimal(txtmontant.Text).ToString("N0");
                txtmontant.SelectionStart = txtmontant.Text.Length;
            }
        }
        public string ok;

        private async void BtnAddDestination_Click(object sender, EventArgs e)
        {
            Ajouter_Element element = new Ajouter_Element("Mode");
            element.ShowDialog();
            if (element.ok == "Oui")
            {
                await CallDestination();
            }
        }

        private async void BtnAddSource_Click(object sender, EventArgs e)
        {
            Ajouter_Element element = new Ajouter_Element("Mode");
            element.ShowDialog();
            if (element.ok == "Oui")
            {
                await CallSource();
            }
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallTask();
        }

        private async Task CallTask()
        {

            var source = FillModeAsync();
            var destination = FillModeAsync();

            var taskList = new List<Task> { source, destination };
            while(taskList.Count > 0)
            {
                var finish = await Task.WhenAny(taskList);
                if(finish == source)
                {
                    cbxSource.DataSource = source.Result;
                    cbxSource.DisplayMember = "Mode";
                    cbxSource.ValueMember = "Id";
                    cbxSource.Text = null;
                }
                else if(finish == destination)
                {
                    cbxDestination.DataSource = destination.Result;
                    cbxDestination.DisplayMember = "Mode";
                    cbxDestination.ValueMember = "Id";
                    cbxDestination.Text = null;
                }
                taskList.Remove(finish);
            }
        }
        private async void BtnNouvelleOpération_Click(object sender, EventArgs e)
        {
            if (cbxDestination.Text != "" && cbxSource.Text != "" && txtmontant.Text != "")
            {
                await CallSave();
            }
        }

        Timer loadTimer = new Timer();

        private async Task CallSave()
        {
            string montant = Regex.Replace(txtmontant.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
            Virement virement = new Virement();
            virement.Date = DateOpération.Value;
            virement.Destination = cbxDestination.Text;
            virement.Source = cbxSource.Text;
            virement.Montant = Convert.ToDecimal(montant);
            
            var result = await SaveVirementAsync(virement);
            if (result)
            {
                Alert.SShow("Virement Interne effectué avec succès.", Alert.AlertType.Sucess);
                txtmontant.Text = null;
                cbxDestination.Text = null;
                cbxSource.Text = null;
                ok = "Oui";
                DateOpération.Value = DateTime.Now;
            }
        }
        
        private async Task<bool> SaveVirementAsync(Virement virement)
        {
            using(var donnée = new QuitayeContext())
            {
                int id = 1;
                var p = (from d in donnée.tbl_payement orderby d.Id descending select new { Id = d.Id }).Take(1);
                if(p.Count() != 0)
                {
                    id = p.First().Id + 1;
                }
                {
                    var vi = new Models.Context. tbl_payement();
                    vi.Id = id;
                    vi.Montant = virement.Montant;
                    vi.Auteur = Principales.profile;
                    vi.Date_Enregistrement = DateTime.Now;
                    vi.Date_Payement = virement.Date;
                    vi.Mode_Payement = virement.Source;
                    vi.Date_Echeance = virement.Date;
                    vi.Nature = "Virement";
                    vi.Type = "Décaissement";
                    vi.Commentaire = "Virement Interne ";
                    donnée.tbl_payement.Add(vi);
                    await donnée.SaveChangesAsync();
                }
                var se = (from d in donnée.tbl_payement orderby d.Id descending select new { Id = d.Id }).Take(1);
                if(se.Count() != 0)
                {
                    id = se.First().Id + 1;
                }
                {
                    var vi = new Models.Context.tbl_payement();
                    vi.Id = id;
                    vi.Montant = virement.Montant;
                    vi.Auteur = Principales.profile;
                    vi.Date_Enregistrement = DateTime.Now;
                    vi.Date_Payement = virement.Date;
                    vi.Mode_Payement = virement.Destination;
                    vi.Nature = "Virement";
                    vi.Type = "Encaissement";
                    vi.Commentaire = "Virement Interne ";
                    donnée.tbl_payement.Add(vi);
                    await donnée.SaveChangesAsync();
                }
            }
            return true;
        }

        async Task CallSource()
        {
            var result = await FillModeAsync();
            cbxSource.DataSource = result;
            cbxSource.DisplayMember = "Mode";
            cbxSource.ValueMember = "Id";
            cbxSource.Text = null;
        }

        async Task CallDestination()
        {
            var result = await FillModeAsync();
            cbxDestination.DataSource = result;
            cbxDestination.DisplayMember = "Mode";
            cbxDestination.ValueMember = "Id";
            cbxDestination.Text = null;
        }

        public static Task<DataTable> FillModeAsync()
        {
            return Task.Factory.StartNew(() => FillMode());
        }
        private static DataTable FillMode()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Mode");
            using (var donnée = new QuitayeContext())
            {
                var der = from d in donnée.tbl_mode_payement orderby d.Id descending select new { Id = d.Id, Mode = d.Mode };
                foreach (var item in der)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Mode;

                    dt.Rows.Add(dr);
                }
            }
            return dt;

        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
