CREATE VIEW dbo.vwimp_orden_compra_ext_ct_cbteble_det_gastos
AS

SELECT        dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdEmpresa, dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdOrdenCompra_ext, dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdEmpresa_ct, 
                         dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdTipoCbte, dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdCbteCble, dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.secuencia_ct, 
                         dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdGasto_tipo, dbo.ct_cbtecble_det.dc_Valor, dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.dc_Observacion, dbo.imp_gasto_x_ct_plancta.IdCtaCble
FROM            dbo.imp_gasto_x_ct_plancta INNER JOIN
                         dbo.imp_gasto ON dbo.imp_gasto_x_ct_plancta.IdGasto_tipo = dbo.imp_gasto.IdGasto_tipo INNER JOIN
                         dbo.ct_cbtecble_det INNER JOIN
                         dbo.imp_orden_compra_ext_ct_cbteble_det_gastos ON dbo.ct_cbtecble_det.IdEmpresa = dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdEmpresa_ct AND 
                         dbo.ct_cbtecble_det.IdTipoCbte = dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdTipoCbte AND dbo.ct_cbtecble_det.IdCbteCble = dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdCbteCble AND 
                         dbo.ct_cbtecble_det.secuencia = dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.secuencia_ct INNER JOIN
                         dbo.ct_plancta ON dbo.ct_cbtecble_det.IdEmpresa = dbo.ct_plancta.IdEmpresa AND dbo.ct_cbtecble_det.IdCtaCble = dbo.ct_plancta.IdCtaCble ON 
                         dbo.imp_gasto_x_ct_plancta.IdEmpresa = dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdEmpresa AND dbo.imp_gasto_x_ct_plancta.IdGasto_tipo = dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdGasto_tipo