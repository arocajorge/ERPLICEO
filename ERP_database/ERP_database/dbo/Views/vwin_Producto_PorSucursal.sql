CREATE VIEW dbo.vwin_Producto_PorSucursal
AS
SELECT        dbo.in_producto_x_tb_bodega.IdEmpresa, dbo.in_producto_x_tb_bodega.IdSucursal, dbo.in_producto_x_tb_bodega.IdProducto, dbo.in_Producto.pr_descripcion, dbo.in_categorias.ca_Categoria, 
                         SUM(ISNULL(dbo.in_movi_inve_detalle.dm_cantidad, 0)) AS Stock, dbo.in_Producto.precio_1, dbo.in_ProductoTipo.tp_ManejaInven
FROM            dbo.in_ProductoTipo INNER JOIN
                         dbo.in_Producto ON dbo.in_ProductoTipo.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_ProductoTipo.IdProductoTipo = dbo.in_Producto.IdProductoTipo RIGHT OUTER JOIN
                         dbo.in_producto_x_tb_bodega ON dbo.in_Producto.IdEmpresa = dbo.in_producto_x_tb_bodega.IdEmpresa AND dbo.in_Producto.IdProducto = dbo.in_producto_x_tb_bodega.IdProducto LEFT OUTER JOIN
                         dbo.in_movi_inve_detalle ON dbo.in_producto_x_tb_bodega.IdEmpresa = dbo.in_movi_inve_detalle.IdEmpresa AND dbo.in_producto_x_tb_bodega.IdSucursal = dbo.in_movi_inve_detalle.IdSucursal AND 
                         dbo.in_producto_x_tb_bodega.IdBodega = dbo.in_movi_inve_detalle.IdBodega AND dbo.in_producto_x_tb_bodega.IdProducto = dbo.in_movi_inve_detalle.IdProducto LEFT OUTER JOIN
                         dbo.in_categorias ON dbo.in_Producto.IdEmpresa = dbo.in_categorias.IdEmpresa AND dbo.in_Producto.IdCategoria = dbo.in_categorias.IdCategoria
WHERE        (dbo.in_Producto.Estado = 'A') AND (dbo.in_producto_x_tb_bodega.IdBodega = 1)
GROUP BY dbo.in_Producto.pr_descripcion, dbo.in_categorias.ca_Categoria, dbo.in_producto_x_tb_bodega.IdEmpresa, dbo.in_producto_x_tb_bodega.IdSucursal, dbo.in_producto_x_tb_bodega.IdProducto, dbo.in_Producto.precio_1, 
                         dbo.in_ProductoTipo.tp_ManejaInven
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_Producto_PorSucursal';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[56] 4[5] 2[21] 3) )"
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
         Begin Table = "in_producto_x_tb_bodega"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 310
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 0
               Left = 392
               Bottom = 300
               Right = 683
            End
            DisplayFlags = 280
            TopColumn = 25
         End
         Begin Table = "in_movi_inve_detalle"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 373
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_categorias"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 273
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_ProductoTipo"
            Begin Extent = 
               Top = 174
               Left = 38
               Bottom = 304
               Right = 281
            End
            DisplayFlags = 280
            TopColumn = 1
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
      Begin ColumnWidths = 12
         ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_Producto_PorSucursal';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'Column = 1440
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_Producto_PorSucursal';

