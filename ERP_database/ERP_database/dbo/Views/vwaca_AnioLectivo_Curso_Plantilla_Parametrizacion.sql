CREATE VIEW dbo.vwaca_AnioLectivo_Curso_Plantilla_Parametrizacion
AS
SELECT        dbo.aca_AnioLectivo_Curso_Plantilla.IdEmpresa, dbo.aca_AnioLectivo_Curso_Plantilla.IdAnio, dbo.aca_AnioLectivo_Curso_Plantilla.IdSede, dbo.aca_AnioLectivo_Curso_Plantilla.IdNivel, 
                         dbo.aca_AnioLectivo_Curso_Plantilla.IdJornada, dbo.aca_AnioLectivo_Curso_Plantilla.IdCurso, dbo.aca_AnioLectivo_Curso_Plantilla.IdPlantilla, dbo.aca_Plantilla_Rubro.IdRubro, 
                         dbo.aca_AnioLectivo_NivelAcademico_Jornada.NomJornada, dbo.aca_AnioLectivo_NivelAcademico_Jornada.OrdenJornada, dbo.aca_AnioLectivo_Jornada_Curso.NomCurso, dbo.aca_AnioLectivo_Jornada_Curso.OrdenCurso, 
                         dbo.aca_Plantilla.NomPlantilla, dbo.aca_AnioLectivo_Rubro.NomRubro, dbo.aca_AnioLectivo_Sede_NivelAcademico.NomNivel, dbo.aca_AnioLectivo_Sede_NivelAcademico.NomSede, 
                         dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdCtaCbleDebe, dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdCtaCbleHaber
FROM            dbo.aca_AnioLectivo_Jornada_Curso INNER JOIN
                         dbo.aca_AnioLectivo_NivelAcademico_Jornada ON dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa = dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdEmpresa AND 
                         dbo.aca_AnioLectivo_Jornada_Curso.IdAnio = dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdAnio AND dbo.aca_AnioLectivo_Jornada_Curso.IdSede = dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdSede AND 
                         dbo.aca_AnioLectivo_Jornada_Curso.IdNivel = dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdNivel AND dbo.aca_AnioLectivo_Jornada_Curso.IdJornada = dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdJornada INNER JOIN
                         dbo.aca_AnioLectivo_Sede_NivelAcademico ON dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdEmpresa = dbo.aca_AnioLectivo_Sede_NivelAcademico.IdEmpresa AND 
                         dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdAnio = dbo.aca_AnioLectivo_Sede_NivelAcademico.IdAnio AND dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdSede = dbo.aca_AnioLectivo_Sede_NivelAcademico.IdSede AND
                          dbo.aca_AnioLectivo_NivelAcademico_Jornada.IdNivel = dbo.aca_AnioLectivo_Sede_NivelAcademico.IdNivel RIGHT OUTER JOIN
                         dbo.aca_Plantilla INNER JOIN
                         dbo.aca_AnioLectivo_Curso_Plantilla ON dbo.aca_Plantilla.IdEmpresa = dbo.aca_AnioLectivo_Curso_Plantilla.IdEmpresa AND dbo.aca_Plantilla.IdAnio = dbo.aca_AnioLectivo_Curso_Plantilla.IdAnio AND 
                         dbo.aca_Plantilla.IdPlantilla = dbo.aca_AnioLectivo_Curso_Plantilla.IdPlantilla INNER JOIN
                         dbo.aca_Plantilla_Rubro ON dbo.aca_Plantilla.IdEmpresa = dbo.aca_Plantilla_Rubro.IdEmpresa AND dbo.aca_Plantilla.IdAnio = dbo.aca_Plantilla_Rubro.IdAnio AND 
                         dbo.aca_Plantilla.IdPlantilla = dbo.aca_Plantilla_Rubro.IdPlantilla INNER JOIN
                         dbo.aca_AnioLectivo_Rubro ON dbo.aca_Plantilla_Rubro.IdEmpresa = dbo.aca_AnioLectivo_Rubro.IdEmpresa AND dbo.aca_Plantilla_Rubro.IdAnio = dbo.aca_AnioLectivo_Rubro.IdAnio AND 
                         dbo.aca_Plantilla_Rubro.IdRubro = dbo.aca_AnioLectivo_Rubro.IdRubro ON dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa = dbo.aca_AnioLectivo_Curso_Plantilla.IdEmpresa AND 
                         dbo.aca_AnioLectivo_Jornada_Curso.IdAnio = dbo.aca_AnioLectivo_Curso_Plantilla.IdAnio AND dbo.aca_AnioLectivo_Jornada_Curso.IdSede = dbo.aca_AnioLectivo_Curso_Plantilla.IdSede AND 
                         dbo.aca_AnioLectivo_Jornada_Curso.IdNivel = dbo.aca_AnioLectivo_Curso_Plantilla.IdNivel AND dbo.aca_AnioLectivo_Jornada_Curso.IdJornada = dbo.aca_AnioLectivo_Curso_Plantilla.IdJornada AND 
                         dbo.aca_AnioLectivo_Jornada_Curso.IdCurso = dbo.aca_AnioLectivo_Curso_Plantilla.IdCurso LEFT OUTER JOIN
                         dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion ON dbo.aca_AnioLectivo_Curso_Plantilla.IdEmpresa = dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdEmpresa AND 
                         dbo.aca_AnioLectivo_Curso_Plantilla.IdAnio = dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdAnio AND dbo.aca_AnioLectivo_Curso_Plantilla.IdSede = dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdSede AND
                          dbo.aca_AnioLectivo_Curso_Plantilla.IdNivel = dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdNivel AND 
                         dbo.aca_AnioLectivo_Curso_Plantilla.IdJornada = dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdJornada AND 
                         dbo.aca_AnioLectivo_Curso_Plantilla.IdCurso = dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdCurso AND 
                         dbo.aca_AnioLectivo_Curso_Plantilla.IdPlantilla = dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdPlantilla AND dbo.aca_Plantilla_Rubro.IdRubro = dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdRubro
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Curso_Plantilla_Parametrizacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'02
               Left = 38
               Bottom = 532
               Right = 234
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Curso_Plantilla_Parametrizacion"
            Begin Extent = 
               Top = 70
               Left = 857
               Bottom = 314
               Right = 1030
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
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2880
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Curso_Plantilla_Parametrizacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[29] 4[43] 2[9] 3) )"
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
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_NivelAcademico_Jornada"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Sede_NivelAcademico"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Plantilla"
            Begin Extent = 
               Top = 138
               Left = 246
               Bottom = 268
               Right = 455
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Curso_Plantilla"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Plantilla_Rubro"
            Begin Extent = 
               Top = 270
               Left = 246
               Bottom = 400
               Right = 442
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Rubro"
            Begin Extent = 
               Top = 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Curso_Plantilla_Parametrizacion';

