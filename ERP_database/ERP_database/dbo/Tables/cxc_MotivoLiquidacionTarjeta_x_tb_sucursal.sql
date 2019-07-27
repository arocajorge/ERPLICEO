CREATE TABLE [dbo].[cxc_MotivoLiquidacionTarjeta_x_tb_sucursal] (
    [IdEmpresa]  INT          NOT NULL,
    [IdMotivo]   NUMERIC (18) NOT NULL,
    [Secuencia]  INT          NOT NULL,
    [IdSucursal] INT          NOT NULL,
    [IdCtaCble]  VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_cxc_MotivoLiquidacionTarjeta_x_tb_sucursal] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMotivo] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_cxc_MotivoLiquidacionTarjeta] FOREIGN KEY ([IdEmpresa], [IdMotivo]) REFERENCES [dbo].[cxc_MotivoLiquidacionTarjeta] ([IdEmpresa], [IdMotivo])
);

