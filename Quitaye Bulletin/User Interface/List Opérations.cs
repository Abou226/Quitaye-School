using FontAwesome.Sharp;
using iTextSharp.text.pdf.parser.clipper;
using Microsoft.SqlServer.Management.Common;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class List_Opérations : Form
    {
        public List_Opérations()
        {
            InitializeComponent();
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
            startDate.Value = DateTime.Today.AddDays(-30);
            timer1.Enabled = false;
            timer1.Interval = 10;
            timer1.Start();
            timer1.Tick += Timer1_Tick;
        }
        private string name;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (cbxCycle.Text != "" && cbxCycle.Text != "Tout")
            {
                name = "Journal Opérations " + cbxCycle.Text + " " + Principales.annéescolaire + " " + startDate.Value.Date.ToString("dd-MM-yyyy") + EndDate.Value.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            }
            else
            {
                name = "Journal Opérations " + Principales.annéescolaire + " " + startDate.Value.Date.ToString("dd-MM-yyyy") + EndDate.Value.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            PrintAction.Print.PrintPdfFile(dataGridView1, name, "Historique Opérations", "Opération Financière", cbxCycle.Text, LogIn.mycontrng, "Quitaye School", true);
        }


        private void btnExcel_Click(object sender, EventArgs e)
        {
            PrintAction.Print.PrintExcelFile(dataGridView1, "Journal Opérations", name, "Quitaye School");
        }

        Timer timer1 = new Timer();

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            ts = EndDate.Value.Date - startDate.Value.Date;
            FillData(ts.Days);
        }

        Timer timer = new Timer();
        string operation;
        private void btnValider_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                if (txtMontant.Text != "" && txtCommentaire.Text != "" && operation != "")
                {
                    if (btnValider.IconChar == IconChar.Check)
                    {
                        AddData();
                    }
                    else if (btnValider.IconChar == IconChar.Edit)
                    {
                        EditData();
                    }
                }
            }
            else
            {
                if (LogIn.trial == true)
                {
                    Alert.SShow("Periode d'essay arrivé terme. Veillez-passez à la version payante !", Alert.AlertType.Info);
                }
                else Alert.SShow("Veillez renouveller votre abonnement !", Alert.AlertType.Info);
            }
        }

        public int P_Id;

        private void EditData()
        {
            using(var donnée = new QuitayeContext())
            {
                string result = Regex.Replace(txtMontant.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                if (filePath != "")
                {
                    byte[] file;
                    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            file = reader.ReadBytes((int)stream.Length);
                        }
                    }
                    var tbl = (from d in donnée.tbl_payement where d.Id == P_Id select d).First();
                    
                    tbl.Année_Scolaire = Principales.annéescolaire;
                    tbl.Commentaire = txtCommentaire.Text;
                    tbl.Montant = Convert.ToDecimal(result);
                    tbl.Type = "Dépenses";
                    tbl.Opération = operation;
                    tbl.Date_Payement = DatePayement.Value.Date;
                    tbl.Fichier = file;
                    tbl.Nom_Fichier = filename;
                    donnée.SaveChangesAsync();
                }
                else
                {
                    var tbl = (from d in donnée.tbl_payement where d.Id == P_Id select d).First();

                    tbl.Année_Scolaire = Principales.annéescolaire;
                    tbl.Commentaire = txtCommentaire.Text;
                    tbl.Type = "Dépenses";
                    tbl.Opération = operation;
                    tbl.Montant = Convert.ToDecimal(result);
                    tbl.Date_Payement = DatePayement.Value.Date;
                    tbl.Date_Enregistrement = DateTime.Now;
                    donnée.SaveChangesAsync();
                }
                btnValider.IconChar = IconChar.Plus;
                btnValider.Text = "Ajouter";
            }
        }
        private async void AddData()
        {
            using(var donnée = new QuitayeContext())
            {
                string result = Regex.Replace(txtMontant.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                if (filePath != "")
                {
                    byte[] file;
                    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            file = reader.ReadBytes((int)stream.Length);
                        }
                    }
                    int id = 0;

                    var pay = from d in donnée.tbl_payement select d;
                    if(pay.Count() != 0)
                    {
                        var pays = (from d in donnée.tbl_payement orderby d.Id descending select d).First();
                        id = pays.Id;
                    }

                    var tbl = new Models.Context.tbl_payement();
                    tbl.Id = id + 1;
                    tbl.Année_Scolaire = Principales.annéescolaire;
                    tbl.Auteur = Principales.profile;
                    tbl.Commentaire = txtCommentaire.Text;
                    tbl.Type = "Dépenses";
                    tbl.Opération = operation;
                    tbl.Date_Payement = DatePayement.Value.Date;
                    tbl.Date_Enregistrement = DateTime.Now;
                    tbl.Montant = Convert.ToDecimal(result);
                    tbl.Fichier = file;
                    tbl.Nom_Fichier = filename;
                    donnée.tbl_payement.Add(tbl);
                    await donnée.SaveChangesAsync();
                }else
                {
                    int id = 0;

                    var pay = from d in donnée.tbl_payement select d;
                    if (pay.Count() != 0)
                    {
                        var pays = (from d in donnée.tbl_payement orderby d.Id descending select d).First();
                        id = pays.Id;
                    }
                    var tbl = new Models.Context.tbl_payement();
                    tbl.Id = id + 1;
                    tbl.Année_Scolaire = Principales.annéescolaire;
                    tbl.Auteur = Principales.profile;
                    tbl.Montant = Convert.ToDecimal(result);
                    tbl.Commentaire = txtCommentaire.Text;
                    tbl.Type = "Dépenses";
                    tbl.Opération = operation;
                    tbl.Date_Payement = DatePayement.Value.Date;
                    tbl.Date_Enregistrement = DateTime.Now;
                    donnée.tbl_payement.Add(tbl);
                    await donnée.SaveChangesAsync();
                }

                ShowData();
            }
        }

        private void ShowData()
        {
            ts = EndDate.Value.Date - startDate.Value.Date;
            if (cbxCycle.Text != "")
            {
                SearchDataType();

            }
            else FillData(ts.Days);
        }

        private void FillData(int days)
        {
            using(var donnée = new QuitayeContext())
            {
                var end = EndDate.Value.Date.AddDays(-days);
                var start = startDate.Value.Date.AddDays(days);
                var don = (from d in donnée.tbl_payement
                          where d.Année_Scolaire == Principales.annéescolaire
                          && (DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(end)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(start)) && d.Montant != 0
                          orderby d.Id descending
                          select new
                          {
                              Id = d.Id,
                              AnnéeScolaire = d.Année_Scolaire,
                              Montant = d.Montant,
                              Commentaire = d.Commentaire,
                              Operation = d.Opération,
                              Type = d.Type,
                              Date = d.Date_Payement,
                              Auteur = d.Auteur,
                          }).ToList();
                
                var recette = (from d in donnée.tbl_payement
                              where d.Année_Scolaire == Principales.annéescolaire && d.Type != "Dépenses"
                              && (DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(end)
                              && DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(start))
                              orderby d.Id descending
                              select d).ToList();

                
                var dep = (from d in donnée.tbl_payement
                              where d.Année_Scolaire == Principales.annéescolaire && d.Type == "Dépenses"
                              && (DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(end)
                              && DbFunctions.TruncateTime(d.Date_Payement.Value) <=  DbFunctions.TruncateTime(start))
                              orderby d.Id descending
                              select d).ToList();

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = don;
                dataGridView2.DataSource = don;
                decimal depenses = Convert.ToDecimal(dep.Sum(x => x.Montant));
                decimal recettes = Convert.ToDecimal(recette.Sum(x => x.Montant));
                lblMontant.Text = "Dépenses: " + depenses.ToString("N0") + " FCFA" + ", Recettes : " + recettes.ToString("N0") + " FCFA" + ", Résultat : " + (recettes - depenses).ToString("N0") + " FCFA";


                if (Principales.type_compte == "Administrateur")
                {
                    DeleteColumn delete = new DeleteColumn();
                    delete.Name = "Delete";
                    delete.HeaderText = "Sup";
                    dataGridView1.Columns.Add(delete);

                    dataGridView1.Columns["Delete"].Width = 40;
                }
            }
        }

        private string filePath = "";
        private string filename;

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            IconButton p = sender as IconButton;

            if (p != null)
            {
                file.Filter = "(*.pdf; *.xls;*.docs)| *.pdf; *.xls; *.docs";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    filePath = file.FileName;
                    filename = Path.GetFileName(filePath);
                    //p.Image = Image.FromFile(file.FileName);
                }
            }
        }

        TimeSpan ts;
        

        private void rCarteBleu_CheckedChanged(object sender, EventArgs e)
        {
            operation = ((RadioButton)sender).Text;
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(LogIn.expiré == false)
            {
                if (e.ColumnIndex == 8 && Principales.type_compte == "Administrateur")
                {
                    P_Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    MsgBox msg = new MsgBox();
                    msg.show("Voulez-vous supprimer cette éléménet ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                    msg.ShowDialog();
                    if (msg.clicked == "Non")
                        return;
                    else if (msg.clicked == "Oui")
                    {
                        using (var donnée = new QuitayeContext())
                        {
                            var tbl = (from d in donnée.tbl_payement where d.Id == P_Id select d).First();
                            donnée.tbl_payement.Remove(tbl);
                            await donnée.SaveChangesAsync();
                            ts = EndDate.Value.Date - startDate.Value.Date;
                            ShowData();
                        }
                    }
                }
            }
            
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            ts = EndDate.Value.Date - startDate.Value.Date;
            if (txtsearch.Text.Length > 0)
            {
                SearchData();

            }
            else FillData(ts.Days);
        }

        private void SearchData()
        {
            using(var donnée = new QuitayeContext())
            {
               
                var tbl = from d in donnée.tbl_payement where d.Commentaire.Contains(txtsearch.Text) 
                          || d.Opération.Contains(txtsearch.Text) 
                          || d.Montant.Value.ToString().Contains(txtsearch.Text) && (DbFunctions.TruncateTime(d.Date_Payement.Value) >= EndDate.Value.Date.AddDays(-ts.Days)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) <= startDate.Value.Date.AddDays(ts.Days)) && d.Montant != 0
                          orderby d.Id descending
                          select new
                          {
                              Id = d.Id,
                              AnnéeScolaire = d.Année_Scolaire,
                              Montant = d.Montant,
                              Commentaire = d.Commentaire,
                              Operation = d.Opération,
                              Type = d.Type,
                              Date = d.Date_Payement,
                              Auteur = d.Auteur,
                          };

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = tbl;
                dataGridView2.DataSource = tbl;

                var recette = from d in donnée.tbl_payement
                              where d.Année_Scolaire == Principales.annéescolaire && d.Type != "Dépenses"
                              && (DbFunctions.TruncateTime(d.Date_Payement.Value) >= EndDate.Value.Date.AddDays(-ts.Days)
                              && DbFunctions.TruncateTime(d.Date_Payement.Value) <= startDate.Value.Date.AddDays(ts.Days))
                              orderby d.Id descending
                              select d;

                var dep = from d in donnée.tbl_payement
                          where d.Année_Scolaire == Principales.annéescolaire && d.Type == "Dépenses"
                          && (DbFunctions.TruncateTime(d.Date_Payement.Value) >= EndDate.Value.Date.AddDays(-ts.Days)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) <= startDate.Value.Date.AddDays(ts.Days))
                          orderby d.Id descending
                          select d;


                decimal depenses = Convert.ToDecimal(dep.Sum(x => x.Montant));
                decimal recettes = Convert.ToDecimal(recette.Sum(x => x.Montant));
                lblMontant.Text = "Dépenses: " + depenses.ToString("N0") + " FCFA" + ", Recettes : " + recettes.ToString("N0") + " FCFA" + ", Résultat : " + (recettes - depenses).ToString("N0") + " FCFA";


                if (Principales.type_compte == "Administrateur")
                {
                    DeleteColumn delete = new DeleteColumn();
                    delete.Name = "Delete";
                    delete.HeaderText = "Sup";
                    dataGridView1.Columns.Add(delete);

                    dataGridView1.Columns["Delete"].Width = 40;
                }
                
            }
        }

        private void SearchDataType()
        {
            using (var donnée = new QuitayeContext())
            {
                var tbl = from d in donnée.tbl_payement
                          where d.Opération.Contains(cbxCycle.Text)
                             && (DbFunctions.TruncateTime(d.Date_Payement.Value) >= EndDate.Value.Date.AddDays(-ts.Days)
                            && DbFunctions.TruncateTime(d.Date_Payement.Value) <= startDate.Value.Date.AddDays(ts.Days)) && d.Montant != 0
                          orderby d.Id descending
                          select new
                          {
                              Id = d.Id,
                              AnnéeScolaire = d.Année_Scolaire,
                              Montant = d.Montant,
                              Commentaire = d.Commentaire,
                              Operation = d.Opération,
                              Type = d.Type,
                              Date = d.Date_Payement,
                              Auteur = d.Auteur,
                          };

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = tbl;
                dataGridView2.DataSource = tbl;

                var recette = from d in donnée.tbl_payement
                              where d.Année_Scolaire == Principales.annéescolaire && d.Type != "Dépenses"
                              && (DbFunctions.TruncateTime(d.Date_Payement.Value) >= EndDate.Value.Date.AddDays(-ts.Days)
                              && DbFunctions.TruncateTime(d.Date_Payement.Value) <= startDate.Value.Date.AddDays(ts.Days))
                              orderby d.Id descending
                              select d;

                var dep = from d in donnée.tbl_payement
                          where d.Année_Scolaire == Principales.annéescolaire && d.Type == "Dépenses"
                          && (DbFunctions.TruncateTime(d.Date_Payement.Value) >= EndDate.Value.Date.AddDays(-ts.Days)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) <= startDate.Value.Date.AddDays(ts.Days))
                          orderby d.Id descending
                          select d;


                decimal depenses = Convert.ToDecimal(dep.Sum(x => x.Montant));
                decimal recettes = Convert.ToDecimal(recette.Sum(x => x.Montant));
                lblMontant.Text = "Dépenses: " + depenses.ToString("N0") + " FCFA" + ", Recettes : " + recettes.ToString("N0") + " FCFA" + ", Résultat : " + (recettes - depenses).ToString("N0") + " FCFA";

                if (Principales.type_compte == "Administrateur")
                {
                    DeleteColumn delete = new DeleteColumn();
                    delete.Name = "Delete";
                    delete.HeaderText = "Sup";
                    dataGridView1.Columns.Add(delete);

                    dataGridView1.Columns["Delete"].Width = 40;
                }
            }
        }


        private void cbxCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxCycle.Text.Length > 0)
            {
                SearchDataType();
            }
        }

        private void txtMontant_TextChanged(object sender, EventArgs e)
        {
            if (txtMontant.Text != "")
            {
                txtMontant.Text = Convert.ToDecimal(txtMontant.Text).ToString("N0");
                txtMontant.SelectionStart = txtMontant.Text.Length;
            }
        }

        private void txtMontant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar != 46 && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            ShowData();
        }
    }
}
