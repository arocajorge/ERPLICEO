CREATE VIEW dbo.vwro_rol_detalle_x_rubro_acumulado
AS
SELECT        dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa, dbo.ro_rol_detalle_x_rubro_acumulado.IdRol, dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpleado, dbo.ro_rol_detalle_x_rubro_acumulado.IdRubro, 
                         dbo.ro_rol_detalle_x_rubro_acumulado.Valor, dbo.ro_rol_detalle_x_rubro_acumulado.IdSucursal, dbo.ro_periodo.pe_FechaIni, dbo.ro_periodo.pe_FechaFin, 
                         dbo.tb_persona.pe_apellido + ' ' + dbo.tb_persona.pe_nombre AS Empleado, dbo.ro_rol.IdNominaTipo
FROM            dbo.tb_persona INNER JOIN
                         dbo.ro_empleado ON dbo.tb_persona.IdPersona = dbo.ro_empleado.IdPersona INNER JOIN
                         dbo.ro_rol INNER JOIN
                         dbo.ro_rol_detalle_x_rubro_acumulado ON dbo.ro_rol.IdEmpresa = dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa AND dbo.ro_rol.IdRol = dbo.ro_rol_detalle_x_rubro_acumulado.IdRol INNER JOIN
                         dbo.ro_periodo ON dbo.ro_rol.IdEmpresa = dbo.ro_periodo.IdEmpresa INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui ON dbo.ro_rol.IdEmpresa = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa AND dbo.ro_rol.IdNominaTipo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_Tipo AND 
                         dbo.ro_rol.IdNominaTipoLiqui = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_TipoLiqui AND dbo.ro_rol.IdPeriodo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo AND 
                         dbo.ro_periodo.IdEmpresa = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa AND dbo.ro_periodo.IdPeriodo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo ON 
                         dbo.ro_empleado.IdEmpresa = dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpleado INNER JOIN
                         dbo.tb_empresa ON dbo.ro_empleado.IdEmpresa = dbo.tb_empresa.IdEmpresa INNER JOIN
                         dbo.ro_rubros_calculados ON dbo.tb_empresa.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa
WHERE        (dbo.ro_rol_detalle_x_rubro_acumulado.IdRubro IN (dbo.ro_rubros_calculados.IdRubro_prov_DIII, dbo.ro_rubros_calculados.IdRubro_prov_DIV))
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_rol_detalle_x_rubro_acumulado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'   Right = 257
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_rubros_calculados"
            Begin Extent = 
               Top = 798
               Left = 38
               Bottom = 928
               Right = 281
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_rol_detalle_x_rubro_acumulado';


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
         Top = -192
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 327
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_rol"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 230
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "ro_rol_detalle_x_rubro_acumulado"
            Begin Extent = 
               Top = 270
               Left = 268
               Bottom = 400
               Right = 438
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "ro_periodo"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 259
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_periodo_x_ro_Nomina_TipoLiqui"
            Begin Extent = 
               Top = 534
               Left = 38
               Bottom = 664
               Right = 235
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_empresa"
            Begin Extent = 
               Top = 666
               Left = 38
               Bottom = 796
            ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_rol_detalle_x_rubro_acumulado';

