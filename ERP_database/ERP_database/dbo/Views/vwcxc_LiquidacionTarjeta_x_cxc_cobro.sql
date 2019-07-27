create view vwcxc_LiquidacionTarjeta_x_cxc_cobro
as
SELECT dbo.cxc_cobro.IdEmpresa, dbo.cxc_cobro.IdSucursal, dbo.cxc_cobro.IdCobro, dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.IdLiquidacion, dbo.cxc_cobro.cr_TotalCobro, dbo.cxc_cobro.cr_fecha, dbo.cxc_cobro.cr_observacion, 
                  dbo.tb_persona.pe_nombreCompleto
FROM     dbo.fa_cliente INNER JOIN
                  dbo.cxc_cobro ON dbo.fa_cliente.IdEmpresa = dbo.cxc_cobro.IdEmpresa AND dbo.fa_cliente.IdCliente = dbo.cxc_cobro.IdCliente INNER JOIN
                  dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.cxc_cobro_tipo ON dbo.cxc_cobro.IdCobro_tipo = dbo.cxc_cobro_tipo.IdCobro_tipo LEFT OUTER JOIN
                  dbo.cxc_LiquidacionTarjeta_x_cxc_cobro ON dbo.cxc_cobro.IdEmpresa = dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.IdEmpresa AND dbo.cxc_cobro.IdSucursal = dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.IdSucursal AND 
                  dbo.cxc_cobro.IdCobro = dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.IdCobro
WHERE  (dbo.cxc_cobro_tipo.EsTarjetaCredito = 1)