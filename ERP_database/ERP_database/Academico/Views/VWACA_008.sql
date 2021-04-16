CREATE VIEW Academico.VWACA_008
AS
SELECT 1 AS Num, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, p.pe_sexo, 1 AS Cantidad, m.IdEmpresa, m.IdMatricula, m.IdAnio, 
                  m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.Fecha, pl.NomPlantilla, pl.IdPlantilla, a.Descripcion, alu.Codigo AS CodigoAlumno, p.pe_nombreCompleto, pt.IdTipoPlantilla, pt.NomPlantillaTipo, CASE WHEN ar.IdRetiro IS NULL 
                  THEN 0 ELSE ar.IdRetiro END AS IdRetiro, CASE WHEN ar.IdRetiro IS NULL THEN '' ELSE 'RETIRADO' END AS EstaRetirado
FROM     dbo.aca_Matricula AS m LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND 
                  m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON m.IdEmpresa = jc.IdEmpresa AND m.IdAnio = jc.IdAnio AND m.IdSede = jc.IdSede AND m.IdNivel = jc.IdNivel AND m.IdJornada = jc.IdJornada AND 
                  m.IdCurso = jc.IdCurso LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON m.IdEmpresa = nj.IdEmpresa AND m.IdAnio = nj.IdAnio AND m.IdSede = nj.IdSede AND m.IdNivel = nj.IdNivel AND m.IdJornada = nj.IdJornada LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel LEFT OUTER JOIN
                  dbo.aca_Plantilla AS pl WITH (nolock) ON m.IdEmpresa = pl.IdEmpresa AND m.IdPlantilla = pl.IdPlantilla LEFT OUTER JOIN
                  dbo.aca_PlantillaTipo AS pt WITH (nolock) ON pl.IdEmpresa = pt.IdEmpresa AND pl.IdTipoPlantilla = pt.IdTipoPlantilla LEFT OUTER JOIN
                  dbo.aca_Alumno AS alu WITH (nolock) ON alu.IdEmpresa = m.IdEmpresa AND alu.IdAlumno = m.IdAlumno LEFT OUTER JOIN
                  dbo.tb_persona AS p WITH (nolock) ON p.IdPersona = alu.IdPersona LEFT OUTER JOIN
                  dbo.aca_AlumnoRetiro AS ar WITH (nolock) ON m.IdEmpresa = ar.IdEmpresa AND m.IdMatricula = ar.IdMatricula AND ar.Estado = 1 LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS a WITH (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdAnio = a.IdAnio
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_008';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[15] 4[43] 2[27] 3) )"
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
         Begin Table = "alu"
            Begin Extent = 
               Top = 13
               Left = 10
               Bottom = 176
               Right = 255
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "m"
            Begin Extent = 
               Top = 0
               Left = 435
               Bottom = 163
               Right = 680
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 171
               Left = 0
               Bottom = 334
               Right = 274
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pl"
            Begin Extent = 
               Top = 0
               Left = 982
               Bottom = 163
               Right = 1227
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ar"
            Begin Extent = 
               Top = 193
               Left = 848
               Bottom = 356
               Right = 1092
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pt"
            Begin Extent = 
               Top = 0
               Left = 1291
               Bottom = 163
               Right = 1536
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 120
               Left = 1203
               Bottom = 283
               Right = 1486
            End
            DisplayFlags = 280
            TopColumn = 0
      ', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_008';










GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'   End
         Begin Table = "sn"
            Begin Extent = 
               Top = 230
               Left = 1316
               Bottom = 393
               Right = 1560
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "nj"
            Begin Extent = 
               Top = 229
               Left = 940
               Bottom = 392
               Right = 1184
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "jc"
            Begin Extent = 
               Top = 197
               Left = 667
               Bottom = 360
               Right = 911
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp"
            Begin Extent = 
               Top = 209
               Left = 391
               Bottom = 379
               Right = 635
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
      Begin ColumnWidths = 30
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
         Table = 2868
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
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_008';







