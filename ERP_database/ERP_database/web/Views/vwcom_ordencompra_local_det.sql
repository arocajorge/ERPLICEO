CREATE VIEW web.vwcom_ordencompra_local_det
AS
SELECT        dbo.com_ordencompra_local_det.IdEmpresa, dbo.com_ordencompra_local_det.IdSucursal, dbo.com_ordencompra_local_det.IdOrdenCompra, dbo.com_ordencompra_local_det.Secuencia, 
                         dbo.com_ordencompra_local_det.IdProducto, dbo.com_ordencompra_local_det.do_Cantidad, dbo.com_ordencompra_local_det.do_precioCompra, dbo.com_ordencompra_local_det.do_porc_des, 
                         dbo.com_ordencompra_local_det.do_descuento, dbo.com_ordencompra_local_det.do_precioFinal, dbo.com_ordencompra_local_det.do_subtotal, dbo.com_ordencompra_local_det.do_iva, 
                         dbo.com_ordencompra_local_det.do_total, dbo.com_ordencompra_local_det.do_observacion, dbo.com_ordencompra_local_det.IdUnidadMedida, dbo.com_ordencompra_local_det.Por_Iva, 
                         dbo.com_ordencompra_local_det.IdCod_Impuesto, dbo.in_Producto.pr_descripcion + ' ' + dbo.in_presentacion.nom_presentacion AS pr_descripcion
FROM            dbo.in_presentacion INNER JOIN
                         dbo.in_Producto ON dbo.in_presentacion.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_presentacion.IdPresentacion = dbo.in_Producto.IdPresentacion RIGHT OUTER JOIN
                         dbo.com_ordencompra_local_det ON dbo.in_Producto.IdEmpresa = dbo.com_ordencompra_local_det.IdEmpresa AND dbo.in_Producto.IdProducto = dbo.com_ordencompra_local_det.IdProducto
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcom_ordencompra_local_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'    GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcom_ordencompra_local_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[57] 4[20] 2[6] 3) )"
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
         Top = -192
         Left = 0
      End
      Begin Tables = 
         Begin Table = "in_presentacion"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 225
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 272
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "com_ordencompra_local_det"
            Begin Extent = 
               Top = 220
               Left = 464
               Bottom = 400
               Right = 727
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 24
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
     ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcom_ordencompra_local_det';

