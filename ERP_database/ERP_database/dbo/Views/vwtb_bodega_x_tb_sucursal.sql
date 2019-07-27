CREATE VIEW vwtb_bodega_x_tb_sucursal
AS
SELECT        tb_bodega.IdEmpresa, tb_bodega.IdSucursal,tb_bodega.IdBodega, tb_sucursal.codigo cod_sucursal, tb_bodega.cod_bodega, 
                         tb_sucursal.Su_Descripcion,tb_bodega.bo_Descripcion
FROM            tb_sucursal INNER JOIN
                         tb_bodega ON tb_sucursal.IdEmpresa = tb_bodega.IdEmpresa AND tb_sucursal.IdSucursal = tb_bodega.IdSucursal