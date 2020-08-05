CREATE VIEW dbo.vwcxc_LiquidacionTarjeta_x_cxc_cobro
AS
SELECT dbo.cxc_cobro.IdEmpresa, dbo.cxc_cobro.IdSucursal, dbo.cxc_cobro.IdCobro, dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.IdLiquidacion, dbo.cxc_cobro.cr_TotalCobro, dbo.cxc_cobro.cr_fecha, dbo.cxc_cobro.cr_observacion, 
                  dbo.tb_persona.pe_nombreCompleto, dbo.cxc_cobro.IdUsuario
FROM     dbo.fa_cliente INNER JOIN
                  dbo.cxc_cobro ON dbo.fa_cliente.IdEmpresa = dbo.cxc_cobro.IdEmpresa AND dbo.fa_cliente.IdCliente = dbo.cxc_cobro.IdCliente INNER JOIN
                  dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.cxc_cobro_tipo ON dbo.cxc_cobro.IdCobro_tipo = dbo.cxc_cobro_tipo.IdCobro_tipo LEFT OUTER JOIN
                  dbo.cxc_LiquidacionTarjeta_x_cxc_cobro ON dbo.cxc_cobro.IdEmpresa = dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.IdEmpresa AND dbo.cxc_cobro.IdSucursal = dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.IdSucursal AND 
                  dbo.cxc_cobro.IdCobro = dbo.cxc_LiquidacionTarjeta_x_cxc_cobro.IdCobro
WHERE  (dbo.cxc_cobro_tipo.EsTarjetaCredito = 1) AND (dbo.cxc_cobro.cr_estado = 'A')
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_LiquidacionTarjeta_x_cxc_cobro';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' Column = 1440
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_LiquidacionTarjeta_x_cxc_cobro';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[49] 4[12] 2[20] 3) )"
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
         Top = -360
         Left = 0
      End
      Begin Tables = 
         Begin Table = "fa_cliente"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 304
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro"
            Begin Extent = 
               Top = 29
               Left = 453
               Bottom = 330
               Right = 701
            End
            DisplayFlags = 280
            TopColumn = 19
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 184
               Left = 34
               Bottom = 347
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro_tipo"
            Begin Extent = 
               Top = 387
               Left = 29
               Bottom = 550
               Right = 274
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_LiquidacionTarjeta_x_cxc_cobro"
            Begin Extent = 
               Top = 394
               Left = 329
               Bottom = 557
               Right = 684
            End
            DisplayFlags = 280
            TopColumn = 2
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
      Begin ColumnWidths = 11
        ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_LiquidacionTarjeta_x_cxc_cobro';

