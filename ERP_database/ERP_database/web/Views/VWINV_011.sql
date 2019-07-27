
CREATE VIEW [web].[VWINV_011]
AS
SELECT        dbo.in_Ing_Egr_Inven_det.IdEmpresa, dbo.in_Ing_Egr_Inven_det.IdSucursal, dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven_det.IdNumMovi, dbo.in_Ing_Egr_Inven_det.Secuencia, 
                         dbo.in_Ing_Egr_Inven_det.IdProducto, dbo.in_Producto.pr_descripcion + '-' + dbo.in_categorias.ca_Categoria AS pr_descripcion, dbo.in_Producto.pr_codigo, dbo.tb_sucursal.Su_Descripcion, dbo.tb_bodega.bo_Descripcion, 
                         dbo.in_Ing_Egr_Inven_det.IdUnidadMedida_sinConversion, dbo.in_UnidadMedida.Descripcion, dbo.in_Ing_Egr_Inven_det.dm_cantidad_sinConversion, dbo.in_Ing_Egr_Inven_det.mv_costo_sinConversion, 
                         dbo.in_Ing_Egr_Inven.cm_observacion, dbo.in_Ing_Egr_Inven.CodMoviInven, dbo.in_Ing_Egr_Inven.cm_fecha, dbo.in_Ing_Egr_Inven.Estado, dbo.in_Motivo_Inven.IdMotivo_Inv, dbo.in_Motivo_Inven.Desc_mov_inv, 
                         dbo.in_Producto.lote_num_lote, dbo.in_Producto.lote_fecha_vcto, dbo.in_presentacion.nom_presentacion, dbo.in_Ing_Egr_Inven.signo, dbo.in_movi_inven_tipo.tm_descripcion, dbo.seg_usuario.Nombre AS NomUsuario
FROM            dbo.in_movi_inven_tipo INNER JOIN
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
                         dbo.in_presentacion ON dbo.in_Producto.IdEmpresa = dbo.in_presentacion.IdEmpresa AND dbo.in_Producto.IdPresentacion = dbo.in_presentacion.IdPresentacion LEFT OUTER JOIN
                         dbo.in_categorias ON dbo.in_Producto.IdEmpresa = dbo.in_categorias.IdEmpresa AND dbo.in_Producto.IdCategoria = dbo.in_categorias.IdCategoria LEFT OUTER JOIN
                         dbo.seg_usuario ON dbo.in_Ing_Egr_Inven.IdUsuario = dbo.seg_usuario.IdUsuario
WHERE        (dbo.in_Ing_Egr_Inven.signo = '-') AND EXISTS
                             (SELECT        *
                               FROM            in_Producto_Composicion c
                               WHERE        c.IdEmpresa = dbo.in_Ing_Egr_Inven_det.IdEmpresa AND c.IdProductoHijo = dbo.in_Ing_Egr_Inven_det.IdProducto)
UNION
SELECT        dbo.in_Ing_Egr_Inven_det.IdEmpresa, dbo.in_Ing_Egr_Inven_det.IdSucursal, dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven_det.IdNumMovi, dbo.in_Ing_Egr_Inven_det.Secuencia, 
                         dbo.in_Ing_Egr_Inven_det.IdProducto, dbo.in_Producto.pr_descripcion, dbo.in_Producto.pr_codigo, dbo.tb_sucursal.Su_Descripcion, dbo.tb_bodega.bo_Descripcion, dbo.in_Ing_Egr_Inven_det.IdUnidadMedida_sinConversion, 
                         dbo.in_UnidadMedida.Descripcion, dbo.in_Ing_Egr_Inven_det.dm_cantidad_sinConversion, dbo.in_Ing_Egr_Inven_det.mv_costo_sinConversion, dbo.in_Ing_Egr_Inven.cm_observacion, dbo.in_Ing_Egr_Inven.CodMoviInven, 
                         dbo.in_Ing_Egr_Inven.cm_fecha, dbo.in_Ing_Egr_Inven.Estado, dbo.in_Motivo_Inven.IdMotivo_Inv, dbo.in_Motivo_Inven.Desc_mov_inv, dbo.in_Producto.lote_num_lote, dbo.in_Producto.lote_fecha_vcto, 
                         dbo.in_presentacion.nom_presentacion, dbo.in_Ing_Egr_Inven.signo, dbo.in_movi_inven_tipo.tm_descripcion, dbo.seg_usuario.Nombre AS NomUsuario
FROM            dbo.in_movi_inven_tipo INNER JOIN
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
                         dbo.in_presentacion ON dbo.in_Producto.IdEmpresa = dbo.in_presentacion.IdEmpresa AND dbo.in_Producto.IdPresentacion = dbo.in_presentacion.IdPresentacion LEFT OUTER JOIN
                         dbo.in_categorias ON dbo.in_Producto.IdEmpresa = dbo.in_categorias.IdEmpresa AND dbo.in_Producto.IdCategoria = dbo.in_categorias.IdCategoria LEFT OUTER JOIN
                         dbo.seg_usuario ON dbo.in_Ing_Egr_Inven.IdUsuario = dbo.seg_usuario.IdUsuario
WHERE        (dbo.in_Ing_Egr_Inven.signo = '-') AND NOT EXISTS
                             (SELECT        *
                               FROM            in_Producto_Composicion c
                               WHERE        c.IdEmpresa = dbo.in_Ing_Egr_Inven_det.IdEmpresa AND c.IdProductoHijo = dbo.in_Ing_Egr_Inven_det.IdProducto)