namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_ravitaillement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column("Code Barre")]
        [StringLength(50)]
        public string Code_Barre { get; set; }

        [StringLength(50)]
        public string Marque { get; set; }

        [StringLength(50)]
        public string Catégorie { get; set; }

        [StringLength(50)]
        public string Taille { get; set; }

        public decimal? Quantité { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        [StringLength(50)]
        public string Fiaile { get; set; }

        public DateTime? Date_Expiration { get; set; }

        [StringLength(50)]
        public string Mesure { get; set; }

        public int? Product_Id { get; set; }
    }
}
