using DocumentFormat.OpenXml.Wordprocessing;
using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using Quitaye_Medical.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class RavitaillementStock : Form
    {
        private ProductObject p = new ProductObject();
        private bool first = true;
        public bool enable = false;
        public List<OpérationTemp> listvente = new List<OpérationTemp>();
        public string ok;
        private Timer startTimer = new Timer();
        public string Filiale { get; }
        public List<string> Code { get; set; }
        public RavitaillementStock(string filaile, List<string> code = null)
        {
            InitializeComponent();
            Filiale = filaile;
            startTimer.Enabled = false;
            startTimer.Interval = 10;
            if(code != null)
            {
                cbxCode.Text = code.FirstOrDefault();
                Code = code;
            }
            
            startTimer.Start();
            startTimer.Tick += StartTimer_Tick;
            btnFermer.Click += BtnFermer_Click;
            btnAjouter.Click += BtnAjouter_Click;
            cbxCode.TextChanged += CbxCode_TextChanged;
            btnEnregistrer.Click += BtnEnregistrer_Click;
            dataGridView1.CellClick += DataGridView1_CellClick;
            cbxDetails.SelectedIndexChanged += CbxDetails_SelectedIndexChanged;
            btnSaveCode.Click += BtnSaveCode_Click;
        }

        private async void CbxDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (first || cbxDetails.Text.Length <= 0
                || !(cbxDetails.Text != ""))
                return;
            List<string> red = new List<string>();
            string[] array1 = cbxDetails.Text.Split('-');
            if (array1.Length >= 2)
            {
                string[] array2 = array1[1].Split(',');
                List<string> le = new List<string>();
                le.Add(array1[0]);
                le.Add(array2[0]);
                var serd = array2[1].Split('_');
                le.Add(serd[0]);
                if (cbxDetails.Text.Contains("_"))
                {
                    string[] array3 = cbxDetails.Text.Split('_');
                    le.Add(array3[1]);
                }

                if (Principales.type_compte.Contains("Administrateur") && Filiale != "")
                {
                    var productObject = await AchatVente.SearchObjectAsync(le, Filiale);
                    p = productObject;
                    productObject = null;
                }
                else
                {
                    var productObject = await AchatVente.SearchObjectAsync(le, Principales.filiale);
                    p = productObject;
                    productObject = null;
                }
                if(p != null)
                {
                    cbxCode.Text = p.Code_Barre;
                    txtProduit.Text = $"{p.Marque} {p.Catégorie} {p.Taille} {p.Type}";
                }

                if (p.Code_Barre == null)
                {
                    txtNewCode.Focus();
                    Alert.SShow("Cet élément n'a pas de code barre. Veillez attribuer un code barre.", Alert.AlertType.Info);
                }
                else
                    txtQuantité.Focus();
                array2 = null;
                le = null;
            }
            red = null;
            array1 = null;
        }

        private async void BtnSaveCode_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewCode.Text) && p != null)
            {
                var result = await SaveChangesAsync(txtNewCode.Text);
                if (result)
                {
                    Alert.SShow("Attribution effectuée avec succès.", Alert.AlertType.Sucess);
                    txtNewCode.Text = null;
                }
            }
        }

        private async Task<bool> SaveChangesAsync(string code)
        {
            using (var donnée = new QuitayeContext())
            {
                var item = (from d in donnée.tbl_stock_produits_vente where d.Id == p.Id select d).FirstOrDefault();
                if (item != null)
                {
                    item.Code_Barre = code;
                    var stock = (from d in donnée.tbl_stock_produits_vente
                                 where d.Marque == item.Marque && d.Catégorie == item.Catégorie
                                 && d.Taille == item.Taille && d.Type == item.Type
                                 select d);
                    foreach (var itemd in stock)
                    {
                        itemd.Code_Barre = code;
                    }

                    var pro = (from d in donnée.tbl_produits
                               where d.Nom == item.Marque
                               && d.Catégorie == item.Catégorie
                               && d.Taille == item.Taille
                               && d.Type == item.Type
                               select d);
                    if (pro != null)
                    {
                        pro.FirstOrDefault().Barcode = code;
                    }

                    await donnée.SaveChangesAsync();
                    return true;
                }
                else return false;
            }
        }


        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!dataGridView1.Columns[e.ColumnIndex].Name.Equals("Sup"))
                return;
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
            if (await DeleteDataAsync(id))
            {
                Alert.SShow("Element supprimer avec succès.", Alert.AlertType.Sucess);
                await CallTaskSecond();
            }
        }

        
        private async Task<bool> DeleteDataAsync(int id)
        {
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_ravitaillement_temp.Where(d => d.Id == id).Take(1);
                if (source.Count() == 0)
                    return false;
                var entity = source.First();
                financeDataContext.tbl_ravitaillement_temp.Remove(entity);
                
                await financeDataContext.SaveChangesAsync();
                return true;
            }
        }

        private async void BtnEnregistrer_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
                return;
            await SaveAll();
            ok = "Oui";
        }

        private async void CbxCode_TextChanged(object sender, EventArgs e)
        {
            if (first)
                return;
            var prod = await SearchProduct.SearchCodeAsync(cbxCode.Text, Filiale);
            if (prod != null && prod.Marque != null && prod.Catégorie != null)
            {
                p = prod;
                txtProduit.Text = prod.Marque + "-" + prod.Taille + ", " + prod.Catégorie;
            }
            else
                txtProduit.Text = null;
            prod = null;
        }

        private async void BtnAjouter_Click(object sender, EventArgs e)
        {
            if (!(txtProduit.Text != "") || !(cbxMesure.Text != "") || !(txtQuantité.Text != ""))
                return;
            if (await AddingTemp(cbxCode.Text, Convert.ToDecimal(txtQuantité.Text)))
            {
                cbxCode.Text = null;
                txtQuantité.Clear();
                txtProduit.Clear();
                await CallTaskSecond();
            }
        }



        private async Task<bool> AddingTemp(string code, Decimal quantité)
        {
            bool result = false;
            if (code != "" && quantité != 0M && p != null)
            {
                OpérationTemp vente = new OpérationTemp();
                vente.Marque = p.Marque;
                vente.Taille = p.Taille;
                vente.Catégorie = p.Catégorie;
                vente.Quantité = Convert.ToInt32(quantité);
                vente.Mesure = cbxMesure.Text;
                vente.Model = p.Type;
                vente.Code_Barre = code;
                vente.Detachement = Filiale;
                vente.Date_Expiration = Date_Expiration.Value.Date;
                result = await AddTempAsync(vente);
                vente = null;
            }
            return result;
        }

        private async Task SaveAll()
        {
            foreach (OpérationTemp item in listvente)
            {
                if (await SaveSingleStockAsync(item))
                    item.Saved = true;
            }
            listvente = listvente.Where((d => d.Saved)).ToList();
            await CallTaskSecond();
            Alert.SShow("Ravitaillement Stock effectué avec succès.", Alert.AlertType.Sucess);
            if (!enable)
                return;
            Close();
        }

        
        private async Task<bool> SaveSingleStockAsync(OpérationTemp vente)
        {

#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
            try
            {
                using (var financeDataContext = new QuitayeContext())
                {
                    var source1 = financeDataContext.tbl_ravitaillement
                        .OrderByDescending(d => d.Id).Select(d => new
                        {
                            Id = d.Id
                        }).Take(1);
                    int num1 = 1;
                    if (source1.Count() != 0)
                    {
                        var data = source1.First();
                        num1 = data.Id + 1;
                    }
                    financeDataContext.tbl_ravitaillement.Add(new Models.Context.tbl_ravitaillement()
                    {
                        Id = num1,
                        Fiaile = Filiale,
                        Marque = vente.Marque,
                        Catégorie = vente.Catégorie,
                        Quantité = new Decimal?((Decimal)vente.Quantité),
                        Taille = vente.Taille,
                        Mesure = vente.Mesure,
                        Auteur = Principales.profile,
                        Code_Barre = vente.Code_Barre,
                        Date = new DateTime?(vente.Date),
                        Date_Expiration = vente.Date_Expiration,
                        Product_Id = vente.Product_Id,
                    });

                    var hist_id = 1;
                    var histg = financeDataContext.tbl_historique_expiration.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();
                    hist_id = histg + 1;
                    var new_hist = new tbl_historique_expiration();
                    new_hist.Id = hist_id;
                    new_hist.Barcode = vente.Code_Barre;
                    new_hist.Filiale = Filiale;
                    new_hist.Quantité = vente.Quantité;
                    new_hist.Type = "Ravitaillement";
                    new_hist.Id_Opération = num1;
                    new_hist.Date = vente.Date;
                    new_hist.Date_Expiration = vente.Date_Expiration;
                    new_hist.Auteur = Principales.profile;
                    new_hist.Product_Id = vente.Product_Id;
                    financeDataContext.tbl_historique_expiration.Add(new_hist);

                    financeDataContext.tbl_expiration.Add(new Models.Context.tbl_expiration()
                    {
                        Quantité = new Decimal?((Decimal)vente.Quantité),
                        Mesure = vente.Mesure,
                        Code_Barre = vente.Code_Barre,
                        Date = new DateTime?(vente.Date),
                        Date_Expiration = vente.Date_Expiration,
                        Reste = vente.Quantité,
                        Filiale = Filiale,
                        Product_Id = vente.Product_Id,
                    });

                    var tblMesureVente = financeDataContext.tbl_mesure_vente
                        .Where(d => d.Nom == vente.Mesure).First();
                    var stock = new Models.Context.tbl_stock_produits_vente();
                    decimal num2 = 0M;
                    var entity = financeDataContext.tbl_ravitaillement_temp
                        .Where(d => d.Id == vente.id).First();
                    financeDataContext.tbl_ravitaillement_temp.Remove(entity);
                    stock = financeDataContext.tbl_stock_produits_vente
                                .Where(d => d.Product_Id == vente.Product_Id
                                && d.Detachement == Filiale).FirstOrDefault();
                    var formuleMesureVente = financeDataContext
                        .tbl_formule_mesure_vente.Where(d => d.Id == stock.Formule).First();
                    int? niveau1 = tblMesureVente.Niveau;
                    int num3 = 1;
                    decimal? quantité;
                    var historique = financeDataContext.tbl_historique_valeur_stock.Where(x => x.Code_Barre == stock.Code_Barre
                        && x.Filiale == stock.Detachement && x.Date.HasValue
                    && DbFunctions.TruncateTime(x.Date) == DbFunctions.TruncateTime(DateTime.Now)).FirstOrDefault();

                    if (niveau1.GetValueOrDefault() == num3 & niveau1.HasValue)
                    {
                        quantité = stock.Quantité;
                        decimal quantité2 = vente.Quantité;
                        stock.Quantité += quantité2;
                        num2 = vente.Quantité;
                    }
                    else
                    {
                        int? niveau2 = tblMesureVente.Niveau;
                        int num4 = 2;
                        if (niveau2.GetValueOrDefault() == num4 & niveau2.HasValue)
                        {
                            decimal num5 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Moyen);
                            var quantité3 = stock.Quantité;
                            var num6 = Convert.ToDecimal(vente.Quantité) * num5;
                            stock.Quantité = quantité3.HasValue ? new Decimal?(quantité3.GetValueOrDefault() + num6) : new Decimal?();
                            num2 = Convert.ToDecimal(vente.Quantité) * num5;

                        }
                        else
                        {
                            int? niveau3 = tblMesureVente.Niveau;
                            int num7 = 3;
                            if (niveau3.GetValueOrDefault() == num7 & niveau3.HasValue)
                            {
                                decimal num8 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Grand);
                                decimal? quantité4 = stock.Quantité;
                                decimal num9 = Convert.ToDecimal(vente.Quantité) * num8;
                                stock.Quantité = quantité4.HasValue ? new Decimal?(quantité4.GetValueOrDefault() + num9) : new Decimal?();
                                num2 = Convert.ToDecimal(vente.Quantité) * num8;
                            }
                            else
                            {
                                int? niveau4 = tblMesureVente.Niveau;
                                int num10 = 4;
                                if (niveau4.GetValueOrDefault() == num10 & niveau4.HasValue)
                                {
                                    decimal num11 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Large);
                                    decimal? quantité5 = stock.Quantité;
                                    decimal num12 = Convert.ToDecimal(vente.Quantité) * num11;
                                    stock.Quantité = quantité5.HasValue ? new Decimal?(quantité5.GetValueOrDefault() + num12) : new Decimal?();
                                    num2 = Convert.ToDecimal(vente.Quantité) * num11;
                                }
                                else
                                {
                                    int? niveau5 = tblMesureVente.Niveau;
                                    int num13 = 5;
                                    if (niveau5.GetValueOrDefault() == num13 & niveau5.HasValue)
                                    {
                                        decimal num14 = Convert.ToDecimal(formuleMesureVente.Petit) / Convert.ToDecimal(formuleMesureVente.Hyper_Large);
                                        decimal? quantité6 = stock.Quantité;
                                        decimal num15 = Convert.ToDecimal(vente.Quantité) * num14;
                                        stock.Quantité = quantité6.HasValue ? new Decimal?(quantité6.GetValueOrDefault() + num15) : new Decimal?();
                                        num2 = Convert.ToDecimal(vente.Quantité) * num14;
                                    }
                                }
                            }
                        }
                    }
                    if (stock != null)
                    {
                        stock = financeDataContext.tbl_stock_produits_vente.Where(d => d.Code_Barre == vente.Code_Barre
                        && d.Detachement == Filiale).First();
                        quantité = stock.Quantité;
                        if (historique != null)
                        {
                            historique.Quantité = stock.Quantité;
                        }
                    }
                    else
                    {
                        int num17 = 1;
                        var source2 = financeDataContext.tbl_stock_produits_vente.OrderByDescending(d => d.Id).Select(d => new
                        {
                            Id = d.Id
                        });
                        if (source2.Count() != 0)
                        {
                            var data = source2.First();
                            num17 = Convert.ToInt32(data.Id) + 1;
                        }
                        financeDataContext.tbl_stock_produits_vente.Add(new Models.Context.tbl_stock_produits_vente()
                        {
                            Id = num17,
                            Marque = vente.Marque,
                            Code_Barre = vente.Code_Barre,
                            Catégorie = vente.Catégorie,
                            Taille = vente.Taille,
                            Quantité = new Decimal?(num2),
                            Formule = new int?(formuleMesureVente.Id),
                            Detachement = Filiale
                        });

                        var hist = 1;
                        var dsv = (from d in financeDataContext.tbl_historique_valeur_stock
                                   orderby d.Id descending
                                   select d).FirstOrDefault();
                        if (dsv != null)
                            hist = Convert.ToInt32(dsv.Id) + 1;
                        var h = new Models.Context.tbl_historique_valeur_stock();
                        h.Code_Barre = vente.Code_Barre;
                        h.Filiale = vente.Filiale;
                        h.Date = DateTimeOffset.Now;
                        h.Prix_Grand = vente.Prix_Grand;
                        h.Prix_Petit = vente.Prix_Petit;
                        h.Prix_Moyen = vente.Prix_Moyen;
                        h.Prix_Large = vente.Prix_Large;
                        h.Prix_Hyper_Large = Convert.ToDecimal(vente.Prix_Hyper_Large);
                        h.Quantité = vente.Quantité;
                        h.Product_Id = vente.Product_Id;
                        h.Id = hist;
                        financeDataContext.tbl_historique_valeur_stock.Add(historique);

                        await financeDataContext.SaveChangesAsync();
                    }

                    await financeDataContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
        }

        
        private async Task<bool> AddTempAsync(OpérationTemp opération)
        {
            using (var financeDataContext = new QuitayeContext())
            {
                int num = 1;
                var source = financeDataContext.tbl_ravitaillement_temp
                            .OrderByDescending(d => d.Id).Select(d => new
                            {
                                Id = d.Id
                            });
                if (source.Count() != 0)
                {
                    var data = source.First();
                    num = data.Id + 1;
                }
                var prod_id = financeDataContext.tbl_multi_barcode.Where(x => x.Barcode == opération.Code_Barre).Select(x => x.Product_Id).FirstOrDefault();
                financeDataContext.tbl_ravitaillement_temp.Add(new Models.Context.tbl_ravitaillement_temp()
                {
                    Id = num,
                    Marque = opération.Marque,
                    Catégorie = opération.Catégorie,
                    Taille = opération.Taille,
                    Code_Barre = opération.Code_Barre,
                    Quantité = new Decimal?((Decimal)opération.Quantité),
                    Date = new DateTime?(DateTime.Now),
                    Mesure = opération.Mesure,
                    Fiaile = Filiale,
                    Auteur = Principales.profile,
                    Date_Expiration = opération.Date_Expiration,
                    Product_Id = prod_id
                });
                await financeDataContext.SaveChangesAsync();
                return true;
            }
        }

        private void BtnFermer_Click(object sender, EventArgs e) => Close();

        private async void StartTimer_Tick(object sender, EventArgs e)
        {
            startTimer.Stop();
            await CallTask();
            
            if (Code != null)
                cbxCode.Text = Code.FirstOrDefault();
            CbxCode_TextChanged(sender, e);
        }

        private async Task CallMesure()
        {
            MesureTable result = await FillMesureAsync();
            cbxMesure.DataSource = result;
            cbxMesure.DisplayMember = "Mesure";
            cbxMesure.ValueMember = "Id";
            cbxMesure.Text = null;
            result = null;
        }

        private Task<MesureTable> FillMesureAsync() => Task.Factory.StartNew(() => FillMesure());

        private MesureTable FillMesure()
        {
            MesureTable mesureTable = new MesureTable();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Mesure");
            using (var financeDataContext = new QuitayeContext())
            {
                var source = financeDataContext.tbl_mesure_vente.OrderBy(d => d.Nom).Select(d => new
                {
                    Id = d.Niveau,
                    Nom = d.Nom,
                    Default = d.Default,
                    Type = d.Type
                });
                if (source.Count() != 0)
                {
                    mesureTable.RowsLists = new List<RowsList>();
                    foreach (var data in source)
                    {
                        RowsList rowsList = new RowsList();
                        DataRow row = dataTable.NewRow();
                        row[0] = data.Id;
                        row[1] = data.Nom;
                        if (data.Default == "Oui")
                        {
                            rowsList.Default = true;
                            rowsList.Mesure = data.Nom;
                            rowsList.Type = data.Type;
                        }
                        else
                            rowsList.Default = false;
                        mesureTable.RowsLists.Add(rowsList);
                        dataTable.Rows.Add(row);
                    }
                }
                mesureTable.Table = dataTable;
            }
            return mesureTable;
        }

        private async Task CallTask()
        {
            Task<MesureTable> mesure = FillMesureAsync();
            Task<DataTable> fillcod = FillCodeAsync();
            Task<List<OpérationTemp>> ventelist = RavitaillementListAsync();
            Task<DataTable> filldata = FillDataAsync();
            Task<DataTable> fillproduit = FillProduiAsync();
            List<Task> taskList = new List<Task>()
            {
                filldata,
                ventelist,
                fillcod,
                fillproduit,
                mesure
            };
            while (taskList.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(taskList);
                if (finishedTask == filldata)
                {
                    first = false;
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = filldata.Result;
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                    try
                    {
                        AddColumns.Addcolumn(dataGridView1);
                        dataGridView1.Columns["edit"].Visible = false;
                    }
                    catch (Exception ex)
                    {
                    }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
                }else if(finishedTask == fillproduit)
                {
                    cbxDetails.DataSource = fillproduit.Result;
                    cbxDetails.DisplayMember = "Nom";
                    cbxDetails.ValueMember = "Id";
                    cbxDetails.Text = null;
                    cbxDetails.Visible = true;
                }
                else if (finishedTask == mesure)
                {
                    cbxMesure.DataSource = mesure.Result.Table;
                    cbxMesure.DisplayMember = "Mesure";
                    cbxMesure.ValueMember = "Id";
                    if (mesure.Result.RowsLists != null && mesure.Result.RowsLists.Count > 0)
                    {
                        foreach (RowsList rowsList in mesure.Result.RowsLists)
                        {
                            RowsList item = rowsList;
                            if (item.Default)
                            {
                                cbxMesure.Text = item.Mesure;
                                break;
                            }
                            cbxMesure.Text = null;
                            item = (RowsList)null;
                        }
                    }
                    else
                        cbxMesure.Text = null;
                }
                else if (finishedTask == ventelist)
                    listvente = ventelist.Result;
                else if (finishedTask == fillcod)
                {
                    cbxCode.DataSource = fillcod.Result;
                    cbxCode.DisplayMember = "Nom";
                    cbxCode.ValueMember = "Id";
                    cbxCode.Text = null;
                    cbxCode.Visible = true;
                }
                taskList.Remove(finishedTask);
                finishedTask = null;
            }
            mesure = null;
            fillcod = null;
            ventelist = null;
            filldata = null;
            taskList = null;
        }
        private async Task CallTaskSecond()
        {
            Task<List<OpérationTemp>> ventelist = RavitaillementListAsync();
            Task<DataTable> filldata = FillDataAsync();
            List<Task> taskList = new List<Task>()
            {
                filldata,
                ventelist
            };
            while (taskList.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(taskList);
                if (finishedTask == filldata)
                {
                    first = false;
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = filldata.Result;
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                    try
                    {
                        AddColumns.Addcolumn(dataGridView1);
                        dataGridView1.Columns["edit"].Visible = false;
                    }
                    catch (Exception ex)
                    {
                    }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
                }
                
                else if (finishedTask == ventelist)
                    listvente = ventelist.Result;
                
                taskList.Remove(finishedTask);
                finishedTask = null;
            }
            ventelist = null;
            filldata = null;
            taskList = null;
        }

        private Task<List<OpérationTemp>> RavitaillementListAsync() => Task.Factory.StartNew(() => RavitaillementList());

        private List<OpérationTemp> RavitaillementList()
        {
            List<OpérationTemp> opérationTempList = new List<OpérationTemp>();
            using (var donnée = new QuitayeContext())
            {
                var source = (from d in donnée.tbl_ravitaillement_temp 
                             where d.Auteur == Principales.profile && d.Fiaile == Filiale
                             join pr in donnée.tbl_produits on d.Product_Id equals pr.Id into pro
                             from p in pro.DefaultIfEmpty()
                             select new
                            {
                                Id = d.Id,
                                Marque = d.Marque,
                                Catégorie = d.Catégorie,
                                Taille = d.Taille,
                                Quantité = d.Quantité,
                                Date = d.Date,
                                Code_Barre = d.Code_Barre,
                                Filiale = d.Fiaile,
                                Date_Expiration = d.Date_Expiration,
                                Mesure = d.Mesure,
                                 Prix_Petit = p.Prix_Petit,
                                 Prix_Moyen = p.Prix_Moyen,
                                 Prix_Grand = p.Prix_Grand,
                                 Prix_Large = p.Prix_Large,
                                 Prix_Hyper_Large = p.Prix_Hyper_Large,
                                 Product_Id = d.Product_Id,
                             }).ToList();
                foreach (var data in source)
                {
                    opérationTempList.Add(new OpérationTemp()
                    {
                        id = data.Id,
                        Marque = data.Marque,
                        Catégorie = data.Catégorie,
                        Taille = data.Taille,
                        Quantité = Convert.ToInt32(data.Quantité),
                        Date = Convert.ToDateTime(data.Date),
                        Code_Barre = data.Code_Barre,
                        Mesure = data.Mesure, 
                        Date_Expiration = data.Date_Expiration.Value,
                        Prix_Petit = Convert.ToDecimal(data.Prix_Petit),
                        Prix_Moyen = Convert.ToDecimal(data.Prix_Moyen),
                        Prix_Grand = Convert.ToDecimal(data.Prix_Grand),
                        Prix_Large = Convert.ToDecimal(data.Prix_Large),
                        Prix_Hyper_Large = Convert.ToDecimal(data.Prix_Hyper_Large),
                        Product_Id = Convert.ToInt32(data.Product_Id),
                    });
                }
            }
            return opérationTempList;
        }

        private async Task CallData()
        {
            DataTable result = await FillDataAsync();
            dataGridView1.Columns.Clear();
            if (result.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Tableau Vide");
                DataRow dr = dt.NewRow();
                dr[0] = "Aucune donnée dans ce tableau !";
                dt.Rows.Add(dr);
                dataGridView1.DataSource = dt;
                dt = null;
                dr = null;
                result = null;
            }
            else
            {
                dataGridView1.DataSource = result;
#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                try
                {
                    AddColumns.Addcolumn(dataGridView1);
                    dataGridView1.Columns["Edit"].Visible = false;
                }
                catch (Exception ex)
                {
                }
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
                result = null;
            }
        }

        private Task<DataTable> FillDataAsync() => Task.Factory.StartNew<DataTable>((() => FillData()));

        private DataTable FillData()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Code_Barre");
            dataTable.Columns.Add("Désignation");
            dataTable.Columns.Add("Quantité", typeof(Decimal));
            dataTable.Columns.Add("Mesure");
            dataTable.Columns.Add("Filiale");
            dataTable.Columns.Add("Date_Expiration");
            using (var donnée = new QuitayeContext())
            {
                var dat = from d in donnée.tbl_ravitaillement_temp
                          where d.Auteur == Principales.profile && d.Fiaile == Filiale
                          select new
                          {
                              Id = d.Id,
                              Marque = d.Marque,
                              Catégorie = d.Catégorie,
                              Taille = d.Taille,
                              Quantité = d.Quantité,
                              Code = d.Code_Barre,
                              Filiale = d.Fiaile,
                              Mesure = d.Mesure, 
                              Date_Expiration = d.Date_Expiration,
                          };
                
                foreach (var data in dat)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Code;
                    row[2] = data.Marque + "-" + data.Taille + ", " + data.Catégorie;
                    row[3] = data.Quantité;
                    row[4] = data.Mesure;
                    row[5] = data.Filiale;
                    row["Date_Expiration"] = data.Date_Expiration.Value.ToString("dd/MM/yyyy");
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }

        private Task<DataTable> FillCodeAsync() => Task.Factory.StartNew(() => FillCode());

        private DataTable FillCode()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Nom");
            using (var donnée = new QuitayeContext())
            {
                var datser = from d in donnée.tbl_stock_produits_vente
                             where d.Detachement == Filiale && d.Code_Barre != null
                             orderby d.Code_Barre descending
                             select new { Id = d.Id, Code = d.Code_Barre };
                
                foreach (var data in datser)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    row[1] = data.Code;
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }
        
        private Task<DataTable> FillProduiAsync() => Task.Factory.StartNew((() => FillProduit()));

        private DataTable FillProduit()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Nom");
            using (var donnée = new QuitayeContext())
            {
                var datser = (from d in donnée.tbl_stock_produits_vente
                             where d.Detachement == Filiale 
                             select new { Id = d.Id,
                                 Marque = d.Marque,
                                 Catégorie = d.Catégorie,
                                 Taille = d.Taille,
                                 Type = d.Type,
                             }).ToList();
                
                foreach (var data in datser)
                {
                    DataRow row = dataTable.NewRow();
                    row[0] = data.Id;
                    if (data.Type == null)
                    {
                        if (data.Taille == null)
                            row[1] = (data.Marque + "-, " + data.Catégorie);
                        else row[1] = (data.Marque + "-" + data.Taille.ToString() + ", " + data.Catégorie);
                    }
                    else
                    {
                        if (data.Taille == null)
                            row[1] = (data.Marque + "-, " + data.Catégorie + "_" + data.Type);
                        else
                            row[1] = (data.Marque + "-" + data.Taille.ToString() + ", " + data.Catégorie + "_" + data.Type);
                    }
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }
    }
}
