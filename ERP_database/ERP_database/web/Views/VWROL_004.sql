CREATE VIEW web.VWROL_004
AS
SELECT        dbo.ro_participacion_utilidad.IdEmpresa, dbo.ro_participacion_utilidad.IdPeriodo, dbo.ro_participacion_utilidad.UtilidadDerechoIndividual, dbo.ro_participacion_utilidad.UtilidadCargaFamiliar, 
                         dbo.ro_participacion_utilidad.LimiteDistribucionUtilidad, dbo.ro_participacion_utilidad_empleado.DiasTrabajados, dbo.ro_participacion_utilidad_empleado.CargasFamiliares, 
                         dbo.ro_participacion_utilidad_empleado.ValorIndividual, dbo.ro_participacion_utilidad_empleado.ValorCargaFamiliar, dbo.ro_participacion_utilidad_empleado.ValorExedenteIESS, 
                         dbo.ro_participacion_utilidad_empleado.ValorTotal, dbo.ro_participacion_utilidad_empleado.IdEmpleado, dbo.tb_persona.pe_apellido + ' ' + dbo.tb_persona.pe_nombre AS Nombres, dbo.tb_persona.pe_cedulaRuc, 
                         dbo.ro_cargo.ca_descripcion, dbo.ro_empleado.em_codigo, dbo.ro_participacion_utilidad.IdUtilidad
FROM            dbo.ro_participacion_utilidad_empleado INNER JOIN
                         dbo.ro_participacion_utilidad ON dbo.ro_participacion_utilidad_empleado.IdEmpresa = dbo.ro_participacion_utilidad.IdEmpresa AND 
                         dbo.ro_participacion_utilidad_empleado.IdUtilidad = dbo.ro_participacion_utilidad.IdUtilidad INNER JOIN
                         dbo.ro_empleado ON dbo.ro_participacion_utilidad_empleado.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_participacion_utilidad_empleado.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_cargo ON dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa AND dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[76] 4[3] 2[3] 3) )"
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
         Begin Table = "ro_participacion_utilidad_empleado"
            Begin Extent = 
               Top = 3
               Left = 0
               Bottom = 378
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_participacion_utilidad"
            Begin Extent = 
               Top = 197
               Left = 330
               Bottom = 432
               Right = 557
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 0
               Left = 403
               Bottom = 130
               Right = 692
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 209
               Left = 637
               Bottom = 339
               Right = 869
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_cargo"
            Begin Extent = 
               Top = 53
               Left = 924
               Bottom = 183
               Right = 1141
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
   ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_004';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'      Column = 1440
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_004';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_004';

