namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_stock_produits_vente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(80)]
        public string Code_Barre { get; set; }

        [StringLength(100)]
        public string Marque { get; set; }

        [StringLength(100)]
        public string Catégorie { get; set; }

        [StringLength(100)]
        public string Taille { get; set; }

        [StringLength(100)]
        public string Type { get; set; }

        [StringLength(50)]
        public string Detachement { get; set; }

        public decimal? Quantité { get; set; }

        public int? Formule { get; set; }

        [StringLength(150)]
        public string Details { get; set; }

        public int? Product_Id { get; set; }
    }
}
