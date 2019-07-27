CREATE view [dbo].[vwin_Ing_Egr_Inven_det_conversion]
as
SELECT dbo.in_Ing_Egr_Inven_det.IdEmpresa, dbo.in_Ing_Egr_Inven_det.IdSucursal, dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven_det.IdNumMovi, dbo.in_Ing_Egr_Inven_det.Secuencia, dbo.in_Ing_Egr_Inven_det.IdBodega, 
                  dbo.in_Ing_Egr_Inven_det.IdProducto, dbo.in_Ing_Egr_Inven_det.dm_cantidad_sinConversion, dbo.in_Ing_Egr_Inven_det.IdUnidadMedida_sinConversion, dbo.in_Ing_Egr_Inven_det.mv_costo_sinConversion, 
                  dbo.in_Producto.IdUnidadMedida_Consumo, dbo.in_UnidadMedida_Equiv_conversion.valor_equiv, ISNULL(ISNULL(dbo.in_Ing_Egr_Inven_det.dm_cantidad_sinConversion, 0) * ISNULL(dbo.in_UnidadMedida_Equiv_conversion.valor_equiv, 
                  1), 0) AS dm_cantidad, 
				  (ISNULL(ISNULL(dbo.in_Ing_Egr_Inven_det.mv_costo_sinConversion, 0) * ISNULL(dbo.in_Ing_Egr_Inven_det.dm_cantidad_sinConversion, 1), 0))
				  /
				  ISNULL(ISNULL(dbo.in_Ing_Egr_Inven_det.dm_cantidad_sinConversion, 0) * ISNULL(dbo.in_UnidadMedida_Equiv_conversion.valor_equiv, 1), 0) AS mv_costo

FROM     dbo.in_Ing_Egr_Inven_det INNER JOIN
                  dbo.in_Producto ON dbo.in_Ing_Egr_Inven_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_Ing_Egr_Inven_det.IdProducto = dbo.in_Producto.IdProducto LEFT OUTER JOIN
                  dbo.in_UnidadMedida_Equiv_conversion ON dbo.in_Ing_Egr_Inven_det.IdUnidadMedida_sinConversion = dbo.in_UnidadMedida_Equiv_conversion.IdUnidadMedida AND 
                  dbo.in_Producto.IdUnidadMedida_Consumo = dbo.in_UnidadMedida_Equiv_conversion.IdUnidadMedida_equiva