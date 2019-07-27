CREATE TABLE [dbo].[ro_empleado_x_division_x_area] (
    [IdEmpresa]   INT           NOT NULL,
    [IdEmpleado]  NUMERIC (18)  NOT NULL,
    [Secuencia]   INT           NOT NULL,
    [IDividion]   INT           NOT NULL,
    [IdArea]      INT           NOT NULL,
    [Porcentaje]  FLOAT (53)    NOT NULL,
    [Observacion] VARCHAR (MAX) NULL,
    [CargaGasto]  BIT           NULL,
    CONSTRAINT [PK_ro_empleado_x_division_x_area] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdEmpleado] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_empleado_x_division_x_area_ro_area] FOREIGN KEY ([IdEmpresa], [IDividion], [IdArea]) REFERENCES [dbo].[ro_area] ([IdEmpresa], [IdDivision], [IdArea]),
    CONSTRAINT [FK_ro_empleado_x_division_x_area_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado])
);





