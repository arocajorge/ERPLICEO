CREATE TABLE [dbo].[ro_nomina_x_horas_extras] (
    [IdEmpresa]         INT           NOT NULL,
    [IdHorasExtras]     INT           NOT NULL,
    [IdNominaTipo]      INT           NOT NULL,
    [IdNominaTipoLiqui] INT           NOT NULL,
    [IdPeriodo]         INT           NOT NULL,
    [Estado]            VARCHAR (1)   NOT NULL,
    [IdUsuario]         VARCHAR (20)  NULL,
    [Fecha_Transac]     DATETIME      NULL,
    [IdUsuarioUltMod]   VARCHAR (20)  NULL,
    [Fecha_UltMod]      DATETIME      NULL,
    [IdUsuarioUltAnu]   VARCHAR (20)  NULL,
    [Fecha_UltAnu]      DATETIME      NULL,
    [nom_pc]            VARCHAR (50)  NULL,
    [ip]                VARCHAR (25)  NULL,
    [MotivoAnulacion]   VARCHAR (100) NULL,
    [observacion]       VARCHAR (500) NULL,
    CONSTRAINT [PK_ro_nomina_x_horas_extras_] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdHorasExtras] ASC),
    CONSTRAINT [FK_ro_nomina_x_horas_extras_ro_periodo_x_ro_Nomina_TipoLiqui] FOREIGN KEY ([IdEmpresa], [IdNominaTipo], [IdNominaTipoLiqui], [IdPeriodo]) REFERENCES [dbo].[ro_periodo_x_ro_Nomina_TipoLiqui] ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui], [IdPeriodo])
);

