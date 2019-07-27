CREATE TABLE [EntidadRegulatoria].[ro_rdep] (
    [IdEmpresa]                INT           NOT NULL,
    [Id_Rdep]                  INT           NOT NULL,
    [IdSucursal]               INT           NOT NULL,
    [pe_anio]                  INT           NOT NULL,
    [IdNomina_Tipo]            INT           NOT NULL,
    [Su_CodigoEstablecimiento] VARCHAR (5)   NULL,
    [Observacion]              VARCHAR (MAX) NULL,
    [Estado]                   BIT           NOT NULL,
    [IdUsuario]                VARCHAR (20)  NULL,
    [Fecha_Transac]            DATETIME      NULL,
    [IdUsuarioUltMod]          VARCHAR (20)  NULL,
    [Fecha_UltMod]             DATETIME      NULL,
    [IdUsuarioUltAnu]          VARCHAR (20)  NULL,
    [Fecha_UltAnu]             DATETIME      NULL,
    [MotiAnula]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ro_rdep] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [Id_Rdep] ASC),
    CONSTRAINT [FK_ro_rdep_ro_Nomina_Tipo1] FOREIGN KEY ([IdEmpresa], [IdNomina_Tipo]) REFERENCES [dbo].[ro_Nomina_Tipo] ([IdEmpresa], [IdNomina_Tipo]),
    CONSTRAINT [FK_ro_rdep_tb_sucursal1] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);

