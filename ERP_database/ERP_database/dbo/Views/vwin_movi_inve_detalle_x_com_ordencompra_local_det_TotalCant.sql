CREATE VIEW [dbo].[vwin_movi_inve_detalle_x_com_ordencompra_local_det_TotalCant]
AS
SELECT        movi_det_oc_det.IdEmpresa_oc AS ocd_IdEmpresa, movi_det_oc_det.IdSucursal_oc AS ocd_IdSucursal, movi_det_oc_det.IdOrdenCompra AS ocd_IdOrdenCompra, 
                         movi_det_oc_det.Secuencia_oc AS ocd_Secuencia, SUM(movi_det.dm_cantidad) AS dm_cantidad_Inv
FROM            dbo.in_Ing_Egr_Inven_det AS movi_det_oc_det INNER JOIN
                         dbo.in_movi_inve_detalle AS movi_det ON movi_det_oc_det.IdEmpresa_inv = movi_det.IdEmpresa AND movi_det_oc_det.IdSucursal_inv = movi_det.IdSucursal AND
                          movi_det_oc_det.IdBodega_inv = movi_det.IdBodega AND movi_det_oc_det.IdMovi_inven_tipo_inv = movi_det.IdMovi_inven_tipo AND 
                         movi_det_oc_det.IdNumMovi_inv = movi_det.IdNumMovi AND movi_det_oc_det.secuencia_inv = movi_det.Secuencia
GROUP BY movi_det_oc_det.IdEmpresa_oc, movi_det_oc_det.IdSucursal_oc, movi_det_oc_det.IdOrdenCompra, movi_det_oc_det.Secuencia_oc
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[1] 2[40] 3) )"
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
         Begin Table = "movi_det_oc_det"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 317
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "movi_det"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 267
               Right = 317
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_movi_inve_detalle_x_com_ordencompra_local_det_TotalCant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_movi_inve_detalle_x_com_ordencompra_local_det_TotalCant';

