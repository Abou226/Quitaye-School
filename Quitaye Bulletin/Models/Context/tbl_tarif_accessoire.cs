namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_tarif_accessoire
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Nom { get; set; }

        public decimal? Tarif_Annuel { get; set; }

        public decimal? Tarif_Mensuel { get; set; }

        public decimal? Tarif_Journalier { get; set; }
    }
}
