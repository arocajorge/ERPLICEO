CREATE VIEW web.VWCXP_017
AS
SELECT a.IdEmpresa, a.IdTipoCbte_Ogiro, a.IdCbteCble_Ogiro, c.Descripcion AS NomDocumento, a.co_serie, a.co_factura, a.Num_Autorizacion, a.fecha_autorizacion, f.Su_Descripcion, f.Su_Direccion, g.pe_nombreCompleto, g.pe_cedulaRuc, 
                  a.co_FechaFactura, a.co_observacion, i.IdFormaPago, i.nom_FormaPago, a.co_subtotal_iva, a.co_subtotal_siniva, a.co_subtotal_iva + a.co_subtotal_siniva AS co_subtotal, a.co_total, a.co_valoriva, j.pr_descripcion, j.pr_codigo, b.Subtotal, 
                  b.DescuentoUni * b.Cantidad AS Descuento, b.ValorIva, b.Total AS TotalDetalle, e.pr_direccion, e.pr_correo, b.Cantidad, b.CostoUni
FROM     dbo.cp_orden_giro AS a INNER JOIN
                  dbo.cp_orden_giro_det AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdTipoCbte_Ogiro = b.IdTipoCbte_Ogiro AND a.IdCbteCble_Ogiro = b.IdCbteCble_Ogiro INNER JOIN
                  dbo.cp_TipoDocumento AS c ON a.IdOrden_giro_Tipo = c.CodTipoDocumento INNER JOIN
                  dbo.tb_sis_Documento_Tipo_Talonario AS d ON c.Codigo = d.CodDocumentoTipo AND a.co_serie = d.Establecimiento + '-' + d.PuntoEmision AND a.co_factura = d.NumDocumento AND a.IdEmpresa = d.IdEmpresa INNER JOIN
                  dbo.cp_proveedor AS e ON e.IdEmpresa = a.IdEmpresa AND e.IdProveedor = a.IdProveedor INNER JOIN
                  dbo.tb_sucursal AS f ON a.IdEmpresa = f.IdEmpresa AND a.IdSucursal = f.IdSucursal INNER JOIN
                  dbo.tb_persona AS g ON e.IdPersona = g.IdPersona LEFT OUTER JOIN
                  dbo.cp_orden_giro_pagos_sri AS h ON a.IdEmpresa = h.IdEmpresa AND a.IdTipoCbte_Ogiro = h.IdTipoCbte_Ogiro AND a.IdCbteCble_Ogiro = h.IdCbteCble_Ogiro LEFT OUTER JOIN
                  dbo.fa_formaPago AS i ON h.codigo_pago_sri = i.IdFormaPago INNER JOIN
                  dbo.in_Producto AS j ON b.IdEmpresa = j.IdEmpresa AND b.IdProducto = j.IdProducto
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCXP_017';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'End
         Begin Table = "h"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 261
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "i"
            Begin Extent = 
               Top = 1351
               Left = 48
               Bottom = 1514
               Right = 256
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "j"
            Begin Extent = 
               Top = 1519
               Left = 48
               Bottom = 1682
               Right = 323
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCXP_017';


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
         Top = -120
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 355
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 276
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 366
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 321
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "e"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 17
         End
         Begin Table = "f"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 320
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "g"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCXP_017';

