CREATE TABLE [dbo].[Af_Activo_fijo_CtaCble] (
    [IdEmpresa]      INT          NOT NULL,
    [IdActivoFijo]   INT          NOT NULL,
    [Secuencia]      INT          NOT NULL,
    [IdDepartamento] NUMERIC (18) NOT NULL,
    [IdCtaCble]      VARCHAR (20) NOT NULL,
    [Porcentaje]     FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_Af_Activo_fijo_CtaCble] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdActivoFijo] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_Af_Activo_fijo_CtaCble_Af_Activo_fijo] FOREIGN KEY ([IdEmpresa], [IdActivoFijo]) REFERENCES [dbo].[Af_Activo_fijo] ([IdEmpresa], [IdActivoFijo]),
    CONSTRAINT [FK_Af_Activo_fijo_CtaCble_Af_Departamento] FOREIGN KEY ([IdEmpresa], [IdDepartamento]) REFERENCES [dbo].[Af_Departamento] ([IdEmpresa], [IdDepartamento]),
    CONSTRAINT [FK_Af_Activo_fijo_CtaCble_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble])
);



