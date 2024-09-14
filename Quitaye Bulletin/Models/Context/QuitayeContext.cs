using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Quitaye_School.Models.Context
{
    public partial class QuitayeContext : DbContext
    {
        public QuitayeContext()
            : base("name=QuitayeContext")
        {
        }

        public virtual DbSet<tbl_année_scolaire> tbl_année_scolaire { get; set; }
        public virtual DbSet<tbl_arrivée> tbl_arrivée { get; set; }
        public virtual DbSet<tbl_arrivée_temp> tbl_arrivée_temp { get; set; }
        public virtual DbSet<tbl_bon> tbl_bon { get; set; }
        public virtual DbSet<tbl_catégorie> tbl_catégorie { get; set; }
        public virtual DbSet<tbl_classe> tbl_classe { get; set; }
        public virtual DbSet<tbl_Compte_Comptable> tbl_Compte_Comptable { get; set; }
        public virtual DbSet<tbl_damaged_expense> tbl_damaged_expense { get; set; }
        public virtual DbSet<tbl_damaged_expense_temp> tbl_damaged_expense_temp { get; set; }
        public virtual DbSet<tbl_emploi_du_temp> tbl_emploi_du_temp { get; set; }
        public virtual DbSet<tbl_enseignant> tbl_enseignant { get; set; }
        public virtual DbSet<tbl_entreprise> tbl_entreprise { get; set; }
        public virtual DbSet<tbl_entreprise_autres_details> tbl_entreprise_autres_details { get; set; }
        public virtual DbSet<tbl_examen> tbl_examen { get; set; }
        public virtual DbSet<tbl_expiration> tbl_expiration { get; set; }
        public virtual DbSet<tbl_filiale> tbl_filiale { get; set; }
        public virtual DbSet<tbl_formule_inscription> tbl_formule_inscription { get; set; }
        public virtual DbSet<tbl_formule_mesure_vente> tbl_formule_mesure_vente { get; set; }
        public virtual DbSet<tbl_fournisseurs> tbl_fournisseurs { get; set; }
        public virtual DbSet<tbl_historique_evolution_stock> tbl_historique_evolution_stock { get; set; }
        public virtual DbSet<tbl_historique_expiration> tbl_historique_expiration { get; set; }
        public virtual DbSet<tbl_historique_valeur_stock> tbl_historique_valeur_stock { get; set; }
        public virtual DbSet<tbl_historiqueeffectif> tbl_historiqueeffectif { get; set; }
        public virtual DbSet<tbl_inscription> tbl_inscription { get; set; }
        public virtual DbSet<tbl_journal_comptable> tbl_journal_comptable { get; set; }
        public virtual DbSet<tbl_list_journaux> tbl_list_journaux { get; set; }
        public virtual DbSet<tbl_marque> tbl_marque { get; set; }
        public virtual DbSet<tbl_matiere> tbl_matiere { get; set; }
        public virtual DbSet<tbl_mesure_vente> tbl_mesure_vente { get; set; }
        public virtual DbSet<tbl_mode_payement> tbl_mode_payement { get; set; }
        public virtual DbSet<tbl_movement_stock> tbl_movement_stock { get; set; }
        public virtual DbSet<tbl_multi_barcode> tbl_multi_barcode { get; set; }
        public virtual DbSet<tbl_note> tbl_note { get; set; }
        public virtual DbSet<tbl_notifier_absence> tbl_notifier_absence { get; set; }
        public virtual DbSet<tbl_num_achat> tbl_num_achat { get; set; }
        public virtual DbSet<tbl_num_damaged> tbl_num_damaged { get; set; }
        public virtual DbSet<tbl_num_payement> tbl_num_payement { get; set; }
        public virtual DbSet<tbl_num_vente> tbl_num_vente { get; set; }
        public virtual DbSet<tbl_operation_default> tbl_operation_default { get; set; }
        public virtual DbSet<tbl_parent_elèves> tbl_parent_elèves { get; set; }
        public virtual DbSet<tbl_payement> tbl_payement { get; set; }
        public virtual DbSet<tbl_produits> tbl_produits { get; set; }
        public virtual DbSet<tbl_ravitaillement> tbl_ravitaillement { get; set; }
        public virtual DbSet<tbl_ravitaillement_temp> tbl_ravitaillement_temp { get; set; }
        public virtual DbSet<tbl_redévance> tbl_redévance { get; set; }
        public virtual DbSet<tbl_responsabilité> tbl_responsabilité { get; set; }
        public virtual DbSet<tbl_scolarité> tbl_scolarité { get; set; }
        public virtual DbSet<tbl_staff> tbl_staff { get; set; }
        public virtual DbSet<tbl_stock_produits_vente> tbl_stock_produits_vente { get; set; }
        public virtual DbSet<tbl_taille> tbl_taille { get; set; }
        public virtual DbSet<tbl_tarif_accessoire> tbl_tarif_accessoire { get; set; }
        public virtual DbSet<tbl_TimeSlots> tbl_TimeSlots { get; set; }
        public virtual DbSet<tbl_type> tbl_type { get; set; }
        public virtual DbSet<tbl_Users> tbl_Users { get; set; }
        public virtual DbSet<tbl_vente> tbl_vente { get; set; }
        public virtual DbSet<tbl_vente_temp> tbl_vente_temp { get; set; }
        public virtual DbSet<tbl_historiqueeffectif_copy> tbl_historiqueeffectif_copy { get; set; }
        public virtual DbSet<tbl_inscription_copy> tbl_inscription_copy { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_année_scolaire>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_année_scolaire>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_arrivée>()
                .Property(e => e.Quantité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_arrivée>()
                .Property(e => e.Prix)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_arrivée>()
                .Property(e => e.Q_Unité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_arrivée_temp>()
                .Property(e => e.Quantité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_arrivée_temp>()
                .Property(e => e.Prix)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_classe>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_classe>()
                .Property(e => e.Scolarité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_classe>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_classe>()
                .Property(e => e.Cycle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_classe>()
                .Property(e => e.Tranche_1)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_classe>()
                .Property(e => e.Tranche_2)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_classe>()
                .Property(e => e.Tranche_3)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_classe>()
                .HasMany(e => e.tbl_emploi_du_temp)
                .WithOptional(e => e.tbl_classe)
                .HasForeignKey(e => e.Classe_Id);

            modelBuilder.Entity<tbl_Compte_Comptable>()
                .Property(e => e.Compte)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Compte_Comptable>()
                .Property(e => e.Catégorie)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Compte_Comptable>()
                .Property(e => e.Nom_Compte)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Compte_Comptable>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Compte_Comptable>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Compte_Comptable>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_damaged_expense>()
                .Property(e => e.Quantité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_damaged_expense>()
                .Property(e => e.Prix_Unité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_damaged_expense>()
                .Property(e => e.Montant)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_damaged_expense>()
                .Property(e => e.Q_Unité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_damaged_expense>()
                .Property(e => e.Reduction)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_damaged_expense_temp>()
                .Property(e => e.Quantité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_damaged_expense_temp>()
                .Property(e => e.Prix_Unité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_damaged_expense_temp>()
                .Property(e => e.Montant)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_enseignant>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_enseignant>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_enseignant>()
                .Property(e => e.Nom_Complet)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_enseignant>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_enseignant>()
                .Property(e => e.Contact1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_enseignant>()
                .Property(e => e.Contact2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_enseignant>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_enseignant>()
                .Property(e => e.Adresse)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_enseignant>()
                .Property(e => e.Nationalité)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_enseignant>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_enseignant>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_enseignant>()
                .HasMany(e => e.tbl_emploi_du_temp)
                .WithOptional(e => e.tbl_enseignant)
                .HasForeignKey(e => e.Prof_Id);

            modelBuilder.Entity<tbl_enseignant>()
                .HasMany(e => e.tbl_TimeSlots)
                .WithOptional(e => e.tbl_enseignant)
                .HasForeignKey(e => e.Prof_Id);

            modelBuilder.Entity<tbl_entreprise>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_entreprise>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_entreprise>()
                .Property(e => e.Adresse)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_entreprise>()
                .Property(e => e.Téléphone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_entreprise>()
                .Property(e => e.Pays)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_entreprise>()
                .Property(e => e.Ville)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_entreprise>()
                .Property(e => e.Secteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_entreprise>()
                .Property(e => e.Type_Produit)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_entreprise>()
                .Property(e => e.Slogan)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_entreprise>()
                .Property(e => e.Couleur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_examen>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_examen>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_examen>()
                .Property(e => e.Cycle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_expiration>()
                .Property(e => e.Quantité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_expiration>()
                .Property(e => e.Reste)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_formule_inscription>()
                .Property(e => e.Formule)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_formule_inscription>()
                .Property(e => e.Montant)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_formule_inscription>()
                .Property(e => e.Gratuit)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_formule_inscription>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_formule_mesure_vente>()
                .Property(e => e.Petit)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_formule_mesure_vente>()
                .Property(e => e.Moyen)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_formule_mesure_vente>()
                .Property(e => e.Grand)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_formule_mesure_vente>()
                .Property(e => e.Large)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_formule_mesure_vente>()
                .Property(e => e.Hyper_Large)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historique_evolution_stock>()
                .Property(e => e.Quantité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historique_evolution_stock>()
                .Property(e => e.Prix_Achat_Petit)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historique_evolution_stock>()
                .Property(e => e.Prix_Achat_Moyen)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historique_expiration>()
                .Property(e => e.Quantité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historique_valeur_stock>()
                .Property(e => e.Quantité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historique_valeur_stock>()
                .Property(e => e.Prix_Petit)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historique_valeur_stock>()
                .Property(e => e.Prix_Moyen)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historique_valeur_stock>()
                .Property(e => e.Prix_Grand)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historique_valeur_stock>()
                .Property(e => e.Prix_Large)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historique_valeur_stock>()
                .Property(e => e.Prix_Hyper_Large)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Nom_Matricule)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Type_Scolarité)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Nationalité)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Classe)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Année_Scolaire)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.NewAnnée_Scolaire)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Cantine)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Transport)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Assurance)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.N_Matricule)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Ref_Pièces)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Cycle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Scolarité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Tranche1)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Tranche2)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historiqueeffectif>()
                .Property(e => e.Tranche3)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Nom_Complet)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Nom_Matricule)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Type_Scolarité)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Nom_Père)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Nom_Mère)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Contact_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Contact_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Adresse)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Nationalité)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Classe)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Année_Scolaire)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Cantine)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Transport)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Assurance)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.N_Matricule)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Ref_Pièces)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Cycle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Scolarité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_inscription>()
                .Property(e => e.Motif_Desactivation)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_journal_comptable>()
                .Property(e => e.Libelle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_journal_comptable>()
                .Property(e => e.N_Facture)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_journal_comptable>()
                .Property(e => e.Compte)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_journal_comptable>()
                .Property(e => e.Compte_Tier)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_journal_comptable>()
                .Property(e => e.Débit)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_journal_comptable>()
                .Property(e => e.Crédit)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_journal_comptable>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_journal_comptable>()
                .Property(e => e.Ref_Pièces)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_journal_comptable>()
                .Property(e => e.Commentaire)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_journal_comptable>()
                .Property(e => e.Nom_Fichier)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_journal_comptable>()
                .Property(e => e.Ref_Payement)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_journal_comptable>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_matiere>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_matiere>()
                .Property(e => e.Matière_Crutiale)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_matiere>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_matiere>()
                .Property(e => e.Coefficient)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_matiere>()
                .Property(e => e.Cycle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_matiere>()
                .Property(e => e.Classe)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_matiere>()
                .Property(e => e.Année_Scolaire)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_movement_stock>()
                .Property(e => e.Type_Opération)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_movement_stock>()
                .Property(e => e.Barcode)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_movement_stock>()
                .Property(e => e.Q_Unité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_movement_stock>()
                .Property(e => e.Filiale)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_note>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_note>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_note>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_note>()
                .Property(e => e.Matière)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_note>()
                .Property(e => e.Classe)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_note>()
                .Property(e => e.Cycle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_note>()
                .Property(e => e.Année_Scolaire)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_note>()
                .Property(e => e.Examen)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_note>()
                .Property(e => e.N_Matricule)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_note>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.N_Matricule)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Classe)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Cycle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Montant)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Commentaire)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Année_Scolaire)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Nom_Fichier)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Tranche1)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Tranche2)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Tranche3)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Opération)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_payement>()
                .Property(e => e.Réduction)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Petit)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Moyen)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Grand)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Large)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Hyper_Large)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Petit_Grossiste)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Moyen_Grossiste)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Grand_Grossiste)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Large_Grossiste)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Hyper_Large_Grossiste)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Achat)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Achat_Petit)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Achat_Moyen)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Achat_Grand)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Achat_Large)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_produits>()
                .Property(e => e.Prix_Achat_Hyper_Large)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_ravitaillement>()
                .Property(e => e.Quantité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_ravitaillement_temp>()
                .Property(e => e.Quantité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_redévance>()
                .Property(e => e.Montant)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_scolarité>()
                .Property(e => e.Année_Scolaire)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_scolarité>()
                .Property(e => e.Classe)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_scolarité>()
                .Property(e => e.Montant)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_scolarité>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_scolarité>()
                .Property(e => e.Cycle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_scolarité>()
                .Property(e => e.Tranche_1)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_scolarité>()
                .Property(e => e.Tranche_2)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_scolarité>()
                .Property(e => e.Tranche_3)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_stock_produits_vente>()
                .Property(e => e.Quantité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_tarif_accessoire>()
                .Property(e => e.Tarif_Annuel)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_tarif_accessoire>()
                .Property(e => e.Tarif_Mensuel)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_tarif_accessoire>()
                .Property(e => e.Tarif_Journalier)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Contact)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Adresse)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Type_Compte)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Departement)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Classe)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Auth_Premier_Cycle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Auth_Second_Cycle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Auth_Lycée)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Auth_Université)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Auth_Cente_Loisir)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Auth_Crèche)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Users>()
                .Property(e => e.Auth_Maternelle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_vente>()
                .Property(e => e.Quantité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_vente>()
                .Property(e => e.Prix_Unité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_vente>()
                .Property(e => e.Montant)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_vente>()
                .Property(e => e.Q_Unité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_vente>()
                .Property(e => e.Reduction)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_vente_temp>()
                .Property(e => e.Quantité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_vente_temp>()
                .Property(e => e.Prix_Unité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_vente_temp>()
                .Property(e => e.Montant)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Nom_Matricule)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Type_Scolarité)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Nationalité)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Classe)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Année_Scolaire)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.NewAnnée_Scolaire)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Cantine)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Transport)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Assurance)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.N_Matricule)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Ref_Pièces)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Cycle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Scolarité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Tranche1)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Tranche2)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_historiqueeffectif_copy>()
                .Property(e => e.Tranche3)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Nom_Complet)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Nom_Matricule)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Type_Scolarité)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Nom_Père)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Nom_Mère)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Contact_1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Contact_2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Adresse)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Nationalité)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Classe)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Année_Scolaire)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Cantine)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Transport)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Assurance)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.N_Matricule)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Auteur)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Ref_Pièces)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Cycle)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Active)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Scolarité)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_inscription_copy>()
                .Property(e => e.Motif_Desactivation)
                .IsUnicode(false);
        }
    }
}
