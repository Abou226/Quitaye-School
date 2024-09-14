namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_matiere
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Nom { get; set; }

        [StringLength(50)]
        public string Matière_Crutiale { get; set; }

        public DateTime? Date_Enregistrement { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        public decimal? Coefficient { get; set; }

        [StringLength(50)]
        public string Cycle { get; set; }

        public int? Enseignant { get; set; }

        [StringLength(50)]
        public string Classe { get; set; }

        [StringLength(50)]
        public string Année_Scolaire { get; set; }
    }
}
