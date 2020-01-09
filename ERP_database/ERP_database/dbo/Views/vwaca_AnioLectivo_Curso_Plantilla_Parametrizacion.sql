CREATE VIEW vwaca_AnioLectivo_Curso_Plantilla_Parametrizacion
AS
SELECT        aca_AnioLectivo_Curso_Plantilla.IdEmpresa, aca_AnioLectivo_Curso_Plantilla.IdAnio, aca_AnioLectivo_Curso_Plantilla.IdSede, aca_AnioLectivo_Curso_Plantilla.IdNivel, aca_AnioLectivo_Curso_Plantilla.IdJornada, 
                         aca_AnioLectivo_Curso_Plantilla.IdCurso, aca_AnioLectivo_Curso_Plantilla.IdPlantilla, aca_Plantilla_Rubro.IdRubro, aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdCtaCble, 
                         aca_AnioLectivo_NivelAcademico_Jornada.NomJornada, aca_AnioLectivo_NivelAcademico_Jornada.OrdenJornada, aca_AnioLectivo_Jornada_Curso.NomCurso, aca_AnioLectivo_Jornada_Curso.OrdenCurso, 
                         aca_Plantilla.NomPlantilla, aca_AnioLectivo_Rubro.NomRubro, aca_AnioLectivo_Sede_NivelAcademico.NomNivel, aca_AnioLectivo_Sede_NivelAcademico.NomSede
FROM            aca_AnioLectivo_Jornada_Curso INNER JOIN
                         aca_AnioLectivo_NivelAcademico_Jornada ON aca_AnioLectivo_Jornada_Curso.IdEmpresa = aca_AnioLectivo_NivelAcademico_Jornada.IdEmpresa AND 
                         aca_AnioLectivo_Jornada_Curso.IdAnio = aca_AnioLectivo_NivelAcademico_Jornada.IdAnio AND aca_AnioLectivo_Jornada_Curso.IdSede = aca_AnioLectivo_NivelAcademico_Jornada.IdSede AND 
                         aca_AnioLectivo_Jornada_Curso.IdNivel = aca_AnioLectivo_NivelAcademico_Jornada.IdNivel AND aca_AnioLectivo_Jornada_Curso.IdJornada = aca_AnioLectivo_NivelAcademico_Jornada.IdJornada INNER JOIN
                         aca_AnioLectivo_Sede_NivelAcademico ON aca_AnioLectivo_NivelAcademico_Jornada.IdEmpresa = aca_AnioLectivo_Sede_NivelAcademico.IdEmpresa AND 
                         aca_AnioLectivo_NivelAcademico_Jornada.IdAnio = aca_AnioLectivo_Sede_NivelAcademico.IdAnio AND aca_AnioLectivo_NivelAcademico_Jornada.IdSede = aca_AnioLectivo_Sede_NivelAcademico.IdSede AND 
                         aca_AnioLectivo_NivelAcademico_Jornada.IdNivel = aca_AnioLectivo_Sede_NivelAcademico.IdNivel RIGHT OUTER JOIN
                         aca_Plantilla INNER JOIN
                         aca_AnioLectivo_Curso_Plantilla ON aca_Plantilla.IdEmpresa = aca_AnioLectivo_Curso_Plantilla.IdEmpresa AND aca_Plantilla.IdAnio = aca_AnioLectivo_Curso_Plantilla.IdAnio AND 
                         aca_Plantilla.IdPlantilla = aca_AnioLectivo_Curso_Plantilla.IdPlantilla INNER JOIN
                         aca_Plantilla_Rubro ON aca_Plantilla.IdEmpresa = aca_Plantilla_Rubro.IdEmpresa AND aca_Plantilla.IdAnio = aca_Plantilla_Rubro.IdAnio AND aca_Plantilla.IdPlantilla = aca_Plantilla_Rubro.IdPlantilla INNER JOIN
                         aca_AnioLectivo_Rubro ON aca_Plantilla_Rubro.IdEmpresa = aca_AnioLectivo_Rubro.IdEmpresa AND aca_Plantilla_Rubro.IdAnio = aca_AnioLectivo_Rubro.IdAnio AND 
                         aca_Plantilla_Rubro.IdRubro = aca_AnioLectivo_Rubro.IdRubro ON aca_AnioLectivo_Jornada_Curso.IdEmpresa = aca_AnioLectivo_Curso_Plantilla.IdEmpresa AND 
                         aca_AnioLectivo_Jornada_Curso.IdAnio = aca_AnioLectivo_Curso_Plantilla.IdAnio AND aca_AnioLectivo_Jornada_Curso.IdSede = aca_AnioLectivo_Curso_Plantilla.IdSede AND 
                         aca_AnioLectivo_Jornada_Curso.IdNivel = aca_AnioLectivo_Curso_Plantilla.IdNivel AND aca_AnioLectivo_Jornada_Curso.IdJornada = aca_AnioLectivo_Curso_Plantilla.IdJornada AND 
                         aca_AnioLectivo_Jornada_Curso.IdCurso = aca_AnioLectivo_Curso_Plantilla.IdCurso LEFT OUTER JOIN
                         aca_AnioLectivo_Curso_Plantilla_Parametrizacion ON aca_AnioLectivo_Curso_Plantilla.IdEmpresa = aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdEmpresa AND 
                         aca_AnioLectivo_Curso_Plantilla.IdAnio = aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdAnio AND aca_AnioLectivo_Curso_Plantilla.IdSede = aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdSede AND 
                         aca_AnioLectivo_Curso_Plantilla.IdNivel = aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdNivel AND aca_AnioLectivo_Curso_Plantilla.IdJornada = aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdJornada AND 
                         aca_AnioLectivo_Curso_Plantilla.IdCurso = aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdCurso AND aca_AnioLectivo_Curso_Plantilla.IdPlantilla = aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdPlantilla AND 
                         aca_Plantilla_Rubro.IdRubro = aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdRubro