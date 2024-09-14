using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Ajout_Enseignant : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Ajout_Enseignant()
        {
            InitializeComponent();
        }

        public string exgenre;
        public int id;
        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                if (txtPrenom.Text != "" && txtNom.Text != "" && txtEmail.Text != "" && txtContact1.Text != "" && txtNationalité.Text != "" && txtAdresse.Text != "")
                {
                    if (btnAjouter.Text == "Ajouter")
                        AddData();
                    else Edit();
                }
            }
            
        }

        private async void Edit()
        {
            using (var donnée = new QuitayeContext())
            {
                //var cycle = (from d in donnée.tbl_classe where d.Nom == cbxClasse.Text select d).First();
                //var eleve = (from d in donnée.tbl_inscription where d.Année_Scolaire == Principales.annéescolaire && d.Classe == classe && d.N_Matricule == matricule select d).First();
                var inscription = (from d in donnée.tbl_enseignant where d.Id == id select d).First();

                inscription.Adresse = txtAdresse.Text;
                inscription.Contact1 = txtContact1.Text;
                inscription.Contact2 = txtContact2.Text;
                inscription.Date_Naissance = NaissanceDate.Value.Date;
                inscription.Email = txtEmail.Text;
                inscription.Genre = genre;
                inscription.Nom = txtNom.Text;
                inscription.Prenom = txtPrenom.Text;
                inscription.Nom_Complet = txtPrenom.Text + " " + txtNom.Text;
                inscription.Nationalité = txtNationalité.Text;
                //inscription.N_Matricule = txtMatriculte.Text;
                inscription.Active = "Oui";
                //inscription.Cycle = cycle.Cycle;
                //if (txtScolarité.Visible == true && txtScolarité.Text != "")
                //    inscription.Scolarité = Convert.ToDecimal(txtScolarité.Text);
                //inscription.Type_Scolarité = cbxTypeScolarité.Text;
                //inscription.Nom_Matricule = inscription.Nom_Complet + "(" + inscription.N_Matricule + ")";
                //if (txtRémise.Visible == true && txtRémise.Text != "")
                //{
                //    inscription.Rémise = Convert.ToDecimal(txtRémise.Text);
                //}
                if (filePath != "")
                {
                    Image temp;
                    temp = new Bitmap(btnImage.Image);
                    MemoryStream strm = new MemoryStream();
                    temp.Save(strm, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ImageByteArray = strm.ToArray();
                    inscription.Image = ImageByteArray;
                }

                await donnée.SaveChangesAsync();

                if (exgenre != genre)
                {
                    var pay = from d in donnée.tbl_payement where d.N_Matricule == id.ToString() && d.Type == "Salaire" && d.Année_Scolaire == Principales.annéescolaire select new { Id = d.Id };
                    dataGridView2.DataSource = pay;
                    if (pay.Count() > 0)
                    {
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            int id = Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value);
                            var payement = donnée.tbl_payement.SingleOrDefault(x => x.Id == id);
                            payement.Genre = genre;
                            await donnée.SaveChangesAsync();
                        }
                    }
                }
            }
        }

        public string filePath;
        Byte[] ImageByteArray;
        public string ok;
        private void btnImage_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (((Control)sender).Name == "btnPicture")
            {
                PictureBox p = sender as PictureBox;

                if (p != null)
                {
                    file.Filter = "(*.jpg; *.jpeg;*.bmp)| *.jpg; *.jpeg; *.bmp";
                    if (file.ShowDialog() == DialogResult.OK)
                    {
                        filePath = file.FileName;
                        p.Image = Image.FromFile(file.FileName);
                    }
                }
            }
            else
            {
                FontAwesome.Sharp.IconPictureBox p = sender as FontAwesome.Sharp.IconPictureBox;
                if (p != null)
                {
                    file.Filter = "(*.jpg; *.jpeg;*bmp)| *.jpg; *.jpeg; *.bmp";
                    if (file.ShowDialog() == DialogResult.OK)
                    {
                        filePath = file.FileName;
                        p.Image = Image.FromFile(file.FileName);
                    }
                }
            }
        }
        private async void AddData()
        {
            using (var donnée = new QuitayeContext())
            {
                var s = new Models.Context.tbl_enseignant();
                s.Adresse = txtAdresse.Text;
                s.Auteur = Principales.profile;
                s.Contact1 = txtContact1.Text;
                s.Contact2 = txtContact2.Text;
                s.Date_Ajout = DateTime.Now;
                s.Date_Naissance = NaissanceDate.Value;
                s.Prenom = txtPrenom.Text;
                s.Nom = txtNom.Text;
                s.Nom_Complet = s.Prenom + " " + s.Nom;
                s.Nationalité = txtNationalité.Text;
                s.Genre = genre;
                s.Email = txtEmail.Text;
                if (filePath != "")
                {
                    Image temp;
                    temp = new Bitmap(btnImage.Image);
                    MemoryStream strm = new MemoryStream();
                    temp.Save(strm, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ImageByteArray = strm.ToArray();
                    s.Image = ImageByteArray;
                }
                else s.Image = null;
                ok = "Oui";
                donnée.tbl_enseignant.Add(s);
                await donnée.SaveChangesAsync();
                ClearData();
                Alert.SShow("Enseignant ajouté avec succès.", Alert.AlertType.Sucess);
            }
        }

        //private void FillData()
        //{
        //    using(var donnée = new QuitayeContext())
        //    {
        //        var s = from d in donnée.tbl_enseigna
        //    }
        //}

        private void ClearData()
        {
            txtAdresse.Clear();
            txtEmail.Clear();
            txtNationalité.Clear();
            txtPrenom.Clear();
            txtNom.Clear();
            txtContact1.Clear();
            genre = null;
            txtContact2.Clear();
        }

        public string genre;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            genre = "Masculin";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            genre = "Feminin";
        }
    }
}
