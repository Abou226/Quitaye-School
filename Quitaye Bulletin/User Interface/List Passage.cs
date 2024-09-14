using Microsoft.Office.Interop.Excel;
using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGrid = Quitaye_School.Models.DataGrid;
using DataTable = System.Data.DataTable;

namespace Quitaye_School.User_Interface
{
    public partial class List_Passage : Form
    {
        private Timer TimerLoad;
        public List_Passage()
        {
            InitializeComponent();
            TimerLoad = new Timer();
            TimerLoad.Enabled = false;
            TimerLoad.Interval = 10;
            TimerLoad.Start();
            TimerLoad.Tick += TimerLoad_Tick;
            cbxAnnéeSuivante.SelectedIndexChanged += CbxClasse_SelectedIndexChanged;
            cbxAnnéePrecedente.SelectedIndexChanged += CbxClasse_SelectedIndexChanged;
            cbxClasse.SelectedIndexChanged += CbxClasse_SelectedIndexChanged;
            dataGridView1.CellClick += DataGridView1_CellClick;
            btnToutAnnuler.Click += BtnToutAnnuler_Click;
            btnToutSelectionner.Click += BtnToutSelectionner_Click;
            txtsearch.TextChanged += Txtsearch_TextChanged;
            btnSelectionDoublons.Click += BtnSelectionDoublons_Click;
        }

        private async void BtnSelectionDoublons_Click(object sender, EventArgs e)
        {
            //var list = await ListDoublonsAsync(cbxClasse.Text);
            ;
        }

        //private Task<List<Elève>> ListDoublonsAsync(string search) => Task.Factory.StartNew(() => ListDoublons(search));
        //private List<Elève> ListDoublons(string search)
        //{
        //    using(var donnée = new QuitayeContext())
        //    {
        //        return (from i in donnée.tbl_inscription
        //                    join hes in donnée.tbl_historiqueeffectif
        //                    on i.N_Matricule equals hes.N_Matricule into ih
        //                    from h in ih.DefaultIfEmpty()
        //                    where h.Active == "Oui"
        //                    && (h.Classe == search || i.Prenom == search 
        //                    || i.Nom == search || h.N_Matricule.Equals(search))
        //                    select new { i, h }
        //                    into d
        //                    group d by new { Matricule = d.h.N_Matricule, Année_Precedente = d.h.Année_Scolaire, Année_Suivante = d.h.NewAnnée_Scolaire } into data where data.Key.Matricule.Count() > 1
        //                    orderby data.FirstOrDefault(x => x.i.N_Matricule == data.Key.Matricule).i.Nom, 
        //                    data.FirstOrDefault(x => x.i.N_Matricule == data.Key.Matricule).i.Prenom, 
        //                    data.FirstOrDefault(x => x.i.N_Matricule == data.Key.Matricule).i.Date_Naissance.Value
        //                    select new Elève()
        //                    {
        //                        Id = data.FirstOrDefault(x => x.i.N_Matricule == data.Key.Matricule).h.Id,
        //                        Prenom = data.i.Prenom,
        //                        Nom = data.i.Nom,
        //                        Matricule = data.i.N_Matricule,
        //                        Date_Naissance = Convert.ToDateTime(data.i.Date_Naissance),
        //                        Genre = data.i.Genre,
        //                        Classe = data.h.Classe,
        //                        Cantine = data.i.Classe
        //                    }).Distinct().OrderBy(x => x.Prenom).OrderBy(x => x.Nom).ToList();
        //    }
        //}

        private async void Txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtsearch.Text))
            {
                MyTable result = await SearchDataAsync(txtsearch.Text);
                DataGrid.ShowDataGrid(result, new DataGridView[2]
                {
                    dataGridView1,
                    dataGridView2
                }, show_delete: true, show_select: true);
                lblEffectif.Text = "Effectif : " + result.Effectif.ToString("N0");
                lblFille.Text = "Fille : " + result.Fille.ToString("N0");
                lblGarçon.Text = "Garçon : " + result.Garçon.ToString("N0");
                result = null;
            }
            else
                await CallTableList();
        }

        private void BtnToutSelectionner_Click(object sender, EventArgs e)
        {
            if (btnToutSelectionner.Text == "Tout Selectionner")
            {
                btnToutSelectionner.Enabled = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                    row.Cells["Selection"].Value = true;
                btnToutSelectionner.Enabled = true;
                btnToutSelectionner.Text = "Tout Deselectionner";
            }
            else
            {
                btnToutSelectionner.Enabled = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                    row.Cells["Selection"].Value = false;
                btnToutSelectionner.Enabled = true;
                btnToutSelectionner.Text = "Tout Selectionner";
            }
        }

        private async void BtnToutAnnuler_Click(object sender, EventArgs e)
        {
            if (!(Principales.type_compte == "Administrateur"))
                return;
            List<int> list = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Selection"].Value))
                    list.Add(Convert.ToInt32(row.Cells["Id"].Value));
            }
            MsgBox msg = new MsgBox();
            msg.show(string.Format("Voulez-vous annuler ce(s) {0} passage(s) ?", list.Count), "Suppression/Annulation", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
            msg.ShowDialog();
            if (msg.clicked == "Oui")
            {
                if (await DeletePassageListAsync(list))
                {

                }
                    await CallTableList();
            }
            list = null;
            msg = null;
        }

        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!dataGridView1.Columns[e.ColumnIndex].Name.Equals("Sup") 
                    || !(Principales.type_compte == "Administrateur"))
                    return;
                int id = int.Parse(dataGridView1.CurrentRow.Cells["Id"].Value.ToString());
                MsgBox msg = new MsgBox();
                msg.show("Voulez-vous annuler ce passage ?", "Annulation/Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                int num = (int)msg.ShowDialog();
                if (msg.clicked == "Oui")
                {
                    if (await DeletePassageAsync(id))
                        await CallTableList();
                }
                msg = null;
            }
            catch (Exception ex)
            {

            }
        }

        
        private async Task<bool> DeletePassageAsync(int id)
        {
            using (var ecoleDataContext = new QuitayeContext())
            {
                var entity = ecoleDataContext
                    .tbl_historiqueeffectif
                    .Where(d => d.Id == id).FirstOrDefault();
                if (entity == null)
                    return false;
                ecoleDataContext.tbl_historiqueeffectif.Remove(entity);
                var elève = ecoleDataContext.tbl_inscription.Where(x =>  x.N_Matricule == entity.N_Matricule).FirstOrDefault();
                var cl = ecoleDataContext.tbl_classe.Where(x => x.Nom == entity.Classe).FirstOrDefault();
                elève.Classe = cl.Nom;
                elève.Année_Scolaire = entity.Année_Scolaire;
                await ecoleDataContext.SaveChangesAsync();
                return true;
            }
        }

        private async Task<bool> DeletePassageListAsync(List<int> ids)
        {
            if (ids.Count == 0)
                return false;
            using (var ecoleDataContext = new QuitayeContext())
            {
                var historiqueeffectifs = ecoleDataContext
                    .tbl_historiqueeffectif.Where(d => ids.Contains(d.Id));
                foreach (var entity in historiqueeffectifs)
                {
                    ecoleDataContext.tbl_historiqueeffectif.Remove(entity);
                    var elève = ecoleDataContext.tbl_inscription.Where(x => x.N_Matricule == entity.N_Matricule).FirstOrDefault();
                    var cl = ecoleDataContext.tbl_classe.Where(x => x.Nom == entity.Classe).FirstOrDefault();
                    elève.Classe = cl.Nom;
                    elève.Année_Scolaire = entity.Année_Scolaire;
                    await ecoleDataContext.SaveChangesAsync();
                }
                return true;
            }
        }

        private async void CbxClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbxAnnéePrecedente.Text) 
                && !string.IsNullOrEmpty(cbxAnnéeSuivante.Text))
            {
                await CallTableList();
            }
            else
            {
                MyTable result = await SearchDataAsync(cbxClasse.Text);
                DataGrid.ShowDataGrid(result, new DataGridView[2]
                {
                    dataGridView1,
                    dataGridView2
                }, show_delete: true, show_select: true);
                lblEffectif.Text = "Effectif : " + result.Effectif.ToString("N0");
                lblFille.Text = "Fille : " + result.Fille.ToString("N0");
                lblGarçon.Text = "Garçon : " + result.Garçon.ToString("N0");
                result = null;
            }
        }

        private async void TimerLoad_Tick(object sender, EventArgs e)
        {
            TimerLoad.Stop();
            await CallTaks();
        }

        public async Task CallTableList()
        {
            var result = await TableListAsync(cbxClasse.Text, 
                cbxAnnéePrecedente.Text, cbxAnnéeSuivante.Text);
            DataGrid.ShowDataGrid(result, new DataGridView[1]
            {
                dataGridView1
            }, show_delete: true, show_select: true);
            lblEffectif.Text = "Effectif : " + result.Effectif.ToString("N0");
            lblFille.Text = "Fille : " + result.Fille.ToString("N0");
            lblGarçon.Text = "Garçon : " + result.Garçon.ToString("N0");
            result = null;
        }

        public async Task CallTaks()
        {
            var cbx = FillAnnéeAsync();
            var preced = FillAnnéeAsync();
            var table = TableListAsync();
            var classe = FillClasseAsync();

            var taskList = new List<Task>()
            {
                cbx,
                table,
                preced,
                classe
            };

            while (taskList.Count > 0)
            {
                Task currentTask = await Task.WhenAny(taskList);
                if (currentTask == cbx)
                    DataGrid.FillCbxAsync(cbxAnnéeSuivante, cbx.Result);
                else if (currentTask == preced)
                    DataGrid.FillCbxAsync(cbxAnnéePrecedente, preced.Result);
                else if (currentTask == classe)
                    DataGrid.FillCbxAsync(cbxClasse, classe.Result);
                else if (currentTask == table)
                {
                    DataGrid.ShowDataGrid(table.Result, new DataGridView[1]
                    {
                        dataGridView1
                    }, show_delete: true, show_select: true);
                    lblEffectif.Text = "Effectif : " + table.Result.Effectif.ToString("N0");
                    lblFille.Text = "Fille : " + table.Result.Fille.ToString("N0");
                    lblGarçon.Text = "Garçon : " + table.Result.Garçon.ToString("N0");
                }
                taskList.Remove(currentTask);
                currentTask = null;
            }
            cbx = null;
            preced = null;
            table = null;
            classe = null;
            taskList = null;
        }

        public Task<System.Data.DataTable> FillClasseAsync() 
            => Task.Factory.StartNew(() => FillClasse());

        private System.Data.DataTable FillClasse()
        {
            using (var ecoleDataContext = new QuitayeContext())
            {
                var list = ecoleDataContext.tbl_classe.OrderBy(d => d.Nom).Select(d => new
                {
                    Id = d.Id,
                    Name = d.Nom
                }).ToList();
                var dataTable = new System.Data.DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Name");
                foreach (var data in list)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Name;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        public Task<DataTable> FillAnnéeAsync() => Task.Factory.StartNew(() => FillAnnée());

        private DataTable FillAnnée()
        {
            using (var ecoleDataContext = new QuitayeContext())
            {
                var list = ecoleDataContext.tbl_année_scolaire
                    .OrderByDescending((d => d.Nom)).Select(d => new
                {
                    Id = d.Id,
                    Name = d.Nom
                }).ToList();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Name");
                foreach (var data in list)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Name;
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        public Task<MyTable> SearchDataAsync(string search) 
            => Task.Factory.StartNew(() => SearchData(search));

        public MyTable SearchData(string search)
        {
            using (var ecoleDataContext = new QuitayeContext())
            {
                var myTable = new MyTable();
                var source = (from i in ecoleDataContext.tbl_inscription
                            join hes in ecoleDataContext.tbl_historiqueeffectif
                            on i.N_Matricule equals hes.N_Matricule into ih
                            from h in ih.DefaultIfEmpty()
                            where (h.Classe.Contains(search)
                            || h.Année_Scolaire.Contains(search) || i.Classe.Contains(search)
                            || i.Prenom.Contains(search) || i.Nom_Père.Contains(search) 
                            || i.Nom_Mère.Contains(search)
                            || i.Nom.Contains(search) || i.N_Matricule.Equals(search))
                            && h.Active == "Oui"
                            select new { i, h }
                            into data
                            orderby data.i.Nom, data.i.Prenom, data.i.Date_Naissance.Value
                            select new
                            {
                                Id = data.h.Id,
                                Prenom = data.i.Prenom,
                                Nom = data.i.Nom,
                                Matricule = data.i.N_Matricule,
                                Date_Naissance = data.i.Date_Naissance,
                                Année_Precedente = data.h.Année_Scolaire,
                                Année_Suivante = data.h.NewAnnée_Scolaire,
                                Date_Opération = data.h.Date_Inscription,
                                Auteur = data.h.Auteur,
                                Genre = data.i.Genre,
                                Classe_Actuelle = data.i.Classe,
                                Classe = data.h.Classe
                            }).Distinct().OrderBy(x => x.Prenom).OrderBy(x => x.Nom).ToList();
                
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Prenom");
                dataTable.Columns.Add("Nom");
                dataTable.Columns.Add("Matricule");
                dataTable.Columns.Add("Date_Naissance");
                dataTable.Columns.Add("Année_Precedente");
                dataTable.Columns.Add("Année_Suivante");
                dataTable.Columns.Add("Date_Opération");
                dataTable.Columns.Add("Classe_Actuelle");
                dataTable.Columns.Add("Classe");
                dataTable.Columns.Add("Auteur");
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row["Id"] = data.Id;
                    row["Prenom"] = data.Prenom;
                    row["Nom"] = data.Nom;
                    row["Matricule"] = data.Matricule;
                    DataRow dataRow = row;
                    string str = data.Date_Naissance.Value.ToString("dd/MM/yyyy");
                    dataRow["Date_Naissance"] = str;
                    row["Année_Precedente"] = data.Année_Precedente;
                    row["Année_Suivante"] = data.Année_Suivante;
                    row["Date_Opération"] = data.Date_Opération;
                    row["Classe_Actuelle"] = data.Classe_Actuelle;
                    row["Classe"] = data.Classe;
                    row["Auteur"] = data.Auteur;
                    dataTable.Rows.Add(row);
                }
                myTable.Table = dataTable;
                myTable.Garçon = source.Where(x => x.Genre == "Masculin").Count();
                myTable.Fille = source.Where(x => x.Genre == "Feminin").Count();
                myTable.Effectif = source.Count();
                return myTable;
            }
        }

        public Task<MyTable> TableListAsync(
          string classe_name = null,
          string année_precedente = null,
          string année_suivante = null)
        {
            return Task.Factory.StartNew(() 
                => TableList(classe_name, année_precedente, année_suivante));
        }

        public MyTable TableList(
          string classe_name,
          string année_precedente,
          string année_suivante)
        {
            if (string.IsNullOrEmpty(classe_name) 
                && (string.IsNullOrEmpty(année_precedente) 
                || string.IsNullOrEmpty(année_suivante)))
                return new MyTable() { Table = new DataTable() };
            MyTable myTable = new MyTable();
            using (var ecoleDataContext = new QuitayeContext())
            {
                var source = ecoleDataContext.tbl_historiqueeffectif
                    .Join(ecoleDataContext.tbl_inscription,
                    d => d.N_Matricule, e => e.N_Matricule, (d, e) => new
                    {
                        d = d,
                        e = e
                    }).Where(data => data.d.Classe == classe_name
                    && data.d.Année_Scolaire == année_precedente
                    && data.d.NewAnnée_Scolaire == année_suivante)
                    .OrderBy(data => data.e.Nom)
                    .ThenBy(data => data.e.Prenom)
                    .ThenBy(data => data.e.Date_Naissance.Value)
                    .Select(data => new
                    {
                        Id = data.d.Id,
                        Prenom = data.e.Prenom,
                        Nom = data.e.Nom,
                        Matricule = data.d.N_Matricule,
                        Date_Naissance = data.e.Date_Naissance,
                        Année_Precedente = data.d.Année_Scolaire,
                        Année_Suivante = data.d.NewAnnée_Scolaire,
                        Date_Opération = data.d.Date_Inscription,
                        Auteur = data.d.Auteur,
                        Classe_Actuelle = data.e.Classe,
                        Classe = data.d.Classe,
                        Genre = data.e.Genre
                    });
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Prenom");
                dataTable.Columns.Add("Nom");
                dataTable.Columns.Add("Matricule");
                dataTable.Columns.Add("Date_Naissance");
                dataTable.Columns.Add("Année_Precedente");
                dataTable.Columns.Add("Année_Suivante");
                dataTable.Columns.Add("Date_Opération");
                dataTable.Columns.Add("Classe_Actuelle");
                dataTable.Columns.Add("Classe");
                dataTable.Columns.Add("Auteur");
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row["Id"] = data.Id;
                    row["Prenom"] = data.Prenom;
                    row["Nom"] = data.Nom;
                    row["Matricule"] = data.Matricule;
                    DataRow dataRow = row;
                    DateTime date = data.Date_Naissance.Value;
                    date = date.Date;
                    string str = date.ToString("dd/MM/yyyy");
                    dataRow["Date_Naissance"] = str;
                    row["Année_Precedente"] = data.Année_Precedente;
                    row["Année_Suivante"] = data.Année_Suivante;
                    row["Date_Opération"] = data.Date_Opération;
                    row["Classe_Actuelle"] = data.Classe_Actuelle;
                    row["Classe"] = data.Classe;
                    row["Auteur"] = data.Auteur;
                    dataTable.Rows.Add(row);
                }
                myTable.Table = dataTable;
                myTable.Garçon = source.Where(x => x.Genre == "Masculin").Count();
                myTable.Fille = source.Where(x => x.Genre == "Feminin").Count();
                myTable.Effectif = source.Count();
                return myTable;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}