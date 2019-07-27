CREATE view [web].[vwcp_orden_giro_det]
as
SELECT        cp_orden_giro_det.IdEmpresa, cp_orden_giro_det.IdCbteCble_Ogiro, cp_orden_giro_det.IdTipoCbte_Ogiro, cp_orden_giro_det.Secuencia, cp_orden_giro_det.IdProducto, cp_orden_giro_det.IdUnidadMedida, 
                         cp_orden_giro_det.Cantidad, cp_orden_giro_det.CostoUni, cp_orden_giro_det.PorDescuento, cp_orden_giro_det.DescuentoUni, cp_orden_giro_det.CostoUniFinal, cp_orden_giro_det.Subtotal, 
                         cp_orden_giro_det.IdCod_Impuesto_Iva, cp_orden_giro_det.PorIva, cp_orden_giro_det.ValorIva, cp_orden_giro_det.Total, cp_orden_giro_det.IdCtaCbleInv, 
                         in_Producto.pr_descripcion + ' ' + in_presentacion.nom_presentacion + ' ' + isnull(in_Producto.lote_num_lote, '') + ' ' + iif(in_Producto.lote_fecha_vcto IS NULL, '', CONVERT(VARCHAR(10), in_Producto.lote_fecha_vcto, 103)) 
                         pr_descripcion, cp_orden_giro_det.IdEmpresa_oc, cp_orden_giro_det.IdSucursal_oc, cp_orden_giro_det.IdOrdenCompra, cp_orden_giro_det.Secuencia_oc
FROM            in_presentacion INNER JOIN
                         in_Producto ON in_presentacion.IdEmpresa = in_Producto.IdEmpresa AND in_presentacion.IdPresentacion = in_Producto.IdPresentacion RIGHT OUTER JOIN
                         cp_orden_giro_det ON in_Producto.IdEmpresa = cp_orden_giro_det.IdEmpresa AND in_Producto.IdProducto = cp_orden_giro_det.IdProducto