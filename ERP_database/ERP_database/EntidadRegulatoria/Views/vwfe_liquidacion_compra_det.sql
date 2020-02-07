CREATE VIEW EntidadRegulatoria.vwfe_liquidacion_compra_det
AS
SELECT cp_orden_giro_det.IdEmpresa, cp_orden_giro_det.IdCbteCble_Ogiro, cp_orden_giro_det.IdTipoCbte_Ogiro, Secuencia, cp_orden_giro_det.IdProducto, in_Producto.IdUnidadMedida, Cantidad, CostoUni, PorDescuento, DescuentoUni, 
                  CostoUniFinal, Subtotal, cp_orden_giro_det.IdCod_Impuesto_Iva, PorIva, cast(dbo.BankersRounding(ValorIva,2) as float) ValorIva, cast(dbo.BankersRounding(Subtotal + ValorIva,2) as float) Total, pr_codigo, pr_codigo2, pr_descripcion, pr_descripcion_2, IdSucursal, IdBodega, IdSucursal_cxp
FROM     dbo.cp_orden_giro INNER JOIN
                  dbo.cp_orden_giro_det ON dbo.cp_orden_giro.IdEmpresa = dbo.cp_orden_giro_det.IdEmpresa AND dbo.cp_orden_giro.IdCbteCble_Ogiro = dbo.cp_orden_giro_det.IdCbteCble_Ogiro AND 
                  dbo.cp_orden_giro.IdTipoCbte_Ogiro = dbo.cp_orden_giro_det.IdTipoCbte_Ogiro INNER JOIN
                  dbo.in_Producto ON dbo.cp_orden_giro_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.cp_orden_giro_det.IdProducto = dbo.in_Producto.IdProducto
UNION ALL
SELECT cp_orden_giro_det_ing_x_oc.IdEmpresa, cp_orden_giro_det_ing_x_oc.IdCbteCble_Ogiro, cp_orden_giro_det_ing_x_oc.IdTipoCbte_Ogiro, Secuencia, cp_orden_giro_det_ing_x_oc.IdProducto, in_Producto.IdUnidadMedida, dm_cantidad, 
                  cp_orden_giro_det_ing_x_oc.do_precioCompra, cp_orden_giro_det_ing_x_oc.do_porc_des, cp_orden_giro_det_ing_x_oc.do_descuento, cp_orden_giro_det_ing_x_oc.do_precioFinal, cp_orden_giro_det_ing_x_oc.do_subtotal, 
                  cp_orden_giro_det_ing_x_oc.IdCod_Impuesto, cp_orden_giro_det_ing_x_oc.Por_Iva, cp_orden_giro_det_ing_x_oc.do_iva, cp_orden_giro_det_ing_x_oc.do_total, pr_codigo, pr_codigo2, pr_descripcion, pr_descripcion_2, IdSucursal, 
                  IdBodega, IdSucursal_cxp
FROM     dbo.cp_orden_giro INNER JOIN
                  dbo.cp_orden_giro_det_ing_x_oc ON dbo.cp_orden_giro.IdEmpresa = dbo.cp_orden_giro_det_ing_x_oc.IdEmpresa AND dbo.cp_orden_giro.IdCbteCble_Ogiro = dbo.cp_orden_giro_det_ing_x_oc.IdCbteCble_Ogiro AND 
                  dbo.cp_orden_giro.IdTipoCbte_Ogiro = dbo.cp_orden_giro_det_ing_x_oc.IdTipoCbte_Ogiro INNER JOIN
                  dbo.in_Producto ON dbo.cp_orden_giro_det_ing_x_oc.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.cp_orden_giro_det_ing_x_oc.IdProducto = dbo.in_Producto.IdProducto
UNION ALL
SELECT cp_orden_giro_det_ing_x_os.IdEmpresa, cp_orden_giro_det_ing_x_os.IdCbteCble_Ogiro, cp_orden_giro_det_ing_x_os.IdTipoCbte_Ogiro, Secuencia, cp_orden_giro_det_ing_x_os.IdProducto, in_Producto.IdUnidadMedida, dm_cantidad, 
                  cp_orden_giro_det_ing_x_os.do_precioCompra, cp_orden_giro_det_ing_x_os.do_porc_des, cp_orden_giro_det_ing_x_os.do_descuento, cp_orden_giro_det_ing_x_os.do_precioFinal, cp_orden_giro_det_ing_x_os.do_subtotal, 
                  cp_orden_giro_det_ing_x_os.IdCod_Impuesto, cp_orden_giro_det_ing_x_os.Por_Iva, cp_orden_giro_det_ing_x_os.do_iva, cp_orden_giro_det_ing_x_os.do_total, pr_codigo, pr_codigo2, pr_descripcion, pr_descripcion_2, IdSucursal, 
                  IdBodega, IdSucursal_cxp
FROM     dbo.cp_orden_giro INNER JOIN
                  dbo.cp_orden_giro_det_ing_x_os ON dbo.cp_orden_giro.IdEmpresa = dbo.cp_orden_giro_det_ing_x_os.IdEmpresa AND dbo.cp_orden_giro.IdCbteCble_Ogiro = dbo.cp_orden_giro_det_ing_x_os.IdCbteCble_Ogiro AND 
                  dbo.cp_orden_giro.IdTipoCbte_Ogiro = dbo.cp_orden_giro_det_ing_x_os.IdTipoCbte_Ogiro INNER JOIN
                  dbo.in_Producto ON dbo.cp_orden_giro_det_ing_x_os.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.cp_orden_giro_det_ing_x_os.IdProducto = dbo.in_Producto.IdProducto