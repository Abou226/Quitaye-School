namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Compte_Comptable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Compte { get; set; }

        [StringLength(150)]
        public string Catégorie { get; set; }

        [StringLength(150)]
        public string Nom_Compte { get; set; }

        public DateTime? Date_Ajout { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Compte_Aux { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(50)]
        public string Préfix { get; set; }
    }
}
