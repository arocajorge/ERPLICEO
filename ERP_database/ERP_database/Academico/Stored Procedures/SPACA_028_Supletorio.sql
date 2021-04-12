CREATE PROCEDURE [Academico].[SPACA_028_Supletorio]
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
SELECT mc.IdEmpresa, mc.IdMatricula, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, c.IdAlumno, 
al.Codigo, p.pe_nombreCompleto NombreAlumno, a.Descripcion, cm.NomMateria, cm.OrdenMateria,
case when @IdCatalogoTipo=6 then null else 
case when @IdCatalogoTipo = 7 then mc.PromedioFinal else null end
end PromedioFinal,
a.PromedioMinimoPromocion
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) inner JOIN
dbo.aca_Matricula AS c with (nolock) ON mc.IdEmpresa = c.IdEmpresa AND mc.IdMatricula = c.IdMatricula INNER JOIN
dbo.aca_AnioLectivo AS a with (nolock) ON c.IdAnio = a.IdAnio AND c.IdEmpresa = a.IdEmpresa 
inner join dbo.aca_Alumno as al with (nolock) on al.IdEmpresa=c.IdEmpresa and al.IdAlumno=c.IdAlumno
inner join tb_persona as p with (nolock) on p.IdPersona=al.IdPersona
left JOIN
dbo.aca_AnioLectivo_Curso_Materia AS cm with (nolock) ON c.IdEmpresa = cm.IdEmpresa AND c.IdAnio = cm.IdAnio AND c.IdSede = cm.IdSede AND c.IdNivel = cm.IdNivel AND c.IdJornada = cm.IdJornada AND c.IdCurso = cm.IdCurso AND 
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
SELECT f.IdEmpresa FROM aca_AlumnoRetiro AS F with (nolock) 
where mc.IdEmpresa = f.IdEmpresa and c.IdMatricula = f.IdMatricula and f.Estado = 1
)