using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Classe : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Classe()
        {
            InitializeComponent();
            timer1.Start();
        }

        public int id;
        public string exnom;
        public string excycle;
        public string ok;
        public static string cycle;
        private async void btnAjouterClasse_Click(object sender, EventArgs e)
        {
            if(txtNom.Text != "" && txtTranche3.Text != "" && txtTranche2.Text != "" && txtTranche1.Text != "" && txtTranche1.Text != "" && cycle != "")
            {
                if(btnAjouterClasse.IconChar == FontAwesome.Sharp.IconChar.Plus)
                {
                    txtScolarité.Text = (Convert.ToDecimal(txtTranche1.Text) + Convert.ToDecimal(txtTranche2.Text) + Convert.ToDecimal(txtTranche3.Text)).ToString();
                    
                        nom = txtNom.Text;
                        scolarit = txtScolarité.Text;
                        tranche1 = txtTranche1.Text;
                        tranche2 = txtTranche2.Text;
                        tranche3 = txtTranche3.Text;
                        await AddClasseAsync();
                        ok = "Oui";
                        ClearData();
                        Alert.SShow("Classe ajouté avec succès.", Alert.AlertType.Sucess);
                    
                }
                else if(btnAjouterClasse.IconChar == FontAwesome.Sharp.IconChar.Edit)
                {
                    txtScolarité.Text = (Convert.ToDecimal(txtTranche1.Text) + Convert.ToDecimal(txtTranche2.Text) + Convert.ToDecimal(txtTranche3.Text)).ToString();
                    using (var donnée = new QuitayeContext())
                    {
                        var classe = (from d in donnée.tbl_classe where d.Id == id select d).First();
                        
                        classe.Nom = txtNom.Text;
                        classe.Tranche_1 = Convert.ToDecimal(txtTranche1.Text);
                        classe.Tranche_2 = Convert.ToDecimal(txtTranche2.Text);
                        classe.Tranche_3 = Convert.ToDecimal(txtTranche3.Text);
                        classe.Cycle = cycle;
                        classe.Scolarité = Convert.ToDecimal(txtScolarité.Text);
                        await donnée.SaveChangesAsync();

                        if (exnom != txtNom.Text)
                        {
                            List<string> er = (from d in donnée.tbl_inscription where d.Classe == exnom && d.Active == "Oui" select d.Id.ToString()).ToList();
                            for (int i = 0; i < er.Count; i++)
                            {
                                var ele = (from d in donnée.tbl_inscription where d.Id == Convert.ToInt32(er[i]) select d).First();
                                ele.Classe = txtNom.Text;
                                await donnée.SaveChangesAsync();
                            }
                        }

                        if(excycle != cycle)
                        {
                            List<string> er = (from d in donnée.tbl_inscription where d.Cycle == excycle && d.Active == "Oui" select d.Id.ToString()).ToList();
                            for (int i = 0; i < er.Count; i++)
                            {
                                var ele = (from d in donnée.tbl_inscription where d.Id == Convert.ToInt32(er[i]) select d).First();
                                ele.Cycle = cycle;
                                await donnée.SaveChangesAsync();
                            }
                        }
                        btnAjouterClasse.Text = "Ajouter Classe";
                        btnAjouterClasse.IconChar = FontAwesome.Sharp.IconChar.Plus;

                        ok = "Oui";
                        ClearData();
                        Alert.SShow("Classe ajouté avec succès.", Alert.AlertType.Sucess);
                    }
                }
                
            }else
            {
                Alert.SShow("Veillez remplir tous les zones de textes.", Alert.AlertType.Info);
            }
        }

        static string nom;
        static string tranche1, tranche2, tranche3, scolarit;
        public static Task AddClasseAsync()
        {
            return Task.Factory.StartNew(() => AddClasse());
        }
        public static async void AddClasse()
        {
            using (var donnée = new QuitayeContext())
            {
                var classe = new Models.Context.tbl_classe();
                classe.Auteur = Principales.profile;
                classe.Date = DateTime.Now;
                classe.Nom = nom;
                classe.Tranche_1 = Convert.ToDecimal(tranche1);
                classe.Tranche_2 = Convert.ToDecimal(tranche2);
                classe.Tranche_3 = Convert.ToDecimal(tranche3);
                classe.Cycle = cycle;
                classe.Scolarité = Convert.ToDecimal(scolarit);
                donnée.tbl_classe.Add(classe);
                await donnée.SaveChangesAsync();
            }
        }

        public void ClearData()
        {
            txtTranche1.Text = "0";
            txtTranche2.Text = "0";
            txtTranche3.Text = "0";
            txtScolarité.Text = "0";
            txtNom.Clear();
        }

        public bool facture_derouler;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (facture_derouler)
            //{
            //    panelFacture.Height += 10;
            //    panelFacture.Visible = true;
            //    if (panelFacture.Size == panelFacture.MaximumSize)
            //    {
            //        timer1.Stop();
            //        facture_derouler = false;
            //    }
            //}
            //else
            //{
            //    panelFacture.Height -= 10;

            //    if (panelFacture.Size == panelFacture.MinimumSize)
            //    {
            //        panelFacture.Visible = false;
            //        timer1.Stop();
            //        facture_derouler = true;
            //    }
            //}
        }

        private void checkFacture_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cycle = "Premier Cycle";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cycle = "Second Cycle";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            cycle = "Lycée";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            cycle = "Université";
        }

        decimal scolarité = 0;
        private void txtTranche3_TextChanged(object sender, EventArgs e)
        {
            if(txtTranche1.Text != "" && txtTranche2.Text != "" && txtTranche3.Text != "")
            {
                scolarité = Convert.ToDecimal(txtTranche1.Text) + Convert.ToDecimal(txtTranche2.Text) + Convert.ToDecimal(txtTranche3.Text);
            }else if(txtTranche1.Text != "" && (txtTranche2.Text == "" || txtTranche3.Text == ""))
            {
                scolarité = Convert.ToDecimal(txtTranche1.Text);
            }
            else if (txtTranche1.Text != "" && txtTranche2.Text != "" && txtTranche3.Text == "")
            {
                scolarité = Convert.ToDecimal(txtTranche1.Text) + Convert.ToDecimal(txtTranche2.Text);
            }
                txtScolarité.Text = scolarité.ToString("N0");
        }

        private void txtTranche3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar != 46 && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            cycle = ((RadioButton)sender).Text;
        }
    }
}
