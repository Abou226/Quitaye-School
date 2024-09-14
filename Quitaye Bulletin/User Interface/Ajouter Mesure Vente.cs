using Quitaye_School.Models;
using Quitaye_School.Models.Context;
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
    public partial class Ajouter_Mesure_Vente : Form
    {
        public Ajouter_Mesure_Vente()
        {
            InitializeComponent();
            btnAjouter.Click += BtnAjouter_Click;
            radioButton1.CheckedChanged += RadioButton_CheckedChanged;
            radioButton2.CheckedChanged += RadioButton_CheckedChanged;
            radioButton3.CheckedChanged += RadioButton_CheckedChanged;
            btnFermer.Click += BtnFermer_Click;
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            txtNom.TextChanged += TxtNom_TextChanged;
            agrandirmesure.Enabled = false;
            agrandirmesure.Interval = 10;
            agrandirmesure.Start();
            agrandirmesure.Tick += Agrandirmesure_Tick;
        }

        public bool derouler_comparaison;
        private void Agrandirmesure_Tick(object sender, EventArgs e)
        {

        }

        private void CheckComparaison_CheckedChanged(object sender, EventArgs e)
        {
            agrandirmesure.Start();
        }

        Timer agrandirmesure = new Timer();

        private void TxtNom_TextChanged(object sender, EventArgs e)
        {

        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            //await EtablirComparaison();
            await FillFlow();
        }
        public string ok;
        List<Comparaison_Mesure> comparaison_Mesure = new List<Comparaison_Mesure>();
        //Comparaison_Ensemble comparaison;
        List<Mesure> list_mesure = new List<Mesure>();

        private async Task FillFlow()
        {
            list_mesure = await MesureListAsync();
            foreach (var item in list_mesure.OrderBy(x => x.Type))
            {
                if (item.Type == "Petit")
                    radioButton1.Enabled = false;
                else if (item.Type == "Moyen")
                {
                    radioButton2.Enabled = false;
                }
                else if (item.Type == "Grand")
                {
                    radioButton3.Enabled = false;
                }
                else if (item.Type == "Large")
                {
                    radioButton4.Enabled = false;
                }
                else if (item.Type == "Hyper Large")
                {

                }
                comparaison_Mesure.Add(new Comparaison_Mesure()
                {
                    Id = item.Id,
                    Mesure = item.Nom,
                    Type = item.Type,
                });
            }

            var liste = from d in comparaison_Mesure orderby d.Type descending select d;
            if (liste.Count() >= 2)
            {
                var lis = (from d in comparaison_Mesure orderby d.Type descending select d).First();
                List<Comparaison_Mesure> lsi = (from d in comparaison_Mesure where d.Type != lis.Type orderby d.Type select d).ToList();
            }
        }

        Timer loadTimer = new Timer();

        private Task<List<Mesure>> MesureListAsync()
        {
            return Task.Factory.StartNew(() => MesureList());
        }
        private List<Mesure> MesureList()
        {
            List<Mesure> list = new List<Mesure>();
            using (var donnée = new QuitayeContext())
            {
                var der = from d in donnée.tbl_mesure_vente orderby d.Type ascending select new { Id = d.Id, Nom = d.Nom, Type = d.Type };
                foreach (var item in der)
                {
                    list.Add(new Mesure()
                    {
                        Id = item.Id,
                        Type = item.Type,
                        Nom = item.Nom,
                    });
                }
            }
            return list;
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        string type = "";
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            type = ((RadioButton)sender).Text;
        }

        private async void BtnAjouter_Click(object sender, EventArgs e)
        {
            if (txtNom.Text != "" && type != "")
            {
                RowsList rows = new RowsList();
                rows.Mesure = txtNom.Text;
                rows.Type = type;
                rows.Default = checkBox1.Checked;
                var result = await InsertDataAsync(rows);
                if (result)
                {
                    txtNom.Clear();
                    foreach (var item in flowLayoutPanel1.Controls)
                    {
                        ((RadioButton)item).Checked = false;
                    }
                    type = "";
                    await FillFlow();
                    checkBox1.Checked = false;
                    ok = "Oui";
                    Alert.SShow("Mesure ajoutée avec succès.", Alert.AlertType.Sucess);
                }
            }
        }

        
        private async Task<bool> InsertDataAsync(RowsList rows)
        {
            using (var donnée = new QuitayeContext())
            {
                int id = 1;
                var ders = from d in donnée.tbl_mesure_vente select d;
                if (ders.Count() != 0)
                {
                    var dese = (from d in donnée.tbl_mesure_vente orderby d.Id descending select d).First();
                    id = dese.Id + 1;
                }

                var tb = new tbl_mesure_vente();
                tb.Id = id;
                tb.Nom = rows.Mesure;
                tb.Type = rows.Type;
                if (rows.Type == "Petit")
                    tb.Niveau = 1;
                else if (rows.Type == "Moyen")
                    tb.Niveau = 2;
                else if (rows.Type == "Grand")
                    tb.Niveau = 3;
                else if (rows.Type == "Large")
                    tb.Niveau = 4;
                else if (rows.Type == "Hyper Large")
                    tb.Niveau = 5;
                if (id == 1)
                    rows.Default = true;
                if (rows.Default)
                {
                    tb.Default = "Oui";
                    var defu = from d in donnée.tbl_mesure_vente where d.Default == "Oui" select d;
                    foreach (var item in defu)
                    {
                        item.Default = "Non";
                        await donnée.SaveChangesAsync();
                    }
                }
                else tb.Default = "Non";
                tb.Date = DateTime.Now;
                tb.Auteur = Principales.profile;
                donnée.tbl_mesure_vente.Add(tb);
                await donnée.SaveChangesAsync();
                return true;
            }
        }
    }
}
