using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quitaye_School.Models;
using Quitaye_School.Models.Context;

namespace Quitaye_School.User_Interface
{
    public partial class Ajout_Filiale : Form
    {
        public Ajout_Filiale()
        {
            InitializeComponent();
            btnFermer.Click += BtnFermer_Click;
            btnSave.Click += BtnSave_Click;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if(txtNom.Text != "" && txtTelephone.Text != "")
            {
                await CallSave();
            }
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async Task CallSave()
        {
            Filiale filiale = new Filiale();
            filiale.Nom = txtNom.Text;
            filiale.Telephone = txtTelephone.Text;
            filiale.Email = txtEmail.Text;
            filiale.Adresse = txtAdresse.Text;
            filiale.Pays = txtPays.Text;
            filiale.Ville = txtVille.Text;
            var result = await SaveDataAsync(filiale);
            if (result)
            {
                ClearData();
                ok = "Oui";
                Alert.SShow("Filiale ajouté avec succès.", Alert.AlertType.Sucess);
            }
        }

        public string ok;
        private void ClearData()
        {
            txtVille.Text = null;
            txtPays.Text = null;
            txtTelephone.Text = null;
            txtNom.Text = null;
            txtAdresse.Text = null;
            txtEmail.Text = null;
        }
        
        private async Task<bool> SaveDataAsync(Filiale filiale)
        {
            using(var donnée = new QuitayeContext())
            {
                int id = 1;
                var sd = (from d in donnée.tbl_filiale orderby d.Id descending select new { Id = d.Id }).Take(1);
                if(sd.Count() != 0)
                {
                    id = sd.First().Id + 1;
                }
                var fil = new Models.Context.tbl_filiale();
                fil.Id = id;
                fil.Nom = filiale.Nom;
                fil.Adresse = filiale.Adresse;
                fil.Telephone = filiale.Telephone;
                fil.Email = filiale.Email;
                fil.Pays = filiale.Pays;
                fil.Ville = filiale.Ville;
                donnée.tbl_filiale.Add(fil);
                await donnée.SaveChangesAsync();
                return true;
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFermer_Click_1(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
