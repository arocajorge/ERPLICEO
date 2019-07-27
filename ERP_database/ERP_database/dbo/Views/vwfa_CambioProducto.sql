CREATE VIEW vwfa_CambioProducto
AS
SELECT fa_CambioProducto.IdEmpresa, fa_CambioProducto.IdSucursal, fa_CambioProducto.IdBodega, fa_CambioProducto.IdCambio, fa_CambioProducto.Fecha, fa_CambioProducto.Observacion, fa_CambioProducto.Estado, 
                  fa_CambioProducto.IdMovi_inven_tipo, fa_CambioProducto.IdNumMovi, tb_sucursal.Su_Descripcion, tb_bodega.bo_Descripcion
FROM     tb_bodega INNER JOIN
                  tb_sucursal ON tb_bodega.IdEmpresa = tb_sucursal.IdEmpresa AND tb_bodega.IdSucursal = tb_sucursal.IdSucursal RIGHT OUTER JOIN
                  fa_CambioProducto ON tb_bodega.IdEmpresa = fa_CambioProducto.IdEmpresa AND tb_bodega.IdSucursal = fa_CambioProducto.IdSucursal AND tb_bodega.IdBodega = fa_CambioProducto.IdBodega