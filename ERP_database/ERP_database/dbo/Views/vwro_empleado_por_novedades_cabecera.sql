/*select * from vwro_empleado_por_novedades_cabecera*/
CREATE VIEW [dbo].[vwro_empleado_por_novedades_cabecera]
AS
SELECT        dbo.ro_novedad_x_empleado.IdEmpresa, dbo.ro_novedad_x_empleado.IdTransaccion, dbo.ro_rubro_tipo.IdRubro, 
                         dbo.ro_rubro_tipo.ru_descripcion AS DescripcionRubro, dbo.ro_Nomina_Tipo.IdNomina_Tipo AS TipoNomina, 
                         dbo.ro_Nomina_Tipo.Descripcion AS DescripcionNomina, dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui AS TipoLiquidacion, 
                         dbo.ro_Nomina_Tipoliqui.DescripcionProcesoNomina AS DescripcionProceso, dbo.ro_novedad_x_empleado.Observacion, dbo.ro_novedad_x_empleado.estado, 
                         dbo.ro_novedad_x_empleado.Fecha
FROM            dbo.ro_novedad_x_empleado INNER JOIN
                         dbo.ro_rubro_tipo ON dbo.ro_novedad_x_empleado.IdRubro = dbo.ro_rubro_tipo.IdRubro INNER JOIN
                         dbo.ro_Nomina_Tipoliqui INNER JOIN
                         dbo.ro_Nomina_Tipo ON dbo.ro_Nomina_Tipoliqui.IdEmpresa = dbo.ro_Nomina_Tipo.IdEmpresa AND 
                         dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo = dbo.ro_Nomina_Tipo.IdNomina_Tipo ON 
                         dbo.ro_novedad_x_empleado.IdEmpresa = dbo.ro_Nomina_Tipoliqui.IdEmpresa AND 
                         dbo.ro_novedad_x_empleado.IdNomina_Tipo = dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo AND 
                         dbo.ro_novedad_x_empleado.IdNomina_TipoLiqui = dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui
GROUP BY dbo.ro_novedad_x_empleado.IdEmpresa, dbo.ro_novedad_x_empleado.IdTransaccion, dbo.ro_rubro_tipo.IdRubro, dbo.ro_rubro_tipo.ru_descripcion, 
                         dbo.ro_Nomina_Tipo.IdNomina_Tipo, dbo.ro_Nomina_Tipo.Descripcion, dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui, 
                         dbo.ro_Nomina_Tipoliqui.DescripcionProcesoNomina, dbo.ro_novedad_x_empleado.Observacion, dbo.ro_novedad_x_empleado.estado, 
                         dbo.ro_novedad_x_empleado.Fecha
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[19] 4[13] 2[15] 3) )"
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
         Begin Table = "ro_rubro_tipo"
            Begin Extent = 
               Top = 82
               Left = 13
               Bottom = 211
               Right = 238
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Nomina_Tipoliqui"
            Begin Extent = 
               Top = 5
               Left = 724
               Bottom = 134
               Right = 976
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Nomina_Tipo"
            Begin Extent = 
               Top = 5
               Left = 1020
               Bottom = 134
               Right = 1245
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_novedad_x_empleado"
            Begin Extent = 
               Top = 6
               Left = 276
               Bottom = 135
               Right = 526
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
      Begin ColumnWidths = 27
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
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 2835
        ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_empleado_por_novedades_cabecera';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 2685
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_empleado_por_novedades_cabecera';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_empleado_por_novedades_cabecera';

