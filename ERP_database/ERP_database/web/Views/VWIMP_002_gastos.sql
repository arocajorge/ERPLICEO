CREATE VIEW web.VWIMP_002_gastos
AS
SELECT        dbo.imp_orden_compra_ext.IdEmpresa, dbo.imp_orden_compra_ext.IdOrdenCompra_ext, dbo.imp_gasto.gt_descripcion, dbo.ct_cbtecble_det.dc_Valor, dbo.ct_cbtecble_det.dc_Observacion, dbo.imp_gasto.gt_orden
FROM            dbo.imp_orden_compra_ext INNER JOIN
                         dbo.imp_orden_compra_ext_ct_cbteble_det_gastos ON dbo.imp_orden_compra_ext.IdEmpresa = dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdEmpresa AND 
                         dbo.imp_orden_compra_ext.IdOrdenCompra_ext = dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdOrdenCompra_ext INNER JOIN
                         dbo.imp_gasto INNER JOIN
                         dbo.imp_gasto_x_ct_plancta ON dbo.imp_gasto.IdGasto_tipo = dbo.imp_gasto_x_ct_plancta.IdGasto_tipo ON dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdGasto_tipo = dbo.imp_gasto_x_ct_plancta.IdGasto_tipo AND 
                         dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdEmpresa = dbo.imp_gasto_x_ct_plancta.IdEmpresa INNER JOIN
                         dbo.ct_cbtecble_det ON dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdEmpresa_ct = dbo.ct_cbtecble_det.IdEmpresa AND 
                         dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdTipoCbte = dbo.ct_cbtecble_det.IdTipoCbte AND dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.IdCbteCble = dbo.ct_cbtecble_det.IdCbteCble AND 
                         dbo.imp_orden_compra_ext_ct_cbteble_det_gastos.secuencia_ct = dbo.ct_cbtecble_det.secuencia
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWIMP_002_gastos';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'lumnWidths = 11
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWIMP_002_gastos';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[32] 4[5] 2[5] 3) )"
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
         Begin Table = "imp_orden_compra_ext"
            Begin Extent = 
               Top = 25
               Left = 888
               Bottom = 198
               Right = 1137
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "imp_orden_compra_ext_ct_cbteble_det_gastos"
            Begin Extent = 
               Top = 23
               Left = 524
               Bottom = 249
               Right = 812
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "imp_gasto"
            Begin Extent = 
               Top = 1
               Left = 127
               Bottom = 165
               Right = 297
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "imp_gasto_x_ct_plancta"
            Begin Extent = 
               Top = 61
               Left = 313
               Bottom = 174
               Right = 507
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_cbtecble_det"
            Begin Extent = 
               Top = 0
               Left = 640
               Bottom = 213
               Right = 903
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
      Begin Co', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWIMP_002_gastos';



