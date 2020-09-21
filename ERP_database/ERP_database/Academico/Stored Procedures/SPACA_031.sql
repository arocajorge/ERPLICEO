
CREATE PROCEDURE [Academico].[SPACA_031]
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
SELECT mc.IdEmpresa, mc.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, a.Codigo, p.pe_nombreCompleto, dbo.aca_AnioLectivo.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, 
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

where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and not exists(
	select f.IdEmpresa from aca_AlumnoRetiro as f
	where f.IdEmpresa = mc.IdEmpresa
	and f.IdMatricula = mc.IdMatricula
	and f.Estado = 1
)
and a.Estado = 1