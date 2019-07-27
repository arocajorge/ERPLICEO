CREATE TABLE [dbo].[ro_Solicitud_Vacaciones_x_empleado_x_historico_vacaciones_x_empleado] (
    [IdEmpresa_sol]   INT           NOT NULL,
    [IdEmpleado_sol]  NUMERIC (18)  NOT NULL,
    [IdSolicitud]     INT           NOT NULL,
    [IdEmpresa_vaca]  INT           NOT NULL,
    [IdEmpleado_vaca] NUMERIC (18)  NOT NULL,
    [IdVacacion]      INT           NOT NULL,
    [Observacion]     VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ro_Solicitud_Vacaciones_x_empleado_x_historico_vacaciones_x_empleado] PRIMARY KEY CLUSTERED ([IdEmpresa_sol] ASC, [IdEmpleado_sol] ASC, [IdSolicitud] ASC, [IdEmpresa_vaca] ASC, [IdEmpleado_vaca] ASC, [IdVacacion] ASC),
    CONSTRAINT [FK_ro_Solicitud_Vacaciones_x_empleado_x_historico_vacaciones_x_empleado_ro_historico_vacaciones_x_empleado] FOREIGN KEY ([IdEmpresa_vaca], [IdEmpleado_vaca], [IdVacacion]) REFERENCES [dbo].[ro_historico_vacaciones_x_empleado] ([IdEmpresa], [IdEmpleado], [IdVacacion]),
    CONSTRAINT [FK_ro_Solicitud_Vacaciones_x_empleado_x_historico_vacaciones_x_empleado_ro_Solicitud_Vacaciones_x_empleado] FOREIGN KEY ([IdEmpresa_sol], [IdEmpleado_vaca], [IdSolicitud]) REFERENCES [dbo].[ro_Solicitud_Vacaciones_x_empleado] ([IdEmpresa], [IdEmpleado], [IdSolicitud])
);

