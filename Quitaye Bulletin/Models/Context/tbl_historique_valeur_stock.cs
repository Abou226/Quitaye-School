namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_historique_valeur_stock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Code_Barre { get; set; }

        public DateTimeOffset? Date { get; set; }

        public decimal? Quantité { get; set; }

        [StringLength(50)]
        public string Filiale { get; set; }

        public decimal? Prix_Petit { get; set; }

        public decimal? Prix_Moyen { get; set; }

        public decimal? Prix_Grand { get; set; }

        public decimal? Prix_Large { get; set; }

        public decimal? Prix_Hyper_Large { get; set; }

        public int? Product_Id { get; set; }
    }
}
