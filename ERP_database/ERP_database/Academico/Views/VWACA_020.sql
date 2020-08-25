create view Academico.VWACA_020 as
SELECT m.IdEmpresa, m.IdMatricula, al.Codigo, m.IdAlumno, pa.IdPersona, pa.pe_nombreCompleto AS NombreAlumno, pa.pe_cedulaRuc AS CedulaAlumno, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, sn.NomSede, 
                  a.Descripcion, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, m.IdPersonaR, prep.pe_cedulaRuc AS CedulaLegal, 
                  prep.pe_nombreCompleto AS NombreLegal, m.Fecha, m.IdPersonaF, dbo.tb_persona.pe_cedulaRuc AS CedulaFactura, dbo.tb_persona.pe_nombreCompleto AS NombreFactura, CASE WHEN r.IdRetiro IS NULL THEN CAST(0 AS BIT) 
                  ELSE CAST(1 AS BIT) END AS EsRetirado
FROM     dbo.aca_Matricula AS m INNER JOIN
                  dbo.aca_AnioLectivo AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAnio = a.IdAnio LEFT OUTER JOIN
                  dbo.tb_persona ON m.IdPersonaF = dbo.tb_persona.IdPersona LEFT OUTER JOIN
                  dbo.tb_persona AS prep ON m.IdPersonaR = prep.IdPersona LEFT OUTER JOIN
                  dbo.tb_persona AS pa INNER JOIN
                  dbo.aca_Alumno AS al ON pa.IdPersona = al.IdPersona ON m.IdEmpresa = al.IdEmpresa AND m.IdAlumno = al.IdAlumno LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
                  m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN
                  dbo.aca_AlumnoRetiro AS r ON m.IdEmpresa = r.IdEmpresa AND m.IdMatricula = r.IdMatricula AND r.Estado = 1
WHERE  (al.Estado = 1)