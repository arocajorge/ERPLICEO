CREATE VIEW dbo.vwaca_MatriculaCalificacion
AS
SELECT mc.IdEmpresa, mc.IdMatricula, m.IdAlumno, a.Codigo, per.pe_nombreCompleto AS pe_nombreCompletoAlumno, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, mc.IdMateria, mc.IdProfesor, pprof.pe_nombreCompleto, 
                  mc.CalificacionP1, mc.CalificacionP2, mc.CalificacionP3, mc.PromedioQ1, mc.ExamenQ1, mc.PromedioFinalQ1, mc.CausaQ1, mc.ResolucionQ1, mc.CalificacionP4, mc.CalificacionP5, mc.CalificacionP6, mc.PromedioQ2, mc.ExamenQ2, 
                  mc.PromedioFinalQ2, mc.CausaQ2, mc.ResolucionQ2, mc.PromedioQuimestres, mc.ExamenMejoramiento, mc.CampoMejoramiento, mc.ExamenSupletorio, mc.ExamenRemedial, mc.ExamenGracia, mc.PromedioFinal, equiv.Descripcion, 
                  equiv.Codigo AS CodigoEquivalencia, mc.IdEquivalenciaPromedioP1, mc.IdEquivalenciaPromedioP2, mc.IdEquivalenciaPromedioP3, mc.IdEquivalenciaPromedioEQ1, mc.IdEquivalenciaPromedioQ1, mc.IdEquivalenciaPromedioP4, 
                  mc.IdEquivalenciaPromedioP5, mc.IdEquivalenciaPromedioP6, mc.IdEquivalenciaPromedioEQ2, mc.IdEquivalenciaPromedioQ2, mc.IdEquivalenciaPromedioPF
FROM     dbo.tb_persona AS pprof INNER JOIN
                  dbo.aca_Profesor AS pro ON pprof.IdPersona = pro.IdPersona RIGHT OUTER JOIN
                  dbo.aca_Matricula AS m INNER JOIN
                  dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN
                  dbo.tb_persona AS per ON a.IdPersona = per.IdPersona INNER JOIN
                  dbo.aca_MatriculaCalificacion AS mc ON m.IdEmpresa = mc.IdEmpresa AND m.IdMatricula = mc.IdMatricula AND m.IdEmpresa = mc.IdEmpresa ON pro.IdEmpresa = mc.IdEmpresa AND pro.IdProfesor = mc.IdProfesor LEFT OUTER JOIN
                  dbo.aca_AnioLectivoEquivalenciaPromedio AS equiv ON mc.IdEmpresa = equiv.IdEmpresa AND mc.IdEquivalenciaPromedioPF = equiv.IdEquivalenciaPromedio
WHERE  (NOT EXISTS
                      (SELECT IdEmpresa
                       FROM      dbo.aca_AlumnoRetiro AS f
                       WHERE   (IdEmpresa = mc.IdEmpresa) AND (IdMatricula = mc.IdMatricula) AND (Estado = 1)))
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaCalificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 36
         Width = 284
         Width = 1200
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
         Column = 3936
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
         Configuration = "(H (1[32] 4[30] 2[26] 3) )"
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
         Begin Table = "pprof"
            Begin Extent = 
               Top = 211
               Left = 1053
               Bottom = 374
               Right = 1327
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pro"
            Begin Extent = 
               Top = 202
               Left = 723
               Bottom = 365
               Right = 968
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "m"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 299
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 0
               Left = 707
               Bottom = 244
               Right = 952
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "per"
            Begin Extent = 
               Top = 70
               Left = 1117
               Bottom = 233
               Right = 1391
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "mc"
            Begin Extent = 
               Top = 0
               Left = 342
               Bottom = 259
               Right = 586
            End
            DisplayFlags = 280
            TopColumn = 29
         End
         Begin Table = "equiv"
            Begin Extent = 
               Top = 284
               Left = 545
               Bottom = 447
               Right = 801
            End
            DisplayFlags = 280
            TopColumn = ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaCalificacion';













