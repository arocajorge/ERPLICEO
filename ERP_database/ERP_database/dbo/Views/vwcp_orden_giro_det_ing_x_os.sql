CREATE VIEW dbo.vwcp_orden_giro_det_ing_x_os
AS
SELECT        dbo.cp_orden_giro_det_ing_x_os.IdEmpresa, dbo.cp_orden_giro_det_ing_x_os.IdCbteCble_Ogiro, dbo.cp_orden_giro_det_ing_x_os.IdTipoCbte_Ogiro, dbo.cp_orden_giro_det_ing_x_os.Secuencia, 
                         dbo.cp_orden_giro_det_ing_x_os.oc_IdSucursal, dbo.cp_orden_giro_det_ing_x_os.oc_IdOrdenCompra, dbo.cp_orden_giro_det_ing_x_os.oc_Secuencia, dbo.cp_orden_giro_det_ing_x_os.dm_cantidad, 
                         dbo.cp_orden_giro_det_ing_x_os.do_porc_des, dbo.cp_orden_giro_det_ing_x_os.do_descuento, dbo.cp_orden_giro_det_ing_x_os.do_precioFinal, dbo.cp_orden_giro_det_ing_x_os.do_subtotal, 
                         dbo.cp_orden_giro_det_ing_x_os.IdCod_Impuesto, dbo.cp_orden_giro_det_ing_x_os.do_iva, dbo.cp_orden_giro_det_ing_x_os.Por_Iva, dbo.cp_orden_giro_det_ing_x_os.do_total, 
                         dbo.cp_orden_giro_det_ing_x_os.IdUnidadMedida, dbo.cp_orden_giro_det_ing_x_os.IdProducto, dbo.in_UnidadMedida.Descripcion AS NomUnidadMedida, dbo.in_Producto.pr_descripcion, 
                         dbo.cp_orden_giro_det_ing_x_os.do_precioCompra
FROM            dbo.in_UnidadMedida INNER JOIN
                         dbo.cp_orden_giro_det_ing_x_os ON dbo.in_UnidadMedida.IdUnidadMedida = dbo.cp_orden_giro_det_ing_x_os.IdUnidadMedida INNER JOIN
                         dbo.in_Producto ON dbo.cp_orden_giro_det_ing_x_os.IdProducto = dbo.in_Producto.IdProducto AND dbo.cp_orden_giro_det_ing_x_os.IdEmpresa = dbo.in_Producto.IdEmpresa
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_giro_det_ing_x_os';


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
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "in_UnidadMedida"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_giro_det_ing_x_os"
            Begin Extent = 
               Top = 6
               Left = 255
               Bottom = 211
               Right = 448
            End
            DisplayFlags = 280
            TopColumn = 11
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 6
               Left = 486
               Bottom = 204
               Right = 720
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_giro_det_ing_x_os';

