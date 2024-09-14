namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_historique_evolution_stock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Barcode { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? Date_Achat { get; set; }

        public decimal? Quantité { get; set; }

        [StringLength(50)]
        public string Mesure { get; set; }

        public decimal? Prix_Achat_Petit { get; set; }

        public decimal? Prix_Achat_Moyen { get; set; }

        public DateTime? Date_Expiration { get; set; }

        [StringLength(100)]
        public string Auteur { get; set; }

        public int? Product_Id { get; set; }
    }
}
