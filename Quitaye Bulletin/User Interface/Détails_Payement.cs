using iTextSharp.text;
using iTextSharp.text.pdf;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Détails_Payement : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Détails_Payement()
        {
            InitializeComponent();
            temp = 1;
            timer1.Start();
            btnDernierRecu.Click += BtnDernierRecu_Click;
            btnToutRecu.Click += BtnToutRecu_Click;
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
        }

        private void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            CallTask();
        }

        Timer loadTimer = new Timer();

        private void BtnToutRecu_Click(object sender, EventArgs e)
        {
            PrintAction.Print.PrintPdfFile(dataGridViews, name, "Recu Payement", "Scolarité", nom, mycontrng, "Quitaye School", false);
        }

        private void BtnDernierRecu_Click(object sender, EventArgs e)
        {
            foreach (var item in dataGridViews)
            {
                PrintAction.Print.PrintPdfFile(item, name, "Recu Payement", "Scolarité", nom, mycontrng, "Quitaye School", false);
                break;
            }
        }

        public string matricule;
        public static string classe;
        public static string nom;
        public string classsss;
        public string nommmmm;
        private int temp;

        private async void CallData()
        {
            var result = await FillDataAsync();
            dataGridView1.DataSource = result;
        }

        List<DataGridView> dataGridViews = new List<DataGridView>();
        private async void CallTask()
        {
            var filldata = FillDataAsync();
            var filllist = FillListAsync();
            var taskList = new List<Task> {filldata, filllist};

            while(taskList.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(taskList);
                if(finishedTask == filllist)
                {
                    dataGridViews = new List<DataGridView>();
                    foreach (var item in filllist.Result)
                    {
                        DataGridView dg = new DataGridView();
                        dg.DataSource = item;
                        dg.Name = item.TableName;
                        dataGridViews.Add(dg);
                    }
                }
                else if(finishedTask == filldata)
                {
                    dataGridView1.DataSource = filldata.Result;
                }

                taskList.Remove(finishedTask);
            }
        }
        private Task<DataTable> FillDataAsync()
        {
            return Task.Factory.StartNew(() => FillDG());
        }
        public DataTable FillDG()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Prenom");
            dt.Columns.Add("Nom");
            dt.Columns.Add("Matricule");
            dt.Columns.Add("Montant");
            dt.Columns.Add("Commentaire");
            dt.Columns.Add("Date");
            dt.Columns.Add("Date_Enregistrement");
            using(var donnée = new QuitayeContext())
            {
                var payement = from d in donnée.tbl_payement
                               where d.Année_Scolaire == Principales.annéescolaire 
                               && d.N_Matricule == matricule && d.Montant != 0 
                               && d.Type == "Scolarité"
                               select new
                               {
                                   Prenom = d.Prenom,
                                   Nom = d.Nom,
                                   Matricule = d.N_Matricule,
                                   Montant = d.Montant,
                                   Commentaire = d.Commentaire,
                                   Date = d.Date_Payement,
                                   Enregistrement = d.Date_Enregistrement,
                               };

                foreach (var item in payement)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Prenom;
                    dr[1] = item.Nom;
                    dr[2] = item.Matricule;
                    dr[3] = item.Montant;
                    dr[4] = item.Commentaire;
                    dr[5] = item.Date;
                    dr[6] = item.Enregistrement;

                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }

        private Task<List<DataTable>> FillListAsync()
        {
            return Task.Factory.StartNew(() => FillList());
        }

        private List<DataTable> FillList()
        {
            List<DataTable> list = new List<DataTable>();
            using(var donnée = new QuitayeContext())
            {
                var data = from d in donnée.tbl_payement
                           where d.Année_Scolaire == Principales.annéescolaire
                           && d.N_Matricule == matricule && d.Montant != 0 && d.Type == "Scolarité" 
                           group d by new
                           {
                               Date = DbFunctions.TruncateTime(d.Date_Payement.Value),
                           } into gr orderby gr.Key.Date descending
                           select new
                           {
                               Date = gr.Key.Date,
                           };
                foreach (var item in data)
                {
                    DataTable dt = new DataTable(item.Date.ToString());
                    dt.Columns.Add("Prenom");
                    dt.Columns.Add("Nom");
                    dt.Columns.Add("N° Matricule");
                    dt.Columns.Add("Montant");
                    dt.Columns.Add("Désignation");
                    dt.Columns.Add("Date", typeof(DateTime));

                    var payement = from d in donnée.tbl_payement
                                   where d.Année_Scolaire == Principales.annéescolaire
                                   && d.N_Matricule == matricule && d.Montant != 0 && 
                                   d.Type == "Scolarité" && DbFunctions.TruncateTime(d.Date_Payement.Value) == DbFunctions.TruncateTime(item.Date)
                                   select new
                                   {
                                       Prenom = d.Prenom,
                                       Nom = d.Nom,
                                       Matricule = d.N_Matricule,
                                       Montant = d.Montant,
                                       Commentaire = d.Commentaire,
                                       Date = d.Date_Payement,
                                   };

                    foreach (var items in payement)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = items.Prenom;
                        dr[1] = items.Nom;
                        dr[2] = items.Matricule;
                        dr[3] = items.Montant;
                        dr[4] = items.Commentaire;
                        dr[5] = items.Date;

                        dt.Rows.Add(dr);
                        dt.TableName = item.Date.ToString();
                    }
                    list.Add(dt);
                }
            }
            return list;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(temp == 1)
            {
                timer1.Stop();
                
                classe = classsss;
                nom = nommmmm;
                name = "Journal Scolarité " + nom + " " + " " + classe + " " + Principales.annéescolaire + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
                temp = 0;
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            PrintAction.Print.PrintExcelFile(dataGridView1, "Historique Payement", name, "Quitaye School");
        }

        
        public string name;

        private void btnPDF_Click(object sender, EventArgs e)
        {
            PrintAction.Print.PrintPdfFile(dataGridView1, name, "Historique Payement", "Scolarité", nom, mycontrng, "Quitaye School", false);
        }
    }
}
