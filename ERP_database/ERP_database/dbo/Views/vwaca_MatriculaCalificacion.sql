CREATE VIEW dbo.vwaca_MatriculaCalificacion
AS
SELECT dbo.aca_MatriculaCalificacion.IdEmpresa, dbo.aca_MatriculaCalificacion.IdMatricula, dbo.aca_Matricula.IdAlumno, dbo.aca_Alumno.Codigo, tb_persona_1.pe_nombreCompleto AS pe_nombreCompletoAlumno, dbo.aca_Matricula.IdAnio, 
                  dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdNivel, dbo.aca_Matricula.IdJornada, dbo.aca_Matricula.IdCurso, dbo.aca_Matricula.IdParalelo, dbo.aca_MatriculaCalificacion.IdMateria, dbo.aca_MatriculaCalificacion.IdProfesor, 
                  dbo.tb_persona.pe_nombreCompleto
FROM     dbo.tb_persona INNER JOIN
                  dbo.aca_Profesor ON dbo.tb_persona.IdPersona = dbo.aca_Profesor.IdPersona RIGHT OUTER JOIN
                  dbo.aca_Matricula INNER JOIN
                  dbo.aca_Alumno ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_Matricula.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona AS tb_persona_1 ON dbo.aca_Alumno.IdPersona = tb_persona_1.IdPersona INNER JOIN
                  dbo.aca_MatriculaCalificacion ON dbo.aca_Matricula.IdEmpresa = dbo.aca_MatriculaCalificacion.IdEmpresa AND dbo.aca_Matricula.IdMatricula = dbo.aca_MatriculaCalificacion.IdMatricula ON 
                  dbo.aca_Profesor.IdEmpresa = dbo.aca_MatriculaCalificacion.IdEmpresa AND dbo.aca_Profesor.IdProfesor = dbo.aca_MatriculaCalificacion.IdProfesor
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaCalificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' Width = 1200
         Width = 1200
         Width = 1200
         Width = 1896
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
         Column = 1908
         Alias = 900
         Table = 2352
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaCalificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[39] 4[37] 2[8] 3) )"
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
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 194
               Left = 1056
               Bottom = 357
               Right = 1330
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Profesor"
            Begin Extent = 
               Top = 202
               Left = 723
               Bottom = 365
               Right = 968
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Matricula"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 299
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 0
               Left = 707
               Bottom = 244
               Right = 952
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona_1"
            Begin Extent = 
               Top = 70
               Left = 1117
               Bottom = 233
               Right = 1391
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_MatriculaCalificacion"
            Begin Extent = 
               Top = 43
               Left = 369
               Bottom = 302
               Right = 613
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
        ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaCalificacion';

