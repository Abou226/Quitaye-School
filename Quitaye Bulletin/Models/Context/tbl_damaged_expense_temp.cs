namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_damaged_expense_temp
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Barcode { get; set; }

        [StringLength(100)]
        public string Produit { get; set; }

        [StringLength(100)]
        public string Catégorie { get; set; }

        [StringLength(100)]
        public string Taille { get; set; }

        [StringLength(100)]
        public string Type_Base { get; set; }

        [StringLength(100)]
        public string Usage { get; set; }

        [StringLength(150)]
        public string Client { get; set; }

        [StringLength(50)]
        public string Num_Client { get; set; }

        public decimal? Quantité { get; set; }

        public decimal? Prix_Unité { get; set; }

        public decimal? Montant { get; set; }

        public DateTime? Date_Damaged { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        [StringLength(50)]
        public string Dept_Auteur { get; set; }

        [StringLength(50)]
        public string Mesure { get; set; }

        public int? Id_Client { get; set; }

        [StringLength(50)]
        public string Filiale { get; set; }

        [StringLength(50)]
        public string Pending { get; set; }

        public int? Product_Id { get; set; }
    }
}
