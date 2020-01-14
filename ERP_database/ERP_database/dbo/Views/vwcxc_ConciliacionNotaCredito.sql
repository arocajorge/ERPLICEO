CREATE VIEW vwcxc_ConciliacionNotaCredito
AS
SELECT cxc_ConciliacionNotaCredito.IdEmpresa, cxc_ConciliacionNotaCredito.IdConciliacion, cxc_ConciliacionNotaCredito.IdSucursal, cxc_ConciliacionNotaCredito.IdBodega, cxc_ConciliacionNotaCredito.IdNota, 
                  cxc_ConciliacionNotaCredito.IdCobro, cxc_ConciliacionNotaCredito.Fecha, cxc_ConciliacionNotaCredito.Valor, cxc_ConciliacionNotaCredito.Observacion, cxc_ConciliacionNotaCredito.Estado, 
                  CASE WHEN fa_notaCreDeb.NaturalezaNota = 'SRI' THEN fa_notaCreDeb.Serie1 + '-' + fa_notaCreDeb.Serie2 + '-' + fa_notaCreDeb.NumNota_Impresa ELSE fa_notaCreDeb.CodNota END AS Referencia, 
                  tb_persona.pe_nombreCompleto
FROM     cxc_ConciliacionNotaCredito INNER JOIN
                  fa_notaCreDeb ON cxc_ConciliacionNotaCredito.IdEmpresa = fa_notaCreDeb.IdEmpresa AND cxc_ConciliacionNotaCredito.IdSucursal = fa_notaCreDeb.IdSucursal AND 
                  cxc_ConciliacionNotaCredito.IdBodega = fa_notaCreDeb.IdBodega AND cxc_ConciliacionNotaCredito.IdNota = fa_notaCreDeb.IdNota INNER JOIN
                  aca_Alumno ON fa_notaCreDeb.IdEmpresa = aca_Alumno.IdEmpresa AND fa_notaCreDeb.IdAlumno = aca_Alumno.IdAlumno INNER JOIN
                  tb_persona ON aca_Alumno.IdPersona = tb_persona.IdPersona