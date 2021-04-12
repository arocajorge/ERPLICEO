CREATE PROCEDURE [Academico].[SPACA_ContabilizacionFacturas]
(
@IdEmpresa int,
@FechaIni date,
@FechaFin date
)
AS
SELECT a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdCbteVta, a.vt_serie1 + '-' + a.vt_serie2 + '-' + a.vt_NumFactura AS vt_NumFactura, d.pe_nombreCompleto AS Alumno, a.vt_fecha, a.vt_Observacion, h.IdCtaCbleDebe, h.IdCtaCbleHaber, 
                  rel.ct_IdEmpresa, rel.ct_IdTipoCbte, rel.ct_IdCbteCble, ct.IdCtaCble, b.Total TotalModulo, isnull(ct.dc_Valor,0) TotalContable, dbo.BankersRounding( b.Total - isnull(ct.dc_Valor,0),2) as Diferencia
FROM     aca_Matricula_Rubro AS f with (nolock) INNER JOIN
                  aca_Matricula AS g with (nolock) ON f.IdEmpresa = g.IdEmpresa AND f.IdMatricula = g.IdMatricula INNER JOIN
                  fa_factura AS a with (nolock) INNER JOIN
                  fa_factura_resumen AS b with (nolock) ON a.IdEmpresa = b.IdEmpresa AND a.IdSucursal = b.IdSucursal AND a.IdBodega = b.IdBodega AND a.IdCbteVta = b.IdCbteVta INNER JOIN
                  aca_Alumno AS c with (nolock) ON a.IdEmpresa = c.IdEmpresa AND a.IdAlumno = c.IdAlumno INNER JOIN
                  tb_persona AS d with (nolock) ON c.IdPersona = d.IdPersona ON f.IdCbteVta = a.IdCbteVta AND f.IdBodega = a.IdBodega AND f.IdSucursal = a.IdSucursal AND f.IdEmpresa = a.IdEmpresa LEFT OUTER JOIN
                  fa_factura_x_ct_cbtecble AS rel with (nolock) ON a.IdEmpresa = rel.vt_IdEmpresa AND a.IdSucursal = rel.vt_IdSucursal AND a.IdBodega = rel.vt_IdBodega AND a.IdCbteVta = rel.vt_IdCbteVta LEFT OUTER JOIN
                  ct_cbtecble_det AS ct with (nolock) ON rel.ct_IdEmpresa = ct.IdEmpresa AND rel.ct_IdTipoCbte = ct.IdTipoCbte AND rel.ct_IdCbteCble = ct.IdCbteCble LEFT OUTER JOIN
                  aca_AnioLectivo_Curso_Plantilla_Parametrizacion AS h with (nolock) ON f.IdPlantilla = h.IdPlantilla AND g.IdEmpresa = h.IdEmpresa AND g.IdAnio = h.IdAnio AND g.IdSede = h.IdSede AND g.IdNivel = h.IdNivel AND g.IdJornada = h.IdJornada AND 
                  g.IdCurso = h.IdCurso AND f.IdRubro = h.IdRubro
WHERE  A.IdEmpresa = @IdEmpresa AND (a.Estado = 'A') and isnull(ct.dc_Valor,1) > 0 and a.vt_fecha between @FechaIni and @FechaFin
--and dbo.BankersRounding( b.Total - isnull(ct.dc_Valor,0),2) <> 0