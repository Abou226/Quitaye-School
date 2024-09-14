using DocumentFormat.OpenXml.Wordprocessing;
using PrintAction;
using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using Quitaye_Medical.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Details_Achat : Form
    {

#pragma warning disable CS0169 // Le champ 'Details_Achat.type' n'est jamais utilisé
        private string type;
#pragma warning restore CS0169 // Le champ 'Details_Achat.type' n'est jamais utilisé
        public int id_client;
        private Timer loadTimer = new Timer();
        public static string ok;
        private Timer filename = new Timer();
        private static string name;
        private static string client;
#pragma warning disable CS0649 // Le champ 'Details_Achat.contact' n'est jamais assigné et aura toujours sa valeur par défaut null
        private static string contact;
#pragma warning restore CS0649 // Le champ 'Details_Achat.contact' n'est jamais assigné et aura toujours sa valeur par défaut null
        private static string num_achat;
        private SearchedTable result = new SearchedTable();

        public Details_Achat(string _num_achat)
        {
            InitializeComponent();
            btnFermer.Click += BtnFermer_Click;
            Details_Achat.num_achat = _num_achat;
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            btnImprimerFacture.Click += BtnImprimerFacture_Click;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            btnAjouter.Click += BtnAjouter_Click;
            btnAddPayement.Click += BtnAddPayement_Click;
            btnValider.Click += BtnValider_Click;
            txtmontant.TextChanged += Txtmontant_TextChanged;
            btnExcel.Click += BtnExcel_Click;
            txtmontant.KeyPress += Txtmontant_KeyPress;
            btnClotureEcriture.Click += BtnClotureEcriture_Click;
            btnSelectionTout.Click += BtnSelectionTout_Click;
            dataGridView1.CellClick += DataGridView1_CellClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            dataGridView1.KeyPress += DataGridView1_KeyPress;
            btnRetournerAchat.Click += BtnRetournerAchat_Click;
        }

        private async void BtnRetournerAchat_Click(object sender, EventArgs e)
        {
            AchatVente vente = new AchatVente(num_achat, client, contact, "Achat", true);
            vente.panel2.Visible = true;
            vente.panel6.Visible = true;
            vente.panel4.Visible = true;
            vente.panel5.Visible = true;
            vente.btnFermer.Visible = true;
            vente.ShowDialog();
            if (!(ok == "Oui"))
            {
                vente = null;
            }
            else
            {
                ok = "Oui";
                ok = null;
                await CallData();
                vente = null;
            }
        }

        private async void BtnExcel_Click(object sender, EventArgs e)
        {
            //name = $"Rapport Achat {num_achat} {DateTime.Now.Date.ToString("dd-MM-yyyy HH.mm.ss")}.xlsx";
            //Print.PrintExcelFile(dataGridView2, "Rapport " + num_achat, name, "Quitaye School");
            string file = $"C:/Quitaye School/Rapport Achat {num_achat} {DateTime.Now.Date.ToString("dd-MM-yyyy HH.mm.ss")}.xlsx";
            //PrintAction.Print.PrintExcelFile(dataGridView2, "Rapport Commane(s)", name, "Quitaye School");
            Alert.SShow("Génération Barcode en cours.. Veillez-patientez !", Alert.AlertType.Info);
            await Task.Run(() => Quitaye_School.Models.Docuement.ExportToExcel(this.dataGridView2, file));
            System.Diagnostics.Process.Start(file);
            Alert.SShow("Génération effectué avec succès.", Alert.AlertType.Sucess);
        }

        private async void DataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '\b')
            {
                return;
            }
            else
            {
                if (e.KeyChar != '\r')
                    return;
                e.Handled = true;

            }
            e.Handled = true;
        }

        private async void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    int _qté = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Quantité"].Value);
                    decimal prix = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["Prix_Unité"].Value);
                    dataGridView1.CurrentRow.Cells["Montant"].Value = (_qté * prix).ToString("N0");
                }
                else if (e.ColumnIndex == 4)
                {
                    int _qté = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Quantité"].Value);
                    decimal prix = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["Prix_Unité"].Value);
                    dataGridView1.CurrentRow.Cells["Montant"].Value = (_qté * prix).ToString("N0");
                    //dataGridView1.CurrentRow.Cells["Prix_Unité"].Value = (prix).ToString("N0");
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                    decimal montant = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["Montant"].Value);
                    //MsgBox msg = new MsgBox();
                    //msg.show("Voulez-vous appliquer cette modification ?", "Modification", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                    //msg.ShowDialog();
                    //if (msg.clicked == "Oui")
                    {
                        var result = await EditAchatAsync(id, 0, montant);
                        if (!result.Item1)
                            Alert.SShow(result.Item2, Alert.AlertType.Warning);
                    }
                }
                else if (e.ColumnIndex == 5)
                {
                    int _qté = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Quantité"].Value);
                    decimal prix = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["Prix_Unité"].Value);
                    //dataGridView1.CurrentRow.Cells["Prix_Unité"].Value = (prix).ToString("N0");
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                    decimal montant = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["Montant"].Value);
                    //MsgBox msg = new MsgBox();
                    //msg.show("Voulez-vous appliquer cette modification ?", "Modification", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                    //msg.ShowDialog();
                    //if (msg.clicked == "Oui")
                    {
                        var result = await EditAchatAsync(id, _qté, montant);
                        if (!result.Item1)
                            Alert.SShow(result.Item2, Alert.AlertType.Warning);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async Task<(bool, string)> EditAchatAsync(int id, int qté = 0, decimal montant = 0)
        {
            using (var donnée = new QuitayeContext())
            {
                var data = donnée.tbl_arrivée.Where(x => x.Id == id
                && x.Cloturé != "Oui").FirstOrDefault();
                var date = DateTime.Now.AddDays(-30);
                if (data != null)
                {
                    if (data.Date_Arrivée.Value.Date >= date)
                    {
                        if (qté != 0)
                        {
                            var prod = donnée.tbl_stock_produits_vente.Where(x => x.Code_Barre == data.Barcode
                            && x.Detachement == "Siège").FirstOrDefault();
                            prod.Quantité -= data.Q_Unité;
                            prod.Quantité += qté;
                            //await donnée.SaveChangesAsync();
                            var histo = donnée.tbl_historique_valeur_stock.Where(x => x.Code_Barre == data.Barcode
                            && DbFunctions.TruncateTime(x.Date) >= DbFunctions.TruncateTime(data.Date_Arrivée)
                              && x.Filiale == "Siège").Select(x => x).ToList();

                            foreach (var item in histo)
                            {
                                item.Quantité -= data.Q_Unité;
                                item.Quantité += qté;
                                item.Prix_Petit = Math.Round(Convert.ToDecimal(data.Prix / data.Quantité), 2);
                            }

                            var evolution = donnée.tbl_historique_evolution_stock.Where(x => x.Barcode == data.Barcode
                                                && DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(data.Date_Expiration)
                                                && DbFunctions.TruncateTime(x.Date_Achat) == DbFunctions.TruncateTime(data.Date_Arrivée)
                                                && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(data.Date_Arrivée)).FirstOrDefault();
                            if (evolution != null)
                            {
                                evolution.Quantité -= data.Q_Unité;
                                evolution.Quantité += qté;
                                evolution.Prix_Achat_Petit = Math.Round(Convert.ToDecimal(data.Prix / data.Quantité), 2);
                            }

                            var evo_list = donnée.tbl_historique_evolution_stock.Where(x => x.Barcode == data.Barcode
                                                && DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(data.Date_Expiration)
                                                && DbFunctions.TruncateTime(x.Date_Achat) == DbFunctions.TruncateTime(data.Date_Arrivée)
                                                && DbFunctions.TruncateTime(x.Date) > DbFunctions.TruncateTime(data.Date_Arrivée)).ToList();
                            foreach (var evo in evo_list)
                            {
                                evo.Quantité -= data.Q_Unité;
                                evo.Quantité += qté;
                                evo.Prix_Achat_Petit = Math.Round(Convert.ToDecimal(data.Prix / data.Quantité), 2);
                            }

                            var der = donnée.tbl_expiration.Where(x => x.Code_Barre == data.Barcode
                            && DbFunctions.TruncateTime(x.Date) >= DbFunctions.TruncateTime(data.Date_Arrivée)
                              && x.Filiale == "Siège").Select(x => x).ToList();

                            foreach (var item in der)
                            {
                                item.Quantité -= data.Q_Unité;
                                item.Reste -= data.Q_Unité;
                                item.Quantité += qté;
                                item.Reste += qté;
                            }

                            var derex = donnée.tbl_historique_expiration.Where(x => x.Barcode == data.Barcode
                            && DbFunctions.TruncateTime(x.Date) >= DbFunctions.TruncateTime(data.Date_Arrivée)
                              && x.Filiale == "Siège").Select(x => x).ToList();

                            foreach (var item in derex)
                            {
                                item.Quantité -= data.Q_Unité;
                                item.Quantité += qté;
                            }
                            data.Q_Unité = qté;
                            data.Quantité = qté;
                        }
                        if (montant != 0)
                        {
                            data.Prix = montant;
                        }
                        await donnée.SaveChangesAsync();
                        return (true, "Succèss");
                    }
                    else return (false, "Impossible, delais de modification epuisé");
                }
                else return (false, "Element cloturé, modification impossible");
            }
        }
        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
                await CallSearchData(txtSearch.Text);
            else await CallData();
        }

        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Sup") == true)
                {
                    //string auteur = dataGridView1.CurrentRow.Cells["Auteur"].Value.ToString();
                    if (!string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells["Id"].Value.ToString()))
                    {
                        //if (Principales.type_compte.Contains("Administrateur"))
                        //{
                        //    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                        //    MsgBox msg = new MsgBox();
                        //    msg.show("Voulez-vous supprimer cet achat ?", "Suppression",
                        //        MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                        //    msg.ShowDialog();
                        //    if (msg.clicked == "Non")

                        //        return;
                        //    else if (msg.clicked == "Oui")
                        //    {
                        //        using (var donnée = new QuitayeContext())
                        //        {
                        //            var valeur = await donnée.tbl_arrivée.Where(x => x.Id == id).FirstOrDefaultAsync();
                        //            //if (v.Date_Arrivée.Value.AddHours(168) >= DateTime.Now)
                        //            if (valeur.Cloturé != "Oui")
                        //            {
                        //                var hist = await donnée.tbl_historique_expiration
                        //                                        .Where(x => x.Id_Opération == valeur.Id
                        //                                        && x.Num_Opération == valeur.Num_Achat).ToListAsync();

                        //                foreach (var item in hist)
                        //                {
                        //                    var expiration = donnée.tbl_expiration.Where(x => DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(item.Date_Expiration)
                        //                    && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(item.Date)
                        //                    && x.Code_Barre == valeur.Barcode).FirstOrDefault();
                        //                    donnée.tbl_expiration.Remove(expiration);
                        //                }

                        //                donnée.tbl_historique_expiration.RemoveRange(hist);

                        //                donnée.tbl_arrivée.Remove(valeur);

                        //                var ms = (from d in donnée.tbl_mesure_vente where d.Nom == valeur.Mesure select d).First();
                        //                var stock = (from d in donnée.tbl_stock_produits_vente where d.Code_Barre == valeur.Barcode select d).First();
                        //                var formu = (from d in donnée.tbl_formule_mesure_vente where d.Id == stock.Formule select d).First();
                        //                if (ms.Niveau == 1)
                        //                {
                        //                    stock.Quantité -= valeur.Quantité;
                        //                }
                        //                else if (ms.Niveau == 2)
                        //                {
                        //                    decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Moyen);
                        //                    stock.Quantité -= Convert.ToDecimal(valeur.Quantité) * unité;
                        //                }
                        //                else if (ms.Niveau == 3)
                        //                {
                        //                    decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Grand);
                        //                    stock.Quantité -= Convert.ToDecimal(valeur.Quantité) * unité;
                        //                }
                        //                else if (ms.Niveau == 4)
                        //                {
                        //                    decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Large);
                        //                    stock.Quantité -= Convert.ToDecimal(valeur.Quantité) * unité;
                        //                }
                        //                else if (ms.Niveau == 5)
                        //                {
                        //                    decimal unité = Convert.ToDecimal(formu.Petit) / Convert.ToDecimal(formu.Hyper_Large);
                        //                    stock.Quantité -= Convert.ToDecimal(valeur.Quantité) * unité;
                        //                }

                        //                var historique = donnée.tbl_historique_valeur_stock.Where(x => x.Code_Barre == stock.Code_Barre
                        //                    && x.Filiale == stock.Detachement && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(valeur.Date_Arrivée)).FirstOrDefault();
                        //                if (historique != null)
                        //                {
                        //                    if (historique.Quantité == stock.Quantité)
                        //                    {
                        //                        donnée.tbl_historique_valeur_stock.Remove(historique);
                        //                    }
                        //                    else if (historique.Quantité > stock.Quantité)
                        //                    {
                        //                        historique.Quantité -= stock.Quantité;

                        //                    }
                        //                    var h = donnée.tbl_historique_valeur_stock.Where(x => x.Code_Barre == stock.Code_Barre
                        //                && x.Filiale == stock.Detachement
                        //                && DbFunctions.TruncateTime(x.Date) >= DbFunctions.TruncateTime(historique.Date) && x.Id != historique.Id).ToList();
                        //                    foreach (var item in h)
                        //                    {
                        //                        item.Quantité -= stock.Quantité;
                        //                    }
                        //                }

                        //                var evolution = donnée.tbl_historique_evolution_stock.Where(x => x.Barcode == stock.Code_Barre
                        //                            && DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(valeur.Date_Expiration)
                        //                            && DbFunctions.TruncateTime(x.Date_Achat) == DbFunctions.TruncateTime(valeur.Date_Arrivée)
                        //                            && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(valeur.Date_Arrivée)).FirstOrDefault();
                        //                evolution.Quantité -= valeur.Quantité;
                        //                var evo_list = donnée.tbl_historique_evolution_stock.Where(x => x.Barcode == stock.Code_Barre
                        //                && DbFunctions.TruncateTime(x.Date_Expiration) == DbFunctions.TruncateTime(valeur.Date_Expiration)
                        //                && DbFunctions.TruncateTime(x.Date_Achat) == DbFunctions.TruncateTime(valeur.Date_Arrivée)
                        //                && DbFunctions.TruncateTime(x.Date) > DbFunctions.TruncateTime(valeur.Date_Arrivée)).ToList();
                        //                foreach (var evo in evo_list)
                        //                {
                        //                    evo.Quantité -= valeur.Quantité;
                        //                }
                        //                await donnée.SaveChangesAsync();
                        //                await CallData();
                        //                Alert.SShow("Achat supprimeé avec succès.", Alert.AlertType.Sucess);
                        //            }
                        //            else
                        //            {
                        //                Alert.SShow("Suppression impossible, écriture cloturée.", Alert.AlertType.Info);
                        //            }
                        //        }
                        //    }
                        //}
                    }
                        
                }
            }
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
                {
                    if (!string.IsNullOrEmpty(row.Cells["Id"].Value.ToString()))
                    {
                        list.Add(Convert.ToInt32(row.Cells["Id"].Value));
                    }
                }
            }

            if (list.Count > 0)
            {
                var msg = new MsgBox();
                msg.show($"Voulez-vus réellement cloturé ce(s) {list.Count} élément(s) ?", "Cloture écriture",
                    MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                msg.ShowDialog();
                if (msg.clicked == "Oui")
                {
                    var result = await AchatRangeClotureAsync(list);
                    if (result)
                    {
                        Alert.SShow("Cloture éffectué avec succès.", Alert.AlertType.Sucess);
                    }
                }
            }
        }

        private async Task<bool> AchatSingleClotureAsync(int id)
        {
            using (var donnée = new QuitayeContext())
            {
                var item = donnée.tbl_arrivée.Where(x => x.Id == id).FirstOrDefault();
                item.Cloturé = "Oui";
                await donnée.SaveChangesAsync();
                return true;
            }
        }

        private async Task<bool> AchatRangeClotureAsync(List<int> ids)
        {
            using (var donnée = new QuitayeContext())
            {
                var items = donnée.tbl_arrivée.Where(x => ids.Contains(x.Id)).ToList();
                foreach (var item in items)
                {
                    item.Cloturé = "Oui";
                }
                await donnée.SaveChangesAsync();
                return true;
            }
        }

        private void Txtmontant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '\b')
                return;
            e.Handled = true;
        }

        private void Txtmontant_TextChanged(object sender, EventArgs e)
        {
            if (!(txtmontant.Text != "") || !(txtmontant.Text != "-"))
                return;
            txtmontant.Text = Convert.ToDecimal(txtmontant.Text).ToString("N0");
            txtmontant.SelectionStart = txtmontant.Text.Length;
        }

        private async void BtnValider_Click(object sender, EventArgs e)
        {
            if (!(txtmontant.Text != "") || !(cbxPayement.Text != ""))
                return;
            string result = Regex.Replace(txtmontant.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
            Payement payement = new Payement()
            {
                Montant = Convert.ToDecimal(result),
                Client = client,
                Date = DateOpération.Value,
                Reference = txtReference.Text,
                Mode_Payement = cbxPayement.Text
            };
            payement.Client = client;
            payement.Id_Client = id_client;
            payement.Num_Opération = num_achat;
            payement.Type = "Décaissement";
            payement.Raison = "Fournisseur";
            if (await SavePayementAsync(payement))
            {
                txtmontant.Text = null;
                txtReference.Text = null;
                await CallData();
                Alert.SShow("Payement enregistré avec succès.", Alert.AlertType.Sucess);
            }
            result = null;
            payement = null;
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallTask();
        }

        private async Task CallTask()
        {
            Task<MesureTable> mode = FillModeAsync();
            Task<SearchedTable> data = FillDataAsync();
            List<Task> tasklist = new List<Task>()
            {
                mode,
                data
            };
            while (tasklist.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(tasklist);
                if (finishedTask == mode)
                {
                    cbxPayement.DataSource = mode.Result.Table;
                    cbxPayement.DisplayMember = "Mode";
                    cbxPayement.ValueMember = "Id";
                    if (mode.Result.RowsLists != null && mode.Result.RowsLists.Count > 0)
                    {
                        foreach (RowsList rowsList in mode.Result.RowsLists)
                        {
                            RowsList item = rowsList;
                            if (item.Default)
                            {
                                cbxPayement.Text = item.Mesure;
                                break;
                            }
                            cbxPayement.Text = null;
                            item = null;
                        }
                    }
                    else
                        cbxPayement.Text = null;
                }
                else if (finishedTask == data)
                {
                    result = data.Result;

                    dataGridView1.DataSource = data.Result.MyTable;
                    dataGridView2.DataSource = data.Result.MyTable;

                    lblMontant.Text = "Payée : " + result.Payement.ToString("N0") + " FCFA, Réduction : " + result.Reduction.ToString("N0") + ", Restant : " + (result.Montant - result.Payement).ToString("N0") + " FCFA";
                    id_client = data.Result.Id_Client;
                    lblTitre.Text = "Fournisseur : " + data.Result.Client;

                    lblNum_Operation.Text = "N° Opération : " + num_achat;
                    lblNum_Operation.Visible = true;
                    lblTitre.Visible = true;
                    lblMontant.Visible = true;
                    try
                    {
                        if (Principales.type_compte.Contains("Administrateur"))
                        {
                            var selection = new DataGridViewCheckBoxColumn();
                            selection.Name = "Select";
                            selection.HeaderText = "Select";
                            selection.Width = 40;

                            dataGridView1.Columns.Add(selection);

                            AddColumns.Addcolumn(dataGridView1);
                            dataGridView1.Columns["Id"].Width = 40;
                            dataGridView1.Columns["Barcode"].Width = 70;
                            dataGridView1.Columns["Quantité"].Width = 70;
                            dataGridView1.Columns["Prix_Unité"].Width = 90;
                            dataGridView1.Columns["Select"].Width = 50;
                            dataGridView1.Columns["Sup"].Width = 40;
                            dataGridView1.Columns["Montant"].Width = 120;
                            dataGridView1.Columns["Edit"].Visible = false;
                        }

                    }
                    catch (Exception)
                    {

                    }
                }
                tasklist.Remove(finishedTask);
                finishedTask = null;
            }
            mode = null;
            data = null;
            tasklist = null;
        }

        private async void BtnAddPayement_Click(object sender, EventArgs e)
        {
            Ajouter_Element element = new Ajouter_Element("Mode");
            int num = (int)element.ShowDialog();
            if (!(element.ok == "Oui"))
            {
                element = null;
            }
            else
            {
                await CallMode();
                element = null;
            }
        }

        private async Task<bool> SavePayementAsync(Payement payement)
        {
            using (var financeDataContext = new QuitayeContext())
            {
                var source1 = financeDataContext.tbl_payement.Select(d => new
                {
                    Id = d.Id
                }).Take(1);
                int num = 1;
                if (source1.Count() != 0)
                {
                    var data = financeDataContext.tbl_payement.OrderByDescending((d => d.Id)).Select(d => new
                    {
                        Id = d.Id
                    }).First();
                    num = data.Id + 1;
                }
                var source2 = financeDataContext.tbl_vente.Where((d => d.Num_Vente == payement.Num_Opération)).Select(d => new
                {
                    Id = d.Id,
                    Montant = d.Montant
                });
                var source3 = financeDataContext.tbl_payement.Where((d => d.Num_Opération == payement.Num_Opération)).Select(d => new
                {
                    Id = d.Id,
                    Montant = d.Montant
                });
                var entity = new Models.Context.tbl_payement()
                {
                    Id = num,
                    Num_Opération = payement.Num_Opération,
                    Client = payement.Client,
                    Montant = new Decimal?(payement.Montant),
                    Num_Client = payement.Num_Client
                };
                entity.Num_Opération = payement.Num_Opération;
                entity.Date_Payement = new DateTime?(payement.Date);
                entity.Date_Enregistrement = new DateTime?(DateTime.Now);
                entity.Auteur = Principales.profile;
                entity.Réduction = new Decimal?(payement.Reduction);
                entity.Raison = payement.Raison;
                entity.Reference = payement.Reference;
                entity.Type = payement.Type;
                if (payement.Id_Client != 0)
                    entity.Compte_Tier = payement.Id_Client.ToString();
                entity.Mode_Payement = payement.Mode_Payement;
                entity.Nature = "Payement";
                entity.Commentaire = "Payement " + client + " " + payement.Num_Opération;
                financeDataContext.tbl_payement.Add(entity);
                decimal? nullable1 = source2.Sum(x => x.Montant);
                decimal? nullable2 = source3.Sum(x => x.Montant);
                decimal? montant = entity.Montant;
                decimal? nullable3 = nullable2.HasValue & montant.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + montant.GetValueOrDefault()) : new Decimal?();
                if (nullable1.GetValueOrDefault() <= nullable3.GetValueOrDefault() & nullable1.HasValue & nullable3.HasValue)
                {
                    foreach (var data in source2)
                    {
                        var item = data;
                        financeDataContext.tbl_vente.Where((d => d.Id == item.Id)).First().Type = "Payée";
                        await financeDataContext.SaveChangesAsync();
                    }
                }
                else
                {
                    foreach (var data in source2)
                    {
                        var item = data;
                        var tblVente = financeDataContext.tbl_vente.Where((d => d.Id == item.Id)).First();
                        tblVente.Type = !tblVente.Type.Contains("A Crédit") ? "Restant" : "A Crédit";
                        await financeDataContext.SaveChangesAsync();
                    }
                }
                return true;
            }
        }

        private async Task CallMode()
        {
            MesureTable result = await FillModeAsync();
            cbxPayement.DataSource = result.Table;
            cbxPayement.DisplayMember = "Mode";
            cbxPayement.ValueMember = "Id";
            if (result.RowsLists != null && result.RowsLists.Count > 0)
            {
                foreach (RowsList rowsList in result.RowsLists)
                {
                    RowsList item = rowsList;
                    if (item.Default)
                    {
                        cbxPayement.Text = item.Mesure;
                        break;
                    }
                    cbxPayement.Text = null;
                    item = null;
                }
                result = null;
            }
            else
            {
                cbxPayement.Text = null;
                result = null;
            }
        }

        public static Task<MesureTable> FillModeAsync() => Task.Factory.StartNew((() => FillMode()));

        private static MesureTable FillMode()
        {
            MesureTable mesureTable = new MesureTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Mode");
            using (var financeDataContext = new QuitayeContext())
            {
                if (Principales.type_compte.Contains("Administrateur"))
                {
                    var source = financeDataContext.tbl_mode_payement.OrderByDescending((d => d.Id)).Select(d => new
                    {
                        Id = d.Id,
                        Mode = d.Mode,
                        Default = d.Defaut
                    });
                    if (source.Count() != 0)
                    {
                        mesureTable.RowsLists = new List<RowsList>();
                        foreach (var data in source)
                        {
                            RowsList rowsList = new RowsList();
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            row[1] = data.Mode;
                            if (data.Default == "Oui")
                            {
                                rowsList.Default = true;
                                rowsList.Mesure = data.Mode;
                            }
                            else
                                rowsList.Default = false;
                            mesureTable.RowsLists.Add(rowsList);
                            dataTable.Rows.Add(row);
                        }
                    }
                }
                else
                {
                    var source = financeDataContext.tbl_mode_payement.Where((d => d.Niveau == "Utilisateur" || d.Niveau == default)).OrderByDescending((d => d.Id)).Select(d => new
                    {
                        Id = d.Id,
                        Mode = d.Mode,
                        Default = d.Defaut
                    });
                    if (source.Count() != 0)
                    {
                        mesureTable.RowsLists = new List<RowsList>();
                        foreach (var data in source)
                        {
                            RowsList rowsList = new RowsList();
                            DataRow row = dataTable.NewRow();
                            row[0] = data.Id;
                            row[1] = data.Mode;
                            if (data.Default == "Oui")
                            {
                                rowsList.Default = true;
                                rowsList.Mesure = data.Mode;
                            }
                            else
                                rowsList.Default = false;
                            mesureTable.RowsLists.Add(rowsList);
                            dataTable.Rows.Add(row);
                        }
                    }
                }
            }
            mesureTable.Table = dataTable;
            return mesureTable;
        }

        private async void BtnAjouter_Click(object sender, EventArgs e)
        {
            AchatVente vente = new AchatVente(num_achat, client, contact, "Achat");
            vente.panel2.Visible = true;
            vente.panel6.Visible = true;
            vente.panel4.Visible = true;
            vente.panel5.Visible = true;
            vente.btnFermer.Visible = true;
            vente.ShowDialog();
            if (!(ok == "Oui"))
            {
                vente = null;
            }
            else
            {
                ok = "Oui";
                ok = null;
                await CallData();
                vente = null;
            }
        }

        private void BtnImprimerFacture_Click(object sender, EventArgs e)
        {
            name = "Facture " + client + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            Print.PrintRecuPdfFile(dataGridView2, name, "Facture : " + client, "N° Opération", num_achat, LogIn.mycontrng, "Quitaye School", false, new Detail_Facture()
            {
                MontantTTC = result.Montant,
                Taxe = result.Montant / 100M * 18M,
                MontantHT = result.Montant
            }, false);
        }

        private void BtnFermer_Click(object sender, EventArgs e) => Close();

        private async Task CallData()
        {
            var searchedTable = await FillDataAsync();
            result = searchedTable;
            searchedTable = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = result.MyTable;
            dataGridView2.DataSource = result.MyTable;

            lblMontant.Text = "Payée : " + result.Payement.ToString("N0") + " FCFA, Réduction : " + result.Reduction.ToString("N0") + ", Restant : " + (result.Montant - result.Payement).ToString("N0") + " FCFA";
            lblTitre.Text = "Fournisseur : " + result.Client;
            lblNum_Operation.Text = "N° Opération : " + num_achat;
            lblNum_Operation.Visible = true;
            lblTitre.Visible = true;
            lblMontant.Visible = true;
            try
            {
                if (Principales.type_compte.Contains("Administrateur"))
                {
                    var selection = new DataGridViewCheckBoxColumn();
                    selection.Name = "Select";
                    selection.HeaderText = "Select";
                    selection.Width = 40;

                    dataGridView1.Columns.Add(selection);

                    AddColumns.Addcolumn(dataGridView1);
                    dataGridView1.Columns["Id"].Width = 40;
                    dataGridView1.Columns["Barcode"].Width = 70;
                    dataGridView1.Columns["Quantité"].Width = 70;
                    dataGridView1.Columns["Prix_Unité"].Width = 90;
                    dataGridView1.Columns["Select"].Width = 50;
                    dataGridView1.Columns["Sup"].Width = 40;
                    dataGridView1.Columns["Montant"].Width = 120;
                    dataGridView1.Columns["Edit"].Visible = false;
                }
            }
            catch (Exception)
            {

            }
        }

        public static Task<SearchedTable> FillDataAsync() => Task.Factory.StartNew(() => FillData());

        public static SearchedTable FillData()
        {
            SearchedTable searchedTable = new SearchedTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Barcode");
            dataTable.Columns.Add("Désignation");
            dataTable.Columns.Add("Quantité");
            dataTable.Columns.Add("Prix_Unité");
            dataTable.Columns.Add("Montant");
            using (var financeDataContext = new QuitayeContext())
            {
                var source1 = (from d in financeDataContext.tbl_arrivée
                               where d.Num_Achat == num_achat
                               join p in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals p.Product_Id into joinedTable
                               from f in joinedTable.DefaultIfEmpty()
                               orderby f.Marque
                               select new
                               {
                                   Id = d.Id,
                                   Montant = d.Prix,
                                   Barcode = d.Barcode,
                                   Quantité = d.Q_Unité,
                                   Catégorie = d.Catégorie,
                                   Prix_Unité = d.Prix / d.Q_Unité,
                                   Taille = d.Taille,
                                   Produit = d.Nom,
                                   Type = f.Type,
                                   Fournisseur = d.Fournisseur,
                                   Id_Fournisseur = d.Id_Fournisseur
                               }).ToList();
                var source2 = financeDataContext.tbl_payement.Where((d => d.Num_Opération == num_achat)).Select(d => new
                {
                    Id = d.Id,
                    Montant = d.Montant,
                    Reduction = d.Réduction
                });
                searchedTable.Payement = Convert.ToDecimal(source2.Sum(x => x.Montant));
                searchedTable.Qté = Convert.ToDecimal(source1.Sum(x => x.Quantité));
                searchedTable.Montant = Convert.ToDecimal(source1.Sum(x => x.Montant));
                searchedTable.Reduction = Convert.ToDecimal(source2.Sum(x => x.Reduction));
                foreach (var data in source1)
                {
                    int? idClient = Convert.ToInt32(data.Id_Fournisseur);
                    int num1;
                    if (idClient.HasValue)
                    {
                        idClient = Convert.ToInt32(data.Id_Fournisseur);
                        int num2 = 0;
                        num1 = !(idClient.GetValueOrDefault() == num2 & idClient.HasValue) ? 1 : 0;
                    }
                    else
                        num1 = 0;
                    if (num1 != 0)
                    {
                        searchedTable.Id_Client = Convert.ToInt32(data.Id_Fournisseur);
                        break;
                    }
                }
                foreach (var data in source1)
                {
                    client = data.Fournisseur;
                    DataRow row = dataTable.NewRow();
                    row["Id"] = data.Id;
                    row["Barcode"] = data.Barcode;
                    if (!string.IsNullOrEmpty(data.Type))
                        row["Désignation"] = data.Produit + " " + data.Taille + " " + data.Catégorie + " " + data.Type;
                    else
                        row["Désignation"] = data.Produit + " " + data.Taille + " " + data.Catégorie;
                    row["Quantité"] = Convert.ToDecimal(data.Quantité).ToString("N0");
                    row["Prix_Unité"] = Convert.ToDecimal(data.Prix_Unité).ToString("N0");
                    row["Montant"] = Convert.ToDecimal(data.Montant).ToString("N0");
                    dataTable.Rows.Add(row);
                }

                var nRow = dataTable.NewRow();
                nRow["Désignation"] = "Total";
                nRow["Quantité"] = Convert.ToDecimal(source1.Sum(x => x.Quantité)).ToString("N0");
                nRow["Prix_Unité"] = Convert.ToDecimal(source1.Sum(x => x.Prix_Unité)).ToString("N0");
                nRow["Montant"] = Convert.ToDecimal(source1.Sum(x => x.Montant)).ToString("N0");
                dataTable.Rows.Add(nRow);
                foreach (var data in source1)
                {
                    if (data.Fournisseur != "" && data.Fournisseur != null)
                    {
                        client = data.Fournisseur;
                        searchedTable.Client = data.Fournisseur;
                        break;
                    }
                }
            }
            searchedTable.MyTable = dataTable;
            return searchedTable;
        }

        private async Task CallSearchData(string search)
        {
            SearchedTable searchedTable = await FillSearchDataAsync(search);
            result = searchedTable;
            searchedTable = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = result.MyTable;
            dataGridView2.DataSource = result.MyTable;

            lblMontant.Text = "Payée : " + result.Payement.ToString("N0") + " FCFA, Réduction : " + result.Reduction.ToString("N0") + ", Restant : " + (result.Montant - result.Payement).ToString("N0") + " FCFA";
            lblTitre.Text = "Fournisseur : " + result.Client;
            lblNum_Operation.Text = "N° Opération : " + num_achat;
            lblNum_Operation.Visible = true;
            lblTitre.Visible = true;
            lblMontant.Visible = true;
            try
            {
                if (Principales.type_compte.Contains("Administrateur"))
                {
                    var selection = new DataGridViewCheckBoxColumn();
                    selection.Name = "Select";
                    selection.HeaderText = "Select";
                    selection.Width = 40;

                    dataGridView1.Columns.Add(selection);
                    AddColumns.Addcolumn(dataGridView1);
                    dataGridView1.Columns["Id"].Width = 40;
                    dataGridView1.Columns["Barcode"].Width = 70;
                    dataGridView1.Columns["Quantité"].Width = 70;
                    dataGridView1.Columns["Prix_Unité"].Width = 90;
                    dataGridView1.Columns["Select"].Width = 50;
                    dataGridView1.Columns["Sup"].Width = 40;
                    dataGridView1.Columns["Montant"].Width = 120;
                    dataGridView1.Columns["Edit"].Visible = false;
                }

            }
            catch (Exception)
            {

            }
        }

        public static Task<SearchedTable> FillSearchDataAsync(string search) => Task.Factory.StartNew(() => FillSearchData(search));

        public static SearchedTable FillSearchData(string text)
        {
            SearchedTable searchedTable = new SearchedTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Barcode");
            dataTable.Columns.Add("Désignation");
            dataTable.Columns.Add("Quantité");
            dataTable.Columns.Add("Prix_Unité");
            dataTable.Columns.Add("Montant");
            using (var financeDataContext = new QuitayeContext())
            {
                var source1 = from d in financeDataContext.tbl_arrivée

                              join p in financeDataContext.tbl_stock_produits_vente on d.Product_Id equals p.Product_Id into joinedTable
                              from f in joinedTable.DefaultIfEmpty()
                              orderby f.Marque
                              where d.Num_Achat == num_achat && (d.Auteur.Contains(text) || d.Fournisseur.Contains(text)
                              || d.Num_Achat.Contains(text)
                              || d.Catégorie.Contains(text) || d.Barcode.ToLower().Equals(text.ToLower())
                              || d.Taille.Contains(text) || d.Nom.Contains(text)
                              || d.Bon_Commande.Contains(text) || f.Type.Contains(text))
                              select new
                              {
                                  Id = d.Id,
                                  Montant = d.Prix,
                                  Barcode = d.Barcode,
                                  Quantité = d.Q_Unité,
                                  Catégorie = d.Catégorie,
                                  Prix_Unité = d.Prix / d.Q_Unité,
                                  Taille = d.Taille,
                                  Produit = d.Nom,
                                  Type = f.Type,
                                  Fournisseur = d.Fournisseur,
                                  Id_Fournisseur = d.Id_Fournisseur
                              };
                var source2 = financeDataContext.tbl_payement.Where((d => d.Num_Opération == num_achat)).Select(d => new
                {
                    Id = d.Id,
                    Montant = d.Montant,
                    Reduction = d.Réduction
                });
                searchedTable.Payement = Convert.ToDecimal(source2.Sum(x => x.Montant));
                searchedTable.Qté = Convert.ToDecimal(source1.Sum(x => x.Quantité));
                searchedTable.Montant = Convert.ToDecimal(source1.Sum(x => x.Montant));
                searchedTable.Reduction = Convert.ToDecimal(source2.Sum(x => x.Reduction));
                foreach (var data in source1)
                {
                    int? idClient = Convert.ToInt32(data.Id_Fournisseur);
                    int num1;
                    if (idClient.HasValue)
                    {
                        idClient = Convert.ToInt32(data.Id_Fournisseur);
                        int num2 = 0;
                        num1 = !(idClient.GetValueOrDefault() == num2 & idClient.HasValue) ? 1 : 0;
                    }
                    else
                        num1 = 0;
                    if (num1 != 0)
                    {
                        searchedTable.Id_Client = Convert.ToInt32(data.Id_Fournisseur);
                        break;
                    }
                }
                foreach (var data in source1)
                {
                    client = data.Fournisseur;
                    DataRow row = dataTable.NewRow();
                    row["Id"] = data.Id;
                    row["Barcode"] = data.Barcode;
                    if (!string.IsNullOrEmpty(data.Type))
                        row["Désignation"] = data.Produit + " " + data.Taille + " " + data.Catégorie + " " + data.Type;
                    else
                        row["Désignation"] = data.Produit + " " + data.Taille + " " + data.Catégorie;
                    row["Quantité"] = Convert.ToDecimal(data.Quantité).ToString("N0");
                    row["Prix_Unité"] = Convert.ToDecimal(data.Prix_Unité).ToString("N0");
                    row["Montant"] = Convert.ToDecimal(data.Montant).ToString("N0");
                    dataTable.Rows.Add(row);
                }

                var nRow = dataTable.NewRow();
                nRow["Désignation"] = "Total";
                nRow["Quantité"] = Convert.ToDecimal(source1.Sum(x => x.Quantité)).ToString("N0");
                nRow["Prix_Unité"] = Convert.ToDecimal(source1.Sum(x => x.Prix_Unité)).ToString("N0");
                nRow["Montant"] = Convert.ToDecimal(source1.Sum(x => x.Montant)).ToString("N0");
                dataTable.Rows.Add(nRow);

                foreach (var data in source1)
                {
                    if (data.Fournisseur != "" && data.Fournisseur != null)
                    {
                        client = data.Fournisseur;
                        searchedTable.Client = data.Fournisseur;
                        break;
                    }
                }
            }
            searchedTable.MyTable = dataTable;
            return searchedTable;
        }
    }
}
