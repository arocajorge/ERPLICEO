CREATE VIEW vwin_Consignacion
AS
SELECT in_Consignacion.IdEmpresa, in_Consignacion.IdConsignacion, in_Consignacion.IdSucursal, in_Consignacion.IdBodega, in_Consignacion.Fecha, in_Consignacion.IdProveedor, in_Consignacion.Observacion, in_Consignacion.Estado, 
                  in_Consignacion.IdNumMovi, tb_sucursal.Su_Descripcion, tb_bodega.bo_Descripcion, in_movi_inven_tipo.tm_descripcion as NombreTipoMovimiento, tb_persona.pe_nombreCompleto as NombreProveedor
FROM     cp_proveedor INNER JOIN
                  tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona RIGHT OUTER JOIN
                  in_Consignacion LEFT OUTER JOIN
                  in_movi_inven_tipo ON in_consignacion.IdMovi_inven_tipo = in_movi_inven_tipo.IdMovi_inven_tipo AND in_consignacion.IdEmpresa = in_movi_inven_tipo.IdEmpresa ON cp_proveedor.IdEmpresa = in_consignacion.IdEmpresa AND 
                  cp_proveedor.IdProveedor = in_consignacion.IdProveedor LEFT OUTER JOIN
                  tb_sucursal INNER JOIN
                  tb_bodega ON tb_sucursal.IdEmpresa = tb_bodega.IdEmpresa AND tb_sucursal.IdSucursal = tb_bodega.IdSucursal ON in_consignacion.IdEmpresa = tb_bodega.IdEmpresa AND in_consignacion.IdSucursal = tb_bodega.IdSucursal AND 
                  in_consignacion.IdBodega = tb_bodega.IdBodega
GO



GO


