CREATE TABLE [dbo].[ro_PrestamoMasivo_Det] (
    [IdEmpresa]  INT          NOT NULL,
    [IdSucursal] INT          NOT NULL,
    [IdCarga]    NUMERIC (18) NOT NULL,
    [Secuencia]  INT          NOT NULL,
    [IdPrestamo] NUMERIC (18) NULL,
    [IdEmpleado] NUMERIC (18) NOT NULL,
    [IdRubro]    VARCHAR (50) NOT NULL,
    [Monto]      FLOAT (53)   NOT NULL,
    [NumCuotas]  INT          NOT NULL,
    CONSTRAINT [PK_ro_PrestamoMasivo_Det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdCarga] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_PrestamoMasivo_Det_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_PrestamoMasivo_Det_ro_prestamo] FOREIGN KEY ([IdEmpresa], [IdPrestamo]) REFERENCES [dbo].[ro_prestamo] ([IdEmpresa], [IdPrestamo]),
    CONSTRAINT [FK_ro_PrestamoMasivo_Det_ro_PrestamoMasivo] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdCarga]) REFERENCES [dbo].[ro_PrestamoMasivo] ([IdEmpresa], [IdSucursal], [IdCarga]),
    CONSTRAINT [FK_ro_PrestamoMasivo_Det_ro_rubro_tipo] FOREIGN KEY ([IdEmpresa], [IdRubro]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro])
);

