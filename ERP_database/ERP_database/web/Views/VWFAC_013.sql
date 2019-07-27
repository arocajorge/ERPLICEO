CREATE VIEW web.VWFAC_013
AS
SELECT        dbo.fa_factura_det.IdEmpresa, dbo.tb_sucursal.Su_CodigoEstablecimiento, dbo.tb_sucursal.Su_Descripcion, dbo.tb_sucursal.Su_Direccion, dbo.tb_sucursal.Su_Telefonos, dbo.fa_factura_det.IdSucursal, 
                         dbo.fa_factura.IdCliente, dbo.tb_persona.pe_nombreCompleto AS nombre_cliente, dbo.tb_persona.pe_cedulaRuc AS ced_ruc_cliente, dbo.tb_persona.pe_direccion AS direccion_cliente, 
                         dbo.tb_persona.pe_celular AS celular_cliente, dbo.tb_persona.pe_telfono_Contacto AS telefono_cliente, dbo.fa_factura_det.IdProforma, dbo.fa_factura_det.Secuencia, dbo.fa_TerminoPago.nom_TerminoPago, 
                         dbo.fa_factura.vt_plazo, dbo.fa_factura.CodCbteVta, dbo.fa_factura.vt_fecha, dbo.fa_factura.Estado, dbo.fa_Vendedor.Codigo, dbo.fa_Vendedor.Ve_Vendedor, dbo.fa_factura_det.vt_cantidad, dbo.fa_factura_det.vt_Precio, 
                         dbo.fa_factura_det.vt_PorDescUnitario, dbo.fa_factura_det.vt_DescUnitario, dbo.fa_factura_det.vt_PrecioFinal, dbo.fa_factura_det.vt_Subtotal, dbo.fa_factura_det.vt_por_iva, dbo.fa_factura_det.vt_iva, 
                         dbo.fa_factura_det.vt_Subtotal + dbo.fa_factura_det.vt_iva AS pd_total, dbo.in_Producto.pr_observacion, dbo.fa_factura_det.IdProducto, dbo.fa_factura.vt_Observacion, dbo.fa_factura_det.IdBodega, dbo.fa_factura_det.IdCbteVta, 
                         dbo.in_Producto.pr_descripcion, dbo.fa_factura.vt_serie1 + '-' + dbo.fa_factura.vt_serie2 + '-' + dbo.fa_factura.vt_NumFactura AS vt_NumFactura, dbo.fa_factura_resumen.SubtotalIVASinDscto, 
                         dbo.fa_factura_resumen.SubtotalSinIVASinDscto, dbo.fa_factura_resumen.SubtotalSinDscto, dbo.fa_factura_resumen.Descuento, dbo.fa_factura_resumen.SubtotalIVAConDscto, dbo.fa_factura_resumen.SubtotalSinIVAConDscto, 
                         dbo.fa_factura_resumen.SubtotalConDscto, dbo.fa_factura_resumen.ValorIVA, dbo.fa_factura_resumen.Total, dbo.fa_factura_resumen.ValorEfectivo, dbo.fa_factura_resumen.Cambio
FROM            dbo.fa_factura INNER JOIN
                         dbo.fa_factura_det ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_det.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_det.IdSucursal AND dbo.fa_factura.IdCbteVta = dbo.fa_factura_det.IdCbteVta AND 
                         dbo.fa_factura.IdBodega = dbo.fa_factura_det.IdBodega INNER JOIN
                         dbo.fa_cliente ON dbo.fa_cliente.IdCliente = dbo.fa_factura.IdCliente AND dbo.fa_factura.IdEmpresa = dbo.fa_cliente.IdEmpresa INNER JOIN
                         dbo.tb_persona ON dbo.tb_persona.IdPersona = dbo.fa_cliente.IdPersona INNER JOIN
                         dbo.fa_Vendedor ON dbo.fa_factura.IdEmpresa = dbo.fa_Vendedor.IdEmpresa AND dbo.fa_factura.IdVendedor = dbo.fa_Vendedor.IdVendedor INNER JOIN
                         dbo.fa_TerminoPago ON dbo.fa_factura.vt_tipo_venta = dbo.fa_TerminoPago.IdTerminoPago LEFT OUTER JOIN
                         dbo.fa_factura_resumen ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_resumen.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_resumen.IdSucursal AND 
                         dbo.fa_factura.IdBodega = dbo.fa_factura_resumen.IdBodega AND dbo.fa_factura.IdCbteVta = dbo.fa_factura_resumen.IdCbteVta LEFT OUTER JOIN
                         dbo.in_Producto ON dbo.fa_factura_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.fa_factura_det.IdProducto = dbo.in_Producto.IdProducto LEFT OUTER JOIN
                         dbo.tb_sucursal ON dbo.tb_sucursal.IdSucursal = dbo.fa_factura_det.IdSucursal AND dbo.tb_sucursal.IdEmpresa = dbo.fa_factura_det.IdEmpresa
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWFAC_013';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'          DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 798
               Left = 38
               Bottom = 928
               Right = 268
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_factura_resumen"
            Begin Extent = 
               Top = 0
               Left = 597
               Bottom = 332
               Right = 816
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
      Begin ColumnWidths = 49
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWFAC_013';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[20] 4[41] 2[20] 3) )"
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
         Begin Table = "fa_factura"
            Begin Extent = 
               Top = 0
               Left = 314
               Bottom = 130
               Right = 527
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_factura_det"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 301
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_cliente"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 254
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
         Begin Table = "fa_Vendedor"
            Begin Extent = 
               Top = 177
               Left = 0
               Bottom = 307
               Right = 179
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_TerminoPago"
            Begin Extent = 
               Top = 534
               Left = 38
               Bottom = 664
               Right = 229
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 666
               Left = 38
               Bottom = 796
               Right = 272
            End
  ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWFAC_013';

