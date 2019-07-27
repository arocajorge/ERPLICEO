CREATE TABLE [dbo].[ro_empleado_x_jornada] (
    [IdEmpresa]   INT          NOT NULL,
    [IdEmpleado]  NUMERIC (18) NOT NULL,
    [Secuencia]   INT          NOT NULL,
    [IdJornada]   INT          NOT NULL,
    [ValorHora]   FLOAT (53)   NOT NULL,
    [MaxNumHoras] INT          NOT NULL,
    CONSTRAINT [PK_ro_empleado_x_jornada] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdEmpleado] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_empleado_x_jornada_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_empleado_x_jornada_ro_jornada] FOREIGN KEY ([IdEmpresa], [IdJornada]) REFERENCES [dbo].[ro_jornada] ([IdEmpresa], [IdJornada])
);





