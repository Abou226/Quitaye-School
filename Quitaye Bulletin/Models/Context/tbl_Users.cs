namespace Quitaye_School.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Users
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Prenom { get; set; }

        [StringLength(50)]
        public string Nom { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Contact { get; set; }

        [StringLength(100)]
        public string Adresse { get; set; }

        [StringLength(20)]
        public string Genre { get; set; }

        [StringLength(50)]
        public string Type_Compte { get; set; }

        [StringLength(50)]
        public string Role { get; set; }

        public DateTime? Date_Ajout { get; set; }

        [StringLength(50)]
        public string Auteur { get; set; }

        [StringLength(50)]
        public string Departement { get; set; }

        [StringLength(50)]
        public string Active { get; set; }

        [StringLength(50)]
        public string Classe { get; set; }

        [StringLength(50)]
        public string Auth_Premier_Cycle { get; set; }

        [StringLength(50)]
        public string Auth_Second_Cycle { get; set; }

        [StringLength(50)]
        public string Auth_Lycée { get; set; }

        [StringLength(50)]
        public string Auth_Université { get; set; }

        [StringLength(50)]
        public string Auth_Cente_Loisir { get; set; }

        [StringLength(50)]
        public string Auth_Crèche { get; set; }

        [StringLength(50)]
        public string Auth_Maternelle { get; set; }

        [StringLength(50)]
        public string Filiale { get; set; }

        [StringLength(150)]
        public string Nom_Complet { get; set; }
    }
}
