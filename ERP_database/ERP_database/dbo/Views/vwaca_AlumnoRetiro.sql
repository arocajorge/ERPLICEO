CREATE VIEW dbo.vwaca_AlumnoRetiro
AS
SELECT ar.IdEmpresa, ar.IdRetiro, ar.IdMatricula, m.IdAnio, m.IdSede, ar.Fecha, ar.Observacion, ar.IdCatalogoESTALU, ar.IdUsuarioAnulacion, m.IdAlumno, a.Codigo, p.pe_nombreCompleto, vw.NomSede, vw.NomNivel, vw.NomJornada, 
                  vw.NomCurso, vw.NomParalelo, p.pe_cedulaRuc, vw.Descripcion, ar.Estado
FROM     dbo.aca_AlumnoRetiro AS ar WITH (nolock) INNER JOIN
                  dbo.aca_Matricula AS m WITH (nolock) ON ar.IdEmpresa = m.IdEmpresa AND ar.IdMatricula = m.IdMatricula INNER JOIN
                  dbo.aca_Alumno AS a WITH (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN
                  dbo.tb_persona AS p WITH (nolock) ON a.IdPersona = p.IdPersona INNER JOIN
                  dbo.vwaca_AnioLectivo_Curso_Paralelo AS vw WITH (nolock) ON m.IdEmpresa = vw.IdEmpresa AND m.IdAnio = vw.IdAnio AND m.IdSede = vw.IdSede AND m.IdNivel = vw.IdNivel AND m.IdJornada = vw.IdJornada AND 
                  m.IdCurso = vw.IdCurso AND m.IdParalelo = vw.IdParalelo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AlumnoRetiro';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[24] 4[17] 2[43] 3) )"
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
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ar"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 296
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "m"
            Begin Extent = 
               Top = 7
               Left = 955
               Bottom = 314
               Right = 1200
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 340
               Bottom = 170
               Right = 585
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 633
               Bottom = 263
               Right = 907
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "vw"
            Begin Extent = 
               Top = 175
               Left = 340
               Bottom = 338
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
      Begin ColumnWidths = 10
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
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Ta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AlumnoRetiro';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'ble = 1176
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AlumnoRetiro';



