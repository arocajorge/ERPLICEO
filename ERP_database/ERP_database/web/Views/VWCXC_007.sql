CREATE VIEW web.VWCXC_007
AS
SELECT dbo.cxc_LiquidacionTarjetaDet.IdEmpresa, dbo.cxc_LiquidacionTarjetaDet.IdSucursal, dbo.cxc_LiquidacionTarjetaDet.IdLiquidacion, dbo.cxc_LiquidacionTarjetaDet.Secuencia, dbo.cxc_LiquidacionTarjetaDet.IdMotivo, 
                  dbo.cxc_MotivoLiquidacionTarjeta.Descripcion AS DescripcionMotivo, dbo.cxc_LiquidacionTarjetaDet.Valor, dbo.tb_sucursal.Su_Descripcion, dbo.cxc_LiquidacionTarjeta.Fecha, dbo.cxc_LiquidacionTarjeta.Lote, 
                  dbo.cxc_LiquidacionTarjeta.Observacion, dbo.cxc_LiquidacionTarjeta.Estado, dbo.cxc_LiquidacionTarjeta.IdEmpresa_ct, dbo.cxc_LiquidacionTarjeta.IdTipoCbte_ct, dbo.cxc_LiquidacionTarjeta.IdCbteCble_ct, 
                  dbo.ba_Banco_Cuenta.ba_descripcion, dbo.seg_usuario.Nombre AS NombreUsuario
FROM     dbo.cxc_MotivoLiquidacionTarjeta INNER JOIN
                  dbo.cxc_LiquidacionTarjetaDet ON dbo.cxc_MotivoLiquidacionTarjeta.IdEmpresa = dbo.cxc_LiquidacionTarjetaDet.IdEmpresa AND dbo.cxc_MotivoLiquidacionTarjeta.IdMotivo = dbo.cxc_LiquidacionTarjetaDet.IdMotivo INNER JOIN
                  dbo.cxc_LiquidacionTarjeta ON dbo.cxc_LiquidacionTarjetaDet.IdEmpresa = dbo.cxc_LiquidacionTarjeta.IdEmpresa AND dbo.cxc_LiquidacionTarjetaDet.IdSucursal = dbo.cxc_LiquidacionTarjeta.IdSucursal AND 
                  dbo.cxc_LiquidacionTarjetaDet.IdLiquidacion = dbo.cxc_LiquidacionTarjeta.IdLiquidacion INNER JOIN
                  dbo.tb_sucursal ON dbo.cxc_LiquidacionTarjeta.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.cxc_LiquidacionTarjeta.IdSucursal = dbo.tb_sucursal.IdSucursal INNER JOIN
                  dbo.ba_Banco_Cuenta ON dbo.cxc_LiquidacionTarjeta.IdEmpresa = dbo.ba_Banco_Cuenta.IdEmpresa AND dbo.cxc_LiquidacionTarjeta.IdBanco = dbo.ba_Banco_Cuenta.IdBanco LEFT OUTER JOIN
                  dbo.seg_usuario ON dbo.cxc_LiquidacionTarjeta.IdUsuarioCreacion = dbo.seg_usuario.IdUsuario