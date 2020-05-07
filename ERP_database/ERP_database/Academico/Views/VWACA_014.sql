create view academico.VWACA_014 as
SELECT dbo.aca_MatriculaCalificacion.IdEmpresa, dbo.aca_MatriculaCalificacion.IdMatricula, dbo.aca_Matricula.IdAnio, dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdNivel, dbo.aca_Matricula.IdJornada, dbo.aca_Matricula.IdCurso, 
                  dbo.aca_Matricula.IdParalelo, dbo.aca_Matricula.IdAlumno, dbo.aca_Alumno.IdPersona, dbo.tb_persona.pe_nombreCompleto AS Alumno, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomNivel, 
                  dbo.vwaca_AnioLectivo_Curso_Paralelo.NomJornada, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomCurso, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomParalelo, dbo.aca_Alumno.Codigo, dbo.aca_MatriculaCalificacion.IdMateria, 
                  dbo.aca_Materia.IdMateriaGrupo, dbo.aca_MateriaGrupo.NomMateriaGrupo, dbo.aca_MateriaGrupo.OrdenMateriaGrupo, dbo.aca_Materia.NomMateria, dbo.aca_Materia.OrdenMateria, dbo.aca_MatriculaCalificacion.CalificacionP1, 
                  dbo.aca_MatriculaCalificacion.CalificacionP2, dbo.aca_MatriculaCalificacion.CalificacionP3, dbo.aca_MatriculaCalificacion.PromedioQ1, dbo.aca_MatriculaCalificacion.ExamenQ1, dbo.aca_MatriculaCalificacion.PromedioFinalQ1, 
                  dbo.aca_MatriculaCalificacion.CalificacionP4, dbo.aca_MatriculaCalificacion.CalificacionP5, dbo.aca_MatriculaCalificacion.CalificacionP6, dbo.aca_MatriculaCalificacion.PromedioQ2, dbo.aca_MatriculaCalificacion.ExamenQ2, 
                  dbo.aca_MatriculaCalificacion.PromedioFinalQ2, dbo.aca_MatriculaCalificacion.ExamenMejoramiento, dbo.aca_MatriculaCalificacion.CampoMejoramiento, dbo.aca_MatriculaCalificacion.ExamenSupletorio, 
                  dbo.aca_MatriculaCalificacion.ExamenRemedial, dbo.aca_MatriculaCalificacion.ExamenGracia, dbo.aca_MatriculaCalificacion.PromedioFinal, dbo.aca_MatriculaCalificacion.IdEquivalenciaPromedio
FROM     dbo.tb_persona INNER JOIN
                  dbo.aca_Alumno ON dbo.tb_persona.IdPersona = dbo.aca_Alumno.IdPersona INNER JOIN
                  dbo.aca_Matricula ON dbo.aca_Alumno.IdEmpresa = dbo.aca_Matricula.IdEmpresa AND dbo.aca_Alumno.IdAlumno = dbo.aca_Matricula.IdAlumno INNER JOIN
                  dbo.vwaca_AnioLectivo_Curso_Paralelo ON dbo.aca_Matricula.IdEmpresa = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdAnio AND 
                  dbo.aca_Matricula.IdSede = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdSede AND dbo.aca_Matricula.IdNivel = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdNivel AND 
                  dbo.aca_Matricula.IdJornada = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdJornada AND dbo.aca_Matricula.IdCurso = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdCurso AND 
                  dbo.aca_Matricula.IdParalelo = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdParalelo INNER JOIN
                  dbo.aca_MatriculaCalificacion ON dbo.aca_Matricula.IdEmpresa = dbo.aca_MatriculaCalificacion.IdEmpresa AND dbo.aca_Matricula.IdMatricula = dbo.aca_MatriculaCalificacion.IdMatricula INNER JOIN
                  dbo.aca_Materia ON dbo.aca_MatriculaCalificacion.IdEmpresa = dbo.aca_Materia.IdEmpresa AND dbo.aca_MatriculaCalificacion.IdMateria = dbo.aca_Materia.IdMateria INNER JOIN
                  dbo.aca_MateriaGrupo ON dbo.aca_Materia.IdEmpresa = dbo.aca_MateriaGrupo.IdEmpresa AND dbo.aca_Materia.IdMateriaGrupo = dbo.aca_MateriaGrupo.IdMateriaGrupo