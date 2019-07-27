create view vwin_producto_Ult_Costo_Hist_x_Sucu
as
SELECT        IdEmpresa, IdSucursal, IdProducto, AVG(costo) AS costo
FROM            vwin_producto_Ult_Costo_Hist_x_Bod
GROUP BY IdEmpresa, IdSucursal, IdProducto