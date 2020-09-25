create view vwaca_AnioLectivo_Curso_Paralelo_Dece
as
SELECT dbo.aca_AnioLectivo_Curso_Paralelo.IdEmpresa, dbo.aca_AnioLectivo_Curso_Paralelo.IdAnio, dbo.aca_AnioLectivo_Curso_Paralelo.IdSede, dbo.aca_AnioLectivo_Curso_Paralelo.IdNivel, dbo.aca_AnioLectivo_Curso_Paralelo.IdJornada, 
                  dbo.aca_AnioLectivo_Curso_Paralelo.IdCurso, dbo.aca_AnioLectivo_Curso_Paralelo.IdParalelo, dbo.aca_AnioLectivo_NivelAcademico_Jornada.NomJornada, dbo.aca_AnioLectivo_NivelAcademico_Jornada.OrdenJornada, 
                  dbo.aca_AnioLectivo_Jornada_Curso.NomCurso, dbo.aca_AnioLectivo_Jornada_Curso.OrdenCurso, dbo.aca_AnioLectivo_Curso_Paralelo.NomParalelo, dbo.aca_AnioLectivo_Curso_Paralelo.OrdenParalelo, 
                  dbo.aca_AnioLectivo_Curso_Paralelo_Dece.ObservacionQ1, dbo.aca_AnioLectivo_Curso_Paralelo_Dece.ObservacionQ2, dbo.aca_AnioLectivo_Curso_Paralelo_Dece.Estado
FROM     dbo.aca_AnioLectivo_NivelAcademico_Jornada INNER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso ON dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdEmpresa = dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa AND 
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdAnio = dbo.aca_AnioLectivo_Jornada_Curso.IdAnio AND dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdSede = dbo.aca_AnioLectivo_Jornada_Curso.IdSede AND 
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdNivel = dbo.aca_AnioLectivo_Jornada_Curso.IdNivel AND dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdJornada = dbo.aca_AnioLectivo_Jornada_Curso.IdJornada INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo ON dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa = dbo.aca_AnioLectivo_Curso_Paralelo.IdEmpresa AND 
                  dbo.aca_AnioLectivo_Jornada_Curso.IdAnio = dbo.aca_AnioLectivo_Curso_Paralelo.IdAnio AND dbo.aca_AnioLectivo_Jornada_Curso.IdSede = dbo.aca_AnioLectivo_Curso_Paralelo.IdSede AND 
                  dbo.aca_AnioLectivo_Jornada_Curso.IdNivel = dbo.aca_AnioLectivo_Curso_Paralelo.IdNivel AND dbo.aca_AnioLectivo_Jornada_Curso.IdJornada = dbo.aca_AnioLectivo_Curso_Paralelo.IdJornada AND 
                  dbo.aca_AnioLectivo_Jornada_Curso.IdCurso = dbo.aca_AnioLectivo_Curso_Paralelo.IdCurso LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo_Dece ON dbo.aca_AnioLectivo_Curso_Paralelo.IdEmpresa = dbo.aca_AnioLectivo_Curso_Paralelo_Dece.IdEmpresa AND 
                  dbo.aca_AnioLectivo_Curso_Paralelo.IdAnio = dbo.aca_AnioLectivo_Curso_Paralelo_Dece.IdAnio AND dbo.aca_AnioLectivo_Curso_Paralelo.IdSede = dbo.aca_AnioLectivo_Curso_Paralelo_Dece.IdSede AND 
                  dbo.aca_AnioLectivo_Curso_Paralelo.IdNivel = dbo.aca_AnioLectivo_Curso_Paralelo_Dece.IdNivel AND dbo.aca_AnioLectivo_Curso_Paralelo.IdJornada = dbo.aca_AnioLectivo_Curso_Paralelo_Dece.IdJornada AND 
                  dbo.aca_AnioLectivo_Curso_Paralelo.IdCurso = dbo.aca_AnioLectivo_Curso_Paralelo_Dece.IdCurso AND dbo.aca_AnioLectivo_Curso_Paralelo.IdParalelo = dbo.aca_AnioLectivo_Curso_Paralelo_Dece.IdParalelo