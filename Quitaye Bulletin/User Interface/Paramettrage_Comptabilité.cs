using FontAwesome.Sharp;
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
    public partial class Paramettrage_Comptabilité : Form
    {
        Form currentChildForm;
        public Paramettrage_Comptabilité()
        {
            InitializeComponent();
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
            temp = 1;
            timer1.Enabled = false;
            timer1.Interval = 10;
            timer1.Start();
            timer1.Tick += Timer1_Tick;
            type = "Compte Comptable";
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (temp == 1)
            {
                timer1.Stop();
                FormDonnée();
                temp = 0;
            }
        }

        Timer timer1 = new Timer();


        bool firstime = true;

        int temp = 0;

        static string type;

        private void Timer_Tick(object sender, EventArgs e)
        {

        }

        private void ChildForm(Form form)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            currentChildForm = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(form);
            panelContenedor.Tag = form;
            form.BringToFront();
            form.Show();
        }

        Timer timer = new Timer();
        private void btnAjouterCompte_Click(object sender, EventArgs e)
        {
            Compte_Comptable compte = new Compte_Comptable();
            compte.ShowDialog();
        }

        //private void FillData()
        //{
        //    using(var donnée = new DonnéePatissDataContext(Compte_Comptable.mycontrng))
        //    {
        //        var don = from d in donnée.tbl_compte_comptable
        //                  orderby d.Compte 
        //                  select new
        //                  {
        //                      Id = d.Id,
        //                      Numéro_Compte = d.Compte,
        //                      Nom_Compte = d.Catégorie,
        //                      Nom_Complet = d.Nom_Compte,
        //                      Compte_Aux = d.Compte_Aux,
        //                      Type = d.Type,
        //                      Description = d.Description,
        //                      Date_Ajout = d.Date_Ajout,
        //                  };
        //        dataGridView1.Columns.Clear();
        //        dataGridView1.DataSource = don;
        //        dataGridView1.Columns[0].Visible = false;

        //        DeleteColumn delete = new DeleteColumn();
        //        delete.HeaderText = "Sup";
        //        delete.Name = "Delete";

        //        EditColumn edit = new EditColumn();
        //        edit.HeaderText = "Edit";
        //        edit.Name = "Edit";
        //        edit.Width = 20;

        //        dataGridView1.Columns.Add(edit);
        //        dataGridView1.Columns.Add(delete);
        //        dataGridView1.Columns["Edit"].Width = 20;
        //        dataGridView1.Columns["Delete"].Width = 30;
        //    }
        //}

        //private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

        //    if(e.ColumnIndex == 8)
        //    {
        //        using(var donnée = new DonnéePatissDataContext(Compte_Comptable.mycontrng))
        //        {
        //            var s = (from d in donnée.tbl_compte_comptable where d.Id == id select d).First();
        //            Compte_Comptable compte = new Compte_Comptable();
        //            compte.id = id;
        //            compte.txtDescription.Text = s.Description;
        //            compte.txtNom.Text = s.Catégorie;
        //            compte.txtNuméro.Text = s.Compte;
        //            compte.btnAjouter.Text = "Modifier";
        //            compte.btnAjouter.IconChar = FontAwesome.Sharp.IconChar.Edit;
        //            compte.ShowDialog();
        //            if (Compte_Comptable.ok == "Oui")
        //                FillData();
        //        }
        //    }else if(e.ColumnIndex == 9)
        //    {
        //        MsgBox msg = new MsgBox();
        //        msg.show("Voulez-vous supprimé cet élément ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
        //        msg.ShowDialog();
        //        if (msg.clicked == "Non")
        //            return;
        //        else if(msg.clicked == "Oui")
        //        {
        //            using (var donnée = new DonnéePatissDataContext(Compte_Comptable.mycontrng))
        //            {
        //                var s = (from d in donnée.tbl_compte_comptable where d.Id == id select d).First();
        //                donnée.tbl_compte_comptable.Remove(s);
        //                donnée.SaveChangesAsync();
        //                Alert.SShow("Element supprimé avec succès", Alert.AlertType.Sucess);
        //                FillData();
        //            }
        //        }
        //    }
        //}

        private void btnAjouterCompte_Click_1(object sender, EventArgs e)
        {
            type = ((IconButton)sender).Text;
            FormDonnée();
        }

        private void FormDonnée()
        {
            if (type == "Compte Comptable")
            {
                ChildForm(new List_Comptable(type));
            }
            else if (type == "Compte Auxiliaire")
            {
                ChildForm(new List_Auxiliare(type));
            }
            else if (type == "Journaux Comptable")
            {
                ChildForm(new List_Journaux(type));
            }
            else if (type == "Balance Initiale")
            {
                ChildForm(new Balance_Initiale(type));
            }
        }
    }
}
