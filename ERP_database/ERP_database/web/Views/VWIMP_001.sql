CREATE VIEW web.VWIMP_001
AS
SELECT        dbo.imp_orden_compra_ext.IdEmpresa, dbo.imp_orden_compra_ext.IdOrdenCompra_ext, dbo.imp_orden_compra_ext.IdProveedor, dbo.imp_orden_compra_ext.oe_fecha, dbo.imp_orden_compra_ext.oe_fecha_llegada_est, 
                         dbo.imp_orden_compra_ext.oe_fecha_embarque_est, dbo.imp_orden_compra_ext.oe_observacion, dbo.tb_moneda.im_descripcion AS NomMoneda, dbo.in_Producto.pr_codigo, dbo.in_Producto.pr_descripcion, 
                         dbo.imp_orden_compra_ext_det.od_cantidad, dbo.imp_orden_compra_ext_det.od_costo, dbo.imp_orden_compra_ext_det.od_por_descuento, dbo.imp_orden_compra_ext_det.od_descuento, 
                         dbo.imp_orden_compra_ext_det.od_costo_final, dbo.imp_orden_compra_ext_det.od_subtotal, dbo.tb_persona.pe_nombreCompleto, dbo.tb_pais.Nombre AS NomPais, dbo.in_presentacion.nom_presentacion, 
                         ViaEmbarque.ca_descripcion AS NomVia, FormaPago.ca_descripcion AS NomFormaPago, dbo.in_UnidadMedida.Descripcion AS NomUnidad, dbo.tb_ciudad.Descripcion_Ciudad, 
                         dbo.imp_orden_compra_ext_det.od_total_fob
FROM            dbo.imp_orden_compra_ext INNER JOIN
                         dbo.imp_orden_compra_ext_det ON dbo.imp_orden_compra_ext.IdEmpresa = dbo.imp_orden_compra_ext_det.IdEmpresa AND 
                         dbo.imp_orden_compra_ext.IdOrdenCompra_ext = dbo.imp_orden_compra_ext_det.IdOrdenCompra_ext INNER JOIN
                         dbo.cp_proveedor ON dbo.imp_orden_compra_ext.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.imp_orden_compra_ext.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                         dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.tb_moneda ON dbo.imp_orden_compra_ext.IdMoneda_origen = dbo.tb_moneda.IdMoneda INNER JOIN
                         dbo.in_Producto ON dbo.imp_orden_compra_ext_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.imp_orden_compra_ext_det.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                         dbo.tb_pais ON dbo.imp_orden_compra_ext.IdPais_embarque = dbo.tb_pais.IdPais INNER JOIN
                         dbo.in_presentacion ON dbo.in_Producto.IdEmpresa = dbo.in_presentacion.IdEmpresa AND dbo.in_Producto.IdPresentacion = dbo.in_presentacion.IdPresentacion INNER JOIN
                         dbo.imp_catalogo AS ViaEmbarque ON dbo.imp_orden_compra_ext.IdCatalogo_via = ViaEmbarque.IdCatalogo INNER JOIN
                         dbo.imp_catalogo AS FormaPago ON dbo.imp_orden_compra_ext.IdCatalogo_forma_pago = FormaPago.IdCatalogo INNER JOIN
                         dbo.in_UnidadMedida ON dbo.imp_orden_compra_ext_det.IdUnidadMedida = dbo.in_UnidadMedida.IdUnidadMedida INNER JOIN
                         dbo.tb_ciudad ON dbo.imp_orden_compra_ext.IdCiudad_destino = dbo.tb_ciudad.IdCiudad
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[72] 4[3] 2[6] 3) )"
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
               Top = 6
               Left = 38
               Bottom = 159
               Right = 287
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "imp_orden_compra_ext_det"
            Begin Extent = 
               Top = 41
               Left = 268
               Bottom = 319
               Right = 502
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "cp_proveedor"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_moneda"
            Begin Extent = 
               Top = 138
               Left = 287
               Bottom = 268
               Right = 457
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 534
               Left = 38
               Bottom = 664
               Right = 272
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_pais"
            Begin Extent = 
               Top = 666
               Left = 38
               Bottom = 796
               Right = 217
       ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWIMP_001';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'     End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_presentacion"
            Begin Extent = 
               Top = 666
               Left = 255
               Bottom = 796
               Right = 442
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ViaEmbarque"
            Begin Extent = 
               Top = 798
               Left = 38
               Bottom = 928
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "FormaPago"
            Begin Extent = 
               Top = 798
               Left = 249
               Bottom = 928
               Right = 422
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_UnidadMedida"
            Begin Extent = 
               Top = 930
               Left = 38
               Bottom = 1060
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_ciudad"
            Begin Extent = 
               Top = 41
               Left = 484
               Bottom = 164
               Right = 678
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
      Begin ColumnWidths = 10
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWIMP_001';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWIMP_001';

