CREATE PROCEDURE [Academico].[SPACA_014]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdAlumno numeric,
@MostrarRetirados bit
)
AS
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, 
cm.NomMateria, cm.NomMateriaArea, cm.NomMateriaGrupo, cm.EsObligatorio, cm.OrdenMateriaArea, cm.OrdenMateriaGrupo, cm.PromediarGrupo,
cm.OrdenMateria, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, 
nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto as NombreInspector, pre.pe_nombreCompleto as NombreRepresentante,
cast(mc.CalificacionP1 as varchar) CalificacionP1, ep1.Codigo AS EquivalenciaPromedioP1,
cast(mc.CalificacionP2 as varchar) CalificacionP2, ep2.Codigo AS EquivalenciaPromedioP2,
cast(mc.CalificacionP3 as varchar) CalificacionP3, ep3.Codigo AS EquivalenciaPromedioP3,
cast(dbo.BankersRounding((mc.PromedioQ1 * 0.80),2) as varchar) PorcentajePromedioQ1, 
cast(dbo.BankersRounding((mc.ExamenQ1 * 0.20),2) as varchar) PorcentajeExamenQ1,
cast(mc.ExamenQ1 as varchar) ExamenQ1, epEQ1.Codigo AS EquivalenciaPromedioEQ1,
cast(mc.PromedioFinalQ1 as varchar) PromedioFinalQ1, epQ1.Codigo AS EquivalenciaPromedioQ1,
cast(mc.CalificacionP4 as varchar) CalificacionP4, ep4.Codigo AS EquivalenciaPromedioP4,
cast(mc.CalificacionP5 as varchar) CalificacionP5, ep5.Codigo AS EquivalenciaPromedioP5,
cast(mc.CalificacionP6 as varchar) CalificacionP6, ep6.Codigo AS EquivalenciaPromedioP6,
cast(dbo.BankersRounding((mc.PromedioQ2*0.80),2) as varchar) PorcentajePromedioQ2, 
cast(dbo.BankersRounding((mc.ExamenQ2 * 0.20),2) as varchar) PorcentajeExamenQ2,
cast(mc.ExamenQ2 as varchar) ExamenQ2, epEQ2.Codigo AS EquivalenciaPromedioEQ2,
cast(mc.PromedioFinalQ2 as varchar) PromedioFinalQ2, epQ2.Codigo AS EquivalenciaPromedioQ2,
cast(mc.PromedioQuimestres as varchar) as PromedioQuimestres_PF, 
case when (mc.ExamenMejoramiento is not null) then cast(mc.PromedioFinal as varchar)
else  cast(mc.PromedioQuimestres as varchar) end as Promedio_PR, 
cast(mc.ExamenMejoramiento as varchar) ExamenMejoramiento, 
cast(mc.CampoMejoramiento as varchar) CampoMejoramiento, 
cast(mc.ExamenSupletorio as varchar) ExamenSupletorio, 
cast(mc.ExamenRemedial as varchar) ExamenRemedial, 
cast(mc.ExamenGracia as varchar) ExamenGracia, 
cast(mc.PromedioFinal as varchar) PromedioFinal, mc.IdEquivalenciaPromedioPF, epPF.Codigo AS EquivalenciaPromedioPF
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
    dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
	LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep1 ON m.IdAnio = ep1.IdAnio AND mc.IdEmpresa = ep1.IdEmpresa AND mc.IdEquivalenciaPromedioP1 = ep1.IdEquivalenciaPromedio 
	LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep2 ON m.IdAnio = ep2.IdAnio AND mc.IdEmpresa = ep2.IdEmpresa AND mc.IdEquivalenciaPromedioP2 = ep2.IdEquivalenciaPromedio 
	LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep3 ON m.IdAnio = ep3.IdAnio AND mc.IdEmpresa = ep3.IdEmpresa AND mc.IdEquivalenciaPromedioP3 = ep3.IdEquivalenciaPromedio 
	LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS epEQ1 ON m.IdAnio = epEQ1.IdAnio AND mc.IdEmpresa = epEQ1.IdEmpresa AND mc.IdEquivalenciaPromedioEQ1 = epEQ1.IdEquivalenciaPromedio 
	LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS epQ1 ON m.IdAnio = epQ1.IdAnio AND mc.IdEmpresa = epQ1.IdEmpresa AND mc.IdEquivalenciaPromedioQ1 = epQ1.IdEquivalenciaPromedio 
	LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep4 ON m.IdAnio = ep4.IdAnio AND mc.IdEmpresa = ep4.IdEmpresa AND mc.IdEquivalenciaPromedioP4 = ep4.IdEquivalenciaPromedio 
	LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep5 ON m.IdAnio = ep5.IdAnio AND mc.IdEmpresa = ep5.IdEmpresa AND mc.IdEquivalenciaPromedioP5 = ep5.IdEquivalenciaPromedio 
	LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS ep6 ON m.IdAnio = ep6.IdAnio AND mc.IdEmpresa = ep6.IdEmpresa AND mc.IdEquivalenciaPromedioP6 = ep6.IdEquivalenciaPromedio 
	LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS epEQ2 ON m.IdAnio = epEQ2.IdAnio AND mc.IdEmpresa = epEQ2.IdEmpresa AND mc.IdEquivalenciaPromedioEQ2 = epEQ2.IdEquivalenciaPromedio 
	LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS epQ2 ON m.IdAnio = epQ2.IdAnio AND mc.IdEmpresa = epQ2.IdEmpresa AND mc.IdEquivalenciaPromedioQ2 = epQ2.IdEquivalenciaPromedio 
	LEFT OUTER JOIN dbo.aca_AnioLectivoEquivalenciaPromedio AS epPF ON m.IdAnio = epPF.IdAnio AND mc.IdEmpresa = epPF.IdEmpresa AND mc.IdEquivalenciaPromedioPF = epPF.IdEquivalenciaPromedio
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
	LEFT OUTER JOIN aca_Profesor AS pro ON cp.IdEmpresa = pro.IdEmpresa and cp.IdProfesorInspector = pro.IdProfesor
	LEFT OUTER JOIN tb_persona as pins on pins.IdPersona = pro.IdPersona
	LEFT OUTER JOIN tb_persona as pre on pre.IdPersona = m.IdPersonaR
	LEFT JOIN	
	(
	select r.IdEmpresa, r.IdMatricula 
	from aca_AlumnoRetiro as r
	where r.Estado = 1
	) as ret on m.IdEmpresa =ret.IdEmpresa and m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
UNION ALL
(

--- CALIFICACIONES CUALITATIVAS  
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaArea, a.NomMateriaGrupo, a.EsObligatorio, a.OrdenMateriaArea, a.OrdenMateriaGrupo, a.PromediarGrupo,
a.OrdenMateria,a.IdCatalogoTipoCalificacion, a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector, a.NombreRepresentante,
cast(max(a.CalificacionP1) as varchar) CalificacionP1, null EquivalenciaP1,
cast(max(a.CalificacionP2) as varchar) CalificacionP2, null EquivalenciaP2,
cast(max(a.CalificacionP3) as varchar) CalificacionP3, null EquivalenciaP3,
null PorcentajePromedioQ1, null PorcentajeExamenQ1,null ExamenQ1, null EquivalenciaPromedioEQ1, 
cast(max(a.PromedioFinalQ1) as varchar) PromedioFinalQ1, null EquivalenciaPromedioQ1,
cast(max(a.CalificacionP4) as varchar) CalificacionP4,null EquivalenciaP4, 
cast(max(a.CalificacionP5) as varchar) CalificacionP5, null EquivalenciaP5, 
cast(max(a.CalificacionP6) as varchar) CalificacionP6, null EquivalenciaP6,
null PorcentajePromedioQ2, null PorcentajeExamenQ2,null ExamenQ2, null EquivalenciaPromedioEQ2, 
cast(max(a.PromedioFinalQ2) as varchar) PromedioFinalQ2, null EquivalenciaPromedioQ2,
null PromedioQuimestres_PF, null Promedio_PR, null ExamenMejoramiento, null CampoMejoramiento, null ExamenSupletorio, null ExamenRemedial, null ExamenGracia, 
cast(max(a.PromedioFinal) as varchar) PromedioFinal, null IdEquivalenciaPromedioPF, null EquivalenciaPromedioPF
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, 
MC.NomMateriaArea, MC.NomMateriaGrupo, MC.PromediarGrupo, MC.EsObligatorio, MC.OrdenMateria,  MC.IdCatalogoTipoCalificacion,
MC.OrdenMateriaGrupo, MC.OrdenMateriaArea, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, 
sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, pre.pe_nombreCompleto NombreRepresentante,
CASE WHEN IdCatalogoParcial = 28 THEN acc.Codigo END AS CalificacionP1, 
CASE WHEN IdCatalogoParcial = 29 THEN acc.Codigo END AS CalificacionP2, 
CASE WHEN IdCatalogoParcial = 30 THEN acc.Codigo END AS CalificacionP3, 
CASE WHEN IdCatalogoParcial = 31 THEN acc.Codigo END AS CalificacionP4, 
CASE WHEN IdCatalogoParcial = 32 THEN acc.Codigo END AS CalificacionP5, 
CASE WHEN IdCatalogoParcial = 33 THEN acc.Codigo END AS CalificacionP6,
pq1.Codigo as PromedioFinalQ1,
pq2.Codigo as PromedioFinalQ2,
pf.Codigo as PromedioFinal
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a INNER JOIN
	dbo.aca_Matricula AS m ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
	dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
	dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona LEFT OUTER JOIN
	dbo.aca_AnioLectivoCalificacionCualitativa AS acc ON acc.IdEmpresa = a.IdEmpresa AND acc.IdAnio = m.IdAnio AND acc.IdCalificacionCualitativa = a.IdCalificacionCualitativa LEFT OUTER JOIN
	dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
	dbo.aca_AnioLectivo_Curso_Paralelo AS cp RIGHT OUTER JOIN
	dbo.aca_AnioLectivo_Jornada_Curso AS jc LEFT OUTER JOIN
	dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn INNER JOIN
	dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
	jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
	dbo.aca_AnioLectivo_Curso_Materia AS MC ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
	cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
	m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
	m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN aca_Profesor AS pro ON cp.IdEmpresa = pro.IdEmpresa and cp.IdProfesorInspector = pro.IdProfesor
	LEFT OUTER JOIN tb_persona as pins on pins.IdPersona = pro.IdPersona
	LEFT OUTER JOIN tb_persona as pre on pre.IdPersona = m.IdPersonaR
LEFT JOIN	
	(
	select r.IdEmpresa, r.IdMatricula 
	from aca_AlumnoRetiro as r
	where r.Estado = 1
	) as ret on m.IdEmpresa =ret.IdEmpresa and m.IdMatricula = ret.IdMatricula
left join aca_MatriculaCalificacionCualitativaPromedio pr on pr.IdEmpresa = a.IdEmpresa and pr.IdMatricula=a.IdMatricula and pr.IdMateria=a.IdMateria
left join aca_AnioLectivoCalificacionCualitativa pq1 on pq1.IdEmpresa=pr.IdEmpresa and pq1.IdAnio=m.IdAnio and pq1.IdCalificacionCualitativa=pr.IdCalificacionCualitativaQ1
left join aca_AnioLectivoCalificacionCualitativa pq2 on pq2.IdEmpresa=pr.IdEmpresa and pq2.IdAnio=m.IdAnio and pq2.IdCalificacionCualitativa=pr.IdCalificacionCualitativaQ2
left join aca_AnioLectivoCalificacionCualitativa pf on pf.IdEmpresa=pr.IdEmpresa and pf.IdAnio=m.IdAnio and pf.IdCalificacionCualitativa=pr.IdCalificacionCualitativaFinal
--WHERE  (a.IdEmpresa = 1) AND (m.IdAnio = 1) AND (m.IdSede = 1) AND (m.IdNivel = 4) AND (m.IdJornada = 1) AND (m.IdCurso = 7) AND (m.IdParalelo = 1)
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaArea, a.NomMateriaGrupo, a.EsObligatorio, a.OrdenMateriaArea, a.OrdenMateriaGrupo,a.PromediarGrupo,
a.OrdenMateria, a.IdCatalogoTipoCalificacion,a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector, a.NombreRepresentante
-----------------
)