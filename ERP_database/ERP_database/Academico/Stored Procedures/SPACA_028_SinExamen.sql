
CREATE PROCEDURE [Academico].[SPACA_028_SinExamen]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdCatalogoTipo int
)
AS
SELECT mc.IdEmpresa, mc.IdMatricula, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, c.IdAlumno, a.Descripcion, 
p.pe_nombreCompleto NombreAlumno, al.Codigo, cm.NomMateria, cm.OrdenMateria,
case when @IdCatalogoTipo=6 then mc.ExamenQ1 else 
case when @IdCatalogoTipo = 7 then mc.ExamenQ2 else null end
end Examen
FROM     dbo.aca_MatriculaCalificacion AS mc inner JOIN
dbo.aca_Matricula AS c ON mc.IdEmpresa = c.IdEmpresa AND mc.IdMatricula = c.IdMatricula INNER JOIN
dbo.aca_AnioLectivo AS a ON c.IdAnio = a.IdAnio AND c.IdEmpresa = a.IdEmpresa 
inner join dbo.aca_Alumno as al on al.IdEmpresa=c.IdEmpresa and al.IdAlumno=c.IdAlumno
inner join tb_persona as p on p.IdPersona=al.IdPersona
left outer JOIN
dbo.aca_AnioLectivo_Curso_Materia AS cm ON c.IdEmpresa = cm.IdEmpresa AND c.IdAnio = cm.IdAnio AND c.IdSede = cm.IdSede AND c.IdNivel = cm.IdNivel AND c.IdJornada = cm.IdJornada AND c.IdCurso = cm.IdCurso AND 
mc.IdMateria = cm.IdMateria
where mc.IdEmpresa= @IdEmpresa 
and c.IdAnio = @IdAnio
and c.IdSede = @IdSede
and c.IdNivel = @IdNivel
and c.IdJornada = @IdJornada
and c.IdCurso = @IdCurso
and c.IdParalelo = @IdParalelo
and a.Estado = 1
AND NOT EXISTS(
SELECT f.IdEmpresa FROM aca_AlumnoRetiro AS F
where mc.IdEmpresa = f.IdEmpresa and c.IdMatricula = f.IdMatricula and f.Estado = 1
)