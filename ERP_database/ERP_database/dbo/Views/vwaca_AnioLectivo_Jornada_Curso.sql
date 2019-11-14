CREATE VIEW dbo.vwaca_AnioLectivo_Jornada_Curso
AS
SELECT dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa, dbo.aca_AnioLectivo_Jornada_Curso.IdAnio, dbo.aca_AnioLectivo.Descripcion, dbo.aca_AnioLectivo_Jornada_Curso.IdSede, dbo.aca_Sede.NomSede, 
                  dbo.aca_AnioLectivo_Jornada_Curso.IdNivel, dbo.aca_NivelAcademico.NomNivel, dbo.aca_AnioLectivo_Jornada_Curso.IdJornada, dbo.aca_Jornada.NomJornada, dbo.aca_AnioLectivo_Jornada_Curso.IdCurso, 
                  dbo.aca_AnioLectivo_Jornada_Curso.NomCurso, dbo.aca_AnioLectivo_Jornada_Curso.OrdenCurso, 
                  dbo.aca_Sede.NomSede + '-' + dbo.aca_NivelAcademico.NomNivel + '-' + dbo.aca_Jornada.NomJornada + '-' + dbo.aca_AnioLectivo_Jornada_Curso.NomCurso AS ComboCurso
FROM     dbo.aca_AnioLectivo_Jornada_Curso INNER JOIN
                  dbo.aca_Jornada ON dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa = dbo.aca_Jornada.IdEmpresa AND dbo.aca_AnioLectivo_Jornada_Curso.IdJornada = dbo.aca_Jornada.IdJornada INNER JOIN
                  dbo.aca_Sede ON dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa = dbo.aca_Sede.IdEmpresa AND dbo.aca_AnioLectivo_Jornada_Curso.IdSede = dbo.aca_Sede.IdSede INNER JOIN
                  dbo.aca_NivelAcademico ON dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa = dbo.aca_NivelAcademico.IdEmpresa AND dbo.aca_AnioLectivo_Jornada_Curso.IdNivel = dbo.aca_NivelAcademico.IdNivel INNER JOIN
                  dbo.aca_AnioLectivo ON dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa = dbo.aca_AnioLectivo.IdEmpresa AND dbo.aca_AnioLectivo_Jornada_Curso.IdAnio = dbo.aca_AnioLectivo.IdAnio
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Jornada_Curso';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'1200
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Jornada_Curso';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[48] 4[2] 2[18] 3) )"
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
         Begin Table = "aca_AnioLectivo_Jornada_Curso"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "aca_Jornada"
            Begin Extent = 
               Top = 7
               Left = 340
               Bottom = 170
               Right = 585
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Sede"
            Begin Extent = 
               Top = 7
               Left = 633
               Bottom = 170
               Right = 878
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_NivelAcademico"
            Begin Extent = 
               Top = 208
               Left = 499
               Bottom = 371
               Right = 744
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 293
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
      Begin ColumnWidths = 14
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
         Width = ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Jornada_Curso';

