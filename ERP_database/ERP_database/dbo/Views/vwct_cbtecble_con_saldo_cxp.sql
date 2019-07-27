CREATE VIEW [dbo].[vwct_cbtecble_con_saldo_cxp]
AS
SELECT        cbteBan.IdEmpresa, cbteBan.IdCbteCble, cbteBan.IdTipocbte, cbteBan.cb_Fecha, cbteBan.cb_Observacion, 
                         CASE tip_ban.CodTipoCbteBan WHEN 'CHEQ' THEN 'Chq.#:' + cbteBan.cb_Cheque + ' Cbte#' + CAST(cbteBan.IdCbteCble AS varchar(20)) 
                         + ' Girado a ' + cbteBan.cb_giradoA + ' / ' + Ban_cta.ba_descripcion + ' / ' + cbteBan.cb_Observacion WHEN 'DEPO' THEN 'Dep.#:' + CAST(cbteBan.IdCbteCble AS varchar(20)) 
                         + ' en  ' + Ban_cta.ba_descripcion + ' / ' + cbteBan.cb_Observacion WHEN 'NCBA' THEN 'N/C.#:' + CAST(cbteBan.IdCbteCble AS varchar(20)) 
                         + ' / ' + Ban_cta.ba_descripcion + '  / ' + cbteBan.cb_Observacion WHEN 'NDBA' THEN 'N/D.#:' + CAST(cbteBan.IdCbteCble AS varchar(20)) 
                         + ' / ' + Ban_cta.ba_descripcion + ' / ' + cbteBan.cb_Observacion ELSE '' END AS referencia, tip_cbte_cb.tc_TipoCbte, cbt_cbl.dc_Valor AS Valor_cbte, ISNULL(Pag.Total_Pagado, 0) AS Valor_cancelado_cbte, 
                         cbt_cbl.dc_Valor - ISNULL(Pag.Total_Pagado, 0) AS valor_Saldo_cbte, 'DIARIO_BAN' AS Tipo, NULL AS IdEmpresaOP, NULL AS IdOrdenPagoOP, NULL AS SecuenciaOP, NULL AS IdCtaCble, NULL 
                         AS IdCtaCble_Anticipo, cbteBan.cb_giradoA AS Beneficiario, cbteBan.IdPersona_Girado_a AS IdBeneficiario
FROM            ba_Cbte_Ban AS cbteBan INNER JOIN
                         ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo AS C INNER JOIN
                         ba_Cbte_Ban_tipo AS tip_ban ON C.CodTipoCbteBan = tip_ban.CodTipoCbteBan ON cbteBan.IdEmpresa = C.IdEmpresa AND cbteBan.IdTipocbte = C.IdTipoCbteCble INNER JOIN
                         ct_cbtecble_tipo AS tip_cbte_cb ON C.IdTipoCbteCble = tip_cbte_cb.IdTipoCbte AND C.IdEmpresa = tip_cbte_cb.IdEmpresa INNER JOIN
                         ba_Banco_Cuenta AS Ban_cta ON cbteBan.IdEmpresa = Ban_cta.IdEmpresa AND cbteBan.IdBanco = Ban_cta.IdBanco INNER JOIN
                         vwct_cbtecble_det_TotalDiario AS cbt_cbl ON cbteBan.IdEmpresa = cbt_cbl.IdEmpresa AND cbteBan.IdCbteCble = cbt_cbl.IdCbteCble AND cbteBan.IdTipocbte = cbt_cbl.IdTipoCbte LEFT OUTER JOIN
                         vwcp_orden_pago_cancelacion_Total_pagado_x_cbteble AS Pag ON cbteBan.IdEmpresa = Pag.IdEmpresa_cbtecble AND cbteBan.IdCbteCble = Pag.IdTipoCbte_cbtecble AND 
                         cbteBan.IdTipocbte = Pag.IdCbteCble_cbtecble
WHERE        (NOT EXISTS
                             (SELECT        IdEmpresa_pago, IdTipoCbte_pago, IdCbteCble_pago
                               FROM            cp_orden_pago_cancelaciones
                               WHERE        (IdEmpresa_pago = cbteBan.IdEmpresa) AND (IdTipoCbte_pago = cbteBan.IdTipocbte) AND (IdCbteCble_pago = cbteBan.IdCbteCble)))
UNION ALL
SELECT        A.IdEmpresa, A.IdCbteCble, A.IdTipoCbte, A.cb_Fecha, A.cb_Observacion, A.cb_Observacion AS referencia, D .tc_TipoCbte, C.dc_Valor AS Valor_Cbte, ISNULL(B.Total_Pagado, 0) AS Valor_Cancelado_cbte, 
                         C.dc_Valor - ISNULL(B.Total_Pagado, 0) AS Valor_saldo_cbte, 'DIARIO' AS Tipo, NULL AS Expr1, NULL AS Expr2, NULL AS Expr3, NULL AS Expr4, NULL AS Expr5, pe_nombreCompleto AS Beneficiario, 
                         og.IdProveedor
FROM            ct_cbtecble AS A INNER JOIN
                         vwct_cbtecble_det_TotalDiario AS C ON A.IdEmpresa = C.IdEmpresa AND A.IdTipoCbte = C.IdTipoCbte AND A.IdCbteCble = C.IdCbteCble INNER JOIN
                         ct_cbtecble_tipo AS D ON A.IdTipoCbte = D .IdTipoCbte AND A.IdEmpresa = D .IdEmpresa LEFT OUTER JOIN
                         vwcp_orden_pago_cancelacion_Total_pagado_x_cbteble AS B ON A.IdEmpresa = B.IdEmpresa_cbtecble AND A.IdTipoCbte = B.IdTipoCbte_cbtecble AND A.IdCbteCble = B.IdCbteCble_cbtecble LEFT OUTER JOIN
                         cp_orden_giro AS og ON og.IdEmpresa = A.IdEmpresa AND og.IdTipoCbte_Ogiro = A.IdTipoCbte AND og.IdCbteCble_Ogiro = A.IdCbteCble LEFT OUTER JOIN
                         cp_proveedor AS p ON p.IdEmpresa = og.IdEmpresa AND p.IdProveedor = og.IdProveedor inner join tb_persona as per on per.IdPersona = p.IdPersona
WHERE        (NOT EXISTS
                             (SELECT        IdEmpresa, IdCbteCble, IdTipocbte
                               FROM            (SELECT        IdEmpresa, IdCbteCble, IdTipocbte
                                                         FROM            ba_Cbte_Ban AS A
                                                         UNION
                                                         SELECT        A.IdEmpresa_cxp, A.IdCbteCble_cxp, A.IdTipoCbte_cxp
                                                         FROM            cp_orden_pago_det AS A INNER JOIN
                                                                                  cp_orden_pago AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdOrdenPago = B.IdOrdenPago
                                                         WHERE        (A.IdEmpresa_cxp IS NOT NULL) AND (A.IdCbteCble_cxp IS NOT NULL) AND (A.IdTipoCbte_cxp IS NOT NULL)
                                                         UNION
                                                         SELECT        IdEmpresa, IdCbteCble_Nota, IdTipoCbte_Nota
                                                         FROM            cp_nota_DebCre AS NC) AS Q
                               WHERE        (IdEmpresa = A.IdEmpresa) AND (IdCbteCble = A.IdCbteCble) AND (IdTipocbte = A.IdTipoCbte)))
UNION ALL
SELECT        OPD.IdEmpresa_cxp, OPD.IdCbteCble_cxp, OPD.IdTipoCbte_cxp, OP.Fecha, OP.Observacion, '' AS referencia, CBT_TP.tc_TipoCbte, OPD.Valor_a_pagar AS Valor_cbte, ISNULL(CAN.Total_Pagado, 0) 
                         AS valor_cancelado, OPD.Valor_a_pagar - ISNULL(CAN.Total_Pagado, 0) AS valor_saldo_cbte, 'ANTPROV' AS Expr3, OPD.IdEmpresa AS IdEmpresaOP, OPD.IdOrdenPago AS IdOrdenPagoOP, 
                         OPD.Secuencia AS SecuenciaOP, Per.IdCtaCble, Per.IdCtaCble_Anticipo, '[' + OP.IdTipo_Persona + ']-' + '[' + CAST(OP.IdEntidad AS varchar(20)) + ']-' + Per.Nombre AS Beneficiario, 
                         Per.IdEntidad AS IdProveedor
FROM            cp_orden_pago AS OP INNER JOIN
                         cp_orden_pago_det AS OPD ON OP.IdEmpresa = OPD.IdEmpresa AND OP.IdOrdenPago = OPD.IdOrdenPago INNER JOIN
                         vwtb_persona_beneficiario AS Per ON OP.IdEmpresa = Per.IdEmpresa AND OP.IdTipo_Persona = Per.IdTipo_Persona AND OP.IdPersona = Per.IdPersona AND OP.IdEntidad = Per.IdEntidad LEFT OUTER JOIN
                         ct_cbtecble_tipo AS CBT_TP ON OPD.IdEmpresa_cxp = CBT_TP.IdEmpresa AND OPD.IdTipoCbte_cxp = CBT_TP.IdTipoCbte LEFT OUTER JOIN
                         vwcp_orden_pago_cancelacion_Total_pagado_x_cbteble AS CAN ON OPD.IdEmpresa_cxp = CAN.IdEmpresa_cbtecble AND OPD.IdTipoCbte_cxp = CAN.IdTipoCbte_cbtecble AND 
                         OPD.IdCbteCble_cxp = CAN.IdCbteCble_cbtecble 
WHERE        (OP.IdTipo_op = 'ANTI_PROVEE') AND (OPD.IdCbteCble_cxp IS NOT NULL)
UNION ALL
SELECT        A.IdEmpresa, A.IdCbteCble_Nota, A.IdTipoCbte_Nota, A.cn_fecha, A.cn_observacion, A.cn_observacion AS referncia, D .tc_TipoCbte, a.cn_total AS Valor_Cbte, ISNULL(B.Total_Pagado, 0) AS Valor_Cancelado_cbte, 
                         round(a.cn_total - ISNULL(B.Total_Pagado, 0), 2) AS Valor_saldo_cbte, CASE WHEN A.DebCre = 'c' THEN 'NOTA-CRED' ELSE 'NOTA-DEB' END AS Tipo, NULL AS Expr1, NULL AS Expr2, NULL AS Expr3, 
                         P.IdCtaCble_CXP AS IdCtaCble, null AS IdCtaAnticipo, pe_nombreCompleto AS Beneficiario, P.IdProveedor
FROM            dbo.cp_nota_DebCre AS A INNER JOIN
                         dbo.cp_proveedor AS P ON A.IdEmpresa = P.IdEmpresa AND A.IdProveedor = P.IdProveedor INNER JOIN
                         dbo.vwct_cbtecble_det_TotalDiario AS C ON A.IdEmpresa = C.IdEmpresa AND A.IdTipoCbte_Nota = C.IdTipoCbte AND A.IdCbteCble_Nota = C.IdCbteCble INNER JOIN
                         dbo.ct_cbtecble_tipo AS D ON A.IdTipoCbte_Nota = D .IdTipoCbte AND A.IdEmpresa = D .IdEmpresa LEFT OUTER JOIN
                             (SELECT        IdEmpresa_pago AS IdEmpresa_cbtecble, IdTipoCbte_pago AS IdTipoCbte_cbtecble, IdCbteCble_pago AS IdCbteCble_cbtecble, SUM(MontoAplicado) AS Total_Pagado
                               FROM            dbo.cp_orden_pago_cancelaciones
                               GROUP BY IdEmpresa_pago, IdTipoCbte_pago, IdCbteCble_pago) AS B ON A.IdEmpresa = B.IdEmpresa_cbtecble AND A.IdTipoCbte_Nota = B.IdTipoCbte_cbtecble AND 
                         A.IdCbteCble_Nota = B.IdCbteCble_cbtecble inner join tb_persona as per on per.IdPersona = p.IdPersona
where a.Estado = 'A'