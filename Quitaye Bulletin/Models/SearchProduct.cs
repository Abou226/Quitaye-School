using Quitaye_School.Models;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.User_Interface
{
    public class SearchProduct
    {
        public static Task<ProductObject> SearchCodeAsync(string code)
        {
            return Task.Factory.StartNew(() => SearchCode(code));
        }
        private static ProductObject SearchCode(string code)
        {
            using (var donnée = new QuitayeContext())
            {
                var result = (from d in donnée.tbl_multi_barcode
                              where d.Barcode == code
                              join pr in donnée.tbl_produits on d.Product_Id equals pr.Id into joinedTable
                              from p in joinedTable.DefaultIfEmpty()
                              join st in donnée.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into joinedTable1
                              from s in joinedTable1.DefaultIfEmpty()
                              where s.Detachement == LogIn.filiale
                              select new ProductObject()
                              {
                                  Id = d.Id,
                                  Marque = s.Marque,
                                  Catégorie = s.Catégorie,
                                  Quantité = (s.Quantité),
                                  Taille = s.Taille,
                                  Stock = s.Quantité,
                                  Prix_Grand = (p.Prix_Grand),
                                  Prix_Petit = (p.Prix_Petit),
                                  Prix_Moyen = (p.Prix_Moyen),
                                  Prix_Large = (p.Prix_Large),
                                  Prix_Hyper_Large = (p.Prix_Hyper_Large),
                                  Prix_Achat_Petit = (p.Prix_Achat_Petit),
                                  Prix_Achat_Moyen = (p.Prix_Achat_Moyen),
                                  Prix_Achat_Grand = (p.Prix_Achat_Grand),
                                  Prix_Achat_Large = (p.Prix_Achat_Large),
                                  Prix_Achat_Hyper_Large = (p.Prix_Achat_Hyper_Large),
                                  Type = p.Type,
                              }).FirstOrDefault();

                return result;
            }
        }

        
        public static async Task<ProductObject> SearchCodeAsync(string code, string filiale)
        {
            using (var donnée = new QuitayeContext())
            {
                var result = (from d in donnée.tbl_multi_barcode
                              where d.Barcode == code 
                              join pr in donnée.tbl_produits on d.Product_Id equals pr.Id into joinedTable
                              from p in joinedTable.DefaultIfEmpty()
                              join st in donnée.tbl_stock_produits_vente on d.Product_Id equals st.Product_Id into joinedTable1
                              from s in joinedTable1.DefaultIfEmpty()
                              where s.Detachement == filiale
                              select new ProductObject()
                              {
                                  Id = d.Id,
                                  Marque = s.Marque,
                                  Catégorie = s.Catégorie,
                                  Quantité = s.Quantité,
                                  Taille = s.Taille,
                                  Formule_Stockage = s.Formule.ToString(),
                                  Prix_Grand = p.Prix_Grand,
                                  Prix_Petit = p.Prix_Petit,
                                  Prix_Moyen = p.Prix_Moyen,
                                  Prix_Large = p.Prix_Large,
                                  Prix_Hyper_Large = p.Prix_Hyper_Large,
                                  Prix_Achat_Petit = p.Prix_Achat_Petit,
                                  Prix_Achat_Moyen = p.Prix_Achat_Moyen,
                                  Prix_Achat_Grand = p.Prix_Achat_Grand,
                                  Prix_Achat_Large = p.Prix_Achat_Large,
                                  Prix_Achat_Hyper_Large = p.Prix_Achat_Hyper_Large,
                                  Type = p.Type,
                                  Source = s.Type,
                                  Stock = s.Quantité,
                                  Code_Barre = s.Code_Barre
                              }).FirstOrDefault();
                if(result  != null && result.Type == null)
                {
                    var dsd = donnée.tbl_produits.Where(x => x.Barcode == code).FirstOrDefault();
                    if(dsd == null)
                    {
                        var pdsd = donnée.tbl_produits.Where(x => x.Nom == result.Marque && x.Catégorie == result.Catégorie && x.Taille == result.Taille && x.Type == result.Source).FirstOrDefault();
                        if(pdsd == null)
                        {
                            //var pdsf = await Page_Importation.NewProduct(result.Source, "1", result.Marque, result.Catégorie, result.Taille, 0, 0, "PIECE", Convert.ToInt32(result.Formule_Stockage));

                        }else
                        {
                            pdsd.Barcode = code;
                            await donnée.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        dsd.Type = result.Source;
                        await donnée.SaveChangesAsync();
                    }
                    
                    result = (from d in donnée.tbl_stock_produits_vente
                              where d.Code_Barre == code && d.Detachement == filiale
                              join p in donnée.tbl_produits on d.Code_Barre equals p.Barcode into joinedTable
                              from f in joinedTable.DefaultIfEmpty()
                              select new ProductObject()
                              {
                                  Id = d.Id,
                                  Marque = d.Marque,
                                  Catégorie = d.Catégorie,
                                  Quantité = d.Quantité,
                                  Taille = d.Taille,
                                  Prix_Grand = f.Prix_Grand,
                                  Prix_Petit = f.Prix_Petit,
                                  Prix_Moyen = f.Prix_Moyen,
                                  Prix_Large = f.Prix_Large,
                                  Prix_Hyper_Large = f.Prix_Hyper_Large,
                                  Prix_Achat_Petit = f.Prix_Achat_Petit,
                                  Prix_Achat_Moyen = f.Prix_Achat_Moyen,
                                  Prix_Achat_Grand = f.Prix_Achat_Grand,
                                  Prix_Achat_Large = f.Prix_Achat_Large,
                                  Prix_Achat_Hyper_Large = f.Prix_Achat_Hyper_Large,
                                  Type = f.Type,
                                  Code_Barre = d.Code_Barre,
                                  Stock = d.Quantité
                              }).FirstOrDefault();
                }

                return result;
            }
        }


        public static Task<ProductObject> SearchRefAsync(string code)
        {
            return Task.Factory.StartNew(() => SearchRef(code));
        }
        private static ProductObject SearchRef(string code)
        {
            ProductObject p = new ProductObject();
            //using (var donnée = new DonnéeFactoryDataContext(LogIn.mycontrng))
            //{
            //    //var der = from d in donnée.tbl_ where d.Code_Barre == code select d;
            //    //var deses = from d in donnée.tbl_stock_gadgets where d.Code_Barre == code && d.Détachement == detachement select d;
            //    //if (der.Count() != 0 && deses.Count() != 0)
            //    //{
            //    //    var des = (from d in donnée.tbl_gadgets where d.Code_Barre == code select d).First();
            //    //    var dese = (from d in donnée.tbl_stock_gadgets where d.Code_Barre == code && d.Détachement == detachement select d).First();
            //    //    p.Marque = des.Marque;
            //    //    p.Catégorie = des.Catégorie;
            //    //    p.Mesure = des.Usage;
            //    //    p.Taille = des.Taille;
            //    //    p.Stock = Convert.ToDecimal(dese.Quantité);
            //    //    p.Type = "Gadget";
            //    //    p.Code_Barre = code.ToUpper();
            //    //    p.Source = detachement;
            //    //}
            //    //else
            //    {
            //        var derre = from d in donnée.tbl_matière_premières where d.Reference == code select d;
            //        if (derre.Count() != 0)
            //        {
            //            var des = (from d in donnée.tbl_matière_premières where d.Reference == code select d).First();
            //            p.Marque = des.Nom;
            //            p.Catégorie = des.Mesure;

            //            p.Type = "Matière Première";
            //            p.Code_Barre = code.ToUpper();

            //            p.Mesure = des.Mesure;
            //        }
            //    }
            //}
            return p;
        }

    }
}
