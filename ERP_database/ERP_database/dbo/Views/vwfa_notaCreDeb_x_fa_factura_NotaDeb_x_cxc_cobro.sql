CREATE view [dbo].[vwfa_notaCreDeb_x_fa_factura_NotaDeb_x_cxc_cobro]
as
SELECT        dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_nt, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_nt, 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_nt, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdNota_nt, 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod, 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod, 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc, dbo.fa_notaCreDeb_x_cxc_cobro.IdEmpresa_cbr, dbo.fa_notaCreDeb_x_cxc_cobro.IdSucursal_cbr, 
                         dbo.fa_notaCreDeb_x_cxc_cobro.IdCobro_cbr, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.Valor_Aplicado
FROM            dbo.fa_notaCreDeb INNER JOIN
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb ON dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_nt AND 
                         dbo.fa_notaCreDeb.IdSucursal = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_nt AND 
                         dbo.fa_notaCreDeb.IdBodega = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_nt AND 
                         dbo.fa_notaCreDeb.IdNota = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdNota_nt LEFT OUTER JOIN
                         dbo.cxc_cobro_det INNER JOIN
                         dbo.fa_notaCreDeb_x_cxc_cobro ON dbo.cxc_cobro_det.IdEmpresa = dbo.fa_notaCreDeb_x_cxc_cobro.IdEmpresa_cbr AND 
                         dbo.cxc_cobro_det.IdSucursal = dbo.fa_notaCreDeb_x_cxc_cobro.IdSucursal_cbr AND dbo.cxc_cobro_det.IdCobro = dbo.fa_notaCreDeb_x_cxc_cobro.IdCobro_cbr ON
                          dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_nt = dbo.fa_notaCreDeb_x_cxc_cobro.IdEmpresa_nt AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_nt = dbo.fa_notaCreDeb_x_cxc_cobro.IdSucursal_nt AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_nt = dbo.fa_notaCreDeb_x_cxc_cobro.IdBodega_nt AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdNota_nt = dbo.fa_notaCreDeb_x_cxc_cobro.IdNota_nt AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod = dbo.cxc_cobro_det.IdEmpresa AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod = dbo.cxc_cobro_det.IdSucursal AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod = dbo.cxc_cobro_det.IdBodega_Cbte AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod = dbo.cxc_cobro_det.IdCbte_vta_nota AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc = dbo.cxc_cobro_det.dc_TipoDocumento
WHERE        (dbo.fa_notaCreDeb.Estado = 'A')