create view academico.VWCAJ_001 as
SELECT dbo.ct_cbtecble_det.IdEmpresa, dbo.ct_cbtecble_det.IdTipoCbte, dbo.ct_cbtecble_det.IdCbteCble, dbo.ct_cbtecble_det.secuencia, dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.dc_Valor, 
                  CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ct_cbtecble_det.dc_Valor ELSE 0 END AS dc_Valor_Debe, CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Haber, 
                  dbo.cxc_cobro_tipo.tc_descripcion, dbo.caj_Caja_Movimiento_det.cr_Valor, dbo.caj_Caja_Movimiento.cm_Signo, dbo.caj_Caja_Movimiento.IdTipoMovi, dbo.caj_Caja_Movimiento_Tipo.tm_descripcion, 
                  dbo.caj_Caja_Movimiento.cm_observacion, dbo.caj_Caja_Movimiento.IdCaja, dbo.caj_Caja.ca_Descripcion, dbo.caj_Caja_Movimiento.cm_fecha, dbo.caj_Caja_Movimiento.Estado, dbo.ct_cbtecble_tipo.tc_TipoCbte, 
                  dbo.ct_cbtecble_det.IdCtaCble, dbo.tb_persona.pe_nombreCompleto, dbo.seg_usuario.Nombre, dbo.tb_sucursal.Su_Descripcion, ISNULL(dbo.caj_Caja_Movimiento.SecuenciaCaja, 0) AS SecuenciaCaja
FROM     dbo.tb_sucursal INNER JOIN
                  dbo.ct_cbtecble ON dbo.tb_sucursal.IdEmpresa = dbo.ct_cbtecble.IdEmpresa AND dbo.tb_sucursal.IdSucursal = dbo.ct_cbtecble.IdSucursal RIGHT OUTER JOIN
                  dbo.caj_Caja_Movimiento INNER JOIN
                  dbo.caj_Caja_Movimiento_det ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.caj_Caja_Movimiento_det.IdEmpresa AND dbo.caj_Caja_Movimiento.IdCbteCble = dbo.caj_Caja_Movimiento_det.IdCbteCble AND 
                  dbo.caj_Caja_Movimiento.IdTipocbte = dbo.caj_Caja_Movimiento_det.IdTipocbte INNER JOIN
                  dbo.caj_Caja ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.caj_Caja.IdEmpresa AND dbo.caj_Caja_Movimiento.IdCaja = dbo.caj_Caja.IdCaja INNER JOIN
                  dbo.ct_cbtecble_det ON dbo.caj_Caja_Movimiento_det.IdEmpresa = dbo.ct_cbtecble_det.IdEmpresa AND dbo.caj_Caja_Movimiento_det.IdTipocbte = dbo.ct_cbtecble_det.IdTipoCbte AND 
                  dbo.caj_Caja_Movimiento_det.IdCbteCble = dbo.ct_cbtecble_det.IdCbteCble INNER JOIN
                  dbo.ct_plancta ON dbo.ct_cbtecble_det.IdEmpresa = dbo.ct_plancta.IdEmpresa AND dbo.ct_cbtecble_det.IdCtaCble = dbo.ct_plancta.IdCtaCble INNER JOIN
                  dbo.cxc_cobro_tipo ON dbo.caj_Caja_Movimiento_det.IdCobro_tipo = dbo.cxc_cobro_tipo.IdCobro_tipo INNER JOIN
                  dbo.caj_Caja_Movimiento_Tipo ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.caj_Caja_Movimiento_Tipo.IdEmpresa AND dbo.caj_Caja_Movimiento.IdTipoMovi = dbo.caj_Caja_Movimiento_Tipo.IdTipoMovi INNER JOIN
                  dbo.ct_cbtecble_tipo ON dbo.ct_cbtecble_det.IdEmpresa = dbo.ct_cbtecble_tipo.IdEmpresa AND dbo.ct_cbtecble_det.IdTipoCbte = dbo.ct_cbtecble_tipo.IdTipoCbte INNER JOIN
                  dbo.tb_persona ON dbo.caj_Caja_Movimiento.IdPersona = dbo.tb_persona.IdPersona ON dbo.ct_cbtecble.IdEmpresa = dbo.caj_Caja_Movimiento.IdEmpresa AND dbo.ct_cbtecble.IdTipoCbte = dbo.caj_Caja_Movimiento.IdTipocbte AND 
                  dbo.ct_cbtecble.IdCbteCble = dbo.caj_Caja_Movimiento.IdCbteCble LEFT OUTER JOIN
                  dbo.seg_usuario ON dbo.caj_Caja_Movimiento.IdUsuario = dbo.seg_usuario.IdUsuario