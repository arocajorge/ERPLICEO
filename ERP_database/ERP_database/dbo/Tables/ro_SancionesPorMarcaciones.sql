CREATE TABLE [dbo].[ro_SancionesPorMarcaciones] (
    [IdEmpresa]          INT           NOT NULL,
    [IdAjuste]           INT           NOT NULL,
    [IdNomina_Tipo]      INT           NOT NULL,
    [IdNomina_TipoLiqui] INT           NOT NULL,
    [FechaInicio]        DATE          NOT NULL,
    [FechaFin]           DATE          NOT NULL,
    [FechaNovedades]     DATE          NOT NULL,
    [Observacion]        VARCHAR (MAX) NULL,
    [Estado]             BIT           NOT NULL,
    [IdUsuario]          VARCHAR (50)  NULL,
    [Fecha_Transac]      DATETIME      NULL,
    [IdUsuarioUltMod]    VARCHAR (50)  NULL,
    [Fecha_UltMod]       DATETIME      NULL,
    [IdUsuarioUltAnu]    VARCHAR (50)  NULL,
    [Fecha_UltAnu]       DATETIME      NULL,
    [MotiAnula]          VARCHAR (200) NULL,
    CONSTRAINT [PK_ro_SancionesPorMarcaciones] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAjuste] ASC),
    CONSTRAINT [FK_ro_SancionesPorMarcaciones_ro_Nomina_Tipoliqui] FOREIGN KEY ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui]) REFERENCES [dbo].[ro_Nomina_Tipoliqui] ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui])
);





