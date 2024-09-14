namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_classe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_classe()
        {
            tbl_emploi_du_temp = new HashSet<tbl_emploi_du_temp>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Nom { get; set; }

        public decimal? Scolarité { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        [StringLength(50)]
        public string Cycle { get; set; }

        [Column("Tranche 1")]
        public decimal? Tranche_1 { get; set; }

        [Column("Tranche 2")]
        public decimal? Tranche_2 { get; set; }

        [Column("Tranche 3")]
        public decimal? Tranche_3 { get; set; }

        public int? Classe_Sup_Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_emploi_du_temp> tbl_emploi_du_temp { get; set; }
    }
}
