using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class List_Employ_Du_Temps : Form
    {
        public List_Employ_Du_Temps(int id, bool for_classe = true)
        {
            InitializeComponent();
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            For_Classe = for_classe;
            Id = id;
            btnAjouter.Click += BtnAjouter_Click;
        }

        private async void BtnAjouter_Click(object sender, EventArgs e)
        {
            if (For_Classe)
            {
                var emploi = new tbl_emploi_du_temp();
                emploi.StartTime = Debut.Value.ToLocalTime();
                emploi.EndTime = Fin.Value.ToLocalTime();
                emploi.Date = DateTime.Now;
                emploi.DayOfWeek = cbxJour.SelectedIndex+1;
                emploi.Classe_Id = Convert.ToInt32(cbxClasse.SelectedValue);
                emploi.Prof_Id = Convert.ToInt32(cbxProf.SelectedValue);
                emploi.Auteur = Principales.profile;
                var result = await AddEmploiDuTempsAsync(emploi);
                if (result)
                {
                    Alert.SShow("", Alert.AlertType.Sucess);
                }
            }
        }

        private async Task<bool> AddEmploiDuTempsAsync(tbl_emploi_du_temp emploi_Du_Temp)
        {
            using(var donnée = new QuitayeContext())
            {
                var id = donnée.tbl_emploi_du_temp.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault() + 1;
                emploi_Du_Temp.Id = id;
                donnée.tbl_emploi_du_temp.Add(emploi_Du_Temp);
                await donnée.SaveChangesAsync();

                return true;
            }
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallTask();
        }

        Timer loadTimer = new Timer();

        public bool For_Classe { get; }
        public int Id { get; }

        private async Task CallData()
        {
            var data = await FillTimeTableAsync(Id);
            if(data.Rows.Count != 0)
            {
                dataGridView1.DataSource = data;
            }
            else
            {
                var dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                var rd = dt.NewRow();
                rd[0] = "Aucun emploie disponible pour l'instant !";
                dt.Rows.Add(rd);
                dataGridView1.DataSource = dt;
            }
        }

        private async Task CallTask()
        {
            var classes = FillClasseAsync();
            var profs = FillProfAsync();
            var matière = FillMatièreAsync();
            var emploies = FillTimeTableAsync(Id);
            var tasklist = new List<Task>() { classes, profs, matière, emploies };

            while(tasklist.Count > 0)
            {
                var current = await Task.WhenAny(tasklist);
                if(current == classes)
                {
                    cbxClasse.DataSource = classes.Result;
                    cbxClasse.DisplayMember = "Name";
                    cbxClasse.ValueMember = "Id";
                    cbxClasse.Text = null;
                }else if(current == profs)
                {
                    cbxProf.DataSource = profs.Result;
                    cbxProf.DisplayMember = "Name";
                    cbxProf.ValueMember = "Id";
                    cbxProf.Text = null;
                }else if(current == emploies)
                {
                    if(emploies.Result.Rows.Count != 0)
                    {
                        dataGridView1.DataSource = emploies.Result;
                    }else
                    {
                        var dt = new DataTable();
                        dt.Columns.Add("Tableau Vide");
                        var rd = dt.NewRow();
                        rd[0] = "Aucun emploie disponible pour l'instant !";
                        dt.Rows.Add(rd);
                        dataGridView1.DataSource = dt;
                    }
                }
                else if(current == matière)
                {
                    cbxMatiere.DataSource = matière.Result;
                    cbxMatiere.DisplayMember = "Name";
                    cbxMatiere.ValueMember = "Id";
                    cbxMatiere.Text = null;
                }

                tasklist.Remove(current);
            }
        }
        public async Task<DataTable> FillClasseAsync()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");

            using(var donnée = new QuitayeContext())
            {
                var data = await (from d in donnée.tbl_classe
                            orderby d.Nom
                            select new
                            {
                                Id = d.Id,
                                Nom = d.Nom
                            }).ToListAsync();

                foreach (var item in data)
                {
                    var rd = dt.NewRow();
                    rd["Id"] = item.Id;
                    rd["Name"] = item.Nom;
                    dt.Rows.Add(rd);
                }

                return dt;
            }
        }

        public async Task<DataTable> FillProfAsync()
        {
            var dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");

            using (var donnée = new QuitayeContext())
            {
                var data = await (from d in donnée.tbl_enseignant
                                  join mat in donnée.tbl_matiere on d.Id equals mat.Enseignant into ma 
                                  from m in ma.DefaultIfEmpty()
                                  orderby d.Nom
                                  select new
                                  {
                                      Id = d.Id,
                                      Nom = d.Nom,
                                      Matières = m.Nom
                                  }).ToListAsync();

                foreach (var item in data)
                {
                    var rd = dt.NewRow();
                    rd["Id"] = item.Id;
                    rd["Name"] = $"{item.Nom} ({item.Matières})";
                    dt.Rows.Add(rd);
                }

                return dt;
            }
        }

        public async Task<DataTable> FillMatièreAsync()
        {
            var dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");

            using (var donnée = new QuitayeContext())
            {
                var data = await (from d in donnée.tbl_matiere
                                  
                                  orderby d.Nom
                                  select new
                                  {
                                      Id = d.Id,
                                      Nom = d.Nom,
                                  }).ToListAsync();

                foreach (var item in data)
                {
                    var rd = dt.NewRow();
                    rd["Id"] = item.Id;
                    rd["Name"] = $"{item.Nom}";
                    dt.Rows.Add(rd);
                }

                return dt;
            }
        }

        public async Task<DataTable> FillTimeTableAsync(int id)
        {
            var dt = new DataTable();
            dt.Columns.Add("Heure");
            dt.Columns.Add("Lundi");
            dt.Columns.Add("Marci");
            dt.Columns.Add("Mercredi");
            dt.Columns.Add("Jeudi");
            dt.Columns.Add("Vendredi");
            dt.Columns.Add("Samédi");
            dt.Columns.Add("Dimanche");

            using (var donnée = new QuitayeContext())
            {
                var schedules = new List<object[]>(); // List to store schedules

                if (For_Classe)
                {
                    var data = await (from d in donnée.tbl_emploi_du_temp
                                      join ens in donnée.tbl_enseignant
                                      on d.Id equals ens.Id into en
                                      from e in en.DefaultIfEmpty()
                                      where d.Classe_Id == id
                                      select new
                                      {
                                          Id = d.Id,
                                          DayOfWeek = d.DayOfWeek,
                                          StartTime = d.StartTime,
                                          EndTime = d.EndTime,
                                          Enseignants = $"M. {e.Nom_Complet}"
                                      }).ToListAsync();

                    // Populate schedules list
                    foreach (var item in data)
                    {
                        var schedule = new object[8];
                        schedule[0] = $"{item.StartTime:HH:mm} - {item.EndTime:HH:mm} ({item.Enseignants})";
                        // Set the appropriate day of the week column based on item.DayOfWeek
                        switch (item.DayOfWeek)
                        {
                            case 1:
                                schedule[1] = item.Id; // You can modify this column as needed
                                break;
                            case 2:
                                schedule[2] = item.Id; // You can modify this column as needed
                                break;
                            case 3:
                                schedule[3] = item.Id; // You can modify this column as needed
                                break;
                            case 4:
                                schedule[4] = item.Id; // You can modify this column as needed
                                break;
                            case 5:
                                schedule[5] = item.Id; // You can modify this column as needed
                                break;
                            case 6:
                                schedule[6] = item.Id; // You can modify this column as needed
                                break;
                            case 7:
                                schedule[7] = item.Id; // You can modify this column as needed
                                break;
                        }
                        schedules.Add(schedule);
                    }
                }
                else
                {
                    var data = await (from d in donnée.tbl_emploi_du_temp
                                      join cla in donnée.tbl_classe
                                      on d.Id equals cla.Id into cl
                                      from c in cl.DefaultIfEmpty()
                                      where d.Prof_Id == id
                                      select new
                                      {
                                          Id = d.Id,
                                          DayOfWeek = d.DayOfWeek,
                                          StartTime = d.StartTime,
                                          EndTime = d.EndTime,
                                          Classe = c.Nom
                                      }).ToListAsync();

                    // Populate schedules list
                    foreach (var item in data)
                    {
                        var schedule = new object[8];
                        schedule[0] = $"{item.StartTime:HH:mm} - {item.EndTime:HH:mm} ({item.Classe})";
                        // Set the appropriate day of the week column based on item.DayOfWeek
                        switch (item.DayOfWeek)
                        {
                            case 1:
                                schedule[1] = item.Id; // You can modify this column as needed
                                break;
                            case 2:
                                schedule[2] = item.Id; // You can modify this column as needed
                                break;
                            case 3:
                                schedule[3] = item.Id; // You can modify this column as needed
                                break;
                            case 4:
                                schedule[4] = item.Id; // You can modify this column as needed
                                break;
                            case 5:
                                schedule[5] = item.Id; // You can modify this column as needed
                                break;
                            case 6:
                                schedule[6] = item.Id; // You can modify this column as needed
                                break;
                            case 7:
                                schedule[7] = item.Id; // You can modify this column as needed
                                break;
                        }
                        schedules.Add(schedule);
                    }
                }

                // Populate the DataTable based on the schedules list
                foreach (var schedule in schedules)
                {
                    dt.Rows.Add(schedule);
                }

                // You can return the populated DataTable (dt) or use it as needed.
                return dt;
            }
        }
    }
}
