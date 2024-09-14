using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Inscription : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Inscription()
        {
            InitializeComponent();
            //FillInscription();
            temp = 1;
            timer1.Start();
        }

        public int temp;

        private void btnInscription_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                Formulaire_Inscription inscription = new Formulaire_Inscription();
                inscription.ShowDialog();
                if (Formulaire_Inscription.ok == "Oui")
                    CallInsciption();
                Formulaire_Inscription.ok = null;
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

        private async void CallInsciption()
        {
            var result = await FillInscriptionAsync();
            if (result.Rows.Count == 0)
            {
                dataGridView1.Columns.Clear();
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée disponible dans le tableau";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = result;
                dataGridView1.Columns[0].Visible = false;

                DossierColumn delete = new DossierColumn();
                delete.HeaderText = "Détails";
                delete.Name = "Dossier";

                dataGridView1.Columns.Add(delete);
                dataGridView1.Columns["Dossier"].Width = 50;
            }
        }

        private Task<DataTable> FillInscriptionAsync()
        {
            return Task.Factory.StartNew(() => FillInscription());
        }

        public DataTable FillInscription()
        {
            using(var donnée = new QuitayeContext())
            {
                
                var don = (from d in donnée.tbl_inscription
                           where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui"
                          orderby d.Id descending
                          select new
                          {
                              Id = d.Id,
                              N_Matricule = d.N_Matricule,
                              Prenom = d.Prenom,
                              Nom = d.Nom,
                              Date_Naissance = d.Date_Naissance,
                              Genre = d.Genre,
                              Nom_Père = d.Nom_Père,
                              Nom_Mère = d.Nom_Mère,
                              Classe = d.Classe,
                              Date_Inscription = d.Date_Inscription,
                          }).Take(20);
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("N° Matricule");
                dt.Columns.Add("Prenom");
                dt.Columns.Add("Nom");
                dt.Columns.Add("Date_Naissance");
                dt.Columns.Add("Genre");
                dt.Columns.Add("Nom_Père");
                dt.Columns.Add("Nom_Mère");
                dt.Columns.Add("Classe");
                dt.Columns.Add("Date_Inscription");

                foreach (var item in don)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.N_Matricule;
                    dr[2] = item.Prenom;
                    dr[3] = item.Nom;
                    dr[4] = item.Date_Naissance;
                    dr[5] = item.Genre;
                    dr[6] = item.Nom_Père;
                    dr[7] = item.Nom_Mère;
                    dr[8] = item.Classe;
                    dr[9] = item.Date_Inscription;

                    dt.Rows.Add(dr);
                }

                return dt;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 10)
            {
                this.Cursor = Cursors.WaitCursor;
                using (var donnée = new QuitayeContext())
                {
                    string id = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    var eleve = (from d in donnée.tbl_inscription where d.N_Matricule == id select d).First();

                    var clas = (from d in donnée.tbl_classe where d.Nom == eleve.Classe select d).First();
                    Détails_Elèves détails_Elèves = new Détails_Elèves();
                    détails_Elèves.lblTitre.Text = "Détails " + eleve.Nom_Complet;
                    détails_Elèves.matricule = id;
                    détails_Elèves.prenom = eleve.Prenom;
                    détails_Elèves.nom = eleve.Nom;
                    
                    détails_Elèves.classes = eleve.Classe;
                    détails_Elèves.genre = eleve.Genre;
                    détails_Elèves.cycle = clas.Cycle;
                    this.Cursor = Cursors.Default;
                    détails_Elèves.ShowDialog();
                    if (détails_Elèves.ok == "Oui")
                        CallInsciption();
                }
                //this.Cursor = Cursors.Default;

            }
            else if(e.ColumnIndex == 8)
            {
                this.Cursor = Cursors.WaitCursor;
                using (var donnée = new QuitayeContext())
                {
                    var cla = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                    var clas = (from d in donnée.tbl_classe where d.Nom == cla select d).First();
                    Détails_Classe détails_Elèves = new Détails_Classe();
                    détails_Elèves.lblTitre.Text = "Détails " + clas.Nom;
                    détails_Elèves.cycle = clas.Cycle;
                    Détails_Classe.classes = clas.Nom;
                    détails_Elèves.classsss = clas.Nom;
                    this.Cursor = Cursors.Default;
                    détails_Elèves.ShowDialog();
                }
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(temp == 1)
            {
                timer1.Stop();
                CallInsciption();
                temp = 0;
            }
        }
    }

    public class DeleteCell : DataGridViewButtonCell
    {
        Image del = Properties.Resources.icons8_delete_sign_50px;
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {

            cellStyle.BackColor = Color.Transparent;
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            graphics.DrawImage(del, cellBounds);
        }
    }
    public class DeleteColumn : DataGridViewButtonColumn
    {
        public DeleteColumn()
        {
            this.CellTemplate = new DeleteCell();
            this.DefaultCellStyle.BackColor = Color.Transparent;

            this.Width = 20;
            //set other options here 
        }
    }

    public class EditCell : DataGridViewButtonCell
    {
        Bunifu.Framework.UI.BunifuFlatButton button = new Bunifu.Framework.UI.BunifuFlatButton();
        Image del = Properties.Resources.icons8_edit_50px_1;
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            cellStyle.BackColor = Color.Transparent;

            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            graphics.DrawImage(del, cellBounds);
        }
    }

    public class EditColumn : DataGridViewButtonColumn
    {
        public EditColumn()
        {
            this.CellTemplate = new EditCell();
            //this.FlatStyle = FlatStyle.Flat;
            this.DefaultCellStyle.BackColor = Color.FromArgb(188, 21, 234);
            //this.DefaultCellStyle.ForeColor = Color.FromArgb(188, 21, 234);
            //this.DefaultCellStyle.ApplyStyle();

            this.Width = 20;
            //set other options here 
        }
    }
    public class DossierCell : DataGridViewButtonCell
    {
        Image del = Properties.Resources.icons8_dossier_50_1_;
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {

            cellStyle.BackColor = Color.Transparent;
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            graphics.DrawImage(del, cellBounds);
        }
    }
    public class DossierColumn : DataGridViewButtonColumn
    {
        public DossierColumn()
        {
            this.CellTemplate = new DossierCell();
            this.DefaultCellStyle.BackColor = Color.Transparent;

            this.Width = 20;
            //set other options here 
        }
    }
}
