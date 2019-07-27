CREATE VIEW [dbo].[vwro_empleado_por_novedades]
AS
SELECT        dbo.ro_novedad_x_empleado.IdEmpresa, dbo.ro_novedad_x_empleado.IdTransaccion, dbo.ro_empleado.IdEmpleado, 
                         dbo.tb_persona.pe_nombreCompleto AS NombreCompleto, dbo.ro_rubro_tipo.IdRubro, dbo.ro_rubro_tipo.ru_descripcion AS DescripcionRubro, 
                         dbo.ro_Nomina_Tipo.IdNomina_Tipo AS TipoNomina, dbo.ro_Nomina_Tipo.Descripcion AS DescripcionNomina, 
                         dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui AS TipoLiquidacion, dbo.ro_Nomina_Tipoliqui.DescripcionProcesoNomina AS DescripcionProceso, 
                         dbo.ro_novedad_x_empleado.Observacion, dbo.ro_novedad_x_empleado.estado AS Estado
FROM            dbo.ro_empleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_novedad_x_empleado INNER JOIN
                         dbo.ro_rubro_tipo ON dbo.ro_novedad_x_empleado.IdRubro = dbo.ro_rubro_tipo.IdRubro INNER JOIN
                         dbo.ro_Nomina_Tipoliqui INNER JOIN
                         dbo.ro_Nomina_Tipo ON dbo.ro_Nomina_Tipoliqui.IdEmpresa = dbo.ro_Nomina_Tipo.IdEmpresa AND 
                         dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo = dbo.ro_Nomina_Tipo.IdNomina_Tipo ON 
                         dbo.ro_novedad_x_empleado.IdEmpresa = dbo.ro_Nomina_Tipoliqui.IdEmpresa AND 
                         dbo.ro_novedad_x_empleado.IdNomina_Tipo = dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo AND 
                         dbo.ro_novedad_x_empleado.IdNomina_TipoLiqui = dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui ON 
                         dbo.ro_empleado.IdEmpleado = dbo.ro_novedad_x_empleado.IdEmpleado_Emp_Novedad AND 
                         dbo.ro_empleado.IdEmpresa = dbo.ro_novedad_x_empleado.IdEmpresa
GROUP BY dbo.ro_novedad_x_empleado.IdEmpresa, dbo.ro_novedad_x_empleado.IdTransaccion, dbo.ro_empleado.IdEmpleado, dbo.tb_persona.pe_nombreCompleto, 
                         dbo.ro_rubro_tipo.IdRubro, dbo.ro_rubro_tipo.ru_descripcion, dbo.ro_Nomina_Tipo.IdNomina_Tipo, dbo.ro_Nomina_Tipo.Descripcion, 
                         dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui, dbo.ro_Nomina_Tipoliqui.DescripcionProcesoNomina, dbo.ro_novedad_x_empleado.Observacion, 
                         dbo.ro_novedad_x_empleado.estado
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[16] 4[38] 2[17] 3) )"
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
         Begin Table = "ro_Nomina_Tipoliqui"
            Begin Extent = 
               Top = 2
               Left = 473
               Bottom = 232
               Right = 709
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Nomina_Tipo"
            Begin Extent = 
               Top = 0
               Left = 787
               Bottom = 129
               Right = 996
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 214
               Left = 1036
               Bottom = 439
               Right = 1299
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 69
               Left = 1233
               Bottom = 353
               Right = 1442
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_rubro_tipo"
            Begin Extent = 
               Top = 303
               Left = 754
               Bottom = 526
               Right = 963
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_novedad_x_empleado"
            Begin Extent = 
               Top = 3
               Left = 80
               Bottom = 298
               Right = 314
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
      Begin ColumnWidths = 36
         Width = 284
  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_empleado_por_novedades';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'       Width = 2610
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 3075
         Width = 1500
         Width = 2655
         Width = 2400
         Width = 2655
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
         Width = 2040
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 3270
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 3450
         Alias = 2880
         Table = 2880
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 2190
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_empleado_por_novedades';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_empleado_por_novedades';

