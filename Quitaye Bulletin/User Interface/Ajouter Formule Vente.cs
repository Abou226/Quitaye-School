using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using Quitaye_School.User_Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Ajouter_Formule_Vente : Form
    {
        public Ajouter_Formule_Vente()
        {
            InitializeComponent();
            btnFermer.Click += BtnFermer_Click;
            btnEnregistrer.Click += BtnEnregistrer_Click;
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallListMesure();
        }

        private async void BtnEnregistrer_Click(object sender, EventArgs e)
        {
            var fill = from d in compa where d.Quantité == 0 select new { Id = d.Id };
            Formule_Mesure_Vente formule = new Formule_Mesure_Vente();
            if (fill.Count() == 0 && txtNom.Text != "")
            {
                foreach (var item in compa)
                {
                    if (item.Type == "Petit")
                        formule.Petit = item.Quantité;
                    else if (item.Type == "Moyen")
                        formule.Moyen = item.Quantité;
                    else if (item.Type == "Grand")
                        formule.Grand = item.Quantité;
                    else if (item.Type == "Large")
                        formule.Large = item.Quantité;
                    else if (item.Type == "Hyper Large")
                        formule.Hyper_Large = item.Quantité;
                }
                if (big.Type == "Petit")
                    formule.Petit = 1;
                else if (big.Type == "Moyen")
                    formule.Moyen = 1;
                else if (big.Type == "Grand")
                    formule.Grand = 1;
                else if (big.Type == "Large")
                    formule.Large = 1;
                else if (big.Type == "Hyper Large")
                    formule.Hyper_Large = 1;
                formule.Formule = txtNom.Text;
                var result = await InserFormuleAsync(formule);
                if (result)
                {
                    foreach (var item in compa)
                    {
                        item.Quantité = 0;
                    }

                    txtNom.Clear();

                    Alert.SShow("Formule ajouté avec succès.", Alert.AlertType.Sucess);
                }
            }
            else Alert.SShow("Veillez saisir les equivalent et formule", Alert.AlertType.Info);
        }

        public string ok;

        Timer loadTimer = new Timer();
        Mesure big = new Mesure();
        List<Comparaison_Mesure> compa = new List<Comparaison_Mesure>();
        private async Task CallListMesure()
        {
            list = await ListMesureAsync();

            var bif = from d in list select d;
            if (bif.Count() != 0)
            {
                big = (from d in list orderby d.Niveau descending select d).First();
                var listExcept = from d in list where d.Nom != big.Nom && d.Type != big.Type select d;
                foreach (var item in listExcept.OrderByDescending(x => x.Niveau))
                {
                    compa.Add(new Comparaison_Mesure()
                    {
                        Mesure = item.Nom,
                        Type = item.Type,
                        Id = item.Id,
                    });
                }
                lblEquivalent.Text = "Equivalent 1 " + big.Nom + " ?";
                foreach (var item in compa)
                {
                    flowLayoutPanel1.Controls.Add(item);
                }
            }
        }

        List<Mesure> list = new List<Mesure>();
        private Task<List<Mesure>> ListMesureAsync()
        {
            return Task.Factory.StartNew(() => ListMesure());
        }

        private List<Mesure> ListMesure()
        {
            List<Mesure> list = new List<Mesure>();

            using (var donnée = new QuitayeContext())
            {
                var des = from d in donnée.tbl_mesure_vente orderby d.Niveau descending select d;
                foreach (var item in des)
                {
                    list.Add(new Mesure()
                    {
                        Nom = item.Nom,
                        Niveau = Convert.ToInt32(item.Niveau),
                        Type = item.Type,
                        Id = item.Id,
                    });
                }
            }

            return list;
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Close();
        }


        private async Task<bool> InserFormuleAsync(Formule_Mesure_Vente formule)
        {
            using (var donnée = new QuitayeContext())
            {
                var tbl = new tbl_formule_mesure_vente();

                tbl.Formule = formule.Formule;
                tbl.Date = DateTime.Now;
                tbl.Auteur = Principales.profile;
                tbl.Petit = formule.Petit;
                tbl.Moyen = formule.Moyen;
                tbl.Grand = formule.Grand;
                tbl.Large = formule.Large;
                tbl.Hyper_Large = formule.Hyper_Large;
                donnée.tbl_formule_mesure_vente.Add(tbl);
                await donnée.SaveChangesAsync();
                ok = "Oui";
                return true;
            }
        }
    }
}
