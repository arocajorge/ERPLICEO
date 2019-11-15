CREATE VIEW dbo.vwaca_Matricula
AS
SELECT dbo.aca_Matricula.IdEmpresa, dbo.aca_Matricula.IdMatricula, dbo.aca_Matricula.Codigo, dbo.aca_Matricula.IdAlumno, dbo.aca_Alumno.IdPersona, dbo.tb_persona.pe_nombreCompleto, dbo.aca_Matricula.IdAnio, 
                  dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdNivel, dbo.aca_Matricula.IdJornada, dbo.aca_Matricula.IdCurso, dbo.aca_Matricula.IdParalelo, dbo.vwaca_AnioLectivo_Jornada_Curso.Descripcion, 
                  dbo.vwaca_AnioLectivo_Jornada_Curso.NomSede, dbo.vwaca_AnioLectivo_Jornada_Curso.NomNivel, dbo.vwaca_AnioLectivo_Jornada_Curso.NomJornada, dbo.vwaca_AnioLectivo_Jornada_Curso.NomCurso, 
                  dbo.aca_Paralelo.NomParalelo
FROM     dbo.aca_Matricula INNER JOIN
                  dbo.aca_Alumno ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_Matricula.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.vwaca_AnioLectivo_Jornada_Curso ON dbo.aca_Matricula.IdEmpresa = dbo.vwaca_AnioLectivo_Jornada_Curso.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.vwaca_AnioLectivo_Jornada_Curso.IdAnio AND 
                  dbo.aca_Matricula.IdSede = dbo.vwaca_AnioLectivo_Jornada_Curso.IdSede AND dbo.aca_Matricula.IdNivel = dbo.vwaca_AnioLectivo_Jornada_Curso.IdNivel AND 
                  dbo.aca_Matricula.IdJornada = dbo.vwaca_AnioLectivo_Jornada_Curso.IdJornada AND dbo.aca_Matricula.IdCurso = dbo.vwaca_AnioLectivo_Jornada_Curso.IdCurso INNER JOIN
                  dbo.aca_Paralelo ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Paralelo.IdEmpresa AND dbo.aca_Matricula.IdParalelo = dbo.aca_Paralelo.IdParalelo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[66] 4[3] 2[13] 3) )"
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
         Begin Table = "aca_Matricula"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 181
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 199
               Left = 65
               Bottom = 362
               Right = 310
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 377
               Left = 48
               Bottom = 540
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "vwaca_AnioLectivo_Jornada_Curso"
            Begin Extent = 
               Top = 0
               Left = 480
               Bottom = 337
               Right = 724
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "aca_Paralelo"
            Begin Extent = 
               Top = 361
               Left = 486
               Bottom = 524
               Right = 731
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 19
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
         Width = 1200', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula';

