CREATE VIEW Academico.VWCXC_005_Diario
AS
SELECT dbo.cxc_LiquidacionTarjeta.IdEmpresa, dbo.cxc_LiquidacionTarjeta.IdSucursal, dbo.cxc_LiquidacionTarjeta.IdLiquidacion, dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.secuencia, dbo.ct_cbtecble_det.IdCtaCble, 
                  CASE WHEN dbo.ct_cbtecble_det.dc_Valor > 0 THEN dbo.ct_cbtecble_det.dc_Valor ELSE 0 END AS Debe, CASE WHEN dbo.ct_cbtecble_det.dc_Valor < 0 THEN ABS(dbo.ct_cbtecble_det.dc_Valor) ELSE 0 END AS Haber
FROM     dbo.ct_cbtecble_det INNER JOIN
                  dbo.ct_plancta ON dbo.ct_cbtecble_det.IdEmpresa = dbo.ct_plancta.IdEmpresa AND dbo.ct_cbtecble_det.IdCtaCble = dbo.ct_plancta.IdCtaCble INNER JOIN
                  dbo.cxc_LiquidacionTarjeta ON dbo.ct_cbtecble_det.IdEmpresa = dbo.cxc_LiquidacionTarjeta.IdEmpresa_ct AND dbo.ct_cbtecble_det.IdTipoCbte = dbo.cxc_LiquidacionTarjeta.IdTipoCbte_ct AND 
                  dbo.ct_cbtecble_det.IdCbteCble = dbo.cxc_LiquidacionTarjeta.IdCbteCble_ct