CREATE VIEW Academico.VWACA_003
AS
SELECT m.IdEmpresa, m.IdMatricula, m.IdAlumno, Representante.pe_nombreCompleto AS NomRepresentante, Representante.pe_cedulaRuc AS CedulaRepresentante, Factura.pe_nombreCompleto AS NomSeFactura, 
                  Factura.pe_cedulaRuc AS CedulaSeFactura, p.pe_nombreCompleto AS NomAlumno, p.pe_nombre AS CedulaAlumno, aca_AnioLectivo_Jornada_Curso.NomCurso, AnioActual.Descripcion AS DescripcionActual, 
                  AnioAnterior.Descripcion AS DescripcionAnterior, aca_Familia.Direccion, MesInicio.smes +' de '+ cast(year(AnioActual.FechaDesde) as varchar)+' a '+MesFin.smes+' de '+cast(year(AnioActual.FechaHasta) as varchar) as DescripcionPensiones
FROM     tb_persona AS p INNER JOIN
                  aca_Matricula AS m INNER JOIN
                  aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN
                  aca_AnioLectivo AS AnioActual ON m.IdEmpresa = AnioActual.IdEmpresa AND m.IdAnio = AnioActual.IdAnio ON p.IdPersona = a.IdPersona INNER JOIN
                  tb_persona AS Factura ON m.IdPersonaF = Factura.IdPersona INNER JOIN
                  tb_persona AS Representante ON m.IdPersonaR = Representante.IdPersona INNER JOIN
                  aca_AnioLectivo_Jornada_Curso ON m.IdEmpresa = aca_AnioLectivo_Jornada_Curso.IdEmpresa AND m.IdAnio = aca_AnioLectivo_Jornada_Curso.IdAnio AND m.IdSede = aca_AnioLectivo_Jornada_Curso.IdSede AND 
                  m.IdNivel = aca_AnioLectivo_Jornada_Curso.IdNivel AND m.IdJornada = aca_AnioLectivo_Jornada_Curso.IdJornada AND m.IdCurso = aca_AnioLectivo_Jornada_Curso.IdCurso INNER JOIN
                  aca_Familia ON a.IdEmpresa = aca_Familia.IdEmpresa AND a.IdAlumno = aca_Familia.IdAlumno AND Representante.IdPersona = aca_Familia.IdPersona LEFT OUTER JOIN
                  aca_AnioLectivo AS AnioAnterior ON AnioActual.IdEmpresa = AnioAnterior.IdEmpresa AND AnioActual.IdAnioLectivoAnterior = AnioAnterior.IdAnio inner join
				  tb_mes AS MesInicio on month(AnioActual.FechaDesde) = MesInicio.idMes
				  inner join tb_mes as MesFin on month(AnioActual.FechaHasta) = MesFin.idMes