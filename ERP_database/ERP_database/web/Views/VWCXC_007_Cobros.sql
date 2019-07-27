CREATE VIEW web.VWCXC_007_Cobros
AS
SELECT dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.IdEmpresa, dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.IdSucursal, dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.IdLiquidacion, dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.Secuencia, 
                  dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.Valor, dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.IdCobro, dbo.cxc_cobro.cr_fecha, dbo.tb_persona.pe_nombreCompleto, dbo.cxc_cobro.cr_observacion
FROM     dbo.cxc_LiquidacionTarjeta_x_cxc_cobro INNER JOIN
                  dbo.cxc_cobro ON dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.IdEmpresa = dbo.cxc_cobro.IdEmpresa AND dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.IdSucursal = dbo.cxc_cobro.IdSucursal AND 
                  dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.IdCobro = dbo.cxc_cobro.IdCobro INNER JOIN
                  dbo.fa_cliente ON dbo.cxc_cobro.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.cxc_cobro.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                  dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona