CREATE PROCEDURE [Academico].[SPACA_013]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdParcial int
)
AS
SELECT mp.IdEmpresa, mp.IdMatricula, mp.IdMateria, mp.IdCatalogoParcial, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, 
                  jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, m.IdAlumno, p.pe_nombreCompleto AS NombreAlumno, a.Codigo AS CodigoAlumno, p.pe_cedulaRuc, cm.NomMateria, cm.NomMateriaArea, cm.NomMateriaGrupo, 
                  cm.EsObligatorio, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.OrdenMateriaArea,c.NomCatalogo, AN.Descripcion, mp.Calificacion1, mp.Calificacion2, mp.Calificacion3, mp.Calificacion4, 
				  mp.Remedial1, mp.Remedial2,
                  mp.Evaluacion, equiv.Letra, equiv.Calificacion, mp.MotivoCalificacion,mp.AccionRemedial,
				  case when mp.IdCatalogoParcial = 28 then 
				  prom.CalificacionP1 
				  when mp.IdCatalogoParcial = 29 then 
				  prom.CalificacionP2 
				  when mp.IdCatalogoParcial = 30 then  
				  prom.CalificacionP3 
				  when mp.IdCatalogoParcial = 31 then  
				  prom.CalificacionP4
				  when mp.IdCatalogoParcial = 32 then 
				  prom.CalificacionP5
				  when mp.IdCatalogoParcial = 33 then 
				  prom.CalificacionP6 END PromedioParcial
FROM     aca_MatriculaCalificacion AS prom RIGHT OUTER JOIN
                  aca_AnioLectivo_Curso_Materia AS cm INNER JOIN
                  tb_persona AS p INNER JOIN
                  aca_Alumno AS a ON p.IdPersona = a.IdPersona INNER JOIN
                  aca_Matricula AS m ON a.IdEmpresa = m.IdEmpresa AND a.IdAlumno = m.IdAlumno INNER JOIN
                  aca_MatriculaCalificacionParcial AS mp ON m.IdEmpresa = mp.IdEmpresa AND m.IdMatricula = mp.IdMatricula ON cm.IdEmpresa = m.IdEmpresa AND cm.IdAnio = m.IdAnio AND cm.IdSede = m.IdSede AND cm.IdNivel = m.IdNivel AND 
                  cm.IdJornada = m.IdJornada AND cm.IdCurso = m.IdCurso AND cm.IdMateria = mp.IdMateria ON prom.IdEmpresa = mp.IdEmpresa AND prom.IdMatricula = mp.IdMatricula AND prom.IdMateria = mp.IdMateria LEFT OUTER JOIN
                  aca_AnioLectivoConductaEquivalencia AS equiv ON m.IdEmpresa = equiv.IdEmpresa AND m.IdAnio = equiv.IdAnio AND mp.Conducta = equiv.Secuencia LEFT OUTER JOIN
                  aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
                  aca_Catalogo AS c ON mp.IdCatalogoParcial = c.IdCatalogo LEFT OUTER JOIN
                  aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
                  m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
where mp.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and mp.IdCatalogoParcial = @IdParcial
and m.IdSede = case when @IdSede = 0 then m.IdSede else @IdSede end
and m.IdNivel = case when @IdNivel = 0 then m.IdNivel else @IdNivel end
and m.IdJornada = case when @IdJornada = 0 then m.IdJornada else @IdJornada end
and m.IdCurso = case when @IdCurso = 0 then m.IdCurso else @IdCurso end
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and not exists(
	select f.IdEmpresa from aca_AlumnoRetiro as f
	where f.IdEmpresa = mp.IdEmpresa
	and f.IdMatricula = mp.IdMatricula
	and f.Estado = 1
)