CREATE PROCEDURE [Academico].[SPACA_028_Promedios]
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
SELECT mc.IdEmpresa, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, 
                  jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, pp.pe_nombreCompleto NombreProfesor, cm.NomMateria, cm.OrdenMateria,ROUND(AVG( ISNULL(mc.PromedioFinalQ1,0)),2) as Quim1, ROUND(AVG( ISNULL(mc.ExamenQ1,0)),2) as EXQuim1, ROUND(AVG(ISNULL(mc.PromedioFinalQ2,0)),2) as Quim2, ROUND(AVG( ISNULL(mc.ExamenQ2,0)),2) as EXQuim2
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
                  dbo.aca_Matricula AS c ON mc.IdEmpresa = c.IdEmpresa AND mc.IdMatricula = c.IdMatricula INNER JOIN
                  dbo.aca_AnioLectivo AS a ON c.IdAnio = a.IdAnio AND c.IdEmpresa = a.IdEmpresa 
				  LEFT OUTER JOIN aca_Profesor pro on pro.IdEmpresa=mc.IdEmpresa and pro.IdProfesor=mc.IdProfesor
				  inner join tb_persona pp on pp.IdPersona=pro.IdPersona
				  INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia AS cm ON c.IdEmpresa = cm.IdEmpresa AND c.IdAnio = cm.IdAnio AND c.IdSede = cm.IdSede AND c.IdNivel = cm.IdNivel AND c.IdJornada = cm.IdJornada AND c.IdCurso = cm.IdCurso AND 
                  mc.IdMateria = cm.IdMateria LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn ON nj.IdEmpresa = sn.IdEmpresa AND nj.IdAnio = sn.IdAnio AND nj.IdSede = sn.IdSede AND nj.IdNivel = sn.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
                  c.IdEmpresa = cp.IdEmpresa AND c.IdAnio = cp.IdAnio AND c.IdSede = cp.IdSede AND c.IdNivel = cp.IdNivel AND c.IdJornada = cp.IdJornada AND c.IdCurso = cp.IdCurso AND c.IdParalelo = cp.IdParalelo

where mc.IdEmpresa= @IdEmpresa 
and c.IdAnio = @IdAnio
and c.IdSede = @IdSede
and c.IdNivel = @IdNivel
and c.IdJornada = @IdJornada
and c.IdCurso = @IdCurso
and c.IdParalelo = @IdParalelo
and a.Estado = 1

GROUP BY mc.IdEmpresa, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, 
                  jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, pp.pe_nombreCompleto, cm.NomMateria, cm.OrdenMateria, ISNULL(mc.PromedioQ1,0), ISNULL(mc.PromedioQ2,0)