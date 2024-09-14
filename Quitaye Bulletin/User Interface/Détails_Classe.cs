using FontAwesome.Sharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PrintAction;
using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Markup;
using Timer = System.Windows.Forms.Timer;

namespace Quitaye_School.User_Interface
{
    public partial class Détails_Classe : Form
    {
        public string cycle;
        private string mycontrng = LogIn.mycontrng;
        public static string classes;
        private int temp;
        private Timer loadTimer = new Timer();
        public string classsss;
        private Timer mytimer = new Timer();
        private MyTable result = new MyTable();
        public string name;
        public string type;
        public string tranche;

        public Détails_Classe()
        {
            InitializeComponent();
            temp = 1;
            timer1.Start();
            mytimer.Enabled = false;
            mytimer.Interval = 10;
            mytimer.Tick += Mytimer_Tick;
            lblScolarité.Visible = false;
            lblScolaritéPayée.Visible = false;
            lblScolaritéReste.Visible = false;
            lblEffectif.Visible = false;
            lblEffectifFille.Visible = false;
            lblEffectifGarçon.Visible = false;
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            loadTimer.Enabled = false;
            loadTimer.Interval = 10;
            loadTimer.Start();
            loadTimer.Tick += LoadTimer_Tick;
            btnFermer.Click += btnFermer_Click;
            btnTranche1.Click += btnTranche_Click;
            btnTranche2.Click += btnTranche_Click;
            btnTranche3.Click += btnTranche_Click;
            btnPDF.Click += btnPDF_Click;
            btnExcel.Click += btnExcel_Click;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            radioButton2.CheckedChanged += radioButton1_CheckedChanged;
            radioButton3.CheckedChanged += radioButton1_CheckedChanged;
            dataGridView1.CellClick += dataGridView1_CellClick;

            int width = SystemInformation.VirtualScreen.Width;
            int height = SystemInformation.VirtualScreen.Height;
            if (width < 1024)
            {
                Width = 1000;
                Height = 620;
            }
            else
            {
                if (width <= 1300 || Width < 1300)
                    return;
                Width = 1345;
                Height = 680;
            }
        }

        private async void Mytimer_Tick(object sender, EventArgs e)
        {
            mytimer.Stop();
            await CallData();
        }

        private async void LoadTimer_Tick(object sender, EventArgs e)
        {
            loadTimer.Stop();
            await CallTask();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 132)
            {
                base.WndProc(ref m);
                if ((int)m.Result != 1)
                    return;
                Point client = PointToClient(new Point(m.LParam.ToInt32()));
                if (client.Y <= 10)
                {
                    if (client.X <= 10)
                    {
                        m.Result = (IntPtr)13;
                    }
                    else
                    {
                        int x = client.X;
                        int num = Size.Width - 10;
                        m.Result = x >= num ? (IntPtr)14 : (IntPtr)12;
                    }
                }
                else
                {
                    int y = client.Y;
                    Size size = Size;
                    int num1 = size.Height - 10;
                    if (y <= num1)
                    {
                        if (client.X <= 10)
                        {
                            m.Result = (IntPtr)10;
                        }
                        else
                        {
                            int x = client.X;
                            size = Size;
                            int num2 = size.Width - 10;
                            m.Result = x >= num2 ? (IntPtr)11 : (IntPtr)2;
                        }
                    }
                    else if (client.X <= 10)
                    {
                        m.Result = (IntPtr)16;
                    }
                    else
                    {
                        int x = client.X;
                        size = Size;
                        int num3 = size.Width - 10;
                        m.Result = x >= num3 ? (IntPtr)17 : (IntPtr)15;
                    }
                }
            }
            else
                base.WndProc(ref m);
        }

        private Task<InfoClasse> Info_GéréraleAsync() => Task.Factory.StartNew(() => Info_Générale());

        private InfoClasse Info_Générale()
        {
            InfoClasse infoClasse1 = new InfoClasse();

            using (var donnée = new QuitayeContext())
            {
                try
                {
                    var queryable1 = donnée.tbl_historiqueeffectif
                        .Where((d => d.NewAnnée_Scolaire == Principales.annéescolaire
                    && d.Classe == Détails_Classe.classes && d.Active == "Oui")).Select(d => new
                    {
                        Matricule = d.N_Matricule
                    });
                    List<string> list = new List<string>();
                    foreach (var data in queryable1)
                        list.Add(data.Matricule);

                    var queryable2 = donnée.tbl_historiqueeffectif
                        .Where(d => d.Année_Scolaire == Principales.annéescolaire 
                    && d.Classe == Détails_Classe.classes && d.Active == "Oui")
                        .Select(d => new
                    {
                        Matricule = d.N_Matricule
                    });
                    
                    list = new List<string>();
                    foreach (var data in queryable2)
                    {
                        
                        list.Add(data.Matricule);
                    }
                    
                    var source1 = donnée
                        .tbl_scolarité.Where(d => d.Année_Scolaire == Principales.annéescolaire 
                        && d.Classe == Détails_Classe.classes);
                    
                    var source2 = donnée
                        .tbl_inscription.Where(d => d.Année_Scolaire == Principales.annéescolaire 
                    && d.Classe == Détails_Classe.classes && d.Active == "Oui")
                        .OrderBy(d => d.Prenom).OrderBy(d => d.Nom).Select(d => new
                    {
                        N_Matricule = d.N_Matricule,
                        Genre = d.Genre,
                        Classe = Détails_Classe.classes,
                        AnnéeScolaire = d.Année_Scolaire,
                        Type_Scolarité = d.Type_Scolarité,
                        Scolarité = d.Scolarité
                    });
                    var source3 = source2.Where(d => d.Genre == "Feminin");
                    var source4 = source2.Where(d => d.Genre == "Masculin");
                    infoClasse1.Fille = (Decimal)source3.Count();
                    infoClasse1.Garçon = source4.Count();
                    infoClasse1.Effectif = (Decimal)source2.Count();
                    
                    list = new List<string>();
                    foreach (var data in source2)
                    {
                        
                        list.Add(data.N_Matricule);
                    }
                    if (source2.Count() == 0)
                    {
                        
                        var source5 = donnée
                            .tbl_historiqueeffectif
                            .Where(d => d.Année_Scolaire == Principales.annéescolaire 
                        && d.Classe == Détails_Classe.classes && d.Active == "Oui").Select(d => new
                        {
                            N_Matricule = d.N_Matricule,
                            Type_Scolarité = d.Type_Scolarité,
                            Scolarité = d.Scolarité
                        });
                        
                        list = new List<string>();
                        foreach (var data in source5)
                        {
                            
                            list.Add(data.N_Matricule);
                        }
                        
                        
                        var source6 = donnée.tbl_inscription.Where(d => list.Contains(d.N_Matricule))
                            .OrderBy(d => d.Prenom)
                            .OrderBy(d => d.Nom).Select(d => new
                        {
                            N_Matricule = d.N_Matricule,
                            Genre = d.Genre,
                            Classe = Détails_Classe.classes,
                            AnnéeScolaire = d.Année_Scolaire,
                            Type_Scolarité = d.Type_Scolarité,
                            Scolarité = d.Scolarité
                        });

                        var source7 = source5.Where(d => d.Type_Scolarité == "Normal");
                        var queryable3 = source5.Where(d => d.Type_Scolarité == "Avec Rémise");
                        var source8 = source6.Where(d => d.Genre == "Feminin");
                        var source9 = source6.Where(d => d.Genre == "Masculin");
                        infoClasse1.Fille = (Decimal)source8.Count();
                        infoClasse1.Garçon = source9.Count();
                        infoClasse1.Effectif = (Decimal)source6.Count();
                        
                        list = new List<string>();
                        foreach (var data in source6)
                        {
                            
                            list.Add(data.N_Matricule);
                        }
                        InfoClasse infoClasse2 = infoClasse1;
                        Decimal num1;
                        if (source1.FirstOrDefault() != null)
                        {
                            
                            Decimal? montant = source1.FirstOrDefault().Montant;
                            Decimal num2 = (Decimal)source7.Count();
                            num1 = Convert.ToDecimal((montant.HasValue ? new Decimal?(montant.GetValueOrDefault() * num2) : new Decimal?()));
                        }
                        else
                        {
                            
                            Decimal? montant = donnée.tbl_scolarité.Where(d => d.Classe == Détails_Classe.classes).FirstOrDefault().Montant;
                            Decimal num3 = (Decimal)source7.Count();
                            num1 = Convert.ToDecimal((montant.HasValue ? new Decimal?(montant.GetValueOrDefault() * num3) : new Decimal?()));
                        }
                        infoClasse2.Scolarité = num1;
                        foreach (var data in queryable3)
                            infoClasse1.Scolarité += Convert.ToDecimal(data.Scolarité);
                        
                        var source10 = donnée.tbl_payement
                            .Where(d => d.Année_Scolaire == Principales.annéescolaire 
                            && list.Contains(d.N_Matricule) 
                            && d.Classe == Détails_Classe.classes).Select(d => new
                        {
                                
                            Montant = d.Montant,
                            N_Matricule = d.N_Matricule,
                            Genre = donnée.tbl_inscription.Where(e => e.N_Matricule == d.N_Matricule).Select(e => new
                            {
                                Genre = e.Genre
                            }).FirstOrDefault().Genre
                        });
                        InfoClasse infoClasse3 = infoClasse1;
                        infoClasse3.Scolarité_Payée = infoClasse3.Scolarité_Payée + Convert.ToDecimal(source10.Sum(x => x.Montant));
                        infoClasse1.Fille = Convert.ToDecimal(source10.Where(x => x.Genre == "Feminin").Sum(x => x.Montant));
                        infoClasse1.Garçon = Convert.ToInt32(source10.Where(x => x.Genre == "Masculin").Sum(x => x.Montant));
                        int int32_1 = Convert.ToInt32(infoClasse1.Fille);
                        int int32_2 = Convert.ToInt32(infoClasse1.Garçon);
                        infoClasse1.Effectif = (Decimal)Convert.ToInt32(int32_1 + int32_2);
                    }
                    else
                    {
                        var source11 = donnée.tbl_inscription
                            .Where(d => d.Année_Scolaire == Principales.annéescolaire 
                        && d.Classe == Détails_Classe.classes && d.Active == "Oui")
                        .OrderBy(d => d.Prenom)
                            .OrderBy(d => d.Nom).Select(d => new
                        {
                            N_Matricule = d.N_Matricule,
                            Genre = d.Genre,
                            Classe = Détails_Classe.classes,
                            Type_Scolarité = d.Type_Scolarité,
                            Scolarité = d.Scolarité
                        });
                        var source12 = source11.Where(d => d.Type_Scolarité == "Normal");
                        var queryable4 = source11.Where(d => d.Type_Scolarité == "Avec Rémise");
                        var source13 = source11.Where(d => d.Genre == "Feminin");
                        var source14 = source11.Where(d => d.Genre == "Masculin");
                        infoClasse1.Fille = source13.Count();
                        infoClasse1.Garçon = source14.Count();
                        infoClasse1.Effectif = (Decimal)source11.Count();
                        
                        list = new List<string>();
                        foreach (var data in source11)
                        {
                            
                            list.Add(data.N_Matricule);
                        }
                        InfoClasse infoClasse4 = infoClasse1;
                        Decimal num4;
                        if (source1.FirstOrDefault() != null)
                        {
                            decimal? montant = source1.FirstOrDefault().Montant;
                            decimal num5 = source12.Count();
                            num4 = Convert.ToDecimal((montant.HasValue ? new Decimal?(montant.GetValueOrDefault() * num5) : new Decimal?()));
                        }
                        else
                        {
                            
                            decimal? montant = donnée.tbl_scolarité.Where(d => d.Classe == Détails_Classe.classes).FirstOrDefault().Montant;
                            decimal num6 = source12.Count();
                            num4 = Convert.ToDecimal((montant.HasValue ? new Decimal?(montant.GetValueOrDefault() * num6) : new Decimal?()));
                        }
                        infoClasse4.Scolarité = num4;
                        foreach (var data in queryable4)
                            infoClasse1.Scolarité += Convert.ToDecimal(data.Scolarité);
                        
                        var source15 = donnée.tbl_payement
                            .Where(d => d.Année_Scolaire == Principales.annéescolaire 
                        && list.Contains(d.N_Matricule)
                        && d.Classe == Détails_Classe.classes).Select(d => new
                        {
                            Montant = d.Montant,
                            N_Matricule = d.N_Matricule,
                            Genre = donnée.tbl_inscription.Where(x => x.N_Matricule == d.N_Matricule).FirstOrDefault().Genre
                        });
                        InfoClasse infoClasse5 = infoClasse1;
                        infoClasse5.Scolarité_Payée = infoClasse5.Scolarité_Payée + Convert.ToDecimal(source15.Sum(x => x.Montant));
                        infoClasse1.Fille = Convert.ToDecimal(source15.Where(x => x.Genre == "Feminin").Sum(x => x.Montant));
                        infoClasse1.Garçon = Convert.ToInt32(source15.Where(x => x.Genre == "Masculin").Sum(x => x.Montant));
                        int int32_3 = Convert.ToInt32(infoClasse1.Fille);
                        int int32_4 = Convert.ToInt32(infoClasse1.Garçon);
                        infoClasse1.Effectif = (Decimal)Convert.ToInt32(int32_3 + int32_4);
                    }
                    infoClasse1.Reste_Scolarité = infoClasse1.Scolarité - infoClasse1.Scolarité_Payée;
                }
                finally
                {
                    
                    if (donnée != null)
                    {
                        
                        donnée.Dispose();
                    }
                }
            }
                
            return infoClasse1;
        }

        private async Task CallTask()
        {
            Task<InfoClasse> info_générale = Info_GéréraleAsync();
            Task<MyTable> filldata = FillDataAsync();
            List<Task> taskList = new List<Task>()
            {
                filldata,
                info_générale
            };

            while (taskList.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(taskList);
                if (finishedTask == info_générale)
                {
                    lblScolarité.Text = info_générale.Result.Scolarité.ToString("N0") + " FCFA";
                    lblScolaritéPayée.Text = filldata.Result.Montant.ToString("N0") + " FCFA";
                    Decimal rest = info_générale.Result.Scolarité - filldata.Result.Montant;
                    lblScolaritéReste.Text = rest.ToString("N0") + " FCFA";
                    lblEffectif.Text = "Effectif Total : " + filldata.Result.Effectif.ToString("N0");
                    lblEffectifGarçon.Text = "Garçon(s) : " + filldata.Result.Garçon.ToString("N0");
                    lblEffectifFille.Text = "Fille(s) : " + filldata.Result.Fille.ToString("N0");
                    lblScolarité.Visible = true;
                    lblScolaritéPayée.Visible = true;
                    lblScolaritéReste.Visible = true;
                    lblEffectif.Visible = true;
                    lblEffectifFille.Visible = true;
                    lblEffectifGarçon.Visible = true;
                }
                else if (finishedTask == filldata)
                {
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = filldata.Result.Table;
                    dataGridView2.DataSource = filldata.Result.Table;
                    result = filldata.Result;
                    lblEffectif.Text = "Effectif Total : " + result.Effectif.ToString("N0");
                    lblEffectifFille.Text = "Fille(s) :" + result.Fille.ToString("N0");
                    lblEffectifGarçon.Text = "Garçon(s) :" + result.Garçon.ToString("N0");
                    try
                    {
                        DossierColumn delete = new DossierColumn();
                        delete.HeaderText = "Détails";
                        delete.Name = "Dossier";
                        dataGridView1.Columns.Add((DataGridViewColumn)delete);
                        dataGridView1.Columns["Dossier"].Width = 50;
                        delete = (DossierColumn)null;
                    }
                    catch (Exception ex1)
                    {
                        Exception ex = ex1;
                    }
                }
                taskList.Remove(finishedTask);
                finishedTask = (Task)null;
            }
            info_générale = null;
            filldata = null;
            taskList = null;
        }

        private async Task CallData()
        {
            MyTable myTable = await FillDataAsync();
            result = myTable;
            myTable = null;
            if (result.Table.Rows.Count == 0)
            {
                dataGridView1.Columns.Clear();
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée disponible dans le tableau";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                lblEffectif.Text = "Effectif Total : " + result.Effectif.ToString("N0");
                lblEffectifFille.Text = "Fille(s) :" + result.Fille.ToString("N0");
                lblEffectifGarçon.Text = "Garçon(s) :" + result.Garçon.ToString("N0");
                dt = null;
                dr = null;
            }
            else
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = result.Table;
                dataGridView2.DataSource = result.Table;
                lblEffectif.Text = "Effectif Total : " + result.Effectif.ToString("N0");
                lblEffectifFille.Text = "Fille(s) :" + result.Fille.ToString("N0");
                lblEffectifGarçon.Text = "Garçon(s) :" + result.Garçon.ToString("N0");
                DossierColumn delete = new DossierColumn();
                delete.HeaderText = "Détails";
                delete.Name = "Dossier";
                dataGridView1.Columns.Add((DataGridViewColumn)delete);
                dataGridView1.Columns["Dossier"].Width = 50;
                delete = (DossierColumn)null;
            }
        }

        private Task<MyTable> FillDataAsync() => Task.Factory.StartNew(() => FillData());

        public MyTable FillData()
        {
            MyTable myTable = new MyTable();
            myTable.Table = new DataTable();
            myTable.Table.Columns.Add("N°_Matricule");
            myTable.Table.Columns.Add("Prenom");
            myTable.Table.Columns.Add("Nom");
            myTable.Table.Columns.Add("Genre");
            myTable.Table.Columns.Add("Montant");
            myTable.Table.Columns.Add("Classe");
            List<Elève> elèveList = new List<Elève>();

            using (var donnée = new QuitayeContext())
            {
                try
                {
                    List<Elève> list;
                    if (donnée.tbl_année_scolaire
                        .OrderByDescending(d => d.Nom)
                        .First().Nom == Principales.annéescolaire)
                    {
                        list = (donnée.tbl_inscription
                            .Where(i => i.Année_Scolaire == Principales.annéescolaire
                        && i.Classe == Détails_Classe.classes
                        && i.Active == "Oui").OrderBy(i => i.Nom)
                        .ThenBy(i => i.Prenom)
                        .Select(d => new Elève()
                        {
                            Matricule = d.N_Matricule,
                            Prenom = d.Prenom,
                            Nom = d.Nom,
                            Genre = d.Genre,
                            Montant_Scolarité = donnée
                            .tbl_payement.Where(x => x.N_Matricule == d.N_Matricule
                            && d.Année_Scolaire == Principales.annéescolaire
                            && d.Classe == classes).Sum(f => f.Montant).Value,
                            Classe = classes
                        })).ToList();
                    }
                    else
                    {
                        //if (classes.Contains("9"))
                        //{
                        //    var cl = donnée.tbl_classe.Where(x => x.Nom.Contains("10")).FirstOrDefault();
                        //    if (cl != null)
                        //    {
                        //        var last_ann = donnée.tbl_année_scolaire.Where(x => x.Nom == Principales.annéescolaire).FirstOrDefault();
                        //        var ann = donnée.tbl_année_scolaire.Where(x => x.Id > last_ann.Id).OrderBy(x => x.Id).FirstOrDefault();

                        //        list = (
                        //    from ins in donnée.tbl_inscription
                        //    join his in donnée.tbl_historiqueeffectif
                        //    on ins.N_Matricule equals his.N_Matricule into joinedHis
                        //    from his in joinedHis.DefaultIfEmpty()
                        //    where (ins.Année_Scolaire == ann.Nom && ins.Classe == cl.Nom && ins.Active == "Oui") ||
                        //          (his != null && his.Année_Scolaire == ann.Nom && his.Classe == cl.Nom && his.Active == "Oui")
                        //    orderby ins.Nom
                        //    select new Elève()
                        //    {
                        //        Matricule = ins.N_Matricule,
                        //        Id = ins.Id,
                        //        Prenom = ins.Prenom,
                        //        Nom = ins.Nom,
                        //        Date_Naissance = ins.Date_Naissance,
                        //        Genre = ins.Genre,
                        //        Père = ins.Nom_Père,
                        //        Mère = ins.Nom_Mère,
                        //        Date_Inscription = ins.Date_Inscription,
                        //        Classe = classes
                        //    }).Distinct().OrderBy(x => x.Nom).ThenBy(x => x.Prenom).ToList();
                        //    }
                        //    else
                        //    {
                        //        list = (
                        //    from ins in donnée.tbl_inscription
                        //    join his in donnée.tbl_historiqueeffectif
                        //    on ins.N_Matricule equals his.N_Matricule into joinedHis
                        //    from his in joinedHis.DefaultIfEmpty()
                        //    where (ins.Année_Scolaire == Principales.annéescolaire && ins.Classe == classes && ins.Active == "Oui") ||
                        //          (his != null && his.Année_Scolaire == Principales.annéescolaire && his.Classe == classes && his.Active == "Oui")
                        //    orderby ins.Nom
                        //    select new Elève()
                        //    {
                        //        Matricule = ins.N_Matricule,
                        //        Id = ins.Id,
                        //        Prenom = ins.Prenom,
                        //        Nom = ins.Nom,
                        //        Date_Naissance = ins.Date_Naissance,
                        //        Genre = ins.Genre,
                        //        Père = ins.Nom_Père,
                        //        Mère = ins.Nom_Mère,
                        //        Date_Inscription = ins.Date_Inscription,
                        //        Classe = classes
                        //    }).Distinct().OrderBy(x => x.Nom).ThenBy(x => x.Prenom).ToList();
                        //    }

                        //}
                        //else
                        {
                            list = (
                            from ins in donnée.tbl_inscription
                            join his in donnée.tbl_historiqueeffectif
                            on ins.N_Matricule equals his.N_Matricule into joinedHis
                            from his in joinedHis.DefaultIfEmpty()
                            where (ins.Année_Scolaire == Principales.annéescolaire && ins.Classe == classes && ins.Active == "Oui") ||
                                  (his != null && his.Année_Scolaire == Principales.annéescolaire && his.Classe == classes && his.Active == "Oui")
                            orderby ins.Nom
                            select new Elève()
                            {
                                Matricule = ins.N_Matricule,
                                Id = ins.Id,
                                Prenom = ins.Prenom,
                                Nom = ins.Nom,
                                Date_Naissance = ins.Date_Naissance,
                                Genre = ins.Genre,
                                Père = ins.Nom_Père,
                                Mère = ins.Nom_Mère,
                                Date_Inscription = ins.Date_Inscription,
                                Classe = classes
                            }).Distinct().OrderBy(x => x.Nom).ThenBy(x => x.Prenom).ToList();
                        }

                    }
                    foreach (var elève in list)
                    {
                        DataRow row = myTable.Table.NewRow();
                        row["N°_Matricule"] = elève.Matricule;
                        row["Prenom"] = elève.Prenom;
                        row["Nom"] = elève.Nom;
                        row["Genre"] = elève.Genre;
                        row["Montant"] = Convert.ToDecimal(elève.Montant_Scolarité).ToString("N0");
                        row["Classe"] = elève.Classe;
                        myTable.Table.Rows.Add(row);
                    }
                    myTable.Effectif = list.Count();
                    myTable.Garçon = list.Where((x => x.Genre == "Masculin")).Count();
                    myTable.Fille = list.Where((x => x.Genre == "Feminin")).Count();
                }
                finally
                {
                    if (donnée != null)
                    {
                        donnée.Dispose();
                    }
                }
            }

            return myTable;
        }

        private async Task CallTranchePayée(string tranche)
        {
            MyTable myTable = await TranchePayéeAsync(tranche);
            result = myTable;
            myTable = null;
            dataGridView1.Columns.Clear();
            if (result.Table.Rows.Count == 0)
            {
                dataGridView1.Columns.Clear();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("Tableau vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée disponible dans le tableau";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                lblEffectif.Text = "Effectif Total : " + result.Effectif.ToString("N0");
                lblEffectifFille.Text = "Fille(s) :" + result.Fille.ToString("N0");
                lblEffectifGarçon.Text = "Garçon(s) :" + result.Garçon.ToString("N0");
                dt = null;
                dr = null;
            }
            else
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = result.Table;
                dataGridView2.DataSource = result.Table;
                lblEffectif.Text = "Effectif Total : " + result.Effectif.ToString("N0");
                lblEffectifFille.Text = "Fille(s) :" + result.Fille.ToString("N0");
                lblEffectifGarçon.Text = "Garçon(s) :" + result.Garçon.ToString("N0");
                DossierColumn delete = new DossierColumn();
                delete.HeaderText = "Détails";
                delete.Name = "Dossier";
                dataGridView1.Columns.Add(delete);
                dataGridView1.Columns["Dossier"].Width = 50;
                delete = null;
            }
        }

        private async Task CallTrancheNonPayée(string tranche)
        {
            MyTable myTable = await TrancheNonPayéeAsync(tranche);
            result = myTable;
            myTable = null;
            dataGridView1.Columns.Clear();
            if (result.Table.Rows.Count == 0)
            {
                dataGridView1.Columns.Clear();
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée disponible dans le tableau";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                lblEffectif.Text = "Effectif Total : " + result.Effectif.ToString("N0");
                lblEffectifFille.Text = "Fille(s) :" + result.Fille.ToString("N0");
                lblEffectifGarçon.Text = "Garçon(s) :" + result.Garçon.ToString("N0");
                dt = null;
                dr = null;
            }
            else
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = result.Table;
                dataGridView2.DataSource = result.Table;
                lblEffectif.Text = "Effectif Total : " + result.Effectif.ToString("N0");
                lblEffectifFille.Text = "Fille(s) :" + result.Fille.ToString("N0");
                lblEffectifGarçon.Text = "Garçon(s) :" + result.Garçon.ToString("N0");
                DossierColumn delete = new DossierColumn();
                delete.HeaderText = "Détails";
                delete.Name = "Dossier";
                dataGridView1.Columns.Add(delete);
                dataGridView1.Columns["Dossier"].Width = 50;
                delete = null;
            }
        }

        public Task<MyTable> TranchePayéeAsync(string tranche)
            => Task.Factory.StartNew(() => TranchePayée(tranche));

        public MyTable TranchePayée(string tranche)
        {
            MyTable myTable = new MyTable();
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("N°_Matricule");
            dataTable.Columns.Add("Prenom");
            dataTable.Columns.Add("Nom");
            dataTable.Columns.Add("Genre");
            dataTable.Columns.Add("Classe");
            dataTable.Columns.Add("Montant");

            using (var donnée = new QuitayeContext())
            {
                try
                {
                    var queryable1 = donnée.tbl_historiqueeffectif
                        .Where(d => d.NewAnnée_Scolaire == Principales.annéescolaire
                        && d.Classe == Détails_Classe.classes
                        && d.Active == "Oui").Select(d => new
                        {
                            Matricule = d.N_Matricule
                        });
                    List<string> stringList = new List<string>();
                    foreach (var data in queryable1)
                    {

                        var source = donnée.tbl_historiqueeffectif
                            .Where(d => d.N_Matricule.Equals(data) 
                            && d.NewAnnée_Scolaire == Principales.annéescolaire)
                            .OrderByDescending(d => d.Id).Select(d => new
                        {
                            Matricule = d.N_Matricule,
                            Classe = d.Classe
                        }).Take(1);
                        if (source.Count() != 0 
                            && source.First().Classe == Détails_Classe.classes)
                            stringList.Add(source.First().Matricule);
                    }

                    var queryable2 = donnée.tbl_historiqueeffectif
                        .Where(d => d.Année_Scolaire == Principales.annéescolaire
                    && d.Classe == Détails_Classe.classes && d.Active == "Oui").Select(d => new
                    {
                        Matricule = d.N_Matricule
                    });
                    
                    var list = new List<string>();
                    foreach (var data in queryable2)
                    {
                        var source = donnée.tbl_historiqueeffectif
                            .Where((d => d.N_Matricule.Equals(data)
                        && d.Année_Scolaire == Principales.annéescolaire))
                            .OrderByDescending(d => d.Id).Select(d => new
                            {
                                Matricule = d.N_Matricule,
                                Classe = d.Classe
                            }).Take(1);
                        if (source.Count() != 0 && source.First().Classe == Détails_Classe.classes)
                        {
                            
                            list.Add(source.First().Matricule);
                        }
                    }

                    var source1 = donnée.tbl_inscription
                        .Where((d => d.Année_Scolaire == Principales.annéescolaire
                    && d.Classe == Détails_Classe.classes
                    && d.Active == "Oui")).OrderBy((d => d.Prenom))
                    .OrderBy((d => d.Nom)).Select(d => new
                    {
                        N_Matricule = d.N_Matricule,
                        Prenom = d.Prenom,
                        Nom = d.Nom,
                        Date_Naissance = d.Date_Naissance,
                        AnnéeScolaire = d.Année_Scolaire,
                        Genre = d.Genre,
                        Nom_Père = d.Nom_Père,
                        Nom_Mère = d.Nom_Mère,
                        Classe = Détails_Classe.classes,
                        Date_Inscription = d.Date_Inscription
                    });
                    list = new List<string>();
                    foreach (var data in source1)
                    {
                        list.Add(data.N_Matricule);
                    }
                    if (source1.Count() == 0)
                    {
                        var queryable3 = donnée.tbl_historiqueeffectif
                            .Where(d => d.Année_Scolaire == Principales.annéescolaire 
                        && d.Classe == Détails_Classe.classes && d.Active == "Oui").Select(d => new
                        {
                            N_Matricule = d.N_Matricule,
                            Type_Scolarité = d.Type_Scolarité,
                            Scolarité = d.Scolarité
                        });
                        
                        list = new List<string>();
                        foreach (var data in queryable3)
                        {
                            
                            list.Add(data.N_Matricule);
                        }
                    }
                    else
                    {
                        var queryable4 = donnée.tbl_inscription
                            .Where((d => d.Année_Scolaire == Principales.annéescolaire
                        && d.Classe == Détails_Classe.classes
                        && d.Active == "Oui")).OrderBy((d => d.Prenom))
                        .OrderBy((d => d.Nom)).Select(d => new
                        {
                            N_Matricule = d.N_Matricule,
                            Prenom = d.Prenom,
                            AnnéeScolaire = d.Année_Scolaire,
                            Nom = d.Nom,
                            Date_Naissance = d.Date_Naissance,
                            Genre = d.Genre,
                            Nom_Père = d.Nom_Père,
                            Nom_Mère = d.Nom_Mère,
                            Classe = Détails_Classe.classes,
                            Date_Inscription = d.Date_Inscription
                        });
                        
                        list = new List<string>();
                        foreach (var data in queryable4)
                        {
                            list.Add(data.N_Matricule);
                        }
                    }

                    var tblScolarité = donnée.tbl_scolarité.Where((d => d.Classe == Détails_Classe.classes
                    && d.Année_Scolaire == Principales.annéescolaire)).First();
                    
                    var scol = tblScolarité;
                    List<tbl_payement> source2 = new List<tbl_payement>();
                    if (tranche == "1")
                    {
                        var source3 = donnée.tbl_payement.Where((d => d.Classe == Détails_Classe.classes
                        && list.Contains(d.N_Matricule) && d.Année_Scolaire == Principales.annéescolaire
                        && d.Cycle == cycle && d.Type == "Scolarité")).GroupBy(d => new
                        {
                            Matricule = d.N_Matricule,
                            Prenom = donnée.tbl_inscription
                            .Where(x => x.N_Matricule == d.N_Matricule)
                            .Select(x => new { Prenom = x.Prenom })
                            .FirstOrDefault().Prenom,
                            Nom = donnée.tbl_inscription
                        .Where(x => x.N_Matricule == d.N_Matricule)
                        .Select(x => new { Nom = x.Nom }).FirstOrDefault().Nom,
                            Classe = d.Classe,
                            Montant = d.Montant
                        }).Where(grs => grs.Sum(x => x.Montant) >= scol.Tranche_1)
                        .OrderByDescending(grs => grs.Sum(x => x.Montant)).Select(y => new
                        {
                            N_Matricule = y.Key.Matricule,
                            Prenom = donnée.tbl_inscription
                                .Where(x => x.N_Matricule == y.Key.Matricule)
                                .Select(x => new { Prenom = x.Prenom })
                                .FirstOrDefault().Prenom,
                            Nom = donnée.tbl_inscription
                                .Where(x => x.N_Matricule == y.Key.Matricule)
                                .Select(x => new { Nom = x.Nom })
                                .FirstOrDefault().Nom,
                            Genre = donnée.tbl_inscription
                                .Where(x => x.N_Matricule == y.Key.Matricule)
                                .Select(x => new { Genre = x.Genre })
                                .FirstOrDefault().Genre,
                            Classe = y.Key.Classe,
                            Montant = y.Sum(x => x.Montant)
                        });
                        foreach (var data in source3)
                            source2.Add(new tbl_payement()
                            {
                                N_Matricule = data.N_Matricule,
                                Prenom = data.Prenom,
                                Nom = data.Nom,
                                Genre = data.Genre,
                                Classe = data.Classe,
                                Montant = data.Montant
                            });
                    }
                    else if (tranche == "2")
                    {
                        var source4 = donnée.tbl_payement.Where(d => d.Classe == Détails_Classe.classes
                        && list.Contains(d.N_Matricule) && d.Année_Scolaire == Principales.annéescolaire
                        && d.Cycle == cycle && d.Type == "Scolarité").GroupBy(d => new
                        {
                            Matricule = d.N_Matricule,
                            Prenom = donnée.tbl_inscription
                          .Where(f => f.N_Matricule == d.N_Matricule)
                          .Select(x => new { Matricule = x.N_Matricule })
                          .FirstOrDefault().Matricule,
                            Nom = d.N_Matricule,
                            Classe = d.Classe
                        }).Where(grs => grs.Sum((x => x.Montant)) >= scol.Tranche_2 + scol.Tranche_1)
                        .OrderByDescending(grs => grs.Sum(x => x.Montant)).Select(f => new
                        {
                            N_Matricule = f.Key.Matricule,
                            Prenom = donnée.tbl_inscription
                          .Where(x => x.N_Matricule == f.Key.Matricule)
                          .FirstOrDefault().Prenom,
                            Nom = donnée.tbl_inscription
                          .Where(x => x.N_Matricule == f.Key.Matricule)
                          .FirstOrDefault().Nom,
                            Genre = donnée.tbl_inscription
                            .Where(x => x.N_Matricule == f.Key.Matricule)
                            .FirstOrDefault().Genre,
                            Classe = f.Key.Classe,
                            Montant = f.Sum(x => x.Montant)
                        });
                        foreach (var data in source4)
                            source2.Add(new tbl_payement()
                            {
                                N_Matricule = data.N_Matricule,
                                Prenom = data.Prenom,
                                Nom = data.Nom,
                                Genre = data.Genre,
                                Classe = data.Classe,
                                Montant = data.Montant
                            });
                    }
                    else
                    {
                        var source5 = donnée.tbl_payement.Where((d => d.Classe == Détails_Classe.classes
                        && list.Contains(d.N_Matricule) && d.Année_Scolaire == Principales.annéescolaire
                        && d.Cycle == cycle && d.Type == "Scolarité")).GroupBy(d => new
                        {
                            Matricule = d.N_Matricule,
                            Prenom = donnée.tbl_inscription
                          .Where(x => x.N_Matricule == d.N_Matricule).FirstOrDefault().Prenom,
                            Nom = donnée.tbl_inscription
                            .Where(x => x.N_Matricule == d.N_Matricule).FirstOrDefault().Nom,
                            Classe = d.Classe
                        }).Where(grs => grs.Sum((x => x.Montant)) >= scol.Tranche_2 + scol.Tranche_1 + scol.Tranche_3)
                        .OrderByDescending(grs => grs.Sum(x => x.Montant)).Select(f => new
                        {
                            N_Matricule = f.Key.Matricule,
                            Prenom = donnée.tbl_inscription
                          .Where(x => x.N_Matricule == f.Key.Matricule).FirstOrDefault().Prenom,
                            Nom = donnée.tbl_inscription
                            .Where(x => x.N_Matricule == f.Key.Matricule)
                            .FirstOrDefault().Nom,
                            Genre = donnée.tbl_inscription
                            .Where(x => x.N_Matricule == f.Key.Matricule)
                            .FirstOrDefault().Genre,
                            Classe = f.Key.Classe,
                            Montant = f.Sum(x => x.Montant)
                        });
                        foreach (var data in source5)
                            source2.Add(new tbl_payement()
                            {
                                N_Matricule = data.N_Matricule,
                                Prenom = data.Prenom,
                                Nom = data.Nom,
                                Genre = data.Genre,
                                Classe = data.Classe,
                                Montant = data.Montant
                            });
                    }
                    myTable.Montant = Convert.ToDecimal(source2.Sum((w => w.Montant)));
                    myTable.Montant_Fille = Convert.ToDecimal(source2.Where((x => x.Genre == "Feminin")).Sum((x => x.Montant)));
                    myTable.Montant_Garçon = Convert.ToDecimal(source2.Where((x => x.Genre == "Masculin")).Sum((x => x.Montant)));

                    foreach (tbl_payement tblPayement in source2)
                    {
                        DataRow row = dataTable.NewRow();
                        row[0] = tblPayement.N_Matricule;
                        row[1] = tblPayement.Prenom;
                        row[2] = tblPayement.Genre;
                        row[3] = tblPayement.Nom;
                        row[4] = tblPayement.Classe;
                        row[5] = tblPayement.Montant;
                        dataTable.Rows.Add(row);
                    }
                    myTable.Table = dataTable;
                    return myTable;
                }
                finally
                {
                    
                    if (donnée != null)
                    {
                        
                        donnée.Dispose();
                    }
                }
            }
        }

        public Task<MyTable> TrancheNonPayéeAsync(string tranche)
        => Task.Factory.StartNew((() => TrancheNonPayée(tranche)));

        public MyTable TrancheNonPayée(string tranche)
        {
            MyTable myTable = new MyTable();
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("N°_Matricule");
            dataTable.Columns.Add("Prenom");
            dataTable.Columns.Add("Genre");
            dataTable.Columns.Add("Nom");
            dataTable.Columns.Add("Classe");
            dataTable.Columns.Add("Montant");

            using (var donnée = new QuitayeContext())
            {
                try
                {
                    var scol = donnée.tbl_scolarité
                        .Where(x => x.Année_Scolaire == Principales.annéescolaire
                    && x.Classe == classes).FirstOrDefault();
                    List<tbl_payement> source1 = new List<tbl_payement>();
                    if (tranche == "1")
                    {
                        var source2 = donnée.tbl_payement.Where(d => d.Classe == Détails_Classe.classes
                        && d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle
                        && d.Type == "Scolarité").GroupBy(d => new
                        {
                            Matricule = d.N_Matricule,
                            Prenom = donnée.tbl_inscription
                            .Where(x => x.N_Matricule == d.N_Matricule).FirstOrDefault().Prenom,
                            Nom = donnée.tbl_inscription
                            .Where(x => x.N_Matricule == d.N_Matricule).FirstOrDefault().Nom,
                            Classe = d.Classe
                        }).Where(grs => grs.Sum(x => x.Montant) < scol.Tranche_1)
                        .OrderByDescending(grs => grs.Sum(x => x.Montant)).Select(d => new
                        {
                            N_Matricule = d.Key.Matricule,
                            Prenom = d.Key.Prenom,
                            Nom = d.Key.Nom,
                            Genre = donnée.tbl_inscription
                            .Where(x => x.N_Matricule == d.Key.Matricule).FirstOrDefault().Genre,
                            Classe = d.Key.Classe,
                            Montant = d.Sum((x => x.Montant))
                        });
                        foreach (var data in source1)
                            source1.Add(new tbl_payement()
                            {
                                N_Matricule = data.N_Matricule,
                                Prenom = data.Prenom,
                                Nom = data.Nom,
                                Genre = data.Genre,
                                Classe = data.Classe,
                                Montant = data.Montant
                            });
                    }
                    else if (tranche == "2")
                    {
                        var source3 = donnée.tbl_payement.Where(d => d.Classe == Détails_Classe.classes
                        && d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle
                        && d.Type == "Scolarité").GroupBy(d => new
                        {
                            Matricule = d.N_Matricule,
                            Prenom = donnée.tbl_inscription
                            .Where(x => x.N_Matricule == d.N_Matricule)
                            .FirstOrDefault().Prenom,
                            Nom = donnée.tbl_inscription
                            .Where(x => x.N_Matricule == d.N_Matricule)
                            .FirstOrDefault().Nom,
                            Classe = d.Classe
                        }).Where(y => y.Sum(x => x.Montant) < scol.Tranche_2 + scol.Tranche_1).OrderByDescending(grs => grs.Sum(x => x.Montant)).Select(d => new
                        {
                            N_Matricule = d.Key.Matricule,
                            Prenom = d.Key.Prenom,
                            Nom = d.Key.Nom,
                            Genre = donnée.tbl_inscription
                            .Where(x => x.N_Matricule == d.Key.Matricule)
                            .FirstOrDefault().Genre,
                            Classe = d.Key.Classe,
                            Montant = d.Sum(x => x.Montant)
                        });
                        foreach (var data in source3)
                            source1.Add(new tbl_payement()
                            {
                                N_Matricule = data.N_Matricule,
                                Prenom = data.Prenom,
                                Nom = data.Nom,
                                Genre = data.Genre,
                                Classe = data.Classe,
                                Montant = data.Montant
                            });
                    }
                    else
                    {
                        var source4 = donnée.tbl_payement
                                .Where(d => d.Classe == Détails_Classe.classes
                                && d.Année_Scolaire == Principales.annéescolaire
                                && d.Cycle == cycle && d.Type == "Scolarité")
                                .GroupBy(d => new
                                {
                                    Matricule = d.N_Matricule,
                                    Prenom = donnée.tbl_inscription
                            .Where(x => x.N_Matricule == d.N_Matricule)
                            .FirstOrDefault().Prenom,
                                    Nom = donnée.tbl_inscription
                            .Where(x => x.N_Matricule == d.N_Matricule)
                            .FirstOrDefault().Nom,
                                    Classe = d.Classe
                                }).Where(grs => grs.Sum(x => x.Montant) < scol.Tranche_2 + scol.Tranche_1 + scol.Tranche_3)
                        .OrderByDescending(grs => grs.Sum(x => x.Montant)).Select(x => new
                        {
                            N_Matricule = x.Key.Matricule,
                            Nom = x.Key.Nom,
                            Prenom = x.Key.Prenom,
                            Genre = donnée.tbl_inscription
                            .Where(y => y.N_Matricule == x.Key.Matricule)
                            .FirstOrDefault().Genre,
                            Classe = x.Key.Classe,
                            Montant = x.Sum(y => y.Montant)
                        });

                        foreach (var tblPayement in source4)
                            source1.Add(new tbl_payement()
                            {
                                N_Matricule = tblPayement.N_Matricule,
                                Prenom = tblPayement.Prenom,
                                Nom = tblPayement.Nom,
                                Genre = tblPayement.Genre,
                                Classe = tblPayement.Classe,
                                Montant = tblPayement.Montant
                            });
                    }
                    myTable.Montant = Convert.ToDecimal(source1.Sum(w => w.Montant));
                    myTable.Montant_Fille = Convert.ToDecimal(source1.Where((x => x.Genre == "Feminin")).Sum((x => x.Montant)));
                    myTable.Montant_Garçon = Convert.ToDecimal(source1.Where((x => x.Genre == "Masculin")).Sum((x => x.Montant)));
                    foreach (tbl_payement tblPayement in source1)
                    {
                        DataRow row = dataTable.NewRow();
                        row[0] = tblPayement.N_Matricule;
                        row[1] = tblPayement.Prenom;
                        row[2] = tblPayement.Nom;
                        row[3] = tblPayement.Genre;
                        row[4] = tblPayement.Classe;
                        row[5] = tblPayement.Montant;
                        dataTable.Rows.Add(row);
                    }
                    myTable.Table = dataTable;
                    return myTable;
                }
                catch (Exception ex)
                {
                    return null;
                }
            } 
        }

        public Task<MyTable> TrancheEnsembleAsync() => Task.Factory.StartNew(() => TrancheEnsemble());

        private async Task CallEnsemble()
        {
            MyTable myTable = await TrancheEnsembleAsync();
            result = myTable;
            myTable = null;
            dataGridView1.Columns.Clear();
            if (result.Table.Rows.Count == 0)
            {
                dataGridView1.Columns.Clear();
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée disponible dans le tableau";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                lblEffectif.Text = "Effectif Total : " + result.Effectif.ToString("N0");
                lblEffectifFille.Text = "Fille(s) :" + result.Fille.ToString("N0");
                lblEffectifGarçon.Text = "Garçon(s) :" + result.Garçon.ToString("N0");
                dt = null;
                dr = null;
            }
            else
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = result.Table;
                dataGridView2.DataSource = result.Table;
                lblEffectif.Text = "Effectif Total : " + result.Effectif.ToString("N0");
                lblEffectifFille.Text = "Fille(s) :" + result.Fille.ToString("N0");
                lblEffectifGarçon.Text = "Garçon(s) :" + result.Garçon.ToString("N0");
                DossierColumn delete = new DossierColumn();
                delete.HeaderText = "Détails";
                delete.Name = "Dossier";
                dataGridView1.Columns.Add(delete);
                dataGridView1.Columns["Dossier"].Width = 50;
                delete = null;
            }
        }

        public MyTable TrancheEnsemble()
        {
            MyTable myTable = new MyTable();
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("N_Matricule");
            dataTable.Columns.Add("Prenom");
            dataTable.Columns.Add("Nom");
            dataTable.Columns.Add("Classe");
            dataTable.Columns.Add("Montant");

            using (var donnée = new QuitayeContext())
            {
                try
                {
                    var source = donnée.tbl_payement.Where((d => d.Classe == Détails_Classe.classes 
                    && d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle 
                    && d.Type == "Scolarité")).GroupBy(d => new
                    {
                        Matricule = d.N_Matricule,
                        Prenom = donnée.tbl_inscription.Where(x => x.N_Matricule == d.N_Matricule).FirstOrDefault().Prenom,
                        Nom = donnée.tbl_inscription.Where(x => x.N_Matricule == d.N_Matricule).FirstOrDefault().Nom,
                        Classe = d.Classe
                    }).OrderByDescending(grs => grs.Sum((x => x.Montant))).Select(grs => new
                    {
                        N_Matricule = grs.Key.Matricule,
                        Prenom = grs.Key.Prenom,
                        Nom = grs.Key.Nom,
                        Genre = donnée.tbl_inscription.Where(x => x.N_Matricule == grs.Key.Matricule).FirstOrDefault().Genre,
                        Classe = grs.Key.Classe,
                        Montant = grs.Sum(x => x.Montant)
                    });
                    myTable.Montant = Convert.ToDecimal(source.Sum(w => w.Montant));
                    myTable.Montant_Fille = Convert.ToDecimal(source.Where(x => x.Genre == "Feminin").Sum(x => x.Montant));
                    myTable.Montant_Garçon = Convert.ToDecimal(source.Where(x => x.Genre == "Masculin").Sum(x => x.Montant));
                    foreach (var data in source)
                    {
                        DataRow row = dataTable.NewRow();
                        row[0] = data.N_Matricule;
                        row[1] = data.Prenom;
                        row[2] = data.Nom;
                        row[3] = data.Genre;
                        row[4] = data.Classe;
                        row[5] = data.Montant;
                        dataTable.Rows.Add(row);
                    }
                    myTable.Table = dataTable;
                    return myTable;
                }
                finally
                {
                    
                    if (donnée != null)
                    {
                        
                        donnée.Dispose();
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (temp != 1)
                return;
            timer1.Stop();
            Détails_Classe.classes = classsss;
            name = "Journal Scolarité " + Détails_Classe.classes + " " + Principales.annéescolaire + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            temp = 0;
        }

        private void btnFermer_Click(object sender, EventArgs e) => Close();

        private void radioButton1_CheckedChanged(object sender, EventArgs e) => type = ((Control)sender).Text;

        private void btnExcel_Click(object sender, EventArgs e) => Print.PrintExcelFile(dataGridView2, "Journal Scloarité", name, "Quitaye School");

        private void btnPDF_Click(object sender, EventArgs e) => Print.PrintRecuPdfFile(dataGridView2, name, "Historique Payement", "Scolarité", classsss, mycontrng, "Quitaye School", true, new Detail_Facture()
        {
            MontantTTC = result.Montant,
            Taxe = result.Montant / 100M * 18M,
            MontantHT = result.Montant - result.Montant / 100M * 18M
        }, false);

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 6)
                return;
            using (var ecoleDataContext = new QuitayeContext())
            {
                string str = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                ParameterExpression parameterExpression;
                // ISSUE: method reference
                var matri = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                var tblInscription = ecoleDataContext
                    .tbl_inscription.Where(x => x.N_Matricule ==matri)
                    .First();
                Détails_Payement détailsPayement = new Détails_Payement();
                détailsPayement.lblTitre.Text = "Détails Payement (" + tblInscription.Nom_Complet + " " + str + ")";
                Détails_Payement.classe = str;
                Détails_Payement.nom = tblInscription.Nom_Complet;
                détailsPayement.classsss = str;
                détailsPayement.nommmmm = tblInscription.Nom_Complet;
                détailsPayement.matricule = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                int num = (int)détailsPayement.ShowDialog();
            }
        }

        private void Détails_Classe_Load(object sender, EventArgs e)
        {
        }

        private async void btnTranche_Click(object sender, EventArgs e)
        {
            IconButton s = (IconButton)sender;
            if (s.Name.EndsWith("1"))
            {
                if (type == "A Jour")
                    await CallTranchePayée("1");
                else if (type == "Retard")
                    await CallTrancheNonPayée("2");
                else
                    await CallEnsemble();
            }
            else if (s.Name.EndsWith("2"))
            {
                if (type == "A Jour")
                    await CallTranchePayée("2");
                else if (type == "Retard")
                    await CallTrancheNonPayée("2");
                else
                    await CallEnsemble();
            }
            else if (s.Name.EndsWith("3"))
            {
                if (type == "A Jour")
                    await CallTranchePayée("3");
                else if (type == "Retard")
                    await CallTrancheNonPayée("2");
                else
                    await CallEnsemble();
            }
            tranche = ((Control)sender).Text;
            s = null;
        }
    }
}
                