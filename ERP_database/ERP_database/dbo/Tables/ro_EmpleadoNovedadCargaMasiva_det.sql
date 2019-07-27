CREATE TABLE [dbo].[ro_EmpleadoNovedadCargaMasiva_det] (
    [IdEmpresa]     INT           NOT NULL,
    [IdCarga]       NUMERIC (18)  NOT NULL,
    [Secuencia]     INT           NOT NULL,
    [IdEmpresa_nov] INT           NOT NULL,
    [IdNovedad]     NUMERIC (18)  NOT NULL,
    [Observacion]   VARCHAR (MAX) NULL,
    [IdEmpleado]    NUMERIC (18)  NOT NULL,
    CONSTRAINT [PK_ro_EmpleadoNovedadCargaMasiva_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCarga] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_EmpleadoNovedadCargaMasiva_det_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_EmpleadoNovedadCargaMasiva_det_ro_empleado_Novedad] FOREIGN KEY ([IdEmpresa], [IdNovedad]) REFERENCES [dbo].[ro_empleado_Novedad] ([IdEmpresa], [IdNovedad]),
    CONSTRAINT [FK_ro_EmpleadoNovedadCargaMasiva_det_ro_EmpleadoNovedadCargaMasiva] FOREIGN KEY ([IdEmpresa], [IdCarga]) REFERENCES [dbo].[ro_EmpleadoNovedadCargaMasiva] ([IdEmpresa], [IdCarga])
);



