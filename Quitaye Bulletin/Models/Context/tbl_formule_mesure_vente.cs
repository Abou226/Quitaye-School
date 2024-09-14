namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_formule_mesure_vente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [StringLength(50)]
        public string Formule { get; set; }

        public decimal? Petit { get; set; }

        public decimal? Moyen { get; set; }

        public decimal? Grand { get; set; }

        public decimal? Large { get; set; }

        [Column("Hyper Large")]
        public decimal? Hyper_Large { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string Auteur { get; set; }

        public int? Niveau { get; set; }
    }
}
