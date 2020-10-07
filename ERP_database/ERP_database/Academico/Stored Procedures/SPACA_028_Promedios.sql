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
                  jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, pp.pe_nombreCompleto NombreProfesor,cm.NomMateria, cm.OrdenMateriaGrupo, cm.OrdenMateria,cm.IdCatalogoTipoCalificacion,
				  cast(dbo.BankersRounding(AVG(mc.PromedioFinalQ1),2) as varchar) as Quim1, 
				  cast((dbo.BankersRounding(AVG(mc.PromedioFinalQ1),2)* 10) as varchar) as PorcentajeQuim1,
				  cast(dbo.BankersRounding(AVG( mc.ExamenQ1),2) as varchar) as EXQuim1, 
				  cast(dbo.BankersRounding(AVG(mc.PromedioFinalQ2),2) as varchar) as Quim2, 
				  cast((dbo.BankersRounding(AVG(mc.PromedioFinalQ2),2)* 10) as varchar) as PorcentajeQuim2,
				  cast(dbo.BankersRounding(AVG(mc.ExamenQ2),2) as varchar) as EXQuim2
FROM     dbo.aca_MatriculaCalificacion AS mc inner JOIN
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
AND NOT EXISTS(
SELECT f.IdEmpresa FROM aca_AlumnoRetiro AS F
where mc.IdEmpresa = f.IdEmpresa and c.IdMatricula = f.IdMatricula and f.Estado = 1
)
--and case when @IdCatalogoQuimestre = 6 then mc.PromedioFinalQ1 else mc.PromedioFinalQ2 end is not null

GROUP BY mc.IdEmpresa, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, 
                  jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, pp.pe_nombreCompleto, cm.IdMateria,cm.NomMateria, cm.OrdenMateriaGrupo, cm.OrdenMateria,cm.IdCatalogoTipoCalificacion

UNION ALL
(
	select 
	x.IdEmpresa, x.IdMateria, x.IdProfesor, x.IdAnio, x.IdSede, x.IdNivel, x.IdJornada, x.IdCurso, x.IdParalelo, x.Descripcion, x.NomSede, 
	x.NomNivel, x.OrdenNivel, x.NomJornada, x.OrdenJornada, x.NomCurso, 
	x.OrdenCurso, x.CodigoParalelo, x.NomParalelo, x.OrdenParalelo, x.NombreProfesor, x.NomMateria, x.OrdenMateriaGrupo, x.OrdenMateria,x.IdCatalogoTipoCalificacion,
	cast(q1.Codigo as varchar) Quim1, 
	null PorcentajeQuim1,
	null as EXQuim1, 
	cast(q2.Codigo as varchar) as Quim2, 
	null PorcentajeQuim2,
	null as EXQuim2
	from
	(
	SELECT mc.IdEmpresa, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, 
		jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, pp.pe_nombreCompleto NombreProfesor,cm.NomMateria, cm.OrdenMateriaGrupo, cm.OrdenMateria,cm.IdCatalogoTipoCalificacion,
		cast(dbo.BankersRounding(AVG(mc.PromedioQ1),2) as varchar) Quim1, 
		null as EXQuim1, 
		cast(dbo.BankersRounding(AVG(mc.PromedioQ2),2) as varchar) as Quim2, 
		null as EXQuim2
	FROM     dbo.aca_MatriculaCalificacionCualitativaPromedio AS mc inner JOIN
		dbo.aca_Matricula AS c ON mc.IdEmpresa = c.IdEmpresa AND mc.IdMatricula = c.IdMatricula INNER JOIN
		dbo.aca_AnioLectivo AS a ON c.IdAnio = a.IdAnio AND c.IdEmpresa = a.IdEmpresa 
		LEFT OUTER JOIN aca_Profesor pro on pro.IdEmpresa=mc.IdEmpresa and pro.IdProfesor=mc.IdProfesor
		inner join tb_persona pp on pp.IdPersona=pro.IdPersona
		Left JOIN
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
	AND NOT EXISTS(
	SELECT f.IdEmpresa FROM aca_AlumnoRetiro AS F
	where mc.IdEmpresa = f.IdEmpresa and c.IdMatricula = f.IdMatricula and f.Estado = 1
	)
	GROUP BY mc.IdEmpresa, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, 
					  jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, pp.pe_nombreCompleto, cm.IdMateria,cm.NomMateria, cm.OrdenMateriaGrupo, cm.OrdenMateria,cm.IdCatalogoTipoCalificacion
	) as x
	left join aca_AnioLectivoCalificacionCualitativa q1 on q1.IdEmpresa=x.IdEmpresa and q1.IdAnio= x.IdAnio and q1.Calificacion >= x.Quim1
	left join aca_AnioLectivoCalificacionCualitativa q2 on q2.IdEmpresa=x.IdEmpresa and q2.IdAnio= x.IdAnio and q2.Calificacion >=x.Quim2
)