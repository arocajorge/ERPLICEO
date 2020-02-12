CREATE VIEW Academico.VWACA_004
AS
SELECT CursoMateria.IdEmpresa, CursoMateria.IdAnio, CursoMateria.IdSede, CursoMateria.IdNivel, CursoMateria.IdJornada, CursoMateria.IdCurso, CursoMateria.IdMateria, aca_AnioLectivo.Descripcion, aca_AnioLectivo.FechaDesde, 
                  aca_AnioLectivo.FechaHasta, SedeNivel.NomSede, SedeNivel.NomNivel, NivelJornada.NomJornada, NivelJornada.OrdenJornada, JornadaCurso.NomCurso, JornadaCurso.OrdenCurso, CursoMateria.NomMateria, 
                  CursoMateria.NomMateriaArea, CursoMateria.NomMateriaGrupo, CursoMateria.OrdenMateria, CursoMateria.OrdenMateriaGrupo, CursoMateria.OrdenMateriaArea, CursoMateria.EsObligatorio
FROM     aca_AnioLectivo_Sede_NivelAcademico AS SedeNivel INNER JOIN
                  aca_AnioLectivo_NivelAcademico_Jornada AS NivelJornada INNER JOIN
                  aca_AnioLectivo_Curso_Materia AS CursoMateria INNER JOIN
                  aca_AnioLectivo_Jornada_Curso AS JornadaCurso ON CursoMateria.IdEmpresa = JornadaCurso.IdEmpresa AND CursoMateria.IdAnio = JornadaCurso.IdAnio AND CursoMateria.IdSede = JornadaCurso.IdSede AND 
                  CursoMateria.IdNivel = JornadaCurso.IdNivel AND CursoMateria.IdJornada = JornadaCurso.IdJornada AND CursoMateria.IdCurso = JornadaCurso.IdCurso ON NivelJornada.IdEmpresa = JornadaCurso.IdEmpresa AND 
                  NivelJornada.IdAnio = JornadaCurso.IdAnio AND NivelJornada.IdSede = JornadaCurso.IdSede AND NivelJornada.IdNivel = JornadaCurso.IdNivel AND NivelJornada.IdJornada = JornadaCurso.IdJornada ON 
                  SedeNivel.IdEmpresa = NivelJornada.IdEmpresa AND SedeNivel.IdAnio = NivelJornada.IdAnio AND SedeNivel.IdSede = NivelJornada.IdSede AND SedeNivel.IdNivel = NivelJornada.IdNivel INNER JOIN
                  aca_AnioLectivo ON SedeNivel.IdEmpresa = aca_AnioLectivo.IdEmpresa AND SedeNivel.IdAnio = aca_AnioLectivo.IdAnio