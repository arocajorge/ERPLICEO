CREATE VIEW [web].[vwcp_orden_pago_con_cancelacion]
	AS

SELECT        dbo.cp_orden_pago_det.IdEmpresa, dbo.cp_orden_pago_det.IdOrdenPago, dbo.cp_orden_pago_det.Secuencia, dbo.cp_orden_pago_det.IdEmpresa_cxp, dbo.cp_orden_pago_det.Valor_a_pagar, 
                         dbo.cp_orden_pago_det.Referencia, dbo.cp_orden_pago_det.IdFormaPago, dbo.cp_orden_pago_det.Fecha_Pago, dbo.cp_orden_pago_cancelaciones.MontoAplicado, dbo.cp_orden_pago_cancelaciones.SaldoAnterior, 
                         dbo.cp_orden_pago_cancelaciones.SaldoActual, dbo.cp_orden_pago.Observacion, dbo.tb_persona.pe_nombreCompleto, dbo.tb_persona.pe_cedulaRuc, dbo.cp_orden_pago.IdTipo_op, dbo.cp_orden_pago.IdTipo_Persona, 
                         dbo.cp_orden_pago.IdPersona, dbo.cp_orden_pago.IdEntidad, dbo.cp_orden_pago.IdEstadoAprobacion, dbo.cp_orden_pago_cancelaciones.IdTipoCbte_cxp, dbo.cp_orden_pago_cancelaciones.IdCbteCble_cxp
FROM            dbo.cp_orden_pago INNER JOIN
                         dbo.cp_orden_pago_det ON dbo.cp_orden_pago.IdEmpresa = dbo.cp_orden_pago_det.IdEmpresa AND dbo.cp_orden_pago.IdOrdenPago = dbo.cp_orden_pago_det.IdOrdenPago INNER JOIN
                         dbo.cp_orden_pago_cancelaciones ON dbo.cp_orden_pago_det.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_op_padre AND 
                         dbo.cp_orden_pago_det.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_op AND dbo.cp_orden_pago_det.IdOrdenPago = dbo.cp_orden_pago_cancelaciones.IdOrdenPago_op AND 
                         dbo.cp_orden_pago_det.IdOrdenPago = dbo.cp_orden_pago_cancelaciones.IdOrdenPago_op_padre AND dbo.cp_orden_pago_det.Secuencia = dbo.cp_orden_pago_cancelaciones.Secuencia_op_padre AND 
                         dbo.cp_orden_pago_det.Secuencia = dbo.cp_orden_pago_cancelaciones.Secuencia_op INNER JOIN
                         dbo.cp_proveedor ON dbo.cp_orden_pago.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.cp_orden_pago.IdEntidad = dbo.cp_proveedor.IdProveedor AND 
                         dbo.cp_orden_pago.IdPersona = dbo.cp_proveedor.IdPersona INNER JOIN
                         dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona