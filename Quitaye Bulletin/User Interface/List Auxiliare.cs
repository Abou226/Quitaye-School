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
    public partial class List_Auxiliare : Form
    {
        string mycontrng = LogIn.mycontrng;
        public List_Auxiliare(string name)
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
                CallCbx();
                temp = 0;
            }
        }

        async void CallCbx()
        {
            var result = await FillCbxAsync();
            cbxCompteGénéral.DataSource = result;
            cbxCompteGénéral.DisplayMember = "Nom_Compte";
            cbxCompteGénéral.ValueMember = "Id";
            cbxCompteGénéral.Text = null;
        }
        private Task<DataTable> FillCbxAsync()
        {
            return Task.Factory.StartNew(() => FillCbx());
        }
        private DataTable FillCbx()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Nom_Compte");
            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_Compte_Comptable where d.Type == null orderby d.Compte ascending select d;

                foreach (var item in s)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Nom_Compte;

                    dt.Rows.Add(dr);
                }

                return dt;
            }
        }

        public static string ok;
        int id;

        Timer timer = new Timer();

        private async void btnAjouter_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                using (var donnée = new QuitayeContext())
                {
                    string[] arr = cbxCompteGénéral.Text.Split('-');
                    if (txtNom.Text != "" && cbxCompteGénéral.Text != "" && cbxType.Text != "" && txtDescription.Text != "")
                    {
                        if (btnAjouter.Text == "Ajouter")
                        {
                            var compe = from d in donnée.tbl_Compte_Comptable where d.Compte_Aux == txtNom.Text select d;
                            if (compe.Count() == 0)
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
                                compte.Catégorie = arr[1];
                                compte.Compte = arr[0];
                                compte.Compte_Aux = txtDescription.Text;
                                compte.Date_Ajout = DateTime.Now;
                                compte.Préfix = txtNom.Text;
                                compte.Nom_Compte = arr[0] + "-" + txtNom.Text;
                                compte.Type = cbxType.Text;
                                donnée.tbl_Compte_Comptable.Add(compte);
                                await donnée.SaveChangesAsync();

                                Alert.SShow("Compte ajouté avec succès.", Alert.AlertType.Sucess);
                                ok = "Oui";
                                CallDG();
                            }
                        }
                        else
                        {
                            var compte = donnée.tbl_Compte_Comptable.SingleOrDefault(x => x.Id == id);
                            compte.Auteur = Principales.profile;
                            compte.Compte_Aux = txtDescription.Text;
                            compte.Préfix = txtNom.Text;
                            compte.Nom_Compte = arr[0] + "-" + txtNom.Text;
                            compte.Type = cbxType.Text;
                            await donnée.SaveChangesAsync();

                            //var der = from d in donnée.tbl_journal_comptable where d.Compte == 

                            Alert.SShow("Compte modifié avec succès.", Alert.AlertType.Sucess);
                            ok = "Oui";
                            CallDG();
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
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Compte");
            dt.Columns.Add("C_Général");
            dt.Columns.Add("Nom_Complet");
            dt.Columns.Add("Compte_Aux");
            dt.Columns.Add("Prefix");
            dt.Columns.Add("Description");
            dt.Columns.Add("Date_Ajout");
            using (var donnée = new QuitayeContext())
            {
                var don = from d in donnée.tbl_Compte_Comptable
                          where d.Type != null
                          orderby d.Compte
                          select new
                          {
                              Id = d.Id,
                              Compte = d.Compte,
                              C_Général = d.Catégorie,
                              Nom_Complet = d.Nom_Compte,
                              Compte_Aux = d.Compte_Aux,
                              Préfix = d.Préfix,
                              Description = d.Description,
                              Date_Ajout = d.Date_Ajout,
                          };

                foreach (var item in don)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Compte;
                    dr[2] = item.C_Général;
                    dr[3] = item.Nom_Complet;
                    dr[4] = item.Compte_Aux;
                    dr[5] = item.Préfix;
                    dr[6] = item.Description;
                    dr[7] = item.Date_Ajout;

                    dt.Rows.Add(dr);
                }

                return dt;

                
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            if (e.ColumnIndex == 8 || e.ColumnIndex == 9)
            {
                using (var donnée = new QuitayeContext())
                {
                    var s = (from d in donnée.tbl_Compte_Comptable where d.Id == id select d).First();
                    if (e.ColumnIndex == 8)
                    {
                        txtNom.Text = s.Préfix;
                        txtDescription.Text = s.Compte_Aux;
                        cbxType.Text = s.Type;
                        cbxCompteGénéral.Text = s.Nom_Compte;
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
