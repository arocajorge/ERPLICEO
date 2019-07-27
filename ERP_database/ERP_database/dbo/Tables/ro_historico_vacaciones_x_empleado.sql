CREATE TABLE [dbo].[ro_historico_vacaciones_x_empleado] (
    [IdEmpresa]        INT          NOT NULL,
    [IdEmpleado]       NUMERIC (18) NOT NULL,
    [IdVacacion]       INT          NOT NULL,
    [IdPeriodo_Inicio] INT          NOT NULL,
    [IdPeriodo_Fin]    INT          NOT NULL,
    [FechaIni]         DATE         NOT NULL,
    [FechaFin]         DATE         NOT NULL,
    [DiasGanado]       INT          NOT NULL,
    [DiasTomados]      INT          NOT NULL,
    CONSTRAINT [PK_ro_historico_vacaciones_x_empleado_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdEmpleado] ASC, [IdVacacion] ASC),
    CONSTRAINT [FK_ro_historico_vacaciones_x_empleado_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado])
);

