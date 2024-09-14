namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_expiration
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Code_Barre { get; set; }

        public DateTime? Date_Expiration { get; set; }

        public decimal? Quantité { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string Mesure { get; set; }

        public decimal? Reste { get; set; }

        [StringLength(50)]
        public string Filiale { get; set; }

        public int? Product_Id { get; set; }
    }
}
