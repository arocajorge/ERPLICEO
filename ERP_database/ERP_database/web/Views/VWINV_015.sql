CREATE view [web].[VWINV_015]
as
SELECT        dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdEmpresa_fa, dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdSucursal_fa, dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdBodega_fa, 
                         dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdCbteVta_fa, dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.Secuencia_fa, dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdEmpresa_eg, 
                         dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdSucursal_eg, dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdMovi_inven_tipo_eg, dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdNumMovi_eg, 
                         dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.Secuencia_eg, dbo.in_Producto.pr_descripcion, dbo.fa_factura_det.vt_cantidad AS CantidadFac, dbo.fa_factura_det.vt_PrecioFinal, 
                         dbo.fa_factura_det.vt_cantidad * dbo.fa_factura_det.vt_PrecioFinal AS TotalFac, ABS(dbo.in_Ing_Egr_Inven_det.dm_cantidad) AS CantidadInv, dbo.in_Ing_Egr_Inven_det.mv_costo AS CostoUni, 
                         ABS(dbo.in_Ing_Egr_Inven_det.dm_cantidad) * dbo.in_Ing_Egr_Inven_det.mv_costo AS TotalCosto, 
                         dbo.fa_factura_det.vt_cantidad * dbo.fa_factura_det.vt_PrecioFinal + dbo.in_Ing_Egr_Inven_det.dm_cantidad * dbo.in_Ing_Egr_Inven_det.mv_costo AS Utilidad, CAST(dbo.in_Producto.IdCategoria AS int) AS IdCategoria, 
                         dbo.in_Producto.IdLinea, dbo.in_Producto.IdGrupo, dbo.in_Producto.IdSubGrupo, dbo.in_categorias.ca_Categoria, dbo.in_linea.nom_linea, dbo.in_grupo.nom_grupo, dbo.in_subgrupo.nom_subgrupo, 
                         dbo.tb_sucursal.Su_Descripcion, dbo.fa_factura.vt_NumFactura, dbo.fa_factura.vt_fecha, dbo.fa_factura_det.IdProducto
FROM            dbo.tb_sucursal RIGHT OUTER JOIN
                         dbo.fa_factura_det INNER JOIN
                         dbo.fa_factura_det_x_in_Ing_Egr_Inven_det ON dbo.fa_factura_det.IdEmpresa = dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdEmpresa_fa AND 
                         dbo.fa_factura_det.IdSucursal = dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdSucursal_fa AND dbo.fa_factura_det.IdBodega = dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdBodega_fa AND 
                         dbo.fa_factura_det.IdCbteVta = dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdCbteVta_fa AND dbo.fa_factura_det.Secuencia = dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.Secuencia_fa INNER JOIN
                         dbo.in_Ing_Egr_Inven_det ON dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdEmpresa_eg = dbo.in_Ing_Egr_Inven_det.IdEmpresa AND 
                         dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdSucursal_eg = dbo.in_Ing_Egr_Inven_det.IdSucursal AND dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdMovi_inven_tipo_eg = dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo AND 
                         dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdNumMovi_eg = dbo.in_Ing_Egr_Inven_det.IdNumMovi AND dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.Secuencia_eg = dbo.in_Ing_Egr_Inven_det.Secuencia INNER JOIN
                         dbo.in_Producto ON dbo.fa_factura_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.fa_factura_det.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                         dbo.fa_factura ON dbo.fa_factura_det.IdEmpresa = dbo.fa_factura.IdEmpresa AND dbo.fa_factura_det.IdSucursal = dbo.fa_factura.IdSucursal AND dbo.fa_factura_det.IdBodega = dbo.fa_factura.IdBodega AND 
                         dbo.fa_factura_det.IdCbteVta = dbo.fa_factura.IdCbteVta ON dbo.tb_sucursal.IdEmpresa = dbo.fa_factura_det.IdEmpresa AND dbo.tb_sucursal.IdSucursal = dbo.fa_factura_det.IdSucursal LEFT OUTER JOIN
                         dbo.in_linea INNER JOIN
                         dbo.in_categorias ON dbo.in_linea.IdEmpresa = dbo.in_categorias.IdEmpresa AND dbo.in_linea.IdCategoria = dbo.in_categorias.IdCategoria INNER JOIN
                         dbo.in_grupo ON dbo.in_linea.IdEmpresa = dbo.in_grupo.IdEmpresa AND dbo.in_linea.IdCategoria = dbo.in_grupo.IdCategoria AND dbo.in_linea.IdLinea = dbo.in_grupo.IdLinea INNER JOIN
                         dbo.in_subgrupo ON dbo.in_grupo.IdEmpresa = dbo.in_subgrupo.IdEmpresa AND dbo.in_grupo.IdCategoria = dbo.in_subgrupo.IdCategoria AND dbo.in_grupo.IdLinea = dbo.in_subgrupo.IdLinea AND 
                         dbo.in_grupo.IdGrupo = dbo.in_subgrupo.IdGrupo ON dbo.in_Producto.IdEmpresa = dbo.in_subgrupo.IdEmpresa AND dbo.in_Producto.IdCategoria = dbo.in_subgrupo.IdCategoria AND 
                         dbo.in_Producto.IdLinea = dbo.in_subgrupo.IdLinea AND dbo.in_Producto.IdGrupo = dbo.in_subgrupo.IdGrupo AND dbo.in_Producto.IdSubGrupo = dbo.in_subgrupo.IdSubgrupo
WHERE        (dbo.fa_factura.Estado = 'A')