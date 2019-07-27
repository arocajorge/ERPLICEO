CREATE VIEW web.VWIMP_002
AS
SELECT        dbo.imp_orden_compra_ext.IdEmpresa, dbo.imp_orden_compra_ext.IdOrdenCompra_ext, dbo.imp_orden_compra_ext.IdProveedor, dbo.imp_orden_compra_ext.IdPais_origen, dbo.imp_orden_compra_ext.IdPais_embarque, 
                         dbo.imp_orden_compra_ext.IdCiudad_destino, dbo.imp_orden_compra_ext.IdCatalogo_via, dbo.imp_orden_compra_ext.IdCatalogo_forma_pago, dbo.imp_orden_compra_ext.oe_fecha, 
                         dbo.imp_orden_compra_ext.oe_fecha_llegada_est, dbo.imp_orden_compra_ext.oe_fecha_embarque_est, dbo.imp_orden_compra_ext.oe_fecha_desaduanizacion_est, dbo.imp_orden_compra_ext.IdCtaCble_importacion, 
                         dbo.imp_orden_compra_ext.oe_observacion, dbo.imp_orden_compra_ext.oe_codigo, dbo.imp_orden_compra_ext.estado, dbo.imp_orden_compra_ext.oe_fecha_llegada, dbo.imp_orden_compra_ext.oe_fecha_embarque, 
                         dbo.imp_orden_compra_ext.oe_fecha_desaduanizacion, dbo.imp_orden_compra_ext.IdMoneda_origen, dbo.imp_orden_compra_ext.IdMoneda_destino, dbo.tb_pais.Nombre AS Paisembarque, tb_pais_1.Nombre AS PaisOrigen, 
                         dbo.imp_catalogo.ca_descripcion AS FormaPago, imp_catalogo_1.ca_descripcion AS ViaEmbarque, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_nombreCompleto, dbo.cp_proveedor.pr_codigo, 
                         dbo.in_Producto.pr_codigo AS Expr1, dbo.in_Producto.pr_descripcion, dbo.in_Producto.lote_fecha_fab, dbo.in_Producto.lote_fecha_vcto, dbo.in_Producto.lote_num_lote, dbo.imp_orden_compra_ext_det.od_cantidad, 
                         dbo.imp_orden_compra_ext_det.od_costo, dbo.imp_orden_compra_ext_det.od_por_descuento, dbo.imp_orden_compra_ext_det.od_descuento, dbo.imp_orden_compra_ext_det.od_costo_final, 
                         dbo.imp_orden_compra_ext_det.od_subtotal, dbo.imp_orden_compra_ext_det.od_cantidad_recepcion, dbo.imp_orden_compra_ext_det.od_costo_convertido, dbo.imp_orden_compra_ext_det.od_total_fob, 
                         dbo.imp_orden_compra_ext_det.od_factor_costo, dbo.imp_orden_compra_ext_det.od_costo_bodega, dbo.imp_orden_compra_ext_det.od_costo_total, dbo.imp_orden_compra_ext_det.IdUnidadMedida, 
                         dbo.tb_ciudad.Descripcion_Ciudad
FROM            dbo.imp_orden_compra_ext INNER JOIN
                         dbo.imp_orden_compra_ext_det ON dbo.imp_orden_compra_ext.IdEmpresa = dbo.imp_orden_compra_ext_det.IdEmpresa AND 
                         dbo.imp_orden_compra_ext.IdOrdenCompra_ext = dbo.imp_orden_compra_ext_det.IdOrdenCompra_ext INNER JOIN
                         dbo.cp_proveedor ON dbo.imp_orden_compra_ext.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.imp_orden_compra_ext.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                         dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.tb_pais ON dbo.imp_orden_compra_ext.IdPais_origen = dbo.tb_pais.IdPais INNER JOIN
                         dbo.tb_pais AS tb_pais_1 ON dbo.imp_orden_compra_ext.IdPais_embarque = tb_pais_1.IdPais INNER JOIN
                         dbo.imp_catalogo ON dbo.imp_orden_compra_ext.IdCatalogo_forma_pago = dbo.imp_catalogo.IdCatalogo INNER JOIN
                         dbo.imp_catalogo AS imp_catalogo_1 ON dbo.imp_orden_compra_ext.IdCatalogo_via = imp_catalogo_1.IdCatalogo INNER JOIN
                         dbo.in_Producto ON dbo.imp_orden_compra_ext_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.imp_orden_compra_ext_det.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                         dbo.tb_ciudad ON dbo.imp_orden_compra_ext.IdCiudad_destino = dbo.tb_ciudad.IdCiudad 
						 GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWIMP_002';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'      End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "imp_catalogo_1"
            Begin Extent = 
               Top = 202
               Left = 529
               Bottom = 332
               Right = 702
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 64
               Left = 379
               Bottom = 385
               Right = 613
            End
            DisplayFlags = 280
            TopColumn = 30
         End
         Begin Table = "imp_liquidacion_det_x_imp_orden_compra_ext"
            Begin Extent = 
               Top = 359
               Left = 514
               Bottom = 584
               Right = 709
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_ciudad"
            Begin Extent = 
               Top = 52
               Left = 503
               Bottom = 182
               Right = 697
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
      Begin ColumnWidths = 31
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWIMP_002';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[85] 4[5] 2[5] 3) )"
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
         Begin Table = "imp_orden_compra_ext"
            Begin Extent = 
               Top = 11
               Left = 0
               Bottom = 337
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "imp_orden_compra_ext_det"
            Begin Extent = 
               Top = 0
               Left = 453
               Bottom = 418
               Right = 664
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_proveedor"
            Begin Extent = 
               Top = 0
               Left = 735
               Bottom = 207
               Right = 967
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 60
               Left = 890
               Bottom = 291
               Right = 1122
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_pais"
            Begin Extent = 
               Top = 77
               Left = 488
               Bottom = 269
               Right = 667
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_pais_1"
            Begin Extent = 
               Top = 290
               Left = 746
               Bottom = 420
               Right = 925
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "imp_catalogo"
            Begin Extent = 
               Top = 196
               Left = 764
               Bottom = 408
               Right = 937
      ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWIMP_002';



