namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_historique_expiration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? Id_Opération { get; set; }

        [StringLength(50)]
        public string Num_Opération { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? Date_Expiration { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        [StringLength(80)]
        public string Barcode { get; set; }

        [StringLength(50)]
        public string Filiale { get; set; }

        public decimal? Quantité { get; set; }

        public int? Product_Id { get; set; }
    }
}
