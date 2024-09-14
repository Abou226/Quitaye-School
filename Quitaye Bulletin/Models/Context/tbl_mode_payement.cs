namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_mode_payement
    {
        public int Id { get; set; }

        [Key]
        [StringLength(50)]
        public string Mode { get; set; }

        [StringLength(50)]
        public string Auteur { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string Niveau { get; set; }

        [StringLength(50)]
        public string Defaut { get; set; }
    }
}
