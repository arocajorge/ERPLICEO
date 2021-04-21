CREATE VIEW dbo.vwaca_AnioLectivoCalificacionHistorico
AS
SELECT ch.IdEmpresa, ch.IdAnio, a.Descripcion, ch.IdAlumno, p.pe_nombreCompleto, ch.IdCurso, ch.Promedio, ch.IdEquivalenciaPromedio, ch.Conducta, ch.SecuenciaConducta, ce.Letra, n.NomNivel, c.NomCurso, ch.AntiguaInstitucion, ch.IdNivel, 
                  ep.Codigo, ch.IdMatricula
FROM     dbo.aca_AnioLectivoCalificacionHistorico AS ch WITH (nolock) INNER JOIN
                  dbo.aca_AnioLectivo AS a WITH (nolock) ON ch.IdEmpresa = a.IdEmpresa AND ch.IdAnio = a.IdAnio INNER JOIN
                  dbo.aca_Alumno AS al WITH (nolock) ON ch.IdEmpresa = al.IdEmpresa AND ch.IdAlumno = al.IdAlumno INNER JOIN
                  dbo.tb_persona AS p WITH (nolock) ON al.IdPersona = p.IdPersona INNER JOIN
                  dbo.aca_Curso AS c WITH (nolock) ON ch.IdEmpresa = c.IdEmpresa AND ch.IdCurso = c.IdCurso INNER JOIN
                  dbo.aca_NivelAcademico AS n WITH (nolock) ON ch.IdEmpresa = n.IdEmpresa AND ch.IdNivel = n.IdNivel LEFT OUTER JOIN
                  dbo.aca_AnioLectivoEquivalenciaPromedio AS ep WITH (nolock) ON ch.IdEmpresa = ep.IdEmpresa AND ch.IdAnio = ep.IdAnio AND ch.IdEquivalenciaPromedio = ep.IdEquivalenciaPromedio LEFT OUTER JOIN
                  dbo.aca_AnioLectivoConductaEquivalencia AS ce WITH (nolock) ON ch.IdEmpresa = ce.IdEmpresa AND ch.IdAnio = ce.IdAnio AND ch.SecuenciaConducta = ce.Secuencia
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivoCalificacionHistorico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'd
         Begin Table = "ce"
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
      Begin ColumnWidths = 18
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
         Configuration = "(H (1[50] 4[8] 2[28] 3) )"
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
         Begin Table = "ch"
            Begin Extent = 
               Top = 25
               Left = 272
               Bottom = 348
               Right = 516
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 7
               Bottom = 170
               Right = 252
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "al"
            Begin Extent = 
               Top = 0
               Left = 550
               Bottom = 163
               Right = 795
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 0
               Left = 1103
               Bottom = 163
               Right = 1377
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 271
               Left = 0
               Bottom = 434
               Right = 245
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "n"
            Begin Extent = 
               Top = 209
               Left = 1286
               Bottom = 372
               Right = 1531
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ep"
            Begin Extent = 
               Top = 173
               Left = 746
               Bottom = 368
               Right = 1111
            End
            DisplayFlags = 280
            TopColumn = 0
         En', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivoCalificacionHistorico';











