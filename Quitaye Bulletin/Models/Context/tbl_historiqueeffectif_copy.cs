namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_historiqueeffectif_copy
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(150)]
        public string Nom_Matricule { get; set; }

        [StringLength(50)]
        public string Genre { get; set; }

        [StringLength(50)]
        public string Type_Scolarité { get; set; }

        [StringLength(50)]
        public string Nationalité { get; set; }

        [StringLength(50)]
        public string Classe { get; set; }

        [StringLength(50)]
        public string Année_Scolaire { get; set; }

        [StringLength(50)]
        public string NewAnnée_Scolaire { get; set; }

        [StringLength(50)]
        public string Cantine { get; set; }

        [StringLength(50)]
        public string Transport { get; set; }

        [StringLength(50)]
        public string Assurance { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string N_Matricule { get; set; }

        public DateTime? Date_Inscription { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        public byte[] Image { get; set; }

        [StringLength(50)]
        public string Ref_Pièces { get; set; }

        [StringLength(50)]
        public string Cycle { get; set; }

        [StringLength(50)]
        public string Active { get; set; }

        public decimal? Scolarité { get; set; }

        public decimal? Tranche1 { get; set; }

        public decimal? Tranche2 { get; set; }

        public decimal? Tranche3 { get; set; }
    }
}
