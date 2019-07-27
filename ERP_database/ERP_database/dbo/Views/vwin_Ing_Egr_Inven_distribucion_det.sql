CREATE VIEW [dbo].[vwin_Ing_Egr_Inven_distribucion_det]
AS
SELECT          ISNULL(ROW_NUMBER() OVER (ORDER BY dbo.in_Ing_Egr_Inven_distribucion.IdEmpresa), 0) AS IdRow, dbo.in_Ing_Egr_Inven_distribucion.IdEmpresa, dbo.in_Ing_Egr_Inven_distribucion.IdSucursal, dbo.in_Ing_Egr_Inven_distribucion.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven_distribucion.IdNumMovi, 
                         dbo.in_Producto.IdProducto_padre, dbo.in_Producto.IdProducto, dbo.in_Producto.pr_descripcion, dbo.in_Ing_Egr_Inven_det.IdUnidadMedida, ISNULL(ABS(dbo.in_Ing_Egr_Inven_det.dm_cantidad), 0) AS dm_cantidad, 
                         dbo.in_Ing_Egr_Inven_det.mv_costo, dbo.in_Producto.lote_fecha_vcto, dbo.in_Producto.lote_num_lote, dbo.in_Producto.lote_fecha_fab
FROM            dbo.in_Ing_Egr_Inven_distribucion INNER JOIN
                         dbo.in_Ing_Egr_Inven AS mov_dis ON dbo.in_Ing_Egr_Inven_distribucion.IdEmpresa_dis = mov_dis.IdEmpresa AND dbo.in_Ing_Egr_Inven_distribucion.IdSucursal_dis = mov_dis.IdSucursal AND 
                         dbo.in_Ing_Egr_Inven_distribucion.IdMovi_inven_tipo_dis = mov_dis.IdMovi_inven_tipo AND dbo.in_Ing_Egr_Inven_distribucion.IdNumMovi_dis = mov_dis.IdNumMovi INNER JOIN
                         dbo.in_Ing_Egr_Inven_det ON mov_dis.IdEmpresa = dbo.in_Ing_Egr_Inven_det.IdEmpresa AND mov_dis.IdSucursal = dbo.in_Ing_Egr_Inven_det.IdSucursal AND 
                         mov_dis.IdMovi_inven_tipo = dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo AND mov_dis.IdNumMovi = dbo.in_Ing_Egr_Inven_det.IdNumMovi INNER JOIN
                         dbo.in_Ing_Egr_Inven AS mov_x_distribuir ON dbo.in_Ing_Egr_Inven_distribucion.IdEmpresa = mov_x_distribuir.IdEmpresa AND dbo.in_Ing_Egr_Inven_distribucion.IdSucursal = mov_x_distribuir.IdSucursal AND 
                         dbo.in_Ing_Egr_Inven_distribucion.IdMovi_inven_tipo = mov_x_distribuir.IdMovi_inven_tipo AND dbo.in_Ing_Egr_Inven_distribucion.IdNumMovi = mov_x_distribuir.IdNumMovi AND 
                         dbo.in_Ing_Egr_Inven_distribucion.signo = mov_x_distribuir.signo INNER JOIN
                         dbo.in_Producto ON dbo.in_Ing_Egr_Inven_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_Ing_Egr_Inven_det.IdProducto = dbo.in_Producto.IdProducto