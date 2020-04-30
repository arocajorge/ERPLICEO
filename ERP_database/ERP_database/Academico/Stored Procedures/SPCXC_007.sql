--EXEC Academico.SPCXC_007 1 ,'2020/4/20'
CREATE PROCEDURE [Academico].[SPCXC_007]
(
@IdEmpresa int,
@FechaCorte date
)
AS
SELECT a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdCbteVta, a.vt_fecha, a.vt_Observacion, a.vt_serie1 + '-' + a.vt_serie2 + '-' + a.vt_NumFactura AS vt_NumFactura, c.IdAlumno, c.Codigo AS CodigoAlumno, d.pe_nombreCompleto, b.IdAnio, 
                  YEAR(ISNULL(e.FechaDesde, a.vt_fecha)) AS Periodo, b.Total, ISNULL(f.dc_ValorPago, 0) AS TotalPagado, ROUND(b.Total - ISNULL(f.dc_ValorPago, 0), 2) AS Saldo, a.vt_fech_venc, DATEDIFF(day, a.vt_fech_venc, @FechaCorte) AS Plazo, j.smes, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
                  nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, h.IdSede, h.IdNivel, h.IdJornada, h.IdCurso, h.IdParalelo,
				  CASE WHEN DATEDIFF(day,a.vt_fech_venc,@FechaCorte) <= 30 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_0_30,
					CASE WHEN DATEDIFF(day,a.vt_fech_venc,@FechaCorte) BETWEEN 31 AND 60 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_31_60,
					CASE WHEN DATEDIFF(day,a.vt_fech_venc,@FechaCorte) BETWEEN 61 AND 90 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_61_90,
					CASE WHEN DATEDIFF(day,a.vt_fech_venc,@FechaCorte) BETWEEN 91 AND 180 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_91_180,
					CASE WHEN DATEDIFF(day,a.vt_fech_venc,@FechaCorte) >= 181 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_181
FROM     aca_AnioLectivo_Jornada_Curso AS jc LEFT OUTER JOIN
                  aca_AnioLectivo_NivelAcademico_Jornada AS nj ON jc.IdEmpresa = nj.IdEmpresa AND jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel AND jc.IdJornada = nj.IdJornada RIGHT OUTER JOIN
                  aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso RIGHT OUTER JOIN
                  fa_factura AS a INNER JOIN
                  fa_factura_resumen AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdSucursal = b.IdSucursal AND a.IdBodega = b.IdBodega AND a.IdCbteVta = b.IdCbteVta INNER JOIN
                  aca_Alumno AS c ON a.IdEmpresa = c.IdEmpresa AND a.IdAlumno = c.IdAlumno INNER JOIN
                  tb_persona AS d ON c.IdPersona = d.IdPersona LEFT OUTER JOIN
                  aca_AnioLectivo AS e ON b.IdEmpresa = e.IdEmpresa AND b.IdAnio = e.IdAnio left join
(
	select B.IdEmpresa, B.IdSucursal, B.IdBodega_Cbte, B.IdCbte_vta_nota, B.dc_TipoDocumento, SUM(B.dc_ValorPago) dc_ValorPago 
	from cxc_cobro as a inner join 
	cxc_cobro_det as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdCobro = b.IdCobro
	where a.cr_estado = 'A' and a.IdEmpresa = @IdEmpresa and a.cr_fecha <= @FechaCorte
	group by B.IdEmpresa, B.IdSucursal, B.IdBodega_Cbte, B.IdCbte_vta_nota, B.dc_TipoDocumento
) as f on a.IdEmpresa = f.IdEmpresa and a.IdSucursal = f.IdSucursal and a.IdBodega = f.IdBodega_Cbte and a.IdCbteVta = f.IdCbte_vta_nota and a.vt_tipoDoc = f.dc_TipoDocumento INNER JOIN
aca_Matricula_Rubro AS g ON a.IdEmpresa = g.IdEmpresa AND a.IdSucursal = g.IdSucursal AND a.IdBodega = g.IdBodega AND a.IdCbteVta = g.IdCbteVta INNER JOIN
aca_Matricula AS h ON g.IdEmpresa = h.IdEmpresa AND g.IdMatricula = h.IdMatricula ON cp.IdEmpresa = h.IdEmpresa AND cp.IdAnio = h.IdAnio AND cp.IdSede = h.IdSede AND cp.IdNivel = h.IdNivel AND cp.IdJornada = h.IdJornada AND 
cp.IdCurso = h.IdCurso AND cp.IdParalelo = h.IdParalelo LEFT OUTER JOIN
aca_AnioLectivo_Periodo AS i ON g.IdEmpresa = i.IdEmpresa AND g.IdAnio = i.IdAnio AND g.IdPeriodo = i.IdPeriodo LEFT OUTER JOIN
tb_mes AS j ON i.IdMes = j.idMes LEFT OUTER JOIN
aca_AnioLectivo_Sede_NivelAcademico AS sn ON nj.IdEmpresa = sn.IdEmpresa and nj.IdAnio = sn.IdAnio and sn.IdSede = nj.IdSede and sn.IdNivel = nj.IdNivel 
where a.IdEmpresa = @IdEmpresa and a.Estado = 'A' AND a.vt_fecha <= @FechaCorte and round(b.Total - isnull(f.dc_ValorPago,0),2) > 0 


UNION ALL


select a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdNota, a.no_fecha, a.sc_observacion, CASE WHEN A.NaturalezaNota = 'SRI' THEN A.Serie1+'-'+A.Serie2+'-'+ A.NumNota_Impresa ELSE ISNULL(A.CodNota,cast(A.IdNota as varchar)) END NumNota_Impresa  ,c.IdAlumno, c.Codigo as CodigoAlumno, d.pe_nombreCompleto, b.IdAnio, year(isnull(e.FechaDesde,a.no_fecha)) as Periodo, b.Total, isnull(f.dc_ValorPago,0) as TotalPagado, round(b.Total - isnull(f.dc_ValorPago,0),2) as Saldo,
a.no_fecha_venc, DATEDIFF(day,a.no_fecha_venc,@FechaCorte) as Plazo, h.smes, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, g.IdSede, g.IdNivel, g.IdJornada, g.IdCurso, g.IdParalelo,
CASE WHEN DATEDIFF(day,a.no_fecha_venc,@FechaCorte) <= 30 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_0_30,
CASE WHEN DATEDIFF(day,a.no_fecha_venc,@FechaCorte) BETWEEN 31 AND 60 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_31_60,
CASE WHEN DATEDIFF(day,a.no_fecha_venc,@FechaCorte) BETWEEN 61 AND 90 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_61_90,
CASE WHEN DATEDIFF(day,a.no_fecha_venc,@FechaCorte) BETWEEN 91 AND 180 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_91_180,
CASE WHEN DATEDIFF(day,a.no_fecha_venc,@FechaCorte) >= 181 THEN round(b.Total - isnull(f.dc_ValorPago,0),2) ELSE 0 END AS VENCIDO_181
from fa_notaCreDeb as a inner join
fa_notaCreDeb_resumen as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdBodega = b.IdBodega and a.IdNota = b.IdNota inner join
aca_Alumno as c on a.IdEmpresa = c.IdEmpresa and a.IdAlumno = c.IdAlumno inner join
tb_persona as d on c.IdPersona = d.IdPersona left join
aca_AnioLectivo as e on b.IdEmpresa = e.IdEmpresa and b.IdAnio = e.IdAnio left join
(
	select B.IdEmpresa, B.IdSucursal, B.IdBodega_Cbte, B.IdCbte_vta_nota, B.dc_TipoDocumento, SUM(B.dc_ValorPago) dc_ValorPago 
	from cxc_cobro as a inner join 
	cxc_cobro_det as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdCobro = b.IdCobro
	where a.cr_estado = 'A' and a.IdEmpresa = @IdEmpresa and a.cr_fecha <= @FechaCorte
	group by B.IdEmpresa, B.IdSucursal, B.IdBodega_Cbte, B.IdCbte_vta_nota, B.dc_TipoDocumento
) as f on a.IdEmpresa = f.IdEmpresa and a.IdSucursal = f.IdSucursal and a.IdBodega = f.IdBodega_Cbte and a.IdNota = f.IdCbte_vta_nota and a.CodDocumentoTipo = f.dc_TipoDocumento LEFT JOIN
aca_Matricula AS g on b.IdEmpresa = g.IdEmpresa and b.IdMatricula = g.IdMatricula left outer join
tb_mes as h on h.idMes = month(a.no_fecha ) left outer join
aca_AnioLectivo_Curso_Paralelo as cp on g.IdEmpresa = cp.IdEmpresa and g.IdSede = cp.IdSede and g.IdNivel = cp.IdNivel and cp.IdJornada = g.IdJornada and cp.IdCurso = g.IdCurso and cp.IdParalelo = g.IdParalelo and cp.IdAnio = g.IdAnio left outer join
aca_AnioLectivo_Jornada_Curso as jc on cp.IdEmpresa = jc.IdEmpresa and cp.IdAnio = jc.IdAnio and cp.IdSede = jc.IdSede and cp.IdNivel = jc.IdNivel and cp.IdJornada = jc.IdJornada and cp.IdCurso = jc.IdCurso left outer join
aca_AnioLectivo_NivelAcademico_Jornada as nj on jc.IdEmpresa = nj.IdEmpresa and jc.IdAnio = nj.IdAnio and jc.IdSede = nj.IdSede and jc.IdNivel = nj.IdNivel and jc.IdJornada = nj.IdJornada left outer join
aca_AnioLectivo_Sede_NivelAcademico as sn on sn.IdEmpresa = nj.IdEmpresa and sn.IdAnio = nj.IdAnio and sn.IdSede = nj.IdSede and sn.IdNivel = nj.IdNivel 
where a.IdEmpresa = @IdEmpresa and a.Estado = 'A' AND a.no_fecha <= @FechaCorte and round(b.Total - isnull(f.dc_ValorPago,0),2) > 0 and a.CreDeb = 'D'