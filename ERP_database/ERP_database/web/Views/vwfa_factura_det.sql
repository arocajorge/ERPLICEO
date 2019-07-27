CREATE VIEW web.vwfa_factura_det
AS
SELECT        dbo.fa_factura_det.IdEmpresa, dbo.fa_factura_det.IdSucursal, dbo.fa_factura_det.IdBodega, dbo.fa_factura_det.IdCbteVta, dbo.fa_factura_det.Secuencia, dbo.fa_factura_det.IdProducto, dbo.fa_factura_det.vt_cantidad, 
                         dbo.fa_factura_det.vt_Precio, dbo.fa_factura_det.vt_PorDescUnitario, dbo.fa_factura_det.vt_DescUnitario, dbo.fa_factura_det.vt_PrecioFinal, dbo.fa_factura_det.vt_Subtotal, dbo.fa_factura_det.vt_iva, dbo.fa_factura_det.vt_total, 
                          dbo.fa_factura_det.vt_detallexItems, dbo.fa_factura_det.vt_por_iva, dbo.fa_factura_det.IdPunto_Cargo, dbo.fa_factura_det.IdPunto_cargo_grupo, dbo.fa_factura_det.IdCod_Impuesto_Iva, 
                          dbo.fa_factura_det.IdCentroCosto,  dbo.fa_factura_det.IdEmpresa_pf, dbo.fa_factura_det.IdSucursal_pf, 
                         dbo.fa_factura_det.IdProforma, dbo.fa_factura_det.Secuencia_pf, dbo.in_Producto.pr_descripcion, dbo.in_presentacion.nom_presentacion, dbo.in_Producto.lote_num_lote, dbo.in_Producto.lote_fecha_vcto, 
                         dbo.in_Producto.se_distribuye, dbo.in_ProductoTipo.tp_ManejaInven
FROM            dbo.in_presentacion RIGHT OUTER JOIN
                         dbo.in_Producto ON dbo.in_presentacion.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.in_presentacion.IdPresentacion = dbo.in_Producto.IdPresentacion LEFT OUTER JOIN
                         dbo.in_ProductoTipo ON dbo.in_Producto.IdEmpresa = dbo.in_ProductoTipo.IdEmpresa AND dbo.in_Producto.IdProductoTipo = dbo.in_ProductoTipo.IdProductoTipo RIGHT OUTER JOIN
                         dbo.fa_factura_det ON dbo.in_Producto.IdEmpresa = dbo.fa_factura_det.IdEmpresa AND dbo.in_Producto.IdProducto = dbo.fa_factura_det.IdProducto
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[67] 4[3] 2[11] 3) )"
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
         Begin Table = "in_presentacion"
            Begin Extent = 
               Top = 197
               Left = 30
               Bottom = 327
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 0
               Left = 625
               Bottom = 552
               Right = 859
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_factura_det"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 301
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_ProductoTipo"
            Begin Extent = 
               Top = 26
               Left = 979
               Bottom = 298
               Right = 1212
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
       ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwfa_factura_det';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwfa_factura_det';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'  Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwfa_factura_det';

