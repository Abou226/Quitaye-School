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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Transaction_Fournisseur : Form
    {
        public string name;
        public Transaction_Fournisseur()
        {
            InitializeComponent();
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            startDate.ValueChanged += StartDate_ValueChanged;
            dataGridView1.CellClick += DataGridView1_CellClick;
            startDate.Value = DateTime.Today.AddDays(-6);
            btnExcel.Click += BtnExcel_Click;
            btnPdf.Click += BtnPdf_Click;
            txtsearch.TextChanged += Txtsearch_TextChanged;
        }

        private async void Txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtsearch.Text))
            {
                await CallSearchData(txtsearch.Text);
            }
            else await CallData();
        }

        private async void BtnExcel_Click(object sender, EventArgs e)
        {
            //Print.PrintExcelFile(dataGridView1, "Rapport Transaction Fournisseur", name, "Quitaye School");
            var file = $"C:/Quitaye School/Rapport Transaction Fournisseur {startDate.Value.Date.ToString("dd-MM-yyyy")}_{EndDate.Value.Date.ToString("dd-MM-yyyy")}_{DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")}";
            //PrintAction.Print.PrintExcelFile(dataGridView2, "Rapport Commane(s)", name, "Quitaye School");
            Alert.SShow("Génération Barcode en cours.. Veillez-patientez !", Alert.AlertType.Info);
            await Task.Run(() => Quitaye_School.Models.Docuement.ExportToExcel(dataGridView1, file));
            System.Diagnostics.Process.Start(file);
            Alert.SShow("Génération effectué avec succès.", Alert.AlertType.Sucess);
        }

        private void BtnPdf_Click(object sender, EventArgs e)
        {
            string[] strArray = new string[6];
            strArray[0] = "Rapport Transaction Fournisseur ";
            DateTime date1 = startDate.Value;
            date1 = date1.Date;
            strArray[1] = date1.ToString("dd-MM-yyyy");
            strArray[2] = " ";
            DateTime date2 = EndDate.Value;
            date2 = date2.Date;
            strArray[3] = date2.ToString("dd-MM-yyyy");
            strArray[4] = " ";
            strArray[5] = DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            name = string.Concat(strArray);
            Print.PrintPdfFile(dataGridView1, name, "Rapport Transaction Fournisseur", "Opération(s)", "Achat / Réglement", LogIn.mycontrng, "Quitaye School", false);
        }


        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Fournisseur") == true)
            {
                string fournisseur = dataGridView1.CurrentRow.Cells["Fournisseur"].Value.ToString();
                var id = await GetFournisseurIdAsync(fournisseur);
                Details_Compte_Tier details = new Details_Compte_Tier(fournisseur,id, startDate.Value, EndDate.Value);
                details.ShowDialog();
                if(details.ok == "Oui")
                {
                    await CallData();
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Num_Vente") == true)
            {

            }
        }

        private async void StartDate_ValueChanged(object sender, EventArgs e)
        {
            await CallData();
        }

        private Task<int> GetFournisseurIdAsync(string nom)
        {
            return Task.Factory.StartNew(() => GetFournisseurId(nom));
        }
        private int GetFournisseurId(string nom)
        {
            using(var donnée = new QuitayeContext())
            {
                var sed = from d in donnée.tbl_fournisseurs where d.Nom == nom select new { Id = d.Id };
                if (sed.Count() != 0)
                {
                    return Convert.ToInt32(sed.First().Id);
                }
                else return 0;
            }
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallData();
        }

        Timer loadTimer = new Timer();

        private async Task CallData()
        {
            var result = await FillDataAsync();
            if (result.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau !";
                dt.Rows.Add(dr);

                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = result;
                var achat = li.Where(x => x.Type == "Achat").Sum(x => x.Montant);
                var payement = li.Where(x => x.Type == "Payement").Sum(x => x.Montant);
                var redevance = li.Sum(x => x.Solde);
                var fournisseur = (achat + redevance);
                var diff = payement - fournisseur;

                if (achat + redevance > payement)
                    lblSolde.Text = " Vous devez " + (-diff).ToString("N0") + " FCFA  à vos fournisseur" ;
                else if (achat + redevance < payement)
                {
                    lblSolde.Text = "Vos fournisseurs vous doivent " + diff.ToString("N0") + " FCFA";
                }
                else
                {
                    lblSolde.Text = "Compte de vos fournisseurs Soldé !";
                }
                lblTotalTransaction.Text = "Total Débit : " + fournisseur.ToString("N0") + " FCFA, Total Crédit : " + payement.ToString("N0") + " FCFA" + ", Rédévance : " + redevance.ToString("N0");
            }
        }
        private Task<DataTable> FillDataAsync()
        {
            return Task.Factory.StartNew(() => FillData());
        }
        private DataTable FillData()
        {
            List<Transaction> list = new List<Transaction>();
            DataTable dt = new DataTable();
            dt.Columns.Add("Num_Ref");
            dt.Columns.Add("Fournisseur");
            dt.Columns.Add("Débit");
            dt.Columns.Add("Crédit");
            dt.Columns.Add("Reference");
            dt.Columns.Add("Date");
            using (var donnée = new QuitayeContext())
            {
                var ders = from d in donnée.tbl_arrivée
                           where (DbFunctions.TruncateTime(d.Date_Arrivée.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Arrivée.Value) >= DbFunctions.TruncateTime(startDate.Value))
                           group d by d.Num_Achat into gr
                           select new
                           {
                               Montant = gr.Sum(x => x.Prix),
                               Fournisseur = gr.FirstOrDefault().Fournisseur,
                               Bon_Commande = gr.FirstOrDefault().Bon_Commande,
                               Date = gr.FirstOrDefault().Date_Arrivée,
                               Num_Facture = gr.Key,
                               
                           };

                var pay = from d in donnée.tbl_payement
                          where  (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value))
                          && d.Type == "Décaissement" && d.Raison == "Fournisseur"
                          group d by d.Num_Opération into gr
                          select new
                          {
                              Fournisseur = gr.FirstOrDefault().Client,
                              Montant = gr.Sum(x => x.Montant),
                              Date = gr.FirstOrDefault().Date_Enregistrement,
                              Num_Ref = gr.Key,
                              Reference = gr.FirstOrDefault().Reference,
                          };

                int id = 1;
                foreach (var item in ders)
                {
                    list.Add(new Transaction()
                    {
                        Id = id,
                        Type = "Achat",
                        Num_Ref = item.Num_Facture,
                        Fournisseur = item.Fournisseur,
                        Montant = Convert.ToDecimal(item.Montant),
                        Date = Convert.ToDateTime(item.Date),
                    });
                    id++;
                }

                foreach (var item in pay)
                {
                    list.Add(new Transaction()
                    {
                        Type = "Payement",
                        Num_Ref = item.Num_Ref,
                        Fournisseur = item.Fournisseur,
                        Montant = Convert.ToDecimal(item.Montant),
                        Date = Convert.ToDateTime(item.Date),
                        Reference = item.Reference,
                    });
                    id++;
                }

                var order = from d in list orderby d.Date descending select d;
                foreach (var item in order)
                {
                    DataRow dr = dt.NewRow();
                    dr["Num_Ref"] = item.Num_Ref;
                    dr["Fournisseur"] = item.Fournisseur;
                    if (item.Type == "Achat")
                        dr["Débit"] = item.Montant.ToString("N0");
                    else if (item.Type == "Payement")
                        dr["Crédit"] = item.Montant.ToString("N0");
                    dr["Date"] = item.Date.ToString("dd/MM/yyyy");
                    dr["Reference"] = item.Reference;

                    dt.Rows.Add(dr);
                }

                var desf = from d in donnée.tbl_redévance where d.Redéveur == "Vous" select new { Montant = d.Montant };
                var desfr = from d in donnée.tbl_redévance where d.Rédevant == "Vous" select new { Montant = d.Montant };
                list.Add(new Transaction()
                {
                    Solde = Convert.ToDecimal(desf.Sum(x => x.Montant)) - Convert.ToDecimal(desfr.Sum(x => x.Montant))
                });

                li = list;
                return dt;
            }
        }

        private async Task CallSearchData(string search)
        {
            var result = await FillSearchDataAsync(search);
            if (result.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau !";
                dt.Rows.Add(dr);

                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = result;
                var achat = li.Where(x => x.Type == "Achat").Sum(x => x.Montant);
                var payement = li.Where(x => x.Type == "Payement").Sum(x => x.Montant);
                var redevance = li.Sum(x => x.Solde);
                var fournisseur = (achat + redevance);
                var diff = payement - fournisseur;

                if (achat + redevance > payement)
                    lblSolde.Text = " Vous devez " + (-diff).ToString("N0") + " FCFA  à vos fournisseur";
                else if (achat + redevance < payement)
                {
                    lblSolde.Text = "Vos fournisseurs vous doivent " + diff.ToString("N0") + " FCFA";
                }
                else
                {
                    lblSolde.Text = "Compte de vos fournisseurs Soldé !";
                }
                lblTotalTransaction.Text = "Total Débit : " + fournisseur.ToString("N0") + " FCFA, Total Crédit : " + payement.ToString("N0") + " FCFA" + ", Rédévance : " + redevance.ToString("N0");
            }
        }
        private Task<DataTable> FillSearchDataAsync(string search)
        {
            return Task.Factory.StartNew(() => FillSearchData(search));
        }
        private DataTable FillSearchData(string search)
        {
            List<Transaction> list = new List<Transaction>();
            DataTable dt = new DataTable();
            dt.Columns.Add("Num_Ref");
            dt.Columns.Add("Fournisseur");
            dt.Columns.Add("Débit");
            dt.Columns.Add("Crédit");
            dt.Columns.Add("Reference");
            dt.Columns.Add("Date");
            using (var donnée = new QuitayeContext())
            {
                var ders = from d in donnée.tbl_arrivée
                           where (DbFunctions.TruncateTime(d.Date_Arrivée.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Arrivée.Value) >= DbFunctions.TruncateTime(startDate.Value))
                           & (d.Auteur.Contains(search) || d.Fournisseur.Contains(search) 
                           || d.Taille.Contains(search) 
                           || d.Barcode.Equals(search) || d.Catégorie.Contains(search) 
                           || d.Nom.Contains(search) || d.Bon_Commande.Contains(search))
                           group d by d.Num_Achat into gr
                           select new
                           {
                               Montant = gr.Sum(x => x.Prix),
                               Fournisseur = gr.FirstOrDefault().Fournisseur,
                               Bon_Commande = gr.FirstOrDefault().Bon_Commande,
                               Date = gr.FirstOrDefault().Date_Arrivée,
                               Num_Facture = gr.Key,
                           };

                var pay = from d in donnée.tbl_payement
                          where  (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value)) && (d.Auteur.Contains(search) 
                          || d.Commentaire.Contains(search) || d.Num_Opération.Contains(search) 
                          || d.Mode_Payement.Contains(search) || d.Num_Client.Contains(search) || d.Reference.Contains(search))
                          && d.Type == "Décaissement" && d.Raison == "Fournisseur"
                          group d by d.Num_Opération into gr
                          select new
                          {
                              Fournisseur = gr.FirstOrDefault().Client,
                              Montant = gr.Sum(x => x.Montant),
                              Date = gr.FirstOrDefault().Date_Enregistrement,
                              Num_Ref = gr.Key,
                              Reference = gr.FirstOrDefault().Reference,
                          };

                int id = 1;
                foreach (var item in ders)
                {
                    list.Add(new Transaction()
                    {
                        Id = id,
                        Type = "Achat",
                        Num_Ref = item.Num_Facture,
                        Fournisseur = item.Fournisseur,
                        Montant = Convert.ToDecimal(item.Montant),
                        Date = Convert.ToDateTime(item.Date),
                    });
                    id++;
                }

                foreach (var item in pay)
                {
                    list.Add(new Transaction()
                    {
                        Type = "Payement",
                        Num_Ref = item.Num_Ref,
                        Fournisseur = item.Fournisseur,
                        Montant = Convert.ToDecimal(item.Montant),
                        Date = Convert.ToDateTime(item.Date),
                        Reference = item.Reference,
                    });
                    id++;
                }

                var order = from d in list orderby d.Date descending select d;
                foreach (var item in order)
                {
                    DataRow dr = dt.NewRow();
                    dr["Num_Ref"] = item.Num_Ref;
                    dr["Fournisseur"] = item.Fournisseur;
                    if (item.Type == "Achat")
                        dr["Débit"] = item.Montant.ToString("N0");
                    else if (item.Type == "Payement")
                        dr["Crédit"] = item.Montant.ToString("N0");
                    dr["Date"] = item.Date.ToString("dd/MM/yyyy");
                    dr["Reference"] = item.Reference;

                    dt.Rows.Add(dr);
                }

                var desf = from d in donnée.tbl_redévance where d.Redéveur == "Vous" select new { Montant = d.Montant };
                var desfr = from d in donnée.tbl_redévance where d.Rédevant == "Vous" select new { Montant = d.Montant };
                list.Add(new Transaction()
                {
                    Solde = Convert.ToDecimal(desf.Sum(x => x.Montant)) - Convert.ToDecimal(desfr.Sum(x => x.Montant))
                });

                li = list;
                return dt;
            }
        }


        List<Transaction> li = new List<Transaction>();
    }
}
