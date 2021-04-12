CREATE PROCEDURE [Academico].[SPACA_024]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdCatalogoParcial int,
@IdAlumno decimal
)
AS
SELECT mp.IdEmpresa, mp.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, a.Codigo, p.pe_nombreCompleto, al.Descripcion,sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, 
                  jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, mp.IdCatalogoParcial, cat.NomCatalogo, mp.IdMateria, cm.NomMateria, cm.OrdenMateriaArea, cm.OrdenMateriaGrupo, cm.OrdenMateria,
				  mp.Calificacion1, case when (ISNULL(mp.Calificacion1,0)) <= al.PromedioMinimoParcial then CAST(mp.Calificacion1 as varchar) else 'OK' end C1,
				  mp.Calificacion2, case when (ISNULL(mp.Calificacion2,0)) <= al.PromedioMinimoParcial then CAST(mp.Calificacion2 as varchar) else 'OK' end C2,
				  mp.Calificacion3, case when (ISNULL(mp.Calificacion3,0)) <= al.PromedioMinimoParcial then CAST(mp.Calificacion3 as varchar) else 'OK' end C3,
                  mp.Calificacion4, case when (ISNULL(mp.Calificacion4,0)) <= al.PromedioMinimoParcial then CAST(mp.Calificacion4 as varchar) else 'OK' end C4,
				  mp.Evaluacion, case when (ISNULL(mp.Evaluacion,0)) <= al.PromedioMinimoParcial then CAST(mp.Evaluacion as varchar) else 'OK' end Ev
FROM     dbo.aca_MatriculaCalificacionParcial AS mp with (nolock) INNER JOIN
                  dbo.aca_Matricula AS m with (nolock) ON mp.IdEmpresa = m.IdEmpresa AND mp.IdMatricula = m.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS a with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN
                  dbo.tb_persona AS p with (nolock) ON a.IdPersona = p.IdPersona
				  inner join aca_AnioLectivo al with (nolock) on al.IdEmpresa=m.IdEmpresa and al.IdAnio = m.IdAnio
				  left outer join aca_Catalogo cat with (nolock) on cat.IdCatalogo = mp.IdCatalogoParcial
LEFT OUTER JOIN
                  aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
                  aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
                  m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
				  RIGHT OUTER JOIN
                  aca_AnioLectivo_Curso_Materia AS cm with (nolock) ON cm.IdEmpresa = m.IdEmpresa AND cm.IdAnio = m.IdAnio AND cm.IdSede = m.IdSede AND cm.IdNivel = m.IdNivel AND 
                  cm.IdJornada = m.IdJornada AND cm.IdCurso = m.IdCurso AND cm.IdMateria = mp.IdMateria
where mp.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and mp.IdCatalogoParcial = @IdCatalogoParcial
and m.IdSede = case when @IdSede = 0 then m.IdSede else @IdSede end
and m.IdNivel = case when @IdNivel = 0 then m.IdNivel else @IdNivel end
and m.IdJornada = case when @IdJornada = 0 then m.IdJornada else @IdJornada end
and m.IdCurso = case when @IdCurso = 0 then m.IdCurso else @IdCurso end
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and not exists(
	select f.IdEmpresa from aca_AlumnoRetiro as f with (nolock) 
	where f.IdEmpresa = mp.IdEmpresa
	and f.IdMatricula = mp.IdMatricula
	and f.Estado = 1
)