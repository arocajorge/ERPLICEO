/*WHERE     (caj_Caja_Movimiento.IdEmpresa = 1) AND (caj_Caja_Movimiento.IdTipocbte = 15) AND (caj_Caja_Movimiento.IdCbteCble = 21)*/
CREATE VIEW [dbo].[vwcaj_MovCaja_x_Cobro]
AS
SELECT        dbo.caj_Caja_Movimiento.IdEmpresa, dbo.caj_Caja_Movimiento.IdCbteCble, dbo.caj_Caja_Movimiento.IdTipocbte, dbo.cxc_cobro.IdBanco, dbo.cxc_cobro.IdCaja, 
                         dbo.cxc_cobro_tipo_Param_conta_x_sucursal.IdCtaCble AS IdCtaCble_TipoCobro, dbo.ba_Banco_Cuenta.IdCtaCble AS IdCtaCble_ban, 
                         dbo.caj_Caja_Movimiento.IdPeriodo AS IdPeriodo_Caja, dbo.cxc_cobro.cr_fecha, dbo.cxc_cobro.cr_fechaDocu, dbo.cxc_cobro.cr_NumDocumento, 
                         dbo.cxc_cobro.cr_TotalCobro, dbo.caj_Caja_Movimiento.cm_valor, dbo.cxc_cobro_det.IdCobro, dbo.cxc_cobro_det.dc_ValorPago, dbo.cxc_cobro.IdCobro_tipo, 
                          dbo.tb_persona.pe_nombreCompleto AS nCliente, '' AS Documento_Cobrado
FROM            dbo.caj_Caja_Movimiento INNER JOIN
                         dbo.cxc_cobro_x_caj_Caja_Movimiento ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.cxc_cobro_x_caj_Caja_Movimiento.mcj_IdEmpresa AND 
                         dbo.caj_Caja_Movimiento.IdCbteCble = dbo.cxc_cobro_x_caj_Caja_Movimiento.mcj_IdCbteCble AND 
                         dbo.caj_Caja_Movimiento.IdTipocbte = dbo.cxc_cobro_x_caj_Caja_Movimiento.mcj_IdTipocbte INNER JOIN
                         dbo.cxc_cobro ON dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdEmpresa = dbo.cxc_cobro.IdEmpresa AND 
                         dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdSucursal = dbo.cxc_cobro.IdSucursal AND 
                         dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdCobro = dbo.cxc_cobro.IdCobro INNER JOIN
                         dbo.caj_Caja ON dbo.cxc_cobro.IdCaja = dbo.caj_Caja.IdCaja AND dbo.cxc_cobro.IdEmpresa = dbo.caj_Caja.IdEmpresa INNER JOIN
                         dbo.ba_Banco_Cuenta ON dbo.cxc_cobro.IdEmpresa = dbo.ba_Banco_Cuenta.IdEmpresa AND dbo.cxc_cobro.IdBanco = dbo.ba_Banco_Cuenta.IdBanco INNER JOIN
                         dbo.cxc_cobro_tipo_Param_conta_x_sucursal ON dbo.cxc_cobro.IdEmpresa = dbo.cxc_cobro_tipo_Param_conta_x_sucursal.IdEmpresa AND 
                         dbo.cxc_cobro.IdSucursal = dbo.cxc_cobro_tipo_Param_conta_x_sucursal.IdSucursal AND 
                         dbo.cxc_cobro.IdCobro_tipo = dbo.cxc_cobro_tipo_Param_conta_x_sucursal.IdCobro_tipo INNER JOIN
                         dbo.cxc_cobro_det ON dbo.cxc_cobro.IdEmpresa = dbo.cxc_cobro_det.IdEmpresa AND dbo.cxc_cobro.IdSucursal = dbo.cxc_cobro_det.IdSucursal AND 
                         dbo.cxc_cobro.IdCobro = dbo.cxc_cobro_det.IdCobro INNER JOIN
                         dbo.fa_cliente ON dbo.cxc_cobro.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.cxc_cobro.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                         dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[13] 4[21] 2[61] 3) )"
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
         Begin Table = "caj_Caja_Movimiento"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 220
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "cxc_cobro_x_caj_Caja_Movimiento"
            Begin Extent = 
               Top = 6
               Left = 258
               Bottom = 125
               Right = 423
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "cxc_cobro"
            Begin Extent = 
               Top = 60
               Left = 693
               Bottom = 319
               Right = 872
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_Caja"
            Begin Extent = 
               Top = 364
               Left = 975
               Bottom = 483
               Right = 1177
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_Banco_Cuenta"
            Begin Extent = 
               Top = 434
               Left = 745
               Bottom = 553
               Right = 933
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro_tipo_Param_conta_x_sucursal"
            Begin Extent = 
               Top = 59
               Left = 1067
               Bottom = 312
               Right = 1251
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro_det"
            Begin Extent = 
               Top = 43
               Left = 34
               Bottom', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcaj_MovCaja_x_Cobro';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' = 269
               Right = 243
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 109
               Left = 216
               Bottom = 238
               Right = 446
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_cliente"
            Begin Extent = 
               Top = 309
               Left = 171
               Bottom = 490
               Right = 409
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 323
               Left = 37
               Bottom = 506
               Right = 247
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
      Begin ColumnWidths = 20
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
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2910
         Alias = 2340
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcaj_MovCaja_x_Cobro';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcaj_MovCaja_x_Cobro';

