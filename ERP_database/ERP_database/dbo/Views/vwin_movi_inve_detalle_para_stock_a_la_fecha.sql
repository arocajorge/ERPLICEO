CREATE VIEW [dbo].[vwin_movi_inve_detalle_para_stock_a_la_fecha]
AS
select cab.IdEmpresa,cab.IdSucursal,det.IdBodega ,cab.IdMovi_inven_tipo,cab.IdNumMovi, 
det.Secuencia, cab.cm_fecha,det.IdProducto,  det.dm_cantidad 
from in_movi_inve cab inner join in_movi_inve_detalle det 
on cab.IdEmpresa = det.IdEmpresa 
and cab.IdSucursal = det.IdSucursal
and cab.IdBodega = det.IdBodega
and cab.IdMovi_inven_tipo = det.IdMovi_inven_tipo 
and cab.IdNumMovi = det.IdNumMovi