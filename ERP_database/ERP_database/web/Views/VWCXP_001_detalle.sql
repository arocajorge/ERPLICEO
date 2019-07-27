CREATE VIEW web.VWCXP_001_detalle
AS
SELECT        dbo.cp_orden_giro.IdEmpresa, dbo.cp_orden_giro.IdCbteCble_Ogiro, dbo.cp_orden_giro.IdTipoCbte_Ogiro, dbo.in_Producto.pr_codigo, dbo.in_Producto.pr_codigo2, dbo.cp_orden_giro_det.IdProducto, 
                         dbo.cp_orden_giro_det.IdUnidadMedida, dbo.cp_orden_giro_det.Cantidad, dbo.cp_orden_giro_det.CostoUni, dbo.cp_orden_giro_det.PorDescuento, dbo.cp_orden_giro_det.DescuentoUni, dbo.cp_orden_giro_det.CostoUniFinal, 
                         dbo.cp_orden_giro_det.Subtotal, dbo.cp_orden_giro_det.PorIva, dbo.cp_orden_giro_det.ValorIva, dbo.cp_orden_giro_det.Total, dbo.in_UnidadMedida.Descripcion, dbo.in_Producto.pr_descripcion, 
                         dbo.in_Producto.pr_descripcion_2
FROM            dbo.cp_orden_giro INNER JOIN
                         dbo.cp_orden_giro_det ON dbo.cp_orden_giro.IdEmpresa = dbo.cp_orden_giro_det.IdEmpresa AND dbo.cp_orden_giro.IdCbteCble_Ogiro = dbo.cp_orden_giro_det.IdCbteCble_Ogiro AND 
                         dbo.cp_orden_giro.IdTipoCbte_Ogiro = dbo.cp_orden_giro_det.IdTipoCbte_Ogiro INNER JOIN
                         dbo.in_Producto ON dbo.cp_orden_giro_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.cp_orden_giro_det.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                         dbo.in_UnidadMedida ON dbo.in_Producto.IdUnidadMedida = dbo.in_UnidadMedida.IdUnidadMedida AND dbo.in_Producto.IdUnidadMedida_Consumo = dbo.in_UnidadMedida.IdUnidadMedida

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[57] 4[5] 2[28] 3) )"
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
         Begin Table = "cp_orden_giro"
            Begin Extent = 
               Top = 167
               Left = 286
               Bottom = 455
               Right = 545
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_giro_det"
            Begin Extent = 
               Top = 6
               Left = 335
               Bottom = 367
               Right = 531
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 17
               Left = 549
               Bottom = 299
               Right = 783
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "in_UnidadMedida"
            Begin Extent = 
               Top = 58
               Left = 22
               Bottom = 377
               Right = 201
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
     ' , @level0type=N'SCHEMA',@level0name=N'web', @level1type=N'VIEW',@level1name=N'VWCXP_001_detalle'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'    Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'web', @level1type=N'VIEW',@level1name=N'VWCXP_001_detalle'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'web', @level1type=N'VIEW',@level1name=N'VWCXP_001_detalle'
GO

