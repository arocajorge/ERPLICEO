CREATE VIEW web.vwin_Ing_Egr_Inven_det_x_devolver
AS
SELECT dbo.in_Ing_Egr_Inven_det.IdEmpresa, dbo.in_Ing_Egr_Inven_det.IdSucursal, dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven_det.IdNumMovi, dbo.in_Ing_Egr_Inven_det.Secuencia, dbo.in_Ing_Egr_Inven_det.IdBodega, 
                  dbo.in_Ing_Egr_Inven_det.dm_cantidad_sinConversion, dbo.in_Ing_Egr_Inven_det.IdUnidadMedida_sinConversion, ISNULL(dbo.in_Ing_Egr_Inven_det.mv_costo_sinConversion, 0) AS mv_costo_sinConversion, 
                  dbo.in_Producto.pr_descripcion, dbo.in_presentacion.nom_presentacion, dbo.in_Producto.lote_fecha_vcto, dbo.in_Producto.lote_num_lote, ISNULL(dev.cant_devuelta, 0) AS cant_devuelta, 
                  dbo.in_UnidadMedida.Descripcion AS NomUnidad, dbo.in_Ing_Egr_Inven_det.IdProducto
FROM     dbo.in_UnidadMedida INNER JOIN
                  dbo.in_Ing_Egr_Inven_det ON dbo.in_UnidadMedida.IdUnidadMedida = dbo.in_Ing_Egr_Inven_det.IdUnidadMedida_sinConversion LEFT OUTER JOIN
                  dbo.in_presentacion INNER JOIN
                  dbo.in_Producto ON dbo.in_presentacion.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_presentacion.IdPresentacion = dbo.in_Producto.IdPresentacion ON dbo.in_Ing_Egr_Inven_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND 
                  dbo.in_Ing_Egr_Inven_det.IdProducto = dbo.in_Producto.IdProducto LEFT OUTER JOIN
                      (SELECT dbo.in_devolucion_inven_det.inv_IdEmpresa, dbo.in_devolucion_inven_det.inv_IdSucursal, dbo.in_devolucion_inven_det.inv_IdMovi_inven_tipo, dbo.in_devolucion_inven_det.inv_IdNumMovi, 
                                         dbo.in_devolucion_inven_det.inv_Secuencia, SUM(dbo.in_devolucion_inven_det.cant_devuelta) AS cant_devuelta
                       FROM      dbo.in_devolucion_inven_det INNER JOIN
                                         dbo.in_devolucion_inven ON dbo.in_devolucion_inven_det.IdEmpresa = dbo.in_devolucion_inven.IdEmpresa AND dbo.in_devolucion_inven_det.IdDev_Inven = dbo.in_devolucion_inven.IdDev_Inven
                       WHERE   (dbo.in_devolucion_inven.Estado = 1)
                       GROUP BY dbo.in_devolucion_inven_det.inv_IdEmpresa, dbo.in_devolucion_inven_det.inv_IdSucursal, dbo.in_devolucion_inven_det.inv_IdMovi_inven_tipo, dbo.in_devolucion_inven_det.inv_IdNumMovi, 
                                         dbo.in_devolucion_inven_det.inv_Secuencia) AS dev ON dbo.in_Ing_Egr_Inven_det.IdEmpresa = dev.inv_IdEmpresa AND dbo.in_Ing_Egr_Inven_det.IdSucursal = dev.inv_IdSucursal AND 
                  dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo = dev.inv_IdMovi_inven_tipo AND dbo.in_Ing_Egr_Inven_det.IdNumMovi = dev.inv_IdNumMovi AND dbo.in_Ing_Egr_Inven_det.Secuencia = dev.inv_Secuencia
WHERE  (ROUND(ABS(dbo.in_Ing_Egr_Inven_det.dm_cantidad_sinConversion) - ISNULL(dev.cant_devuelta, 0), 2) > 0)
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
         Top = -360
         Left = 0
      End
      Begin Tables = 
         Begin Table = "in_UnidadMedida"
            Begin Extent = 
               Top = 401
               Left = 457
               Bottom = 564
               Right = 665
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Ing_Egr_Inven_det"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 357
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "in_presentacion"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 264
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 323
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dev"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 288
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
      Begin ColumnWidths = 17
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
     ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwin_Ing_Egr_Inven_det_x_devolver';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'    Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwin_Ing_Egr_Inven_det_x_devolver';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwin_Ing_Egr_Inven_det_x_devolver';

