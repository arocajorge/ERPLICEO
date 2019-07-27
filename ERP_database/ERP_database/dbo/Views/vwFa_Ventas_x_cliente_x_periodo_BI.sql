CREATE VIEW [dbo].[vwFa_Ventas_x_cliente_x_periodo_BI]
AS
SELECT     dbo.tb_empresa.IdEmpresa, dbo.tb_empresa.em_nombre AS Empresa, dbo.tb_sucursal.IdSucursal, dbo.tb_sucursal.Su_Descripcion AS Sucursal, 
                      dbo.tb_bodega.IdBodega, dbo.tb_bodega.bo_Descripcion AS Bodega, dbo.fa_cliente.IdCliente, dbo.tb_persona.pe_nombreCompleto AS Cliente, 
                      dbo.fa_Vendedor.IdVendedor, dbo.fa_Vendedor.Ve_Vendedor AS Vendedor, dbo.in_Producto.IdProducto, dbo.in_Producto.pr_codigo AS CodProducto, 
                      dbo.in_Producto.pr_descripcion AS Producto, dbo.in_categorias.IdCategoria, dbo.in_categorias.ca_Categoria AS Categoria, dbo.fa_factura.vt_tipoDoc, 
                      dbo.fa_factura.vt_serie1, dbo.fa_factura.vt_serie2, dbo.fa_factura.vt_NumFactura, dbo.fa_factura.vt_fecha, dbo.tb_Calendario.IdCalendario, 
                      dbo.tb_Calendario.AnioFiscal, dbo.tb_Calendario.NombreMes, dbo.tb_Calendario.NombreFecha, dbo.tb_Calendario.Mes_x_anio, dbo.fa_factura_det.vt_cantidad, 
                      dbo.fa_factura_det.vt_Subtotal
FROM         dbo.fa_factura INNER JOIN
                      dbo.fa_factura_det ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_det.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_det.IdSucursal AND 
                      dbo.fa_factura.IdBodega = dbo.fa_factura_det.IdBodega AND dbo.fa_factura.IdCbteVta = dbo.fa_factura_det.IdCbteVta INNER JOIN
                      dbo.in_Producto ON dbo.fa_factura_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.fa_factura_det.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                      dbo.in_categorias ON dbo.in_Producto.IdEmpresa = dbo.in_categorias.IdEmpresa AND dbo.in_Producto.IdCategoria = dbo.in_categorias.IdCategoria INNER JOIN
                      dbo.fa_cliente ON dbo.fa_factura.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_factura.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                      dbo.fa_Vendedor ON dbo.fa_factura.IdEmpresa = dbo.fa_Vendedor.IdEmpresa AND dbo.fa_factura.IdVendedor = dbo.fa_Vendedor.IdVendedor INNER JOIN
                      dbo.tb_sucursal ON dbo.fa_factura.IdEmpresa = dbo.tb_sucursal.IdEmpresa INNER JOIN
                      dbo.tb_bodega ON dbo.fa_factura.IdEmpresa = dbo.tb_bodega.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.tb_bodega.IdSucursal AND 
                      dbo.fa_factura.IdBodega = dbo.tb_bodega.IdBodega AND dbo.tb_sucursal.IdEmpresa = dbo.tb_bodega.IdEmpresa AND 
                      dbo.tb_sucursal.IdSucursal = dbo.tb_bodega.IdSucursal INNER JOIN
                      dbo.tb_empresa ON dbo.fa_Vendedor.IdEmpresa = dbo.tb_empresa.IdEmpresa AND dbo.tb_sucursal.IdEmpresa = dbo.tb_empresa.IdEmpresa INNER JOIN
                      dbo.tb_Calendario ON dbo.fa_factura.vt_fecha = dbo.tb_Calendario.fecha INNER JOIN
                      dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona
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
         Begin Table = "fa_factura"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 219
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_factura_det"
            Begin Extent = 
               Top = 0
               Left = 760
               Bottom = 213
               Right = 941
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 245
               Right = 260
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_categorias"
            Begin Extent = 
               Top = 246
               Left = 38
               Bottom = 365
               Right = 260
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_cliente"
            Begin Extent = 
               Top = 366
               Left = 38
               Bottom = 485
               Right = 248
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_Vendedor"
            Begin Extent = 
               Top = 126
               Left = 298
               Bottom = 245
               Right = 466
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 486
               Left = 38
               Bottom = 605
               Right = 252
            End
  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwFa_Ventas_x_cliente_x_periodo_BI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'          DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_bodega"
            Begin Extent = 
               Top = 606
               Left = 38
               Bottom = 725
               Right = 236
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_empresa"
            Begin Extent = 
               Top = 726
               Left = 38
               Bottom = 845
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_Calendario"
            Begin Extent = 
               Top = 366
               Left = 286
               Bottom = 485
               Right = 468
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 606
               Left = 274
               Bottom = 725
               Right = 466
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
      Begin ColumnWidths = 12
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwFa_Ventas_x_cliente_x_periodo_BI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwFa_Ventas_x_cliente_x_periodo_BI';

