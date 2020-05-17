CREATE VIEW vwba_ArchivoRecaudacionDet_Saldos
AS
SELECT A.IdEmpresa, A.IdAlumno, A.IdMatricula, A.CodigoAlumno, A.pe_nombreCompleto, SUM(A.Saldo) Saldo, SUM(A.SaldoProntoPago) SaldoProntoPago
FROM(
SELECT a.IdEmpresa, a.IdAlumno, d.IdMatricula, c.Codigo AS CodigoAlumno, f.pe_nombreCompleto, dbo.BankersRounding(b.Total - ISNULL(g.dc_ValorPago, 0), 2) AS Saldo,
CASE WHEN dbo.BankersRounding(b.Total - ISNULL(g.dc_ValorPago, 0), 2) > 0 THEN
CASE WHEN ap.FechaProntoPago >= cast(getdate() as date) THEN dbo.BankersRounding(B.ValorProntoPago - ISNULL(g.dc_ValorPago, 0), 2) ELSE dbo.BankersRounding(b.Total - ISNULL(g.dc_ValorPago, 0), 2) END
ELSE 0 END AS SaldoProntoPago
FROM     (SELECT IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento, SUM(dc_ValorPago) AS dc_ValorPago
                  FROM      cxc_cobro_det AS a
                  WHERE   (dc_TipoDocumento = 'FACT') AND (estado = 'A')
                  GROUP BY IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento) AS g RIGHT OUTER JOIN
                  fa_factura AS a INNER JOIN
                  fa_factura_resumen AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdSucursal = b.IdSucursal AND a.IdBodega = b.IdBodega AND a.IdCbteVta = b.IdCbteVta INNER JOIN
                  aca_Alumno AS c ON a.IdEmpresa = c.IdEmpresa AND a.IdAlumno = c.IdAlumno INNER JOIN
                  aca_Matricula AS d ON d.IdEmpresa = c.IdEmpresa AND d.IdAlumno = c.IdAlumno INNER JOIN
                  aca_AnioLectivo AS e ON d.IdEmpresa = e.IdEmpresa AND d.IdAnio = e.IdAnio INNER JOIN
                  tb_persona AS f ON c.IdPersona = f.IdPersona LEFT OUTER JOIN
                  aca_AnioLectivo_Periodo AS ap ON b.IdPeriodo = ap.IdPeriodo AND b.IdEmpresa = ap.IdEmpresa ON g.IdEmpresa = a.IdEmpresa AND g.IdSucursal = a.IdSucursal AND g.IdBodega_Cbte = a.IdBodega AND 
                  g.IdCbte_vta_nota = a.IdCbteVta AND g.dc_TipoDocumento = a.vt_tipoDoc
WHERE  (e.EnCurso = 1) AND (dbo.BankersRounding(b.Total - ISNULL(g.dc_ValorPago, 0), 2) > 0) and a.Estado = 'A'
UNION ALL
SELECT A.IdEmpresa, A.IdAlumno, G.IdMatricula, C.Codigo, D.pe_nombreCompleto, dbo.BankersRounding(b.Total - ISNULL(E.dc_ValorPago, 0), 2), dbo.BankersRounding(b.Total - ISNULL(E.dc_ValorPago, 0), 2)

FROM fa_notaCreDeb AS A INNER JOIN
fa_notaCreDeb_resumen AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdSucursal = B.IdSucursal AND A.IdBodega = B.IdBodega AND A.IdNota = B.IdNota INNER JOIN
aca_Alumno AS C ON A.IdEmpresa = C.IdEmpresa AND A.IdAlumno = C.IdAlumno INNER JOIN
tb_persona AS D ON C.IdPersona = D.IdPersona LEFT JOIN
(
	SELECT IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento, SUM(dc_ValorPago) AS dc_ValorPago
	FROM      cxc_cobro_det AS a
	WHERE   (dc_TipoDocumento = 'NTDB') AND (estado = 'A')
	GROUP BY IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento
) AS E on E.IdEmpresa = A.IdEmpresa AND A.IdSucursal = E.IdSucursal AND A.IdBodega = E.IdBodega_Cbte AND A.IdNota = E.IdCbte_vta_nota AND A.CodDocumentoTipo = E.dc_TipoDocumento INNER JOIN
aca_Matricula AS G ON A.IdEmpresa = G.IdEmpresa AND A.IdAlumno = G.IdAlumno INNER JOIN
aca_AnioLectivo AS H ON G.IdEmpresa = H.IdEmpresa AND G.IdAnio = H.IdAnio
WHERE H.EnCurso = 1 AND (dbo.BankersRounding(b.Total - ISNULL(E.dc_ValorPago, 0), 2) > 0) and a.Estado = 'A' AND A.CreDeb = 'D'
) A
GROUP BY A.IdEmpresa, A.IdAlumno, A.IdMatricula, A.CodigoAlumno, A.pe_nombreCompleto 
HAVING DBO.BankersRounding(SUM(A.SaldoProntoPago),2) > 0