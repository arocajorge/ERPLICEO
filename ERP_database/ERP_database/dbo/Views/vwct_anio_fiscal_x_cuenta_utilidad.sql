CREATE VIEW dbo.vwct_anio_fiscal_x_cuenta_utilidad
AS
SELECT        dbo.ct_anio_fiscal_x_cuenta_utilidad.IdEmpresa, dbo.ct_anio_fiscal_x_cuenta_utilidad.IdanioFiscal, dbo.ct_anio_fiscal_x_cuenta_utilidad.IdCtaCble, 
                         dbo.ct_plancta.pc_Cuenta, dbo.ct_plancta.IdNivelCta, dbo.ct_plancta.IdGrupoCble, null IdTipoCtaCble, dbo.ct_grupocble.gc_GrupoCble
FROM            dbo.ct_anio_fiscal_x_cuenta_utilidad INNER JOIN
                         dbo.ct_plancta ON dbo.ct_anio_fiscal_x_cuenta_utilidad.IdEmpresa = dbo.ct_plancta.IdEmpresa AND 
                         dbo.ct_anio_fiscal_x_cuenta_utilidad.IdCtaCble = dbo.ct_plancta.IdCtaCble INNER JOIN
                         dbo.ct_grupocble ON dbo.ct_plancta.IdGrupoCble = dbo.ct_grupocble.IdGrupoCble
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
         Begin Table = "ct_plancta"
            Begin Extent = 
               Top = 1
               Left = 374
               Bottom = 212
               Right = 682
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "ct_anio_fiscal_x_cuenta_utilidad"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 152
               Right = 296
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_grupocble"
            Begin Extent = 
               Top = 6
               Left = 720
               Bottom = 194
               Right = 929
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
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwct_anio_fiscal_x_cuenta_utilidad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwct_anio_fiscal_x_cuenta_utilidad';

