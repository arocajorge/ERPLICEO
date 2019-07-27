
CREATE PROCEDURE [dbo].[spsys_actualizar_costo_ingresos]
AS
update in_movi_inve_detalle set mv_costo = a.Costo, mv_costo_sinConversion = a.Costo
from (
select * from tbsys_ProductosConCosto0 as f
) a where in_movi_inve_detalle.IdEmpresa = a.IdEmpresa
and in_movi_inve_detalle.IdProducto = a.IdProducto

update in_Ing_Egr_Inven_det set mv_costo = a.Costo, mv_costo_sinConversion = a.Costo
from (
select * from tbsys_ProductosConCosto0 as f
) a where in_Ing_Egr_Inven_det.IdEmpresa = a.IdEmpresa
and in_Ing_Egr_Inven_det.IdProducto = a.IdProducto