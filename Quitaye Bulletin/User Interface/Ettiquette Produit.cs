using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace Quitaye_School.User_Interface
{
    public partial class Ettiquette_Produit : Form
    {
        public Timer LoadTimer { get; set; }
        public Ettiquette_Produit()
        {
            InitializeComponent();
            btnFermer.Click += BtnFermer_Click;
            LoadTimer = new Timer();
            LoadTimer.Enabled = false;
            LoadTimer.Interval = 10;
            LoadTimer.Start();
            LoadTimer.Tick += LoadTimer_Tick;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            dataGridView1.CellClick += DataGridView1_CellClick;
            dataGridView1.KeyPress += DataGridView1_KeyPress;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            btnSelectionTout.Click += BtnSelectionTout_Click;
            btnEnregistrer.Click += BtnEnregistrer_Click;
            btnImprimerBarcode.Click += BtnImprimerBarcode_Click;
        }

        private void DataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
                return;

        }

        private async void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Prix_Unité") == true)
            //{
            //    decimal prix = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["Prix_Unité"].Value);
            //    string barcode = dataGridView1.CurrentRow.Cells["Code_Barre"].Value.ToString();

            //    var result = await EditPriceAsync(barcode, prix);
            //    if (!result.Item1)
            //        Alert.SShow("Modification non effectué, veillez reessayer!", Alert.AlertType.Warning);
            //    //dataGridView1.CurrentRow = dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1];
            //    //await CallData();
            //}
            
        }

        private async void BtnImprimerBarcode_Click(object sender, EventArgs e)
        {
            if (dataGridView1.ColumnCount < 2)
                return;

            var list = new List<Quitaye_School.Models.Barcode>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["Select"].Value);
                var produit = row.Cells["Désignation"].Value.ToString();
                if (isSelected)
                {
                    var item = BarcodesList.Where(x => x.Id == Convert.ToInt32(row.Cells["Id"].Value)).FirstOrDefault();

                    list.Add(item);
                }
            }

            var file = $@"C:\Quitaye School\Barcode List {DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")}.pdf";
            Alert.SShow("Génération Barcode en cours.. Veillez-patientez !", Alert.AlertType.Info);
            await Task.Run(() => BarcodePrinter.PrintBarcodeOnLabelAsync(list, BarcodeFormat.CODE_128, file, 200, 70, list.Count));
            //await Task.Run(() => BarcodePrinter.PrintBarcodeOnLabelAsync(list, BarcodeFormat.CODE_128, file, new BarcodePaperSize { Width = 4.625f, Height = 4.625f }, list.Count));
            System.Diagnostics.Process.Start(file);
            Alert.SShow("Génération effectué avec succès.", Alert.AlertType.Sucess);
        }

        private async Task<(bool, string)> EditPriceAsync(string barcode, decimal prix_pièce)
        {
            using (var donnée = new QuitayeContext())
            {
                var prod = donnée.tbl_produits.Where(x => x.Barcode == barcode).FirstOrDefault();
                if (prod != null)
                {
                    prod.Prix_Petit = prix_pièce;
                    var formule = donnée.tbl_formule_mesure_vente.Where(x => x.Id == prod.Formule_Stockage).FirstOrDefault();
                    if (formule != null)
                    {
                        prod.Prix_Moyen = prix_pièce * formule.Petit;
                    }
                    await donnée.SaveChangesAsync();
                }
                return (true, "Sucess");
            }
        }


        public List<Models.Barcode> BarcodesList { get; set; }
        private async void BtnEnregistrer_Click(object sender, EventArgs e)
        {
            if (dataGridView1.ColumnCount < 2)
                return;

            var list = new List<Quitaye_School.Models.Barcode>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["Select"].Value);
                var produit = row.Cells["Désignation"].Value.ToString();
                if (isSelected)
                {
                    var item = BarcodesList.Where(x => x.Id == Convert.ToInt32(row.Cells["Id"].Value)).FirstOrDefault();

                    list.Add(item);
                }
            }

            var file = $@"C:\Quitaye School\Etiquette List {DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")}.pdf";
            Alert.SShow("Génération Ettiquette en cours.. Veillez-patientez !", Alert.AlertType.Info);
            await Task.Run(() => BarcodePrinter.PrintEtiquetteAsync(list, BarcodeFormat.CODE_128, file));
            System.Diagnostics.Process.Start(file);
            Alert.SShow("Génération effectué avec succès.", Alert.AlertType.Sucess);
        }

        private void BtnSelectionTout_Click(object sender, EventArgs e)
        {
            if (btnSelectionTout.Text == "Tout Selectionner")
            {
                Ids.Clear();
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    dataGridView1.Rows[item.Index].Cells["Select"].Value = true;
                    var id = dataGridView1.Rows[item.Index].Cells["Id"].Value.ToString();
                    Ids.Add(id);
                }

                btnSelectionTout.Text = "Tout Deselectionner";
                lblSelectedQuantité.Text = "Quantité Selectionné : " + Ids.Count.ToString("N0");

            }
            else if (btnSelectionTout.Text == "Tout Deselectionner")
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    dataGridView1.Rows[item.Index].Cells["Select"].Value = false;
                    var id = dataGridView1.Rows[item.Index].Cells["Id"].Value.ToString();
                    Ids.Remove(id);
                }

                btnSelectionTout.Text = "Tout Selectionner";
                lblSelectedQuantité.Text = "Quantité Selectionné : " + Ids.Count.ToString("N0");
            }
        }

        public List<string> Ids { get; set; } = new List<string>();

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 1)
            {
                var code = dataGridView1.CurrentRow.Cells[1].Value as string;
                var barcode = BarcodesList.Where(x => x.BarcodeText == code).FirstOrDefault();
                //var code_barre = new Code_Barre(barcode);
                //code_barre.ShowDialog();
            }else if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Select"))
            {
                var id = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
                if (Ids.Contains(id))
                {
                    Ids.Remove(id);
                    dataGridView1.CurrentRow.Cells["Select"].Value = false;
                }
                else
                {
                    Ids.Add(id);
                    dataGridView1.CurrentRow.Cells["Select"].Value = true;
                }
               
                lblSelectedQuantité.Text = "Quantité Selectionné : " + Ids.Count.ToString("N0");
            }
        }

        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                await CallData(txtSearch.Text);
            }
            else await CallData();
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            LoadTimer.Stop();
            await CallTask();
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task CallTask()
        {
            var filldata = FillDataAsync();

            var tasklist = new List<Task>() { filldata };
            while(tasklist.Count > 0)
            {
                var current = await Task.WhenAny(tasklist);
                if(current == filldata)
                {
                    dataGridView1.Columns.Clear();
                    if (filldata.Result.Item1.Rows.Count == 0)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Tableau Vide");
                        DataRow dr = dt.NewRow();
                        dr[0] = "Aucune donnée dans ce tableau";
                        dt.Rows.Add(dr);
                        dataGridView1.DataSource = dt;
                        lblSelectedQuantité.Text = "Quantité Selectionné : " + Ids.Count.ToString("N0");
                    }
                    else
                    {
                        dataGridView1.DataSource = filldata.Result.Item1;
                        BarcodesList = filldata.Result.Item2;
                        var selection = new DataGridViewCheckBoxColumn();
                        selection.Name = "Select";
                        selection.HeaderText = "Select";
                        dataGridView1.Columns.Add(selection);
                        dataGridView1.Columns["Id"].Visible = false;
                        lblSelectedQuantité.Text = "Quantité Selectionné : " + Ids.Count.ToString("N0");
                    }
                }

                tasklist.Remove(current);
            }
        }
        private async Task CallData()
        {
            var result = await FillDataAsync();
            dataGridView1.Columns.Clear();
            if (result.Item1.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                lblSelectedQuantité.Text = "Quantité Selectionné : " + Ids.Count.ToString("N0");
            }
            else
            {
                dataGridView1.DataSource = result.Item1;
                BarcodesList = result.Item2;
                var selection = new DataGridViewCheckBoxColumn();
                selection.Name = "Select";
                selection.HeaderText = "Select";
                dataGridView1.Columns.Add(selection);
                dataGridView1.Columns["Id"].Visible = false;
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    var id = item.Cells["Id"].Value.ToString();
                    if (Ids.Contains(id))
                        dataGridView1.Rows[item.Index].Cells["Select"].Value = true;
                }
                lblSelectedQuantité.Text = "Quantité Selectionné : " + Ids.Count.ToString("N0");

            }
        }
        private Task<(DataTable, List<Models.Barcode>)> FillDataAsync() => Task.Factory.StartNew(() => FillData());
        private (DataTable, List<Models.Barcode>) FillData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Code_Barre");
            dt.Columns.Add("Désignation");
            
            dt.Columns.Add("Prix_Unité");
            dt.Columns.Add("Prix_Achat");
            dt.Columns.Add("Marge (%)");
            using(var donnée = new QuitayeContext())
            {
                var result = (from d in donnée.tbl_stock_produits_vente
                              where d.Code_Barre != null
                              && d.Code_Barre != ""
                              join p in donnée.tbl_produits on d.Product_Id
                              equals p.Id into joinedTable
                              from f in joinedTable.DefaultIfEmpty()
                              join ar in donnée.tbl_arrivée on d.Product_Id equals ar.Product_Id into joinedArrivée
                              from a in joinedArrivée.DefaultIfEmpty()
                              orderby d.Marque
                              select new
                              {
                                  Id = d.Id,
                                  Nom = d.Marque,
                                  Catégorie = d.Catégorie,
                                  Taille = d.Taille,
                                  Prix_Vente = f.Prix_Petit,
                                  Type = d.Type,
                                  Code_Barre = d.Code_Barre,
                                  Product_Id = d.Product_Id,
                                  Filiale = d.Detachement,
                                  Prix_Achat = a.Prix / a.Quantité,
                                  Désignation = d.Marque + " " + d.Catégorie + " " + d.Taille + " " + d.Type
                              }).Distinct().OrderBy(x => x.Désignation).ThenBy(x => x.Code_Barre).ToList().Select(x => new
                              {
                                  Id = x.Id,
                                  Nom = x.Nom,
                                  Catégorie = x.Catégorie,
                                  Taille = x.Taille,
                                  Code_Barre = string.Join(",", (from s in donnée.tbl_multi_barcode
                                                                 where s.Product_Id == x.Product_Id
                                                                 select s.Barcode)),
                                  Prix_Unité = x.Prix_Vente,
                                  Type = x.Type,
                                  Filiale = x.Filiale,
                                  Prix_Achat = x.Prix_Achat
                              }).ToList();

                var list = new List<Models.Barcode>();
                foreach (var item in result)
                {
                    list.Add(new Models.Barcode() { Id = Convert.ToInt32(item.Id), 
                        BarcodeText = item.Code_Barre, Marque = item.Nom, 
                        Catégorie = item.Catégorie, Taille = item.Taille, 
                        Type = item.Type, Price = item.Prix_Unité });
                    var row = dt.NewRow();
                    row["Code_Barre"] = item.Code_Barre;
                    row["Désignation"] = $"{item.Nom} {item.Catégorie} {item.Taille}-{item.Type}";
                    row["Id"] = item.Id;
                    var diff = (item.Prix_Unité - item.Prix_Achat);
                    var percent = Convert.ToDecimal(diff / item.Prix_Unité)*100;
                    row["Marge (%)"] = Math.Round(percent, 2);
                    row["Prix_Unité"] = item.Prix_Unité;
                    row["Prix_Achat"] = Convert.ToDecimal(item.Prix_Achat).ToString("N0");

                    dt.Rows.Add(row);
                }

                return (dt, list);
            }
        }

        private async Task CallData(string search)
        {
            var result = await FillDataAsync(search);
            dataGridView1.Columns.Clear();
            if (result.Item1.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                lblSelectedQuantité.Text = "Quantité Selectionné : " + Ids.Count.ToString("N0");
            }
            else
            {
                dataGridView1.DataSource = result.Item1;
                BarcodesList = result.Item2;
                var selection = new DataGridViewCheckBoxColumn();
                selection.Name = "Select";
                selection.HeaderText = "Select";
                dataGridView1.Columns.Add(selection);
                dataGridView1.Columns["Id"].Visible = false;
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    var id = item.Cells["Id"].Value.ToString();
                    if (Ids.Contains(id))
                        dataGridView1.Rows[item.Index].Cells["Select"].Value = true;
                }
                lblSelectedQuantité.Text = "Quantité Selectionné : " + Ids.Count.ToString("N0");

            }
        }
        private Task<(DataTable, List<Models.Barcode>)> FillDataAsync(string search) => Task.Factory.StartNew(() => FillData(search));
        private (DataTable, List<Models.Barcode>) FillData(string search)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Code_Barre");
            dt.Columns.Add("Désignation");
            dt.Columns.Add("Prix_Unité");
            dt.Columns.Add("Prix_Achat");
            dt.Columns.Add("Marge (%)");
            using (var donnée = new QuitayeContext())
            {
                var result = (from d in donnée.tbl_stock_produits_vente
                              where d.Code_Barre != null && d.Code_Barre != "" && (d.Code_Barre.Contains(search) 
                              || d.Marque.Contains(search) || d.Catégorie.Contains(search) 
                              || d.Detachement.Contains(search) || d.Taille.Contains(search) 
                              || d.Type.Contains(search))
                              join p in donnée.tbl_produits on d.Code_Barre equals p.Barcode into joinedTable
                              from f in joinedTable.DefaultIfEmpty()
                              join ar in donnée.tbl_arrivée on d.Product_Id equals ar.Product_Id into joinedProduct
                              from a in joinedProduct.DefaultIfEmpty()
                              orderby d.Marque
                              select new
                              {
                                  Id = d.Id,
                                  Nom = d.Marque,
                                  Catégorie = d.Catégorie,
                                  Taille = d.Taille,
                                  Code_Barre = d.Code_Barre,
                                  Type = d.Type,
                                  Filiale = d.Detachement,
                                  Prix_Unité = f.Prix_Petit,
                                  Prix_Achat = a.Prix / a.Quantité,
                                  Product_Id = d.Product_Id,
                                  Désignation = d.Marque + " " + d.Catégorie + " " + d.Taille + " " + d.Type
                              }).Distinct().OrderBy(x => x.Désignation).ThenBy(x => x.Code_Barre).ToList().Select(x => new
                              {
                                  Id = x.Id,
                                  Nom = x.Nom,
                                  Catégorie = x.Catégorie,
                                  Taille = x.Taille,
                                  Code_Barre = string.Join(",", (from s in donnée.tbl_multi_barcode
                                                                 where s.Product_Id == x.Product_Id
                                                                 select s.Barcode)),
                                  Prix_Unité = x.Prix_Unité,
                                  Type = x.Type,
                                  Filiale = x.Filiale,
                                  Prix_Achat = x.Prix_Achat
                              }).ToList();
                var list = new List<Models.Barcode>();
                foreach (var item in result)
                {
                    list.Add(new Models.Barcode()
                    {
                        Id = Convert.ToInt32(item.Id),
                        BarcodeText = item.Code_Barre,
                        Marque = item.Nom,
                        Catégorie = item.Catégorie,
                        Taille = item.Taille,
                        Type = item.Type,
                        Price = item.Prix_Unité
                    });
                    var row = dt.NewRow();
                    row["Code_Barre"] = item.Code_Barre;
                    row["Désignation"] = $"{item.Nom} {item.Catégorie} {item.Taille} - {item.Type}";
                    row["Id"] = item.Id;
                    var diff = (item.Prix_Unité - item.Prix_Achat);
                    var percent = Math.Round(Convert.ToDecimal(diff / item.Prix_Unité), 2)*100;
                    row["Marge (%)"] = percent;
                    row["Prix_Unité"] = item.Prix_Unité;
                    row["Prix_Achat"] = Convert.ToDecimal(item.Prix_Achat).ToString("N0");


                    dt.Rows.Add(row);
                }

                return (dt, list);
            }
        }
    }
}
