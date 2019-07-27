CREATE VIEW web.VWCXP_005
AS
SELECT dbo.cp_ConciliacionAnticipo.IdEmpresa, dbo.cp_ConciliacionAnticipo.IdConciliacion, dbo.cp_ConciliacionAnticipo.IdTipoCbte, dbo.cp_ConciliacionAnticipo.IdCbteCble, dbo.ct_cbtecble_det.secuencia, dbo.ct_cbtecble_det.IdCtaCble, 
                  dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.dc_Valor, CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ct_cbtecble_det.dc_Valor ELSE 0 END AS dc_Valor_Debe, 
                  CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Haber, dbo.cp_ConciliacionAnticipo.Fecha, dbo.cp_ConciliacionAnticipo.Observacion, dbo.cp_ConciliacionAnticipo.Estado, 
                  dbo.seg_usuario.Nombre AS NomUsuario, dbo.cp_ConciliacionAnticipo.IdSucursal, dbo.cp_ConciliacionAnticipo.IdProveedor, dbo.tb_persona.pe_nombreCompleto, dbo.tb_sucursal.Su_Descripcion
FROM     dbo.cp_ConciliacionAnticipo INNER JOIN
                  dbo.ct_plancta INNER JOIN
                  dbo.ct_cbtecble_det ON dbo.ct_plancta.IdEmpresa = dbo.ct_cbtecble_det.IdEmpresa AND dbo.ct_plancta.IdCtaCble = dbo.ct_cbtecble_det.IdCtaCble ON dbo.cp_ConciliacionAnticipo.IdEmpresa = dbo.ct_cbtecble_det.IdEmpresa AND 
                  dbo.cp_ConciliacionAnticipo.IdTipoCbte = dbo.ct_cbtecble_det.IdTipoCbte AND dbo.cp_ConciliacionAnticipo.IdCbteCble = dbo.ct_cbtecble_det.IdCbteCble INNER JOIN
                  dbo.tb_sucursal ON dbo.cp_ConciliacionAnticipo.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.cp_ConciliacionAnticipo.IdSucursal = dbo.tb_sucursal.IdSucursal INNER JOIN
                  dbo.cp_proveedor ON dbo.cp_ConciliacionAnticipo.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.cp_ConciliacionAnticipo.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                  dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona LEFT OUTER JOIN
                  dbo.seg_usuario ON dbo.cp_ConciliacionAnticipo.IdUsuarioCreacion = dbo.seg_usuario.IdUsuario