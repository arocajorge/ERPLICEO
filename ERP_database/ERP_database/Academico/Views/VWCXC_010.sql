CREATE VIEW [Academico].[VWCXC_010]
AS
SELECT a.IdEmpresa, 'FACTURA' Tipo, a.IdCbteVta, 1 AS Orden, a.vt_fecha, a.vt_Observacion, a.vt_serie1 + '-' + a.vt_serie2 + '-' + a.vt_NumFactura Referencia, year(a.vt_fecha) Anio, month(a.vt_fecha) IdMes, e.smes, b.Total AS Debe, 0 AS Haber, 
                  b.Total AS Valor, d .pe_nombreCompleto, c.Codigo AS CodigoAlumno, a.IdAlumno
FROM     fa_factura AS a INNER JOIN
                  fa_factura_resumen AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdSucursal = b.IdSucursal AND a.IdBodega = b.IdBodega AND a.IdCbteVta = b.IdCbteVta INNER JOIN
                  aca_Alumno AS c ON a.IdEmpresa = c.IdEmpresa AND a.IdAlumno = c.IdAlumno INNER JOIN
                  tb_persona AS d ON c.IdPersona = d .IdPersona LEFT JOIN
                  tb_mes AS e ON month(a.vt_fecha) = e.idMes
WHERE  a.Estado = 'A'
UNION ALL
SELECT a.IdEmpresa, CASE WHEN A.CreDeb = 'C' THEN 'NOTA DE CREDITO' ELSE 'NOTA DE DEBITO' END Tipo, a.IdNota, 1 AS Orden, a.no_fecha, a.sc_observacion, 
                  CASE WHEN A.NaturalezaNota = 'SRI' THEN a.Serie1 + '-' + a.Serie2 + '-' + a.NumNota_Impresa ELSE ISNULL(A.CodNota, CAST(A.IdNota AS VARCHAR)) END Referencia, year(a.no_fecha) Anio, month(a.no_fecha) IdMes, e.smes, 
                  CASE WHEN A.CreDeb = 'C' THEN 0 ELSE b.Total END AS Debe, CASE WHEN A.CreDeb = 'C' THEN b.Total ELSE 0 END AS Haber, CASE WHEN A.CreDeb = 'C' THEN b.Total * - 1 ELSE b.Total END AS Valor, d .pe_nombreCompleto, 
                  c.Codigo AS CodigoAlumno, a.IdAlumno
FROM     fa_notaCreDeb AS a INNER JOIN
                  fa_notaCreDeb_resumen AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdSucursal = b.IdSucursal AND a.IdBodega = b.IdBodega AND a.IdNota = b.IdNota INNER JOIN
                  aca_Alumno AS c ON a.IdEmpresa = c.IdEmpresa AND a.IdAlumno = c.IdAlumno INNER JOIN
                  tb_persona AS d ON c.IdPersona = d .IdPersona LEFT JOIN
                  tb_mes AS e ON month(a.no_fecha) = e.idMes
WHERE  a.Estado = 'A'
 UNION ALL
SELECT a.IdEmpresa, CASE WHEN SUM(B.dc_ValorProntoPago) IS NULL THEN 'PAGO ANTICIPADO' ELSE CASE WHEN A.IdCobro_tipo = 'NTCR' THEN 'NOTA DE CREDITO' ELSE 'PAGO' END END Tipo, a.IdCobro, 2 AS Orden, a.cr_fecha, a.cr_observacion + ' ' + isnull(a.cr_ObservacionPantalla, ''), 
                  cast(a.IdCobro AS varchar) Referencia, year(a.cr_fecha) Anio, month(a.cr_fecha) IdMes, e.smes, 0 AS Debe, isnull(sum(b.dc_ValorPago), a.cr_TotalCobro) AS Haber, isnull(sum(b.dc_ValorPago) * - 1, a.cr_TotalCobro) AS Valor, 
                  d .pe_nombreCompleto, c.Codigo AS CodigoAlumno, a.IdAlumno
FROM     cxc_cobro AS a INNER JOIN
                  cxc_cobro_det AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdSucursal = b.IdSucursal AND a.IdCobro = b.IdCobro INNER JOIN
                  aca_Alumno AS c ON a.IdEmpresa = c.IdEmpresa AND a.IdAlumno = c.IdAlumno INNER JOIN
                  tb_persona AS d ON c.IdPersona = d .IdPersona LEFT JOIN
                  tb_mes AS e ON month(a.cr_fecha) = e.idMes 
WHERE  a.cr_estado = 'A' AND NOT EXISTS
                      (SELECT A1.IdEmpresa
                       FROM      fa_notaCreDeb AS A1 INNER JOIN
                                         fa_notaCreDeb_resumen AS B1 ON A1.IdEmpresa = B1.IdEmpresa AND A1.IdSucursal = B1.IdSucursal AND A1.IdBodega = B1.IdBodega AND A1.IdNota = B1.IdNota INNER JOIN
                                         fa_notaCreDeb_x_cxc_cobro AS C1 ON A1.IdEmpresa = C1.IdEmpresa_nt AND A1.IdSucursal = C1.IdSucursal_nt AND A1.IdBodega = C1.IdBodega_nt AND A1.IdNota = C1.IdNota_nt INNER JOIN
                                         cxc_cobro AS D1 ON C1.IdEmpresa_cbr = D1.IdEmpresa AND C1.IdSucursal_cbr = D1.IdSucursal AND C1.IdCobro_cbr = D1.IdCobro
                       WHERE   DBO.BankersRounding(B1.Total - C1.Valor_cobro, 2) = 0 AND A1.no_fecha = D1.cr_fecha AND A.IdEmpresa = D1.IdEmpresa AND A.IdSucursal = D1.IdSucursal AND A.IdCobro = D1.IdCobro)
					   and a.IdCobro_tipo <> 'NTCR'
GROUP BY a.IdEmpresa, a.IdCobro, a.cr_fecha, a.cr_observacion + ' ' + isnull(a.cr_ObservacionPantalla, ''), cast(a.IdCobro AS varchar), year(a.cr_fecha), month(a.cr_fecha), e.smes, d .pe_nombreCompleto, c.Codigo, a.IdAlumno, a.cr_TotalCobro, 
                  a.IdCobro_tipo