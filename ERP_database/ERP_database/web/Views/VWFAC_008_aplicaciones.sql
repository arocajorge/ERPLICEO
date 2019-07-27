CREATE VIEW [web].[VWFAC_008_aplicaciones]
AS
SELECT        fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_nt, fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_nt, fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_nt, fa_notaCreDeb_x_fa_factura_NotaDeb.IdNota_nt, 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.secuencia, fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod, fa_notaCreDeb_x_fa_factura_NotaDeb.Valor_Aplicado, 
                         IIF(fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc = 'FACT', fa_factura.vt_tipoDoc + '-' + fa_factura.vt_serie1 + '-' + fa_factura.vt_serie2 + '-' + fa_factura.vt_NumFactura, 
                         fa_notaCreDeb.CodDocumentoTipo + '-' + fa_notaCreDeb.Serie1 + '-' + fa_notaCreDeb.Serie2 + '-' + fa_notaCreDeb.NumNota_Impresa) AS NumFactura, IIF(fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc = 'FACT', 
                         fa_factura.vt_fecha, fa_notaCreDeb.no_fecha) vt_fecha
FROM            fa_notaCreDeb_x_fa_factura_NotaDeb LEFT OUTER JOIN
                         fa_notaCreDeb ON fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod = fa_notaCreDeb.IdEmpresa AND fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod = fa_notaCreDeb.IdSucursal AND 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod = fa_notaCreDeb.IdBodega AND fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod = fa_notaCreDeb.IdNota AND 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc = fa_notaCreDeb.CodDocumentoTipo LEFT OUTER JOIN
                         fa_factura ON fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod = fa_factura.IdEmpresa AND fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod = fa_factura.IdSucursal AND 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod = fa_factura.IdBodega AND fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod = fa_factura.IdCbteVta AND 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc = fa_factura.vt_tipoDoc