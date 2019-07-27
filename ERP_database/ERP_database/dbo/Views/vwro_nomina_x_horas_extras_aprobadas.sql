CREATE VIEW dbo.vwro_nomina_x_horas_extras_aprobadas
AS
SELECT        he.IdEmpresa, he.IdHorasExtras, he.IdNominaTipo, he.IdNominaTipoLiqui, he.IdPeriodo, he_det.IdEmpleado, ISNULL(SUM(he_det.Valor25), 0) AS Valor25, ISNULL(SUM(he_det.Valor50), 0) AS Valor50, 
                         ISNULL(SUM(he_det.Valor100), 0) AS Valor100, dbo.ro_empleado.IdSucursal
FROM            dbo.ro_nomina_x_horas_extras AS he INNER JOIN
                         dbo.ro_nomina_x_horas_extras_det AS he_det ON he.IdEmpresa = he_det.IdEmpresa AND he.IdHorasExtras = he_det.IdHorasExtras INNER JOIN
                         dbo.ro_empleado ON he_det.IdEmpresa = dbo.ro_empleado.IdEmpresa AND he_det.IdEmpleado = dbo.ro_empleado.IdEmpleado
WHERE        (he_det.es_HorasExtrasAutorizadas = 1)
GROUP BY he.IdEmpresa, he.IdHorasExtras, he.IdNominaTipo, he.IdNominaTipoLiqui, he.IdPeriodo, he_det.IdEmpleado, dbo.ro_empleado.IdSucursal
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_nomina_x_horas_extras_aprobadas';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[77] 4[5] 2[1] 3) )"
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
         Begin Table = "he"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 235
               Right = 246
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "he_det"
            Begin Extent = 
               Top = 23
               Left = 474
               Bottom = 153
               Right = 718
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 6
               Left = 756
               Bottom = 336
               Right = 1061
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
      Begin ColumnWidths = 12
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_nomina_x_horas_extras_aprobadas';



