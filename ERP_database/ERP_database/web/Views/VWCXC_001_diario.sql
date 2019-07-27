CREATE VIEW web.VWCXC_001_diario
AS
SELECT cxc_cobro_x_ct_cbtecble.cbr_IdEmpresa, cxc_cobro_x_ct_cbtecble.cbr_IdSucursal, cxc_cobro_x_ct_cbtecble.cbr_IdCobro, 
                  ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdTipoCbte, ct_cbtecble_det.IdCbteCble, ct_cbtecble_det.secuencia,
ct_cbtecble_det.IdCtaCble, ct_plancta.pc_Cuenta, ct_cbtecble_det.dc_Valor, CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ct_cbtecble_det.dc_Valor ELSE 0 END AS dc_Valor_Debe, 
                  CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Haber
FROM     ct_plancta INNER JOIN
                  ct_cbtecble_det ON ct_plancta.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_plancta.IdCtaCble = ct_cbtecble_det.IdCtaCble INNER JOIN
                  cxc_cobro_x_ct_cbtecble ON ct_cbtecble_det.IdEmpresa = cxc_cobro_x_ct_cbtecble.ct_IdEmpresa AND ct_cbtecble_det.IdTipoCbte = cxc_cobro_x_ct_cbtecble.ct_IdTipoCbte AND 
                  ct_cbtecble_det.IdCbteCble = cxc_cobro_x_ct_cbtecble.ct_IdCbteCble