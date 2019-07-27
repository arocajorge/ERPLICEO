CREATE TABLE [dbo].[cxc_LiquidacionTarjeta_x_cxc_cobro] (
    [IdEmpresa]     INT          NOT NULL,
    [IdSucursal]    INT          NOT NULL,
    [IdLiquidacion] NUMERIC (18) NOT NULL,
    [Secuencia]     INT          NOT NULL,
    [Valor]         FLOAT (53)   NOT NULL,
    [IdCobro]       NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_cxc_LiquidacionTarjeta_x_cxc_cobro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdLiquidacion] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cxc_LiquidacionTarjeta_x_cxc_cobro_cxc_cobro] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdCobro]) REFERENCES [dbo].[cxc_cobro] ([IdEmpresa], [IdSucursal], [IdCobro]),
    CONSTRAINT [FK_cxc_LiquidacionTarjeta_x_cxc_cobro_cxc_LiquidacionTarjeta] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdLiquidacion]) REFERENCES [dbo].[cxc_LiquidacionTarjeta] ([IdEmpresa], [IdSucursal], [IdLiquidacion])
);

