﻿CREATE VIEW dbo.vwcxc_SeguimientoCartera
AS
SELECT dbo.cxc_SeguimientoCartera.IdEmpresa, dbo.cxc_SeguimientoCartera.IdSeguimiento, dbo.cxc_SeguimientoCartera.IdAlumno, dbo.aca_Alumno.Codigo, dbo.tb_persona.pe_nombreCompleto, dbo.cxc_SeguimientoCartera.Fecha, 
                  dbo.cxc_SeguimientoCartera.Observacion, dbo.cxc_SeguimientoCartera.Estado, dbo.cxc_SeguimientoCartera.IdMatricula, dbo.cxc_SeguimientoCartera.CorreoEnviado, dbo.cxc_SeguimientoCartera.IdUsuarioCreacion
FROM     dbo.aca_Alumno INNER JOIN
                  dbo.cxc_SeguimientoCartera ON dbo.aca_Alumno.IdEmpresa = dbo.cxc_SeguimientoCartera.IdEmpresa AND dbo.aca_Alumno.IdAlumno = dbo.cxc_SeguimientoCartera.IdAlumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_SeguimientoCartera';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[46] 4[3] 2[32] 3) )"
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
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 0
               Left = 322
               Bottom = 163
               Right = 567
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_SeguimientoCartera"
            Begin Extent = 
               Top = 0
               Left = 662
               Bottom = 240
               Right = 907
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 140
               Left = 10
               Bottom = 303
               Right = 284
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_SeguimientoCartera';


