--[dbo].[vwin_in_Producto_x_tb_bodega_x_UnidadMedida]
--[dbo].[vwin_producto]
--create  VIEW [dbo].[vwin_Producto_a_Cortar_CusCider]


CREATE VIEW [dbo].[vwin_Producto_Marca_Tipo_Categoria]
AS
SELECT        dbo.in_Producto.IdEmpresa, dbo.in_Producto.IdProducto, dbo.in_Producto.pr_codigo, dbo.in_Producto.pr_codigo_barra, dbo.in_Producto.IdProductoTipo, 
                         dbo.in_Producto.IdPresentacion, dbo.in_Producto.IdCategoria, dbo.in_Producto.pr_descripcion, dbo.in_Producto.pr_observacion, dbo.in_Producto.IdUnidadMedida, 
                         0 pr_precio_publico,
                         0 pr_stock, 0 pr_pedidos, dbo.in_Producto.IdMarca, 
                         0 pr_stock_minimo, dbo.in_Producto.IdUsuario, dbo.in_Producto.Fecha_Transac, dbo.in_Producto.IdUsuarioUltMod, dbo.in_Producto.Fecha_UltMod, 
                         dbo.in_Producto.IdUsuarioUltAnu, dbo.in_Producto.Fecha_UltAnu, dbo.in_Producto.pr_motivoAnulacion, dbo.in_Producto.nom_pc, dbo.in_Producto.ip, 
                         dbo.in_Producto.Estado, dbo.in_Producto.pr_descripcion_2, 
                         
                         dbo.in_Producto.IdLinea, dbo.in_Producto.IdGrupo, dbo.in_Producto.IdSubGrupo,  dbo.in_Marca.Descripcion, 
                         dbo.in_categorias.ca_Categoria, dbo.in_ProductoTipo.tp_descripcion, dbo.in_Producto.IdUnidadMedida_Consumo
FROM            dbo.in_Producto INNER JOIN
                         dbo.in_Marca ON dbo.in_Producto.IdEmpresa = dbo.in_Marca.IdEmpresa 
						 AND dbo.in_Producto.IdMarca = dbo.in_Marca.IdMarca INNER JOIN
                         dbo.in_ProductoTipo ON dbo.in_Producto.IdEmpresa = dbo.in_ProductoTipo.IdEmpresa AND 
                         dbo.in_Producto.IdProductoTipo = dbo.in_ProductoTipo.IdProductoTipo INNER JOIN
                         dbo.in_categorias ON dbo.in_Producto.IdEmpresa = dbo.in_categorias.IdEmpresa AND dbo.in_Producto.IdCategoria = dbo.in_categorias.IdCategoria