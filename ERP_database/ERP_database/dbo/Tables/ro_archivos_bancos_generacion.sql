CREATE TABLE [dbo].[ro_archivos_bancos_generacion] (
    [IdEmpresa]        INT           NOT NULL,
    [IdArchivo]        NUMERIC (18)  NOT NULL,
    [IdNomina]         INT           NOT NULL,
    [IdNominaTipo]     INT           NOT NULL,
    [IdPeriodo]        INT           NOT NULL,
    [IdCuentaBancaria] INT           NOT NULL,
    [IdProceso]        INT           NOT NULL,
    [estado]           VARCHAR (50)  NOT NULL,
    [IdUsuario]        VARCHAR (20)  NULL,
    [Fecha_Transac]    DATETIME      NULL,
    [IdUsuarioUltMod]  VARCHAR (20)  NULL,
    [Fecha_UltMod]     DATETIME      NULL,
    [IdUsuarioUltAnu]  VARCHAR (20)  NULL,
    [Fecha_UltAnu]     DATETIME      NULL,
    [MotiAnula]        VARCHAR (200) NULL,
    [IdSucursal]       INT           NULL,
    CONSTRAINT [PK_ro_archivos_bancos_generacion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdArchivo] ASC),
    CONSTRAINT [FK_ro_archivos_bancos_generacion_ro_periodo_x_ro_Nomina_TipoLiqui] FOREIGN KEY ([IdEmpresa], [IdNomina], [IdNominaTipo], [IdPeriodo]) REFERENCES [dbo].[ro_periodo_x_ro_Nomina_TipoLiqui] ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui], [IdPeriodo]),
    CONSTRAINT [FK_ro_archivos_bancos_generacion_tb_banco_procesos_bancarios_x_empresa] FOREIGN KEY ([IdEmpresa], [IdProceso]) REFERENCES [dbo].[tb_banco_procesos_bancarios_x_empresa] ([IdEmpresa], [IdProceso]),
    CONSTRAINT [FK_ro_archivos_bancos_generacion_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);















