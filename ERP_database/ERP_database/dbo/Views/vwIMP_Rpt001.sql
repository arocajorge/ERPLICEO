CREATE VIEW [dbo].[vwIMP_Rpt001]
	AS 

	SELECT        dbo.imp_orden_compra_ext.IdEmpresa, dbo.imp_orden_compra_ext.IdOrdenCompra_ext, dbo.tb_persona.pe_direccion, null pe_telefonoCasa, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, 
                         dbo.imp_catalogo.ca_descripcion, dbo.in_Producto.pr_descripcion, dbo.in_Producto.pr_codigo, dbo.tb_ciudad.Descripcion_Ciudad, dbo.imp_orden_compra_ext.oe_fecha_llegada_est, 
                         dbo.imp_orden_compra_ext.oe_fecha_embarque_est, dbo.imp_orden_compra_ext.oe_fecha_desaduanizacion_est, dbo.imp_orden_compra_ext.oe_observacion, dbo.imp_orden_compra_ext.oe_codigo, 
                          dbo.imp_orden_compra_ext.fecha_creacion, dbo.imp_orden_compra_ext.oe_fecha_llegada, 
                         dbo.imp_orden_compra_ext.oe_fecha_embarque, dbo.imp_orden_compra_ext.oe_fecha, dbo.imp_orden_compra_ext_det.od_cantidad, dbo.imp_orden_compra_ext_det.od_costo, 
                         dbo.imp_orden_compra_ext_det.od_por_descuento, dbo.imp_orden_compra_ext_det.od_descuento, dbo.imp_orden_compra_ext_det.od_costo_final, dbo.imp_orden_compra_ext_det.od_subtotal, 
                         dbo.imp_orden_compra_ext_det.od_cantidad_recepcion, dbo.imp_orden_compra_ext_det.od_costo_convertido, dbo.imp_orden_compra_ext_det.od_total_fob, dbo.imp_orden_compra_ext_det.od_factor_costo, 
                         dbo.imp_orden_compra_ext_det.od_costo_bodega, dbo.imp_orden_compra_ext_det.od_costo_total, paisOrigen.Nombre AS paisOrigen, paisEmbarque.Nombre AS paisEmbarque, dbo.tb_persona.pe_nombreCompleto
FROM            dbo.imp_orden_compra_ext INNER JOIN
                         dbo.imp_orden_compra_ext_det ON dbo.imp_orden_compra_ext.IdEmpresa = dbo.imp_orden_compra_ext_det.IdEmpresa AND 
                         dbo.imp_orden_compra_ext.IdOrdenCompra_ext = dbo.imp_orden_compra_ext_det.IdOrdenCompra_ext INNER JOIN
                         dbo.in_Producto ON dbo.imp_orden_compra_ext_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.imp_orden_compra_ext_det.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                         dbo.cp_proveedor ON dbo.imp_orden_compra_ext.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.imp_orden_compra_ext.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                         dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.tb_pais AS paisOrigen ON dbo.imp_orden_compra_ext.IdPais_origen = paisOrigen.IdPais INNER JOIN
                         dbo.tb_pais AS paisEmbarque ON dbo.imp_orden_compra_ext.IdPais_embarque = paisEmbarque.IdPais INNER JOIN
                         dbo.tb_ciudad ON dbo.imp_orden_compra_ext.IdCiudad_destino = dbo.tb_ciudad.IdCiudad INNER JOIN
                         dbo.imp_catalogo ON dbo.imp_catalogo.IdCatalogo = dbo.imp_catalogo.IdCatalogo