CREATE VIEW [dbo].[vwin_producto_x_tb_bodega_stock_x_lote]
AS
SELECT        dbo.in_producto_x_tb_bodega.IdEmpresa, dbo.in_producto_x_tb_bodega.IdSucursal, dbo.in_producto_x_tb_bodega.IdBodega, dbo.in_producto_x_tb_bodega.IdProducto, p_hijo.pr_codigo, p_hijo.pr_descripcion + ' '+pre.nom_presentacion pr_descripcion, 
                         p_hijo.IdProducto_padre, p_hijo.lote_fecha_fab, p_hijo.lote_fecha_vcto, p_hijo.lote_num_lote, ISNULL(SUM(mov.dm_cantidad), 0) AS stock
FROM            dbo.in_producto_x_tb_bodega INNER JOIN
                         dbo.in_Producto AS p_hijo ON dbo.in_producto_x_tb_bodega.IdEmpresa = p_hijo.IdEmpresa AND dbo.in_producto_x_tb_bodega.IdProducto = p_hijo.IdProducto INNER JOIN
                         dbo.in_Producto AS p_padre ON p_hijo.IdEmpresa = p_padre.IdEmpresa AND p_hijo.IdProducto_padre = p_padre.IdProducto LEFT OUTER JOIN
                             (SELECT        IdEmpresa, IdSucursal, IdBodega, IdProducto, dm_cantidad
                               FROM            dbo.in_movi_inve_detalle) AS mov ON dbo.in_producto_x_tb_bodega.IdProducto = mov.IdProducto AND dbo.in_producto_x_tb_bodega.IdBodega = mov.IdBodega AND 
                         dbo.in_producto_x_tb_bodega.IdSucursal = mov.IdSucursal AND mov.IdEmpresa = dbo.in_producto_x_tb_bodega.IdEmpresa
						 left join dbo.in_presentacion as pre on p_hijo.IdEmpresa = pre.IdEmpresa and p_hijo.IdPresentacion = pre.IdPresentacion
WHERE        (p_hijo.Estado = 'A') AND p_padre.Estado = 'A' AND (p_hijo.lote_fecha_vcto > CAST(GETDATE() AS date)) AND NOT EXISTS
                             (SELECT        IdEmpresa
                               FROM            in_Producto_Composicion comp
                               WHERE        dbo.in_producto_x_tb_bodega.IdEmpresa = comp.IdEmpresa AND dbo.in_producto_x_tb_bodega.IdProducto = comp.IdProductoHijo) AND NOT EXISTS
                             (SELECT        IdEmpresa
                               FROM            in_Producto_Composicion comp
                               WHERE        dbo.in_producto_x_tb_bodega.IdEmpresa = comp.IdEmpresa AND dbo.in_producto_x_tb_bodega.IdProducto = comp.IdProductoPadre)
GROUP BY dbo.in_producto_x_tb_bodega.IdEmpresa, dbo.in_producto_x_tb_bodega.IdSucursal, dbo.in_producto_x_tb_bodega.IdBodega, dbo.in_producto_x_tb_bodega.IdProducto, p_hijo.pr_codigo, p_hijo.pr_descripcion, pre.nom_presentacion, 
                         p_hijo.IdProducto_padre, p_hijo.lote_fecha_fab, p_hijo.lote_fecha_vcto, p_hijo.lote_num_lote
UNION ALL
SELECT        A.IdEmpresa, A.IdSucursal, A.IdBodega, A.IdProducto_combo, A.pr_codigo, A.pr_descripcion, A.IdProducto_padre, A.lote_fecha_fab, A.lote_fecha_vcto, A.lote_num_lote, ISNULL(MIN(A.cantidad), 0) 
                         AS cantidad
FROM            (SELECT        dbo.in_movi_inve_detalle.IdEmpresa, dbo.in_movi_inve_detalle.IdSucursal, dbo.in_movi_inve_detalle.IdBodega, dbo.in_Producto.IdProducto, in_Producto_1.pr_codigo, 
                                                    in_Producto_1.pr_descripcion +' ' + pre.nom_presentacion pr_descripcion, in_Producto_1.IdProducto_padre, in_Producto_1.lote_fecha_fab, in_Producto_1.lote_fecha_vcto, in_Producto_1.lote_num_lote, 
                                                    SUM(dbo.in_movi_inve_detalle.dm_cantidad) AS cantidad, in_Producto_Composicion.IdProductoPadre AS IdProducto_combo
                          FROM            dbo.in_movi_inve_detalle INNER JOIN
                                                    dbo.in_Producto ON dbo.in_movi_inve_detalle.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_movi_inve_detalle.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                                                    dbo.in_Producto_Composicion ON dbo.in_Producto.IdEmpresa = dbo.in_Producto_Composicion.IdEmpresa AND 
                                                    dbo.in_Producto.IdProducto = dbo.in_Producto_Composicion.IdProductoHijo INNER JOIN
                                                    dbo.in_Producto AS in_Producto_1 ON dbo.in_Producto_Composicion.IdEmpresa = in_Producto_1.IdEmpresa AND dbo.in_Producto_Composicion.IdProductoPadre = in_Producto_1.IdProducto
													left join in_presentacion as pre on in_Producto_1.IdEmpresa = pre.IdEmpresa and in_Producto_1.IdPresentacion = pre.IdPresentacion
                          WHERE        in_producto.Estado = 'A' AND in_Producto_1.Estado = 'A'
                          GROUP BY dbo.in_movi_inve_detalle.IdEmpresa, dbo.in_movi_inve_detalle.IdSucursal, dbo.in_movi_inve_detalle.IdBodega, dbo.in_Producto.IdProducto, in_Producto_1.pr_codigo, in_Producto_1.pr_descripcion, pre.nom_presentacion,
                                                     in_Producto_1.IdProducto_padre, in_Producto_1.lote_fecha_fab, in_Producto_1.lote_fecha_vcto, in_Producto_1.lote_num_lote, in_Producto_Composicion.IdProductoPadre) A
GROUP BY A.IdEmpresa, A.IdSucursal, A.IdBodega, A.IdProducto_combo, A.pr_codigo, A.pr_descripcion , A.IdProducto_padre, A.lote_fecha_fab, A.lote_fecha_vcto, A.lote_num_lote