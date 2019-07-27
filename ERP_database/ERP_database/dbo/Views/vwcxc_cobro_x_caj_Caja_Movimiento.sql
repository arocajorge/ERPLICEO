
CREATE VIEW [dbo].[vwcxc_cobro_x_caj_Caja_Movimiento]
AS
SELECT     dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdEmpresa, dbo.tb_empresa.em_nombre, dbo.tb_sucursal.Su_Descripcion, 
                      dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdSucursal, dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdCobro, 
                      dbo.cxc_cobro_x_caj_Caja_Movimiento.mcj_IdEmpresa, dbo.cxc_cobro_x_caj_Caja_Movimiento.mcj_IdCbteCble, 
                      dbo.cxc_cobro_x_caj_Caja_Movimiento.mcj_IdTipocbte, dbo.cxc_cobro_tipo.tc_descripcion, dbo.ct_cbtecble_tipo.tc_TipoCbte
FROM         dbo.tb_sucursal INNER JOIN
                      dbo.tb_empresa ON dbo.tb_sucursal.IdEmpresa = dbo.tb_empresa.IdEmpresa INNER JOIN
                      dbo.cxc_cobro_x_caj_Caja_Movimiento ON dbo.tb_empresa.IdEmpresa = dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdEmpresa AND 
                      dbo.tb_sucursal.IdSucursal = dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdSucursal INNER JOIN
                      dbo.cxc_cobro ON dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdEmpresa = dbo.cxc_cobro.IdEmpresa AND 
                      dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdSucursal = dbo.cxc_cobro.IdSucursal AND 
                      dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdCobro = dbo.cxc_cobro.IdCobro INNER JOIN
                      dbo.cxc_cobro_tipo ON dbo.cxc_cobro.IdCobro_tipo = dbo.cxc_cobro_tipo.IdCobro_tipo INNER JOIN
                      dbo.ct_cbtecble ON dbo.tb_empresa.IdEmpresa = dbo.ct_cbtecble.IdEmpresa AND 
                      dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdEmpresa = dbo.ct_cbtecble.IdEmpresa AND 
                      dbo.cxc_cobro_x_caj_Caja_Movimiento.mcj_IdTipocbte = dbo.ct_cbtecble.IdTipoCbte AND 
                      dbo.cxc_cobro_x_caj_Caja_Movimiento.mcj_IdCbteCble = dbo.ct_cbtecble.IdCbteCble INNER JOIN
                      dbo.ct_cbtecble_tipo ON dbo.ct_cbtecble.IdTipoCbte = dbo.ct_cbtecble_tipo.IdTipoCbte
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[66] 4[2] 2[2] 3) )"
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
         Begin Table = "cxc_cobro_x_caj_Caja_Movimiento"
            Begin Extent = 
               Top = 6
               Left = 6
               Bottom = 172
               Right = 171
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_empresa"
            Begin Extent = 
               Top = 0
               Left = 388
               Bottom = 110
               Right = 592
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 0
               Left = 491
               Bottom = 171
               Right = 705
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro"
            Begin Extent = 
               Top = 27
               Left = 739
               Bottom = 128
               Right = 918
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro_tipo"
            Begin Extent = 
               Top = 150
               Left = 735
               Bottom = 269
               Right = 920
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_cbtecble"
            Begin Extent = 
               Top = 191
               Left = 180
               Bottom = 310
               Right = 353
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_cbtecble_tipo"
            Begin Extent = 
               Top = 204
               Left = 507
               Bottom = 345
               Right = 676
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_cobro_x_caj_Caja_Movimiento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
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
      Begin ColumnWidths = 11
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_cobro_x_caj_Caja_Movimiento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_cobro_x_caj_Caja_Movimiento';

