CREATE TABLE [dbo].[cxc_LiquidacionTarjetaDet] (
    [IdEmpresa]     INT          NOT NULL,
    [IdSucursal]    INT          NOT NULL,
    [IdLiquidacion] NUMERIC (18) NOT NULL,
    [Secuencia]     INT          NOT NULL,
    [IdMotivo]      NUMERIC (18) NOT NULL,
    [Porcentaje]    FLOAT (53)   NOT NULL,
    [Valor]         FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_cxc_LiquidacionTarjetaDet] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdLiquidacion] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cxc_LiquidacionTarjetaDet_cxc_LiquidacionTarjeta] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdLiquidacion]) REFERENCES [dbo].[cxc_LiquidacionTarjeta] ([IdEmpresa], [IdSucursal], [IdLiquidacion]),
    CONSTRAINT [FK_cxc_LiquidacionTarjetaDet_cxc_MotivoLiquidacionTarjeta] FOREIGN KEY ([IdEmpresa], [IdMotivo]) REFERENCES [dbo].[cxc_MotivoLiquidacionTarjeta] ([IdEmpresa], [IdMotivo])
);

