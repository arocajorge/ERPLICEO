CREATE view [web].[vwin_Producto_Composicion]
as
SELECT dbo.in_Producto_Composicion.IdEmpresa, dbo.in_Producto_Composicion.IdProductoPadre, dbo.in_Producto_Composicion.IdProductoHijo, dbo.in_Producto_Composicion.IdUnidadMedida, dbo.in_Producto_Composicion.Cantidad, 
                  dbo.in_Producto.pr_descripcion, dbo.in_presentacion.nom_presentacion, dbo.in_categorias.ca_Categoria, dbo.in_Producto.lote_fecha_fab, dbo.in_Producto.lote_fecha_vcto, dbo.in_Producto.lote_num_lote, 
                  dbo.in_ProductoTipo.tp_ManejaInven, dbo.in_Producto.se_distribuye
FROM     dbo.in_Producto_Composicion INNER JOIN
                  dbo.in_Producto ON dbo.in_Producto_Composicion.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_Producto_Composicion.IdEmpresa = dbo.in_Producto.IdEmpresa AND 
                  dbo.in_Producto_Composicion.IdProductoHijo = dbo.in_Producto.IdProducto INNER JOIN
                  dbo.in_categorias ON dbo.in_Producto.IdEmpresa = dbo.in_categorias.IdEmpresa AND dbo.in_Producto.IdCategoria = dbo.in_categorias.IdCategoria INNER JOIN
                  dbo.in_presentacion ON dbo.in_Producto.IdEmpresa = dbo.in_presentacion.IdEmpresa AND dbo.in_Producto.IdPresentacion = dbo.in_presentacion.IdPresentacion INNER JOIN
                  dbo.in_ProductoTipo ON dbo.in_Producto.IdEmpresa = dbo.in_ProductoTipo.IdEmpresa AND dbo.in_Producto.IdProductoTipo = dbo.in_ProductoTipo.IdProductoTipo