CREATE VIEW dbo.vwro_permiso_x_empleado
AS
SELECT        dbo.ro_permiso_x_empleado.IdEmpresa, dbo.ro_permiso_x_empleado.IdPermiso, dbo.ro_permiso_x_empleado.IdEmpleado, dbo.ro_permiso_x_empleado.IdEmpleadoAprueba, dbo.ro_permiso_x_empleado.FechaInicio, 
                         dbo.ro_permiso_x_empleado.FechaFin, dbo.ro_permiso_x_empleado.HoraSalida, dbo.ro_permiso_x_empleado.HoraRegreso, dbo.ro_permiso_x_empleado.DescuentaVacaciones, dbo.ro_permiso_x_empleado.Recuperable, 
                         dbo.ro_permiso_x_empleado.Asunto, dbo.ro_permiso_x_empleado.Descripcion, dbo.ro_permiso_x_empleado.TipoPermiso, dbo.ro_permiso_x_empleado.estado, dbo.tb_persona.pe_cedulaRuc, 
                         dbo.tb_persona.pe_nombreCompleto
FROM            dbo.ro_empleado INNER JOIN
                         dbo.ro_permiso_x_empleado ON dbo.ro_empleado.IdEmpresa = dbo.ro_permiso_x_empleado.IdEmpresa AND dbo.ro_empleado.IdEmpresa = dbo.ro_permiso_x_empleado.IdEmpresa AND 
                         dbo.ro_empleado.IdEmpleado = dbo.ro_permiso_x_empleado.IdEmpleadoAprueba AND dbo.ro_empleado.IdEmpleado = dbo.ro_permiso_x_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_permiso_x_empleado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[67] 4[5] 2[11] 3) )"
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
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 180
               Right = 327
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_permiso_x_empleado"
            Begin Extent = 
               Top = 6
               Left = 365
               Bottom = 302
               Right = 568
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 20
               Left = 705
               Bottom = 294
               Right = 937
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
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_permiso_x_empleado';

