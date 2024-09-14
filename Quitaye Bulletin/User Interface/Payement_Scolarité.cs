using iTextSharp.text;
using iTextSharp.text.pdf;
using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Payement_Scolarité : Form
    {
        string mycontrng = LogIn.mycontrng;
        private string name;
        //private int temp;

        public Payement_Scolarité()
        {
            InitializeComponent();
            startDate.Value = DateTime.Now.AddDays(-30);
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            timer1.Start();
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallDG();
        }

        Timer loadTimer = new Timer();
        async Task CallDG()
        {
            var result = await FillDGAscyn();
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
                lblGarçon.Text = "Garçon : " + result.Montant.ToString("N0") + " FCFA";
            }
            else
            {
                dataGridView1.DataSource = result.Table;
                lblTotal.Text = "Total : " + result.Montant.ToString("N0") + " FCFA";
                lblFille.Text = "Fille : " + result.Montant_Fille.ToString("N0") + " FCFA";
                lblGarçon.Text = "Garçon : " + result.Montant.ToString("N0") + " FCFA";
            }
            lblFille.Visible = true;
            lblGarçon.Visible = true;
            lblTotal.Visible = true;
        }

        Task<MyTable> FillDGAscyn()
        {
            return Task.Factory.StartNew(() => FillDG());
        }

        public MyTable FillDG()
        {
            MyTable table = new MyTable();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Prenom");
            dt.Columns.Add("Nom");
            dt.Columns.Add("Genre");
            dt.Columns.Add("Matricule");
            dt.Columns.Add("Montant");
            dt.Columns.Add("Commentaire");
            dt.Columns.Add("Classe");
            dt.Columns.Add("Cycle");
            dt.Columns.Add("Date");
            dt.Columns.Add("Auteur");
            using (var donnée = new QuitayeContext())
            {
                var don = from d in donnée.tbl_payement
                          where d.Année_Scolaire == Principales.annéescolaire && d.Type == "Scolarité"
                          && (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value)) && d.Montant != 0
                          orderby d.Id descending
                          select new
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
                              Date = d.Date_Payement,
                              Auteur = d.Auteur,
                          };
                var fille = from d in donnée.tbl_payement
                            where d.Genre == "Feminin" && d.Année_Scolaire == Principales.annéescolaire && d.Type == "Scolarité"
                            select new { Montant = d.Montant };
                var garçon = from d in donnée.tbl_payement
                             where d.Genre == "Masculin" && d.Année_Scolaire == Principales.annéescolaire && d.Type == "Scolarité"
                             select new { Montant = d.Montant };

                foreach (var item in don)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Prenom;
                    dr[2] = item.Nom;
                    dr[3] = item.Genre;
                    dr[4] = item.Matricule;
                    dr[5] = item.Montant;
                    dr[6] = item.Commentaire;
                    dr[7] = item.Classe;
                    dr[8] = item.Cycle;
                    dr[9] = item.Date;
                    dr[10] = item.Auteur;

                    dt.Rows.Add(dr);
                }

                table.Table = dt;
                table.Montant = Convert.ToDecimal(don.Sum(x => x.Montant));
                table.Montant_Fille = Convert.ToDecimal(fille.Sum(x => x.Montant));
                table.Montant_Garçon = Convert.ToDecimal(garçon.Sum(x => x.Montant));
                return table;
            }
        }

        async Task CallDGFiltre()
        {
            cycle = cbxCycle.Text;
            var result = await FillDGFiltreAsync();
            dataGridView1.Columns.Clear();
            if(result.Table.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");

                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau !";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                lblTotal.Text = "Total : " + result.Montant.ToString("N0") + " FCFA";
                lblFille.Text = "Fille : " + result.Montant_Fille.ToString("N0") + " FCFA";
                lblGarçon.Text = "Garçon : " + result.Montant.ToString("N0") + " FCFA";
            }
            else
            {
                dataGridView1.DataSource = result.Table;
                lblTotal.Text = "Total : " + result.Montant.ToString("N0") + " FCFA";
                lblFille.Text = "Fille : " + result.Montant_Fille.ToString("N0") + " FCFA";
                lblGarçon.Text = "Garçon : " + result.Montant.ToString("N0") + " FCFA";
            }
            lblFille.Visible = true;
            lblGarçon.Visible = true;
            lblTotal.Visible = true;
        }

        private Task<MyTable> FillDGFiltreAsync()
        {
            return Task.Factory.StartNew(() => FillDGFiltre());
        }

        string cycle;
        public MyTable FillDGFiltre()
        {
            MyTable table = new MyTable();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Prenom");
            dt.Columns.Add("Nom");
            dt.Columns.Add("Genre");
            dt.Columns.Add("Matricule");
            dt.Columns.Add("Montant");
            dt.Columns.Add("Commentaire");
            dt.Columns.Add("Classe");
            dt.Columns.Add("Cycle");
            dt.Columns.Add("Date");
            dt.Columns.Add("Auteur");

            using (var donnée = new QuitayeContext())
            {
                var don = from d in donnée.tbl_payement
                          where d.Année_Scolaire == Principales.annéescolaire && d.Type == "Scolarité" && d.Montant != 0
                          && (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value))
                          && d.Cycle == cycle
                          orderby d.Id descending
                          select new
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
                              Date = d.Date_Payement,
                              Auteur = d.Auteur,
                          };
                var fille = from d in donnée.tbl_payement 
                            where d.Genre == "Feminin" && d.Année_Scolaire == Principales.annéescolaire 
                            && d.Cycle == cycle select new { Montant = d.Montant };
                var garçon = from d in donnée.tbl_payement 
                             where d.Genre == "Masculin" && d.Année_Scolaire == Principales.annéescolaire 
                             && d.Cycle == cycle select new { Montant = d.Montant };
                foreach (var item in don)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Prenom;
                    dr[2] = item.Nom;
                    dr[3] = item.Genre;
                    dr[4] = item.Matricule;
                    dr[5] = item.Montant;
                    dr[6] = item.Commentaire;
                    dr[7] = item.Classe;
                    dr[8] = item.Cycle;
                    dr[9] = item.Date;
                    dr[10] = item.Auteur;

                    dt.Rows.Add(dr);
                }

                table.Table = dt;
                table.Montant = Convert.ToDecimal(don.Sum(x => x.Montant));
                table.Montant_Fille = Convert.ToDecimal(fille.Sum(x => x.Montant));
                table.Montant_Garçon = Convert.ToDecimal(garçon.Sum(x => x.Montant));
                return table;
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
                ts = EndDate.Value.Date - startDate.Value.Date;
               await CallDGFiltre();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            PrintAction.Print.PrintExcelFile(dataGridView1, "Journal Scolarité", name, "Quitaye School");
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            PrintAction.Print.PrintPdfFile(dataGridView1, name, "Historique Payement", "Scolarité", cbxCycle.Text, mycontrng, "Quitaye School", true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(cbxCycle.Text != "" && cbxCycle.Text != "Tout")
            {
                name = "Journal Scolarité " + cbxCycle.Text + " " + Principales.annéescolaire + " " + startDate.Value.Date.ToString("dd-MM-yyyy") + EndDate.Value.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            }
            else
            {
                name = "Journal Scolarité " + Principales.annéescolaire + " " + startDate.Value.Date.ToString("dd-MM-yyyy") + EndDate.Value.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            }
        }


        TimeSpan ts;
        

        private async void cbxCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCycle.Text == "" || cbxCycle.Text == "Tout")
            {
               await CallDG();
            }
            else
            {
               await CallDGFiltre();
            }
        }
    }
}
