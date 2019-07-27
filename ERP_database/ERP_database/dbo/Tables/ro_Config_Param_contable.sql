CREATE TABLE [dbo].[ro_Config_Param_contable] (
    [IdEmpresa]       INT          NOT NULL,
    [IdDivision]      INT          NOT NULL,
    [IdArea]          INT          NOT NULL,
    [IdDepartamento]  INT          NOT NULL,
    [IdRubro]         VARCHAR (50) NOT NULL,
    [IdCtaCble]       VARCHAR (20) NULL,
    [IdCentroCosto]   VARCHAR (20) NULL,
    [DebCre]          CHAR (1)     NULL,
    [IdCtaCble_Haber] VARCHAR (20) NULL,
    CONSTRAINT [PK_ro_config_param_contable] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdDivision] ASC, [IdArea] ASC, [IdDepartamento] ASC, [IdRubro] ASC),
    CONSTRAINT [FK_ro_Config_Param_contable_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_ro_Config_Param_contable_ct_plancta1] FOREIGN KEY ([IdEmpresa], [IdCtaCble_Haber]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_ro_Config_Param_contable_ro_area] FOREIGN KEY ([IdEmpresa], [IdDivision], [IdArea]) REFERENCES [dbo].[ro_area] ([IdEmpresa], [IdDivision], [IdArea]),
    CONSTRAINT [FK_ro_Config_Param_contable_ro_Departamento] FOREIGN KEY ([IdEmpresa], [IdDepartamento]) REFERENCES [dbo].[ro_Departamento] ([IdEmpresa], [IdDepartamento]),
    CONSTRAINT [FK_ro_Config_Param_contable_ro_rubro_tipo] FOREIGN KEY ([IdEmpresa], [IdRubro]) REFERENCES [dbo].[ro_rubro_tipo] ([IdEmpresa], [IdRubro]),
    CONSTRAINT [FK_ro_Config_Param_contable_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);



