CREATE TABLE [dbo].[tbl_année_scolaire] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Nom]    VARCHAR (50) NULL,
    [Auteur] VARCHAR (50) NULL,
    [Date]   DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[tbl_classe] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Nom]       VARCHAR (50)  NULL,
    [Scolarité] DECIMAL (18)  NULL,
    [Date]      DATETIME      NULL,
    [Auteur]    VARCHAR (150) NULL,
    [Cycle]     VARCHAR (50)  NULL,
    [Tranche 1] DECIMAL (18)  NULL,
    [Tranche 2] DECIMAL (18)  NULL,
    [Tranche 3] DECIMAL (18)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[tbl_Compte_Comptable] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Compte]     INT           NULL,
    [Catégorie]  VARCHAR (250) NULL,
    [Nom_Compte] VARCHAR (450) NULL,
    [Date_Ajout] DATETIME      NULL,
    [Auteur]     VARCHAR (150) NULL,
    CONSTRAINT [PK_tbl_Compte_Comptable] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[tbl_examen] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Nom]                 VARCHAR (50)  NULL,
    [Date_Enregistrement] DATETIME      NULL,
    [Auteur]              VARCHAR (150) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[tbl_formule_inscription] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Formule]    VARCHAR (50)  NULL,
    [Montant]    DECIMAL (18)  NULL,
    [Gratuit]    VARCHAR (50)  NULL,
    [Date_Ajout] DATETIME      NULL,
    [Auteur]     VARCHAR (150) NULL,
    [Compte]     INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[tbl_inscription] (
    [Id]                  INT             NOT NULL,
    [Prenom]              VARCHAR (50)    NULL,
    [Nom]                 VARCHAR (50)    NULL,
    [Nom_Complet]         VARCHAR (150)   NULL,
    [Nom_Matricule]       VARCHAR (150)   NULL,
    [Date_Naissance]      DATE            NULL,
    [Genre]               VARCHAR (50)    NULL,
    [Type_Scolarité]      VARCHAR (50)    NULL,
    [Nom_Père]            VARCHAR (150)   NULL,
    [Nom_Mère]            VARCHAR (150)   NULL,
    [Contact 1]           VARCHAR (50)    NULL,
    [Contact 2]           VARCHAR (50)    NULL,
    [Email]               VARCHAR (100)   NULL,
    [Adresse]             VARCHAR (150)   NULL,
    [Nationalité]         VARCHAR (50)    NULL,
    [Classe]              VARCHAR (50)    NULL,
    [Année_Scolaire]      VARCHAR (50)    NULL,
    [N_Matricule]         VARCHAR (50)    NOT NULL,
    [Date_Inscription]    DATETIME        NULL,
    [Auteur]              VARCHAR (150)   NULL,
    [Image]               VARBINARY (MAX) NULL,
    [Ref_Pièces]          VARCHAR (50)    NULL,
    [Cycle]               VARCHAR (50)    NULL,
    [Active]              VARCHAR (50)    NULL,
    [Scolarité]           DECIMAL (18)    NULL,
    [Motif_Desactivation] VARCHAR (50)    NULL,
    [Date_Desactivation]  DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([N_Matricule] ASC)
);
CREATE TABLE [dbo].[tbl_journal_comptable] (
    [Id]                  INT             NOT NULL,
    [Date]                DATE            NULL,
    [Date_Enregistrement] DATETIME        NULL,
    [Libelle]             VARCHAR (150)   NULL,
    [Compte]              VARCHAR (50)    NULL,
    [Débit]               DECIMAL (18)    NULL,
    [Crédit]              DECIMAL (18)    NULL,
    [Auteur]              VARCHAR (150)   NULL,
    [Ref_Pièces]          VARCHAR (50)    NULL,
    [Commentaire]         VARCHAR (150)   NULL,
    [Nom_Fichier]         VARCHAR (150)   NULL,
    [Fichier]             VARBINARY (MAX) NULL,
    [Ref_Payement]        VARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[tbl_matière] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Nom]                 VARCHAR (50)  NULL,
    [Date_Enregistrement] DATETIME      NULL,
    [Auteur]              VARCHAR (150) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[tbl_note] (
    [Id]                  INT           NOT NULL,
    [Note]                INT           NULL,
    [Coeff]               INT           NULL,
    [Prenom]              VARCHAR (50)  NULL,
    [Nom]                 VARCHAR (50)  NULL,
    [Genre]               VARCHAR (50)  NULL,
    [Matière]             VARCHAR (50)  NULL,
    [Classe]              VARCHAR (50)  NULL,
    [Cycle]               VARCHAR (50)  NULL,
    [Année_Scolaire]      VARCHAR (50)  NULL,
    [Examen]              VARCHAR (50)  NULL,
    [N_Matricule]         VARCHAR (50)  NULL,
    [Date]                DATE          NULL,
    [Date_Enregistrement] DATETIME      NULL,
    [Auteur]              VARCHAR (150) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[tbl_payement] (
    [Id]                  INT             NOT NULL,
    [Prenom]              VARCHAR (50)    NULL,
    [Nom]                 VARCHAR (50)    NULL,
    [Genre]               VARCHAR (50)    NULL,
    [N_Matricule]         VARCHAR (50)    NULL,
    [Classe]              VARCHAR (50)    NULL,
    [Cycle]               VARCHAR (50)    NULL,
    [Montant]             DECIMAL (18)    NULL,
    [Date_Payement]       DATE            NULL,
    [Date_Enregistrement] DATETIME        NULL,
    [Auteur]              VARCHAR (150)   NULL,
    [Commentaire]         VARCHAR (150)   NULL,
    [Année_Scolaire]      VARCHAR (50)    NULL,
    [Fichier]             VARBINARY (MAX) NULL,
    [Nom_Fichier]         VARCHAR (100)   NULL,
    [Type]                VARCHAR (50)    NULL,
    [Tranche1]            DECIMAL (18)    NULL,
    [Tranche2]            DECIMAL (18)    NULL,
    [Tranche3]            DECIMAL (18)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[tbl_Users] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Prenom]      VARCHAR (50)  NULL,
    [Nom]         VARCHAR (50)  NULL,
    [Username]    VARCHAR (50)  NULL,
    [Password]    VARCHAR (50)  NULL,
    [Email]       VARCHAR (50)  NULL,
    [Contact]     VARCHAR (50)  NULL,
    [Adresse]     VARCHAR (100) NULL,
    [Genre]       VARCHAR (20)  NULL,
    [Type_Compte] VARCHAR (50)  NULL,
    [Role]        VARCHAR (50)  NULL,
    [Date_Ajout]  DATETIME      NULL,
    [Auteur]      VARCHAR (50)  NULL,
    [Departement] VARCHAR (50)  NULL,
    [Active]      VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

