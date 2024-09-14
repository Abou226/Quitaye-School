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
    public partial class Rapport_Payement : Form
    {
        public Rapport_Payement()
        {
            InitializeComponent();
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            startDate.Value = DateTime.Today.AddDays(-6);
            btnExcel.Click += btnExcel_Click;
            btnPdf.Click += btnPDF_Click;
            cbxType.SelectedIndexChanged += CbxType_SelectedIndexChanged;
            cbxMode.SelectedIndexChanged += CbxMode_SelectedIndexChanged;
            dataGridView1.CellClick += DataGridView1_CellClick;
            startDate.ValueChanged += StartDate_ValueChanged;
            EndDate.ValueChanged += StartDate_ValueChanged;
            txtsearch.TextChanged += Txtsearch_TextChanged;
            btnClotureEcriture.Click += BtnClotureEcriture_Click;
            btnSelectionTout.Click += BtnSelectionTout_Click;
        }

        private void BtnSelectionTout_Click(object sender, EventArgs e)
        {
            if (btnSelectionTout.Text == "Tout Selectionner")
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    item.Cells["Select"].Value = true;
                }

                btnSelectionTout.Text = "Tout Deselectionner";
            }
            else if (btnSelectionTout.Text == "Tout Deselectionner")
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    item.Cells["Select"].Value = false;
                }

                btnSelectionTout.Text = "Tout Selectionner";
            }
        }

        private async void BtnClotureEcriture_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["Select"].Value);
                if (isSelected)
                    list.Add(Convert.ToInt32(row.Cells["Id"].Value));
            }

            if (list.Count > 0)
            {
                var msg = new MsgBox();
                msg.show($"Voulez-vus réellement cloturé ce(s) {list.Count} élément(s) ?", "Cloture écriture",
                    MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                msg.ShowDialog();
                if (msg.clicked == "Oui")
                {
                    var result = await RangeClotureAsync(list);
                    if (result)
                    {
                        Alert.SShow("Cloture éffectué avec succès.", Alert.AlertType.Sucess);
                    }
                }
            }
        }

        private async void Txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (!first)
            {
               
                if (txtsearch.Text != "")
                    await CallSeach(txtsearch.Text);
                else await CallData();
            }
        }

        private async Task<bool> SingleClotureAsync(int id)
        {
            using (var donnée = new QuitayeContext())
            {
                var item = donnée.tbl_payement.Where(x => x.Id == id).FirstOrDefault();
                item.Cloturé = "Oui";
                await donnée.SaveChangesAsync();
                return true;
            }
        }

        private async Task<bool> RangeClotureAsync(List<int> ids)
        {
            using (var donnée = new QuitayeContext())
            {
                var items = donnée.tbl_payement.Where(x => ids.Contains(x.Id)).ToList();
                foreach (var item in items)
                {
                    item.Cloturé = "Oui";
                }
                await donnée.SaveChangesAsync();
                return true;
            }
        }

        private async void StartDate_ValueChanged(object sender, EventArgs e)
        {
            await CallData();
        }

        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string auteur = dataGridView1.CurrentRow.Cells["Auteur"].Value.ToString() ;
            if (Principales.type_compte.Contains("Administrateur") || auteur == Principales.profile)
            {
                if (dataGridView1.Columns.Count >= 2)
                {
                    if (e.ColumnIndex >= 0)
                    {
                        if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Sup") == true)
                        {
                            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                            MsgBox msg = new MsgBox();
                            msg.show("Voulez-vous supprimer ce payement ?", "Suppression", 
                                MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                            msg.ShowDialog();
                            if (msg.clicked == "Non")
                                return;
                            else if (msg.clicked == "Oui")
                            {
                                using(var donnée = new QuitayeContext())
                                {
                                    var v = (from d in donnée.tbl_payement 
                                             where d.Id == id && (d.Auteur == Principales.profile 
                                             || Principales.type_compte.Contains("Administrateur")) 
                                             select d).FirstOrDefault();
                                    if(v != null)
                                    {
                                        if (v.Cloturé != "Oui")
                                        {
                                            donnée.tbl_payement.Remove(v);
                                            await donnée.SaveChangesAsync();
                                            await CallData();
                                            Alert.SShow("Vente supprimeé avec succès.", Alert.AlertType.Sucess);
                                        }
                                        else Alert.SShow("Suppression impossible, ecriture cloturé.", Alert.AlertType.Info);
                                    }else Alert.SShow("Suppression impossible.", Alert.AlertType.Info);
                                }
                            }
                        }
                        else if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Désignation"))
                        {
                            var désignation = dataGridView1.CurrentRow.Cells["Désignation"].Value.ToString();
                            //var details = new Details_Payement(startDate.Value, EndDate.Value, désignation);
                            //details.ShowDialog();
                        }
                    }
                }
            }
        }

        private async void CbxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!first)
            {
                await CallSeach();
            }
        }

        private async void CbxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!second)
            {
                await CallSeach();
            }
        }

        string name;
        bool second = true;
        private async void btnExcel_Click(object sender, EventArgs e)
        {
            //PrintAction.Print.PrintExcelFile(dataGridView2, "Rapport Payement", name, "Quitaye School");

            var file = "";
            file = "C:/Quitaye School/Rapport des Payements " + cbxMode.Text + ", " + cbxType.Text + " " + startDate.Value.Date.ToString("dd-MM-yyyy") + EndDate.Value.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");

            //PrintAction.Print.PrintExcelFile(dataGridView2, "Rapport Commane(s)", name, "Quitaye School");
            Alert.SShow("Génération Barcode en cours.. Veillez-patientez !", Alert.AlertType.Info);
            await Task.Run(() => Quitaye_School.Models.Docuement.ExportToExcel(dataGridView2, file));
            System.Diagnostics.Process.Start(file);
            Alert.SShow("Génération effectué avec succès.", Alert.AlertType.Sucess);
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            name = "Rapport des Payements " + cbxMode.Text + ", " + cbxType.Text + " " + startDate.Value.Date.ToString("dd-MM-yyyy") + EndDate.Value.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            PrintAction.Print.PrintPdfFile(dataGridView3, name, "Rapport Payement", "Registre", "Opération(s)", LogIn.mycontrng, "Quitaye School", true);
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallTask();
        }
        bool first = true;
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

        private async Task CallTask()
        {
            var mode = FillModeAsync();
            var type = FillTypeAsync();
            var data = FillDataAsync();
            var tasklist = new List<Task> { mode, type, data };
            while (tasklist.Count > 0)
            {
                var finishedTask = await Task.WhenAny(tasklist);
                if (finishedTask == mode)
                {
                    cbxMode.DataSource = mode.Result;
                    cbxMode.DisplayMember = "Mode";
                    cbxMode.ValueMember = "Id";
                    cbxMode.Text = null;
                    first = false;
                }else if(finishedTask == type)
                {
                    cbxType.DataSource = type.Result;
                    cbxType.DisplayMember = "Type";
                    cbxType.ValueMember = "Id";
                    cbxType.Text = null;
                    second = false;
                }
                else if (finishedTask == data)
                {
                    dataGridView1.Columns.Clear();
                    if(data.Result.Tables.Item1.Rows.Count == 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Tableau Vide");
                        DataRow dr = dt.NewRow();
                        dr[0] = "Aucune donnée dans ce tableau !";
                        dt.Rows.Add(dr);
                        dataGridView1.DataSource = dt;
                        lblMontant.Text = "Montant Encaissé : " + data.Result.Montant.ToString("N0") + " FCFA , Montant Décaissé : " + data.Result.Quantité.ToString("N0") + " FCFA";
                    }
                    else
                    {
                        dataGridView1.DataSource = data.Result.Tables.Item1;
                        dataGridView2.DataSource = data.Result.Tables.Item2;
                        dataGridView3.DataSource = data.Result.Tables.Item1;
                        lblMontant.Text = "Montant Encaissé : " + data.Result.Montant.ToString("N0") + " FCFA , Montant Décaissé : " + data.Result.Quantité.ToString("N0") + " FCFA";

                        try
                        {
                            var selection = new DataGridViewCheckBoxColumn();
                            selection.Name = "Select";
                            selection.HeaderText = "Select";
                            selection.Width = 40;
                            AddColumns.Addcolumn(dataGridView1);
                            dataGridView1.Columns.Add(selection);
                            dataGridView1.Columns["Edit"].Visible = false;
                        }
                        catch (Exception)
                        {

                        }
                    }
                    
                }
                tasklist.Remove(finishedTask);
            }
        }
        public static Task<DataTable> FillTypeAsync()
        {
            return Task.Factory.StartNew(() => FillType());
        }
        private static DataTable FillType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Type");
            using (var donnée = new QuitayeContext())
            {
                var der = from d in donnée.tbl_payement group d by new { Type = d.Type }into gr select new { Id = gr.Key.Type, Type = gr.Key.Type, };
                foreach (var item in der)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Type;

                    dt.Rows.Add(dr);
                }
            }
            return dt;

        }


        private async Task CallSeach()
        {
            var result = await SearchPayementsAsync(cbxMode.Text, cbxType.Text);
            dataGridView1.Columns.Clear();
            if (result.Table.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau !";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                lblMontant.Text = "Montant Encaissé : " + result.Montant.ToString("N0") + " FCFA , Montant Décaissé : " + result.Quantité.ToString("N0") + " FCFA";
            }
            else
            {
                dataGridView1.DataSource = result.Tables.Item1;
                dataGridView2.DataSource = result.Tables.Item2;
                dataGridView3.DataSource = result.Tables.Item1;
                lblMontant.Text = "Montant Encaissé : " + result.Montant.ToString("N0") + " FCFA , Montant Décaissé : " + result.Quantité.ToString("N0") + " FCFA";

                try
                {
                    AddColumns.Addcolumn(dataGridView1);
                    dataGridView1.Columns["Edit"].Visible = false;
                }
                catch (Exception)
                {

                }
            }
        }
        private Task<MyTable> SearchPayementsAsync(string mode, string type)
        {
            return Task.Factory.StartNew(() => SearchPayements(mode, type));
        }
        private MyTable SearchPayements(string mode, string type)
        {
            MyTable table = new MyTable();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Désignation");
            dt.Columns.Add("Mode_Payement");
            dt.Columns.Add("Montant", typeof(decimal));
            dt.Columns.Add("Num_Opération");
            dt.Columns.Add("Type");
            dt.Columns.Add("Date");
            dt.Columns.Add("Date_Enregistrement");
            dt.Columns.Add("Auteur");
            using (var donnée = new QuitayeContext())
            {
                if(Principales.type_compte.Contains("Administrateur"))
                {
                    if (mode.Length > 0 && type.Length > 0)
                    {
                        var ds = from d in donnée.tbl_payement
                                 where d.Mode_Payement == mode && d.Type == type && (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value))
                                 && (d.Nature != "Virement")
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
                                     Date_Enregistrement = d.Date_Enregistrement,
                                     Auteur = d.Auteur,
                                 };

                        table.Montant = Convert.ToDecimal(ds.Where(x => x.Type == "Encaissement").Sum(x => x.Montant));
                        table.Quantité = Convert.ToDecimal(ds.Where(x => x.Type == "Décaissement").Sum(x => x.Montant));

                        foreach (var item in ds)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.Id;
                            dr[1] = item.Désignation;
                            dr[2] = item.Mode_Payement;
                            dr[3] = item.Montant;
                            dr[4] = item.Num_Opération;
                            dr[5] = item.Type;
                            dr[6] = item.Date.Value.ToString("dd/MM/yyyy");
                            dr[7] = item.Date_Enregistrement.Value.ToString("dd/MM/yyyy hh:mm:ss");
                            dr[8] = item.Auteur;

                            dt.Rows.Add(dr);
                        }
                    }
                    else if (mode.Length > 0 && type.Length == 0)

                    {
                        var ds = from d in donnée.tbl_payement
                                 where d.Mode_Payement == mode && (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value)) && (d.Nature != "Virement")
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
                                     Date_Enregistrement = d.Date_Enregistrement,
                                     Auteur = d.Auteur,
                                 };

                        table.Montant = Convert.ToDecimal(ds.Where(x => x.Type == "Encaissement").Sum(x => x.Montant));
                        table.Quantité = Convert.ToDecimal(ds.Where(x => x.Type == "Décaissement").Sum(x => x.Montant));

                        foreach (var item in ds)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.Id;
                            dr[1] = item.Désignation;
                            dr[2] = item.Mode_Payement;
                            dr[3] = item.Montant;
                            dr[4] = item.Num_Opération;
                            dr[5] = item.Type;
                            dr[6] = item.Date.Value.ToString("dd/MM/yyyy");
                            dr[7] = item.Date_Enregistrement.Value.ToString("dd/MM/yyyy hh:mm:ss");
                            dr[8] = item.Auteur;

                            dt.Rows.Add(dr);
                        }
                    }
                    else if (mode.Length == 0 && type.Length > 0)
                    {
                        var ds = from d in donnée.tbl_payement
                                 where d.Type == type && (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value)) && (d.Nature != "Virement")
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
                                     Date_Enregistrement = d.Date_Enregistrement,
                                     Auteur = d.Auteur,
                                 };

                        table.Montant = Convert.ToDecimal(ds.Where(x => x.Type == "Encaissement").Sum(x => x.Montant));
                        table.Quantité = Convert.ToDecimal(ds.Where(x => x.Type == "Décaissement").Sum(x => x.Montant));

                        foreach (var item in ds)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.Id;
                            dr[1] = item.Désignation;
                            dr[2] = item.Mode_Payement;
                            dr[3] = item.Montant;
                            dr[4] = item.Num_Opération;
                            dr[5] = item.Type;
                            dr[6] = item.Date.Value.ToString("dd/MM/yyyy");
                            dr[7] = item.Date_Enregistrement.Value.ToString("dd/MM/yyyy hh:mm:ss");
                            dr[8] = item.Auteur;

                            dt.Rows.Add(dr);
                        }
                    }

                }
                else
                {
                    if (mode.Length > 0 && type.Length > 0)
                    {
                        var ds = from d in donnée.tbl_payement
                                 where d.Mode_Payement == mode && d.Type == type && (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value))
                                 && (d.Nature != "Virement" || d.Nature == null)
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
                                     Date_Enregistrement = d.Date_Enregistrement,
                                     Auteur = d.Auteur,
                                 };

                        table.Montant = Convert.ToDecimal(ds.Where(x => x.Type == "Encaissement").Sum(x => x.Montant));
                        table.Quantité = Convert.ToDecimal(ds.Where(x => x.Type == "Décaissement").Sum(x => x.Montant));

                        foreach (var item in ds)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.Id;
                            dr[1] = item.Désignation;
                            dr[2] = item.Mode_Payement;
                            dr[3] = item.Montant;
                            dr[4] = item.Num_Opération;
                            dr[5] = item.Type;
                            dr[6] = item.Date.Value.ToString("dd/MM/yyyy");
                            dr[7] = item.Date_Enregistrement.Value.ToString("dd/MM/yyyy hh:mm:ss");
                            dr[8] = item.Auteur;

                            dt.Rows.Add(dr);
                        }
                    }
                    else if (mode.Length > 0 && type.Length == 0)

                    {
                        var ds = from d in donnée.tbl_payement
                                 where d.Mode_Payement == mode && (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value)) && (d.Nature != "Virement" || d.Nature == null)
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
                                     Date_Enregistrement = d.Date_Enregistrement,
                                     Auteur = d.Auteur,
                                 };

                        table.Montant = Convert.ToDecimal(ds.Where(x => x.Type == "Encaissement").Sum(x => x.Montant));
                        table.Quantité = Convert.ToDecimal(ds.Where(x => x.Type == "Décaissement").Sum(x => x.Montant));

                        foreach (var item in ds)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.Id;
                            dr[1] = item.Désignation;
                            dr[2] = item.Mode_Payement;
                            dr[3] = item.Montant;
                            dr[4] = item.Num_Opération;
                            dr[5] = item.Type;
                            dr[6] = item.Date.Value.ToString("dd/MM/yyyy");
                            dr[7] = item.Date_Enregistrement.Value.ToString("dd/MM/yyyy hh:mm:ss");
                            dr[8] = item.Auteur;

                            dt.Rows.Add(dr);
                        }
                    }
                    else if (mode.Length == 0 && type.Length > 0)
                    {
                        var ds = from d in donnée.tbl_payement
                                 where d.Type == type && (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value)) && (d.Nature != "Virement" || d.Nature == null)
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
                                     Date_Enregistrement = d.Date_Enregistrement,
                                     Auteur = d.Auteur,
                                 };

                        table.Montant = Convert.ToDecimal(ds.Where(x => x.Type == "Encaissement").Sum(x => x.Montant));
                        table.Quantité = Convert.ToDecimal(ds.Where(x => x.Type == "Décaissement").Sum(x => x.Montant));

                        foreach (var item in ds)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = item.Id;
                            dr[1] = item.Désignation;
                            dr[2] = item.Mode_Payement;
                            dr[3] = item.Montant;
                            dr[4] = item.Num_Opération;
                            dr[5] = item.Type;
                            dr[6] = item.Date.Value.ToString("dd/MM/yyyy");
                            dr[7] = item.Date_Enregistrement.Value.ToString("dd/MM/yyyy hh:mm:ss");
                            dr[8] = item.Auteur;

                            dt.Rows.Add(dr);
                        }
                    }

                }

                table.Table = dt;
                return table;
            }
        }

        private async Task CallSeach(string seach)
        {
            var result = await SearchPayementsAsync(txtsearch.Text);
            dataGridView1.Columns.Clear();
            if (result.Tables.Item1.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau !";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                lblMontant.Text = "Montant Encaissé : " + result.Montant.ToString("N0") + " FCFA , Montant Décaissé : " + result.Quantité.ToString("N0") + " FCFA";
            }
            else
            {
                dataGridView1.DataSource = result.Tables.Item1;
                dataGridView2.DataSource = result.Tables.Item2;
                dataGridView3.DataSource = result.Tables.Item1;
                lblMontant.Text = "Montant Encaissé : " + result.Montant.ToString("N0") + " FCFA , Montant Décaissé : " + result.Quantité.ToString("N0") + " FCFA";

                try
                {
                    AddColumns.Addcolumn(dataGridView1);
                    dataGridView1.Columns["Edit"].Visible = false;
                }
                catch (Exception)
                {

                }
            }
        }
        private Task<MyTable> SearchPayementsAsync(string search)
        {
            return Task.Factory.StartNew(() => SearchPayements(search));
        }
        private MyTable SearchPayements(string search)
        {
            MyTable table = new MyTable();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Désignation");
            dt.Columns.Add("Mode_Payement");
            dt.Columns.Add("Montant");
            dt.Columns.Add("Num_Opération");
            dt.Columns.Add("Type");
            dt.Columns.Add("Date");
            dt.Columns.Add("Date_Enregistrement");
            dt.Columns.Add("Auteur");

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Id", typeof(int));
            dt2.Columns.Add("Désignation");
            dt2.Columns.Add("Mode_Payement");
            dt2.Columns.Add("Montant", typeof(decimal));
            dt2.Columns.Add("Num_Opération");
            dt2.Columns.Add("Type");
            dt2.Columns.Add("Date", typeof(DateTime));
            dt2.Columns.Add("Date_Enregistrement", typeof(DateTime));
            dt2.Columns.Add("Auteur");

            using (var donnée = new QuitayeContext())
            {
                if(Principales.type_compte.Contains("Administrateur"))
                {
                    var ds = from d in donnée.tbl_payement
                             where (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value)) &&
                             (d.Mode_Payement.Contains(search) || d.Type.Contains(search) 
                             || d.Auteur.Contains(search) || d.Commentaire.Contains(search)
                             || d.Num_Opération.Contains(search) || d.Client.Contains(search)) && (d.Nature != "Virement")
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
                                 Date_Enregistrement = d.Date_Enregistrement,
                                 Auteur = d.Auteur,
                             };

                    table.Montant = Convert.ToDecimal(ds.Where(x => x.Type == "Encaissement").Sum(x => x.Montant));
                    table.Quantité = Convert.ToDecimal(ds.Where(x => x.Type == "Décaissement").Sum(x => x.Montant));

                    foreach (var item in ds)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = item.Id;
                        dr[1] = item.Désignation;
                        dr[2] = item.Mode_Payement;
                        dr[3] = Convert.ToDecimal(item.Montant).ToString("N0");
                        dr[4] = item.Num_Opération;
                        dr[5] = item.Type;
                        dr[6] = item.Date.Value.ToString("dd/MM/yyyy");
                        dr[7] = item.Date_Enregistrement.Value.ToString("dd/MM/yyyy hh:mm:ss");
                        dr[8] = item.Auteur;

                        dt.Rows.Add(dr);

                        DataRow dr2 = dt2.NewRow();
                        dr2[0] = item.Id;
                        dr2[1] = item.Désignation;
                        dr2[2] = item.Mode_Payement;
                        dr2[3] = item.Montant;
                        dr2[4] = item.Num_Opération;
                        dr2[5] = item.Type;
                        dr2[6] = Convert.ToDateTime(item.Date.Value);
                        dr2[7] = Convert.ToDateTime(item.Date_Enregistrement.Value);
                        dr2[8] = item.Auteur;

                        dt2.Rows.Add(dr2);
                    }
                }else
                {
                    var ds = from d in donnée.tbl_payement
                             where (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value)) && (d.Nature != "Virement" || d.Nature == null) &&
                             (d.Mode_Payement.Contains(search) || d.Type.Contains(search) || d.Auteur.Contains(search)|| 
                             d.Num_Opération.Contains(search) || d.Client.Contains(search)) 
                             && (d.Nature != "Virement" || d.Nature == null)
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
                                 Date_Enregistrement = d.Date_Enregistrement,
                                 Auteur = d.Auteur,
                             };

                    table.Montant = Convert.ToDecimal(ds.Where(x => x.Type == "Encaissement").Sum(x => x.Montant));
                    table.Quantité = Convert.ToDecimal(ds.Where(x => x.Type == "Décaissement").Sum(x => x.Montant));

                    foreach (var item in ds)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = item.Id;
                        dr[1] = item.Désignation;
                        dr[2] = item.Mode_Payement;
                        dr[3] = Convert.ToDecimal(item.Montant).ToString("N0");
                        dr[4] = item.Num_Opération;
                        dr[5] = item.Type;
                        dr[6] = item.Date.Value.ToString("dd/MM/yyyy");
                        dr[7] = item.Date_Enregistrement.Value.ToString("dd/MM/yyyy hh:mm:ss");
                        dr[8] = item.Auteur;

                        dt.Rows.Add(dr);

                        DataRow dr2 = dt2.NewRow();
                        dr2[0] = item.Id;
                        dr2[1] = item.Désignation;
                        dr2[2] = item.Mode_Payement;
                        dr2[3] = item.Montant;
                        dr2[4] = item.Num_Opération;
                        dr2[5] = item.Type;
                        dr2[6] = Convert.ToDateTime(item.Date.Value);
                        dr2[7] = Convert.ToDateTime(item.Date_Enregistrement.Value);
                        dr2[8] = item.Auteur;

                        dt2.Rows.Add(dr2);
                    }
                }
                    
                table.Tables = (dt, dt2);
                return table;
            }
        }


        Timer loadTimer = new Timer();
        private async Task CallData()
        {
            var result = await FillDataAsync();
            dataGridView1.Columns.Clear();
            if (result.Tables.Item1.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau !";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                lblMontant.Text = "Montant Encaissé : " + result.Montant.ToString("N0") + " FCFA , Montant Décaissé : " + result.Quantité.ToString("N0") + " FCFA";
            }
            else
            {
                dataGridView1.DataSource = result.Tables.Item1;
                dataGridView2.DataSource = result.Tables.Item2;
                dataGridView3.DataSource = result.Tables.Item1;
                lblMontant.Text = "Montant Encaissé : " + result.Montant.ToString("N0") + " FCFA , Montant Décaissé : " + result.Quantité.ToString("N0") + " FCFA";

                try
                {
                    var selection = new DataGridViewCheckBoxColumn();
                    selection.Name = "Select";
                    selection.HeaderText = "Select";
                    selection.Width = 40;
                    AddColumns.Addcolumn(dataGridView1);
                    dataGridView1.Columns.Add(selection);
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
            dt.Columns.Add("Montant");
            dt.Columns.Add("Num_Opération");
            dt.Columns.Add("Type");
            dt.Columns.Add("Date");
            dt.Columns.Add("Date_Enregistrement");
            dt.Columns.Add("Auteur");

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Id", typeof(int));
            dt2.Columns.Add("Désignation");
            dt2.Columns.Add("Mode_Payement");
            dt2.Columns.Add("Montant", typeof(decimal));
            dt2.Columns.Add("Num_Opération");
            dt2.Columns.Add("Type");
            dt2.Columns.Add("Date", typeof(DateTime));
            dt2.Columns.Add("Date_Enregistrement", typeof(DateTime));
            dt2.Columns.Add("Auteur");

            using (var donnée = new QuitayeContext())
            {
                if(Principales.type_compte.Contains("Administrateur"))
                {
                    var des = (from d in donnée.tbl_payement
                              where (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value)) && (d.Nature != "Virement")
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
                                  Date_Enregistrement = d.Date_Enregistrement,
                                  Auteur = d.Auteur,
                              }).ToList();
                    table.Montant = Convert.ToDecimal(des.Where(x => x.Type == "Encaissement").Sum(x => x.Montant));
                    table.Quantité = Convert.ToDecimal(des.Where(x => x.Type == "Décaissement").Sum(x => x.Montant));

                    foreach (var item in des)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = item.Id;
                        dr[1] = item.Désignation;
                        dr[2] = item.Mode_Payement;
                        dr[3] = Convert.ToDecimal(item.Montant).ToString("N0");
                        dr[4] = item.Num_Opération;
                        dr[5] = item.Type;
                        dr[6] = item.Date.Value.ToString("dd/MM/yyyy");
                        dr[7] = item.Date_Enregistrement.Value.ToString("dd/MM/yyyy hh:mm:ss");
                        dr[8] = item.Auteur;

                        dt.Rows.Add(dr);

                        DataRow dr2 = dt2.NewRow();
                        dr2[0] = item.Id;
                        dr2[1] = item.Désignation;
                        dr2[2] = item.Mode_Payement;
                        dr2[3] = item.Montant;
                        dr2[4] = item.Num_Opération;
                        dr2[5] = item.Type;
                        dr2[6] = Convert.ToDateTime(item.Date.Value);
                        dr2[7] = Convert.ToDateTime(item.Date_Enregistrement.Value);
                        dr2[8] = item.Auteur;

                        dt2.Rows.Add(dr2);
                    }
                }else
                {
                    var des = from d in donnée.tbl_payement
                              where (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value)) && (d.Nature != "Virement" || d.Nature == null)
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
                                  Date_Enregistrement = d.Date_Enregistrement,
                                  Auteur = d.Auteur,
                              };
                    table.Montant = Convert.ToDecimal(des.Where(x => x.Type == "Encaissement").Sum(x => x.Montant));
                    table.Quantité = Convert.ToDecimal(des.Where(x => x.Type == "Décaissement").Sum(x => x.Montant));

                    foreach (var item in des)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = item.Id;
                        dr[1] = item.Désignation;
                        dr[2] = item.Mode_Payement;
                        dr[3] = Convert.ToDecimal(item.Montant).ToString("N0");
                        dr[4] = item.Num_Opération;
                        dr[5] = item.Type;
                        dr[6] = item.Date.Value.ToString("dd/MM/yyyy");
                        dr[7] = item.Date_Enregistrement.Value.ToString("dd/MM/yyyy hh:mm:ss");
                        dr[8] = item.Auteur;

                        dt.Rows.Add(dr);

                        DataRow dr2 = dt2.NewRow();
                        dr2[0] = item.Id;
                        dr2[1] = item.Désignation;
                        dr2[2] = item.Mode_Payement;
                        dr2[3] = item.Montant;
                        dr2[4] = item.Num_Opération;
                        dr2[5] = item.Type;
                        dr2[6] = Convert.ToDateTime(item.Date.Value);
                        dr2[7] = Convert.ToDateTime(item.Date_Enregistrement.Value);
                        dr2[8] = item.Auteur;

                        dt2.Rows.Add(dr2);
                    }
                }
                
                table.Tables = (dt, dt2);
                return table;
            }
        }
    }
}
