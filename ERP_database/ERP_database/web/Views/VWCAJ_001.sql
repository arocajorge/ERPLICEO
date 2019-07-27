CREATE VIEW web.VWCAJ_001
AS
SELECT        dbo.ct_cbtecble_det.IdEmpresa, dbo.ct_cbtecble_det.IdTipoCbte, dbo.ct_cbtecble_det.IdCbteCble, dbo.ct_cbtecble_det.secuencia, dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.dc_Valor, 
                         CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ct_cbtecble_det.dc_Valor ELSE 0 END AS dc_Valor_Debe, CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Haber, 
                         dbo.cxc_cobro_tipo.tc_descripcion, dbo.caj_Caja_Movimiento_det.cr_Valor, dbo.caj_Caja_Movimiento.cm_Signo, dbo.caj_Caja_Movimiento.IdTipoMovi, dbo.caj_Caja_Movimiento_Tipo.tm_descripcion, 
                         dbo.caj_Caja_Movimiento.cm_observacion, dbo.caj_Caja_Movimiento.IdCaja, dbo.caj_Caja.ca_Descripcion, dbo.caj_Caja_Movimiento.cm_fecha, dbo.caj_Caja_Movimiento.Estado, dbo.ct_cbtecble_tipo.tc_TipoCbte, 
                         dbo.ct_cbtecble_det.IdCtaCble, dbo.tb_persona.pe_nombreCompleto, dbo.seg_usuario.Nombre, dbo.tb_sucursal.Su_Descripcion, ISNULL(dbo.caj_Caja_Movimiento.SecuenciaCaja, 0) AS SecuenciaCaja
FROM            dbo.tb_sucursal INNER JOIN
                         dbo.ct_cbtecble ON dbo.tb_sucursal.IdEmpresa = dbo.ct_cbtecble.IdEmpresa AND dbo.tb_sucursal.IdSucursal = dbo.ct_cbtecble.IdSucursal RIGHT OUTER JOIN
                         dbo.caj_Caja_Movimiento INNER JOIN
                         dbo.caj_Caja_Movimiento_det ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.caj_Caja_Movimiento_det.IdEmpresa AND dbo.caj_Caja_Movimiento.IdCbteCble = dbo.caj_Caja_Movimiento_det.IdCbteCble AND 
                         dbo.caj_Caja_Movimiento.IdTipocbte = dbo.caj_Caja_Movimiento_det.IdTipocbte INNER JOIN
                         dbo.caj_Caja ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.caj_Caja.IdEmpresa AND dbo.caj_Caja_Movimiento.IdCaja = dbo.caj_Caja.IdCaja INNER JOIN
                         dbo.ct_cbtecble_det ON dbo.caj_Caja_Movimiento_det.IdEmpresa = dbo.ct_cbtecble_det.IdEmpresa AND dbo.caj_Caja_Movimiento_det.IdTipocbte = dbo.ct_cbtecble_det.IdTipoCbte AND 
                         dbo.caj_Caja_Movimiento_det.IdCbteCble = dbo.ct_cbtecble_det.IdCbteCble INNER JOIN
                         dbo.ct_plancta ON dbo.ct_cbtecble_det.IdEmpresa = dbo.ct_plancta.IdEmpresa AND dbo.ct_cbtecble_det.IdCtaCble = dbo.ct_plancta.IdCtaCble INNER JOIN
                         dbo.cxc_cobro_tipo ON dbo.caj_Caja_Movimiento_det.IdCobro_tipo = dbo.cxc_cobro_tipo.IdCobro_tipo INNER JOIN
                         dbo.caj_Caja_Movimiento_Tipo ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.caj_Caja_Movimiento_Tipo.IdEmpresa AND dbo.caj_Caja_Movimiento.IdTipoMovi = dbo.caj_Caja_Movimiento_Tipo.IdTipoMovi INNER JOIN
                         dbo.ct_cbtecble_tipo ON dbo.ct_cbtecble_det.IdEmpresa = dbo.ct_cbtecble_tipo.IdEmpresa AND dbo.ct_cbtecble_det.IdTipoCbte = dbo.ct_cbtecble_tipo.IdTipoCbte INNER JOIN
                         dbo.tb_persona ON dbo.caj_Caja_Movimiento.IdPersona = dbo.tb_persona.IdPersona ON dbo.ct_cbtecble.IdEmpresa = dbo.caj_Caja_Movimiento.IdEmpresa AND 
                         dbo.ct_cbtecble.IdTipoCbte = dbo.caj_Caja_Movimiento.IdTipocbte AND dbo.ct_cbtecble.IdCbteCble = dbo.caj_Caja_Movimiento.IdCbteCble LEFT OUTER JOIN
                         dbo.seg_usuario ON dbo.caj_Caja_Movimiento.IdUsuario = dbo.seg_usuario.IdUsuario
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[37] 4[5] 2[40] 3) )"
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
         Top = -288
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 54
               Left = 1548
               Bottom = 217
               Right = 1820
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_cbtecble"
            Begin Extent = 
               Top = 43
               Left = 1282
               Bottom = 206
               Right = 1494
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_Caja_Movimiento"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 582
               Right = 256
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_Caja_Movimiento_det"
            Begin Extent = 
               Top = 34
               Left = 447
               Bottom = 197
               Right = 641
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_Caja"
            Begin Extent = 
               Top = 41
               Left = 935
               Bottom = 311
               Right = 1183
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_cbtecble_det"
            Begin Extent = 
               Top = 732
               Left = 365
               Bottom = 941
               Right = 674
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_plancta"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right =', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCAJ_001';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' 259
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "cxc_cobro_tipo"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 338
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_Caja_Movimiento_Tipo"
            Begin Extent = 
               Top = 661
               Left = 653
               Bottom = 824
               Right = 870
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_cbtecble_tipo"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 256
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 1350
               Left = 38
               Bottom = 1480
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "seg_usuario"
            Begin Extent = 
               Top = 276
               Left = 642
               Bottom = 538
               Right = 890
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
      Begin ColumnWidths = 25
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCAJ_001';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCAJ_001';

