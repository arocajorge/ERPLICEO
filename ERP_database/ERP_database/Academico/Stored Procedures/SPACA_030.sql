
CREATE PROCEDURE [Academico].[SPACA_030]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdAlumno decimal,
@MostrarRetirados bit
)
AS
SELECT mc.IdEmpresa, mc.IdMatricula, mc.IdMateria, alu.Codigo, p.pe_nombreCompleto, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
                  nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
				  mc.CalificacionP1, mc.CalificacionP2, mc.CalificacionP3, 
				  mc.PromedioQ1, dbo.BankersRounding((mc.PromedioQ1 * 0.80),2) as PorcentajeQ1, 
				  mc.ExamenQ1, dbo.BankersRounding((mc.ExamenQ1 * 0.20),2) as PorcentajeEQ1,  mc.PromedioFinalQ1, 
				  mc.CalificacionP4, mc.CalificacionP5, mc.CalificacionP6, 
				  mc.PromedioQ2, dbo.BankersRounding((mc.PromedioQ2 * 0.80),2) as PorcentajeQ2, 
				  mc.ExamenQ2, dbo.BankersRounding((mc.ExamenQ2 * 0.20),2) as PorcentajeEQ2, mc.PromedioFinalQ2,
				  dbo.BankersRounding(( (mc.PromedioFinalQ1 + mc.PromedioFinalQ2) / 2), 2) AS PromedioQuimestralFinal, 
				  mc.ExamenMejoramiento, mc.CampoMejoramiento, mc.ExamenSupletorio, mc.ExamenRemedial, mc.ExamenGracia, mc.PromedioFinal, 
                  dbo.aca_AnioLectivo_Curso_Materia.NomMateria, dbo.aca_AnioLectivo_Curso_Materia.NomMateriaArea, 
                  dbo.aca_AnioLectivo_Curso_Materia.NomMateriaGrupo, dbo.aca_AnioLectivo_Curso_Materia.EsObligatorio, dbo.aca_AnioLectivo_Curso_Materia.OrdenMateria, dbo.aca_AnioLectivo_Curso_Materia.OrdenMateriaGrupo, 
                  dbo.aca_AnioLectivo_Curso_Materia.OrdenMateriaArea, cp.IdProfesorInspector, pins.pe_nombreCompleto AS NombreInspector, cp.IdProfesorTutor, ptut.pe_nombreCompleto NombreTutor
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
                  dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
                  dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia ON m.IdEmpresa = dbo.aca_AnioLectivo_Curso_Materia.IdEmpresa AND m.IdAnio = dbo.aca_AnioLectivo_Curso_Materia.IdAnio AND m.IdSede = dbo.aca_AnioLectivo_Curso_Materia.IdSede AND 
                  m.IdNivel = dbo.aca_AnioLectivo_Curso_Materia.IdNivel AND m.IdJornada = dbo.aca_AnioLectivo_Curso_Materia.IdJornada AND m.IdCurso = dbo.aca_AnioLectivo_Curso_Materia.IdCurso AND 
                  mc.IdMateria = dbo.aca_AnioLectivo_Curso_Materia.IdMateria 
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