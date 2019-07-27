CREATE view [dbo].[vwcp_orden_giro_x_pagar]
as
SELECT        OG.IdEmpresa, OG.IdCbteCble_Ogiro, OG.IdTipoCbte_Ogiro, OG.IdOrden_giro_Tipo, OG.IdProveedor, OG.co_serie, OG.co_factura + '/' + CAST(OG.IdCbteCble_Ogiro AS varchar(20)) AS co_factura, OG.co_FechaFactura, 
                         OG.co_observacion, OG.IdSucursal, OG.co_fechaOg, OG.co_subtotal_iva, OG.co_subtotal_siniva, OG.co_baseImponible, OG.co_Por_iva, OG.co_valoriva, OG.co_total, isnull(OG.co_total - isnull(ret.re_valor_retencion, 0), 0) 
                         co_valorpagar, ROUND(ISNULL(OP.Total_Cancelado_OP, 0), 2) AS Total_Pagado, ROUND(ROUND(isnull(OG.co_total - isnull(ret.re_valor_retencion, 0), 0), 2) - ROUND(ISNULL(OP.Total_Cancelado_OP, 0), 2), 2) AS Saldo_OG, 
                         dbo.cp_TipoDocumento.Descripcion AS nom_tipo_Documento, tb_persona.pe_nombreCompleto AS nom_proveedor, dbo.ct_cbtecble_tipo.CodTipoCbte, dbo.ct_cbtecble_tipo.tc_TipoCbte, 
                         dbo.cp_orden_pago_tipo_x_empresa.IdTipo_op, dbo.cp_orden_pago_tipo_x_empresa.IdEstadoAprobacion, cast(isnull(a.Fecha_vcto_cuota, OG.co_FechaFactura_vct) AS datetime) co_FechaFactura_vct, null IdTipoFlujo, 
                         '' AS nom_flujo, dbo.tb_persona.IdPersona, dbo.cp_TipoDocumento.Codigo AS cod_Documento, OG.Estado, CASE WHEN conci.IdConciliacion_Caja IS NULL THEN cast(0 AS bit) ELSE cast(1 AS bit) 
                         END en_conci_caja, a.IdCuota, a.Secuencia, A.Valor_cuota, OG.IdTipoMovi
FROM            dbo.cp_orden_giro AS OG INNER JOIN
                         dbo.cp_orden_pago_tipo_x_empresa ON OG.IdEmpresa = dbo.cp_orden_pago_tipo_x_empresa.IdEmpresa INNER JOIN
                         dbo.cp_TipoDocumento ON OG.IdOrden_giro_Tipo = dbo.cp_TipoDocumento.CodTipoDocumento INNER JOIN
                         dbo.cp_proveedor ON OG.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND OG.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                         dbo.ct_cbtecble_tipo ON OG.IdTipoCbte_Ogiro = dbo.ct_cbtecble_tipo.IdTipoCbte AND OG.IdEmpresa = dbo.ct_cbtecble_tipo.IdEmpresa INNER JOIN
                         dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona LEFT OUTER JOIN
                         dbo.cp_conciliacion_Caja_det AS conci ON OG.IdEmpresa = conci.IdEmpresa_OGiro AND OG.IdCbteCble_Ogiro = conci.IdCbteCble_Ogiro AND OG.IdTipoCbte_Ogiro = conci.IdTipoCbte_Ogiro LEFT OUTER JOIN
                             (SELECT        IdEmpresa_cxp, IdCbteCble_cxp, IdTipoCbte_cxp, SUM(Valor_a_pagar) AS Total_Cancelado_OP
                               FROM            dbo.cp_orden_pago_det
                               GROUP BY IdEmpresa_cxp, IdCbteCble_cxp, IdTipoCbte_cxp) AS OP ON OG.IdEmpresa = OP.IdEmpresa_cxp AND OG.IdTipoCbte_Ogiro = OP.IdTipoCbte_cxp AND OG.IdCbteCble_Ogiro = OP.IdCbteCble_cxp LEFT JOIN
                             (SELECT        *
                               FROM            (SELECT        isnull(ROW_NUMBER() OVER (partition BY cp_cuotas_x_doc.IdEmpresa, cp_cuotas_x_doc.IdTipoCbte, cp_cuotas_x_doc.IdCbteCble
                                                         ORDER BY cp_cuotas_x_doc_det.Fecha_vcto_cuota), 0) IdRow, cp_cuotas_x_doc_det.IdEmpresa, cp_cuotas_x_doc_det.IdCuota, cp_cuotas_x_doc_det.Secuencia, cp_cuotas_x_doc_det.Num_cuota, 
                                                         cp_cuotas_x_doc_det.Fecha_vcto_cuota, cp_cuotas_x_doc_det.Valor_cuota, cp_cuotas_x_doc_det.IdEmpresa_op, cp_cuotas_x_doc_det.IdOrdenPago, cp_cuotas_x_doc_det.Secuencia_op, 
                                                         cp_cuotas_x_doc.IdEmpresa_ct, cp_cuotas_x_doc.IdTipoCbte, cp_cuotas_x_doc.IdCbteCble
                               FROM            cp_cuotas_x_doc INNER JOIN
                                                         cp_cuotas_x_doc_det ON cp_cuotas_x_doc.IdEmpresa = cp_cuotas_x_doc_det.IdEmpresa AND cp_cuotas_x_doc.IdCuota = cp_cuotas_x_doc_det.IdCuota
                               WHERE        dbo.cp_cuotas_x_doc_det.Estado = 0 AND dbo.cp_cuotas_x_doc_det.IdEmpresa_op IS NULL) CUO
WHERE        cuo.IdRow = 1) A ON A.IdEmpresa_ct = OG.IdEmpresa AND A.IdTipoCbte = OG.IdTipoCbte_Ogiro AND A.IdCbteCble = OG.IdCbteCble_Ogiro INNER JOIN
tb_persona AS per ON per.IdPersona = cp_proveedor.IdPersona LEFT JOIN
    (SELECT        IdEmpresa_Ogiro, IdTipoCbte_Ogiro, IdCbteCble_Ogiro, sum(de.re_valor_retencion) re_valor_retencion
      FROM            cp_retencion AS ca INNER JOIN
                                cp_retencion_det AS de ON ca.IdEmpresa = de.IdEmpresa AND ca.IdRetencion = de.IdRetencion
      WHERE        ca.Estado = 'A' AND ca.IdEmpresa_Ogiro IS NOT NULL
      GROUP BY IdEmpresa_Ogiro, IdTipoCbte_Ogiro, IdCbteCble_Ogiro) AS ret ON OG.IdEmpresa = ret.IdEmpresa_Ogiro AND og.IdTipoCbte_Ogiro = ret.IdTipoCbte_Ogiro AND og.IdCbteCble_Ogiro = ret.IdCbteCble_Ogiro
WHERE        (dbo.cp_orden_pago_tipo_x_empresa.IdTipo_op = 'FACT_PROVEE') AND og.Estado = 'A'
UNION
SELECT        dbo.cp_nota_DebCre.IdEmpresa, dbo.cp_nota_DebCre.IdCbteCble_Nota, dbo.cp_nota_DebCre.IdTipoCbte_Nota, '05' AS Expr4, dbo.cp_nota_DebCre.IdProveedor, CASE WHEN cp_nota_DebCre.cn_serie1 IS NULL 
                         THEN '' ELSE isnull(cp_nota_DebCre.cn_serie1, '') + '-' + isnull(cp_nota_DebCre.cn_serie2, '') END AS serie, CASE WHEN cp_nota_DebCre.cn_Nota IS NULL OR
                         cp_nota_DebCre.cn_Nota = '' THEN CAST(cp_nota_DebCre.IdCbteCble_Nota AS varchar(20)) ELSE cp_nota_DebCre.cn_Nota END AS cn_Nota, dbo.cp_nota_DebCre.cn_fecha, dbo.cp_nota_DebCre.cn_observacion, 
                         dbo.cp_nota_DebCre.IdSucursal, dbo.cp_nota_DebCre.cn_fecha AS Expr1, dbo.cp_nota_DebCre.cn_subtotal_iva, dbo.cp_nota_DebCre.cn_subtotal_siniva, dbo.cp_nota_DebCre.cn_baseImponible, 
                         dbo.cp_nota_DebCre.cn_Por_iva, dbo.cp_nota_DebCre.cn_valoriva, ROUND(dbo.cp_nota_DebCre.cn_total, 2) AS Expr2, ROUND(dbo.cp_nota_DebCre.cn_total, 2) AS Expr3, ROUND(ISNULL(OP.Total_Cancelado_OP, 0), 2) 
                         AS Total_Pagado, ROUND(ROUND(dbo.cp_nota_DebCre.cn_total, 2) - ROUND(ISNULL(OP.Total_Cancelado_OP, 0), 2), 2) AS Saldo, 'Nota de débito' AS Expr5, tb_persona.pe_nombreCompleto AS nom_proveedor, 
                         dbo.ct_cbtecble_tipo.CodTipoCbte, dbo.ct_cbtecble_tipo.tc_TipoCbte, dbo.cp_orden_pago_tipo_x_empresa.IdTipo_op, dbo.cp_orden_pago_tipo_x_empresa.IdEstadoAprobacion, isnull(a.Fecha_vcto_cuota, 
                         dbo.cp_nota_DebCre.cn_Fecha_vcto) cn_Fecha_vcto, null IdTipoFlujo, '' AS nom_flujo, dbo.tb_persona.IdPersona, 'N/D' AS Expr6, dbo.cp_nota_DebCre.Estado, 
                         CASE WHEN conci.IdConciliacion_Caja IS NULL THEN cast(0 AS bit) ELSE cast(1 AS bit) END en_conci_caja, a.IdCuota, a.Secuencia, A.Valor_cuota, NULL

FROM            dbo.cp_conciliacion_Caja_det AS conci RIGHT OUTER JOIN
                         dbo.tb_persona INNER JOIN
                         dbo.cp_proveedor ON dbo.tb_persona.IdPersona = dbo.cp_proveedor.IdPersona INNER JOIN
                         dbo.cp_nota_DebCre INNER JOIN
                         dbo.ct_cbtecble_tipo ON dbo.cp_nota_DebCre.IdEmpresa = dbo.ct_cbtecble_tipo.IdEmpresa AND dbo.cp_nota_DebCre.IdTipoCbte_Nota = dbo.ct_cbtecble_tipo.IdTipoCbte INNER JOIN
                         dbo.cp_orden_pago_tipo_x_empresa ON dbo.cp_nota_DebCre.IdEmpresa = dbo.cp_orden_pago_tipo_x_empresa.IdEmpresa ON dbo.cp_proveedor.IdEmpresa = dbo.cp_nota_DebCre.IdEmpresa AND 
                         dbo.cp_proveedor.IdProveedor = dbo.cp_nota_DebCre.IdProveedor ON conci.IdEmpresa_OGiro = dbo.cp_nota_DebCre.IdEmpresa AND conci.IdCbteCble_Ogiro = dbo.cp_nota_DebCre.IdCbteCble_Nota AND 
                         conci.IdTipoCbte_Ogiro = dbo.cp_nota_DebCre.IdTipoCbte_Nota LEFT OUTER JOIN
                             (SELECT        IdEmpresa_cxp, IdCbteCble_cxp, IdTipoCbte_cxp, SUM(Valor_a_pagar) AS Total_Cancelado_OP
                               FROM            dbo.cp_orden_pago_det
                               GROUP BY IdEmpresa_cxp, IdCbteCble_cxp, IdTipoCbte_cxp) AS OP ON dbo.cp_nota_DebCre.IdEmpresa = OP.IdEmpresa_cxp AND dbo.cp_nota_DebCre.IdCbteCble_Nota = OP.IdCbteCble_cxp AND 
                         dbo.cp_nota_DebCre.IdTipoCbte_Nota = OP.IdTipoCbte_cxp LEFT JOIN
                             (SELECT        TOP (1) cp_cuotas_x_doc_det.IdEmpresa, cp_cuotas_x_doc_det.IdCuota, cp_cuotas_x_doc_det.Secuencia, cp_cuotas_x_doc_det.Num_cuota, cp_cuotas_x_doc_det.Fecha_vcto_cuota, 
                                                         cp_cuotas_x_doc_det.Valor_cuota, cp_cuotas_x_doc_det.IdEmpresa_op, cp_cuotas_x_doc_det.IdOrdenPago, cp_cuotas_x_doc_det.Secuencia_op, cp_cuotas_x_doc.IdEmpresa_ct, cp_cuotas_x_doc.IdTipoCbte, 
                                                         cp_cuotas_x_doc.IdCbteCble
                               FROM            cp_cuotas_x_doc INNER JOIN
                                                         cp_cuotas_x_doc_det ON cp_cuotas_x_doc.IdEmpresa = cp_cuotas_x_doc_det.IdEmpresa AND cp_cuotas_x_doc.IdCuota = cp_cuotas_x_doc_det.IdCuota
                               WHERE        dbo.cp_cuotas_x_doc_det.Estado = 0
                               ORDER BY dbo.cp_cuotas_x_doc_det.Fecha_vcto_cuota) A ON A.IdEmpresa_ct = cp_nota_DebCre.IdEmpresa AND A.IdTipoCbte = cp_nota_DebCre.IdTipoCbte_Nota AND 
                         A.IdCbteCble = cp_nota_DebCre.IdCbteCble_Nota INNER JOIN
                         tb_persona AS per ON per.IdPersona = cp_proveedor.IdPersona
WHERE        (dbo.cp_orden_pago_tipo_x_empresa.IdTipo_op = 'FACT_PROVEE') AND (dbo.cp_nota_DebCre.DebCre = 'D') AND cp_nota_DebCre.Estado = 'A'
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[6] 4[4] 2[72] 3) )"
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
         Top = 0
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_giro_x_pagar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_giro_x_pagar';

