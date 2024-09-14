using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Paramettre_Staff : Form
    {
        public Paramettre_Staff()
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
            FillDataRespon();
        }

        Timer timer = new Timer();

        private void btnClasse_Click(object sender, EventArgs e)
        {
            Ajouter_Role role = new Ajouter_Role();
            role.ShowDialog();
            if(role.ok == "Oui")
            {
                FillDataRespon();
            }
        }

        private void FillDataRespon()
        {
            using (var donnée = new QuitayeContext())
            {
                try
                {
                    var s = (from d in donnée.tbl_responsabilité
                             orderby d.Id descending
                             select new
                             {
                                 Id = d.Id,
                                 Nom = d.Responsabilité,
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
                catch (Exception ex)
                {
                    var w32ex = ex as SqlException;
                    if (w32ex == null)
                    {
                        w32ex = ex.InnerException as SqlException;
                        int code = w32ex.ErrorCode;
                    }
                    if (w32ex != null)
                    {
                        int code = w32ex.ErrorCode;
                        // do stuff

                        if (code == -2146232060)
                        {
                            MsgBox msg = new MsgBox();
                            msg.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                            msg.ShowDialog();
                        }
                    }
                }
            }
        }
    }
}
