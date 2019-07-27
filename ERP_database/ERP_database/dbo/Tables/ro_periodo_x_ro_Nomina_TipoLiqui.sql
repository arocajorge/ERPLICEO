CREATE TABLE [dbo].[ro_periodo_x_ro_Nomina_TipoLiqui] (
    [IdEmpresa]          INT      NOT NULL,
    [IdNomina_Tipo]      INT      NOT NULL,
    [IdNomina_TipoLiqui] INT      NOT NULL,
    [IdPeriodo]          INT      NOT NULL,
    [Cerrado]            CHAR (1) NOT NULL,
    [Procesado]          CHAR (1) NOT NULL,
    [Contabilizado]      CHAR (1) NOT NULL,
    CONSTRAINT [PK_ro_periodo_x_ro_Nomina_TipoLiqui] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdNomina_Tipo] ASC, [IdNomina_TipoLiqui] ASC, [IdPeriodo] ASC),
    CONSTRAINT [FK_ro_periodo_x_ro_Nomina_TipoLiqui_ro_Nomina_Tipoliqui] FOREIGN KEY ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui]) REFERENCES [dbo].[ro_Nomina_Tipoliqui] ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui]),
    CONSTRAINT [FK_ro_periodo_x_ro_Nomina_TipoLiqui_ro_periodo] FOREIGN KEY ([IdEmpresa], [IdPeriodo]) REFERENCES [dbo].[ro_periodo] ([IdEmpresa], [IdPeriodo])
);

