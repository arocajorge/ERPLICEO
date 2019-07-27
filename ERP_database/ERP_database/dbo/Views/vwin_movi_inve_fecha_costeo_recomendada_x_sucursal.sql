CREATE VIEW vwin_movi_inve_fecha_costeo_recomendada_x_sucursal
as
SELECT        in_movi_inve_detalle.IdEmpresa, in_movi_inve_detalle.IdSucursal, MIN(in_movi_inve.cm_fecha) fecha_sin_costear, 
                         ISNULL(in_movi_inve_detalle.Costeado,0) Costeado
FROM            in_movi_inve INNER JOIN
                         in_movi_inve_detalle ON in_movi_inve.IdEmpresa = in_movi_inve_detalle.IdEmpresa AND in_movi_inve.IdSucursal = in_movi_inve_detalle.IdSucursal AND 
                         in_movi_inve.IdBodega = in_movi_inve_detalle.IdBodega AND in_movi_inve.IdMovi_inven_tipo = in_movi_inve_detalle.IdMovi_inven_tipo AND 
                         in_movi_inve.IdNumMovi = in_movi_inve_detalle.IdNumMovi
WHERE ISNULL(in_movi_inve_detalle.Costeado,0) = 0
GROUP BY in_movi_inve_detalle.IdEmpresa, in_movi_inve_detalle.IdSucursal,  ISNULL(in_movi_inve_detalle.Costeado,0)