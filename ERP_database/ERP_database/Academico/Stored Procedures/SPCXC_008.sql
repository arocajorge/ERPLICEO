--EXEC Academico.SPCXC_008 1 ,'2020/4/20',1,0,0,0,0,0,0
CREATE PROCEDURE [Academico].[SPCXC_008]
(
@IdEmpresa int,
@FechaCorte date,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdAlumno numeric
)
AS

select a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdCbteVta, a.vt_fecha, a.vt_Observacion, A.vt_serie1+'-'+A.vt_serie2+'-'+ A.vt_NumFactura vt_NumFactura, c.IdAlumno, c.Codigo as CodigoAlumno, d.pe_nombreCompleto, year(isnull(e.FechaDesde,a.vt_fecha)) as Periodo, b.Total, isnull(f.dc_ValorPago,0) as TotalPagado, round(b.Total - isnull(f.dc_ValorPago,0),2) as Saldo,
a.vt_fech_venc, DATEDIFF(day,a.vt_fech_venc,@FechaCorte) as Plazo, 
g.IdMatricula, g.IdAnio, g.IdSede, g.IdNivel, g.IdJornada, g.IdCurso, g.IdParalelo,
g.OrdenNivel, g.OrdenJornada, g.OrdenCurso, g.OrdenParalelo,
g.NomSede, g.NomNivel, g.NomJornada, g.NomCurso, g.NomParalelo
from fa_factura as a inner join
fa_factura_resumen as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdBodega = b.IdBodega and a.IdCbteVta = b.IdCbteVta inner join
aca_Alumno as c on a.IdEmpresa = c.IdEmpresa and a.IdAlumno = c.IdAlumno inner join
tb_persona as d on c.IdPersona = d.IdPersona left join
aca_AnioLectivo as e on b.IdEmpresa = e.IdEmpresa and b.IdAnio = e.IdAnio left join
(
	select B.IdEmpresa, B.IdSucursal, B.IdBodega_Cbte, B.IdCbte_vta_nota, B.dc_TipoDocumento, SUM(B.dc_ValorPago) dc_ValorPago 
	from cxc_cobro as a inner join 
	cxc_cobro_det as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdCobro = b.IdCobro
	where a.cr_estado = 'A' and a.IdEmpresa = @IdEmpresa and a.cr_fecha <= @FechaCorte
	group by B.IdEmpresa, B.IdSucursal, B.IdBodega_Cbte, B.IdCbte_vta_nota, B.dc_TipoDocumento
) as f on a.IdEmpresa = f.IdEmpresa and a.IdSucursal = f.IdSucursal and a.IdBodega = f.IdBodega_Cbte and a.IdCbteVta = f.IdCbte_vta_nota and a.vt_tipoDoc = f.dc_TipoDocumento left join
(
	SELECT m.IdEmpresa, m.IdMatricula, m.IdAlumno, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, 
		cp.OrdenParalelo, a.Descripcion AS DescripcionAnio, a.EnCurso, a.Estado
	FROM     aca_AnioLectivo_Jornada_Curso AS jc LEFT OUTER JOIN
		aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
		aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdNivel = nj.IdNivel AND sn.IdSede = nj.IdSede AND sn.IdAnio = nj.IdAnio AND sn.IdEmpresa = nj.IdEmpresa ON jc.IdEmpresa = nj.IdEmpresa AND jc.IdAnio = nj.IdAnio AND 
		jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel AND jc.IdJornada = nj.IdJornada RIGHT OUTER JOIN
		aca_AnioLectivo_Curso_Paralelo AS cp RIGHT OUTER JOIN
		aca_AnioLectivo AS a INNER JOIN
		aca_Matricula AS m ON a.IdEmpresa = m.IdEmpresa AND a.IdAnio = m.IdAnio ON cp.IdEmpresa = m.IdEmpresa AND cp.IdAnio = m.IdAnio AND cp.IdSede = m.IdSede AND cp.IdNivel = m.IdNivel AND cp.IdJornada = m.IdJornada AND 
		cp.IdCurso = m.IdCurso AND cp.IdParalelo = m.IdParalelo ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND 
		jc.IdCurso = cp.IdCurso
	WHERE  (a.EnCurso = 1) AND (a.Estado = 1) 
	and m.IdAnio = @IdAnio
	and m.IdSede = iif(@IdSede = 0, m.IdSede, @IdSede)
	and m.IdNivel = iif(@IdNivel = 0, m.IdNivel, @IdNivel)
	and m.IdJornada = iif(@IdJornada = 0, m.IdJornada, @IdJornada)
	and m.IdCurso = iif(@IdCurso = 0, m.IdCurso, @IdCurso)
	and m.IdParalelo = iif(@IdParalelo = 0, m.IdParalelo, @IdParalelo)
) as g on a.IdEmpresa = g.IdEmpresa and a.IdAlumno = g.IdAlumno
where a.IdEmpresa = @IdEmpresa and a.Estado = 'A' AND a.vt_fecha <= @FechaCorte and round(b.Total - isnull(f.dc_ValorPago,0),2) > 0 
and a.IdAlumno = iif(@IdAlumno = 0, a.IdAlumno, @IdAlumno) --and g.IdMatricula is not null
UNION ALL
select a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdNota, a.no_fecha, a.sc_observacion, CASE WHEN A.NaturalezaNota = 'SRI' THEN A.Serie1+'-'+A.Serie2+'-'+ A.NumNota_Impresa ELSE ISNULL(A.CodNota,cast(A.IdNota as varchar)) END NumNota_Impresa  ,c.IdAlumno, c.Codigo as CodigoAlumno, d.pe_nombreCompleto, year(isnull(e.FechaDesde,a.no_fecha)) as Periodo, b.Total, isnull(f.dc_ValorPago,0) as TotalPagado, round(b.Total - isnull(f.dc_ValorPago,0),2) as Saldo,
a.no_fecha_venc, DATEDIFF(day,a.no_fecha_venc,@FechaCorte) as Plazo,
g.IdMatricula, g.IdAnio, g.IdSede, g.IdNivel, g.IdJornada, g.IdCurso, g.IdParalelo,
g.OrdenNivel, g.OrdenJornada, g.OrdenCurso, g.OrdenParalelo,
g.NomSede, g.NomNivel, g.NomJornada, g.NomCurso, g.NomParalelo
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
) as f on a.IdEmpresa = f.IdEmpresa and a.IdSucursal = f.IdSucursal and a.IdBodega = f.IdBodega_Cbte and a.IdNota = f.IdCbte_vta_nota and a.CodDocumentoTipo = f.dc_TipoDocumento left join
(
	SELECT m.IdEmpresa, m.IdMatricula, m.IdAlumno, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, 
		cp.OrdenParalelo, a.Descripcion AS DescripcionAnio, a.EnCurso, a.Estado
	FROM     aca_AnioLectivo_Jornada_Curso AS jc LEFT OUTER JOIN
		aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
		aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdNivel = nj.IdNivel AND sn.IdSede = nj.IdSede AND sn.IdAnio = nj.IdAnio AND sn.IdEmpresa = nj.IdEmpresa ON jc.IdEmpresa = nj.IdEmpresa AND jc.IdAnio = nj.IdAnio AND 
		jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel AND jc.IdJornada = nj.IdJornada RIGHT OUTER JOIN
		aca_AnioLectivo_Curso_Paralelo AS cp RIGHT OUTER JOIN
		aca_AnioLectivo AS a INNER JOIN
		aca_Matricula AS m ON a.IdEmpresa = m.IdEmpresa AND a.IdAnio = m.IdAnio ON cp.IdEmpresa = m.IdEmpresa AND cp.IdAnio = m.IdAnio AND cp.IdSede = m.IdSede AND cp.IdNivel = m.IdNivel AND cp.IdJornada = m.IdJornada AND 
		cp.IdCurso = m.IdCurso AND cp.IdParalelo = m.IdParalelo ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND 
		jc.IdCurso = cp.IdCurso
	WHERE  (a.EnCurso = 1) AND (a.Estado = 1)
	and m.IdAnio = @IdAnio
	and m.IdSede = iif(@IdSede = 0, m.IdSede, @IdSede)
	and m.IdNivel = iif(@IdNivel = 0, m.IdNivel, @IdNivel)
	and m.IdJornada = iif(@IdJornada = 0, m.IdJornada, @IdJornada)
	and m.IdCurso = iif(@IdCurso = 0, m.IdCurso, @IdCurso)
	and m.IdParalelo = iif(@IdParalelo = 0, m.IdParalelo, @IdParalelo)
) as g on a.IdEmpresa = g.IdEmpresa and a.IdAlumno = g.IdAlumno
where a.IdEmpresa = @IdEmpresa and a.Estado = 'A' AND a.no_fecha <= @FechaCorte and round(b.Total - isnull(f.dc_ValorPago,0),2) > 0 and a.CreDeb = 'D'
and a.IdAlumno = iif(@IdAlumno = 0, a.IdAlumno, @IdAlumno) --and g.IdMatricula is not null