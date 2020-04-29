CREATE VIEW dbo.vwaca_MatriculaCalificacionParcial
AS
SELECT dbo.aca_MatriculaCalificacionParcial.IdEmpresa, dbo.aca_MatriculaCalificacionParcial.IdMatricula, dbo.aca_Matricula.IdAnio, dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdNivel, dbo.aca_Matricula.IdJornada, dbo.aca_Matricula.IdCurso, 
                  dbo.aca_Matricula.IdParalelo, dbo.aca_Matricula.IdAlumno, dbo.tb_persona.pe_nombreCompleto, dbo.aca_MatriculaCalificacionParcial.IdCatalogoParcial, dbo.aca_MatriculaCalificacionParcial.IdProfesor, 
                  dbo.aca_MatriculaCalificacionParcial.IdMateria, dbo.aca_MatriculaCalificacionParcial.Calificacion1, dbo.aca_MatriculaCalificacionParcial.Calificacion2, dbo.aca_MatriculaCalificacionParcial.Calificacion3, 
                  dbo.aca_MatriculaCalificacionParcial.Calificacion4, dbo.aca_MatriculaCalificacionParcial.Evaluacion, dbo.aca_MatriculaCalificacionParcial.Remedial1, dbo.aca_MatriculaCalificacionParcial.Remedial2, 
                  dbo.aca_MatriculaCalificacionParcial.Conducta, dbo.aca_MatriculaCalificacionParcial.MotivoCalificacion, dbo.aca_MatriculaCalificacionParcial.MotivoConducta, dbo.aca_MatriculaCalificacionParcial.AccionRemedial, 
                  dbo.aca_Alumno.Codigo, dbo.aca_MatriculaCalificacion.CalificacionP1, dbo.aca_MatriculaCalificacion.CalificacionP2, dbo.aca_MatriculaCalificacion.CalificacionP3, dbo.aca_MatriculaCalificacion.CalificacionP4, 
                  dbo.aca_MatriculaCalificacion.CalificacionP5, dbo.aca_MatriculaCalificacion.CalificacionP6
FROM     dbo.aca_MatriculaCalificacionParcial INNER JOIN
                  dbo.aca_Matricula ON dbo.aca_MatriculaCalificacionParcial.IdEmpresa = dbo.aca_Matricula.IdEmpresa AND dbo.aca_MatriculaCalificacionParcial.IdMatricula = dbo.aca_Matricula.IdMatricula INNER JOIN
                  dbo.aca_Alumno ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_Matricula.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.aca_MatriculaCalificacion ON dbo.aca_MatriculaCalificacionParcial.IdEmpresa = dbo.aca_MatriculaCalificacion.IdEmpresa AND dbo.aca_MatriculaCalificacionParcial.IdMatricula = dbo.aca_MatriculaCalificacion.IdMatricula AND 
                  dbo.aca_MatriculaCalificacionParcial.IdMateria = dbo.aca_MatriculaCalificacion.IdMateria AND dbo.aca_MatriculaCalificacionParcial.IdProfesor = dbo.aca_MatriculaCalificacion.IdProfesor
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaCalificacionParcial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'   Width = 1200
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaCalificacionParcial';




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
         Begin Table = "aca_MatriculaCalificacionParcial"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 254
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Matricula"
            Begin Extent = 
               Top = 226
               Left = 768
               Bottom = 459
               Right = 1013
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 7
               Left = 633
               Bottom = 170
               Right = 878
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
         Begin Table = "aca_MatriculaCalificacion"
            Begin Extent = 
               Top = 36
               Left = 360
               Bottom = 430
               Right = 616
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
      Begin ColumnWidths = 26
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
      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaCalificacionParcial';



