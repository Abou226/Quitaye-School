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
    public partial class Bulletin : Form
    {
        string mycontrng = LogIn.mycontrng;
        public Bulletin()
        {
            InitializeComponent();
            FillCbx();
            //CallFill(dataGridView1);
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
            timer1.Enabled = false;
            timer1.Interval = 10;
            timer1.Start();
            timer1.Tick += Timer1_Tick;
            //CallDGV(dataGridView1);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            FillCbx();
        }

        Timer timer1 = new Timer();
        string exam;
        public string clase;
        string name;
        private void Timer_Tick(object sender, EventArgs e)
        {
            name = DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            exam = cbxExamen.Text;
            
        }

        Timer timer = new Timer();
        private async void CallDGV(DataGridView dgv)
        {
            if(etat == "Normal")
            {
                using (var donnée = new QuitayeContext())
                {
                    var matie = from d in donnée.tbl_matiere where d.Classe == clase  select new { Matière = d.Nom, };
                    dataGridView2.DataSource = matie;
                }
            }else
            {
                using (var donnée = new QuitayeContext())
                {
                    var matie = (from d in donnée.tbl_matiere where d.Classe == clase && d.Matière_Crutiale == "Oui" select new { Matière = d.Nom, }).ToList();
                    dataGridView2.DataSource = matie;
                }
            }
            var result = await FillDGAsync();
            dgv.Columns.Clear();
            dgv.DataSource = result;
            if(cycle != "Premier Cycle")
            {
                dgv.Columns[0].Width = 170;
                dgv.Columns[6].Width = 90;
            }
            //dgv.Rows.Add();
        }

        public string matricule;
        public string cycle;
        private void FillCbx()
        {
            using(var donnée = new QuitayeContext())
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
                if(etat == "Normal")
                {
                    if (cycle == "Premier Cycle" || cycle == "Maternelle" || cycle == "Crèche" || cycle == "Cente Loisir")
                        return Task.Factory.StartNew(() => FillData());
                    else return Task.Factory.StartNew(() => Filldataa());
                }else
                {
                    if (cycle == "Premier Cycle" || cycle == "Maternelle" || cycle == "Crèche" || cycle == "Cente Loisir")
                        return Task.Factory.StartNew(() => FillDataDEF());
                    else return Task.Factory.StartNew(() => FilldataaDEF());
                }
            }catch (Exception ex)
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

        Decimal[] moyenne;

        public DataTable FillData()
        {

            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_note
                        where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == matricule && d.Cycle == cycle && d.Examen == exam
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

                DataTable dt = new DataTable("tbl_inven");
                var effectif = from d in donnée.tbl_inscription where d.Classe == clase && d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" select new { Matricule = d.N_Matricule };
                var matie = from d in donnée.tbl_matiere where d.Cycle == cycle select new { Matière = d.Nom, };
                var sez = (from d in donnée.tbl_inscription where d.N_Matricule == matricule select d).First();
                //int lengh = matie.Count();
                dataGridView3.DataSource = effectif;
                dataGridView2.DataSource = matie;
                moyenne = new Decimal[effectif.Count()];
                for (int j = 0; j < moyenne.Length; j++)
                {
                    moyenne[j] = new Decimal();
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {

                        string matière = dataGridView2.Rows[i].Cells[0].Value.ToString();

                        var classs = from d in donnée.tbl_note
                                     where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == matière && d.Examen == exam
                                     select new
                                     {
                                         Note = d.Note_Classe,
                                     };
                        decimal classes = 0;
                        decimal composes = 0;
                        if (classs.Count() != 0)
                        {
                            var clas = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == matière && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        }).First();
                            classes = Convert.ToDecimal(clas.Note);
                        }

                        var compo = from d in donnée.tbl_note
                                    where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == matière && d.Examen == exam
                                    select new
                                    {
                                        Note = d.Note_Compo,
                                    };
                        if (compo.Count() != 0)
                        {
                            var compos = (from d in donnée.tbl_note
                                          where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == matière && d.Examen == exam
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
                    decimal moyen = 0;
                    if(coe != 0)
                    {
                        moyen = Math.Round(Convert.ToDecimal(mgc / coe), 2);
                    }
                    moyenne[j] = moyen;
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
                                         where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == matière && d.Examen == exam
                                         select new
                                         {
                                             Note = d.Note_Classe,
                                         };
                            decimal classes = 0;
                            decimal composes = 0;
                            if (classs.Count() != 0)
                            {
                                var clas = (from d in donnée.tbl_note
                                            where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == matière && d.Examen == exam
                                            select new
                                            {
                                                Note = d.Note_Classe,
                                            }).First();
                                classes = Convert.ToDecimal(clas.Note);
                            }

                            var compo = from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == matière && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Compo,
                                        };
                            if (compo.Count() != 0)
                            {
                                var compos = (from d in donnée.tbl_note
                                              where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == matière && d.Examen == exam
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

                    if (exam.Length != 0)
                    {
                        dt.Rows.Add();

                        string observation = "";
                        decimal moyen = Math.Round(Convert.ToDecimal(mgc / coe), 2);
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
                        var result = moyenne.Where(x => x > moyen).ToArray();
                        dt.Rows.Add(drs);
                        int count = Convert.ToInt32(result.Count());
                        dt.Rows.Add();
                        DataRow dre = dt.NewRow();
                        dre[0] = "Rang";
                        if (count == 0)
                        {
                            if(sez.Genre == "Masculin")
                                dre[2] = "1er";
                            else dre[2] = "1ere";
                        }
                        
                        else  
                        dre[2] = (count + 1)+ "eme";
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
        public DataTable Filldataa()
        {
            //ts = EndDate.Value.Date - startDate.Value.Date;
            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_note
                        where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == matricule && d.Cycle == cycle && d.Examen == exam
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


                var effectif = from d in donnée.tbl_inscription where d.Classe == clase && d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" select new { Matricule = d.N_Matricule };
                var matie = from d in donnée.tbl_matiere where d.Cycle == cycle select new { Matière = d.Nom, };
                var sez = (from d in donnée.tbl_inscription where d.N_Matricule == matricule select d).First();
                //int lengh = matie.Count();
                dataGridView3.DataSource = effectif;
                dataGridView2.DataSource = matie;
                moyenne = new Decimal[effectif.Count()];
                for (int j = 0; j < moyenne.Length; j++)
                {
                    moyenne[j] = new Decimal();

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {

                        var classs = from d in donnée.tbl_note
                                     where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                     select new
                                     {
                                         Note = d.Note_Classe,
                                     };
                        decimal classes = 0;
                        decimal composes = 0;
                        if (classs.Count() != 0)
                        {
                            var clas = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        }).First();
                            classes = Convert.ToDecimal(clas.Note);
                        }

                        var compo = from d in donnée.tbl_note
                                    where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                    select new
                                    {
                                        Note = d.Note_Compo,
                                    };
                        if (compo.Count() != 0)
                        {
                            var compos = (from d in donnée.tbl_note
                                          where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                          select new
                                          {
                                              Note = d.Note_Compo,
                                          }).First();
                            composes = Convert.ToDecimal(compos.Note);
                        }
                        var coeff = (from d in donnée.tbl_matiere where d.Nom == dataGridView2.Rows[i].Cells[0].Value.ToString() select d).First();
                        coe += Convert.ToDecimal(coeff.Coefficient);
                        DataRow row = dt.NewRow();

                        decimal MG = Math.Round(Convert.ToDecimal((Convert.ToDecimal(classes) + Convert.ToDecimal(composes)) / 3), 2);
                        

                        mgc += Convert.ToDecimal(MG * coeff.Coefficient);
                        
                        dt.Rows.Add(row);
                    }
                    decimal moyen = Math.Round(Convert.ToDecimal(mgc / coe), 2);
                    moyenne[j] = moyen;
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
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        var classs = from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        };
                        decimal classes = 0;
                        decimal composes = 0;
                        if (classs.Count() != 0)
                        {
                            var clas = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        }).First();
                            classes = Convert.ToDecimal(clas.Note);
                        }

                        var compo = from d in donnée.tbl_note
                                    where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                    select new
                                    {
                                        Note = d.Note_Compo,
                                    };
                        if (compo.Count() != 0)
                        {
                            var compos = (from d in donnée.tbl_note
                                            where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                            select new
                                            {
                                                Note = d.Note_Compo,
                                            }).First();
                            composes = Convert.ToDecimal(compos.Note);
                        }
                        var coeff = (from d in donnée.tbl_matiere where d.Nom == dataGridView2.Rows[i].Cells[0].Value.ToString() select d).First();
                        coe += Convert.ToDecimal(coeff.Coefficient);
                        DataRow row = dt.NewRow();

                        decimal MG = Math.Round(Convert.ToDecimal((Convert.ToDecimal(classes) + Convert.ToDecimal(composes)) / 3), 2);
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

                        row[0] = dataGridView2.Rows[i].Cells[0].Value.ToString();
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

                        if (exam.Length != 0 )
                        {
                            dt.Rows.Add();

                        decimal moyen = Math.Round(Convert.ToDecimal(mgc / coe), 2);
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

                        var result = moyenne.Where(x => x > moyen).ToArray();
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

        public DataTable FillDataDEF()
        {
            using (var donnée = new QuitayeContext())
            {
                var s = from d in donnée.tbl_note
                        where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == matricule && d.Cycle == cycle && d.Examen == exam
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

                DataTable dt = new DataTable("tbl_inven");
                var effectif = from d in donnée.tbl_inscription where d.Classe == clase && d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" select new { Matricule = d.N_Matricule };
                var matie = from d in donnée.tbl_matiere where d.Cycle == cycle && d.Matière_Crutiale == "Oui" select new { Matière = d.Nom, };
                var sez = (from d in donnée.tbl_inscription where d.N_Matricule == matricule select d).First();
                //int lengh = matie.Count();
                dataGridView3.DataSource = effectif;
                dataGridView2.DataSource = matie;
                moyenne = new Decimal[effectif.Count()];
                for (int j = 0; j < moyenne.Length; j++)
                {
                    moyenne[j] = new Decimal();

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {

                        string matière = dataGridView2.Rows[i].Cells[0].Value.ToString();

                        var classs = from d in donnée.tbl_note
                                     where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == matière && d.Examen == exam
                                     select new
                                     {
                                         Note = d.Note_Classe,
                                     };
                        decimal classes = 0;
                        decimal composes = 0;
                        if (classs.Count() != 0)
                        {
                            var clas = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == matière && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        }).First();
                            classes = Convert.ToDecimal(clas.Note);
                        }

                        var compo = from d in donnée.tbl_note
                                    where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == matière && d.Examen == exam
                                    select new
                                    {
                                        Note = d.Note_Compo,
                                    };
                        if (compo.Count() != 0)
                        {
                            var compos = (from d in donnée.tbl_note
                                          where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == matière && d.Examen == exam
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
                    decimal moyen = Math.Round(Convert.ToDecimal(mgc / coe), 2);
                    moyenne[j] = moyen;
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
                                     where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == matière && d.Examen == exam
                                     select new
                                     {
                                         Note = d.Note_Classe,
                                     };
                        decimal classes = 0;
                        decimal composes = 0;
                        if (classs.Count() != 0)
                        {
                            var clas = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == matière && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        }).First();
                            classes = Convert.ToDecimal(clas.Note);
                        }

                        var compo = from d in donnée.tbl_note
                                    where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == matière && d.Examen == exam
                                    select new
                                    {
                                        Note = d.Note_Compo,
                                    };
                        if (compo.Count() != 0)
                        {
                            var compos = (from d in donnée.tbl_note
                                          where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == matière && d.Examen == exam
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

                    if (exam.Length != 0)
                    {
                        dt.Rows.Add();

                        string observation = "";
                        decimal moyen = Math.Round(Convert.ToDecimal(mgc / coe), 2);
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
                        var result = moyenne.Where(x => x > moyen).ToArray();
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
                        where d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == matricule && d.Cycle == cycle && d.Examen == exam
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


                var effectif = from d in donnée.tbl_inscription where d.Classe == clase && d.Année_Scolaire == Principales.annéescolaire && d.Active == "Oui" select new { Matricule = d.N_Matricule };
                var matie = from d in donnée.tbl_matiere where d.Cycle == cycle && d.Matière_Crutiale == "Oui" select new { Matière = d.Nom, };
                var sez = (from d in donnée.tbl_inscription where d.N_Matricule == matricule select d).First();
                //int lengh = matie.Count();
                dataGridView3.DataSource = effectif;
                dataGridView2.DataSource = matie;
                moyenne = new Decimal[effectif.Count()];
                for (int j = 0; j < moyenne.Length; j++)
                {
                    moyenne[j] = new Decimal();

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {




                        var classs = from d in donnée.tbl_note
                                     where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                     select new
                                     {
                                         Note = d.Note_Classe,
                                     };
                        decimal classes = 0;
                        decimal composes = 0;
                        if (classs.Count() != 0)
                        {
                            var clas = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        }).First();
                            classes = Convert.ToDecimal(clas.Note);
                        }

                        var compo = from d in donnée.tbl_note
                                    where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                    select new
                                    {
                                        Note = d.Note_Compo,
                                    };
                        if (compo.Count() != 0)
                        {
                            var compos = (from d in donnée.tbl_note
                                          where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == dataGridView3.Rows[j].Cells[0].Value.ToString() && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                          select new
                                          {
                                              Note = d.Note_Compo,
                                          }).First();
                            composes = Convert.ToDecimal(compos.Note);
                        }
                        var coeff = (from d in donnée.tbl_matiere where d.Nom == dataGridView2.Rows[i].Cells[0].Value.ToString() select d).First();
                        coe += Convert.ToDecimal(coeff.Coefficient);
                        DataRow row = dt.NewRow();

                        decimal MG = Math.Round(Convert.ToDecimal((Convert.ToDecimal(classes) + Convert.ToDecimal(composes)) / 3), 2);


                        mgc += Convert.ToDecimal(MG * coeff.Coefficient);

                        dt.Rows.Add(row);
                    }
                    decimal moyen = Math.Round(Convert.ToDecimal(mgc / coe), 2);
                    moyenne[j] = moyen;
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
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        var classs = from d in donnée.tbl_note
                                     where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                     select new
                                     {
                                         Note = d.Note_Classe,
                                     };
                        decimal classes = 0;
                        decimal composes = 0;
                        if (classs.Count() != 0)
                        {
                            var clas = (from d in donnée.tbl_note
                                        where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                        select new
                                        {
                                            Note = d.Note_Classe,
                                        }).First();
                            classes = Convert.ToDecimal(clas.Note);
                        }

                        var compo = from d in donnée.tbl_note
                                    where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                    select new
                                    {
                                        Note = d.Note_Compo,
                                    };
                        if (compo.Count() != 0)
                        {
                            var compos = (from d in donnée.tbl_note
                                          where d.Année_Scolaire == Principales.annéescolaire && d.Cycle == cycle && d.N_Matricule == matricule && d.Matière == dataGridView2.Rows[i].Cells[0].Value.ToString() && d.Examen == exam
                                          select new
                                          {
                                              Note = d.Note_Compo,
                                          }).First();
                            composes = Convert.ToDecimal(compos.Note);
                        }
                        var coeff = (from d in donnée.tbl_matiere where d.Nom == dataGridView2.Rows[i].Cells[0].Value.ToString() select d).First();
                        coe += Convert.ToDecimal(coeff.Coefficient);
                        DataRow row = dt.NewRow();

                        decimal MG = Math.Round(Convert.ToDecimal((Convert.ToDecimal(classes) + Convert.ToDecimal(composes)) / 3), 2);
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

                        row[0] = dataGridView2.Rows[i].Cells[0].Value.ToString();
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

                    if (exam.Length != 0)
                    {
                        dt.Rows.Add();

                        decimal moyen = Math.Round(Convert.ToDecimal(mgc / coe), 2);
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

                        var result = moyenne.Where(x => x > moyen).ToArray();
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


        private void cbxExamen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (exam != "") {

                CallDGV(dataGridView1);
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            PrintAction.Print.PrintPdfFile(dataGridView1, "Bulletin Scolaire " + lblTitre.Text + " " + clase + " " + Principales.annéescolaire + " " + cbxExamen.Text + " "+name, "Bulletin Scolaire", "Résultat", "Scolaire", mycontrng, "Quitaye School", false);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            PrintAction.Print.PrintExcelFile(dataGridView1, "Bulletin Scolaire ", lblTitre.Text+ " "+ clase+" "+ cbxExamen.Text +" "+ Principales.annéescolaire + " " + name, "Quitaye School");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            etat = ((RadioButton)sender).Text;
            CallDGV(dataGridView1);
        }
    }
}
