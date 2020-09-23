
create PROCEDURE [Academico].[SPACA_028_PromedioConducta]
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
SELECT mco.IdEmpresa, mat.IdAnio, mat.IdSede, mat.IdNivel, mat.IdJornada, mat.IdCurso, mat.IdParalelo,
dbo.BankersRounding(avg(mco.PromedioFinalQ1),2) AS CalificacionQ1,  dbo.BankersRounding(avg(mco.PromedioFinalQ2),2) AS CalificacionQ2
FROM     dbo.aca_MatriculaConducta AS mco INNER JOIN
                  dbo.aca_Matricula AS mat ON mco.IdEmpresa = mat.IdEmpresa AND mco.IdMatricula = mat.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS al ON mat.IdEmpresa = al.IdEmpresa AND mat.IdAlumno = al.IdAlumno 
				  LEFT OUTER JOIN
                  dbo.aca_Matricula AS m ON al.IdAlumno = m.IdAlumno AND al.IdEmpresa = m.IdEmpresa AND mco.IdEmpresa = m.IdEmpresa AND mco.IdMatricula = m.IdMatricula 
where mco.IdEmpresa= @IdEmpresa 
and mat.IdAnio = @IdAnio
and mat.IdSede = @IdSede
and mat.IdNivel = @IdNivel
and mat.IdJornada = @IdJornada
and mat.IdCurso = @IdCurso
and mat.IdParalelo = @IdParalelo
and al.Estado = 1
AND NOT EXISTS(
SELECT f.IdEmpresa FROM aca_AlumnoRetiro AS F
where mco.IdEmpresa = f.IdEmpresa and mat.IdMatricula = f.IdMatricula and f.Estado = 1
)         
group by mco.IdEmpresa, mat.IdAnio, mat.IdSede, mat.IdNivel, mat.IdJornada, mat.IdCurso, mat.IdParalelo