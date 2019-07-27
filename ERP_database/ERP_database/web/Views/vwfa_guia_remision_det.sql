create view web.vwfa_guia_remision_det as

SELECT        dbo.fa_guia_remision_det.IdEmpresa, dbo.fa_guia_remision_det.IdSucursal, dbo.fa_guia_remision_det.IdBodega, dbo.fa_guia_remision_det.Secuencia, dbo.fa_guia_remision_det.IdProducto, 
                         dbo.fa_guia_remision_det.gi_cantidad, dbo.fa_guia_remision_det.gi_detallexItems, dbo.in_Producto.pr_descripcion, dbo.in_Producto.pr_descripcion_2, dbo.in_Producto.pr_codigo, dbo.in_Producto.lote_fecha_fab, 
                         dbo.in_Producto.lote_fecha_vcto, dbo.in_Producto.lote_num_lote, dbo.in_categorias.ca_Categoria, dbo.in_presentacion.nom_presentacion, dbo.fa_guia_remision_det.IdGuiaRemision, 
                         dbo.fa_guia_remision_det_x_factura.IdEmpresa_fact, dbo.fa_guia_remision_det_x_factura.IdSucursal_fact, dbo.fa_guia_remision_det_x_factura.IdBodega_fact, dbo.fa_guia_remision_det_x_factura.IdCbteVta_fact, 
                         dbo.fa_guia_remision_det_x_factura.Secuencia_fact
FROM            dbo.in_Producto INNER JOIN
                         dbo.fa_guia_remision_det ON dbo.in_Producto.IdEmpresa = dbo.fa_guia_remision_det.IdEmpresa AND dbo.in_Producto.IdProducto = dbo.fa_guia_remision_det.IdProducto INNER JOIN
                         dbo.in_categorias ON dbo.in_Producto.IdEmpresa = dbo.in_categorias.IdEmpresa AND dbo.in_Producto.IdCategoria = dbo.in_categorias.IdCategoria INNER JOIN
                         dbo.in_presentacion ON dbo.in_Producto.IdEmpresa = dbo.in_presentacion.IdEmpresa AND dbo.in_Producto.IdPresentacion = dbo.in_presentacion.IdPresentacion LEFT OUTER JOIN
                         dbo.fa_guia_remision_det_x_factura ON dbo.fa_guia_remision_det.IdEmpresa = dbo.fa_guia_remision_det_x_factura.IdEmpresa_guia AND 
                         dbo.fa_guia_remision_det.IdSucursal = dbo.fa_guia_remision_det_x_factura.IdSucursal_guia AND dbo.fa_guia_remision_det.IdBodega = dbo.fa_guia_remision_det_x_factura.IdBodega_guia AND 
                         dbo.fa_guia_remision_det.IdGuiaRemision = dbo.fa_guia_remision_det_x_factura.IdGuiaRemision_guia AND dbo.fa_guia_remision_det.Secuencia = dbo.fa_guia_remision_det_x_factura.Secuencia_guia