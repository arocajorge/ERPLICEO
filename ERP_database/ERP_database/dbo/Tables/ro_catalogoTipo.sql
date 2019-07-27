CREATE TABLE [dbo].[ro_catalogoTipo] (
    [IdTipoCatalogo]  INT           NOT NULL,
    [Codigo]          VARCHAR (10)  NULL,
    [tc_Descripcion]  NVARCHAR (50) NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [nom_pc]          VARCHAR (50)  NULL,
    [ip]              VARCHAR (25)  NULL,
    [MotivoAnulacion] VARCHAR (100) NULL,
    [ca_estado]       VARCHAR (1)   NULL,
    CONSTRAINT [PK_ro_CatalogoTipo] PRIMARY KEY CLUSTERED ([IdTipoCatalogo] ASC)
);

