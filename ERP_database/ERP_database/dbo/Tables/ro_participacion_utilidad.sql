CREATE TABLE [dbo].[ro_participacion_utilidad] (
    [IdEmpresa]                  INT           NOT NULL,
    [IdUtilidad]                 INT           NOT NULL,
    [IdNomina]                   INT           NOT NULL,
    [IdNominaTipo_liq]           INT           NOT NULL,
    [IdPeriodo]                  INT           NOT NULL,
    [UtilidadDerechoIndividual]  FLOAT (53)    NOT NULL,
    [UtilidadCargaFamiliar]      FLOAT (53)    NOT NULL,
    [LimiteDistribucionUtilidad] FLOAT (53)    NOT NULL,
    [FechaIngresa]               DATETIME      NULL,
    [UsuarioIngresa]             VARCHAR (25)  NULL,
    [Observacion]                VARCHAR (100) NULL,
    [IdUsuarioModifica]          VARCHAR (25)  NULL,
    [Fecha_ultima_modif]         DATETIME      NULL,
    [IdUsuario_anula]            VARCHAR (25)  NULL,
    [Fecha_anulacion]            DATETIME      NULL,
    [Estado]                     VARCHAR (1)   NULL,
    CONSTRAINT [PK_ro_participacion_utilidad] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdUtilidad] ASC),
    CONSTRAINT [FK_ro_participacion_utilidad_ro_periodo_x_ro_Nomina_TipoLiqui] FOREIGN KEY ([IdEmpresa], [IdNomina], [IdNominaTipo_liq], [IdPeriodo]) REFERENCES [dbo].[ro_periodo_x_ro_Nomina_TipoLiqui] ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui], [IdPeriodo])
);

