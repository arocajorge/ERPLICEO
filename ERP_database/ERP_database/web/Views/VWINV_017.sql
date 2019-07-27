create view web.VWINV_017 AS
SELECT        dbo.in_Ing_Egr_Inven_det.IdEmpresa, dbo.in_Ing_Egr_Inven_det.IdSucursal, dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven_det.IdNumMovi, dbo.in_Ing_Egr_Inven_det.Secuencia, 
                         dbo.in_Ing_Egr_Inven_det.IdProducto, dbo.in_Producto.pr_descripcion, dbo.in_Producto.pr_codigo, dbo.tb_sucursal.Su_Descripcion, dbo.tb_bodega.bo_Descripcion, dbo.in_Ing_Egr_Inven_det.IdUnidadMedida_sinConversion, 
                         dbo.in_UnidadMedida.Descripcion, dbo.in_Ing_Egr_Inven_det.dm_cantidad_sinConversion, dbo.in_Ing_Egr_Inven_det.mv_costo_sinConversion, dbo.in_Ing_Egr_Inven.cm_observacion, dbo.in_Ing_Egr_Inven.CodMoviInven, 
                         dbo.in_Ing_Egr_Inven.cm_fecha, dbo.in_Ing_Egr_Inven.Estado, dbo.in_Motivo_Inven.IdMotivo_Inv, dbo.in_Motivo_Inven.Desc_mov_inv, dbo.in_Producto.lote_num_lote, dbo.in_Producto.lote_fecha_vcto, 
                         dbo.in_presentacion.nom_presentacion, dbo.in_Ing_Egr_Inven.signo, dbo.in_movi_inven_tipo.tm_descripcion, dbo.seg_usuario.Nombre AS NomUsuario, dbo.in_Ing_Egr_Inven.IdResponsable, dbo.cp_proveedor.IdPersona, 
                         dbo.tb_persona.pe_nombreCompleto, dbo.in_Ing_Egr_Inven_det.IdOrdenCompra
FROM            dbo.tb_persona INNER JOIN
                         dbo.cp_proveedor ON dbo.tb_persona.IdPersona = dbo.cp_proveedor.IdPersona INNER JOIN
                         dbo.in_movi_inven_tipo INNER JOIN
                         dbo.tb_bodega INNER JOIN
                         dbo.in_Ing_Egr_Inven INNER JOIN
                         dbo.tb_sucursal ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdSucursal = dbo.tb_sucursal.IdSucursal ON dbo.tb_bodega.IdEmpresa = dbo.in_Ing_Egr_Inven.IdEmpresa AND 
                         dbo.tb_bodega.IdSucursal = dbo.in_Ing_Egr_Inven.IdSucursal AND dbo.tb_bodega.IdBodega = dbo.in_Ing_Egr_Inven.IdBodega ON dbo.in_movi_inven_tipo.IdEmpresa = dbo.in_Ing_Egr_Inven.IdEmpresa AND 
                         dbo.in_movi_inven_tipo.IdMovi_inven_tipo = dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo INNER JOIN
                         dbo.in_Ing_Egr_Inven_det ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.in_Ing_Egr_Inven_det.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdSucursal = dbo.in_Ing_Egr_Inven_det.IdSucursal AND 
                         dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo = dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo AND dbo.in_Ing_Egr_Inven.IdNumMovi = dbo.in_Ing_Egr_Inven_det.IdNumMovi INNER JOIN
                         dbo.in_Producto ON dbo.in_Ing_Egr_Inven_det.IdProducto = dbo.in_Producto.IdProducto AND dbo.in_Ing_Egr_Inven_det.IdEmpresa = dbo.in_Producto.IdEmpresa INNER JOIN
                         dbo.in_UnidadMedida ON dbo.in_Ing_Egr_Inven_det.IdUnidadMedida_sinConversion = dbo.in_UnidadMedida.IdUnidadMedida INNER JOIN
                         dbo.in_Motivo_Inven ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.in_Motivo_Inven.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdMotivo_Inv = dbo.in_Motivo_Inven.IdMotivo_Inv INNER JOIN
                         dbo.in_presentacion ON dbo.in_Producto.IdEmpresa = dbo.in_presentacion.IdEmpresa AND dbo.in_Producto.IdPresentacion = dbo.in_presentacion.IdPresentacion ON 
                         dbo.cp_proveedor.IdEmpresa = dbo.in_Ing_Egr_Inven.IdEmpresa AND dbo.cp_proveedor.IdProveedor = dbo.in_Ing_Egr_Inven.IdResponsable LEFT OUTER JOIN
                         dbo.seg_usuario ON dbo.in_Ing_Egr_Inven.IdUsuario = dbo.seg_usuario.IdUsuario
WHERE        (dbo.in_Ing_Egr_Inven.signo = '+')