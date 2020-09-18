
CREATE PROCEDURE [Academico].[SPACA_014_MatriculaCalificacionCualitativa]
(
@IdEmpresa int,
@IdMatricula decimal,
@IdCatalogoParcialTipo int
)
AS
SELECT mcc.IdEmpresa, mcc.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, 
                  cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, acm.NomMateria, acm.NomMateriaArea, acm.NomMateriaGrupo, acm.IdMateria, acm.EsObligatorio, acm.OrdenMateria, acm.OrdenMateriaGrupo, acm.OrdenMateriaArea, 
                  mcc.IdCatalogoParcial, cat.IdCatalogoTipo, mcc.IdCalificacionCualitativa, acc.Codigo AS CodigoCalificacionCualitativa, acc.DescripcionCorta, mcc.Conducta, ce.Letra, mcc.MotivoConducta
FROM     dbo.aca_MatriculaCalificacionCualitativa AS mcc INNER JOIN
                  dbo.aca_Matricula AS m ON mcc.IdEmpresa = m.IdEmpresa AND mcc.IdMatricula = m.IdMatricula INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia AS acm ON m.IdEmpresa = acm.IdEmpresa AND m.IdAnio = acm.IdAnio AND m.IdSede = acm.IdSede AND m.IdNivel = acm.IdNivel AND m.IdJornada = acm.IdJornada AND m.IdCurso = acm.IdCurso AND 
                  mcc.IdMateria = acm.IdMateria INNER JOIN
                  dbo.aca_Catalogo AS cat ON mcc.IdCatalogoParcial = cat.IdCatalogo LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND 
                  m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN
                  dbo.aca_AnioLectivoCalificacionCualitativa AS acc ON mcc.IdEmpresa = acc.IdEmpresa AND mcc.IdCalificacionCualitativa = acc.IdCalificacionCualitativa LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada ON cp.IdEmpresa = jc.IdEmpresa AND 
                  cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada AND cp.IdCurso = jc.IdCurso LEFT OUTER JOIN
                  dbo.aca_AnioLectivoConductaEquivalencia AS ce ON ce.IdEmpresa = mcc.IdEmpresa AND ce.Secuencia = mcc.Conducta AND ce.IdAnio = m.IdAnio
where mcc.IdEmpresa = @IdEmpresa 
and mcc.IdMatricula = @IdMatricula
and cat.IdCatalogo = @IdCatalogoParcialTipo
and not exists(
	select f.IdEmpresa from aca_AlumnoRetiro as f
	where f.IdEmpresa = mcc.IdEmpresa
	and f.IdMatricula = mcc.IdMatricula
	and f.Estado = 1
)