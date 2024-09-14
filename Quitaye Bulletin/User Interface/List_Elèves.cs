using iTextSharp.text;
using iTextSharp.text.pdf;
using PrintAction;
using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class List_Elèves : Form
    {
        string mycontrng = LogIn.mycontrng;
        private int temp;
        private static string name;
        public string classes;

        public List_Elèves()
        {
            InitializeComponent();
            lblEffectif.Visible = false;
            lblFille.Visible = false;
            lblGarçon.Visible = false;
            temp = 1;
            timer1.Start();
            LoadTimer = new Timer();
            LoadTimer.Enabled = false;
            LoadTimer.Interval = 100;
            LoadTimer.Start();
            LoadTimer.Tick += LoadTimer_Tick;
            
            //btnReset.Click += BtnReset_Click;
        }

        private async void BtnReset_Click(object sender, EventArgs e)
        {
            MsgBox msg = new MsgBox();
            msg.show("voulez-vous effectuer un reset?", "Reset", 
                MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
            int num = (int)msg.ShowDialog();
            if (!(msg.clicked == "Oui"))
            {
                msg = null;
            }
            else
            {
                await CallReset();
                msg = null;
            }
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            LoadTimer.Stop();
            await CallTasks();
        }

        public Timer LoadTimer { get; set; }

        private async Task CallTasks()
        {
            var classe_data = FillClasseAsync();
            var data = FillDataAsync();
            var tasklist = new List<Task>()
            {
                classe_data,
                data
            };
            while (tasklist.Count > 0)
            {
                var currentTask = await Task.WhenAny(tasklist);
                if (currentTask == data)
                    ShowTableau(data.Result);
                else if (currentTask == classe_data)
                    ShowClasse(classe_data.Result);
                tasklist.Remove(currentTask);
                currentTask = null;
            }
            classe_data = null;
            data = null;
            tasklist = null;
        }

        private async Task CallData()
        {
            MyTable data = await FillDataAsync();
            ShowTableau(data);
            data = null;
        }

        private void ShowTableau(MyTable data)
        {
            if (data.Effectif > 0M)
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = data.Table;
                dataGridView2.DataSource = data.Table;
                dataGridView1.Columns[0].Visible = false;
                DossierColumn dossierColumn = new DossierColumn();
                dossierColumn.HeaderText = "Détails";
                dossierColumn.Name = "Dossier";
                dataGridView1.Columns.Add(dossierColumn);
                dataGridView1.Columns["Dossier"].Width = 50;
                lblFille.Text = "Fille : " + data.Fille.ToString();
                lblGarçon.Text = "Garçon : " + data.Garçon.ToString();
                lblEffectif.Text = "Effectif Total : " + data.Effectif.ToString(); ;
                lblEffectif.Visible = true;
                lblFille.Visible = true;
                lblGarçon.Visible = true;
            }
            else
            {
                dataGridView1.Columns.Clear();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Tableau vide");
                DataRow row = dataTable.NewRow();
                row[0] = "Aucune donnée disponible dans le tableau";
                dataTable.Rows.Add(row);
                dataGridView1.DataSource = dataTable;
                lblFille.Text = "Fille : " + data.Fille.ToString();
                lblGarçon.Text = "Garçon : " + data.Garçon.ToString();
                lblEffectif.Text = "Effectif Total : " + data.Effectif.ToString();
            }
        }

        private Task<MyTable> FillDataAsync() => Task.Factory.StartNew((() => FillData()));

        private MyTable FillData()
        {
            MyTable myTable = new MyTable()
            {
                Table = new DataTable()
            };
            myTable.Table.Columns.Add("N_Matricule");
            myTable.Table.Columns.Add("Prenom");
            myTable.Table.Columns.Add("Nom");
            myTable.Table.Columns.Add("Date_Naissance");
            myTable.Table.Columns.Add("Genre");
            myTable.Table.Columns.Add("Nom_Père");
            myTable.Table.Columns.Add("Nom_Mère");
            myTable.Table.Columns.Add("Classe");
            myTable.Table.Columns.Add("Date_Inscription");
            List<Elève> elèveList = new List<Elève>();
            using (var ecoleDataContext = new QuitayeContext())
            {
                List<Elève> list;
                if (ecoleDataContext.tbl_année_scolaire
                    .OrderByDescending(d => d.Nom)
                    .First().Nom == Principales.annéescolaire)
                {
                    list = ecoleDataContext.tbl_inscription
                        .Where(i => i.Année_Scolaire == Principales.annéescolaire 
                        && i.Classe == classes && i.Active == "Oui")
                        .OrderBy(i => i.Nom).ThenBy(i => i.Prenom)
                        .Select(i => new Elève()
                        {
                            Id = i.Id,
                            Prenom = i.Prenom,
                            Nom = i.Nom,
                            Date_Naissance = i.Date_Naissance,
                            Père = i.Nom_Père,
                            Mère = i.Nom_Mère,
                            Genre = i.Genre,
                            Classe = i.Classe,
                            Date_Inscription = i.Date_Inscription,
                            Matricule = i.N_Matricule,
                        }).ToList();

                }
                else
                {
                    //var class_sup = (from d in ecoleDataContext.tbl_classe 
                    //                 where d.Nom == classes select new {Sup = d.Classe_Sup_Id }).FirstOrDefault();
                    
                    //if (classes.Contains("9"))
                    //{
                    //    var cl = ecoleDataContext.tbl_classe.Where(x => x.Nom.Contains("10")).FirstOrDefault();
                    //    if(cl != null)
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
                    //        Classe = classes
                    //    }).Distinct().OrderBy(x => x.Nom).ThenBy(x => x.Prenom).ToList();
                    //    }else
                    //    {
                    //        list = (
                    //    from ins in ecoleDataContext.tbl_inscription
                    //    join his in ecoleDataContext.tbl_historiqueeffectif
                    //    on ins.N_Matricule equals his.N_Matricule into joinedHis
                    //    from his in joinedHis.DefaultIfEmpty()
                    //    where (ins.Année_Scolaire == Principales.annéescolaire && ins.Classe == classes && ins.Active == "Oui") ||
                    //          (his != null && his.Année_Scolaire == Principales.annéescolaire && his.Classe == classes && his.Active == "Oui")
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
                    //        Classe = classes
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
                        where (ins.Année_Scolaire == Principales.annéescolaire && ins.Classe == classes && ins.Active == "Oui") ||
                              (his != null && his.Année_Scolaire == Principales.annéescolaire && his.Classe == classes && his.Active == "Oui")
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
                            Date_Inscription = ins.Date_Inscription,
                            Classe = classes
                        }).Distinct().OrderBy(x => x.Nom).ThenBy(x => x.Prenom).ToList();
                    }
                    

                    //var encour = (from d in ecoleDataContext.tbl_inscription where d.Classe == classes)

                    {
                        //var result = (ecoleDataContext
                        //    .tbl_inscriptions
                        //    .GroupJoin(ecoleDataContext.tbl_historiqueeffectif, 
                        //    (i => i.N_Matricule), (he => he.N_Matricule), 
                        //    (i, hs) => new
                        //    {
                        //        i = i,
                        //        hs = hs
                        //    }).SelectMany(data => data.hs.DefaultIfEmpty(), (data, h) => new
                        //    {
                        //        data = data,
                        //        h = h
                        //    }).Where(data => data.data.i.Classe == classes 
                        //    && data.data.i.Année_Scolaire == Principales.annéescolaire 
                        //    || data.h.Classe == classes 
                        //    && data.h.Année_Scolaire == Principales.annéescolaire 
                        //    && data.h.Active == "Oui").Distinct().OrderBy(data => data.data.i.Nom)
                        //    .ThenBy(data => data.data.i.Prenom));
                        //list = (from d in res
                        //        select new Elève()
                        //        {
                        //            Id = d.data.i.Id,
                        //            Prenom = d.data.i.Prenom,
                        //            Nom = d.data.i.Nom,
                        //            Date_Naissance = Convert.ToDateTime(d.data.i.Date_Naissance),
                        //            Père = d.data.i.Nom_Père,
                        //            Mère = d.data.i.Nom_Mère,
                        //            Genre = d.data.i.Genre,
                        //            Classe = d.data.i.Classe,
                        //            Date_Inscription = Convert.ToDateTime(d.data.i.Date_Inscription),
                        //            Matricule = d.data.i.N_Matricule,
                        //        }).ToList();
                    }

                }
                foreach (var elève in list)
                {
                    var row = myTable.Table.NewRow();
                    row["N_Matricule"] = elève.Matricule;
                    row["Prenom"] = elève.Prenom;
                    if(elève.Classe != classes)
                    {
                        //elève.Classe =
                            ;
                    }
                    row["Nom"] = elève.Nom;
                    row["Date_Naissance"] = Convert.ToDateTime(elève.Date_Naissance).ToString("dd/MM/yyyy");
                    row["Genre"] = elève.Genre;
                    row["Nom_Père"] = elève.Père;
                    row["Nom_Mère"] = elève.Mère;
                    row["Classe"] = elève.Classe;
                    row["Date_Inscription"] = elève.Date_Inscription;
                    myTable.Table.Rows.Add(row);
                }
                myTable.Effectif = list.Count();
                myTable.Garçon = list.Where((x => x.Genre == "Masculin")).Count();
                myTable.Fille = list.Where(x => x.Genre == "Feminin").Count();
            }
            return myTable;
        }

        private async Task CallClasse()
        {
            MyTable classe_data = await FillClasseAsync();
            ShowClasse(classe_data);
            classe_data = null;
        }

        private void ShowClasse(MyTable classe_data)
        {
            cbxClasse.DataSource = classe_data.Table;
            cbxClasse.DisplayMember = "Name";
            cbxClasse.ValueMember = "Id";
            cbxClasse.Text = null;
        }

        private Task<MyTable> FillClasseAsync() => Task.Factory.StartNew(() => FillClasse());

        private MyTable FillClasse()
        {
            MyTable myTable = new MyTable()
            {
                Table = new DataTable()
            };
            myTable.Table.Columns.Add("Id");
            myTable.Table.Columns.Add("Name");
            var tblClasseList = new List<Models.Context.tbl_classe>();
            using (var ecoleDataContext = new QuitayeContext())
            {
                //if (Principales.type_compte == "Administrateur" 
                //    || Principales.departement == "Finance/Comptabilité")
                //    tblClasseList = ecoleDataContext
                //        .tbl_classe.OrderBy(d => d.Nom).ToList();
                //else if (Principales.role == "Agent")
                    tblClasseList = ecoleDataContext
                        .tbl_classe
                        .OrderBy(d => d.Nom).ToList();
                //else if (Principales.role == "Responsable")
                //    tblClasseList = ecoleDataContext
                //        .tbl_classe.Where(d => d.Cycle == Principales.auth1 
                //    || d.Cycle == Principales.auth2 || d.Cycle == Principales.auth3 
                //    || d.Cycle == Principales.auth4).OrderBy(d => d.Nom).ToList();
                foreach (var tblClasse in tblClasseList)
                {
                    DataRow row = myTable.Table.NewRow();
                    row["Id"] = tblClasse.Id;
                    row["Name"] = tblClasse.Nom;
                    myTable.Table.Rows.Add(row);
                }
            }
            return myTable;
        }

        private async Task CallReset()
        {
            bool result = await ResetEffectifAsync();
            if (!result)
                return;
            Alert.SShow("Reset effectué avec succès.", Alert.AlertType.Sucess);
            await CallResetTable();
        }

        private async Task<bool> ResetEffectifAsync()
        {
            using (var ecoleDataContext = new QuitayeContext())
            {
                DateTime date = new DateTime(2022, 9, 19);
                var source = ecoleDataContext
                    .tbl_inscription
                    .Join(ecoleDataContext.tbl_historiqueeffectif, 
                    (i => i.N_Matricule), (h => h.N_Matricule), (i, h) => new
                {
                    i = i,
                    h = h
                }).Where(data => data.h.Classe != data.i.Classe 
                && data.h.Date_Inscription.Value.Date >= date).Select(data => new
                {
                    Inscription = data.i,
                    Historique = data.h
                }).GroupBy(hi => hi.Historique.N_Matricule)
                .Where(g => g.Count() > 0).OrderByDescending (g => g.Count());
                var result = from g in source select new
                {
                    Id = g.FirstOrDefault().Historique.Id,
                    Prenom = g.FirstOrDefault().Inscription.Prenom,
                    Nom = g.FirstOrDefault().Inscription.Nom,
                    Classe = g.FirstOrDefault().Historique.Classe,
                    N_Matricule = g.FirstOrDefault().Historique.N_Matricule,
                    Année_Scolaire = g.FirstOrDefault().Historique.Année_Scolaire,
                    Date_Naissaince = g.FirstOrDefault().Inscription.Date_Naissance,
                    New_Année_Scolaire = g.FirstOrDefault().Historique.NewAnnée_Scolaire,
                    Date_Opération = g.FirstOrDefault().Historique.Date_Inscription,
                    Classe_Actuelle = g.FirstOrDefault().Inscription.Classe,
                    Nb = g.Count()
                };
                foreach (var data in result)
                {
                    var item = data;
                    var unique = ecoleDataContext.tbl_historiqueeffectif.Where(d => d.N_Matricule == item.N_Matricule && d.Date_Inscription.Value.Date < date).OrderByDescending((d => d.Date_Inscription)).FirstOrDefault();
                    var queryable1 = ecoleDataContext
                        .tbl_historiqueeffectif.Where(d => d.N_Matricule == item.N_Matricule 
                        && d.Date_Inscription.Value.Date >= date && d.Id != unique.Id);
                    if (unique != null)
                    {
                        var tblInscription = ecoleDataContext
                            .tbl_inscription.Where(d => d.N_Matricule == unique.N_Matricule)
                            .FirstOrDefault();
                        if (tblInscription != null)
                        {
                            tblInscription.Classe = unique.Classe;
                            tblInscription.Année_Scolaire = unique.NewAnnée_Scolaire;
                            await ecoleDataContext.SaveChangesAsync();
                        }
                        if (queryable1 != null)
                        {
                            foreach (var entity in queryable1)
                            {
                                ecoleDataContext.tbl_historiqueeffectif.Remove(entity);
                                await ecoleDataContext.SaveChangesAsync();
                            }
                        }
                    }
                    else
                    {
                        var tblInscription = ecoleDataContext
                            .tbl_inscription.Where(d => d.N_Matricule == item.N_Matricule)
                            .FirstOrDefault();
                        if (tblInscription != null)
                        {
                            tblInscription.Classe = item.Classe;
                            tblInscription.Année_Scolaire = item.Année_Scolaire;
                            await ecoleDataContext.SaveChangesAsync();
                        }
                        var queryable2 = ecoleDataContext.tbl_historiqueeffectif.Where(d => d.N_Matricule == item.N_Matricule 
                        && d.Date_Inscription.Value.Date >= date && d.Id != item.Id);
                        if (queryable2 != null)
                        {
                            foreach (var entity in queryable2)
                            {
                                ecoleDataContext.tbl_historiqueeffectif.Remove(entity);
                                await ecoleDataContext.SaveChangesAsync();
                            }
                        }
                    }
                }
                return true;
            }
        }

        private async Task CallResetTable()
        {
            MyTable data = await ResetTableAsync();
            if (data.Table.Rows.Count > 0)
            {
                dataGridView1.DataSource = data.Table;
                data = null;
            }
            else
            {
                dataGridView1.Columns.Clear();
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée disponible dans le tableau";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                dt = null;
                dr = null;
                data = null;
            }
        }

        private Task<MyTable> ResetTableAsync() => Task.Factory.StartNew((() => ResetTable()));

        private MyTable ResetTable()
        {
            using (var ecoleDataContext = new QuitayeContext())
            {
                MyTable myTable = new MyTable()
                {
                    Table = new DataTable()
                };
                myTable.Table.Columns.Add("Id");
                myTable.Table.Columns.Add("Prenom");
                myTable.Table.Columns.Add("Nom");
                myTable.Table.Columns.Add("Classe");
                myTable.Table.Columns.Add("N_Matricule");
                myTable.Table.Columns.Add("Année_Scolaire");
                myTable.Table.Columns.Add("Date_Naissance");
                myTable.Table.Columns.Add("New_Année_Scolaire");
                myTable.Table.Columns.Add("Date_Opération");
                myTable.Table.Columns.Add("Classe_Actuelle");
                myTable.Table.Columns.Add("Nb");
                DateTime date = new DateTime(2022, 9, 19);
                var source = ecoleDataContext.tbl_inscription.
                    Join(ecoleDataContext.tbl_historiqueeffectif, 
                    (i => i.N_Matricule), (h => h.N_Matricule), (i, h) => new
                {
                    i = i,
                    h = h
                }).Where(data => data.h.Classe != data.i.Classe 
                && data.h.Date_Inscription.Value.Date >= date).Select(data => new
                {
                    Inscription = data.i,
                    Historique = data.h
                }).GroupBy(hi => hi.Historique.N_Matricule)
                .Where(g => g.Count() > 0).OrderByDescending(g => g.Count());
                var result = from g in source select new
                {
                    Id = g.FirstOrDefault().Historique.Id,
                    Prenom = g.FirstOrDefault().Inscription.Prenom,
                    Nom = g.FirstOrDefault().Inscription.Nom,
                    Classe = g.FirstOrDefault().Historique.Classe,
                    N_Matricule = g.FirstOrDefault().Historique.N_Matricule,
                    Date_Naissance = g.FirstOrDefault().Inscription.Date_Naissance,
                    Année_Scolaire = g.FirstOrDefault().Historique.Année_Scolaire,
                    New_Année_Scolaire = g.FirstOrDefault().Historique.NewAnnée_Scolaire,
                    Date_Opération = g.FirstOrDefault().Historique.Date_Inscription,
                    Classe_Actuelle = g.FirstOrDefault().Inscription.Classe,
                    Nb = g.Count()
                };
                foreach (var data in result)
                {
                    DataRow row = myTable.Table.NewRow();
                    row["Id"] = data.Id;
                    row["Prenom"] = data.Prenom;
                    row["Nom"] = data.Nom;
                    row["Classe"] = data.Classe;
                    row["N_Matricule"] = data.N_Matricule;
                    row["Date_Naissance"] = data.Date_Naissance;
                    row["Année_Scolaire"] = data.Année_Scolaire;
                    row["New_Année_Scolaire"] = data.New_Année_Scolaire;
                    row["Date_Opération"] = data.Date_Opération;
                    row["Classe_Actuelle"] = data.Classe_Actuelle;
                    row["Nb"] = data.Nb;
                    myTable.Table.Rows.Add(row);
                }
                return myTable;
            }
        }

        private async void cbxClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            classes = cbxClasse.Text;
            await CallData();
            List_Elèves.name = "Effectif " + classes + " " + Principales.annéescolaire + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                using (QuitayeContext ecoleDataContext = new QuitayeContext())
                {
                    string str = dataGridView1.CurrentRow.Cells["Classe"].Value.ToString();
                    string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    var eleve = ecoleDataContext.tbl_inscription.Where((d => d.N_Matricule == id)).First();
                    var tblClasse = ecoleDataContext.tbl_classe.Where((d => d.Nom == eleve.Classe)).First();
                    var détailsElèves = new Détails_Elèves();
                    détailsElèves.lblTitre.Text = "Détails " + eleve.Nom_Complet;
                    détailsElèves.matricule = id;
                    détailsElèves.prenom = eleve.Prenom;
                    détailsElèves.nom = eleve.Nom;
                    détailsElèves.classes = str;
                    détailsElèves.cycle = tblClasse.Cycle;
                    détailsElèves.genre = eleve.Genre;
                    int num = (int)détailsElèves.ShowDialog();
                }
            }
            else
            {
                if (e.ColumnIndex != 7)
                    return;
                using (var ecoleDataContext = new QuitayeContext())
                {
                    var cl = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    var tblClasse = ecoleDataContext.tbl_classe.Where(d => d.Nom == cl).First();
                    Détails_Classe détailsClasse = new Détails_Classe();
                    détailsClasse.lblTitre.Text = "Détails " + tblClasse.Nom;
                    détailsClasse.cycle = tblClasse.Cycle;
                    Détails_Classe.classes = tblClasse.Nom;
                    détailsClasse.classsss = tblClasse.Nom;
                    Cursor = Cursors.Default;
                    détailsClasse.ShowDialog();
                }
            }
        }

        private async void btnClasse_Click(object sender, EventArgs e)
        {
            Classe cla = new Classe();
            int num = (int)cla.ShowDialog();
            if (!(cla.ok == "Oui"))
            {
                cla = null;
            }
            else
            {
                await CallClasse();
                cla = null;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e) => Print.PrintExcelFile(dataGridView1, "Effectif " + classes, List_Elèves.name, "Quitaye School");

        private void btnPDF_Click(object sender, EventArgs e) => Print.PrintPdfFile(dataGridView2, List_Elèves.name, "Effectif " + classes, "Année Scolaire", Principales.annéescolaire, mycontrng, "Quitaye School", true);

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (temp != 1)
                return;
            classes = cbxClasse.Text;
            List_Elèves.name = "Effectif " + classes + " " + Principales.annéescolaire + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
        }
    }
}
