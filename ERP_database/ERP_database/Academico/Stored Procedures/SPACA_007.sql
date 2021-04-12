--exec [Academico].[SPACA_007] 1,1,1,1,1,1,0,'2020/01/01','2020/12/01',0
CREATE procedure [Academico].[SPACA_007]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@FechaIni date,
@FechaFin date,
@MostrarRetirados bit
)AS
BEGIN
SELECT dbo.aca_Matricula.IdEmpresa, dbo.aca_Matricula.IdMatricula, dbo.aca_Matricula.IdAnio, dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdNivel, dbo.aca_Matricula.IdJornada, dbo.aca_Matricula.IdCurso, dbo.aca_Matricula.IdParalelo, 
                  sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.OrdenCurso, jc.NomCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, p.pe_sexo, 1 AS Cantidad, dbo.aca_Matricula.Fecha, 
                  dbo.aca_Plantilla.NomPlantilla, dbo.aca_Plantilla.IdPlantilla, a.Descripcion, dbo.aca_Plantilla.IdTipoPlantilla, dbo.aca_PlantillaTipo.NomPlantillaTipo, CASE WHEN ret.IdEmpresa IS NULL THEN CAST(0 AS bit) ELSE CAST(1 AS bit) 
                  END AS EsRetirado, CASE WHEN ret.IdEmpresa IS NULL THEN '' ELSE 'RETIRADO' END AS EsRetiradoString
FROM     dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
                  dbo.aca_Plantilla with (nolock) RIGHT OUTER JOIN
                  dbo.aca_Alumno AS alu with (nolock) LEFT OUTER JOIN
                  dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) RIGHT OUTER JOIN
                  dbo.aca_Matricula ON jc.IdEmpresa = dbo.aca_Matricula.IdEmpresa AND jc.IdAnio = dbo.aca_Matricula.IdAnio AND jc.IdSede = dbo.aca_Matricula.IdSede AND jc.IdNivel = dbo.aca_Matricula.IdNivel AND 
                  jc.IdJornada = dbo.aca_Matricula.IdJornada AND jc.IdCurso = dbo.aca_Matricula.IdCurso LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON dbo.aca_Matricula.IdEmpresa = cp.IdEmpresa AND dbo.aca_Matricula.IdAnio = cp.IdAnio AND dbo.aca_Matricula.IdSede = cp.IdSede AND dbo.aca_Matricula.IdNivel = cp.IdNivel AND 
                  dbo.aca_Matricula.IdJornada = cp.IdJornada AND dbo.aca_Matricula.IdCurso = cp.IdCurso AND dbo.aca_Matricula.IdParalelo = cp.IdParalelo LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS a with (nolock) ON dbo.aca_Matricula.IdEmpresa = a.IdEmpresa AND dbo.aca_Matricula.IdAnio = a.IdAnio ON alu.IdEmpresa = dbo.aca_Matricula.IdEmpresa AND alu.IdAlumno = dbo.aca_Matricula.IdAlumno ON 
                  dbo.aca_Plantilla.IdEmpresa = dbo.aca_Matricula.IdEmpresa AND dbo.aca_Plantilla.IdAnio = dbo.aca_Matricula.IdAnio AND dbo.aca_Plantilla.IdPlantilla = dbo.aca_Matricula.IdPlantilla LEFT OUTER JOIN
                  dbo.aca_PlantillaTipo ON dbo.aca_Plantilla.IdEmpresa = dbo.aca_PlantillaTipo.IdEmpresa AND dbo.aca_Plantilla.IdTipoPlantilla = dbo.aca_PlantillaTipo.IdTipoPlantilla LEFT OUTER JOIN
                      (SELECT IdEmpresa, IdMatricula
                       FROM      dbo.aca_AlumnoRetiro AS f with (nolock) 
                       WHERE   (Estado = 1)) AS ret ON dbo.aca_Matricula.IdEmpresa = ret.IdEmpresa AND dbo.aca_Matricula.IdMatricula = ret.IdMatricula LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON jc.IdEmpresa = nj.IdEmpresa AND jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel AND jc.IdJornada = nj.IdJornada ON sn.IdEmpresa = nj.IdEmpresa AND 
                  sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel
where aca_Matricula.IdEmpresa = @IdEmpresa
and aca_Matricula.IdAnio between @IdAnio and CASE WHEN @IdAnio = 0 THEN 999999 ELSE @IdAnio end
and aca_Matricula.IdSede between @IdSede and CASE WHEN @IdSede = 0 THEN 999999 ELSE @IdSede end
and aca_Matricula.IdNivel between @IdNivel and CASE WHEN @IdNivel = 0 THEN 999999 ELSE @IdNivel end
and aca_Matricula.IdJornada between @IdJornada and CASE WHEN @IdJornada = 0 THEN 999999 ELSE @IdJornada end
and aca_Matricula.IdCurso between @IdCurso and CASE WHEN @IdCurso = 0 THEN 999999 ELSE @IdCurso end
and aca_Matricula.IdParalelo between @IdParalelo and CASE WHEN @IdParalelo = 0 THEN 999999 ELSE @IdParalelo end
and aca_Matricula.Fecha between @FechaIni and @FechaFin
and isnull(ret.IdEmpresa,0) = case when @MostrarRetirados = 1 then isnull(ret.IdEmpresa,0) else 0 end

END