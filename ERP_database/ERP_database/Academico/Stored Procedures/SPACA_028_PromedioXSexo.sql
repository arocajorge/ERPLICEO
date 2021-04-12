CREATE PROCEDURE [Academico].[SPACA_028_PromedioXSexo]
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
SELECT A.IdEmpresa, A.Sexo, SUM(A.CantQ1) CantQ1,SUM(A.CantQ2) CantQ2, 
dbo.BankersRounding(AVG(A.PromedioFinalQ1),2) PromedioFinalQ1, dbo.BankersRounding(avg(A.PromedioFinalQ2),2) PromedioFinalQ2
FROM (
select a.IdEmpresa, a.IdMatricula, a.IdAlumno, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo,
case when c.pe_sexo = 'SEXO_FEM' THEN 'Mujer' else 'Hombre' end as Sexo, 
dbo.BankersRounding(AVG(d.PromedioFinalQ1),2) PromedioFinalQ1, 
dbo.BankersRounding(AVG(d.PromedioFinalQ2),2) PromedioFinalQ2,
case when AVG(d.PromedioFinalQ1) IS NOT NULL THEN 1 else 0 end  CantQ1,
case when AVG(d.PromedioFinalQ2) IS NOT NULL THEN 1 else 0 end  CantQ2
from aca_Matricula as a with (nolock) inner join
aca_Alumno as b with (nolock) on a.IdEmpresa = b.IdEmpresa and a.IdAlumno = b.IdAlumno inner join
tb_persona as c with (nolock) on b.IdPersona = c.IdPersona left join
aca_MatriculaCalificacion as d with (nolock) on a.IdEmpresa = d.IdEmpresa and a.IdMatricula = d.IdMatricula
where a.IdEmpresa = @IdEmpresa 
and a.IdAnio = @IdAnio
and a.IdSede = @IdSede
and a.IdNivel = @IdNivel
and a.IdJornada = @IdJornada
and a.IdCurso = @IdCurso
and a.IdParalelo = @IdParalelo
and b.Estado = 1
AND NOT EXISTS(
SELECT f.IdEmpresa FROM aca_AlumnoRetiro AS F with (nolock) 
where a.IdEmpresa = f.IdEmpresa and a.IdMatricula = f.IdMatricula and f.Estado = 1
)
group by a.IdEmpresa, a.IdMatricula, a.IdAlumno, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, c.pe_sexo
)A
GROUP BY A.IdEmpresa, A.Sexo