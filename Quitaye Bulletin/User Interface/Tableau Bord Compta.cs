using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Tableau_Bord_Compta : Form
    {
        string mycontrng = LogIn.mycontrng;
        public string classes;
        public string cycle;

        public Tableau_Bord_Compta()
        {
            InitializeComponent();
            timer1.Start();
            
            lblGarçon.Visible = false;
            lblFille.Visible = false;
            lblEffec.Visible = false;
            ShowChartsData.Enabled = false;
            ShowChartsData.Interval = 10;
            ShowChartsData.Start();
            ShowChartsData.Tick += ShowChartsData_Tick;
        }

        private void ShowChartsData_Tick(object sender, EventArgs e)
        {
            ShowChartsData.Stop();
            ShowCharts();
        }

        Timer ShowChartsData = new Timer();
        


        decimal total = 0;
        //public void FillDG()
        //{
        //    try
        //    {
        //        using (var donnée = new QuitayeContext())
        //        {
        //            var s = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire && d.Montant != 0 select d;
        //            decimal montant = 0;
        //            if (s.Count() > 0)
        //            {
        //                montant = Convert.ToDecimal(s.Sum(x => x.Montant));
        //            }
        //            //decimal scolarit = 0;
        //            var sse = from d in donnée.tbl_classe select new { Id = d.Id };
        //            dataGridView1.DataSource = sse;
        //            decimal montantScolarité = 0;
        //            for (int i = 0; i < dataGridView1.Rows.Count; i++)
        //            {
        //                var so = (from d in donnée.tbl_classe where d.Id == Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) select d).First();
        //                var sos = from d in donnée.tbl_inscription where d.Classe == so.Nom && d.Année_Scolaire == Principales.annéescolaire && d.Type_Scolarité == "Normal" && d.Active == "Oui" select d;
        //                montantScolarité += Convert.ToDecimal(so.Scolarité * sos.Count());
        //                var ss = from d in donnée.tbl_inscription where d.Classe == so.Nom && d.Année_Scolaire == Principales.annéescolaire && d.Type_Scolarité == "Avec Rémise" && d.Active == "Oui" select d;
        //                decimal sommeremise = Convert.ToDecimal(ss.Sum(x => x.Scolarité));
        //                montantScolarité += Convert.ToDecimal(sommeremise * ss.Count());
        //            }
        //            //var ss = (from d in donnée.tbl_classe where d.Nom == classes select d).First();
        //            var masculin = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire && d.Genre == "Masculin" select d;
        //            var fille = from d in donnée.tbl_payement where d.Classe != "Dépnses" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire && d.Genre == "Feminin" select d;
        //            var c = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire select d;
        //            //scolarit = Convert.ToDecimal(ss.Scolarité * c.Count());
        //            decimal payée = Convert.ToDecimal(s.Sum(x => x.Montant));

        //            var g = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" && d.Genre == "Masculin" select d;
        //            var f = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" && d.Genre == "Feminin" select d;
        //            var tranche1 = from d in donnée.tbl_payement where d.Année_Scolaire == Principales.annéescolaire && d.Type == "Scolarité" select d;
        //            total = Convert.ToDecimal(tranche1.Sum(x => x.Montant));
        //            var effect = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire where d.Active == "Oui" select d;
                    
        //            lblEffec.Text = "Effectif : " + Convert.ToDecimal(effect.Count()).ToString("N0");
        //            lblFille.Text = "Fille(s) : " + Convert.ToDecimal(f.Count()).ToString("N0");
        //            lblGarçon.Text = "Garçon(s) : " + Convert.ToDecimal(g.Count()).ToString("N0");
        //            //lblTranche1.
                    
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var w32ex = ex as SqlException;
        //        if (w32ex == null)
        //        {
        //            w32ex = ex.InnerException as SqlException;
        //            int code = w32ex.ErrorCode;
        //        }
        //        if (w32ex != null)
        //        {
        //            int code = w32ex.ErrorCode;
        //            // do stuff

        //            if (code == -2146232060)
        //            {
        //                MsgBox msg = new MsgBox();
        //                msg.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
        //                msg.ShowDialog();
        //            }
        //            else
        //            {
        //                MsgBox msg = new MsgBox();
        //                msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
        //                msg.ShowDialog();
        //            }
        //        }
        //    }
        //}

        //decimal tranche1 = 0;
        
        private void flowLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }
        private Task<List<decimal>> CallDadaAsync()
        {
            return Task.Factory.StartNew(() => CallData());
        }
        private List<decimal> CallData()
        {
            List<decimal> list = new List<decimal>();
            using(var donnée = new QuitayeContext())
            {
                var garcon = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" && d.Genre == "Masculin" select d;
                var fille = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" && d.Genre == "Feminin" select d;
                decimal f = Convert.ToDecimal(fille.Count());
                decimal g = Convert.ToDecimal(garcon.Count());

                list.Add(f);
                list.Add(g);
                list.Add(g + f);
            }
            return list;
        }

        List<decimal> listcalculeprevision = new List<decimal>();
        private async void ShowCharts()
        {
            var calculeprevision = CalculePrevisionScolaritéAsync();
            var previsioncycle = CalculePrevisionScolaritéAsyncCycle();
            var encaissemencycle = EncaissementCycleAsync();
            var encaissementfille = EncaissementFilleAsync();
            var encaissementgarçon = EncaissementGarçonAsync();
            var effectif = CallDadaAsync();
           await Task.WhenAll(encaissementfille, encaissementgarçon);
            var encfille = encaissementfille.Result;
            var engarçon = encaissementgarçon.Result;

            await Task.WhenAll(calculeprevision);
            var taskList = new List<Task> { calculeprevision, previsioncycle, encaissemencycle, effectif };
            listcalculeprevision = calculeprevision.Result;
            int index = 0;
            foreach (var item in listcalculeprevision)
            {
                string genre;
                if (index == 0)
                {
                    genre = "Fille";
                }
                else genre = "Garçon";
                ChartPrevisionScolarité.Series["Series1"].Points.AddXY(genre, item.ToString("N0"));
                
                index++;
            }
            lblMontantPrevisionScolarité.Text = "Prevision Scolarité : "+ (calculeprevision.Result[0] + calculeprevision.Result[1]).ToString("N0")+" FCFA";
            lblMontantTotalEncaissement.Text = "Montant Encaissement : "+(encaissementfille.Result + encaissementgarçon.Result).ToString("N0") + " FCFA";
            lblMontantTotalEncaissement.Visible = true;
            lblMontantPrevisionScolarité.Visible = true;
            index = 0;
            chartEncaissement.Series["Series1"].Points.Clear();
            chartEncaissement.Series["Series1"].Points.AddXY("Fille", encfille);
            chartEncaissement.Series["Series1"].Points.AddXY("Garçon", engarçon);

            while (taskList.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(taskList);
                if(finishedTask == previsioncycle)
                {
                    List<PrevisionScolarité> prevision = new List<PrevisionScolarité>();
                    prevision = previsioncycle.Result;
                    ChartScolaritéCycle.Series["Series1"].Points.Clear();
                    foreach (var item in prevision)
                    {
                        ChartScolaritéCycle.Series["Series1"].Points.AddXY(item.Nom_Cycle, item.Donnée);
                    }
                }else if(finishedTask == encaissemencycle)
                {
                    List<PrevisionScolarité> prevision = new List<PrevisionScolarité>();
                    prevision = encaissemencycle.Result;
                    ChartEncaissementCycle.Series["Series1"].Points.Clear();
                    foreach (var item in prevision)
                    {
                        ChartEncaissementCycle.Series["Series1"].Points.AddXY(item.Nom_Cycle, item.Donnée);
                    }
                }else if(finishedTask == effectif)
                {
                    lblEffec.Text = "Effectif : " + Convert.ToDecimal(effectif.Result[2]).ToString("N0");
                    lblFille.Text = "Fille(s) : " + Convert.ToDecimal(effectif.Result[0]).ToString("N0");
                    lblGarçon.Text = "Garçon(s) : " + Convert.ToDecimal(effectif.Result[1]).ToString("N0");
                    lblFille.Visible = true;
                    lblGarçon.Visible = true;
                    lblEffec.Visible = true;
                }
                taskList.Remove(finishedTask);
            }

            index = 0;
        }

        private Task<List<decimal>> CalculePrevisionScolaritéAsync()
        {
            return Task.Factory.StartNew(() => CalculePrevisionScolarité());
        }
        private List<decimal> CalculePrevisionScolarité()
        {
            List<decimal> list = new List<decimal>();
            using(var donnée = new QuitayeContext())
            {
                var cla = from d in donnée.tbl_classe select d;
                decimal f = 0;
                decimal g = 0;
                foreach (var item in cla)
                {
                    var fillenormal = from d in donnée.tbl_inscription 
                                      where d.Année_Scolaire == Principales.annéescolaire 
                                      && d.Classe == item.Nom && d.Genre == "Feminin" 
                                      && d.Active == "Oui" && d.Type_Scolarité == "Normal" 
                                      select d;
                    
                    var garconnormal = from d in donnée.tbl_inscription 
                                       where d.Année_Scolaire == Principales.annéescolaire 
                                       && d.Classe == item.Nom && d.Genre == "Masculin" 
                                       && d.Active == "Oui" && d.Type_Scolarité == "Normal" 
                                       select d;
                    f += Convert.ToDecimal(fillenormal.Count() * item.Scolarité);
                    g += Convert.ToDecimal(garconnormal.Count() * item.Scolarité);
                }

                var garconRemise = from d in donnée.tbl_inscription 
                                   where d.Année_Scolaire == Principales.annéescolaire 
                                   && d.Genre == "Masculin" && d.Active == "Oui" 
                                   && d.Type_Scolarité == "Avec Rémise" 
                                   select d;
                var filleRemise = from d in donnée.tbl_inscription 
                                  where d.Année_Scolaire == Principales.annéescolaire 
                                  && d.Genre == "Feminin" && d.Active == "Oui" 
                                  && d.Type_Scolarité == "Avec Rémise" 
                                  select d;
                f += Convert.ToDecimal(filleRemise.Sum(x => x.Scolarité));
                g += Convert.ToDecimal(garconRemise.Sum(x => x.Scolarité));
                list.Add(f);
                list.Add(g);
            }
            return list;
        }

        private Task<List<PrevisionScolarité>> CalculePrevisionScolaritéAsyncCycle()
        {
            return Task.Factory.StartNew(() => CalculePrevisionScolaritéCycle());
        }
        private List<PrevisionScolarité> CalculePrevisionScolaritéCycle()
        {
            List<PrevisionScolarité> prevision = new List<PrevisionScolarité>();
            using (var donnée = new QuitayeContext())
            {
                var cycle = from d in donnée.tbl_classe group d by new { Cycle = d.Cycle} into gr select new { Cycle = gr.Key.Cycle};
                decimal f = 0;
                decimal g = 0;
                foreach (var item in cycle)
                {
                    var cla = from d in donnée.tbl_classe where d.Cycle == item.Cycle select d;
                    foreach (var se in cla)
                    {
                        var fillenormal = from d in donnée.tbl_inscription
                                          where d.Année_Scolaire == Principales.annéescolaire
                                          && d.Cycle == item.Cycle && d.Classe == se.Nom && d.Genre == "Feminin"
                                          && d.Active == "Oui" && d.Type_Scolarité == "Normal"
                                          select d;

                        var garconnormal = from d in donnée.tbl_inscription
                                           where d.Année_Scolaire == Principales.annéescolaire
                                           && d.Cycle == item.Cycle && d.Classe == se.Nom && d.Genre == "Masculin"
                                           && d.Active == "Oui" && d.Type_Scolarité == "Normal"
                                           select d;
                        f += Convert.ToDecimal(fillenormal.Count() * se.Scolarité);
                        g += Convert.ToDecimal(garconnormal.Count() * se.Scolarité);

                        var garconRemise = from d in donnée.tbl_inscription
                                           where d.Année_Scolaire == Principales.annéescolaire
                                           && d.Genre == "Masculin" && d.Cycle == item.Cycle && d.Classe == se.Nom && d.Active == "Oui"
                                           && d.Type_Scolarité == "Avec Rémise"
                                           select d;
                        var filleRemise = from d in donnée.tbl_inscription
                                          where d.Année_Scolaire == Principales.annéescolaire
                                          && d.Cycle == item.Cycle && d.Classe == se.Nom && d.Genre == "Feminin" && d.Active == "Oui"
                                          && d.Type_Scolarité == "Avec Rémise"
                                          select d;
                        f += Convert.ToDecimal(filleRemise.Sum(x => x.Scolarité));
                        g += Convert.ToDecimal(garconRemise.Sum(x => x.Scolarité));

                    }

                    decimal list = 0;
                    list = f + g;
                    prevision.Add(new PrevisionScolarité()
                    {
                        Nom_Cycle = item.Cycle,
                        Donnée = list
                    });
                }
            }
            return prevision;
        }


        private Task<decimal> EncaissementFilleAsync()
        {
            return Task.Factory.StartNew(() => EncaissementFille());
        }

        private decimal EncaissementFille()
        {
            decimal f = 0;
            using (var donnée = new QuitayeContext())
            {
                var cld = from d in donnée.tbl_classe select d;
                
                foreach (var item in cld)
                {
                    var fille = from d in donnée.tbl_inscription
                                      where d.Année_Scolaire == Principales.annéescolaire
                                      && d.Classe == item.Nom && d.Genre == "Feminin"
                                      && d.Active == "Oui"
                                      select d;

                    foreach (var items in fille)
                    {
                        var payement = from d in donnée.tbl_payement
                                       where d.N_Matricule == items.N_Matricule && d.Année_Scolaire == Principales.annéescolaire
                                       select d;
                        f += Convert.ToDecimal(payement.Sum(x => x.Montant));
                    }
                }
            }
            return f;
        }

        private Task<decimal> EncaissementGarçonAsync()
        {
            return Task.Factory.StartNew(() => EncaissementGarçon());
        }

        private decimal EncaissementGarçon()
        {
            decimal f = 0;
            using (var donnée = new QuitayeContext())
            {
                var cld = from d in donnée.tbl_classe select d;

                foreach (var item in cld)
                {
                    var fille = from d in donnée.tbl_inscription
                                where d.Année_Scolaire == Principales.annéescolaire
                                && d.Classe == item.Nom && d.Genre == "Masculin"
                                && d.Active == "Oui"
                                select d;

                    foreach (var items in fille)
                    {
                        var payement = from d in donnée.tbl_payement
                                       where d.N_Matricule == items.N_Matricule && d.Année_Scolaire == Principales.annéescolaire
                                       select d;
                        f += Convert.ToDecimal(payement.Sum(x => x.Montant));
                    }
                }
            }
            return f;
        }


        private Task<List<PrevisionScolarité>> EncaissementCycleAsync()
        {
            return Task.Factory.StartNew(() => EncaissementCycle());
        }

        private List<PrevisionScolarité> EncaissementCycle()
        {
            decimal f = 0;
            List<PrevisionScolarité> pre = new List<PrevisionScolarité>();
            using (var donnée = new QuitayeContext())
            {
                var cycle = from d in donnée.tbl_classe group d by new { Cycle = d.Cycle }into gr select new { Cycle = gr.Key.Cycle};

                foreach (var item in cycle)
                {
                    var cla = from d in donnée.tbl_classe where d.Cycle == item.Cycle select d;
                    foreach (var its in cla)
                    {
                        var fille = from d in donnée.tbl_inscription
                                    where d.Année_Scolaire == Principales.annéescolaire
                                    && d.Cycle == its.Cycle && d.Classe == its.Nom 
                                    && d.Active == "Oui"
                                    select d;

                        foreach (var items in fille)
                        {
                            var payement = from d in donnée.tbl_payement
                                           where d.N_Matricule == items.N_Matricule 
                                           && d.Année_Scolaire == Principales.annéescolaire
                                           select d;
                            f += Convert.ToDecimal(payement.Sum(x => x.Montant));
                        }
                    }
                    pre.Add(new PrevisionScolarité()
                    {
                        Nom_Cycle = item.Cycle,
                        Donnée = f,
                    });
                }
            }
            return pre;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            
        }
    }

    public class PrevisionScolarité
    {
        private decimal _list;

        public decimal Donnée
        {
            get { return _list; }
            set { _list = value; }
        }

        private string _nom;

        public string Nom_Cycle
        {
            get { return _nom; }
            set { _nom = value; }
        }
    }
}
