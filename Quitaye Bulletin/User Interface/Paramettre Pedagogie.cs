using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Paramettre_Pedagogie : Form
    {
        public Timer timer { get; set; } 
        public Paramettre_Pedagogie()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            await CallExament();
            await CallMatière();
        }

        private async Task CallExament()
        {
            MyTable result = await FillExamenAsync();
            dataGridView2.Columns.Clear();
            if (result.Table.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau !";
                dt.Rows.Add(dr);
                dataGridView2.DataSource = dt;
                dt = null;
                dr = null;
                result = null;
            }
            else
            {
                dataGridView2.DataSource = result.Table;
                DeleteColumn deleteColumn = new DeleteColumn();
                deleteColumn.HeaderText = "Sup";
                deleteColumn.Name = "Sup";
                deleteColumn.Width = 40;
                try
                {
                    dataGridView2.Columns.Add(deleteColumn);
                    dataGridView2.Columns["Sup"].Width = 40;
                }
                catch (Exception ex)
                {
                }
                deleteColumn = null;
                result = null;
            }
        }

        private Task<MyTable> FillExamenAsync() => Task.Factory.StartNew(() => FillExamen());

        private MyTable FillExamen()
        {
            MyTable myTable = new MyTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Nom");
            dataTable.Columns.Add("Date_Ajout");
            dataTable.Columns.Add("Auteur");
            using (var ecoleDataContext = new QuitayeContext())
            {
                var source = ecoleDataContext.tbl_examen.OrderByDescending((d => d.Date_Enregistrement)).Select(d => new
                {
                    Id = d.Id,
                    Nom = d.Nom,
                    Date = d.Date_Enregistrement,
                    Auteur = d.Auteur
                });
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Nom;
                    row[2] = data.Date;
                    row[3] = data.Auteur;
                    dataTable.Rows.Add(row);
                }
                myTable.Table = dataTable;
                return myTable;
            }
        }

        private async Task CallMatière()
        {
            MyTable result = await FillMatièreAsync();
            dataGridView1.Columns.Clear();
            if (result.Table.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau !";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                dt = null;
                dr = null;
                result = null;
            }
            else
            {
                dataGridView1.DataSource = result.Table;
                DeleteColumn deleteColumn = new DeleteColumn();
                deleteColumn.HeaderText = "Sup";
                deleteColumn.Name = "Sup";
                deleteColumn.Width = 40;
                try
                {
                    dataGridView1.Columns.Add(deleteColumn);
                    dataGridView1.Columns["Sup"].Width = 40;
                }
                catch (Exception ex)
                {
                }
                deleteColumn = null;
                result = null;
            }
        }

        private Task<MyTable> FillMatièreAsync() => Task.Factory.StartNew(() => FillMatière());

        private MyTable FillMatière()
        {
            MyTable myTable = new MyTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Nom");
            dataTable.Columns.Add("Date_Ajout");
            dataTable.Columns.Add("Auteur");
            using (var ecoleDataContext = new QuitayeContext())
            {
                var source = ecoleDataContext.tbl_matiere.OrderByDescending(d => d.Date_Enregistrement).Select(d => new
                {
                    Id = d.Id,
                    Nom = d.Nom,
                    Date = d.Date_Enregistrement,
                    Auteur = d.Auteur
                });
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Nom;
                    row[2] = data.Date;
                    row[3] = data.Auteur;
                    dataTable.Rows.Add(row);
                }
                myTable.Table = dataTable;
                return myTable;
            }
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void btnExamen_Click(object sender, EventArgs e)
        {
            Ajout_Examen examen = new Ajout_Examen();
            int num = (int)examen.ShowDialog();
            if (!(examen.ok == "Oui"))
            {
                examen = null;
            }
            else
            {
                await CallExament();
                examen = null;
            }
        }

        private async void btnMatière_Click(object sender, EventArgs e)
        {
            Ajout_Matière matière = new Ajout_Matière();
            int num = (int)matière.ShowDialog();
            if (!(matière.ok == "Oui"))
            {
                matière = null;
            }
            else
            {
                await CallMatière();
                matière = null;
            }
        }
    }
}
