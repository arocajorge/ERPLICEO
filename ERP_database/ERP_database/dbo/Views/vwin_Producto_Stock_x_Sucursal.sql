CREATE view vwin_Producto_Stock_x_Sucursal
as
SELECT        IdEmpresa, IdSucursal, IdProducto, SUM(dm_cantidad) AS Stock
FROM            dbo.in_movi_inve_detalle
GROUP BY IdEmpresa, IdSucursal,  IdProducto