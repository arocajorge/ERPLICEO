CREATE VIEW web.vwcp_conciliacion_Caja_det
AS
SELECT        dbo.cp_conciliacion_Caja_det.IdEmpresa, dbo.cp_conciliacion_Caja_det.IdConciliacion_Caja, dbo.cp_conciliacion_Caja_det.Secuencia, dbo.cp_conciliacion_Caja_det.IdEmpresa_OGiro, 
                         dbo.cp_conciliacion_Caja_det.IdCbteCble_Ogiro, dbo.cp_conciliacion_Caja_det.IdTipoCbte_Ogiro, dbo.cp_conciliacion_Caja_det.Tipo_documento, ISNULL(SUM(dbo.cp_orden_pago_cancelaciones.MontoAplicado), 0) 
                         AS MontoAplicado, dbo.tb_persona.IdPersona, dbo.tb_persona.pe_nombreCompleto, dbo.cp_orden_giro.IdProveedor, dbo.cp_orden_giro.IdSucursal, dbo.cp_orden_giro.co_Por_iva, dbo.cp_orden_giro.co_FechaFactura, 
                         dbo.cp_orden_giro.co_observacion, dbo.cp_orden_giro.co_baseImponible, dbo.cp_orden_giro.co_valoriva, dbo.cp_orden_giro.co_valorpagar, 
                         ISNULL(dbo.cp_orden_giro.co_total - ISNULL(SUM(dbo.cp_orden_pago_cancelaciones.MontoAplicado), 0), 0) AS SaldoOG, dbo.cp_conciliacion_Caja_det.Valor_a_aplicar, dbo.cp_conciliacion_Caja_det.IdEmpresa_OP, 
                         dbo.cp_conciliacion_Caja_det.IdOrdenPago_OP, dbo.cp_orden_giro.co_factura, dbo.cp_orden_giro.co_total
FROM            dbo.cp_orden_pago_cancelaciones INNER JOIN
                         dbo.cp_orden_pago_det ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_op = dbo.cp_orden_pago_det.IdEmpresa AND dbo.cp_orden_pago_cancelaciones.IdOrdenPago_op = dbo.cp_orden_pago_det.IdOrdenPago AND 
                         dbo.cp_orden_pago_cancelaciones.Secuencia_op = dbo.cp_orden_pago_det.Secuencia RIGHT OUTER JOIN
                         dbo.cp_orden_giro INNER JOIN
                         dbo.cp_proveedor ON dbo.cp_orden_giro.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.cp_orden_giro.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                         dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.cp_conciliacion_Caja_det ON dbo.cp_orden_giro.IdEmpresa = dbo.cp_conciliacion_Caja_det.IdEmpresa_OGiro AND dbo.cp_orden_giro.IdCbteCble_Ogiro = dbo.cp_conciliacion_Caja_det.IdCbteCble_Ogiro AND 
                         dbo.cp_orden_giro.IdTipoCbte_Ogiro = dbo.cp_conciliacion_Caja_det.IdTipoCbte_Ogiro ON dbo.cp_orden_pago_det.IdEmpresa_cxp = dbo.cp_orden_giro.IdEmpresa AND 
                         dbo.cp_orden_pago_det.IdCbteCble_cxp = dbo.cp_orden_giro.IdCbteCble_Ogiro AND dbo.cp_orden_pago_det.IdTipoCbte_cxp = dbo.cp_orden_giro.IdTipoCbte_Ogiro
GROUP BY dbo.cp_conciliacion_Caja_det.IdEmpresa, dbo.cp_conciliacion_Caja_det.IdConciliacion_Caja, dbo.cp_conciliacion_Caja_det.Secuencia, dbo.cp_conciliacion_Caja_det.IdEmpresa_OGiro, 
                         dbo.cp_conciliacion_Caja_det.IdCbteCble_Ogiro, dbo.cp_conciliacion_Caja_det.IdTipoCbte_Ogiro, dbo.cp_conciliacion_Caja_det.Tipo_documento, dbo.tb_persona.IdPersona, dbo.tb_persona.pe_nombreCompleto, 
                         dbo.cp_orden_giro.IdProveedor, dbo.cp_orden_giro.IdSucursal, dbo.cp_orden_giro.co_Por_iva, dbo.cp_orden_giro.co_FechaFactura, dbo.cp_orden_giro.co_observacion, dbo.cp_orden_giro.co_baseImponible, 
                         dbo.cp_orden_giro.co_valoriva, dbo.cp_orden_giro.co_valorpagar, dbo.cp_orden_giro.co_total, dbo.cp_conciliacion_Caja_det.Valor_a_aplicar, dbo.cp_conciliacion_Caja_det.IdEmpresa_OP, 
                         dbo.cp_conciliacion_Caja_det.IdOrdenPago_OP, dbo.cp_orden_giro.co_factura
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[58] 4[3] 2[25] 3) )"
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
         Begin Table = "cp_orden_pago_cancelaciones"
            Begin Extent = 
               Top = 255
               Left = 0
               Bottom = 497
               Right = 229
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "cp_orden_pago_det"
            Begin Extent = 
               Top = 247
               Left = 347
               Bottom = 377
               Right = 569
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_giro"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 449
               Right = 313
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "cp_proveedor"
            Begin Extent = 
               Top = 177
               Left = 546
               Bottom = 307
               Right = 794
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 345
               Left = 557
               Bottom = 475
               Right = 805
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_conciliacion_Caja_det"
            Begin Extent = 
               Top = 0
               Left = 489
               Bottom = 376
               Right = 768
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
      Begin ColumnWidths = 9
         Width', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcp_conciliacion_Caja_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' = 284
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcp_conciliacion_Caja_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcp_conciliacion_Caja_det';

