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
    public partial class Situation_Expiration : Form
    {
        public Timer LoadTimer { get; set; }
        public Situation_Expiration()
        {
            InitializeComponent();
            LoadTimer = new Timer();
            LoadTimer.Enabled = false;
            LoadTimer.Interval = 10;
            LoadTimer.Start();
            LoadTimer.Tick += LoadTimer_Tick;
            EndDate.Value = DateTime.Now.AddDays(120);
            startDate.Value = DateTime.Now.AddDays(-60);
            startDate.ValueChanged += StartDate_ValueChanged;
            EndDate.ValueChanged += StartDate_ValueChanged;
            btnExcel.Click += btnExcel_Click;
            btnPdf.Click += btnPDF_Click;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
        }

        private async void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Date_Expiration") == true)
            {
                var parts = dataGridView1.CurrentRow.Cells["Date_Expiration"].Value.ToString().Split('/');
                var day = Convert.ToInt32(parts[0]);
                var month = Convert.ToInt32(parts[1]);
                var year = Convert.ToInt32(parts[2]);
                DateTime date = new DateTime(year, month, day);
                string barcode = dataGridView1.CurrentRow.Cells["Barcode"].Value.ToString();
                var id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value.ToString());

                var result = await EditDateAsync(barcode, id, date);
                if (!result.Item1)
                    Alert.SShow("Modification non effectué, veillez reessayer!", Alert.AlertType.Warning);
                //dataGridView1.CurrentRow = dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1];
                //await CallData();
            }
        }

        private async Task<(bool, string)> EditDateAsync(string barcode, int id, DateTime date)
        {
            using (var donnée = new QuitayeContext())
            {
                
                var prod = donnée.tbl_expiration.Where(x => x.Code_Barre == barcode && x.Id == id).FirstOrDefault();
                
                if (prod != null)
                {
                    var oldDate = prod.Date_Expiration;
                    prod.Date_Expiration = date;
                    var arrivé = donnée.tbl_arrivée.Where(x => DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(oldDate) 
                    && x.Barcode == barcode).ToList();
                    foreach (var item in arrivé)
                    {
                        item.Date_Expiration = date;
                    }

                    var evolu = donnée.tbl_historique_evolution_stock.Where(x => DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(oldDate) && x.Barcode == barcode).ToList();
                    foreach (var item in evolu)
                    {
                        item.Date_Expiration = date;
                    }

                    var histo_ex = donnée.tbl_historique_expiration.Where(x => DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(oldDate) && x.Barcode == barcode).ToList();
                    foreach (var item in histo_ex)
                    {
                        item.Date_Expiration = date;
                    }

                    await donnée.SaveChangesAsync();
                }
                return (true, "Sucess");
            }
        }


        private async void btnExcel_Click(object sender, EventArgs e)
        {
            //var name = $"Situation expiration des produits {startDate.Value.Date.ToString("dd-MM-yyyy")}_{EndDate.Value.Date.ToString("dd-MM-yyyy")}_{DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")}";
            //Print.PrintExcelFile(this.dataGridView1, "Situation Expiration Stock", name, "Quitaye School");
            var file = $"C:/Quitaye School/Situation expiration des produits {startDate.Value.Date.ToString("dd-MM-yyyy")}_{EndDate.Value.Date.ToString("dd-MM-yyyy")}_{DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")}";
            //PrintAction.Print.PrintExcelFile(dataGridView2, "Rapport Commane(s)", name, "Quitaye School");
            Alert.SShow("Génération Barcode en cours.. Veillez-patientez !", Alert.AlertType.Info);
            await Task.Run(() => Quitaye_School.Models.Docuement.ExportToExcel(dataGridView1, file));
            System.Diagnostics.Process.Start(file);
            Alert.SShow("Génération effectué avec succès.", Alert.AlertType.Sucess);
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            var name = $"Situation expiration des produits {startDate.Value.Date.ToString("dd-MM-yyyy")}_{EndDate.Value.Date.ToString("dd-MM-yyyy")}_{DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")}";
            Print.PrintPdfFile(this.dataGridView1, name, "Situation d'expiration", "List", "Produit", LogIn.mycontrng, "Quitaye School", true);
        }

        private async void StartDate_ValueChanged(object sender, EventArgs e)
        {
            await ShowData();
        }

        private async Task ShowData()
        {
            if (!string.IsNullOrEmpty(txtsearch.Text))
            {
                await CallSearch(txtsearch.Text);
            }
            else
            {
                await CallData();
            }
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            LoadTimer.Stop();
            await ShowData();
        }

        private async Task CallData()
        {
            var result = await FillDataAsync();
            dataGridView1.Columns.Clear();
            if (result.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");

                var row = dt.NewRow();
                row["Tableau Vide"] = "Aucune donnée dans ce tableau !";

                dt.Rows.Add(row);
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = result;
            }
        }

        private Task<DataTable> FillDataAsync() => Task.Factory.StartNew(() => FillData());

        private DataTable FillData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Barcode");
            dt.Columns.Add("Marque");
            dt.Columns.Add("Catégorie");
            dt.Columns.Add("Taille");
            dt.Columns.Add("Type");
            
            using (var donnée = new QuitayeContext())
            {
                var items = (from d in donnée.tbl_expiration
                           where d.Reste > 0 && (d.Date_Expiration.Value <= EndDate.Value.Date
                           && d.Date_Expiration.Value >= startDate.Value.Date)
                           join p in donnée.tbl_stock_produits_vente on d.Code_Barre equals p.Code_Barre into joinedTable
                           from f in joinedTable.DefaultIfEmpty()
                           select new
                           {
                               Id = d.Id,
                               Barcode = f.Code_Barre,
                               Marque = f.Marque,
                               Catégorie = f.Catégorie,
                               Taille = f.Taille,
                               Q_Unité = d.Reste,
                               Date_Expiration = d.Date_Expiration.Value,
                               Formule = f.Formule,
                               Type = f.Type
                           }).ToList();

                var mesures = donnée.tbl_mesure_vente.OrderBy(d => d.Niveau).Select(d => new
                {
                    Mesure = d.Nom,
                    Type = d.Type,
                    Niveau = d.Niveau
                });
                foreach (var data in mesures)
                    dt.Columns.Add("Qté_" + data.Mesure);
                if (mesures.Count() == 0)
                    dt.Columns.Add("Quantité");

                dt.Columns.Add("Date_Expiration");

                foreach (var item in items)
                {
                    var formuleMesureVente = donnée.tbl_formule_mesure_vente
                        .Where(d => d.Id == item.Formule).First();

                    var row = dt.NewRow();
                    row["Id"] = item.Id;
                    row["Barcode"] = item.Barcode;
                    row["Marque"] = item.Marque;
                    row["Catégorie"] = item.Catégorie;
                    row["Taille"] = item.Taille;
                    row["Type"] = item.Type;

                    int num1 = 6;
                    decimal? nullable1;
                    decimal num2;
                    foreach (var data2 in mesures)
                    {
                        if (data2.Type == "Petit")
                            row[num1++] = item.Q_Unité;
                        else if (data2.Type == "Moyen")
                        {
                            nullable1 = formuleMesureVente.Moyen;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                decimal num3 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Moyen);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Q_Unité) / num3, 2);
                            }
                        }
                        else if (data2.Type == "Grand")
                        {
                            nullable1 = formuleMesureVente.Grand;
                            num2 = 0M;
                            int num4;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                nullable1 = formuleMesureVente.Moyen;
                                num4 = nullable1.HasValue ? 1 : 0;
                            }
                            else
                                num4 = 0;
                            if (num4 != 0)
                            {
                                decimal num5 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Grand);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Q_Unité) / num5, 3);
                            }
                        }
                        else if (data2.Type == "Large")
                        {
                            nullable1 = formuleMesureVente.Large;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                decimal num6 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Large);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Q_Unité) / num6, 4);
                            }
                        }
                        else if (data2.Type == "Hyper Large")
                        {
                            nullable1 = formuleMesureVente.Hyper_Large;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                decimal num7 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Hyper_Large);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Q_Unité) / num7, 5);
                            }
                        }
                    }

                    row[num1++] = item.Date_Expiration.ToString("dd/MM/yyyy");

                    dt.Rows.Add(row);
                }

                return dt;
            }
        }

        private async Task CallSearch(string search)
        {
            var result = await FillDataAsync(search);
            dataGridView1.Columns.Clear();
            if (result.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");

                var row = dt.NewRow();
                row["Tableau Vide"] = "Aucune donnée dans ce tableau !";

                dt.Rows.Add(row);
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = result;
            }
        }

        private Task<DataTable> FillDataAsync(string search) => Task.Factory.StartNew(() => FillData(search));

        private DataTable FillData(string search)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Barcode");
            dt.Columns.Add("Marque");
            dt.Columns.Add("Catégorie");
            dt.Columns.Add("Taille");
            dt.Columns.Add("Type");

            using (var donnée = new QuitayeContext())
            {
                var items = (from d in donnée.tbl_expiration
                             where d.Reste > 0 && (d.Date_Expiration.Value <= EndDate.Value.Date
                             && d.Date_Expiration.Value >= startDate.Value.Date)
                             join p in donnée.tbl_stock_produits_vente on d.Code_Barre equals p.Code_Barre into joinedTable
                             from f in joinedTable.DefaultIfEmpty()
                             where f.Marque.Contains(search) || f.Catégorie.Contains(search) 
                             || f.Code_Barre.ToLower().Equals(search.ToLower()) 
                             || f.Type.Contains(search) || f.Taille.Contains(search) 
                             || f.Detachement.Contains(search)
                             select new
                             {
                                 Id = d.Id,
                                 Barcode = f.Code_Barre,
                                 Marque = f.Marque,
                                 Catégorie = f.Catégorie,
                                 Taille = f.Taille,
                                 Q_Unité = d.Reste,
                                 Date_Expiration = d.Date_Expiration.Value,
                                 Formule = f.Formule,
                                 Type = f.Type
                             }).ToList();

                var mesures = donnée.tbl_mesure_vente.OrderBy(d => d.Niveau).Select(d => new
                {
                    Mesure = d.Nom,
                    Type = d.Type,
                    Niveau = d.Niveau
                });
                foreach (var data in mesures)
                    dt.Columns.Add("Qté_" + data.Mesure);
                if (mesures.Count() == 0)
                    dt.Columns.Add("Quantité");

                dt.Columns.Add("Date_Expiration");

                foreach (var item in items)
                {
                    var formuleMesureVente = donnée.tbl_formule_mesure_vente
                        .Where(d => d.Id == item.Formule).First();

                    var row = dt.NewRow();
                    row["Id"] = item.Id;
                    row["Barcode"] = item.Barcode;
                    row["Marque"] = item.Marque;
                    row["Catégorie"] = item.Catégorie;
                    row["Taille"] = item.Taille;
                    row["Type"] = item.Type;

                    int num1 = 6;
                    decimal? nullable1;
                    decimal num2;
                    foreach (var data2 in mesures)
                    {
                        if (data2.Type == "Petit")
                            row[num1++] = item.Q_Unité;
                        else if (data2.Type == "Moyen")
                        {
                            nullable1 = formuleMesureVente.Moyen;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                decimal num3 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Moyen);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Q_Unité) / num3, 2);
                            }
                        }
                        else if (data2.Type == "Grand")
                        {
                            nullable1 = formuleMesureVente.Grand;
                            num2 = 0M;
                            int num4;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                nullable1 = formuleMesureVente.Moyen;
                                num4 = nullable1.HasValue ? 1 : 0;
                            }
                            else
                                num4 = 0;
                            if (num4 != 0)
                            {
                                decimal num5 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Grand);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Q_Unité) / num5, 3);
                            }
                        }
                        else if (data2.Type == "Large")
                        {
                            nullable1 = formuleMesureVente.Large;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                decimal num6 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Large);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Q_Unité) / num6, 4);
                            }
                        }
                        else if (data2.Type == "Hyper Large")
                        {
                            nullable1 = formuleMesureVente.Hyper_Large;
                            num2 = 0M;
                            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
                            {
                                decimal num7 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Hyper_Large);
                                row[num1++] = Math.Round(Convert.ToDecimal(item.Q_Unité) / num7, 5);
                            }
                        }
                    }

                    row[num1++] = item.Date_Expiration.ToString("dd/MM/yyyy");

                    dt.Rows.Add(row);
                }

                return dt;
            }
        }
    }
}
