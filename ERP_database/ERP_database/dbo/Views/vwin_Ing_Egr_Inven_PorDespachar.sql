create view vwin_Ing_Egr_Inven_PorDespachar as
SELECT        dbo.in_Ing_Egr_Inven.IdEmpresa, dbo.in_Ing_Egr_Inven.IdSucursal, dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven.IdNumMovi, dbo.in_Ing_Egr_Inven.IdBodega, dbo.in_Ing_Egr_Inven.signo, 
                         dbo.in_Ing_Egr_Inven.CodMoviInven, dbo.in_Ing_Egr_Inven.cm_observacion, dbo.in_Ing_Egr_Inven.cm_fecha, dbo.in_Ing_Egr_Inven.IdMotivo_Inv, dbo.in_Ing_Egr_Inven.IdResponsable, dbo.in_Ing_Egr_Inven.IdEstadoAproba, 
                         dbo.in_Ing_Egr_Inven.IdUsuarioAR, dbo.in_Ing_Egr_Inven.FechaAR, dbo.in_Ing_Egr_Inven.IdUsuarioDespacho, dbo.in_Ing_Egr_Inven.FechaDespacho, dbo.tb_bodega.bo_Descripcion, dbo.in_Motivo_Inven.Desc_mov_inv, 
                         dbo.in_movi_inven_tipo.tm_descripcion, dbo.in_Ing_Egr_Inven.Estado, dbo.in_Catalogo.Nombre AS EstadoAprobacion
FROM            dbo.in_Ing_Egr_Inven INNER JOIN
                         dbo.tb_bodega ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.tb_bodega.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdBodega = dbo.tb_bodega.IdBodega AND dbo.in_Ing_Egr_Inven.IdSucursal = dbo.tb_bodega.IdSucursal INNER JOIN
                         dbo.in_Motivo_Inven ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.in_Motivo_Inven.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdMotivo_Inv = dbo.in_Motivo_Inven.IdMotivo_Inv INNER JOIN
                         dbo.in_Catalogo ON dbo.in_Ing_Egr_Inven.IdEstadoAproba = dbo.in_Catalogo.IdCatalogo LEFT OUTER JOIN
                         dbo.in_movi_inven_tipo ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.in_movi_inven_tipo.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo = dbo.in_movi_inven_tipo.IdMovi_inven_tipo
WHERE        (dbo.in_Ing_Egr_Inven.IdEstadoAproba = 'APRO' and dbo.in_Ing_Egr_Inven.Estado = 'A' and dbo.in_Ing_Egr_Inven.signo = '-' and dbo.in_Ing_Egr_Inven.FechaDespacho is null)