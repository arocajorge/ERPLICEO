CREATE VIEW vwin_transferencia_x_in_movi_inve_agrupada_para_recosteo
AS
SELECT        in_transferencia.IdEmpresa, in_transferencia.IdSucursalOrigen, tb_sucursal.codigo AS cod_sucursal, tb_sucursal.Su_Descripcion AS nom_sucursal, 
                         in_transferencia.IdBodegaOrigen, tb_bodega.cod_bodega, tb_bodega.bo_Descripcion AS nom_bodega, in_transferencia.tr_fecha
FROM            tb_bodega INNER JOIN
                         tb_sucursal ON tb_bodega.IdEmpresa = tb_sucursal.IdEmpresa AND tb_bodega.IdSucursal = tb_sucursal.IdSucursal INNER JOIN
                         in_movi_inve_detalle AS in_movi_inve_detalle_1 INNER JOIN
                         in_Ing_Egr_Inven_det INNER JOIN
                         in_Ing_Egr_Inven_det AS in_Ing_Egr_Inven_det_1 INNER JOIN
                         in_transferencia ON in_Ing_Egr_Inven_det_1.IdEmpresa = in_transferencia.IdEmpresa_Ing_Egr_Inven_Destino AND 
                         in_Ing_Egr_Inven_det_1.IdSucursal = in_transferencia.IdSucursal_Ing_Egr_Inven_Destino AND 
                         in_Ing_Egr_Inven_det_1.IdMovi_inven_tipo = in_transferencia.IdMovi_inven_tipo_SucuDest AND 
                         in_Ing_Egr_Inven_det_1.IdNumMovi = in_transferencia.IdNumMovi_Ing_Egr_Inven_Destino ON 
                         in_Ing_Egr_Inven_det.IdEmpresa = in_transferencia.IdEmpresa_Ing_Egr_Inven_Origen AND 
                         in_Ing_Egr_Inven_det.IdSucursal = in_transferencia.IdSucursal_Ing_Egr_Inven_Origen AND 
                         in_Ing_Egr_Inven_det.IdMovi_inven_tipo = in_transferencia.IdMovi_inven_tipo_SucuOrig AND 
                         in_Ing_Egr_Inven_det.IdNumMovi = in_transferencia.IdNumMovi_Ing_Egr_Inven_Origen INNER JOIN
                         in_movi_inve_detalle ON in_Ing_Egr_Inven_det.IdEmpresa_inv = in_movi_inve_detalle.IdEmpresa AND 
                         in_Ing_Egr_Inven_det.IdSucursal_inv = in_movi_inve_detalle.IdSucursal AND in_Ing_Egr_Inven_det.IdBodega_inv = in_movi_inve_detalle.IdBodega AND 
                         in_Ing_Egr_Inven_det.IdMovi_inven_tipo_inv = in_movi_inve_detalle.IdMovi_inven_tipo AND 
                         in_Ing_Egr_Inven_det.IdNumMovi_inv = in_movi_inve_detalle.IdNumMovi AND in_Ing_Egr_Inven_det.secuencia_inv = in_movi_inve_detalle.Secuencia ON 
                         in_movi_inve_detalle_1.IdEmpresa = in_Ing_Egr_Inven_det_1.IdEmpresa_inv AND in_movi_inve_detalle_1.IdSucursal = in_Ing_Egr_Inven_det_1.IdSucursal_inv AND
                          in_movi_inve_detalle_1.IdBodega = in_Ing_Egr_Inven_det_1.IdBodega_inv AND 
                         in_movi_inve_detalle_1.IdMovi_inven_tipo = in_Ing_Egr_Inven_det_1.IdMovi_inven_tipo_inv AND 
                         in_movi_inve_detalle_1.IdNumMovi = in_Ing_Egr_Inven_det_1.IdNumMovi_inv AND in_movi_inve_detalle_1.Secuencia = in_Ing_Egr_Inven_det_1.secuencia_inv ON
                          tb_bodega.IdEmpresa = in_transferencia.IdEmpresa AND tb_bodega.IdSucursal = in_transferencia.IdSucursalOrigen AND 
                         tb_bodega.IdBodega = in_transferencia.IdBodegaOrigen
GROUP BY in_transferencia.IdEmpresa, in_transferencia.IdSucursalOrigen, in_transferencia.IdBodegaOrigen, in_transferencia.tr_fecha, tb_bodega.cod_bodega, 
                         tb_bodega.bo_Descripcion, tb_sucursal.codigo, tb_sucursal.Su_Descripcion