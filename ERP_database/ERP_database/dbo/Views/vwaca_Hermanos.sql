CREATE VIEW dbo.vwaca_Hermanos
AS
SELECT f.IdEmpresa, f.IdAlumno, vw.IdAnio, vw.IdCurso, vw.Descripcion, f.IdCatalogoPAREN, f.IdPersona, vw.NomCurso, vw.NomParalelo, p.pe_nombreCompleto
FROM     dbo.aca_Matricula AS m WITH (nolock) INNER JOIN
                  dbo.vwaca_AnioLectivo_Curso_Paralelo AS vw WITH (nolock) ON m.IdEmpresa = vw.IdEmpresa AND m.IdAnio = vw.IdAnio AND m.IdSede = vw.IdSede AND m.IdNivel = vw.IdNivel AND m.IdJornada = vw.IdJornada AND 
                  m.IdCurso = vw.IdCurso AND m.IdParalelo = vw.IdParalelo RIGHT OUTER JOIN
                  dbo.aca_Familia AS f WITH (nolock) ON m.IdAlumno = f.IdAlumno AND m.IdEmpresa = f.IdEmpresa LEFT OUTER JOIN
                  dbo.tb_persona AS p WITH (nolock) INNER JOIN
                  dbo.aca_Alumno AS al WITH (nolock) ON p.IdPersona = al.IdPersona ON f.IdAlumno = al.IdAlumno AND f.IdEmpresa = al.IdEmpresa
WHERE  (f.IdCatalogoPAREN = 10) OR
                  (f.IdCatalogoPAREN = 11)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Hermanos';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'  Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Hermanos';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[53] 4[8] 2[17] 3) )"
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
         Left = -120
      End
      Begin Tables = 
         Begin Table = "m"
            Begin Extent = 
               Top = 0
               Left = 826
               Bottom = 284
               Right = 1071
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "vw"
            Begin Extent = 
               Top = 7
               Left = 1229
               Bottom = 395
               Right = 1537
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "f"
            Begin Extent = 
               Top = 7
               Left = 489
               Bottom = 329
               Right = 734
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 19
               Left = 115
               Bottom = 182
               Right = 389
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "al"
            Begin Extent = 
               Top = 217
               Left = 181
               Bottom = 446
               Right = 426
            End
            DisplayFlags = 280
            TopColumn = 19
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 12
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1356
         Width = 2280
         Width = 2880
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
       ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Hermanos';





