CREATE VIEW dbo.vwaca_MatriculaCalificacionCualitativa
AS
SELECT dbo.aca_MatriculaCalificacionCualitativa.IdEmpresa, dbo.aca_MatriculaCalificacionCualitativa.IdMatricula, dbo.aca_Matricula.IdAnio, dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdNivel, dbo.aca_Matricula.IdJornada, 
                  dbo.aca_Matricula.IdCurso, dbo.aca_Matricula.IdParalelo, dbo.aca_MatriculaCalificacionCualitativa.IdMateria, dbo.aca_MatriculaCalificacionCualitativa.IdCatalogoParcial, dbo.aca_MatriculaCalificacionCualitativa.IdProfesor, 
                  dbo.aca_Matricula.IdAlumno, dbo.aca_Alumno.Codigo, dbo.tb_persona.pe_nombreCompleto, dbo.aca_MatriculaCalificacionCualitativa.IdCalificacionCualitativa, dbo.aca_MatriculaCalificacionCualitativa.Conducta, 
                  dbo.aca_MatriculaCalificacionCualitativa.MotivoConducta, dbo.aca_AnioLectivoCalificacionCualitativa.Codigo AS CodigoCalificacion, dbo.aca_AnioLectivoCalificacionCualitativa.DescripcionCorta, 
                  dbo.aca_AnioLectivoConductaEquivalencia.Letra
FROM     dbo.aca_MatriculaCalificacionCualitativa INNER JOIN
                  dbo.aca_Matricula ON dbo.aca_MatriculaCalificacionCualitativa.IdEmpresa = dbo.aca_Matricula.IdEmpresa AND dbo.aca_MatriculaCalificacionCualitativa.IdMatricula = dbo.aca_Matricula.IdMatricula INNER JOIN
                  dbo.aca_Alumno ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_Matricula.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona LEFT OUTER JOIN
                  dbo.aca_AnioLectivoConductaEquivalencia ON dbo.aca_Matricula.IdAnio = dbo.aca_AnioLectivoConductaEquivalencia.IdAnio AND 
                  dbo.aca_MatriculaCalificacionCualitativa.IdEmpresa = dbo.aca_AnioLectivoConductaEquivalencia.IdEmpresa AND 
                  dbo.aca_MatriculaCalificacionCualitativa.Conducta = dbo.aca_AnioLectivoConductaEquivalencia.Secuencia LEFT OUTER JOIN
                  dbo.aca_AnioLectivoCalificacionCualitativa ON dbo.aca_Matricula.IdAnio = dbo.aca_AnioLectivoCalificacionCualitativa.IdAnio AND 
                  dbo.aca_MatriculaCalificacionCualitativa.IdEmpresa = dbo.aca_AnioLectivoCalificacionCualitativa.IdEmpresa AND 
                  dbo.aca_MatriculaCalificacionCualitativa.IdCalificacionCualitativa = dbo.aca_AnioLectivoCalificacionCualitativa.IdCalificacionCualitativa LEFT OUTER JOIN
                  dbo.aca_AnioLectivoConductaEquivalencia AS aca_AnioLectivoConductaEquivalencia_1 ON dbo.aca_Matricula.IdAnio = aca_AnioLectivoConductaEquivalencia_1.IdAnio AND 
                  dbo.aca_MatriculaCalificacionCualitativa.IdEmpresa = aca_AnioLectivoConductaEquivalencia_1.IdEmpresa AND dbo.aca_MatriculaCalificacionCualitativa.Conducta = aca_AnioLectivoConductaEquivalencia_1.Secuencia
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaCalificacionCualitativa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'            Left = 806
               Bottom = 414
               Right = 1062
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 21
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1380
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaCalificacionCualitativa';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[54] 4[9] 2[20] 3) )"
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
         Begin Table = "aca_MatriculaCalificacionCualitativa"
            Begin Extent = 
               Top = 0
               Left = 48
               Bottom = 247
               Right = 320
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "aca_Matricula"
            Begin Extent = 
               Top = 0
               Left = 470
               Bottom = 287
               Right = 715
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 7
               Left = 661
               Bottom = 170
               Right = 906
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 7
               Left = 954
               Bottom = 170
               Right = 1228
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivoConductaEquivalencia_1"
            Begin Extent = 
               Top = 148
               Left = 839
               Bottom = 311
               Right = 1095
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivoCalificacionCualitativa"
            Begin Extent = 
               Top = 222
               Left = 337
               Bottom = 424
               Right = 592
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivoConductaEquivalencia"
            Begin Extent = 
               Top = 251
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_MatriculaCalificacionCualitativa';



