namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_scolarité
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Année_Scolaire { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string Classe { get; set; }

        public decimal? Montant { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        [StringLength(50)]
        public string Cycle { get; set; }

        [Column("Tranche 1")]
        public decimal? Tranche_1 { get; set; }

        [Column("Tranche 2")]
        public decimal? Tranche_2 { get; set; }

        [Column("Tranche 3")]
        public decimal? Tranche_3 { get; set; }
    }
}
