create view academico.VWBAN_003 as

SELECT dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan, dbo.ba_Cbte_Ban.IdEmpresa, dbo.ba_Cbte_Ban.IdCbteCble, dbo.ba_Cbte_Ban.IdTipocbte, dbo.ba_Cbte_Ban.IdBanco, dbo.ba_Banco_Cuenta.ba_descripcion, dbo.ba_Cbte_Ban.cb_Fecha, 
                  dbo.ba_Cbte_Ban.cb_Observacion, dbo.ba_Cbte_Ban.Estado, dbo.ba_Cbte_Ban.IdTipoNota, dbo.tb_persona.pe_nombreCompleto, dbo.ct_cbtecble_det.IdCtaCble, dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.dc_Valor, 
                  CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ct_cbtecble_det.dc_Valor ELSE 0 END AS dc_Valor_Debe, CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Haber, 
                  dbo.ba_Cbte_Ban.cb_Cheque, dbo.ba_Cbte_Ban.cb_giradoA, dbo.seg_usuario.Nombre AS NombreUsuario, dbo.tb_sucursal.Su_Descripcion
FROM     dbo.ba_Cbte_Ban_tipo INNER JOIN
                  dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo ON dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan = dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.CodTipoCbteBan INNER JOIN
                  dbo.ba_Cbte_Ban ON dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdEmpresa = dbo.ba_Cbte_Ban.IdEmpresa AND dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdTipoCbteCble = dbo.ba_Cbte_Ban.IdTipocbte INNER JOIN
                  dbo.ba_Banco_Cuenta ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ba_Banco_Cuenta.IdEmpresa AND dbo.ba_Cbte_Ban.IdBanco = dbo.ba_Banco_Cuenta.IdBanco INNER JOIN
                  dbo.ct_cbtecble_det ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ct_cbtecble_det.IdEmpresa AND dbo.ba_Cbte_Ban.IdTipocbte = dbo.ct_cbtecble_det.IdTipoCbte AND 
                  dbo.ba_Cbte_Ban.IdCbteCble = dbo.ct_cbtecble_det.IdCbteCble INNER JOIN
                  dbo.ct_plancta ON dbo.ct_cbtecble_det.IdCtaCble = dbo.ct_plancta.IdCtaCble AND dbo.ct_cbtecble_det.IdEmpresa = dbo.ct_plancta.IdEmpresa LEFT OUTER JOIN
                  dbo.tb_sucursal ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ba_Cbte_Ban.IdSucursal = dbo.tb_sucursal.IdSucursal LEFT OUTER JOIN
                  dbo.tb_persona INNER JOIN
                  dbo.cp_orden_pago ON dbo.tb_persona.IdPersona = dbo.cp_orden_pago.IdPersona INNER JOIN
                  dbo.cp_orden_pago_cancelaciones ON dbo.cp_orden_pago.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_op AND dbo.cp_orden_pago.IdOrdenPago = dbo.cp_orden_pago_cancelaciones.IdOrdenPago_op ON 
                  dbo.ba_Cbte_Ban.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_pago AND dbo.ba_Cbte_Ban.IdCbteCble = dbo.cp_orden_pago_cancelaciones.IdCbteCble_pago AND 
                  dbo.ba_Cbte_Ban.IdTipocbte = dbo.cp_orden_pago_cancelaciones.IdTipoCbte_pago LEFT OUTER JOIN
                  dbo.seg_usuario ON dbo.ba_Cbte_Ban.IdUsuario = dbo.seg_usuario.IdUsuario
WHERE  (dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan = 'CHEQ')
GROUP BY dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan, dbo.ba_Cbte_Ban.IdEmpresa, dbo.ba_Cbte_Ban.IdCbteCble, dbo.ba_Cbte_Ban.IdTipocbte, dbo.ba_Cbte_Ban.IdBanco, dbo.ba_Banco_Cuenta.ba_descripcion, dbo.ba_Cbte_Ban.cb_Fecha, 
                  dbo.ba_Cbte_Ban.cb_Observacion, dbo.ba_Cbte_Ban.Estado, dbo.ba_Cbte_Ban.IdTipoNota, dbo.tb_persona.pe_nombreCompleto, dbo.ct_cbtecble_det.IdCtaCble, dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.dc_Valor, 
                  dbo.ba_Cbte_Ban.cb_Cheque, dbo.ba_Cbte_Ban.cb_giradoA, dbo.ct_cbtecble_det.secuencia, dbo.seg_usuario.Nombre, dbo.tb_sucursal.Su_Descripcion