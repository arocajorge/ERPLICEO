


CREATE VIEW [dbo].[vwin_in_Producto_x_tb_bodega_x_UnidadMedida]
AS
SELECT        A.IdEmpresa, A.IdProducto, A.pr_codigo, A.pr_codigo_barra, A.IdProductoTipo, A.IdPresentacion, A.IdCategoria, A.pr_descripcion, A.pr_observacion, 
                         A.IdUnidadMedida, 0 pr_precio_publico, 0 AS pr_pedidos,
                           A.IdMarca,                          
                         0 pr_stock_minimo, A.IdUsuario, A.Fecha_Transac, A.IdUsuarioUltMod, A.Fecha_UltMod, A.IdUsuarioUltAnu, A.Fecha_UltAnu, 
                         A.pr_motivoAnulacion, A.nom_pc, A.ip, A.Estado, A.pr_descripcion_2,  B.IdCtaCble_Inven AS IdCtaCble_Inventario, 
                         B.IdCtaCble_Costo, ''IdCentro_Costo_Inventario, ''IdCentro_Costo_Costo, B.IdCtaCble_Gasto_x_cxp, ''IdCentroCosto_x_Gasto_x_cxp, 
                         ''IdCentroCosto_sub_centro_costo_inv, ''IdCentroCosto_sub_centro_costo_cost, ''IdCentroCosto_sub_centro_costo_cxp, A.IdLinea, A.IdGrupo, A.IdSubGrupo, 
                          '' IdNaturaleza,  0 IdMotivo_Vta, B.IdBodega, C.bo_Descripcion, C.IdSucursal, D.Su_Descripcion, 
                         B.IdCtaCble_Inven, ISNULL(dbo.vwin_Producto_Stock.Stock, 0) AS pr_stock_Bodega, 0 AS pr_costo_promedio_bodega, 
                         uni.Descripcion AS Descripcion_UniMedida, consu.Descripcion AS Descripcion_TipoConsumo, ISNULL(dbo.vwin_Producto_Stock.Stock, 0) AS pr_stock, 
                         A.IdUnidadMedida_Consumo, '' IdCtaCble_CosBaseIva, '' IdCtaCble_CosBase0, '' IdCtaCble_VenIva, '' IdCtaCble_Ven0, '' IdCtaCble_DesIva, '' IdCtaCble_Des0, 
                         '' IdCtaCble_DevIva, '' IdCtaCble_Dev0, B.IdCtaCble_Vta, A.IdCod_Impuesto_Iva, A.Aparece_modu_Ventas, A.Aparece_modu_Compras, 
                         A.Aparece_modu_Inventario, A.Aparece_modu_Activo_F
FROM            dbo.in_Producto AS A INNER JOIN
                         dbo.in_producto_x_tb_bodega AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdProducto = B.IdProducto INNER JOIN
                         dbo.tb_bodega AS C ON B.IdEmpresa = C.IdEmpresa AND B.IdBodega = C.IdBodega AND B.IdSucursal = C.IdSucursal INNER JOIN
                         dbo.tb_sucursal AS D ON C.IdEmpresa = D.IdEmpresa AND C.IdSucursal = D.IdSucursal INNER JOIN
                         dbo.in_UnidadMedida AS uni ON uni.IdUnidadMedida = A.IdUnidadMedida INNER JOIN
                         dbo.in_UnidadMedida AS consu ON A.IdUnidadMedida_Consumo = consu.IdUnidadMedida LEFT OUTER JOIN
                         dbo.vwin_Producto_Stock ON B.IdEmpresa = dbo.vwin_Producto_Stock.IdEmpresa AND B.IdSucursal = dbo.vwin_Producto_Stock.IdSucursal AND 
                         B.IdBodega = dbo.vwin_Producto_Stock.IdBodega AND B.IdProducto = dbo.vwin_Producto_Stock.IdProducto
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[71] 4[5] 2[6] 3) )"
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
         Begin Table = "A"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 208
               Right = 272
            End
            DisplayFlags = 280
            TopColumn = 56
         End
         Begin Table = "B"
            Begin Extent = 
               Top = 27
               Left = 546
               Bottom = 346
               Right = 836
            End
            DisplayFlags = 280
            TopColumn = 18
         End
         Begin Table = "C"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 399
               Right = 299
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "D"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 531
               Right = 268
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "uni"
            Begin Extent = 
               Top = 534
               Left = 38
               Bottom = 663
               Right = 248
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "consu"
            Begin Extent = 
               Top = 666
               Left = 38
               Bottom = 795
               Right = 248
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "vwin_Producto_Stock"
            Begin Extent = 
               Top = 798
               Left = 38
               Bottom = 927
               Right = 247
            End
            DisplayFlags = 280
            TopColu', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_in_Producto_x_tb_bodega_x_UnidadMedida';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'mn = 0
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
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_in_Producto_x_tb_bodega_x_UnidadMedida';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_in_Producto_x_tb_bodega_x_UnidadMedida';

