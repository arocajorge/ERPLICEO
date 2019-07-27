CREATE VIEW [dbo].[vwin_in_Guia_x_traspaso_bodega_x_in_transferencia_det]
AS
SELECT        dbo.in_Guia_x_traspaso_bodega_x_in_transferencia_det.IdEmpresa_tras, dbo.in_Guia_x_traspaso_bodega_x_in_transferencia_det.IdGuia, 
                         dbo.in_Guia_x_traspaso_bodega_x_in_transferencia_det.IdEmpresa, dbo.in_Guia_x_traspaso_bodega_x_in_transferencia_det.IdSucursalOrigen, 
                         dbo.in_Guia_x_traspaso_bodega_x_in_transferencia_det.IdBodegaOrigen, dbo.in_Guia_x_traspaso_bodega_x_in_transferencia_det.IdTransferencia, 
                         dbo.in_Guia_x_traspaso_bodega_x_in_transferencia_det.dt_secuencia, dbo.in_Guia_x_traspaso_bodega_x_in_transferencia_det.cantidad, 
                         dbo.in_Guia_x_traspaso_bodega_x_in_transferencia_det.observacion, dbo.in_Producto.pr_descripcion, dbo.in_Producto.pr_observacion, 
                         dbo.tb_bodega.bo_Descripcion, dbo.tb_sucursal.Su_Descripcion, dbo.in_transferencia_det.dt_cantidad
FROM            dbo.in_Guia_x_traspaso_bodega_x_in_transferencia_det INNER JOIN
                         dbo.in_transferencia_det ON dbo.in_Guia_x_traspaso_bodega_x_in_transferencia_det.IdEmpresa = dbo.in_transferencia_det.IdEmpresa AND 
                         dbo.in_Guia_x_traspaso_bodega_x_in_transferencia_det.IdTransferencia = dbo.in_transferencia_det.IdTransferencia AND 
                         dbo.in_Guia_x_traspaso_bodega_x_in_transferencia_det.dt_secuencia = dbo.in_transferencia_det.dt_secuencia AND 
                         dbo.in_Guia_x_traspaso_bodega_x_in_transferencia_det.IdBodegaOrigen = dbo.in_transferencia_det.IdBodegaOrigen AND 
                         dbo.in_Guia_x_traspaso_bodega_x_in_transferencia_det.IdSucursalOrigen = dbo.in_transferencia_det.IdSucursalOrigen INNER JOIN
                         dbo.in_Producto ON dbo.in_transferencia_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND 
                         dbo.in_transferencia_det.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                         dbo.tb_bodega ON dbo.in_transferencia_det.IdEmpresa = dbo.tb_bodega.IdEmpresa AND dbo.in_transferencia_det.IdSucursalOrigen = dbo.tb_bodega.IdSucursal AND
                          dbo.in_transferencia_det.IdBodegaOrigen = dbo.tb_bodega.IdBodega INNER JOIN
                         dbo.tb_sucursal ON dbo.in_transferencia_det.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.in_transferencia_det.IdSucursalOrigen = dbo.tb_sucursal.IdSucursal
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[63] 4[4] 2[4] 3) )"
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
         Begin Table = "in_Guia_x_traspaso_bodega_x_in_transferencia_det"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 278
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_transferencia_det"
            Begin Extent = 
               Top = 0
               Left = 316
               Bottom = 304
               Right = 525
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 0
               Left = 625
               Bottom = 143
               Right = 825
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_bodega"
            Begin Extent = 
               Top = 28
               Left = 890
               Bottom = 176
               Right = 1151
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 182
               Left = 637
               Bottom = 311
               Right = 867
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
      Begin ColumnWidths = 14
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
         Width = 1500', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_in_Guia_x_traspaso_bodega_x_in_transferencia_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_in_Guia_x_traspaso_bodega_x_in_transferencia_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_in_Guia_x_traspaso_bodega_x_in_transferencia_det';

