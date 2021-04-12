
CREATE PROCEDURE [Academico].[SPACA_029]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdMateria int
)
AS
SELECT pp.IdEmpresa, pp.IdAnio, pp.IdSede, pp.IdNivel, pp.IdJornada, pp.IdCurso, pp.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
                  nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, pp.IdMateria, pp.IdProfesor, cma.NomMateria, p.pe_nombreCompleto NombreProfesor
FROM     dbo.aca_AnioLectivo_Paralelo_Profesor AS pp with (nolock) INNER JOIN
                  dbo.aca_AnioLectivo AS a with (nolock) ON pp.IdEmpresa = a.IdEmpresa AND pp.IdAnio = a.IdAnio  LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia AS cma with (nolock) ON pp.IdEmpresa = cma.IdEmpresa AND pp.IdAnio = cma.IdAnio AND pp.IdSede = cma.IdSede AND pp.IdNivel = cma.IdNivel AND pp.IdJornada = cma.IdJornada AND 
                  pp.IdCurso = cma.IdCurso AND pp.IdMateria = cma.IdMateria LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON pp.IdEmpresa = cp.IdEmpresa AND pp.IdAnio = cp.IdAnio AND pp.IdSede = cp.IdSede AND pp.IdNivel = cp.IdNivel AND pp.IdJornada = cp.IdJornada AND pp.IdCurso = cp.IdCurso AND 
                  pp.IdParalelo = cp.IdParalelo LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada AND cp.IdCurso = jc.IdCurso LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON jc.IdEmpresa = nj.IdEmpresa AND jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel AND jc.IdJornada = nj.IdJornada LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) ON nj.IdEmpresa = sn.IdEmpresa AND nj.IdAnio = sn.IdAnio AND nj.IdSede = sn.IdSede AND nj.IdNivel = sn.IdNivel
left outer join aca_Profesor pro with (nolock) on pro.IdEmpresa=pp.IdEmpresa and pro.IdProfesor = pp.IdProfesor
left outer join tb_persona p with (nolock) on pro.IdPersona = p.IdPersona

where cp.IdEmpresa = @IdEmpresa 
and cp.IdAnio = @IdAnio
and cp.IdSede = @IdSede
and cp.IdNivel = @IdNivel
and cp.IdJornada = @IdJornada
and cp.IdCurso = @IdCurso
and cp.IdParalelo = @IdParalelo
and cma.IdMateria = @IdMateria