CREATE VIEW dbo.vwaca_MatriculaAsistencia
AS
SELECT ma.IdEmpresa, ma.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, a.Codigo, p.pe_nombreCompleto, ma.FInjustificadaP1, ma.FJustificadaP1, ma.AtrasosP1, ma.FInjustificadaP2, 
                  ma.FJustificadaP2, ma.AtrasosP2, ma.FInjustificadaP3, ma.FJustificadaP3, ma.AtrasosP3, ma.FInjustificadaP4, ma.FJustificadaP4, ma.AtrasosP4, ma.FInjustificadaP5, ma.FJustificadaP5, ma.AtrasosP5, ma.FInjustificadaP6, 
                  ma.FJustificadaP6, ma.AtrasosP6, dbo.aca_AnioLectivo_Curso_Paralelo.IdProfesorTutor, dbo.aca_AnioLectivo_Curso_Paralelo.IdProfesorInspector
FROM     dbo.aca_MatriculaAsistencia AS ma INNER JOIN
                  dbo.aca_Matricula AS m ON ma.IdEmpresa = m.IdEmpresa AND ma.IdMatricula = m.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN
                  dbo.tb_persona AS p ON a.IdPersona = p.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo ON m.IdEmpresa = dbo.aca_AnioLectivo_Curso_Paralelo.IdEmpresa AND m.IdAnio = dbo.aca_AnioLectivo_Curso_Paralelo.IdAnio AND m.IdSede = dbo.aca_AnioLectivo_Curso_Paralelo.IdSede AND 
                  m.IdNivel = dbo.aca_AnioLectivo_Curso_Paralelo.IdNivel AND m.IdJornada = dbo.aca_AnioLectivo_Curso_Paralelo.IdJornada AND m.IdCurso = dbo.aca_AnioLectivo_Curso_Paralelo.IdCurso AND 
                  m.IdParalelo = dbo.aca_AnioLectivo_Curso_Paralelo.IdParalelo
WHERE  (NOT EXISTS
                      (SELECT IdEmpresa
                       FROM      dbo.aca_AlumnoRetiro AS f
                       WHERE   (IdEmpresa = ma.IdEmpresa) AND (IdMatricula = ma.IdMatricula) AND (Estado = 1)))
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaAsistencia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'= 1500
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaAsistencia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[53] 4[18] 2[11] 3) )"
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
         Begin Table = "m"
            Begin Extent = 
               Top = 7
               Left = 340
               Bottom = 305
               Right = 585
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 633
               Bottom = 170
               Right = 878
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 926
               Bottom = 170
               Right = 1200
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ma"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 311
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "aca_AnioLectivo_Curso_Paralelo"
            Begin Extent = 
               Top = 200
               Left = 837
               Bottom = 413
               Right = 1081
            End
            DisplayFlags = 280
            TopColumn = 6
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 32
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
         Width = 1704
         Width = 1200
         Width = 1512
         Width = 1200
         Width ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaAsistencia';

