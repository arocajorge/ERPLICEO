CREATE VIEW dbo.vwro_Empleado_Novedades
AS
SELECT        dbo.ro_empleado_Novedad.IdEmpleado, dbo.ro_empleado_Novedad.Fecha, dbo.ro_empleado_Novedad.IdUsuario, dbo.ro_empleado_Novedad.Fecha_Transac, dbo.ro_empleado_Novedad.IdUsuarioUltMod, 
                         dbo.ro_empleado_Novedad.Fecha_UltAnu, dbo.ro_empleado_Novedad.IdUsuarioUltAnu, dbo.ro_empleado_Novedad.MotiAnula, dbo.ro_empleado_Novedad.Estado, dbo.ro_Nomina_Tipoliqui.DescripcionProcesoNomina, 
                         dbo.ro_empleado_Novedad.IdNomina_Tipo, dbo.ro_empleado_Novedad.IdNomina_TipoLiqui, dbo.tb_persona.pe_nombreCompleto, dbo.ro_empleado.em_codigo, dbo.tb_persona.pe_cedulaRuc, 
                         dbo.ro_Nomina_Tipo.Descripcion AS descripcion_tiponomina, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.ro_cargo.ca_descripcion, dbo.ro_empleado.em_status, dbo.ro_empleado_Novedad.Observacion, 
                         dbo.tb_sucursal.Su_Descripcion, dbo.ro_empleado_Novedad.IdSucursal
FROM            dbo.ro_Nomina_Tipo INNER JOIN
                         dbo.ro_Nomina_Tipoliqui ON dbo.ro_Nomina_Tipo.IdEmpresa = dbo.ro_Nomina_Tipoliqui.IdEmpresa AND dbo.ro_Nomina_Tipo.IdNomina_Tipo = dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo INNER JOIN
                         dbo.tb_persona INNER JOIN
                         dbo.ro_empleado INNER JOIN
                         dbo.ro_empleado_Novedad ON dbo.ro_empleado.IdEmpresa = dbo.ro_empleado_Novedad.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_empleado_Novedad.IdEmpleado ON 
                         dbo.tb_persona.IdPersona = dbo.ro_empleado.IdPersona ON dbo.ro_Nomina_Tipoliqui.IdEmpresa = dbo.ro_empleado_Novedad.IdEmpresa AND 
                         dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo = dbo.ro_empleado_Novedad.IdNomina_Tipo AND dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui = dbo.ro_empleado_Novedad.IdNomina_TipoLiqui INNER JOIN
                         dbo.ro_cargo ON dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa AND dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo INNER JOIN
                         dbo.tb_sucursal ON dbo.ro_empleado_Novedad.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ro_empleado_Novedad.IdSucursal = dbo.tb_sucursal.IdSucursal
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[38] 4[5] 2[35] 3) )"
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
         Begin Table = "ro_Nomina_Tipo"
            Begin Extent = 
               Top = 0
               Left = 0
               Bottom = 129
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Nomina_Tipoliqui"
            Begin Extent = 
               Top = 10
               Left = 259
               Bottom = 157
               Right = 475
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_rubro_tipo"
            Begin Extent = 
               Top = 62
               Left = 1026
               Bottom = 281
               Right = 1197
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado_novedad_det"
            Begin Extent = 
               Top = 7
               Left = 794
               Bottom = 172
               Right = 1041
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 113
               Left = 42
               Bottom = 389
               Right = 234
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 24
               Left = 382
               Bottom = 250
               Right = 594
            End
            DisplayFlags = 280
            TopColumn = 57
         End
         Begin Table = "ro_empleado_Novedad"
            Begin Extent = 
               Top = 19
               Left = 537
               Bottom = 237
               Ri', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_Empleado_Novedades';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'ght = 746
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_cargo"
            Begin Extent = 
               Top = 174
               Left = 784
               Bottom = 304
               Right = 981
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
      Begin ColumnWidths = 37
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1995
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
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 3600
         Alias = 2880
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_Empleado_Novedades';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_Empleado_Novedades';

