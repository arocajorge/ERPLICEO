CREATE TABLE [dbo].[fa_catalogo] (
    [IdCatalogo]      VARCHAR (15)  NOT NULL,
    [IdCatalogo_tipo] INT           NOT NULL,
    [Nombre]          VARCHAR (50)  NOT NULL,
    [Estado]          CHAR (1)      NOT NULL,
    [Abrebiatura]     VARCHAR (10)  NULL,
    [NombreIngles]    VARCHAR (50)  NULL,
    [Orden]           INT           NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [FechaUltMod]     DATETIME      NULL,
    [nom_pc]          VARCHAR (50)  NULL,
    [ip]              VARCHAR (25)  NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [MotiAnula]       VARCHAR (200) NULL,
    CONSTRAINT [PK_fa_catalogo_1] PRIMARY KEY CLUSTERED ([IdCatalogo] ASC),
    CONSTRAINT [FK_fa_catalogo_fa_catalogo_tipo] FOREIGN KEY ([IdCatalogo_tipo]) REFERENCES [dbo].[fa_catalogo_tipo] ([IdCatalogo_tipo])
);

