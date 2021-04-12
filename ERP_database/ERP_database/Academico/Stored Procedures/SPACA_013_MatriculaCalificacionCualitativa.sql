CREATE PROCEDURE [Academico].[SPACA_013_MatriculaCalificacionCualitativa]
(
@IdEmpresa int,
@IdMatricula decimal,
@IdCatalogoParcial int
)
AS
SELECT mcc.IdEmpresa, mcc.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, p.pe_nombreCompleto AS NombreAlumno, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, 
                  jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, acm.NomMateria, acm.NomMateriaArea, acm.NomMateriaGrupo, acm.IdMateria, acm.EsObligatorio, acm.OrdenMateria, acm.OrdenMateriaGrupo, 
                  acm.OrdenMateriaArea, mcc.IdCatalogoParcial, mcc.IdCalificacionCualitativa, acc.Codigo AS CodigoCalificacionCualitativa, acc.DescripcionCorta, mcc.Conducta, ce.Letra, mcc.MotivoConducta, 
                  pp.pe_nombreCompleto AS NombreProfesor
FROM     dbo.aca_MatriculaCalificacionCualitativa AS mcc with (nolock) INNER JOIN
                  dbo.aca_Matricula AS m with (nolock) ON mcc.IdEmpresa = m.IdEmpresa AND mcc.IdMatricula = m.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
                  dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pro with (nolock) ON pro.IdEmpresa = mcc.IdEmpresa AND pro.IdProfesor = mcc.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pp with (nolock) ON pro.IdPersona = pp.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia AS acm with (nolock) ON m.IdEmpresa = acm.IdEmpresa AND m.IdAnio = acm.IdAnio AND m.IdSede = acm.IdSede AND m.IdNivel = acm.IdNivel AND m.IdJornada = acm.IdJornada AND m.IdCurso = acm.IdCurso AND 
                  mcc.IdMateria = acm.IdMateria LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND 
                  m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN
                  dbo.aca_AnioLectivoCalificacionCualitativa AS acc with (nolock) ON mcc.IdEmpresa = acc.IdEmpresa AND mcc.IdCalificacionCualitativa = acc.IdCalificacionCualitativa LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada ON cp.IdEmpresa = jc.IdEmpresa AND 
                  cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada AND cp.IdCurso = jc.IdCurso LEFT OUTER JOIN
                  dbo.aca_AnioLectivoConductaEquivalencia AS ce with (nolock) ON ce.IdEmpresa = mcc.IdEmpresa AND ce.Secuencia = mcc.Conducta AND ce.IdAnio = m.IdAnio
where mcc.IdEmpresa = @IdEmpresa 
and mcc.IdMatricula = @IdMatricula
and mcc.IdCatalogoParcial = @IdCatalogoParcial
and not exists(
	select f.IdEmpresa from aca_AlumnoRetiro as f with (nolock) 
	where f.IdEmpresa = mcc.IdEmpresa
	and f.IdMatricula = mcc.IdMatricula
	and f.Estado = 1
)