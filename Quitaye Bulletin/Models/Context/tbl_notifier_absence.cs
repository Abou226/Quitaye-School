namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_notifier_absence
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(150)]
        public string Personne { get; set; }

        [StringLength(50)]
        public string Genre { get; set; }

        [StringLength(50)]
        public string N_Matricule { get; set; }

        public DateTime? Date_Ajout { get; set; }

        public DateTime? Date_Absence { get; set; }

        [StringLength(150)]
        public string Auteur { get; set; }

        [StringLength(150)]
        public string Commentaire { get; set; }

        [StringLength(50)]
        public string Année_Scolaire { get; set; }

        [StringLength(50)]
        public string Classe { get; set; }

        [StringLength(50)]
        public string Cycle { get; set; }

        public byte[] Fichier { get; set; }

        [StringLength(150)]
        public string Nom_Fichier { get; set; }
    }
}
