using FontAwesome.Sharp;
using PrintAction;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Scolarité_Individuelle : Form
    {
        private string mycontrng = LogIn.mycontrng;
        public string reste;
        public decimal scolarité;
        public string classes;
        public string prenom;
        public string nom;
        public string matricule;
        public string cycle;
        public string genre;
        private string filePath = "";
        private string filename;
        public string ok;
        private string name;
        private string operation = null;
        public Scolarité_Individuelle()
        {
            InitializeComponent();
            if (!(Principales.type_compte == "Administrateur"))
                return;
            CheckNegatif.Visible = true;
        }

        private void btnFermer_Click(object sender, EventArgs e) => Close();

        private async Task CallRecu()
        {
            DataTable result = await Scolarité_Individuelle.FillRecuAsync(matricule);
            dataGridView1.DataSource = result;
            result = null;
        }

        public static Task<DataTable> FillRecuAsync(string _matricule) => Task.Factory.StartNew((() => Scolarité_Individuelle.FillRecu(_matricule)));

        private static DataTable FillRecu(string _matricule)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Désignation");
            dataTable.Columns.Add("Montant");
            dataTable.Columns.Add("Date");
            using (var ecoleDataContext = new QuitayeContext())
            {
                var source = (from d in ecoleDataContext.tbl_payement
                    where (d.N_Matricule == _matricule
                    && d.Type == "Scolarité"
                    && d.Année_Scolaire == Principales.annéescolaire) && d.Montant > 0
                    orderby d.Id descending
                    select new 
                    {
                        Id = d.Id,
                        Montant = d.Montant,
                        Commentaire = d.Commentaire,
                        Date = DbFunctions.TruncateTime(d.Date_Payement.Value)
                    }).Take(1);
                foreach (var data in source)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Commentaire;
                    row[2] = data.Montant;
                    row[3] = data.Date.Value.ToString("dd/MM/yyy");
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            }
        }

        private async void btnValider_Click(object sender, EventArgs e)
        {
            if (!LogIn.expiré)
            {
                try
                {
                    if (!(txtMontant.Text != "") || !(operation != ""))
                        return;
                    MsgBox msg = new MsgBox();
                    msg.show("Voulez-vous valider cette opération ?",
                        "Attente Confirmation", 
                        MsgBox.MsgBoxButton.OuiNon, 
                        MsgBox.MsgBoxIcon.Question);
                    int num1 = (int)msg.ShowDialog();
                    if (msg.clicked == "Non")
                        return;
                    if (msg.clicked == "Oui")
                    {
                        using (var donnée = new QuitayeContext())
                        {
                            var s = donnée.tbl_payement
                                .OrderByDescending((d => d.Id)).Select(d => new
                            {
                                Id = d.Id
                            });
                            var ses = donnée.tbl_inscription
                                .Where((d => d.N_Matricule == matricule)).Select(d => new
                            {
                                Id = d.Id,
                                Scolarité = d.Scolarité,
                                Nom_Complet = d.Nom_Complet
                            }).First();
                            var jour = donnée.tbl_journal_comptable.Select(d => new
                            {
                                Id = d.Id
                            });
                            string result = Regex.Replace(txtMontant.Text, "(?<=\\d)\\p{Zs}(?=\\d)", "");
                            DateTime now;
                            if (s.Count() > 0)
                            {
                                var ss = s.First();
                                decimal? nullable1;
                                if (filePath != "")
                                {
                                    byte[] file;
                                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                                    {
                                        using (BinaryReader reader = new BinaryReader(stream))
                                            file = reader.ReadBytes((int)stream.Length);
                                    }
                                    var payement = new Models.Context.tbl_payement();
                                    payement.Id = ss.Id + 1;
                                    payement.Auteur = Principales.profile;
                                    payement.Année_Scolaire = Principales.annéescolaire;
                                    payement.Classe = classes;
                                    payement.Montant = !CheckNegatif.Checked ? new decimal?(Convert.ToDecimal(result)) : new decimal?(-Convert.ToDecimal(result));
                                    payement.Prenom = prenom;
                                    payement.Nom = nom;
                                    payement.N_Matricule = matricule;
                                    payement.Date_Payement = DateOp.Value;
                                    payement.Date_Enregistrement = DateTime.Now;
                                    payement.Commentaire = txtCommentaire.Text;
                                    payement.Fichier = file;
                                    payement.Nom_Fichier = filename;
                                    payement.Cycle = cycle;
                                    payement.Genre = genre;
                                    payement.Type = "Scolarité";
                                    payement.Opération = operation;
                                    payement.Auteur = Principales.profile;
                                    donnée.tbl_payement.Add(payement);
                                    await donnée.SaveChangesAsync();
                                    var dee = donnée.tbl_payement
                                        .Where(d => d.Année_Scolaire == Principales.annéescolaire 
                                        && d.N_Matricule == matricule 
                                        && d.Classe == classes).Select(d => new
                                    {
                                        Montant = d.Montant,
                                        Tranche1 = d.Tranche1,
                                        Tranche2 = d.Tranche2,
                                        Tranche3 = d.Tranche3
                                    });
                                    decimal? scolarité = ses.Scolarité;
                                    decimal num2 = 0M;
                                    if (scolarité.GetValueOrDefault() > num2 & scolarité.HasValue)
                                    {
                                        Decimal t1 = Math.Round(Convert.ToDecimal(ses.Scolarité) / 3M, 0);
                                        Decimal t2 = Math.Round(Convert.ToDecimal(ses.Scolarité) / 3M, 0);
                                        Decimal t3 = Math.Round(Convert.ToDecimal(ses.Scolarité) / 3M, 0);
                                        Decimal montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                        Decimal tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                        Decimal tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                        Decimal tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                        if (montant > 0M && tranche1 < t1)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant > t1 && montant < t1 + t2)
                                            {
                                                Decimal ne1 = montant - t1;
                                                if (!(tranche1 == t1))
                                                {
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(-(t1 - tranche1));
                                                        pay.Tranche2 = new decimal?(-ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(t1 - tranche1);
                                                        pay.Tranche2 = new decimal?(ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                            }
                                            else if (montant < t1)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t1 && tranche1 < t1)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - tranche1;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - tranche1;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant <= t3 + t2 + t1 && tranche1 < t1 && tranche2 < t2 && tranche3 < t3)
                                            {
                                                Decimal ne1 = t1 - tranche1;
                                                Decimal ne2 = t2 - tranche2;
                                                Decimal ne3 = t3 - tranche3;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(-ne1);
                                                    pay.Tranche2 = new decimal?(-ne2);
                                                    pay.Tranche3 = new decimal?(-ne3);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(ne1);
                                                    pay.Tranche2 = new decimal?(ne2);
                                                    pay.Tranche3 = new decimal?(ne3);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        else if (montant > t1 && tranche2 < t2)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant < t1 + t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant > t1 + t2 && tranche2 == t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - (t1 + t2);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - (t1 + t2);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t2 + t1 && tranche2 < t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant > t1 + t2 && tranche2 < t2)
                                            {
                                                Decimal ne1 = t2 + t1 - (tranche2 + tranche1);
                                                Decimal ne2 = Convert.ToDecimal(payement.Montant) - ne1;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = new decimal?(-ne1);
                                                    pay.Tranche3 = new decimal?(-ne2);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = new decimal?(ne1);
                                                    pay.Tranche3 = new decimal?(ne2);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        else if (montant > t1 + t2 && tranche3 < t3)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant < t3 + t2 + t1)
                                            {
                                                Decimal ne1 = t3 + t2 + t1 - montant;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t3 + t2 + t1 && tranche3 < t3)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = t3 - tranche3;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = t3 - tranche3;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t3 + t2 + t1 + 1M && montant < t3 + t2 + t1 + 2M && tranche3 < t3)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - (t2 + t1);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - (t2 + t1);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                    }
                                    else if (!ses.Scolarité.HasValue)
                                    {
                                        var cl = donnée.tbl_classe.Where((d => d.Nom == classes)).First();
                                        Decimal t1 = Convert.ToDecimal(cl.Tranche_1);
                                        Decimal t2 = Convert.ToDecimal(cl.Tranche_2);
                                        Decimal t3 = Convert.ToDecimal(cl.Tranche_3);
                                        Decimal montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                        Decimal tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                        Decimal tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                        Decimal tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                        if (montant > 0M && tranche1 < t1)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant > t1 && montant < t1 + t2)
                                            {
                                                Decimal ne1 = montant - t1;
                                                if (!(tranche1 == t1))
                                                {
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(-(t1 - tranche1));
                                                        pay.Tranche2 = new decimal?(-ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(t1 - tranche1);
                                                        pay.Tranche2 = new decimal?(ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                            }
                                            else if (montant < t1)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t1 && tranche1 < t1)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - tranche1;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - tranche1;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant <= t3 + t2 + t1 && tranche1 < t1 && tranche2 < t2 && tranche3 < t3)
                                            {
                                                Decimal ne1 = t1 - tranche1;
                                                Decimal ne2 = t2 - tranche2;
                                                Decimal ne3 = t3 - tranche3;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(-ne1);
                                                    pay.Tranche2 = new decimal?(-ne2);
                                                    pay.Tranche3 = new decimal?(-ne3);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(ne1);
                                                    pay.Tranche2 = new decimal?(ne2);
                                                    pay.Tranche3 = new decimal?(ne3);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        else if (montant > t1 && tranche2 < t2)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant < t1 + t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant > t1 + t2 && tranche2 == t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - (t1 + t2);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - (t1 + t2);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t2 + t1 && tranche2 < t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant > t1 + t2 && tranche2 < t2)
                                            {
                                                Decimal ne1 = t2 + t1 - (tranche2 + tranche1);
                                                Decimal ne2 = Convert.ToDecimal(payement.Montant) - ne1;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = new decimal?(-ne1);
                                                    pay.Tranche3 = new decimal?(-ne2);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = new decimal?(ne1);
                                                    pay.Tranche3 = new decimal?(ne2);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        else if (montant > t1 + t2 && tranche3 < t3)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant < t3 + t2 + t1)
                                            {
                                                Decimal ne1 = t3 + t2 + t1 - montant;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t3 + t2 + t1 && tranche3 < t3)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = t3 - tranche3;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = t3 - tranche3;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t3 + t2 + t1 + 1M && montant < t3 + t2 + t1 + 2M && tranche3 < t3)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - (t2 + t1);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - (t2 + t1);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        cl = null;
                                    }
                                    ok = "Oui";
                                    file = (byte[])null;
                                    dee = null;
                                }
                                else
                                {
                                    var payement = new Models.Context.tbl_payement();
                                    payement.Id = ss.Id + 1;
                                    payement.Auteur = Principales.profile;
                                    payement.Année_Scolaire = Principales.annéescolaire;
                                    payement.Classe = classes;
                                    payement.Montant = !CheckNegatif.Checked ? new decimal?(Convert.ToDecimal(result)) : new decimal?(-Convert.ToDecimal(result));
                                    payement.Prenom = prenom;
                                    payement.Nom = nom;
                                    payement.Opération = operation;
                                    payement.N_Matricule = matricule;
                                    var tblPayement = payement;
                                    tblPayement.Date_Payement = DateOp.Value;
                                    payement.Date_Enregistrement = DateTime.Now;
                                    payement.Commentaire = txtCommentaire.Text;
                                    payement.Cycle = cycle;
                                    payement.Genre = genre;
                                    payement.Type = "Scolarité";
                                    donnée.tbl_payement.Add(payement);
                                    await donnée.SaveChangesAsync();
                                    var dee = donnée.tbl_payement.Where((d => d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == matricule && d.Classe == classes && d.Type == "Scolarité")).Select(d => new
                                    {
                                        Montant = d.Montant,
                                        Tranche1 = d.Tranche1,
                                        Tranche2 = d.Tranche2,
                                        Tranche3 = d.Tranche3
                                    });
                                    nullable1 = ses.Scolarité;
                                    Decimal num3 = 0M;
                                    if (nullable1.GetValueOrDefault() > num3 & nullable1.HasValue)
                                    {
                                        Decimal t1 = Math.Round(Convert.ToDecimal(ses.Scolarité) / 3M, 0);
                                        Decimal t2 = Math.Round(Convert.ToDecimal(ses.Scolarité) / 3M, 0);
                                        Decimal t3 = Math.Round(Convert.ToDecimal(ses.Scolarité) / 3M, 0);
                                        Decimal montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                        Decimal tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                        Decimal tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                        Decimal tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                        if (montant > 0M && tranche1 < t1)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant > t1 && montant < t1 + t2)
                                            {
                                                Decimal ne1 = montant - t1;
                                                if (!(tranche1 == t1))
                                                {
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(-(t1 - tranche1));
                                                        pay.Tranche2 = new decimal?(-ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(t1 - tranche1);
                                                        pay.Tranche2 = new decimal?(ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                            }
                                            else if (montant < t1)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t1 && tranche1 < t1)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - tranche1;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - tranche1;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant <= t3 + t2 + t1 && tranche1 < t1 && tranche2 < t2 && tranche3 < t3)
                                            {
                                                Decimal ne1 = t1 - tranche1;
                                                Decimal ne2 = t2 - tranche2;
                                                Decimal ne3 = t3 - tranche3;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(-ne1);
                                                    pay.Tranche2 = new decimal?(-ne2);
                                                    pay.Tranche3 = new decimal?(-ne3);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(ne1);
                                                    pay.Tranche2 = new decimal?(ne2);
                                                    pay.Tranche3 = new decimal?(ne3);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        else if (montant > t1 && tranche2 < t2)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant < t1 + t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant > t1 + t2 && tranche2 == t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - (t1 + t2);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - (t1 + t2);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t2 + t1 && tranche2 < t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant > t1 + t2 && tranche2 < t2)
                                            {
                                                Decimal ne1 = t2 + t1 - (tranche2 + tranche1);
                                                Decimal ne2 = Convert.ToDecimal(payement.Montant) - ne1;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = new decimal?(-ne1);
                                                    pay.Tranche3 = new decimal?(-ne2);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = new decimal?(ne1);
                                                    pay.Tranche3 = new decimal?(ne2);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        else if (montant > t1 + t2 && tranche3 < t3)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant < t3 + t2 + t1)
                                            {
                                                Decimal ne1 = t3 + t2 + t1 - montant;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t3 + t2 + t1 && tranche3 < t3)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = t3 - tranche3;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = t3 - tranche3;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t3 + t2 + t1 + 1M && montant < t3 + t2 + t1 + 2M && tranche3 < t3)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - (t2 + t1);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - (t2 + t1);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        nullable1 = ses.Scolarité;
                                        if (!nullable1.HasValue)
                                        {
                                            var cl = donnée.tbl_classe.Where((d => d.Nom == classes)).First();
                                            Decimal t1 = Convert.ToDecimal(cl.Tranche_1);
                                            Decimal t2 = Convert.ToDecimal(cl.Tranche_2);
                                            Decimal t3 = Convert.ToDecimal(cl.Tranche_3);
                                            Decimal montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            Decimal tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            Decimal tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            Decimal tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant > 0M && tranche1 < t1)
                                            {
                                                montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                                tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                                tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                                tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                                if (montant > t1 && montant < t1 + t2)
                                                {
                                                    Decimal ne1 = montant - t1;
                                                    if (!(tranche1 == t1))
                                                    {
                                                        if (CheckNegatif.Checked)
                                                        {
                                                            var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                            pay.Tranche1 = new decimal?(-(t1 - tranche1));
                                                            pay.Tranche2 = new decimal?(-ne1);
                                                            await donnée.SaveChangesAsync();
                                                            pay = null;
                                                        }
                                                        else
                                                        {
                                                            var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                            pay.Tranche1 = new decimal?(t1 - tranche1);
                                                            pay.Tranche2 = new decimal?(ne1);
                                                            await donnée.SaveChangesAsync();
                                                            pay = null;
                                                        }
                                                    }
                                                }
                                                else if (montant < t1)
                                                {
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = payement.Montant;
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = payement.Montant;
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                                else if (montant == t1 && tranche1 < t1)
                                                {
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        Decimal ne1 = montant - tranche1;
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(-ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        Decimal ne1 = montant - tranche1;
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                                else if (montant <= t3 + t2 + t1 && tranche1 < t1 && tranche2 < t2 && tranche3 < t3)
                                                {
                                                    Decimal ne1 = t1 - tranche1;
                                                    Decimal ne2 = t2 - tranche2;
                                                    Decimal ne3 = t3 - tranche3;
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(-ne1);
                                                        pay.Tranche2 = new decimal?(-ne2);
                                                        pay.Tranche3 = new decimal?(-ne3);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(ne1);
                                                        pay.Tranche2 = new decimal?(ne2);
                                                        pay.Tranche3 = new decimal?(ne3);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                            }
                                            else if (montant > t1 && tranche2 < t2)
                                            {
                                                montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                                tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                                tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                                tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                                if (montant < t1 + t2)
                                                {
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche2 = payement.Montant;
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche2 = payement.Montant;
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                                else if (montant > t1 + t2 && tranche2 == t2)
                                                {
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        Decimal ne1 = montant - (t1 + t2);
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche3 = new decimal?(-ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        Decimal ne1 = montant - (t1 + t2);
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche3 = new decimal?(ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                                else if (montant == t2 + t1 && tranche2 < t2)
                                                {
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche2 = payement.Montant;
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche2 = payement.Montant;
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                                else if (montant > t1 + t2 && tranche2 < t2)
                                                {
                                                    Decimal ne1 = t2 + t1 - (tranche2 + tranche1);
                                                    Decimal ne2 = Convert.ToDecimal(payement.Montant) - ne1;
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche2 = new decimal?(-ne1);
                                                        pay.Tranche3 = new decimal?(-ne2);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche2 = new decimal?(ne1);
                                                        pay.Tranche3 = new decimal?(ne2);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                            }
                                            else if (montant > t1 + t2 && tranche3 < t3)
                                            {
                                                montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                                tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                                tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                                tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                                if (montant < t3 + t2 + t1)
                                                {
                                                    Decimal ne1 = t3 + t2 + t1 - montant;
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche3 = payement.Montant;
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche3 = payement.Montant;
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                                else if (montant > t1 + t2 && tranche3 == t3)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else if (montant > t1 && tranche2 == t2)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else if (montant > 0M && tranche1 == t1)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else if (montant == t3 + t2 + t1 && tranche3 < t3)
                                                {
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        Decimal ne1 = t3 - tranche3;
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche3 = new decimal?(-ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        Decimal ne1 = t3 - tranche3;
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche3 = new decimal?(ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                                else if (montant == t3 + t2 + t1 + 1M && montant < t3 + t2 + t1 + 2M && tranche3 < t3)
                                                {
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        Decimal ne1 = montant - (t2 + t1);
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche3 = new decimal?(-ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        Decimal ne1 = montant - (t2 + t1);
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche3 = new decimal?(ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                            }
                                            cl = null;
                                        }
                                    }
                                    ok = "Oui";
                                    dee = null;
                                }
                                if (jour.Count() > 0)
                                {
                                    var j = donnée.tbl_journal_comptable.OrderByDescending((d => d.Id)).First();
                                    int id = Convert.ToInt32(j.Id) + 1;
                                    Decimal montant = 0M;
                                    montant = Convert.ToDecimal(result);
                                    var V = donnée.tbl_journal_comptable.Where((d => d.Ref_Pièces.StartsWith("V"))).OrderByDescending((d => d.Ref_Pièces));
                                    if (V.Count() > 0)
                                    {
                                        var di = donnée.tbl_journal_comptable.Where((d => d.Ref_Pièces.StartsWith("V"))).OrderByDescending((d => d.Ref_Pièces)).First();
                                        string[] arry = di.Ref_Pièces.Split('V');
                                        int reference = Convert.ToInt32(arry[1]) + 1;
                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = id;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = new DateTime?(DateTime.Now);
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal);
                                        await donnée.SaveChangesAsync();
                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal1.Id = id + 1;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = new DateTime?(DateTime.Now);
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                        di = null;
                                        arry = (string[])null;
                                        journal = null;
                                        journal1 = null;
                                    }
                                    else
                                    {
                                        int reference = 1;
                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = id;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = new DateTime?(DateTime.Now);
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal);
                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal1.Id = id + 1;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = new DateTime?(DateTime.Now);
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                        journal = null;
                                        journal1 = null;
                                    }
                                    j = null;
                                    V = null;
                                }
                                else
                                {
                                    Decimal montant = 0M;
                                    montant = Convert.ToDecimal(result);
                                    var V = donnée.tbl_journal_comptable.Where((d => d.Ref_Pièces.StartsWith("V"))).OrderByDescending((d => d.Ref_Pièces));
                                    if (V.Count() > 0)
                                    {
                                        var di = donnée.tbl_journal_comptable.Where((d => d.Ref_Pièces.StartsWith("V"))).OrderByDescending((d => d.Ref_Pièces)).First();
                                        string[] arry = di.Ref_Pièces.Split('V');
                                        int reference = Convert.ToInt32(arry[1]) + 1;
                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = 1;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = new DateTime?(DateTime.Now);
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal);
                                        await donnée.SaveChangesAsync();
                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal.Id = 2;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = new DateTime?(DateTime.Now);
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                        di = null;
                                        arry = (string[])null;
                                        journal = null;
                                        journal1 = null;
                                    }
                                    else
                                    {
                                        int reference = 1;
                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = 1;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = new DateTime?(DateTime.Now);
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal);
                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal1.Id = 2;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = new DateTime?(DateTime.Now);
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                        journal = null;
                                        journal1 = null;
                                    }
                                    V = null;
                                }
                                txtMontant.Clear();
                                var se = donnée.tbl_payement.Where(d => d.N_Matricule == matricule && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire);
                                
                                Decimal scolarité1 = scolarité;
                                nullable1 = se.Sum((x => x.Montant));
                                string str = "Reste : " + Convert.ToDecimal((nullable1.HasValue ? new decimal?(scolarité1 - nullable1.GetValueOrDefault()) : new decimal?())).ToString();
                                lblReste.Text = str;
                                Alert.SShow("Payement validé avec succès.", Alert.AlertType.Sucess);
                                ss = null;
                                se = null;
                            }
                            else
                            {
                                byte[] file;
                                if (filePath != "")
                                {
                                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                                    {
                                        using (BinaryReader reader = new BinaryReader(stream))
                                            file = reader.ReadBytes((int)stream.Length);
                                    }
                                    var payement = new Models.Context.tbl_payement();
                                    payement.Id = 1;
                                    payement.Auteur = Principales.profile;
                                    payement.Année_Scolaire = Principales.annéescolaire;
                                    payement.Classe = classes;
                                    payement.Montant = !CheckNegatif.Checked ? new decimal?(Convert.ToDecimal(result)) : new decimal?(-Convert.ToDecimal(result));
                                    payement.Prenom = prenom;
                                    payement.Nom = nom;
                                    payement.Opération = operation;
                                    payement.N_Matricule = matricule;
                                    var tblPayement = payement;
                                    tblPayement.Date_Payement = DateOp.Value;
                                    payement.Date_Enregistrement = DateTime.Now;
                                    payement.Commentaire = txtCommentaire.Text;
                                    payement.Genre = genre;
                                    payement.Fichier = file;
                                    payement.Nom_Fichier = filename;
                                    payement.Cycle = cycle;
                                    payement.Type = "Scolarité";
                                    donnée.tbl_payement.Add(payement);
                                    await donnée.SaveChangesAsync();
                                    var dee = donnée.tbl_payement.Where((d => d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == matricule && d.Classe == classes)).Select(d => new
                                    {
                                        Montant = d.Montant,
                                        Tranche1 = d.Tranche1,
                                        Tranche2 = d.Tranche2,
                                        Tranche3 = d.Tranche3
                                    });
                                    decimal? scolarité = ses.Scolarité;
                                    Decimal num4 = 0M;
                                    if (scolarité.GetValueOrDefault() > num4 & scolarité.HasValue)
                                    {
                                        Decimal t1 = Math.Round(Convert.ToDecimal(ses.Scolarité) / 3M, 0);
                                        Decimal t2 = Math.Round(Convert.ToDecimal(ses.Scolarité) / 3M, 0);
                                        Decimal t3 = Math.Round(Convert.ToDecimal(ses.Scolarité) / 3M, 0);
                                        Decimal montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                        Decimal tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                        Decimal tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                        Decimal tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                        if (montant > 0M && tranche1 < t1)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant > t1 && montant < t1 + t2)
                                            {
                                                Decimal ne1 = montant - t1;
                                                if (!(tranche1 == t1))
                                                {
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(-(t1 - tranche1));
                                                        pay.Tranche2 = new decimal?(-ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(t1 - tranche1);
                                                        pay.Tranche2 = new decimal?(ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                            }
                                            else if (montant < t1)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t1 && tranche1 < t1)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - tranche1;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - tranche1;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant <= t3 + t2 + t1 && tranche1 < t1 && tranche2 < t2 && tranche3 < t3)
                                            {
                                                Decimal ne1 = t1 - tranche1;
                                                Decimal ne2 = t2 - tranche2;
                                                Decimal ne3 = t3 - tranche3;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(-ne1);
                                                    pay.Tranche2 = new decimal?(-ne2);
                                                    pay.Tranche3 = new decimal?(-ne3);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(ne1);
                                                    pay.Tranche2 = new decimal?(ne2);
                                                    pay.Tranche3 = new decimal?(ne3);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        else if (montant > t1 && tranche2 < t2)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant < t1 + t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant > t1 + t2 && tranche2 == t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - (t1 + t2);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - (t1 + t2);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t2 + t1 && tranche2 < t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant > t1 + t2 && tranche2 < t2)
                                            {
                                                Decimal ne1 = t2 + t1 - (tranche2 + tranche1);
                                                Decimal ne2 = Convert.ToDecimal(payement.Montant) - ne1;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = new decimal?(-ne1);
                                                    pay.Tranche3 = new decimal?(-ne2);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = new decimal?(ne1);
                                                    pay.Tranche3 = new decimal?(ne2);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        else if (montant > t1 + t2 && tranche3 < t3)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant < t3 + t2 + t1)
                                            {
                                                Decimal ne1 = t3 + t2 + t1 - montant;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t3 + t2 + t1 && tranche3 < t3)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = t3 - tranche3;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = t3 - tranche3;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t3 + t2 + t1 + 1M && montant < t3 + t2 + t1 + 2M && tranche3 < t3)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - (t2 + t1);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - (t2 + t1);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                    }
                                    else if (!ses.Scolarité.HasValue)
                                    {
                                        var cl = donnée.tbl_classe.Where((d => d.Nom == classes)).First();
                                        Decimal t1 = Convert.ToDecimal(cl.Tranche_1);
                                        Decimal t2 = Convert.ToDecimal(cl.Tranche_2);
                                        Decimal t3 = Convert.ToDecimal(cl.Tranche_3);
                                        Decimal montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                        Decimal tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                        Decimal tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                        Decimal tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                        if (montant > 0M && tranche1 < t1)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant > t1 && montant < t1 + t2)
                                            {
                                                Decimal ne1 = montant - t1;
                                                if (!(tranche1 == t1))
                                                {
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(-(t1 - tranche1));
                                                        pay.Tranche2 = new decimal?(-ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(t1 - tranche1);
                                                        pay.Tranche2 = new decimal?(ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                            }
                                            else if (montant < t1)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t1 && tranche1 < t1)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - tranche1;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - tranche1;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant <= t3 + t2 + t1 && tranche1 < t1 && tranche2 < t2 && tranche3 < t3)
                                            {
                                                Decimal ne1 = t1 - tranche1;
                                                Decimal ne2 = t2 - tranche2;
                                                Decimal ne3 = t3 - tranche3;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(-ne1);
                                                    pay.Tranche2 = new decimal?(-ne2);
                                                    pay.Tranche3 = new decimal?(-ne3);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(ne1);
                                                    pay.Tranche2 = new decimal?(ne2);
                                                    pay.Tranche3 = new decimal?(ne3);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        else if (montant > t1 && tranche2 < t2)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant < t1 + t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant > t1 + t2 && tranche2 == t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - (t1 + t2);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - (t1 + t2);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t2 + t1 && tranche2 < t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant > t1 + t2 && tranche2 < t2)
                                            {
                                                Decimal ne1 = t2 + t1 - (tranche2 + tranche1);
                                                Decimal ne2 = Convert.ToDecimal(payement.Montant) - ne1;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = new decimal?(-ne1);
                                                    pay.Tranche3 = new decimal?(-ne2);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = new decimal?(ne1);
                                                    pay.Tranche3 = new decimal?(ne2);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        else if (montant > t1 + t2 && tranche3 < t3)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant < t3 + t2 + t1)
                                            {
                                                Decimal ne1 = t3 + t2 + t1 - montant;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t3 + t2 + t1 && tranche3 < t3)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = t3 - tranche3;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = t3 - tranche3;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t3 + t2 + t1 + 1M && montant < t3 + t2 + t1 + 2M && tranche3 < t3)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - (t2 + t1);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - (t2 + t1);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        cl = null;
                                    }
                                    ok = "Oui";
                                    dee = null;
                                }
                                else
                                {
                                    var payement = new Models.Context.tbl_payement();
                                    payement.Id = 1;
                                    payement.Auteur = Principales.profile;
                                    payement.Année_Scolaire = Principales.annéescolaire;
                                    payement.Classe = classes;
                                    payement.Montant = !CheckNegatif.Checked ? new decimal?(Convert.ToDecimal(result)) : new decimal?(-Convert.ToDecimal(result));
                                    payement.Prenom = prenom;
                                    payement.Opération = operation;
                                    payement.Nom = nom;
                                    payement.N_Matricule = matricule;
                                    var tblPayement = payement;
                                    tblPayement.Date_Payement = DateOp.Value;
                                    payement.Date_Enregistrement = DateTime.Now;
                                    payement.Commentaire = txtCommentaire.Text;
                                    payement.Cycle = cycle;
                                    payement.Genre = genre;
                                    payement.Type = "Scolarité";
                                    donnée.tbl_payement.Add(payement);
                                    await donnée.SaveChangesAsync();
                                    var dee = donnée.tbl_payement.Where((d => d.Année_Scolaire == Principales.annéescolaire && d.N_Matricule == matricule && d.Classe == classes)).Select(d => new
                                    {
                                        Montant = d.Montant,
                                        Tranche1 = d.Tranche1,
                                        Tranche2 = d.Tranche2,
                                        Tranche3 = d.Tranche3
                                    });
                                    decimal? scolarité = ses.Scolarité;
                                    Decimal num5 = 0M;
                                    if (scolarité.GetValueOrDefault() > num5 & scolarité.HasValue)
                                    {
                                        Decimal t1 = Math.Round(Convert.ToDecimal(ses.Scolarité) / 3M, 0);
                                        Decimal t2 = Math.Round(Convert.ToDecimal(ses.Scolarité) / 3M, 0);
                                        Decimal t3 = Math.Round(Convert.ToDecimal(ses.Scolarité) / 3M, 0);
                                        Decimal montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                        Decimal tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                        Decimal tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                        Decimal tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                        if (montant > 0M && tranche1 < t1)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant > t1 && montant < t1 + t2)
                                            {
                                                Decimal ne1 = montant - t1;
                                                if (!(tranche1 == t1))
                                                {
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(-(t1 - tranche1));
                                                        pay.Tranche2 = new decimal?(-ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(t1 - tranche1);
                                                        pay.Tranche2 = new decimal?(ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                            }
                                            else if (montant < t1)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t1 && tranche1 < t1)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - tranche1;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - tranche1;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant <= t3 + t2 + t1 && tranche1 < t1 && tranche2 < t2 && tranche3 < t3)
                                            {
                                                Decimal ne1 = t1 - tranche1;
                                                Decimal ne2 = t2 - tranche2;
                                                Decimal ne3 = t3 - tranche3;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(-ne1);
                                                    pay.Tranche2 = new decimal?(-ne2);
                                                    pay.Tranche3 = new decimal?(-ne3);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(ne1);
                                                    pay.Tranche2 = new decimal?(ne2);
                                                    pay.Tranche3 = new decimal?(ne3);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        else if (montant > t1 && tranche2 < t2)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant < t1 + t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant > t1 + t2 && tranche2 == t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - (t1 + t2);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - (t1 + t2);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t2 + t1 && tranche2 < t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant > t1 + t2 && tranche2 < t2)
                                            {
                                                Decimal ne1 = t2 + t1 - (tranche2 + tranche1);
                                                Decimal ne2 = Convert.ToDecimal(payement.Montant) - ne1;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = new decimal?(-ne1);
                                                    pay.Tranche3 = new decimal?(-ne2);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = new decimal?(ne1);
                                                    pay.Tranche3 = new decimal?(ne2);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        else if (montant > t1 + t2 && tranche3 < t3)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant < t3 + t2 + t1)
                                            {
                                                Decimal ne1 = t3 + t2 + t1 - montant;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t3 + t2 + t1 && tranche3 < t3)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = t3 - tranche3;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = t3 - tranche3;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t3 + t2 + t1 + 1M && montant < t3 + t2 + t1 + 2M && tranche3 < t3)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - (t2 + t1);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - (t2 + t1);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                    }
                                    else if (!ses.Scolarité.HasValue)
                                    {
                                        var cl = donnée.tbl_classe.Where((d => d.Nom == classes)).First();
                                        Decimal t1 = Convert.ToDecimal(cl.Tranche_1);
                                        Decimal t2 = Convert.ToDecimal(cl.Tranche_2);
                                        Decimal t3 = Convert.ToDecimal(cl.Tranche_3);
                                        Decimal montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                        Decimal tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                        Decimal tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                        Decimal tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                        if (montant > 0M && tranche1 < t1)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant > t1 && montant < t1 + t2)
                                            {
                                                Decimal ne1 = montant - t1;
                                                if (!(tranche1 == t1))
                                                {
                                                    if (CheckNegatif.Checked)
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(-(t1 - tranche1));
                                                        pay.Tranche2 = new decimal?(-ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                    else
                                                    {
                                                        var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                        pay.Tranche1 = new decimal?(t1 - tranche1);
                                                        pay.Tranche2 = new decimal?(ne1);
                                                        await donnée.SaveChangesAsync();
                                                        pay = null;
                                                    }
                                                }
                                            }
                                            else if (montant < t1)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t1 && tranche1 < t1)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - tranche1;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - tranche1;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant <= t3 + t2 + t1 && tranche1 < t1 && tranche2 < t2 && tranche3 < t3)
                                            {
                                                Decimal ne1 = t1 - tranche1;
                                                Decimal ne2 = t2 - tranche2;
                                                Decimal ne3 = t3 - tranche3;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(-ne1);
                                                    pay.Tranche2 = new decimal?(-ne2);
                                                    pay.Tranche3 = new decimal?(-ne3);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche1 = new decimal?(ne1);
                                                    pay.Tranche2 = new decimal?(ne2);
                                                    pay.Tranche3 = new decimal?(ne3);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        else if (montant > t1 && tranche2 < t2)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant < t1 + t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant > t1 + t2 && tranche2 == t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - (t1 + t2);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - (t1 + t2);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t2 + t1 && tranche2 < t2)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant > t1 + t2 && tranche2 < t2)
                                            {
                                                Decimal ne1 = t2 + t1 - (tranche2 + tranche1);
                                                Decimal ne2 = Convert.ToDecimal(payement.Montant) - ne1;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = new decimal?(-ne1);
                                                    pay.Tranche3 = new decimal?(-ne2);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche2 = new decimal?(ne1);
                                                    pay.Tranche3 = new decimal?(ne2);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        else if (montant > t1 + t2 && tranche3 < t3)
                                        {
                                            montant = Convert.ToDecimal(dee.Sum(x => x.Montant));
                                            tranche1 = Convert.ToDecimal(dee.Sum(x => x.Tranche1));
                                            tranche2 = Convert.ToDecimal(dee.Sum(x => x.Tranche2));
                                            tranche3 = Convert.ToDecimal(dee.Sum(x => x.Tranche3));
                                            if (montant < t3 + t2 + t1)
                                            {
                                                Decimal ne1 = t3 + t2 + t1 - montant;
                                                if (CheckNegatif.Checked)
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = payement.Montant;
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t3 + t2 + t1 && tranche3 < t3)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = t3 - tranche3;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = t3 - tranche3;
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                            else if (montant == t3 + t2 + t1 + 1M && montant < t3 + t2 + t1 + 2M && tranche3 < t3)
                                            {
                                                if (CheckNegatif.Checked)
                                                {
                                                    Decimal ne1 = montant - (t2 + t1);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(-ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                                else
                                                {
                                                    Decimal ne1 = montant - (t2 + t1);
                                                    var pay = donnée.tbl_payement.SingleOrDefault((x => x.Id == payement.Id));
                                                    pay.Tranche3 = new decimal?(ne1);
                                                    await donnée.SaveChangesAsync();
                                                    pay = null;
                                                }
                                            }
                                        }
                                        cl = null;
                                    }
                                    dee = null;
                                }
                                if (jour.Count() > 0)
                                {
                                    var j = donnée.tbl_journal_comptable.OrderByDescending((d => d.Id)).First();
                                    int id = Convert.ToInt32(j.Id) + 1;
                                    Decimal montant = 0M;
                                    montant = Convert.ToDecimal(result);
                                    var V = donnée.tbl_journal_comptable.Where((d => d.Ref_Pièces.StartsWith("V"))).OrderByDescending((d => d.Ref_Pièces));
                                    if (V.Count() > 0)
                                    {
                                        var di = donnée.tbl_journal_comptable.Where((d => d.Ref_Pièces.StartsWith("V"))).OrderByDescending((d => d.Ref_Pièces)).First();
                                        string[] arry = di.Ref_Pièces.Split('V');
                                        int reference = Convert.ToInt32(arry[1]) + 1;
                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = id;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = new DateTime?(DateTime.Now);
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal);
                                        await donnée.SaveChangesAsync();
                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal1.Id = id + 1;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = new DateTime?(DateTime.Now);
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                        di = null;
                                        arry = (string[])null;
                                        journal = null;
                                        journal1 = null;
                                    }
                                    else
                                    {
                                        int reference = 1;
                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = id;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = new DateTime?(DateTime.Now);
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal);
                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal1.Id = id + 1;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = new DateTime?(DateTime.Now);
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                        journal = null;
                                        journal1 = null;
                                    }
                                    j = null;
                                    V = null;
                                }
                                else
                                {
                                    Decimal montant = 0M;
                                    montant = Convert.ToDecimal(result);
                                    var V = donnée.tbl_journal_comptable.Where((d => d.Ref_Pièces.StartsWith("V"))).OrderByDescending((d => d.Ref_Pièces));
                                    if (V.Count() > 0)
                                    {
                                        var di = donnée.tbl_journal_comptable.Where((d => d.Ref_Pièces.StartsWith("V"))).OrderByDescending((d => d.Ref_Pièces)).First();
                                        string[] arry = di.Ref_Pièces.Split('V');
                                        int reference = Convert.ToInt32(arry[1]) + 1;
                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = 1;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = new DateTime?(DateTime.Now);
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal);
                                        await donnée.SaveChangesAsync();
                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal.Id = 2;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = new DateTime?(DateTime.Now);
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                        di = null;
                                        arry = (string[])null;
                                        journal = null;
                                        journal1 = null;
                                    }
                                    else
                                    {
                                        int reference = 1;
                                        var journal = new Models.Context.tbl_journal_comptable();
                                        journal.Id = 1;
                                        journal.Auteur = Principales.profile;
                                        journal.Compte = "570100";
                                        journal.Date = new DateTime?(DateTime.Now);
                                        journal.Date_Enregistrement = DateTime.Now;
                                        journal.Débit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal.Libelle = "Caisse";
                                        journal.Commentaire = ses.Nom_Complet + " " + txtCommentaire.Text;
                                        journal.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal);
                                        var journal1 = new Models.Context.tbl_journal_comptable();
                                        journal1.Id = 2;
                                        journal1.Auteur = Principales.profile;
                                        journal1.Compte = "411E" + matricule;
                                        journal1.Date = new DateTime?(DateTime.Now);
                                        journal1.Date_Enregistrement = DateTime.Now;
                                        journal1.Crédit = !CheckNegatif.Checked ? new decimal?(montant) : new decimal?(-montant);
                                        journal1.Libelle = ses.Nom_Complet;
                                        journal1.Commentaire = txtCommentaire.Text;
                                        journal1.Ref_Pièces = "V" + reference.ToString();
                                        donnée.tbl_journal_comptable.Add(journal1);
                                        await donnée.SaveChangesAsync();
                                        journal = null;
                                        journal1 = null;
                                    }
                                    V = null;
                                }
                                var se = donnée.tbl_payement.Where(d => d.N_Matricule == matricule 
                                && d.Type == "Scolarité" && d.Année_Scolaire == Principales.annéescolaire);
                                
                                Decimal scolarité2 = scolarité;
                                decimal? nullable4 = se.Sum(x => x.Montant);
                                string str = "Reste : " + Convert.ToDecimal(nullable4.HasValue ? new decimal?(scolarité2 - nullable4.GetValueOrDefault()) : new decimal?()).ToString();
                                lblReste.Text = str;
                                txtMontant.Clear();
                                Alert.SShow("Payement validé avec succès.", Alert.AlertType.Sucess);
                                file = (byte[])null;
                            }
                            await CallRecu();
                            string[] strArray = new string[8]
                            {
                            "Reçu Payement ",
                            prenom,
                            " ",
                            nom,
                            " N° Matricule ",
                            matricule,
                            " ",
                            null
                            };
                            now = DateTime.Now;
                            strArray[7] = now.ToString("dd-MM-yyyy HH.mm.ss");
                            name = string.Concat(strArray);
                            Detail_Facture facture = await FactureAsync(matricule);
                            facture.Type_Operation = "Scolarité";
                            Print.PrintRecuPdfFile(dataGridView1, name, "Année_Scolaire " + Principales.annéescolaire, "Payement ", prenom + " " + nom + " N°" + matricule, LogIn.mycontrng, "Quitaye School", false, facture, true);
                            s = null;
                            ses = null;
                            jour = null;
                            result = null;
                            facture = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!(ex is SqlException w32ex))
                    {
                        w32ex = ex.InnerException as SqlException;
                        int codes = w32ex.ErrorCode;
                    }
                    if (w32ex != null)
                    {
                        int codes = w32ex.ErrorCode;
                        if (codes == -2146232060)
                        {
                            MsgBox msg = new MsgBox();
                            msg.show("Erreur réseau, Veillez verifier votre connection réseau !!", "Erreur", MsgBox.MsgBoxButton.OK, MsgBox.MsgBoxIcon.Error);
                            msg.ShowDialog();
                        }
                        else
                        {
                            MsgBox msg = new MsgBox();
                            msg.show(ex.Message, "Erreur", MsgBox.MsgBoxButton.OK, 
                                MsgBox.MsgBoxIcon.Error);
                            msg.ShowDialog();
                            
                        }
                    }
                    w32ex = null;
                }

            }
            else if (LogIn.trial)
                Alert.SShow("Periode d'essay arrivé terme. Veillez-passez à la version payante !", Alert.AlertType.Info);
            else
                Alert.SShow("Veillez renouveller votre abonnement !", Alert.AlertType.Info);
        }

        private Task<Detail_Facture> FactureAsync(string matricule) => Task.Factory.StartNew(() => Facture(matricule));

        private Detail_Facture Facture(string matricule)
        {
            var detailFacture = new Detail_Facture();
            using (var ecoleDataContext = new QuitayeContext())
            {
                var source1 = ecoleDataContext
                    .tbl_inscription.Where(d => d.N_Matricule == matricule).Select(d => new
                {
                    Type = d.Type_Scolarité,
                    Scolarité = d.Scolarité,
                    Classe = d.Classe,
                    Cycle = d.Cycle
                }).Take(1);
                if (source1.Count() != 0)
                {
                    var ps = source1.First();
                    if (ps.Type == "Normal")
                    {
                        var source2 = ecoleDataContext.tbl_scolarité.Where(d => d.Cycle == ps.Cycle 
                        && d.Classe == ps.Classe && d.Année_Scolaire == Principales.annéescolaire);
                        if (source2.Count() == 0)
                            source2 = ecoleDataContext.tbl_scolarité.Where(d => d.Classe == ps.Classe).OrderByDescending(x => x.Id);
                        detailFacture.Scolarité = Convert.ToDecimal(source2.FirstOrDefault().Montant);
                    }
                    else
                        detailFacture.Scolarité = !(ps.Type == "Avec Rémise") ? 0M : Convert.ToDecimal(ps.Scolarité);
                }
                var source3 = ecoleDataContext
                    .tbl_payement.Where(d => d.Année_Scolaire == Principales.annéescolaire 
                    && d.N_Matricule == matricule && d.Type == "Scolarité")
                    .GroupBy(d => new
                {
                    Date = DbFunctions.TruncateTime(d.Date_Payement.Value)
                }).OrderByDescending(gr => gr.Key.Date).Select(gr => new
                {
                    Date = gr.Key.Date,
                    Montant = gr.Sum(x => x.Montant)
                });
                detailFacture.MontantPayée = Convert.ToDecimal(source3.Sum(x => x.Montant));
                detailFacture.Restant = detailFacture.Scolarité - detailFacture.MontantPayée;
                detailFacture.Type_Operation = "Scolarité";
                detailFacture.PayementJour = Convert.ToDecimal(source3.First().Montant);
            }
            return detailFacture;
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (!(sender is IconButton))
                return;
            openFileDialog.Filter = "(*.pdf; *.xls;*.docs)| *.pdf; *.xls; *.docs";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                filename = Path.GetFileName(filePath);
            }
        }

        private void txtMontant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '\b')
                return;
            e.Handled = true;
        }

        private void txtMontant_TextChanged(object sender, EventArgs e)
        {
            if (!(txtMontant.Text != ""))
                return;
            txtMontant.Text = Convert.ToDecimal(txtMontant.Text).ToString("N0");
            txtMontant.SelectionStart = txtMontant.Text.Length;
        }

        private void rEspèce_CheckedChanged(object sender, EventArgs e) => operation = ((Control)sender).Text;
    }
}
