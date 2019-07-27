CREATE VIEW vwin_Ajuste
AS
SELECT in_Ajuste.IdEmpresa, in_Ajuste.IdAjuste, in_Ajuste.IdSucursal, in_Ajuste.IdBodega, in_Ajuste.IdMovi_inven_tipo_ing, in_Ajuste.IdMovi_inven_tipo_egr, in_Ajuste.IdNumMovi_ing, in_Ajuste.IdNumMovi_egr, in_Ajuste.IdCatalogo_Estado, 
                  in_Ajuste.Estado, in_Ajuste.Fecha, in_Ajuste.Observacion, in_Catalogo.Nombre AS NombreEstado, tb_sucursal.Su_Descripcion, tb_bodega.bo_Descripcion
FROM     in_Ajuste INNER JOIN
                  tb_bodega ON in_Ajuste.IdEmpresa = tb_bodega.IdEmpresa AND in_Ajuste.IdEmpresa = tb_bodega.IdEmpresa AND in_Ajuste.IdSucursal = tb_bodega.IdSucursal AND in_Ajuste.IdSucursal = tb_bodega.IdSucursal AND 
                  in_Ajuste.IdBodega = tb_bodega.IdBodega AND in_Ajuste.IdBodega = tb_bodega.IdBodega INNER JOIN
                  tb_sucursal ON tb_bodega.IdEmpresa = tb_sucursal.IdEmpresa AND tb_bodega.IdSucursal = tb_sucursal.IdSucursal INNER JOIN
                  in_Catalogo ON in_Ajuste.IdCatalogo_Estado = in_Catalogo.IdCatalogo