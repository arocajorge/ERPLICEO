
create PROCEDURE [Academico].[SPACA_032]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@MostrarRetirados bit
)
AS
SELECT mc.IdEmpresa, mc.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, a.Codigo, p.pe_nombreCompleto, 
case when ret.IdMatricula is not null then cast(1 as bit) else cast(0 as bit) end as EsRetirado,
				  case when ret.IdMatricula is not null THEN 'RETIRADO' else 'ACTIVO' end as EsRetiradoString,
dbo.aca_AnioLectivo.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, 
                  nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
				  equivq1.Secuencia AS SecuenciaQ1, equivq1.Letra AS LetraQ1, mc.MotivoPromedioFinalQ1,
				  equivq2.Secuencia AS SecuenciaQ2, equivq2.Letra AS LetraQ2, mc.MotivoPromedioFinalQ2,
				  equivf.Secuencia AS SecuenciaPF, equivf.Letra AS LetraPF, mc.MotivoPromedioFinal,
				  cp.IdProfesorInspector, dbo.tb_persona.pe_nombreCompleto AS NombreInspector
FROM     dbo.aca_MatriculaConducta AS mc LEFT OUTER JOIN
                  dbo.tb_persona RIGHT OUTER JOIN
                  dbo.aca_Profesor AS pro ON dbo.tb_persona.IdPersona = pro.IdPersona RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON pro.IdEmpresa = cp.IdEmpresa AND pro.IdProfesor = cp.IdProfesorInspector RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo INNER JOIN
                  dbo.aca_Alumno AS a INNER JOIN
                  dbo.aca_Matricula AS m ON a.IdEmpresa = m.IdEmpresa AND a.IdAlumno = m.IdAlumno INNER JOIN
                  dbo.tb_persona AS p ON a.IdPersona = p.IdPersona ON dbo.aca_AnioLectivo.IdEmpresa = m.IdEmpresa AND dbo.aca_AnioLectivo.IdAnio = m.IdAnio ON cp.IdEmpresa = m.IdEmpresa AND cp.IdAnio = m.IdAnio AND 
                  cp.IdSede = m.IdSede AND cp.IdNivel = m.IdNivel AND cp.IdJornada = m.IdJornada AND cp.IdCurso = m.IdCurso AND cp.IdParalelo = m.IdParalelo ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada ON cp.IdEmpresa = jc.IdEmpresa AND 
                  cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada AND cp.IdCurso = jc.IdCurso LEFT OUTER JOIN
                  dbo.aca_AnioLectivoConductaEquivalencia AS equivq1 ON equivq1.IdEmpresa = mc.IdEmpresa AND equivq1.IdAnio = m.IdAnio AND equivq1.Secuencia = mc.SecuenciaPromedioFinalQ1 LEFT OUTER JOIN
                  dbo.aca_AnioLectivoConductaEquivalencia AS equivq2 ON equivq2.IdEmpresa = mc.IdEmpresa AND equivq2.IdAnio = m.IdAnio AND equivq2.Secuencia = mc.SecuenciaPromedioFinalQ2 LEFT OUTER JOIN
                  dbo.aca_AnioLectivoConductaEquivalencia AS equivf ON equivf.IdEmpresa = mc.IdEmpresa AND equivf.IdAnio = m.IdAnio AND equivf.Secuencia = mc.SecuenciaPromedioFinal
				  left join
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r
					where r.Estado = 1
					) as ret on m.IdEmpresa =ret.IdEmpresa and m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = case when @IdSede = 0 then m.IdSede else @IdSede end
and m.IdNivel = case when @IdNivel = 0 then m.IdNivel else @IdNivel end
and m.IdJornada = case when @IdJornada = 0 then m.IdJornada else @IdJornada end
and m.IdCurso = case when @IdCurso = 0 then m.IdCurso else @IdCurso end
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and a.Estado = 1