CREATE TABLE [dbo].[ro_rol] (
    [IdEmpresa]         INT           NOT NULL,
    [IdRol]             NUMERIC (18)  NOT NULL,
    [IdSucursal]        INT           NULL,
    [IdNominaTipo]      INT           NOT NULL,
    [IdNominaTipoLiqui] INT           NOT NULL,
    [IdPeriodo]         INT           NOT NULL,
    [Descripcion]       VARCHAR (100) NOT NULL,
    [Observacion]       VARCHAR (200) NULL,
    [Cerrado]           VARCHAR (50)  NOT NULL,
    [FechaIngresa]      DATETIME      NOT NULL,
    [UsuarioIngresa]    VARCHAR (25)  NOT NULL,
    [FechaModifica]     DATETIME      NULL,
    [UsuarioModifica]   VARCHAR (25)  NULL,
    [FechaAnula]        DATETIME      NULL,
    [UsuarioAnula]      VARCHAR (25)  NULL,
    [MotivoAnula]       VARCHAR (200) NULL,
    [UsuarioCierre]     VARCHAR (25)  NULL,
    [FechaCierre]       DATETIME      NULL,
    [IdCentroCosto]     VARCHAR (20)  NULL,
    [PorAportePersonal] FLOAT (53)    NULL,
    [SalarioBasico]     FLOAT (53)    NULL,
    CONSTRAINT [PK_ro_rol] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRol] ASC),
    CONSTRAINT [FK_ro_rol_ro_Nomina_Tipo] FOREIGN KEY ([IdEmpresa], [IdNominaTipo]) REFERENCES [dbo].[ro_Nomina_Tipo] ([IdEmpresa], [IdNomina_Tipo]),
    CONSTRAINT [FK_ro_rol_ro_Nomina_Tipoliqui] FOREIGN KEY ([IdEmpresa], [IdNominaTipo], [IdNominaTipoLiqui]) REFERENCES [dbo].[ro_Nomina_Tipoliqui] ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui]),
    CONSTRAINT [FK_ro_rol_ro_periodo_x_ro_Nomina_TipoLiqui] FOREIGN KEY ([IdEmpresa], [IdNominaTipo], [IdNominaTipoLiqui], [IdPeriodo]) REFERENCES [dbo].[ro_periodo_x_ro_Nomina_TipoLiqui] ([IdEmpresa], [IdNomina_Tipo], [IdNomina_TipoLiqui], [IdPeriodo]),
    CONSTRAINT [FK_ro_rol_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);









