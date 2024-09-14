using Quitaye_School.Models;
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
    public partial class Pret_Remboursement : Form
    {
        public Pret_Remboursement()
        {
            InitializeComponent();
            btnNouvelleOpération.Click += BtnNouvelleOpération_Click;
            startDate.Value = DateTime.Now.AddDays(-6);
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            dataGridView1.CellClick += DataGridView1_CellClick;
            startDate.ValueChanged += StartDate_ValueChanged;
            EndDate.ValueChanged += StartDate_ValueChanged;
        }

        private async void StartDate_ValueChanged(object sender, EventArgs e)
        {
            await CallData();
        }

        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Principales.type_compte.Contains("Administrateur") || Principales.role == "Administrateur")
            {
                if (e.ColumnIndex >= 2)
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Sup") == true)
                    {
                        MsgBox msg = new MsgBox();
                        msg.show("Voulez-vous supprimer cet élément ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                        msg.ShowDialog();
                        if (msg.clicked == "Non")
                            return;
                        else if (msg.clicked == "Oui")
                        {
                            if (await DeleteDataAsync(id))
                            {
                                Alert.SShow("Element supprimé avec succès.", Alert.AlertType.Sucess);
                                await CallData();
                            }
                        }
                    }
                }
            }
        }

        
        private async Task<bool> DeleteDataAsync(int id)
        {
            using (var donnée = new QuitayeContext())
            {
                var der = (from d in donnée.tbl_payement where d.Id == id select d).First();
                donnée.tbl_payement.Remove(der);
                await donnée.SaveChangesAsync();
                return true;
            }
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallData();
        }

        Timer loadTimer = new Timer();

        private async void BtnNouvelleOpération_Click(object sender, EventArgs e)
        {
            Nouveau_Pret_Remboursement pret_Remboursement = new Nouveau_Pret_Remboursement();
            pret_Remboursement.ShowDialog();
            if(pret_Remboursement.ok == "Oui")
            {
                await CallData();
            }
        }

        private async Task CallData()
        {
            var result = await FillDataAsync();
            dataGridView1.Columns.Clear();
            if(result.Table.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
            }
            else
            {
                try
                {
                    dataGridView1.DataSource = result.Table;
                    AddColumns.Addcolumn(dataGridView1);
                    dataGridView1.Columns["Edit"].Visible = false;
                }
                catch (Exception)
                {

                }
            }
        }
        private Task<MyTable> FillDataAsync()
        {
            return Task.Factory.StartNew(() => FillData());
        }
        private MyTable FillData()
        {
            MyTable table = new MyTable();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Désignation");
            dt.Columns.Add("Mode_Payement");
            dt.Columns.Add("Montant", typeof(decimal));
            dt.Columns.Add("Num_Opération");
            dt.Columns.Add("Type");
            dt.Columns.Add("Nature");
            dt.Columns.Add("Date", typeof(DateTime));
            dt.Columns.Add("Date_Enregistrement", typeof(DateTime));
            dt.Columns.Add("Date_Echeance", typeof(DateTime));
            dt.Columns.Add("Auteur");
            using (var donnée = new QuitayeContext())
            {
                var des = from d in donnée.tbl_payement
                          where (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value)) && (d.Nature == "Prêt" || d.Nature == "Remboursement")
                          orderby d.Id descending
                          select new
                          {
                              Id = d.Id,
                              Désignation = d.Commentaire,
                              Mode_Payement = d.Mode_Payement,
                              Montant = d.Montant,
                              Num_Opération = d.Num_Opération,
                              Date = d.Date_Payement,
                              Type = d.Type,
                              Nature = d.Nature,
                              Date_Enregistrement = d.Date_Enregistrement,
                              Auteur = d.Auteur,
                              Date_Echance = d.Date_Echeance,
                          };
                table.Montant = Convert.ToDecimal(des.Where(x => x.Type == "Encaissement").Sum(x => x.Montant));
                table.Quantité = Convert.ToDecimal(des.Where(x => x.Type == "Décaissement").Sum(x => x.Montant));

                foreach (var item in des)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Désignation;
                    dr[2] = item.Mode_Payement;
                    dr[3] = item.Montant;
                    dr[4] = item.Num_Opération;
                    dr[5] = item.Type;
                    dr[6] = item.Nature;
                    dr[7] = Convert.ToDateTime(item.Date.Value.ToString("dd/MM/yyyy"));
                    dr[8] = Convert.ToDateTime(item.Date_Enregistrement.Value.ToString("dd/MM/yyyy hh:mm:ss"));
                    dr[9] = Convert.ToDateTime(item.Date_Echance.Value.ToString("dd/MM/yyyy"));
                    dr[10] = item.Auteur;

                    dt.Rows.Add(dr);
                }

                table.Table = dt;
                return table;
            }
        }
    }
}
