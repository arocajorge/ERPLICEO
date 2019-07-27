CREATE VIEW vwin_ConsignacionDet
AS
SELECT in_ConsignacionDet.IdEmpresa, in_ConsignacionDet.IdConsignacion, in_ConsignacionDet.Secuencia, in_ConsignacionDet.IdProducto, in_ConsignacionDet.IdUnidadMedida, in_ConsignacionDet.Cantidad, in_ConsignacionDet.Costo, 
                  in_ConsignacionDet.Observacion, in_Producto.pr_descripcion
FROM     in_ConsignacionDet LEFT OUTER JOIN
                  in_Producto ON in_ConsignacionDet.IdEmpresa = in_Producto.IdEmpresa AND in_ConsignacionDet.IdProducto = in_Producto.IdProducto