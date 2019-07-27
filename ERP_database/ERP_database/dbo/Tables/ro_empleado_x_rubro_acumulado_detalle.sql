CREATE TABLE [dbo].[ro_empleado_x_rubro_acumulado_detalle] (
    [IdEmpresa]              INT          NOT NULL,
    [IdEmpleado]             NUMERIC (18) NOT NULL,
    [IdRubro]                VARCHAR (50) NOT NULL,
    [Secuencia]              INT          NOT NULL,
    [IdRubroContabilizacion] VARCHAR (50) NOT NULL,
    [IdJornada]              INT          NOT NULL,
    CONSTRAINT [PK_ro_empleado_x_rubro_acumulado_detalle] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdEmpleado] ASC, [IdRubro] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_empleado_x_rubro_acumulado_detalle_ro_empleado_x_rubro_acumulado] FOREIGN KEY ([IdEmpresa], [IdEmpleado], [IdRubro]) REFERENCES [dbo].[ro_empleado_x_rubro_acumulado] ([IdEmpresa], [IdEmpleado], [IdRubro]),
    CONSTRAINT [FK_ro_empleado_x_rubro_acumulado_detalle_ro_jornada] FOREIGN KEY ([IdEmpresa], [IdJornada]) REFERENCES [dbo].[ro_jornada] ([IdEmpresa], [IdJornada]),
    CONSTRAINT [FK_ro_empleado_x_rubro_acumulado_detalle_ro_rubro_tipo] FOREIGN KEY ([IdEmpresa], [IdRubroContabilizacion]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro])
);

