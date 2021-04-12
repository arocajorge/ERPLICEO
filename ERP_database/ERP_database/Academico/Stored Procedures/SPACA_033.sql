CREATE PROCEDURE [Academico].[SPACA_033]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdMateria int
)
AS
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, 
dbo.aca_AnioLectivo_Curso_Materia.NomMateria, dbo.aca_AnioLectivo_Curso_Materia.NomMateriaArea, dbo.aca_AnioLectivo_Curso_Materia.NomMateriaGrupo, 
                  dbo.aca_AnioLectivo_Curso_Materia.EsObligatorio, dbo.aca_AnioLectivo_Curso_Materia.OrdenMateriaArea, dbo.aca_AnioLectivo_Curso_Materia.OrdenMateriaGrupo, dbo.aca_AnioLectivo_Curso_Materia.OrdenMateria, 
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
                  nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pp.pe_nombreCompleto as NombreProfesor,
				  mc.CalificacionP1, ep1.Codigo AS EquivalenciaPromedioP1,
				  mc.CalificacionP2, ep2.Codigo AS EquivalenciaPromedioP2,
				  mc.CalificacionP3, ep3.Codigo AS EquivalenciaPromedioP3,
				  mc.PromedioQ1, 
				  mc.ExamenQ1, epEQ1.Codigo AS EquivalenciaPromedioEQ1,
				  mc.PromedioFinalQ1, epQ1.Codigo AS EquivalenciaPromedioQ1,
				  mc.CalificacionP4, ep4.Codigo AS EquivalenciaPromedioP4,
                  mc.CalificacionP5, ep5.Codigo AS EquivalenciaPromedioP5,
				  mc.CalificacionP6, ep6.Codigo AS EquivalenciaPromedioP6,
				  mc.PromedioQ2, 
				  mc.ExamenQ2, epEQ2.Codigo AS EquivalenciaPromedioEQ2,
				  mc.PromedioFinalQ2, epQ2.Codigo AS EquivalenciaPromedioQ2,
				  mc.PromedioQuimestres as PromedioQuimestres_PF,
				  case when (mc.ExamenMejoramiento is not null) then mc.PromedioFinal
					else  mc.PromedioQuimestres end as Promedio_PR, 
				  mc.ExamenMejoramiento, mc.CampoMejoramiento, mc.ExamenSupletorio, mc.ExamenRemedial, mc.ExamenGracia, 
				  mc.PromedioFinal, mc.IdEquivalenciaPromedioPF, epPF.Codigo AS EquivalenciaPromedioPF
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
                  dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
                  dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia with (nolock) ON m.IdEmpresa = dbo.aca_AnioLectivo_Curso_Materia.IdEmpresa AND m.IdAnio = dbo.aca_AnioLectivo_Curso_Materia.IdAnio AND m.IdSede = dbo.aca_AnioLectivo_Curso_Materia.IdSede AND 
                  m.IdNivel = dbo.aca_AnioLectivo_Curso_Materia.IdNivel AND m.IdJornada = dbo.aca_AnioLectivo_Curso_Materia.IdJornada AND m.IdCurso = dbo.aca_AnioLectivo_Curso_Materia.IdCurso AND 
                  mc.IdMateria = dbo.aca_AnioLectivo_Curso_Materia.IdMateria 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep1 with (nolock) ON m.IdAnio = ep1.IdAnio AND mc.IdEmpresa = ep1.IdEmpresa AND mc.IdEquivalenciaPromedioP1 = ep1.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep2 with (nolock) ON m.IdAnio = ep2.IdAnio AND mc.IdEmpresa = ep2.IdEmpresa AND mc.IdEquivalenciaPromedioP2 = ep2.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep3 with (nolock) ON m.IdAnio = ep3.IdAnio AND mc.IdEmpresa = ep3.IdEmpresa AND mc.IdEquivalenciaPromedioP3 = ep3.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS epEQ1 with (nolock) ON m.IdAnio = epEQ1.IdAnio AND mc.IdEmpresa = epEQ1.IdEmpresa AND mc.IdEquivalenciaPromedioEQ1 = epEQ1.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS epQ1 with (nolock) ON m.IdAnio = epQ1.IdAnio AND mc.IdEmpresa = epQ1.IdEmpresa AND mc.IdEquivalenciaPromedioQ1 = epQ1.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep4 with (nolock) ON m.IdAnio = ep4.IdAnio AND mc.IdEmpresa = ep4.IdEmpresa AND mc.IdEquivalenciaPromedioP4 = ep4.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep5 with (nolock) ON m.IdAnio = ep5.IdAnio AND mc.IdEmpresa = ep5.IdEmpresa AND mc.IdEquivalenciaPromedioP5 = ep5.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep6 with (nolock) ON m.IdAnio = ep6.IdAnio AND mc.IdEmpresa = ep6.IdEmpresa AND mc.IdEquivalenciaPromedioP6 = ep6.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS epEQ2 with (nolock) ON m.IdAnio = epEQ2.IdAnio AND mc.IdEmpresa = epEQ2.IdEmpresa AND mc.IdEquivalenciaPromedioEQ2 = epEQ2.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS epQ2 with (nolock) ON m.IdAnio = epQ2.IdAnio AND mc.IdEmpresa = epQ2.IdEmpresa AND mc.IdEquivalenciaPromedioQ2 = epQ2.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS epPF with (nolock) ON m.IdAnio = epPF.IdAnio AND mc.IdEmpresa = epPF.IdEmpresa AND mc.IdEquivalenciaPromedioPF = epPF.IdEquivalenciaPromedio
				  LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
                  m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
				  LEFT OUTER JOIN aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa and mc.IdProfesor = pro.IdProfesor
				  LEFT OUTER JOIN tb_persona as pp with (nolock) on pp.IdPersona = pro.IdPersona
				  LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
					where r.Estado = 1
					) as ret on m.IdEmpresa =ret.IdEmpresa and m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and mc.IdMateria = @IdMateria
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and isnull(ret.IdMatricula,0) = 0
UNION ALL
(

--- CALIFICACIONES CUALITATIVAS  
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaArea, a.NomMateriaGrupo, a.EsObligatorio, a.OrdenMateriaArea, a.OrdenMateriaGrupo, a.OrdenMateria,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, 
a.NombreProfesor,
max(a.CalificacionP1) CalificacionP1, null EquivalenciaP1,
max(a.CalificacionP2) CalificacionP2, null EquivalenciaP2,
max(a.CalificacionP3) CalificacionP3, null EquivalenciaP3,
null PromedioQ1, null ExamenQ1, null EquivalenciaPromedioEQ1, null PromedioFinalQ1, null EquivalenciaPromedioQ1,
max(a.CalificacionP4) CalificacionP4,null EquivalenciaP4, 
max(a.CalificacionP5) CalificacionP5, null EquivalenciaP5, 
max(a.CalificacionP6) CalificacionP6, null EquivalenciaP6,
null PromedioQ2, null ExamenQ2, null EquivalenciaPromedioEQ2, null PromedioFinalQ2, null EquivalenciaPromedioQ2,
null PromedioQuimestralFinal, null PromedioMejoramiento, null ExamenMejoramiento, null CampoMejoramiento, null ExamenSupletorio, null ExamenRemedial, null ExamenGracia, 
null PromedioFinal, null IdEquivalenciaPromedioPF, null EquivalenciaPromedioPF
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaArea, MC.NomMateriaGrupo, MC.EsObligatorio, MC.OrdenMateria, 
                  MC.OrdenMateriaGrupo, MC.OrdenMateriaArea, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
                  cp.NomParalelo, cp.OrdenParalelo, pp.pe_nombreCompleto AS NombreProfesor,
				  CASE WHEN IdCatalogoParcial = 28 THEN acc.Codigo END AS CalificacionP1, 
				  CASE WHEN IdCatalogoParcial = 29 THEN acc.Codigo END AS CalificacionP2, 
                  CASE WHEN IdCatalogoParcial = 30 THEN acc.Codigo END AS CalificacionP3, 
				  CASE WHEN IdCatalogoParcial = 31 THEN acc.Codigo END AS CalificacionP4, 
				  CASE WHEN IdCatalogoParcial = 32 THEN acc.Codigo END AS CalificacionP5, 
                  CASE WHEN IdCatalogoParcial = 33 THEN acc.Codigo END AS CalificacionP6
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) INNER JOIN
                  dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
                  dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona LEFT OUTER JOIN
                  dbo.aca_AnioLectivoCalificacionCualitativa AS acc with (nolock) ON acc.IdEmpresa = a.IdEmpresa AND acc.IdAnio = m.IdAnio AND acc.IdCalificacionCualitativa = a.IdCalificacionCualitativa LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
                  jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
                  cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
                  m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
                   m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa and a.IdProfesor = pro.IdProfesor
				  LEFT OUTER JOIN tb_persona as pp with (nolock) on pp.IdPersona = pro.IdPersona
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
					where r.Estado = 1
					) as ret on m.IdEmpresa =ret.IdEmpresa and m.IdMatricula = ret.IdMatricula
--WHERE  (a.IdEmpresa = 1) AND (m.IdAnio = 1) AND (m.IdSede = 1) AND (m.IdNivel = 4) AND (m.IdJornada = 1) AND (m.IdCurso = 7) AND (m.IdParalelo = 1)
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and a.IdMateria = @IdMateria
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and isnull(ret.IdMatricula,0) = 0
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaArea, a.NomMateriaGrupo, a.EsObligatorio, a.OrdenMateriaArea, a.OrdenMateriaGrupo, a.OrdenMateria,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreProfesor
)