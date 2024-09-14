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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Details_Compte_Tier : Form
    {
        public Details_Compte_Tier(string _compte, int _id, DateTime _start, DateTime _end)
        {
            InitializeComponent();
            compte = _compte;
            startDate.Value = _start;
            EndDate.Value = _end;
            btnAddPayement.Click += BtnAddPayement_Click;
            btnValider.Click += BtnValider_Click;
            txtmontant.TextChanged += Txtmontant_TextChanged;
            lblDetails.Text = "Compte : " + compte;
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            btnFermer.Click += BtnFermer_Click;
            txtmontant.KeyPress += Txtmontant_KeyPress;
            btnRédévance.Click += BtnRédévance_Click;
            startDate.ValueChanged += StartDate_ValueChanged;
            EndDate.ValueChanged += StartDate_ValueChanged;
            id = _id;
        }

        public int id;
        private async void StartDate_ValueChanged(object sender, EventArgs e)
        {
            await CallData();
        }

        private void BtnRédévance_Click(object sender, EventArgs e)
        {
            Ancienne_Rédévance ancienne = new Ancienne_Rédévance(compte);
            ancienne.ShowDialog();
        }

        private void Txtmontant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar != 46 && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallTask();
        }

#pragma warning disable CS0169 // Le champ 'Details_Compte_Tier.type' n'est jamais utilisé
        string type;
#pragma warning restore CS0169 // Le champ 'Details_Compte_Tier.type' n'est jamais utilisé
        Timer loadTimer = new Timer();
        private void Txtmontant_TextChanged(object sender, EventArgs e)
        {
            if (txtmontant.Text != "" && txtmontant.Text != "-")
            {
                txtmontant.Text = Convert.ToDecimal(txtmontant.Text).ToString("N0");
                txtmontant.SelectionStart = txtmontant.Text.Length;
            }
        }

        string num_payement;
        private async void BtnValider_Click(object sender, EventArgs e)
        {
            if (txtmontant.Text != "" && cbxPayement.Text != "")
            {
                string result = Regex.Replace(txtmontant.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                Payement payement = new Payement();
                payement.Montant = Convert.ToDecimal(result);
                payement.Client = compte;
                payement.Date = DateOpération.Value;
                payement.Reference = txtReference.Text;
                payement.Num_Opération = num_payement;
                payement.Mode_Payement = cbxPayement.Text;
                payement.Type = "Décaissement";
                payement.Raison = "Fournisseur";
                payement.Id_Client = id;
                num_payement = await Numero_Payement();
                var insert = await SavePayementAsync(payement);
                if (insert)
                {
                    txtmontant.Text = null;
                    txtReference.Text = null;
                    ok = "Oui";
                    await CallData();
                    Alert.SShow("Payement enregistré avec succès.", Alert.AlertType.Sucess);
                }
            }
        }
       public string ok;
        string compte;
#pragma warning disable CS0169 // Le champ 'Details_Compte_Tier.start' n'est jamais utilisé
#pragma warning disable CS0169 // Le champ 'Details_Compte_Tier.end' n'est jamais utilisé
        DateTime start, end;
#pragma warning restore CS0169 // Le champ 'Details_Compte_Tier.end' n'est jamais utilisé
#pragma warning restore CS0169 // Le champ 'Details_Compte_Tier.start' n'est jamais utilisé

        public static async Task<string> Numero_Payement()
        {
            using (var donnée = new QuitayeContext())
            {
                var dese = (from d in donnée.tbl_num_payement
                            orderby d.Id descending select new { Id = d.Id }).Take(1);
                int id = 1;
                if (dese.Count() != 0)
                {
                    var sas = from d in donnée.tbl_num_payement
                              where d.Date.Value.Month == DateTime.Now.Month && d.Date.Value.Year == DateTime.Now.Year
                              orderby d.Id descending
                              select new { Id = d.Id };
                    if (sas.Count() != 0)
                    {
                        id = Convert.ToInt32(donnée.tbl_num_payement.OrderByDescending(x => x.Id).First().Id) + 1;
                    }
                    else
                    {
                        var se = (from d in donnée.tbl_num_payement 
                                  orderby d.Id descending select new { Id = d.Id }).First().Id;
                        id = Convert.ToInt32(se)+ 1;
                    }
                }


                var num_Cmd = new Models.Context.tbl_num_payement();

                string num;
                if (id < 10)
                {
                    num = "0000" + id;
                }
                else if (id >= 10 && id < 100)
                {
                    num = "000" + id;
                }
                else if (id >= 100 && id < 1000)
                {
                    num = "00" + id;
                }
                else if (id >= 1000 && id < 10000)
                {
                    num = "0" + id;
                }
                else
                {
                    num = id.ToString();
                }

                string mois = DateTime.Now.ToString("MM");
                string année = DateTime.Now.ToString("yy");
                num_Cmd.Id = id;
                num_Cmd.OrderId = id;
                num_Cmd.Order = mois + année + "-PAY." + num;
                num_Cmd.Date = DateTime.Now;
                donnée.tbl_num_payement.Add(num_Cmd);
                await donnée.SaveChangesAsync();
                return num_Cmd.Order;
            }
        }

        private async void BtnAddPayement_Click(object sender, EventArgs e)
        {
            Ajouter_Element element = new Ajouter_Element("Mode");
            element.ShowDialog();
            if (element.ok == "Oui")
            {
                await CallMode(cbxPayement);
                await CallMode(cbxMode);
            }
        }

        
        private async Task<bool> SavePayementAsync(Payement payement)
        {
            using (var donnée = new QuitayeContext())
            {
                var pay = (from d in donnée.tbl_payement
                           orderby d.Id descending select new { Id = d.Id }).Take(1);
                int ie = 1;
                if (pay.Count() != 0)
                {
                    ie = pay.First().Id + 1;
                }

                var pays = new Models.Context.tbl_payement();
                pays.Id = ie;
                pays.Num_Opération = payement.Num_Opération;
                pays.Client = compte;
                pays.Montant = payement.Montant;
                pays.Num_Client = payement.Num_Client;
                pays.Num_Opération = payement.Num_Opération;
                pays.Type = payement.Type;
                pays.Date_Payement = payement.Date;
                pays.Date_Enregistrement = DateTime.Now;
                pays.Auteur = Principales.profile;
                //pays.Compte_Tier = compte;
                pays.Raison = payement.Raison;
                pays.Réduction = payement.Reduction;
                pays.Compte_Tier = payement.Id_Client.ToString();
                pays.Mode_Payement = payement.Mode_Payement;
                pays.Nature = "Payement";
                pays.Reference = payement.Reference;
                pays.Commentaire = "Payement " + compte + " " + payement.Num_Opération;
                donnée.tbl_payement.Add(pays);
                await donnée.SaveChangesAsync();
                return true;
            }
        }

        private async Task CallTask()
        {
            var mode = FillModeAsync();
            var pay = FillModeAsync();
            var type = FillTypeAsync();
            var data = FillDataAsync();
            var tasklist = new List<Task> { mode, data, pay, type };
            while (tasklist.Count > 0)
            {
                var finishedTask = await Task.WhenAny(tasklist);
                if (finishedTask == mode)
                {
                    cbxPayement.DataSource = mode.Result.Table;
                    cbxPayement.DisplayMember = "Mode";
                    cbxPayement.ValueMember = "Id";

                    if (mode.Result.RowsLists != null && mode.Result.RowsLists.Count > 0)
                    {
                        foreach (var item in mode.Result.RowsLists)
                        {
                            if (item.Default == true)
                            {
                                cbxPayement.Text = item.Mesure;
                                break;
                            }
                            else
                            {
                                cbxPayement.Text = null;
                            }
                        }
                    }
                    else cbxPayement.Text = null;
                }
                else if(finishedTask == pay)
                {
                    cbxMode.DataSource = pay.Result.Table;
                    cbxMode.DisplayMember = "Mode";
                    cbxMode.ValueMember = "Id";
                    cbxMode.Text = null;
                }else if(finishedTask == type)
                {
                    cbxType.DataSource = type.Result;
                    cbxType.DisplayMember = "Type";
                    cbxType.ValueMember = "Id";
                    cbxType.Text = null;
                }
                else if (finishedTask == data)
                {
                    if (data.Result.Rows.Count == 0)
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
                        var achat = li.Where(x => x.Type == "Achat").Sum(x => x.Montant);
                        var payement = li.Where(x => x.Type == "Payement").Sum(x => x.Montant);
                        var redevance = li.Sum(x => x.Solde);
                        var fournisseur = (achat + redevance);
                        var diff = payement - fournisseur;
                        
                        if (achat + redevance > payement)
                            lblSolde.Text = " Vous devez " + (-diff).ToString("N0") + " FCFA  à "+compte;
                        else if (achat + redevance < payement)
                        {
                            lblSolde.Text = compte +" vous doit " + diff.ToString("N0") + " FCFA";
                        }
                        else
                        {
                            lblSolde.Text = "Compte " + compte + " Soldé !";
                        }
                        lblTotalTransaction.Text = "Total Débit : " + fournisseur.ToString("N0") + " FCFA, Total Crédit : " + payement.ToString("N0") + " FCFA"+", Rédévance : "+redevance.ToString("N0");
                        dataGridView1.DataSource = data.Result;
                    }
                }
                tasklist.Remove(finishedTask);
            }
        }
        async Task CallMode(ComboBox cbx)
        {
            var result = await FillModeAsync();
            cbx.DataSource = result;
            cbx.DisplayMember = "Mode";
            cbx.ValueMember = "Id";
            cbx.Text = null;
        }
        public static Task<MesureTable> FillModeAsync()
        {
            return Task.Factory.StartNew(() => FillMode());
        }
        private static MesureTable FillMode()
        {
            MesureTable mesureTable = new MesureTable();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Mode");
            using (var donnée = new QuitayeContext())
            {
                var der = from d in donnée.tbl_mode_payement 
                          orderby d.Id descending select new { Id = d.Id, Mode = d.Mode, Default = d.Defaut };
                if (der.Count() != 0)
                {
                    mesureTable.RowsLists = new List<RowsList>();
                    foreach (var item in der)
                    {
                        RowsList rows = new RowsList();
                        DataRow dr = dt.NewRow();
                        dr[0] = item.Id;
                        dr[1] = item.Mode;

                        if (item.Default == "Oui")
                        {
                            rows.Default = true;
                            rows.Mesure = item.Mode;
                        }
                        else rows.Default = false;
                        mesureTable.RowsLists.Add(rows);
                        dt.Rows.Add(dr);
                    }
                }
            }
            mesureTable.Table = dt;
            return mesureTable;

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
                var der = from d in donnée.tbl_payement group d by new { Type = d.Type } into gr select new { Id = gr.Key.Type, Type = gr.Key.Type, };
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

        protected override void WndProc(ref Message m)
        {
            //if (m.Msg == 0x84)
            //{
            //    Point pos = new Point(m.LParam.ToInt32());
            //    pos = PointToClient(pos);
            //    if (pos.Y < cCaption)
            //    {
            //        m.Result = (IntPtr)2;
            //        return;
            //    }
            //    if (pos.X >= ClientSize.Width - cGrip && pos.Y >= ClientSize.Height - cGrip)
            //    {
            //        m.Result = (IntPtr)17;
            //        return;
            //    }
            //}
            //base.WndProc(ref m);


            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                    if ((int)m.Result == 0x01/*HTCLIENT*/)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        }
                        else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        }
                        else
                        {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
            }
            base.WndProc(ref m);
        }

        private async Task CallData()
        {
            var result = await FillDataAsync();
            dataGridView1.Columns.Clear();
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
                var achat = li.Where(x => x.Type == "Achat").Sum(x => x.Montant);
                var payement = li.Where(x => x.Type == "Payement").Sum(x => x.Montant);
                var redevance = li.Sum(x => x.Solde);
                var fournisseur = (achat + redevance);
                var diff = payement - fournisseur;

                if (achat + redevance > payement)
                    lblSolde.Text = " Vous devez " + (-diff).ToString("N0") + " FCFA  à " + compte;
                else if (achat + redevance < payement)
                {
                    lblSolde.Text = compte + " vous doit " + diff.ToString("N0") + " FCFA";
                }
                else
                {
                    lblSolde.Text = "Compte " + compte + " Soldé !";
                }
                lblTotalTransaction.Text = "Total Débit : " + fournisseur.ToString("N0") + " FCFA, Total Crédit : " + payement.ToString("N0") + " FCFA" + ", Rédévance : " + redevance.ToString("N0")+" FCFA";
                dataGridView1.DataSource = result;
            }
        }
        private Task<DataTable> FillDataAsync()
        {
            return Task.Factory.StartNew(() => FillData());
        }
        List<Transaction> li = new List<Transaction>();
        private DataTable FillData()
        {
            List<Transaction> list = new List<Transaction>();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Num_Ref");
            dt.Columns.Add("Intervenant");
            dt.Columns.Add("Débit");
            dt.Columns.Add("Crédit");
            dt.Columns.Add("Date");
            using (var donnée = new QuitayeContext())
            {
                var ders = from d in donnée.tbl_arrivée
                           where (DbFunctions.TruncateTime(d.Date_Arrivée.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Arrivée.Value) >= DbFunctions.TruncateTime(startDate.Value))
                           && d.Fournisseur == compte && d.Id_Fournisseur == this.id.ToString()
                           group d by new
                           {
                               Num_Achat = d.Num_Achat,
                           } into gr
                           select new
                           {
                               Montant = gr.Sum(x => x.Prix),
                               Fournisseur = gr.FirstOrDefault().Fournisseur,
                               Bon_Commande = gr.FirstOrDefault().Bon_Commande,
                               Date = gr.FirstOrDefault().Date_Action,
                               Num_Facture = gr.Key.Num_Achat,
                           };

                var pay = from d in donnée.tbl_payement
                          where (DbFunctions.TruncateTime(d.Date_Payement.Value) <= DbFunctions.TruncateTime(EndDate.Value)
                          && DbFunctions.TruncateTime(d.Date_Payement.Value) >= DbFunctions.TruncateTime(startDate.Value))
                          && d.Type == "Décaissement" && d.Raison == "Fournisseur" 
                          && (d.Compte_Tier == this.id.ToString())
                          select new
                          {
                              Id = d.Id,
                              Fournisseur = d.Client,
                              Montant = d.Montant,
                              Date = d.Date_Payement,
                              Num_Ref = d.Num_Opération
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
                        Id = id,
                        Type = "Payement",
                        Num_Ref = item.Num_Ref,
                        Fournisseur = item.Fournisseur,
                        Montant = Convert.ToDecimal(item.Montant),
                        Date = Convert.ToDateTime(item.Date),
                    });
                    id++;
                }

                var order = from d in list orderby d.Date descending select d;
                
                foreach (var item in order)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Num_Ref;
                    dr[2] = item.Fournisseur;
                    if (item.Type == "Achat")
                        dr[3] = item.Montant.ToString("N0");
                    else if (item.Type == "Payement")
                        dr[4] = item.Montant.ToString("N0");
                    dr[5] = item.Date.ToString("dd/MM/yyyy");
                    
                    dt.Rows.Add(dr);
                }

                var desf = from d in donnée.tbl_redévance where d.Redéveur == compte select new { Montant = d.Montant };
                var desfr = from d in donnée.tbl_redévance where d.Rédevant == compte select new { Montant = d.Montant };
                list.Add(new Transaction()
                {
                    Solde = Convert.ToDecimal(desfr.Sum(x => x.Montant))- Convert.ToDecimal(desf.Sum(x => x.Montant))
                });
                li = list;
                return dt;
            }
        }
    }
}
