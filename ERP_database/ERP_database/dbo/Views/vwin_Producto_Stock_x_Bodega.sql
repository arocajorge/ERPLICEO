CREATE view vwin_Producto_Stock_x_Bodega
as
SELECT        IdEmpresa, IdSucursal,IdBodega, IdProducto, SUM(dm_cantidad) AS Stock
FROM            dbo.in_movi_inve_detalle
GROUP BY IdEmpresa, IdSucursal, IdBodega, IdProducto