CREATE VIEW Academico.VWACA_013
AS
SELECT dbo.aca_MatriculaCalificacionParcial.IdEmpresa, dbo.aca_MatriculaCalificacionParcial.IdMatricula, dbo.aca_Matricula.IdAnio, dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdNivel, dbo.aca_Matricula.IdJornada, dbo.aca_Matricula.IdCurso, 
                  dbo.aca_Matricula.IdParalelo, dbo.aca_MatriculaCalificacionParcial.IdMateria, dbo.aca_Materia.NomMateria, dbo.aca_MatriculaCalificacionParcial.IdCatalogoParcial, dbo.aca_Catalogo.NomCatalogo, 
                  dbo.aca_MatriculaCalificacionParcial.IdProfesor, dbo.aca_Matricula.IdAlumno, dbo.aca_Alumno.IdPersona, dbo.tb_persona.pe_nombreCompleto AS Alumno, dbo.aca_MatriculaCalificacionParcial.Calificacion1, 
                  dbo.aca_MatriculaCalificacionParcial.Calificacion2, dbo.aca_MatriculaCalificacionParcial.Calificacion3, dbo.aca_MatriculaCalificacionParcial.Calificacion4, dbo.aca_MatriculaCalificacionParcial.Evaluacion, 
                  dbo.aca_MatriculaCalificacionParcial.Remedial1, dbo.aca_MatriculaCalificacionParcial.Remedial2, dbo.aca_MatriculaCalificacionParcial.Conducta, dbo.aca_MatriculaCalificacionParcial.MotivoCalificacion, 
                  dbo.aca_MatriculaCalificacionParcial.MotivoConducta, dbo.aca_MatriculaCalificacionParcial.AccionRemedial, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomNivel, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomJornada, 
                  dbo.vwaca_AnioLectivo_Curso_Paralelo.NomCurso, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomParalelo, dbo.aca_Alumno.Codigo, dbo.aca_MatriculaCalificacion.CalificacionP1, dbo.aca_MatriculaCalificacion.CalificacionP2, 
                  dbo.aca_MatriculaCalificacion.CalificacionP3, dbo.aca_MatriculaCalificacion.CalificacionP4, dbo.aca_MatriculaCalificacion.CalificacionP5, dbo.aca_MatriculaCalificacion.CalificacionP6
FROM     dbo.aca_Materia INNER JOIN
                  dbo.aca_Catalogo INNER JOIN
                  dbo.aca_MatriculaCalificacionParcial ON dbo.aca_Catalogo.IdCatalogo = dbo.aca_MatriculaCalificacionParcial.IdCatalogoParcial ON dbo.aca_Materia.IdEmpresa = dbo.aca_MatriculaCalificacionParcial.IdEmpresa AND 
                  dbo.aca_Materia.IdMateria = dbo.aca_MatriculaCalificacionParcial.IdMateria INNER JOIN
                  dbo.aca_Matricula ON dbo.aca_MatriculaCalificacionParcial.IdEmpresa = dbo.aca_Matricula.IdEmpresa AND dbo.aca_MatriculaCalificacionParcial.IdMatricula = dbo.aca_Matricula.IdMatricula INNER JOIN
                  dbo.tb_persona INNER JOIN
                  dbo.aca_Alumno ON dbo.tb_persona.IdPersona = dbo.aca_Alumno.IdPersona ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_Matricula.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.vwaca_AnioLectivo_Curso_Paralelo ON dbo.aca_Matricula.IdEmpresa = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdAnio AND 
                  dbo.aca_Matricula.IdSede = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdSede AND dbo.aca_Matricula.IdNivel = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdNivel AND 
                  dbo.aca_Matricula.IdJornada = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdJornada AND dbo.aca_Matricula.IdCurso = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdCurso AND 
                  dbo.aca_Matricula.IdParalelo = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdParalelo INNER JOIN
                  dbo.aca_MatriculaCalificacion ON dbo.aca_Matricula.IdEmpresa = dbo.aca_MatriculaCalificacion.IdEmpresa AND dbo.aca_Matricula.IdMatricula = dbo.aca_MatriculaCalificacion.IdMatricula AND 
                  dbo.aca_MatriculaCalificacionParcial.IdMateria = dbo.aca_MatriculaCalificacion.IdMateria
GO



GO



GO


