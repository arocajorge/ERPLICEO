CREATE TABLE [dbo].[ro_catalogo] (
    [CodCatalogo]     VARCHAR (10)  NOT NULL,
    [IdCatalogo]      INT           NOT NULL,
    [IdTipoCatalogo]  INT           NOT NULL,
    [ca_descripcion]  VARCHAR (250) NOT NULL,
    [ca_estado]       VARCHAR (1)   NOT NULL,
    [ca_orden]        INT           NOT NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [nom_pc]          VARCHAR (50)  NULL,
    [ip]              VARCHAR (25)  NULL,
    [MotivoAnulacion] VARCHAR (100) NULL,
    CONSTRAINT [PK_ro_Catalogo] PRIMARY KEY CLUSTERED ([CodCatalogo] ASC),
    CONSTRAINT [FK_ro_catalogo_ro_catalogoTipo] FOREIGN KEY ([IdTipoCatalogo]) REFERENCES [dbo].[ro_catalogoTipo] ([IdTipoCatalogo])
);

