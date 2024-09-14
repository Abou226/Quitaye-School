using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using Quitaye_Medical.Models;
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
    public partial class Paramettre_Produit : Form
    {
        public Paramettre_Produit()
        {
            InitializeComponent();
            btnFormule.Click += BtnFormule_Click;
            btnMesure.Click += BtnMesure_Click;
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            dataGridView1.CellClick += DataGridView1_CellClick;
            dataGridView2.CellClick += DataGridView2_CellClick;
            radioButton1.CheckedChanged += RadioButton_CheckedChanged;
            radioButton2.CheckedChanged += RadioButton_CheckedChanged;
        }


        private  async void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var result = await SaveDefaultAsync((RadioButton)sender);
        }
        
        private async Task<bool> SaveDefaultAsync(RadioButton radioButton)
        {
            using (var donnée = new QuitayeContext())
            {
                var des = (from d in donnée.tbl_operation_default where d.Nom == radioButton.Text select d).First();
                des.Default = "Oui";

                var desr = from d in donnée.tbl_operation_default where d.Nom != radioButton.Text select d;
                foreach (var item in desr)
                {
                    item.Default = "Non";
                    await donnée.SaveChangesAsync();
                }
                
                await donnée.SaveChangesAsync();
            }
            return true;
        }
        private async void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Principales.role == "Administrateur" || Principales.type_compte.Contains("Administrateur"))
            {
                if (dataGridView2.Columns.Count >= 2)
                {
                    if (e.ColumnIndex >= 0)
                    {
                        if (dataGridView2.Columns[e.ColumnIndex].Name.Equals("Sup") == true)
                        {
                            int id = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
                            MsgBox msg = new MsgBox();
                            msg.show("Voulez-vous supprimer cette mesure ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                            msg.ShowDialog();
                            if (msg.clicked == "Non")
                                return;
                            else if (msg.clicked == "Oui")
                            {
                                using (var donnée = new QuitayeContext())
                                {
                                    var v = (from d in donnée.tbl_mesure_vente where d.Id == id select d).First();
                                    donnée.tbl_mesure_vente.Remove(v);
                                    await donnée.SaveChangesAsync();
                                    await CallMesure();
                                    Alert.SShow("Mesure supprimeé avec succès.", Alert.AlertType.Sucess);
                                }
                            }
                        }
                    }
                }
            }
        }

        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(Principales.role == "Administrateur" || Principales.type_compte.Contains("Administrateur"))
            {
                if (dataGridView1.Columns.Count >= 2)
                {
                    if (e.ColumnIndex >= 0)
                    {
                        if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Sup") == true)
                        {
                            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                            MsgBox msg = new MsgBox();
                            msg.show("Voulez-vous supprimer cette formule ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                            msg.ShowDialog();
                            if (msg.clicked == "Non")
                                return;
                            else if (msg.clicked == "Oui")
                            {
                                using (var donnée = new QuitayeContext())
                                {
                                    var v = (from d in donnée.tbl_formule_mesure_vente where d.Id == id select d).First();
                                    donnée.tbl_formule_mesure_vente.Remove(v);
                                    await donnée.SaveChangesAsync();
                                    await CallFormule();
                                    Alert.SShow("Formule supprimeé avec succès.", Alert.AlertType.Sucess);
                                }
                            }
                        }
                    }
                }
            }
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallTask();
            var grossiste = await GrossisteAsync();
            foreach (var item in tableLayoutPanel1.Controls)
            {
                RadioButton rd = (RadioButton)item;
                if(rd.Text == grossiste.Text)
                {
                    
                }
            }
        }

        private Task<RadioButton> GrossisteAsync()
        {
            return Task.Factory.StartNew(() => Grossiste());
        }

        private RadioButton Grossiste()
        {
            RadioButton r = new RadioButton();
            using(var donnée = new QuitayeContext())
            {
                var des = from d in donnée.tbl_operation_default  select d;
                foreach (var item in des)
                {
                    item.Default = "Oui";
                    r.Checked = true;
                    r.Text = item.Nom;
                }
                return r;
            }
        }

        private async Task CallFormule()
        {
            var result = await FillFormuleAsync();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = result;

            try
            {
                AddColumns.Addcolumn(dataGridView1);
                dataGridView1.Columns["Edit"].Visible = false;
            }
            catch (Exception)
            {
                
            }
        }

        private Task<DataTable> FillFormuleAsync()
        {
            return Task.Factory.StartNew(() => FillFormule());
        }
        private DataTable FillFormule()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Formule");
            dt.Columns.Add("Auteur");
            dt.Columns.Add("Date_Ajout");

            using(var donnée = new QuitayeContext())
            {
                var des = from d in donnée.tbl_formule_mesure_vente select d;
                foreach (var item in des)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Formule;
                    dr[2] = item.Auteur;
                    dr[3] = item.Date;

                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }


        private async Task CallTask()
        {
            var mesure = FillMesureAsync();
            var formule = FillFormuleAsync();

            var taskList = new List<Task> { mesure, formule };

            while(taskList.Count > 0)
            {

                var finishedtask = await Task.WhenAny(taskList);

                if(finishedtask == mesure)
                {
                    dataGridView2.Columns.Clear();
                    dataGridView2.DataSource = mesure.Result;
                    try
                    {
                        AddColumns.Addcolumn(dataGridView2);
                        dataGridView2.Columns["Edit"].Visible = false;
                    }
                    catch (Exception)
                    {
                        
                    }
                }
                else if(finishedtask == formule)
                {
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = formule.Result;
                    try
                    {
                        AddColumns.Addcolumn(dataGridView1);
                        dataGridView1.Columns["Edit"].Visible = false;
                    }
                    catch (Exception)
                    {

                    }
                }

                taskList.Remove(finishedtask);
            }
        }
        private async Task CallMesure()
        {
            var result = await FillMesureAsync();

            dataGridView2.Columns.Clear();
            dataGridView2.DataSource = result;

            try
            {
                AddColumns.Addcolumn(dataGridView2);
                dataGridView2.Columns["Edit"].Visible = false;
            }
            catch (Exception)
            {
                
            }
        }

        private Task<DataTable> FillMesureAsync()
        {
            return Task.Factory.StartNew(() => FillMesure());
        }
        private DataTable FillMesure()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Mesure");
            dt.Columns.Add("Auteur");
            dt.Columns.Add("Date_Ajout");

            using (var donnée = new QuitayeContext())
            {
                var des = from d in donnée.tbl_mesure_vente select d;
                foreach (var item in des)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Nom;
                    dr[2] = item.Auteur;
                    dr[3] = item.Date;

                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }


        Timer loadTimer = new Timer();
        private async void BtnMesure_Click(object sender, EventArgs e)
        {
            Ajouter_Mesure_Vente element = new Ajouter_Mesure_Vente();
            element.ShowDialog();
            if (element.ok == "Oui")
            {
                await CallMesure();
            }
        }

        private async void BtnFormule_Click(object sender, EventArgs e)
        {
            Ajouter_Formule_Vente formule = new Ajouter_Formule_Vente();
            formule.ShowDialog();
            if (formule.ok == "Oui")
            {
                await CallFormule();
                //await CallPrix();
            }
        }
    }
}
