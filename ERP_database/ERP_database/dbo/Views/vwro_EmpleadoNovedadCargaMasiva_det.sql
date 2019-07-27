CREATE VIEW dbo.vwro_EmpleadoNovedadCargaMasiva_det
AS
SELECT        dbo.ro_EmpleadoNovedadCargaMasiva_det.IdEmpresa, dbo.ro_EmpleadoNovedadCargaMasiva_det.IdCarga, dbo.ro_EmpleadoNovedadCargaMasiva_det.Secuencia, 
                         dbo.ro_EmpleadoNovedadCargaMasiva_det.IdEmpresa_nov, dbo.ro_EmpleadoNovedadCargaMasiva_det.IdNovedad, dbo.ro_EmpleadoNovedadCargaMasiva_det.Observacion, 
                         dbo.ro_EmpleadoNovedadCargaMasiva_det.IdEmpleado, dbo.ro_empleado.em_codigo, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_cedulaRuc, dbo.ro_empleado_novedad_det.Valor
FROM            dbo.ro_EmpleadoNovedadCargaMasiva_det INNER JOIN
                         dbo.ro_empleado ON dbo.ro_EmpleadoNovedadCargaMasiva_det.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_EmpleadoNovedadCargaMasiva_det.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_empleado_Novedad ON dbo.ro_EmpleadoNovedadCargaMasiva_det.IdEmpresa = dbo.ro_empleado_Novedad.IdEmpresa AND 
                         dbo.ro_EmpleadoNovedadCargaMasiva_det.IdNovedad = dbo.ro_empleado_Novedad.IdNovedad AND dbo.ro_empleado.IdEmpresa = dbo.ro_empleado_Novedad.IdEmpresa AND 
                         dbo.ro_empleado.IdEmpresa = dbo.ro_empleado_Novedad.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_empleado_Novedad.IdEmpleado AND 
                         dbo.ro_empleado.IdEmpleado = dbo.ro_empleado_Novedad.IdEmpleado INNER JOIN
                         dbo.ro_empleado_novedad_det ON dbo.ro_empleado_Novedad.IdEmpresa = dbo.ro_empleado_novedad_det.IdEmpresa AND dbo.ro_empleado_Novedad.IdNovedad = dbo.ro_empleado_novedad_det.IdNovedad
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_EmpleadoNovedadCargaMasiva_det';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[53] 4[5] 2[5] 3) )"
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
         Begin Table = "ro_EmpleadoNovedadCargaMasiva_det"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 274
               Right = 268
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 0
               Left = 237
               Bottom = 332
               Right = 526
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 15
               Left = 530
               Bottom = 309
               Right = 762
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado_Novedad"
            Begin Extent = 
               Top = 192
               Left = 96
               Bottom = 322
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado_novedad_det"
            Begin Extent = 
               Top = 106
               Left = 567
               Bottom = 352
               Right = 737
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
      Begin ColumnWidths = ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_EmpleadoNovedadCargaMasiva_det';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'11
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_EmpleadoNovedadCargaMasiva_det';

