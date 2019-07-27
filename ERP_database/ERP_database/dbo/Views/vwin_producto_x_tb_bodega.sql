CREATE VIEW dbo.vwin_producto_x_tb_bodega
AS
SELECT dbo.in_producto_x_tb_bodega.IdEmpresa, dbo.in_producto_x_tb_bodega.IdSucursal, dbo.in_producto_x_tb_bodega.IdBodega, dbo.in_producto_x_tb_bodega.IdProducto, dbo.in_producto_x_tb_bodega.Stock_minimo, 
                  dbo.in_Producto.pr_descripcion, dbo.tb_bodega.bo_Descripcion, dbo.tb_sucursal.Su_Descripcion
FROM     dbo.in_producto_x_tb_bodega INNER JOIN
                  dbo.in_Producto ON dbo.in_producto_x_tb_bodega.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_producto_x_tb_bodega.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                  dbo.tb_sucursal INNER JOIN
                  dbo.tb_bodega ON dbo.tb_sucursal.IdEmpresa = dbo.tb_bodega.IdEmpresa AND dbo.tb_sucursal.IdSucursal = dbo.tb_bodega.IdSucursal ON dbo.in_producto_x_tb_bodega.IdEmpresa = dbo.tb_bodega.IdEmpresa AND 
                  dbo.in_producto_x_tb_bodega.IdSucursal = dbo.tb_bodega.IdSucursal AND dbo.in_producto_x_tb_bodega.IdBodega = dbo.tb_bodega.IdBodega