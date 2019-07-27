CREATE TABLE [dbo].[ro_EmpleadoFoto] (
    [IdEmpresa]  INT          NOT NULL,
    [IdEmpleado] NUMERIC (18) NOT NULL,
    [Foto]       IMAGE        NULL,
    CONSTRAINT [PK_ro_EmpleadoFoto] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdEmpleado] ASC),
    CONSTRAINT [FK_ro_EmpleadoFoto_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado])
);

