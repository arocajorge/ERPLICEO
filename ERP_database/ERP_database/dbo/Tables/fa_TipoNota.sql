CREATE TABLE [dbo].[fa_TipoNota] (
    [IdEmpresa]       INT           NOT NULL,
    [IdTipoNota]      INT           NOT NULL,
    [CodTipoNota]     VARCHAR (20)  NULL,
    [Tipo]            CHAR (1)      NOT NULL,
    [No_Descripcion]  VARCHAR (150) NOT NULL,
    [GeneraMoviInven] BIT           NOT NULL,
    [IdCtaCble]       VARCHAR (20)  NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [Estado]          CHAR (1)      NOT NULL,
    [MotiAnula]       VARCHAR (200) NULL,
    CONSTRAINT [PK_fa_TipoNota] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTipoNota] ASC),
    CONSTRAINT [FK_fa_TipoNota_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble])
);



