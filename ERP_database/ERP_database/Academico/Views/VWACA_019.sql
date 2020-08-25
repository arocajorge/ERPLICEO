CREATE VIEW Academico.VWACA_019
AS
SELECT m.IdEmpresa, m.IdMatricula, al.Codigo, m.IdAlumno, pa.IdPersona, pa.pe_nombreCompleto AS NombreAlumno, pa.pe_cedulaRuc AS CedulaAlumno, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, sn.NomSede, 
                  a.Descripcion, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, m.IdPersonaR, prep.pe_cedulaRuc AS CedulaRep, prep.pe_nombreCompleto AS NombreRep, 
                  m.Fecha, CASE WHEN r.IdRetiro IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS EsRetirado
FROM     dbo.aca_Matricula AS m INNER JOIN
                  dbo.aca_AnioLectivo AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAnio = a.IdAnio LEFT OUTER JOIN
                  dbo.tb_persona AS prep ON m.IdPersonaR = prep.IdPersona LEFT OUTER JOIN
                  dbo.tb_persona AS pa INNER JOIN
                  dbo.aca_Alumno AS al ON pa.IdPersona = al.IdPersona ON m.IdEmpresa = al.IdEmpresa AND m.IdAlumno = al.IdAlumno LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
                  m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN
                  dbo.aca_AlumnoRetiro AS r ON m.IdEmpresa = r.IdEmpresa AND m.IdMatricula = r.IdMatricula AND r.Estado = 1
WHERE  (al.Estado = 1)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_019';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'   End
         Begin Table = "jc"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp"
            Begin Extent = 
               Top = 1351
               Left = 48
               Bottom = 1514
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "r"
            Begin Extent = 
               Top = 1519
               Left = 48
               Bottom = 1682
               Right = 292
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
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2220
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
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_019';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[19] 4[42] 2[20] 3) )"
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
               Left = 48
               Bottom = 170
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 331
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "prep"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pa"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "al"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "sn"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "nj"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
      ', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_019';

