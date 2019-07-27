CREATE TABLE [dbo].[ro_Nomina_Tipoliqui] (
    [IdEmpresa]                INT           NOT NULL,
    [IdNomina_Tipo]            INT           NOT NULL,
    [IdNomina_TipoLiqui]       INT           NOT NULL,
    [DescripcionProcesoNomina] VARCHAR (50)  NOT NULL,
    [IdUsuario]                VARCHAR (20)  NULL,
    [IdUsuarioAnu]             VARCHAR (20)  NULL,
    [MotivoAnu]                VARCHAR (100) NULL,
    [IdUsuarioUltModi]         VARCHAR (20)  NULL,
    [FechaAnu]                 DATETIME      NULL,
    [FechaTransac]             DATETIME      NOT NULL,
    [FechaUltModi]             DATETIME      NULL,
    [Estado]                   CHAR (1)      NOT NULL,
    CONSTRAINT [PK_ro_Nomina_Tipoliqui] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdNomina_Tipo] ASC, [IdNomina_TipoLiqui] ASC),
    CONSTRAINT [FK_ro_Nomina_Tipoliqui_ro_Nomina_Tipo] FOREIGN KEY ([IdEmpresa], [IdNomina_Tipo]) REFERENCES [dbo].[ro_Nomina_Tipo] ([IdEmpresa], [IdNomina_Tipo])
);

