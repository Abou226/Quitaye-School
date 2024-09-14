using Quitaye_School.Models;
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
using DataGrid = Quitaye_School.Models.DataGrid;


namespace Quitaye_School.User_Interface
{
    public partial class Re_Passage_Elève : Form
    {
        private Timer timer = new Timer();
        public string cycle;
        public Re_Passage_Elève()
        {
            InitializeComponent();
            lblEffectif.Visible = false;
            lblFille.Visible = false;
            lblGarçon.Visible = false;
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
            btnSelectTout.Click += BtnSelectTout_Click;
            cbxClasse.SelectedIndexChanged += CbxClasse_SelectedIndexChanged;
            //btnRedoublant.Click += BtnRedoublant_Click;
            btnAdmission.Click += btnAdmission_Click;
            txtsearch.TextChanged += Txtsearch_TextChanged;
        }

        private async void Txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtsearch.Text))
                await ShowSearchStudentList();
            else await ShowStudentList();
        }

        private async Task CallSearch()
        {
            var result = await SearchStudentListAsync(txtsearch.Text);
        }

        private async void BtnRedoublant_Click(object sender, EventArgs e)
        {
            bool ok = false;
            btnAdmission.Enabled = false;
            //btnRedoublant.Enabled = false;

            List<string> list = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Selection"].Value))
                    list.Add(row.Cells["Matricule"].Value.ToString());
            }

            if (list.Count > 0)
            {
                MsgBox msg = new MsgBox();
                msg.show(string.Format("Voulez-vous faire l'adminission de ce(s) {0} élève(s) / étudiant(s) ?", list.Count), "Confirmation", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                msg.ShowDialog();
                if (msg.clicked == "Oui")
                {
                    using (var donnée = new QuitayeContext())
                    {
                        foreach (string item in list)
                            ok = await RedoublementAsync(donnée, item,
                                cbxAnnéePrecedent.Text, Principales.annéescolaire, cbxClasse.Text, new Elève() { Matricule = item, Classe = cbxClasse.Text });
                    }
                    if (ok)
                        Alert.SShow("Redoublement effectué avec succès.", Alert.AlertType.Sucess);
                    await ShowStudentList();
                }
                msg = null;
            }
            dataGridView1.DataSource = null;
            cbxClasse.Enabled = true;
            cbxAnnéePrecedent.Enabled = true;
            btnAdmission.Enabled = true;
            //btnRedoublant.Enabled = true;
            list = null;
        }

        private async void CbxClasse_SelectedIndexChanged(object sender, EventArgs e)
            => await ShowStudentList();

        private void BtnSelectTout_Click(object sender, EventArgs e)
        {
            if (btnSelectTout.Text == "Tout Selectionner")
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                    row.Cells["Selection"].Value = true;
                btnSelectTout.Text = "Tout Deselectionner";
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                    row.Cells["Selection"].Value = false;
                btnSelectTout.Text = "Tout Selectionner";
            }
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            await CallTask();
        }

        public async Task CallTask()
        {
            Task<DataTable> classe = FillCbxAsync();
            Task<DataTable> année = FillAnnéeAsync();
            var class_passage = FillCbxAsync();
            List<Task> tasklist = new List<Task>()
            {
                classe,
                année,
                class_passage
            };

            while (tasklist.Count > 0)
            {
                Task current = await Task.WhenAny(tasklist);
                if (current == classe)
                    DataGrid.FillCbxAsync(cbxClasse, classe.Result, "Nom");
                else if (current == année)
                    DataGrid.FillCbxAsync(cbxAnnéePrecedent, année.Result, "Nom");
                else if(current == class_passage)
                    DataGrid.FillCbxAsync(cbxClassePassage, class_passage.Result, "Nom");
                tasklist.Remove(current);
                current = null;
            }
            classe = null;
            année = null;
            tasklist = null;
        }

        public Task<DataTable> FillCbxAsync() => Task.Factory.StartNew((() => Fillcbx()));

        private DataTable Fillcbx()
        {
            var tblClasseList = new List<Models.Context.tbl_classe>();
            using (var ecoleDataContext = new QuitayeContext())
            {
                if (Principales.type_compte == "Administrateur"
                    || Principales.departement == "Finance/Comptabilité")
                    tblClasseList = ecoleDataContext.tbl_classe.OrderBy((d => d.Nom)).ToList();
                else if (Principales.role == "Agent")
                    tblClasseList = ecoleDataContext
                        .tbl_classe.Where(d => d.Nom == Principales.classes).OrderBy((d => d.Nom)).ToList();
                else if (Principales.role == "Responsable")
                    tblClasseList = ecoleDataContext.tbl_classe.Where(d => d.Cycle == Principales.auth1
                    || d.Cycle == Principales.auth2 || d.Cycle == Principales.auth3
                    || d.Cycle == Principales.auth4).OrderBy(d => d.Nom).ToList();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Nom");
                foreach (var tblClasse in tblClasseList)
                {
                    DataRow row = dataTable.NewRow();
                    row["Id"] = tblClasse.Id;
                    row["Nom"] = tblClasse.Nom;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        public Task<MyTable> SearchStudentListAsync(string search)
            => Task.Factory.StartNew((() => SearchStudentList(search)));

        public MyTable SearchStudentList(string search)
        {
            MyTable myTable = new MyTable();
            using (var ecoleDataContext = new QuitayeContext())
            {
                var source = ecoleDataContext.tbl_année_scolaire.OrderByDescending(d => d.Nom);
                List<Elève> list;
                
                    list = (from i in ecoleDataContext.tbl_inscription
                            where (i.Classe.Contains(search)
                            || i.Année_Scolaire.Contains(search) || i.Nom.Contains(search) || i.Genre.Contains(search)
                            || i.Prenom.Contains(search) || i.Nom_Complet.Trim().Contains(search.Trim()) 
                            || i.Nom_Mère.Contains(search) || i.Nom_Père.Contains(search))
                            select new Elève
                            {
                                Id = i.Id,
                                Prenom = i.Prenom,
                                Nom = i.Nom,
                                Matricule = i.N_Matricule,
                                Père = i.Nom_Père,
                                Mère = i.Nom_Mère,
                                Date_Naissance = i.Date_Naissance,
                                Année_Scolaire = i.Année_Scolaire,
                                Date_Operation = i.Date_Inscription,
                                Auteur = i.Auteur,
                                Genre = i.Genre,
                                Classe = i.Classe
                            }).Distinct().OrderBy(x => x.Prenom)
                            .OrderBy(x => x.Nom).ToList();
                
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Prenom");
                dataTable.Columns.Add("Nom");
                dataTable.Columns.Add("Matricule");
                dataTable.Columns.Add("Genre");
                dataTable.Columns.Add("Date_Naissance");
                dataTable.Columns.Add("Année_Scolaire");
                dataTable.Columns.Add("Date_Opération");
                dataTable.Columns.Add("Classe");
                dataTable.Columns.Add("Auteur");
                foreach (var data in list)
                {
                    DataRow row = dataTable.NewRow();
                    row["Id"] = data.Id;
                    row["Prenom"] = data.Prenom;
                    row["Nom"] = data.Nom;
                    row["Matricule"] = data.Matricule;
                    row["Genre"] = data.Genre;
                    DataRow dataRow = row;
                    string str = Convert.ToDateTime(data.Date_Naissance).ToString("dd/MM/yyyy");
                    dataRow["Date_Naissance"] = str;
                    row["Année_Scolaire"] = data.Année_Scolaire;
                    row["Date_Opération"] = data.Date_Operation;
                    row["Classe"] = data.Classe;
                    row["Auteur"] = data.Auteur;
                    dataTable.Rows.Add(row);
                }
                myTable.Table = dataTable;
                myTable.Effectif = list.Count;
                myTable.Garçon = list.Where(x => x.Genre == "Masculin").Count();
                myTable.Fille = list.Where(x => x.Genre == "Feminin").Count();
                return myTable;
            }
        }

        public Task<MyTable> StudentListAsync(string classe_name, string année_precedente)
            => Task.Factory.StartNew((() => StudentList(classe_name, année_precedente)));

        public MyTable StudentList(string classe_name, string année_precedente)
        {
            MyTable myTable = new MyTable();
            using (var ecoleDataContext = new QuitayeContext())
            {
                var source = ecoleDataContext.tbl_année_scolaire.OrderByDescending(d => d.Nom);
                List<Elève> list;
                //if (source.First().Nom == Principales.annéescolaire)
                //{
                //    list = ecoleDataContext.tbl_inscription
                //        .Where(i => i.Année_Scolaire == année_precedente 
                //        && i.Classe == classe_name && i.Active == "Oui").OrderBy(i => i.Nom)
                //        .ThenBy(i => i.Prenom).Select(i => new Elève() 
                //        {
                //            Id = i.Id,
                //            Prenom = i.Prenom,
                //            Nom = i.Nom,
                //            Date_Naissance = i.Date_Naissance,
                //            Père = i.Nom_Père,
                //            Mère = i.Nom_Mère,
                //            Genre = i.Genre,
                //            Classe = i.Classe,
                //            Date_Inscription = i.Date_Inscription,
                //            Matricule = i.N_Matricule,
                //        }).ToList();
                //}
                //else
                {
                    list = (from i in ecoleDataContext.tbl_inscription
                            where ( (i.Classe == classe_name 
                            && i.Année_Scolaire == année_precedente))
                            
                            select new Elève
                            {
                                Id = i.Id,
                                Prenom = i.Prenom,
                                Nom = i.Nom,
                                Matricule = i.N_Matricule,
                                Père = i.Nom_Père,
                                Mère = i.Nom_Mère,
                                Date_Naissance = i.Date_Naissance,
                                Année_Scolaire = i.Année_Scolaire,
                                Date_Operation = i.Date_Inscription,
                                Auteur = i.Auteur,
                                Genre = i.Genre,
                                Classe = i.Classe
                            }).Distinct().OrderBy(x => x.Prenom)
                            .OrderBy(x => x.Nom).ToList();
                }

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Prenom");
                dataTable.Columns.Add("Nom");
                dataTable.Columns.Add("Matricule");
                dataTable.Columns.Add("Genre");
                dataTable.Columns.Add("Date_Naissance");
                dataTable.Columns.Add("Année_Scolaire");
                dataTable.Columns.Add("Date_Opération");
                dataTable.Columns.Add("Classe");
                dataTable.Columns.Add("Auteur");
                foreach (var data in list)
                {
                    DataRow row = dataTable.NewRow();
                    row["Id"] = data.Id;
                    row["Prenom"] = data.Prenom;
                    row["Nom"] = data.Nom;
                    row["Matricule"] = data.Matricule;
                    row["Genre"] = data.Genre;
                    DataRow dataRow = row;
                    string str = Convert.ToDateTime(data.Date_Naissance).ToString("dd/MM/yyyy");
                    dataRow["Date_Naissance"] = str;
                    row["Année_Scolaire"] = data.Année_Scolaire;
                    row["Date_Opération"] = data.Date_Operation;
                    row["Classe"] = data.Classe;
                    row["Auteur"] = data.Auteur;
                    dataTable.Rows.Add(row);
                }
                myTable.Table = dataTable;
                myTable.Effectif = list.Count;
                myTable.Garçon = list.Where((x => x.Genre == "Masculin")).Count();
                myTable.Fille = list.Where((x => x.Genre == "Feminin")).Count();
                return myTable;
            }
        }

        public async Task ShowStudentList()
        {
            //if (string.IsNullOrEmpty(cbxClasse.Text) 
            //    || string.IsNullOrEmpty(cbxAnnéePrecedent.Text))
            //    return;
            MyTable result = await StudentListAsync(cbxClasse.Text, cbxAnnéePrecedent.Text);
            DataGrid.ShowDataGrid(result, new DataGridView[]
            {
                dataGridView1
             }, show_delete: true, show_select: true);
            lblFille.Text = "Fille : " + result.Fille.ToString();
            lblGarçon.Text = "Garçon : " + result.Garçon.ToString();
            lblEffectif.Text = "Effectif : " + result.Effectif.ToString();
            lblEffectif.Visible = true;
            lblFille.Visible = true;
            lblGarçon.Visible = true;
            result = null;
        }

        public async Task ShowSearchStudentList()
        {
            //if (string.IsNullOrEmpty(cbxClasse.Text) 
            //    || string.IsNullOrEmpty(cbxAnnéePrecedent.Text))
            //    return;
            MyTable result = await SearchStudentListAsync(txtsearch.Text);
            DataGrid.ShowDataGrid(result, new DataGridView[]
            {
                dataGridView1
             }, show_delete: true, show_select: true);
            lblFille.Text = "Fille : " + result.Fille.ToString();
            lblGarçon.Text = "Garçon : " + result.Garçon.ToString();
            lblEffectif.Text = "Effectif : " + result.Effectif.ToString();
            lblEffectif.Visible = true;
            lblFille.Visible = true;
            lblGarçon.Visible = true;
            result = null;
        }

        public Task<DataTable> FillAnnéeAsync()
            => Task.Factory.StartNew(() => FillAnnée());

        private DataTable FillAnnée()
        {
            using (var ecoleDataContext = new QuitayeContext())
            {
                var queryable = ecoleDataContext.tbl_année_scolaire
                    .OrderByDescending(d => d.Nom).Select(d => new
                    {
                        Id = d.Id,
                        Nom = d.Nom
                    });
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Nom");
                foreach (var data in queryable)
                {
                    DataRow row = dataTable.NewRow();
                    row["Id"] = data.Id;
                    row["Nom"] = data.Nom;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        private async void btnAdmission_Click(object sender, EventArgs e)
        {
            bool ok = false;
            btnAdmission.Enabled = false;
            //btnRedoublant.Enabled = false;
            List<string> list = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Selection"].Value))
                    list.Add(row.Cells["Matricule"].Value.ToString());
            }
            if (list.Count > 0)
            {
                MsgBox msg = new MsgBox();
                msg.show(string.Format("Voulez-vous faire l'adminission de ce(s) {0} élève(s) / étudiant(s) ?", list.Count), "Confirmation", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                msg.ShowDialog();
                if (msg.clicked == "Oui")
                {
                    using (var donnée = new QuitayeContext())
                    {
                        foreach (string item in list)
                            ok = await AdmissionAsync(donnée, item,
                                cbxAnnéePrecedent.Text, Principales.annéescolaire, cbxClassePassage.Text,
                                new Elève() { Matricule = item, Classe = cbxClassePassage.Text });
                    }
                    if (ok)
                        Alert.SShow("Admission effectué avec succès.", Alert.AlertType.Sucess);
                    await ShowStudentList();
                }
                msg = null;
            }
            dataGridView1.DataSource = null;
            cbxClasse.Enabled = true;
            cbxAnnéePrecedent.Enabled = true;
            btnAdmission.Enabled = true;
            //btnRedoublant.Enabled = true;
            list = null;
        }

        private async Task<bool> AdmissionAsync(
          QuitayeContext donnée,
          string matricule,
          string année_precedente,
          string année_suivante,
          string classe_name, Elève elève)
        {
            var cl = donnée.tbl_classe.Where(d => d.Nom == classe_name).Select(d => new
            {
                Classe_Sup_Id = d.Classe_Sup_Id
            }).FirstOrDefault();
            var tblInscription = donnée.tbl_inscription
                .Where(d => d.N_Matricule == matricule).First();
            var entity = new Models.Context.tbl_historiqueeffectif();
            int num1 = 1;
            var source1 = donnée.tbl_historiqueeffectif
                .OrderByDescending((d => d.Id)).Select(d => new
                {
                    Id = d.Id
                });
            if (source1.Count() != 0)
                num1 = source1.First().Id + 1;
            entity.Id = num1;
            entity.N_Matricule = tblInscription.N_Matricule;
            entity.Année_Scolaire = année_precedente;
            entity.Classe = elève.Classe;
            entity.Cycle = donnée.tbl_classe.Where(x => x.Nom == elève.Classe).FirstOrDefault().Cycle;
            entity.Auteur = Principales.profile;
            entity.Date_Inscription = new DateTime?(DateTime.Now);
            entity.Scolarité = tblInscription.Scolarité;
            entity.Transport = tblInscription.Transport;
            entity.Cantine = tblInscription.Cantine;
            entity.Type_Scolarité = tblInscription.Type_Scolarité;
            entity.Assurance = tblInscription.Assurance;
            entity.Active = tblInscription.Active;
            entity.NewAnnée_Scolaire = année_suivante;
            donnée.tbl_historiqueeffectif.Add(entity);
            var année = (from d in donnée.tbl_année_scolaire 
                         orderby d.Nom descending select new { Nom = d.Nom }).FirstOrDefault();
            if(année != null)
            {
                if (année_suivante == année.Nom)
                {
                    tblInscription.Classe = classe_name;
                    tblInscription.Année_Scolaire = année_suivante;
                }
            }
            
            //tblInscription.Type_Scolarité = "Normal";
            //tblInscription.Scolarité = new Decimal?();
            await donnée.SaveChangesAsync();
            var tblClasse = donnée.tbl_classe.Where(d => d.Id == cl.Classe_Sup_Id).FirstOrDefault();
            if (tblClasse != null)
            {
                //tblInscription.Classe = tblClasse.Nom;
                //tblInscription.Cycle = tblClasse.Cycle;
            }
            //tblInscription.Cantine = null;
            //tblInscription.Transport = null;
            //tblInscription.Assurance = null;
            //tblInscription.Année_Scolaire = année_suivante;
            await donnée.SaveChangesAsync();
            var source2 = donnée.tbl_payement.OrderByDescending(d => d.Id).Select(d => new
            {
                Id = d.Id
            }).Take(1);
            int num2 = 1;
            if (source2.Count() != 0)
                num2 = source2.First().Id;
            donnée.tbl_payement.Add(new Models.Context.tbl_payement()
            {
                Id = num2 + 1,
                Montant = new Decimal?(0M),
                N_Matricule = elève.Matricule,
                Prenom = tblInscription.Prenom,
                Nom = tblInscription.Nom,
                Classe = elève.Classe,
                Cycle = donnée.tbl_classe.Where(x => x.Nom == elève.Classe).FirstOrDefault().Cycle,
                Année_Scolaire = année_suivante,
                Type = "Scolarité"
            });
            await donnée.SaveChangesAsync();
            return true;
        }

        private async Task<bool> RedoublementAsync(
          QuitayeContext donnée,
          string matricule,
          string année_precedente,
          string année_suivante,
          string classe_name, Elève elève)
        {
            var data = donnée.tbl_classe.Where(d => d.Nom == classe_name).Select(d => new
            {
                Classe = d.Nom,
                Cycle = d.Cycle
            }).FirstOrDefault();
            var tblInscription = donnée.tbl_inscription
                .Where(d => d.N_Matricule == matricule).First();
            var entity = new Models.Context.tbl_historiqueeffectif();
            int num1 = 1;
            var source1 = donnée.tbl_historiqueeffectif
                .OrderByDescending(d => d.Id).Select(d => new
                {
                    Id = d.Id
                });
            if (source1.Count() != 0)
                num1 = source1.First().Id + 1;
            entity.Id = num1;
            entity.N_Matricule = tblInscription.N_Matricule;
            entity.Année_Scolaire = année_precedente;
            entity.Classe = elève.Classe;
            entity.Cycle = donnée.tbl_classe.Where(x => x.Nom == elève.Classe).FirstOrDefault().Cycle;
            entity.Auteur = Principales.profile;
            entity.Date_Inscription = new DateTime?(DateTime.Now);
            entity.Scolarité = tblInscription.Scolarité;
            entity.Transport = tblInscription.Transport;
            entity.Cantine = tblInscription.Cantine;
            entity.Type_Scolarité = tblInscription.Type_Scolarité;
            entity.Assurance = tblInscription.Assurance;
            entity.Active = tblInscription.Active;
            entity.NewAnnée_Scolaire = année_suivante;
            donnée.tbl_historiqueeffectif.Add(entity);
            //tblInscription.Type_Scolarité = "Normal";
            //tblInscription.Scolarité = new Decimal?();
            donnée.SaveChangesAsync();
            if (data != null)
            {
                //tblInscription.Classe = Classe;
                //tblInscription.Cycle = Cycle;
            }
            //tblInscription.Cantine = null;
            //tblInscription.Transport = null;
            //tblInscription.Assurance = null;
            //tblInscription.Année_Scolaire = année_suivante;
            await donnée.SaveChangesAsync();
            var source2 = donnée.tbl_payement
                .OrderByDescending(d => d.Id).Select(d => new
                {
                    Id = d.Id
                }).Take(1);
            int num2 = 1;
            if (source2.Count() != 0)
                num2 = source2.First().Id;
            donnée.tbl_payement.Add(new Models.Context.tbl_payement()
            {
                Id = num2 + 1,
                Montant = new Decimal?(0M),
                N_Matricule = tblInscription.N_Matricule,
                Prenom = tblInscription.Prenom,
                Nom = tblInscription.Nom,
                Classe = elève.Classe,
                Cycle = donnée.tbl_classe.Where(x => x.Nom == elève.Classe).FirstOrDefault().Cycle,
                Année_Scolaire = année_suivante,
                Type = "Scolarité"
            });
            await donnée.SaveChangesAsync();
            return true;
        }

        private void cbxClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
