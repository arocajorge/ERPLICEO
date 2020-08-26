CREATE VIEW dbo.vwaca_AnioLectivoCalificacionHistorico
AS
SELECT dbo.aca_AnioLectivoCalificacionHistorico.IdEmpresa, dbo.aca_AnioLectivoCalificacionHistorico.IdAnio, dbo.aca_AnioLectivo.Descripcion, dbo.aca_AnioLectivoCalificacionHistorico.IdAlumno, dbo.tb_persona.pe_nombreCompleto, 
                  dbo.aca_AnioLectivoCalificacionHistorico.IdCurso, dbo.aca_AnioLectivoCalificacionHistorico.Promedio, dbo.aca_AnioLectivoCalificacionHistorico.IdEquivalenciaPromedio, dbo.aca_AnioLectivoCalificacionHistorico.Conducta, 
                  dbo.aca_AnioLectivoCalificacionHistorico.SecuenciaConducta, dbo.aca_AnioLectivoConductaEquivalencia.Letra, dbo.aca_NivelAcademico.NomNivel, dbo.aca_Curso.NomCurso, 
                  dbo.aca_AnioLectivoCalificacionHistorico.AntiguaInstitucion, dbo.aca_AnioLectivoCalificacionHistorico.IdNivel, dbo.aca_AnioLectivoEquivalenciaPromedio.Codigo
FROM     dbo.aca_AnioLectivoCalificacionHistorico INNER JOIN
                  dbo.aca_AnioLectivo ON dbo.aca_AnioLectivoCalificacionHistorico.IdEmpresa = dbo.aca_AnioLectivo.IdEmpresa AND dbo.aca_AnioLectivoCalificacionHistorico.IdAnio = dbo.aca_AnioLectivo.IdAnio INNER JOIN
                  dbo.aca_Alumno ON dbo.aca_AnioLectivoCalificacionHistorico.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_AnioLectivoCalificacionHistorico.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.aca_Curso ON dbo.aca_AnioLectivoCalificacionHistorico.IdEmpresa = dbo.aca_Curso.IdEmpresa AND dbo.aca_AnioLectivoCalificacionHistorico.IdCurso = dbo.aca_Curso.IdCurso INNER JOIN
                  dbo.aca_NivelAcademico ON dbo.aca_AnioLectivoCalificacionHistorico.IdEmpresa = dbo.aca_NivelAcademico.IdEmpresa AND dbo.aca_AnioLectivoCalificacionHistorico.IdNivel = dbo.aca_NivelAcademico.IdNivel LEFT OUTER JOIN
                  dbo.aca_AnioLectivoEquivalenciaPromedio ON dbo.aca_AnioLectivoCalificacionHistorico.IdEmpresa = dbo.aca_AnioLectivoEquivalenciaPromedio.IdEmpresa AND 
                  dbo.aca_AnioLectivoCalificacionHistorico.IdAnio = dbo.aca_AnioLectivoEquivalenciaPromedio.IdAnio AND 
                  dbo.aca_AnioLectivoCalificacionHistorico.IdEquivalenciaPromedio = dbo.aca_AnioLectivoEquivalenciaPromedio.IdEquivalenciaPromedio LEFT OUTER JOIN
                  dbo.aca_AnioLectivoConductaEquivalencia ON dbo.aca_AnioLectivoCalificacionHistorico.IdEmpresa = dbo.aca_AnioLectivoConductaEquivalencia.IdEmpresa AND 
                  dbo.aca_AnioLectivoCalificacionHistorico.IdAnio = dbo.aca_AnioLectivoConductaEquivalencia.IdAnio AND dbo.aca_AnioLectivoCalificacionHistorico.SecuenciaConducta = dbo.aca_AnioLectivoConductaEquivalencia.Secuencia
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivoCalificacionHistorico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'= 368
               Right = 1111
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivoConductaEquivalencia"
            Begin Extent = 
               Top = 46
               Left = 724
               Bottom = 209
               Right = 968
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
      Begin ColumnWidths = 16
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
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2172
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivoCalificacionHistorico';










GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[48] 4[28] 2[7] 3) )"
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
         Begin Table = "aca_AnioLectivoCalificacionHistorico"
            Begin Extent = 
               Top = 25
               Left = 272
               Bottom = 348
               Right = 516
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo"
            Begin Extent = 
               Top = 7
               Left = 7
               Bottom = 170
               Right = 252
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 2
               Left = 689
               Bottom = 165
               Right = 934
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 0
               Left = 1103
               Bottom = 163
               Right = 1377
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Curso"
            Begin Extent = 
               Top = 271
               Left = 0
               Bottom = 434
               Right = 245
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_NivelAcademico"
            Begin Extent = 
               Top = 209
               Left = 1286
               Bottom = 372
               Right = 1531
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivoEquivalenciaPromedio"
            Begin Extent = 
               Top = 173
               Left = 746
               Bottom ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivoCalificacionHistorico';









