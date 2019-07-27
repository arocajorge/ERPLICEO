CREATE TABLE [dbo].[ro_empleado_x_horario] (
    [IdEmpresa]        INT          NOT NULL,
    [IdEmpleado]       NUMERIC (18) NOT NULL,
    [IdHorario]        NUMERIC (18) NOT NULL,
    [EsPredeterminado] BIT          NOT NULL,
    [FechaIngresa]     DATETIME     NOT NULL,
    [UsuarioIngresa]   VARCHAR (25) NOT NULL,
    CONSTRAINT [PK_ro_empleado_x_horario] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdEmpleado] ASC, [IdHorario] ASC),
    CONSTRAINT [FK_ro_empleado_x_horario_ro_horario] FOREIGN KEY ([IdEmpresa], [IdHorario]) REFERENCES [dbo].[ro_horario] ([IdEmpresa], [IdHorario])
);

