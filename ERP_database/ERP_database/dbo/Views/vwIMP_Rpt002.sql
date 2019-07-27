CREATE VIEW [dbo].[vwIMP_Rpt002]
	AS 
	SELECT        dbo.imp_orden_compra_ext.IdEmpresa, dbo.imp_orden_compra_ext.IdOrdenCompra_ext, dbo.imp_orden_compra_ext.IdProveedor, dbo.imp_orden_compra_ext.IdPais_origen, dbo.imp_orden_compra_ext.IdPais_embarque, 
                         dbo.imp_orden_compra_ext.IdCiudad_destino, dbo.imp_orden_compra_ext.IdCatalogo_via, dbo.imp_orden_compra_ext.IdCatalogo_forma_pago, dbo.imp_orden_compra_ext.oe_fecha, 
                         dbo.imp_orden_compra_ext.oe_fecha_llegada_est, dbo.imp_orden_compra_ext.oe_fecha_embarque_est, dbo.imp_orden_compra_ext.oe_fecha_desaduanizacion_est, dbo.imp_orden_compra_ext.IdCtaCble_importacion, 
                         dbo.imp_orden_compra_ext.oe_observacion, dbo.imp_orden_compra_ext.oe_codigo, dbo.imp_orden_compra_ext.estado, dbo.imp_orden_compra_ext.oe_fecha_llegada, dbo.imp_orden_compra_ext.oe_fecha_embarque, 
                         dbo.imp_orden_compra_ext.oe_fecha_desaduanizacion, dbo.imp_orden_compra_ext.IdMoneda_origen, dbo.imp_orden_compra_ext.IdMoneda_destino, dbo.tb_pais.Nombre AS Paisembarque, tb_pais_1.Nombre AS PaisOrigen, 
                         dbo.imp_catalogo.ca_descripcion AS FormaPago, imp_catalogo_1.ca_descripcion AS ViaEmbarque, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_nombreCompleto, dbo.cp_proveedor.pr_codigo, 
                         dbo.in_Producto.pr_codigo AS Expr1, dbo.in_Producto.pr_descripcion, dbo.in_Producto.lote_fecha_fab, dbo.in_Producto.lote_fecha_vcto, dbo.in_Producto.lote_num_lote, dbo.imp_orden_compra_ext_det.od_cantidad, 
                         dbo.imp_orden_compra_ext_det.od_costo, dbo.imp_orden_compra_ext_det.od_por_descuento, dbo.imp_orden_compra_ext_det.od_descuento, dbo.imp_orden_compra_ext_det.od_costo_final, 
                         dbo.imp_orden_compra_ext_det.od_subtotal, dbo.imp_orden_compra_ext_det.od_cantidad_recepcion, dbo.imp_orden_compra_ext_det.od_costo_convertido, dbo.imp_orden_compra_ext_det.od_total_fob, 
                         dbo.imp_orden_compra_ext_det.od_factor_costo, dbo.imp_orden_compra_ext_det.od_costo_bodega, dbo.imp_orden_compra_ext_det.od_costo_total, dbo.imp_orden_compra_ext_det.IdUnidadMedida, 
                         dbo.tb_ciudad.Descripcion_Ciudad
FROM            dbo.imp_orden_compra_ext INNER JOIN
                         dbo.imp_orden_compra_ext_det ON dbo.imp_orden_compra_ext.IdEmpresa = dbo.imp_orden_compra_ext_det.IdEmpresa AND 
                         dbo.imp_orden_compra_ext.IdOrdenCompra_ext = dbo.imp_orden_compra_ext_det.IdOrdenCompra_ext INNER JOIN
                         dbo.cp_proveedor ON dbo.imp_orden_compra_ext.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.imp_orden_compra_ext.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                         dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.tb_pais ON dbo.imp_orden_compra_ext.IdPais_origen = dbo.tb_pais.IdPais INNER JOIN
                         dbo.tb_pais AS tb_pais_1 ON dbo.imp_orden_compra_ext.IdPais_embarque = tb_pais_1.IdPais INNER JOIN
                         dbo.imp_catalogo ON dbo.imp_orden_compra_ext.IdCatalogo_forma_pago = dbo.imp_catalogo.IdCatalogo INNER JOIN
                         dbo.imp_catalogo AS imp_catalogo_1 ON dbo.imp_orden_compra_ext.IdCatalogo_via = imp_catalogo_1.IdCatalogo INNER JOIN
                         dbo.in_Producto ON dbo.imp_orden_compra_ext_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.imp_orden_compra_ext_det.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                         dbo.tb_ciudad ON dbo.imp_orden_compra_ext.IdCiudad_destino = dbo.tb_ciudad.IdCiudad