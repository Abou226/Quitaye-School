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

namespace Quitaye_School.User_Interface
{
    public partial class Edit_Group_Classe : Form
    {
        public Timer LoadTimer { get; set; }
        public Edit_Group_Classe()
        {
            InitializeComponent();
            LoadTimer = new Timer();
            LoadTimer.Enabled = false;
            LoadTimer.Interval = 10;
            LoadTimer.Start();
            LoadTimer.Tick += LoadTimer_Tick;
            btnValidate.Click += BtnValidate_Click;
            txtsearch.TextChanged += Txtsearch_TextChanged;
        }

        private async void Txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtsearch.Text))
            {
                await CallSearch(txtsearch.Text);
            }
        }

        private async void BtnValidate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbxCycle.Text))
            {
                var classe = cbxCycle.Text;
                cbxCycle.Enabled = false;
                List<Elève> list = new List<Elève>();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Selection"].Value))
                        list.Add(new Elève() { Matricule = row.Cells["N_Matricule"].Value.ToString(), Classe = classe });
                }

                MsgBox msg = new MsgBox();
                msg.show(string.Format("Voulez-vous modifier la classe ce(s) {0} elève(s) ?", list.Count), "Modification", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                msg.ShowDialog();
                if (msg.clicked == "Oui")
                {
                    await SaveAll(list);
                }
                cbxCycle.Enabled = true;
            }
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            LoadTimer.Stop();
            await CallTask();
        }

        private async Task CallTask()
        {
            var classe = FillClasseAsync();
            var data = FillDataAsync();

            var tasks = new List<Task>() { classe, data };

            while(tasks.Count > 0)
            {
                var current = await Task.WhenAny(tasks);
                if(current == classe)
                {
                    Quitaye_School.Models.DataGrid.FillCbxAsync(cbxCycle, classe.Result, "Nom");
                }
                else if(current == data)
                {
                    //dataGridView1.DataSource = data.Result.Table;
                    Quitaye_School.Models.DataGrid.ShowDataGrid(data.Result, new DataGridView[] { dataGridView1 }, false, false, false, false, true);
                }

                tasks.Remove(current);
            }
        }

        private async Task CallClasse()
        {
            var result = await FillClasseAsync();
            Quitaye_School.Models.DataGrid.FillCbxAsync(cbxCycle, result, "Nom");
        }
        private Task<DataTable> FillClasseAsync() => Task.Factory.StartNew(() => FillClasse());
        private DataTable FillClasse()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Nom");
            using(var donnée = new QuitayeContext())
            {
                var dar = (from d in donnée.tbl_classe 
                           orderby d.Nom select new { Id = d.Id, Nom = d.Nom });
                foreach (var item in dar)
                {
                    var row = dt.NewRow();
                    row["Id"] = item.Id;
                    row["Nom"] = item.Nom;
                    dt.Rows.Add(row);
                }
            }

            return dt;
        }

        private async Task SaveAll(List<Elève> list)
        {
            foreach (var item in list)
            {
                await SaveSingleAsync(item);
            }

            Alert.SShow("Operation de modification effectué avec succès.", Alert.AlertType.Sucess);
            await CallData();
        }

     
        private async Task<bool> SaveSingleAsync(Elève eleve)
        {
            using(var donnée = new QuitayeContext())
            {
                var el = (from d in donnée.tbl_inscription 
                          where d.N_Matricule == eleve.Matricule select d).FirstOrDefault();
                el.Classe = eleve.Classe;
                await donnée.SaveChangesAsync();
                return true;
            }
        }

        private async Task CallData()
        {
            var data = await FillDataAsync();
            Quitaye_School.Models.DataGrid.ShowDataGrid(data, new DataGridView[] { dataGridView1 }, false, false, false, false, true);
        }

        private async Task CallSearch(string search)
        {
            var data = await FillSearchAsync(search);
            Quitaye_School.Models.DataGrid.ShowDataGrid(data, new DataGridView[] { dataGridView1 }, false, false, false, false, true);
        }
        private Task<MyTable> FillDataAsync() => Task.Factory.StartNew(() => FillData());
        private MyTable FillData()
        {
            MyTable table = new MyTable();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("N_Matricule");
            dt.Columns.Add("Prenom");
            dt.Columns.Add("Nom");
            dt.Columns.Add("Date_Naissance");
            dt.Columns.Add("Classe_Actuelle");
            dt.Columns.Add("Genre");
            dt.Columns.Add("Année_Scolaire");
            dt.Columns.Add("Père");
            dt.Columns.Add("Mère");

            using(var donnée = new QuitayeContext())
            {
                var elève = (from d in donnée.tbl_inscription 
                             orderby d.Nom, d.Prenom select new
                {
                                 Id = d.Id,
                                 Prenom = d.Prenom,
                                 Nom = d.Nom, 
                                 N_Matricule = d.N_Matricule,
                                 Genre = d.Genre,
                                 Classe = d.Classe,
                                 AnnéeScolaire = d.Année_Scolaire,
                                 Date_Naissance = d.Date_Naissance,
                                 Père = d.Nom_Père,
                                 Mère = d.Nom_Mère,
                });

                foreach (var item in elève)
                {
                    var row = dt.NewRow();
                    row["Id"] = item.Id;
                    row["N_Matricule"] = item.N_Matricule;
                    row["Prenom"] = item.Prenom;
                    row["Nom"] = item.Nom;
                    row["Genre"] = item.Genre;
                    row["Classe_Actuelle"] = item.Classe;
                    row["Année_Scolaire"] = item.AnnéeScolaire;
                    row["Date_Naissance"] = item.Date_Naissance.Value.ToString("dd/MM/yyyy");
                    row["Père"] = item.Père;
                    row["Mère"] = item.Mère;
                    dt.Rows.Add(row);
                }
                table.Table = dt;
                return table;
            }
        }

        private Task<MyTable> FillSearchAsync(string search) => Task.Factory.StartNew(() => FillSearch(search));
        private MyTable FillSearch(string search)
        {
            MyTable table = new MyTable();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("N_Matricule");
            dt.Columns.Add("Prenom");
            dt.Columns.Add("Nom");
            dt.Columns.Add("Date_Naissance");
            dt.Columns.Add("Classe_Actuelle");
            dt.Columns.Add("Genre");
            dt.Columns.Add("Année_Scolaire");
            dt.Columns.Add("Père");
            dt.Columns.Add("Mère");

            using (var donnée = new QuitayeContext())
            {
                var elève = (from d in donnée.tbl_inscription
                             where d.Prenom.Contains(search) || d.Nom.Contains(search) 
                             || (d.Prenom + d.Nom).Contains(search) || d.Nom_Complet.Contains(search) 
                             || d.Classe.Contains(search) || d.Genre.Contains(search) || d.N_Matricule.Equals(search)
                             orderby d.Nom, d.Prenom
                             select new
                             {
                                 Id = d.Id,
                                 Prenom = d.Prenom,
                                 Nom = d.Nom,
                                 N_Matricule = d.N_Matricule,
                                 Genre = d.Genre,
                                 Classe = d.Classe,
                                 AnnéeScolaire = d.Année_Scolaire,
                                 Date_Naissance = d.Date_Naissance,
                                 Père = d.Nom_Père,
                                 Mère = d.Nom_Mère
                             });

                foreach (var item in elève)
                {
                    var row = dt.NewRow();
                    row["Id"] = item.Id;
                    row["N_Matricule"] = item.N_Matricule;
                    row["Prenom"] = item.Prenom;
                    row["Nom"] = item.Nom;
                    row["Genre"] = item.Genre;
                    row["Classe_Actuelle"] = item.Classe;
                    row["Année_Scolaire"] = item.AnnéeScolaire;
                    row["Date_Naissance"] = item.Date_Naissance.Value.ToString("dd/MM/yyyy");
                    row["Père"] = item.Père;
                    row["Mère"] = item.Mère;
                    dt.Rows.Add(row);
                }
                table.Table = dt;
                return table;
            }
        }
    }
}
