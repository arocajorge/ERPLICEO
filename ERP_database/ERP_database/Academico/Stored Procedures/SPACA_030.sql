
CREATE PROCEDURE [Academico].[SPACA_030]
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
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
                  CM.NomMateria, CM.NomMateriaArea, 
                  CM.NomMateriaGrupo, CM.EsObligatorio, 
				  ISNULL(CM.OrdenMateria,0) OrdenMateria, ISNULL(CM.OrdenMateriaGrupo,2) OrdenMateriaGrupo, 
                  ISNULL(CM.OrdenMateriaArea,0) OrdenMateriaArea, 
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
                  nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
				pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
				  cast(mc.CalificacionP1 as varchar) CalificacionP1, 
				  cast(mc.CalificacionP2 as varchar) CalificacionP2, 
				  cast(mc.CalificacionP3 as varchar) CalificacionP3,
				  cast(mc.PromedioQ1 as varchar) PromedioQ1, 
				  cast (dbo.BankersRounding((mc.PromedioQ1 * 0.80),2) as varchar)as PorcentajeQ1, 
				  cast (mc.ExamenQ1 as varchar) ExamenQ1, cast (dbo.BankersRounding((mc.ExamenQ1 * 0.20),2) as varchar) as PorcentajeEQ1,  
				  cast(mc.PromedioFinalQ1 as varchar) PromedioFinalQ1, 
				  cast (mc.CalificacionP4 as varchar) CalificacionP4, 
				  cast (mc.CalificacionP5 as varchar) CalificacionP5, 
				  cast (mc.CalificacionP6 as varchar) CalificacionP6, 
				  cast (mc.PromedioQ2 as varchar) PromedioQ2, 
				  cast(dbo.BankersRounding((mc.PromedioQ2 * 0.80),2) as varchar) as PorcentajeQ2, 
				  cast (mc.ExamenQ2 as varchar) ExamenQ2, 
				  cast(dbo.BankersRounding((mc.ExamenQ2 * 0.20),2) as varchar) as PorcentajeEQ2, 
				  cast (mc.PromedioFinalQ2 as varchar) PromedioFinalQ2,
				  cast(dbo.BankersRounding(( (mc.PromedioFinalQ1 + mc.PromedioFinalQ2) / 2), 2) as varchar) AS PromedioQuimestralFinal, 
				  cast(mc.ExamenMejoramiento as varchar) ExamenMejoramiento, cast(mc.CampoMejoramiento as varchar) CampoMejoramiento, 
				  cast(mc.ExamenSupletorio as varchar) ExamenSupletorio, cast(mc.ExamenRemedial as varchar) ExamenRemedial, 
				  cast(mc.ExamenGracia as varchar) ExamenGracia, cast(mc.PromedioFinal as varchar) PromedioFinal
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
union all
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaArea, a.NomMateriaGrupo, a.EsObligatorio, ISNULL(a.OrdenMateriaArea,0) OrdenMateriaGrupo, 
ISNULL(a.OrdenMateriaGrupo,0) OrdenMateriaGrupo, ISNULL(a.OrdenMateria,0) OrdenMateria,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, 
a.NombreInspector , a.NombreTutor,
cast(max(a.CalificacionP1) as varchar) CalificacionP1, 
cast(max(a.CalificacionP2) as varchar) CalificacionP2, 
cast(max(a.CalificacionP3) as varchar) CalificacionP3,
cast(null as varchar) PromedioQ1, cast(null as varchar) PorcentajeQ1, 
cast(null as varchar) ExamenQ1, cast(null as varchar) PorcentajeEQ1, 
cast(null as varchar) PromedioFinalQ1,
cast(max(a.CalificacionP4) as varchar) CalificacionP4, 
cast(max(a.CalificacionP5) as varchar) CalificacionP5, 
cast(max(a.CalificacionP6) as varchar) CalificacionP6, 
cast(null as varchar) PromedioQ2, cast(null as varchar) PorcentajeQ2, 
cast(null as varchar) ExamenQ2, cast(null as varchar) PorcentajeEQ2, 
cast(null as varchar) PromedioFinalQ2, 
cast(null as varchar) PromedioQuimestralFinal, 
cast(null as varchar) ExamenMejoramiento, cast(null as varchar) CampoMejoramiento, 
cast(null as varchar) ExamenSupletorio, cast(null as varchar) ExamenRemedial, 
cast(null as varchar) ExamenGracia, 
cast(null as varchar) PromedioFinal
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaArea, MC.NomMateriaGrupo, MC.EsObligatorio, MC.OrdenMateria, 
                  MC.OrdenMateriaGrupo, MC.OrdenMateriaArea, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
                  cp.NomParalelo, cp.OrdenParalelo, 
				  CASE WHEN IdCatalogoParcial = 28 THEN acc.Codigo END AS CalificacionP1, 
				  CASE WHEN IdCatalogoParcial = 29 THEN acc.Codigo END AS CalificacionP2, 
                  CASE WHEN IdCatalogoParcial = 30 THEN acc.Codigo END AS CalificacionP3, 
				  CASE WHEN IdCatalogoParcial = 31 THEN acc.Codigo END AS CalificacionP4, 
				  CASE WHEN IdCatalogoParcial = 32 THEN acc.Codigo END AS CalificacionP5, 
                  CASE WHEN IdCatalogoParcial = 33 THEN acc.Codigo END AS CalificacionP6,
				  pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a INNER JOIN
                  dbo.aca_Matricula AS m ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
                  dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona LEFT OUTER JOIN
                  dbo.aca_AnioLectivoCalificacionCualitativa AS acc ON acc.IdEmpresa = a.IdEmpresa AND acc.IdAnio = m.IdAnio AND acc.IdCalificacionCualitativa = a.IdCalificacionCualitativa LEFT OUTER JOIN
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
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut ON ptut.IdPersona = pt.IdPersona 
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
a.NomMateria, a.NomMateriaArea, a.NomMateriaGrupo, a.EsObligatorio, a.OrdenMateriaArea, a.OrdenMateriaGrupo, a.OrdenMateria,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)

union all
(
SELECT mco.IdEmpresa, m.IdAnio, m.IdSede,m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo,m.IdAlumno, mco.IdMatricula, 
9999 IdMateria, cast('CONDUCTA' as varchar(500)) NombreMateria, cast('CONDUCTA' as varchar(500)) NombreArea,cast('CONDUCTA' as varchar(500)) NombreGrupo,
cast(1 as bit) EsOBligatorio, 9999 OrdenMateria, 9999 OrdenGrupo, 9999 OrdenArea,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
                  nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
				pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
cast(equiv1.Letra as varchar) as LetraP1,
cast(equiv2.Letra as varchar) as LetraP2,
cast(equiv3.Letra as varchar) as LetraP3,
cast(null as varchar)  PromedioQ1,
cast(null as varchar)  PorcentajeQ1,
cast(null as varchar)  ExamenQ1,
cast(null as varchar)  PorcentajeExamenQ1,
cast(equivq1.Letra as varchar) as PromedioFinalQ1,
cast(equiv4.Letra as varchar) as LetraP4,
cast(equiv5.Letra as varchar) as LetraP5,
cast(equiv6.Letra as varchar) as LetraP6,
cast(null as varchar)  PromedioQ2,
cast(null as varchar)  PorcentajeQ2,
cast(null as varchar)  ExamenQ2,
cast(null as varchar)   PorcentajeExamenQ2,
cast(equivq2.Letra as varchar) as PromedioFinalQ2,
cast(equivf.Letra as varchar) as PromedioQuimestralFinal, 
cast(null as varchar)  ExamenMejoramiento, cast(null as varchar)   CampoMejoramiento, 
cast(null as varchar)  ExamenSupletorio, cast(null as varchar) ExamenRemedial, cast(null as varchar)  ExamenGracia, 
cast(null as varchar)  PromedioFinal
FROM     dbo.aca_MatriculaConducta AS mco 
INNER JOIN dbo.aca_Matricula AS m ON mco.IdEmpresa = m.IdEmpresa AND mco.IdMatricula = m.IdMatricula
inner join dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno 
INNER JOIN dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona 
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv1 on equiv1.IdEmpresa=mco.IdEmpresa and equiv1.IdAnio=m.IdAnio and equiv1.Secuencia = mco.SecuenciaPromedioFinalP1
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv2 on equiv2.IdEmpresa=mco.IdEmpresa and equiv2.IdAnio=m.IdAnio and equiv2.Secuencia = mco.SecuenciaPromedioFinalP2
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv3 on equiv3.IdEmpresa=mco.IdEmpresa and equiv3.IdAnio=m.IdAnio and equiv3.Secuencia = mco.SecuenciaPromedioFinalP3
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equivq1 on equivq1.IdEmpresa=mco.IdEmpresa and equivq1.IdAnio=m.IdAnio and equivq1.Secuencia = mco.SecuenciaPromedioFinalQ1
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv4 on equiv4.IdEmpresa=mco.IdEmpresa and equiv4.IdAnio=m.IdAnio and equiv4.Secuencia = mco.SecuenciaPromedioFinalP4
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv5 on equiv5.IdEmpresa=mco.IdEmpresa and equiv5.IdAnio=m.IdAnio and equiv5.Secuencia = mco.SecuenciaPromedioFinalP5
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv6 on equiv6.IdEmpresa=mco.IdEmpresa and equiv6.IdAnio=m.IdAnio and equiv6.Secuencia = mco.SecuenciaPromedioFinalP6
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