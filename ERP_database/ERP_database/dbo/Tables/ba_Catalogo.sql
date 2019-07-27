CREATE TABLE [dbo].[ba_Catalogo] (
    [IdCatalogo]      VARCHAR (50)  NOT NULL,
    [IdTipoCatalogo]  VARCHAR (10)  NOT NULL,
    [ca_descripcion]  VARCHAR (250) NOT NULL,
    [ca_estado]       VARCHAR (1)   NOT NULL,
    [IdUsuario]       VARCHAR (50)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (50)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [nom_pc]          VARCHAR (50)  NULL,
    [ip]              VARCHAR (50)  NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [MotiAnula]       VARCHAR (200) NULL,
    CONSTRAINT [PK_ba_Catalogo] PRIMARY KEY CLUSTERED ([IdCatalogo] ASC),
    CONSTRAINT [FK_ba_Catalogo_ba_CatalogoTipo] FOREIGN KEY ([IdTipoCatalogo]) REFERENCES [dbo].[ba_CatalogoTipo] ([IdTipoCatalogo])
);

