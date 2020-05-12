CREATE PROCEDURE [Academico].[SPACA_014]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int
)
AS
SELECT mc.IdEmpresa, mc.IdMatricula, mc.IdMateria, alu.Codigo, p.pe_nombreCompleto, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
                  nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
				  mc.CalificacionP1, mc.IdEquivalenciaPromedioP1, ep1.Codigo AS EquivalenciaPromedioP1,
				  mc.CalificacionP2, mc.IdEquivalenciaPromedioP2, ep2.Codigo AS EquivalenciaPromedioP2,
				  mc.CalificacionP3, mc.IdEquivalenciaPromedioP3, ep3.Codigo AS EquivalenciaPromedioP3,
				  mc.PromedioQ1, 
				  mc.ExamenQ1, mc.IdEquivalenciaPromedioEQ1, epEQ1.Codigo AS EquivalenciaPromedioEQ1,
				  mc.PromedioFinalQ1, mc.IdEquivalenciaPromedioQ1, epQ1.Codigo AS EquivalenciaPromedioQ1,
				  mc.CalificacionP4, mc.IdEquivalenciaPromedioP4, ep4.Codigo AS EquivalenciaPromedioP4,
                  mc.CalificacionP5, mc.IdEquivalenciaPromedioP5, ep5.Codigo AS EquivalenciaPromedioP5,
				  mc.CalificacionP6, mc.IdEquivalenciaPromedioP6, ep6.Codigo AS EquivalenciaPromedioP6,
				  mc.PromedioQ2, 
				  mc.ExamenQ2, mc.IdEquivalenciaPromedioEQ2, epEQ2.Codigo AS EquivalenciaPromedioEQ2,
				  mc.PromedioFinalQ2, mc.IdEquivalenciaPromedioQ2, epQ2.Codigo AS EquivalenciaPromedioQ2,
				  round(cast(((isnull(mc.PromedioFinalQ1,0)+isnull(mc.PromedioFinalQ2,0))/2) as float),2) as PromedioQuimestralFinal, 
				  mc.ExamenMejoramiento, mc.CampoMejoramiento, mc.ExamenSupletorio, mc.ExamenRemedial, mc.ExamenGracia, 
				  mc.PromedioFinal, mc.IdEquivalenciaPromedioPF, epPF.Codigo AS EquivalenciaPromedioPF,
                  dbo.aca_AnioLectivo_Curso_Materia.NomMateria, dbo.aca_AnioLectivo_Curso_Materia.NomMateriaArea, dbo.aca_AnioLectivo_Curso_Materia.NomMateriaGrupo, 
                  dbo.aca_AnioLectivo_Curso_Materia.EsObligatorio, dbo.aca_AnioLectivo_Curso_Materia.OrdenMateria, 
				  dbo.aca_AnioLectivo_Curso_Materia.OrdenMateriaGrupo, dbo.aca_AnioLectivo_Curso_Materia.OrdenMateriaArea
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
                  dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
                  dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia ON m.IdEmpresa = dbo.aca_AnioLectivo_Curso_Materia.IdEmpresa AND m.IdAnio = dbo.aca_AnioLectivo_Curso_Materia.IdAnio AND m.IdSede = dbo.aca_AnioLectivo_Curso_Materia.IdSede AND 
                  m.IdNivel = dbo.aca_AnioLectivo_Curso_Materia.IdNivel AND m.IdJornada = dbo.aca_AnioLectivo_Curso_Materia.IdJornada AND m.IdCurso = dbo.aca_AnioLectivo_Curso_Materia.IdCurso AND 
                  mc.IdMateria = dbo.aca_AnioLectivo_Curso_Materia.IdMateria 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep1 ON m.IdAnio = ep1.IdAnio AND mc.IdEmpresa = ep1.IdEmpresa AND mc.IdEquivalenciaPromedioP1 = ep1.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep2 ON m.IdAnio = ep2.IdAnio AND mc.IdEmpresa = ep2.IdEmpresa AND mc.IdEquivalenciaPromedioP2 = ep2.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep3 ON m.IdAnio = ep3.IdAnio AND mc.IdEmpresa = ep3.IdEmpresa AND mc.IdEquivalenciaPromedioP3 = ep3.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS epEQ1 ON m.IdAnio = epEQ1.IdAnio AND mc.IdEmpresa = epEQ1.IdEmpresa AND mc.IdEquivalenciaPromedioEQ1 = epEQ1.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS epQ1 ON m.IdAnio = epQ1.IdAnio AND mc.IdEmpresa = epQ1.IdEmpresa AND mc.IdEquivalenciaPromedioQ1 = epQ1.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep4 ON m.IdAnio = ep4.IdAnio AND mc.IdEmpresa = ep4.IdEmpresa AND mc.IdEquivalenciaPromedioP4 = ep4.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep5 ON m.IdAnio = ep5.IdAnio AND mc.IdEmpresa = ep5.IdEmpresa AND mc.IdEquivalenciaPromedioP5 = ep5.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep6 ON m.IdAnio = ep6.IdAnio AND mc.IdEmpresa = ep6.IdEmpresa AND mc.IdEquivalenciaPromedioP6 = ep6.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS epEQ2 ON m.IdAnio = epEQ2.IdAnio AND mc.IdEmpresa = epEQ2.IdEmpresa AND mc.IdEquivalenciaPromedioEQ2 = epEQ2.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS epQ2 ON m.IdAnio = epQ2.IdAnio AND mc.IdEmpresa = epQ2.IdEmpresa AND mc.IdEquivalenciaPromedioQ2 = epQ2.IdEquivalenciaPromedio 
				  LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS epPF ON m.IdAnio = epPF.IdAnio AND mc.IdEmpresa = epPF.IdEmpresa AND mc.IdEquivalenciaPromedioPF = epPF.IdEquivalenciaPromedio

				  LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
                  m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo

where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = case when @IdSede = 0 then m.IdSede else @IdSede end
and m.IdNivel = case when @IdNivel = 0 then m.IdNivel else @IdNivel end
and m.IdJornada = case when @IdJornada = 0 then m.IdJornada else @IdJornada end
and m.IdCurso = case when @IdCurso = 0 then m.IdCurso else @IdCurso end
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and not exists(
	select f.IdEmpresa from aca_AlumnoRetiro as f
	where f.IdEmpresa = mc.IdEmpresa
	and f.IdMatricula = mc.IdMatricula
	and f.Estado = 1
)