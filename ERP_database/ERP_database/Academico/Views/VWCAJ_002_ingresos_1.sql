create view academico.VWCAJ_002_ingresos as
SELECT conci.IdEmpresa, conci.IdConciliacion_Caja, caj.IdTipocbte, caj.IdCbteCble, conci.valor_disponible, conci.valor_aplicado, caj.cr_Valor, per.pe_nombreCompleto, caj_cab.cm_observacion, caj_cab.cm_fecha, caj_cab.SecuenciaCaja
FROM     dbo.cp_conciliacion_Caja_det_Ing_Caja AS conci INNER JOIN
                  dbo.caj_Caja_Movimiento_det AS caj ON caj.IdEmpresa = conci.IdEmpresa AND caj.IdTipocbte = conci.IdTipocbte_movcaj AND caj.IdCbteCble = conci.IdCbteCble_movcaj INNER JOIN
                  dbo.caj_Caja_Movimiento AS caj_cab ON caj_cab.IdEmpresa = caj.IdEmpresa AND caj_cab.IdTipocbte = caj.IdTipocbte AND caj_cab.IdCbteCble = caj.IdCbteCble INNER JOIN
                  dbo.tb_persona AS per ON caj_cab.IdPersona = per.IdPersona