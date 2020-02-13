CREATE VIEW [Academico].[VWACA_007]
AS
SELECT sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.OrdenCurso, jc.NomCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, p.pe_sexo, 1 AS Cantidad, dbo.aca_Matricula.IdEmpresa, 
                  dbo.aca_Matricula.IdMatricula, dbo.aca_Matricula.IdAnio, dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdNivel, dbo.aca_Matricula.IdJornada, dbo.aca_Matricula.IdCurso, dbo.aca_Matricula.IdParalelo, dbo.aca_Matricula.Fecha, 
                  dbo.aca_Plantilla.NomPlantilla, dbo.aca_Plantilla.IdPlantilla
FROM     dbo.aca_AnioLectivo AS a INNER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn ON a.IdEmpresa = sn.IdEmpresa AND a.IdAnio = sn.IdAnio INNER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel INNER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso INNER JOIN
                  dbo.aca_Matricula ON cp.IdEmpresa = dbo.aca_Matricula.IdEmpresa AND cp.IdAnio = dbo.aca_Matricula.IdAnio AND cp.IdSede = dbo.aca_Matricula.IdSede AND cp.IdNivel = dbo.aca_Matricula.IdNivel AND 
                  cp.IdJornada = dbo.aca_Matricula.IdJornada AND cp.IdCurso = dbo.aca_Matricula.IdCurso AND cp.IdParalelo = dbo.aca_Matricula.IdParalelo INNER JOIN
                  dbo.aca_Alumno AS alu ON dbo.aca_Matricula.IdEmpresa = alu.IdEmpresa AND dbo.aca_Matricula.IdAlumno = alu.IdAlumno INNER JOIN
                  dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
                  dbo.aca_Plantilla ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Plantilla.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.aca_Plantilla.IdAnio AND dbo.aca_Matricula.IdPlantilla = dbo.aca_Plantilla.IdPlantilla