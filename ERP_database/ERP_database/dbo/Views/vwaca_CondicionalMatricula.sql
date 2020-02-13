CREATE VIEW dbo.vwaca_CondicionalMatricula
AS
SELECT dbo.aca_CondicionalMatricula.IdEmpresa, dbo.aca_CondicionalMatricula.IdCondicional, dbo.aca_CondicionalMatricula.IdAlumno, dbo.aca_Alumno.Codigo, dbo.tb_persona.pe_nombreCompleto, dbo.aca_CondicionalMatricula.IdAnio, 
                  dbo.aca_AnioLectivo.Descripcion, dbo.aca_CondicionalMatricula.Fecha, dbo.aca_CondicionalMatricula.IdCatalogoCONDIC, dbo.aca_Catalogo.NomCatalogo, dbo.aca_CondicionalMatricula.Observacion, 
                  dbo.aca_CondicionalMatricula.Estado
FROM     dbo.aca_CondicionalMatricula INNER JOIN
                  dbo.aca_AnioLectivo ON dbo.aca_CondicionalMatricula.IdEmpresa = dbo.aca_AnioLectivo.IdEmpresa AND dbo.aca_CondicionalMatricula.IdAnio = dbo.aca_AnioLectivo.IdAnio INNER JOIN
                  dbo.aca_Alumno ON dbo.aca_CondicionalMatricula.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_CondicionalMatricula.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.aca_Catalogo ON dbo.aca_CondicionalMatricula.IdCatalogoCONDIC = dbo.aca_Catalogo.IdCatalogo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_CondicionalMatricula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'0
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_CondicionalMatricula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[30] 2[11] 3) )"
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
         Begin Table = "aca_CondicionalMatricula"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 296
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo"
            Begin Extent = 
               Top = 228
               Left = 423
               Bottom = 391
               Right = 668
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 0
               Left = 776
               Bottom = 163
               Right = 1021
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 7
               Left = 1069
               Bottom = 170
               Right = 1343
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Catalogo"
            Begin Extent = 
               Top = 174
               Left = 1104
               Bottom = 337
               Right = 1349
            End
            DisplayFlags = 280
            TopColumn = 4
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1704
         Width = 1884
         Width = 120', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_CondicionalMatricula';

