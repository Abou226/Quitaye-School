namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_produits
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(80)]
        public string Barcode { get; set; }

        [StringLength(100)]
        public string Nom { get; set; }

        [StringLength(100)]
        public string Catégorie { get; set; }

        [StringLength(100)]
        public string Type { get; set; }

        [StringLength(100)]
        public string Taille { get; set; }

        public int? Stock_min { get; set; }

        public int? Stock_max { get; set; }

        public DateTime? Date_Ajout { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        [StringLength(50)]
        public string Dept_Auteur { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        [StringLength(50)]
        public string Usage { get; set; }

        public decimal? Prix_Petit { get; set; }

        public decimal? Prix_Moyen { get; set; }

        public decimal? Prix_Grand { get; set; }

        public decimal? Prix_Large { get; set; }

        public decimal? Prix_Hyper_Large { get; set; }

        [StringLength(50)]
        public string Mesure { get; set; }

        public int? Formule_Stockage { get; set; }

        [StringLength(100)]
        public string Nom_Image { get; set; }

        public decimal? Prix_Petit_Grossiste { get; set; }

        public decimal? Prix_Moyen_Grossiste { get; set; }

        public decimal? Prix_Grand_Grossiste { get; set; }

        public decimal? Prix_Large_Grossiste { get; set; }

        public decimal? Prix_Hyper_Large_Grossiste { get; set; }

        public decimal? Prix_Achat { get; set; }

        [StringLength(50)]
        public string Filiale { get; set; }

        public decimal? Prix_Achat_Petit { get; set; }

        public decimal? Prix_Achat_Moyen { get; set; }

        public decimal? Prix_Achat_Grand { get; set; }

        public decimal? Prix_Achat_Large { get; set; }

        public decimal? Prix_Achat_Hyper_Large { get; set; }

        [StringLength(150)]
        public string Details { get; set; }
    }
}
