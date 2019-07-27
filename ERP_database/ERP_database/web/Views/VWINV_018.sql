CREATE VIEW web.VWINV_018
AS
SELECT dbo.in_AjusteDet.IdEmpresa, dbo.in_AjusteDet.IdAjuste, dbo.in_AjusteDet.Secuencia, dbo.in_Ajuste.IdSucursal, dbo.in_Ajuste.IdBodega, dbo.in_Ajuste.IdMovi_inven_tipo_ing, dbo.in_Ajuste.IdMovi_inven_tipo_egr, 
                  dbo.in_Ajuste.IdNumMovi_ing, dbo.in_Ajuste.IdNumMovi_egr, dbo.in_Ajuste.IdCatalogo_Estado, dbo.in_Ajuste.Estado, dbo.in_Ajuste.Fecha, dbo.in_Ajuste.Observacion, dbo.tb_sucursal.Su_Descripcion, dbo.tb_bodega.bo_Descripcion, 
                  movi_ing.tm_descripcion AS tm_descripcion_ing, movi_egr.tm_descripcion AS tm_descripcion_egr, dbo.in_AjusteDet.IdProducto, dbo.in_AjusteDet.IdUnidadMedida, dbo.in_AjusteDet.StockSistema, dbo.in_AjusteDet.StockFisico, 
                  dbo.in_AjusteDet.Ajuste, dbo.in_AjusteDet.Costo, dbo.in_Producto.pr_descripcion, dbo.in_UnidadMedida.Descripcion AS NomUnidadMedida, dbo.in_categorias.ca_Categoria, dbo.in_linea.nom_linea, 
                  dbo.in_AjusteDet.Ajuste * dbo.in_AjusteDet.Costo AS Total, dbo.in_Catalogo.Nombre AS NombreEstado
FROM     dbo.in_UnidadMedida INNER JOIN
                  dbo.in_Ajuste INNER JOIN
                  dbo.in_AjusteDet ON dbo.in_Ajuste.IdEmpresa = dbo.in_AjusteDet.IdEmpresa AND dbo.in_Ajuste.IdAjuste = dbo.in_AjusteDet.IdAjuste INNER JOIN
                  dbo.tb_bodega ON dbo.in_Ajuste.IdEmpresa = dbo.tb_bodega.IdEmpresa AND dbo.in_Ajuste.IdEmpresa = dbo.tb_bodega.IdEmpresa AND dbo.in_Ajuste.IdSucursal = dbo.tb_bodega.IdSucursal AND 
                  dbo.in_Ajuste.IdSucursal = dbo.tb_bodega.IdSucursal AND dbo.in_Ajuste.IdBodega = dbo.tb_bodega.IdBodega AND dbo.in_Ajuste.IdBodega = dbo.tb_bodega.IdBodega INNER JOIN
                  dbo.tb_sucursal ON dbo.tb_bodega.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.tb_bodega.IdSucursal = dbo.tb_sucursal.IdSucursal INNER JOIN
                  dbo.in_Producto ON dbo.in_AjusteDet.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_AjusteDet.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                  dbo.in_linea INNER JOIN
                  dbo.in_categorias ON dbo.in_linea.IdEmpresa = dbo.in_categorias.IdEmpresa AND dbo.in_linea.IdCategoria = dbo.in_categorias.IdCategoria ON dbo.in_Producto.IdEmpresa = dbo.in_linea.IdEmpresa AND 
                  dbo.in_Producto.IdCategoria = dbo.in_linea.IdCategoria AND dbo.in_Producto.IdLinea = dbo.in_linea.IdLinea ON dbo.in_UnidadMedida.IdUnidadMedida = dbo.in_AjusteDet.IdUnidadMedida INNER JOIN
                  dbo.in_Catalogo ON dbo.in_Ajuste.IdCatalogo_Estado = dbo.in_Catalogo.IdCatalogo LEFT OUTER JOIN
                  dbo.in_movi_inven_tipo AS movi_ing ON dbo.in_Ajuste.IdEmpresa = movi_ing.IdEmpresa AND dbo.in_Ajuste.IdMovi_inven_tipo_ing = movi_ing.IdMovi_inven_tipo LEFT OUTER JOIN
                  dbo.in_movi_inven_tipo AS movi_egr ON dbo.in_Ajuste.IdEmpresa = movi_egr.IdEmpresa AND dbo.in_Ajuste.IdMovi_inven_tipo_egr = movi_egr.IdMovi_inven_tipo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWINV_018';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_categorias"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 257
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "movi_ing"
            Begin Extent = 
               Top = 1351
               Left = 48
               Bottom = 1514
               Right = 303
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "movi_egr"
            Begin Extent = 
               Top = 1519
               Left = 48
               Bottom = 1682
               Right = 303
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Catalogo"
            Begin Extent = 
               Top = 1521
               Left = 351
               Bottom = 1684
               Right = 559
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
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWINV_018';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Top = -1514
         Left = 0
      End
      Begin Tables = 
         Begin Table = "in_UnidadMedida"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 256
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Ajuste"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_AjusteDet"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 255
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_bodega"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 360
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 320
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 323
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_linea"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 256
            End
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWINV_018';

