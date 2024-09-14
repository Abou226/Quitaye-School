namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_emploi_du_temp
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? Prof_Id { get; set; }

        public int? Classe_Id { get; set; }

        public int? DayOfWeek { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        public virtual tbl_classe tbl_classe { get; set; }

        public virtual tbl_enseignant tbl_enseignant { get; set; }
    }
}
