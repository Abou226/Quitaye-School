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
    public partial class List_Employée : Form
    {
        public List_Employée()
        {
            InitializeComponent();
            FillCbx();
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
            tim.Enabled = false;
            tim.Interval = 10;
            tim.Start();
            tim.Tick += Tim_Tick;
        }

        private void Tim_Tick(object sender, EventArgs e)
        {
            //if (cbxCycle.Text != "" && cbxCycle.Text != "Tout")
            //{
            //    name = "List Employées " + cbxCycle.Text + " " + Principales.annéescolaire + " " + startDate.Value.Date.ToString("dd-MM-yyyy") + EndDate.Value.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            //}
            //else
            {
                name = "List Employées  " + Principales.annéescolaire + " " + startDate.Value.Date.ToString("dd-MM-yyyy") + EndDate.Value.Date.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            }
        }

        Timer tim = new Timer();
        

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            FillData();
        }

        Timer timer = new Timer();
        private int id;
        private string name;

        private void FillCbx()
        {
            using(var donnée = new QuitayeContext())
            {
                var s = (from d in donnée.tbl_responsabilité orderby d.Responsabilité ascending select d).ToList();
                cbxRole.DataSource = s;
                cbxRole.DisplayMember = "Responsabilité";
                cbxRole.ValueMember = "Id";
                cbxRole.Text = null;
            }
        }

        private async void btnAjouter_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                if (txtAdresse.Text != "" && txtContact.Text != "" && txtNom.Text != "" && cbxRole.Text != "" && cbxGenre.Text != "")
                {
                    if (btnAjouter.IconChar == FontAwesome.Sharp.IconChar.Plus)
                    {
                        using (var donnée = new QuitayeContext())
                        {
                            var et = (from d in donnée.tbl_staff select d).Take(1);
                            int id = 0;
                            if (et.Count() != 0)
                            {
                                var re = (from d in donnée.tbl_staff orderby d.Id descending select d).First();
                                id = re.Id;
                            }

                            var s = new Models.Context.tbl_staff();
                            s.Id = id + 1;
                            s.Auteur = Principales.profile;
                            s.Date_Ajout = DateTime.Now;
                            s.Role = cbxRole.Text;
                            s.Nom = txtNom.Text;
                            s.Genre = cbxGenre.Text;
                            //s.Salaire = Convert.ToDecimal(txtSalaire.Text);
                            s.Contact = txtContact.Text;
                            s.Adresse = txtAdresse.Text;
                            donnée.tbl_staff.Add(s);
                            await donnée.SaveChangesAsync();
                            ClearData();
                            Alert.SShow("Employée ajouté avec succès !", Alert.AlertType.Sucess);
                            FillData();
                        }
                    }
                    else
                    {
                        using (var donnée = new QuitayeContext())
                        {

                            var s = (from d in donnée.tbl_staff where d.Id == id select d).First();

                            s.Role = cbxRole.Text;
                            s.Nom = txtNom.Text;
                            s.Genre = cbxGenre.Text;
                            s.Contact = txtContact.Text;
                            s.Adresse = txtAdresse.Text;
                            await donnée.SaveChangesAsync();
                            ClearData();
                            Alert.SShow("Employée modifié avec succès !", Alert.AlertType.Sucess);
                            btnAjouter.IconChar = FontAwesome.Sharp.IconChar.Plus;
                            btnAjouter.Text = "Ajouter";
                            FillData();
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

        private void FillData()
        {
            using(var donnée = new QuitayeContext())
            {
                var s = (from d in donnée.tbl_staff
                        orderby d.Id descending
                        select new
                        {
                            Id = d.Id,
                            Nom = d.Nom,
                            Genre = d.Genre,
                            Adresse = d.Adresse,
                            Contact = d.Contact,
                            Role = d.Role,
                            Date_Ajout = d.Date_Ajout,
                            Auteur = d.Auteur,
                        }).ToList();
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = s;
                dataGridView2.DataSource = s;

                DossierColumn edit = new DossierColumn();
                edit.Name = "Dossier";
                edit.HeaderText = "Details";

                DeleteColumn dele = new DeleteColumn();
                dele.Name = "Delete";
                dele.HeaderText = "Sup";

                dataGridView1.Columns.Add(edit);
                //dataGridView1.Columns.Add(dele);
                dataGridView1.Columns["Dossier"].Width = 35;
                //dataGridView1.Columns["Delete"].Width = 25;
            }
        }

        private void ClearData()
        {
            txtNom.Clear();
            txtAdresse.Clear();
            txtContact.Clear();
            cbxGenre.Text = null;
            cbxRole.Text = null;
            txtSalaire.Clear();
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(LogIn.expiré == false && Principales.type_compte == "Administrateur")
            {
                if (e.ColumnIndex == 8)
                {
                    id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    using (var donnée = new QuitayeContext())
                    {
                        var s = (from d in donnée.tbl_staff where d.Id == id select d).First();
                        Details_Staff staff = new Details_Staff(id);
                        staff.matricule = id.ToString();
                        staff.prenom = s.Nom;
                        staff.genre = s.Genre;
                        //staff.salaire = s.Salaire.ToString();
                        staff.cycle = "Staff";
                        staff.lblTitre.Text = "Détails " + s.Nom;
                        staff.ShowDialog();
                    }
                }
                else if (e.ColumnIndex == 9)
                {
                    MsgBox msg = new MsgBox();
                    msg.show("Voulez-vous supprimer cet employée", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Succes);
                    msg.ShowDialog();
                    if (msg.clicked == "Non")
                        return;
                    else if (msg.clicked == "Oui")
                    {
                        id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                        using (var donnée = new QuitayeContext())
                        {
                            var s = (from d in donnée.tbl_staff where d.Id == id select d).First();
                            donnée.tbl_staff.Remove(s);
                            await donnée.SaveChangesAsync();
                            Alert.SShow("Employée supprimé avec succès !", Alert.AlertType.Sucess);
                            FillData();
                        }
                    }
                }
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            PrintAction.Print.PrintPdfFile(dataGridView2, name, "Registre Staff", "List Global", Principales.annéescolaire, LogIn.mycontrng, "Quitaye School", true);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            PrintAction.Print.PrintExcelFile(dataGridView2, "Registre Staff", name, "Quitaye School");
        }

        private void txtSalaire_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
