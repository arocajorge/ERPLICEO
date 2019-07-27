CREATE TABLE [dbo].[ro_SancionesPorMarcaciones_x_novedad] (
    [IdEmpresa]          INT          NOT NULL,
    [IdAjuste]           INT          NOT NULL,
    [Secuencia]          INT          NOT NULL,
    [IdNovedad]          NUMERIC (18) NOT NULL,
    [IdEmpresa_nov]      INT          NOT NULL,
    [IdEmpleado]         NUMERIC (18) NOT NULL,
    [IdNomina_Tipo]      INT          NOT NULL,
    [IdNomina_TipoLiqui] INT          NOT NULL,
    CONSTRAINT [PK_ro_SancionesPorMarcaciones_x_novedad] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAjuste] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_SancionesPorMarcaciones_x_novedad_ro_empleado_Novedad] FOREIGN KEY ([IdEmpresa_nov], [IdNovedad]) REFERENCES [dbo].[ro_empleado_Novedad] ([IdEmpresa], [IdNovedad]),
    CONSTRAINT [FK_ro_SancionesPorMarcaciones_x_novedad_ro_Nomina_Tipoliqui] FOREIGN KEY ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui]) REFERENCES [dbo].[ro_Nomina_Tipoliqui] ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui])
);

