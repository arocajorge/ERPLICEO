CREATE VIEW vwcp_orden_pago_det_con_cta_acreedora
AS
SELECT        dbo.cp_orden_pago_det.IdEmpresa, dbo.cp_orden_pago_det.IdOrdenPago, dbo.cp_orden_pago_det.Secuencia, dbo.cp_orden_pago_det.IdEmpresa_cxp, dbo.cp_orden_pago_det.IdCbteCble_cxp, 
                         dbo.cp_orden_pago_det.IdTipoCbte_cxp, dbo.cp_orden_pago_det.Valor_a_pagar, dbo.cp_orden_pago_det.Referencia, dbo.cp_orden_pago_det.IdFormaPago, dbo.cp_orden_pago_det.Fecha_Pago, 
                         dbo.vwct_cbtecble_con_ctacble_acreedora.IdCtaCble_Acreedora, dbo.cp_orden_pago.Observacion, dbo.cp_orden_pago.IdTipo_op, dbo.cp_orden_pago.Fecha, dbo.tb_persona.pe_nombreCompleto AS Nombre, 
                         dbo.cp_orden_pago.IdTipo_Persona, dbo.cp_orden_pago.IdPersona, dbo.cp_orden_pago.IdEntidad
FROM            dbo.cp_orden_pago INNER JOIN
                         dbo.cp_orden_pago_det ON dbo.cp_orden_pago.IdEmpresa = dbo.cp_orden_pago_det.IdEmpresa AND dbo.cp_orden_pago.IdOrdenPago = dbo.cp_orden_pago_det.IdOrdenPago INNER JOIN
                         dbo.tb_persona ON dbo.cp_orden_pago.IdPersona = dbo.tb_persona.IdPersona LEFT OUTER JOIN
                         dbo.vwct_cbtecble_con_ctacble_acreedora ON dbo.cp_orden_pago_det.IdEmpresa_cxp = dbo.vwct_cbtecble_con_ctacble_acreedora.IdEmpresa AND 
                         dbo.cp_orden_pago_det.IdTipoCbte_cxp = dbo.vwct_cbtecble_con_ctacble_acreedora.IdTipoCbte AND dbo.cp_orden_pago_det.IdCbteCble_cxp = dbo.vwct_cbtecble_con_ctacble_acreedora.IdCbteCble