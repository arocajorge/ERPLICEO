CREATE TABLE [dbo].[ro_empleado_x_ro_rubro] (
    [IdEmpresa]          INT           NOT NULL,
    [IdRubroFijo]        INT           NOT NULL,
    [IdEmpleado]         NUMERIC (18)  NOT NULL,
    [IdNomina_Tipo]      INT           NOT NULL,
    [IdNomina_TipoLiqui] INT           NOT NULL,
    [IdRubro]            VARCHAR (50)  NOT NULL,
    [Valor]              FLOAT (53)    NOT NULL,
    [Estado]             CHAR (1)      NULL,
    [IdUsuario]          VARCHAR (20)  NULL,
    [Fecha_Transac]      DATETIME      NULL,
    [IdUsuarioUltMod]    VARCHAR (20)  NULL,
    [Fecha_UltMod]       DATETIME      NULL,
    [IdUsuarioUltAnu]    VARCHAR (20)  NULL,
    [Fecha_UltAnu]       DATETIME      NULL,
    [MotiAnula]          VARCHAR (200) NULL,
    [FechaInicio]        DATE          NULL,
    [FechaFin]           DATE          NULL,
    [es_indifinido]      BIT           NOT NULL,
    CONSTRAINT [PK_ro_empleado_x_ro_rubro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRubroFijo] ASC),
    CONSTRAINT [FK_ro_empleado_x_ro_rubro_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_empleado_x_ro_rubro_ro_Nomina_Tipoliqui] FOREIGN KEY ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui]) REFERENCES [dbo].[ro_Nomina_Tipoliqui] ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui]),
    CONSTRAINT [FK_ro_empleado_x_ro_rubro_ro_rubro_tipo] FOREIGN KEY ([IdEmpresa], [IdRubro]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro])
);



