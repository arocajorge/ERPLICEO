CREATE VIEW dbo.vwin_Ing_Egr_Inven_distribucion_x_distribuir
AS
SELECT        dbo.in_Ing_Egr_Inven.IdEmpresa, dbo.in_Ing_Egr_Inven.IdSucursal, dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven.IdNumMovi, dbo.in_Ing_Egr_Inven.signo, dbo.in_Producto.IdProducto_padre, 
                         dbo.in_Ing_Egr_Inven_det.IdProducto, dbo.in_Producto.pr_descripcion, dbo.in_Ing_Egr_Inven_det.IdUnidadMedida, ABS(dbo.in_Ing_Egr_Inven_det.dm_cantidad) AS can_total, ABS(ISNULL(SUM(Dist.dm_cantidad), 0)) 
                         AS can_distribuida, ISNULL(ABS(dbo.in_Ing_Egr_Inven_det.dm_cantidad) - ABS(ISNULL(SUM(Dist.dm_cantidad), 0)), 0) AS can_x_distribuir, dbo.in_Producto.IdCategoria, dbo.in_Producto.IdPresentacion
FROM            dbo.in_Ing_Egr_Inven INNER JOIN
                         dbo.in_Ing_Egr_Inven_det ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.in_Ing_Egr_Inven_det.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdSucursal = dbo.in_Ing_Egr_Inven_det.IdSucursal AND 
                         dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo = dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo AND dbo.in_Ing_Egr_Inven.IdNumMovi = dbo.in_Ing_Egr_Inven_det.IdNumMovi INNER JOIN
                         dbo.in_Producto ON dbo.in_Ing_Egr_Inven_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_Ing_Egr_Inven_det.IdProducto = dbo.in_Producto.IdProducto LEFT OUTER JOIN
                             (SELECT        distribucion.IdEmpresa, distribucion.IdSucursal, distribucion.IdMovi_inven_tipo, distribucion.IdNumMovi, det_mov.IdUnidadMedida, det_mov.IdProducto, det_mov.dm_cantidad, dis_pro.IdProducto_padre
                               FROM            dbo.in_Ing_Egr_Inven AS cab_mov INNER JOIN
                                                         dbo.in_Ing_Egr_Inven_det AS det_mov ON cab_mov.IdEmpresa = det_mov.IdEmpresa AND cab_mov.IdSucursal = det_mov.IdSucursal AND cab_mov.IdMovi_inven_tipo = det_mov.IdMovi_inven_tipo AND 
                                                         cab_mov.IdNumMovi = det_mov.IdNumMovi INNER JOIN
                                                         dbo.in_Ing_Egr_Inven_distribucion AS distribucion ON cab_mov.IdEmpresa = distribucion.IdEmpresa_dis AND cab_mov.IdSucursal = distribucion.IdSucursal_dis AND 
                                                         cab_mov.IdMovi_inven_tipo = distribucion.IdMovi_inven_tipo_dis AND cab_mov.IdNumMovi = distribucion.IdNumMovi_dis INNER JOIN
                                                         dbo.in_Producto AS dis_pro ON det_mov.IdEmpresa = dis_pro.IdEmpresa AND det_mov.IdProducto = dis_pro.IdProducto
                               WHERE        (cab_mov.Estado = 'A') AND (ISNULL(dis_pro.se_distribuye, 0) = 0)) AS Dist ON dbo.in_Ing_Egr_Inven.IdEmpresa = Dist.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdSucursal = Dist.IdSucursal AND 
                         dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo = Dist.IdMovi_inven_tipo AND dbo.in_Ing_Egr_Inven.IdNumMovi = Dist.IdNumMovi AND dbo.in_Producto.IdProducto_padre = Dist.IdProducto_padre
WHERE        (dbo.in_Ing_Egr_Inven.Estado = 'A') AND (dbo.in_Producto.se_distribuye = 1)
GROUP BY dbo.in_Ing_Egr_Inven.IdEmpresa, dbo.in_Ing_Egr_Inven.IdSucursal, dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven.IdNumMovi, dbo.in_Ing_Egr_Inven.signo, dbo.in_Producto.IdProducto_padre, 
                         dbo.in_Ing_Egr_Inven_det.IdProducto, dbo.in_Producto.pr_descripcion, dbo.in_Ing_Egr_Inven_det.IdUnidadMedida, ABS(dbo.in_Ing_Egr_Inven_det.dm_cantidad), dbo.in_Producto.IdCategoria, dbo.in_Producto.IdPresentacion
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[84] 4[5] 2[5] 3) )"
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
         Top = -93
         Left = 0
      End
      Begin Tables = 
         Begin Table = "in_Ing_Egr_Inven"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 240
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Ing_Egr_Inven_det"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 317
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 153
               Left = 621
               Bottom = 508
               Right = 871
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "Dist"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 240
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_Ing_Egr_Inven_distribucion_x_distribuir';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_Ing_Egr_Inven_distribucion_x_distribuir';

