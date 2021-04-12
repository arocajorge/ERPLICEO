
CREATE PROCEDURE [Academico].[SPACA_026]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdMateria int,
@IdCatalogoParcial int,
@MostrarRetirados bit
)
AS
SELECT mcc.IdEmpresa, mcc.IdMatricula, mcc.IdMateria, alu.Codigo, p.pe_nombreCompleto AS NombreAlumno, 
case when ret.IdMatricula is not null then cast(1 as bit) else cast(0 as bit) end as EsRetirado,
				  case when ret.IdMatricula is not null THEN 'RETIRADO' else 'ACTIVO' end as EsRetiradoString, 
				  m.IdSede, m.IdAnio, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, 
                  nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, MC.NomMateria, MC.NomMateriaArea, MC.NomMateriaGrupo, MC.EsObligatorio, MC.OrdenMateria, MC.OrdenMateriaGrupo, 
                  MC.OrdenMateriaArea, mcc.IdCatalogoParcial, C.NomCatalogo, mcc.IdCalificacionCualitativa, acc.Codigo AS CodigoCalificacionCualitativa, acc.DescripcionCorta, mcc.IdProfesor, pp.pe_nombreCompleto AS NombreProfesor, 
                  dbo.aca_AnioLectivoConductaEquivalencia.Letra, mcc.Conducta
FROM     dbo.aca_AnioLectivoCalificacionCualitativa AS acc with (nolock) RIGHT OUTER JOIN
                  dbo.aca_MatriculaCalificacionCualitativa AS mcc with (nolock) INNER JOIN
                  dbo.aca_Matricula AS m with (nolock) ON mcc.IdEmpresa = m.IdEmpresa AND mcc.IdMatricula = m.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS alu with (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN
                  dbo.tb_persona AS p with (nolock) ON alu.IdPersona = p.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia AS MC ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND 
                  mcc.IdMateria = MC.IdMateria LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS AN with (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN
                  dbo.tb_persona AS pp RIGHT OUTER JOIN
                  dbo.aca_Profesor with (nolock) ON pp.IdPersona = dbo.aca_Profesor.IdPersona ON mcc.IdEmpresa = dbo.aca_Profesor.IdEmpresa AND mcc.IdProfesor = dbo.aca_Profesor.IdProfesor LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND 
                  m.IdParalelo = cp.IdParalelo ON acc.IdEmpresa = mcc.IdEmpresa AND acc.IdCalificacionCualitativa = mcc.IdCalificacionCualitativa LEFT OUTER JOIN
                  dbo.aca_Catalogo AS C ON mcc.IdCatalogoParcial = C.IdCatalogo LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada ON cp.IdEmpresa = jc.IdEmpresa AND 
                  cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada AND cp.IdCurso = jc.IdCurso LEFT OUTER JOIN
                  dbo.aca_AnioLectivoConductaEquivalencia with (nolock) ON mcc.IdEmpresa = dbo.aca_AnioLectivoConductaEquivalencia.IdEmpresa AND mcc.Conducta = dbo.aca_AnioLectivoConductaEquivalencia.Secuencia AND 
                  m.IdAnio = dbo.aca_AnioLectivoConductaEquivalencia.IdAnio
				  left join
					(
					select r.IdEmpresa, r.IdMatricula 
					from aca_AlumnoRetiro as r with (nolock) 
					where r.Estado = 1
					) as ret on m.IdEmpresa =ret.IdEmpresa and m.IdMatricula = ret.IdMatricula
where m.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and mcc.IdCatalogoParcial = @IdCatalogoParcial
and mcc.IdMateria = @IdMateria
and m.IdSede = case when @IdSede = 0 then m.IdSede else @IdSede end
and m.IdNivel = case when @IdNivel = 0 then m.IdNivel else @IdNivel end
and m.IdJornada = case when @IdJornada = 0 then m.IdJornada else @IdJornada end
and m.IdCurso = case when @IdCurso = 0 then m.IdCurso else @IdCurso end
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and alu.Estado = 1