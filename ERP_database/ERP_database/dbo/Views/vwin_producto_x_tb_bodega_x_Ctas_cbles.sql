CREATE VIEW [dbo].[vwin_producto_x_tb_bodega_x_Ctas_cbles]
AS
SELECT        dbo.tb_sucursal.IdEmpresa, dbo.tb_sucursal.IdSucursal, dbo.tb_sucursal.Su_Descripcion AS nom_sucursal, dbo.tb_bodega.IdBodega, dbo.tb_bodega.bo_Descripcion AS nom_bodega, 
                         dbo.in_Producto.IdProducto, dbo.in_Producto.pr_codigo AS cod_producto, dbo.in_Producto.pr_descripcion AS nom_producto, dbo.in_producto_x_tb_bodega.IdCtaCble_Inven, 
                         dbo.in_producto_x_tb_bodega.IdCtaCble_Costo, dbo.in_producto_x_tb_bodega.IdCtaCble_Vta, dbo.in_producto_x_tb_bodega.IdCtaCble_Gasto_x_cxp, '' IdCtaCble_Des0, 
                         '' IdCtaCble_Dev0, dbo.in_categorias.IdCategoria, dbo.in_categorias.ca_Categoria AS nom_categoria, dbo.in_linea.IdLinea, dbo.in_linea.nom_linea, dbo.in_grupo.IdGrupo, 
                         dbo.in_grupo.nom_grupo
FROM            dbo.in_linea INNER JOIN
                         dbo.in_categorias ON dbo.in_linea.IdEmpresa = dbo.in_categorias.IdEmpresa AND dbo.in_linea.IdCategoria = dbo.in_categorias.IdCategoria INNER JOIN
                         dbo.in_grupo ON dbo.in_linea.IdEmpresa = dbo.in_grupo.IdEmpresa AND dbo.in_linea.IdCategoria = dbo.in_grupo.IdCategoria AND dbo.in_linea.IdLinea = dbo.in_grupo.IdLinea INNER JOIN
                         dbo.in_Producto INNER JOIN
                         dbo.in_producto_x_tb_bodega ON dbo.in_Producto.IdEmpresa = dbo.in_producto_x_tb_bodega.IdEmpresa AND dbo.in_Producto.IdProducto = dbo.in_producto_x_tb_bodega.IdProducto INNER JOIN
                         dbo.tb_bodega ON dbo.in_producto_x_tb_bodega.IdEmpresa = dbo.tb_bodega.IdEmpresa AND dbo.in_producto_x_tb_bodega.IdSucursal = dbo.tb_bodega.IdSucursal AND 
                         dbo.in_producto_x_tb_bodega.IdBodega = dbo.tb_bodega.IdBodega INNER JOIN
                         dbo.tb_sucursal ON dbo.tb_bodega.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.tb_bodega.IdSucursal = dbo.tb_sucursal.IdSucursal ON dbo.in_grupo.IdEmpresa = dbo.in_Producto.IdEmpresa AND 
                         dbo.in_grupo.IdCategoria = dbo.in_Producto.IdCategoria AND dbo.in_grupo.IdLinea = dbo.in_Producto.IdLinea AND dbo.in_grupo.IdGrupo = dbo.in_Producto.IdGrupo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[60] 4[5] 2[5] 3) )"
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
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 3
               Left = 51
               Bottom = 413
               Right = 285
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_producto_x_tb_bodega"
            Begin Extent = 
               Top = 20
               Left = 402
               Bottom = 119
               Right = 633
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_bodega"
            Begin Extent = 
               Top = 10
               Left = 726
               Bottom = 242
               Right = 987
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 13
               Left = 1032
               Bottom = 371
               Right = 1262
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_categorias"
            Begin Extent = 
               Top = 192
               Left = 768
               Bottom = 399
               Right = 997
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_linea"
            Begin Extent = 
               Top = 170
               Left = 622
               Bottom = 392
               Right = 831
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_grupo"
            Begin Extent = 
               Top = 147
               Left = 369
               Bottom = 416
               Right = 578
            E', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_producto_x_tb_bodega_x_Ctas_cbles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'nd
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
      Begin ColumnWidths = 25
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
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2145
         Alias = 3990
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_producto_x_tb_bodega_x_Ctas_cbles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_producto_x_tb_bodega_x_Ctas_cbles';

