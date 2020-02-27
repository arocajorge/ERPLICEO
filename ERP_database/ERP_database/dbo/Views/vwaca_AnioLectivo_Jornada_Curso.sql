CREATE VIEW dbo.vwaca_AnioLectivo_Jornada_Curso
AS
SELECT dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa, dbo.aca_AnioLectivo_Jornada_Curso.IdAnio, dbo.aca_AnioLectivo.Descripcion, dbo.aca_AnioLectivo_Jornada_Curso.IdSede, dbo.aca_Sede.NomSede, 
                  dbo.aca_AnioLectivo_Jornada_Curso.IdNivel, dbo.aca_NivelAcademico.NomNivel, dbo.aca_AnioLectivo_Jornada_Curso.IdJornada, dbo.aca_AnioLectivo_NivelAcademico_Jornada.NomJornada, 
                  dbo.aca_AnioLectivo_Jornada_Curso.IdCurso, dbo.aca_AnioLectivo_Jornada_Curso.NomCurso, dbo.aca_AnioLectivo_Jornada_Curso.OrdenCurso, CAST(dbo.aca_Sede.IdSede AS varchar) 
                  + '-' + dbo.aca_NivelAcademico.NomNivel + '-' + dbo.aca_AnioLectivo_NivelAcademico_Jornada.NomJornada + '-' + dbo.aca_AnioLectivo_Jornada_Curso.NomCurso AS ComboCurso, dbo.aca_AnioLectivo.BloquearMatricula
FROM     dbo.aca_AnioLectivo_Jornada_Curso INNER JOIN
                  dbo.aca_Sede ON dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa = dbo.aca_Sede.IdEmpresa AND dbo.aca_AnioLectivo_Jornada_Curso.IdSede = dbo.aca_Sede.IdSede INNER JOIN
                  dbo.aca_NivelAcademico ON dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa = dbo.aca_NivelAcademico.IdEmpresa AND dbo.aca_AnioLectivo_Jornada_Curso.IdNivel = dbo.aca_NivelAcademico.IdNivel INNER JOIN
                  dbo.aca_AnioLectivo ON dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa = dbo.aca_AnioLectivo.IdEmpresa AND dbo.aca_AnioLectivo_Jornada_Curso.IdAnio = dbo.aca_AnioLectivo.IdAnio INNER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada ON dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa = dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdEmpresa AND 
                  dbo.aca_AnioLectivo_Jornada_Curso.IdAnio = dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdAnio AND dbo.aca_AnioLectivo_Jornada_Curso.IdSede = dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdSede AND 
                  dbo.aca_AnioLectivo_Jornada_Curso.IdNivel = dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdNivel AND dbo.aca_AnioLectivo_Jornada_Curso.IdJornada = dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdJornada
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Jornada_Curso';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
         Width = 1200
         Width = 1200
         Width = 4092
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Jornada_Curso';












GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[47] 4[13] 2[18] 3) )"
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
         Top = -120
         Left = 0
      End
      Begin Tables = 
         Begin Table = "aca_AnioLectivo_Jornada_Curso"
            Begin Extent = 
               Top = 65
               Left = 105
               Bottom = 322
               Right = 349
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Sede"
            Begin Extent = 
               Top = 426
               Left = 1188
               Bottom = 589
               Right = 1433
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_NivelAcademico"
            Begin Extent = 
               Top = 223
               Left = 1191
               Bottom = 386
               Right = 1436
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo"
            Begin Extent = 
               Top = 408
               Left = 463
               Bottom = 677
               Right = 708
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "aca_AnioLectivo_NivelAcademico_Jornada"
            Begin Extent = 
               Top = 0
               Left = 768
               Bottom = 238
               Right = 1039
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
         Width = 1200', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Jornada_Curso';







