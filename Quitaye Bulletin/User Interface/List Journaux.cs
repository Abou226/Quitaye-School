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
    public partial class List_Journaux : Form
    {
        string mycontrng = LogIn.mycontrng;
        public List_Journaux(string name)
        {
            InitializeComponent();
            lblFormName.Text = name;
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
            temp = 1;
        }

        int temp = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (temp == 1)
            {
                FillDG();
                temp = 0;
            }
        }

        Timer timer = new Timer();

        private void FillDG()
        {
            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_list_journaux
                        select new
                        {

                            Id = d.Id,
                            Journal = d.Nom,
                            Code_Journal = d.Prefix,
                            Type = d.Type,
                            Description = d.Description,
                            Date_Ajout = d.Date_Ajout,
                            Auteur = d.Auteur,
                        };

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = s;
                DeleteColumn delete = new DeleteColumn();
                delete.HeaderText = "Sup";
                delete.Name = "Delete";

                EditColumn edit = new EditColumn();
                edit.HeaderText = "Edit";
                edit.Name = "Edit";
                edit.Width = 20;

                dataGridView1.Columns.Add(edit);
                dataGridView1.Columns.Add(delete);
                dataGridView1.Columns["Edit"].Width = 20;
                dataGridView1.Columns["Delete"].Width = 30;
            }
        }

        public static string ok;
        int id;
        private async void btnAjouter_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                using (var donnée = new QuitayeContext())
                {

                    if (txtNom.Text != "" && txtPréfix.Text != "" && cbxType.Text != "")
                    {
                        if (btnAjouter.Text == "Ajouter")
                        {
                            var compe = from d in donnée.tbl_list_journaux select d;
                            int i = 1;
                            if (compe.Count() != 0)
                            {
                                var c = (from d in donnée.tbl_list_journaux orderby d.Id descending select d).First();
                                i += c.Id;
                            }
                            var compte = new Models.Context.tbl_list_journaux();
                            compte.Id = i;
                            compte.Auteur = Principales.profile;
                            compte.Nom = txtNom.Text;
                            if (checkLier.Checked == true)
                            {
                                string[] comp = cbxCompte.Text.Split('-');
                                compte.Compte = comp[0];
                            }
                            compte.Prefix = txtPréfix.Text;
                            compte.Date_Ajout = DateTime.Now;
                            compte.Description = txtDescription.Text;
                            compte.Type = cbxType.Text;
                            donnée.tbl_list_journaux.Add(compte);
                            await donnée.SaveChangesAsync();
                            ClearData();
                            Alert.SShow("Compte ajouté avec succès.", Alert.AlertType.Sucess);
                            ok = "Oui";
                            FillDG();
                        }
                        else
                        {
                            string excode = null;
                            var compte = donnée.tbl_list_journaux.SingleOrDefault(x => x.Id == id);
                            compte.Nom = txtNom.Text;
                            excode = compte.Prefix;
                            compte.Prefix = txtPréfix.Text;
                            if (checkLier.Checked == true)
                            {
                                string[] comp = cbxCompte.Text.Split('-');
                                compte.Compte = comp[0];
                            }
                            else compte.Compte = null;
                            compte.Description = txtDescription.Text;
                            compte.Type = cbxType.Text;
                            await donnée.SaveChangesAsync();

                            if (excode != compte.Prefix)
                            {
                                List<int> k = donnée.tbl_journal_comptable.Where(x => x.Ref_Pièces.StartsWith(excode)).Select(x => x.Id).Distinct().ToList();
                                for (int i = 0; i < k.Count; i++)
                                {
                                    var s = (from d in donnée.tbl_journal_comptable where d.Id == k[i] select d).First();
                                    string[] ar = s.Ref_Pièces.Split('-');
                                    s.Ref_Pièces = txtPréfix.Text + '-' + ar[1];
                                    await donnée.SaveChangesAsync();
                                }
                            }

                            Alert.SShow("Compte modifié avec succès.", Alert.AlertType.Sucess);
                            ok = "Oui";
                            ClearData();
                            btnAjouter.IconChar = FontAwesome.Sharp.IconChar.Plus;
                            btnAjouter.Text = "Ajouter";
                            FillDG();
                        }
                    }
                }
            }
            else
            {
                if (LogIn.trial == true)
                {
                    Alert.SShow("Periode d'essay arrivé terme. Veillez-passez à la version payante !", Alert.AlertType.Info);
                }
                else Alert.SShow("Veillez renouveller votre abonnement !", Alert.AlertType.Info);
            }

        }

        private void ClearData()
        {
            txtPréfix.Clear();
            txtNom.Clear();
            txtDescription.Clear();
            cbxType.Text = null;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            if (e.ColumnIndex == 7 || e.ColumnIndex == 8)
            {
                using (var donnée = new QuitayeContext())
                {
                    var s = (from d in donnée.tbl_list_journaux where d.Id == id select d).First();
                    if (e.ColumnIndex == 7)
                    {
                        txtDescription.Text = s.Description;
                        txtPréfix.Text = s.Prefix;
                        txtNom.Text = s.Nom;
                        cbxType.Text = s.Type;
                        btnAjouter.IconChar = FontAwesome.Sharp.IconChar.Edit;
                        btnAjouter.Text = "Modifier";
                    }
                    else if (e.ColumnIndex == 8)
                    {

                    }
                }
            }
        }

        private void FillCbxCompte()
        {
            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_Compte_Comptable where d.Type == null orderby d.Compte select d;
                cbxCompte.DataSource = s;
                cbxCompte.DisplayMember = "Nom_Compte";
                cbxCompte.ValueMember = "Id";
            }
        }

        private void checkLier_CheckedChanged(object sender, EventArgs e)
        {
            if (checkLier.Checked == true)
            {
                cbxCompte.Visible = true;
                FillCbxCompte();
            }
            else cbxCompte.Visible = false;
        }
    }
}
