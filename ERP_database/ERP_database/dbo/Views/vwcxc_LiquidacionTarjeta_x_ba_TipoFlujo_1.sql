CREATE VIEW dbo.vwcxc_LiquidacionTarjeta_x_ba_TipoFlujo
AS
SELECT dbo.cxc_LiquidacionTarjeta_x_ba_TipoFlujo.IdEmpresa, dbo.cxc_LiquidacionTarjeta_x_ba_TipoFlujo.IdSucursal, dbo.cxc_LiquidacionTarjeta_x_ba_TipoFlujo.IdLiquidacion, dbo.cxc_LiquidacionTarjeta_x_ba_TipoFlujo.Secuencia, 
                  dbo.cxc_LiquidacionTarjeta_x_ba_TipoFlujo.IdTipoFlujo, dbo.cxc_LiquidacionTarjeta_x_ba_TipoFlujo.Porcentaje, dbo.cxc_LiquidacionTarjeta_x_ba_TipoFlujo.Valor, dbo.ba_TipoFlujo.Descricion
FROM     dbo.cxc_LiquidacionTarjeta_x_ba_TipoFlujo INNER JOIN
                  dbo.ba_TipoFlujo ON dbo.cxc_LiquidacionTarjeta_x_ba_TipoFlujo.IdEmpresa = dbo.ba_TipoFlujo.IdEmpresa AND dbo.cxc_LiquidacionTarjeta_x_ba_TipoFlujo.IdTipoFlujo = dbo.ba_TipoFlujo.IdTipoFlujo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_LiquidacionTarjeta_x_ba_TipoFlujo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Begin Table = "cxc_LiquidacionTarjeta_x_ba_TipoFlujo"
            Begin Extent = 
               Top = 7
               Left = 55
               Bottom = 266
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_TipoFlujo"
            Begin Extent = 
               Top = 7
               Left = 340
               Bottom = 170
               Right = 584
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
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 3648
         Width = 1200
         Width = 1200
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_LiquidacionTarjeta_x_ba_TipoFlujo';

