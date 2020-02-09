CREATE VIEW ACADEMICO.VWCXC_002_diario AS
SELECT dbo.cxc_cobro_x_ct_cbtecble.cbr_IdEmpresa, dbo.cxc_cobro_x_ct_cbtecble.cbr_IdSucursal, dbo.cxc_cobro_x_ct_cbtecble.cbr_IdCobro, dbo.ct_cbtecble_det.IdEmpresa, dbo.ct_cbtecble_det.IdTipoCbte, dbo.ct_cbtecble_det.IdCbteCble, 
                  dbo.ct_cbtecble_det.secuencia, dbo.ct_cbtecble_det.IdCtaCble, dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.dc_Valor, CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ct_cbtecble_det.dc_Valor ELSE 0 END AS dc_Valor_Debe, 
                  CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Haber
FROM     dbo.ct_plancta INNER JOIN
                  dbo.ct_cbtecble_det ON dbo.ct_plancta.IdEmpresa = dbo.ct_cbtecble_det.IdEmpresa AND dbo.ct_plancta.IdCtaCble = dbo.ct_cbtecble_det.IdCtaCble INNER JOIN
                  dbo.cxc_cobro_x_ct_cbtecble ON dbo.ct_cbtecble_det.IdEmpresa = dbo.cxc_cobro_x_ct_cbtecble.ct_IdEmpresa AND dbo.ct_cbtecble_det.IdTipoCbte = dbo.cxc_cobro_x_ct_cbtecble.ct_IdTipoCbte AND 
                  dbo.ct_cbtecble_det.IdCbteCble = dbo.cxc_cobro_x_ct_cbtecble.ct_IdCbteCble