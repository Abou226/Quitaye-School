namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_filiale
    {
        public int Id { get; set; }

        [Key]
        [StringLength(50)]
        public string Nom { get; set; }

        [StringLength(75)]
        public string Adresse { get; set; }

        [StringLength(50)]
        public string Telephone { get; set; }

        [StringLength(50)]
        public string Pays { get; set; }

        [StringLength(50)]
        public string Ville { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
    }
}
