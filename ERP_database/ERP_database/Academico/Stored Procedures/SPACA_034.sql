CREATE PROCEDURE [Academico].[SPACA_034]
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
--EVALUCACION COMPORTAMIENTO--
SELECT mco.IdEmpresa, m.IdAnio, m.IdSede,m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo,m.IdAlumno, mco.IdMatricula, 
0 IdMateria, null NombreMateria, null NombreGrupo,
0 OrdenMateria, 0 OrdenGrupo,0 PromediarGrupo, NULL IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo,
cast(equiv.Letra as varchar) as Calificacion, CAST(equiv.Calificacion AS numeric(18,2)) CalificacionNumerica,'EVALUACION DEL COMPORTAMIENTO' Columna, 1 OrdenColumna
FROM     dbo.aca_MatriculaConducta AS mco 
INNER JOIN dbo.aca_Matricula AS m ON mco.IdEmpresa = m.IdEmpresa AND mco.IdMatricula = m.IdMatricula
inner join dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno 
INNER JOIN dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona 
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv on equiv.IdEmpresa=mco.IdEmpresa and equiv.IdAnio=m.IdAnio and equiv.Secuencia = mco.SecuenciaPromedioFinal
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mco.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
---PROMEDIO MATERIAS CUALITATIVAS--
UNION ALL 
(
SELECT
PROY.IdEmpresa, PROY.IdAnio, PROY.IdSede, PROY.IdNivel, PROY.IdJornada, PROY.IdCurso, PROY.IdParalelo, PROY.IdAlumno, PROY.IdMatricula, 0 IdMateria, 
null NomMateria, PROY.NomMateriaGrupo,PROY.OrdenMateria, PROY.OrdenMateriaGrupo, PROY.PromediarGrupo, PROY.IdCatalogoTipoCalificacion,
PROY.Codigo, PROY.NombreAlumno, PROY.Descripcion,PROY.NomSede, PROY.NomNivel, PROY.OrdenNivel, PROY.NomJornada, 
PROY.OrdenJornada, PROY.NomCurso, PROY.OrdenCurso, PROY.CodigoParalelo, PROY.NomParalelo, PROY.OrdenParalelo, 
LPROM.Codigo Calificacion, PROY.CalificacionNumerica, 'EVALUACION DE PROYECTOS ESCOLARES' Columna, 2 OrdenColumna
FROM(
SELECT 
PROM.IdEmpresa, PROM.IdAnio, PROM.IdSede, PROM.IdNivel, PROM.IdJornada, PROM.IdCurso, PROM.IdParalelo, PROM.IdAlumno, PROM.IdMatricula, null IdMateria, 
null NomMateria, null NomMateriaGrupo,PROM.OrdenMateria, PROM.OrdenMateriaGrupo, PROM.PromediarGrupo, PROM.IdCatalogoTipoCalificacion,
PROM.Codigo, PROM.NombreAlumno, PROM.Descripcion,PROM.NomSede, PROM.NomNivel, PROM.OrdenNivel, PROM.NomJornada, 
PROM.OrdenJornada, PROM.NomCurso, PROM.OrdenCurso, PROM.CodigoParalelo, PROM.NomParalelo, PROM.OrdenParalelo,
dbo.BankersRounding(AVG(PROM.CalificacionNumerica),2) CalificacionNumerica
FROM(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, null NomMateriaGrupo, 1 OrdenMateria, 1 OrdenMateriaGrupo, 0 PromediarGrupo,a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,
cast(max(a.CalificacionQUIM) as varchar)  Calificacion, CAST(max(a.CalificacionNumericaQUIM) AS numeric(18,2)) CalificacionNumerica
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaGrupo, MC.OrdenMateria, 
	MC.OrdenMateriaGrupo, mc.PromediarGrupo, mc.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
	cp.NomParalelo, cp.OrdenParalelo, epQ.Codigo CalificacionQUIM, epQ.Calificacion CalificacionNumericaQUIM
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ ON m.IdAnio = epQ.IdAnio AND b.IdEmpresa = epQ.IdEmpresa AND b.IdCalificacionCualitativaQ2 = epQ.IdCalificacionCualitativa 
	LEFT OUTER JOIN
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
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo
)PROM
group by
PROM.IdEmpresa, PROM.IdAnio, PROM.IdSede, PROM.IdNivel, PROM.IdJornada, PROM.IdCurso, PROM.IdParalelo, PROM.IdAlumno, PROM.IdMatricula, PROM.IdMateria, 
PROM.NomMateria, PROM.NomMateriaGrupo, PROM.OrdenMateria, PROM.OrdenMateriaGrupo, PROM.PromediarGrupo, PROM.IdCatalogoTipoCalificacion,
PROM.Codigo, PROM.NombreAlumno, PROM.Descripcion,PROM.NomSede, PROM.NomNivel, PROM.OrdenNivel, PROM.NomJornada, 
PROM.OrdenJornada, PROM.NomCurso, PROM.OrdenCurso, PROM.CodigoParalelo, PROM.NomParalelo, PROM.OrdenParalelo
)PROY
LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS LPROM ON PROY.IdAnio = LPROM.IdAnio AND PROY.IdEmpresa = LPROM.IdEmpresa 
AND PROY.CalificacionNumerica = LPROM.Calificacion
)
---MATERIAS QUE NO SE PROMEDIAN---
---QUIMESTRE I---
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
CAST(mc.PromedioFinalQ1 as varchar) Calificacion, CAST(mc.PromedioFinalQ1 AS numeric(18,2)) AS CalificacionNumerica, 'I QUIMESTRE' as Columna, 1 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)
---QUIMESTRE II---
UNION ALL 
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
CAST(mc.PromedioFinalQ2 as varchar) Calificacion, CAST(mc.PromedioFinalQ2 AS numeric(18,2)) AS CalificacionNumerica, 'II QUIMESTRE' as Columna, 2 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)
---PROMEDIO DE LOS 2 QUIMESTRES---
UNION ALL 
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
CAST(mc.PromedioQuimestres as varchar) Calificacion, CAST(mc.PromedioQuimestres AS numeric(18,2)) AS CalificacionNumerica, 'PROMEDIO' as Columna, 3 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)
---SUPLETORIO---
UNION ALL 
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
CASE WHEN (mc.ExamenGracia>0) then CAST(mc.ExamenGracia as varchar)
else  case when (mc.ExamenRemedial>0) then CAST(mc.ExamenRemedial as varchar)
	else case when mc.ExamenSupletorio!= null then CAST(mc.ExamenSupletorio as varchar) else null end
	end 
end Calificacion,
CASE WHEN mc.ExamenGracia>0 then CAST(mc.ExamenGracia as numeric(18,2))
else case when mc.ExamenRemedial >0then CAST(mc.ExamenRemedial as numeric(18,2))
else case when mc.ExamenSupletorio>0 then CAST(mc.ExamenSupletorio as numeric(18,2)) else null end
end 
end CalificacionNumerica,
'SUPLETORIO' as Columna, 4 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)
-- PROMEDIO FINAL DE MATERIAS QUE NO SE PROMEDIAN
UNION ALL 
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
CAST(mc.PromedioFinal as varchar) Calificacion, CAST(mc.PromedioFinal AS numeric(18,2)) AS CalificacionNumerica, 'PROMEDIO FINAL' as Columna, 5 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)



---MATERIAS QUE SE PROMEDIAN
---QUIMESTRE I---
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, CAST(mc.PromedioFinalQ1 as varchar) Calificacion,
PromedioFinalQ1 as CalificacionNumerica,
'I QUIMESTRE' as Columna, 1 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)
---QUIMESTRE II---
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, CAST(mc.PromedioFinalQ2 as varchar) Calificacion,
PromedioFinalQ2 as CalificacionNumerica,
'II QUIMESTRE' as Columna, 2 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)
---PROMEDIOS DE LOS 2 QUIMESTRES---
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, CAST(mc.PromedioQuimestres as varchar) Calificacion,
PromedioQuimestres as CalificacionNumerica,
'PROMEDIO' as Columna, 2 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)
---SUPLETORIO---
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo,
CASE WHEN (mc.ExamenGracia>=0) then CAST(mc.ExamenGracia as varchar)
else  case when (mc.ExamenRemedial>=0) then CAST(mc.ExamenRemedial as varchar)
	else case when mc.ExamenSupletorio>=0 then CAST(mc.ExamenSupletorio as varchar) else null end
	end 
end Calificacion,
CASE WHEN mc.ExamenGracia>=0 then CAST(mc.ExamenGracia as numeric(18,2))
else case when mc.ExamenRemedial >=0then CAST(mc.ExamenRemedial as numeric(18,2))
else case when mc.ExamenSupletorio>=0 then CAST(mc.ExamenSupletorio as numeric(18,2)) else null end
end 
end CalificacionNumerica,
'SUPLETORIO' as Columna, 3 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)

---PROMEDIO X MATERIA OPTATIVA---
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, CAST(mc.PromedioFinal as varchar) Calificacion,
mc.PromedioFinal as CalificacionNumerica,
'PROMEDIO FINAL' as Columna, 6 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)

--------------MATERIA PROMEDIADA---
---I QUIMESTRE---
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, null IdMateria,
'OPTATIVA' NomMateria, cm.NomMateriaGrupo, 999 OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, null Calificacion, null CalificacionNumerica, 'I QUIMESTRE' Columna, 1 OrdenColuma
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
    dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
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
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
group by
mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo,
cp.NomParalelo, cp.OrdenParalelo
)
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, null IdMateria,
'OPTATIVA' NomMateria, cm.NomMateriaGrupo, 999 OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, null Calificacion, null CalificacionNumerica, 'II QUIMESTRE' Columna, 2 OrdenColuma
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
    dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
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
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
group by
mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo,
cp.NomParalelo, cp.OrdenParalelo
)
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, null IdMateria,
'OPTATIVA' NomMateria, cm.NomMateriaGrupo, 999 OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, null Calificacion, null CalificacionNumerica, 'PROMEDIO' Columna, 3 OrdenColuma
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
    dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
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
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
group by
mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo,
cp.NomParalelo, cp.OrdenParalelo
)
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, null IdMateria,
'OPTATIVA' NomMateria, cm.NomMateriaGrupo, 999 OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, null Calificacion, null CalificacionNumerica, 'SUPLETORIO' Columna, 4 OrdenColuma
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
    dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
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
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
group by
mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo,
cp.NomParalelo, cp.OrdenParalelo
)
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, null IdMateria,
'OPTATIVA' NomMateria, cm.NomMateriaGrupo, 999 OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, null Calificacion, null CalificacionNumerica, 'PROMEDIO FINAL' Columna, 5 OrdenColuma
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
    dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
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
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
group by
mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo,
cp.NomParalelo, cp.OrdenParalelo
)
/*
UNION ALL 
(
select 
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, 
null IdMateria, 'OPTATIVAS' NomMateria, 'OPTATIVAS' MateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 1 PromediarGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede,
a.NomNivel, a.OrdenNivel,a.NomJornada, a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,
a.Calificacion, a.CalificacionNumerica, 'I QUIMESTRE' Columna,7 OrdenColumna
from (
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula,
cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, CAST(dbo.BankersRounding(AVG(mc.PromedioFinalQ1),2) as varchar) Calificacion,
dbo.BankersRounding(AVG(mc.PromedioFinalQ1),2) CalificacionNumerica
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
    dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
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
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
group by
mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula,
cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo
)a
)
---II QUIMESTRE---
UNION ALL 
(
select 
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, 
null IdMateria, 'OPTATIVAS' NomMateria, 'OPTATIVAS' MateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 1 PromediarGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede,
a.NomNivel, a.OrdenNivel,a.NomJornada, a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,
a.Calificacion, a.CalificacionNumerica, 'II QUIMESTRE' Columna,7 OrdenColumna
from (
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula,
cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, CAST(dbo.BankersRounding(AVG(mc.PromedioFinalQ2),2) as varchar) Calificacion,
dbo.BankersRounding(AVG(mc.PromedioFinalQ2),2) CalificacionNumerica
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
    dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
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
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
group by
mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula,
cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo
)a
)
---PROMEDIO---
UNION ALL
(
select 
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, 
null IdMateria, 'OPTATIVAS' NomMateria, 'OPTATIVAS' MateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 1 PromediarGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede,
a.NomNivel, a.OrdenNivel,a.NomJornada, a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,
a.Calificacion, a.CalificacionNumerica, 'OPTATIVAS' Columna,7 OrdenColumna
from (
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula,
cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, CAST(dbo.BankersRounding(AVG(mc.PromedioFinal),2) as varchar) Calificacion,
dbo.BankersRounding(AVG(mc.PromedioFinal),2) CalificacionNumerica
FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN
    dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
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
and m.IdParalelo = @IdParalelo
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
group by
mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula,
cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo
)a
)*/