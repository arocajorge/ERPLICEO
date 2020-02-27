CREATE TABLE [dbo].[cxc_LiquidacionTarjeta_x_ba_TipoFlujo] (
    [IdEmpresa]     INT          NOT NULL,
    [IdSucursal]    INT          NOT NULL,
    [IdLiquidacion] NUMERIC (18) NOT NULL,
    [Secuencia]     INT          NOT NULL,
    [IdTipoFlujo]   NUMERIC (18) NOT NULL,
    [Porcentaje]    FLOAT (53)   NOT NULL,
    [Valor]         FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_cxc_LiquidacionTarjeta_x_ba_TipoFlujo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdLiquidacion] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cxc_LiquidacionTarjeta_x_ba_TipoFlujo_ba_TipoFlujo] FOREIGN KEY ([IdEmpresa], [IdTipoFlujo]) REFERENCES [dbo].[ba_TipoFlujo] ([IdEmpresa], [IdTipoFlujo]),
    CONSTRAINT [FK_cxc_LiquidacionTarjeta_x_ba_TipoFlujo_cxc_LiquidacionTarjeta] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdLiquidacion]) REFERENCES [dbo].[cxc_LiquidacionTarjeta] ([IdEmpresa], [IdSucursal], [IdLiquidacion])
);

