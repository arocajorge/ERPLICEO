CREATE TABLE [dbo].[ro_horario_planificacion_det] (
    [IdEmpresa]       INT           NOT NULL,
    [IdPlanificacion] NUMERIC (18)  NOT NULL,
    [IdEmpleado]      NUMERIC (18)  NOT NULL,
    [IdCalendario]    INT           NOT NULL,
    [fecha]           DATE          NOT NULL,
    [IdHorario]       NUMERIC (18)  NOT NULL,
    [Estado]          CHAR (1)      NULL,
    [Observacion]     VARCHAR (200) NULL,
    CONSTRAINT [PK_ro_horario_planificacion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCalendario] ASC, [IdEmpleado] ASC, [IdPlanificacion] ASC),
    CONSTRAINT [FK_ro_horario_planificacion_det_ro_horario_planificacion] FOREIGN KEY ([IdEmpresa], [IdPlanificacion]) REFERENCES [dbo].[ro_horario_planificacion] ([IdEmpresa], [IdPlanificacion]),
    CONSTRAINT [FK_ro_horario_planificacion_det_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa]),
    CONSTRAINT [FK_ro_horario_planificacion_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_horario_planificacion_ro_horario] FOREIGN KEY ([IdEmpresa], [IdHorario]) REFERENCES [dbo].[ro_horario] ([IdEmpresa], [IdHorario])
);

