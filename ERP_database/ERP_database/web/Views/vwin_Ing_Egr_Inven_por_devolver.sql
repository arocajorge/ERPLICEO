CREATE VIEW [web].[vwin_Ing_Egr_Inven_por_devolver]
AS
SELECT dbo.in_Ing_Egr_Inven.IdEmpresa, dbo.in_Ing_Egr_Inven.IdSucursal, dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven.IdNumMovi, dbo.in_Ing_Egr_Inven.signo, dbo.in_movi_inven_tipo.tm_descripcion, 
                  dbo.in_Ing_Egr_Inven.cm_observacion, dbo.in_Ing_Egr_Inven.cm_fecha, dbo.tb_sucursal.Su_Descripcion
FROM     dbo.in_movi_inven_tipo RIGHT OUTER JOIN
                  dbo.in_Ing_Egr_Inven ON dbo.in_movi_inven_tipo.IdEmpresa = dbo.in_Ing_Egr_Inven.IdEmpresa AND dbo.in_movi_inven_tipo.IdMovi_inven_tipo = dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo LEFT OUTER JOIN
                  dbo.tb_sucursal ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdSucursal = dbo.tb_sucursal.IdSucursal LEFT OUTER JOIN
                  dbo.in_Ing_Egr_Inven_det ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.in_Ing_Egr_Inven_det.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdSucursal = dbo.in_Ing_Egr_Inven_det.IdSucursal AND 
                  dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo = dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo AND dbo.in_Ing_Egr_Inven.IdNumMovi = dbo.in_Ing_Egr_Inven_det.IdNumMovi LEFT OUTER JOIN
                      (SELECT dbo.in_devolucion_inven_det.inv_IdEmpresa, dbo.in_devolucion_inven_det.inv_IdSucursal, dbo.in_devolucion_inven_det.inv_IdMovi_inven_tipo, dbo.in_devolucion_inven_det.inv_IdNumMovi, 
                                         SUM(dbo.in_devolucion_inven_det.cant_devuelta) AS cant_devuelta
                       FROM      dbo.in_devolucion_inven_det INNER JOIN
                                         dbo.in_devolucion_inven ON dbo.in_devolucion_inven_det.IdEmpresa = dbo.in_devolucion_inven.IdEmpresa AND dbo.in_devolucion_inven_det.IdDev_Inven = dbo.in_devolucion_inven.IdDev_Inven
                       WHERE   (dbo.in_devolucion_inven.Estado = 1)
                       GROUP BY dbo.in_devolucion_inven_det.inv_IdEmpresa, dbo.in_devolucion_inven_det.inv_IdSucursal, dbo.in_devolucion_inven_det.inv_IdMovi_inven_tipo, dbo.in_devolucion_inven_det.inv_IdNumMovi) AS DEV ON 
                  dbo.in_Ing_Egr_Inven.IdEmpresa = DEV.inv_IdEmpresa AND dbo.in_Ing_Egr_Inven.IdSucursal = DEV.inv_IdSucursal AND dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo = DEV.inv_IdMovi_inven_tipo AND 
                  dbo.in_Ing_Egr_Inven.IdNumMovi = DEV.inv_IdNumMovi
WHERE  (dbo.in_movi_inven_tipo.Estado = 'A') AND (dbo.in_Ing_Egr_Inven.Estado = 'A')
GROUP BY dbo.in_Ing_Egr_Inven.IdEmpresa, dbo.in_Ing_Egr_Inven.IdSucursal, dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven.IdNumMovi, dbo.in_Ing_Egr_Inven.signo, dbo.in_movi_inven_tipo.tm_descripcion, 
                  dbo.in_Ing_Egr_Inven.cm_observacion, dbo.in_Ing_Egr_Inven.cm_fecha, DEV.cant_devuelta, dbo.tb_sucursal.Su_Descripcion
HAVING (ROUND(SUM(dbo.in_Ing_Egr_Inven_det.dm_cantidad_sinConversion), 2) <> ROUND(ISNULL(DEV.cant_devuelta, 0), 2))