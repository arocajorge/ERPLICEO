CREATE TABLE [dbo].[ro_empleado_Novedad] (
    [IdEmpresa]          INT           NOT NULL,
    [IdNovedad]          NUMERIC (18)  NOT NULL,
    [IdEmpleado]         NUMERIC (18)  NOT NULL,
    [IdSucursal]         INT           NULL,
    [Observacion]        VARCHAR (MAX) NULL,
    [IdNomina_Tipo]      INT           NOT NULL,
    [IdNomina_TipoLiqui] INT           NOT NULL,
    [Fecha]              DATETIME      NOT NULL,
    [IdJornada]          INT           NULL,
    [IdUsuario]          VARCHAR (20)  NOT NULL,
    [Estado]             CHAR (1)      NOT NULL,
    [Fecha_Transac]      DATETIME      NOT NULL,
    [IdUsuarioUltMod]    VARCHAR (20)  NULL,
    [Fecha_UltMod]       DATETIME      NULL,
    [IdUsuarioUltAnu]    VARCHAR (20)  NULL,
    [Fecha_UltAnu]       DATETIME      NULL,
    [MotiAnula]          VARCHAR (200) NULL,
    CONSTRAINT [PK_ro_empleado_Novedad] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdNovedad] ASC),
    CONSTRAINT [FK_ro_empleado_Novedad_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_empleado_Novedad_ro_empleado1] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_empleado_Novedad_ro_jornada] FOREIGN KEY ([IdEmpresa], [IdJornada]) REFERENCES [dbo].[ro_jornada] ([IdEmpresa], [IdJornada]),
    CONSTRAINT [FK_ro_empleado_Novedad_ro_Nomina_Tipoliqui] FOREIGN KEY ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui]) REFERENCES [dbo].[ro_Nomina_Tipoliqui] ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui])
);





