
CREATE PROCEDURE [Academico].[SPACA_030]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdAlumno numeric,
@MostrarRetirados bit,
@IdCatalogoParcial int
)

AS
--declare
--@IdEmpresa int=1,
--@IdAnio int=1,
--@IdSede int=1,
--@IdNivel int=4,
--@IdJornada int=1,
--@IdCurso int=7,
--@IdParalelo int=1,
--@IdAlumno numeric=0,
--@MostrarRetirados bit=0

IF @IdCatalogoParcial = 6
--MATERIAS QUE NO SE PROMEDIAN--
--P1--
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo,CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.CalificacionP1 as varchar) Calificacion, mc.CalificacionP1 as CalificacionNumerica, 'P1' as Columna, 1 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
--P2--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.CalificacionP2 as varchar) Calificacion, mc.CalificacionP2 as CalificacionNumerica,'P2' as Columna, 2 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)
--P3--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.CalificacionP3 as varchar) Calificacion,mc.CalificacionP3 as CalificacionNumerica,'P3' as Columna, 3 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)
--PROMEDIO 80%--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(dbo.BankersRounding((mc.PromedioQ1 * 0.80),2) as varchar) Calificacion,
dbo.BankersRounding((mc.PromedioQ1 * 0.80),2) as CalificacionNumerica,
'80%' as Columna, 4 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)
--EXAMEN--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.ExamenQ1 as varchar) Calificacion, mc.ExamenQ1 as CalificacionNumerica, 'EX' as Columna, 5 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
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
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)
--EXAMEN 20%--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(dbo.BankersRounding((mc.ExamenQ1 * 0.20),2)as varchar) Calificacion, 
dbo.BankersRounding((mc.ExamenQ1 * 0.20),2) as CalificacionNumerica,'20%' as Columna, 6 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)
--PROMEDIO QUIMESTRE--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.PromedioFinalQ1 as varchar) Calificacion, mc.PromedioFinalQ1 AS CalificacionNumerica, 'PF' as Columna, 7 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)

--PROMEDIO FINAL DE GRUPO DE MATERIAS QUE NO SE PROMEDIAN
UNION
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,
99 IdMateria, NULL NomMateria, CM.NomMateriaGrupo, 99 OrdenMateria, CM.OrdenMateriaGrupo,CM.PromediarGrupo, CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(dbo.BankersRounding(AVG(ISNULL(PromedioFinalQ1,0)),2) as varchar) Calificacion, 
dbo.BankersRounding(AVG(ISNULL(PromedioFinalQ1,0)),2) CalificacionNumerica, 'PENSUM' as Columna, 8 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
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
GROUP BY mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,
CM.NomMateriaGrupo, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto, ptut.pe_nombreCompleto
)

--MATERIAS QUE SE PROMEDIAN--
--P1--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
'OPTATIVAS' MateriaGrupo, cm.OrdenMateria, 99 OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.CalificacionP1 as varchar) Calificacion, mc.CalificacionP1 CalificacionNumerica, 'P1' Columna, 1 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm with (nolock) ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
	LEFT OUTER JOIN
	dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
    dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
	LEFT JOIN	
	(
	select r.IdEmpresa, r.IdMatricula 
	from aca_AlumnoRetiro as r with (nolock) 
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
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)

--P2--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
'OPTATIVAS' MateriaGrupo, cm.OrdenMateria, 99 OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.CalificacionP2 as varchar) Calificacion, mc.CalificacionP2 CalificacionNumerica, 'P2' Columna, 2 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm with (nolock) ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
	LEFT OUTER JOIN
	dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
    dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
	LEFT JOIN	
	(
	select r.IdEmpresa, r.IdMatricula 
	from aca_AlumnoRetiro as r with (nolock) 
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
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)
--P3--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
'OPTATIVAS' MateriaGrupo, cm.OrdenMateria, 99 OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.CalificacionP3 as varchar) Calificacion, mc.CalificacionP3 CalificacionNumerica, 'P3' Columna, 3 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm with (nolock) ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
	LEFT OUTER JOIN
	dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
    dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
	LEFT JOIN	
	(
	select r.IdEmpresa, r.IdMatricula 
	from aca_AlumnoRetiro as r with (nolock) 
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
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)
--PROMEDIO 80%--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
'OPTATIVAS' MateriaGrupo, cm.OrdenMateria, 99 OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(dbo.BankersRounding((mc.PromedioQ1 * 0.80),2) as varchar) Calificacion,
dbo.BankersRounding((mc.PromedioQ1 * 0.80),2) as CalificacionNumerica,
'80%' as Columna, 4 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)
--EXAMEN--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
'OPTATIVAS' MateriaGrupo, cm.OrdenMateria, 99 OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.ExamenQ1 as varchar) Calificacion,
mc.ExamenQ1  as CalificacionNumerica,
'EX' as Columna, 5 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)
--EXAMEN 20%--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
'OPTATIVAS' MateriaGrupo, cm.OrdenMateria, 99 OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(dbo.BankersRounding((mc.ExamenQ1 * 0.20),2) as varchar) Calificacion,
dbo.BankersRounding((mc.ExamenQ1 * 0.20),2) as CalificacionNumerica,
'20%' as Columna, 6 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)
--PROMEDIO OPTATIVAS--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,NULL NomMateria,
'OPTATIVAS' MateriaGrupo, cm.OrdenMateria, 99 OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.PromedioFinalQ1 as varchar) Calificacion,
PromedioFinalQ1 as CalificacionNumerica,
'PF' as Columna, 6 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)
--PROMEDIO QUIMESTRE--
UNION ALL
(
select 
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, 
null IdMateria, null NomMateria, 'OPTATIVAS' MateriaGrupo, 99 OrdenMateria, 99 OrdenMateriaGrupo, 1 PromediarGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede,
a.NomNivel, a.OrdenNivel,a.NomJornada, a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,
a.NombreInspector, a.NombreTutor, a.Calificacion, a.CalificacionNumerica, 'OPTATIVAS' Columna,7 OrdenColumna
from (
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula,
cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(dbo.BankersRounding(AVG(mc.PromedioFinalQ1),2) as varchar) Calificacion,
dbo.BankersRounding(AVG(mc.PromedioFinalQ1),2) CalificacionNumerica
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm with (nolock) ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
	LEFT OUTER JOIN
	dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
    dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
	LEFT JOIN	
	(
	select r.IdEmpresa, r.IdMatricula 
	from aca_AlumnoRetiro as r with (nolock) 
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
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
group by
mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula,
cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto, ptut.pe_nombreCompleto
)a
)


--MATERIAS CUALITATIVAS--
--P1--
UNION ALL
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo,a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
cast(max(a.CalificacionP1) as varchar) Calificacion, max(a.CalificacionNumericaP1) CalificacionNumerica, 'P1' Columna, 1 OrdenColumna
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaGrupo, MC.OrdenMateria, 
	MC.OrdenMateriaGrupo, MC.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
	cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
	CASE WHEN IdCatalogoParcial = 28 THEN ep1.Codigo END AS CalificacionP1, 
	CASE WHEN IdCatalogoParcial = 29 THEN ep2.Codigo END AS CalificacionP2, 
	CASE WHEN IdCatalogoParcial = 30 THEN ep3.Codigo END AS CalificacionP3,
	epQ1.Codigo CalificacionQ1,
	CASE WHEN IdCatalogoParcial = 28 THEN ep1.Calificacion END AS CalificacionNumericaP1, 
	CASE WHEN IdCatalogoParcial = 29 THEN ep2.Calificacion END AS CalificacionNumericaP2, 
	CASE WHEN IdCatalogoParcial = 30 THEN ep3.Calificacion END AS CalificacionNumericaP3,
	epQ1.Calificacion CalificacionNumericaQ1
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b with (nolock) on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep1 with (nolock) ON m.IdAnio = ep1.IdAnio AND a.IdEmpresa = ep1.IdEmpresa AND a.IdCalificacionCualitativa = ep1.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep2 with (nolock) ON m.IdAnio = ep2.IdAnio AND a.IdEmpresa = ep2.IdEmpresa AND a.IdCalificacionCualitativa = ep2.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep3 with (nolock) ON m.IdAnio = ep3.IdAnio AND a.IdEmpresa = ep3.IdEmpresa AND a.IdCalificacionCualitativa = ep3.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ1 with (nolock) ON m.IdAnio = epQ1.IdAnio AND b.IdEmpresa = epQ1.IdEmpresa AND b.IdCalificacionCualitativaQ1 = epQ1.IdCalificacionCualitativa 
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion, 
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)
--P2--
UNION ALL
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo,a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
cast(max(a.CalificacionP2) as varchar) Calificacion, max(a.CalificacionNumericaP2) CalificacionNumerica, 'P2' Columna, 2 OrdenColumna
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaArea, MC.NomMateriaGrupo, MC.EsObligatorio, MC.OrdenMateria, 
	MC.OrdenMateriaGrupo, MC.OrdenMateriaArea, MC.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
	cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
	CASE WHEN IdCatalogoParcial = 28 THEN ep1.Codigo END AS CalificacionP1, 
	CASE WHEN IdCatalogoParcial = 29 THEN ep2.Codigo END AS CalificacionP2, 
	CASE WHEN IdCatalogoParcial = 30 THEN ep3.Codigo END AS CalificacionP3,
	epQ1.Codigo CalificacionQ1,
	CASE WHEN IdCatalogoParcial = 28 THEN ep1.Calificacion END AS CalificacionNumericaP1, 
	CASE WHEN IdCatalogoParcial = 29 THEN ep2.Calificacion END AS CalificacionNumericaP2, 
	CASE WHEN IdCatalogoParcial = 30 THEN ep3.Calificacion END AS CalificacionNumericaP3,
	epQ1.Calificacion CalificacionNumericaQ1
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b with (nolock) on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep1 with (nolock) ON m.IdAnio = ep1.IdAnio AND a.IdEmpresa = ep1.IdEmpresa AND a.IdCalificacionCualitativa = ep1.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep2 with (nolock) ON m.IdAnio = ep2.IdAnio AND a.IdEmpresa = ep2.IdEmpresa AND a.IdCalificacionCualitativa = ep2.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep3 with (nolock) ON m.IdAnio = ep3.IdAnio AND a.IdEmpresa = ep3.IdEmpresa AND a.IdCalificacionCualitativa = ep3.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ1 with (nolock) ON m.IdAnio = epQ1.IdAnio AND b.IdEmpresa = epQ1.IdEmpresa AND b.IdCalificacionCualitativaQ1 = epQ1.IdCalificacionCualitativa 
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion, 
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)

--P3--
UNION ALL
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
cast(max(a.CalificacionP3) as varchar) Calificacion, max(a.CalificacionNumericaP3) CalificacionNumerica, 'P3' Columna, 3 OrdenColumna
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaArea, MC.NomMateriaGrupo, MC.EsObligatorio, MC.OrdenMateria, 
	MC.OrdenMateriaGrupo, MC.OrdenMateriaArea, MC.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
	cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
	CASE WHEN IdCatalogoParcial = 28 THEN ep1.Codigo END AS CalificacionP1, 
	CASE WHEN IdCatalogoParcial = 29 THEN ep2.Codigo END AS CalificacionP2, 
	CASE WHEN IdCatalogoParcial = 30 THEN ep3.Codigo END AS CalificacionP3, 
	epQ1.Codigo CalificacionQ1,
	CASE WHEN IdCatalogoParcial = 28 THEN ep1.Calificacion END AS CalificacionNumericaP1, 
	CASE WHEN IdCatalogoParcial = 29 THEN ep2.Calificacion END AS CalificacionNumericaP2, 
	CASE WHEN IdCatalogoParcial = 30 THEN ep3.Calificacion END AS CalificacionNumericaP3,
	epQ1.Calificacion CalificacionNumericaQ1
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b with (nolock) on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep1 with (nolock) ON m.IdAnio = ep1.IdAnio AND a.IdEmpresa = ep1.IdEmpresa AND a.IdCalificacionCualitativa = ep1.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep2 with (nolock) ON m.IdAnio = ep2.IdAnio AND a.IdEmpresa = ep2.IdEmpresa AND a.IdCalificacionCualitativa = ep2.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep3 with (nolock) ON m.IdAnio = ep3.IdAnio AND a.IdEmpresa = ep3.IdEmpresa AND a.IdCalificacionCualitativa = ep3.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ1 with (nolock) ON m.IdAnio = epQ1.IdAnio AND b.IdEmpresa = epQ1.IdEmpresa AND b.IdCalificacionCualitativaQ1 = epQ1.IdCalificacionCualitativa 
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion, a.EsObligatorio,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)
--PROMEDIO 80%--
UNION ALL
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo,a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
null Calificacion, null CalificacionNumerica, '80%' Columna, 4 OrdenColumna
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, 
MC.NomMateria, MC.NomMateriaGrupo, MC.OrdenMateria,MC.OrdenMateriaGrupo, MC.PromediarGrupo,  MC.IdCatalogoTipoCalificacion, 
alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CASE WHEN IdCatalogoParcial = 28 THEN ep1.Codigo END AS CalificacionP1, 
CASE WHEN IdCatalogoParcial = 29 THEN ep2.Codigo END AS CalificacionP2, 
CASE WHEN IdCatalogoParcial = 30 THEN ep3.Codigo END AS CalificacionP3, 
epQ1.Codigo CalificacionQ1
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b with (nolock) on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep1 with (nolock) ON m.IdAnio = ep1.IdAnio AND a.IdEmpresa = ep1.IdEmpresa AND a.IdCalificacionCualitativa = ep1.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep2 with (nolock) ON m.IdAnio = ep2.IdAnio AND a.IdEmpresa = ep2.IdEmpresa AND a.IdCalificacionCualitativa = ep2.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep3 with (nolock) ON m.IdAnio = ep3.IdAnio AND a.IdEmpresa = ep3.IdEmpresa AND a.IdCalificacionCualitativa = ep3.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ1 with (nolock) ON m.IdAnio = epQ1.IdAnio AND b.IdEmpresa = epQ1.IdEmpresa AND b.IdCalificacionCualitativaQ1 = epQ1.IdCalificacionCualitativa 
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)
--EXAMEN%--
UNION ALL
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo,a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
null Calificacion, null CalificacionNumerica, 'EX' Columna, 5 OrdenColumna
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaGrupo, MC.OrdenMateria, 
	MC.OrdenMateriaGrupo, MC.PromediarGrupo, MC.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
	cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
	CASE WHEN IdCatalogoParcial = 28 THEN ep1.Codigo END AS CalificacionP1, 
	CASE WHEN IdCatalogoParcial = 29 THEN ep2.Codigo END AS CalificacionP2, 
	CASE WHEN IdCatalogoParcial = 30 THEN ep3.Codigo END AS CalificacionP3, 
	epQ1.Codigo CalificacionQ1
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b with (nolock) on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep1 with (nolock) ON m.IdAnio = ep1.IdAnio AND a.IdEmpresa = ep1.IdEmpresa AND a.IdCalificacionCualitativa = ep1.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep2 with (nolock) ON m.IdAnio = ep2.IdAnio AND a.IdEmpresa = ep2.IdEmpresa AND a.IdCalificacionCualitativa = ep2.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep3 with (nolock) ON m.IdAnio = ep3.IdAnio AND a.IdEmpresa = ep3.IdEmpresa AND a.IdCalificacionCualitativa = ep3.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ1 with (nolock) ON m.IdAnio = epQ1.IdAnio AND b.IdEmpresa = epQ1.IdEmpresa AND b.IdCalificacionCualitativaQ1 = epQ1.IdCalificacionCualitativa 
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)
--EXAMEN 20%--
UNION ALL
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo,a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
null Calificacion, null CalificacionNumerica, '20%' Columna, 6 OrdenColumna
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaArea, MC.NomMateriaGrupo, MC.OrdenMateria, 
	MC.OrdenMateriaGrupo, MC.PromediarGrupo, Mc.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
	cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
	CASE WHEN IdCatalogoParcial = 28 THEN ep1.Codigo END AS CalificacionP1, 
	CASE WHEN IdCatalogoParcial = 29 THEN ep2.Codigo END AS CalificacionP2, 
	CASE WHEN IdCatalogoParcial = 30 THEN ep3.Codigo END AS CalificacionP3, 
	epQ1.Codigo CalificacionQ1
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b with (nolock) on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep1 with (nolock) ON m.IdAnio = ep1.IdAnio AND a.IdEmpresa = ep1.IdEmpresa AND a.IdCalificacionCualitativa = ep1.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep2 with (nolock) ON m.IdAnio = ep2.IdAnio AND a.IdEmpresa = ep2.IdEmpresa AND a.IdCalificacionCualitativa = ep2.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep3 with (nolock) ON m.IdAnio = ep3.IdAnio AND a.IdEmpresa = ep3.IdEmpresa AND a.IdCalificacionCualitativa = ep3.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ1 with (nolock) ON m.IdAnio = epQ1.IdAnio AND b.IdEmpresa = epQ1.IdEmpresa AND b.IdCalificacionCualitativaQ1 = epQ1.IdCalificacionCualitativa 
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaArea, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo,  a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)
--PROMEDIO QUIMESTRE--
UNION ALL
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo,a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
cast(max(a.CalificacionQ1) as varchar)  Calificacion, max(a.CalificacionNumericaQ1) CalificacionNumerica, 'PF' Columna, 7 OrdenColumna
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaArea, MC.NomMateriaGrupo, MC.EsObligatorio, MC.OrdenMateria, 
	MC.OrdenMateriaGrupo, mc.PromediarGrupo, mc.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
	cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
	CASE WHEN IdCatalogoParcial = 28 THEN ep1.Codigo END AS CalificacionP1, 
	CASE WHEN IdCatalogoParcial = 29 THEN ep2.Codigo END AS CalificacionP2, 
	CASE WHEN IdCatalogoParcial = 30 THEN ep3.Codigo END AS CalificacionP3, 
	epQ1.Codigo CalificacionQ1,
	CASE WHEN IdCatalogoParcial = 28 THEN ep1.Calificacion END AS CalificacionNumericaP1, 
	CASE WHEN IdCatalogoParcial = 29 THEN ep2.Calificacion END AS CalificacionNumericaP2, 
	CASE WHEN IdCatalogoParcial = 30 THEN ep3.Calificacion END AS CalificacionNumericaP3,
	epQ1.Calificacion CalificacionNumericaQ1
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b with (nolock) on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep1 with (nolock) ON m.IdAnio = ep1.IdAnio AND a.IdEmpresa = ep1.IdEmpresa AND a.IdCalificacionCualitativa = ep1.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep2 with (nolock) ON m.IdAnio = ep2.IdAnio AND a.IdEmpresa = ep2.IdEmpresa AND a.IdCalificacionCualitativa = ep2.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep3 with (nolock) ON m.IdAnio = ep3.IdAnio AND a.IdEmpresa = ep3.IdEmpresa AND a.IdCalificacionCualitativa = ep3.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ1 with (nolock) ON m.IdAnio = epQ1.IdAnio AND b.IdEmpresa = epQ1.IdEmpresa AND b.IdCalificacionCualitativaQ1 = epQ1.IdCalificacionCualitativa 
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaArea, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)
---PROMEDIO MATERIAS CUALITATIVAS--
UNION ALL 
(
SELECT
PROY.IdEmpresa, PROY.IdAnio, PROY.IdSede, PROY.IdNivel, PROY.IdJornada, PROY.IdCurso, PROY.IdParalelo, PROY.IdAlumno, PROY.IdMatricula, PROY.IdMateria, 
PROY.NomMateria, PROY.NomMateriaGrupo,PROY.OrdenMateria, PROY.OrdenMateriaGrupo, PROY.PromediarGrupo, PROY.IdCatalogoTipoCalificacion,
PROY.Codigo, PROY.NombreAlumno, PROY.Descripcion,PROY.NomSede, PROY.NomNivel, PROY.OrdenNivel, PROY.NomJornada, 
PROY.OrdenJornada, PROY.NomCurso, PROY.OrdenCurso, PROY.CodigoParalelo, PROY.NomParalelo, PROY.OrdenParalelo, PROY.NombreInspector , PROY.NombreTutor,
LPROM.Codigo Calificacion, PROY.CalificacionNumerica, 'PROYECTOS' Columna, 7 OrdenColumna
FROM(
SELECT 
PROM.IdEmpresa, PROM.IdAnio, PROM.IdSede, PROM.IdNivel, PROM.IdJornada, PROM.IdCurso, PROM.IdParalelo, PROM.IdAlumno, PROM.IdMatricula, PROM.IdMateria, 
PROM.NomMateria, PROM.NomMateriaGrupo,PROM.OrdenMateria, PROM.OrdenMateriaGrupo, PROM.PromediarGrupo, PROM.IdCatalogoTipoCalificacion,
PROM.Codigo, PROM.NombreAlumno, PROM.Descripcion,PROM.NomSede, PROM.NomNivel, PROM.OrdenNivel, PROM.NomJornada, 
PROM.OrdenJornada, PROM.NomCurso, PROM.OrdenCurso, PROM.CodigoParalelo, PROM.NomParalelo, PROM.OrdenParalelo, PROM.NombreInspector , PROM.NombreTutor,
dbo.BankersRounding(AVG(ISNULL(PROM.CalificacionNumerica,0)),2) CalificacionNumerica
FROM(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo,a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
cast(max(a.CalificacionQUIM) as varchar)  Calificacion, max(a.CalificacionNumericaQUIM) CalificacionNumerica
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaGrupo, MC.OrdenMateria, 
	MC.OrdenMateriaGrupo, mc.PromediarGrupo, mc.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
	cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
	epQ.Codigo CalificacionQUIM,
	epQ.Calificacion CalificacionNumericaQUIM
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ with (nolock) ON m.IdAnio = epQ.IdAnio AND b.IdEmpresa = epQ.IdEmpresa AND b.IdCalificacionCualitativaQ1 = epQ.IdCalificacionCualitativa 
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
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)PROM
group by
PROM.IdEmpresa, PROM.IdAnio, PROM.IdSede, PROM.IdNivel, PROM.IdJornada, PROM.IdCurso, PROM.IdParalelo, PROM.IdAlumno, PROM.IdMatricula, PROM.IdMateria, 
PROM.NomMateria, PROM.NomMateriaGrupo, PROM.OrdenMateria, PROM.OrdenMateriaGrupo, PROM.PromediarGrupo, PROM.IdCatalogoTipoCalificacion,
PROM.Codigo, PROM.NombreAlumno, PROM.Descripcion,PROM.NomSede, PROM.NomNivel, PROM.OrdenNivel, PROM.NomJornada, 
PROM.OrdenJornada, PROM.NomCurso, PROM.OrdenCurso, PROM.CodigoParalelo, PROM.NomParalelo, PROM.OrdenParalelo,PROM.NombreInspector , PROM.NombreTutor
)PROY
LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS LPROM ON PROY.IdAnio = LPROM.IdAnio AND PROY.IdEmpresa = LPROM.IdEmpresa 
AND PROY.CalificacionNumerica = LPROM.Calificacion
)
--PROMEDIO GENERAL--
UNION ALL
(
SELECT m.IdEmpresa, m.IdAnio, m.IdSede,m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo,m.IdAlumno, m.IdMatricula, 
null IdMateria, NULL NombreMateria, NULL NombreGrupo,
99999 OrdenMateria, 99999 OrdenGrupo,0 PromediarGrupo,NULL IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
NULL Calificacion, NULL CalificacionNumerica,'P FINAL' Columna, 1 OrdenColumna
FROM     dbo.aca_Matricula AS m with (nolock) 
inner join dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno 
INNER JOIN dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where m.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
)

--CONDUCTA--
--CONDUCTA P1--
union all
(
SELECT mco.IdEmpresa, m.IdAnio, m.IdSede,m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo,m.IdAlumno, mco.IdMatricula, 
null IdMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreGrupo,
999999 OrdenMateria, 999999 OrdenGrupo,0 PromediarGrupo, NULL IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
cast(equiv.Letra as varchar) as Calificacion, equiv.Calificacion CalificacionNumerica, 'P1' Columna,1 OrdenColumna
FROM     dbo.aca_MatriculaConducta AS mco with (nolock) 
INNER JOIN dbo.aca_Matricula AS m with (nolock) ON mco.IdEmpresa = m.IdEmpresa AND mco.IdMatricula = m.IdMatricula
inner join dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno 
INNER JOIN dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv on equiv.IdEmpresa=mco.IdEmpresa and equiv.IdAnio=m.IdAnio and equiv.Secuencia = mco.SecuenciaPromedioFinalP1
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mco.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
)
--CONDUCTA P2--
union all
(
SELECT mco.IdEmpresa, m.IdAnio, m.IdSede,m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo,m.IdAlumno, mco.IdMatricula, 
null IdMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreGrupo,
999999 OrdenMateria, 999999 OrdenGrupo,0 PromediarGrupo, NULL IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
cast(equiv.Letra as varchar) as Calificacion,equiv.Calificacion CalificacionNumerica,'P2' Columna, 2 OrdenColumna
FROM     dbo.aca_MatriculaConducta AS mco with (nolock) 
INNER JOIN dbo.aca_Matricula AS m with (nolock) ON mco.IdEmpresa = m.IdEmpresa AND mco.IdMatricula = m.IdMatricula
inner join dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno 
INNER JOIN dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv with (nolock) on equiv.IdEmpresa=mco.IdEmpresa and equiv.IdAnio=m.IdAnio and equiv.Secuencia = mco.SecuenciaPromedioFinalP2
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mco.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
)
--CONDUCTA P3--
union all
(
SELECT mco.IdEmpresa, m.IdAnio, m.IdSede,m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo,m.IdAlumno, mco.IdMatricula, 
null IdMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreGrupo,
999999 OrdenMateria, 999999 OrdenGrupo,0 PromediarGrupo, NULL IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
cast(equiv.Letra as varchar) as Calificacion,equiv.Calificacion CalificacionNumerica,'P3' Columna, 3 OrdenColumna
FROM     dbo.aca_MatriculaConducta AS mco with (nolock) 
INNER JOIN dbo.aca_Matricula AS m with (nolock) ON mco.IdEmpresa = m.IdEmpresa AND mco.IdMatricula = m.IdMatricula
inner join dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno 
INNER JOIN dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv with (nolock) on equiv.IdEmpresa=mco.IdEmpresa and equiv.IdAnio=m.IdAnio and equiv.Secuencia = mco.SecuenciaPromedioFinalP3
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mco.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
)
--CONDUCTA PF--
union all
(
SELECT mco.IdEmpresa, m.IdAnio, m.IdSede,m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo,m.IdAlumno, mco.IdMatricula, 
null IdMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreGrupo,
999999 OrdenMateria, 999999 OrdenGrupo,0 PromediarGrupo, NULL IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
cast(equiv.Letra as varchar) as Calificacion,equiv.Calificacion CalificacionNumerica,'PF' Columna, 4 OrdenColumna
FROM     dbo.aca_MatriculaConducta AS mco with (nolock) 
INNER JOIN dbo.aca_Matricula AS m with (nolock) ON mco.IdEmpresa = m.IdEmpresa AND mco.IdMatricula = m.IdMatricula
inner join dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno 
INNER JOIN dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv with (nolock) on equiv.IdEmpresa=mco.IdEmpresa and equiv.IdAnio=m.IdAnio and equiv.Secuencia = mco.SecuenciaPromedioFinalQ1
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mco.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
)
ELSE 
IF @IdCatalogoParcial = 7
--MATERIAS QUE NO SE PROMEDIAN--
--P1--
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo,CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.CalificacionP4 as varchar) Calificacion, mc.CalificacionP4 as CalificacionNumerica, 'P1' as Columna, 1 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
--P2--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.CalificacionP5 as varchar) Calificacion, mc.CalificacionP5 as CalificacionNumerica,'P2' as Columna, 2 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)
--P3--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.CalificacionP6 as varchar) Calificacion,mc.CalificacionP6 as CalificacionNumerica,'P3' as Columna, 3 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)
--PROMEDIO 80%--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(dbo.BankersRounding((mc.PromedioQ2 * 0.80),2) as varchar) Calificacion,
dbo.BankersRounding((mc.PromedioQ2 * 0.80),2) as CalificacionNumerica,
'80%' as Columna, 4 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)
--EXAMEN--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.ExamenQ2 as varchar) Calificacion, mc.ExamenQ2 as CalificacionNumerica, 'EX' as Columna, 5 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)
--EXAMEN 20%--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(dbo.BankersRounding((mc.ExamenQ2 * 0.20),2)as varchar) Calificacion, 
dbo.BankersRounding((mc.ExamenQ2 * 0.20),2) as CalificacionNumerica,'20%' as Columna, 6 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)
--PROMEDIO QUIMESTRE--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, 
CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.PromedioFinalQ2 as varchar) Calificacion, mc.PromedioFinalQ2 AS CalificacionNumerica, 'PF' as Columna, 7 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 0
and cm.IdCatalogoTipoCalificacion=40
)

--PROMEDIO FINAL DE GRUPO DE MATERIAS QUE NO SE PROMEDIAN
UNION
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,
99 IdMateria, NULL NomMateria, CM.NomMateriaGrupo, 99 OrdenMateria, CM.OrdenMateriaGrupo,CM.PromediarGrupo, CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(dbo.BankersRounding(AVG(ISNULL(PromedioFinalQ2,0)),2) as varchar) Calificacion, 
dbo.BankersRounding(AVG(ISNULL(PromedioFinalQ2,0)),2) CalificacionNumerica, 'PENSUM' as Columna, 8 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
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
GROUP BY mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,
CM.NomMateriaGrupo, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto, ptut.pe_nombreCompleto
)
--MATERIAS QUE SE PROMEDIAN--
--P1--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
'OPTATIVAS' MateriaGrupo, cm.OrdenMateria, 99 OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.CalificacionP4 as varchar) Calificacion, mc.CalificacionP4 CalificacionNumerica, 'P1' Columna, 1 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm with (nolock) ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
	LEFT OUTER JOIN
	dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
    dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
	LEFT JOIN	
	(
	select r.IdEmpresa, r.IdMatricula 
	from aca_AlumnoRetiro as r with (nolock) 
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
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)

--P2--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
'OPTATIVAS' MateriaGrupo, cm.OrdenMateria, 99 OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.CalificacionP5 as varchar) Calificacion, mc.CalificacionP5 CalificacionNumerica, 'P2' Columna, 2 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm with (nolock) ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
	LEFT OUTER JOIN
	dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
    dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
	LEFT JOIN	
	(
	select r.IdEmpresa, r.IdMatricula 
	from aca_AlumnoRetiro as r with (nolock) 
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
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)
--P3--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
'OPTATIVAS' MateriaGrupo, cm.OrdenMateria, 99 OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.CalificacionP6 as varchar) Calificacion, mc.CalificacionP6 CalificacionNumerica, 'P3' Columna, 3 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm with (nolock) ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
	LEFT OUTER JOIN
	dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
    dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
	LEFT JOIN	
	(
	select r.IdEmpresa, r.IdMatricula 
	from aca_AlumnoRetiro as r with (nolock) 
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
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)
--PROMEDIO 80%--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
'OPTATIVAS' MateriaGrupo, cm.OrdenMateria, 99 OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(dbo.BankersRounding((mc.PromedioQ2 * 0.80),2) as varchar) Calificacion,
dbo.BankersRounding((mc.PromedioQ2 * 0.80),2) as CalificacionNumerica,
'80%' as Columna, 4 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)
--EXAMEN--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
'OPTATIVAS' MateriaGrupo, cm.OrdenMateria, 99 OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.ExamenQ2 as varchar) Calificacion,
mc.ExamenQ2  as CalificacionNumerica,
'EX' as Columna, 5 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)
--EXAMEN 20%--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,cm.NomMateria,
'OPTATIVAS' MateriaGrupo, cm.OrdenMateria, 99 OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(dbo.BankersRounding((mc.ExamenQ2 * 0.20),2) as varchar) Calificacion,
dbo.BankersRounding((mc.ExamenQ2 * 0.20),2) as CalificacionNumerica,
'20%' as Columna, 6 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)
--PROMEDIO OPTATIVAS--
UNION ALL
(
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria,NULL NomMateria,
'OPTATIVAS' MateriaGrupo, cm.OrdenMateria, 99 OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(mc.PromedioFinalQ2 as varchar) Calificacion,
PromedioFinalQ2 as CalificacionNumerica,
'PF' as Columna, 6 OrdenColumna
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
dbo.aca_AnioLectivo_Curso_Materia CM with (nolock) ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND 
m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND 
mc.IdMateria = CM.IdMateria 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mc.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdJornada = @IdJornada
and m.IdNivel = @IdNivel
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
)
--PROMEDIO QUIMESTRE--
UNION ALL
(
select 
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, 
null IdMateria, null NomMateria, 'OPTATIVAS' MateriaGrupo, 99 OrdenMateria, 99 OrdenMateriaGrupo, 1 PromediarGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede,
a.NomNivel, a.OrdenNivel,a.NomJornada, a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,
a.NombreInspector, a.NombreTutor, a.Calificacion, a.CalificacionNumerica, 'OPTATIVAS' Columna,7 OrdenColumna
from (
SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula,
cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CAST(dbo.BankersRounding(AVG(mc.PromedioFinalQ2),2) as varchar) Calificacion,
dbo.BankersRounding(AVG(mc.PromedioFinalQ2),2) CalificacionNumerica
FROM     dbo.aca_MatriculaCalificacion AS mc with (nolock) INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS cm with (nolock) ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND 
    m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND 
    mc.IdMateria = cm.IdMateria 
LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
	LEFT OUTER JOIN
	dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
    dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
    dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
	LEFT JOIN	
	(
	select r.IdEmpresa, r.IdMatricula 
	from aca_AlumnoRetiro as r with (nolock) 
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
and cm.PromediarGrupo = 1
and cm.IdCatalogoTipoCalificacion=40
group by
mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula,
cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo,cm.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto,
AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto, ptut.pe_nombreCompleto
)a
)

--MATERIAS CUALITATIVAS--
--P1--
UNION ALL
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo,a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
cast(max(a.CalificacionP1) as varchar) Calificacion, max(a.CalificacionNumericaP1) CalificacionNumerica, 'P1' Columna, 1 OrdenColumna
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaGrupo, MC.OrdenMateria, 
	MC.OrdenMateriaGrupo, MC.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
	cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
	CASE WHEN IdCatalogoParcial = 31 THEN ep1.Codigo END AS CalificacionP1, 
	CASE WHEN IdCatalogoParcial = 32 THEN ep2.Codigo END AS CalificacionP2, 
	CASE WHEN IdCatalogoParcial = 33 THEN ep3.Codigo END AS CalificacionP3,
	epQ1.Codigo CalificacionQ2,
	CASE WHEN IdCatalogoParcial = 31 THEN ep1.Calificacion END AS CalificacionNumericaP1, 
	CASE WHEN IdCatalogoParcial = 32 THEN ep2.Calificacion END AS CalificacionNumericaP2, 
	CASE WHEN IdCatalogoParcial = 33 THEN ep3.Calificacion END AS CalificacionNumericaP3,
	epQ1.Calificacion CalificacionNumericaQ2
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b with (nolock) on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep1 with (nolock) ON m.IdAnio = ep1.IdAnio AND a.IdEmpresa = ep1.IdEmpresa AND a.IdCalificacionCualitativa = ep1.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep2 with (nolock) ON m.IdAnio = ep2.IdAnio AND a.IdEmpresa = ep2.IdEmpresa AND a.IdCalificacionCualitativa = ep2.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep3 with (nolock) ON m.IdAnio = ep3.IdAnio AND a.IdEmpresa = ep3.IdEmpresa AND a.IdCalificacionCualitativa = ep3.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ1 with (nolock) ON m.IdAnio = epQ1.IdAnio AND b.IdEmpresa = epQ1.IdEmpresa AND b.IdCalificacionCualitativaQ1 = epQ1.IdCalificacionCualitativa 
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion, 
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)
--P2--
UNION ALL
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo,a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
cast(max(a.CalificacionP2) as varchar) Calificacion, max(a.CalificacionNumericaP2) CalificacionNumerica, 'P2' Columna, 2 OrdenColumna
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaArea, MC.NomMateriaGrupo, MC.EsObligatorio, MC.OrdenMateria, 
	MC.OrdenMateriaGrupo, MC.OrdenMateriaArea, MC.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
	cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
	CASE WHEN IdCatalogoParcial = 31 THEN ep1.Codigo END AS CalificacionP1, 
	CASE WHEN IdCatalogoParcial = 32 THEN ep2.Codigo END AS CalificacionP2, 
	CASE WHEN IdCatalogoParcial = 33 THEN ep3.Codigo END AS CalificacionP3,
	epQ1.Codigo CalificacionQ2,
	CASE WHEN IdCatalogoParcial = 31 THEN ep1.Calificacion END AS CalificacionNumericaP1, 
	CASE WHEN IdCatalogoParcial = 32 THEN ep2.Calificacion END AS CalificacionNumericaP2, 
	CASE WHEN IdCatalogoParcial = 33 THEN ep3.Calificacion END AS CalificacionNumericaP3,
	epQ1.Calificacion CalificacionNumericaQ2
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b with (nolock) on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep1 with (nolock) ON m.IdAnio = ep1.IdAnio AND a.IdEmpresa = ep1.IdEmpresa AND a.IdCalificacionCualitativa = ep1.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep2 with (nolock) ON m.IdAnio = ep2.IdAnio AND a.IdEmpresa = ep2.IdEmpresa AND a.IdCalificacionCualitativa = ep2.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep3 with (nolock) ON m.IdAnio = ep3.IdAnio AND a.IdEmpresa = ep3.IdEmpresa AND a.IdCalificacionCualitativa = ep3.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ1 with (nolock) ON m.IdAnio = epQ1.IdAnio AND b.IdEmpresa = epQ1.IdEmpresa AND b.IdCalificacionCualitativaQ1 = epQ1.IdCalificacionCualitativa 
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion, 
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)

--P3--
UNION ALL
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
cast(max(a.CalificacionP3) as varchar) Calificacion, max(a.CalificacionNumericaP3) CalificacionNumerica, 'P3' Columna, 3 OrdenColumna
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaArea, MC.NomMateriaGrupo, MC.EsObligatorio, MC.OrdenMateria, 
	MC.OrdenMateriaGrupo, MC.OrdenMateriaArea, MC.IdCatalogoTipoCalificacion,alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
	cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
	CASE WHEN IdCatalogoParcial = 31 THEN ep1.Codigo END AS CalificacionP1, 
	CASE WHEN IdCatalogoParcial = 32 THEN ep2.Codigo END AS CalificacionP2, 
	CASE WHEN IdCatalogoParcial = 33 THEN ep3.Codigo END AS CalificacionP3,
	epQ1.Codigo CalificacionQ2,
	CASE WHEN IdCatalogoParcial = 31 THEN ep1.Calificacion END AS CalificacionNumericaP1, 
	CASE WHEN IdCatalogoParcial = 32 THEN ep2.Calificacion END AS CalificacionNumericaP2, 
	CASE WHEN IdCatalogoParcial = 33 THEN ep3.Calificacion END AS CalificacionNumericaP3,
	epQ1.Calificacion CalificacionNumericaQ2
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b with (nolock) on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep1 with (nolock) ON m.IdAnio = ep1.IdAnio AND a.IdEmpresa = ep1.IdEmpresa AND a.IdCalificacionCualitativa = ep1.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep2 with (nolock) ON m.IdAnio = ep2.IdAnio AND a.IdEmpresa = ep2.IdEmpresa AND a.IdCalificacionCualitativa = ep2.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep3 with (nolock) ON m.IdAnio = ep3.IdAnio AND a.IdEmpresa = ep3.IdEmpresa AND a.IdCalificacionCualitativa = ep3.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ1 with (nolock) ON m.IdAnio = epQ1.IdAnio AND b.IdEmpresa = epQ1.IdEmpresa AND b.IdCalificacionCualitativaQ1 = epQ1.IdCalificacionCualitativa 
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion, a.EsObligatorio,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)
--PROMEDIO 80%--
UNION ALL
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo,a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
null Calificacion, null CalificacionNumerica, '80%' Columna, 4 OrdenColumna
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, 
MC.NomMateria, MC.NomMateriaGrupo, MC.OrdenMateria,MC.OrdenMateriaGrupo, MC.PromediarGrupo,  MC.IdCatalogoTipoCalificacion, 
alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
CASE WHEN IdCatalogoParcial = 31 THEN ep1.Codigo END AS CalificacionP1, 
CASE WHEN IdCatalogoParcial = 32 THEN ep2.Codigo END AS CalificacionP2, 
CASE WHEN IdCatalogoParcial = 33 THEN ep3.Codigo END AS CalificacionP3, 
epQ1.Codigo CalificacionQ2
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b with (nolock) on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep1 with (nolock) ON m.IdAnio = ep1.IdAnio AND a.IdEmpresa = ep1.IdEmpresa AND a.IdCalificacionCualitativa = ep1.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep2 with (nolock) ON m.IdAnio = ep2.IdAnio AND a.IdEmpresa = ep2.IdEmpresa AND a.IdCalificacionCualitativa = ep2.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep3 with (nolock) ON m.IdAnio = ep3.IdAnio AND a.IdEmpresa = ep3.IdEmpresa AND a.IdCalificacionCualitativa = ep3.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ1 with (nolock) ON m.IdAnio = epQ1.IdAnio AND b.IdEmpresa = epQ1.IdEmpresa AND b.IdCalificacionCualitativaQ1 = epQ1.IdCalificacionCualitativa 
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)
--EXAMEN%--
UNION ALL
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo,a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
null Calificacion, null CalificacionNumerica, 'EX' Columna, 5 OrdenColumna
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaGrupo, MC.OrdenMateria, 
	MC.OrdenMateriaGrupo, MC.PromediarGrupo, MC.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
	cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
	CASE WHEN IdCatalogoParcial = 31 THEN ep1.Codigo END AS CalificacionP1, 
	CASE WHEN IdCatalogoParcial = 2 THEN ep2.Codigo END AS CalificacionP2, 
	CASE WHEN IdCatalogoParcial = 33 THEN ep3.Codigo END AS CalificacionP3, 
	epQ1.Codigo CalificacionQ1
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b with (nolock) on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep1 with (nolock) ON m.IdAnio = ep1.IdAnio AND a.IdEmpresa = ep1.IdEmpresa AND a.IdCalificacionCualitativa = ep1.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep2 with (nolock) ON m.IdAnio = ep2.IdAnio AND a.IdEmpresa = ep2.IdEmpresa AND a.IdCalificacionCualitativa = ep2.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep3 with (nolock) ON m.IdAnio = ep3.IdAnio AND a.IdEmpresa = ep3.IdEmpresa AND a.IdCalificacionCualitativa = ep3.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ1 with (nolock) ON m.IdAnio = epQ1.IdAnio AND b.IdEmpresa = epQ1.IdEmpresa AND b.IdCalificacionCualitativaQ1 = epQ1.IdCalificacionCualitativa 
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)
--EXAMEN 20%--
UNION ALL
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo,a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
null Calificacion, null CalificacionNumerica, '20%' Columna, 6 OrdenColumna
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaArea, MC.NomMateriaGrupo, MC.OrdenMateria, 
	MC.OrdenMateriaGrupo, MC.PromediarGrupo, Mc.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
	cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
	CASE WHEN IdCatalogoParcial = 31 THEN ep1.Codigo END AS CalificacionP1, 
	CASE WHEN IdCatalogoParcial = 32 THEN ep2.Codigo END AS CalificacionP2, 
	CASE WHEN IdCatalogoParcial = 33 THEN ep3.Codigo END AS CalificacionP3, 
	epQ1.Codigo CalificacionQ1
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b with (nolock) on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep1 with (nolock) ON m.IdAnio = ep1.IdAnio AND a.IdEmpresa = ep1.IdEmpresa AND a.IdCalificacionCualitativa = ep1.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep2 with (nolock) ON m.IdAnio = ep2.IdAnio AND a.IdEmpresa = ep2.IdEmpresa AND a.IdCalificacionCualitativa = ep2.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep3 with (nolock) ON m.IdAnio = ep3.IdAnio AND a.IdEmpresa = ep3.IdEmpresa AND a.IdCalificacionCualitativa = ep3.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ1 with (nolock) ON m.IdAnio = epQ1.IdAnio AND b.IdEmpresa = epQ1.IdEmpresa AND b.IdCalificacionCualitativaQ1 = epQ1.IdCalificacionCualitativa 
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaArea, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo,  a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)
--PROMEDIO QUIMESTRE--
UNION ALL
(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo,a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
cast(max(a.CalificacionQ2) as varchar)  Calificacion, max(a.CalificacionNumericaQ2) CalificacionNumerica, 'PF' Columna, 7 OrdenColumna
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaArea, MC.NomMateriaGrupo, MC.EsObligatorio, MC.OrdenMateria, 
	MC.OrdenMateriaGrupo, mc.PromediarGrupo, mc.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
	cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
	CASE WHEN IdCatalogoParcial = 31 THEN ep1.Codigo END AS CalificacionP1, 
	CASE WHEN IdCatalogoParcial = 32 THEN ep2.Codigo END AS CalificacionP2, 
	CASE WHEN IdCatalogoParcial = 33 THEN ep3.Codigo END AS CalificacionP3, 
	epQ2.Codigo CalificacionQ2,
	CASE WHEN IdCatalogoParcial = 31 THEN ep1.Calificacion END AS CalificacionNumericaP1, 
	CASE WHEN IdCatalogoParcial = 32 THEN ep2.Calificacion END AS CalificacionNumericaP2, 
	CASE WHEN IdCatalogoParcial = 33 THEN ep3.Calificacion END AS CalificacionNumericaP3,
	epQ2.Calificacion CalificacionNumericaQ2
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b with (nolock) on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep1 with (nolock) ON m.IdAnio = ep1.IdAnio AND a.IdEmpresa = ep1.IdEmpresa AND a.IdCalificacionCualitativa = ep1.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep2 with (nolock) ON m.IdAnio = ep2.IdAnio AND a.IdEmpresa = ep2.IdEmpresa AND a.IdCalificacionCualitativa = ep2.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS ep3 with (nolock) ON m.IdAnio = ep3.IdAnio AND a.IdEmpresa = ep3.IdEmpresa AND a.IdCalificacionCualitativa = ep3.IdCalificacionCualitativa 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ2 with (nolock) ON m.IdAnio = epQ2.IdAnio AND b.IdEmpresa = epQ2.IdEmpresa AND b.IdCalificacionCualitativaQ2 = epQ2.IdCalificacionCualitativa 
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaArea, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)
---PROMEDIO MATERIAS CUALITATIVAS--
UNION ALL 
(
SELECT
PROY.IdEmpresa, PROY.IdAnio, PROY.IdSede, PROY.IdNivel, PROY.IdJornada, PROY.IdCurso, PROY.IdParalelo, PROY.IdAlumno, PROY.IdMatricula, PROY.IdMateria, 
PROY.NomMateria, PROY.NomMateriaGrupo,PROY.OrdenMateria, PROY.OrdenMateriaGrupo, PROY.PromediarGrupo, PROY.IdCatalogoTipoCalificacion,
PROY.Codigo, PROY.NombreAlumno, PROY.Descripcion,PROY.NomSede, PROY.NomNivel, PROY.OrdenNivel, PROY.NomJornada, 
PROY.OrdenJornada, PROY.NomCurso, PROY.OrdenCurso, PROY.CodigoParalelo, PROY.NomParalelo, PROY.OrdenParalelo, PROY.NombreInspector , PROY.NombreTutor,
LPROM.Codigo Calificacion, PROY.CalificacionNumerica, 'PROYECTOS' Columna, 7 OrdenColumna
FROM(
SELECT 
PROM.IdEmpresa, PROM.IdAnio, PROM.IdSede, PROM.IdNivel, PROM.IdJornada, PROM.IdCurso, PROM.IdParalelo, PROM.IdAlumno, PROM.IdMatricula, PROM.IdMateria, 
PROM.NomMateria, PROM.NomMateriaGrupo,PROM.OrdenMateria, PROM.OrdenMateriaGrupo, PROM.PromediarGrupo, PROM.IdCatalogoTipoCalificacion,
PROM.Codigo, PROM.NombreAlumno, PROM.Descripcion,PROM.NomSede, PROM.NomNivel, PROM.OrdenNivel, PROM.NomJornada, 
PROM.OrdenJornada, PROM.NomCurso, PROM.OrdenCurso, PROM.CodigoParalelo, PROM.NomParalelo, PROM.OrdenParalelo, PROM.NombreInspector , PROM.NombreTutor,
dbo.BankersRounding(AVG(ISNULL(PROM.CalificacionNumerica,0)),2) CalificacionNumerica
FROM(
select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, cast('PROYECTOS ESCOLARES' as varchar(500)) NomMateriaGrupo, 999 OrdenMateria, 999 OrdenMateriaGrupo, 0 PromediarGrupo,a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.NombreInspector , a.NombreTutor,
cast(max(a.CalificacionQUIM) as varchar)  Calificacion, max(a.CalificacionNumericaQUIM) CalificacionNumerica
from(
SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaGrupo, MC.OrdenMateria, 
	MC.OrdenMateriaGrupo, mc.PromediarGrupo, mc.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, 
	cp.NomParalelo, cp.OrdenParalelo, pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
	epQ.Codigo CalificacionQUIM,
	epQ.Calificacion CalificacionNumericaQUIM
FROM     dbo.aca_MatriculaCalificacionCualitativa AS a with (nolock) 
INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b with (nolock) on a.IdEmpresa=b.IdEmpresa and a.IdMatricula=b.IdMatricula and a.IdMateria=b.IdMateria
INNER JOIN
    dbo.aca_Matricula AS m with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN
    dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
    dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
	LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ with (nolock) ON m.IdAnio = epQ.IdAnio AND b.IdEmpresa = epQ.IdEmpresa AND b.IdCalificacionCualitativaQ2 = epQ.IdCalificacionCualitativa 
	LEFT OUTER JOIN
    dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) RIGHT OUTER JOIN
    dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) INNER JOIN
    dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND 
    jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN
    dbo.aca_AnioLectivo_Curso_Materia AS MC with (nolock) ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON 
    cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND 
    m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND
    m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
LEFT OUTER JOIN
				  dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
                  dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
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
) a
group by
a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, 
a.NomMateria, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion,
a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, 
a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo,a.NombreInspector , a.NombreTutor
)PROM
group by
PROM.IdEmpresa, PROM.IdAnio, PROM.IdSede, PROM.IdNivel, PROM.IdJornada, PROM.IdCurso, PROM.IdParalelo, PROM.IdAlumno, PROM.IdMatricula, PROM.IdMateria, 
PROM.NomMateria, PROM.NomMateriaGrupo, PROM.OrdenMateria, PROM.OrdenMateriaGrupo, PROM.PromediarGrupo, PROM.IdCatalogoTipoCalificacion,
PROM.Codigo, PROM.NombreAlumno, PROM.Descripcion,PROM.NomSede, PROM.NomNivel, PROM.OrdenNivel, PROM.NomJornada, 
PROM.OrdenJornada, PROM.NomCurso, PROM.OrdenCurso, PROM.CodigoParalelo, PROM.NomParalelo, PROM.OrdenParalelo,PROM.NombreInspector , PROM.NombreTutor
)PROY
LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS LPROM with (nolock) ON PROY.IdAnio = LPROM.IdAnio AND PROY.IdEmpresa = LPROM.IdEmpresa 
AND PROY.CalificacionNumerica = LPROM.Calificacion
)
--PROMEDIO GENERAL--
UNION ALL
(
SELECT m.IdEmpresa, m.IdAnio, m.IdSede,m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo,m.IdAlumno, m.IdMatricula, 
null IdMateria, NULL NombreMateria, NULL NombreGrupo,
99999 OrdenMateria, 99999 OrdenGrupo,0 PromediarGrupo,NULL IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
NULL Calificacion, NULL CalificacionNumerica,'P FINAL' Columna, 1 OrdenColumna
FROM     dbo.aca_Matricula AS m with (nolock) 
inner join dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno 
INNER JOIN dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where m.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
)

--CONDUCTA--
--CONDUCTA P1--
union all
(
SELECT mco.IdEmpresa, m.IdAnio, m.IdSede,m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo,m.IdAlumno, mco.IdMatricula, 
null IdMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreGrupo,
999999 OrdenMateria, 999999 OrdenGrupo,0 PromediarGrupo, NULL IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
cast(equiv.Letra as varchar) as Calificacion, equiv.Calificacion CalificacionNumerica, 'P1' Columna,1 OrdenColumna
FROM     dbo.aca_MatriculaConducta AS mco with (nolock) 
INNER JOIN dbo.aca_Matricula AS m with (nolock) ON mco.IdEmpresa = m.IdEmpresa AND mco.IdMatricula = m.IdMatricula
inner join dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno 
INNER JOIN dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv with (nolock) on equiv.IdEmpresa=mco.IdEmpresa and equiv.IdAnio=m.IdAnio and equiv.Secuencia = mco.SecuenciaPromedioFinalP4
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mco.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
)
--CONDUCTA P2--
union all
(
SELECT mco.IdEmpresa, m.IdAnio, m.IdSede,m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo,m.IdAlumno, mco.IdMatricula, 
null IdMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreGrupo,
999999 OrdenMateria, 999999 OrdenGrupo,0 PromediarGrupo, NULL IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
cast(equiv.Letra as varchar) as Calificacion,equiv.Calificacion CalificacionNumerica,'P2' Columna, 2 OrdenColumna
FROM     dbo.aca_MatriculaConducta AS mco with (nolock) 
INNER JOIN dbo.aca_Matricula AS m with (nolock) ON mco.IdEmpresa = m.IdEmpresa AND mco.IdMatricula = m.IdMatricula
inner join dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno 
INNER JOIN dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv with (nolock) on equiv.IdEmpresa=mco.IdEmpresa and equiv.IdAnio=m.IdAnio and equiv.Secuencia = mco.SecuenciaPromedioFinalP5
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock)  ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
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
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
)
--CONDUCTA P3--
union all
(
SELECT mco.IdEmpresa, m.IdAnio, m.IdSede,m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo,m.IdAlumno, mco.IdMatricula, 
null IdMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreGrupo,
999999 OrdenMateria, 999999 OrdenGrupo,0 PromediarGrupo, NULL IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
cast(equiv.Letra as varchar) as Calificacion,equiv.Calificacion CalificacionNumerica,'P3' Columna, 3 OrdenColumna
FROM     dbo.aca_MatriculaConducta AS mco with (nolock) 
INNER JOIN dbo.aca_Matricula AS m with (nolock) ON mco.IdEmpresa = m.IdEmpresa AND mco.IdMatricula = m.IdMatricula
inner join dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno 
INNER JOIN dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv with (nolock) on equiv.IdEmpresa=mco.IdEmpresa and equiv.IdAnio=m.IdAnio and equiv.Secuencia = mco.SecuenciaPromedioFinalP6
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS r with (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mco.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
)
--CONDUCTA PF--
union all
(
SELECT mco.IdEmpresa, m.IdAnio, m.IdSede,m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo,m.IdAlumno, mco.IdMatricula, 
null IdMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreMateria, cast('COMPORTAMIENTO' as varchar(500)) NombreGrupo,
999999 OrdenMateria, 999999 OrdenGrupo,0 PromediarGrupo, NULL IdCatalogoTipoCalificacion,
alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, 
pins.pe_nombreCompleto AS NombreInspector, ptut.pe_nombreCompleto NombreTutor,
cast(equiv.Letra as varchar) as Calificacion,equiv.Calificacion CalificacionNumerica,'PF' Columna, 4 OrdenColumna
FROM     dbo.aca_MatriculaConducta AS mco with (nolock) 
INNER JOIN dbo.aca_Matricula AS m with (nolock) ON mco.IdEmpresa = m.IdEmpresa AND mco.IdMatricula = m.IdMatricula
inner join dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno 
INNER JOIN dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona 
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv with (nolock) on equiv.IdEmpresa=mco.IdEmpresa and equiv.IdAnio=m.IdAnio and equiv.Secuencia = mco.SecuenciaPromedioFinalQ2
LEFT OUTER JOIN
dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
LEFT OUTER JOIN
dbo.aca_Profesor AS pro with (nolock) ON cp.IdEmpresa = pro.IdEmpresa AND cp.IdProfesorInspector = pro.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS pins with (nolock) ON pins.IdPersona = pro.IdPersona LEFT OUTER JOIN
dbo.aca_Profesor AS pt with (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND cp.IdProfesorTutor = pt.IdProfesor LEFT OUTER JOIN
dbo.tb_persona AS ptut with (nolock) ON ptut.IdPersona = pt.IdPersona 
LEFT OUTER JOIN
    (SELECT IdEmpresa, IdMatricula
    FROM      dbo.aca_AlumnoRetiro AS rwith (nolock) 
    WHERE   (Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula
where mco.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = @IdSede
and m.IdNivel = @IdNivel
and m.IdJornada = @IdJornada
and m.IdCurso = @IdCurso
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
)