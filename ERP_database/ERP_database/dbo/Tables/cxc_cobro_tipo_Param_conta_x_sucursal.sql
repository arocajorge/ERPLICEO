CREATE TABLE [dbo].[cxc_cobro_tipo_Param_conta_x_sucursal] (
    [IdEmpresa]          INT          NOT NULL,
    [IdSucursal]         INT          NOT NULL,
    [IdCobro_tipo]       VARCHAR (20) NOT NULL,
    [IdCtaCble]          VARCHAR (20) NULL,
    [IdCtaCble_Anticipo] VARCHAR (20) NULL,
    CONSTRAINT [PK_cxc_cobro_tipo_Parametro_conta_x_sucursal] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdCobro_tipo] ASC),
    CONSTRAINT [FK_cxc_cobro_tipo_Param_conta_x_sucursal_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_cxc_cobro_tipo_Param_conta_x_sucursal_ct_plancta1] FOREIGN KEY ([IdEmpresa], [IdCtaCble_Anticipo]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_cxc_cobro_tipo_Param_conta_x_sucursal_cxc_cobro_tipo] FOREIGN KEY ([IdCobro_tipo]) REFERENCES [dbo].[cxc_cobro_tipo] ([IdCobro_tipo]),
    CONSTRAINT [FK_cxc_cobro_tipo_Param_conta_x_sucursal_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);

