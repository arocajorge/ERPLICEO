CREATE PROCEDURE [Academico].[SPACA_029_Promedios]
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
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, d.IdMateria,
 AVG(d.PromedioFinalQ1) PromedioFinalQ1, AVG(d.PromedioFinalQ2) PromedioFinalQ2,
 AVG(d.ExamenQ1) ExamenQ1, AVG(d.ExamenQ2) ExamenQ2
from aca_Matricula as a with (nolock) inner join
aca_Alumno as b with (nolock) on a.IdEmpresa = b.IdEmpresa and a.IdAlumno = b.IdAlumno inner join
tb_persona as c with (nolock) on b.IdPersona = c.IdPersona left join
aca_MatriculaCalificacion as d with (nolock) on a.IdEmpresa = d.IdEmpresa and a.IdMatricula = d.IdMatricula
--WHERE a.IdEmpresa = 1 and a.IdAnio = 1 AND a.IdSede = 1 AND a.IdNivel = 4 AND a.IdJornada = 1 AND a.IdCurso = 7 AND a.IdParalelo = 1 and d.IdMateria = 16
where a.IdEmpresa= @IdEmpresa 
and a.IdAnio = @IdAnio
and a.IdSede = @IdSede
and a.IdNivel = @IdNivel
and a.IdJornada = @IdJornada
and a.IdCurso = @IdCurso
and a.IdParalelo = @IdParalelo
and d.IdMateria = @IdMateria
and b.Estado = 1
AND NOT EXISTS(
SELECT f.IdEmpresa FROM aca_AlumnoRetiro AS F with (nolock) 
where a.IdEmpresa = f.IdEmpresa and a.IdMatricula = f.IdMatricula and f.Estado = 1
)
group by a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, d.IdMateria