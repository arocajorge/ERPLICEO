CREATE  VIEW [dbo].[vwAf_Valores_Depre_Contabilizar]
AS
SELECT        dpr.IdEmpresa, dpr.IdDepreciacion, dpr.Cod_Depreciacion, dpr.IdPeriodo, af.IdActivoFijo, af.IdActivoFijoTipo IdActijoFijoTipo, af.Af_Nombre, 
                         tip.Af_Descripcion, dpr.Descripcion, dpr.Fecha_Depreciacion, '' cod_tipo_depreciacion, ddt.Valor_Depreciacion
FROM            dbo.Af_Depreciacion AS dpr INNER JOIN
                         dbo.Af_Depreciacion_Det AS ddt ON dpr.IdEmpresa = ddt.IdEmpresa AND dpr.IdDepreciacion = ddt.IdDepreciacion INNER JOIN
                         dbo.Af_Activo_fijo AS af ON af.IdEmpresa = ddt.IdEmpresa AND af.IdActivoFijo = ddt.IdActivoFijo INNER JOIN
                         dbo.Af_Activo_fijo_tipo AS tip ON tip.IdEmpresa = af.IdEmpresa AND tip.IdActivoFijoTipo = af.IdActivoFijoTipo

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[18] 4[7] 2[40] 3) )"
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
         Begin Table = "dpr"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 263
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ddt"
            Begin Extent = 
               Top = 195
               Left = 13
               Bottom = 324
               Right = 238
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "af"
            Begin Extent = 
               Top = 15
               Left = 339
               Bottom = 357
               Right = 564
            End
            DisplayFlags = 280
            TopColumn = 68
         End
         Begin Table = "tip"
            Begin Extent = 
               Top = 0
               Left = 857
               Bottom = 361
               Right = 1090
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tde"
            Begin Extent = 
               Top = 127
               Left = 617
               Bottom = 367
               Right = 846
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
      Begin ColumnWidths = 18
         Width = 284
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
         Wid', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwAf_Valores_Depre_Contabilizar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'th = 1815
         Width = 1500
         Width = 1875
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwAf_Valores_Depre_Contabilizar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwAf_Valores_Depre_Contabilizar';

