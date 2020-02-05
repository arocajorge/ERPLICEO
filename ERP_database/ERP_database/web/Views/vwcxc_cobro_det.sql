CREATE VIEW web.vwcxc_cobro_det
AS
SELECT dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdCobro, dbo.cxc_cobro_det.secuencial, dbo.cxc_cobro_det.dc_TipoDocumento, dbo.cxc_cobro_det.IdBodega_Cbte, dbo.cxc_cobro_det.IdCbte_vta_nota, 
                  dbo.cxc_cobro_det.dc_ValorPago, dbo.cxc_cobro_det.IdCobro_tipo, dbo.cxc_cobro_det.dc_TipoDocumento + '-' + CAST(CAST(ISNULL(dbo.fa_factura.vt_NumFactura, dbo.fa_notaCreDeb.NumNota_Impresa) AS int) AS VARCHAR(20)) 
                  AS vt_NumFactura, ISNULL(dbo.fa_factura.vt_Observacion, dbo.fa_notaCreDeb.sc_observacion) AS vt_Observacion, ISNULL(dbo.fa_factura.CodCbteVta, dbo.fa_notaCreDeb.CodNota) AS CodDoc, 
                  ISNULL(SUM(dbo.fa_factura_det.vt_Subtotal), 0) + ISNULL(SUM(dbo.fa_notaCreDeb_det.sc_subtotal), 0) AS vt_Subtotal, ISNULL(SUM(dbo.fa_factura_det.vt_iva), 0) + ISNULL(SUM(dbo.fa_notaCreDeb_det.sc_iva), 0) AS vt_iva, 
                  ISNULL(SUM(dbo.fa_factura_det.vt_total), 0) + ISNULL(SUM(dbo.fa_notaCreDeb_det.sc_total), 0) AS vt_total, ISNULL(cobro.ValorCobrado, 0) AS ValorCobrado, ISNULL(SUM(dbo.fa_factura_det.vt_total), 0) 
                  + ISNULL(SUM(dbo.fa_notaCreDeb_det.sc_total), 0) - ISNULL(cobro.ValorCobrado, 0) AS saldo, ISNULL(SUM(dbo.fa_factura_det.vt_total), 0) + ISNULL(SUM(dbo.fa_notaCreDeb_det.sc_total), 0) - ISNULL(cobro.ValorCobrado, 0) 
                  + dbo.cxc_cobro_det.dc_ValorPago AS saldo_sin_cobro, ISNULL(dbo.fa_factura.vt_fecha, dbo.fa_notaCreDeb.no_fecha) AS vt_fecha, ISNULL(dbo.fa_factura.vt_fech_venc, dbo.fa_notaCreDeb.no_fecha_venc) AS vt_fech_venc, 
                  ISNULL(dbo.fa_factura.IdCliente, dbo.fa_notaCreDeb.IdCliente) AS IdCliente, ISNULL(dbo.fa_cliente_contactos.Nombres, dbo.tb_persona.pe_nombreCompleto) AS pe_nombreCompleto, dbo.cxc_cobro_det.IdNotaCredito, 
                  dbo.cxc_cobro_det.dc_ValorProntoPago
FROM     dbo.tb_persona INNER JOIN
                  dbo.fa_notaCreDeb_det INNER JOIN
                  dbo.fa_notaCreDeb ON dbo.fa_notaCreDeb_det.IdEmpresa = dbo.fa_notaCreDeb.IdEmpresa AND dbo.fa_notaCreDeb_det.IdSucursal = dbo.fa_notaCreDeb.IdSucursal AND 
                  dbo.fa_notaCreDeb_det.IdBodega = dbo.fa_notaCreDeb.IdBodega AND dbo.fa_notaCreDeb_det.IdNota = dbo.fa_notaCreDeb.IdNota INNER JOIN
                  dbo.fa_cliente ON dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_notaCreDeb.IdCliente = dbo.fa_cliente.IdCliente ON dbo.tb_persona.IdPersona = dbo.fa_cliente.IdPersona RIGHT OUTER JOIN
                  dbo.cxc_cobro_det ON dbo.fa_notaCreDeb.IdEmpresa = dbo.cxc_cobro_det.IdEmpresa AND dbo.fa_notaCreDeb.IdSucursal = dbo.cxc_cobro_det.IdSucursal AND dbo.fa_notaCreDeb.IdBodega = dbo.cxc_cobro_det.IdBodega_Cbte AND 
                  dbo.fa_notaCreDeb.IdNota = dbo.cxc_cobro_det.IdCbte_vta_nota AND dbo.fa_notaCreDeb.CodDocumentoTipo = dbo.cxc_cobro_det.dc_TipoDocumento LEFT OUTER JOIN
                  dbo.fa_cliente_contactos INNER JOIN
                  dbo.fa_factura INNER JOIN
                  dbo.fa_factura_det ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_det.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_det.IdSucursal AND dbo.fa_factura.IdBodega = dbo.fa_factura_det.IdBodega AND 
                  dbo.fa_factura.IdCbteVta = dbo.fa_factura_det.IdCbteVta ON dbo.fa_cliente_contactos.IdEmpresa = dbo.fa_factura.IdEmpresa AND dbo.fa_cliente_contactos.IdCliente = dbo.fa_factura.IdCliente ON 
                  dbo.cxc_cobro_det.IdEmpresa = dbo.fa_factura.IdEmpresa AND dbo.cxc_cobro_det.IdSucursal = dbo.fa_factura.IdSucursal AND dbo.cxc_cobro_det.IdBodega_Cbte = dbo.fa_factura.IdBodega AND 
                  dbo.cxc_cobro_det.IdCbte_vta_nota = dbo.fa_factura.IdCbteVta AND dbo.cxc_cobro_det.dc_TipoDocumento = dbo.fa_factura.vt_tipoDoc LEFT OUTER JOIN
                      (SELECT IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento, SUM(dc_ValorPago) AS ValorCobrado
                       FROM      dbo.cxc_cobro_det AS det
                       WHERE   (estado = 'A')
                       GROUP BY IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento) AS cobro ON cobro.IdEmpresa = dbo.cxc_cobro_det.IdEmpresa AND cobro.IdSucursal = dbo.cxc_cobro_det.IdSucursal AND 
                  cobro.IdBodega_Cbte = dbo.cxc_cobro_det.IdBodega_Cbte AND cobro.IdCbte_vta_nota = dbo.cxc_cobro_det.IdCbte_vta_nota AND cobro.dc_TipoDocumento = dbo.cxc_cobro_det.dc_TipoDocumento
GROUP BY dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdCobro, dbo.cxc_cobro_det.secuencial, dbo.cxc_cobro_det.dc_TipoDocumento, dbo.cxc_cobro_det.IdBodega_Cbte, 
                  dbo.cxc_cobro_det.IdCbte_vta_nota, dbo.cxc_cobro_det.dc_ValorPago, dbo.cxc_cobro_det.IdCobro_tipo, dbo.fa_factura.vt_NumFactura, dbo.fa_notaCreDeb.NumNota_Impresa, dbo.fa_factura.vt_Observacion, 
                  dbo.fa_notaCreDeb.sc_observacion, dbo.fa_factura.CodCbteVta, dbo.fa_notaCreDeb.CodNota, dbo.fa_factura.vt_fecha, dbo.fa_notaCreDeb.no_fecha, dbo.fa_factura.vt_fech_venc, dbo.fa_notaCreDeb.no_fecha_venc, 
                  dbo.fa_factura.IdCliente, dbo.fa_notaCreDeb.IdCliente, dbo.cxc_cobro_det.dc_ValorPago, cobro.ValorCobrado, dbo.fa_cliente_contactos.Nombres, dbo.tb_persona.pe_nombreCompleto, dbo.cxc_cobro_det.IdNotaCredito, 
                  dbo.cxc_cobro_det.dc_ValorProntoPago
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcxc_cobro_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'           End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_factura_det"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 300
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cobro"
            Begin Extent = 
               Top = 986
               Left = 551
               Bottom = 1356
               Right = 792
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
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcxc_cobro_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[78] 4[3] 2[0] 3) )"
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
         Top = -720
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 338
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_notaCreDeb_det"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 300
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_notaCreDeb"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 294
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_cliente"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 320
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro_det"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 1234
               Right = 294
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_cliente_contactos"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 258
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_factura"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 315
 ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcxc_cobro_det';

