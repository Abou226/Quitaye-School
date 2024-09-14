namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_entreprise
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string Nom { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(150)]
        public string Adresse { get; set; }

        [StringLength(50)]
        public string Téléphone { get; set; }

        [StringLength(50)]
        public string Pays { get; set; }

        [StringLength(50)]
        public string Ville { get; set; }

        [StringLength(50)]
        public string Secteur { get; set; }

        [StringLength(50)]
        public string Type_Produit { get; set; }

        [StringLength(150)]
        public string Slogan { get; set; }

        [StringLength(50)]
        public string Couleur { get; set; }

        public DateTime? Date_Ouverture { get; set; }
    }
}
