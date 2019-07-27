CREATE TABLE [dbo].[ro_tipo_gastos_personales] (
    [IdTipoGasto]     VARCHAR (10)  NOT NULL,
    [nom_tipo_gasto]  VARCHAR (50)  NOT NULL,
    [estado]          VARCHAR (1)   NOT NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [MotiAnula]       VARCHAR (200) NULL,
    CONSTRAINT [PK_ro_tipo_gastos_personales] PRIMARY KEY CLUSTERED ([IdTipoGasto] ASC)
);





