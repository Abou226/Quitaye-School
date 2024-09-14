namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_formule_inscription
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Formule { get; set; }

        public decimal? Montant { get; set; }

        [StringLength(50)]
        public string Gratuit { get; set; }

        public DateTime? Date_Ajout { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        public int? Compte { get; set; }
    }
}
