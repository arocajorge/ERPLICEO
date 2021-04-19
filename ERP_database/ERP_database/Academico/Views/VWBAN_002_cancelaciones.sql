CREATE VIEW Academico.VWBAN_002_cancelaciones
AS
SELECT dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdEmpresa, dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdCbteCble, dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdTipocbte, dbo.cxc_cobro_det.IdEmpresa, 
                  dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdBodega_Cbte, ISNULL(dbo.cxc_cobro_det.IdCbte_vta_nota, dbo.caj_Caja_Movimiento.IdCbteCble) AS IdCbte_vta_nota, dbo.cxc_cobro_det.dc_TipoDocumento, 
                  CASE WHEN fa_factura.IdCbteVta IS NULL AND fa_notaCreDeb.IdNota IS NULL THEN 'ING ' + CAST(caj_Caja_Movimiento.IdCbteCble AS VARCHAR(20)) WHEN fa_factura.IdCbteVta IS NULL AND fa_notaCreDeb.IdNota IS NOT NULL 
                  THEN 'NTDB ' + fa_notaCreDeb.CodNota ELSE 'FACT ' + fa_factura.vt_NumFactura END AS Referencia, CASE WHEN fa_factura.IdCbteVta IS NULL AND fa_notaCreDeb.IdNota IS NULL 
                  THEN caj_Caja_Movimiento.cm_observacion WHEN fa_factura.IdCbteVta IS NULL AND fa_notaCreDeb.IdNota IS NOT NULL THEN fa_notaCreDeb.sc_observacion ELSE fa_factura.vt_Observacion END AS Observacion, 
                  ISNULL(dbo.caj_Caja_Movimiento_det.cr_Valor, dbo.cxc_cobro_det.dc_ValorPago) AS monto
FROM     dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito WITH (nolock) LEFT OUTER JOIN
                  dbo.cxc_cobro_x_caj_Caja_Movimiento WITH (nolock) INNER JOIN
                  dbo.cxc_cobro_det WITH (nolock) ON dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdEmpresa = dbo.cxc_cobro_det.IdEmpresa AND dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdSucursal = dbo.cxc_cobro_det.IdSucursal AND 
                  dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdCobro = dbo.cxc_cobro_det.IdCobro RIGHT OUTER JOIN
                  dbo.caj_Caja_Movimiento_det WITH (nolock) INNER JOIN
                  dbo.caj_Caja_Movimiento WITH (nolock) ON dbo.caj_Caja_Movimiento_det.IdEmpresa = dbo.caj_Caja_Movimiento.IdEmpresa AND dbo.caj_Caja_Movimiento_det.IdCbteCble = dbo.caj_Caja_Movimiento.IdCbteCble AND 
                  dbo.caj_Caja_Movimiento_det.IdTipocbte = dbo.caj_Caja_Movimiento.IdTipocbte LEFT OUTER JOIN
                  dbo.cxc_cobro_det_x_ct_cbtecble_det WITH (nolock) ON dbo.caj_Caja_Movimiento_det.IdEmpresa = dbo.cxc_cobro_det_x_ct_cbtecble_det.IdEmpresa_ct AND 
                  dbo.caj_Caja_Movimiento_det.IdCbteCble = dbo.cxc_cobro_det_x_ct_cbtecble_det.IdCbteCble_ct AND dbo.caj_Caja_Movimiento_det.IdTipocbte = dbo.cxc_cobro_det_x_ct_cbtecble_det.IdTipoCbte_ct AND 
                  dbo.caj_Caja_Movimiento_det.Secuencia = dbo.cxc_cobro_det_x_ct_cbtecble_det.secuencia_ct ON dbo.cxc_cobro_det.secuencial = dbo.cxc_cobro_det_x_ct_cbtecble_det.secuencial_cb AND 
                  dbo.cxc_cobro_det.IdCobro = dbo.cxc_cobro_det_x_ct_cbtecble_det.IdCobro_cb AND dbo.cxc_cobro_det.IdSucursal = dbo.cxc_cobro_det_x_ct_cbtecble_det.IdSucursal_cb AND 
                  dbo.cxc_cobro_det.IdEmpresa = dbo.cxc_cobro_det_x_ct_cbtecble_det.IdEmpresa_cb AND dbo.cxc_cobro_x_caj_Caja_Movimiento.mcj_IdEmpresa = dbo.caj_Caja_Movimiento.IdEmpresa AND 
                  dbo.cxc_cobro_x_caj_Caja_Movimiento.mcj_IdCbteCble = dbo.caj_Caja_Movimiento.IdCbteCble AND dbo.cxc_cobro_x_caj_Caja_Movimiento.mcj_IdTipocbte = dbo.caj_Caja_Movimiento.IdTipocbte ON 
                  dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdEmpresa = dbo.caj_Caja_Movimiento.IdEmpresa AND dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdCbteCble = dbo.caj_Caja_Movimiento.IdCbteCble AND 
                  dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdTipocbte = dbo.caj_Caja_Movimiento.IdTipocbte LEFT OUTER JOIN
                  dbo.fa_factura WITH (nolock) ON dbo.cxc_cobro_det.IdEmpresa = dbo.fa_factura.IdEmpresa AND dbo.cxc_cobro_det.IdSucursal = dbo.fa_factura.IdSucursal AND dbo.cxc_cobro_det.IdBodega_Cbte = dbo.fa_factura.IdBodega AND 
                  dbo.cxc_cobro_det.IdCbte_vta_nota = dbo.fa_factura.IdCbteVta AND dbo.cxc_cobro_det.dc_TipoDocumento = dbo.fa_factura.vt_tipoDoc LEFT OUTER JOIN
                  dbo.fa_notaCreDeb WITH (nolock) ON dbo.cxc_cobro_det.IdEmpresa = dbo.fa_notaCreDeb.IdEmpresa AND dbo.cxc_cobro_det.IdSucursal = dbo.fa_notaCreDeb.IdSucursal AND 
                  dbo.cxc_cobro_det.IdBodega_Cbte = dbo.fa_notaCreDeb.IdBodega AND dbo.cxc_cobro_det.IdCbte_vta_nota = dbo.fa_notaCreDeb.IdNota AND dbo.cxc_cobro_det.dc_TipoDocumento = dbo.fa_notaCreDeb.CodDocumentoTipo
GROUP BY dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdEmpresa, dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdCbteCble, dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdTipocbte, dbo.fa_factura.IdCbteVta, 
                  dbo.fa_notaCreDeb.IdNota, dbo.caj_Caja_Movimiento.IdCbteCble, dbo.fa_factura.IdCbteVta, dbo.fa_notaCreDeb.CodNota, dbo.fa_factura.vt_NumFactura, dbo.caj_Caja_Movimiento.cm_observacion, dbo.fa_notaCreDeb.sc_observacion, 
                  dbo.fa_factura.vt_Observacion, dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdBodega_Cbte, ISNULL(dbo.cxc_cobro_det.IdCbte_vta_nota, dbo.caj_Caja_Movimiento.IdCbteCble), 
                  dbo.cxc_cobro_det.dc_TipoDocumento, ISNULL(dbo.caj_Caja_Movimiento_det.cr_Valor, dbo.cxc_cobro_det.dc_ValorPago)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWBAN_002_cancelaciones';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'  Left = 48
               Bottom = 1178
               Right = 315
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_notaCreDeb"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 308
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
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
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
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWBAN_002_cancelaciones';


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
         Begin Table = "ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro_x_caj_Caja_Movimiento"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro_det"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_Caja_Movimiento_det"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_Caja_Movimiento"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro_det_x_ct_cbtecble_det"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_factura"
            Begin Extent = 
               Top = 1015
             ', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWBAN_002_cancelaciones';

