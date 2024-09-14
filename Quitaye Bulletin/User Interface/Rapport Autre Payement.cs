using PrintAction;
using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Rapport_Autre_Payement : Form
    {
        private string mycontrng = LogIn.mycontrng;
        private string name;
        private Timer loadTimer = new Timer();
        private Timer timer1 = new Timer();
        private string cycle;
        private MyTable result = new MyTable();
        private TimeSpan ts;
        public Rapport_Autre_Payement()
        {
            InitializeComponent();
            startDate.Value = DateTime.Now.AddDays(-30.0);
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            
            timer1.Start();
            btnExcel.Click += btnExcel_Click;
            btnPdf.Click += btnPDF_Click;
            cbxCycle.SelectedIndexChanged += cbxCycle_SelectedIndexChanged;
            startDate.ValueChanged += startDate_ValueChanged;
            EndDate.ValueChanged += startDate_ValueChanged;
            txtsearch.TextChanged += txtsearch_TextChanged;
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallDG();
        }

        private async Task CallDG()
        {
            MyTable myTable = await FillDGAscyn();
            result = myTable;
            myTable = null;
            dataGridView1.Columns.Clear();
            if (result.Table.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau !";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                lblTotal.Text = "Total : " + result.Montant.ToString("N0") + " FCFA";
                lblFille.Text = "Fille : " + result.Montant_Fille.ToString("N0") + " FCFA";
                lblGarçon.Text = "Garçon : " + result.Montant_Garçon.ToString("N0") + " FCFA";
                dt = null;
                dr = null;
            }
            else
            {
                dataGridView1.DataSource = result.Table;
                lblTotal.Text = "Total : " + result.Montant.ToString("N0") + " FCFA";
                lblFille.Text = "Fille : " + result.Montant_Fille.ToString("N0") + " FCFA";
                lblGarçon.Text = "Garçon : " + result.Montant_Garçon.ToString("N0") + " FCFA";
            }
            lblFille.Visible = true;
            lblGarçon.Visible = true;
            lblTotal.Visible = true;
        }

        private Task<MyTable> FillDGAscyn() => Task.Factory.StartNew((() => FillDG()));

        public MyTable FillDG()
        {
            MyTable myTable = new MyTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Prenom");
            dataTable.Columns.Add("Nom");
            dataTable.Columns.Add("Genre");
            dataTable.Columns.Add("Matricule");
            dataTable.Columns.Add("Montant");
            dataTable.Columns.Add("Type");
            dataTable.Columns.Add("Commentaire");
            dataTable.Columns.Add("Classe");
            dataTable.Columns.Add("Cycle");
            dataTable.Columns.Add("Date");
            dataTable.Columns.Add("Auteur");
            
            using (var donnée = new QuitayeContext())
            {
                try
                {
                    var source1 = donnée.tbl_payement.Where(d => d.Année_Scolaire == Principales.annéescolaire 
                    && d.Type == "Cantine" && d.Type == "Transport" && d.Type == "Assurance"
                    && (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value))
                    && d.Montant != 0).OrderByDescending(d => d.Id).Select(d => new
                    {
                        Id = d.Id,
                        Prenom = donnée.tbl_inscription.Where(x => x.N_Matricule == d.N_Matricule).FirstOrDefault().Prenom,
                        Nom = donnée.tbl_inscription.Where(x => x.N_Matricule == d.N_Matricule).FirstOrDefault().Nom,
                        Genre = donnée.tbl_inscription.Where(x => x.N_Matricule == d.N_Matricule).FirstOrDefault().Genre,
                        Matricule = d.N_Matricule,
                        Montant = d.Montant,
                        Commentaire = d.Commentaire,
                        Classe = d.Classe,
                        Cycle = d.Cycle,
                        Date = d.Date_Payement,
                        Type = d.Type,
                        Auteur = d.Auteur
                    });
                    var source2 = source1.Where(d => d.Genre == "Feminin").Select(d => new
                    {
                        Montant = d.Montant
                    });
                    var source3 = source1.Where(d => d.Genre == "Masculin").Select(d => new
                    {
                        Montant = d.Montant
                    });
                    foreach (var data in source1)
                    {
                        DataRow row = dataTable.NewRow();
                        row[0] = data.Id;
                        row[1] = data.Prenom;
                        row[2] = data.Nom;
                        row[3] = data.Genre;
                        row[4] = data.Matricule;
                        row[5] = data.Montant;
                        row[6] = data.Type;
                        row[7] = data.Commentaire;
                        row[8] = data.Classe;
                        row[9] = data.Cycle;
                        row[10] = data.Date;
                        row[11] = data.Auteur;
                        dataTable.Rows.Add(row);
                    }
                    myTable.Table = dataTable;
                    myTable.Montant = Convert.ToDecimal(source1.Sum(x => x.Montant));
                    myTable.Montant_Fille = Convert.ToDecimal(source2.Sum(x => x.Montant));
                    myTable.Montant_Garçon = Convert.ToDecimal(source3.Sum(x => x.Montant));
                    return myTable;
                }
                finally
                {
                    if (donnée != null)
                    {
                        
                        donnée.Dispose();
                    }
                }
            }
        }

        private async Task CallDGFiltre()
        {
            cycle = cbxCycle.Text;
            MyTable myTable = await FillDGFiltreAsync();
            result = myTable;
            myTable = (MyTable)null;
            dataGridView1.Columns.Clear();
            if (result.Table.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau !";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                lblTotal.Text = "Total : " + result.Montant.ToString("N0") + " FCFA";
                lblFille.Text = "Fille : " + result.Montant_Fille.ToString("N0") + " FCFA";
                lblGarçon.Text = "Garçon : " + result.Montant_Garçon.ToString("N0") + " FCFA";
                dt = null;
                dr = null;
            }
            else
            {
                dataGridView1.DataSource = result.Table;
                lblTotal.Text = "Total : " + result.Montant.ToString("N0") + " FCFA";
                lblFille.Text = "Fille : " + result.Montant_Fille.ToString("N0") + " FCFA";
                lblGarçon.Text = "Garçon : " + result.Montant_Garçon.ToString("N0") + " FCFA";
            }
            lblFille.Visible = true;
            lblGarçon.Visible = true;
            lblTotal.Visible = true;
        }

        private Task<MyTable> FillDGFiltreAsync() => Task.Factory.StartNew(() => FillDGFiltre());

        public MyTable FillDGFiltre()
        {
            MyTable myTable = new MyTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Prenom");
            dataTable.Columns.Add("Nom");
            dataTable.Columns.Add("Genre");
            dataTable.Columns.Add("Matricule");
            dataTable.Columns.Add("Montant");
            dataTable.Columns.Add("Type");
            dataTable.Columns.Add("Commentaire");
            dataTable.Columns.Add("Classe");
            dataTable.Columns.Add("Cycle");
            dataTable.Columns.Add("Date");
            dataTable.Columns.Add("Auteur");
            using (var ecoleDataContext = new QuitayeContext())
            {
                var source1 = ecoleDataContext.tbl_payement
                    .Where((d => d.Année_Scolaire == Principales.annéescolaire 
                && d.Type == "Cantine" && d.Type == "Transport" 
                && d.Type == "Assurance" && d.Montant != 0
                && (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value))
                && d.Cycle == cycle)).OrderByDescending(d => d.Id).Select(d => new
                {
                    Id = d.Id,
                    Prenom = d.Prenom,
                    Nom = d.Nom,
                    Genre = d.Genre,
                    Matricule = d.N_Matricule,
                    Montant = d.Montant,
                    Commentaire = d.Commentaire,
                    Classe = d.Classe,
                    Cycle = d.Cycle,
                    Type = d.Type,
                    Date = d.Date_Payement,
                    Auteur = d.Auteur
                });
                var source2 = source1.Where(d => d.Genre == "Feminin").Select(d => new
                {
                    Montant = d.Montant
                });
                var source3 = source1.Where(d => d.Genre == "Masculin").Select(d => new
                {
                    Montant = d.Montant
                });
                foreach (var data in source1)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Prenom;
                    row[2] = data.Nom;
                    row[3] = data.Genre;
                    row[4] = data.Matricule;
                    row[5] = data.Montant;
                    row[6] = data.Type;
                    row[7] = data.Commentaire;
                    row[8] = data.Classe;
                    row[9] = data.Cycle;
                    row[10] = data.Date;
                    row[11] = data.Auteur;
                    dataTable.Rows.Add(row);
                }
                myTable.Table = dataTable;
                myTable.Montant = Convert.ToDecimal(source1.Sum(x => x.Montant));
                myTable.Montant_Fille = Convert.ToDecimal(source2.Sum(x => x.Montant));
                myTable.Montant_Garçon = Convert.ToDecimal(source3.Sum(x => x.Montant));
                return myTable;
            }
        }

        private async Task CallSearch()
        {
            MyTable myTable = await DataSearchAsync(txtsearch.Text);
            result = myTable;
            myTable = null;
            dataGridView1.Columns.Clear();
            if (result.Table.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau !";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                lblTotal.Text = "Total : " + result.Montant.ToString("N0") + " FCFA";
                lblFille.Text = "Fille : " + result.Montant_Fille.ToString("N0") + " FCFA";
                lblGarçon.Text = "Garçon : " + result.Montant_Garçon.ToString("N0") + " FCFA";
                dt = null;
                dr = null;
            }
            else
            {
                dataGridView1.DataSource = result.Table;
                lblTotal.Text = "Total : " + result.Montant.ToString("N0") + " FCFA";
                lblFille.Text = "Fille : " + result.Montant_Fille.ToString("N0") + " FCFA";
                lblGarçon.Text = "Garçon : " + result.Montant_Garçon.ToString("N0") + " FCFA";
            }
        }

        private Task<MyTable> DataSearchAsync(string search) => Task.Factory.StartNew((() => DataSearch(search)));

        public MyTable DataSearch(string search)
        {
            MyTable myTable = new MyTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Prenom");
            dataTable.Columns.Add("Nom");
            dataTable.Columns.Add("Genre");
            dataTable.Columns.Add("Matricule");
            dataTable.Columns.Add("Montant");
            dataTable.Columns.Add("Type");
            dataTable.Columns.Add("Commentaire");
            dataTable.Columns.Add("Classe");
            dataTable.Columns.Add("Cycle");
            dataTable.Columns.Add("Date");
            dataTable.Columns.Add("Auteur");
            
            using(var donnée = new QuitayeContext())
            {
                try
                {
                    var source1 = donnée.tbl_payement
                        .Where(d => d.Année_Scolaire == Principales.annéescolaire 
                    && d.Type == "Cantine" && d.Type == "Transport" && d.Type == "Assurance" 
                    && d.Montant != 0
                    && (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value))
                    && (d.Prenom.Contains(search) 
                    || d.Nom.Contains(search) 
                    || d.N_Matricule.Contains(search) 
                    || d.Opération.Contains(search))).Select(d => new
                    {
                        Id = d.Id,
                        Prenom = donnée.tbl_inscription.Where(x => x.N_Matricule == d.N_Matricule).FirstOrDefault().Prenom,
                        Nom = donnée.tbl_inscription.Where(x => x.N_Matricule == d.N_Matricule).FirstOrDefault().Nom, 
                        Genre = donnée.tbl_inscription.Where(x => x.N_Matricule == d.N_Matricule).FirstOrDefault().Genre,
                        Matricule = d.N_Matricule,
                        Montant = d.Montant,
                        Commentaire = d.Commentaire,
                        Classe = d.Classe,
                        Cycle = d.Cycle,
                        Type = d.Type,
                        Date = d.Date_Payement,
                        Auteur = d.Auteur
                    });
                    var source2 = source1.Where(d => d.Genre == "Feminin").Select(d => new
                    {
                        Montant = d.Montant
                    });
                    var source3 = source1.Where(d => d.Genre == "Masculin").Select(d => new
                    {
                        Montant = d.Montant
                    });
                    foreach (var data in source1)
                    {
                        DataRow row = dataTable.NewRow();
                        row[0] = data.Id;
                        row[1] = data.Prenom;
                        row[2] = data.Nom;
                        row[3] = data.Genre;
                        row[4] = data.Matricule;
                        row[5] = data.Montant;
                        row[6] = data.Type;
                        row[7] = data.Commentaire;
                        row[8] = data.Classe;
                        row[9] = data.Cycle;
                        row[10] = data.Date;
                        row[11] = data.Auteur;
                        dataTable.Rows.Add(row);
                    }
                    myTable.Table = dataTable;
                    myTable.Montant = Convert.ToDecimal(source1.Sum(x => x.Montant));
                    myTable.Montant_Fille = Convert.ToDecimal(source2.Sum(x => x.Montant));
                    myTable.Montant_Garçon = Convert.ToDecimal(source3.Sum(x => x.Montant));
                    return myTable;
                }
                finally
                {
                    
                    if (donnée != null)
                    {
                        
                        donnée.Dispose();
                    }
                }
            }
        }

        private async void startDate_ValueChanged(object sender, EventArgs e)
        {
            if (cbxCycle.Text == "" || cbxCycle.Text == "Tout")
            {
                await CallDG();
            }
            else
            {
                DateTime dateTime = EndDate.Value;
                DateTime date1 = dateTime.Date;
                dateTime = startDate.Value;
                DateTime date2 = dateTime.Date;
                ts = date1 - date2;
                await CallDGFiltre();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e) => Print.PrintExcelFile(dataGridView1, "Journal Autre Payement " + txtsearch.Text, name, "Quitaye School");

        private void btnPDF_Click(object sender, EventArgs e) => Print.PrintRecuPdfFile(dataGridView1, name, "Historique Payement", "Autres Payement", cbxCycle.Text + " " + txtsearch.Text, mycontrng, "Quitaye School", true, new Detail_Facture()
        {
            MontantTTC = result.Montant,
            Taxe = result.Montant / 100M * 18M,
            MontantHT = result.Montant - result.Montant / 100M * 18M
        }, false);

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cbxCycle.Text != "" && cbxCycle.Text != "Tout")
            {
                string[] strArray = new string[9]
                {
          "Journal Scolarité ",
          cbxCycle.Text,
          " ",
          Principales.annéescolaire,
          " ",
          null,
          null,
          null,
          null
                };
                DateTime date1 = startDate.Value;
                date1 = date1.Date;
                strArray[5] = date1.ToString("dd-MM-yyyy");
                DateTime date2 = EndDate.Value;
                date2 = date2.Date;
                strArray[6] = date2.ToString("dd-MM-yyyy");
                strArray[7] = " ";
                strArray[8] = DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
                name = string.Concat(strArray);
            }
            else
            {
                string[] strArray = new string[7]
                {
          "Journal Scolarité ",
          Principales.annéescolaire,
          " ",
          null,
          null,
          null,
          null
                };
                DateTime date = startDate.Value;
                date = date.Date;
                strArray[3] = date.ToString("dd-MM-yyyy");
                DateTime dateTime = EndDate.Value;
                dateTime = dateTime.Date;
                strArray[4] = dateTime.ToString("dd-MM-yyyy");
                strArray[5] = " ";
                dateTime = DateTime.Now;
                strArray[6] = dateTime.ToString("dd-MM-yyyy HH.mm.ss");
                name = string.Concat(strArray);
            }
        }

        private async void cbxCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCycle.Text == "" || cbxCycle.Text == "Tout")
                await CallDG();
            else
                await CallDGFiltre();
        }

        private async void txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtsearch.Text))
                await CallSearch();
            else
                await CallDG();
        }
    }
}