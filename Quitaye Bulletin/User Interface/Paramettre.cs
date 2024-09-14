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
    public partial class Paramettre : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Paramettre()
        {
            InitializeComponent();
            EnterData();
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            CallAnnéeScolaire();
            CallClasse();
        }

        Timer timer = new Timer();
        public void EnterData()
        {
            using (var donnée = new QuitayeContext())
            {
                var s = (from d in donnée.tbl_entreprise where d.Id == 1 select d).First();
                txtAdresse.Text = s.Adresse;
                txtEmailEntre.Text = s.Email;
                txtNomEntreprise.Text = s.Nom;
                txtTelephone.Text = s.Téléphone;
                txtPays.Text = s.Pays;
                txtVille.Text = s.Ville;
                cbxSecteur.Text = s.Secteur;
                cbxTypedeProd.Text = s.Type_Produit;
                txtSlogan.Text = s.Slogan;
                cbxColeurs.Text = s.Couleur;
                pictureBox1.BackColor = SetCouleur();
            }
        }
        public Color SetCouleur()
        {
            Color col = new Color();
            if (cbxColeurs.Text == "Noire")
                col = Color.Black;
            else if (cbxColeurs.Text == "Blanc")
                col = Color.White;
            else if (cbxColeurs.Text == "Jaune")
                col = Color.Yellow;
            else if (cbxColeurs.Text == "Rouge")
                col = Color.Red;
            else if (cbxColeurs.Text == "Rose")
                col = Color.Pink;
            else if (cbxColeurs.Text == "Cyan")
                col = Color.Cyan;
            else if (cbxColeurs.Text == "Vert")
                col = Color.Green;
            else if (cbxColeurs.Text == "Gris")
                col = Color.Gray;
            else if (cbxColeurs.Text == "Gris Foncé")
                col = Color.DarkGray;
            else if (cbxColeurs.Text == "Bleu")
                col = Color.Blue;
            else col = Color.Black;
            return col;
        }

        private void txtNomEntreprise_TextChanged_1(object sender, EventArgs e)
        {
            using (var donnée = new QuitayeContext())
            {
                
            }
        }

        private async void cbxSecteur_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var donnée = new QuitayeContext())
            {
                if (((ComboBox)sender).Name == "cbxSecteur")
                {
                    var entreprise = donnée.tbl_entreprise.SingleOrDefault(x => x.Id == 1);
                    entreprise.Secteur = cbxSecteur.Text;
                    await donnée.SaveChangesAsync();
                }
                else if (((ComboBox)sender).Name == "cbxTypedeProd")
                {
                    var entreprise = donnée.tbl_entreprise.SingleOrDefault(x => x.Id == 1);
                    entreprise.Type_Produit = cbxTypedeProd.Text;
                    await donnée.SaveChangesAsync();
                }
                else if (((ComboBox)sender).Name == "cbxColeurs")
                {
                    var entreprise = donnée.tbl_entreprise.SingleOrDefault(x => x.Id == 1);
                    entreprise.Couleur = cbxColeurs.Text;
                    await donnée.SaveChangesAsync();
                    pictureBox1.BackColor = SetCouleur();
                }
            }
        }

        private async void btnValidé_Click(object sender, EventArgs e)
        {
            var donnée = new DonnéeEcoleDataContext();
            var s = (from d in donnée.tbl_entreprise where d.Id == 1 select d).First();
            s.Nom = txtNomEntreprise.Text;
            s.Pays = txtPays.Text;
            s.Ville = txtVille.Text;
            s.Secteur = cbxSecteur.Text;
            s.Slogan = txtSlogan.Text;
            s.Téléphone = txtTelephone.Text;
            s.Email = txtEmailEntre.Text;
            s.Couleur = cbxColeurs.Text;
            s.Adresse = txtAdresse.Text;
            donnée.SubmitChanges();

            if(Program.EntrepriseType() != null)
            {
                donnée = new DonnéeEcoleDataContext();
                var client = (from d in donnée.tbl_client_quitaye where d.Email == Program.EntrepriseEmail() select d).First();
                client.Entreprise = txtNomEntreprise.Text;
                client.Ville = txtVille.Text;
                client.Pays = txtPays.Text;
                client.Slogan = txtSlogan.Text;
                client.Adresse = txtAdresse.Text;
                client.Contact = txtTelephone.Text;
                donnée.SubmitChanges();
            }
            Principales.entreprise = s.Nom;
            Alert.SShow("Informations enregistrée(s) avec succès.", Alert.AlertType.Sucess);
        }

        private void btnClasse_Click(object sender, EventArgs e)
        {
            Classe cl = new Classe();
            cl.ShowDialog();
            if(cl.ok == "Oui")
            {
                CallClasse();
            }
        }

        private void btnNouvelleAnnée_Click(object sender, EventArgs e)
        {
            Ajouter_Année année = new Ajouter_Année();
            année.ShowDialog();

            if(année.ok == "Oui")
            {
                CallClasse();
            }
        }


        private async void CallClasse()
        {
            var result = await FillClasseAsync();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = result;
            DeleteColumn deleteColumn = new DeleteColumn();
            deleteColumn.HeaderText = "Sup";
            deleteColumn.Name = "Sup";

            EditColumn edit = new EditColumn();
            edit.Name = "Edit";
            edit.HeaderText = "Edit";

            dataGridView1.Columns.Add(edit);

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns.Add(deleteColumn);
            dataGridView1.Columns["Edit"].Width = 20;
            dataGridView1.Columns["Sup"].Width = 25;
        }
        private Task<DataTable> FillClasseAsync()
        {
            return Task.Factory.StartNew(() => FillClasse());
        }

        private DataTable FillClasse()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Classe");
            dt.Columns.Add("Cycle");
            dt.Columns.Add("Scolarité");
            dt.Columns.Add("Date_Ajout", typeof(DateTime));
            dt.Columns.Add("Auteur");
            using(var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_classe orderby d.Id descending select new { Id = d.Id, Classe = d.Nom, Cycle = d.Cycle, Scolarité = d.Scolarité, Date_Ajout = d.Date, Auteur = d.Auteur };
                foreach (var item in s)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Classe;
                    dr[2] = item.Cycle;
                    dr[3] = item.Scolarité;
                    dr[4] = item.Date_Ajout;
                    dr[5] = item.Auteur;
                    dt.Rows.Add(dr);
                }

                return dt;
            }
        }

        private async void CallAnnéeScolaire()
        {
            var result = await FillAnnéeScolaireAsync();
            dataGridView2.Columns.Clear();
            dataGridView2.DataSource = result;
            DeleteColumn deleteColumn = new DeleteColumn();
            deleteColumn.HeaderText = "Sup";
            deleteColumn.Name = "Sup";

            EditColumn edit = new EditColumn();
            edit.Name = "Edit";
            edit.HeaderText = "Edit";

            dataGridView2.Columns.Add(edit);

            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns.Add(deleteColumn);
            dataGridView2.Columns["Edit"].Width = 20;
            dataGridView2.Columns["Sup"].Width = 25;
        }
        private Task<DataTable> FillAnnéeScolaireAsync()
        {
            return Task.Factory.StartNew(() => FillAnnéeScolaire());
        }
        private DataTable FillAnnéeScolaire()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Année_Scolaire");
            dt.Columns.Add("Date_Ajout", typeof(DateTime));
            dt.Columns.Add("Auteur");
            using(var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_année_scolaire orderby d.Id descending select new { Id = d.Id, Année = d.Nom, Date_Ajout = d.Date, Auteur = d.Auteur };
                foreach (var item in s)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Année;
                    dr[2] = item.Date_Ajout;
                    dr[3] = item.Auteur;

                    dt.Rows.Add(dr);
                }

                return dt;
            }
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 6)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                using(var donnée = new QuitayeContext())
                {
                    var s = (from d in donnée.tbl_classe where d.Id == id select d).First();
                    Classe cl = new Classe();
                    cl.id = s.Id;
                    cl.excycle = s.Cycle;
                    cl.exnom = s.Nom;
                    cl.txtNom.Text = s.Nom;
                    cl.txtScolarité.Text = Convert.ToDecimal(s.Scolarité).ToString("N0");
                    cl.txtTranche1.Text = s.Tranche_1.ToString();
                    cl.txtTranche2.Text = s.Tranche_2.ToString();
                    cl.txtTranche3.Text = s.Tranche_3.ToString();
                    Classe.cycle = s.Cycle;
                    if (s.Cycle == "Crèche")
                        cl.rCrèche.Checked = true;
                    else if (s.Cycle == "Maternelle")
                        cl.rMaternelle.Checked = true;
                    else if (s.Cycle == "Premier Cycle")
                        cl.rPremierCycle.Checked = true;
                    else if (s.Cycle == "Second Cycle")
                        cl.rSecondCycle.Checked = true;
                    else if (s.Cycle == "Lycée")
                        cl.rLycée.Checked = true;
                    else if (s.Cycle == "Université")
                        cl.rUniversité.Checked = true;
                    cl.btnAjouterClasse.IconChar = FontAwesome.Sharp.IconChar.Edit;
                    cl.btnAjouterClasse.Text = "Modifier Classe";
                    cl.ShowDialog();
                }
            }else if(e.ColumnIndex == 7)
            {
                MsgBox msg = new MsgBox();
                msg.show("Voulez-vous supprimer cet élément ?", "Supprimer", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                msg.ShowDialog();
                if (msg.clicked == "Non")
                    return;
                else if (msg.clicked == "Oui")
                {
                    using (var donné = new QuitayeContext())
                    {
                        int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                        var s = (from d in donné.tbl_classe where d.Id == id select d).First();
                        donné.tbl_classe.Remove(s);
                        await donné.SaveChangesAsync();
                        Alert.SShow("Element supprimer avec succès.", Alert.AlertType.Sucess);
                        CallClasse();
                    }
                }
            }
        }

        private async void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 4)
            {
                int id = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
                using(var donnée = new QuitayeContext())
                {
                    var s = (from d in donnée.tbl_année_scolaire where d.Id == id select d).First();
                    Ajouter_Année ajouter_Année = new Ajouter_Année();
                    ajouter_Année.txtNom.Text = s.Nom;
                    ajouter_Année.id = s.Id;
                    ajouter_Année.btnAjouterClasse.Text = "Modifier";
                    ajouter_Année.btnAjouterClasse.IconChar = FontAwesome.Sharp.IconChar.Edit;
                    ajouter_Année.ShowDialog();
                }
            }else if(e.ColumnIndex == 5)
            {
                MsgBox msg = new MsgBox();
                msg.show("Voulez-vous supprimer cet élément ?", "Supprimer", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                msg.ShowDialog();
                if (msg.clicked == "Non")
                    return;
                else if(msg.clicked == "Oui")
                {
                    using(var donné = new QuitayeContext())
                    {
                        int id = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
                        var s = (from d in donné.tbl_année_scolaire where d.Id == id select d).First();
                        donné.tbl_année_scolaire.Remove(s);
                        await donné.SaveChangesAsync();
                        Alert.SShow("Element supprimer avec succès.", Alert.AlertType.Sucess);
                        CallAnnéeScolaire();
                    }
                }
            }
        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
