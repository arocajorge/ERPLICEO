CREATE VIEW [web].[vwin_Ing_Egr_Inven_det]
AS
SELECT dbo.in_Ing_Egr_Inven_det.IdEmpresa, dbo.in_Ing_Egr_Inven_det.IdSucursal, dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven_det.IdNumMovi, dbo.in_Ing_Egr_Inven_det.Secuencia, dbo.in_Ing_Egr_Inven_det.IdBodega, 
                  dbo.in_Ing_Egr_Inven_det.IdProducto, dbo.in_Ing_Egr_Inven_det.dm_cantidad, dbo.in_Ing_Egr_Inven_det.dm_observacion, dbo.in_Ing_Egr_Inven_det.mv_costo, dbo.in_Ing_Egr_Inven_det.IdUnidadMedida, dbo.in_Ing_Egr_Inven_det.IdEmpresa_oc, dbo.in_Ing_Egr_Inven_det.IdSucursal_oc, 
                  dbo.in_Ing_Egr_Inven_det.IdOrdenCompra, dbo.in_Ing_Egr_Inven_det.Secuencia_oc, dbo.in_Ing_Egr_Inven_det.IdEmpresa_inv, 
                  dbo.in_Ing_Egr_Inven_det.IdSucursal_inv, dbo.in_Ing_Egr_Inven_det.IdBodega_inv, dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo_inv, dbo.in_Ing_Egr_Inven_det.IdNumMovi_inv, dbo.in_Ing_Egr_Inven_det.secuencia_inv, 
                  dbo.in_Ing_Egr_Inven_det.dm_cantidad_sinConversion, dbo.in_Ing_Egr_Inven_det.IdUnidadMedida_sinConversion, dbo.in_Ing_Egr_Inven_det.mv_costo_sinConversion, 
                  dbo.in_Ing_Egr_Inven_det.IdMotivo_Inv, dbo.in_Producto.pr_descripcion, dbo.in_presentacion.nom_presentacion, dbo.in_Producto.lote_fecha_vcto, dbo.in_Producto.lote_num_lote, dbo.in_ProductoTipo.tp_ManejaInven, 
                  dbo.in_Producto.se_distribuye
FROM     dbo.in_presentacion INNER JOIN
                  dbo.in_Producto ON dbo.in_presentacion.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_presentacion.IdPresentacion = dbo.in_Producto.IdPresentacion INNER JOIN
                  dbo.in_ProductoTipo ON dbo.in_Producto.IdEmpresa = dbo.in_ProductoTipo.IdEmpresa AND dbo.in_Producto.IdProductoTipo = dbo.in_ProductoTipo.IdProductoTipo RIGHT OUTER JOIN
                  dbo.in_Ing_Egr_Inven_det ON dbo.in_Producto.IdEmpresa = dbo.in_Ing_Egr_Inven_det.IdEmpresa AND dbo.in_Producto.IdProducto = dbo.in_Ing_Egr_Inven_det.IdProducto