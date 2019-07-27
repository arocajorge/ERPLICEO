CREATE VIEW [web].[VWCXP_006_diario]
AS
SELECT r.IdEmpresa, r.IdRetencion, r.IdEmpresa_Ogiro, r.IdTipoCbte_Ogiro, r.IdCbteCble_Ogiro, ct.IdTipoCbte, ct.IdCbteCble, ct.secuencia, ct.IdCtaCble, pc.pc_Cuenta, ct.dc_Valor, CASE WHEN ct.dc_Valor > 0 THEN ct.dc_Valor ELSE 0 END AS dc_Valor_Debe, 
                  CASE WHEN ct.dc_Valor < 0 THEN ABS(ct.dc_Valor) ELSE 0 END AS dc_Valor_Haber
FROM     dbo.cp_retencion_x_ct_cbtecble AS ret INNER JOIN
                  dbo.ct_cbtecble_det AS ct ON ret.ct_IdEmpresa = ct.IdEmpresa AND ret.ct_IdTipoCbte = ct.IdTipoCbte AND ret.ct_IdCbteCble = ct.IdCbteCble INNER JOIN
                  dbo.ct_plancta AS pc ON pc.IdEmpresa = ct.IdEmpresa AND pc.IdCtaCble = ct.IdCtaCble INNER JOIN
                  dbo.cp_retencion AS r ON ret.rt_IdEmpresa = r.IdEmpresa AND ret.rt_IdRetencion = r.IdRetencion
