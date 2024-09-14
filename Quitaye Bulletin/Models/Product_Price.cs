using Quitaye_School.User_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class Product_Price
    {
        public int? Id { get; set; }
        public string Nom { get; set; }
        public string Catégorie { get; set; }
        public string Taille { get; set; }
        public decimal? Quantité { get; set; }
        public int? Formule { get; set; }
        public string Code_Barre { get; set; }
        public decimal? Prix_Achat { get; set; }
        public decimal? Perte { get; set; }
        public decimal? Prix_Achat_Petit { get; set; }
        public decimal? Prix_Vente { get; set; }
        public string Type { get; set; }
        public string Filiale { get; set; }
        public Quitaye_School.Models.Context.tbl_produits Produits { get; set; }
        public Quitaye_School.Models.Context.tbl_arrivée Arrivée { get; set; }
        public Quitaye_School.Models.Context.tbl_vente Vente { get; set; }
        public string Désignation { get; set; }
        public int Product_Id { get; set; }
    }
}
