CREATE VIEW vwpro_Fabricacion
AS
SELECT        pro_Fabricacion.IdEmpresa, pro_Fabricacion.IdFabricacion, pro_Fabricacion.egr_IdSucursal, pro_Fabricacion.egr_IdBodega, pro_Fabricacion.egr_IdMovi_inven_tipo, pro_Fabricacion.egr_IdNumMovi, 
                         pro_Fabricacion.ing_IdSucursal, pro_Fabricacion.ing_IdBodega, pro_Fabricacion.ing_IdMovi_inven_tipo, pro_Fabricacion.ing_IdNumMovi, pro_Fabricacion.Fecha, pro_Fabricacion.Observacion, pro_Fabricacion.Estado, 
                         tb_sucursal.Su_Descripcion
FROM            pro_Fabricacion LEFT OUTER JOIN
                         tb_sucursal ON pro_Fabricacion.ing_IdSucursal = tb_sucursal.IdSucursal AND pro_Fabricacion.IdEmpresa = tb_sucursal.IdEmpresa