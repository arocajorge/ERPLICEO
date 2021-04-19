CREATE VIEW dbo.vwaca_AnioLectivo_Curso_Paralelo
AS
SELECT cp.IdEmpresa, cp.IdAnio, an.Descripcion, cp.IdSede, s.NomSede, cp.IdNivel, n.NomNivel, cp.IdJornada, j.NomJornada, cp.IdCurso, c.NomCurso, cp.IdParalelo, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, cp.IdProfesorTutor, 
                  cp.IdProfesorInspector, pr.IdPersona AS IdPersonaTutor, pr1.IdPersona AS IdPersonaInpector, p.pe_nombreCompleto AS NomTutor, pp.pe_nombreCompleto AS NomInspector, n.Orden AS OrdenNivel, j.OrdenJornada, 
                  c.OrdenCurso
FROM     dbo.tb_persona AS pp WITH (nolock) INNER JOIN
                  dbo.aca_Profesor AS pr1 WITH (nolock) ON pp.IdPersona = pr1.IdPersona RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) INNER JOIN
                  dbo.aca_AnioLectivo AS an WITH (nolock) ON cp.IdEmpresa = an.IdEmpresa AND cp.IdAnio = an.IdAnio INNER JOIN
                  dbo.aca_Sede AS s WITH (nolock) ON cp.IdEmpresa = s.IdEmpresa AND cp.IdSede = s.IdSede INNER JOIN
                  dbo.aca_Jornada AS j WITH (nolock) ON cp.IdEmpresa = j.IdEmpresa AND cp.IdJornada = j.IdJornada INNER JOIN
                  dbo.aca_Curso AS c WITH (nolock) ON cp.IdEmpresa = c.IdEmpresa AND cp.IdCurso = c.IdCurso INNER JOIN
                  dbo.aca_NivelAcademico AS n WITH (nolock) ON cp.IdEmpresa = n.IdEmpresa AND cp.IdNivel = n.IdNivel ON pr1.IdEmpresa = cp.IdEmpresa AND pr1.IdProfesor = cp.IdProfesorInspector LEFT OUTER JOIN
                  dbo.tb_persona AS p WITH (nolock) INNER JOIN
                  dbo.aca_Profesor AS pr WITH (nolock) ON p.IdPersona = pr.IdPersona ON cp.IdEmpresa = pr.IdEmpresa AND cp.IdProfesorTutor = pr.IdProfesor
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Curso_Paralelo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'        End
         Begin Table = "n"
            Begin Extent = 
               Top = 0
               Left = 877
               Bottom = 163
               Right = 1122
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 162
               Left = 882
               Bottom = 325
               Right = 1156
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pr"
            Begin Extent = 
               Top = 206
               Left = 423
               Bottom = 369
               Right = 668
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
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1500
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Curso_Paralelo';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[46] 4[7] 2[25] 3) )"
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
         Top = -120
         Left = -311
      End
      Begin Tables = 
         Begin Table = "pp"
            Begin Extent = 
               Top = 341
               Left = 880
               Bottom = 504
               Right = 1154
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pr1"
            Begin Extent = 
               Top = 365
               Left = 425
               Bottom = 528
               Right = 670
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "cp"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 353
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "an"
            Begin Extent = 
               Top = 7
               Left = 340
               Bottom = 170
               Right = 585
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s"
            Begin Extent = 
               Top = 7
               Left = 633
               Bottom = 170
               Right = 878
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "j"
            Begin Extent = 
               Top = 118
               Left = 1254
               Bottom = 281
               Right = 1499
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 0
               Left = 1145
               Bottom = 163
               Right = 1390
            End
            DisplayFlags = 280
            TopColumn = 1
 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Curso_Paralelo';







