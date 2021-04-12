CREATE PROCEDURE [Academico].[SPACA_015]
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

SELECT m.IdEmpresa, m.IdMatricula, m.IdAlumno, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdPlantilla, dbo.aca_Plantilla.NomPlantilla, al.Codigo AS CodigoAlumno, pa.pe_nombreCompleto AS NombreAlumno, 
                  pa.pe_cedulaRuc AS IdentificacionAlumno, pa.pe_fechaNacimiento AS FechaNacAlumno, sn.NomSede, a.Descripcion AS NomAnioLectivo, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, 
                  cp.NomParalelo, cp.OrdenParalelo, pr.pe_cedulaRuc, pr.pe_nombreCompleto, f.Direccion, isnull(f.Celular,'') Celular,isnull(f.Correo,'') Correo, isnull(pr.pe_telfono_Contacto,'') pe_telfono_Contacto, pro.Descripcion
FROM     dbo.tb_persona AS pr with (nolock) INNER JOIN
                  dbo.aca_Familia AS f with (nolock) ON pr.IdPersona = f.IdPersona and f.Estado=1 LEFT OUTER JOIN
                  dbo.tb_profesion AS pro with (nolock) ON pr.IdProfesion = pro.IdProfesion RIGHT OUTER JOIN
                  dbo.aca_Matricula AS m with (nolock) ON f.IdEmpresa = m.IdEmpresa AND f.IdAlumno = m.IdAlumno AND f.IdPersona = m.IdPersonaR LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS a with (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdAnio = a.IdAnio LEFT OUTER JOIN
                  dbo.tb_persona AS pa with (nolock) INNER JOIN
                  dbo.aca_Alumno AS al with (nolock) ON pa.IdPersona = al.IdPersona ON m.IdEmpresa = al.IdEmpresa AND m.IdAlumno = al.IdAlumno LEFT OUTER JOIN
                  dbo.aca_Plantilla with (nolock) ON m.IdEmpresa = dbo.aca_Plantilla.IdEmpresa AND m.IdAnio = dbo.aca_Plantilla.IdAnio AND m.IdPlantilla = dbo.aca_Plantilla.IdPlantilla LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn with (nolock) RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj with (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc with (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp with (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
                  m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo


where m.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = case when @IdSede = 0 then m.IdSede else @IdSede end
and m.IdNivel = case when @IdNivel = 0 then m.IdNivel else @IdNivel end
and m.IdJornada = case when @IdJornada = 0 then m.IdJornada else @IdJornada end
and m.IdCurso = case when @IdCurso = 0 then m.IdCurso else @IdCurso end
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and not exists(
	select f.IdEmpresa from aca_AlumnoRetiro as f with (nolock) 
	where f.IdEmpresa = m.IdEmpresa
	and f.IdMatricula = m.IdMatricula
	and f.Estado = 1
)