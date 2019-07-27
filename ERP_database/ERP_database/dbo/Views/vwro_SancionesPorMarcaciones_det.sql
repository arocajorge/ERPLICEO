CREATE VIEW dbo.vwro_SancionesPorMarcaciones_det
AS
SELECT        dbo.ro_SancionesPorMarcaciones_det.IdEmpresa, dbo.ro_SancionesPorMarcaciones_det.IdAjuste, dbo.ro_SancionesPorMarcaciones_det.Secuencia, dbo.ro_SancionesPorMarcaciones_det.IdCalendario, 
                         dbo.ro_SancionesPorMarcaciones_det.IdEmpleado, dbo.ro_SancionesPorMarcaciones_det.IdSucursal, dbo.ro_SancionesPorMarcaciones_det.Minutos, dbo.ro_SancionesPorMarcaciones_det.Observacion, 
                         dbo.tb_persona.pe_nombreCompleto, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_cedulaRuc, dbo.ro_empleado.em_codigo, dbo.ro_SancionesPorMarcaciones_det.EsHoraIngreso, 
                         dbo.ro_SancionesPorMarcaciones_det.HoraIngreso, dbo.ro_SancionesPorMarcaciones_det.EsHoraSalida, dbo.ro_SancionesPorMarcaciones_det.FechaRegistro, dbo.ro_SancionesPorMarcaciones_det.HoraSalio
FROM            dbo.tb_persona INNER JOIN
                         dbo.ro_empleado ON dbo.tb_persona.IdPersona = dbo.ro_empleado.IdPersona INNER JOIN
                         dbo.ro_SancionesPorMarcaciones_det ON dbo.ro_empleado.IdEmpresa = dbo.ro_SancionesPorMarcaciones_det.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_SancionesPorMarcaciones_det.IdEmpleado
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_SancionesPorMarcaciones_det';




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[36] 4[5] 2[50] 3) )"
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
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 21
               Left = 727
               Bottom = 303
               Right = 959
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 18
               Left = 362
               Bottom = 392
               Right = 651
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_SancionesPorMarcaciones_det"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 327
               Right = 338
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_SancionesPorMarcaciones_det';



