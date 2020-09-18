CREATE VIEW dbo.vwaca_AnioLectivo_Paralelo_Profesor_NoAsignados
AS
SELECT aca_AnioLectivo_Curso_Materia.IdEmpresa, aca_AnioLectivo_Curso_Materia.IdAnio, aca_AnioLectivo_Curso_Materia.IdSede, aca_AnioLectivo_Curso_Materia.IdNivel, aca_AnioLectivo_Curso_Materia.IdJornada, 
                  aca_AnioLectivo_Curso_Materia.IdCurso, dbo.aca_AnioLectivo_Curso_Paralelo.IdParalelo, aca_AnioLectivo_Curso_Materia.IdMateria, aca_AnioLectivo_Curso_Materia.NomMateria, 
                  aca_AnioLectivo_Curso_Materia.IdCatalogoTipoCalificacion
FROM     dbo.aca_AnioLectivo_Curso_Materia AS aca_AnioLectivo_Curso_Materia INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo ON aca_AnioLectivo_Curso_Materia.IdEmpresa = dbo.aca_AnioLectivo_Curso_Paralelo.IdEmpresa AND aca_AnioLectivo_Curso_Materia.IdAnio = dbo.aca_AnioLectivo_Curso_Paralelo.IdAnio AND 
                  aca_AnioLectivo_Curso_Materia.IdSede = dbo.aca_AnioLectivo_Curso_Paralelo.IdSede AND aca_AnioLectivo_Curso_Materia.IdNivel = dbo.aca_AnioLectivo_Curso_Paralelo.IdNivel AND 
                  aca_AnioLectivo_Curso_Materia.IdJornada = dbo.aca_AnioLectivo_Curso_Paralelo.IdJornada AND aca_AnioLectivo_Curso_Materia.IdCurso = dbo.aca_AnioLectivo_Curso_Paralelo.IdCurso
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Paralelo_Profesor_NoAsignados';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[56] 4[5] 2[20] 3) )"
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
         Begin Table = "aca_AnioLectivo_Curso_Materia"
            Begin Extent = 
               Top = 6
               Left = 396
               Bottom = 344
               Right = 706
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Curso_Paralelo"
            Begin Extent = 
               Top = 5
               Left = 57
               Bottom = 301
               Right = 323
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
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Paralelo_Profesor_NoAsignados';



