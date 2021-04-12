
CREATE PROCEDURE [Academico].[SPACA_022]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int
)
AS
SELECT ma.IdEmpresa, ma.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, a.Codigo, tb_persona_1.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, 
                  jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, CASE WHEN ret.IdMatricula IS NOT NULL THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS EsRetirado, ma.FInjustificadaP1, ma.FJustificadaP1, ma.AtrasosP1, 
                  ma.FInjustificadaP2, ma.FJustificadaP2, ma.AtrasosP2, ma.FInjustificadaP3, ma.FJustificadaP3, ma.AtrasosP3, ma.FInjustificadaP4, ma.FJustificadaP4, ma.AtrasosP4, ma.FInjustificadaP5, ma.FJustificadaP5, ma.AtrasosP5, 
                  ma.FInjustificadaP6, ma.FJustificadaP6, ma.AtrasosP6, cp.IdProfesorInspector, pa.pe_nombreCompleto AS NombreInspector
FROM     dbo.tb_persona AS pa with (nolock) INNER JOIN
                  dbo.aca_Profesor AS p ON pa.IdPersona = p.IdPersona LEFT OUTER JOIN
                      (SELECT IdEmpresa, IdMatricula
                       FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
                       WHERE   (Estado = 1)) AS ret RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
                  dbo.aca_MatriculaAsistencia AS ma with (nolock) INNER JOIN
                  dbo.aca_Matricula AS m with (nolock) ON ma.IdEmpresa = m.IdEmpresa AND ma.IdMatricula = m.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS a with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN
                  dbo.tb_persona AS tb_persona_1 with (nolock) ON a.IdPersona = tb_persona_1.IdPersona ON cp.IdEmpresa = m.IdEmpresa AND cp.IdAnio = m.IdAnio AND cp.IdSede = m.IdSede AND cp.IdNivel = m.IdNivel AND cp.IdJornada = m.IdJornada AND 
                  cp.IdCurso = m.IdCurso AND cp.IdParalelo = m.IdParalelo LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada ON cp.IdEmpresa = jc.IdEmpresa AND 
                  cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada AND cp.IdCurso = jc.IdCurso ON ret.IdEmpresa = m.IdEmpresa AND ret.IdMatricula = m.IdMatricula ON 
                  p.IdProfesor = cp.IdProfesorInspector AND p.IdEmpresa = cp.IdEmpresa AND p.IdEmpresa = cp.IdEmpresa
where ma.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and not exists(
	select f.IdEmpresa from aca_AlumnoRetiro as f with (nolock) 
	where f.IdEmpresa = ma.IdEmpresa
	and f.IdMatricula = ma.IdMatricula
	and f.Estado = 1
)
and a.Estado = 1