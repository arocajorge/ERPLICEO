CREATE PROCEDURE [Academico].[SPACA_020]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdAlumno decimal,
@MostrarRetirados bit
)
AS
SELECT m.IdEmpresa, m.IdMatricula, al.Codigo, m.IdAlumno, pa.IdPersona, pa.pe_nombreCompleto AS NombreAlumno, pa.pe_cedulaRuc AS CedulaAlumno, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, sn.NomSede, 
                  a.Descripcion, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, m.IdPersonaR, prep.pe_cedulaRuc AS CedulaLegal, 
                  prep.pe_nombreCompleto AS NombreLegal, m.Fecha, m.IdPersonaF, dbo.tb_persona.pe_cedulaRuc AS CedulaFactura, dbo.tb_persona.pe_nombreCompleto AS NombreFactura, 
				  case when ret.IdMatricula is not null then cast(1 as bit) else cast(0 as bit) end as EsRetirado,
				case when ret.IdMatricula is not null THEN 'RETIRADO' else 'ACTIVO' end as EsRetiradoString, pension.Total
FROM     dbo.aca_Matricula AS m INNER JOIN
                  dbo.aca_AnioLectivo AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAnio = a.IdAnio 
				  INNER JOIN
                  (
				  select pla.IdEmpresa, pla.IdPlantilla, pla.IdAnio, pru.IdRubro, pru.Total
					from aca_Plantilla pla 
					inner join aca_Plantilla_Rubro pru on pru.IdEmpresa = pla.IdEmpresa and pru.IdPlantilla = pla.IdPlantilla and pru.IdRubro=2
				  ) as pension
				  on pension.IdEmpresa = m.IdEmpresa and pension.IdPlantilla=m.IdPlantilla and pension.IdAnio=m.IdAnio
				  LEFT OUTER JOIN
                  dbo.tb_persona ON m.IdPersonaF = dbo.tb_persona.IdPersona LEFT OUTER JOIN
                  dbo.tb_persona AS prep ON m.IdPersonaR = prep.IdPersona LEFT OUTER JOIN
                  dbo.tb_persona AS pa INNER JOIN
                  dbo.aca_Alumno AS al ON pa.IdPersona = al.IdPersona ON m.IdEmpresa = al.IdEmpresa AND m.IdAlumno = al.IdAlumno LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
                  m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo 
				  left join
		(
		select r.IdEmpresa, r.IdMatricula 
		from aca_AlumnoRetiro as r
		where r.Estado = 1
		) as ret on m.IdEmpresa =ret.IdEmpresa and m.IdMatricula = ret.IdMatricula
where m.IdEmpresa = @IdEmpresa 
and m.IdAnio = @IdAnio
and m.IdSede = case when @IdSede = 0 then m.IdSede else @IdSede end
and m.IdNivel = case when @IdNivel = 0 then m.IdNivel else @IdNivel end
and m.IdJornada = case when @IdJornada = 0 then m.IdJornada else @IdJornada end
and m.IdCurso = case when @IdCurso = 0 then m.IdCurso else @IdCurso end
and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end
and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end
and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end
and al.Estado = 1