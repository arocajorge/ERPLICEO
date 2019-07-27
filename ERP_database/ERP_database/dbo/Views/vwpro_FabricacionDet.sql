CREATE VIEW [dbo].[vwpro_FabricacionDet]
AS
SELECT dbo.pro_FabricacionDet.IdEmpresa, dbo.pro_FabricacionDet.IdFabricacion, dbo.pro_FabricacionDet.Secuencia, dbo.pro_FabricacionDet.Signo, dbo.pro_FabricacionDet.IdProducto, dbo.pro_FabricacionDet.IdUnidadMedida, 
                  dbo.pro_FabricacionDet.Cantidad, dbo.pro_FabricacionDet.Costo, dbo.pro_FabricacionDet.RealizaMovimiento, dbo.in_Producto.pr_descripcion, dbo.in_ProductoTipo.tp_ManejaInven, dbo.in_Producto.se_distribuye
FROM     dbo.in_ProductoTipo INNER JOIN
                  dbo.in_Producto ON dbo.in_ProductoTipo.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_ProductoTipo.IdProductoTipo = dbo.in_Producto.IdProductoTipo RIGHT OUTER JOIN
                  dbo.pro_FabricacionDet ON dbo.in_Producto.IdEmpresa = dbo.pro_FabricacionDet.IdEmpresa AND dbo.in_Producto.IdProducto = dbo.pro_FabricacionDet.IdProducto