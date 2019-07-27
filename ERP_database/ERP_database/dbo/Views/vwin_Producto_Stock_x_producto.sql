CREATE view vwin_Producto_Stock_x_producto
as
SELECT        IdEmpresa, IdProducto, SUM(dm_cantidad) AS Stock
FROM            dbo.in_movi_inve_detalle
GROUP BY IdEmpresa, IdProducto