namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_enseignant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_enseignant()
        {
            tbl_emploi_du_temp = new HashSet<tbl_emploi_du_temp>();
            tbl_TimeSlots = new HashSet<tbl_TimeSlots>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Prenom { get; set; }

        [StringLength(50)]
        public string Nom { get; set; }

        [StringLength(150)]
        public string Nom_Complet { get; set; }

        public DateTime? Date_Naissance { get; set; }

        [StringLength(50)]
        public string Genre { get; set; }

        [StringLength(50)]
        public string Contact1 { get; set; }

        [StringLength(50)]
        public string Contact2 { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(150)]
        public string Adresse { get; set; }

        [StringLength(50)]
        public string Nationalité { get; set; }

        public DateTime? Date_Ajout { get; set; }

        [StringLength(100)]
        public string Auteur { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        [StringLength(50)]
        public string Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_emploi_du_temp> tbl_emploi_du_temp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_TimeSlots> tbl_TimeSlots { get; set; }
    }
}
