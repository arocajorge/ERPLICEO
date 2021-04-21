CREATE VIEW dbo.vwaca_AnioLectivo_Curso_Plantilla_Parametrizacion
AS
SELECT dbo.aca_AnioLectivo_Curso_Plantilla.IdEmpresa, dbo.aca_AnioLectivo_Curso_Plantilla.IdAnio, dbo.aca_AnioLectivo_Curso_Plantilla.IdSede, dbo.aca_AnioLectivo_Curso_Plantilla.IdNivel, dbo.aca_AnioLectivo_Curso_Plantilla.IdJornada, 
                  dbo.aca_AnioLectivo_Curso_Plantilla.IdCurso, dbo.aca_AnioLectivo_Curso_Plantilla.IdPlantilla, pr.IdRubro, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, p.NomPlantilla, ar.NomRubro, sn.NomNivel, sn.NomSede, 
                  dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdCtaCbleDebe, dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdCtaCbleHaber, 
                  dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdCtaCbleDebe + ' - ' + ct_planctaDebe.pc_Cuenta AS pc_CuentaDebe, 
                  dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdCtaCbleHaber + ' - ' + ct_planctaHaber.pc_Cuenta AS pc_CuentaHaber
FROM     dbo.ct_plancta AS ct_planctaHaber WITH (nolock) RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion WITH (nolock) ON ct_planctaHaber.IdCtaCble = dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdCtaCbleHaber AND 
                  ct_planctaHaber.IdEmpresa = dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdEmpresa LEFT OUTER JOIN
                  dbo.ct_plancta AS ct_planctaDebe WITH (nolock) ON dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdEmpresa = ct_planctaDebe.IdEmpresa AND 
                  dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdCtaCbleDebe = ct_planctaDebe.IdCtaCble RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) INNER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON jc.IdEmpresa = nj.IdEmpresa AND jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel AND jc.IdJornada = nj.IdJornada INNER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON nj.IdEmpresa = sn.IdEmpresa AND nj.IdAnio = sn.IdAnio AND nj.IdSede = sn.IdSede AND nj.IdNivel = sn.IdNivel RIGHT OUTER JOIN
                  dbo.aca_Plantilla AS p WITH (nolock) INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Plantilla WITH (nolock) ON p.IdEmpresa = dbo.aca_AnioLectivo_Curso_Plantilla.IdEmpresa AND p.IdAnio = dbo.aca_AnioLectivo_Curso_Plantilla.IdAnio AND 
                  p.IdPlantilla = dbo.aca_AnioLectivo_Curso_Plantilla.IdPlantilla INNER JOIN
                  dbo.aca_Plantilla_Rubro AS pr WITH (nolock) ON p.IdEmpresa = pr.IdEmpresa AND p.IdAnio = pr.IdAnio AND p.IdPlantilla = pr.IdPlantilla INNER JOIN
                  dbo.aca_AnioLectivo_Rubro AS ar WITH (nolock) ON pr.IdEmpresa = ar.IdEmpresa AND pr.IdAnio = ar.IdAnio AND pr.IdRubro = ar.IdRubro ON jc.IdEmpresa = dbo.aca_AnioLectivo_Curso_Plantilla.IdEmpresa AND 
                  jc.IdAnio = dbo.aca_AnioLectivo_Curso_Plantilla.IdAnio AND jc.IdSede = dbo.aca_AnioLectivo_Curso_Plantilla.IdSede AND jc.IdNivel = dbo.aca_AnioLectivo_Curso_Plantilla.IdNivel AND 
                  jc.IdJornada = dbo.aca_AnioLectivo_Curso_Plantilla.IdJornada AND jc.IdCurso = dbo.aca_AnioLectivo_Curso_Plantilla.IdCurso ON 
                  dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdEmpresa = dbo.aca_AnioLectivo_Curso_Plantilla.IdEmpresa AND dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdAnio = dbo.aca_AnioLectivo_Curso_Plantilla.IdAnio AND 
                  dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdSede = dbo.aca_AnioLectivo_Curso_Plantilla.IdSede AND dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdNivel = dbo.aca_AnioLectivo_Curso_Plantilla.IdNivel AND 
                  dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdJornada = dbo.aca_AnioLectivo_Curso_Plantilla.IdJornada AND 
                  dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdCurso = dbo.aca_AnioLectivo_Curso_Plantilla.IdCurso AND dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdPlantilla = dbo.aca_AnioLectivo_Curso_Plantilla.IdPlantilla AND 
                  dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.IdRubro = pr.IdRubro
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Curso_Plantilla_Parametrizacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Curso_Plantilla"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 328
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pr"
            Begin Extent = 
               Top = 140
               Left = 564
               Bottom = 270
               Right = 760
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "ar"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 234
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
      Begin ColumnWidths = 21
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
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2880
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Curso_Plantilla_Parametrizacion';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[30] 4[9] 2[43] 3) )"
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
         Begin Table = "ct_planctaHaber"
            Begin Extent = 
               Top = 0
               Left = 289
               Bottom = 163
               Right = 533
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Curso_Plantilla_Parametrizacion"
            Begin Extent = 
               Top = 12
               Left = 920
               Bottom = 256
               Right = 1093
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "ct_planctaDebe"
            Begin Extent = 
               Top = 0
               Left = 1109
               Bottom = 163
               Right = 1353
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "jc"
            Begin Extent = 
               Top = 0
               Left = 71
               Bottom = 130
               Right = 241
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "nj"
            Begin Extent = 
               Top = 0
               Left = 525
               Bottom = 130
               Right = 695
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "sn"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 27
               Left = 739
               Bottom = 157
               Right = 948
            End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Curso_Plantilla_Parametrizacion';



