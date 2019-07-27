CREATE VIEW web.VWACTF_002
AS
SELECT dbo.Af_Venta_Activo.IdEmpresa, dbo.Af_Venta_Activo.IdVtaActivo, dbo.Af_Venta_Activo.IdActivoFijo, dbo.Af_Activo_fijo.Af_Nombre, dbo.Af_Venta_Activo.ValorActivo, 
                  dbo.Af_Venta_Activo.Valor_Tot_Bajas, dbo.Af_Venta_Activo.Valor_Tot_Mejora, dbo.Af_Venta_Activo.Valor_Depre_Acu, dbo.Af_Venta_Activo.Valor_Neto, dbo.Af_Venta_Activo.Valor_Venta, 
                  dbo.Af_Venta_Activo.Fecha_Venta, dbo.Af_Venta_Activo.Estado, dbo.Af_Venta_Activo.Concepto_Vta, dbo.Af_Venta_Activo.NumComprobante, dbo.ct_cbtecble_det.IdCtaCble, dbo.ct_cbtecble_det.dc_Valor, 
                  CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ct_cbtecble_det.dc_Valor ELSE 0 END AS dc_Valor_Debe, CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Haber, 
                  dbo.ct_plancta.pc_Cuenta
FROM     Af_Venta_Activo INNER JOIN
                  Af_Activo_fijo ON Af_Venta_Activo.IdEmpresa = Af_Activo_fijo.IdEmpresa AND Af_Venta_Activo.IdActivoFijo = Af_Activo_fijo.IdActivoFijo INNER JOIN
                  ct_plancta INNER JOIN
                  ct_cbtecble_det ON ct_plancta.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_plancta.IdCtaCble = ct_cbtecble_det.IdCtaCble ON Af_Venta_Activo.IdEmpresa_ct = ct_cbtecble_det.IdEmpresa AND 
                  Af_Venta_Activo.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND Af_Venta_Activo.IdCbteCble = ct_cbtecble_det.IdCbteCble