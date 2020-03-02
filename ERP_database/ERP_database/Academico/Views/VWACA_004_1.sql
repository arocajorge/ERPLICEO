CREATE VIEW Academico.VWACA_004
AS
SELECT TOP (100) PERCENT CursoMateria.IdEmpresa, CursoMateria.IdAnio, CursoMateria.IdSede, CursoMateria.IdNivel, CursoMateria.IdJornada, CursoMateria.IdCurso, CursoMateria.IdMateria, dbo.aca_AnioLectivo.Descripcion, 
                  dbo.aca_AnioLectivo.FechaDesde, dbo.aca_AnioLectivo.FechaHasta, SedeNivel.NomSede, SedeNivel.NomNivel, NivelJornada.NomJornada, NivelJornada.OrdenJornada, JornadaCurso.NomCurso, JornadaCurso.OrdenCurso, 
                  CursoMateria.NomMateria, CursoMateria.NomMateriaArea, CursoMateria.NomMateriaGrupo, CursoMateria.OrdenMateria, CursoMateria.OrdenMateriaGrupo, CursoMateria.OrdenMateriaArea, CursoMateria.EsObligatorio, 
                  SedeNivel.OrdenNivel
FROM     dbo.aca_AnioLectivo_Sede_NivelAcademico AS SedeNivel INNER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS NivelJornada INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia AS CursoMateria INNER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS JornadaCurso ON CursoMateria.IdEmpresa = JornadaCurso.IdEmpresa AND CursoMateria.IdAnio = JornadaCurso.IdAnio AND CursoMateria.IdSede = JornadaCurso.IdSede AND 
                  CursoMateria.IdNivel = JornadaCurso.IdNivel AND CursoMateria.IdJornada = JornadaCurso.IdJornada AND CursoMateria.IdCurso = JornadaCurso.IdCurso ON NivelJornada.IdEmpresa = JornadaCurso.IdEmpresa AND 
                  NivelJornada.IdAnio = JornadaCurso.IdAnio AND NivelJornada.IdSede = JornadaCurso.IdSede AND NivelJornada.IdNivel = JornadaCurso.IdNivel AND NivelJornada.IdJornada = JornadaCurso.IdJornada ON 
                  SedeNivel.IdEmpresa = NivelJornada.IdEmpresa AND SedeNivel.IdAnio = NivelJornada.IdAnio AND SedeNivel.IdSede = NivelJornada.IdSede AND SedeNivel.IdNivel = NivelJornada.IdNivel INNER JOIN
                  dbo.aca_AnioLectivo ON SedeNivel.IdEmpresa = dbo.aca_AnioLectivo.IdEmpresa AND SedeNivel.IdAnio = dbo.aca_AnioLectivo.IdAnio
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_004';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'h = 1200
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
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_004';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[45] 4[0] 2[11] 3) )"
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
         Begin Table = "SedeNivel"
            Begin Extent = 
               Top = 25
               Left = 324
               Bottom = 188
               Right = 518
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "NivelJornada"
            Begin Extent = 
               Top = 26
               Left = 582
               Bottom = 241
               Right = 776
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "CursoMateria"
            Begin Extent = 
               Top = 4
               Left = 1224
               Bottom = 280
               Right = 1452
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "JornadaCurso"
            Begin Extent = 
               Top = 3
               Left = 884
               Bottom = 258
               Right = 1078
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo"
            Begin Extent = 
               Top = 2
               Left = 10
               Bottom = 165
               Right = 255
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
      Begin ColumnWidths = 25
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
         Widt', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_004';

