CREATE VIEW  vwfa_factura_ParaContabilizarAcademico
AS
SELECT fr.IdEmpresa, fr.IdSucursal, fr.IdBodega, fr.IdCbteVta, pp.IdCtaCbleDebe, pp.IdCtaCbleHaber, fr.SubtotalConDscto, fr.ValorIVA, fr.Total, tb_persona.pe_nombreCompleto AS NombreAlumno, 
                  tb_persona_1.pe_nombreCompleto AS NombreCliente, f.vt_serie1 + '-' + f.vt_serie2 + '-' + f.vt_NumFactura AS vt_NumFactura, f.vt_fecha, f.vt_Observacion
FROM     fa_factura AS f INNER JOIN
                  fa_factura_resumen AS fr ON f.IdCbteVta = fr.IdCbteVta AND f.IdBodega = fr.IdBodega AND f.IdSucursal = fr.IdSucursal AND f.IdEmpresa = fr.IdEmpresa INNER JOIN
                  aca_Alumno ON f.IdEmpresa = aca_Alumno.IdEmpresa AND f.IdAlumno = aca_Alumno.IdAlumno INNER JOIN
                  tb_persona ON aca_Alumno.IdPersona = tb_persona.IdPersona INNER JOIN
                  fa_cliente ON f.IdEmpresa = fa_cliente.IdEmpresa AND f.IdCliente = fa_cliente.IdCliente INNER JOIN
                  tb_persona AS tb_persona_1 ON fa_cliente.IdPersona = tb_persona_1.IdPersona INNER JOIN
                  aca_AnioLectivo_Curso_Plantilla_Parametrizacion AS pp INNER JOIN
                  aca_Matricula_Rubro AS m ON pp.IdEmpresa = m.IdEmpresa AND pp.IdAnio = m.IdAnio AND pp.IdSede = m.IdSede AND pp.IdNivel = m.IdNivel AND pp.IdJornada = m.IdJornada AND pp.IdCurso = m.IdCurso AND 
                  pp.IdPlantilla = m.IdPlantilla AND pp.IdRubro = m.IdRubro ON f.IdEmpresa = m.IdEmpresa AND f.IdSucursal = m.IdSucursal AND f.IdBodega = m.IdBodega AND f.IdCbteVta = m.IdCbteVta