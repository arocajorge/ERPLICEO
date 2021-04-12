CREATE PROCEDURE [Academico].[SPACA_028_PromediosBajos]
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
SELECT mc.IdEmpresa, mc.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, a.Codigo, p.pe_nombreCompleto, mc.IdMateria, cm.NomMateria, cm.OrdenMateriaGrupo, cm.OrdenMateria, 
                  mc.PromedioFinalQ1, mc.CausaQ1, mc.ResolucionQ1, mc.PromedioFinalQ2, mc.CausaQ2, mc.ResolucionQ2, an.PromedioMinimoPromocion
FROM     dbo.aca_Alumno AS a with (nolock) RIGHT OUTER JOIN
                  dbo.aca_Matricula AS m with (nolock) ON a.IdEmpresa = m.IdEmpresa AND a.IdAlumno = m.IdAlumno LEFT OUTER JOIN
                  dbo.tb_persona AS p with (nolock) ON a.IdPersona = p.IdPersona RIGHT OUTER JOIN
                  dbo.aca_MatriculaCalificacion AS mc with (nolock) ON m.IdEmpresa = mc.IdEmpresa AND m.IdMatricula = mc.IdMatricula LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia AS cm with (nolock) ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
                  mc.IdMateria = cm.IdMateria LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS an with (nolock) ON m.IdEmpresa = an.IdEmpresa AND m.IdAnio = an.IdAnio
				  LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
					where r.Estado = 1
					) as ret on m.IdEmpresa =ret.IdEmpresa and m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa= @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and a.Estado = 1
AND NOT EXISTS(
SELECT f.IdEmpresa FROM aca_AlumnoRetiro AS F with (nolock) 
where mc.IdEmpresa = f.IdEmpresa and m.IdMatricula = f.IdMatricula and f.Estado = 1
)