using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
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
using System.Xml.Linq;

namespace Quitaye_School.User_Interface
{
    public partial class Inscription_Entreprise : Form
    {
        public static string mycontrng = GetConnectionsStrings.GetSConnectionString();
        public Inscription_Entreprise()
        {
            InitializeComponent();

        }

        private static string db;
        private static string hosts;
        private static string usernames;
        private static string passwords;


        private void btnConnecter_Click(object sender, EventArgs e)
        {
            panelRegister.Visible = false;
            panelConnection.Visible = true;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            panelConnection.Visible = false;
            panelRegister.Visible = true;
        }

        public static Server InitializeServer(string host, string username, string password)
        {
            // Setup a new connection to the data server
            ServerConnection connection = new
                                 ServerConnection(host);
            // Log in using SQL authentication
            connection.LoginSecure = false;
            connection.Login = username;
            connection.Password = password;
            Server sqlServer = new Server(connection);
            return sqlServer;
        }

        private void Checktbl_client_quitaye()
        {
            DoesTableExist("tbl_client_quitaye", mycontrng);
            if (count > 0)
                return;
            else Createtbl_client_quitaye();
            count = 0;
        }

        private void Createtbl_client_quitaye()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {
                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_client_quitaye]
                                    (
	                                    [Id] INT NOT NULL PRIMARY KEY, 
                                        [Entreprise] NVARCHAR(150) NULL, 
                                        [Email] NVARCHAR(150) NULL, 
                                        [Password] VARCHAR(50) NULL, 
                                        [InstanceDb] NVARCHAR(350) NULL, 
	                                    [InstanceUser] NVARCHAR(50) NULL,
                                        [InstancePswd] NVARCHAR(50) NULL, 
                                        [Date_Inscription] DATETIME NULL, 
                                        [Date_Expiration] DATETIME NULL, 
                                        [Type_Entreprise] NVARCHAR(50) NULL, 
                                        [Logiciel] VARCHAR(50) NULL, 
                                        [Type_Abonnement] VARCHAR(50) NULL, 
                                        [Referant] NVARCHAR(150) NULL,
                                        [Last_Login] DATETIME NULL, 
                                    );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtEmailEntre.Text != "")
            {
                this.Cursor = Cursors.AppStarting;
                //CreateDb("Gestion Client", GetConnectionsStrings.GetSHostName(), GetConnectionsStrings.GetSUsername(), GetConnectionsStrings.GetSPassword());
                //CreateDb(txtEmailEntre.Text, GetConnectionsStrings.GetHostName(), GetConnectionsStrings.GetUsername(), GetConnectionsStrings.GetPassword());
                //CreateLogin("admin", "admin", name);
                //MapUser("admin", "admin");

                mycontrng = GetConnectionsStrings.GetSConnectionString();
                var donnée = new DonnéeEcoleDataContext();
                
                Task task = new Task(Checktbl_client_quitaye);
                task.Start();
                await task;

                var inst = (from d in donnée.tbl_instances orderby d.Id descending select d).First();
                var se = from d in donnée.tbl_client_quitaye orderby d.Id descending select d;

                var rer = from d in donnée.tbl_client_quitaye where d.Email == txtEmailEntre.Text select d;
                if(rer.Count() == 0)
                {
                    int id = 1;
                    if (se.Count() != 0)
                    {
                        var ss = (from d in donnée.tbl_client_quitaye orderby d.Id descending select d).First();
                        id = ss.Id + 1;
                    }
                    var client = new tbl_client_quitaye();
                    client.Id = id;
                    client.Entreprise = txtNomEntreprise.Text;
                    client.Email = txtEmailEntre.Text;
                    client.Entreprise = txtNomEntreprise.Text;
                    client.Logiciel = "Quitaye School";
                    client.Password = txtPassword.Text;
                    client.InstancePswd = inst.Password;
                    client.InstanceUser = inst.User;
                    client.InstanceDb = inst.Instance;
                    client.Date_Expiration = DateTime.Today.AddDays(30);
                    client.Date_Inscription = DateTime.Now;
                    client.Last_Login = DateTime.Now;
                    donnée.tbl_client_quitaye.InsertOnSubmit(client);
                    inst.Nombre_Db += 1;
                    donnée.SubmitChanges();
                }
                
                mycontrng = "Data Source=" + inst.Instance + ";Initial Catalog=" + txtEmailEntre.Text + ";User ID=" + inst.User + ";Password=" + inst.Password;
                CreateDb(txtEmailEntre.Text, inst.Instance, inst.User, inst.Password);
                Task task1 = new Task(CheckTables);
                task1.Start();
                await task1;

                donnée = new DonnéeEcoleDataContext();
                XDocument doc = XDocument.Load(Environment.CurrentDirectory + "\\Entreprise.xml");

                var data = (from s in doc.Descendants("Entreprise")
                            where s.Element("Id").Value == "1"
                            select s).First();
                data.Element("Email").Value = txtEmailEntre.Text;
                doc.Save(Environment.CurrentDirectory + "\\Entreprise.xml");


                var entreprise = new tbl_entreprise();

                entreprise.Email = txtEmailEntre.Text;
                entreprise.Nom = txtNomEntreprise.Text;
                donnée.tbl_entreprise.InsertOnSubmit(entreprise);
                donnée.SubmitChanges();
                this.Cursor = Cursors.Default;
                this.Hide();
                Program.RestartLogInInternet();
            }
        }

        protected override CreateParams CreateParams
        {
            //get
            //{
            //    CreateParams cp = base.CreateParams;
            //    cp.Style |= 0x20000; // <--- use 0x20000
            //    return cp;
            //}
            get
            {
                const int WS_MINIMIZEBOX = 0x00020000;
                var cp = base.CreateParams;
                cp.Style |= WS_MINIMIZEBOX;
                return cp;
            }
        }

        private void Create()
        {
            try
            {
                Server sqlServer = InitializeServer(hosts, usernames, passwords);
                if (db != "Gestion Client")
                {
                    if (sqlServer.Databases[db] == null)
                    {

                        Database dbs = new Database(sqlServer, db);
                        dbs.Create();
                    }
                    else
                    {
                        //Alert.SShow("Ce nom existe déjà!!", Alert.AlertType.Warning);
                    }
                }
                else
                {
                    if (sqlServer.Databases[db] == null)
                    {
                        Database dbs = new Database(sqlServer, db);
                        dbs.Create();
                        Alert.SShow("Enregistrement effectué avec succès.", Alert.AlertType.Sucess);
                    }
                }

            }
            catch (Exception ex)
            {
                MsgBox msg = new MsgBox();
                msg.show("Operation non finalisée \n" + ex.Message, "Error", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                msg.ShowDialog();
                //Alert.SShow(, Alert.AlertType.Error);
            }
        }

        public async void CreateDb(string databaseName, string host, string username, string pass)
        {
            db = databaseName;
            hosts = host;
            usernames = username;
            passwords = pass;


            Task task = new Task(Create);
            task.Start();
            Alert.SShow("Configuration en cours... Veillez patientez !!", Alert.AlertType.Info);
            await task;
            //Alert.SShow("Opération terminée. Lancement en cours..", Alert.AlertType.Sucess);

        }


        public void CreateDbs(string databaseName, string host, string username, string pass)
        {
            try
            {
                Server sqlServer = InitializeServer(host, username, pass);
                if (sqlServer.Databases[databaseName] == null)
                {
                    Database db = new Database(sqlServer, databaseName);
                    db.Create();
                }

            }
            catch (Exception ex)
            {
                MsgBox msg = new MsgBox();
                msg.show("Operation non finalisée \n" + ex.Message, "Error", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                msg.ShowDialog();
                //Alert.SShow(, Alert.AlertType.Error);
            }
        }
        static int count = 0;
        private static bool DoesTableExist(string name, string connection)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                DataTable dt = conn.GetSchema("TABLES", new string[] { null, null, name });
                count = dt.Rows.Count;
                return dt.Rows.Count > 0;
            }
        }

        private void CheckTables()
        {
            Checktbl_annéescolaire();
            Checktbl_notifier_absence();
            Checktbl_classe();
            Checktbl_compte_comptable();
            Checktbl_enseignant();
            Checktbl_entreprise();
            Checktbl_examen();
            Checktbl_formulaire_inscription();
            Checktbl_list_journaux();
            Checktbl_inscription();
            Checktbl_journal_comptable();
            Checktbl_matiere();
            Checktbl_note();
            Checktbl_payement();
            Checktbl_Users();
            Checktbl_responsabilité();
            Checktbl_staff();
            Checktbl_parent_elèves();
            Checktbl_tarif_accessoire();
        }

        #region Check Area
        private void Checktbl_inscription()
        {
            DoesTableExist("tbl_inscription", mycontrng);
            if (count > 0)
                return;
            else Createtbl_inscription();
            count = 0;
        }

        private void Checktbl_staff()
        {
            DoesTableExist("tbl_staff", mycontrng);
            if (count > 0)
                return;
            else Createtbl_staff();
            count = 0;
        }

        private void Checktbl_parent_elèves()
        {
            DoesTableExist("tbl_parent_elèves", mycontrng);
            if (count > 0)
                return;
            else Createtbl_parent_elèves();
            count = 0;
        }

        private void Createtbl_parent_elèves()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_parent_elèves]
                                    (
	                                    [Id] INT NOT NULL PRIMARY KEY, 
                                        [Prenom] NVARCHAR(100) NULL, 
                                        [Nom] NVARCHAR(100) NULL, 
                                        [Genre] NVARCHAR(50) NULL, 
                                        [Pays] NVARCHAR(50) NULL, 
                                        [Contact] NVARCHAR(50) NULL, 
                                        [Adresse] NVARCHAR(150) NULL, 
                                        [Email] NVARCHAR(150) NULL, 
                                        [Ville] NVARCHAR(50) NULL, 
                                        [Date_Ajout] DATETIME NULL, 
                                        [Auteur] NVARCHAR(150) NULL, 
                                        [Nom_Contact] NVARCHAR(150) NULL
                                    );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Checktbl_tarif_accessoire()
        {
            DoesTableExist("tbl_tarif_accessoire", mycontrng);
            if (count > 0)
                return;
            else Createtbl_tarif_accessoire();
            count = 0;
        }

        private void Createtbl_tarif_accessoire()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_tarif_accessoire]
                                (
	                                [Id] INT NOT NULL PRIMARY KEY, 
                                    [Nom] NVARCHAR(50) NULL, 
                                    [Tarif_Annuel] DECIMAL NULL, 
                                    [Tarif_Mensuel] DECIMAL NULL, 
                                    [Tarif_Journalier] DECIMAL NULL
                                );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }


        private void Checktbl_responsabilité()
        {
            DoesTableExist("tbl_responsabilité", mycontrng);
            if (count > 0)
                return;
            else Createtbl_responsabilité();
            count = 0;
        }
        

        private void Checktbl_journal_comptable()
        {
            DoesTableExist("tbl_journal_comptable", mycontrng);
            if (count > 0)
                return;
            else Createtbl_journal_comptable();
            count = 0;
        }

        private void Checktbl_payement()
        {
            DoesTableExist("tbl_payement", mycontrng);
            if (count > 0)
                return;
            else Createtbl_payement();
            count = 0;
        }

        private void Checktbl_Users()
        {
            DoesTableExist("tbl_Users", mycontrng);
            if (count > 0)
                return;
            else Createtbl_Users();
            count = 0;
        }

        private void Checktbl_note()
        {
            DoesTableExist("tbl_note", mycontrng);
            if (count > 0)
                return;
            else Createtbl_note();
            count = 0;
        }

        private void Checktbl_examen()
        {
            DoesTableExist("tbl_examen", mycontrng);
            if (count > 0)
                return;
            else Createtbl_examen();
            count = 0;
        }

        private void Checktbl_compte_comptable()
        {
            DoesTableExist("tbl_Compte_Comptable", mycontrng);
            if (count > 0)
                return;
            else Createtbl_compte_comptable();
            count = 0;
        }

        private void Checktbl_classe()
        {
            DoesTableExist("tbl_classe", mycontrng);
            if (count > 0)
                return;
            else Createtbl_classe();
            count = 0;
        }

        private void Checktbl_formulaire_inscription()
        {
            DoesTableExist("tbl_formule_inscription", mycontrng);
            if (count > 0)
                return;
            else Createtbl_formulaire_inscription();
            count = 0;
        }

        private void Checktbl_annéescolaire()
        {
            DoesTableExist("tbl_année_scolaire", mycontrng);
            if (count > 0)
                return;
            else Createtbl_annéescolaire();
            count = 0;
        }
        private void Checktbl_enseignant()
        {
            DoesTableExist("tbl_enseignant", mycontrng);
            if (count > 0)
                return;
            else Createtbl_enseignant();
            count = 0;
        }

        private void Checktbl_notifier_absence()
        {
            DoesTableExist("tbl_notifier_absence", mycontrng);
            if (count > 0)
                return;
            else Createtbl_notifier_Absence();
            count = 0;
        }

        private void Createtbl_notifier_Absence()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_notifier_absence] (
                                    [Id]             INT           NOT NULL,
                                    [Personne]       NVARCHAR(150)  NULL,
                                    [Genre]          NVARCHAR(50)  NULL,
                                    [N_Matricule]    NVARCHAR(50)  NULL,
                                    [Date_Ajout]     DATETIME      NULL,
                                    [Date_Absence]   DATETIME      NULL,
                                    [Auteur]         NVARCHAR(150) NULL,
                                    [Commentaire]    NVARCHAR(150) NULL,
                                    [Année_Scolaire] NVARCHAR(50)  NULL,
                                    [Classe]         NVARCHAR(50)  NULL,
                                    [Cycle]          NVARCHAR(50)  NULL,
                                    [Fichier] VARBINARY(MAX) NULL, 
                                    [Nom_Fichier] NVARCHAR(150) NULL, 
                                    PRIMARY KEY CLUSTERED ([Id] ASC)
                                );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Createtbl_staff()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_staff]
                                    (
	                                    [Id] INT NOT NULL PRIMARY KEY, 
                                        [Nom] NVARCHAR(150) NULL, 
                                        [Genre] NVARCHAR(50) NULL, 
                                        [Adresse] NVARCHAR(50) NULL, 
                                        [Role] NVARCHAR(150) NULL, 
                                        [Contact] NVARCHAR(50) NULL, 
                                        [Auteur] NVARCHAR(150) NULL, 
                                        [Date_Ajout] DATETIME NULL
                                    );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Createtbl_responsabilité()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_responsabilité]
                                (
	                                [Id] INT NOT NULL PRIMARY KEY, 
                                    [Responsabilité] NVARCHAR(150) NULL, 
                                    [Date_Ajout] DATETIME NULL, 
                                    [Auteur] NVARCHAR(50) NULL
                                );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Createtbl_Users()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_Users] (
                                    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
                                    [Prenom]             VARCHAR (50)  NULL,
                                    [Nom]                VARCHAR (50)  NULL,
                                    [Username]           VARCHAR (50)  NULL,
                                    [Password]           VARCHAR (50)  NULL,
                                    [Email]              VARCHAR (50)  NULL,
                                    [Contact]            VARCHAR (50)  NULL,
                                    [Adresse]            VARCHAR (100) NULL,
                                    [Genre]              VARCHAR (20)  NULL,
                                    [Type_Compte]        VARCHAR (50)  NULL,
                                    [Role]               VARCHAR (50)  NULL,
                                    [Date_Ajout]         DATETIME      NULL,
                                    [Auteur]             VARCHAR (50)  NULL,
                                    [Departement]        VARCHAR (50)  NULL,
                                    [Active]             VARCHAR (50)  NULL,
                                    [Classe]             VARCHAR (50)  NULL,
                                    [Auth_Premier_Cycle] VARCHAR (50)  NULL,
                                    [Auth_Second_Cycle]  VARCHAR (50)  NULL,
                                    [Auth_Lycée]         VARCHAR (50)  NULL,
                                    [Auth_Université]    VARCHAR (50)  NULL,
                                    [Auth_Cente_Loisir] VARCHAR(50) NULL, 
                                    [Auth_Crèche] VARCHAR(50) NULL, 
                                    [Auth_Maternelle] VARCHAR(50) NULL, 
                                    PRIMARY KEY CLUSTERED ([Id] ASC)
                                );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Createtbl_payement()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_payement] (
                                    [Id]                  INT             NOT NULL,
                                    [Prenom]              VARCHAR (50)    NULL,
                                    [Nom]                 VARCHAR (50)    NULL,
                                    [Genre]               VARCHAR (50)    NULL,
                                    [N_Matricule]         VARCHAR (50)    NULL,
                                    [Classe]              VARCHAR (50)    NULL,
                                    [Cycle]               VARCHAR (50)    NULL,
                                    [Montant]             DECIMAL (18)    NULL,
                                    [Date_Payement]       DATE            NULL,
                                    [Date_Enregistrement] DATETIME        NULL,
                                    [Auteur]              VARCHAR (150)   NULL,
                                    [Commentaire]         VARCHAR (150)   NULL,
                                    [Année_Scolaire]      VARCHAR (50)    NULL,
                                    [Fichier]             VARBINARY (MAX) NULL,
                                    [Nom_Fichier]         VARCHAR (100)   NULL,
                                    [Type]                VARCHAR (50)    NULL,
                                    [Tranche1]            DECIMAL (18)    NULL,
                                    [Tranche2]            DECIMAL (18)    NULL,
                                    [Tranche3]            DECIMAL (18)    NULL,
                                    [Opération] VARCHAR(50) NULL, 
                                    PRIMARY KEY CLUSTERED ([Id] ASC)
                                );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Createtbl_journal_comptable()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_journal_comptable] (
                                [Id]                  INT             NOT NULL,
                                [Date]                DATE            NULL,
                                [Date_Enregistrement] DATETIME        NULL,
                                [Libelle]             VARCHAR (150)   NULL,
                                [N_Facture]           VARCHAR (150)   NULL,
                                [Compte]              VARCHAR (50)    NULL,
                                [Compte_Tier]         VARCHAR (50)    NULL,
                                [Débit]               DECIMAL (18)    NULL,
                                [Crédit]              DECIMAL (18)    NULL,
                                [Auteur]              VARCHAR (150)   NULL,
                                [Ref_Pièces]          VARCHAR (50)    NULL,
                                [Commentaire]         VARCHAR (150)   NULL,
                                [Nom_Fichier]         VARCHAR (150)   NULL,
                                [Fichier]             VARBINARY (MAX) NULL,
                                [Ref_Payement]        VARCHAR (150)   NULL,
                                [Active]              VARCHAR (50)    NULL,
                                [Validé]              NVARCHAR (50)   NULL,
                                [Type]                NVARCHAR (50)   NULL,
                                PRIMARY KEY CLUSTERED ([Id] ASC)
                            );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Createtbl_note()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_note] (
                                [Id]                  INT           NOT NULL,
                                [Note_Classe]         INT           NULL,
                                [Note_Compo]          INT           NULL,
                                [Coeff]               INT           NULL,
                                [Prenom]              VARCHAR (50)  NULL,
                                [Nom]                 VARCHAR (50)  NULL,
                                [Genre]               VARCHAR (50)  NULL,
                                [Matière]             VARCHAR (50)  NULL,
                                [Classe]              VARCHAR (50)  NULL,
                                [Cycle]               VARCHAR (50)  NULL,
                                [Année_Scolaire]      VARCHAR (50)  NULL,
                                [Examen]              VARCHAR (50)  NULL,
                                [N_Matricule]         VARCHAR (50)  NULL,
                                [Date]                DATE          NULL,
                                [Date_Enregistrement] DATETIME      NULL,
                                [Auteur]              VARCHAR (150) NULL,
                                [Enseignant]          INT           NULL,
                                PRIMARY KEY CLUSTERED ([Id] ASC)
                            );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Createtbl_inscription()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_inscription] (
                                    [Id]                  INT             NOT NULL,
                                    [Prenom]              VARCHAR (50)    NULL,
                                    [Nom]                 VARCHAR (50)    NULL,
                                    [Nom_Complet]         VARCHAR (150)   NULL,
                                    [Nom_Matricule]       VARCHAR (150)   NULL,
                                    [Date_Naissance]      DATE            NULL,
                                    [Genre]               VARCHAR (50)    NULL,
                                    [Type_Scolarité]      VARCHAR (50)    NULL,
                                    [Nom_Père]            VARCHAR (150)   NULL,
                                    [Nom_Mère]            VARCHAR (150)   NULL,
                                    [Contact 1]           VARCHAR (50)    NULL,
                                    [Contact 2]           VARCHAR (50)    NULL,
                                    [Email]               VARCHAR (100)   NULL,
                                    [Adresse]             VARCHAR (150)   NULL,
                                    [Nationalité]         VARCHAR (50)    NULL,
                                    [Classe]              VARCHAR (50)    NULL,
                                    [Année_Scolaire]      VARCHAR (50)    NULL,
	                                [Cantine]			  VARCHAR (50)    NULL,
                                    [Transport]           VARCHAR (50)    NULL,
                                    [Assurance]			  VARCHAR (50)    NULL,
                                    [N_Matricule]         VARCHAR (50)    NOT NULL,
                                    [Date_Inscription]    DATETIME        NULL,
                                    [Auteur]              VARCHAR (150)   NULL,
                                    [Image]               VARBINARY (MAX) NULL,
                                    [Ref_Pièces]          VARCHAR (50)    NULL,
                                    [Cycle]               VARCHAR (50)    NULL,
                                    [Active]              VARCHAR (50)    NULL,
                                    [Scolarité]           DECIMAL (18)    NULL,
                                    [Motif_Desactivation] VARCHAR (50)    NULL,
                                    [Date_Desactivation]  DATETIME        NULL,
                                    PRIMARY KEY CLUSTERED ([N_Matricule] ASC)
                                );


                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Createtbl_formulaire_inscription()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_formule_inscription] (
                                [Id]         INT           IDENTITY (1, 1) NOT NULL,
                                [Formule]    VARCHAR (50)  NULL,
                                [Montant]    DECIMAL (18)  NULL,
                                [Gratuit]    VARCHAR (50)  NULL,
                                [Date_Ajout] DATETIME      NULL,
                                [Auteur]     VARCHAR (150) NULL,
                                [Compte]     INT           NULL,
                                PRIMARY KEY CLUSTERED ([Id] ASC)
                            );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Checktbl_list_journaux()
        {
            DoesTableExist("tbl_list_journaux", mycontrng);
            if (count > 0)
                return;
            else Createtbl_list_journaux();
            count = 0;
        }

        private void Createtbl_list_journaux()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_list_journaux] (
                                    [Id]          INT            NOT NULL,
                                    [Nom]         NVARCHAR (150) NULL,
                                    [Prefix]      NVARCHAR (50)  NULL,
                                    [Type]        NVARCHAR (50)  NULL,
                                    [Description] NVARCHAR (150) NULL,
                                    [Date_Ajout]  DATETIME       NULL,
                                    [Auteur]      NVARCHAR (100) NULL,
                                    [Compte]      NVARCHAR (50)  NULL,
                                    PRIMARY KEY CLUSTERED ([Id] ASC)
                                );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Createtbl_examen()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_examen] (
                                [Id]                  INT           IDENTITY (1, 1) NOT NULL,
                                [Nom]                 VARCHAR (50)  NULL,
                                [Date_Enregistrement] DATETIME      NULL,
                                [Auteur]              VARCHAR (150) NULL,
                                [Cycle]               VARCHAR (50)  NULL,
                                PRIMARY KEY CLUSTERED ([Id] ASC)
                            );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Createtbl_compte_comptable()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_Compte_Comptable] (
                                    [Id]          INT          NOT NULL,
                                    [Compte]      VARCHAR (50)  NOT NULL,
                                    [Catégorie]   VARCHAR (150) NULL,
                                    [Nom_Compte]  VARCHAR (150) NULL,
                                    [Date_Ajout]  DATETIME      NULL,
                                    [Auteur]      VARCHAR (150) NULL,
                                    [Description] VARCHAR (150) NULL,
                                    [Compte_Aux]  NVARCHAR (50) NULL,
                                    [Type]        VARCHAR (50)  NULL,
                                    [Préfix]      NVARCHAR (50) NULL,
                                    PRIMARY KEY CLUSTERED ([Id] ASC)
                                );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Createtbl_classe()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_classe] (
                                    [Id]        INT           IDENTITY (1, 1) NOT NULL,
                                    [Nom]       VARCHAR (50)  NULL,
                                    [Scolarité] DECIMAL (18)  NULL,
                                    [Date]      DATETIME      NULL,
                                    [Auteur]    VARCHAR (150) NULL,
                                    [Cycle]     VARCHAR (50)  NULL,
                                    [Tranche 1] DECIMAL (18)  NULL,
                                    [Tranche 2] DECIMAL (18)  NULL,
                                    [Tranche 3] DECIMAL (18)  NULL,
                                    PRIMARY KEY CLUSTERED ([Id] ASC)
                                );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Createtbl_annéescolaire()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_année_scolaire] (
                                    [Id]     INT          IDENTITY (1, 1) NOT NULL,
                                    [Nom]    VARCHAR (50) NULL,
                                    [Auteur] VARCHAR (50) NULL,
                                    [Date]   DATETIME     NULL,
                                    PRIMARY KEY CLUSTERED ([Id] ASC)
                                );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Createtbl_enseignant()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_enseignant] (
                                    [Id]             INT           IDENTITY (1, 1) NOT NULL,
                                    [Prenom]         VARCHAR (50)  NULL,
                                    [Nom]            VARCHAR (50)  NULL,
                                    [Nom_Complet]    VARCHAR (150) NULL,
                                    [Date_Naissance] DATETIME      NULL,
                                    [Genre]          VARCHAR (50)  NULL,
                                    [Contact1]       VARCHAR (50)  NULL,
                                    [Contact2]       VARCHAR (50)  NULL,
                                    [Email]          VARCHAR (100) NULL,
                                    [Adresse]        VARCHAR (150) NULL,
                                    [Nationalité]    VARCHAR (50)  NULL,
                                    [Date_Ajout]     DATETIME      NULL,
                                    [Auteur]         VARCHAR (100) NULL,
                                    [Image]          IMAGE         NULL,
                                    [Active]         VARCHAR (50)  NULL,
                                    PRIMARY KEY CLUSTERED ([Id] ASC)
                                );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }


        private void Checktbl_matiere()
        {
            DoesTableExist("tbl_matiere", mycontrng);
            if (count > 0)
                return;
            else Createtbl_matiere();
            count = 0;
        }

        private void Createtbl_matiere()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_matiere] (
                                    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
                                    [Nom]                 VARCHAR (50)  NULL,
                                    [Matière_Crutiale]    VARCHAR (50)  NULL,
                                    [Date_Enregistrement] DATETIME      NULL,
                                    [Auteur]              VARCHAR (150) NULL,
                                    [Coefficient]         INT           NULL,
                                    [Cycle]               VARCHAR (50)  NULL,
                                    [Enseignant]          INT           NULL,
                                    [Classe]              VARCHAR (50)  NULL,
                                    [Année_Scolaire]      VARCHAR (50)  NULL,
                                    PRIMARY KEY CLUSTERED ([Id] ASC)
                                );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Checktbl_entreprise()
        {
            DoesTableExist("tbl_entreprise", mycontrng);
            if (count > 0)
                return;
            else Createtbl_entreprise();
            count = 0;
        }

        private void Createtbl_entreprise()
        {
            using (SqlConnection con = new SqlConnection(mycontrng))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = @"CREATE TABLE [dbo].[tbl_entreprise] (
                                [Id]           INT           IDENTITY (1, 1) NOT NULL,
                                [Nom]          VARCHAR (150) NULL,
                                [Email]        VARCHAR (50)  NULL,
                                [Adresse]      VARCHAR (150) NULL,
                                [Téléphone]    VARCHAR (50)  NULL,
                                [Pays]         VARCHAR (50)  NULL,
                                [Ville]        VARCHAR (50)  NULL,
                                [Secteur]      VARCHAR (50)  NULL,
                                [Type_Produit] VARCHAR (50)  NULL,
                                [Slogan]       VARCHAR (150) NULL,
                                [Couleur]      VARCHAR (50)  NULL,
                                PRIMARY KEY CLUSTERED ([Id] ASC)
                            );
                        ";
                    //var commandStr = "IF NOT EXISTS (Select * from tbl_pert_deterioration) CREATE TABLE tbl_pert_deterioration(Id INT IDENTITY (1, 1) NOT NULL,Boisson  VARCHAR (100) NULL,Quantité DECIMAL (18)  NULL,Type VARCHAR (50)  NULL,Date DATETIME NULL,Auteur VARCHAR (150) NULL,Cause VARCHAR (50)  NULL)";

                    using (SqlCommand command = new SqlCommand(query, con))
                        command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        #endregion


        private void btnFermer_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (txtpasswordconnection.Text != "" && txtemailConnection.Text != "")
            {
                mycontrng = GetConnectionsStrings.GetSConnectionString();
                using (var donnée = new DonnéeEcoleDataContext())
                {
                    this.Cursor = Cursors.AppStarting;
                    var ss = from d in donnée.tbl_client_quitaye
                             where d.Password == txtpasswordconnection.Text && d.Email == txtemailConnection.Text
                             select d;

                    if (ss.Count() > 0)
                    {
                        var f = (from d in donnée.tbl_client_quitaye where d.Password == txtpasswordconnection.Text && d.Email == txtemailConnection.Text select d).First();
                        f.Last_Login = DateTime.Now;
                        donnée.SubmitChanges();
                        XDocument doc = XDocument.Load(Environment.CurrentDirectory + "\\Entreprise.xml");

                        var data = (from s in doc.Descendants("Entreprise")
                                    where s.Element("Id").Value == "1"
                                    select s).First();
                        data.Element("Email").Value = f.Email;
                        doc.Save(Environment.CurrentDirectory + "\\Entreprise.xml");
                        var inst = (from d in donnée.tbl_instances orderby d.Id descending select d).First();

                       

                        mycontrng = "Data Source=" + f.InstanceDb + ";Initial Catalog=" + f.Email + ";User ID=" + f.InstanceUser + ";Password=" + f.InstancePswd;
                        CreateDbs(f.Email, f.InstanceDb, f.InstanceUser, f.InstancePswd);
                        CheckTables();
                        Program.EntrepriseEmail();
                        this.Hide();
                        Program.RestartLogInInternet();
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        lblAvertiss.Visible = true;
                        lblAvertiss.Text = "Mot de passe ou adresse email incorrect.";
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void txtemailConnection_TextChanged(object sender, EventArgs e)
        {
            lblAvertiss.Visible = false;
        }

        private void btnFermer_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtpasswordconnection_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                iconButton2_Click(e, null);
            }
        }

        private void txtNomEntreprise_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                btnRegister_Click(e, null);
            }
        }
    }
}
