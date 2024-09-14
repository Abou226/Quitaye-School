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
    public partial class List_Comptable : Form
    {
        string mycontrng = LogIn.mycontrng;
        public List_Comptable(string name)
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
                CallDG();
                temp = 0;
            }
        }
        Timer timer = new Timer();

        private async void CallDG()
        {
            var result = await FillDGAsync();

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = result;
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
        private Task<DataTable> FillDGAsync()
        {
            return Task.Factory.StartNew(() => FillDG());
        }
        private DataTable FillDG()
        {
            using (var donnée = new QuitayeContext())
            {
                var don = from d in donnée.tbl_Compte_Comptable
                          where d.Type == null
                          orderby d.Compte
                          select new
                          {
                              Id = d.Id,
                              Compte = d.Compte,
                              Nom = d.Catégorie,
                              Nom_Complet = d.Nom_Compte,
                              Description = d.Description,
                              Date_Ajout = d.Date_Ajout,
                          };

                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("Compte");
                dt.Columns.Add("Nom");
                dt.Columns.Add("Nom_Complet");
                dt.Columns.Add("Description");
                dt.Columns.Add("Date_Ajout");


                foreach (var item in don)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Compte;
                    dr[2] = item.Nom;
                    dr[3] = item.Nom_Complet;
                    dr[4] = item.Description;
                    dr[5] = item.Date_Ajout;
                    dt.Rows.Add(dr);
                }

                return dt;
            }
        }
        public static string ok;
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                using (var donnée = new QuitayeContext())
                {
                    if (btnAjouter.Text == "Ajouter")
                    {
                        description = txtDescription.Text;
                        nom = txtNom.Text;
                        numéro = txtNuméro.Text;
                        AjouterCompteComptableAsync(donnée);
                        Alert.SShow("Compte ajouté avec succès.", Alert.AlertType.Sucess);
                        ok = "Oui";
                        CallDG();
                    }
                    else
                    {
                        description = txtDescription.Text;
                        nom = txtNom.Text;
                        numéro = txtNuméro.Text;
                        ModifierCompteComptableAsync(donnée);
                        Alert.SShow("Compte modifier avec succès.", Alert.AlertType.Sucess);
                        btnAjouter.IconChar = FontAwesome.Sharp.IconChar.Plus;
                        btnAjouter.Text = "Ajouter";
                        ok = "Oui";
                        CallDG();
                    }
                }
            }else
            {
                if (LogIn.trial == true)
                {
                    Alert.SShow("Periode d'essay arrivé terme. Veillez-passez à la version payante !", Alert.AlertType.Info);
                }
                else Alert.SShow("Veillez renouveller votre abonnement !", Alert.AlertType.Info);
            }
        }

        private static string nom;
        private static string numéro;
        private static string description;

        private static Task AjouterCompteComptableAsync(QuitayeContext donnée)
        {
            return Task.Factory.StartNew(() => AjouterCompteComptable(donnée));
        }
        private static async void AjouterCompteComptable(QuitayeContext donnée)
        {
            if (nom != "" && numéro != "")
            {
                var ere = from d in donnée.tbl_Compte_Comptable select d;
                int id = 1;
                if (ere.Count() != 0)
                {
                    var res = (from d in donnée.tbl_Compte_Comptable orderby d.Id descending select d).First();
                    id = res.Id + 1;
                }
                var compte = new Models.Context.tbl_Compte_Comptable();
                compte.Id = id;
                compte.Auteur = Principales.profile;
                compte.Catégorie = nom;
                compte.Compte = numéro;
                compte.Nom_Compte = numéro + "-" + nom;
                compte.Date_Ajout = DateTime.Now;
                compte.Description = description;
                donnée.tbl_Compte_Comptable.Add(compte);
                await donnée.SaveChangesAsync();

                
            }
        }


        private static Task ModifierCompteComptableAsync(QuitayeContext donnée)
        {
            return Task.Factory.StartNew(() => ModifierCompteComptable(donnée));
        }
        private static async void ModifierCompteComptable(QuitayeContext donnée)
        {
            if (nom != "" && numéro != "")
            {
                var compte = donnée.tbl_Compte_Comptable.SingleOrDefault(x => x.Id == id);
                
                compte.Auteur = Principales.profile;
                compte.Catégorie = nom;
                compte.Compte = numéro;
                compte.Nom_Compte = numéro + "-" + nom;
                compte.Date_Ajout = DateTime.Now;
                compte.Description = description;
                await donnée.SaveChangesAsync();
            }
        }

        static int id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
            {
                using (var donnée = new QuitayeContext())
                {
                    var s = (from d in donnée.tbl_Compte_Comptable where d.Id == id select d).First();
                    if (e.ColumnIndex == 6)
                    {
                        txtNom.Text = s.Catégorie;
                        txtDescription.Text = s.Description;
                        txtNuméro.Text = s.Compte;
                        btnAjouter.Text = "Modifier";
                        btnAjouter.IconChar = FontAwesome.Sharp.IconChar.Edit;

                    }
                    else if (e.ColumnIndex == 7)
                    {

                    }
                }
            }
        }
    }
}
