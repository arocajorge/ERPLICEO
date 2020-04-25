CREATE VIEW dbo.vwaca_MatriculaConducta
AS
SELECT dbo.aca_MatriculaConducta.IdEmpresa, dbo.aca_MatriculaConducta.IdMatricula, dbo.aca_Matricula.IdAlumno, dbo.aca_Matricula.IdAnio, dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdNivel, dbo.aca_Matricula.IdJornada, 
                  dbo.aca_Matricula.IdCurso, dbo.aca_Matricula.IdParalelo, dbo.vwaca_AnioLectivo_Curso_Paralelo.Descripcion, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomSede, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomNivel, 
                  dbo.vwaca_AnioLectivo_Curso_Paralelo.NomJornada, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomCurso, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomParalelo, dbo.vwaca_AnioLectivo_Curso_Paralelo.OrdenParalelo, 
                  dbo.vwaca_AnioLectivo_Curso_Paralelo.OrdenCurso, dbo.vwaca_AnioLectivo_Curso_Paralelo.OrdenJornada, dbo.vwaca_AnioLectivo_Curso_Paralelo.OrdenNivel, dbo.tb_persona.pe_nombreCompleto, 
                  dbo.aca_MatriculaConducta.SecuenciaPromedioP1, dbo.aca_MatriculaConducta.PromedioP1, dbo.aca_MatriculaConducta.SecuenciaPromedioFinalP1, dbo.aca_MatriculaConducta.PromedioFinalP1, 
                  dbo.aca_MatriculaConducta.SecuenciaPromedioP2, dbo.aca_MatriculaConducta.PromedioP2, dbo.aca_MatriculaConducta.SecuenciaPromedioFinalP2, dbo.aca_MatriculaConducta.PromedioFinalP2, 
                  dbo.aca_MatriculaConducta.SecuenciaPromedioP3, dbo.aca_MatriculaConducta.PromedioP3, dbo.aca_MatriculaConducta.SecuenciaPromedioFinalP3, dbo.aca_MatriculaConducta.PromedioFinalP3, 
                  dbo.aca_MatriculaConducta.SecuenciaPromedioQ1, dbo.aca_MatriculaConducta.PromedioQ1, dbo.aca_MatriculaConducta.SecuenciaPromedioFinalQ1, dbo.aca_MatriculaConducta.PromedioFinalQ1, 
                  dbo.aca_MatriculaConducta.SecuenciaPromedioP4, dbo.aca_MatriculaConducta.PromedioP4, dbo.aca_MatriculaConducta.SecuenciaPromedioFinalP4, dbo.aca_MatriculaConducta.PromedioFinalP4, 
                  dbo.aca_MatriculaConducta.SecuenciaPromedioP5, dbo.aca_MatriculaConducta.PromedioP5, dbo.aca_MatriculaConducta.SecuenciaPromedioFinalP5, dbo.aca_MatriculaConducta.PromedioFinalP5, 
                  dbo.aca_MatriculaConducta.SecuenciaPromedioP6, dbo.aca_MatriculaConducta.PromedioP6, dbo.aca_MatriculaConducta.SecuenciaPromedioFinalP6, dbo.aca_MatriculaConducta.PromedioFinalP6, 
                  dbo.aca_MatriculaConducta.SecuenciaPromedioQ2, dbo.aca_MatriculaConducta.PromedioQ2, dbo.aca_MatriculaConducta.SecuenciaPromedioFinalQ2, dbo.aca_MatriculaConducta.PromedioFinalQ2, 
                  dbo.aca_MatriculaConducta.SecuenciaPromedioGeneral, dbo.aca_MatriculaConducta.PromedioGeneral, dbo.aca_MatriculaConducta.SecuenciaPromedioFinal, dbo.aca_MatriculaConducta.PromedioFinal
FROM     dbo.aca_Matricula INNER JOIN
                  dbo.aca_MatriculaConducta ON dbo.aca_Matricula.IdEmpresa = dbo.aca_MatriculaConducta.IdEmpresa AND dbo.aca_Matricula.IdMatricula = dbo.aca_MatriculaConducta.IdMatricula INNER JOIN
                  dbo.tb_persona INNER JOIN
                  dbo.aca_Alumno ON dbo.tb_persona.IdPersona = dbo.aca_Alumno.IdPersona ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_Matricula.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.vwaca_AnioLectivo_Curso_Paralelo ON dbo.aca_Matricula.IdEmpresa = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdAnio AND 
                  dbo.aca_Matricula.IdSede = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdSede AND dbo.aca_Matricula.IdNivel = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdNivel AND 
                  dbo.aca_Matricula.IdJornada = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdJornada AND dbo.aca_Matricula.IdCurso = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdCurso AND 
                  dbo.aca_Matricula.IdParalelo = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdParalelo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaConducta';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'     Width = 1200
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
         Configuration = "(H (1[27] 4[50] 2[8] 3) )"
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
         Left = -40
      End
      Begin Tables = 
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 13
               Left = 1172
               Bottom = 176
               Right = 1446
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Alumno"
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
         Begin Table = "aca_Matricula"
            Begin Extent = 
               Top = 0
               Left = 420
               Bottom = 317
               Right = 665
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_MatriculaConducta"
            Begin Extent = 
               Top = 3
               Left = 87
               Bottom = 522
               Right = 331
            End
            DisplayFlags = 280
            TopColumn = 18
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 57
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
    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaConducta';



