CREATE VIEW dbo.vwpre_Presupuesto
AS
SELECT        dbo.pre_Presupuesto.IdEmpresa, dbo.pre_Presupuesto.IdPresupuesto, dbo.pre_Presupuesto.IdSucursal, dbo.tb_sucursal.Su_Descripcion, dbo.pre_Presupuesto.IdPeriodo, dbo.pre_PresupuestoPeriodo.FechaInicio, 
                         dbo.pre_PresupuestoPeriodo.FechaFin, dbo.pre_PresupuestoPeriodo.EstadoCierre, dbo.pre_Presupuesto.Observacion, dbo.pre_Presupuesto.Estado, 
                         dbo.pre_Presupuesto.MontoSolicitado, dbo.pre_Presupuesto.MontoAprobado, dbo.pre_PresupuestoPeriodo.DescripciónPeriodo, dbo.pre_Presupuesto.IdUsuarioAprobacion, dbo.pre_Presupuesto.FechaAprobacion, 
                         dbo.pre_Presupuesto.MotivoAprobacion
FROM          
                         dbo.pre_Presupuesto INNER JOIN
                         dbo.pre_PresupuestoPeriodo ON dbo.pre_Presupuesto.IdEmpresa = dbo.pre_PresupuestoPeriodo.IdEmpresa AND dbo.pre_Presupuesto.IdPeriodo = dbo.pre_PresupuestoPeriodo.IdPeriodo INNER JOIN
                         dbo.tb_sucursal ON dbo.pre_Presupuesto.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.pre_Presupuesto.IdSucursal = dbo.tb_sucursal.IdSucursal
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwpre_Presupuesto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'alue = 1170
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwpre_Presupuesto';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[34] 4[5] 2[8] 3) )"
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
         Begin Table = "pre_Grupo"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pre_Presupuesto"
            Begin Extent = 
               Top = 0
               Left = 307
               Bottom = 130
               Right = 516
            End
            DisplayFlags = 280
            TopColumn = 15
         End
         Begin Table = "pre_PresupuestoPeriodo"
            Begin Extent = 
               Top = 9
               Left = 555
               Bottom = 159
               Right = 764
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 534
               Left = 38
               Bottom = 664
               Right = 268
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
         NewV', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwpre_Presupuesto';



