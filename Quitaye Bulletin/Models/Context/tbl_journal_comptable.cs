namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_journal_comptable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public DateTime? Date_Enregistrement { get; set; }

        [StringLength(150)]
        public string Libelle { get; set; }

        [StringLength(150)]
        public string N_Facture { get; set; }

        [StringLength(50)]
        public string Compte { get; set; }

        [StringLength(50)]
        public string Compte_Tier { get; set; }

        public decimal? Débit { get; set; }

        public decimal? Crédit { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        [StringLength(50)]
        public string Ref_Pièces { get; set; }

        [StringLength(150)]
        public string Commentaire { get; set; }

        [StringLength(150)]
        public string Nom_Fichier { get; set; }

        public byte[] Fichier { get; set; }

        [StringLength(150)]
        public string Ref_Payement { get; set; }

        [StringLength(50)]
        public string Active { get; set; }

        [StringLength(50)]
        public string Validé { get; set; }

        [StringLength(50)]
        public string Type { get; set; }
    }
}
