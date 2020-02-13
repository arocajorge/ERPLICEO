CREATE VIEW [Academico].[VWCXC_002]
AS
SELECT a.IdEmpresa, A.IdSucursal, a.IdCobro, a.IdAlumno, c.pe_nombreCompleto, CASE WHEN a.cr_estado = 'I' THEN 'ANULADO' ELSE '' END AS cr_estado, a.cr_fecha, d.tc_descripcion, b.Codigo AS CodigoAlumno, a.cr_observacion, e.NomSede, e.NomNivel, 
                  e.NomJornada, e.NomCurso, e.NomParalelo, e.CodigoParalelo, e.NomPlantilla, a.cr_TotalCobro, a.cr_Saldo, g.pe_nombreCompleto AS NomCliente, g.pe_cedulaRuc AS CedulaCliente, a.IdCliente, case when a.IdCobro_tipo = 'TARJ' THEN h.NombreTarjeta else a.cr_Banco END cr_Banco,
				  case when a.IdCobro_tipo = 'TARJ' THEN a.cr_Tarjeta else a.cr_NumDocumento END cr_NumDocumento
FROM     dbo.cxc_cobro AS a INNER JOIN
                  dbo.aca_Alumno AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdAlumno = b.IdAlumno INNER JOIN
                  dbo.tb_persona AS c ON c.IdPersona = b.IdPersona INNER JOIN
                  dbo.cxc_cobro_tipo AS d ON a.IdCobro_tipo = d.IdCobro_tipo LEFT OUTER JOIN
                      (SELECT A1.IdEmpresa, A1.IdAnio, A1.IdAlumno, SedeNivel.NomSede, SedeNivel.NomNivel, NivelJornada.NomJornada, JornadaCurso.NomCurso, CursoParalelo.NomParalelo, CursoParalelo.CodigoParalelo, A3.NomPlantilla
                       FROM      dbo.aca_AnioLectivo_NivelAcademico_Jornada AS NivelJornada INNER JOIN
                                         dbo.aca_AnioLectivo_Sede_NivelAcademico AS SedeNivel ON NivelJornada.IdEmpresa = SedeNivel.IdEmpresa AND NivelJornada.IdAnio = SedeNivel.IdAnio AND NivelJornada.IdSede = SedeNivel.IdSede AND 
                                         NivelJornada.IdNivel = SedeNivel.IdNivel INNER JOIN
                                         dbo.aca_AnioLectivo_Jornada_Curso AS JornadaCurso ON NivelJornada.IdEmpresa = JornadaCurso.IdEmpresa AND NivelJornada.IdAnio = JornadaCurso.IdAnio AND NivelJornada.IdSede = JornadaCurso.IdSede AND 
                                         NivelJornada.IdNivel = JornadaCurso.IdNivel AND NivelJornada.IdJornada = JornadaCurso.IdJornada INNER JOIN
                                         dbo.aca_AnioLectivo_Curso_Paralelo AS CursoParalelo ON JornadaCurso.IdEmpresa = CursoParalelo.IdEmpresa AND JornadaCurso.IdAnio = CursoParalelo.IdAnio AND JornadaCurso.IdSede = CursoParalelo.IdSede AND 
                                         JornadaCurso.IdNivel = CursoParalelo.IdNivel AND JornadaCurso.IdJornada = CursoParalelo.IdJornada AND JornadaCurso.IdCurso = CursoParalelo.IdCurso LEFT OUTER JOIN
                                         dbo.aca_Matricula AS A1 INNER JOIN
                                         dbo.aca_AnioLectivo AS A2 ON A1.IdEmpresa = A2.IdEmpresa AND A1.IdAnio = A2.IdAnio ON CursoParalelo.IdEmpresa = A1.IdEmpresa AND CursoParalelo.IdAnio = A1.IdAnio AND CursoParalelo.IdSede = A1.IdSede AND 
                                         CursoParalelo.IdNivel = A1.IdNivel AND CursoParalelo.IdJornada = A1.IdJornada AND CursoParalelo.IdCurso = A1.IdCurso AND CursoParalelo.IdParalelo = A1.IdPersonaF LEFT OUTER JOIN
                                         dbo.aca_Plantilla AS A3 ON A1.IdEmpresa = A3.IdEmpresa AND A1.IdAnio = A3.IdAnio AND A1.IdPlantilla = A3.IdPlantilla
                       WHERE   (A2.EnCurso = 1)) AS e ON b.IdEmpresa = e.IdEmpresa AND b.IdAlumno = e.IdAlumno INNER JOIN
                  dbo.fa_cliente AS f ON f.IdEmpresa = a.IdEmpresa AND f.IdCliente = a.IdCliente INNER JOIN
                  dbo.tb_persona AS g ON g.IdPersona = f.IdPersona left join 
				  tb_TarjetaCredito h on a.IdEmpresa = h.IdEmpresa and a.IdTarjeta = h.IdTarjeta