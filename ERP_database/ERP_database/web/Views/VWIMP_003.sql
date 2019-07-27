CREATE VIEW web.VWIMP_003
AS
SELECT        imp_orden_compra_ext_det.IdEmpresa, imp_orden_compra_ext_det.IdOrdenCompra_ext, imp_orden_compra_ext_det.Secuencia, CASE WHEN ProdHijo.IdProducto_padre IS NULL 
                         THEN prodHijo.IdProducto ELSE prodPadre.IdProducto END AS IdProducto, ProdHijo.pr_descripcion, ProdHijo.IdPresentacion, in_presentacion.nom_presentacion, ProdHijo.IdMarca, in_Marca.Descripcion AS NomMarca, 
                         imp_orden_compra_ext.IdProveedor, tb_persona.pe_nombreCompleto, imp_orden_compra_ext.IdPais_embarque, tb_pais.Nombre AS NomPais, imp_orden_compra_ext.oe_observacion, 
                         imp_orden_compra_ext_det.od_cantidad_recepcion, imp_orden_compra_ext_det.od_costo_final, imp_orden_compra_ext_det.od_total_fob, imp_orden_compra_ext_det.od_costo_bodega, 
                         imp_orden_compra_ext_det.od_costo_total, imp_orden_compra_ext_det.od_por_descuento, imp_orden_compra_ext_det.od_costo, imp_orden_compra_ext.oe_fecha
FROM            tb_persona INNER JOIN
                         imp_orden_compra_ext INNER JOIN
                         imp_orden_compra_ext_det ON imp_orden_compra_ext.IdEmpresa = imp_orden_compra_ext_det.IdEmpresa AND imp_orden_compra_ext.IdOrdenCompra_ext = imp_orden_compra_ext_det.IdOrdenCompra_ext INNER JOIN
                         in_Producto AS ProdHijo ON imp_orden_compra_ext_det.IdEmpresa = ProdHijo.IdEmpresa AND imp_orden_compra_ext_det.IdProducto = ProdHijo.IdProducto INNER JOIN
                         cp_proveedor ON imp_orden_compra_ext.IdEmpresa = cp_proveedor.IdEmpresa AND imp_orden_compra_ext.IdProveedor = cp_proveedor.IdProveedor ON tb_persona.IdPersona = cp_proveedor.IdPersona INNER JOIN
                         tb_pais ON imp_orden_compra_ext.IdPais_embarque = tb_pais.IdPais LEFT OUTER JOIN
                         in_presentacion ON ProdHijo.IdEmpresa = in_presentacion.IdEmpresa AND ProdHijo.IdPresentacion = in_presentacion.IdPresentacion LEFT OUTER JOIN
                         in_Marca ON ProdHijo.IdEmpresa = in_Marca.IdEmpresa AND ProdHijo.IdMarca = in_Marca.IdMarca LEFT OUTER JOIN
                         in_Producto AS ProdPadre ON ProdHijo.IdEmpresa = ProdPadre.IdEmpresa AND ProdHijo.IdProducto_padre = ProdPadre.IdProducto
where imp_orden_compra_ext.estado = 1