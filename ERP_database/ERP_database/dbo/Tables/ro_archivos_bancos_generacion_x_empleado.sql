CREATE TABLE [dbo].[ro_archivos_bancos_generacion_x_empleado] (
    [IdEmpresa]  INT          NOT NULL,
    [IdArchivo]  NUMERIC (18) NOT NULL,
    [Secuencia]  INT          NOT NULL,
    [IdSucursal] INT          NOT NULL,
    [IdEmpleado] NUMERIC (18) NOT NULL,
    [Valor]      FLOAT (53)   NOT NULL,
    [pagacheque] BIT          NOT NULL,
    CONSTRAINT [PK_ro_archivos_bancos_generacion_x_empleado_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdArchivo] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_archivos_bancos_generacion_x_empleado_ro_archivos_bancos_generacion] FOREIGN KEY ([IdEmpresa], [IdArchivo]) REFERENCES [dbo].[ro_archivos_bancos_generacion] ([IdEmpresa], [IdArchivo]),
    CONSTRAINT [FK_ro_archivos_bancos_generacion_x_empleado_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado])
);









