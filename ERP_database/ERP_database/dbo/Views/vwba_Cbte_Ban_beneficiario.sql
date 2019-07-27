CREATE VIEW [dbo].[vwba_Cbte_Ban_beneficiario]
AS
SELECT ba_Cbte_Ban.IdEmpresa, ba_Cbte_Ban.IdTipocbte, ba_Cbte_Ban.IdCbteCble, tb_persona.IdPersona, tb_persona.pe_nombreCompleto
FROM     ba_Cbte_Ban INNER JOIN
                  tb_persona ON ba_Cbte_Ban.IdPersona_Girado_a = tb_persona.IdPersona
WHERE  cb_Cheque IS NOT NULL
UNION
SELECT dbo.ba_Cbte_Ban.IdEmpresa, dbo.ba_Cbte_Ban.IdTipocbte, dbo.ba_Cbte_Ban.IdCbteCble, dbo.tb_persona.IdPersona, dbo.tb_persona.pe_nombreCompleto
FROM     dbo.cp_orden_pago INNER JOIN
                  dbo.tb_persona ON dbo.cp_orden_pago.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.cp_orden_pago_cancelaciones INNER JOIN
                  dbo.ba_Cbte_Ban ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_pago = dbo.ba_Cbte_Ban.IdEmpresa AND dbo.cp_orden_pago_cancelaciones.IdCbteCble_pago = dbo.ba_Cbte_Ban.IdCbteCble AND 
                  dbo.cp_orden_pago_cancelaciones.IdTipoCbte_pago = dbo.ba_Cbte_Ban.IdTipocbte ON dbo.cp_orden_pago.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_op AND 
                  dbo.cp_orden_pago.IdOrdenPago = dbo.cp_orden_pago_cancelaciones.IdOrdenPago_op
WHERE  (dbo.ba_Cbte_Ban.cb_Cheque IS NULL)
GROUP BY dbo.ba_Cbte_Ban.IdEmpresa, dbo.ba_Cbte_Ban.IdTipocbte, dbo.ba_Cbte_Ban.IdCbteCble, dbo.tb_persona.IdPersona, dbo.tb_persona.pe_nombreCompleto
/*UNION
SELECT ba_Cbte_Ban.IdEmpresa, ba_Cbte_Ban.IdTipocbte, ba_Cbte_Ban.IdCbteCble, tb_persona.IdPersona, tb_persona.pe_nombreCompleto
FROM     ro_empleado INNER JOIN
                  tb_persona ON ro_empleado.IdPersona = tb_persona.IdPersona INNER JOIN
                  ba_Archivo_Transferencia_Det INNER JOIN
                  ba_Cbte_Ban ON ba_Archivo_Transferencia_Det.IdEmpresa_pago = ba_Cbte_Ban.IdEmpresa AND ba_Archivo_Transferencia_Det.IdCbteCble_pago = ba_Cbte_Ban.IdCbteCble AND 
                  ba_Archivo_Transferencia_Det.IdTipoCbte_pago = ba_Cbte_Ban.IdTipocbte ON ro_empleado.IdEmpleado = ba_Archivo_Transferencia_Det.IdEmpleado AND ro_empleado.IdEmpresa = ba_Archivo_Transferencia_Det.IdEmpresa
GROUP BY ba_Cbte_Ban.IdEmpresa, ba_Cbte_Ban.IdTipocbte, ba_Cbte_Ban.IdCbteCble, tb_persona.IdPersona, tb_persona.pe_nombreCompleto*/
UNION
SELECT dbo.ba_Cbte_Ban.IdEmpresa, dbo.ba_Cbte_Ban.IdTipocbte, dbo.ba_Cbte_Ban.IdCbteCble, dbo.tb_persona.IdPersona, dbo.tb_persona.pe_nombreCompleto
FROM     dbo.ba_Cbte_Ban INNER JOIN
                  dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdEmpresa AND 
                  dbo.ba_Cbte_Ban.IdCbteCble = dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdCbteCble AND dbo.ba_Cbte_Ban.IdTipocbte = dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdTipocbte INNER JOIN
                  dbo.caj_Caja_Movimiento ON dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdEmpresa = dbo.caj_Caja_Movimiento.IdEmpresa AND 
                  dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdCbteCble = dbo.caj_Caja_Movimiento.IdCbteCble AND dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdTipocbte = dbo.caj_Caja_Movimiento.IdTipocbte INNER JOIN
                  dbo.tb_persona ON dbo.caj_Caja_Movimiento.IdPersona = dbo.tb_persona.IdPersona
GROUP BY dbo.ba_Cbte_Ban.IdEmpresa, dbo.ba_Cbte_Ban.IdTipocbte, dbo.ba_Cbte_Ban.IdCbteCble, dbo.tb_persona.IdPersona, dbo.tb_persona.pe_nombreCompleto