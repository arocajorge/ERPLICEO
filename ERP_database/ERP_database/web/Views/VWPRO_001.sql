CREATE VIEW web.VWPRO_001
AS
SELECT        d.IdEmpresa, d.IdFabricacion, d.Secuencia, c.Fecha, c.Observacion, c.Estado, in_su.Su_Descripcion AS in_Su_Descripcion, in_bo.bo_Descripcion AS in_bo_Descripcion, eg_su.Su_Descripcion AS eg_Su_Descripcion, 
                         eg_bo.bo_Descripcion AS eg_bo_Descripcion, in_ti.tm_descripcion AS in_NombreTipo, eg_ti.tm_descripcion AS eg_NombreTipo, c.egr_IdNumMovi, c.ing_IdNumMovi, d.Signo, d.IdProducto, d.IdUnidadMedida, d.Cantidad, 
                         d.Costo, d.RealizaMovimiento, p.pr_descripcion, u.Descripcion AS NombreUnidad, 
						 case when d.Signo = '+' then 2 else 1 end as Orden,case when d.Signo = '+' then 'PRODUCTO TERMINADO' else 'MATERIA PRIMA' end as Tipo,
						 case when d.RealizaMovimiento = 1 then 'SI' else 'NO' end as Movimiento
FROM            dbo.pro_Fabricacion AS c INNER JOIN
                         dbo.pro_FabricacionDet AS d ON c.IdEmpresa = d.IdEmpresa AND c.IdFabricacion = d.IdFabricacion INNER JOIN
                         dbo.tb_bodega AS in_bo ON c.IdEmpresa = in_bo.IdEmpresa AND c.ing_IdSucursal = in_bo.IdSucursal AND c.ing_IdBodega = in_bo.IdBodega INNER JOIN
                         dbo.tb_sucursal AS in_su ON in_bo.IdEmpresa = in_su.IdEmpresa AND in_bo.IdSucursal = in_su.IdSucursal INNER JOIN
                         dbo.tb_bodega AS eg_bo ON c.IdEmpresa = eg_bo.IdEmpresa AND c.egr_IdSucursal = eg_bo.IdSucursal AND c.egr_IdBodega = eg_bo.IdBodega INNER JOIN
                         dbo.tb_sucursal AS eg_su ON eg_bo.IdEmpresa = eg_su.IdEmpresa AND eg_bo.IdSucursal = eg_su.IdSucursal INNER JOIN
                         dbo.in_Producto AS p ON d.IdEmpresa = p.IdEmpresa AND d.IdProducto = p.IdProducto INNER JOIN
                         dbo.in_UnidadMedida AS u ON d.IdUnidadMedida = u.IdUnidadMedida LEFT OUTER JOIN
                         dbo.in_movi_inven_tipo AS eg_ti ON c.IdEmpresa = eg_ti.IdEmpresa AND c.egr_IdMovi_inven_tipo = eg_ti.IdMovi_inven_tipo LEFT OUTER JOIN
                         dbo.in_movi_inven_tipo AS in_ti ON c.IdEmpresa = in_ti.IdEmpresa AND c.ing_IdMovi_inven_tipo = in_ti.IdMovi_inven_tipo