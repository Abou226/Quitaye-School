using DocumentFormat.OpenXml.Drawing.Charts;
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
using System.Windows.Markup;
using DataTable = System.Data.DataTable;
using DataGrid = Quitaye_School.Models.DataGrid;

namespace Quitaye_School.User_Interface
{
    public partial class Passage_Elève : Form
    {
        private Timer timer = new Timer();
        public string cycle;
        public Passage_Elève()
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
            btnRedoublant.Click += BtnRedoublant_Click;
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private async void BtnRedoublant_Click(object sender, EventArgs e)
        {
            bool ok = false;
            btnAdmission.Enabled = false;
            btnRedoublant.Enabled = false;

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
                                cbxAnnéePrecedent.Text, Principales.annéescolaire, cbxClasse.Text, new Elève() { Matricule = item, Classe = cbxClasse.Text});
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
            btnRedoublant.Enabled = true;
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
            List<Task> tasklist = new List<Task>()
            {
                classe,
                année
            };

            while (tasklist.Count > 0)
            {
                Task current = await Task.WhenAny(tasklist);
                if (current == classe)
                    DataGrid.FillCbxAsync(cbxClasse, classe.Result, "Nom");
                else if (current == année)
                    DataGrid.FillCbxAsync(cbxAnnéePrecedent, année.Result, "Nom");
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

        public Task<MyTable> StudentListAsync(string classe_name, string année_precedente) 
            => Task.Factory.StartNew((() => StudentList(classe_name, année_precedente)));

        public MyTable StudentList(string classe_name, string année_precedente)
        {
            MyTable myTable = new MyTable();
            using (var ecoleDataContext = new QuitayeContext())
            {
                var source = ecoleDataContext.tbl_année_scolaire.OrderByDescending(d => d.Nom);
                var list = new List<Elève>();
                if (source.First().Nom == Principales.annéescolaire)
                {
                    list = ecoleDataContext.tbl_inscription
                                .Where(i => i.Année_Scolaire == année_precedente
                                && i.Classe == classe_name && i.Active == "Oui").OrderBy(i => i.Nom)
                                .ThenBy(i => i.Prenom).Select(i => new Elève()
                                {
                                    Id = i.Id,
                                    Prenom = i.Prenom,
                                    Nom_Complet = i.Prenom +" "+i.Nom,
                                    Nom = i.Nom,
                                    Date_Naissance = i.Date_Naissance,
                                    Classe_Actuelle = i.Classe,
                                    Père = i.Nom_Père,
                                    Année_Scolaire = i.Année_Scolaire,
                                    Mère = i.Nom_Mère,
                                    Genre = i.Genre,
                                    Classe = i.Classe,
                                    Date_Inscription = i.Date_Inscription,
                                    Date_Operation = i.Date_Inscription,
                                    Matricule = i.N_Matricule,
                                    Auteur = i.Auteur,
                                }).ToList();
                }
                else
                {
                    //if (classe_name.Contains("9"))
                    //{
                    //    var cl = ecoleDataContext.tbl_classe.Where(x => x.Nom.Contains("10")).FirstOrDefault();
                    //    if (cl != null)
                    //    {
                    //        var last_ann = ecoleDataContext.tbl_année_scolaire.Where(x => x.Nom == Principales.annéescolaire).FirstOrDefault();
                    //        var ann = ecoleDataContext.tbl_année_scolaire.Where(x => x.Id > last_ann.Id).OrderBy(x => x.Id).FirstOrDefault();

                    //        list = (
                    //    from ins in ecoleDataContext.tbl_inscription
                    //    join his in ecoleDataContext.tbl_historiqueeffectif
                    //    on ins.N_Matricule equals his.N_Matricule into joinedHis
                    //    from his in joinedHis.DefaultIfEmpty()
                    //    where (ins.Année_Scolaire == ann.Nom && ins.Classe == cl.Nom && ins.Active == "Oui") ||
                    //          (his != null && his.Année_Scolaire == ann.Nom && his.Classe == cl.Nom && his.Active == "Oui")
                    //    orderby ins.Nom
                    //    select new Elève()
                    //    {
                    //        Matricule = ins.N_Matricule,
                    //        Id = ins.Id,
                    //        Prenom = ins.Prenom,
                    //        Nom = ins.Nom,
                    //        Date_Naissance = ins.Date_Naissance,
                    //        Genre = ins.Genre,
                    //        Père = ins.Nom_Père,
                    //        Mère = ins.Nom_Mère,
                    //        Date_Inscription = ins.Date_Inscription,
                    //        Classe = classe_name
                    //    }).Distinct().OrderBy(x => x.Nom).ThenBy(x => x.Prenom).ToList();
                    //    }
                    //    else
                    //    {
                    //        list = (
                    //    from ins in ecoleDataContext.tbl_inscription
                    //    join his in ecoleDataContext.tbl_historiqueeffectif
                    //    on ins.N_Matricule equals his.N_Matricule into joinedHis
                    //        from his in joinedHis.DefaultIfEmpty()
                    //    where (ins.Année_Scolaire == Principales.annéescolaire && ins.Classe == classe_name && ins.Active == "Oui") ||
                    //          (his != null && his.Année_Scolaire == Principales.annéescolaire && his.Classe == classe_name && his.Active == "Oui")
                    //    orderby ins.Nom
                    //    select new Elève()
                    //    {
                    //        Matricule = ins.N_Matricule,
                    //        Id = ins.Id,
                    //        Prenom = ins.Prenom,
                    //        Nom = ins.Nom,
                    //        Date_Naissance = ins.Date_Naissance,
                    //        Genre = ins.Genre,
                    //        Père = ins.Nom_Père,
                    //        Mère = ins.Nom_Mère,
                    //        Date_Inscription = ins.Date_Inscription,
                    //        Classe = classe_name
                    //    }).Distinct().OrderBy(x => x.Nom).ThenBy(x => x.Prenom).ToList();
                    //    }

                    //}
                    //else
                    {
                        list = (
                        from ins in ecoleDataContext.tbl_inscription
                        join his in ecoleDataContext.tbl_historiqueeffectif
                        on ins.N_Matricule equals his.N_Matricule into joinedHis
                        from his in joinedHis.DefaultIfEmpty()
                        where (ins.Année_Scolaire == Principales.annéescolaire && ins.Classe == classe_name && ins.Active == "Oui") ||
                              (his != null && his.Année_Scolaire == Principales.annéescolaire && his.Classe == classe_name && his.Active == "Oui")
                        orderby ins.Nom
                        select new Elève()
                        {
                            Matricule = ins.N_Matricule,
                            Id = ins.Id,
                            Prenom = ins.Prenom,
                            Nom = ins.Nom,
                            Date_Naissance = ins.Date_Naissance,
                            Genre = ins.Genre,
                            Père = ins.Nom_Père,
                            Mère = ins.Nom_Mère,
                            Classe_Actuelle = ins.Classe,
                            Date_Inscription = ins.Date_Inscription,
                            Date_Operation = his.Date_Inscription,
                            Auteur = his.Auteur,
                            Classe = classe_name
                        }).Distinct().OrderBy(x => x.Nom).ThenBy(x => x.Prenom).ToList();
                    }
                }
                var dataTable = new System.Data.DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Nom_Complet");
                dataTable.Columns.Add("Matricule");
                dataTable.Columns.Add("Date_Naissance");
                dataTable.Columns.Add("Année_Precedente");
                dataTable.Columns.Add("Année_Suivante");
                dataTable.Columns.Add("Date_Opération");
                dataTable.Columns.Add("Classe_Actuelle");
                dataTable.Columns.Add("Classe");
                dataTable.Columns.Add("Auteur");
                foreach (var data in list)
                {
                    DataRow row = dataTable.NewRow();
                    row["Id"] = data.Id;
                    row["Nom_Complet"] = $"{data.Prenom} {data.Nom}";
                    row["Matricule"] = data.Matricule;
                    DataRow dataRow = row;
                    string str = Convert.ToDateTime(data.Date_Naissance).ToString("dd/MM/yyyy");
                    dataRow["Date_Naissance"] = str;
                    row["Année_Precedente"] = année_precedente;
                    row["Année_Suivante"] = Principales.annéescolaire;
                    row["Date_Opération"] = data.Date_Operation;
                    row["Classe_Actuelle"] = data.Classe_Actuelle;
                    row["Classe"] = data.Classe;
                    row["Auteur"] = data.Auteur;
                    dataTable.Rows.Add(row);
                }
                myTable.Table = dataTable;
                myTable.Garçon = list.Where(x => x.Genre == "Masculin").Count();
                myTable.Fille = list.Where(x => x.Genre == "Feminin").Count();
                myTable.Effectif = list.Count();
                return myTable;
            }
        }

        public async Task ShowStudentList()
        {
            if (string.IsNullOrEmpty(cbxClasse.Text)
                || string.IsNullOrEmpty(cbxAnnéePrecedent.Text))
                return;
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

        public Task<System.Data.DataTable> FillAnnéeAsync() 
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
            btnRedoublant.Enabled = false;
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
                                cbxAnnéePrecedent.Text, Principales.annéescolaire, cbxClasse.Text, 
                                new Elève() { Matricule = item, Classe = cbxClasse.Text  });
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
            btnRedoublant.Enabled = true;
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
            tblInscription.Type_Scolarité = "Normal";
            tblInscription.Scolarité = new Decimal?();
            await donnée.SaveChangesAsync();
            var tblClasse = donnée.tbl_classe.Where(d => d.Id == cl.Classe_Sup_Id).FirstOrDefault();
            if (tblClasse != null)
            {
                tblInscription.Classe = tblClasse.Nom;
                tblInscription.Cycle = tblClasse.Cycle;
            }
            tblInscription.Cantine = null;
            tblInscription.Transport = null;
            tblInscription.Assurance = null;
            tblInscription.Année_Scolaire = année_suivante;
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
            tblInscription.Type_Scolarité = "Normal";
            tblInscription.Scolarité = new Decimal?();
            await donnée.SaveChangesAsync();
            if (data != null)
            {
                tblInscription.Classe = data.Classe;
                tblInscription.Cycle = data.Cycle;
            }
            tblInscription.Cantine = null;
            tblInscription.Transport = null;
            tblInscription.Assurance = null;
            tblInscription.Année_Scolaire = année_suivante;
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
