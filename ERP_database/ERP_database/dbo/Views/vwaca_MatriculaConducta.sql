CREATE VIEW dbo.vwaca_MatriculaConducta
AS
SELECT mc.IdEmpresa, mc.IdMatricula, m.IdAlumno, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, dbo.vwaca_AnioLectivo_Curso_Paralelo.Descripcion, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomSede, 
                  dbo.vwaca_AnioLectivo_Curso_Paralelo.NomNivel, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomJornada, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomCurso, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomParalelo, 
                  dbo.vwaca_AnioLectivo_Curso_Paralelo.OrdenParalelo, dbo.vwaca_AnioLectivo_Curso_Paralelo.OrdenCurso, dbo.vwaca_AnioLectivo_Curso_Paralelo.OrdenJornada, dbo.vwaca_AnioLectivo_Curso_Paralelo.OrdenNivel, 
                  p.pe_nombreCompleto, mc.SecuenciaPromedioP1, mc.PromedioP1, mc.SecuenciaPromedioFinalP1, mc.PromedioFinalP1, mc.SecuenciaPromedioP2, mc.PromedioP2, mc.SecuenciaPromedioFinalP2, mc.PromedioFinalP2, 
                  mc.SecuenciaPromedioP3, mc.PromedioP3, mc.SecuenciaPromedioFinalP3, mc.PromedioFinalP3, mc.SecuenciaPromedioQ1, mc.PromedioQ1, mc.SecuenciaPromedioFinalQ1, mc.PromedioFinalQ1, mc.SecuenciaPromedioP4, 
                  mc.PromedioP4, mc.SecuenciaPromedioFinalP4, mc.PromedioFinalP4, mc.SecuenciaPromedioP5, mc.PromedioP5, mc.SecuenciaPromedioFinalP5, mc.PromedioFinalP5, mc.SecuenciaPromedioP6, mc.PromedioP6, 
                  mc.SecuenciaPromedioFinalP6, mc.PromedioFinalP6, mc.SecuenciaPromedioQ2, mc.PromedioQ2, mc.SecuenciaPromedioFinalQ2, mc.PromedioFinalQ2, mc.SecuenciaPromedioGeneral, mc.PromedioGeneral, mc.SecuenciaPromedioFinal, 
                  mc.PromedioFinal, mc.MotivoPromedioFinalP1, mc.MotivoPromedioFinalP2, mc.MotivoPromedioFinalP3, mc.MotivoPromedioFinalQ1, mc.MotivoPromedioFinalP4, mc.MotivoPromedioFinalP5, mc.MotivoPromedioFinalP6, 
                  mc.MotivoPromedioFinalQ2, mc.MotivoPromedioFinal
FROM     dbo.aca_Matricula AS m INNER JOIN
                  dbo.aca_MatriculaConducta AS mc ON m.IdEmpresa = mc.IdEmpresa AND m.IdMatricula = mc.IdMatricula INNER JOIN
                  dbo.tb_persona AS p INNER JOIN
                  dbo.aca_Alumno AS a ON p.IdPersona = a.IdPersona ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN
                  dbo.vwaca_AnioLectivo_Curso_Paralelo ON m.IdEmpresa = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdEmpresa AND m.IdAnio = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdAnio AND 
                  m.IdSede = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdSede AND m.IdNivel = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdNivel AND m.IdJornada = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdJornada AND 
                  m.IdCurso = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdCurso AND m.IdParalelo = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdParalelo
WHERE  (NOT EXISTS
                      (SELECT IdEmpresa
                       FROM      dbo.aca_AlumnoRetiro AS f
                       WHERE   (IdEmpresa = mc.IdEmpresa) AND (IdMatricula = mc.IdMatricula) AND (Estado = 1)))
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaConducta';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'      Width = 1200
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
         Column = 4572
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaConducta';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[53] 4[13] 2[23] 3) )"
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
         Top = -240
         Left = -40
      End
      Begin Tables = 
         Begin Table = "m"
            Begin Extent = 
               Top = 0
               Left = 420
               Bottom = 317
               Right = 665
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "mc"
            Begin Extent = 
               Top = 3
               Left = 87
               Bottom = 522
               Right = 331
            End
            DisplayFlags = 280
            TopColumn = 27
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 13
               Left = 1172
               Bottom = 176
               Right = 1446
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 11
               Left = 831
               Bottom = 174
               Right = 1076
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "vwaca_AnioLectivo_Curso_Paralelo"
            Begin Extent = 
               Top = 176
               Left = 783
               Bottom = 516
               Right = 1027
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
      Begin ColumnWidths = 66
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
         Width = 1200
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaConducta';





