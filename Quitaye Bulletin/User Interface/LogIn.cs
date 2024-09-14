using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Linq.Expressions;
using Quitaye_School.Models.Context;

namespace Quitaye_School.User_Interface
{
    public partial class LogIn : Form
    {
        public static string type_compte;
        public static string filiale;
        public static string role;
        public static string profile;
        public static string mycontrng = ConfigurationManager.ConnectionStrings["conntrng"].ConnectionString;
        public LogIn()
        {
            InitializeComponent();
            //SelectInstance();
            Tables();
            //AddTarif();
            //ParentUpdate();
            enligne = false;
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Tick += LoadTimer_Tick;
            loadTimer.Start();
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await FillDG();
        }

        Timer loadTimer = new Timer();

        public LogIn(string connection) : this()
        {
            InitializeComponent();
            mycontrng = connection;
            SelectInstance();
            Tables();
            //AddTarif();
            //ParentUpdate();
            //FillDG();
        }

        private async void Tables()
        {
            Task task = new Task(CheckTables);
            task.Start();
            await task;
        }

        public static bool expiré = false;
        public static bool enligne = true;
        public static bool trial = true;
        public static int days;
        public static DateTime date;

        private void SelectInstance()
        {
            using (var donnée = new DonnéeEcoleDataContext())
            {
                TimeSpan ts;
                try
                {
                    var inst = (from d in donnée.tbl_client_quitaye where d.Email == Program.EntrepriseEmail() select d).First();
                    Principales.entreprise = inst.Entreprise;
                    if (inst.Date_Expiration.Value.Date < DateTime.Today.Date)
                        expiré = true;
                    if (inst.Type_Abonnement != null)
                    {
                        trial = false;
                    }
                    else
                    {
                        trial = true;
                    }
                    date = inst.Date_Expiration.Value.Date;

                    ts = inst.Date_Expiration.Value.Date - DateTime.Today.Date;
                    days = ts.Days;

                    inst.Last_Login = DateTime.Now;
                    donnée.SubmitChanges();
                    mycontrng = "Data Source=" + inst.InstanceDb + ";Initial Catalog=" + Program.EntrepriseEmail() + ";User ID=" + inst.InstanceUser + ";Password=" + inst.InstancePswd;

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

        

        private static int count;

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
            Checktbl_tarif_accessoire();
            Checktbl_annéescolaire();
            Checktbl_scolarité();
            Checktbl_notifier_absence();
            Checktbl_classe();
            Checktbl_compte_comptable();
            Checktbl_enseignant();
            Checktbl_entreprise();
            Checktbl_examen();
            Checktbl_historiqueeffectif();
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
        }

        private void Checktbl_inscription()
        {
            LogIn.DoesTableExist("tbl_inscription", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_inscription();
            LogIn.count = 0;
        }

        private void Checktbl_historiqueeffectif()
        {
            LogIn.DoesTableExist("tbl_historiqueeffectif", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_historiqueeffectif();
            LogIn.count = 0;
        }

        private void Checktbl_parent_elèves()
        {
            LogIn.DoesTableExist("tbl_parent_elèves", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_parent_elèves();
            LogIn.count = 0;
        }

        private void Createtbl_parent_elèves()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_parent_elèves]\r\n                                    (\r\n\t                                    [Id] INT NOT NULL PRIMARY KEY, \r\n                                        [Prenom] NVARCHAR(100) NULL, \r\n                                        [Nom] NVARCHAR(100) NULL, \r\n                                        [Genre] NVARCHAR(50) NULL, \r\n                                        [Pays] NVARCHAR(50) NULL, \r\n                                        [Contact] NVARCHAR(50) NULL, \r\n                                        [Adresse] NVARCHAR(150) NULL, \r\n                                        [Email] NVARCHAR(150) NULL, \r\n                                        [Ville] NVARCHAR(50) NULL, \r\n                                        [Date_Ajout] DATETIME NULL, \r\n                                        [Auteur] NVARCHAR(150) NULL,\r\n                                        [Nom_Contact] NVARCHAR(150) NULL\r\n                                    );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Checktbl_tarif_accessoire()
        {
            LogIn.DoesTableExist("tbl_tarif_accessoire", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_tarif_accessoire();
            LogIn.count = 0;
        }

        private void Createtbl_tarif_accessoire()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_tarif_accessoire]\r\n                                (\r\n\t                                [Id] INT NOT NULL PRIMARY KEY, \r\n                                    [Nom] NVARCHAR(50) NULL, \r\n                                    [Tarif_Annuel] DECIMAL NULL, \r\n                                    [Tarif_Mensuel] DECIMAL NULL, \r\n                                    [Tarif_Journalier] DECIMAL NULL\r\n                                );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Checktbl_list_journaux()
        {
            LogIn.DoesTableExist("tbl_list_journaux", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_list_journaux();
            LogIn.count = 0;
        }

        private Task<bool> AddColAsync(string table, string column, string type) => Task.Factory.StartNew<bool>((Func<bool>)(() => AddCol(table, column, type)));

        private bool AddCol(string table, string column, string type)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = LogIn.mycontrng;
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand("ALTER TABLE  [dbo].[" + table + "] ADD  " + column + " " + type + "  NULL ", connection);
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            connection.Close();
            return true;
        }

        private void Createtbl_list_journaux()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_list_journaux] (\r\n                                    [Id]          INT            NOT NULL,\r\n                                    [Nom]         NVARCHAR (150) NULL,\r\n                                    [Prefix]      NVARCHAR (50)  NULL,\r\n                                    [Type]        NVARCHAR (50)  NULL,\r\n                                    [Description] NVARCHAR (150) NULL,\r\n                                    [Date_Ajout]  DATETIME       NULL,\r\n                                    [Auteur]      NVARCHAR (100) NULL,\r\n                                    [Compte]      NVARCHAR (50)  NULL,\r\n                                    PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                                );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Checktbl_journal_comptable()
        {
            LogIn.DoesTableExist("tbl_journal_comptable", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_journal_comptable();
            LogIn.count = 0;
        }

        private void Checktbl_payement()
        {
            LogIn.DoesTableExist("tbl_payement", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_payement();
            LogIn.count = 0;
        }

        private void Checktbl_responsabilité()
        {
            LogIn.DoesTableExist("tbl_responsabilité", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_responsabilité();
            LogIn.count = 0;
        }

        private void Checktbl_Users()
        {
            LogIn.DoesTableExist("tbl_Users", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_Users();
            LogIn.count = 0;
        }

        private void Checktbl_note()
        {
            LogIn.DoesTableExist("tbl_note", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_note();
            LogIn.count = 0;
        }

        private void Checktbl_examen()
        {
            LogIn.DoesTableExist("tbl_examen", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_examen();
            LogIn.count = 0;
        }

        private void Checktbl_compte_comptable()
        {
            LogIn.DoesTableExist("tbl_Compte_Comptable", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_compte_comptable();
            LogIn.count = 0;
        }

        private void Checktbl_classe()
        {
            LogIn.DoesTableExist("tbl_classe", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_classe();
            LogIn.count = 0;
        }

        private void Checktbl_formulaire_inscription()
        {
            LogIn.DoesTableExist("tbl_formule_inscription", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_formulaire_inscription();
            LogIn.count = 0;
        }

        private void Checktbl_staff()
        {
            LogIn.DoesTableExist("tbl_staff", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_staff();
            LogIn.count = 0;
        }

        private void Checktbl_annéescolaire()
        {
            LogIn.DoesTableExist("tbl_année_scolaire", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_annéescolaire();
            LogIn.count = 0;
        }

        private void Checktbl_scolarité()
        {
            LogIn.DoesTableExist("tbl_scolarité", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_scolarité();
            LogIn.count = 0;
        }

        private void Checktbl_enseignant()
        {
            LogIn.DoesTableExist("tbl_enseignant", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_enseignant();
            LogIn.count = 0;
        }

        private void Checktbl_notifier_absence()
        {
            LogIn.DoesTableExist("tbl_notifier_absence", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_notifier_Absence();
            LogIn.count = 0;
        }

        private void Createtbl_notifier_Absence()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_notifier_absence] (\r\n                                    [Id]             INT           NOT NULL,\r\n                                    [Personne]       NVARCHAR(150)  NULL,\r\n                                    [Genre]          NVARCHAR(50)  NULL,\r\n                                    [N_Matricule]    NVARCHAR(50)  NULL,\r\n                                    [Date_Ajout]     DATETIME      NULL,\r\n                                    [Date_Absence]   DATETIME      NULL,\r\n                                    [Auteur]         NVARCHAR(150) NULL,\r\n                                    [Commentaire]    NVARCHAR(150) NULL,\r\n                                    [Année_Scolaire] NVARCHAR(50)  NULL,\r\n                                    [Classe]         NVARCHAR(50)  NULL,\r\n                                    [Cycle]          NVARCHAR(50)  NULL,\r\n                                    [Fichier] VARBINARY(MAX) NULL, \r\n                                    [Nom_Fichier] NVARCHAR(150) NULL, \r\n                                    PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                                );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Createtbl_staff()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_staff]\r\n                                    (\r\n\t                                    [Id] INT NOT NULL PRIMARY KEY, \r\n                                        [Nom] NVARCHAR(150) NULL, \r\n                                        [Genre] NVARCHAR(50) NULL, \r\n                                        [Adresse] NVARCHAR(50) NULL, \r\n                                        [Role] NVARCHAR(150) NULL, \r\n                                        [Contact] NVARCHAR(50) NULL, \r\n                                        [Auteur] NVARCHAR(150) NULL, \r\n                                        [Date_Ajout] DATETIME NULL\r\n                                    );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Createtbl_Users()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_Users] (\r\n                                    [Id]                 INT           IDENTITY (1, 1) NOT NULL,\r\n                                    [Prenom]             VARCHAR (50)  NULL,\r\n                                    [Nom]                VARCHAR (50)  NULL,\r\n                                    [Username]           VARCHAR (50)  NULL,\r\n                                    [Password]           VARCHAR (50)  NULL,\r\n                                    [Email]              VARCHAR (50)  NULL,\r\n                                    [Contact]            VARCHAR (50)  NULL,\r\n                                    [Adresse]            VARCHAR (100) NULL,\r\n                                    [Genre]              VARCHAR (20)  NULL,\r\n                                    [Type_Compte]        VARCHAR (50)  NULL,\r\n                                    [Role]               VARCHAR (50)  NULL,\r\n                                    [Date_Ajout]         DATETIME      NULL,\r\n                                    [Auteur]             VARCHAR (50)  NULL,\r\n                                    [Departement]        VARCHAR (50)  NULL,\r\n                                    [Active]             VARCHAR (50)  NULL,\r\n                                    [Classe]             VARCHAR (50)  NULL,\r\n                                    [Auth_Premier_Cycle] VARCHAR (50)  NULL,\r\n                                    [Auth_Second_Cycle]  VARCHAR (50)  NULL,\r\n                                    [Auth_Lycée]         VARCHAR (50)  NULL,\r\n                                    [Auth_Université]    VARCHAR (50)  NULL,\r\n                                    [Auth_Cente_Loisir] VARCHAR(50) NULL, \r\n                                    [Auth_Crèche] VARCHAR(50) NULL, \r\n                                    [Auth_Maternelle] VARCHAR(50) NULL, \r\n                                    PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                                );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MsgBox msgBox = new MsgBox();
                    msgBox.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Createtbl_payement()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_payement] (\r\n                                    [Id]                  INT             NOT NULL,\r\n                                    [Prenom]              VARCHAR (50)    NULL,\r\n                                    [Nom]                 VARCHAR (50)    NULL,\r\n                                    [Genre]               VARCHAR (50)    NULL,\r\n                                    [N_Matricule]         VARCHAR (50)    NULL,\r\n                                    [Classe]              VARCHAR (50)    NULL,\r\n                                    [Cycle]               VARCHAR (50)    NULL,\r\n                                    [Montant]             DECIMAL (18)    NULL,\r\n                                    [Date_Payement]       DATE            NULL,\r\n                                    [Date_Enregistrement] DATETIME        NULL,\r\n                                    [Auteur]              VARCHAR (150)   NULL,\r\n                                    [Commentaire]         VARCHAR (150)   NULL,\r\n                                    [Année_Scolaire]      VARCHAR (50)    NULL,\r\n                                    [Fichier]             VARBINARY (MAX) NULL,\r\n                                    [Nom_Fichier]         VARCHAR (100)   NULL,\r\n                                    [Type]                VARCHAR (50)    NULL,\r\n                                    [Tranche1]            DECIMAL (18)    NULL,\r\n                                    [Tranche2]            DECIMAL (18)    NULL,\r\n                                    [Tranche3]            DECIMAL (18)    NULL,\r\n                                    [Opération] VARCHAR(50) NULL, \r\n                                    PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                                );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Createtbl_responsabilité()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_responsabilité]\r\n                                (\r\n\t                                [Id] INT NOT NULL PRIMARY KEY, \r\n                                    [Responsabilité] NVARCHAR(150) NULL, \r\n                                    [Date_Ajout] DATETIME NULL, \r\n                                    [Auteur] NVARCHAR(50) NULL\r\n                                );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Createtbl_journal_comptable()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_journal_comptable] (\r\n                                [Id]                  INT             NOT NULL,\r\n                                [Date]                DATE            NULL,\r\n                                [Date_Enregistrement] DATETIME        NULL,\r\n                                [Libelle]             VARCHAR (150)   NULL,\r\n                                [N_Facture]           VARCHAR (150)   NULL,\r\n                                [Compte]              VARCHAR (50)    NULL,\r\n                                [Compte_Tier]         VARCHAR (50)    NULL,\r\n                                [Débit]               DECIMAL (18)    NULL,\r\n                                [Crédit]              DECIMAL (18)    NULL,\r\n                                [Auteur]              VARCHAR (150)   NULL,\r\n                                [Ref_Pièces]          VARCHAR (50)    NULL,\r\n                                [Commentaire]         VARCHAR (150)   NULL,\r\n                                [Nom_Fichier]         VARCHAR (150)   NULL,\r\n                                [Fichier]             VARBINARY (MAX) NULL,\r\n                                [Ref_Payement]        VARCHAR (150)   NULL,\r\n                                [Active]              VARCHAR (50)    NULL,\r\n                                [Validé]              NVARCHAR (50)   NULL,\r\n                                [Type]                NVARCHAR (50)   NULL,\r\n                                PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                            );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Createtbl_note()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_note] (\r\n                                [Id]                  INT           NOT NULL,\r\n                                [Note_Classe]         DECIMAL (18,2)  NULL,\r\n                                [Note_Compo]          DECIMAL (18,2)  NULL,\r\n                                [Coeff]               DECIMAL (18,2)  NULL,\r\n                                [Prenom]              VARCHAR (50)  NULL,\r\n                                [Nom]                 VARCHAR (50)  NULL,\r\n                                [Genre]               VARCHAR (50)  NULL,\r\n                                [Matière]             VARCHAR (50)  NULL,\r\n                                [Classe]              VARCHAR (50)  NULL,\r\n                                [Cycle]               VARCHAR (50)  NULL,\r\n                                [Année_Scolaire]      VARCHAR (50)  NULL,\r\n                                [Examen]              VARCHAR (50)  NULL,\r\n                                [N_Matricule]         VARCHAR (50)  NULL,\r\n                                [Date]                DATE          NULL,\r\n                                [Date_Enregistrement] DATETIME      NULL,\r\n                                [Auteur]              VARCHAR (150) NULL,\r\n                                [Enseignant]          INT           NULL,\r\n                                PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                            );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Createtbl_inscription()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_inscription] (\r\n                                [Id]                  INT             NOT NULL,\r\n                                [Prenom]              VARCHAR (50)    NULL,\r\n                                [Nom]                 VARCHAR (50)    NULL,\r\n                                [Nom_Complet]         VARCHAR (150)   NULL,\r\n                                [Nom_Matricule]       VARCHAR (150)   NULL,\r\n                                [Date_Naissance]      DATE            NULL,\r\n                                [Genre]               VARCHAR (50)    NULL,\r\n                                [Type_Scolarité]      VARCHAR (50)    NULL,\r\n                                [Nom_Père]            VARCHAR (150)   NULL,\r\n                                [Nom_Mère]            VARCHAR (150)   NULL,\r\n                                [Contact 1]           VARCHAR (50)    NULL,\r\n                                [Contact 2]           VARCHAR (50)    NULL,\r\n                                [Email]               VARCHAR (100)   NULL,\r\n                                [Adresse]             VARCHAR (150)   NULL,\r\n                                [Nationalité]         VARCHAR (50)    NULL,\r\n                                [Classe]              VARCHAR (50)    NULL,\r\n                                [Année_Scolaire]      VARCHAR (50)    NULL,\r\n\t                            [Cantine]\t\t\t  VARCHAR (50)    NULL,\r\n                                [Transport]           VARCHAR (50)    NULL,\r\n                                [Assurance]\t\t\t  VARCHAR (50)    NULL,\r\n                                [N_Matricule]         VARCHAR (50)    NOT NULL,\r\n                                [Date_Inscription]    DATETIME        NULL,\r\n                                [Auteur]              VARCHAR (150)   NULL,\r\n                                [Image]               VARBINARY (MAX) NULL,\r\n                                [Ref_Pièces]          VARCHAR (50)    NULL,\r\n                                [Cycle]               VARCHAR (50)    NULL,\r\n                                [Active]              VARCHAR (50)    NULL,\r\n                                [Scolarité]           DECIMAL (18)    NULL,\r\n                                [Motif_Desactivation] VARCHAR (50)    NULL,\r\n                                [Date_Desactivation]  DATETIME        NULL,\r\n                                PRIMARY KEY CLUSTERED ([N_Matricule] ASC)\r\n                            );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Createtbl_historiqueeffectif()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_historiqueeffectif] (\r\n                                [Id]                  INT             NOT NULL,\r\n                                [Nom_Matricule]       VARCHAR (150)   NULL,\r\n                                [Genre]               VARCHAR (50)    NULL,\r\n                                [Type_Scolarité]      VARCHAR (50)    NULL,\r\n                                [Nationalité]         VARCHAR (50)    NULL,\r\n                                [Classe]              VARCHAR (50)    NULL,\r\n                                [Année_Scolaire]      VARCHAR (50)    NULL,\r\n                                [NewAnnée_Scolaire]   VARCHAR (50)    NULL,\r\n\t                            [Cantine]\t\t\t  VARCHAR (50)    NULL,\r\n                                [Transport]           VARCHAR (50)    NULL,\r\n                                [Assurance]\t\t\t  VARCHAR (50)    NULL,\r\n                                [N_Matricule]         VARCHAR (50)    NOT NULL,\r\n                                [Date_Inscription]    DATETIME        NULL,\r\n                                [Auteur]              VARCHAR (150)   NULL,\r\n                                [Image]               VARBINARY (MAX) NULL,\r\n                                [Ref_Pièces]          VARCHAR (50)    NULL,\r\n                                [Cycle]               VARCHAR (50)    NULL,\r\n                                [Active]              VARCHAR (50)    NULL,\r\n                                [Scolarité]           DECIMAL (18)    NULL,\r\n                                PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                            );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Createtbl_formulaire_inscription()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_formule_inscription] (\r\n                                [Id]         INT           IDENTITY (1, 1) NOT NULL,\r\n                                [Formule]    VARCHAR (50)  NULL,\r\n                                [Montant]    DECIMAL (18)  NULL,\r\n                                [Gratuit]    VARCHAR (50)  NULL,\r\n                                [Date_Ajout] DATETIME      NULL,\r\n                                [Auteur]     VARCHAR (150) NULL,\r\n                                [Compte]     INT           NULL,\r\n                                PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                            );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Createtbl_examen()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_examen] (\r\n                                [Id]                  INT           IDENTITY (1, 1) NOT NULL,\r\n                                [Nom]                 VARCHAR (50)  NULL,\r\n                                [Date_Enregistrement] DATETIME      NULL,\r\n                                [Auteur]              VARCHAR (150) NULL,\r\n                                [Cycle]               VARCHAR (50)  NULL,\r\n                                PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                            );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Createtbl_compte_comptable()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_Compte_Comptable] (\r\n                                [Id]          INT           NOT NULL,\r\n                                [Compte]      VARCHAR (50)  NOT NULL,\r\n                                [Catégorie]   VARCHAR (150) NULL,\r\n                                [Nom_Compte]  VARCHAR (150) NULL,\r\n                                [Date_Ajout]  DATETIME      NULL,\r\n                                [Auteur]      VARCHAR (150) NULL,\r\n                                [Description] VARCHAR (150) NULL,\r\n                                [Compte_Aux]  NVARCHAR (50) NULL,\r\n                                [Type]        VARCHAR (50)  NULL,\r\n                                [Préfix]      NVARCHAR (50) NULL,\r\n                                PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                            );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Createtbl_classe()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_classe] (\r\n                                    [Id]        INT           IDENTITY (1, 1) NOT NULL,\r\n                                    [Nom]       VARCHAR (50)  NULL,\r\n                                    [Scolarité] DECIMAL (18)  NULL,\r\n                                    [Date]      DATETIME      NULL,\r\n                                    [Auteur]    VARCHAR (150) NULL,\r\n                                    [Cycle]     VARCHAR (50)  NULL,\r\n                                    [Tranche 1] DECIMAL (18)  NULL,\r\n                                    [Tranche 2] DECIMAL (18)  NULL,\r\n                                    [Tranche 3] DECIMAL (18)  NULL,\r\n                                    PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                                );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Createtbl_annéescolaire()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_année_scolaire] (\r\n                                    [Id]     INT          IDENTITY (1, 1) NOT NULL,\r\n                                    [Nom]    VARCHAR (50) NULL,\r\n                                    [Auteur] VARCHAR (50) NULL,\r\n                                    [Date]   DATETIME     NULL,\r\n                                    PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                                );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Createtbl_scolarité()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_scolarité] (\r\n                                    [Id]     INT          IDENTITY (1, 1) NOT NULL,\r\n                                    [Année_Scolaire]    VARCHAR (50) NULL,\r\n                                    [Date]   DATETIME     NULL,\r\n                                    [Classe]       VARCHAR (50)  NULL,\r\n                                    [Montant] DECIMAL (18)  NULL,\r\n                                    [Auteur]    VARCHAR (150) NULL,\r\n                                    [Cycle]     VARCHAR (50)  NULL,\r\n                                    [Tranche 1] DECIMAL (18)  NULL,\r\n                                    [Tranche 2] DECIMAL (18)  NULL,\r\n                                    [Tranche 3] DECIMAL (18)  NULL,\r\n                                    PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                                );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Createtbl_enseignant()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_enseignant] (\r\n                                    [Id]             INT           IDENTITY (1, 1) NOT NULL,\r\n                                    [Prenom]         VARCHAR (50)  NULL,\r\n                                    [Nom]            VARCHAR (50)  NULL,\r\n                                    [Nom_Complet]    VARCHAR (150) NULL,\r\n                                    [Date_Naissance] DATETIME      NULL,\r\n                                    [Genre]          VARCHAR (50)  NULL,\r\n                                    [Contact1]       VARCHAR (50)  NULL,\r\n                                    [Contact2]       VARCHAR (50)  NULL,\r\n                                    [Email]          VARCHAR (100) NULL,\r\n                                    [Adresse]        VARCHAR (150) NULL,\r\n                                    [Nationalité]    VARCHAR (50)  NULL,\r\n                                    [Date_Ajout]     DATETIME      NULL,\r\n                                    [Auteur]         VARCHAR (100) NULL,\r\n                                    [Image]          IMAGE         NULL,\r\n                                    [Active]         VARCHAR (50)  NULL,\r\n                                    PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                                );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Checktbl_matiere()
        {
            LogIn.DoesTableExist("tbl_matiere", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_matiere();
            LogIn.count = 0;
        }

        private void Createtbl_matiere()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_matiere] (\r\n                                    [Id]                  INT           IDENTITY (1, 1) NOT NULL,\r\n                                    [Nom]                 VARCHAR (50)  NULL,\r\n                                    [Matière_Crutiale]    VARCHAR (50)  NULL,\r\n                                    [Date_Enregistrement] DATETIME      NULL,\r\n                                    [Auteur]              VARCHAR (150) NULL,\r\n                                    [Coefficient]         DECIMAL (18,2)NULL,\r\n                                    [Cycle]               VARCHAR (50)  NULL,\r\n                                    [Enseignant]          INT           NULL,\r\n                                    [Classe]              VARCHAR (50)  NULL,\r\n                                    [Année_Scolaire]      VARCHAR (50)  NULL,\r\n                                    PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                                );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Checktbl_entreprise()
        {
            LogIn.DoesTableExist("tbl_entreprise", LogIn.mycontrng);
            if (LogIn.count > 0)
                return;
            Createtbl_entreprise();
            LogIn.count = 0;
        }

        private void Createtbl_entreprise()
        {
            using (SqlConnection connection = new SqlConnection(LogIn.mycontrng))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("CREATE TABLE [dbo].[tbl_entreprise] (\r\n                                [Id]           INT           IDENTITY (1, 1) NOT NULL,\r\n                                [Nom]          VARCHAR (150) NULL,\r\n                                [Email]        VARCHAR (50)  NULL,\r\n                                [Adresse]      VARCHAR (150) NULL,\r\n                                [Téléphone]    VARCHAR (50)  NULL,\r\n                                [Pays]         VARCHAR (50)  NULL,\r\n                                [Ville]        VARCHAR (50)  NULL,\r\n                                [Secteur]      VARCHAR (50)  NULL,\r\n                                [Type_Produit] VARCHAR (50)  NULL,\r\n                                [Slogan]       VARCHAR (150) NULL,\r\n                                [Couleur]      VARCHAR (50)  NULL,\r\n                                PRIMARY KEY CLUSTERED ([Id] ASC)\r\n                            );\r\n                        ", connection))
                        sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException sqlException))
                    {
                        sqlException = ex.InnerException as SqlException;
                        int errorCode = sqlException.ErrorCode;
                    }
                    if (sqlException == null || sqlException.ErrorCode != -2146232060)
                        return;
                    MsgBox msgBox = new MsgBox();
                    msgBox.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    int num = (int)msgBox.ShowDialog();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private async void ParentUpdate()
        {
            using(var donnée = new QuitayeContext())
            {
                var der = from d in donnée.tbl_inscription select d;
                var pr = from d in donnée.tbl_parent_elèves select d;
                if(der.Count() != 0 && pr.Count() == 0)
                {
                    foreach (var item in der)
                    {
                        int id = 0;
                        var fr = from d in donnée.tbl_parent_elèves select d;
                        if(fr.Count() != 0)
                        {
                            var er = (from d in donnée.tbl_parent_elèves 
                                      orderby d.Id descending 
                                      select d).First();
                            id = er.Id;
                        }
                        

                        var tbl = new Models.Context.tbl_parent_elèves();
                        tbl.Id = id + 1;
                        tbl.Prenom = item.Nom_Père;
                        tbl.Nom_Contact = item.Nom_Père + " " + item.Contact_1;
                        tbl.Adresse = item.Adresse;
                        tbl.Contact = item.Contact_1;
                        tbl.Genre = "Homme";
                        tbl.Date_Ajout = item.Date_Inscription;
                        tbl.Email = item.Email;
                        tbl.Auteur = "Quitaye School";
                        donnée.tbl_parent_elèves.Add(tbl);
                        await donnée.SaveChangesAsync();

                        var tbl1 = new Models.Context.tbl_parent_elèves();
                        tbl1.Id = id + 2;
                        tbl1.Prenom = item.Nom_Mère;
                        tbl1.Nom_Contact = item.Nom_Mère + " " + item.Contact_2;
                        tbl1.Adresse = item.Adresse;
                        tbl1.Contact = item.Contact_2;
                        tbl1.Genre = "Femme";
                        tbl1.Date_Ajout = item.Date_Inscription;
                        tbl1.Email = item.Email;
                        tbl1.Auteur = "Quitaye School";
                        donnée.tbl_parent_elèves.Add(tbl1);
                        await donnée.SaveChangesAsync();
                    }
                }
            }
        }

        private async Task<bool> InsertHistoriqueDataAsync(Models.Context.tbl_historiqueeffectif his,
            QuitayeContext context)
        {
            var id = context.tbl_historiqueeffectif
                .OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault()+1;
            his.Id = id;
            his.Date_Inscription = DateTime.Now;
            if(his.Scolarité != null)
            {
                var cla = (from d in context.tbl_classe where d.Nom == his.Classe select d).FirstOrDefault();
                if(cla != null)
                {
                    his.Tranche1 = cla.Tranche_1;
                    his.Tranche2 = cla.Tranche_2;
                    his.Tranche3 = cla.Tranche_3;
                }
            }
            his.Année_Scolaire = "2021-2022";
            context.tbl_historiqueeffectif.Add(his);
            //var ele = context.tbl_inscription.Where(x => x.N_Matricule == his.N_Matricule).FirstOrDefault();
            //ele.Classe = his.Classe;
            //ele.Année_Scolaire = his.NewAnnée_Scolaire;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task FillDG()
        {
            using(var donnée = new QuitayeContext())
            {
                try
                {
                    bool result = await AddColAsync("tbl_historiqueeffectif", "Tranche1", "Decimal(18,0)");
                    bool results = await AddColAsync("tbl_historiqueeffectif", "Tranche2", "Decimal(18,0)");
                    bool resultss = await AddColAsync("tbl_historiqueeffectif", "Tranche3", "Decimal(18,0)");
                    bool resultdss = await AddColAsync("tbl_historiqueeffectif", "Ancienne_Classe", "Nvarchar(50)");
                    bool resultssd = await AddColAsync("tbl_Users", "Filiale", "Nvarchar(50)");
                    bool resultssh = await AddColAsync("tbl_Users", "Nom_Complet", "Nvarchar(150)");
                    bool resultsse = await AddColAsync("tbl_payement", "Cloturé", "Nvarchar(50)");
                    bool resulsse = await AddColAsync("tbl_payement", "Nature", "Nvarchar(50)");
                    bool resultse = await AddColAsync("tbl_payement", "Raison", "Nvarchar(50)");
                    bool resultsed = await AddColAsync("tbl_payement", "Num_Opération", "Nvarchar(50)");
                    bool resultseo = await AddColAsync("tbl_payement", "Client", "Nvarchar(50)");
                    bool resultsef = await AddColAsync("tbl_payement", "Num_Client", "Nvarchar(50)");
                    bool resultseg = await AddColAsync("tbl_payement", "Mode_Payement", "Nvarchar(50)");
                    bool resultseh = await AddColAsync("tbl_payement", "Compte_Tier", "Nvarchar(50)");
                    bool resulsseh = await AddColAsync("tbl_payement", "Reference", "Nvarchar(50)");
                    bool resulse = await AddColAsync("tbl_payement", "Réduction", "Decimal(18,0)");
                    bool resultsfe = await AddColAsync("tbl_payement", "Date_Echeance", "datetime");



                    //var ele = (from d in donnée.tbl_inscription
                    //           join hist in donnée.tbl_historiqueeffectif 
                    //           on d.N_Matricule equals hist.N_Matricule into joinedTable
                    //           from h in joinedTable.DefaultIfEmpty()
                    //           where d.Classe == "SECTION MOYEN" || d.Classe == "SECTION GRAND" 
                    //           && d.Année_Scolaire == "2022-2023" && (h == null)
                    //           select new
                    //           {
                    //               Classe = d.Classe,
                    //               N_Matricule = d.N_Matricule,
                    //               Active = d.Active,
                    //               Genre = d.Genre,
                    //               Auteur = d.Auteur,
                    //               Scolarité = d.Scolarité,
                    //               Cycle = d.Cycle,
                    //               NewAnnée_Scolaire = d.Année_Scolaire
                    //           }).ToList();

                    //foreach (var item in ele)
                    //{
                    //    var res = await InsertHistoriqueDataAsync(new Models.Context.tbl_historiqueeffectif()
                    //    {
                    //        Classe = item.Classe,
                    //        N_Matricule = item.N_Matricule,
                    //        Active = item.Active,
                    //        Genre = item.Genre,
                    //        Auteur = item.Auteur,
                    //        Scolarité = item.Scolarité,
                    //        Cycle = item.Cycle,
                    //        NewAnnée_Scolaire = item.NewAnnée_Scolaire
                    //    }, donnée) ;
                    //}

                    //var don = from d in donnée.tbl_Users
                    //          select new
                    //          {
                    //              Prenom = d.Prenom,
                    //              Nom = d.Nom,
                    //          };
                    //var di = from d in donnée.tbl_entreprise select d;

                    ;
                    //var payemt = donnée.tbl_payement.Where(d => d.Montant != 0).GroupBy(d => new
                    //{
                    //    Classe = d.Classe,
                    //    Matricule = d.N_Matricule
                    //}).Select(gr => new
                    //{
                    //    Classe = gr.Key.Classe,
                    //    Matricule = gr.Key.Matricule,
                    //    Montant = gr.Sum((x => x.Montant)),
                    //    Tranche1 = gr.Sum((x => x.Tranche1)),
                    //    Tranche2 = gr.Sum((x => x.Tranche2)),
                    //    Tranche3 = gr.Sum((x => x.Tranche3))
                    //});

                    //var max = payemt.GroupBy(d => new
                    //{
                    //    Classe = d.Classe
                    //}).Select(gr => new
                    //{
                    //    Classe = gr.Key.Classe,
                    //    Max = gr.Max(x => x.Montant),
                    //    Tranche1 = gr.Max(x => x.Tranche1),
                    //    Tranche2 = gr.Max(x => x.Tranche2),
                    //    Tranche3 = gr.Max(x => x.Tranche3)
                    //});
                    //var effect = donnée.tbl_historiqueeffectif.Where((d => d.Scolarité == 0 && d.Type_Scolarité == "Normal")).ToList();
                    //foreach (var item in effect)
                    //{
                    //    foreach (var items in max)
                    //    {
                    //        if (items.Classe == item.Classe)
                    //        {
                    //            item.Scolarité = items.Max;
                    //            item.Tranche1 = items.Tranche1;
                    //            item.Tranche2 = items.Tranche2;
                    //            item.Tranche3 = items.Tranche3;
                    //            await donnée.SaveChangesAsync();
                    //        }
                    //    }
                    //}
                    //var année = donnée.tbl_année_scolaire.OrderBy((d => d.Date)).Select(d => new
                    //{
                    //    Nom = d.Nom,
                    //    Date = d.Date
                    //}).Take(2);
                    //if (année.Count() >= 1)
                    //{
                    //    var source = année;
                    //    foreach (var data1 in source)
                    //    {
                    //        var item = data1;
                    //        var eleve = donnée.tbl_inscription.Where((d => d.Active == "Oui")).Select(d => new
                    //        {
                    //            Matricule = d.N_Matricule,
                    //            Prenom = d.Prenom,
                    //            Nom = d.Nom,
                    //            Genre = d.Genre,
                    //            classe = d.Classe
                    //        });
                    //        foreach (var data2 in eleve)
                    //        {
                    //            var items = data2;
                    //            var pay = donnée.tbl_payement.Where((d => d.Année_Scolaire == item.Nom && d.N_Matricule == items.Matricule)).Take(1);
                    //            if (pay.Count() == 0)
                    //            {
                    //                var sco = donnée.tbl_scolarité.Where((d => d.Année_Scolaire == item.Nom && d.Classe == items.classe)).First();
                    //                var p = new Models.Context.tbl_payement();
                    //                p.Nom = items.Nom;
                    //                p.Prenom = items.Prenom;
                    //                p.Genre = items.Genre;
                    //                p.Classe = sco.Classe;
                    //                p.Année_Scolaire = item.Nom;
                    //                p.Montant = new Decimal?(0M);
                    //                p.Date_Enregistrement = new DateTime?(DateTime.Now);
                    //                p.Date_Payement = new DateTime?(DateTime.Now);
                    //                p.Cycle = sco.Cycle;
                    //                p.Auteur = "Système";
                    //                p.N_Matricule = items.Matricule;
                    //                donnée.tbl_payement.Add(p);
                    //                await donnée.SaveChangesAsync();
                    //                sco = null;
                    //                p = null;
                    //            }
                    //            pay = null;
                    //        }
                    //        eleve = null;
                    //    }
                    //}

                    //DirectoryInfo directory = Directory.CreateDirectory(@"C:\Quitaye School");
                    //if (di.Count() == 0)
                    //{
                    //    var s = new Models.Context.tbl_entreprise();
                    //    s.Nom = null;
                    //    donnée.tbl_entreprise.Add(s);
                    //    await donnée.SaveChangesAsync();
                    //}
                    //dataGridView1.DataSource = don;
                    //if (don.Count() == 0)
                    //    btnInscrire.Visible = true;
                    //donnée.Dispose();
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
                finally
                {
                    donnée.Dispose();
                }
            }
        }

        List<string> list = new List<string>() { "Cantine", "Transport", "Assurance", };
        private async void AddTarif()
        {
            using(var donnée = new QuitayeContext())
            {
                var er = from d in donnée.tbl_tarif_accessoire select new { Id = d.Id };
                if(er.Count() == 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        int id = 0;
                        if(i == 0)
                        {
                            
                        }else
                        {
                            var rer = (from d in donnée.tbl_tarif_accessoire 
                                       orderby d.Id descending 
                                       select d).First();
                            id = rer.Id;
                        }
                        
                        var rd = new Models.Context.tbl_tarif_accessoire();
                        rd.Id = id + 1;
                        rd.Nom = list[i];
                        donnée.tbl_tarif_accessoire.Add(rd);
                        await donnée.SaveChangesAsync();
                    }
                }
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hwd, int pa1, int pa2, int pa3);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void btnConnecter_Click(object sender, EventArgs e)
        {
            if (btnConnecter.Text == "Connecter")
            {
                using (var donnée = new QuitayeContext())
                {
                    try
                    {
                        if (txtPassword.Text != "" && txtUser.Text != "")
                        {
                            Cursor = Cursors.WaitCursor;
                            var s = from d in donnée.tbl_Users
                                    where d.Password == txtPassword.Text && d.Username == txtUser.Text && d.Active == "Oui" || d.Active == null
                                    select d;

                            if (s.Count() > 0)
                            {
                                var f = (from d in donnée.tbl_Users where d.Password == txtPassword.Text && d.Username == txtUser.Text select d).FirstOrDefault();
                                if (f == null)
                                {
                                    lblAvertiss.Visible = true;
                                    Cursor = Cursors.Default;
                                    return;
                                }
                                    
                                else lblAvertiss.Visible = false;
                                Principales.profile = f.Prenom + " " + f.Nom;
                                if (f.Auth_Premier_Cycle == "Oui")
                                    Principales.auth1 = "Premier Cycle";
                                if (f.Auth_Second_Cycle == "Oui")
                                    Principales.auth2 = "Second Cycle";
                                if (f.Auth_Lycée == "Oui")
                                    Principales.auth3 = "Lycée";
                                if (f.Auth_Université == "Oui")
                                    Principales.auth4 = "Université";
                                //Principales.role = f.Role;
                                Hide();
                                Cursor = Cursors.Default;
                                
                                Welcome welcome = new Welcome(Principales.profile);
                                welcome.ShowDialog();
                                Principales principales = new Principales();
                                principales.lblProfile.Text = Principales.profile;
                                Principales.role = f.Role;
                                Principales.type_compte = f.Type_Compte;
                                Principales.filiale = f.Filiale;
                                Principales.classes = f.Classe;
                                Principales.id = f.Id;
                                Principales.departement = f.Departement;
                                LogIn.type_compte = f.Type_Compte;
                                LogIn.profile = Principales.profile;
                                LogIn.role = f.Role;
                                LogIn.filiale = f.Filiale;
                                
                                
                                principales.ShowDialog();
                            }
                            else
                            {
                                lblAvertiss.Visible = true;
                                lblAvertiss.Text = "Mot de passe ou nom d'utilisateur incorrect.";
                                Cursor = Cursors.Default;
                            }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        MsgBox msg = new MsgBox();
                        msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                        msg.ShowDialog();
                    }
                }
            }
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            lblAvertiss.Visible = false;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                btnConnecter_Click(e, null);
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnInscrire_Click(object sender, EventArgs e)
        {
            Les_Utilisateurs utilisateurs = new Les_Utilisateurs();
            utilisateurs.cbxType.SelectedIndex = 1;
            utilisateurs.ShowDialog();
        }
    }
}
