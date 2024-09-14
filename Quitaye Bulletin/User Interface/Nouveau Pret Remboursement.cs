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
    public partial class Nouveau_Pret_Remboursement : Form
    {
        public Nouveau_Pret_Remboursement()
        {
            InitializeComponent();
            btnFermer.Click += BtnFermer_Click;
            txtmontant.TextChanged += Txtmontant_TextChanged;
            btnAddPayement.Click += BtnAddPayement_Click;
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            cbxCatégorie.SelectedIndexChanged += CbxCatégorie_SelectedIndexChanged;
            cbxNomIndividu.SelectedIndexChanged += CbxNomIndividu_SelectedIndexChanged;
            txtNomIndividu.TextChanged += TxtNomIndividu_TextChanged;
            btnNouvelleOpération.Click += BtnNouvelleOpération_Click;
        }

        private async void BtnNouvelleOpération_Click(object sender, EventArgs e)
        {
            if(txtmontant.Text != "" && cbxCatégorie.Text != "" && (cbxNomIndividu.Text != "" || txtNomIndividu.Text != "") && cbxPayement.Text != "" && cbxTypeOpération.Text != "" && (radioButton1.Checked || radioButton2.Checked))
            {
                await CallSave();
            }
        }

        private void TxtNomIndividu_TextChanged(object sender, EventArgs e)
        {
            if (!first)
            {
                radioButton2.Text = txtNomIndividu.Text;
            }
        }

        private void CbxNomIndividu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!first)
            {
                radioButton2.Text = cbxNomIndividu.Text;
            }
        }

        private async void CbxCatégorie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!first)
            {
                await CallPartenaire(cbxCatégorie.Text);
            }
        }
        bool first = true;
        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallMode();
        }

        Timer loadTimer = new Timer();

        async Task CallMode()
        {
            var result = await FillModeAsync();
            cbxPayement.DataSource = result;
            cbxPayement.DisplayMember = "Mode";
            cbxPayement.ValueMember = "Id";
            cbxPayement.Text = null;
            first = false;
        }
        public static Task<DataTable> FillModeAsync()
        {
            return Task.Factory.StartNew(() => FillMode());
        }
        private static DataTable FillMode()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Mode");
            using (var donnée = new QuitayeContext())
            {
                var der = from d in donnée.tbl_mode_payement orderby d.Id descending select new { Id = d.Id, Mode = d.Mode };
                foreach (var item in der)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Mode;

                    dt.Rows.Add(dr);
                }
            }
            return dt;

        }
        List<Partenaires> partenaires = new List<Partenaires>();
        async Task CallPartenaire(string type)
        {
            partenaires = await FillPartenaireAsync();
            var des = from d in partenaires where d.Type == type select new { Id = d.Id, Nom = d.Nom };
            cbxNomIndividu.DataSource = des.OrderBy(x => x.Nom ).ToList();
            cbxNomIndividu.DisplayMember = "Nom";
            cbxNomIndividu.ValueMember = "Id";
            cbxNomIndividu.Text = null;
            if(type == "Autres")
            {
                radioButton2.Text = txtNomIndividu.Text;
                cbxNomIndividu.Visible = false;
                txtNomIndividu.Visible = true;
            }
            else
            {
                radioButton2.Text = cbxNomIndividu.Text;
                cbxNomIndividu.Visible = true;
                txtNomIndividu.Visible = false;
            }
        }
        public static Task<List<Partenaires>> FillPartenaireAsync()
        {
            return Task.Factory.StartNew(() => FillPartenaire());
        }
        private static List<Partenaires> FillPartenaire()
        {
            List<Partenaires> list = new List<Partenaires>();
            using (var donnée = new QuitayeContext())
            {
                var client = from d in donnée.tbl_inscription where d.Active == "Oui" orderby d.Id descending select new { Id = d.Id, Prenom = d.Prenom, Nom = d.Nom };
                int id = 1;
                foreach (var item in client)
                {
                    list.Add(new Partenaires()
                    {
                        Id = id,
                        Id_Partenaire = item.Id,
                        Nom = item.Prenom +" "+ item.Nom,
                        Type = "Client",
                    });
                    id++;
                }

                var four = from d in donnée.tbl_fournisseurs select new { Id = d.Id, Nom = d.Nom };
                foreach (var item in four)
                {
                    list.Add(new Partenaires()
                    {
                        Id = id,
                        Id_Partenaire = item.Id,
                        Nom = item.Nom,
                        Type = "Fournisseur",
                    });
                    id++;
                }

                var employé = from d in donnée.tbl_staff select new { Id = d.Id, Nom = d.Nom };
                foreach (var item in employé)
                {
                    list.Add(new Partenaires()
                    {
                        Id = id,
                        Id_Partenaire = item.Id,
                        Nom = item.Nom,
                        Type = "Employé(e)",
                    });
                    id++;
                }
            }
            return list;
        }


        private async void BtnAddPayement_Click(object sender, EventArgs e)
        {
            Ajouter_Element element = new Ajouter_Element("Mode");
            element.ShowDialog();
            if (element.ok == "Oui")
            {
                await CallMode();
            }
        }

#pragma warning disable CS0169 // Le champ 'Nouveau_Pret_Remboursement.type' n'est jamais utilisé
        string type;
#pragma warning restore CS0169 // Le champ 'Nouveau_Pret_Remboursement.type' n'est jamais utilisé
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

        private async Task CallSave()
        {
            string result = Regex.Replace(txtmontant.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
            Pret_Argent pret = new Pret_Argent();
            pret.Catégorie = cbxCatégorie.Text;
            pret.Type = cbxTypeOpération.Text;
            pret.Reference = txtReference.Text;
            if (radioButton1.Checked)
            {
                pret.Preteur = "Vous";
                pret.Pretant = radioButton2.Text;
            }
            else if (radioButton2.Checked)
            {
                pret.Preteur = radioButton2.Text;
                pret.Pretant = "Vous";
            }
                
            pret.Id_Preteur = Convert.ToInt32(cbxNomIndividu.SelectedValue);
            pret.Mode_Payement = cbxPayement.Text;
            pret.Montant = Convert.ToDecimal(result);
            pret.Date_Echeance = DateOpération.Value;
            var insert = await SaveAsync(pret);
            if (insert)
            {
                Alert.SShow("Opération enregistré avec succès.", Alert.AlertType.Sucess);
                ok = "Oui";
                txtmontant.Text = null;
                txtNomIndividu.Text = null;
                txtReference.Text = null;
                cbxCatégorie.Text = null;
                cbxNomIndividu.Text = null;
                cbxPayement.Text = null;
                cbxTypeOpération.Text = null;
                radioButton1.Checked = false;
                radioButton2.Checked = false;
            }
        }

        public string ok;
        
        private async Task<bool> SaveAsync(Pret_Argent pret)
        {
            using(var donnée = new QuitayeContext())
            {
                int id = 1;
                var deser = (from d in donnée.tbl_payement orderby d.Id descending select new { Id = d.Id }).Take(1);
                if(deser.Count() != 0)
                {
                    id = deser.First().Id + 1;
                }

                var pre = new Models.Context.tbl_payement();
                pre.Id = id;
                if(pret.Type == "Prêt")
                {
                    if (pret.Preteur == "Vous")
                    {
                        pre.Client = pret.Pretant;
                        pre.Type = "Décaissement";
                        pre.Commentaire = pret.Type + " accordé à " + pret.Pretant;
                    }
                    else
                    {
                        pre.Client = pret.Preteur;
                        pre.Type = "Encaissement";
                        pre.Commentaire = "Vous avez pris un " + pret.Type + " chez " + pre.Client;
                    }
                }
                else
                {
                    if (pret.Preteur == "Vous")
                    {
                        pre.Client = pret.Pretant;
                        pre.Type = "Encaissement";
                        pre.Commentaire = pret.Type + " effectué par "+pret.Pretant ;
                    }
                    else
                    {
                        pre.Client = pret.Preteur;
                        pre.Type = "Décaissement";
                        pre.Commentaire = "Vous avez "+ pret.Type + " " + pre.Client;
                    }
                }
                
                pre.Nature = pret.Type;
                pre.Auteur = Principales.profile;
                pre.Date_Enregistrement = DateTime.Now;
                pre.Date_Payement = DateTime.Today;
                pre.Montant = pret.Montant;
                pre.Date_Echeance = pret.Date_Echeance;
                pre.Mode_Payement = pret.Mode_Payement;
                pre.Num_Opération = pret.Reference;
                pre.Raison = pret.Catégorie;
                pre.Compte_Tier = pret.Id_Preteur.ToString();
                donnée.tbl_payement.Add(pre);
                await donnée.SaveChangesAsync();
                return true;
            }
        }
    }
}
