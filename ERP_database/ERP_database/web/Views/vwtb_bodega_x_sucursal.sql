CREATE VIEW web.vwtb_bodega_x_sucursal
AS
SELECT dbo.tb_bodega.IdEmpresa, dbo.tb_bodega.IdSucursal, dbo.tb_bodega.IdBodega, dbo.tb_bodega.cod_bodega, dbo.tb_bodega.bo_Descripcion, dbo.tb_bodega.cod_punto_emision, dbo.tb_bodega.bo_manejaFacturacion, 
                  dbo.tb_sucursal.codigo, dbo.tb_sucursal.Su_Descripcion, dbo.tb_sucursal.Estado
FROM     dbo.tb_sucursal INNER JOIN
                  dbo.tb_bodega ON dbo.tb_sucursal.IdEmpresa = dbo.tb_bodega.IdEmpresa AND dbo.tb_sucursal.IdSucursal = dbo.tb_bodega.IdSucursal
WHERE  (dbo.tb_bodega.Estado = 'A') AND (dbo.tb_sucursal.Estado = 'A')

GO



GO


