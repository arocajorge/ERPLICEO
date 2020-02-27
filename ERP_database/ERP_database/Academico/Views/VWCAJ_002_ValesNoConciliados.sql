create view academico.VWCAJ_002_ValesNoConciliados as
SELECT dbo.cp_conciliacion_Caja_ValesNoConciliados.IdEmpresa, dbo.cp_conciliacion_Caja_ValesNoConciliados.IdConciliacion_Caja, caj.IdTipocbte, caj.IdCbteCble, dbo.cp_conciliacion_Caja_ValesNoConciliados.Valor, per.pe_nombreCompleto, 
                  caj_cab.cm_observacion, caj_cab.cm_fecha, caj_cab.SecuenciaCaja
FROM     dbo.caj_Caja_Movimiento_det AS caj INNER JOIN
                  dbo.caj_Caja_Movimiento AS caj_cab ON caj_cab.IdEmpresa = caj.IdEmpresa AND caj_cab.IdTipocbte = caj.IdTipocbte AND caj_cab.IdCbteCble = caj.IdCbteCble INNER JOIN
                  dbo.tb_persona AS per ON caj_cab.IdPersona = per.IdPersona INNER JOIN
                  dbo.cp_conciliacion_Caja_ValesNoConciliados ON caj.IdEmpresa = dbo.cp_conciliacion_Caja_ValesNoConciliados.IdEmpresa_movcaja AND caj.IdTipocbte = dbo.cp_conciliacion_Caja_ValesNoConciliados.IdTipocbte_movcaja AND 
                  caj.IdCbteCble = dbo.cp_conciliacion_Caja_ValesNoConciliados.IdCbteCble_movcaja