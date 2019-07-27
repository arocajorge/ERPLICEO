CREATE VIEW [dbo].[vwin_producto_x_tb_bodega_Costo_Historico]
AS
SELECT        dbo.in_producto_x_tb_bodega_Costo_Historico.IdEmpresa, dbo.in_producto_x_tb_bodega_Costo_Historico.IdSucursal, dbo.tb_sucursal.codigo AS cod_sucursal, 
                         dbo.tb_sucursal.Su_Descripcion AS nom_sucursal, dbo.in_producto_x_tb_bodega_Costo_Historico.IdBodega, dbo.tb_bodega.cod_bodega, 
                         dbo.tb_bodega.bo_Descripcion AS nom_bodega, dbo.in_producto_x_tb_bodega_Costo_Historico.IdProducto, dbo.in_Producto.pr_codigo AS cod_producto, 
                         dbo.in_Producto.pr_descripcion AS nom_producto, dbo.in_producto_x_tb_bodega_Costo_Historico.IdFecha, 
                         dbo.in_producto_x_tb_bodega_Costo_Historico.Secuencia, dbo.in_producto_x_tb_bodega_Costo_Historico.fecha, 
                         dbo.in_producto_x_tb_bodega_Costo_Historico.costo, dbo.in_producto_x_tb_bodega_Costo_Historico.Stock_a_la_fecha, 
                         dbo.in_producto_x_tb_bodega_Costo_Historico.Observacion, dbo.in_producto_x_tb_bodega_Costo_Historico.fecha_trans
FROM            dbo.in_Producto INNER JOIN
                         dbo.in_producto_x_tb_bodega_Costo_Historico INNER JOIN
                         dbo.tb_bodega INNER JOIN
                         dbo.tb_sucursal ON dbo.tb_bodega.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.tb_bodega.IdSucursal = dbo.tb_sucursal.IdSucursal ON 
                         dbo.in_producto_x_tb_bodega_Costo_Historico.IdEmpresa = dbo.tb_bodega.IdEmpresa AND 
                         dbo.in_producto_x_tb_bodega_Costo_Historico.IdSucursal = dbo.tb_bodega.IdSucursal AND 
                         dbo.in_producto_x_tb_bodega_Costo_Historico.IdBodega = dbo.tb_bodega.IdBodega ON 
                         dbo.in_Producto.IdProducto = dbo.in_producto_x_tb_bodega_Costo_Historico.IdProducto AND 
                         dbo.in_Producto.IdEmpresa = dbo.in_producto_x_tb_bodega_Costo_Historico.IdEmpresa