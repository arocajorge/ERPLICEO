CREATE VIEW [dbo].[vwin_devolucion_inven_det]
AS
SELECT        dbo.in_devolucion_inven_det.IdEmpresa, dbo.in_devolucion_inven_det.IdDev_Inven, dbo.in_devolucion_inven_det.secuencia, dbo.in_devolucion_inven_det.inv_IdEmpresa, dbo.in_devolucion_inven_det.inv_IdSucursal, 
                         dbo.in_devolucion_inven_det.inv_IdMovi_inven_tipo, dbo.in_devolucion_inven_det.inv_IdNumMovi, dbo.in_devolucion_inven_det.inv_Secuencia, dbo.in_devolucion_inven_det.cant_devuelta, dbo.in_Producto.pr_descripcion, 
                         dbo.in_presentacion.nom_presentacion, isnull( dbo.in_Ing_Egr_Inven_det.dm_cantidad_sinConversion,0)dm_cantidad_sinConversion, dbo.in_Producto.lote_fecha_vcto, dbo.in_Producto.lote_num_lote, isnull(dbo.in_Ing_Egr_Inven_det.IdBodega,0)IdBodega, 
                         dbo.in_UnidadMedida.Descripcion AS NomUnidad, isnull(dbo.in_Ing_Egr_Inven_det.mv_costo_sinConversion,0)mv_costo_sinConversion, dbo.in_Ing_Egr_Inven_det.IdUnidadMedida_sinConversion, ISNULL(dbo.in_Ing_Egr_Inven_det.IdProducto,0) IdProducto
FROM            dbo.in_UnidadMedida INNER JOIN
                         dbo.in_Ing_Egr_Inven_det ON dbo.in_UnidadMedida.IdUnidadMedida = dbo.in_Ing_Egr_Inven_det.IdUnidadMedida_sinConversion LEFT OUTER JOIN
                         dbo.in_Producto INNER JOIN
                         dbo.in_presentacion ON dbo.in_Producto.IdEmpresa = dbo.in_presentacion.IdEmpresa AND dbo.in_Producto.IdPresentacion = dbo.in_presentacion.IdPresentacion ON 
                         dbo.in_Ing_Egr_Inven_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_Ing_Egr_Inven_det.IdProducto = dbo.in_Producto.IdProducto RIGHT OUTER JOIN
                         dbo.in_devolucion_inven_det ON dbo.in_Ing_Egr_Inven_det.IdEmpresa = dbo.in_devolucion_inven_det.inv_IdEmpresa AND dbo.in_Ing_Egr_Inven_det.IdSucursal = dbo.in_devolucion_inven_det.inv_IdSucursal AND 
                         dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo = dbo.in_devolucion_inven_det.inv_IdMovi_inven_tipo AND dbo.in_Ing_Egr_Inven_det.IdNumMovi = dbo.in_devolucion_inven_det.inv_IdNumMovi AND 
                         dbo.in_Ing_Egr_Inven_det.Secuencia = dbo.in_devolucion_inven_det.inv_Secuencia