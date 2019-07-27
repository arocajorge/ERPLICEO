CREATE TABLE [dbo].[ro_rol_x_prestamo_detalle] (
    [IdEmpresa]     INT           NOT NULL,
    [IdRol]         NUMERIC (18)  NOT NULL,
    [Secuencia]     INT           NOT NULL,
    [IdEmpresa_pre] INT           NULL,
    [IdPrestamo]    NUMERIC (18)  NULL,
    [NumCuota]      INT           NULL,
    [Observacion]   VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ro_rol_x_prestamo_detalle] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRol] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_rol_x_prestamo_detalle_ro_rol] FOREIGN KEY ([IdEmpresa], [IdRol]) REFERENCES [dbo].[ro_rol] ([IdEmpresa], [IdRol])
);

