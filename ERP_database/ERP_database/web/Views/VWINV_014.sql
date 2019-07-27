create view web.VWINV_014 as
SELECT        dbo.in_Consignacion.IdEmpresa, dbo.in_Consignacion.IdConsignacion, dbo.in_Consignacion.IdSucursal, dbo.tb_sucursal.Su_Descripcion, dbo.in_Consignacion.IdBodega, dbo.tb_bodega.bo_Descripcion, 
                         dbo.in_Consignacion.Fecha, dbo.in_Consignacion.IdProveedor, dbo.cp_proveedor.IdPersona, dbo.cp_proveedor.pr_codigo, dbo.tb_persona.pe_nombreCompleto, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, 
                         dbo.in_Consignacion.Observacion, dbo.in_Consignacion.Estado, dbo.in_Consignacion.MotivoAnulacion, dbo.in_ConsignacionDet.Secuencia, dbo.in_ConsignacionDet.IdProducto, dbo.in_Producto.pr_descripcion, 
                         dbo.in_ConsignacionDet.IdUnidadMedida, dbo.in_ConsignacionDet.Cantidad, dbo.in_ConsignacionDet.Costo, dbo.in_ConsignacionDet.Observacion AS ObservacionDet
FROM            dbo.cp_proveedor INNER JOIN
                         dbo.in_Consignacion ON dbo.cp_proveedor.IdEmpresa = dbo.in_Consignacion.IdEmpresa AND dbo.cp_proveedor.IdProveedor = dbo.in_Consignacion.IdProveedor INNER JOIN
                         dbo.in_ConsignacionDet ON dbo.in_Consignacion.IdEmpresa = dbo.in_ConsignacionDet.IdEmpresa AND dbo.in_Consignacion.IdConsignacion = dbo.in_ConsignacionDet.IdConsignacion INNER JOIN
                         dbo.in_Producto ON dbo.in_ConsignacionDet.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_ConsignacionDet.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                         dbo.tb_bodega ON dbo.in_Consignacion.IdEmpresa = dbo.tb_bodega.IdEmpresa AND dbo.in_Consignacion.IdSucursal = dbo.tb_bodega.IdSucursal AND dbo.in_Consignacion.IdBodega = dbo.tb_bodega.IdBodega INNER JOIN
                         dbo.tb_empresa ON dbo.cp_proveedor.IdEmpresa = dbo.tb_empresa.IdEmpresa AND dbo.in_Consignacion.IdEmpresa = dbo.tb_empresa.IdEmpresa AND dbo.in_Producto.IdEmpresa = dbo.tb_empresa.IdEmpresa INNER JOIN
                         dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.tb_sucursal ON dbo.tb_bodega.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.tb_bodega.IdSucursal = dbo.tb_sucursal.IdSucursal AND dbo.tb_empresa.IdEmpresa = dbo.tb_sucursal.IdEmpresa