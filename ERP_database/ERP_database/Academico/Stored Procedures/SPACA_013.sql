
CREATE PROCEDURE [Academico].[SPACA_013]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdParcial int,
@IdAlumno decimal,
@MostrarRetirados bit
)
AS
SELECT mp.IdEmpresa, mp.IdMatricula, mp.IdMateria, mp.IdCatalogoParcial, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, 
                  jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, m.IdAlumno, p.pe_nombreCompleto AS NombreAlumno, a.Codigo AS CodigoAlumno, p.pe_cedulaRuc, cm.NomMateria, cm.NomMateriaArea, cm.NomMateriaGrupo, 
                  cm.EsObligatorio, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.OrdenMateriaArea,ct.NomCatalogoTipo, c.NomCatalogo, AN.Descripcion, mp.Calificacion1, mp.Calificacion2, mp.Calificacion3, mp.Calificacion4, mp.Remedial1, mp.Remedial2, mp.Evaluacion, 
                  equiv.Letra, equiv.Calificacion, mp.MotivoCalificacion, mp.AccionRemedial, 
                  CASE WHEN mp.IdCatalogoParcial = 28 THEN prom.CalificacionP1 WHEN mp.IdCatalogoParcial = 29 THEN prom.CalificacionP2 WHEN mp.IdCatalogoParcial = 30 THEN prom.CalificacionP3 WHEN mp.IdCatalogoParcial = 31 THEN prom.CalificacionP4
                   WHEN mp.IdCatalogoParcial = 32 THEN prom.CalificacionP5 WHEN mp.IdCatalogoParcial = 33 THEN prom.CalificacionP6 END AS PromedioParcial,EP.Codigo CodigoEquivalenciaPromedio,
EquivM.Secuencia as SecuenciaPromedioConducta, EquivM.Letra as LetraPromedioConducta, cp.IdProfesorTutor,pp.pe_nombreCompleto as NombreTutor, pre.pe_nombreCompleto as NombreRepresentante
FROM     dbo.aca_MatriculaConducta AS mc RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia AS cm INNER JOIN
                  dbo.tb_persona AS p INNER JOIN
                  dbo.aca_Alumno AS a ON p.IdPersona = a.IdPersona INNER JOIN
                  dbo.aca_Matricula AS m ON a.IdEmpresa = m.IdEmpresa AND a.IdAlumno = m.IdAlumno INNER JOIN
                  dbo.aca_MatriculaCalificacionParcial AS mp ON m.IdEmpresa = mp.IdEmpresa AND m.IdMatricula = mp.IdMatricula ON cm.IdEmpresa = m.IdEmpresa AND cm.IdAnio = m.IdAnio AND cm.IdSede = m.IdSede AND cm.IdNivel = m.IdNivel AND 
                  cm.IdJornada = m.IdJornada AND cm.IdCurso = m.IdCurso AND cm.IdMateria = mp.IdMateria ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula LEFT OUTER JOIN
                  dbo.aca_MatriculaCalificacion AS prom ON mp.IdEmpresa = prom.IdEmpresa AND mp.IdMatricula = prom.IdMatricula AND mp.IdMateria = prom.IdMateria LEFT OUTER JOIN
                  dbo.aca_AnioLectivoConductaEquivalencia AS equiv ON m.IdEmpresa = equiv.IdEmpresa AND m.IdAnio = equiv.IdAnio AND mp.Conducta = equiv.Secuencia LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
                  dbo.aca_AnioLectivoEquivalenciaPromedio AS EP ON m.IdEmpresa = EP.IdEmpresa AND m.IdAnio = EP.IdAnio AND 
                  EP.IdEquivalenciaPromedio = CASE WHEN mp.IdCatalogoParcial = 28 THEN prom.IdEquivalenciaPromedioP1 ELSE CASE WHEN mp.IdCatalogoParcial = 29 THEN prom.IdEquivalenciaPromedioP2 ELSE CASE WHEN mp.IdCatalogoParcial = 30
                   THEN prom.IdEquivalenciaPromedioP3 ELSE CASE WHEN mp.IdCatalogoParcial = 31 THEN prom.IdEquivalenciaPromedioP4 ELSE CASE WHEN mp.IdCatalogoParcial = 32 THEN prom.IdEquivalenciaPromedioP5 ELSE CASE WHEN mp.IdCatalogoParcial
                   = 33 THEN prom.IdEquivalenciaPromedioP6 ELSE NULL END END END END END END LEFT OUTER JOIN
                  dbo.aca_Catalogo AS c ON mp.IdCatalogoParcial = c.IdCatalogo LEFT OUTER JOIN
                  dbo.aca_CatalogoTipo AS ct ON c.IdCatalogoTipo = ct.IdCatalogoTipo LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
                  m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo
				  LEFT OUTER JOIN aca_Profesor AS pro ON cp.IdEmpresa = pro.IdEmpresa and cp.IdProfesorTutor = pro.IdProfesor
				  LEFT OUTER JOIN tb_persona as pp on pp.IdPersona = pro.IdPersona
				  LEFT OUTER JOIN tb_persona as pre on pre.IdPersona = m.IdPersonaR
				  LEFT JOIN
				  dbo.aca_AnioLectivoConductaEquivalencia AS EquivM ON m.IdEmpresa = EquivM.IdEmpresa AND m.IdAnio = EquivM.IdAnio 
				  AND EquivM.Secuencia = 
				  CASE 
					WHEN mp.IdCatalogoParcial = 28 THEN mc.SecuenciaPromedioFinalP1 
						else
						case WHEN mp.IdCatalogoParcial = 29 THEN mc.SecuenciaPromedioFinalP2 else
							case WHEN mp.IdCatalogoParcial = 30 THEN mc.SecuenciaPromedioFinalP3 else
								case WHEN mp.IdCatalogoParcial = 31 THEN mc.SecuenciaPromedioFinalP4 else
									case WHEN mp.IdCatalogoParcial = 32 THEN mc.SecuenciaPromedioFinalP5 else
										case WHEN mp.IdCatalogoParcial = 33 THEN mc.SecuenciaPromedioFinalP6 else null
										end
									end
								end
							end
						end
				  END
				  LEFT JOIN	
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r
					where r.Estado = 1
					) as ret on m.IdEmpresa =ret.IdEmpresa and m.IdMatricula = ret.IdMatricula
where mp.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and mp.IdCatalogoParcial = @IdParcial
and m.IdSede = case when @IdSede = 0 then m.IdSede else @IdSede end
and m.IdNivel = case when @IdNivel = 0 then m.IdNivel else @IdNivel end
and m.IdJornada = case when @IdJornada = 0 then m.IdJornada else @IdJornada end
and m.IdCurso = case when @IdCurso = 0 then m.IdCurso else @IdCurso end
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end