CREATE VIEW [Academico].[VWCXC_002_Aplicaciones]
AS
SELECT a.IdEmpresa, a.IdSucursal, a.IdCobro, a.secuencial, a.dc_TipoDocumento, CASE WHEN a.dc_TipoDocumento = 'NTDB' THEN (CASE WHEN d .NaturalezaNota = 'SRI' THEN CAST(CAST(D .NumNota_Impresa AS NUMERIC) AS VARCHAR) 
                  ELSE D .CodNota END) + '     ' + isnull(D .sc_observacion, '') ELSE CAST(CAST(B.vt_NumFactura AS NUMERIC) AS VARCHAR) + '     ' + isnull(b.vt_Observacion, '') END AS vt_NumFactura, ISNULL(c.Total, e.Total) AS Total, 
                  a.dc_ValorProntoPago, a.dc_ValorPago
FROM     dbo.cxc_cobro_det AS a LEFT OUTER JOIN
                  dbo.fa_factura AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdSucursal = b.IdSucursal AND a.IdBodega_Cbte = b.IdBodega AND a.IdCbte_vta_nota = b.IdCbteVta AND a.dc_TipoDocumento = b.vt_tipoDoc LEFT OUTER JOIN
                  dbo.fa_factura_resumen AS c ON b.IdEmpresa = c.IdEmpresa AND b.IdSucursal = c.IdSucursal AND b.IdBodega = c.IdBodega AND b.IdCbteVta = c.IdCbteVta LEFT OUTER JOIN
                  dbo.fa_notaCreDeb AS d ON a.IdEmpresa = d.IdEmpresa AND a.IdSucursal = d.IdSucursal AND a.IdBodega_Cbte = d.IdBodega AND a.IdCbte_vta_nota = d.IdNota AND a.dc_TipoDocumento = d.CodDocumentoTipo LEFT OUTER JOIN
                  dbo.fa_notaCreDeb_resumen AS e ON d.IdEmpresa = e.IdEmpresa AND d.IdSucursal = e.IdSucursal AND d.IdBodega = e.IdBodega AND d.IdNota = e.IdNota
union all
select a.IdEmpresa, a.IdSucursal, a.IdCobro, 0, '','COBRO ANTICIPADO SIN DOCUMENTOS',a.cr_TotalCobro, 0, a.cr_TotalCobro from cxc_cobro as a 
where a.IdAlumno is not null and a.cr_Saldo > 0