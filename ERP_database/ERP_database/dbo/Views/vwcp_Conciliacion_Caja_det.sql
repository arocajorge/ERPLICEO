CREATE view  [dbo].[vwcp_Conciliacion_Caja_det]
as
SELECT        dbo.cp_conciliacion_Caja.IdEmpresa, dbo.cp_conciliacion_Caja.IdConciliacion_Caja, dbo.cp_conciliacion_Caja.IdCaja, dbo.cp_conciliacion_Caja.Fecha, 
                         dbo.caj_Caja.ca_Descripcion, dbo.caj_Caja.IdUsuario_Responsable, dbo.cp_orden_giro.IdOrden_giro_Tipo, dbo.cp_orden_giro.IdProveedor, 
                         dbo.cp_orden_giro.co_fechaOg, dbo.cp_orden_giro.co_serie, dbo.cp_orden_giro.co_factura, dbo.cp_orden_giro.co_FechaFactura, 
                         dbo.cp_orden_giro.co_FechaFactura_vct, dbo.cp_orden_giro.co_observacion, dbo.cp_orden_giro.co_subtotal_iva, dbo.cp_orden_giro.co_subtotal_siniva, 
                         dbo.cp_orden_giro.co_baseImponible, dbo.cp_orden_giro.co_Por_iva, dbo.cp_orden_giro.co_valoriva, dbo.cp_orden_giro.co_total, 
                         dbo.cp_orden_giro.co_valorpagar, dbo.cp_orden_giro.IdIden_credito, null IdTipoFlujo, '' IdCtaCble_Gasto, dbo.cp_orden_giro.Estado, 
                         dbo.cp_orden_giro.Num_Autorizacion, ISNULL(dbo.vwcp_Retencion_valor_total_x_cbte_cxp.Total_Retencion, 0) 
                         AS Total_Retencion, dbo.vwcp_Retencion_valor_total_x_cbte_cxp.IdRetencion, dbo.cp_conciliacion_Caja_det.IdEmpresa_OGiro, 
                         dbo.cp_conciliacion_Caja_det.IdCbteCble_Ogiro, dbo.cp_conciliacion_Caja_det.IdTipoCbte_Ogiro, dbo.cp_conciliacion_Caja.IdEstadoCierre, 
                         dbo.cp_conciliacion_Caja.Observacion, dbo.cp_conciliacion_Caja.IdEmpresa_op, dbo.cp_conciliacion_Caja.IdOrdenPago_op, 
                         dbo.cp_conciliacion_Caja_det.Secuencia, dbo.cp_conciliacion_Caja.IdCtaCble, dbo.cp_orden_giro.IdTipoMovi, dbo.cp_conciliacion_Caja.Dif_x_pagar_o_cobrar, 
                         dbo.cp_conciliacion_Caja.Total_fondo, dbo.cp_conciliacion_Caja.Total_fact_vale, dbo.cp_conciliacion_Caja.Total_Ing, dbo.cp_conciliacion_Caja.Ingresos, 
                         dbo.cp_conciliacion_Caja.Saldo_cont_al_periodo, dbo.cp_conciliacion_Caja.IdPeriodo, dbo.cp_conciliacion_Caja_det.Valor_a_aplicar, 
                         dbo.cp_conciliacion_Caja_det.Tipo_documento, dbo.cp_conciliacion_Caja_det.IdEmpresa_OP AS IdEmpresa_OP_conci, 
                         dbo.cp_conciliacion_Caja_det.IdOrdenPago_OP AS IdOrdenPago_OP_Conci, dbo.tb_persona.pe_cedulaRuc
FROM            dbo.cp_conciliacion_Caja INNER JOIN
                         dbo.caj_Caja ON dbo.cp_conciliacion_Caja.IdEmpresa = dbo.caj_Caja.IdEmpresa AND dbo.cp_conciliacion_Caja.IdCaja = dbo.caj_Caja.IdCaja INNER JOIN
                         dbo.cp_conciliacion_Caja_det ON dbo.cp_conciliacion_Caja.IdEmpresa = dbo.cp_conciliacion_Caja_det.IdEmpresa AND 
                         dbo.cp_conciliacion_Caja.IdConciliacion_Caja = dbo.cp_conciliacion_Caja_det.IdConciliacion_Caja INNER JOIN
                         dbo.cp_orden_giro ON dbo.cp_conciliacion_Caja_det.IdEmpresa_OGiro = dbo.cp_orden_giro.IdEmpresa AND 
                         dbo.cp_conciliacion_Caja_det.IdCbteCble_Ogiro = dbo.cp_orden_giro.IdCbteCble_Ogiro AND 
                         dbo.cp_conciliacion_Caja_det.IdTipoCbte_Ogiro = dbo.cp_orden_giro.IdTipoCbte_Ogiro INNER JOIN
                         dbo.cp_proveedor ON dbo.cp_orden_giro.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND 
                         dbo.cp_orden_giro.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                         dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona LEFT OUTER JOIN
                         dbo.vwcp_Retencion_valor_total_x_cbte_cxp ON dbo.cp_orden_giro.IdEmpresa = dbo.vwcp_Retencion_valor_total_x_cbte_cxp.IdEmpresa_Ogiro AND 
                         dbo.cp_orden_giro.IdCbteCble_Ogiro = dbo.vwcp_Retencion_valor_total_x_cbte_cxp.IdCbteCble_Ogiro AND 
                         dbo.cp_orden_giro.IdTipoCbte_Ogiro = dbo.vwcp_Retencion_valor_total_x_cbte_cxp.IdTipoCbte_Ogiro
GROUP BY dbo.cp_conciliacion_Caja.IdEmpresa, dbo.cp_conciliacion_Caja.IdConciliacion_Caja, dbo.cp_conciliacion_Caja.IdCaja, dbo.cp_conciliacion_Caja.Fecha, 
                         dbo.caj_Caja.ca_Descripcion, dbo.caj_Caja.IdUsuario_Responsable, dbo.cp_orden_giro.IdOrden_giro_Tipo, dbo.cp_orden_giro.IdProveedor, 
                         dbo.cp_orden_giro.co_fechaOg, dbo.cp_orden_giro.co_serie, dbo.cp_orden_giro.co_factura, dbo.cp_orden_giro.co_FechaFactura, 
                         dbo.cp_orden_giro.co_FechaFactura_vct, dbo.cp_orden_giro.co_observacion, dbo.cp_orden_giro.co_subtotal_iva, dbo.cp_orden_giro.co_subtotal_siniva, 
                         dbo.cp_orden_giro.co_baseImponible, dbo.cp_orden_giro.co_Por_iva, dbo.cp_orden_giro.co_valoriva, dbo.cp_orden_giro.co_total, 
                         dbo.cp_orden_giro.co_valorpagar, dbo.cp_orden_giro.IdIden_credito, dbo.cp_orden_giro.Estado, 
                          dbo.cp_orden_giro.Num_Autorizacion, dbo.vwcp_Retencion_valor_total_x_cbte_cxp.Total_Retencion, 
                         dbo.vwcp_Retencion_valor_total_x_cbte_cxp.IdRetencion, dbo.cp_conciliacion_Caja_det.IdEmpresa_OGiro, dbo.cp_conciliacion_Caja_det.IdCbteCble_Ogiro, 
                         dbo.cp_conciliacion_Caja_det.IdTipoCbte_Ogiro, dbo.cp_conciliacion_Caja.IdEstadoCierre, dbo.cp_conciliacion_Caja.Observacion, 
                         dbo.cp_conciliacion_Caja.IdEmpresa_op, dbo.cp_conciliacion_Caja.IdOrdenPago_op, dbo.cp_conciliacion_Caja_det.Secuencia, dbo.cp_conciliacion_Caja.IdCtaCble, 
                          dbo.cp_orden_giro.IdTipoMovi, 
                         dbo.cp_conciliacion_Caja.Dif_x_pagar_o_cobrar, dbo.cp_conciliacion_Caja.Total_fondo, dbo.cp_conciliacion_Caja.Total_fact_vale, 
                         dbo.cp_conciliacion_Caja.Total_Ing, dbo.cp_conciliacion_Caja.Ingresos, dbo.cp_conciliacion_Caja.Saldo_cont_al_periodo, dbo.cp_conciliacion_Caja.IdPeriodo, 
                         dbo.cp_conciliacion_Caja_det.Valor_a_aplicar, dbo.cp_conciliacion_Caja_det.Tipo_documento, dbo.cp_conciliacion_Caja_det.IdEmpresa_OP, 
                         dbo.cp_conciliacion_Caja_det.IdOrdenPago_OP, dbo.tb_persona.pe_cedulaRuc
UNION
/*NOTA DE DEBITO*/ SELECT dbo.cp_conciliacion_Caja.IdEmpresa, dbo.cp_conciliacion_Caja.IdConciliacion_Caja, dbo.cp_conciliacion_Caja.IdCaja, 
                         dbo.cp_conciliacion_Caja.Fecha, dbo.caj_Caja.ca_Descripcion, dbo.caj_Caja.IdUsuario_Responsable, '05', dbo.cp_nota_DebCre.IdProveedor, 
                         dbo.cp_nota_DebCre.cn_fecha, dbo.cp_nota_DebCre.cn_serie1 + '-' + dbo.cp_nota_DebCre.cn_serie2 AS serie, dbo.cp_nota_DebCre.cn_Nota, 
                         dbo.cp_nota_DebCre.cn_fecha, dbo.cp_nota_DebCre.cn_Fecha_vcto, dbo.cp_nota_DebCre.cn_observacion, dbo.cp_nota_DebCre.cn_subtotal_iva, 
                         dbo.cp_nota_DebCre.cn_subtotal_siniva, dbo.cp_nota_DebCre.cn_baseImponible, dbo.cp_nota_DebCre.cn_Por_iva, dbo.cp_nota_DebCre.cn_valoriva, 
                         dbo.cp_nota_DebCre.cn_total, dbo.cp_nota_DebCre.cn_total, NULL asIdIden_credito, NULL AS IdTipoFlujo, NULL AS IdCtaCble_Gasto, dbo.cp_nota_DebCre.Estado, 
                          dbo.cp_nota_DebCre.cn_Autorizacion, 0 AS Total_Retencion, NULL AS IdRetencion, 
                         dbo.cp_conciliacion_Caja_det.IdEmpresa_OGiro, dbo.cp_conciliacion_Caja_det.IdCbteCble_Ogiro, dbo.cp_conciliacion_Caja_det.IdTipoCbte_Ogiro, 
                         dbo.cp_conciliacion_Caja.IdEstadoCierre, dbo.cp_conciliacion_Caja.Observacion, dbo.cp_conciliacion_Caja.IdEmpresa_op, 
                         dbo.cp_conciliacion_Caja.IdOrdenPago_op, dbo.cp_conciliacion_Caja_det.Secuencia, dbo.cp_conciliacion_Caja.IdCtaCble,  dbo.cp_conciliacion_Caja_det.IdTipoMovi, 
                         dbo.cp_conciliacion_Caja.Dif_x_pagar_o_cobrar, dbo.cp_conciliacion_Caja.Total_fondo, dbo.cp_conciliacion_Caja.Total_fact_vale, 
                         dbo.cp_conciliacion_Caja.Total_Ing, dbo.cp_conciliacion_Caja.Ingresos, dbo.cp_conciliacion_Caja.Saldo_cont_al_periodo, dbo.cp_conciliacion_Caja.IdPeriodo, 
                         dbo.cp_conciliacion_Caja_det.Valor_a_aplicar, dbo.cp_conciliacion_Caja_det.Tipo_documento, 
                         dbo.cp_conciliacion_Caja_det.IdEmpresa_OP AS IdEmpresa_OP_conci, dbo.cp_conciliacion_Caja_det.IdOrdenPago_OP AS IdOrdenPago_OP_Conci, NULL 
                         pe_cedulaRuc
FROM            cp_conciliacion_Caja INNER JOIN
                         caj_Caja ON cp_conciliacion_Caja.IdEmpresa = caj_Caja.IdEmpresa AND cp_conciliacion_Caja.IdCaja = caj_Caja.IdCaja INNER JOIN
                         cp_conciliacion_Caja_det ON cp_conciliacion_Caja.IdEmpresa = cp_conciliacion_Caja_det.IdEmpresa AND 
                         cp_conciliacion_Caja.IdConciliacion_Caja = cp_conciliacion_Caja_det.IdConciliacion_Caja INNER JOIN
                         cp_nota_DebCre ON cp_conciliacion_Caja_det.IdEmpresa_OGiro = cp_nota_DebCre.IdEmpresa AND 
                         cp_conciliacion_Caja_det.IdCbteCble_Ogiro = cp_nota_DebCre.IdCbteCble_Nota AND 
                         cp_conciliacion_Caja_det.IdTipoCbte_Ogiro = cp_nota_DebCre.IdTipoCbte_Nota
GROUP BY dbo.cp_conciliacion_Caja.IdEmpresa, dbo.cp_conciliacion_Caja.IdConciliacion_Caja, dbo.cp_conciliacion_Caja.IdCaja, dbo.cp_conciliacion_Caja.Fecha, 
                         dbo.caj_Caja.ca_Descripcion, dbo.caj_Caja.IdUsuario_Responsable, dbo.cp_nota_DebCre.IdProveedor, dbo.cp_nota_DebCre.cn_fecha, 
                         dbo.cp_nota_DebCre.cn_serie1, dbo.cp_nota_DebCre.cn_serie2, dbo.cp_nota_DebCre.cn_Nota, dbo.cp_nota_DebCre.cn_fecha, dbo.cp_nota_DebCre.cn_Fecha_vcto, 
                         dbo.cp_nota_DebCre.cn_observacion, dbo.cp_nota_DebCre.cn_subtotal_iva, dbo.cp_nota_DebCre.cn_subtotal_siniva, dbo.cp_nota_DebCre.cn_baseImponible, 
                         dbo.cp_nota_DebCre.cn_Por_iva, dbo.cp_nota_DebCre.cn_valoriva, dbo.cp_nota_DebCre.cn_total, dbo.cp_nota_DebCre.cn_total, dbo.cp_nota_DebCre.Estado, 
                         dbo.cp_nota_DebCre.cn_Autorizacion, dbo.cp_conciliacion_Caja_det.IdEmpresa_OGiro, 
                         dbo.cp_conciliacion_Caja_det.IdCbteCble_Ogiro, dbo.cp_conciliacion_Caja_det.IdTipoCbte_Ogiro, dbo.cp_conciliacion_Caja.IdEstadoCierre, 
                         dbo.cp_conciliacion_Caja.Observacion, dbo.cp_conciliacion_Caja.IdEmpresa_op, dbo.cp_conciliacion_Caja.IdOrdenPago_op, 
                         dbo.cp_conciliacion_Caja_det.Secuencia, dbo.cp_conciliacion_Caja.IdCtaCble,  dbo.cp_conciliacion_Caja_det.IdTipoMovi, dbo.cp_conciliacion_Caja.Dif_x_pagar_o_cobrar, 
                         dbo.cp_conciliacion_Caja.Total_fondo, dbo.cp_conciliacion_Caja.Total_fact_vale, dbo.cp_conciliacion_Caja.Total_Ing, dbo.cp_conciliacion_Caja.Ingresos, 
                         dbo.cp_conciliacion_Caja.Saldo_cont_al_periodo, dbo.cp_conciliacion_Caja.IdPeriodo, dbo.cp_conciliacion_Caja_det.Valor_a_aplicar, 
                         dbo.cp_conciliacion_Caja_det.Tipo_documento, dbo.cp_conciliacion_Caja_det.IdEmpresa_OP, dbo.cp_conciliacion_Caja_det.IdOrdenPago_OP
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[81] 4[5] 2[9] 3) )"
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
         Begin Table = "cp_conciliacion_Caja"
            Begin Extent = 
               Top = 5
               Left = 339
               Bottom = 415
               Right = 548
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_Caja"
            Begin Extent = 
               Top = 288
               Left = 515
               Bottom = 417
               Right = 725
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_conciliacion_Caja_det"
            Begin Extent = 
               Top = 95
               Left = 9
               Bottom = 401
               Right = 286
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_giro"
            Begin Extent = 
               Top = 11
               Left = 602
               Bottom = 470
               Right = 875
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "vwcp_Retencion_valor_total_x_cbte_cxp"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 77
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 40
         Width = 284
         Width = 1500
         Width = 2145
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_Conciliacion_Caja_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
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
      Begin ColumnWidths = 12
         Column = 2355
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_Conciliacion_Caja_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_Conciliacion_Caja_det';

