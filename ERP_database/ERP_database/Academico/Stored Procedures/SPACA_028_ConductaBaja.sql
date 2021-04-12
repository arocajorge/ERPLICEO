CREATE PROCEDURE [Academico].[SPACA_028_ConductaBaja]
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
SELECT mco.IdEmpresa, mco.IdMatricula, mat.IdAnio, mat.IdSede, mat.IdNivel, mat.IdJornada, mat.IdCurso, mat.IdParalelo, al.Codigo, p.pe_nombreCompleto, equivq1.Secuencia AS SecuenciaQ1, equivq1.Calificacion AS CalificacionQ1, 
                  equivq1.Letra AS LetraQ1, mco.MotivoPromedioFinalQ1, equivq2.Secuencia AS SecuenciaQ2, equivq2.Calificacion AS CalificacionQ2, equivq2.Letra AS LetraQ2, mco.MotivoPromedioFinalQ2, equivf.Secuencia AS SecuenciaPF, 
                  equivf.Calificacion AS CalificacionPF, equivf.Letra AS LetraPF, mco.MotivoPromedioFinal
FROM     dbo.aca_MatriculaConducta AS mco with (nolock) INNER JOIN
                  dbo.aca_Matricula AS mat with (nolock) ON mco.IdEmpresa = mat.IdEmpresa AND mco.IdMatricula = mat.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS al with (nolock) ON mat.IdEmpresa = al.IdEmpresa AND mat.IdAlumno = al.IdAlumno INNER JOIN
                  dbo.tb_persona AS p with (nolock) ON al.IdPersona = p.IdPersona LEFT OUTER JOIN
                  dbo.aca_Matricula AS m with (nolock) ON al.IdAlumno = m.IdAlumno AND al.IdEmpresa = m.IdEmpresa AND mco.IdEmpresa = m.IdEmpresa AND mco.IdMatricula = m.IdMatricula LEFT OUTER JOIN
                  dbo.aca_AnioLectivoConductaEquivalencia AS equivq1 with (nolock) ON equivq1.IdEmpresa = mco.IdEmpresa AND equivq1.IdAnio = m.IdAnio AND equivq1.Secuencia = mco.SecuenciaPromedioFinalQ1 LEFT OUTER JOIN
                  dbo.aca_AnioLectivoConductaEquivalencia AS equivq2 with (nolock) ON equivq2.IdEmpresa = mco.IdEmpresa AND equivq2.IdAnio = m.IdAnio AND equivq2.Secuencia = mco.SecuenciaPromedioFinalQ2 LEFT OUTER JOIN
                  dbo.aca_AnioLectivoConductaEquivalencia AS equivf with (nolock) ON equivf.IdEmpresa = mco.IdEmpresa AND equivf.IdAnio = m.IdAnio AND equivf.Secuencia = mco.SecuenciaPromedioFinal
where mco.IdEmpresa= @IdEmpresa 
and mat.IdAnio = @IdAnio
and mat.IdSede = @IdSede
and mat.IdNivel = @IdNivel
and mat.IdJornada = @IdJornada
and mat.IdCurso = @IdCurso
and mat.IdParalelo = @IdParalelo
and al.Estado = 1
AND NOT EXISTS(
SELECT f.IdEmpresa FROM aca_AlumnoRetiro AS F with (nolock) 
where mco.IdEmpresa = f.IdEmpresa and mat.IdMatricula = f.IdMatricula and f.Estado = 1
)