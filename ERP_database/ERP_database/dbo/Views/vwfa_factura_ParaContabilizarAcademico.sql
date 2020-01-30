CREATE VIEW vwfa_factura_ParaContabilizarAcademico
AS
SELECT fa_factura_resumen.IdEmpresa, fa_factura_resumen.IdSucursal, fa_factura_resumen.IdBodega, fa_factura_resumen.IdCbteVta, aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdCtaCbleDebe, 
                  aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdCtaCbleHaber, fa_factura_resumen.SubtotalConDscto, fa_factura_resumen.ValorIVA, fa_factura_resumen.Total, tb_persona.pe_nombreCompleto AS NombreAlumno, 
                  tb_persona_1.pe_nombreCompleto AS NombreCliente, fa_factura.vt_serie1 + '-' + fa_factura.vt_serie2 + '-' + fa_factura.vt_NumFactura AS vt_NumFactura, fa_factura.vt_fecha, fa_factura.vt_Observacion
FROM     aca_AnioLectivo_Curso_Plantilla_Parametrizacion INNER JOIN
                  fa_factura_resumen ON aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdEmpresa = fa_factura_resumen.IdEmpresa AND aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdAnio = fa_factura_resumen.IdAnio AND 
                  aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdPlantilla = fa_factura_resumen.IdPlantilla AND aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdRubro = fa_factura_resumen.IdRubro INNER JOIN
                  aca_Matricula ON aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdEmpresa = aca_Matricula.IdEmpresa AND aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdAnio = aca_Matricula.IdAnio AND 
                  aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdSede = aca_Matricula.IdSede AND aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdNivel = aca_Matricula.IdNivel AND 
                  aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdJornada = aca_Matricula.IdJornada AND aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdCurso = aca_Matricula.IdCurso INNER JOIN
                  fa_factura ON fa_factura_resumen.IdCbteVta = fa_factura.IdCbteVta AND fa_factura_resumen.IdBodega = fa_factura.IdBodega AND fa_factura_resumen.IdSucursal = fa_factura.IdSucursal AND 
                  fa_factura_resumen.IdEmpresa = fa_factura.IdEmpresa AND aca_Matricula.IdAlumno = fa_factura.IdAlumno INNER JOIN
                  aca_Alumno ON fa_factura.IdEmpresa = aca_Alumno.IdEmpresa AND fa_factura.IdAlumno = aca_Alumno.IdAlumno INNER JOIN
                  tb_persona ON aca_Alumno.IdPersona = tb_persona.IdPersona INNER JOIN
                  fa_cliente ON fa_factura.IdEmpresa = fa_cliente.IdEmpresa AND fa_factura.IdCliente = fa_cliente.IdCliente INNER JOIN
                  tb_persona AS tb_persona_1 ON fa_cliente.IdPersona = tb_persona_1.IdPersona