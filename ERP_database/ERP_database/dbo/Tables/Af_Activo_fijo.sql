CREATE TABLE [dbo].[Af_Activo_fijo] (
    [IdEmpresa]                INT            NOT NULL,
    [IdActivoFijo]             INT            NOT NULL,
    [CodActivoFijo]            VARCHAR (50)   NULL,
    [Af_Nombre]                VARCHAR (MAX)  NOT NULL,
    [IdActivoFijoTipo]         INT            NOT NULL,
    [IdCategoriaAF]            INT            NOT NULL,
    [IdSucursal]               INT            NOT NULL,
    [IdDepartamento]           NUMERIC (18)   NOT NULL,
    [IdArea]                   NUMERIC (18)   NOT NULL,
    [IdCatalogo_Marca]         VARCHAR (35)   NOT NULL,
    [IdCatalogo_Modelo]        VARCHAR (35)   NOT NULL,
    [Af_NumSerie]              VARCHAR (50)   NULL,
    [IdCatalogo_Color]         VARCHAR (35)   NOT NULL,
    [IdTipoCatalogo_Ubicacion] VARCHAR (35)   NOT NULL,
    [Af_fecha_compra]          DATETIME       NOT NULL,
    [Af_fecha_ini_depre]       DATETIME       NOT NULL,
    [Af_fecha_fin_depre]       DATETIME       NOT NULL,
    [Af_costo_compra]          FLOAT (53)     NOT NULL,
    [Af_Depreciacion_acum]     FLOAT (53)     NOT NULL,
    [Af_Vida_Util]             INT            NOT NULL,
    [Af_Meses_depreciar]       INT            NOT NULL,
    [Af_porcentaje_deprec]     FLOAT (53)     NOT NULL,
    [Af_observacion]           VARCHAR (5000) NULL,
    [Af_NumPlaca]              VARCHAR (50)   NULL,
    [Estado]                   CHAR (1)       NOT NULL,
    [IdEmpleadoEncargado]      NUMERIC (18)   NOT NULL,
    [IdEmpleadoCustodio]       NUMERIC (18)   NOT NULL,
    [Af_Codigo_Barra]          VARCHAR (50)   NULL,
    [Estado_Proceso]           VARCHAR (35)   NOT NULL,
    [Af_ValorSalvamento]       FLOAT (53)     NOT NULL,
    [Cantidad]                 INT            NOT NULL,
    [IdUsuario]                VARCHAR (20)   NULL,
    [Fecha_Transac]            DATETIME       NULL,
    [IdUsuarioUltMod]          VARCHAR (20)   NULL,
    [Fecha_UltMod]             DATETIME       NULL,
    [IdUsuarioUltAnu]          VARCHAR (20)   NULL,
    [Fecha_UltAnu]             DATETIME       NULL,
    [MotiAnula]                VARCHAR (100)  NULL,
    CONSTRAINT [PK_Af_Activo_fijo_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdActivoFijo] ASC),
    CONSTRAINT [FK_Af_Activo_fijo_Af_Activo_fijo_Categoria] FOREIGN KEY ([IdEmpresa], [IdCategoriaAF]) REFERENCES [dbo].[Af_Activo_fijo_Categoria] ([IdEmpresa], [IdCategoriaAF]),
    CONSTRAINT [FK_Af_Activo_fijo_Af_Activo_fijo_tipo] FOREIGN KEY ([IdEmpresa], [IdActivoFijoTipo]) REFERENCES [dbo].[Af_Activo_fijo_tipo] ([IdEmpresa], [IdActivoFijoTipo]),
    CONSTRAINT [FK_Af_Activo_fijo_Af_Area] FOREIGN KEY ([IdEmpresa], [IdArea]) REFERENCES [dbo].[Af_Area] ([IdEmpresa], [IdArea]),
    CONSTRAINT [FK_Af_Activo_fijo_Af_Catalogo5] FOREIGN KEY ([IdCatalogo_Marca]) REFERENCES [dbo].[Af_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_Af_Activo_fijo_Af_Catalogo6] FOREIGN KEY ([IdCatalogo_Modelo]) REFERENCES [dbo].[Af_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_Af_Activo_fijo_Af_Catalogo7] FOREIGN KEY ([IdCatalogo_Color]) REFERENCES [dbo].[Af_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_Af_Activo_fijo_Af_Catalogo8] FOREIGN KEY ([IdTipoCatalogo_Ubicacion]) REFERENCES [dbo].[Af_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_Af_Activo_fijo_Af_Catalogo9] FOREIGN KEY ([Estado_Proceso]) REFERENCES [dbo].[Af_Catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_Af_Activo_fijo_Af_Departamento] FOREIGN KEY ([IdEmpresa], [IdDepartamento]) REFERENCES [dbo].[Af_Departamento] ([IdEmpresa], [IdDepartamento]),
    CONSTRAINT [FK_Af_Activo_fijo_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleadoEncargado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_Af_Activo_fijo_ro_empleado1] FOREIGN KEY ([IdEmpresa], [IdEmpleadoCustodio]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_Af_Activo_fijo_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa]),
    CONSTRAINT [FK_Af_Activo_fijo_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);











