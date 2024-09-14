using Quitaye_School.Models.Context;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Fournisseur : Form
    {
        string mycontrng = LogIn.mycontrng;
        public string Ok { get; set; }
        public Fournisseur()
        {
            InitializeComponent();
            timer1.Start();
            int width = SystemInformation.VirtualScreen.Width;
            int height = SystemInformation.VirtualScreen.Height;

            if (width < 1024)
            {
                Width = 1000;
                Height = 620;
            }
            else if (width > 1300)
            {
                if (Width == 1100)
                {

                }
                else
                {
                    Width = 1345;
                    Height = 680;
                }
            }
        }

        public int id;

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnAjouter_Click(object sender, EventArgs e)
        {
            if(txtAdresse.Text != "" && txtNom.Text != "" && txtTelephone.Text != "" && cbxGenre.Text != "")
            {
                if(btnAjouter.Text == "Ajouter")
                {
                    nom = txtNom.Text;
                    adresse = txtAdresse.Text;
                    telephone = txtTelephone.Text;
                    genre = cbxGenre.Text;
                    if (insert)
                    {
                        await InsertDataAsync();
                        ClearData();
                        CallData();
                    }
                }
                else
                {
                    nom = txtNom.Text;
                    adresse = txtAdresse.Text;
                    telephone = txtTelephone.Text;
                    genre = cbxGenre.Text;
                    await EditDataAsync(id);
                    btnAjouter.Text = "Ajouter";
                    btnAjouter.IconChar = FontAwesome.Sharp.IconChar.Plus;
                    ClearData();
                    CallData();
                }
            }
        }

        static string nom, adresse, genre, telephone;

        private static Task SetDataAsync(int id)
        {
            return Task.Factory.StartNew(() => SetData(id));
        }
        public static void SetData(int id)
        {
            using(var donnée = new QuitayeContext())
            {
                var s = (from d in donnée.tbl_fournisseurs where d.Id == id select d).First();
                nom = s.Nom;
                adresse = s.Adresse;
                telephone = s.Contact;
                genre = s.Genre;
            }
        }


        public Task DeleteDataAsync(int id)
        {
            return Task.Factory.StartNew(() => DeleteData(id));
        }
        public async void DeleteData(int id)
        {
            using (var donnée = new QuitayeContext())
            {
                var der = from d in donnée.tbl_fournisseurs where d.Id == id select d;
                if(der.Count() != 0)
                {
                    var don = (from d in donnée.tbl_fournisseurs where d.Id == id select d).First();
                    donnée.tbl_fournisseurs.Add(don);
                    await donnée.SaveChangesAsync();
                    ok = "Oui";
                    Ok = "Oui";
                }
            }
        }

        public static string ok;

        private Task EditDataAsync(int id)
        {
            return Task.Factory.StartNew(() => EditData(id));
        }
        public  async void EditData(int id)
        {
            using (var donnée = new QuitayeContext())
            {
                var don = from d in donnée.tbl_fournisseurs where d.Id == id select d;
                
                if(don.Count() != 0)
                {
                    var des = (from d in donnée.tbl_fournisseurs where d.Id == id select d).First();
                    des.Adresse = adresse;
                    des.Contact = telephone;
                    des.Genre = genre;
                    des.Nom = nom;
                    await donnée.SaveChangesAsync();
                    Ok = "Oui";
                    ok = "Oui";
                }
            }
        }

        private Task InsertDataAsync()
        {
            return Task.Factory.StartNew(() => InsertData());
        }
        private bool insert = true;
        public async void InsertData()
        {
            insert = false;
            using(var donnée = new QuitayeContext())
            {
                var fr = from d in donnée.tbl_fournisseurs orderby d.Id descending select d;
                int id = 0;
                if(fr.Count() != 0)
                {
                    var fre = (from d in donnée.tbl_fournisseurs orderby d.Id descending select d).First();
                    id = Convert.ToInt32(fre.Id);
                }
                var don = new tbl_fournisseurs();
                don.Id = id + 1;
                don.Adresse = adresse;
                don.Auteur = Principales.profile;
                don.Contact = telephone;
                don.Date_Ajout = DateTime.Now;
                don.Genre = genre;
                don.Nom = nom;
                donnée.tbl_fournisseurs.Add(don);
                await donnée.SaveChangesAsync();

                insert = true;
                ok = "Oui";
                Ok = "Oui";
            }
        }

        public void ClearData()
        {
            txtNom.Clear();
            txtTelephone.Clear();
            cbxGenre.Text = null;
            txtAdresse.Clear();
        }

        async void CallData()
        {
            var result = await SelectDataAsync();
            dataGridView1.Columns.Clear();
            if (result.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau !";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
            }
            else
            {
                dataGridView1.DataSource = result;
                EditColumn edit = new EditColumn();
                edit.HeaderText = "Edit";
                edit.Name = "Edit";

                DeleteColumn delete = new DeleteColumn();
                delete.HeaderText = "Supp";
                delete.Name = "Supp";

                dataGridView1.Columns.Add(edit);
                dataGridView1.Columns.Add(delete);
                dataGridView1.Columns["Edit"].Width = 20;
                dataGridView1.Columns["Supp"].Width = 30;
            }
        }
        private Task<DataTable> SelectDataAsync()
        {
            return Task.Factory.StartNew(() => SelectData());
        }
        public DataTable SelectData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Fournisseur");
            dt.Columns.Add("Genre");
            dt.Columns.Add("Contact");
            dt.Columns.Add("Adresse");
            dt.Columns.Add("Date_ajout", typeof(DateTime));
            dt.Columns.Add("Auteur");
            using(var donnée = new QuitayeContext())
            {
                var don = from d in donnée.tbl_fournisseurs orderby d.Id descending select d;
                foreach (var item in don)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Nom;
                    dr[2] = item.Genre;
                    dr[3] = item.Contact;
                    dr[4] = item.Adresse;
                    dr[5] = item.Date_Ajout;
                    dr[6] = item.Auteur;

                    dt.Rows.Add(dr);
                }

                return dt;
            }
        }

        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 7)
            {
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                await SetDataAsync(id);
                txtNom.Text = nom;
                txtAdresse.Text = adresse;
                txtTelephone.Text = telephone;
                cbxGenre.Text = genre;
                btnAjouter.Text = "Modifier";
                btnAjouter.IconChar = FontAwesome.Sharp.IconChar.Edit;
            }else if(e.ColumnIndex == 8)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                MsgBox msg = new MsgBox();
                msg.show("Voulez-vous supprimé cet élément ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                msg.ShowDialog();
                if (msg.clicked == "Non")
                    return;
                else if(msg.clicked == "Oui")
                {
                   await DeleteDataAsync(id);
                   ClearData();
                   CallData();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            CallData();
        }
    }
}
