
CREATE PROCEDURE [Academico].[SPACA_028_Profesores]
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
SELECT sn.IdEmpresa, sn.IdAnio, sn.IdSede, nj.IdJornada, nj.IdNivel, jc.IdCurso, cp.IdParalelo, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, 
                  cp.OrdenParalelo, cm.EsObligatorio, cm.OrdenMateria, cm.NomMateria, cm.OrdenMateriaGrupo, pp.IdProfesor, per.pe_nombreCompleto AS NombreProfesor
FROM     dbo.aca_AnioLectivo_Curso_Materia AS cm RIGHT OUTER JOIN
                  dbo.tb_persona AS per INNER JOIN
                  dbo.aca_Profesor AS pro ON per.IdPersona = pro.IdPersona RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Paralelo_Profesor AS pp ON pro.IdEmpresa = pp.IdEmpresa AND pro.IdProfesor = pp.IdProfesor LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON pp.IdEmpresa = cp.IdEmpresa AND pp.IdAnio = cp.IdAnio AND pp.IdSede = cp.IdSede AND pp.IdNivel = cp.IdNivel AND pp.IdJornada = cp.IdJornada AND pp.IdCurso = cp.IdCurso AND 
                  pp.IdParalelo = cp.IdParalelo ON cm.IdEmpresa = pp.IdEmpresa AND cm.IdAnio = pp.IdAnio AND cm.IdSede = pp.IdSede AND cm.IdNivel = pp.IdNivel AND cm.IdJornada = pp.IdJornada AND cm.IdCurso = pp.IdCurso AND 
                  cm.IdMateria = pp.IdMateria LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON cm.IdEmpresa = jc.IdEmpresa AND cm.IdAnio = jc.IdAnio AND cm.IdSede = jc.IdSede AND cm.IdNivel = jc.IdNivel AND cm.IdJornada = jc.IdJornada AND cm.IdCurso = jc.IdCurso AND 
                  cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada AND cp.IdCurso = jc.IdCurso LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS a RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn ON a.IdAnio = sn.IdAnio AND a.IdEmpresa = sn.IdEmpresa RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdEmpresa = nj.IdEmpresa AND jc.IdAnio = nj.IdAnio AND 
                  jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel AND jc.IdJornada = nj.IdJornada
where sn.IdEmpresa = @IdEmpresa 
and sn.IdAnio = @IdAnio
and sn.IdSede = @IdSede
and nj.IdNivel = @IdNivel
and nj.IdJornada = @IdJornada
and jc.IdCurso = @IdCurso
and cp.IdParalelo = @IdParalelo
and a.Estado = 1