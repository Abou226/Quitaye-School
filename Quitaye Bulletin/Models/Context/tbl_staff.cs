namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_staff
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(150)]
        public string Nom { get; set; }

        [StringLength(50)]
        public string Genre { get; set; }

        [StringLength(50)]
        public string Adresse { get; set; }

        [StringLength(150)]
        public string Role { get; set; }

        [StringLength(50)]
        public string Contact { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        public DateTime? Date_Ajout { get; set; }
    }
}
