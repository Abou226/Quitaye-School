namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_list_journaux
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(150)]
        public string Nom { get; set; }

        [StringLength(50)]
        public string Prefix { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        public DateTime? Date_Ajout { get; set; }

        [StringLength(100)]
        public string Auteur { get; set; }

        [StringLength(50)]
        public string Compte { get; set; }
    }
}
