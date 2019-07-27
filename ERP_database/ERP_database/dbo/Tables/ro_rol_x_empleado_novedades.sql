CREATE TABLE [dbo].[ro_rol_x_empleado_novedades] (
    [IdEmpresa]     INT           NOT NULL,
    [IdRol]         NUMERIC (18)  NOT NULL,
    [Secuencia]     INT           NOT NULL,
    [Secuencia_nov] INT           NOT NULL,
    [IdEmpresa_nov] INT           NOT NULL,
    [IdNovedad]     NUMERIC (18)  NOT NULL,
    [Observacion]   VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ro_rol_x_empleado_novedades] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRol] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_rol_x_empleado_novedades_ro_empleado_Novedad] FOREIGN KEY ([IdEmpresa], [IdNovedad]) REFERENCES [dbo].[ro_empleado_Novedad] ([IdEmpresa], [IdNovedad]),
    CONSTRAINT [FK_ro_rol_x_empleado_novedades_ro_rol] FOREIGN KEY ([IdEmpresa], [IdRol]) REFERENCES [dbo].[ro_rol] ([IdEmpresa], [IdRol])
);

