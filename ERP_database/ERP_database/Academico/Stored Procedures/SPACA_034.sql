CREATE PROCEDURE [Academico].[SPACA_034]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdAlumno numeric,
@MostrarRetirados bit
)
AS
--- NO SE PROMEDIAN ---
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
                  CM.NomMateria, CM.NomMateriaArea, 
                  CM.NomMateriaGrupo, 
				  ISNULL(CM.OrdenMateria,0) OrdenMateria, ISNULL(CM.OrdenMateriaGrupo,2) OrdenMateriaGrupo, 
                  ISNULL(CM.OrdenMateriaArea,0) OrdenMateriaArea, 
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
                  nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo,  
				  cast(mc.PromedioFinalQ1 as varchar) PromedioFinalQ1, 
				  cast (mc.PromedioFinalQ2 as varchar) PromedioFinalQ2,
				  CASE WHEN (mc.ExamenSupletorio is null) then null
				  ELSE
					CASE WHEN (mc.ExamenSupletorio < AN.PromedioMinimoPromocion) THEN
						CASE WHEN (mc.ExamenRemedial is null) THEN null  ELSE 
							CASE WHEN (mc.ExamenRemedial < AN.PromedioMinimoPromocion) THEN
								CASE WHEN (mc.ExamenGracia is null) THEN null ELSE  mc.ExamenGracia END
							ELSE mc.ExamenRemedial END
						END
					ELSE mc.ExamenSupletorio END 
					END Supletorio,
				  cast(mc.PromedioFinal as varchar) PromedioFinal
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
                  dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
                  dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
                  m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
                  mc.IdMateria = CM.IdMateria 
				  LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
                  m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
				  LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut ON ptut.IdPersona = pt.IdPersona 
				  LEFT OUTER JOIN
                      (SELECT IdEmpresa, IdMatricula
                       FROM      dbo.aca_AlumnoRetiro AS r
                       WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40

--- SE PROMEDIAN ---
UNION ALL
(
select 
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, 
999999 IdMateria, 'OPTATIVA' NomMateria,'OPTATIVA' NomMateriaArea, a.NomMateriaGrupo, 0 OrdenMateriaArea, a.OrdenMateriaGrupo,
0 OrdenMateria, a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede,
a.NomNivel, a.OrdenNivel,a.NomJornada, a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,
a.PromedioFinalQ1,a.PromedioFinalQ2, a.Supletorio, a.PromedioFinal
from (
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula,
cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, AN.PromedioMinimoPromocion,
CAST(dbo.BankersRounding(AVG(mc.PromedioFinalQ1),2) as varchar) PromedioFinalQ1,
CAST(dbo.BankersRounding(AVG(mc.PromedioFinalQ2),2) as varchar) PromedioFinalQ2,
CASE WHEN (AVG(mc.ExamenSupletorio) is null) then null
	ELSE
	CASE WHEN (AVG(mc.ExamenSupletorio) < AN.PromedioMinimoPromocion) THEN
		CASE WHEN (AVG(mc.ExamenRemedial) is null) THEN null  ELSE 
			CASE WHEN (AVG(mc.ExamenRemedial) < AN.PromedioMinimoPromocion) THEN
				CASE WHEN (AVG(mc.ExamenGracia) is null) THEN null ELSE  CAST(dbo.BankersRounding(AVG(mc.ExamenGracia),2) as varchar) END
			ELSE CAST(dbo.BankersRounding(AVG(mc.ExamenRemedial),2) as varchar)END
		END
	ELSE CAST(dbo.BankersRounding(AVG(mc.ExamenSupletorio),2) as varchar)END 
	END Supletorio,
CAST(dbo.BankersRounding(AVG(mc.PromedioFinal),2) as varchar) PromedioFinal
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
    dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
	LEFT JOIN	
	(
	select r.IdEmpresa, r.IdMatricula 
	from aca_AlumnoRetiro as r
	where r.Estado = 1
	) as ret on m.IdEmpresa =ret.IdEmpresa and m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = 1 
and m.IdAnio = 1
and m.IdSede = 1
and m.IdJornada = 1
and m.IdNivel = 4
and m.IdCurso = 8
and m.IdParalelo = case when 1 = 0 then m.IdParalelo else 1 end
and m.IdAlumno = case when 4007 = 0 then m.IdAlumno else 4007 end
and isnull(ret.IdMatricula,0) = case when 0 = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
group by
mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula,
cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, AN.PromedioMinimoPromocion
)a
)

--- CUALITATIVAS ---
UNION ALL
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaArea, a.NomMateriaGrupo, ISNULL(a.OrdenMateriaArea,0) OrdenMateriaGrupo, 
ISNULL(a.OrdenMateriaGrupo,0) OrdenMateriaGrupo, ISNULL(a.OrdenMateria,0) OrdenMateria,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, 
cast(max(a.CalificacionQ1) as varchar)  PromedioQ1,
cast(max(a.CalificacionQ2) as varchar) PromedioQ2, 
cast(null as varchar) Supletorio,
cast(max(a.CalificacionPF) as varchar) PromedioFinal
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaArea, MC.NomMateriaGrupo, MC.EsObligatorio, MC.OrdenMateria, 
                  MC.OrdenMateriaGrupo, MC.OrdenMateriaArea, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
                  cp.NomParalelo, cp.OrdenParalelo, 
				  epQ1.Codigo CalificacionQ1,
				  epQ2.Codigo CalificacionQ2,
				  epPF.Codigo CalificacionPF
FROM     dbo.aca_MatriculaCalificacionCualitativaPromedio AS a 
INNER JOIN
    dbo.aca_Matricula AS m ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ1 ON m.IdAnio = epQ1.IdAnio AND a.IdEmpresa = epQ1.IdEmpresa AND a.IdCalificacionCualitativaQ1 = epQ1.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ2 ON m.IdAnio = epQ2.IdAnio AND a.IdEmpresa = epQ2.IdEmpresa AND a.IdCalificacionCualitativaQ2 = epQ2.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epPF ON m.IdAnio = epPF.IdAnio AND a.IdEmpresa = epPF.IdEmpresa AND a.IdCalificacionCualitativaFinal = epPF.IdCalificacionCualitativa	
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT JOIN	
	(
	select r.IdEmpresa, r.IdMatricula 
	from aca_AlumnoRetiro as r
	where r.Estado = 1
	) as ret on m.IdEmpresa =ret.IdEmpresa and m.IdMatricula = ret.IdMatricula
--WHERE  (a.IdEmpresa = 1) AND (m.IdAnio = 1) AND (m.IdSede = 1) AND (m.IdNivel = 4) AND (m.IdJornada = 1) AND (m.IdCurso = 7) AND (m.IdParalelo = 1)
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = case when @IdNivel = 0 then m.IdNivel else @IdNivel end
and m.IdCurso = case when @IdCurso = 0 then m.IdCurso else @IdCurso end
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaArea, a.NomMateriaGrupo, a.OrdenMateriaArea, a.OrdenMateriaGrupo, a.OrdenMateria,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo
)
--- CONDUCTA ---
UNION ALL
(
SELECT mco.IdEmpresa, m.IdAnio, m.IdSede,m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo,m.IdAlumno, mco.IdMatricula, 
9999 IdMateria, cast('CONDUCTA' as varchar(500)) NombreMateria, cast('CONDUCTA' as varchar(500)) NombreArea,cast('CONDUCTA' as varchar(500)) NombreGrupo,
9999 OrdenMateria, 9999 OrdenGrupo, 9999 OrdenArea,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
                  nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
cast(equivq1.Letra as varchar) as PromedioFinalQ1,
cast(equivq2.Letra as varchar) as PromedioFinalQ2,
cast(null as varchar)  Supletorio,
cast(equivf.Letra as varchar) as PromedioQuimestralFinal
FROM     dbo.aca_MatriculaConducta AS mco 
INNER JOIN dbo.aca_Matricula AS m ON mco.IdEmpresa = m.IdEmpresa AND mco.IdMatricula = m.IdMatricula
inner join dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno 
INNER JOIN dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona 
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equivq1 on equivq1.IdEmpresa=mco.IdEmpresa and equivq1.IdAnio=m.IdAnio and equivq1.Secuencia = mco.SecuenciaPromedioFinalQ1
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equivq2 on equivq2.IdEmpresa=mco.IdEmpresa and equivq2.IdAnio=m.IdAnio and equivq2.Secuencia = mco.SecuenciaPromedioFinalQ2
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equivf on equivf.IdEmpresa=mco.IdEmpresa and equivf.IdAnio=m.IdAnio and equivf.Secuencia = mco.SecuenciaPromedioFinal
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mco.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
)

--exec Academico.spaca_034 1,1,1,4,1,8,1,4007,0