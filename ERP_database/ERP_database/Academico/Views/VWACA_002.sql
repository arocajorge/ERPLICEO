create view academico.VWACA_002 as
SELECT dbo.aca_Matricula.IdEmpresa, dbo.aca_Matricula.IdMatricula, dbo.aca_Matricula.IdAlumno, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomSede, dbo.vwaca_AnioLectivo_Curso_Paralelo.Descripcion, dbo.aca_AnioLectivo.EnCurso, 
                  dbo.vwaca_AnioLectivo_Curso_Paralelo.NomNivel, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomJornada, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomCurso, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomParalelo, 
                  dbo.aca_Alumno.Codigo AS CodigoAlumno, dbo.tb_persona.pe_nombreCompleto AS NombreAlumno, tb_persona_1.pe_cedulaRuc AS CedulaRep, tb_persona_1.pe_nombreCompleto AS NombreRep
FROM     dbo.aca_Matricula INNER JOIN
                  dbo.vwaca_AnioLectivo_Curso_Paralelo ON dbo.aca_Matricula.IdEmpresa = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdAnio AND 
                  dbo.aca_Matricula.IdSede = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdSede AND dbo.aca_Matricula.IdNivel = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdNivel AND 
                  dbo.aca_Matricula.IdJornada = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdJornada AND dbo.aca_Matricula.IdCurso = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdCurso AND 
                  dbo.aca_Matricula.IdParalelo = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdParalelo INNER JOIN
                  dbo.aca_Alumno ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_Matricula.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.tb_persona AS tb_persona_1 ON dbo.aca_Matricula.IdPersonaR = tb_persona_1.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo ON dbo.aca_Matricula.IdEmpresa = dbo.aca_AnioLectivo.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.aca_AnioLectivo.IdAnio