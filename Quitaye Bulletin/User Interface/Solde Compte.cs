using Microsoft.Office.Interop.Excel;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Solde_Compte : Form
    {
        public Solde_Compte()
        {
            InitializeComponent();
            AddData();
            timerrefresh.Enabled = false;
            timerrefresh.Interval = 10;
            timerrefresh.Start();
            timerrefresh.Tick += Timerrefresh_Tick;
            FillData();
            lblOperation.Text = "Opération : " + list[0];
        }

        public static string operations;

        private void FillData()
        {
            using(var donnée = new QuitayeContext())
            {
                try
                {
                    var id = list[0];
                    var tbl = (from d in donnée.tbl_payement
                              where d.Opération == id && d.Année_Scolaire == Principales.annéescolaire
                                 && d.Montant != 0
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
                    dataGridView1.DataSource = tbl;
                }
                catch (Exception ex)
                {
                    var w32ex = ex as SqlException;
                    if (w32ex == null)
                    {
                        w32ex = ex.InnerException as SqlException;
                        int code = w32ex.ErrorCode;
                    }
                    if (w32ex != null)
                    {
                        int code = w32ex.ErrorCode;
                        // do stuff

                        if (code == -2146232060)
                        {
                            MsgBox msg = new MsgBox();
                            msg.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                            msg.ShowDialog();
                        }
                        else
                        {
                            MsgBox msg = new MsgBox();
                            msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                            msg.ShowDialog();
                        }
                    }
                }
            }
        }
        private void Timerrefresh_Tick(object sender, EventArgs e)
        {
            if (refresh)
            {
                refresh = false;
                try
                {
                    var donnée = new QuitayeContext();

                    var tbl = (from d in donnée.tbl_payement
                              where d.Opération == operations && d.Année_Scolaire == Principales.annéescolaire
                                 && d.Montant != 0
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
                    lblOperation.Text = "Opération : " + operations;
                    dataGridView1.DataSource = tbl;
                }
                catch (Exception ex)
                {
                    var w32ex = ex as SqlException;
                    if (w32ex == null)
                    {
                        w32ex = ex.InnerException as SqlException;
                        int code = w32ex.ErrorCode;
                    }
                    if (w32ex != null)
                    {
                        int code = w32ex.ErrorCode;
                        // do stuff

                        if (code == -2146232060)
                        {
                            MsgBox msg = new MsgBox();
                            msg.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                            msg.ShowDialog();
                        }else
                        {
                            MsgBox msg = new MsgBox();
                            msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                            msg.ShowDialog();
                        }
                    }
                }
                
            }
        }

        static List<string> list = new List<string>{"Espèce", "Chèque", "Virement", "Carte Bleu" };
        Custom_Solde[] sold = new Custom_Solde[list.Count];

        public static bool refresh = false;
        private void AddData()
        {
            using(var donné = new QuitayeContext())
            {
               flowLayoutPanel1.Controls.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    var id = list[i];
                    var depenses = (from d in donné.tbl_payement where d.Opération == id && d.Année_Scolaire == Principales.annéescolaire && d.Type == "Dépenses" select new {Montant = d.Montant}).ToList();
                    var recette = (from d in donné.tbl_payement where d.Opération  == id && d.Année_Scolaire == Principales.annéescolaire &&  d.Type != "Dépenses" select new {Montant = d.Montant});

                    sold[i] = new Custom_Solde();
                    sold[i].Id = i;
                    sold[i].Titre = id;
                    sold[i].Solde = "Solde : "+ Convert.ToDecimal(Convert.ToDecimal(recette.Sum(x => x.Montant)) - Convert.ToDecimal(depenses.Sum(x => x.Montant))).ToString("N0");
                    sold[i].Margin = new Padding(20);
                    flowLayoutPanel1.Controls.Add(sold[i]);
                }
            }
        }

        Timer timerrefresh = new Timer();
    }
}
