using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using Quitaye_Medical.Models;
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
    public partial class Les_Utilisateurs : Form
    {
        public Les_Utilisateurs()
        {
            InitializeComponent();
            temp = 1;
            timer1.Start();
            int width = SystemInformation.VirtualScreen.Width;
            int height = SystemInformation.VirtualScreen.Height;
            btnUser.Click += btnUser_Click;
            btnRedo.Click += btnRedo_Click;
            btnFermer.Click += btnFermer_Click;
            btnAddFiliale.Click += BtnAddFiliale_Click;
            dataGridView1.CellClick += dataGridView1_CellClick;
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

            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
        }

        
        private async void BtnAddFiliale_Click(object sender, EventArgs e)
        {
            Ajout_Filiale ajout = new Ajout_Filiale();
            int num = (int)ajout.ShowDialog();
            if (!(ajout.ok == "Oui"))
            {
                ajout = null;
            }
            else
            {
                await CallFiliale();
                ajout = null;
            }
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallTask();
        }

        private async Task CallTask()
        {
            var filiale = FillFilialeAsync();
            var data = FillDataAsync();

            var taskList = new List<Task> { filiale, data };
            while(taskList.Count != 0)
            {
                var finish = await Task.WhenAny(taskList);
                
                if(finish == filiale)
                {
                    cbxfiliale.DataSource = filiale.Result;
                    cbxfiliale.DisplayMember = "Nom";
                    cbxfiliale.ValueMember = "Id";
                    cbxfiliale.Text = null;

                }else if(finish == data)
                {
                    dataGridView1.Columns.Clear();
                    if (data.Result.Rows.Count == 0)
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
                        dataGridView1.DataSource = data.Result;
                        try
                        {
                            AddColumns.Addcolumn(dataGridView1);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                taskList.Remove(finish);
            }
        }

        string mycontrng = LogIn.mycontrng;
        protected override void WndProc(ref Message m)
        {
            
            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg)
            {
                case 0x0084/*NCHITTEST*/ :
                    base.WndProc(ref m);

                if ((int)m.Result == 0x01/*HTCLIENT*/)
                {
                    Point screenPoint = new Point(m.LParam.ToInt32());
                    Point clientPoint = PointToClient(screenPoint);
                    if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                    {
                        if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                            m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                        else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                            m.Result = (IntPtr)12/*HTTOP*/ ;
                        else
                            m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                    }
                    else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                    {
                        if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                            m.Result = (IntPtr)10/*HTLEFT*/ ;
                        else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                            m.Result = (IntPtr)2/*HTCAPTION*/ ;
                        else
                            m.Result = (IntPtr)11/*HTRIGHT*/ ;
                    }
                    else
                    {
                        if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                            m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                        else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                            m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                        else
                            m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                    }
                }
                return;
            }
            base.WndProc(ref m);
        }

        public string genre;
        public int id;

        public int temp;
        private async void btnUser_Click(object sender, EventArgs e)
        {
            if (LogIn.expiré == false)
            {
                if (btnUser.IconChar == FontAwesome.Sharp.IconChar.User)
                {
                    if (txtUser.Text != "" && txtPassword.Text != "" && txtAdresse.Text != "" && txtContact.Text != "" && txtNom.Text != "" && txtPrenom.Text != "" && cbxGenre.Text != "" && cbxDepartement.Text != "" && cbxType.Text != "")
                    {
                        await SaveData();
                        await CallData();
                        ClearData();
                    }
                }
                else
                {
                    if (txtAdresse.Text != "" && txtContact.Text != "" && txtNom.Text != "" && txtPrenom.Text != "" && cbxGenre.Text != "" && cbxDepartement.Text != "" && cbxType.Text != "" && cbxRole.Text != "")
                    {
                        await SaveData();
                        await CallData();
                        ClearData();
                    }
                    btnUser.IconChar = FontAwesome.Sharp.IconChar.User;
                    btnUser.Text = "Ajouter";
                }
            }
        }

        Timer loadTimer = new Timer();
        private async Task CallFiliale()
        {
            var result = await FillFilialeAsync();
            cbxfiliale.DataSource = result;
            cbxfiliale.DisplayMember = "Nom";
            cbxfiliale.ValueMember = "Id";
            cbxfiliale.Text = null;
        }
        private Task<DataTable> FillFilialeAsync()
        {
            return Task.Factory.StartNew(() => FillFiliale());
        }
        private DataTable FillFiliale()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Nom");

            using(var donnée = new QuitayeContext())
            {
                var sed = from d in donnée.tbl_filiale
                          orderby d.Nom select new { Id = d.Id, Nom = d.Nom };
                foreach (var item in sed)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Nom;

                    dt.Rows.Add(dr);
                }

                return dt;
            }
        }

        private async Task CallData()
        {
            var result = await FillDataAsync();
            dataGridView1.Columns.Clear();
            if(result.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau !";
                dt.Rows.Add(dr);

                dataGridView1.DataSource = dt;
            }else
            {
                dataGridView1.DataSource = result;
                try
                {
                    AddColumns.Addcolumn(dataGridView1);
                }
                catch (Exception)
                {

                }
            }
        }
        private Task<DataTable> FillDataAsync()
        {
            return Task.Factory.StartNew(() => FillData());
        }
        public DataTable FillData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Prenom");
            dt.Columns.Add("Nom");
            dt.Columns.Add("Genre");
            dt.Columns.Add("Adresse");
            dt.Columns.Add("Email");
            dt.Columns.Add("Type_Compte");
            dt.Columns.Add("Date_Ajout");
            dt.Columns.Add("Auteur");
            using (var donnée = new QuitayeContext())
            {
                var don = from d in donnée.tbl_Users
                          select new
                          {
                              Id = d.Id,
                              Prenom = d.Prenom,
                              Nom = d.Nom,
                              Genre = d.Genre,
                              Adresse = d.Adresse,
                              Contact = d.Contact,
                              Email = d.Email,
                              Type_Compte = d.Type_Compte,
                              Date_Ajout = d.Date_Ajout,
                              Auteur = d.Auteur,
                          };
                foreach (var item in don)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Id;
                    dr[1] = item.Prenom;
                    dr[2] = item.Nom;
                    dr[3] = item.Genre;
                    dr[4] = item.Adresse;
                    dr[5] = item.Email;
                    dr[6] = item.Type_Compte;
                    dr[7] = item.Date_Ajout;
                    dr[8] = item.Auteur;

                    dt.Rows.Add(dr);

                }

                return dt;
            }
        }

        public void ClearData()
        {
            txtAdresse.Clear();
            txtNom.Clear();
            txtPrenom.Clear();
            txtPassword.Clear();
            txtEmail.Clear();
            txtContact.Clear();
            txtUser.Clear();
            cbxDepartement.Text = null;
            cbxGenre.Text = null;
            cbxType.Text = null;
            cbxRole.Text = null;
            cbxfiliale.Text = null;
        }


        private Task<Users> UsersAsync(int id)
        {
            return Task.Factory.StartNew(() => Users(id));
        }
        private Users Users(int id)
        {
            Users u = new Users();
            using(var donnée = new QuitayeContext())
            {
                var sr = (from d in donnée.tbl_Users where d.Id == id select d).First();
                u.Id = id;
                u.Genre = sr.Genre;
                u.Prenom = sr.Prenom;
                u.Nom = sr.Nom;
                u.Role = sr.Role;
                u.Telephone = sr.Contact;
                u.Type_Compte = sr.Type_Compte;
                u.Departement = sr.Departement;
                u.Genre = sr.Genre;
                u.Date = Convert.ToDateTime(sr.Date_Ajout.Value);
                u.Email = sr.Email;
                u.Filiale = sr.Filiale;
                u.Adresse = sr.Adresse;
            }
            return u;
        }
        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Edit"))
            {
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                var s = await UsersAsync(id);
                txtAdresse.Text = s.Adresse;
                txtPrenom.Text = s.Prenom;
                txtNom.Text = s.Nom;
                txtEmail.Text = s.Email;
                txtContact.Text = s.Telephone;
                txtPassword.Text = s.Password;
                cbxGenre.Text = s.Genre;
                lblDate.Text = s.Date.ToString();
                cbxType.Text = s.Type_Compte;
                cbxRole.Text = s.Role;
                cbxDepartement.Text = s.Departement;
                txtUser.Text = s.UserName;
                cbxfiliale.Text = s.Filiale;
                btnUser.Text = "Modifier";
                btnUser.IconChar = FontAwesome.Sharp.IconChar.Edit;
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Sup"))
            {
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                MsgBox msg = new MsgBox();
                msg.show("Voulez-vous désactiver cet utilisateur ?", "Désactivation", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Question);
                msg.ShowDialog();
                if (msg.clicked == "Non")
                    return;
                else if (msg.clicked == "Oui")
                {
                    using (var donnée = new QuitayeContext())
                    {
                        var users = donnée.tbl_Users.SingleOrDefault(x => x.Id == id);
                        users.Active = "Non";
                        await donnée.SaveChangesAsync();
                        Alert.SShow("Utilisateur désactivé avec succès.", Alert.AlertType.Sucess);
                    }
                }
            }
        }

        private async Task SaveData()
        {
            Users u = new Users();
            u.Genre = cbxGenre.Text;
            u.Telephone = txtContact.Text;
            u.Role = cbxDepartement.Text;
            u.UserName = txtUser.Text;
            u.Password = txtPassword.Text;
            u.Departement = cbxDepartement.Text;
            u.Role = cbxRole.Text;
            u.Prenom = txtPrenom.Text;
            u.Nom = txtNom.Text;
            u.Filiale = cbxfiliale.Text;
            u.Adresse = txtAdresse.Text;
            u.Email = txtEmail.Text;
            u.Type_Compte = cbxType.Text;
            if (btnUser.IconChar == FontAwesome.Sharp.IconChar.User)
            {
                var save = await SaveUserAsync(u, true);
            }
            else
            {
                var save = await SaveUserAsync(u, false);
            }
        }

        
        private async Task<bool> SaveUserAsync(Users user, bool add)
        {
            using(var donnée = new QuitayeContext())
            {
                if (add)
                {
                    int id = 1;
                    var seds = (from d in donnée.tbl_Users orderby d.Id descending select new { Id = d.Id }).Take(1);
                    if (seds.Count() != 0)
                    {
                        id = seds.First().Id + 1;
                    }

                    var u = new Models.Context.tbl_Users();
                    u.Id = id;
                    u.Genre = user.Genre;
                    u.Email = user.Email;
                    u.Prenom = user.Prenom;
                    u.Nom = user.Nom;
                    u.Adresse = user.Adresse;
                    u.Password = user.Password;
                    u.Username = user.UserName;
                    u.Type_Compte = user.Type_Compte;
                    u.Filiale = user.Filiale;
                    u.Departement = user.Departement;
                    u.Contact = user.Telephone;
                    u.Active = "Oui";
                    u.Role = user.Role;
                    u.Auteur = Principales.profile;
                    u.Date_Ajout = DateTime.Now;
                    u.Nom_Complet = user.Prenom + " " + user.Nom;
                    donnée.tbl_Users.Add(u);
                }else
                {
                    var u = (from d in donnée.tbl_Users where d.Id == id select d).First();
                    u.Genre = user.Genre;
                    u.Email = user.Email;
                    u.Prenom = user.Prenom;
                    u.Nom = user.Nom;
                    u.Adresse = user.Adresse;
                    u.Type_Compte = user.Type_Compte;
                    u.Filiale = user.Filiale;
                    
                    u.Departement = user.Departement;
                    u.Contact = user.Telephone;
                    u.Role = user.Role;
                    u.Nom_Complet = user.Prenom + " " + user.Nom;
                }
                await donnée.SaveChangesAsync();
                return true;
            }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (temp == 1)
            {
                await CallData();
                temp = 0;
                timer1.Stop();
            }
        }

        bool enable = true;
        private void btnFermer_Click(object sender, EventArgs e)
        {
            if(enable == true)
            Close();
        }

        

        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (btnUser.Text != "Ajouter Utilisateur")
            {
                btnUser.IconChar = FontAwesome.Sharp.IconChar.User;
                btnUser.Text = "Ajouter Utilisateur";
                txtAdresse.Clear();
                txtContact.Clear();
                txtEmail.Clear();
                txtNom.Clear();
                txtPassword.Clear();
                txtPrenom.Clear();
                txtUser.Clear();
                cbxGenre.Text = null;
                cbxDepartement.Text = null;
                cbxType.Text = null;
            }
        }
    }
}
