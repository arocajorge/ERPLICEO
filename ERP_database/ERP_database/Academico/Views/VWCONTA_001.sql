CREATE VIEW ACADEMICO.VWCONTA_001 AS
SELECT dbo.ct_cbtecble_det.IdEmpresa, dbo.ct_cbtecble_det.IdTipoCbte, dbo.ct_cbtecble_det.IdCbteCble, dbo.ct_cbtecble_det.secuencia, dbo.ct_cbtecble.cb_Fecha, dbo.ct_cbtecble.cb_Observacion, dbo.ct_cbtecble.cb_Estado, 
                  dbo.ct_cbtecble_det.IdCtaCble, dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.dc_Valor, CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Debe, 
                  CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Haber, dbo.ct_cbtecble_tipo.tc_TipoCbte, dbo.ct_cbtecble_det.dc_Observacion, dbo.tb_sucursal.Su_Descripcion, 
                  dbo.seg_usuario.Nombre AS NombreUsuario
FROM     dbo.ct_cbtecble INNER JOIN
                  dbo.ct_cbtecble_det ON dbo.ct_cbtecble.IdEmpresa = dbo.ct_cbtecble_det.IdEmpresa AND dbo.ct_cbtecble.IdTipoCbte = dbo.ct_cbtecble_det.IdTipoCbte AND dbo.ct_cbtecble.IdCbteCble = dbo.ct_cbtecble_det.IdCbteCble INNER JOIN
                  dbo.ct_plancta ON dbo.ct_cbtecble_det.IdEmpresa = dbo.ct_plancta.IdEmpresa AND dbo.ct_cbtecble_det.IdCtaCble = dbo.ct_plancta.IdCtaCble INNER JOIN
                  dbo.ct_cbtecble_tipo ON dbo.ct_cbtecble.IdEmpresa = dbo.ct_cbtecble_tipo.IdEmpresa AND dbo.ct_cbtecble.IdTipoCbte = dbo.ct_cbtecble_tipo.IdTipoCbte INNER JOIN
                  dbo.tb_sucursal ON dbo.ct_cbtecble.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ct_cbtecble.IdSucursal = dbo.tb_sucursal.IdSucursal LEFT OUTER JOIN
                  dbo.seg_usuario ON dbo.ct_cbtecble.IdUsuario = dbo.seg_usuario.IdUsuario