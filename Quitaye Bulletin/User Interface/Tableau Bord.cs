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
    public partial class Tableau_Bord : Form
    {
        string mycontrng = LogIn.mycontrng;

        public Tableau_Bord()
        {
            InitializeComponent();
            timer1.Start();
            lblRevenuGarçon.Visible = false;
            lblRevenuFille.Visible = false;
            lblGlobalRevenu.Visible = false;
            lbClasse.Visible = false;
            lblGarçon.Visible = false;
            lblFille.Visible = false;
            lblEffec.Visible = false;
        }

        decimal total = 0;
        public void FillDG()
        {
            try
            {
                using (var donnée = new QuitayeContext())
                {
                    var s = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire && d.Montant != 0 select d;
                    decimal montant = 0;
                    if (s.Count() > 0)
                    {
                        montant = Convert.ToDecimal(s.Sum(x => x.Montant));
                    }
                    //decimal scolarit = 0;
                    var sse = from d in donnée.tbl_classe select new { Id = d.Id };
                    var premier = from d in donnée.tbl_classe where d.Cycle == "Premier Cycle" select new { Id = d.Id };
                    var second = from d in donnée.tbl_classe where d.Cycle == "Second Cycle" select new { Id = d.Id };
                    var lycéé = from d in donnée.tbl_classe where d.Cycle == "Lycée" select new { Id = d.Id };
                    var université = from d in donnée.tbl_classe where d.Cycle == "Université" select new { Id = d.Id };
                    dataGridView1.DataSource = sse;
                    //decimal montantScolarité = 0;
                    //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    //{
                    //    var so = (from d in donnée.tbl_classe where d.Id == Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) select d).First();
                    //    var sos = from d in donnée.tbl_inscription where d.Classe == so.Nom && d.Année_Scolaire == Principales.annéescolaire && d.Type_Scolarité == "Normal" && d.Active == "Oui" select d;
                    //    montantScolarité += Convert.ToDecimal(so.Scolarité * sos.Count());
                    //    var ss = from d in donnée.tbl_inscription where d.Classe == so.Nom && d.Année_Scolaire == Principales.annéescolaire && d.Type_Scolarité == "Avec Rémise" && d.Active == "Oui" select d;
                    //    decimal sommeremise = Convert.ToDecimal(ss.Sum(x => x.Scolarité));
                    //    montantScolarité += Convert.ToDecimal(sommeremise * ss.Count());
                    //}
                    //var ss = (from d in donnée.tbl_classe where d.Nom == classes select d).First();
                    var masculin = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire && d.Genre == "Masculin" select d;
                    var fille = from d in donnée.tbl_payement where d.Classe != "Dépnses" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire && d.Genre == "Feminin" select d;
                    var c = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire select d;
                    var cyclepremier = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Cycle == "Premier Cycle" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire select d;
                    var cyclesecond = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Cycle == "Second Cycle" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire select d;
                    var cyclelycée = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Cycle == "Lycée" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire select d;
                    var cycleuniversité = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Cycle == "Université" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire select d;

                    var cfille = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Type == "Scolarité" && d.Genre == "Feminin" && d.Année_Scolaire == Principales.annéescolaire select d;
                    var fillecyclepremier = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Cycle == "Premier Cycle" && d.Genre == "Feminin" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire select d;
                    var fillecyclesecond = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Cycle == "Second Cycle" && d.Genre == "Feminin" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire select d;
                    var fillecyclelycée = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Cycle == "Lycée" && d.Genre == "Feminin" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire select d;
                    var fillecycleuniversité = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Cycle == "Université" && d.Genre == "Feminin" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire select d;


                    var cgarçon = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Type == "Scolarité" && d.Genre == "Masculin" && d.Année_Scolaire == Principales.annéescolaire select d;
                    var garçoncyclepremier = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Cycle == "Premier Cycle" && d.Genre == "Masculin" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire select d;
                    var garçoncyclesecond = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Cycle == "Second Cycle" && d.Genre == "Masculin" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire select d;
                    var garçoncyclelycée = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Cycle == "Lycée" && d.Genre == "Masculin" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire select d;
                    var garçoncycleuniversité = from d in donnée.tbl_payement where d.Classe != "Dépenses" && d.Cycle == "Université" && d.Genre == "Masculin" && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire select d;


                    //scolarit = Convert.ToDecimal(ss.Scolarité * c.Count());
                    decimal payée = Convert.ToDecimal(s.Sum(x => x.Montant));

                    var g = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" && d.Genre == "Masculin" select d;
                    var gpremier = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" && d.Genre == "Masculin" && d.Cycle == "Premier Cycle" select d;
                    var gsecond = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" && d.Genre == "Masculin" && d.Cycle == "Second Cycle" select d;
                    var glycée = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" && d.Genre == "Masculin" && d.Cycle == "Lycée" select d;
                    var guniversité = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" && d.Genre == "Masculin" && d.Cycle == "Université" select d;
                    //var tranche1 = from d in donnée.tbl_payement where d.Année_Scolaire == Principales.annéescolaire && d.Type == "Scolarité" select d;


                    var f = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" && d.Genre == "Feminin" select d;
                    var fpremier = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" && d.Genre == "Feminin" && d.Cycle == "Premier Cycle" select d;
                    var fsecond = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" && d.Genre == "Feminin" && d.Cycle == "Second Cycle" select d;
                    var flycée = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" && d.Genre == "Feminin" && d.Cycle == "Lycée" select d;
                    var funiversité = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" && d.Genre == "Feminin" && d.Cycle == "Université" select d;
                    var tranche1 = from d in donnée.tbl_payement where d.Année_Scolaire == Principales.annéescolaire && d.Type == "Scolarité" select d;
                    total = Convert.ToDecimal(tranche1.Sum(x => x.Montant));
                    var effect = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" select d;
                    var effectPremier = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == "Premier Cycle" && d.Active == "Oui" select d;
                    var effectlycée = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == "Lycée" && d.Active == "Oui" select d;
                    var effectSecond = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == "Second Cycle" && d.Active == "Oui" select d;
                    var effectuniversité = from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == "Université" && d.Active == "Oui" select d;
                    string eleveclasse = " élève(s) / classe";

                    lblFille.Text = "Fille : " + f.Count();
                    lblGarçon.Text = "Garçon : " + g.Count();
                    lblEffec.Text = "Effectif : " + Convert.ToDecimal(effect.Count()).ToString("N0");
                    if (sse.Count() != 0)
                    {
                        lblGlobal.Text = "Global : " + Math.Round(effect.Count() / Convert.ToDecimal(sse.Count()), 1) + eleveclasse;

                    }

                    else lblGlobal.Text = "Global : 0" + eleveclasse;
                    if (lycéé.Count() != 0)
                        lblEffectifLycée.Text = "Lycée : " + Math.Round(effectlycée.Count() / Convert.ToDecimal(lycéé.Count()), 1) + eleveclasse;
                    else lblEffectifLycée.Text = "Lycée : 0 " + eleveclasse;
                    if (premier.Count() != 0)
                        lblEffectifPremierCycle.Text = "Premier Cycle : " + Math.Round(effectPremier.Count() / Convert.ToDecimal(premier.Count()), 1) + eleveclasse;

                    else lblEffectifPremierCycle.Text = "Premier Cycle : 0" + eleveclasse;
                    if (second.Count() != 0)
                        lblEffectifSecondCycle.Text = "Second Cycle : " + Math.Round(effectSecond.Count() / Convert.ToDecimal(second.Count()), 1) + eleveclasse;
                    else lblEffectifSecondCycle.Text = "Second Cycle : 0" + eleveclasse;
                    if (université.Count() != 0)
                        lblEffectifUniversité.Text = "Université : " + Math.Round(effectuniversité.Count() / Convert.ToDecimal(université.Count()), 1) + eleveclasse;
                    else lblEffectifUniversité.Text = "Université : 0" + eleveclasse;

                    string elevev = " / élève";
                    if (effect.Count() != 0)
                        lblGlobalRevenu.Text = "Global : " + Convert.ToDecimal(Math.Round(Convert.ToDecimal(c.Sum(x => x.Montant)) / Convert.ToDecimal(effect.Count()), 1)).ToString("N0") + " FCFA" + elevev;
                    else lblGlobalRevenu.Text = "Global : 0 FCFA" + elevev;

                    if (effectPremier.Count() != 0)
                        lblGlobalPremierCycle.Text = "Premier Cycle : " + Convert.ToDecimal(Math.Round(Convert.ToDecimal(cyclepremier.Sum(x => x.Montant)) / Convert.ToDecimal(effectPremier.Count()), 1)).ToString("N0") + " FCFA" + elevev;
                    else lblGlobalPremierCycle.Text = "Premier Cycle : 0 FCFA" + elevev;

                    if (effectSecond.Count() != 0)
                        lblGlobalSecondCycle.Text = "Second Cycle : " + Convert.ToDecimal(Math.Round(Convert.ToDecimal(cyclesecond.Sum(x => x.Montant)) / Convert.ToDecimal(effectSecond.Count()), 1)).ToString("N0") + " FCFA" + elevev;
                    else lblGlobalSecondCycle.Text = "Second Cycle : 0 FCFA" + elevev;

                    if (effectlycée.Count() != 0)
                        lblGlobalLycée.Text = "Lycée : " + Convert.ToDecimal(Math.Round(Convert.ToDecimal(cyclelycée.Sum(x => x.Montant)) / Convert.ToDecimal(effectlycée.Count()), 1)).ToString("N0") + " FCFA" + elevev;
                    else lblGlobalLycée.Text = "Lycée : 0 FCFA" + elevev;

                    if (effectuniversité.Count() != 0)
                        lblGlobalUniversité.Text = "Université : " + Convert.ToDecimal(Math.Round(Convert.ToDecimal(cycleuniversité.Sum(x => x.Montant)) / Convert.ToDecimal(effectuniversité.Count()), 1)).ToString("N0") + " FCFA" + elevev;
                    else lblGlobalUniversité.Text = "Université : 0 FCFA" + elevev;

                    if (f.Count() != 0)
                        lblRevenuFille.Text = "Global : " + Convert.ToDecimal(Math.Round(Convert.ToDecimal(cfille.Sum(x => x.Montant)) / Convert.ToDecimal(f.Count()), 1)).ToString("N0") + " FCFA" + elevev;
                    else lblRevenuFille.Text = "Global : 0 FCFA" + elevev;

                    if (fpremier.Count() != 0)
                        lblFillPremierCycle.Text = "Premier Cycle : " + Convert.ToDecimal(Math.Round(Convert.ToDecimal(fillecyclepremier.Sum(x => x.Montant)) / Convert.ToDecimal(fpremier.Count()), 1)).ToString("N0") + " FCFA" + elevev;
                    else lblFillPremierCycle.Text = "Premier Cycle : 0 FCFA" + elevev;

                    if (fsecond.Count() != 0)
                        lblFilleSecondCycle.Text = "Second Cycle : " + Convert.ToDecimal(Math.Round(Convert.ToDecimal(fillecyclesecond.Sum(x => x.Montant)) / Convert.ToDecimal(fsecond.Count()), 1)).ToString("N0") + " FCFA" + elevev;
                    else lblFilleSecondCycle.Text = "Second Cycle : 0 FCFA" + elevev;

                    if (flycée.Count() != 0)
                        lblFilleLycée.Text = "Lycée : " + Convert.ToDecimal(Math.Round(Convert.ToDecimal(fillecyclelycée.Sum(x => x.Montant)) / Convert.ToDecimal(flycée.Count()), 1)).ToString("N0") + " FCFA" + elevev;
                    else lblFilleLycée.Text = "Lycée : 0 FCFA" + elevev;

                    if (funiversité.Count() != 0)
                        lblFillUniversité.Text = "Université : " + Convert.ToDecimal(Math.Round(Convert.ToDecimal(fillecycleuniversité.Sum(x => x.Montant)) / Convert.ToDecimal(funiversité.Count()), 1)).ToString("N0") + " FCFA" + elevev;
                    else lblFillUniversité.Text = "Université : 0 FCFA" + elevev;

                    if (g.Count() != 0)
                        lblRevenuGarçon.Text = "Global : " + Convert.ToDecimal(Math.Round(Convert.ToDecimal(cgarçon.Sum(x => x.Montant)) / Convert.ToDecimal(g.Count()), 1)).ToString("N0") + " FCFA" + elevev;
                    else lblRevenuGarçon.Text = "Global : 0 FCFA" + elevev;

                    if (gpremier.Count() != 0)
                        lblGarçonPremierCycle.Text = "Premier Cycle : " + Convert.ToDecimal(Math.Round(Convert.ToDecimal(garçoncyclepremier.Sum(x => x.Montant)) / Convert.ToDecimal(gpremier.Count()), 1)).ToString("N0") + " FCFA" + elevev;
                    else lblGarçonPremierCycle.Text = "Premier Cycle : 0 FCFA" + elevev;

                    if (gsecond.Count() != 0)
                        lblGarçonSecondCycle.Text = "Second Cycle : " + Convert.ToDecimal(Math.Round(Convert.ToDecimal(garçoncyclesecond.Sum(x => x.Montant)) / Convert.ToDecimal(gsecond.Count()), 1)).ToString("N0") + " FCFA" + elevev;
                    else lblGarçonSecondCycle.Text = "Second Cycle : 0 FCFA" + elevev;

                    if (glycée.Count() != 0)
                        lblGarçonLycée.Text = "Lycée : " + Convert.ToDecimal(Math.Round(Convert.ToDecimal(garçoncyclelycée.Sum(x => x.Montant)) / Convert.ToDecimal(glycée.Count()), 1)).ToString("N0") + " FCFA" + elevev;
                    else lblGarçonLycée.Text = "Lycée : 0 FCFA" + elevev;

                    if (guniversité.Count() != 0)
                        lblGarçonUniversité.Text = "Université : " + Convert.ToDecimal(Math.Round(Convert.ToDecimal(garçoncycleuniversité.Sum(x => x.Montant)) / Convert.ToDecimal(guniversité.Count()), 1)).ToString("N0") + " FCFA" + elevev;
                    else lblGarçonUniversité.Text = "Université : 0 FCFA" + elevev;


                    lblGlobalRevenu.Visible = true;
                    lblRevenuGarçon.Visible = true;
                    lblRevenuFille.Visible = true;

                    lblGlobal.Visible = true;
                    lbClasse.Visible = true;
                    lblEffec.Visible = true;
                    lblFille.Visible = true;
                    lblGarçon.Visible = true;
                    lbClasse.Text = "Total : " + sse.Count();
                    lblPremierCycle.Text = "Premier Cycle : " + premier.Count();
                    lblSecondCycle.Text = "Second Cycle : " + second.Count();
                    lblLycée.Text = "Lycée : " + lycéé.Count();
                    lblUniversité.Text = "Université : " + université.Count();
                }
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
                }
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            FillDG();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
