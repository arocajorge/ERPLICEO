CREATE VIEW vwcxc_MotivoLiquidacionTarjeta_x_tb_sucursal
AS
SELECT cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.IdEmpresa, cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.IdMotivo, cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.Secuencia, cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.IdSucursal, 
                  cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.IdCtaCble, cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.IdCtaCble + ' - ' + ct_plancta.pc_Cuenta pc_Cuenta, tb_sucursal.Su_Descripcion
FROM     cxc_MotivoLiquidacionTarjeta_x_tb_sucursal LEFT OUTER JOIN
                  tb_sucursal ON cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.IdSucursal = tb_sucursal.IdSucursal AND cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.IdEmpresa = tb_sucursal.IdEmpresa LEFT OUTER JOIN
                  ct_plancta ON cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.IdEmpresa = ct_plancta.IdEmpresa AND cxc_MotivoLiquidacionTarjeta_x_tb_sucursal.IdCtaCble = ct_plancta.IdCtaCble