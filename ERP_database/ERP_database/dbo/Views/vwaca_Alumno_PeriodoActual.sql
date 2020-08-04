CREATE VIEW dbo.vwaca_Alumno_PeriodoActual
AS
SELECT a.IdEmpresa, a.Codigo, a.IdAlumno, e.IdMatricula, b.pe_nombreCompleto AS NombreAlumno, b.pe_cedulaRuc, c.pe_nombreCompleto AS NombreRepresentante, c.Correo AS CorreoRepresentante, 
                  d.pe_nombreCompleto AS NombreEmiteFactura, d.Correo AS CorreoEmiteFactura, c.Celular AS CelularRepresentante, d.Celular AS CelularEmiteFactura, c.Telefono AS TelefonoRepresentante, d.Telefono AS TelefonoEmiteFactura, 
                  e.IdAnio, e.IdSede, e.IdNivel, e.IdJornada, e.IdCurso, e.IdParalelo, sn.NomSede, sn.NomNivel, nj.NomJornada, jc.NomCurso, cp.NomParalelo, ISNULL(g.Saldo, 0) AS Saldo, ISNULL(g.SaldoProntoPago, 0) AS SaldoProntoPago, 
                  ISNULL(g.CantDeudas, 0) AS CantDeudas, j.NomPlantillaTipo
FROM     dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND 
                  jc.IdCurso = cp.IdCurso RIGHT OUTER JOIN
                  dbo.aca_Alumno AS a INNER JOIN
                  dbo.tb_persona AS b ON a.IdPersona = b.IdPersona LEFT OUTER JOIN
                      (SELECT xx.IdEmpresa, xx.IdAlumno, xx.Telefono, xx.Celular, xx.Correo, xxx.pe_nombreCompleto
                       FROM      dbo.aca_Familia AS xx LEFT OUTER JOIN
                                         dbo.tb_persona AS xxx ON xx.IdPersona = xxx.IdPersona
                       WHERE   (xx.EsRepresentante = 1) AND (xx.Estado = 1)) AS c ON a.IdEmpresa = c.IdEmpresa AND a.IdAlumno = c.IdAlumno LEFT OUTER JOIN
                      (SELECT xx.IdEmpresa, xx.IdAlumno, xx.Telefono, xx.Celular, xx.Correo, xxx.pe_nombreCompleto
                       FROM      dbo.aca_Familia AS xx LEFT OUTER JOIN
                                         dbo.tb_persona AS xxx ON xx.IdPersona = xxx.IdPersona
                       WHERE   (xx.SeFactura = 1) AND (xx.Estado = 1)) AS d ON a.IdEmpresa = d.IdEmpresa AND a.IdAlumno = d.IdAlumno INNER JOIN
                  dbo.aca_Matricula AS e ON a.IdEmpresa = e.IdEmpresa AND a.IdAlumno = e.IdAlumno INNER JOIN
                  dbo.aca_AnioLectivo AS f ON e.IdEmpresa = f.IdEmpresa AND e.IdAnio = f.IdAnio ON cp.IdEmpresa = e.IdEmpresa AND cp.IdAnio = e.IdAnio AND cp.IdSede = e.IdSede AND cp.IdNivel = e.IdNivel AND cp.IdJornada = e.IdJornada AND 
                  cp.IdCurso = e.IdCurso AND cp.IdParalelo = e.IdParalelo LEFT OUTER JOIN
                      (SELECT IdEmpresa, IdAlumno, SUM(Saldo) AS Saldo, SUM(ValorProntoPago - TotalxCobrado) AS SaldoProntoPago, COUNT(*) AS CantDeudas
                       FROM      dbo.vwcxc_cartera_x_cobrar AS xy
                       GROUP BY IdEmpresa, IdAlumno) AS g ON g.IdEmpresa = a.IdEmpresa AND g.IdAlumno = a.IdAlumno LEFT OUTER JOIN
                  dbo.aca_Plantilla AS i ON e.IdEmpresa = i.IdEmpresa AND e.IdAnio = i.IdAnio AND e.IdPlantilla = i.IdPlantilla LEFT OUTER JOIN
                  dbo.aca_PlantillaTipo AS j ON i.IdEmpresa = j.IdEmpresa AND i.IdTipoPlantilla = j.IdTipoPlantilla
WHERE  (f.EnCurso = 1) AND (f.Estado = 1) AND (a.Estado = 1) AND (NOT EXISTS
                      (SELECT IdEmpresa
                       FROM      dbo.aca_AlumnoRetiro AS xx
                       WHERE   (e.IdEmpresa = IdEmpresa) AND (e.IdMatricula = IdMatricula) AND (Estado = 1)))
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Alumno_PeriodoActual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'End
         Begin Table = "f"
            Begin Extent = 
               Top = 1519
               Left = 48
               Bottom = 1682
               Right = 331
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "g"
            Begin Extent = 
               Top = 1687
               Left = 48
               Bottom = 1850
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "i"
            Begin Extent = 
               Top = 1855
               Left = 48
               Bottom = 2018
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "j"
            Begin Extent = 
               Top = 2023
               Left = 48
               Bottom = 2186
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 7
               Left = 340
               Bottom = 170
               Right = 584
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 7
               Left = 632
               Bottom = 170
               Right = 876
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
      Begin ColumnWidths = 9
         Width = 284
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
         Alias = 2748
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Alumno_PeriodoActual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[18] 4[35] 2[29] 3) )"
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
         Begin Table = "sn"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "nj"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 292
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
               Top = 511
               Left = 48
               Bottom = 674
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "e"
            Begin Extent = 
               Top = 1351
               Left = 48
               Bottom = 1514
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Alumno_PeriodoActual';

