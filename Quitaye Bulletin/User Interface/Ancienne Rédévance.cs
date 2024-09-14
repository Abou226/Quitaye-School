using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Ancienne_Rédévance : Form
    {
        public Ancienne_Rédévance(string nom)
        {
            InitializeComponent();
            btnFermer.Click += BtnFermer_Click;
            txtmontant.TextChanged += Txtmontant_TextChanged;
            btnEnregistrer.Click += BtnEnregistrer_Click;
            rfournisseur.Text = nom;
        }


        private async void BtnEnregistrer_Click(object sender, EventArgs e)
        {
            if(txtmontant.Text != "" && (rfournisseur.Checked || rvous.Checked))
            {
                Rédévance rédévance = new Rédévance();
                if (rfournisseur.Checked)
                {
                    rédévance.Rédéveur = rfournisseur.Text;
                    rédévance.Rédéveur = "Vous";
                }else if (rvous.Checked)
                {
                    rédévance.Rédéveur = "Vous";
                    rédévance.Rédévant = rfournisseur.Text;
                }
                string result = Regex.Replace(txtmontant.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                rédévance.Montant = Convert.ToDecimal(result);

                var resul = await RedevanceAsync(rédévance);
                if (resul)
                {
                    txtmontant.Text = null;
                    foreach (var item in flowLayoutPanel1.Controls)
                    {
                        RadioButton r = (RadioButton)item;
                        r.Checked = false;
                    }
                    Alert.SShow("Rédévance enrégistrée avec succès.", Alert.AlertType.Sucess);
                }
            }
        }

        private void Txtmontant_TextChanged(object sender, EventArgs e)
        {
            if (txtmontant.Text != "" && txtmontant.Text != "-")
            {
                txtmontant.Text = Convert.ToDecimal(txtmontant.Text).ToString("N0");
                txtmontant.SelectionStart = txtmontant.Text.Length;
            }
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async Task<bool> RedevanceAsync(Rédévance rédévance)
        {
            using(var donnée = new QuitayeContext())
            {
                var deser = (from d in donnée.tbl_redévance select new { Id = d.Id }).Take(1);
                int id = 1;
                if(deser.Count() != 0)
                {
                    var des = deser.First();
                    id = des.Id + 1;
                }
                var tb = new tbl_redévance();
                tb.Redéveur = rédévance.Rédéveur;
                tb.Rédevant = rédévance.Rédévant;
                tb.Montant = rédévance.Montant;
                tb.Auteur = Principales.profile;
                tb.Date = DateTime.Now;
                donnée.tbl_redévance.Add(tb);
                await donnée.SaveChangesAsync();
                return true;
            }
        }
    }
}
