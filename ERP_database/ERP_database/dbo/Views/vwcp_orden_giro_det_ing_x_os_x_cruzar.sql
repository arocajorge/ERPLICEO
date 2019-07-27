CREATE VIEW dbo.vwcp_orden_giro_det_ing_x_os_x_cruzar
AS
SELECT        dbo.com_ordencompra_local_det.IdEmpresa, dbo.com_ordencompra_local_det.IdSucursal, dbo.com_ordencompra_local_det.IdOrdenCompra, dbo.com_ordencompra_local_det.Secuencia, dbo.com_ordencompra_local.Tipo, 
                         dbo.com_ordencompra_local.SecuenciaTipo, dbo.com_ordencompra_local_det.IdProducto, dbo.in_Producto.pr_descripcion, dbo.com_ordencompra_local_det.do_Cantidad, dbo.com_ordencompra_local_det.do_precioFinal, 
                         dbo.com_ordencompra_local_det.IdUnidadMedida, dbo.com_ordencompra_local.IdProveedor, dbo.tb_persona.pe_nombreCompleto, dbo.com_ordencompra_local.oc_observacion, dbo.com_ordencompra_local.oc_fecha, 
                         dbo.com_ordencompra_local_det.do_precioCompra, dbo.com_ordencompra_local_det.do_porc_des, dbo.com_ordencompra_local_det.do_descuento, dbo.com_ordencompra_local_det.do_subtotal, 
                         dbo.com_ordencompra_local_det.do_total, dbo.com_ordencompra_local_det.do_observacion, dbo.com_ordencompra_local_det.Por_Iva, dbo.com_ordencompra_local_det.do_iva, 
                         dbo.com_ordencompra_local_det.IdCod_Impuesto
FROM            dbo.com_ordencompra_local INNER JOIN
                         dbo.com_ordencompra_local_det ON dbo.com_ordencompra_local.IdEmpresa = dbo.com_ordencompra_local_det.IdEmpresa AND dbo.com_ordencompra_local.IdSucursal = dbo.com_ordencompra_local_det.IdSucursal AND 
                         dbo.com_ordencompra_local.IdOrdenCompra = dbo.com_ordencompra_local_det.IdOrdenCompra INNER JOIN
                         dbo.in_Producto ON dbo.com_ordencompra_local_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.com_ordencompra_local_det.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                         dbo.cp_proveedor ON dbo.com_ordencompra_local.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.com_ordencompra_local.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                         dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona LEFT OUTER JOIN
                         dbo.cp_orden_giro_det_ing_x_os ON dbo.cp_orden_giro_det_ing_x_os.IdEmpresa = dbo.com_ordencompra_local_det.IdEmpresa AND 
                         dbo.cp_orden_giro_det_ing_x_os.oc_IdSucursal = dbo.com_ordencompra_local_det.IdSucursal AND dbo.cp_orden_giro_det_ing_x_os.oc_IdOrdenCompra = dbo.com_ordencompra_local_det.IdOrdenCompra AND 
                         dbo.cp_orden_giro_det_ing_x_os.oc_Secuencia = dbo.com_ordencompra_local_det.Secuencia
WHERE        (dbo.com_ordencompra_local.Tipo = 'OS') AND (dbo.cp_orden_giro_det_ing_x_os.IdEmpresa IS NULL) AND (dbo.com_ordencompra_local.IdEstadoAprobacion_cat = 'APRO')
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_giro_det_ing_x_os_x_cruzar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'dth = 284
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_giro_det_ing_x_os_x_cruzar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[30] 4[34] 2[12] 3) )"
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
         Begin Table = "com_ordencompra_local"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 255
            End
            DisplayFlags = 280
            TopColumn = 10
         End
         Begin Table = "com_ordencompra_local_det"
            Begin Extent = 
               Top = 6
               Left = 293
               Bottom = 136
               Right = 496
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 6
               Left = 534
               Bottom = 136
               Right = 768
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_proveedor"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 138
               Left = 308
               Bottom = 268
               Right = 540
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_giro_det_ing_x_os"
            Begin Extent = 
               Top = 138
               Left = 578
               Bottom = 268
               Right = 771
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
         Wi', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_giro_det_ing_x_os_x_cruzar';

