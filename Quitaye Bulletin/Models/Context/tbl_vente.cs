namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_vente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Barcode { get; set; }

        [StringLength(50)]
        public string Produit { get; set; }

        [StringLength(50)]
        public string Catégorie { get; set; }

        [StringLength(50)]
        public string Taille { get; set; }

        [StringLength(50)]
        public string Usage { get; set; }

        [StringLength(50)]
        public string Client { get; set; }

        [StringLength(50)]
        public string Num_Client { get; set; }

        public decimal? Quantité { get; set; }

        public decimal? Prix_Unité { get; set; }

        public decimal? Montant { get; set; }

        public DateTime? Date_Vente { get; set; }

        [StringLength(50)]
        public string Auteur { get; set; }

        [StringLength(50)]
        public string Dept_Auteur { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(100)]
        public string Type_Base { get; set; }

        [StringLength(50)]
        public string Num_Vente { get; set; }

        public DateTime? Date_Action { get; set; }

        public DateTime? Date_Payement { get; set; }

        [StringLength(150)]
        public string Auteur_Payement { get; set; }

        [StringLength(50)]
        public string Mesure { get; set; }

        public decimal? Q_Unité { get; set; }

        [Column("Mode Payement")]
        [StringLength(50)]
        public string Mode_Payement { get; set; }

        public int? Id_Client { get; set; }

        [StringLength(50)]
        public string Filiale { get; set; }

        [StringLength(20)]
        public string Cloturé { get; set; }

        public DateTime? Date_Expiration { get; set; }

        public decimal? Reduction { get; set; }

        public int? Product_Id { get; set; }
    }
}
