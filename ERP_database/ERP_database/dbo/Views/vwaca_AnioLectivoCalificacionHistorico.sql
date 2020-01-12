CREATE VIEW dbo.vwaca_AnioLectivoCalificacionHistorico
AS
SELECT dbo.aca_AnioLectivoCalificacionHistorico.IdEmpresa, dbo.aca_AnioLectivoCalificacionHistorico.IdAnio, dbo.aca_AnioLectivo.Descripcion, dbo.aca_AnioLectivoCalificacionHistorico.IdAlumno, dbo.tb_persona.pe_nombreCompleto, 
                  dbo.aca_AnioLectivoCalificacionHistorico.IdCurso, dbo.aca_AnioLectivo_Jornada_Curso.NomCurso, dbo.aca_AnioLectivoCalificacionHistorico.Promedio, dbo.aca_AnioLectivoCalificacionHistorico.Conducta
FROM     dbo.aca_AnioLectivoCalificacionHistorico INNER JOIN
                  dbo.aca_AnioLectivo ON dbo.aca_AnioLectivoCalificacionHistorico.IdEmpresa = dbo.aca_AnioLectivo.IdEmpresa AND dbo.aca_AnioLectivoCalificacionHistorico.IdAnio = dbo.aca_AnioLectivo.IdAnio INNER JOIN
                  dbo.aca_Alumno ON dbo.aca_AnioLectivoCalificacionHistorico.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_AnioLectivoCalificacionHistorico.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso ON dbo.aca_AnioLectivoCalificacionHistorico.IdEmpresa = dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa AND 
                  dbo.aca_AnioLectivoCalificacionHistorico.IdAnio = dbo.aca_AnioLectivo_Jornada_Curso.IdAnio AND dbo.aca_AnioLectivoCalificacionHistorico.IdCurso = dbo.aca_AnioLectivo_Jornada_Curso.IdCurso
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivoCalificacionHistorico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'= 
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivoCalificacionHistorico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[35] 4[39] 2[7] 3) )"
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
         Begin Table = "aca_AnioLectivoCalificacionHistorico"
            Begin Extent = 
               Top = 208
               Left = 315
               Bottom = 475
               Right = 559
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo"
            Begin Extent = 
               Top = 8
               Left = 129
               Bottom = 171
               Right = 374
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 0
               Left = 514
               Bottom = 163
               Right = 759
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 7
               Left = 926
               Bottom = 170
               Right = 1200
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Jornada_Curso"
            Begin Extent = 
               Top = 198
               Left = 667
               Bottom = 480
               Right = 911
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
      Begin ColumnWidths = 10
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
      End
   End
   Begin CriteriaPane ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivoCalificacionHistorico';

