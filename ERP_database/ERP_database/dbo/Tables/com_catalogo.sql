CREATE TABLE [dbo].[com_catalogo] (
    [IdCatalogocompra]      VARCHAR (25)  NOT NULL,
    [IdCatalogocompra_tipo] VARCHAR (15)  NOT NULL,
    [CodCatalogo]           VARCHAR (15)  NULL,
    [Nombre]                VARCHAR (50)  NOT NULL,
    [Estado]                CHAR (1)      NOT NULL,
    [Abrebiatura]           VARCHAR (10)  NULL,
    [NombreIngles]          VARCHAR (50)  NULL,
    [Orden]                 INT           NULL,
    [IdUsuario]             VARCHAR (20)  NULL,
    [IdUsuarioUltMod]       VARCHAR (20)  NULL,
    [FechaUltMod]           DATETIME      NULL,
    [nom_pc]                VARCHAR (50)  NULL,
    [ip]                    VARCHAR (25)  NULL,
    [IdUsuarioUltAnu]       VARCHAR (20)  NULL,
    [Fecha_UltAnu]          DATETIME      NULL,
    [MotiAnula]             VARCHAR (200) NULL,
    CONSTRAINT [PK_com_catalogo_1] PRIMARY KEY CLUSTERED ([IdCatalogocompra] ASC),
    CONSTRAINT [FK_com_catalogo_com_catalogo_tipo] FOREIGN KEY ([IdCatalogocompra_tipo]) REFERENCES [dbo].[com_catalogo_tipo] ([IdCatalogocompra_tipo])
);

