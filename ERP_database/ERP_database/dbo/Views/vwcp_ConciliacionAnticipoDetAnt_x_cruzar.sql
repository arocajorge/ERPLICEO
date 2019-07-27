CREATE VIEW [dbo].[vwcp_ConciliacionAnticipoDetAnt_x_cruzar]
AS
SELECT dbo.cp_orden_pago_det.IdEmpresa, dbo.cp_orden_pago_det.IdOrdenPago, dbo.cp_orden_pago.IdSucursal, dbo.cp_orden_pago.IdEntidad AS IdProveedor, dbo.tb_persona.pe_nombreCompleto, dbo.cp_orden_pago.Observacion, 
                  dbo.cp_orden_pago.Fecha, dbo.cp_orden_pago_det.Valor_a_pagar AS ValorOP, ISNULL(DET.MontoAplicado, 0) AS MontoAplicado, round(dbo.cp_orden_pago_det.Valor_a_pagar - ISNULL(DET.MontoAplicado, 0),2) AS Saldo
FROM     dbo.cp_orden_pago INNER JOIN
                  dbo.cp_orden_pago_det ON dbo.cp_orden_pago.IdEmpresa = dbo.cp_orden_pago_det.IdEmpresa AND dbo.cp_orden_pago.IdOrdenPago = dbo.cp_orden_pago_det.IdOrdenPago INNER JOIN
                  dbo.cp_orden_pago_cancelaciones ON dbo.cp_orden_pago_det.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_op AND dbo.cp_orden_pago_det.IdOrdenPago = dbo.cp_orden_pago_cancelaciones.IdOrdenPago_op AND 
                  dbo.cp_orden_pago_det.Secuencia = dbo.cp_orden_pago_cancelaciones.Secuencia_op INNER JOIN
                  dbo.cp_orden_pago_tipo_x_empresa ON dbo.cp_orden_pago.IdTipo_op = dbo.cp_orden_pago_tipo_x_empresa.IdTipo_op AND dbo.cp_orden_pago.IdEmpresa = dbo.cp_orden_pago_tipo_x_empresa.IdEmpresa INNER JOIN
                  dbo.tb_persona ON dbo.cp_orden_pago.IdPersona = dbo.tb_persona.IdPersona LEFT OUTER JOIN
                      (SELECT IdEmpresa, IdOrdenPago, SUM(MontoAplicado) AS MontoAplicado
                       FROM      dbo.cp_ConciliacionAnticipoDetAnt AS D
                       GROUP BY IdEmpresa, IdOrdenPago) AS DET ON dbo.cp_orden_pago.IdEmpresa = DET.IdEmpresa AND dbo.cp_orden_pago.IdOrdenPago = DET.IdOrdenPago
WHERE  (dbo.cp_orden_pago_tipo_x_empresa.Dispara_Alerta = 1) AND (dbo.cp_orden_pago.IdTipo_Persona = 'PROVEE') AND (dbo.cp_orden_pago.Estado = 'A') and round(dbo.cp_orden_pago_det.Valor_a_pagar - ISNULL(DET.MontoAplicado, 0),2) > 0