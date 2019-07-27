CREATE view [web].[vwct_RevisionContableFacturas]
as
SELECT        dbo.fa_factura.IdEmpresa, dbo.fa_factura.IdSucursal, dbo.fa_factura.IdBodega, dbo.fa_factura.IdCbteVta, ISNULL(dbo.fa_factura_x_ct_cbtecble.ct_IdEmpresa, 0) AS ct_IdEmpresa, 
                         ISNULL(dbo.fa_factura_x_ct_cbtecble.ct_IdTipoCbte, 0) AS ct_IdTipoCbte, ISNULL(dbo.fa_factura_x_ct_cbtecble.ct_IdCbteCble, 0) AS ct_IdCbteCble, dbo.fa_cliente_contactos.Nombres, 
                         dbo.fa_factura.vt_tipoDoc + '-' + dbo.fa_factura.vt_serie1 + '-' + dbo.fa_factura.vt_serie2 + '-' + dbo.fa_factura.vt_NumFactura AS Referencia, dbo.fa_factura.vt_fecha, fa_factura_resumen.Total AS TotalModulo, 
                         ISNULL(ROUND(SUM(dbo.ct_cbtecble_det.dc_Valor), 2), 0) AS TotalContabilidad, fa_factura_resumen.Total - ISNULL(ROUND(SUM(dbo.ct_cbtecble_det.dc_Valor), 2), 0) AS Diferencia
FROM            dbo.fa_factura INNER JOIN
                         dbo.fa_cliente ON dbo.fa_factura.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_factura.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                         dbo.fa_cliente_contactos ON dbo.fa_factura.IdEmpresa = dbo.fa_cliente_contactos.IdEmpresa AND dbo.fa_factura.IdCliente = dbo.fa_cliente_contactos.IdCliente INNER JOIN
                         dbo.fa_factura_resumen ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_resumen.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_resumen.IdSucursal AND 
                         dbo.fa_factura.IdBodega = dbo.fa_factura_resumen.IdBodega AND dbo.fa_factura.IdCbteVta = dbo.fa_factura_resumen.IdCbteVta LEFT OUTER JOIN
                         dbo.fa_factura_x_ct_cbtecble ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_x_ct_cbtecble.vt_IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_x_ct_cbtecble.vt_IdSucursal AND 
                         dbo.fa_factura.IdBodega = dbo.fa_factura_x_ct_cbtecble.vt_IdBodega AND dbo.fa_factura.IdCbteVta = dbo.fa_factura_x_ct_cbtecble.vt_IdCbteVta LEFT OUTER JOIN
                         dbo.ct_cbtecble_det ON dbo.fa_factura_x_ct_cbtecble.ct_IdEmpresa = dbo.ct_cbtecble_det.IdEmpresa AND dbo.fa_factura_x_ct_cbtecble.ct_IdTipoCbte = dbo.ct_cbtecble_det.IdTipoCbte AND 
                         dbo.fa_factura_x_ct_cbtecble.ct_IdCbteCble = dbo.ct_cbtecble_det.IdCbteCble AND dbo.fa_cliente.IdCtaCble_cxc_Credito = ISNULL(dbo.ct_cbtecble_det.IdCtaCble, dbo.fa_cliente.IdCtaCble_cxc_Credito)
WHERE        (dbo.fa_factura.Estado = 'A')
GROUP BY dbo.fa_factura_x_ct_cbtecble.ct_IdEmpresa, dbo.fa_factura_x_ct_cbtecble.ct_IdTipoCbte, dbo.fa_factura_x_ct_cbtecble.ct_IdCbteCble, fa_factura_resumen.Total, dbo.fa_factura.IdEmpresa, dbo.fa_factura.IdSucursal, dbo.fa_factura.IdBodega, 
                         dbo.fa_factura.IdCbteVta, dbo.fa_cliente_contactos.Nombres, dbo.fa_factura.vt_serie1, dbo.fa_factura.vt_serie2, dbo.fa_factura.vt_NumFactura, dbo.fa_factura.vt_tipoDoc, dbo.fa_factura.vt_fecha
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwct_RevisionContableFacturas';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'umnWidths = 12
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwct_RevisionContableFacturas';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[64] 4[3] 2[14] 3) )"
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
               Left = 490
               Bottom = 130
               Right = 719
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_cliente"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_cliente_contactos"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_factura_x_ct_cbtecble"
            Begin Extent = 
               Top = 257
               Left = 99
               Bottom = 511
               Right = 285
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_cbtecble_det"
            Begin Extent = 
               Top = 275
               Left = 315
               Bottom = 405
               Right = 528
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_factura_resumen"
            Begin Extent = 
               Top = 0
               Left = 873
               Bottom = 130
               Right = 1108
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
      Begin Col', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwct_RevisionContableFacturas';

