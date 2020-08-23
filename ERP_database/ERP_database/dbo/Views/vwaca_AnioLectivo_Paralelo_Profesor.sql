CREATE VIEW dbo.vwaca_AnioLectivo_Paralelo_Profesor
AS
SELECT dbo.aca_AnioLectivo_Paralelo_Profesor.IdEmpresa, dbo.aca_AnioLectivo_Paralelo_Profesor.IdAnio, dbo.aca_AnioLectivo_Paralelo_Profesor.IdSede, dbo.aca_AnioLectivo_Paralelo_Profesor.IdNivel, 
                  dbo.aca_AnioLectivo_Paralelo_Profesor.IdJornada, dbo.aca_AnioLectivo_Paralelo_Profesor.IdCurso, dbo.aca_AnioLectivo_Paralelo_Profesor.IdMateria, dbo.aca_AnioLectivo_Paralelo_Profesor.IdParalelo, 
                  dbo.aca_AnioLectivo_Paralelo_Profesor.IdProfesor, dbo.aca_Profesor.IdPersona, dbo.tb_persona.pe_nombreCompleto, dbo.aca_AnioLectivo_Curso_Materia.NomMateria, dbo.aca_Profesor.IdUsuario, 
                  dbo.aca_AnioLectivo_Curso_Materia.IdCatalogoTipoCalificacion
FROM     dbo.aca_AnioLectivo_Curso_Materia RIGHT OUTER JOIN
                  dbo.tb_persona INNER JOIN
                  dbo.aca_Profesor ON dbo.tb_persona.IdPersona = dbo.aca_Profesor.IdPersona RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Paralelo_Profesor ON dbo.aca_Profesor.IdEmpresa = dbo.aca_AnioLectivo_Paralelo_Profesor.IdEmpresa AND dbo.aca_Profesor.IdProfesor = dbo.aca_AnioLectivo_Paralelo_Profesor.IdProfesor ON 
                  dbo.aca_AnioLectivo_Curso_Materia.IdEmpresa = dbo.aca_AnioLectivo_Paralelo_Profesor.IdEmpresa AND dbo.aca_AnioLectivo_Curso_Materia.IdAnio = dbo.aca_AnioLectivo_Paralelo_Profesor.IdAnio AND 
                  dbo.aca_AnioLectivo_Curso_Materia.IdSede = dbo.aca_AnioLectivo_Paralelo_Profesor.IdSede AND dbo.aca_AnioLectivo_Curso_Materia.IdNivel = dbo.aca_AnioLectivo_Paralelo_Profesor.IdNivel AND 
                  dbo.aca_AnioLectivo_Curso_Materia.IdJornada = dbo.aca_AnioLectivo_Paralelo_Profesor.IdJornada AND dbo.aca_AnioLectivo_Curso_Materia.IdCurso = dbo.aca_AnioLectivo_Paralelo_Profesor.IdCurso AND 
                  dbo.aca_AnioLectivo_Curso_Materia.IdMateria = dbo.aca_AnioLectivo_Paralelo_Profesor.IdMateria
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Paralelo_Profesor';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[51] 4[3] 2[16] 3) )"
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
         Left = -270
      End
      Begin Tables = 
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 262
               Left = 1196
               Bottom = 425
               Right = 1470
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "aca_Profesor"
            Begin Extent = 
               Top = 1
               Left = 1011
               Bottom = 274
               Right = 1256
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "aca_AnioLectivo_Paralelo_Profesor"
            Begin Extent = 
               Top = 1
               Left = 368
               Bottom = 374
               Right = 607
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Curso_Materia"
            Begin Extent = 
               Top = 0
               Left = 717
               Bottom = 329
               Right = 961
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
      Begin ColumnWidths = 15
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
         Width = 2424
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Paralelo_Profesor';










GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'         NewValue = 1170
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Paralelo_Profesor';











