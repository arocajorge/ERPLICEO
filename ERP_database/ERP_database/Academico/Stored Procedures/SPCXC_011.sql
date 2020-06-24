CREATE PROCEDURE Academico.SPCXC_011
(
@IdEmpresa int,
@IdAlumno numeric
)
AS

select a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdCbteVta, a.vt_tipoDoc, b.Total, isnull(e.dc_ValorPago,0) as dc_ValorPago, dbo.BankersRounding(b.Total - isnull(e.dc_ValorPago,0),2) Saldo,
a.vt_Observacion, a.vt_fecha, a.IdAlumno, f.NomNivel, f.NomJornada, f.NomCurso, f.NomParalelo, D.pe_nombreCompleto AS Alumno, f.pe_nombreCompleto as Representante, c.Codigo,
CASE WHEN ar.AplicaProntoPago = 1 AND ap.FechaProntoPago >= CAST(GETDATE() AS DATE) 
THEN b.ValorProntoPago ELSE b.Total END AS ValorProntoPago, ap.FechaProntoPago AS FechaProntoPago
from fa_factura as a inner join
fa_factura_resumen as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdBodega = b.IdBodega and a.IdCbteVta = b.IdCbteVta inner join
aca_Alumno as c on a.IdEmpresa = c.IdEmpresa and a.IdAlumno = c.IdAlumno inner join
tb_persona as d on c.IdPersona = d.IdPersona left join
(
	select b.IdEmpresa, b.IdSucursal, b.IdBodega_Cbte, b.IdCbte_vta_nota, sum(b.dc_ValorPago) dc_ValorPago 
	from cxc_cobro as a inner join
	cxc_cobro_det as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdCobro = b.IdCobro
	where a.cr_estado = 'A' AND B.estado = 'A' AND B.dc_TipoDocumento = 'FACT'
	group by b.IdEmpresa, b.IdSucursal, b.IdBodega_Cbte, b.IdCbte_vta_nota
) e on a.IdEmpresa = e.IdEmpresa and a.IdSucursal = e.IdSucursal and a.IdBodega = e.IdBodega_Cbte and a.IdCbteVta = e.IdCbte_vta_nota left join
(
SELECT m.IdEmpresa, m.IdMatricula, m.IdAlumno, sn.NomNivel, nj.NomJornada, jc.NomCurso, cp.NomParalelo, r.pe_nombreCompleto
FROM     aca_AnioLectivo_Sede_NivelAcademico AS sn INNER JOIN
                  aca_AnioLectivo AS a ON sn.IdEmpresa = a.IdEmpresa AND sn.IdAnio = a.IdAnio RIGHT OUTER JOIN
                  aca_Matricula AS m LEFT OUTER JOIN
                  aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND 
                  m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN
                  aca_AnioLectivo_Jornada_Curso AS jc ON cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada AND cp.IdCurso = jc.IdCurso LEFT OUTER JOIN
                  aca_AnioLectivo_NivelAcademico_Jornada AS nj ON jc.IdEmpresa = nj.IdEmpresa AND jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel AND jc.IdJornada = nj.IdJornada ON sn.IdEmpresa = nj.IdEmpresa AND 
                  sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel left join
				  tb_persona as r on r.IdPersona = m.IdPersonaR
WHERE M.IdEmpresa = @IdEmpresa and M.IdAlumno = @IdAlumno AND (a.EnCurso = 1) AND (NOT EXISTS
                      (SELECT IdEmpresa
                       FROM      aca_AlumnoRetiro AS f
                       WHERE   (m.IdEmpresa = IdEmpresa) AND (m.IdMatricula = IdMatricula) AND (Estado = 1)))
) as f on a.IdEmpresa = f.IdEmpresa and a.IdAlumno = f.IdAlumno left join

aca_AnioLectivo_Rubro as ar on b.IdEmpresa = ar.IdEmpresa and b.IdAnio = ar.IdAnio and b.IdRubro = ar.IdRubro left join
aca_AnioLectivo_Periodo as ap on b.IdEmpresa = ap.IdEmpresa and b.IdPeriodo = ap.IdPeriodo

 where a.IdEmpresa = @IdEmpresa and a.IdAlumno = @IdAlumno and
dbo.BankersRounding(b.Total - isnull(e.dc_ValorPago,0),2) > 0
and a.Estado = 'A'
UNION ALL
select a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdNota, a.CodDocumentoTipo, b.Total, isnull(e.dc_ValorPago,0) as dc_ValorPago, dbo.BankersRounding(b.Total - isnull(e.dc_ValorPago,0),2) Saldo,
a.sc_observacion, a.no_fecha, a.IdAlumno, f.NomNivel, f.NomJornada, f.NomCurso, f.NomParalelo, D.pe_nombreCompleto AS Alumno, f.pe_nombreCompleto as Representante, c.Codigo, b.Total  AS ValorProntoPago, cast(GETDATE() as date) AS FechaProntoPago
from fa_notaCreDeb as a inner join
fa_notaCreDeb_resumen as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdBodega = b.IdBodega and a.IdNota = b.IdNota inner join
aca_Alumno as c on a.IdEmpresa = c.IdEmpresa and a.IdAlumno = c.IdAlumno inner join
tb_persona as d on c.IdPersona = d.IdPersona left join
(
	select b.IdEmpresa, b.IdSucursal, b.IdBodega_Cbte, b.IdCbte_vta_nota, sum(b.dc_ValorPago) dc_ValorPago 
	from cxc_cobro as a inner join
	cxc_cobro_det as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdCobro = b.IdCobro
	where a.cr_estado = 'A' AND B.estado = 'A' AND B.dc_TipoDocumento = 'NTDB'
	group by b.IdEmpresa, b.IdSucursal, b.IdBodega_Cbte, b.IdCbte_vta_nota
) e on a.IdEmpresa = e.IdEmpresa and a.IdSucursal = e.IdSucursal and a.IdBodega = e.IdBodega_Cbte and a.IdNota = e.IdCbte_vta_nota left join
(
SELECT m.IdEmpresa, m.IdMatricula, m.IdAlumno, sn.NomNivel, nj.NomJornada, jc.NomCurso, cp.NomParalelo, r.pe_nombreCompleto
FROM     aca_AnioLectivo_Sede_NivelAcademico AS sn INNER JOIN
                  aca_AnioLectivo AS a ON sn.IdEmpresa = a.IdEmpresa AND sn.IdAnio = a.IdAnio RIGHT OUTER JOIN
                  aca_Matricula AS m LEFT OUTER JOIN
                  aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND 
                  m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN
                  aca_AnioLectivo_Jornada_Curso AS jc ON cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada AND cp.IdCurso = jc.IdCurso LEFT OUTER JOIN
                  aca_AnioLectivo_NivelAcademico_Jornada AS nj ON jc.IdEmpresa = nj.IdEmpresa AND jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel AND jc.IdJornada = nj.IdJornada ON sn.IdEmpresa = nj.IdEmpresa AND 
                  sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel left join
				  tb_persona as r on r.IdPersona = m.IdPersonaR
WHERE M.IdEmpresa = @IdEmpresa and M.IdAlumno = @IdAlumno AND (a.EnCurso = 1) AND (NOT EXISTS
                      (SELECT IdEmpresa
                       FROM      aca_AlumnoRetiro AS f
                       WHERE   (m.IdEmpresa = IdEmpresa) AND (m.IdMatricula = IdMatricula) AND (Estado = 1)))
) as f on a.IdEmpresa = f.IdEmpresa and a.IdAlumno = f.IdAlumno
 where a.IdEmpresa = @IdEmpresa and a.IdAlumno = @IdAlumno and
dbo.BankersRounding(b.Total - isnull(e.dc_ValorPago,0),2) > 0
and a.Estado = 'A' AND A.CreDeb = 'D'