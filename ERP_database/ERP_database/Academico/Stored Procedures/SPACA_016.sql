CREATE PROCEDURE [Academico].[SPACA_016]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdMateria int,
@IdCatalogoParcial int
)
AS
SELECT mcc.IdEmpresa, mcc.IdMatricula, mcc.IdMateria, alu.Codigo, p.pe_nombreCompleto AS NombreAlumno, m.IdSede, m.IdAnio, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
                  nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, MC.NomMateria, MC.NomMateriaArea, MC.NomMateriaGrupo, MC.EsObligatorio, MC.OrdenMateria, MC.OrdenMateriaGrupo, 
                  MC.OrdenMateriaArea, mcc.IdCatalogoParcial, C.NomCatalogo, mcc.IdCalificacionCualitativa, acc.Codigo AS CodigoCalificacionCualitativa, acc.DescripcionCorta, mcc.IdProfesor, pp.pe_nombreCompleto AS NombreProfesor, 
                  dbo.aca_AnioLectivoConductaEquivalencia.Letra, mcc.Conducta
FROM     dbo.aca_AnioLectivoCalificacionCualitativa AS acc RIGHT OUTER JOIN
                  dbo.aca_MatriculaCalificacionCualitativa AS mcc INNER JOIN
                  dbo.aca_Matricula AS m ON mcc.IdEmpresa = m.IdEmpresa AND mcc.IdMatricula = m.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
                  dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia AS MC ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND 
                  mcc.IdMateria = MC.IdMateria LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
                  dbo.tb_persona AS pp RIGHT OUTER JOIN
                  dbo.aca_Profesor ON pp.IdPersona = dbo.aca_Profesor.IdPersona ON mcc.IdEmpresa = dbo.aca_Profesor.IdEmpresa AND mcc.IdProfesor = dbo.aca_Profesor.IdProfesor LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND 
                  m.IdParalelo = cp.IdParalelo ON acc.IdEmpresa = mcc.IdEmpresa AND acc.IdCalificacionCualitativa = mcc.IdCalificacionCualitativa LEFT OUTER JOIN
                  dbo.aca_Catalogo AS C ON mcc.IdCatalogoParcial = C.IdCatalogo LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada ON cp.IdEmpresa = jc.IdEmpresa AND 
                  cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada AND cp.IdCurso = jc.IdCurso LEFT OUTER JOIN
                  dbo.aca_AnioLectivoConductaEquivalencia ON mcc.IdEmpresa = dbo.aca_AnioLectivoConductaEquivalencia.IdEmpresa AND mcc.Conducta = dbo.aca_AnioLectivoConductaEquivalencia.Secuencia AND 
                  m.IdAnio = dbo.aca_AnioLectivoConductaEquivalencia.IdAnio
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