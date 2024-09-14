using Microsoft.SqlServer.Management.Smo;
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
    public partial class Paramettre_Elève : Form
    {
        public Paramettre_Elève()
        {
            InitializeComponent();
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            SetTarif();
            FillParent();
        }

        Timer timer = new Timer();
        private async void btnAjouterParent_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false && Principales.type_compte == "Administrateur")
            {
                if (txtPrenom.Text != "" && txtNom.Text != "" && txtTelephone.Text != "" && cbxGenre.Text != "")
                {
                    if (btnAjouterParent.IconChar == FontAwesome.Sharp.IconChar.Plus)
                    {
                        using (var donnée = new QuitayeContext())
                        {
                            var er = from d in donnée.tbl_parent_elèves select d;
                            int id = 0;
                            if (er.Count() != 0)
                            {
                                var ers = (from d in donnée.tbl_parent_elèves orderby d.Id descending select d).First();
                                id = ers.Id;
                            }

                            var s = new Models.Context.tbl_parent_elèves();
                            s.Id = id + 1;
                            s.Auteur = Principales.profile;
                            s.Date_Ajout = DateTime.Now;
                            s.Prenom = txtPrenom.Text;
                            s.Nom = txtNom.Text;
                            s.Genre = cbxGenre.Text;
                            s.Contact = txtTelephone.Text;
                            s.Ville = txtVille.Text;
                            s.Pays = txtPays.Text;
                            s.Email = txtEmail.Text;
                            s.Adresse = txtAdresse.Text;
                            s.Nom_Contact = s.Prenom + " " + s.Nom + " " + s.Contact;
                            donnée.tbl_parent_elèves.Add(s);
                            donnée.SaveChangesAsync();
                            ClearData();
                            Alert.SShow("Parent ajouté avec succès.", Alert.AlertType.Sucess);
                            FillParent();
                        }
                    }else if(btnAjouterParent.IconChar == FontAwesome.Sharp.IconChar.Edit)
                    {
                        using(var donnée = new QuitayeContext())
                        {
                            var s = (from d in donnée.tbl_parent_elèves where d.Id == id select d).First();
                            s.Prenom = txtPrenom.Text;
                            s.Nom = txtNom.Text;
                            s.Genre = cbxGenre.Text;
                            s.Contact = txtTelephone.Text;
                            s.Ville = txtVille.Text;
                            s.Pays = txtPays.Text;
                            s.Email = txtEmail.Text;
                            s.Adresse = txtAdresse.Text;
                            s.Nom_Contact = s.Prenom + " " + s.Nom + " " + s.Contact;
                            await donnée.SaveChangesAsync();
                            ClearData();
                            Alert.SShow("Parent modifié avec succès.", Alert.AlertType.Sucess);
                            FillParent();
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

        private void FillParent()
        {
            using(var donnée = new QuitayeContext())
            {
                var s = (from d in donnée.tbl_parent_elèves
                        orderby d.Id descending
                        select new
                        {
                            Id = d.Id,
                            Prenom = d.Prenom,
                            Nom = d.Nom,
                            Genre = d.Genre,
                            Adresse = d.Adresse,
                            Email = d.Email,
                            Téléphone = d.Contact,
                            Pays = d.Pays,
                            Ville = d.Ville,
                            Date_Ajout = d.Date_Ajout,
                            Auteur = d.Auteur,
                        }).ToList();
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = s;

                EditColumn edit = new EditColumn();
                edit.Name = "Edit";
                edit.HeaderText = "Edit";

                DeleteColumn dele = new DeleteColumn();
                dele.Name = "Delete";
                dele.HeaderText = "Sup";

                dataGridView1.Columns.Add(edit);
                dataGridView1.Columns.Add(dele);
                dataGridView1.Columns["Edit"].Width = 35;
                dataGridView1.Columns["Delete"].Width = 25;
            }
        }

        private void ClearData()
        {
            txtAdresse.Clear();
            txtEmail.Clear();
            txtPrenom.Clear();
            txtNom.Clear();
            txtPays.Clear();
            txtVille.Clear();
            txtTelephone.Clear();
            cbxGenre.Text = null;
        }

        private void SetTarif()
        {
            using(var donnée = new QuitayeContext())
            {
                var cantine = (from d in donnée.tbl_tarif_accessoire where d.Id == 1 select d).First();
                txtCantineAnnuel.Text = Convert.ToDecimal(cantine.Tarif_Annuel).ToString("N0");
                txtCantineMensuel.Text = Convert.ToDecimal(cantine.Tarif_Mensuel).ToString("N0");
                txtCantineJournalier.Text = Convert.ToDecimal(cantine.Tarif_Journalier).ToString("N0");

                var transport = (from d in donnée.tbl_tarif_accessoire where d.Id == 2 select d).First();
                txtTransportAnnuel.Text = Convert.ToDecimal(transport.Tarif_Annuel).ToString("N0");
                txtTransportMensuel.Text = Convert.ToDecimal(transport.Tarif_Mensuel).ToString("N0");
                txtTransportJournalier.Text = Convert.ToDecimal(transport.Tarif_Journalier).ToString("N0");

                var assurance = (from d in donnée.tbl_tarif_accessoire where d.Id == 3 select d).First();
                txtAssuranceAnnuel.Text = Convert.ToDecimal(assurance.Tarif_Annuel).ToString("N0");
                txtAssuranceMensuel.Text = Convert.ToDecimal(assurance.Tarif_Mensuel).ToString("N0");
                txtAssuranceJournalier.Text = Convert.ToDecimal(assurance.Tarif_Journalier).ToString("N0");
            }
        }

        public int id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 11)
            {
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                using(var donnée = new QuitayeContext())
                {
                    var s = (from d in donnée.tbl_parent_elèves where d.Id == id select d).First();
                    txtAdresse.Text = s.Adresse;
                    txtEmail.Text = s.Email;
                    txtNom.Text = s.Nom;
                    txtPrenom.Text = s.Prenom;
                    txtPays.Text = s.Pays;
                    cbxGenre.Text = s.Genre;
                    txtTelephone.Text = s.Contact;
                    txtVille.Text = s.Ville;
                    btnAjouterParent.IconChar = FontAwesome.Sharp.IconChar.Edit;
                    btnAjouterParent.Text = "Modifier";
                }   
            }else if(e.ColumnIndex == 12)
            {
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                MsgBox msg = new MsgBox();
                msg.show("Voulez-vous supprimer cette personne ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                msg.ShowDialog();
                if (msg.clicked == "Non")
                    return;
                else if(msg.clicked == "Oui")
                {
                    using(var donnée = new QuitayeContext())
                    {
                        var ers = (from d in donnée.tbl_parent_elèves where d.Id == id select d).First();
                        donnée.tbl_parent_elèves.Remove(ers);
                        donnée.SaveChangesAsync();
                        Alert.SShow("Parent supprimé avec succès.", Alert.AlertType.Sucess);
                        FillParent();
                    }
                }
            }
        }

        private async void btnValidé_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false && Principales.type_compte == "Administrateur")
            {
                using (var donnée = new QuitayeContext())
                {
                    if (txtAssuranceAnnuel.Text != "" || txtAssuranceJournalier.Text != "" || txtAssuranceMensuel.Text != "" || txtCantineAnnuel.Text != "" || txtCantineJournalier.Text != "" || txtCantineMensuel.Text != "" || txtTransportAnnuel.Text != "" || txtTransportJournalier.Text != "" || txtTransportMensuel.Text != "")
                    {
                        string cantineannuel = Regex.Replace(txtCantineAnnuel.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                        string cantinemensuel = Regex.Replace(txtCantineMensuel.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                        string cantinejournalier = Regex.Replace(txtCantineJournalier.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                        string transportannuel = Regex.Replace(txtTransportAnnuel.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                        string transportmensuel = Regex.Replace(txtTransportMensuel.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                        string transportjournalier = Regex.Replace(txtTransportJournalier.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                        string assuranceannuel = Regex.Replace(txtAssuranceAnnuel.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                        string assurancemensuel = Regex.Replace(txtAssuranceMensuel.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                        string assurancejournalier = Regex.Replace(txtAssuranceJournalier.Text, @"(?<=\d)\p{Zs}(?=\d)", "");
                        var cantine = (from d in donnée.tbl_tarif_accessoire where d.Id == 1 select d).First();
                        if (txtCantineAnnuel.Text != "")
                            cantine.Tarif_Annuel = Convert.ToDecimal(cantineannuel);
                        if (txtCantineMensuel.Text != "")
                            cantine.Tarif_Mensuel = Convert.ToDecimal(cantinemensuel);
                        if (txtCantineJournalier.Text != "")
                            cantine.Tarif_Journalier = Convert.ToDecimal(cantinejournalier);
                        await donnée.SaveChangesAsync();

                        var transport = (from d in donnée.tbl_tarif_accessoire where d.Id == 2 select d).First();
                        if (txtTransportAnnuel.Text != "")
                            transport.Tarif_Annuel = Convert.ToDecimal(transportannuel);
                        if (txtTransportMensuel.Text != "")
                            transport.Tarif_Mensuel = Convert.ToDecimal(transportmensuel);
                        if (txtTransportJournalier.Text != "")
                            transport.Tarif_Journalier = Convert.ToDecimal(transportjournalier);
                        await donnée.SaveChangesAsync();

                        var assurance = (from d in donnée.tbl_tarif_accessoire where d.Id == 3 select d).First();
                        if (txtAssuranceAnnuel.Text != "")
                            assurance.Tarif_Annuel = Convert.ToDecimal(assuranceannuel);
                        if (txtAssuranceMensuel.Text != "")
                            assurance.Tarif_Mensuel = Convert.ToDecimal(assurancemensuel);
                        if (txtAssuranceJournalier.Text != "")
                            assurance.Tarif_Journalier = Convert.ToDecimal(assurancejournalier);
                        await donnée.SaveChangesAsync();

                        Alert.SShow("Tarif(s) enregistré(s) avec succès !", Alert.AlertType.Sucess);
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

        private void txtCantineMensuel_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != "")
            {
                ((TextBox)sender).Text = Convert.ToDecimal(((TextBox)sender).Text).ToString("N0");
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }

        }
    }
}
