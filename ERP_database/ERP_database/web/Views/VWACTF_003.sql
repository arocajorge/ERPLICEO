CREATE VIEW [web].[VWACTF_003]
AS
SELECT dbo.Af_Retiro_Activo.IdEmpresa, dbo.Af_Retiro_Activo.IdRetiroActivo, dbo.Af_Retiro_Activo.NumComprobante, dbo.Af_Retiro_Activo.IdActivoFijo, dbo.Af_Activo_fijo.Af_Nombre, dbo.Af_Retiro_Activo.ValorActivo, 
                  dbo.Af_Retiro_Activo.Valor_Tot_Bajas, dbo.Af_Retiro_Activo.Valor_Tot_Mejora, dbo.Af_Retiro_Activo.Valor_Depre_Acu, dbo.Af_Retiro_Activo.Valor_Neto, 
                  dbo.Af_Retiro_Activo.Fecha_Retiro, dbo.Af_Retiro_Activo.Estado, dbo.Af_Retiro_Activo.Concepto_Retiro, dbo.ct_cbtecble_det.IdCtaCble, dbo.ct_cbtecble_det.dc_Valor, 
                  CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ct_cbtecble_det.dc_Valor ELSE 0 END AS dc_Valor_Debe, CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Haber, 
                  dbo.ct_plancta.pc_Cuenta
FROM     Af_Activo_fijo INNER JOIN
                  Af_Retiro_Activo ON Af_Activo_fijo.IdEmpresa = Af_Retiro_Activo.IdEmpresa AND Af_Activo_fijo.IdActivoFijo = Af_Retiro_Activo.IdActivoFijo INNER JOIN
                  ct_plancta INNER JOIN
                  ct_cbtecble_det ON ct_plancta.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_plancta.IdCtaCble = ct_cbtecble_det.IdCtaCble ON Af_Retiro_Activo.IdEmpresa_ct = ct_cbtecble_det.IdEmpresa AND 
                  Af_Retiro_Activo.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND Af_Retiro_Activo.IdCbteCble = ct_cbtecble_det.IdCbteCble