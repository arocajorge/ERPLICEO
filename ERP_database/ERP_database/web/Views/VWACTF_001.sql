CREATE VIEW web.VWACTF_001
AS
SELECT Af_Mej_Baj_Activo.IdEmpresa, Af_Mej_Baj_Activo.Id_Mejora_Baja_Activo, Af_Mej_Baj_Activo.Id_Tipo, Af_Mej_Baj_Activo.IdActivoFijo, Af_Activo_fijo.Af_Nombre, Af_Mej_Baj_Activo.ValorActivo, Af_Mej_Baj_Activo.Valor_Tot_Bajas, 
                  Af_Mej_Baj_Activo.Valor_Tot_Mejora, Af_Mej_Baj_Activo.Valor_Depre_Acu, Af_Mej_Baj_Activo.Valor_Neto, Af_Mej_Baj_Activo.Valor_Mej_Baj_Activo, Af_Mej_Baj_Activo.Fecha_MejBaj, Af_Mej_Baj_Activo.Estado, 
                  Af_Mej_Baj_Activo.Motivo, ct_cbtecble_det.IdCtaCble, ct_cbtecble_det.dc_Valor, CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ct_cbtecble_det.dc_Valor ELSE 0 END dc_Valor_Debe,
				  CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END dc_Valor_Haber,
				  ct_plancta.pc_Cuenta
FROM     Af_Activo_fijo INNER JOIN
                  Af_Mej_Baj_Activo ON Af_Activo_fijo.IdEmpresa = Af_Mej_Baj_Activo.IdEmpresa AND Af_Activo_fijo.IdActivoFijo = Af_Mej_Baj_Activo.IdActivoFijo INNER JOIN
                  ct_cbtecble_det ON Af_Mej_Baj_Activo.IdEmpresa_ct = ct_cbtecble_det.IdEmpresa AND Af_Mej_Baj_Activo.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND Af_Mej_Baj_Activo.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
                  ct_plancta ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble