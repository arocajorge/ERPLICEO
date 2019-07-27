CREATE VIEW [dbo].[vwCXP_Rpt001]
AS
SELECT        isnull(ROW_NUMBER() OVER (ORDER BY A.IdEmpresa), 0) AS IdRow, A.*
FROM            (SELECT        dbo.vwcp_orden_giro_total_saldo.IdEmpresa, dbo.vwcp_orden_giro_total_saldo.IdCbteCble_Ogiro, dbo.vwcp_orden_giro_total_saldo.IdTipoCbte_Ogiro, dbo.vwcp_orden_giro_total_saldo.IdOrden_giro_Tipo, 
                                                    dbo.vwcp_orden_giro_total_saldo.co_factura AS Documento, dbo.cp_TipoDocumento.Descripcion AS nom_tipo_doc, dbo.cp_TipoDocumento.Codigo AS cod_tipo_doc, 
                                                    dbo.vwcp_orden_giro_total_saldo.co_FechaFactura co_fechaOg, dbo.vwcp_orden_giro_total_saldo.co_FechaFactura_vct, OG.co_plazo, dbo.cp_proveedor.IdProveedor, 
                                                    tb_persona.pe_nombreCompleto AS nom_proveedor, dbo.vwcp_orden_giro_total_saldo.co_total AS total, dbo.vwcp_orden_giro_total_saldo.co_total * - 1 AS Valor, 0 AS Valor_debe, 
                                                    dbo.vwcp_orden_giro_total_saldo.co_total AS Valor_Haber, ROUND(dbo.vwcp_orden_giro_total_saldo.SaldoOG, 2) AS Saldo, dbo.vwcp_orden_giro_total_saldo.co_observacion AS Observacion, 
                                                    dbo.tb_persona.pe_cedulaRuc AS Ruc_Proveedor, '' representante_legal, 'CBTE_CXP' AS Tipo_cbte, NULL AS IdEmpresa_pago, NULL AS IdTipoCbte_pago, NULL AS IdCbteCble_pago, NULL 
                                                    AS cb_Observacion_pago, NULL AS tc_TipoCbte_pago, NULL AS cb_Cheque_pago, dbo.cp_proveedor_clase.IdClaseProveedor, dbo.cp_proveedor_clase.descripcion_clas_prove, 1 AS NUM_QUERRY, 
                                                    CASE WHEN conci.IdEmpresa_OGiro IS NULL THEN cast(0 AS bit) ELSE cast(1 AS bit) END AS en_conciliacion
                          FROM            dbo.vwcp_orden_giro_total_saldo INNER JOIN
                                                    dbo.cp_TipoDocumento ON dbo.vwcp_orden_giro_total_saldo.IdOrden_giro_Tipo = dbo.cp_TipoDocumento.CodTipoDocumento INNER JOIN
                                                    dbo.cp_proveedor ON dbo.vwcp_orden_giro_total_saldo.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.vwcp_orden_giro_total_saldo.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                                                    dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                                                    dbo.cp_proveedor_clase ON dbo.cp_proveedor.IdEmpresa = dbo.cp_proveedor_clase.IdEmpresa AND dbo.cp_proveedor.IdClaseProveedor = dbo.cp_proveedor_clase.IdClaseProveedor INNER JOIN
                                                    cp_orden_giro OG ON OG.IdEmpresa = vwcp_orden_giro_total_saldo.IdEmpresa AND OG.IdTipoCbte_Ogiro = vwcp_orden_giro_total_saldo.IdTipoCbte_Ogiro AND 
                                                    OG.IdCbteCble_Ogiro = vwcp_orden_giro_total_saldo.IdCbteCble_Ogiro LEFT JOIN
                                                        (SELECT        IdEmpresa_OGiro, IdTipoCbte_Ogiro, IdCbteCble_Ogiro
                                                          FROM            cp_conciliacion_Caja_det
                                                          GROUP BY IdEmpresa_OGiro, IdTipoCbte_Ogiro, IdCbteCble_Ogiro) conci ON conci.IdEmpresa_OGiro = og.IdEmpresa AND conci.IdTipoCbte_Ogiro = og.IdTipoCbte_Ogiro AND 
                                                    conci.IdCbteCble_Ogiro = og.IdCbteCble_Ogiro
                          WHERE        OG.Estado = 'A'
                          /*where cp_proveedor.IdProveedor = 317*/ UNION ALL
                          SELECT        dbo.vwcp_nota_DebCre_total_saldo.IdEmpresa, dbo.vwcp_nota_DebCre_total_saldo.IdCbteCble_Nota, dbo.vwcp_nota_DebCre_total_saldo.IdTipoCbte_Nota, '05' AS Expr1, nd.cod_nota AS Documento, 
                                                   dbo.cp_TipoDocumento.Descripcion AS nom_tipo_doc, dbo.cp_TipoDocumento.Codigo AS cod_tipo_doc, nd.cn_fecha, nd.cn_Fecha_vcto, DATEDIFF(DAY, nd.cn_fecha, nd.cn_Fecha_vcto) AS plazo, 
                                                   dbo.cp_proveedor.IdProveedor, tb_persona.pe_nombreCompleto AS nom_proveedor, dbo.vwcp_nota_DebCre_total_saldo.cn_total AS Valor_a_pagar, dbo.vwcp_nota_DebCre_total_saldo.cn_total * - 1, 0 AS Valor_debe, 
                                                   dbo.vwcp_nota_DebCre_total_saldo.cn_total AS Valor_Haber, ROUND(dbo.vwcp_nota_DebCre_total_saldo.SaldoOG, 2) AS Saldo, dbo.vwcp_nota_DebCre_total_saldo.cn_observacion AS Observacion, 
                                                   dbo.tb_persona.pe_cedulaRuc AS Ruc_Proveedor, '' representante_legal, 'CBTE_CXP' AS Tipo_cbte, NULL AS IdEmpresa_pago, NULL AS IdTipoCbte_pago, NULL AS IdCbteCble_pago, NULL 
                                                   AS cb_Observacion_pago, NULL AS tc_TipoCbte_pago, NULL AS cb_Cheque_pago, dbo.cp_proveedor_clase.IdClaseProveedor, dbo.cp_proveedor_clase.descripcion_clas_prove, 2, cast(0 AS bit)
                          FROM            dbo.vwcp_nota_DebCre_total_saldo INNER JOIN
                                                   dbo.cp_TipoDocumento ON dbo.vwcp_nota_DebCre_total_saldo.DebCre = 'D' AND dbo.cp_TipoDocumento.CodTipoDocumento = '05' INNER JOIN
                                                   dbo.cp_proveedor ON dbo.vwcp_nota_DebCre_total_saldo.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.vwcp_nota_DebCre_total_saldo.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                                                   dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                                                   dbo.cp_proveedor_clase ON dbo.cp_proveedor.IdEmpresa = dbo.cp_proveedor_clase.IdEmpresa AND dbo.cp_proveedor.IdClaseProveedor = dbo.cp_proveedor_clase.IdClaseProveedor INNER JOIN
                                                   cp_nota_DebCre nd ON vwcp_nota_DebCre_total_saldo.IdEmpresa = nd.IdEmpresa AND vwcp_nota_DebCre_total_saldo.IdTipoCbte_Nota = nd.IdTipoCbte_Nota AND 
                                                   vwcp_nota_DebCre_total_saldo.IdCbteCble_Nota = nd.IdCbteCble_Nota
                          WHERE        nd.Estado = 'A' AND nd.DebCre = 'D'
                          /*where cp_proveedor.IdProveedor = 317*/ UNION ALL
                          SELECT        dbo.cp_orden_giro.IdEmpresa, dbo.cp_orden_giro.IdCbteCble_Ogiro, dbo.cp_orden_giro.IdTipoCbte_Ogiro, dbo.cp_orden_giro.IdOrden_giro_Tipo, dbo.cp_orden_giro.co_factura AS Documento, 
                                                   dbo.cp_TipoDocumento.Descripcion AS nom_tipo_doc, dbo.cp_TipoDocumento.Codigo AS cod_tipo_doc, isnull(isnull(dbo.ba_Cbte_Ban.cb_Fecha, cp_nota_DebCre.cn_fecha), ct_cbtecble.cb_Fecha), NULL, NULL, 
                                                   dbo.cp_proveedor.IdProveedor, tb_persona.pe_nombreCompleto AS nom_proveedor, dbo.cp_orden_giro.co_total, dbo.cp_orden_pago_cancelaciones.MontoAplicado, 
                                                   dbo.cp_orden_pago_cancelaciones.MontoAplicado AS Valor_debe, 0 AS Valor_Haber, NULL AS Saldo, dbo.cp_orden_giro.co_observacion AS Observacion, dbo.tb_persona.pe_cedulaRuc AS Ruc_Proveedor, 
                                                   '' representante_legal, 'CBTE_PAGO' AS Tipo_cbte, dbo.ct_cbtecble.IdEmpresa AS IdEmpresa_pago, dbo.ct_cbtecble.IdTipoCbte AS IdTipoCbte_pago, dbo.ct_cbtecble.IdCbteCble AS IdCbteCble_pago, 
                                                   dbo.ct_cbtecble.cb_Observacion AS cb_Observacion_pago, dbo.ct_cbtecble_tipo.CodTipoCbte AS tc_TipoCbte_pago, dbo.ba_Cbte_Ban.cb_Cheque AS cb_Cheque_pago, dbo.cp_proveedor_clase.IdClaseProveedor, 
                                                   dbo.cp_proveedor_clase.descripcion_clas_prove, 3 AS Expr1, CASE WHEN conci.IdEmpresa_OGiro IS NULL THEN cast(0 AS bit) ELSE cast(1 AS bit) END AS en_conciliacion
                          FROM            dbo.cp_orden_pago_cancelaciones INNER JOIN
                                                   dbo.cp_orden_pago_det ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_op = dbo.cp_orden_pago_det.IdEmpresa AND 
                                                   dbo.cp_orden_pago_cancelaciones.IdOrdenPago_op = dbo.cp_orden_pago_det.IdOrdenPago AND dbo.cp_orden_pago_cancelaciones.Secuencia_op = dbo.cp_orden_pago_det.Secuencia INNER JOIN
                                                   dbo.cp_orden_pago ON dbo.cp_orden_pago_det.IdEmpresa = dbo.cp_orden_pago.IdEmpresa AND dbo.cp_orden_pago_det.IdOrdenPago = dbo.cp_orden_pago.IdOrdenPago INNER JOIN
                                                   dbo.cp_orden_giro ON dbo.cp_orden_pago_det.IdEmpresa_cxp = dbo.cp_orden_giro.IdEmpresa AND dbo.cp_orden_pago_det.IdCbteCble_cxp = dbo.cp_orden_giro.IdCbteCble_Ogiro AND 
                                                   dbo.cp_orden_pago_det.IdTipoCbte_cxp = dbo.cp_orden_giro.IdTipoCbte_Ogiro INNER JOIN
                                                   dbo.ct_cbtecble ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_pago = dbo.ct_cbtecble.IdEmpresa AND dbo.cp_orden_pago_cancelaciones.IdTipoCbte_pago = dbo.ct_cbtecble.IdTipoCbte AND 
                                                   dbo.cp_orden_pago_cancelaciones.IdCbteCble_pago = dbo.ct_cbtecble.IdCbteCble INNER JOIN
                                                   dbo.ct_cbtecble_tipo ON dbo.ct_cbtecble.IdTipoCbte = dbo.ct_cbtecble_tipo.IdTipoCbte AND dbo.ct_cbtecble.IdEmpresa = dbo.ct_cbtecble_tipo.IdEmpresa INNER JOIN
                                                   dbo.cp_TipoDocumento ON dbo.cp_orden_giro.IdOrden_giro_Tipo = dbo.cp_TipoDocumento.CodTipoDocumento INNER JOIN
                                                   dbo.cp_proveedor ON dbo.cp_orden_giro.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.cp_orden_giro.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                                                   dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                                                   dbo.cp_proveedor_clase ON dbo.cp_proveedor.IdEmpresa = dbo.cp_proveedor_clase.IdEmpresa AND dbo.cp_proveedor.IdClaseProveedor = dbo.cp_proveedor_clase.IdClaseProveedor LEFT OUTER JOIN
                                                   dbo.cp_nota_DebCre ON dbo.ct_cbtecble.IdEmpresa = dbo.cp_nota_DebCre.IdEmpresa AND dbo.ct_cbtecble.IdTipoCbte = dbo.cp_nota_DebCre.IdTipoCbte_Nota AND 
                                                   dbo.ct_cbtecble.IdCbteCble = dbo.cp_nota_DebCre.IdCbteCble_Nota LEFT OUTER JOIN
                                                   dbo.ba_Cbte_Ban ON dbo.ct_cbtecble.IdEmpresa = dbo.ba_Cbte_Ban.IdEmpresa AND dbo.ct_cbtecble.IdTipoCbte = dbo.ba_Cbte_Ban.IdTipocbte AND dbo.ct_cbtecble.IdCbteCble = dbo.ba_Cbte_Ban.IdCbteCble LEFT 
                                                   JOIN
                                                       (SELECT        IdEmpresa_OGiro, IdTipoCbte_Ogiro, IdCbteCble_Ogiro
                                                         FROM            cp_conciliacion_Caja_det
                                                         GROUP BY IdEmpresa_OGiro, IdTipoCbte_Ogiro, IdCbteCble_Ogiro) conci ON conci.IdEmpresa_OGiro = cp_orden_giro.IdEmpresa AND conci.IdTipoCbte_Ogiro = cp_orden_giro.IdTipoCbte_Ogiro AND 
                                                   conci.IdCbteCble_Ogiro = cp_orden_giro.IdCbteCble_Ogiro
                          /*where cp_proveedor.IdProveedor = 317*/ UNION ALL
                          SELECT        dbo.cp_nota_DebCre.IdEmpresa, dbo.cp_nota_DebCre.IdCbteCble_Nota, dbo.cp_nota_DebCre.IdTipoCbte_Nota, '05' AS Expr1, cp_nota_DebCre.cod_nota AS Documento, 
                                                   dbo.cp_TipoDocumento.Descripcion AS nom_tipo_doc, dbo.cp_TipoDocumento.Codigo AS cod_tipo_doc, isnull(ba_Cbte_Ban.cb_Fecha, ct_cbtecble.cb_Fecha), NULL, NULL, dbo.cp_proveedor.IdProveedor, 
                                                   tb_persona.pe_nombreCompleto AS nom_proveedor, dbo.cp_nota_DebCre.cn_total AS Valor_a_pagar, dbo.cp_orden_pago_cancelaciones.MontoAplicado, 
                                                   dbo.cp_orden_pago_cancelaciones.MontoAplicado AS Valor_debe, 0 AS Valor_Haber, NULL AS Saldo, dbo.cp_nota_DebCre.cn_observacion AS Observacion, dbo.tb_persona.pe_cedulaRuc AS Ruc_Proveedor, 
                                                   '' representante_legal, 'CBTE_PAGO' AS Tipo_cbte, dbo.ct_cbtecble.IdEmpresa AS IdEmpresa_pago, dbo.ct_cbtecble.IdTipoCbte AS IdTipoCbte_pago, dbo.ct_cbtecble.IdCbteCble AS IdCbteCble_pago, 
                                                   dbo.ct_cbtecble.cb_Observacion AS cb_Observacion_pago, dbo.ct_cbtecble_tipo.CodTipoCbte AS tc_TipoCbte_pago, dbo.ba_Cbte_Ban.cb_Cheque AS cb_Cheque_pago, dbo.cp_proveedor_clase.IdClaseProveedor, 
                                                   dbo.cp_proveedor_clase.descripcion_clas_prove, 4, cast(0 AS bit)
                          FROM            dbo.cp_orden_pago_cancelaciones INNER JOIN
                                                   dbo.cp_orden_pago_det ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_op = dbo.cp_orden_pago_det.IdEmpresa AND 
                                                   dbo.cp_orden_pago_cancelaciones.IdOrdenPago_op = dbo.cp_orden_pago_det.IdOrdenPago AND dbo.cp_orden_pago_cancelaciones.Secuencia_op = dbo.cp_orden_pago_det.Secuencia INNER JOIN
                                                   dbo.cp_orden_pago ON dbo.cp_orden_pago_det.IdEmpresa = dbo.cp_orden_pago.IdEmpresa AND dbo.cp_orden_pago_det.IdOrdenPago = dbo.cp_orden_pago.IdOrdenPago INNER JOIN
                                                   dbo.cp_nota_DebCre ON dbo.cp_orden_pago_det.IdEmpresa_cxp = dbo.cp_nota_DebCre.IdEmpresa AND dbo.cp_orden_pago_det.IdCbteCble_cxp = dbo.cp_nota_DebCre.IdCbteCble_Nota AND 
                                                   dbo.cp_orden_pago_det.IdTipoCbte_cxp = dbo.cp_nota_DebCre.IdTipoCbte_Nota INNER JOIN
                                                   dbo.ct_cbtecble ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_pago = dbo.ct_cbtecble.IdEmpresa AND dbo.cp_orden_pago_cancelaciones.IdTipoCbte_pago = dbo.ct_cbtecble.IdTipoCbte AND 
                                                   dbo.cp_orden_pago_cancelaciones.IdCbteCble_pago = dbo.ct_cbtecble.IdCbteCble INNER JOIN
                                                   dbo.ct_cbtecble_tipo ON dbo.ct_cbtecble.IdTipoCbte = dbo.ct_cbtecble_tipo.IdTipoCbte AND dbo.ct_cbtecble.IdEmpresa = dbo.ct_cbtecble_tipo.IdEmpresa INNER JOIN
                                                   dbo.cp_TipoDocumento ON dbo.cp_TipoDocumento.CodTipoDocumento = '05' INNER JOIN
                                                   dbo.cp_proveedor ON dbo.cp_nota_DebCre.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.cp_nota_DebCre.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                                                   dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                                                   dbo.cp_proveedor_clase ON dbo.cp_proveedor.IdEmpresa = dbo.cp_proveedor_clase.IdEmpresa AND dbo.cp_proveedor.IdClaseProveedor = dbo.cp_proveedor_clase.IdClaseProveedor LEFT OUTER JOIN
                                                   dbo.ba_Cbte_Ban ON dbo.ct_cbtecble.IdEmpresa = dbo.ba_Cbte_Ban.IdEmpresa AND dbo.ct_cbtecble.IdTipoCbte = dbo.ba_Cbte_Ban.IdTipocbte AND dbo.ct_cbtecble.IdCbteCble = dbo.ba_Cbte_Ban.IdCbteCble
                          UNION ALL
                          SELECT        dbo.cp_orden_pago_det.IdEmpresa_cxp, cp_orden_pago_det.IdCbteCble_cxp AS IdCbteCble_Ogiro, cp_orden_pago_det.IdTipoCbte_cxp AS IdTipoCbte_Ogiro, NULL AS IdOrden_giro_Tipo, 
                                                   CAST(dbo.vwcp_orden_pago.IdOrdenPago AS VARCHAR(20)) AS Documento, 'Orden de pago' AS nom_tipo_doc, 'OP' AS cod_tipo_doc, dbo.vwcp_orden_pago.Fecha, vwcp_orden_pago.Fecha, 0, 
                                                   dbo.cp_proveedor.IdProveedor, tb_persona.pe_nombreCompleto AS nom_proveedor, dbo.cp_orden_pago_det.Valor_a_pagar, dbo.cp_orden_pago_det.Valor_a_pagar * - 1, 0 AS Valor_debe, 
                                                   dbo.cp_orden_pago_det.Valor_a_pagar AS Valor_Haber, dbo.vwcp_orden_pago.Saldo, dbo.vwcp_orden_pago.Observacion, dbo.tb_persona.pe_cedulaRuc AS Ruc_Proveedor, '' representante_legal, 
                                                   'CBTE_CXP' AS Tipo_cbte, NULL AS IdEmpresa_pago, NULL AS IdTipoCbte_pago, NULL AS IdCbteCble_pago, NULL AS cb_Observacion_pago, NULL AS tc_TipoCbte_pago, NULL AS cb_Cheque_pago, 
                                                   dbo.cp_proveedor_clase.IdClaseProveedor, dbo.cp_proveedor_clase.descripcion_clas_prove, 5, cast(0 AS bit)
                          FROM            dbo.vwcp_orden_pago INNER JOIN
                                                   dbo.cp_orden_pago_det ON dbo.vwcp_orden_pago.IdEmpresa = dbo.cp_orden_pago_det.IdEmpresa AND dbo.vwcp_orden_pago.IdOrdenPago = dbo.cp_orden_pago_det.IdOrdenPago INNER JOIN
                                                   dbo.cp_proveedor ON dbo.vwcp_orden_pago.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.vwcp_orden_pago.IdEntidad = dbo.cp_proveedor.IdProveedor INNER JOIN
                                                   dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                                                   dbo.cp_proveedor_clase ON dbo.cp_proveedor.IdEmpresa = dbo.cp_proveedor_clase.IdEmpresa AND dbo.cp_proveedor.IdClaseProveedor = dbo.cp_proveedor_clase.IdClaseProveedor
                          WHERE        (NOT EXISTS
                                                       (SELECT        IdEmpresa
                                                         FROM            dbo.cp_orden_giro AS og
                                                         WHERE        (IdEmpresa = dbo.cp_orden_pago_det.IdEmpresa_cxp) AND (IdTipoCbte_Ogiro = dbo.cp_orden_pago_det.IdTipoCbte_cxp) AND (IdCbteCble_Ogiro = dbo.cp_orden_pago_det.IdCbteCble_cxp))) AND 
                                                   (NOT EXISTS
                                                       (SELECT        IdEmpresa
                                                         FROM            dbo.cp_nota_DebCre AS nd
                                                         WHERE        (IdEmpresa = dbo.cp_orden_pago_det.IdEmpresa_cxp) AND (IdTipoCbte_Nota = dbo.cp_orden_pago_det.IdTipoCbte_cxp) AND (IdCbteCble_Nota = dbo.cp_orden_pago_det.IdCbteCble_cxp))) AND 
                                                   (dbo.vwcp_orden_pago.Estado = 'A') AND (dbo.vwcp_orden_pago.IdTipo_Persona = 'PROVEE')
                          /*and cp_proveedor.IdProveedor = 317  */ UNION ALL
                          SELECT        dbo.cp_orden_pago_det.IdEmpresa_cxp, cp_orden_pago_det.IdCbteCble_cxp AS IdCbteCble_Ogiro, cp_orden_pago_det.IdTipoCbte_cxp AS IdTipoCbte_Ogiro, NULL AS IdOrden_giro_Tipo, 
                                                   CAST(dbo.cp_orden_pago.IdOrdenPago AS VARCHAR(20)) AS Documento, 'Orden de pago' AS nom_tipo_doc, 'OP' AS cod_tipo_doc, dbo.ba_Cbte_Ban.cb_Fecha, NULL, NULL, dbo.cp_proveedor.IdProveedor, 
                                                   tb_persona.pe_nombreCompleto AS nom_proveedor, dbo.cp_orden_pago_det.Valor_a_pagar, dbo.cp_orden_pago_cancelaciones.MontoAplicado, dbo.cp_orden_pago_cancelaciones.MontoAplicado AS Valor_debe, 
                                                   0 AS Valor_Haber, NULL AS IdEmpresa_pago, dbo.cp_orden_pago.Observacion, dbo.tb_persona.pe_cedulaRuc AS Ruc_Proveedor, '' representante_legal, 'CBTE_PAGO' AS Tipo_cbte, 
                                                   dbo.ct_cbtecble.IdEmpresa AS Expr1, dbo.ct_cbtecble.IdTipoCbte AS IdTipoCbte_pago, dbo.ct_cbtecble.IdCbteCble AS IdCbteCble_pago, dbo.ct_cbtecble.cb_Observacion AS cb_Observacion_pago, 
                                                   dbo.ct_cbtecble_tipo.CodTipoCbte AS tc_TipoCbte_pago, dbo.ba_Cbte_Ban.cb_Cheque AS cb_Cheque_pago, dbo.cp_proveedor_clase.IdClaseProveedor, dbo.cp_proveedor_clase.descripcion_clas_prove, 6, 
                                                   cast(0 AS bit)
                          FROM            dbo.cp_orden_pago_cancelaciones INNER JOIN
                                                   dbo.cp_orden_pago_det ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_op = dbo.cp_orden_pago_det.IdEmpresa AND 
                                                   dbo.cp_orden_pago_cancelaciones.IdOrdenPago_op = dbo.cp_orden_pago_det.IdOrdenPago AND dbo.cp_orden_pago_cancelaciones.Secuencia_op = dbo.cp_orden_pago_det.Secuencia INNER JOIN
                                                   dbo.cp_orden_pago ON dbo.cp_orden_pago_det.IdEmpresa = dbo.cp_orden_pago.IdEmpresa AND dbo.cp_orden_pago_det.IdOrdenPago = dbo.cp_orden_pago.IdOrdenPago INNER JOIN
                                                   dbo.ct_cbtecble ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_pago = dbo.ct_cbtecble.IdEmpresa AND dbo.cp_orden_pago_cancelaciones.IdTipoCbte_pago = dbo.ct_cbtecble.IdTipoCbte AND 
                                                   dbo.cp_orden_pago_cancelaciones.IdCbteCble_pago = dbo.ct_cbtecble.IdCbteCble INNER JOIN
                                                   dbo.ct_cbtecble_tipo ON dbo.ct_cbtecble.IdTipoCbte = dbo.ct_cbtecble_tipo.IdTipoCbte AND dbo.ct_cbtecble.IdEmpresa = dbo.ct_cbtecble_tipo.IdEmpresa INNER JOIN
                                                   dbo.cp_proveedor ON dbo.cp_orden_pago.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.cp_orden_pago.IdEntidad = dbo.cp_proveedor.IdProveedor INNER JOIN
                                                   dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                                                   dbo.cp_proveedor_clase ON dbo.cp_proveedor.IdEmpresa = dbo.cp_proveedor_clase.IdEmpresa AND dbo.cp_proveedor.IdClaseProveedor = dbo.cp_proveedor_clase.IdClaseProveedor LEFT OUTER JOIN
                                                   dbo.ba_Cbte_Ban ON dbo.ct_cbtecble.IdEmpresa = dbo.ba_Cbte_Ban.IdEmpresa AND dbo.ct_cbtecble.IdTipoCbte = dbo.ba_Cbte_Ban.IdTipocbte AND 
                                                   dbo.ct_cbtecble.IdCbteCble = dbo.ba_Cbte_Ban.IdCbteCble
                          WHERE        (NOT EXISTS
                                                       (SELECT        IdEmpresa
                                                         FROM            dbo.cp_orden_giro AS og
                                                         WHERE        (IdEmpresa = dbo.cp_orden_pago_det.IdEmpresa_cxp) AND (IdTipoCbte_Ogiro = dbo.cp_orden_pago_det.IdTipoCbte_cxp) AND (IdCbteCble_Ogiro = dbo.cp_orden_pago_det.IdCbteCble_cxp))) AND 
                                                   (NOT EXISTS
                                                       (SELECT        IdEmpresa
                                                         FROM            dbo.cp_nota_DebCre AS nd
                                                         WHERE        (IdEmpresa = dbo.cp_orden_pago_det.IdEmpresa_cxp) AND (IdTipoCbte_Nota = dbo.cp_orden_pago_det.IdTipoCbte_cxp) AND (IdCbteCble_Nota = dbo.cp_orden_pago_det.IdCbteCble_cxp))) AND 
                                                   (dbo.cp_orden_pago.IdTipo_Persona = 'PROVEE')
                          UNION ALL
                          SELECT        dbo.cp_orden_pago_det.IdEmpresa_cxp, cp_orden_pago_det.IdCbteCble_cxp AS IdCbteCble_Ogiro, cp_orden_pago_det.IdTipoCbte_cxp, NULL AS IdOrden_giro_Tipo, 
                                                   CAST(dbo.cp_orden_pago.IdOrdenPago AS VARCHAR(20)) AS Documento, 'Orden de pago' AS nom_tipo_doc, 'OP' AS cod_tipo_doc, dbo.cp_nota_DebCre.cn_fecha, NULL, NULL, dbo.cp_proveedor.IdProveedor, 
                                                   tb_persona.pe_nombreCompleto AS nom_proveedor, dbo.cp_orden_pago_det.Valor_a_pagar AS Valor_a_pagar, dbo.cp_orden_pago_cancelaciones.MontoAplicado, 
                                                   dbo.cp_orden_pago_cancelaciones.MontoAplicado AS Valor_debe, 0 AS Valor_Haber, NULL AS IdEmpresa_pago, dbo.cp_orden_pago.Observacion AS Observacion, dbo.tb_persona.pe_cedulaRuc AS Ruc_Proveedor, 
                                                   '' representante_legal, 'CBTE_PAGO' AS Tipo_cbte, dbo.ct_cbtecble.IdEmpresa AS Expr1, dbo.ct_cbtecble.IdTipoCbte AS IdTipoCbte_pago, dbo.ct_cbtecble.IdCbteCble AS IdCbteCble_pago, 
                                                   dbo.ct_cbtecble.cb_Observacion AS cb_Observacion_pago, dbo.ct_cbtecble_tipo.CodTipoCbte AS tc_TipoCbte_pago, NULL AS cb_Cheque_pago, dbo.cp_proveedor_clase.IdClaseProveedor, 
                                                   dbo.cp_proveedor_clase.descripcion_clas_prove, 7, cast(0 AS bit)
                          FROM            dbo.cp_orden_pago_cancelaciones INNER JOIN
                                                   dbo.cp_orden_pago_det ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_op = dbo.cp_orden_pago_det.IdEmpresa AND 
                                                   dbo.cp_orden_pago_cancelaciones.IdOrdenPago_op = dbo.cp_orden_pago_det.IdOrdenPago AND dbo.cp_orden_pago_cancelaciones.Secuencia_op = dbo.cp_orden_pago_det.Secuencia INNER JOIN
                                                   dbo.cp_orden_pago ON dbo.cp_orden_pago_det.IdEmpresa = dbo.cp_orden_pago.IdEmpresa AND dbo.cp_orden_pago_det.IdOrdenPago = dbo.cp_orden_pago.IdOrdenPago INNER JOIN
                                                   dbo.ct_cbtecble ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_pago = dbo.ct_cbtecble.IdEmpresa AND dbo.cp_orden_pago_cancelaciones.IdTipoCbte_pago = dbo.ct_cbtecble.IdTipoCbte AND 
                                                   dbo.cp_orden_pago_cancelaciones.IdCbteCble_pago = dbo.ct_cbtecble.IdCbteCble INNER JOIN
                                                   dbo.ct_cbtecble_tipo ON dbo.ct_cbtecble.IdTipoCbte = dbo.ct_cbtecble_tipo.IdTipoCbte AND dbo.ct_cbtecble.IdEmpresa = dbo.ct_cbtecble_tipo.IdEmpresa INNER JOIN
                                                   dbo.cp_proveedor ON dbo.cp_orden_pago.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.cp_orden_pago.IdEntidad = dbo.cp_proveedor.IdProveedor INNER JOIN
                                                   dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                                                   dbo.cp_proveedor_clase ON dbo.cp_proveedor.IdEmpresa = dbo.cp_proveedor_clase.IdEmpresa AND dbo.cp_proveedor.IdClaseProveedor = dbo.cp_proveedor_clase.IdClaseProveedor INNER JOIN
                                                   dbo.cp_nota_DebCre ON dbo.ct_cbtecble.IdEmpresa = dbo.cp_nota_DebCre.IdEmpresa AND dbo.ct_cbtecble.IdTipoCbte = dbo.cp_nota_DebCre.IdTipoCbte_Nota AND 
                                                   dbo.ct_cbtecble.IdCbteCble = dbo.cp_nota_DebCre.IdCbteCble_Nota
                          WHERE        (NOT EXISTS
                                                       (SELECT        IdEmpresa
                                                         FROM            dbo.cp_orden_giro AS og
                                                         WHERE        (IdEmpresa = dbo.cp_orden_pago_det.IdEmpresa_cxp) AND (IdTipoCbte_Ogiro = dbo.cp_orden_pago_det.IdTipoCbte_cxp) AND (IdCbteCble_Ogiro = dbo.cp_orden_pago_det.IdCbteCble_cxp))) AND 
                                                   (NOT EXISTS
                                                       (SELECT        IdEmpresa
                                                         FROM            dbo.cp_nota_DebCre AS nd
                                                         WHERE        (IdEmpresa = dbo.cp_orden_pago_det.IdEmpresa_cxp) AND (IdTipoCbte_Nota = dbo.cp_orden_pago_det.IdTipoCbte_cxp) AND (IdCbteCble_Nota = dbo.cp_orden_pago_det.IdCbteCble_cxp))) AND 
                                                   (dbo.cp_orden_pago.IdTipo_Persona = 'PROVEE')
                          UNION ALL
                          SELECT        dbo.cp_orden_giro.IdEmpresa, dbo.cp_orden_giro.IdCbteCble_Ogiro, dbo.cp_orden_giro.IdTipoCbte_Ogiro, dbo.cp_orden_giro.IdOrden_giro_Tipo, dbo.cp_orden_giro.co_factura AS Documento, 
                                                   'Factura' AS nom_tipo_doc, 'FACT' AS cod_tipo_doc, dbo.vwcp_Retencion_valor_total_x_cbte_cxp.fecha, NULL, NULL, dbo.cp_orden_giro.IdProveedor, tb_persona.pe_nombreCompleto, 
                                                   dbo.cp_orden_giro.co_total AS valor_a_pagar, dbo.vwcp_Retencion_valor_total_x_cbte_cxp.Total_Retencion, dbo.vwcp_Retencion_valor_total_x_cbte_cxp.Total_Retencion AS Debe, 0 AS Haber, 0 AS Saldo, 
                                                   dbo.cp_orden_giro.co_observacion, dbo.tb_persona.pe_cedulaRuc, '' representante_legal, 'CBTE_PAGO' AS tipo_cbte_pago, dbo.cp_retencion_x_ct_cbtecble.ct_IdEmpresa, 
                                                   dbo.cp_retencion_x_ct_cbtecble.ct_IdTipoCbte, dbo.cp_retencion_x_ct_cbtecble.ct_IdCbteCble, dbo.cp_retencion.observacion, cp_retencion.CodDocumentoTipo, cp_retencion.NumRetencion, 
                                                   cp_proveedor_clase.IdClaseProveedor, cp_proveedor_clase.descripcion_clas_prove, 8, CASE WHEN conci.IdEmpresa_OGiro IS NULL THEN cast(0 AS bit) ELSE cast(1 AS bit) END AS en_conciliacion
                          FROM            dbo.cp_orden_giro INNER JOIN
                                                   dbo.vwcp_Retencion_valor_total_x_cbte_cxp ON dbo.cp_orden_giro.IdEmpresa = dbo.vwcp_Retencion_valor_total_x_cbte_cxp.IdEmpresa_Ogiro AND 
                                                   dbo.cp_orden_giro.IdTipoCbte_Ogiro = dbo.vwcp_Retencion_valor_total_x_cbte_cxp.IdTipoCbte_Ogiro AND 
                                                   dbo.cp_orden_giro.IdCbteCble_Ogiro = dbo.vwcp_Retencion_valor_total_x_cbte_cxp.IdCbteCble_Ogiro INNER JOIN
                                                   dbo.cp_proveedor ON dbo.cp_orden_giro.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.cp_orden_giro.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                                                   dbo.cp_proveedor_clase ON dbo.cp_proveedor.IdEmpresa = dbo.cp_proveedor_clase.IdEmpresa AND dbo.cp_proveedor.IdClaseProveedor = dbo.cp_proveedor_clase.IdClaseProveedor INNER JOIN
                                                   dbo.cp_retencion_x_ct_cbtecble ON dbo.vwcp_Retencion_valor_total_x_cbte_cxp.IdEmpresa = dbo.cp_retencion_x_ct_cbtecble.rt_IdEmpresa AND 
                                                   dbo.vwcp_Retencion_valor_total_x_cbte_cxp.IdRetencion = dbo.cp_retencion_x_ct_cbtecble.rt_IdRetencion INNER JOIN
                                                   dbo.ct_cbtecble_tipo ON dbo.cp_retencion_x_ct_cbtecble.ct_IdEmpresa = dbo.ct_cbtecble_tipo.IdEmpresa AND dbo.cp_retencion_x_ct_cbtecble.ct_IdTipoCbte = dbo.ct_cbtecble_tipo.IdTipoCbte INNER JOIN
                                                   dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                                                   dbo.cp_retencion ON dbo.vwcp_Retencion_valor_total_x_cbte_cxp.IdEmpresa = dbo.cp_retencion.IdEmpresa AND dbo.vwcp_Retencion_valor_total_x_cbte_cxp.IdRetencion = dbo.cp_retencion.IdRetencion LEFT JOIN
                                                       (SELECT        IdEmpresa_OGiro, IdTipoCbte_Ogiro, IdCbteCble_Ogiro
                                                         FROM            cp_conciliacion_Caja_det
                                                         GROUP BY IdEmpresa_OGiro, IdTipoCbte_Ogiro, IdCbteCble_Ogiro) conci ON conci.IdEmpresa_OGiro = cp_orden_giro.IdEmpresa AND conci.IdTipoCbte_Ogiro = cp_orden_giro.IdTipoCbte_Ogiro AND 
                                                   conci.IdCbteCble_Ogiro = cp_orden_giro.IdCbteCble_Ogiro
                          WHERE        dbo.cp_retencion.Estado = 'A'/*
                  UNION
                  SELECT dbo.cp_orden_giro.IdEmpresa, dbo.cp_orden_giro.IdCbteCble_Ogiro, dbo.cp_orden_giro.IdTipoCbte_Ogiro, dbo.cp_orden_giro.IdOrden_giro_Tipo, dbo.cp_orden_giro.co_factura, dbo.cp_TipoDocumento.Descripcion, 
                                    dbo.cp_TipoDocumento.Codigo, dbo.ct_cbtecble.cb_Fecha, NULL AS Expr1, dbo.cp_orden_giro.co_plazo, dbo.cp_orden_giro.IdProveedor, LTRIM(RTRIM(dbo.tb_persona.pe_nombreCompleto)), dbo.cp_orden_giro.co_total, 
                                    dbo.cp_orden_pago_cancelaciones.MontoAplicado, dbo.cp_orden_pago_cancelaciones.MontoAplicado AS debe, 0 AS haber, 0 AS saldo, dbo.cp_orden_giro.co_observacion, dbo.tb_persona.pe_cedulaRuc, 
                                    dbo.cp_proveedor.representante_legal, 'CBTE_PAGO' AS Tipo_cbte, dbo.cp_orden_pago_cancelaciones.IdEmpresa_pago, dbo.cp_orden_pago_cancelaciones.IdTipoCbte_pago, 
                                    dbo.cp_orden_pago_cancelaciones.IdCbteCble_pago, dbo.cp_conciliacion.Observacion, dbo.ct_cbtecble_tipo.CodTipoCbte, NULL AS Expr2, dbo.cp_proveedor.IdClaseProveedor, cp_proveedor_clase.descripcion_clas_prove, 
                                    9 AS NUM_QUERRY, CASE WHEN conci.IdEmpresa_OGiro IS NULL THEN cast(0 AS bit) ELSE cast(1 AS bit) END AS en_conciliacion
                  FROM     dbo.cp_orden_giro INNER JOIN
                                    dbo.cp_orden_pago_cancelaciones ON dbo.cp_orden_giro.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_cxp AND dbo.cp_orden_giro.IdCbteCble_Ogiro = dbo.cp_orden_pago_cancelaciones.IdCbteCble_cxp AND 
                                    dbo.cp_orden_giro.IdTipoCbte_Ogiro = dbo.cp_orden_pago_cancelaciones.IdTipoCbte_cxp INNER JOIN
                                    dbo.cp_conciliacion ON dbo.cp_orden_pago_cancelaciones.IdEmpresa = dbo.cp_conciliacion.IdEmpresa AND dbo.cp_orden_pago_cancelaciones.Idcancelacion = dbo.cp_conciliacion.IdCancelacion INNER JOIN
                                    dbo.cp_TipoDocumento ON dbo.cp_orden_giro.IdOrden_giro_Tipo = dbo.cp_TipoDocumento.CodTipoDocumento INNER JOIN
                                    dbo.cp_proveedor ON dbo.cp_orden_giro.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.cp_orden_giro.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                                    dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                                    dbo.ct_cbtecble ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_pago = dbo.ct_cbtecble.IdEmpresa AND dbo.cp_orden_pago_cancelaciones.IdTipoCbte_pago = dbo.ct_cbtecble.IdTipoCbte AND 
                                    dbo.cp_orden_pago_cancelaciones.IdCbteCble_pago = dbo.ct_cbtecble.IdCbteCble INNER JOIN
                                    dbo.ct_cbtecble_tipo ON dbo.ct_cbtecble.IdEmpresa = dbo.ct_cbtecble_tipo.IdEmpresa AND dbo.ct_cbtecble.IdTipoCbte = dbo.ct_cbtecble_tipo.IdTipoCbte INNER JOIN
                                    dbo.cp_proveedor_clase ON dbo.cp_proveedor.IdEmpresa = dbo.cp_proveedor_clase.IdEmpresa AND dbo.cp_proveedor.IdClaseProveedor = dbo.cp_proveedor_clase.IdClaseProveedor LEFT JOIN
                                        (SELECT IdEmpresa_OGiro, IdTipoCbte_Ogiro, IdCbteCble_Ogiro
                                         FROM      cp_conciliacion_Caja_det
                                         GROUP BY IdEmpresa_OGiro, IdTipoCbte_Ogiro, IdCbteCble_Ogiro) conci ON conci.IdEmpresa_OGiro = cp_orden_giro.IdEmpresa AND conci.IdTipoCbte_Ogiro = cp_orden_giro.IdTipoCbte_Ogiro AND 
                                    conci.IdCbteCble_Ogiro = cp_orden_giro.IdCbteCble_Ogiro
                  WHERE  NOT EXISTS
                                        (SELECT CRE.IdEmpresa
                                         FROM      cp_nota_DebCre CRE
                                         WHERE   cp_orden_pago_cancelaciones.IdEmpresa_pago = CRE.IdEmpresa AND cp_orden_pago_cancelaciones.IdTipoCbte_pago = CRE.IdTipoCbte_Nota AND 
                                                           cp_orden_pago_cancelaciones.IdCbteCble_pago = CRE.IdCbteCble_Nota) AND cp_conciliacion.Estado = 'A'*/ )
                          A
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwCXP_Rpt001';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[16] 4[4] 2[44] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -288
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwCXP_Rpt001';

