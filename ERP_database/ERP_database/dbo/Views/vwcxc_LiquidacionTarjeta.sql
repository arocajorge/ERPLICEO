CREATE VIEW vwcxc_LiquidacionTarjeta
AS
SELECT cxc_LiquidacionTarjeta.IdEmpresa, cxc_LiquidacionTarjeta.IdSucursal, cxc_LiquidacionTarjeta.IdLiquidacion, cxc_LiquidacionTarjeta.Lote, cxc_LiquidacionTarjeta.Fecha, cxc_LiquidacionTarjeta.IdBanco, 
                  cxc_LiquidacionTarjeta.Observacion, cxc_LiquidacionTarjeta.Estado, cxc_LiquidacionTarjeta.IdEmpresa_ct, cxc_LiquidacionTarjeta.IdTipoCbte_ct, cxc_LiquidacionTarjeta.IdCbteCble_ct, cxc_LiquidacionTarjeta.Valor, 
                  ba_Banco_Cuenta.ba_descripcion, tb_sucursal.Su_Descripcion
FROM     cxc_LiquidacionTarjeta INNER JOIN
                  ba_Banco_Cuenta ON cxc_LiquidacionTarjeta.IdEmpresa = ba_Banco_Cuenta.IdEmpresa AND cxc_LiquidacionTarjeta.IdBanco = ba_Banco_Cuenta.IdBanco INNER JOIN
                  tb_sucursal ON cxc_LiquidacionTarjeta.IdEmpresa = tb_sucursal.IdEmpresa AND cxc_LiquidacionTarjeta.IdSucursal = tb_sucursal.IdSucursal