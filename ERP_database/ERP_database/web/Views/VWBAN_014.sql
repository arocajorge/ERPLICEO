CREATE VIEW [web].[VWBAN_014]
AS
SELECT dbo.cp_orden_pago_cancelaciones.IdEmpresa_pago, dbo.cp_orden_pago_cancelaciones.IdTipoCbte_pago, dbo.cp_orden_pago_cancelaciones.IdCbteCble_pago, CASE WHEN ba_Archivo_Transferencia.IdArchivo IS NOT NULL 
                  THEN 'ARCHIVO BANCARIO' ELSE ba_Cbte_Ban_tipo.Descripcion END AS TipoPago, dbo.tb_persona.pe_nombreCompleto, dbo.ba_Cbte_Ban.IdBanco, dbo.ba_Banco_Cuenta.ba_descripcion, ISNULL(dbo.ba_Cbte_Ban.cb_Cheque, 
                  CAST(dbo.ba_Cbte_Ban.IdCbteCble AS varchar(20))) AS cb_Cheque, dbo.ba_Cbte_Ban.cb_Observacion AS BAN_Observacion, dbo.cp_orden_pago.IdPersona, ISNULL(dbo.cp_orden_giro.co_FechaFactura, 
                  ISNULL(dbo.cp_nota_DebCre.cn_fecha, dbo.cp_orden_pago.Fecha)) AS CXP_Fecha, ISNULL(dbo.cp_orden_giro.co_factura, ISNULL(dbo.cp_nota_DebCre.cod_nota, 
                  'OP ' + CAST(dbo.cp_orden_pago.IdOrdenPago AS varchar(20)))) AS CXP_Documento, ISNULL(dbo.cp_orden_giro.co_observacion, ISNULL(dbo.cp_nota_DebCre.cn_observacion, dbo.cp_orden_pago.Observacion)) AS CXP_Observacion, 
                  dbo.cp_orden_pago_cancelaciones.MontoAplicado, dbo.ba_Cbte_Ban.cb_Fecha AS BAN_Fecha, dbo.cp_orden_pago_cancelaciones.IdCbteCble_cxp, cp_orden_pago_cancelaciones.IdTipoCbte_cxp
FROM     dbo.ba_Archivo_Transferencia RIGHT OUTER JOIN
                  dbo.tb_persona INNER JOIN
                  dbo.cp_orden_pago ON dbo.tb_persona.IdPersona = dbo.cp_orden_pago.IdPersona INNER JOIN
                  dbo.ba_Cbte_Ban INNER JOIN
                  dbo.ba_Banco_Cuenta ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ba_Banco_Cuenta.IdEmpresa AND dbo.ba_Cbte_Ban.IdBanco = dbo.ba_Banco_Cuenta.IdBanco INNER JOIN
                  dbo.cp_orden_pago_cancelaciones ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_pago AND dbo.ba_Cbte_Ban.IdCbteCble = dbo.cp_orden_pago_cancelaciones.IdCbteCble_pago AND 
                  dbo.ba_Cbte_Ban.IdTipocbte = dbo.cp_orden_pago_cancelaciones.IdTipoCbte_pago ON dbo.cp_orden_pago.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_op AND 
                  dbo.cp_orden_pago.IdOrdenPago = dbo.cp_orden_pago_cancelaciones.IdOrdenPago_op INNER JOIN
                  dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo INNER JOIN
                  dbo.ba_Cbte_Ban_tipo ON dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.CodTipoCbteBan = dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdEmpresa AND 
                  dbo.ba_Cbte_Ban.IdTipocbte = dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdTipoCbteCble LEFT OUTER JOIN
                  dbo.cp_orden_giro ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_cxp = dbo.cp_orden_giro.IdEmpresa AND dbo.cp_orden_pago_cancelaciones.IdTipoCbte_cxp = dbo.cp_orden_giro.IdTipoCbte_Ogiro AND 
                  dbo.cp_orden_pago_cancelaciones.IdCbteCble_cxp = dbo.cp_orden_giro.IdCbteCble_Ogiro LEFT OUTER JOIN
                  dbo.cp_nota_DebCre ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_cxp = dbo.cp_nota_DebCre.IdEmpresa AND dbo.cp_orden_pago_cancelaciones.IdTipoCbte_cxp = dbo.cp_nota_DebCre.IdTipoCbte_Nota AND 
                  dbo.cp_orden_pago_cancelaciones.IdCbteCble_cxp = dbo.cp_nota_DebCre.IdCbteCble_Nota ON dbo.ba_Archivo_Transferencia.IdTipoCbte = dbo.ba_Cbte_Ban.IdTipocbte AND 
                  dbo.ba_Archivo_Transferencia.IdCbteCble = dbo.ba_Cbte_Ban.IdCbteCble AND dbo.ba_Archivo_Transferencia.IdEmpresa = dbo.ba_Cbte_Ban.IdEmpresa