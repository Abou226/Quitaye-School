namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_arrivée
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Barcode { get; set; }

        [StringLength(50)]
        public string Nom { get; set; }

        [StringLength(50)]
        public string Catégorie { get; set; }

        [StringLength(50)]
        public string Fournisseur { get; set; }

        [StringLength(50)]
        public string Bon_Commande { get; set; }

        [StringLength(50)]
        public string Taille { get; set; }

        public decimal? Quantité { get; set; }

        public DateTime? Date_Arrivée { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        public decimal? Prix { get; set; }

        [StringLength(50)]
        public string Mesure { get; set; }

        public decimal? Q_Unité { get; set; }

        [StringLength(50)]
        public string Num_Achat { get; set; }

        public DateTime? Date_Action { get; set; }

        public DateTime? Date_Expiration { get; set; }

        [StringLength(50)]
        public string Id_Fournisseur { get; set; }

        [StringLength(20)]
        public string Cloturé { get; set; }

        public int? Product_Id { get; set; }
    }
}
