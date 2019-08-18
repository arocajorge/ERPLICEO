CREATE VIEW [web].[VWBAN_013]
AS
SELECT cp_orden_pago_cancelaciones.IdEmpresa_pago, cp_orden_pago_cancelaciones.IdTipoCbte_pago, cp_orden_pago_cancelaciones.IdCbteCble_pago, CASE WHEN ba_Archivo_Transferencia.IdArchivo IS NOT NULL 
                  THEN 'ARCHIVO BANCARIO' ELSE ba_Cbte_Ban_tipo.Descripcion END AS TipoPago, tb_persona.pe_nombreCompleto, ba_Cbte_Ban.IdBanco, ba_Banco_Cuenta.ba_descripcion, isnull(ba_Cbte_Ban.cb_Cheque,cast( ba_Cbte_Ban.IdCbteCble as varchar(20))) cb_Cheque, ba_Cbte_Ban.cb_Observacion BAN_Observacion, 
                  cp_orden_pago.IdPersona, ISNULL(cp_orden_giro.co_FechaFactura, ISNULL(cp_nota_DebCre.cn_fecha,cp_orden_pago.Fecha)) As CXP_Fecha, ISNULL('FA '+cp_orden_giro.co_factura,ISNULL('ND '+cp_nota_DebCre.cod_nota, 'OP '+cast( cp_orden_pago.IdOrdenPago as varchar(20)))) CXP_Documento,
				  isnull(cp_orden_giro.co_observacion,isnull(cp_nota_DebCre.cn_observacion,cp_orden_pago.Observacion)) as CXP_Observacion,cp_orden_pago_cancelaciones.MontoAplicado,
				  ba_Cbte_Ban.cb_Fecha BAN_Fecha, cp_orden_pago_cancelaciones.IdCbteCble_cxp
FROM     ba_Archivo_Transferencia RIGHT OUTER JOIN
                  tb_persona INNER JOIN
                  cp_orden_pago ON tb_persona.IdPersona = cp_orden_pago.IdPersona INNER JOIN
                  ba_Cbte_Ban INNER JOIN
                  ba_Banco_Cuenta ON ba_Cbte_Ban.IdEmpresa = ba_Banco_Cuenta.IdEmpresa AND ba_Cbte_Ban.IdBanco = ba_Banco_Cuenta.IdBanco INNER JOIN
                  cp_orden_pago_cancelaciones ON ba_Cbte_Ban.IdEmpresa = cp_orden_pago_cancelaciones.IdEmpresa_pago AND ba_Cbte_Ban.IdCbteCble = cp_orden_pago_cancelaciones.IdCbteCble_pago AND 
                  ba_Cbte_Ban.IdTipocbte = cp_orden_pago_cancelaciones.IdTipoCbte_pago ON cp_orden_pago.IdEmpresa = cp_orden_pago_cancelaciones.IdEmpresa_op AND 
                  cp_orden_pago.IdOrdenPago = cp_orden_pago_cancelaciones.IdOrdenPago_op INNER JOIN
                  ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo INNER JOIN
                  ba_Cbte_Ban_tipo ON ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.CodTipoCbteBan = ba_Cbte_Ban_tipo.CodTipoCbteBan ON ba_Cbte_Ban.IdEmpresa = ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdEmpresa AND 
                  ba_Cbte_Ban.IdTipocbte = ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdTipoCbteCble LEFT OUTER JOIN
                  cp_nota_DebCre ON cp_orden_pago_cancelaciones.IdEmpresa_cxp = cp_nota_DebCre.IdEmpresa AND cp_orden_pago_cancelaciones.IdTipoCbte_cxp = cp_nota_DebCre.IdTipoCbte_Nota AND 
                  cp_orden_pago_cancelaciones.IdCbteCble_cxp = cp_nota_DebCre.IdCbteCble_Nota LEFT OUTER JOIN
                  cp_orden_giro ON cp_orden_pago_cancelaciones.IdEmpresa_pago = cp_orden_giro.IdEmpresa AND cp_orden_pago_cancelaciones.IdTipoCbte_pago = cp_orden_giro.IdTipoCbte_Ogiro AND 
                  cp_orden_pago_cancelaciones.IdCbteCble_pago = cp_orden_giro.IdCbteCble_Ogiro ON ba_Archivo_Transferencia.IdTipoCbte = ba_Cbte_Ban.IdTipocbte AND ba_Archivo_Transferencia.IdCbteCble = ba_Cbte_Ban.IdCbteCble AND 
                  ba_Archivo_Transferencia.IdEmpresa = ba_Cbte_Ban.IdEmpresa