CREATE VIEW Academico.VWCXC_012
AS
SELECT sc.IdEmpresa, sc.IdSeguimiento, sc.IdAlumno, sc.IdMatricula, CASE WHEN sc.CorreoEnviado = 1 THEN 'ENVIADO' ELSE '' END AS CorreoEnviado, sc.Fecha, sc.Observacion, sc.Estado, sc.IdUsuarioCreacion, sc.FechaCreacion, 
                  p.pe_nombreCompleto, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.CodigoParalelo, cp.OrdenParalelo
FROM     aca_AnioLectivo_Curso_Paralelo AS cp LEFT OUTER JOIN
                  aca_AnioLectivo_Jornada_Curso AS jc ON cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada AND cp.IdCurso = jc.IdCurso LEFT OUTER JOIN
                  aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdEmpresa = nj.IdEmpresa AND jc.IdAnio = nj.IdAnio AND 
                  jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel AND jc.IdJornada = nj.IdJornada RIGHT OUTER JOIN
                  cxc_SeguimientoCartera AS sc INNER JOIN
                  aca_Alumno AS a ON sc.IdEmpresa = a.IdEmpresa AND sc.IdAlumno = a.IdAlumno INNER JOIN
                  tb_persona AS p ON a.IdPersona = p.IdPersona LEFT OUTER JOIN
                  aca_Matricula AS m ON sc.IdEmpresa = m.IdEmpresa AND sc.IdMatricula = m.IdMatricula ON cp.IdEmpresa = m.IdEmpresa AND cp.IdAnio = m.IdAnio AND cp.IdSede = m.IdSede AND cp.IdNivel = m.IdNivel AND 
                  cp.IdJornada = m.IdJornada AND cp.IdCurso = m.IdCurso AND cp.IdParalelo = m.IdParalelo