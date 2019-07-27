CREATE TABLE [dbo].[ro_HorasProfesores] (
    [IdEmpresa]       INT           NOT NULL,
    [IdCarga]         NUMERIC (18)  NOT NULL,
    [IdNomina]        INT           NOT NULL,
    [IdNominaTipo]    INT           NOT NULL,
    [IdPeriodo]       INT           NOT NULL,
    [FechaCarga]      DATE          NOT NULL,
    [Observacion]     VARCHAR (MAX) NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Estado]          BIT           NOT NULL,
    [Fecha_Transac]   DATETIME      NOT NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [MotiAnula]       VARCHAR (200) NULL,
    CONSTRAINT [PK_ro_HorasProfesores] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCarga] ASC),
    CONSTRAINT [FK_ro_HorasProfesores_ro_Nomina_Tipoliqui] FOREIGN KEY ([IdEmpresa], [IdNomina], [IdNominaTipo]) REFERENCES [dbo].[ro_Nomina_Tipoliqui] ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui])
);



