namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_examen
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Nom { get; set; }

        public DateTime? Date_Enregistrement { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        [StringLength(50)]
        public string Cycle { get; set; }
    }
}
