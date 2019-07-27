CREATE VIEW dbo.vwfa_CambioProductoDet_FacturasConMovimiento
AS
SELECT dbo.fa_CambioProductoDet.IdEmpresa, dbo.fa_CambioProductoDet.IdSucursal, dbo.fa_CambioProductoDet.IdBodega, dbo.fa_CambioProductoDet.IdCambio, dbo.fa_CambioProductoDet.Secuencia AS Secuencia_ca, 
                  dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdEmpresa_eg, dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdSucursal_eg, dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdMovi_inven_tipo_eg, 
                  dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdNumMovi_eg, dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.Secuencia_eg, dbo.fa_CambioProductoDet.CantidadCambio, ISNULL(dbo.in_Ing_Egr_Inven_det.mv_costo_sinConversion, 0) 
                  AS Costo, dbo.in_Ing_Egr_Inven_det.IdUnidadMedida_sinConversion, dbo.in_Ing_Egr_Inven_det.IdProducto, dbo.in_Ing_Egr_Inven_det.IdBodega AS IdBodega_eg
FROM     dbo.in_Ing_Egr_Inven_det INNER JOIN
                  dbo.fa_factura_det_x_in_Ing_Egr_Inven_det ON dbo.in_Ing_Egr_Inven_det.IdEmpresa = dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdEmpresa_eg AND 
                  dbo.in_Ing_Egr_Inven_det.IdSucursal = dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdSucursal_eg AND dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo = dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdMovi_inven_tipo_eg AND 
                  dbo.in_Ing_Egr_Inven_det.IdNumMovi = dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdNumMovi_eg AND dbo.in_Ing_Egr_Inven_det.Secuencia = dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.Secuencia_eg RIGHT OUTER JOIN
                  dbo.fa_CambioProductoDet ON dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdEmpresa_fa = dbo.fa_CambioProductoDet.IdEmpresa AND 
                  dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdSucursal_fa = dbo.fa_CambioProductoDet.IdSucursal AND dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdBodega_fa = dbo.fa_CambioProductoDet.IdBodega AND 
                  dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.IdCbteVta_fa = dbo.fa_CambioProductoDet.IdCbteVta AND dbo.fa_factura_det_x_in_Ing_Egr_Inven_det.Secuencia_fa = dbo.fa_CambioProductoDet.SecuenciaFact
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_CambioProductoDet_FacturasConMovimiento';


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
         Begin Table = "in_Ing_Egr_Inven_det"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 357
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "fa_factura_det_x_in_Ing_Egr_Inven_det"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 286
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_CambioProductoDet"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 268
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
      Begin ColumnWidths = 11
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_CambioProductoDet_FacturasConMovimiento';

