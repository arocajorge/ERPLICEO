CREATE TABLE [dbo].[ro_NominasPagosCheques] (
    [IdEmpresa]          INT           NOT NULL,
    [IdTransaccion]      INT           NOT NULL,
    [IdNomina_Tipo]      INT           NOT NULL,
    [IdNomina_TipoLiqui] INT           NOT NULL,
    [IdPeriodo]          INT           NOT NULL,
    [Observacion]        VARCHAR (MAX) NULL,
    [Estado]             BIT           NOT NULL,
    [IdUsuario]          VARCHAR (20)  NULL,
    [IdUsuarioAnu]       VARCHAR (20)  NULL,
    [MotivoAnu]          VARCHAR (100) NULL,
    [IdUsuarioUltModi]   VARCHAR (20)  NULL,
    [FechaAnu]           DATETIME      NULL,
    [FechaTransac]       DATETIME      NOT NULL,
    [FechaUltModi]       DATETIME      NULL,
    CONSTRAINT [PK_ro_NominasPagosCheques] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTransaccion] ASC),
    CONSTRAINT [FK_ro_NominasPagosCheques_ro_Nomina_Tipoliqui] FOREIGN KEY ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui]) REFERENCES [dbo].[ro_Nomina_Tipoliqui] ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui])
);

