CREATE VIEW dbo.vwro_SancionesPorMarcaciones_x_novedad
AS
SELECT        dbo.ro_SancionesPorMarcaciones_x_novedad.IdEmpresa, dbo.ro_SancionesPorMarcaciones_x_novedad.IdAjuste, dbo.ro_SancionesPorMarcaciones_x_novedad.Secuencia, 
                         dbo.ro_SancionesPorMarcaciones_x_novedad.IdNovedad, dbo.ro_SancionesPorMarcaciones_x_novedad.IdEmpresa_nov, dbo.ro_SancionesPorMarcaciones_x_novedad.IdEmpleado, 
                         dbo.ro_SancionesPorMarcaciones_x_novedad.IdNomina_Tipo, dbo.ro_SancionesPorMarcaciones_x_novedad.IdNomina_TipoLiqui, dbo.ro_empleado_novedad_det.Valor, dbo.ro_empleado_novedad_det.FechaPago, 
                         dbo.ro_empleado.em_codigo, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_nombreCompleto
FROM            dbo.ro_SancionesPorMarcaciones_x_novedad INNER JOIN
                         dbo.ro_empleado_Novedad ON dbo.ro_SancionesPorMarcaciones_x_novedad.IdEmpresa_nov = dbo.ro_empleado_Novedad.IdEmpresa AND 
                         dbo.ro_SancionesPorMarcaciones_x_novedad.IdNovedad = dbo.ro_empleado_Novedad.IdNovedad INNER JOIN
                         dbo.ro_empleado ON dbo.ro_empleado_Novedad.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_empleado_Novedad.IdEmpresa = dbo.ro_empleado.IdEmpresa AND 
                         dbo.ro_empleado_Novedad.IdEmpleado = dbo.ro_empleado.IdEmpleado AND dbo.ro_empleado_Novedad.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_empleado_novedad_det ON dbo.ro_empleado_Novedad.IdEmpresa = dbo.ro_empleado_novedad_det.IdEmpresa AND dbo.ro_empleado_Novedad.IdNovedad = dbo.ro_empleado_novedad_det.IdNovedad
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_SancionesPorMarcaciones_x_novedad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_SancionesPorMarcaciones_x_novedad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[72] 4[5] 2[6] 3) )"
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
         Begin Table = "ro_SancionesPorMarcaciones_x_novedad"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 316
               Right = 356
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado_Novedad"
            Begin Extent = 
               Top = 6
               Left = 273
               Bottom = 308
               Right = 542
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 28
               Left = 894
               Bottom = 380
               Right = 1126
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 34
               Left = 591
               Bottom = 263
               Right = 880
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado_novedad_det"
            Begin Extent = 
               Top = 168
               Left = 580
               Bottom = 380
               Right = 887
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
         Filter = 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_SancionesPorMarcaciones_x_novedad';

