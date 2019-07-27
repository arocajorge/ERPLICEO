CREATE VIEW web.VWROL_003
AS
SELECT        dbo.ro_empleado.IdEmpresa, dbo.ro_empleado.IdEmpleado, dbo.tb_persona.IdPersona, dbo.ro_empleado_Novedad.IdNovedad, dbo.ro_empleado_novedad_det.FechaPago, dbo.ro_empleado_novedad_det.Valor, 
                         dbo.ro_empleado_Novedad.Fecha_Transac, dbo.ro_cargo.ca_descripcion, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_cedulaRuc, dbo.ro_Nomina_Tipoliqui.DescripcionProcesoNomina, 
                         dbo.ro_empleado_novedad_det.Observacion, dbo.ro_rubro_tipo.ru_descripcion, dbo.ro_catalogo.ca_descripcion AS EstadoCobro
FROM            dbo.ro_empleado INNER JOIN
                         dbo.ro_empleado_Novedad ON dbo.ro_empleado.IdEmpresa = dbo.ro_empleado_Novedad.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_empleado_Novedad.IdEmpleado INNER JOIN
                         dbo.ro_empleado_novedad_det ON dbo.ro_empleado_Novedad.IdEmpresa = dbo.ro_empleado_novedad_det.IdEmpresa AND dbo.ro_empleado_Novedad.IdNovedad = dbo.ro_empleado_novedad_det.IdNovedad INNER JOIN
                         dbo.ro_cargo ON dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa AND dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_Nomina_Tipoliqui ON dbo.ro_empleado_Novedad.IdNomina_TipoLiqui = dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui AND dbo.ro_empleado_Novedad.IdNomina_Tipo = dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo AND 
                         dbo.ro_empleado_Novedad.IdEmpresa = dbo.ro_Nomina_Tipoliqui.IdEmpresa INNER JOIN
                         dbo.ro_rubro_tipo ON dbo.ro_empleado_novedad_det.IdRubro = dbo.ro_rubro_tipo.IdRubro AND dbo.ro_empleado_novedad_det.IdEmpresa = dbo.ro_rubro_tipo.IdEmpresa INNER JOIN
                         dbo.ro_catalogo ON dbo.ro_empleado_novedad_det.EstadoCobro = dbo.ro_catalogo.CodCatalogo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_003';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'1148
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_catalogo"
            Begin Extent = 
               Top = 258
               Left = 806
               Bottom = 388
               Right = 987
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
      Begin ColumnWidths = 16
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_003';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[70] 4[5] 2[5] 3) )"
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
               Top = 0
               Left = 168
               Bottom = 130
               Right = 457
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado_Novedad"
            Begin Extent = 
               Top = 15
               Left = 528
               Bottom = 190
               Right = 725
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado_novedad_det"
            Begin Extent = 
               Top = 71
               Left = 875
               Bottom = 320
               Right = 1105
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_cargo"
            Begin Extent = 
               Top = 40
               Left = 36
               Bottom = 170
               Right = 253
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 161
               Left = 81
               Bottom = 291
               Right = 313
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Nomina_Tipoliqui"
            Begin Extent = 
               Top = 108
               Left = 203
               Bottom = 238
               Right = 439
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_rubro_tipo"
            Begin Extent = 
               Top = 129
               Left = 930
               Bottom = 259
               Right = ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_003';

