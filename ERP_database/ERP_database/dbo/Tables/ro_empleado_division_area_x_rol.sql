CREATE TABLE [dbo].[ro_empleado_division_area_x_rol] (
    [IdEmpresa]   INT           NOT NULL,
    [IdRol]       NUMERIC (18)  NOT NULL,
    [Secuencia]   INT           NOT NULL,
    [IdEmpleado]  NUMERIC (18)  NOT NULL,
    [IDividion]   INT           NOT NULL,
    [IdArea]      INT           NOT NULL,
    [Porcentaje]  FLOAT (53)    NOT NULL,
    [Observacion] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ro_empleado_division_area_x_rol] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRol] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_empleado_division_area_x_rol_ro_area] FOREIGN KEY ([IdEmpresa], [IDividion], [IdArea]) REFERENCES [dbo].[ro_area] ([IdEmpresa], [IdDivision], [IdArea]),
    CONSTRAINT [FK_ro_empleado_division_area_x_rol_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_empleado_division_area_x_rol_ro_rol] FOREIGN KEY ([IdEmpresa], [IdRol]) REFERENCES [dbo].[ro_rol] ([IdEmpresa], [IdRol])
);

