namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_parent_elèves
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(100)]
        public string Prenom { get; set; }

        [StringLength(100)]
        public string Nom { get; set; }

        [StringLength(50)]
        public string Genre { get; set; }

        [StringLength(50)]
        public string Pays { get; set; }

        [StringLength(50)]
        public string Contact { get; set; }

        [StringLength(150)]
        public string Adresse { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Ville { get; set; }

        public DateTime? Date_Ajout { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        [StringLength(150)]
        public string Nom_Contact { get; set; }
    }
}
