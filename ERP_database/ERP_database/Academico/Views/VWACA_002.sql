CREATE VIEW Academico.VWACA_002
AS
SELECT m.IdEmpresa, m.IdMatricula, m.IdAlumno, m.IdAnio, ISNULL(vw.Descripcion, '') AS Descripcion, an.EnCurso, ISNULL(vw.NomSede, '') AS NomSede, ISNULL(vw.NomNivel, '') AS NomNivel, ISNULL(vw.NomJornada, '') AS NomJornada, 
                  ISNULL(vw.NomCurso, '') AS NomCurso, ISNULL(vw.NomParalelo, '') AS NomParalelo, ISNULL(a.Codigo, '') AS CodigoAlumno, ISNULL(p.pe_nombreCompleto, '') AS NombreAlumno, p1.pe_cedulaRuc AS CedulaRep, 
                  ISNULL(p1.pe_nombreCompleto, '') AS NombreRep, ISNULL(pl.NomPlantilla, '') AS NomPlantilla, m.IdUsuarioCreacion, m.FechaCreacion
FROM     dbo.aca_Matricula AS m WITH (nolock) INNER JOIN
                  dbo.vwaca_AnioLectivo_Curso_Paralelo AS vw WITH (nolock) ON m.IdEmpresa = vw.IdEmpresa AND m.IdAnio = vw.IdAnio AND m.IdSede = vw.IdSede AND m.IdNivel = vw.IdNivel AND m.IdJornada = vw.IdJornada AND 
                  m.IdCurso = vw.IdCurso AND m.IdParalelo = vw.IdParalelo INNER JOIN
                  dbo.aca_Alumno AS a WITH (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN
                  dbo.tb_persona AS p WITH (nolock) ON a.IdPersona = p.IdPersona INNER JOIN
                  dbo.tb_persona AS p1 WITH (nolock) ON m.IdPersonaR = p1.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo AS an WITH (nolock) ON m.IdEmpresa = an.IdEmpresa AND m.IdAnio = an.IdAnio INNER JOIN
                  dbo.aca_Plantilla AS pl WITH (nolock) ON m.IdEmpresa = pl.IdEmpresa AND m.IdAnio = pl.IdAnio AND m.IdPlantilla = pl.IdPlantilla
GO



GO





GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_002';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'     End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 19
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
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_002';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Top = -1200
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
            TopColumn = 16
         End
         Begin Table = "vw"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p1"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "an"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pl"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 3
    ', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_002';



