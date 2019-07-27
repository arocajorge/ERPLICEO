CREATE  view vwfa_notaCreDeb_x_fa_factura_NotaDeb_sin_cobros
as
SELECT        fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_nt, fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_nt, fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_nt, 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdNota_nt, fa_notaCreDeb_x_fa_factura_NotaDeb.secuencia, 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod, fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod, 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod, fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod, 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc, fa_notaCreDeb_x_fa_factura_NotaDeb.Valor_Aplicado, fa_notaCreDeb_x_fa_factura_NotaDeb.fecha_cruce, 
                         cxc_cobro_det.IdCobro
FROM            fa_notaCreDeb_x_fa_factura_NotaDeb LEFT OUTER JOIN
                         cxc_cobro_det ON fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod = cxc_cobro_det.IdEmpresa AND 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod = cxc_cobro_det.IdSucursal AND 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod = cxc_cobro_det.IdBodega_Cbte AND 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod = cxc_cobro_det.IdCbte_vta_nota AND 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc = cxc_cobro_det.dc_TipoDocumento
where  cxc_cobro_det.IdCobro is null