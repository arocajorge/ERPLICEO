CREATE TABLE [dbo].[ct_grupocble] (
    [IdGrupoCble]          VARCHAR (5)  NOT NULL,
    [gc_GrupoCble]         CHAR (50)    CONSTRAINT [DF__ct_grupoc__Grupo__07C12930] DEFAULT (NULL) NOT NULL,
    [gc_Orden]             TINYINT      CONSTRAINT [DF__ct_grupoc__Orden__08B54D69] DEFAULT (NULL) NOT NULL,
    [gc_estado_financiero] CHAR (2)     NOT NULL,
    [gc_signo_operacion]   INT          NULL,
    [Estado]               CHAR (1)     NOT NULL,
    [IdGrupo_Mayor]        VARCHAR (50) NULL,
    CONSTRAINT [PK__ct_grupo__D1E02FAB05D8E0BE] PRIMARY KEY CLUSTERED ([IdGrupoCble] ASC),
    CONSTRAINT [FK_ct_grupocble_ct_grupocble_Mayor] FOREIGN KEY ([IdGrupo_Mayor]) REFERENCES [dbo].[ct_grupocble_Mayor] ([IdGrupo_Mayor])
);

