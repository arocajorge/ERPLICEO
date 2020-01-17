CREATE VIEW vwcxc_ConciliacionNotaCreditoDet
AS
SELECT cxc_ConciliacionNotaCreditoDet.IdEmpresa, cxc_ConciliacionNotaCreditoDet.IdConciliacion, cxc_ConciliacionNotaCreditoDet.Secuencia, cxc_ConciliacionNotaCreditoDet.IdSucursal, cxc_ConciliacionNotaCreditoDet.IdBodega, 
                  cxc_ConciliacionNotaCreditoDet.IdCbteVtaNota, cxc_ConciliacionNotaCreditoDet.vt_TipoDoc, cxc_ConciliacionNotaCreditoDet.Valor,
				  CASE WHEN cxc_ConciliacionNotaCreditoDet.vt_TipoDoc = 'FACT' THEN fa_factura.vt_serie1+'-'+fa_factura.vt_serie2+'-'+fa_factura.vt_NumFactura ELSE 
				  CASE WHEN fa_notaCreDeb.NaturalezaNota = 'SRI' THEN fa_notaCreDeb.Serie1+'-'+fa_notaCreDeb.Serie2+'-'+fa_notaCreDeb.NumNota_Impresa ELSE fa_notaCreDeb.CodNota END end AS ReferenciaDet
FROM     cxc_ConciliacionNotaCreditoDet LEFT OUTER JOIN
                  fa_notaCreDeb ON cxc_ConciliacionNotaCreditoDet.IdEmpresa = fa_notaCreDeb.IdEmpresa AND cxc_ConciliacionNotaCreditoDet.IdSucursal = fa_notaCreDeb.IdSucursal AND 
                  cxc_ConciliacionNotaCreditoDet.IdBodega = fa_notaCreDeb.IdBodega AND cxc_ConciliacionNotaCreditoDet.IdCbteVtaNota = fa_notaCreDeb.IdNota AND 
                  cxc_ConciliacionNotaCreditoDet.vt_TipoDoc = fa_notaCreDeb.CodDocumentoTipo LEFT OUTER JOIN
                  fa_factura ON cxc_ConciliacionNotaCreditoDet.IdEmpresa = fa_factura.IdEmpresa AND cxc_ConciliacionNotaCreditoDet.IdSucursal = fa_factura.IdSucursal AND cxc_ConciliacionNotaCreditoDet.IdBodega = fa_factura.IdBodega AND 
                  cxc_ConciliacionNotaCreditoDet.IdCbteVtaNota = fa_factura.IdCbteVta AND cxc_ConciliacionNotaCreditoDet.vt_TipoDoc = fa_factura.vt_tipoDoc