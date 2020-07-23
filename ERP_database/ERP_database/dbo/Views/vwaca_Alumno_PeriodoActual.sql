﻿CREATE VIEW vwaca_Alumno_PeriodoActual
AS
SELECT a.IdEmpresa, a.Codigo, a.IdAlumno, b.pe_nombreCompleto AS NombreAlumno, b.pe_cedulaRuc, c.pe_nombreCompleto AS NombreRepresentante, c.Correo AS CorreoRepresentante, d.pe_nombreCompleto AS NombreEmiteFactura, 
                  d.Correo AS CorreoEmiteFactura, c.Celular AS CelularRepresentante, d.Celular AS CelularEmiteFactura, e.IdAnio, e.IdSede, e.IdNivel, e.IdJornada, e.IdCurso, e.IdParalelo,
				  sn.NomSede, sn.NomNivel, nj.NomJornada, jc.NomCurso, cp.NomParalelo
FROM     aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso RIGHT OUTER JOIN
                  aca_Alumno AS a INNER JOIN
                  tb_persona AS b ON a.IdPersona = b.IdPersona LEFT OUTER JOIN
                      (SELECT xx.IdEmpresa, xx.IdAlumno, xx.Celular, xx.Correo, xxx.pe_nombreCompleto
                       FROM      aca_Familia AS xx LEFT OUTER JOIN
                                         tb_persona AS xxx ON xx.IdPersona = xxx.IdPersona
                       WHERE   (xx.EsRepresentante = 1) AND (xx.Estado = 1)) AS c ON a.IdEmpresa = c.IdEmpresa AND a.IdAlumno = c.IdAlumno LEFT OUTER JOIN
                      (SELECT xx.IdEmpresa, xx.IdAlumno, xx.Celular, xx.Correo, xxx.pe_nombreCompleto
                       FROM      aca_Familia AS xx LEFT OUTER JOIN
                                         tb_persona AS xxx ON xx.IdPersona = xxx.IdPersona
                       WHERE   (xx.SeFactura = 1) AND (xx.Estado = 1)) AS d ON a.IdEmpresa = d.IdEmpresa AND a.IdAlumno = d.IdAlumno INNER JOIN
                  aca_Matricula AS e ON a.IdEmpresa = e.IdEmpresa AND a.IdAlumno = e.IdAlumno INNER JOIN
                  aca_AnioLectivo AS f ON e.IdEmpresa = f.IdEmpresa AND e.IdAnio = f.IdAnio ON cp.IdEmpresa = e.IdEmpresa AND cp.IdAnio = e.IdAnio AND cp.IdSede = e.IdSede AND cp.IdNivel = e.IdNivel AND cp.IdJornada = e.IdJornada AND 
                  cp.IdCurso = e.IdCurso AND cp.IdParalelo = e.IdParalelo
WHERE  (f.EnCurso = 1) AND (f.Estado = 1) AND (a.Estado = 1) AND (NOT EXISTS
                      (SELECT IdEmpresa
                       FROM      aca_AlumnoRetiro AS xx
                       WHERE   (e.IdEmpresa = IdEmpresa) AND (e.IdMatricula = IdMatricula) AND (Estado = 1)))