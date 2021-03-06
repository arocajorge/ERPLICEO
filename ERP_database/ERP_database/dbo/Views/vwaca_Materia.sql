﻿CREATE VIEW dbo.vwaca_Materia
AS
SELECT m.IdEmpresa, m.IdMateria, m.IdMateriaArea, m.IdMateriaGrupo, m.OrdenMateria, ma.OrdenMateriaArea, mg.OrdenMateriaGrupo, m.NomMateria, ma.NomMateriaArea, mg.NomMateriaGrupo, mg.PromediarGrupo, m.EsObligatorio, 
                  m.Estado, m.IdCatalogoTipoCalificacion, c.NomCatalogo
FROM     dbo.aca_Materia AS m WITH (nolock) LEFT OUTER JOIN
                  dbo.aca_Catalogo AS c WITH (nolock) ON m.IdCatalogoTipoCalificacion = c.IdCatalogo LEFT OUTER JOIN
                  dbo.aca_MateriaArea AS ma WITH (nolock) ON m.IdEmpresa = ma.IdEmpresa AND m.IdMateriaArea = ma.IdMateriaArea LEFT OUTER JOIN
                  dbo.aca_MateriaGrupo AS mg WITH (nolock) ON m.IdEmpresa = mg.IdEmpresa AND m.IdMateriaGrupo = mg.IdMateriaGrupo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Materia';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[39] 4[31] 2[22] 3) )"
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
         Begin Table = "m"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 279
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 173
               Left = 507
               Bottom = 336
               Right = 752
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ma"
            Begin Extent = 
               Top = 116
               Left = 877
               Bottom = 279
               Right = 1122
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "mg"
            Begin Extent = 
               Top = 0
               Left = 594
               Bottom = 163
               Right = 839
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 16
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 2088
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2460
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
       ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Materia';




















GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'  SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Materia';





