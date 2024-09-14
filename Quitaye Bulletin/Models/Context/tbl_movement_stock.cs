namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_movement_stock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string Type_Opération { get; set; }

        [StringLength(50)]
        public string Barcode { get; set; }

        public decimal? Q_Unité { get; set; }

        [StringLength(50)]
        public string Filiale { get; set; }
    }
}
