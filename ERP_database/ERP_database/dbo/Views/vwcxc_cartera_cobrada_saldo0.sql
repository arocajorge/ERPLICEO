CREATE view [dbo].[vwcxc_cartera_cobrada_saldo0]
as
SELECT     a.IdEmpresa, a.IdSucursal, a.IdBodega, a.vt_tipoDoc, a.vt_tipoDoc + '-' + CAST(cast(a.vt_NumFactura AS numeric) AS VARCHAR) vt_NunDocumento, a.vt_Observacion, 
                      a.IdCbteVta, a.CodCbteVta, c.Su_Descripcion, a.IdCliente, a.IdAlumno, a.vt_fecha, b.Total, a.vt_fech_venc, e.pe_nombreCompleto, B.IdAnio, B.IdPlantilla, A.IdPuntoVta, 
                      isnull(sum(f.dc_ValorPago), 0) dc_ValorPago
FROM         fa_factura AS a INNER JOIN
                      fa_factura_resumen AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdSucursal = b.IdSucursal AND a.IdBodega = b.IdBodega AND a.IdCbteVta = b.IdCbteVta LEFT JOIN
                      tb_sucursal AS c ON a.IdEmpresa = c.IdEmpresa AND a.IdSucursal = c.IdSucursal INNER JOIN
                      fa_cliente AS d ON a.IdEmpresa = d .IdEmpresa AND a.IdCliente = d .IdCliente INNER JOIN
                      tb_persona AS e ON d .IdPersona = e.IdPersona LEFT JOIN
                      cxc_cobro_det AS f ON a.IdEmpresa = f.IdEmpresa AND a.IdSucursal = f.IdSucursal AND a.IdBodega = f.IdBodega_Cbte AND a.IdCbteVta = f.IdCbte_vta_nota AND 
                      a.vt_tipoDoc = f.dc_TipoDocumento AND f.estado = 'A'
WHERE     a.Estado = 'A' 
AND A.vt_fecha >= DATEADD(YEAR, - 1, GETDATE()) 
GROUP BY a.IdEmpresa, a.IdSucursal, a.IdBodega, a.vt_tipoDoc, a.vt_tipoDoc, a.vt_NumFactura, a.vt_Observacion, a.IdCbteVta, a.CodCbteVta, c.Su_Descripcion, a.IdCliente, 
                      a.IdAlumno, a.vt_fecha, b.Total, a.vt_fech_venc, e.pe_nombreCompleto, B.IdAnio, B.IdPlantilla, A.IdPuntoVta
HAVING      DBO.BankersRounding(B.Total - isnull(sum(f.dc_ValorPago), 0), 2) = 0
UNION ALL
SELECT     a.IdEmpresa, a.IdSucursal, a.IdBodega, a.CodDocumentoTipo, 
                      a.CodDocumentoTipo + '-' + CASE WHEN a.NaturalezaNota = 'SRI' THEN CAST(cast(a.NumNota_Impresa AS numeric) AS VARCHAR) ELSE ISNULL(A.CodNota, 
                      CAST(A.IdNota AS VARCHAR)) END vt_NunDocumento, a.sc_observacion, a.IdNota, a.CodNota, c.Su_Descripcion, a.IdCliente, a.IdAlumno, a.no_fecha, b.Total, 
                      a.no_fecha_venc, e.pe_nombreCompleto, B.IdAnio, NULL, A.IdPuntoVta, isnull(sum(f.dc_ValorPago), 0) dc_ValorPago
FROM         fa_notaCreDeb AS a INNER JOIN
                      fa_notaCreDeb_resumen AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdSucursal = b.IdSucursal AND a.IdBodega = b.IdBodega AND a.IdNota = b.IdNota LEFT JOIN
                      tb_sucursal AS c ON a.IdEmpresa = c.IdEmpresa AND a.IdSucursal = c.IdSucursal INNER JOIN
                      fa_cliente AS d ON a.IdEmpresa = d .IdEmpresa AND a.IdCliente = d .IdCliente INNER JOIN
                      tb_persona AS e ON d .IdPersona = e.IdPersona LEFT JOIN
                      cxc_cobro_det AS f ON a.IdEmpresa = f.IdEmpresa AND a.IdSucursal = f.IdSucursal AND a.IdBodega = f.IdBodega_Cbte AND a.IdNota = f.IdCbte_vta_nota AND 
                      a.CodDocumentoTipo = f.dc_TipoDocumento AND f.estado = 'A'
WHERE     a.Estado = 'A' AND a.CreDeb = 'D' AND A.no_fecha >= DATEADD(YEAR, - 1, GETDATE())
GROUP BY a.IdEmpresa, a.IdSucursal, a.IdBodega, a.CodDocumentoTipo, a.CodNota, a.NumNota_Impresa, a.sc_observacion, a.IdNota, a.CodNota, c.Su_Descripcion, 
                      a.IdCliente, a.IdAlumno, a.no_fecha, b.Total, a.no_fecha_venc, e.pe_nombreCompleto, B.IdAnio, A.IdPuntoVta, a.NaturalezaNota
HAVING      DBO.BankersRounding(B.Total - isnull(sum(f.dc_ValorPago), 0), 2) = 0