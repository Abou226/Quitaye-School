namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_note
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public decimal? Note_Classe { get; set; }

        public decimal? Note_Compo { get; set; }

        public decimal? Coeff { get; set; }

        [StringLength(50)]
        public string Prenom { get; set; }

        [StringLength(50)]
        public string Nom { get; set; }

        [StringLength(50)]
        public string Genre { get; set; }

        [StringLength(50)]
        public string Matière { get; set; }

        [StringLength(50)]
        public string Classe { get; set; }

        [StringLength(50)]
        public string Cycle { get; set; }

        [StringLength(50)]
        public string Année_Scolaire { get; set; }

        [StringLength(50)]
        public string Examen { get; set; }

        [StringLength(50)]
        public string N_Matricule { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public DateTime? Date_Enregistrement { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        public int? Enseignant { get; set; }
    }
}
