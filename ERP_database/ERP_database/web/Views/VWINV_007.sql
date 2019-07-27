CREATE VIEW web.VWINV_007
AS
SELECT dbo.in_transferencia_det.IdEmpresa, dbo.in_transferencia_det.IdSucursalOrigen, dbo.in_transferencia_det.IdBodegaOrigen, dbo.in_transferencia_det.IdTransferencia, dbo.in_transferencia_det.dt_secuencia, 
                  dbo.in_transferencia_det.IdProducto, dbo.in_Producto.pr_codigo, dbo.in_Producto.pr_descripcion, dbo.in_transferencia_det.dt_cantidad, dbo.in_transferencia_det.IdUnidadMedida, 
                  dbo.in_UnidadMedida.Descripcion AS nom_unidad_medida, suc_origen.codigo AS cod_sucursal_origen, suc_origen.Su_Descripcion AS nom_sucursal_origen, bod_origen.cod_bodega AS cod_bodega_origen, 
                  bod_origen.bo_Descripcion AS nom_bodega_origen, suc_destino.codigo AS cod_sucursal_destino, suc_destino.Su_Descripcion AS nom_sucursal_destino, bod_destino.cod_bodega AS cod_bodega_destino, 
                  bod_destino.bo_Descripcion AS nom_bodega_destino, dbo.in_transferencia.tr_fecha, dbo.in_transferencia.tr_Observacion, dbo.in_transferencia.Estado, dbo.in_transferencia.Codigo
FROM     dbo.in_transferencia INNER JOIN
                  dbo.in_transferencia_det ON dbo.in_transferencia.IdEmpresa = dbo.in_transferencia_det.IdEmpresa AND dbo.in_transferencia.IdSucursalOrigen = dbo.in_transferencia_det.IdSucursalOrigen AND 
                  dbo.in_transferencia.IdBodegaOrigen = dbo.in_transferencia_det.IdBodegaOrigen AND dbo.in_transferencia.IdTransferencia = dbo.in_transferencia_det.IdTransferencia INNER JOIN
                  dbo.tb_bodega AS bod_origen ON dbo.in_transferencia.IdEmpresa = bod_origen.IdEmpresa AND dbo.in_transferencia.IdSucursalOrigen = bod_origen.IdSucursal AND 
                  dbo.in_transferencia.IdBodegaOrigen = bod_origen.IdBodega INNER JOIN
                  dbo.tb_sucursal AS suc_origen ON bod_origen.IdEmpresa = suc_origen.IdEmpresa AND bod_origen.IdSucursal = suc_origen.IdSucursal INNER JOIN
                  dbo.tb_bodega AS bod_destino ON dbo.in_transferencia.IdEmpresa = bod_destino.IdEmpresa AND dbo.in_transferencia.IdSucursalDest = bod_destino.IdSucursal AND 
                  dbo.in_transferencia.IdBodegaDest = bod_destino.IdBodega INNER JOIN
                  dbo.tb_sucursal AS suc_destino ON bod_destino.IdEmpresa = suc_destino.IdEmpresa AND bod_destino.IdSucursal = suc_destino.IdSucursal INNER JOIN
                  dbo.in_UnidadMedida ON dbo.in_transferencia_det.IdUnidadMedida = dbo.in_UnidadMedida.IdUnidadMedida INNER JOIN
                  dbo.in_Producto ON dbo.in_transferencia_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_transferencia_det.IdProducto = dbo.in_Producto.IdProducto
