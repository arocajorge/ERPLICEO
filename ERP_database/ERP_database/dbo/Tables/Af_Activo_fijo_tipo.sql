CREATE TABLE [dbo].[Af_Activo_fijo_tipo] (
    [IdEmpresa]              INT            NOT NULL,
    [IdActivoFijoTipo]       INT            NOT NULL,
    [CodActivoFijo]          VARCHAR (50)   NULL,
    [Af_Descripcion]         VARCHAR (5000) NOT NULL,
    [Af_Porcentaje_depre]    FLOAT (53)     NOT NULL,
    [Af_anio_depreciacion]   INT            NOT NULL,
    [IdCtaCble_Activo]       VARCHAR (20)   NOT NULL,
    [IdCtaCble_Dep_Acum]     VARCHAR (20)   NOT NULL,
    [IdCtaCble_Gastos_Depre] VARCHAR (20)   NOT NULL,
    [Se_Deprecia]            BIT            NOT NULL,
    [IdUsuario]              VARCHAR (20)   NULL,
    [Fecha_Transac]          DATETIME       NULL,
    [IdUsuarioUltMod]        VARCHAR (20)   NULL,
    [Fecha_UltMod]           DATETIME       NULL,
    [IdUsuarioUltAnu]        VARCHAR (20)   NULL,
    [Fecha_UltAnu]           DATETIME       NULL,
    [Estado]                 CHAR (1)       NOT NULL,
    [MotiAnula]              VARCHAR (100)  NULL,
    [IdCtaCble_CostoVenta]   VARCHAR (20)   NOT NULL,
    [IdCtaCble_Mejora]       VARCHAR (20)   NOT NULL,
    [IdCtaCble_Baja]         VARCHAR (20)   NOT NULL,
    [IdCtaCble_Retiro]       VARCHAR (20)   NOT NULL,
    CONSTRAINT [PK_Af_Activo_fijo_tipo_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdActivoFijoTipo] ASC),
    CONSTRAINT [FK_Af_Activo_fijo_tipo_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble_Activo]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_Af_Activo_fijo_tipo_ct_plancta1] FOREIGN KEY ([IdEmpresa], [IdCtaCble_Dep_Acum]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_Af_Activo_fijo_tipo_ct_plancta2] FOREIGN KEY ([IdEmpresa], [IdCtaCble_Gastos_Depre]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble])
);





