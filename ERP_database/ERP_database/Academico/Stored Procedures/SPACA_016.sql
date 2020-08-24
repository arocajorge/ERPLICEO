CREATE PROCEDURE [Academico].[SPACA_016]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdCatalogoParcial int
)
AS
SELECT mcc.IdEmpresa, mcc.IdMatricula, mcc.IdMateria, alu.Codigo, p.pe_nombreCompleto AS NombreAlumno, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, 
                  sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, dbo.aca_AnioLectivo_Curso_Materia.NomMateria, 
                  dbo.aca_AnioLectivo_Curso_Materia.NomMateriaArea, dbo.aca_AnioLectivo_Curso_Materia.NomMateriaGrupo, dbo.aca_AnioLectivo_Curso_Materia.EsObligatorio, dbo.aca_AnioLectivo_Curso_Materia.OrdenMateria, 
                  dbo.aca_AnioLectivo_Curso_Materia.OrdenMateriaGrupo, dbo.aca_AnioLectivo_Curso_Materia.OrdenMateriaArea, mcc.IdCatalogoParcial, dbo.aca_Catalogo.NomCatalogo, mcc.IdCalificacionCualitativa, 
                  acc.Codigo AS CodigoCalificacionCualitativa, acc.DescripcionCorta, pp.pe_nombreCompleto AS NombreProfesor
FROM     dbo.aca_Catalogo RIGHT OUTER JOIN
                  dbo.tb_persona AS pp RIGHT OUTER JOIN
                  dbo.aca_Profesor ON pp.IdPersona = dbo.aca_Profesor.IdPersona RIGHT OUTER JOIN
                  dbo.aca_MatriculaCalificacionCualitativa AS mcc INNER JOIN
                  dbo.aca_Matricula AS m ON mcc.IdEmpresa = m.IdEmpresa AND mcc.IdMatricula = m.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
                  dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia ON m.IdEmpresa = dbo.aca_AnioLectivo_Curso_Materia.IdEmpresa AND m.IdAnio = dbo.aca_AnioLectivo_Curso_Materia.IdAnio AND m.IdSede = dbo.aca_AnioLectivo_Curso_Materia.IdSede AND 
                  m.IdNivel = dbo.aca_AnioLectivo_Curso_Materia.IdNivel AND m.IdJornada = dbo.aca_AnioLectivo_Curso_Materia.IdJornada AND m.IdCurso = dbo.aca_AnioLectivo_Curso_Materia.IdCurso AND 
                  mcc.IdMateria = dbo.aca_AnioLectivo_Curso_Materia.IdMateria ON dbo.aca_Profesor.IdEmpresa = mcc.IdEmpresa AND dbo.aca_Profesor.IdProfesor = mcc.IdProfesor LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND 
                  m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN
                  dbo.aca_AnioLectivoCalificacionCualitativa AS acc ON mcc.IdEmpresa = acc.IdEmpresa AND mcc.IdCalificacionCualitativa = acc.IdCalificacionCualitativa LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio ON dbo.aca_Catalogo.IdCatalogo = mcc.IdCatalogoParcial LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada ON cp.IdEmpresa = jc.IdEmpresa AND 
                  cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada AND cp.IdCurso = jc.IdCurso
where mcc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and mcc.IdCatalogoParcial = @IdCatalogoParcial
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and not exists(
	select f.IdEmpresa from aca_AlumnoRetiro as f
	where f.IdEmpresa = mcc.IdEmpresa
	and f.IdMatricula = mcc.IdMatricula
	and f.Estado = 1
)