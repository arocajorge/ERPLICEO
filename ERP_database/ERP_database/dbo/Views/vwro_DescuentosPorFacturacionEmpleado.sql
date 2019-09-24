CREATE VIEW vwro_DescuentosPorFacturacionEmpleado
AS
SELECT fa_TerminoPago.AplicaDescuentoNomina, fa_TerminoPago.nom_TerminoPago, fa_TerminoPago.Num_Coutas, fa_TerminoPago.CodigoRubroDescto, ro_empleado.IdEmpresa, ro_empleado.IdEmpleado, ro_empleado.IdPersona, 
                  tb_persona.pe_nombreCompleto, fa_factura_resumen.Total, fa_factura.vt_serie1+'-'+ fa_factura.vt_serie2 +'-'+ fa_factura.vt_NumFactura vt_NumFactura, fa_factura.vt_fecha, fa_factura.vt_Observacion, tb_persona.pe_cedulaRuc, ro_contrato.IdNomina, 
                  ro_contrato.EstadoContrato, isnull( ro_empleado.IdSucursalContabilizacion,ro_empleado.IdSucursal)IdSucursal, ro_empleado.em_status
FROM     fa_factura INNER JOIN
                  fa_factura_resumen ON fa_factura.IdEmpresa = fa_factura_resumen.IdEmpresa AND fa_factura.IdSucursal = fa_factura_resumen.IdSucursal AND fa_factura.IdBodega = fa_factura_resumen.IdBodega AND 
                  fa_factura.IdCbteVta = fa_factura_resumen.IdCbteVta INNER JOIN
                  fa_TerminoPago INNER JOIN
                  ro_rubro_tipo ON fa_TerminoPago.CodigoRubroDescto = ro_rubro_tipo.rub_codigo ON fa_factura.vt_tipo_venta = fa_TerminoPago.IdTerminoPago INNER JOIN
                  fa_cliente ON fa_factura.IdEmpresa = fa_cliente.IdEmpresa AND fa_factura.IdCliente = fa_cliente.IdCliente INNER JOIN
                  ro_empleado INNER JOIN
                  tb_persona ON ro_empleado.IdPersona = tb_persona.IdPersona ON fa_cliente.IdPersona = tb_persona.IdPersona INNER JOIN
                  ro_contrato ON ro_empleado.IdEmpresa = ro_contrato.IdEmpresa AND ro_empleado.IdEmpleado = ro_contrato.IdEmpleado
WHERE  (fa_TerminoPago.AplicaDescuentoNomina = 1) AND ro_contrato.EstadoContrato <> 'ECT_LIQ' AND fa_factura.Estado = 'A' and ro_empleado.em_status <> 'EST_LIQ'