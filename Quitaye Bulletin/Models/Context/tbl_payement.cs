namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_payement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Prenom { get; set; }

        [StringLength(50)]
        public string Nom { get; set; }

        [StringLength(50)]
        public string Genre { get; set; }

        [StringLength(50)]
        public string N_Matricule { get; set; }

        [StringLength(50)]
        public string Classe { get; set; }

        [StringLength(50)]
        public string Cycle { get; set; }

        public decimal? Montant { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date_Payement { get; set; }

        public DateTime? Date_Enregistrement { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        [StringLength(150)]
        public string Commentaire { get; set; }

        [StringLength(50)]
        public string Année_Scolaire { get; set; }

        public byte[] Fichier { get; set; }

        [StringLength(100)]
        public string Nom_Fichier { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public decimal? Tranche1 { get; set; }

        public decimal? Tranche2 { get; set; }

        public decimal? Tranche3 { get; set; }

        [StringLength(50)]
        public string Opération { get; set; }

        [StringLength(50)]
        public string Cloturé { get; set; }

        [StringLength(50)]
        public string Nature { get; set; }

        [StringLength(50)]
        public string Raison { get; set; }

        [StringLength(50)]
        public string Num_Opération { get; set; }

        [StringLength(50)]
        public string Client { get; set; }

        [StringLength(50)]
        public string Num_Client { get; set; }

        [StringLength(50)]
        public string Mode_Payement { get; set; }

        [StringLength(50)]
        public string Compte_Tier { get; set; }

        [StringLength(50)]
        public string Reference { get; set; }

        public decimal? Réduction { get; set; }

        public DateTime? Date_Echeance { get; set; }
    }
}
