using Quitaye_School.Models;
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
    
    public partial class Registre_Bulletin : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Registre_Bulletin()
        {
            InitializeComponent();
            Fillcbx();
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private Task<Entreprise> EcoleAsync()
        {
            return Task.Factory.StartNew(() => Ecole());
        }
        private Entreprise Ecole()
        {
            Entreprise entreprise = new Entreprise();
            using (var donnée = new QuitayeContext())
            {
                var der = (from d in donnée.tbl_entreprise where d.Id == 1 select d).First();
                entreprise.Nom = der.Nom;
                entreprise.Email = der.Email;
                entreprise.Slogan = der.Slogan;
                entreprise.Adresse = der.Adresse;
                return entreprise;
            }
        }
        

        string name;
        private void Timer_Tick(object sender, EventArgs e)
        {
            clase = cbxClasse.Text;
            exam = cbxExamen.Text;
            name = "Bulletin " + clase + " " + exam + " " + Principales.annéescolaire; 
        }

        private void FillExamen()
        {
            using(var donnée = new QuitayeContext())
            {
                var re = from d in donnée.tbl_classe where d.Nom == cbxClasse.Text select d;
                if(re.Count() != 0)
                {
                    var ss = (from d in donnée.tbl_classe where d.Nom == cbxClasse.Text select d).First();
                    var s = (from d in donnée.tbl_examen where d.Cycle == ss.Cycle orderby d.Nom select d).ToList();
                    cycle = ss.Cycle;
                    cbxExamen.DataSource = s;
                    cbxExamen.DisplayMember = "Nom";
                    cbxExamen.ValueMember = "Id";
                    cbxExamen.Text = null;
                }
            }
        }

        private void Fillcbx()
        {
            if (Principales.type_compte == "Administrateur" || Principales.departement == "Finance/Comptabilité")
            {
                using (var donnée = new QuitayeContext())
                {
                    var s = (from d in donnée.tbl_classe orderby d.Nom select d).ToList();
                    cbxClasse.DataSource = s;
                    cbxClasse.DisplayMember = "Nom";
                    cbxClasse.ValueMember = "Id";
                    cbxClasse.Text = null;
                }
            }
            else
            {
                if (Principales.role == "Agent")
                {
                    using (var donnée = new QuitayeContext())
                    {
                        var s = (from d in donnée.tbl_classe where d.Nom == Principales.classes orderby d.Nom select d).ToList();
                        cbxClasse.DataSource = s;
                        cbxClasse.DisplayMember = "Nom";
                        cbxClasse.ValueMember = "Id";
                        cbxClasse.Text = null;
                    }
                }
                else if (Principales.role == "Responsable")
                {
                    using (var donnée = new QuitayeContext())
                    {
                        var s = (from d in donnée.tbl_classe where d.Cycle == Principales.auth1 || d.Cycle == Principales.auth2 || d.Cycle == Principales.auth3 || d.Cycle == Principales.auth4 orderby d.Nom select d).ToList();
                        cbxClasse.DataSource = s;
                        cbxClasse.DisplayMember = "Nom";
                        cbxClasse.ValueMember = "Id";
                        cbxClasse.Text = null;
                    }
                }
            }
        }
        private void btnAfficher_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                if (cbxClasse.Text != "" && cbxExamen.Text != "")
                    FillDataAll();
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
        public static Custom_Bulletin[] bulletins;

        public static List<Custom_Bulletin> listbulletins;

        private async void FillDataAll()
        {
            flowLayoutPanel1.Controls.Clear();
            using(var donnée = new QuitayeContext())
            {
                if (etat == "Normal")
                {
                    var matie = (from d in donnée.tbl_matiere where d.Classe == clase select new { Matière = d.Nom, }).ToList();
                    dataGridView2.DataSource = matie;
                }
                else
                {
                    var matie = (from d in donnée.tbl_matiere where d.Classe == clase && d.Matière_Crutiale == "Oui" select new { Matière = d.Nom, }).ToList();
                    dataGridView2.DataSource = matie;
                }

                var eleves = (from d in donnée.tbl_inscription
                              where d.Année_Scolaire == Principales.annéescolaire && d.Classe == clase && d.Active == "Oui"
                              select new
                              {
                                  Id = d.Id,
                              }).ToList();
                dataGridView1.DataSource = eleves;
                int[,] array = new int[dataGridView1.RowCount, dataGridView1.ColumnCount];
                for (int x = 0; x < array.GetLength(0); x++)
                    for (int i = 0; i < array.GetLength(1); i++)
                        array[x, i] = Convert.ToInt32(dataGridView1.Rows[x].Cells[i].Value);
                bulletins = new Custom_Bulletin[array.Length];
                var entre = (from d in donnée.tbl_entreprise where d.Id == 1 select d).First();
                
                for (int i = 0; i < array.Length; i++)
                {
                    var id = array[i, 0];
                    var re = (from d in donnée.tbl_inscription where d.Id == id  select d).First();
                    matricule = re.N_Matricule;
                    var result = await FillDGAsync();
                    bulletins[i] = new Custom_Bulletin(cycle);

                    //CallDGV(bulletins[i].Data);
                    bulletins[i].Id = re.Id;
                    bulletins[i].Nom_Complet = re.Nom_Complet;
                    bulletins[i].DataSource = result;
                    //if (cycle != "Premier Cycle")
                    //{
                    //    bulletins[i].ColumnOneWidth = 170;
                    //    bulletins[i].ColumnLastWidth = 90;
                    //}
                    bulletins[i].Ecole = entre.Nom;
                    bulletins[i].Année_Scolaire = Principales.annéescolaire;
                    bulletins[i].Classe = re.Classe;
                    bulletins[i].Matricule = re.N_Matricule;
                    bulletins[i].Genre = re.Genre;
                    bulletins[i].Examen = exam;
                    bulletins[i].Nom = re.Nom;
                    bulletins[i].Prenom = re.Prenom;
                    bulletins[i].Height = (result.Rows.Count * 22) + 160;
                    flowLayoutPanel1.Controls.Add(bulletins[i]);
                }
            }
        }

        private async void FillDataAllAsync()
        {
            flowLayoutPanel1.Controls.Clear();
            using (var donnée = new QuitayeContext())
            {
                if (etat == "Normal")
                {
                    var matie = (from d in donnée.tbl_matiere where d.Classe == clase select new { Matière = d.Nom, }).ToList();
                    dataGridView2.DataSource = matie;
                }
                else
                {
                    var matie = (from d in donnée.tbl_matiere where d.Classe == clase && d.Matière_Crutiale == "Oui" select new { Matière = d.Nom, }).ToList();
                    dataGridView2.DataSource = matie;
                }

                var eleves = from d in donnée.tbl_inscription  
                             where d.Année_Scolaire == Principales.annéescolaire
                             && d.Classe == clase && d.Active == "Oui"
                             select new { Id = d.Id, };
                dataGridView1.DataSource = eleves;
                int[,] array = new int[dataGridView1.RowCount, dataGridView1.ColumnCount];
                for (int x = 0; x < array.GetLength(0); x++)
                    for (int i = 0; i < array.GetLength(1); i++)
                        array[x, i] = Convert.ToInt32(dataGridView1.Rows[x].Cells[i].Value);
                bulletins = new Custom_Bulletin[array.Length];
                var entre = (from d in donnée.tbl_entreprise where d.Id == 1 select d).First();

                //foreach (var item in bulletins)
                //{

                //}
                for (int i = 0; i < array.Length; i++)
                {
                    var re = (from d in donnée.tbl_inscription where d.Id == array[i, 0] select d).First();
                    matricule = re.N_Matricule;
                    var result = await FillDGAsync();
                    bulletins[i] = new Custom_Bulletin(cycle);

                    //CallDGV(bulletins[i].Data);
                    bulletins[i].Id = re.Id;
                    bulletins[i].Nom_Complet = re.Nom_Complet;
                    bulletins[i].DataSource = result;
                    //if (cycle != "Premier Cycle")
                    //{
                    //    bulletins[i].ColumnOneWidth = 170;
                    //    bulletins[i].ColumnLastWidth = 90;
                    //}
                    bulletins[i].Ecole = entre.Nom;
                    bulletins[i].Année_Scolaire = Principales.annéescolaire;
                    bulletins[i].Classe = re.Classe;
                    bulletins[i].Matricule = re.N_Matricule;
                    bulletins[i].Genre = re.Genre;
                    bulletins[i].Examen = exam;
                    bulletins[i].Nom = re.Nom;
                    bulletins[i].Prenom = re.Prenom;
                    bulletins[i].Height = (result.Rows.Count * 22) + 160;
                    flowLayoutPanel1.Controls.Add(bulletins[i]);
                }
            }
        }

        public string matricule;
        public string cycle;
        private void FillCbx()
        {
            using (var donnée = new QuitayeContext())
            {
                var s = (from d in donnée.tbl_examen where d.Cycle == cycle select d).ToList();
                cbxExamen.DataSource = s;
                cbxExamen.DisplayMember = "Nom";
                cbxExamen.ValueMember = "Id";
                cbxExamen.Text = null;
            }
        }

        public Task<DataTable> FillDGAsync()
        {
            try
            {
                if (etat == "Normal")
                {
                    if (cycle == "Premier Cycle" || cycle == "Maternelle" || cycle == "Crèche" || cycle == "Cente Loisir")
                        return Task.Factory.StartNew(() => FillData());
                    else return Task.Factory.StartNew(() => Filldataa());
                }
                else
                {
                    if (cycle == "Premier Cycle" || cycle == "Maternelle" || cycle == "Crèche" || cycle == "Cente Loisir")
                        return Task.Factory.StartNew(() => FillDataDEF());
                    else return Task.Factory.StartNew(() => FilldataaDEF());
                }
            }
            catch (Exception ex)
            {
                MsgBox msg = new MsgBox();
                msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Succes);
                msg.ShowDialog();
                return Task.Factory.StartNew(() => Tale());
            }
        }

        public string etat = "Normal";

        private DataTable Tale()
        {
            DataTable dt = new DataTable("dtdd");

            dt.Columns.Add("Matiere");
            dt.Columns.Add("Note");
            dt.Columns.Add("Observation");
            DataRow dr = dt.NewRow();
            dr[0] = "";
            dr[1] = "";
            dr[2] = "";
            dt.Rows.Add(dr);
            return dt;
        }

        
        
        private async void FillBulletin()
        {
            var result = await NewBulletinAsync();
            var ecole = await EcoleAsync();
            foreach (var item in result.Table)
            {
                foreach (var items in result.List_Note)
                {
                    listbulletins.Add(new Custom_Bulletin(cycle)
                    {
                        Nom_Complet = items.Nom_Complet,
                        DataSource = item,
                        Matricule = items.Matricule,
                        Année_Scolaire = Principales.annéescolaire,
                        Classe = clase,
                        Genre  = items.Genre,
                        Prenom = items.Prenom,
                        Nom = items.Nom,
                        Examen = exam,
                        Ecole = ecole.Nom,
                    });
                }
            }

            foreach (var item in listbulletins)
            {
                flowLayoutPanel1.Controls.Add(item);
            }
        }
        private Task<BulletinIndividuel> NewBulletinAsync()
        {
            return Task.Factory.StartNew(() => NewBulletin());
        }

        private BulletinIndividuel NewBulletin()
        {
            BulletinIndividuel bulletin = new BulletinIndividuel();
            
            using (var donnée = new QuitayeContext())
            {
                if(cycle == "Premier Cycle" || cycle == "Crèche" || cycle == "Matèrnelle" || cycle.StartsWith("Cent"))
                {
                    var matières = (from d in donnée.tbl_matiere where d.Classe == clase && d.Cycle == cycle select d).ToList();
                    var effectif = (from d in donnée.tbl_inscription
                                    where d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui"
                                    && d.Classe == clase
                                    select new
                                    {
                                        Id = d.Id,
                                        Matricule = d.N_Matricule,
                                        Nom_Complet = d.Nom_Complet,
                                        Nom = d.Nom,
                                        Prenom = d.Prenom,
                                        Genre = d.Genre
                                    }).ToList();
                    foreach (var item in effectif)
                    {
                        foreach (var items in matières)
                        {
                            var note = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire 
                                        && d.N_Matricule == item.Matricule 
                                        && d.Matière == items.Nom && d.Examen == exam
                                        select new
                                        {
                                            Id = d.Id,
                                            Note_Compo = d.Note_Compo
                                        }).ToList();

                            bulletin.List_Matière.Add(items.Nom);
                            foreach (var ites in note)
                            {
                                bulletin.List_Note.Add(new Matière()
                                {
                                    Matricule = item.Matricule,
                                    Matières = items.Nom,
                                    Coeff = Convert.ToInt32(items.Coefficient),
                                    Note_Compo = Convert.ToDecimal(ites.Note_Compo),
                                    Moyenne = Convert.ToDecimal(ites.Note_Compo)*Convert.ToInt32(items.Coefficient),
                                    Nom = item.Nom,
                                    Nom_Complet = item.Nom_Complet,
                                    Prenom = item.Prenom,
                                });
                            }
                        }
                    }

                    var list = (from d in bulletin.List_Note  select new { Matricule =d.Matricule }).ToList();
                    if(list.Count() != 0)
                    {
                        var lis = (from d in bulletin.List_Note
                                  group d by 
                                  new 
                                  { 
                                      Matricule = d.Matricule
                                  } into gr
                                  select new 
                                  { 
                                      Matricule = gr.Key.Matricule
                                  }).ToList();
                    }

                    foreach (var item in effectif)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Matiere");
                        dt.Columns.Add("Note");
                        dt.Columns.Add("Observation");
                        
                        var met = (from d in bulletin.List_Note group d by new { Matière = d.Matières } into gr select new { Matière = gr.Key.Matière }).ToList();
                        
                        foreach (var itese in met)
                        {
                            
                            var notes = (from d in bulletin.List_Note where d.Matricule == item.Matricule select d).ToList();
                            foreach (var items in notes)
                            {
                                var mat = (from d in bulletin.List_Note where d.Matricule == items.Matricule && d.Matières == itese.Matière select d).ToList();

                                foreach (var ites in mat)
                                {
                                    string obser = "";
                                    if(ites.Note_Compo <= 2)
                                    {
                                        obser = "Null";
                                    }else if (ites.Note_Compo <= 4)
                                    {
                                        obser = "Faible";
                                    }else if(ites.Note_Compo >= 5)
                                    {
                                        obser = "Passable";
                                    }else if(ites.Note_Compo >= 6)
                                    {
                                        obser = "Assez Bien";
                                    }else if(ites.Note_Compo >= 7)
                                    {
                                        obser = "Bien";
                                    }else if(ites.Note_Compo >= 8)
                                    {
                                        obser = "Très Bien";
                                    }else if(ites.Note_Compo >= 9)
                                    {
                                        obser = "Excellent";
                                    }

                                    DataRow dr = dt.NewRow();
                                    dr[0] = itese.Matière;
                                    dr[1] = ites.Note_Compo;
                                    dr[2] = obser;

                                    //DataRow de = dt.NewRow();
                                    //dt.Rows.Add(de);
                                    //dt.Rows.Add(de);

                                    //DataRow ds = dt.NewRow();
                                    //ds[1] = "Moyenne";
                                    //ds[2] = ites.Moyenne;

                                    dt.Rows.Add(dr);

                                }
                            }
                        }

                        bulletin.Table.Add(dt);
                    }
                }
            }

            return bulletin;
        }

        public DataTable FillData()
        {
            using (var donnée = new QuitayeContext())
            {
                var s = (from d in donnée.tbl_note
                         where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == matricule && d.Cycle == cycle && d.Examen == exam
                         orderby d.Coeff descending
                         orderby d.Matière
                         group d by new { Matière = d.Matière, AnnéeScolaire = d.Année_Scolaire, Cycle = d.Cycle, Matricule = d.N_Matricule } into gr
                         select new
                         {
                             Matière = gr.Key.Matière,
                             Année_Scolaire = gr.Key.AnnéeScolaire,
                             Cycle = gr.Key.Cycle,
                             Matricule = gr.Key.Matricule,
                         }).ToList();
                // Convert the Linq result into DataTable
                decimal coe = 0;
                decimal mgc = 0;

                
                var effectif = from d in donnée.tbl_inscription where d.Classe == clase 
                               && d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" 
                               select new { Matricule = d.N_Matricule };
                var matie = from d in donnée.tbl_matiere where d.Cycle == cycle select new { Matière = d.Nom, };
                DataTable dt = new DataTable("db" + matricule);
                var sez = (from d in donnée.tbl_inscription where d.N_Matricule == matricule select new { Id = d.Id, Genre = d.Genre }).First();

                foreach (var item in effectif)
                {
                    foreach (var items in matie)
                    {
                        string matière = items.Matière;

                        var classs = (from d in donnée.tbl_note
                                      where d.Année_Scolaire == Principales.annéescolaire 
                                      && d.Cycle == cycle && d.N_Matricule == item.Matricule 
                                      && d.Matière == matière && d.Examen == exam
                                      select new
                                      {
                                          Note = d.Note_Classe,
                                      }).ToList();
                        decimal classes = 0;
                        decimal composes = 0;
                        if (classs.Count() != 0)
                        { 
                            var clas = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire 
                                        && d.Cycle == cycle && d.N_Matricule == item.Matricule
                                        && d.Matière == matière && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        }).First();
                            classes = Convert.ToDecimal(clas.Note);
                        }

                        var compo = from d in donnée.tbl_note
                                    where d.Année_Scolaire == Principales.annéescolaire 
                                    && d.Cycle == cycle && d.N_Matricule == item.Matricule
                                    && d.Matière == matière && d.Examen == exam
                                    select new
                                    {
                                        Note = d.Note_Compo,
                                    };
                        if (compo.Count() != 0)
                        {
                            var compos = (from d in donnée.tbl_note
                                          where d.Année_Scolaire == Principales.annéescolaire 
                                          && d.Cycle == cycle && d.N_Matricule == item.Matricule
                                          && d.Matière == matière && d.Examen == exam
                                          select new
                                          {
                                              Note = d.Note_Compo,
                                          }).First();
                            composes = Convert.ToDecimal(compos.Note);
                        }
                        var coeff = (from d in donnée.tbl_matiere where d.Nom == matière && d.Cycle == cycle select d).First();
                        coe += Convert.ToDecimal(coeff.Coefficient);
                        DataRow row = dt.NewRow();

                        decimal di = coe;
                        decimal MG = Math.Round((((composes * coe)) / di), 2);


                        mgc += Convert.ToDecimal(MG);

                        //dt.Rows.Add(row);
                    }
                    if (mgc != 0 && coe != 0)
                    {
                        decimal moyen = Math.Round(Convert.ToDecimal(mgc / matie.Count()), 2);
                        list_moyenne.Add(new Info_Moyenne() { Moyenne = moyen, Matricule = item.Matricule });
                        coe = 0;
                        mgc = 0;
                    }
                    coe = 0;
                    mgc = 0;
                }

                dt.Rows.Clear();


                try
                {
                    dt.Columns.Add("Matiere");
                    dt.Columns.Add("Note");
                    dt.Columns.Add("Observation");
                    matie = from d in donnée.tbl_matiere where d.Cycle == cycle select new { Matière = d.Nom, };
                    //int lengh = matie.Count();
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        string matière = dataGridView2.Rows[i].Cells[0].Value.ToString();

                        var classs = from d in donnée.tbl_note
                                     where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle 
                                     && d.N_Matricule == matricule && d.Matière == matière && d.Examen == exam
                                     select new
                                     {
                                         Note = d.Note_Classe,
                                     };
                        decimal classes = 0;
                        decimal composes = 0;
                        if (classs.Count() != 0)
                        {
                            var clas = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle 
                                        && d.N_Matricule == matricule && d.Matière == matière && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        }).First();
                            classes = Convert.ToDecimal(clas.Note);
                        }

                        var compo = from d in donnée.tbl_note
                                    where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle 
                                    && d.N_Matricule == matricule && d.Matière == matière && d.Examen == exam
                                    select new
                                    {
                                        Note = d.Note_Compo,
                                    };
                        if (compo.Count() != 0)
                        {
                            var compos = (from d in donnée.tbl_note
                                          where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle 
                                          && d.N_Matricule == matricule && d.Matière == matière && d.Examen == exam
                                          select new
                                          {
                                              Note = d.Note_Compo,
                                          }).First();
                            composes = Convert.ToDecimal(compos.Note);
                        }
                        var coeff = (from d in donnée.tbl_matiere where d.Nom == matière select d).First();
                        coe += Convert.ToDecimal(coeff.Coefficient);
                        DataRow row = dt.NewRow();

                        decimal MG = Math.Round(Convert.ToDecimal(Convert.ToDecimal(composes)), 2);
                        string observation = "";
                        if (MG < 1)
                            observation = "Null";
                        else if (MG < 2)
                            observation = "Mal";
                        else if (MG < 3)
                            observation = "Mediocre";
                        else if (MG < 4)
                            observation = "Faible";
                        else if (MG < 5)
                            observation = "Insufissant";
                        else if (MG < 6)
                            observation = "Passable";
                        else if (MG < 7)
                            observation = "Assez-Bien";
                        else if (MG < 8)
                            observation = "Bien";
                        else if (MG < 9)
                            observation = "Tres Bien";
                        else if (MG < 10)
                            observation = "Excellent";

                        row[0] = matière;
                        row[1] = composes;
                        row[2] = observation;
                        //row[7] = "Des";
                        mgc += Convert.ToDecimal(MG * coeff.Coefficient);
                        dt.Rows.Add(row);
                    }

                    dt.Rows.Add();

                    DataRow dr = dt.NewRow();
                    dr[0] = "Total";
                    dr[1] = coe;
                    dr[2] = mgc;
                    dt.Rows.Add(dr);

                    if (exam!= "")
                    {
                        dt.Rows.Add();

                        string observation = "";
                        decimal moyen = 0;
                        if (mgc != 0 && coe != 0)
                        {
                            moyen = Math.Round(Convert.ToDecimal(mgc / coe), 2);
                        }
                        if (moyen < 1)
                            observation = "Null";
                        else if (moyen < 2)
                            observation = "Mal";
                        else if (moyen < 3)
                            observation = "Mediocre";
                        else if (moyen < 4)
                            observation = "Faible";
                        else if (moyen < 5)
                            observation = "Insufissant";
                        else if (moyen < 6)
                            observation = "Passable";
                        else if (moyen < 7)
                            observation = "Assez-Bien";
                        else if (moyen < 8)
                            observation = "Bien";
                        else if (moyen < 9)
                            observation = "Tres Bien";
                        else if (moyen < 10)
                            observation = "Excellent";
                        DataRow drs = dt.NewRow();
                        drs[0] = "Moyenne Compo";
                        drs[1] = moyen;
                        drs[2] = observation;
                        var result = list_moyenne.Where(x => x.Moyenne > moyen).ToArray();
                        dt.Rows.Add(drs);
                        int count = Convert.ToInt32(result.Count());
                        dt.Rows.Add();
                        DataRow dre = dt.NewRow();
                        dre[0] = "Rang";
                        if (count == 0)
                        {
                            if (sez.Genre == "Masculin")
                                dre[2] = "1er";
                            else dre[2] = "1ere";
                        }

                        else
                            dre[2] = (count + 1) + "eme";
                        dt.Rows.Add(dre);
                    }
                    // Databind

                    return dt;
                }
                catch (Exception ex)
                {
                    donnée.Dispose();

                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                    return dt;
                }

            }
        }
        List<Info_Moyenne> list_moyenne = new List<Info_Moyenne>();
        public DataTable Filldataa()
        {
            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_note
                        where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == matricule 
                        && d.Cycle == cycle && d.Examen == exam
                        orderby d.Matière
                        group d by new
                        {
                            Matière = d.Matière,
                            AnnéeScolaire = d.Année_Scolaire,
                            Cycle = d.Cycle,
                            Matricule = d.N_Matricule
                        } into gr
                        select new
                        {
                            Matière = gr.Key.Matière,
                            Année_Scolaire = gr.Key.AnnéeScolaire,
                            Cycle = gr.Key.Cycle,
                            Matricule = gr.Key.Matricule,
                        };
                // Convert the Linq result into DataTable
                DataTable dt = new DataTable("db"+matricule);

                decimal coe = 0;
                decimal mgc = 0;

                var effectif = from d in donnée.tbl_inscription where d.Classe == clase 
                               && d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" 
                               select new { Matricule = d.N_Matricule };
                var matie = from d in donnée.tbl_matiere where d.Cycle == cycle select new { Matière = d.Nom, };
                var sez = (from d in donnée.tbl_inscription where d.N_Matricule == matricule select d).First();
                //int lengh = matie.Count();
                
                foreach (var item in effectif)
                {


                    foreach (var items in matie)
                    {

                    
                        string matière = items.Matière;

                        var classs = from d in donnée.tbl_note
                                     where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle 
                                     && d.N_Matricule == item.Matricule 
                                     && d.Matière == matière && d.Examen == exam
                                     select new
                                     {
                                         Note = d.Note_Classe,
                                     };
                        decimal classes = 0;
                        decimal composes = 0;
                        if (classs.Count() != 0)
                        {
                            var clas = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle 
                                        && d.N_Matricule == item.Matricule 
                                        && d.Matière == matière && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        }).First();
                            classes = Convert.ToDecimal(clas.Note);
                        }

                        var compo = from d in donnée.tbl_note
                                    where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle 
                                    && d.N_Matricule == item.Matricule 
                                    && d.Matière == matière && d.Examen == exam
                                    select new
                                    {
                                        Note = d.Note_Compo,
                                    };
                        if (compo.Count() != 0)
                        {
                            var compos = (from d in donnée.tbl_note
                                          where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle 
                                          && d.N_Matricule == item.Matricule 
                                          && d.Matière == matière && d.Examen == exam
                                          select new
                                          {
                                              Note = d.Note_Compo,
                                          }).First();
                            composes = Convert.ToDecimal(compos.Note);
                        }
                        var coeff = (from d in donnée.tbl_matiere 
                                     where d.Nom == items.Matière select d).First();
                        coe += Convert.ToDecimal(coeff.Coefficient);
                        DataRow row = dt.NewRow();

                        decimal di = coe + 1;
                        
                        decimal MG = Math.Round((((classes + (composes * coe)) / di)) / 2, 2);

                        
                        mgc += Convert.ToDecimal(MG);

                        dt.Rows.Add(row);
                    }
                    if (mgc != 0 && coe != 0)
                    {
                        decimal moyen = Math.Round(Convert.ToDecimal(mgc / matie.Count()), 2);
                        
                        list_moyenne.Add(new Info_Moyenne()
                        {
                            Matricule = item.Matricule,
                            Moyenne = moyen,
                        });
                        coe = 0;
                        mgc = 0;
                    }
                    coe = 0;
                    mgc = 0;
                }

                dt.Rows.Clear();

                try
                {
                    dt.Columns.Add("Matiere");
                    dt.Columns.Add("N.Classe");
                    dt.Columns.Add("N.Compo");
                    dt.Columns.Add("M.Gén");
                    dt.Columns.Add("Coeff");
                    dt.Columns.Add("M.G.C");
                    dt.Columns.Add("Observation");

                    //int lengh = matie.Count();
                    foreach (var item in matie)
                    {

                    
                        string matière = item.Matière;
                        var classs = from d in donnée.tbl_note
                                     where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle 
                                     && d.N_Matricule == matricule && d.Matière == item.Matière 
                                     && d.Examen == exam
                                     select new
                                     {
                                         Note = d.Note_Classe,
                                     };
                        decimal classes = 0;
                        decimal composes = 0;
                        if (classs.Count() != 0)
                        {
                            var clas = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle 
                                        && d.N_Matricule == matricule && d.Matière ==  matière
                                        && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        }).First();
                            classes = Convert.ToDecimal(clas.Note);
                        }

                        var compo = from d in donnée.tbl_note
                                    where d.Année_Scolaire == Principales.annéescolaire 
                                    && d.Cycle == cycle && d.N_Matricule == matricule 
                                    && d.Matière == item.Matière && d.Examen == exam
                                    select new
                                    {
                                        Note = d.Note_Compo,
                                    };
                        if (compo.Count() != 0)
                        {
                            var compos = (from d in donnée.tbl_note
                                          where d.Année_Scolaire == Principales.annéescolaire 
                                          && d.Cycle == cycle && d.N_Matricule == matricule 
                                          && d.Matière == matière && d.Examen == exam
                                          select new
                                          {
                                              Note = d.Note_Compo,
                                          }).First();
                            composes = Convert.ToDecimal(compos.Note);
                        }
                        var coeff = (from d in donnée.tbl_matiere where d.Nom == matière select d).First();
                        coe += Convert.ToDecimal(coeff.Coefficient);
                        DataRow row = dt.NewRow();

                        decimal di = coe + 1;
                        decimal MG = Math.Round((((classes + (composes * coe)) / di)) / 2, 2);

                        string observation = "";
                        if (MG < 2)
                            observation = "Null";
                        else if (MG < 4)
                            observation = "Mal";
                        else if (MG < 6)
                            observation = "Mediocre";
                        else if (MG < 8)
                            observation = "Faible";
                        else if (MG < 10)
                            observation = "Insufissant";
                        else if (MG < 12)
                            observation = "Passable";
                        else if (MG < 14)
                            observation = "Assez-Bien";
                        else if (MG < 16)
                            observation = "Bien";
                        else if (MG < 18)
                            observation = "Tres Bien";
                        else if (MG < 20)
                            observation = "Excellent";

                        row[0] = item.Matière;
                        row[1] = classes;
                        row[2] = composes;
                        row[3] = MG;
                        row[4] = coeff.Coefficient;
                        row[5] = MG * coeff.Coefficient;
                        mgc += Convert.ToDecimal(MG * coeff.Coefficient);
                        row[6] = observation;
                        dt.Rows.Add(row);
                    }

                    dt.Rows.Add();

                    DataRow dr = dt.NewRow();
                    dr[0] = "Total";
                    dr[4] = coe;
                    dr[5] = mgc;
                    dt.Rows.Add(dr);

                    if (exam != "")
                    {
                        dt.Rows.Add();

                        decimal moyen = 0;
                        if (mgc != 0 && coe != 0)
                        {
                            moyen = Math.Round(Convert.ToDecimal(mgc / coe), 2);
                        }
                        string observation = "";
                        if (moyen < 2)
                            observation = "Null";
                        else if (moyen < 4)
                            observation = "Mal";
                        else if (moyen < 6)
                            observation = "Mediocre";
                        else if (moyen < 8)
                            observation = "Faible";
                        else if (moyen < 10)
                            observation = "Insufissant";
                        else if (moyen < 12)
                            observation = "Passable";
                        else if (moyen < 14)
                            observation = "Assez-Bien";
                        else if (moyen < 16)
                            observation = "Bien";
                        else if (moyen < 18)
                            observation = "Tres Bien";
                        else if (moyen < 20)
                            observation = "Excellent";
                        DataRow drs = dt.NewRow();
                        drs[0] = "Moyenne du Trimestre";
                        drs[5] = moyen;
                        drs[6] = observation;
                        dt.Rows.Add(drs);

                        var result = list_moyenne.Where(x => x.Moyenne > moyen).ToArray();
                        //dt.Rows.Add(drs);
                        int count = Convert.ToInt32(result.Count());
                        dt.Rows.Add();
                        DataRow dre = dt.NewRow();
                        dre[0] = "Rang";
                        if (count == 0)
                        {
                            if (sez.Genre == "Masculin")
                                dre[6] = "1er";
                            else dre[6] = "1ere";
                        }
                        else
                            dre[6] = (count + 1) + "eme";
                        dt.Rows.Add(dre);
                    }

                    list_moyenne = new List<Info_Moyenne>();
                    // Databind

                    return dt;
                }
                catch (Exception ex)
                {
                    donnée.Dispose();

                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                    return dt;
                }
            }
        }

        public DataTable FillDataDEF()
        {
            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_note
                        where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == matricule 
                        && d.Cycle == cycle && d.Examen == exam
                        orderby d.Coeff descending
                        orderby d.Matière
                        group d by new
                        {
                            Matière = d.Matière,
                            AnnéeScolaire = d.Année_Scolaire,
                            Cycle = d.Cycle,
                            Matricule = d.N_Matricule
                        } into gr
                        select new
                        {
                            Matière = gr.Key.Matière,
                            Année_Scolaire = gr.Key.AnnéeScolaire,
                            Cycle = gr.Key.Cycle,
                            Matricule = gr.Key.Matricule,
                        };
                // Convert the Linq result into DataTable
                decimal coe = 0;
                decimal mgc = 0;

                DataTable dt = new DataTable("db"+matricule);
                var effectif = from d in donnée.tbl_inscription where d.Classe == clase 
                               && d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" 
                               select new { Matricule = d.N_Matricule };
                var matie = from d in donnée.tbl_matiere where d.Cycle == cycle 
                            && d.Matière_Crutiale == "Oui" select new { Matière = d.Nom, };
                var sez = (from d in donnée.tbl_inscription where d.N_Matricule == matricule select d).First();
                //int lengh = matie.Count();
                dataGridView3.DataSource = effectif;
                foreach (var item in effectif)
                {


                    foreach (var items in matie)
                    {


                        string matière = items.Matière;

                        var classs = from d in donnée.tbl_note
                                     where d.Année_Scolaire == Principales.annéescolaire 
                                     && d.Cycle == cycle && d.N_Matricule == item.Matricule 
                                     && d.Matière == matière && d.Examen == exam
                                     select new
                                     {
                                         Note = d.Note_Classe,
                                     };
                        decimal classes = 0;
                        decimal composes = 0;
                        if (classs.Count() != 0)
                        {
                            var clas = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire 
                                        && d.Cycle == cycle && d.N_Matricule == item.Matricule 
                                        && d.Matière == matière && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        }).First();
                            classes = Convert.ToDecimal(clas.Note);
                        }

                        var compo = from d in donnée.tbl_note
                                    where d.Année_Scolaire == Principales.annéescolaire 
                                    && d.Cycle == cycle && d.N_Matricule == item.Matricule
                                    && d.Matière == matière && d.Examen == exam
                                    select new
                                    {
                                        Note = d.Note_Compo,
                                    };
                        if (compo.Count() != 0)
                        {
                            var compos = (from d in donnée.tbl_note
                                          where d.Année_Scolaire == Principales.annéescolaire 
                                          && d.Cycle == cycle && d.N_Matricule == item.Matricule
                                          && d.Matière == matière && d.Examen == exam
                                          select new
                                          {
                                              Note = d.Note_Compo,
                                          }).First();
                            composes = Convert.ToDecimal(compos.Note);
                        }
                        var coeff = (from d in donnée.tbl_matiere where d.Nom == matière select d).First();
                        coe += Convert.ToDecimal(coeff.Coefficient);
                        DataRow row = dt.NewRow();

                        decimal MG = Math.Round(Convert.ToDecimal(Convert.ToDecimal(composes)), 2);

                        mgc += Convert.ToDecimal(MG * coeff.Coefficient);

                        
                        //dt.Rows.Add(row);
                    }

                    if(mgc != 0 && coe != 0)
                    {
                        decimal moyen = Math.Round(Convert.ToDecimal(mgc / coe), 2);
                        list_moyenne.Add(new Info_Moyenne()
                        {
                            Matricule = item.Matricule,
                            Moyenne = moyen,
                        });
                        coe = 0;
                        mgc = 0;
                    }
                    coe = 0;
                    mgc = 0;
                }

                dt.Rows.Clear();


                try
                {
                    dt.Columns.Add("Matiere");
                    dt.Columns.Add("Note");
                    dt.Columns.Add("Observation");
                    matie = from d in donnée.tbl_matiere where d.Cycle == cycle select new { Matière = d.Nom, };
                    //int lengh = matie.Count();
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        string matière = dataGridView2.Rows[i].Cells[0].Value.ToString();

                        var classs = from d in donnée.tbl_note
                                     where d.Année_Scolaire == Principales.annéescolaire 
                                     && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == matière && d.Examen == exam
                                     select new
                                     {
                                         Note = d.Note_Classe,
                                     };
                        decimal classes = 0;
                        decimal composes = 0;
                        if (classs.Count() != 0)
                        {
                            var clas = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire 
                                        && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == matière 
                                        && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        }).First();
                            classes = Convert.ToDecimal(clas.Note);
                        }

                        var compo = from d in donnée.tbl_note
                                    where d.Année_Scolaire == Principales.annéescolaire 
                                    && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == matière && d.Examen == exam
                                    select new
                                    {
                                        Note = d.Note_Compo,
                                    };
                        if (compo.Count() != 0)
                        {
                            var compos = (from d in donnée.tbl_note
                                          where d.Année_Scolaire == Principales.annéescolaire 
                                          && d.Cycle == cycle && d.N_Matricule == matricule 
                                          && d.Matière == matière && d.Examen == exam
                                          select new
                                          {
                                              Note = d.Note_Compo,
                                          }).First();
                            composes = Convert.ToDecimal(compos.Note);
                        }
                        var coeff = (from d in donnée.tbl_matiere where d.Nom == matière select d).First();
                        coe += Convert.ToDecimal(coeff.Coefficient);
                        DataRow row = dt.NewRow();

                        decimal MG = Math.Round(Convert.ToDecimal(Convert.ToDecimal(composes)), 2);
                        string observation = "";
                        if (MG < 1)
                            observation = "Null";
                        else if (MG < 2)
                            observation = "Mal";
                        else if (MG < 3)
                            observation = "Mediocre";
                        else if (MG < 4)
                            observation = "Faible";
                        else if (MG < 5)
                            observation = "Insufissant";
                        else if (MG < 6)
                            observation = "Passable";
                        else if (MG < 7)
                            observation = "Assez-Bien";
                        else if (MG < 8)
                            observation = "Bien";
                        else if (MG < 9)
                            observation = "Tres Bien";
                        else if (MG < 10)
                            observation = "Excellent";

                        row[0] = matière;
                        row[1] = composes;
                        row[2] = observation;
                        //row[7] = "Des";
                        mgc += Convert.ToDecimal(MG * coeff.Coefficient);
                        dt.Rows.Add(row);
                    }

                    dt.Rows.Add();

                    DataRow dr = dt.NewRow();
                    dr[0] = "Total";
                    dr[1] = coe;
                    dr[2] = mgc;
                    dt.Rows.Add(dr);

                    if (exam != "")
                    {
                        dt.Rows.Add();

                        string observation = "";
                        decimal moyen = 0;
                        if (mgc != 0 && coe != 0)
                        {
                            moyen = Math.Round(Convert.ToDecimal(mgc / coe), 2);
                        }
                        //decimal moyen = Math.Round(Convert.ToDecimal(mgc / coe), 2);
                        if (moyen < 1)
                            observation = "Null";
                        else if (moyen < 2)
                            observation = "Mal";
                        else if (moyen < 3)
                            observation = "Mediocre";
                        else if (moyen < 4)
                            observation = "Faible";
                        else if (moyen < 5)
                            observation = "Insufissant";
                        else if (moyen < 6)
                            observation = "Passable";
                        else if (moyen < 7)
                            observation = "Assez-Bien";
                        else if (moyen < 8)
                            observation = "Bien";
                        else if (moyen < 9)
                            observation = "Tres Bien";
                        else if (moyen < 10)
                            observation = "Excellent";
                        DataRow drs = dt.NewRow();
                        drs[0] = "Moyenne Compo";
                        drs[1] = moyen;
                        drs[2] = observation;
                        var result = list_moyenne.Where(x => x.Moyenne > moyen).ToArray();
                        dt.Rows.Add(drs);
                        int count = Convert.ToInt32(result.Count());
                        dt.Rows.Add();
                        DataRow dre = dt.NewRow();
                        dre[0] = "Rang";
                        if (count == 0)
                        {
                            if (sez.Genre == "Masculin")
                                dre[2] = "1er";
                            else dre[2] = "1ere";
                        }
                        else
                            dre[2] = (count + 1) + "eme";
                        dt.Rows.Add(dre);
                    }


                    // Databind

                    return dt;

                }
                catch (Exception ex)
                {
                    donnée.Dispose();

                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                    return dt;
                }

            }
        }
        public DataTable FilldataaDEF()
        {
            //ts = EndDate.Value.Date - startDate.Value.Date;
            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_note
                        where d.Année_Scolaire == Principales.annéescolaire 
                        && d.N_Matricule == matricule && d.Cycle == cycle && d.Examen == exam
                        orderby d.Coeff descending
                        orderby d.Matière
                        group d by new
                        {
                            Matière = d.Matière,
                            AnnéeScolaire = d.Année_Scolaire,
                            Cycle = d.Cycle,
                            Matricule = d.N_Matricule
                        } into gr
                        select new
                        {
                            Matière = gr.Key.Matière,
                            Année_Scolaire = gr.Key.AnnéeScolaire,
                            Cycle = gr.Key.Cycle,
                            Matricule = gr.Key.Matricule,
                        };
                // Convert the Linq result into DataTable
                DataTable dt = new DataTable("tbl_inventaire");

                decimal coe = 0;
                decimal mgc = 0;


                var effectif = from d in donnée.tbl_inscription where d.Classe == clase 
                               && d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" 
                               select new { Matricule = d.N_Matricule };
                var matie = from d in donnée.tbl_matiere where d.Cycle == cycle 
                            && d.Matière_Crutiale == "Oui" select new { Matière = d.Nom, };
                var sez = (from d in donnée.tbl_inscription where d.N_Matricule == matricule select d).First();
                //int lengh = matie.Count();
                
                foreach (var item in effectif)
                {

                    foreach (var items in matie)
                    {

                        var classs = from d in donnée.tbl_note
                                     where d.Année_Scolaire == Principales.annéescolaire 
                                     && d.Cycle == cycle && d.N_Matricule == item.Matricule
                                     && d.Matière == items.Matière && d.Examen == exam
                                     select new
                                     {
                                         Note = d.Note_Classe,
                                     };
                        decimal classes = 0;
                        decimal composes = 0;
                        if (classs.Count() != 0)
                        {
                            var clas = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire 
                                        && d.Cycle == cycle && d.N_Matricule == item.Matricule 
                                        && d.Matière == items.Matière && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        }).First();
                            classes = Convert.ToDecimal(clas.Note);
                        }

                        var compo = from d in donnée.tbl_note
                                    where d.Année_Scolaire == Principales.annéescolaire 
                                    && d.Cycle == cycle && d.N_Matricule == item.Matricule 
                                    && d.Matière == items.Matière && d.Examen == exam
                                    select new
                                    {
                                        Note = d.Note_Compo,
                                    };
                        if (compo.Count() != 0)
                        {
                            var compos = (from d in donnée.tbl_note
                                          where d.Année_Scolaire == Principales.annéescolaire 
                                          && d.Cycle == cycle && d.N_Matricule == item.Matricule 
                                          && d.Matière == items.Matière && d.Examen == exam
                                          select new
                                          {
                                              Note = d.Note_Compo,
                                          }).First();
                            composes = Convert.ToDecimal(compos.Note);
                        }
                        var coeff = (from d in donnée.tbl_matiere where d.Nom == items.Matière select new { Id = d.Id, Nom = d.Nom, Coefficient = d.Coefficient }).First();
                        coe += Convert.ToDecimal(coeff.Coefficient);
                        DataRow row = dt.NewRow();

                        decimal di = coe + 1;
                        decimal MG = Math.Round(((classes + (composes * coe)) / di), 2);

                        mgc += Convert.ToDecimal(MG);

                        dt.Rows.Add(row);
                    }
                    if (mgc != 0 && coe != 0)
                    {
                        decimal moyen = Math.Round(Convert.ToDecimal(mgc / matie.Count()), 2);
                        list_moyenne.Add(new Info_Moyenne()
                        {
                            Matricule = item.Matricule,
                            Moyenne = moyen,
                        });
                        coe = 0;
                        mgc = 0;
                    }
                    coe = 0;
                    mgc = 0;
                }

                dt.Rows.Clear();

                try
                {
                    dt.Columns.Add("Matiere");
                    dt.Columns.Add("N.Classe");
                    dt.Columns.Add("N.Compo");
                    dt.Columns.Add("M.Gén");
                    dt.Columns.Add("Coeff");
                    dt.Columns.Add("M.G.C");
                    dt.Columns.Add("Observation");

                    //int lengh = matie.Count();
                    foreach (var item in matie)
                    {

                        var classs = from d in donnée.tbl_note
                                     where d.Année_Scolaire == Principales.annéescolaire 
                                     && d.Cycle == cycle && d.N_Matricule == matricule 
                                     && d.Matière == item.Matière && d.Examen == exam
                                     select new
                                     {
                                         Note = d.Note_Classe,
                                     };
                        decimal classes = 0;
                        decimal composes = 0;
                        if (classs.Count() != 0)
                        {
                            var clas = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire 
                                        && d.Cycle == cycle && d.N_Matricule == matricule 
                                        && d.Matière == item.Matière && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        }).First();
                            classes = Convert.ToDecimal(clas.Note);
                        }

                        var compo = from d in donnée.tbl_note
                                    where d.Année_Scolaire == Principales.annéescolaire 
                                    && d.Cycle == cycle && d.N_Matricule == matricule 
                                    && d.Matière == item.Matière && d.Examen == exam
                                    select new
                                    {
                                        Note = d.Note_Compo,
                                    };
                        if (compo.Count() != 0)
                        {
                            var compos = (from d in donnée.tbl_note
                                          where d.Année_Scolaire == Principales.annéescolaire 
                                          && d.Cycle == cycle && d.N_Matricule == matricule 
                                          && d.Matière == item.Matière && d.Examen == exam
                                          select new
                                          {
                                              Note = d.Note_Compo,
                                          }).First();
                            composes = Convert.ToDecimal(compos.Note);
                        }
                        var coeff = (from d in donnée.tbl_matiere where d.Nom == item.Matière select d).First();
                        coe += Convert.ToDecimal(coeff.Coefficient);
                        DataRow row = dt.NewRow();

                        decimal di = coe + 1;
                        decimal MG = Math.Round((((classes + (composes * coe)) / di))/2, 2);

                        string observation = "";
                        if (MG < 2)
                            observation = "Null";
                        else if (MG < 4)
                            observation = "Mal";
                        else if (MG < 6)
                            observation = "Mediocre";
                        else if (MG < 8)
                            observation = "Faible";
                        else if (MG < 10)
                            observation = "Insufissant";
                        else if (MG < 12)
                            observation = "Passable";
                        else if (MG < 14)
                            observation = "Assez-Bien";
                        else if (MG < 16)
                            observation = "Bien";
                        else if (MG < 18)
                            observation = "Tres Bien";
                        else if (MG < 20)
                            observation = "Excellent";

                        row[0] = item.Matière;
                        row[1] = classes;
                        row[2] = composes;
                        row[3] = MG;
                        row[4] = coeff.Coefficient;
                        row[5] = MG*coeff.Coefficient;
                        mgc += Convert.ToDecimal(MG);
                        row[6] = observation;
                        dt.Rows.Add(row);
                    }

                    dt.Rows.Add();

                    DataRow dr = dt.NewRow();
                    dr[0] = "Total";
                    dr[4] = coe;
                    dr[5] = mgc;
                    dt.Rows.Add(dr);

                    if (exam != "")
                    {
                        dt.Rows.Add();

                        decimal moyen = 0;
                        if (mgc != 0 && coe != 0)
                        {
                            moyen = Math.Round(Convert.ToDecimal(mgc / matie.Count()), 2);
                        }
                        string observation = "";
                        if (moyen < 2)
                            observation = "Null";
                        else if (moyen < 4)
                            observation = "Mal";
                        else if (moyen < 6)
                            observation = "Mediocre";
                        else if (moyen < 8)
                            observation = "Faible";
                        else if (moyen < 10)
                            observation = "Insufissant";
                        else if (moyen < 12)
                            observation = "Passable";
                        else if (moyen < 14)
                            observation = "Assez-Bien";
                        else if (moyen < 16)
                            observation = "Bien";
                        else if (moyen < 18)
                            observation = "Tres Bien";
                        else if (moyen < 20)
                            observation = "Excellent";
                        DataRow drs = dt.NewRow();
                        drs[0] = "Moyenne du Trimestre";
                        drs[5] = moyen;
                        drs[6] = observation;
                        dt.Rows.Add(drs);

                        var result = list_moyenne.Where(x => x.Moyenne > moyen).ToArray();
                        //dt.Rows.Add(drs);
                        int count = Convert.ToInt32(result.Count());
                        dt.Rows.Add();
                        DataRow dre = dt.NewRow();
                        dre[0] = "Rang";
                        if (count == 0)
                        {
                            if (sez.Genre == "Masculin")
                                dre[6] = "1er";
                            else dre[6] = "1ere";
                        }
                        else
                            dre[6] = (count + 1) + "eme";
                        dt.Rows.Add(dre);
                    }


                    // Databind

                    return dt;
                }
                catch (Exception ex)
                {
                    donnée.Dispose();

                    MsgBox msg = new MsgBox();
                    msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                    msg.ShowDialog();
                    return dt;
                }


            }
        }

        Timer timer = new Timer();

        int count = 0;
        string clase;
        string exam;
        private void cbxClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //timer.Start();
            count = cbxClasse.Text.Length;
            if(cbxClasse.Text.Length > 0)
            {
                FillExamen();
            }
        }

        Timer timer1 = new Timer();
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            etat = ((RadioButton)sender).Text;
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            List<DataGridView> datas = new List<DataGridView>();
            List<string> listeleves = new List<string>();
            List<string> listmatricule = new List<string>();
            for (int i = 0; i < bulletins.Length; i++)
            {
                datas.Add(bulletins[i].dataGridView1);
                listeleves.Add(bulletins[i].Nom_Complet);
                listmatricule.Add(bulletins[i].Matricule);
            }
            var donnée = new QuitayeContext();
            var s = (from d in donnée.tbl_entreprise where d.Id == 1 select d).First();
            PrintAction.Print.PrintPdfBulletin(name + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss"), mycontrng, datas.ToArray(), "Quitaye School", s.Nom, cbxExamen.Text, cbxClasse.Text, s.Téléphone, Principales.annéescolaire, listeleves, listmatricule, true) ;
        }
    }

    public class BulletinIndividuel
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private List<Matière> _list_note;

        public List<Matière> List_Note
        {
            get { return _list_note; }
            set { _list_note = value; }
        }

        private List<Matière> _list_note_individuell;

        public List<Matière> List_Note_Perso
        {
            get { return _list_note_individuell; }
            set { _list_note_individuell = value; }
        }

        private List<string> _list_matière;

        public List<string> List_Matière
        {
            get { return _list_matière; }
            set { _list_matière = value; }
        }


        private List<DataTable> dataTable;

        public List<DataTable> Table
        {
            get { return dataTable; }
            set { dataTable = value; }
        }

        private string _rang;

        public string Rang
        {
            get { return _rang; }
            set { _rang = value; }
        }


        private decimal _moyenne;

        public decimal Moyenne
        {
            get { return _moyenne; }
            set { _moyenne = value; }
        }


    }

    public class Matière
    {
        private decimal _note_classe;

        public decimal Note_Classe
        {
            get { return _note_classe; }
            set { _note_classe = value; }
        }

        private int _coeff;

        public int Coeff
        {
            get { return _coeff; }
            set { _coeff = value; }
        }

        private decimal _moyenne;

        public decimal Moyenne
        {
            get { return _moyenne; }
            set { _moyenne = value; }
        }

        private string _nom_complet;

        public string Nom_Complet
        {
            get { return _nom_complet; }
            set { _nom_complet = value; }
        }
        private string _prenom;

        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }


        private string _genre;

        public string Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }


        private decimal _note_compo;

        public decimal Note_Compo
        {
            get { return _note_compo; }
            set { _note_compo = value; }
        }


        private string _matricule;

        public string Matricule
        {
            get { return _matricule; }
            set { _matricule = value; }
        }

        private string _eleve;

        public string Nom
        {
            get { return _eleve; }
            set { _eleve = value; }
        }

        private string _matière;

        public string Matières
        {
            get { return _matière; }
            set { _matière = value; }
        }

    }
    public class Entreprise
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _nom;

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _adresse;

        public string Adresse
        {
            get { return _adresse; }
            set { _adresse = value; }
        }

        private string _slogan;

        public string Slogan
        {
            get { return _slogan; }
            set { _slogan = value; }
        }


    }

}
