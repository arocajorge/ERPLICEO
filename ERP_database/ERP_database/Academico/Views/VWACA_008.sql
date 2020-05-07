CREATE VIEW Academico.VWACA_008
AS
SELECT 1 AS Num, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.OrdenCurso, jc.NomCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, p.pe_sexo, 1 AS Cantidad, dbo.aca_Matricula.IdEmpresa, 
                  dbo.aca_Matricula.IdMatricula, dbo.aca_Matricula.IdAnio, dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdNivel, dbo.aca_Matricula.IdJornada, dbo.aca_Matricula.IdCurso, dbo.aca_Matricula.IdParalelo, dbo.aca_Matricula.Fecha, 
                  dbo.aca_Plantilla.NomPlantilla, dbo.aca_Plantilla.IdPlantilla, a.Descripcion, p.pe_nombreCompleto, alu.Codigo AS CodigoAlumno, dbo.aca_Plantilla.IdTipoPlantilla, dbo.aca_PlantillaTipo.NomPlantillaTipo, 
                  CASE WHEN dbo.aca_AlumnoRetiro.IdRetiro IS NULL THEN 0 ELSE dbo.aca_AlumnoRetiro.IdRetiro END AS IdRetiro, CASE WHEN dbo.aca_AlumnoRetiro.IdRetiro IS NULL THEN '' ELSE 'RETIRADO' END AS EstaRetirado
FROM     dbo.aca_Alumno AS alu INNER JOIN
                  dbo.aca_Matricula ON alu.IdEmpresa = dbo.aca_Matricula.IdEmpresa AND alu.IdAlumno = dbo.aca_Matricula.IdAlumno INNER JOIN
                  dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN
                  dbo.aca_Plantilla ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Plantilla.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.aca_Plantilla.IdAnio AND dbo.aca_Matricula.IdPlantilla = dbo.aca_Plantilla.IdPlantilla LEFT OUTER JOIN
                  dbo.aca_AlumnoRetiro ON dbo.aca_Matricula.IdEmpresa = dbo.aca_AlumnoRetiro.IdEmpresa AND dbo.aca_Matricula.IdMatricula = dbo.aca_AlumnoRetiro.IdMatricula AND dbo.aca_AlumnoRetiro.Estado = 1 LEFT OUTER JOIN
                  dbo.aca_PlantillaTipo ON dbo.aca_Plantilla.IdEmpresa = dbo.aca_PlantillaTipo.IdEmpresa AND dbo.aca_Plantilla.IdTipoPlantilla = dbo.aca_PlantillaTipo.IdTipoPlantilla LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS a INNER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn ON a.IdEmpresa = sn.IdEmpresa AND a.IdAnio = sn.IdAnio INNER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel INNER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
                  dbo.aca_Matricula.IdEmpresa = cp.IdEmpresa AND dbo.aca_Matricula.IdAnio = cp.IdAnio AND dbo.aca_Matricula.IdSede = cp.IdSede AND dbo.aca_Matricula.IdNivel = cp.IdNivel AND dbo.aca_Matricula.IdJornada = cp.IdJornada AND 
                  dbo.aca_Matricula.IdCurso = cp.IdCurso AND dbo.aca_Matricula.IdParalelo = cp.IdParalelo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_008';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[12] 2[29] 3) )"
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
               Top = 7
               Left = 48
               Bottom = 170
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Matricula"
            Begin Extent = 
               Top = 7
               Left = 341
               Bottom = 170
               Right = 586
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 634
               Bottom = 170
               Right = 908
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Plantilla"
            Begin Extent = 
               Top = 7
               Left = 956
               Bottom = 170
               Right = 1201
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_PlantillaTipo"
            Begin Extent = 
               Top = 157
               Left = 186
               Bottom = 320
               Right = 431
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
         End
         Begin Table = "sn"
            Begin Extent = 
               Top = 175
               Left = 672
               Bottom = 338
               Right = 916
            End
            DisplayFlags = 28', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_008';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'0
            TopColumn = 0
         End
         Begin Table = "nj"
            Begin Extent = 
               Top = 175
               Left = 964
               Bottom = 338
               Right = 1208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "jc"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp"
            Begin Extent = 
               Top = 343
               Left = 340
               Bottom = 506
               Right = 584
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
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_008';





