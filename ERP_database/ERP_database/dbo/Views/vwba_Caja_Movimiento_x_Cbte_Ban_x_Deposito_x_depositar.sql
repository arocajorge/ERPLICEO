CREATE VIEW [dbo].[vwba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_x_depositar]
AS
SELECT dbo.caj_Caja_Movimiento_det.IdEmpresa, dbo.caj_Caja_Movimiento_det.IdTipocbte, dbo.caj_Caja_Movimiento_det.IdCbteCble, dbo.caj_Caja_Movimiento_det.Secuencia, dbo.cxc_cobro_tipo.tc_descripcion, 
                  dbo.caj_Caja_Movimiento_det.cr_Valor, dbo.caj_Caja_Movimiento.cm_fecha, dbo.caj_Caja_Movimiento.cm_observacion, dbo.caj_Caja_Movimiento_Tipo.tm_descripcion, dbo.caj_Caja_Movimiento.Estado, 
                  dbo.tb_persona.pe_nombreCompleto, dbo.cxc_cobro.cr_NumDocumento, dbo.caj_Caja.IdCtaCble, dbo.caj_Caja.IdCaja, dbo.caj_Caja.ca_Descripcion, ISNULL(dbo.cxc_cobro_x_ct_cbtecble.cbr_IdSucursal , dbo.caj_Caja.IdSucursal) AS cbr_IdSucursal
                   
FROM     dbo.caj_Caja_Movimiento_det INNER JOIN
                  dbo.cxc_cobro_tipo ON dbo.caj_Caja_Movimiento_det.IdCobro_tipo = dbo.cxc_cobro_tipo.IdCobro_tipo INNER JOIN
                  dbo.caj_Caja_Movimiento ON dbo.caj_Caja_Movimiento_det.IdEmpresa = dbo.caj_Caja_Movimiento.IdEmpresa AND dbo.caj_Caja_Movimiento_det.IdCbteCble = dbo.caj_Caja_Movimiento.IdCbteCble AND 
                  dbo.caj_Caja_Movimiento_det.IdTipocbte = dbo.caj_Caja_Movimiento.IdTipocbte INNER JOIN
                  dbo.caj_Caja_Movimiento_Tipo ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.caj_Caja_Movimiento_Tipo.IdEmpresa AND dbo.caj_Caja_Movimiento.IdTipoMovi = dbo.caj_Caja_Movimiento_Tipo.IdTipoMovi INNER JOIN
                  dbo.tb_persona ON dbo.caj_Caja_Movimiento.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.caj_Caja ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.caj_Caja.IdEmpresa AND dbo.caj_Caja_Movimiento.IdCaja = dbo.caj_Caja.IdCaja LEFT OUTER JOIN
                  dbo.cxc_cobro_x_ct_cbtecble INNER JOIN
                  dbo.cxc_cobro ON dbo.cxc_cobro_x_ct_cbtecble.cbr_IdEmpresa = dbo.cxc_cobro.IdEmpresa AND dbo.cxc_cobro_x_ct_cbtecble.cbr_IdSucursal = dbo.cxc_cobro.IdSucursal AND 
                  dbo.cxc_cobro_x_ct_cbtecble.cbr_IdCobro = dbo.cxc_cobro.IdCobro ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.cxc_cobro_x_ct_cbtecble.ct_IdEmpresa AND 
                  dbo.caj_Caja_Movimiento.IdTipocbte = dbo.cxc_cobro_x_ct_cbtecble.ct_IdTipoCbte AND dbo.caj_Caja_Movimiento.IdCbteCble = dbo.cxc_cobro_x_ct_cbtecble.ct_IdCbteCble
WHERE  (dbo.caj_Caja_Movimiento_Tipo.SeDeposita = 1) AND (dbo.caj_Caja_Movimiento.Estado = 'A') AND (NOT EXISTS
                      (SELECT mcj_IdEmpresa
                       FROM      dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito AS f
                       WHERE   (dbo.caj_Caja_Movimiento_det.IdEmpresa = mcj_IdEmpresa) AND (dbo.caj_Caja_Movimiento_det.IdTipocbte = mcj_IdTipocbte) AND (dbo.caj_Caja_Movimiento_det.IdCbteCble = mcj_IdCbteCble)))
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_x_depositar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'         Right = 247
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro"
            Begin Extent = 
               Top = 534
               Left = 38
               Bottom = 707
               Right = 232
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
      Begin ColumnWidths = 17
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_x_depositar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[81] 4[5] 2[5] 3) )"
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
         Begin Table = "caj_Caja_Movimiento_det"
            Begin Extent = 
               Top = 176
               Left = 860
               Bottom = 462
               Right = 1170
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro_tipo"
            Begin Extent = 
               Top = 2
               Left = 577
               Bottom = 359
               Right = 787
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_Caja_Movimiento"
            Begin Extent = 
               Top = 251
               Left = 272
               Bottom = 604
               Right = 564
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "caj_Caja_Movimiento_Tipo"
            Begin Extent = 
               Top = 28
               Left = 898
               Bottom = 273
               Right = 1219
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_Caja"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 248
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro_x_ct_cbtecble"
            Begin Extent = 
               Top = 0
               Left = 0
               Bottom = 269
      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_x_depositar';

