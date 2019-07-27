CREATE VIEW dbo.vwcp_Conciliacion_Caja
AS
SELECT        dbo.cp_conciliacion_Caja.IdEmpresa, dbo.cp_conciliacion_Caja.IdConciliacion_Caja, dbo.cp_conciliacion_Caja.Fecha, dbo.cp_conciliacion_Caja.IdCaja, dbo.cp_conciliacion_Caja.IdEstadoCierre, 
                         dbo.cp_conciliacion_Caja.Observacion, dbo.cp_conciliacion_Caja.IdEmpresa_op, dbo.cp_conciliacion_Caja.IdOrdenPago_op, dbo.cp_orden_pago_det.Valor_a_pagar AS Valor_pagado, 
                         dbo.caj_Caja.ca_Descripcion AS nom_Caja, dbo.cp_catalogo.Nombre AS nom_Estado, dbo.cp_conciliacion_Caja.IdCtaCble, dbo.cp_conciliacion_Caja.Saldo_cont_al_periodo, dbo.cp_conciliacion_Caja.Ingresos, 
                         dbo.cp_conciliacion_Caja.Total_Ing, dbo.cp_conciliacion_Caja.Total_fact_vale, dbo.cp_conciliacion_Caja.Total_fondo, dbo.cp_conciliacion_Caja.Dif_x_pagar_o_cobrar, dbo.cp_conciliacion_Caja.IdPeriodo, 
                         dbo.cp_conciliacion_Caja.Fecha_ini, dbo.cp_conciliacion_Caja.Fecha_fin, dbo.cp_conciliacion_Caja.IdTipoFlujo, dbo.cp_conciliacion_Caja.IdEmpresa_mov_caj, dbo.cp_conciliacion_Caja.IdTipoCbte_mov_caj, 
                         dbo.cp_conciliacion_Caja.IdCbteCble_mov_caj
FROM            dbo.cp_catalogo_tipo INNER JOIN
                         dbo.cp_catalogo ON dbo.cp_catalogo_tipo.IdCatalogo_tipo = dbo.cp_catalogo.IdCatalogo_tipo RIGHT OUTER JOIN
                         dbo.cp_orden_pago_det INNER JOIN
                         dbo.cp_orden_pago ON dbo.cp_orden_pago_det.IdEmpresa = dbo.cp_orden_pago.IdEmpresa AND dbo.cp_orden_pago_det.IdOrdenPago = dbo.cp_orden_pago.IdOrdenPago RIGHT OUTER JOIN
                         dbo.cp_conciliacion_Caja INNER JOIN
                         dbo.caj_Caja ON dbo.cp_conciliacion_Caja.IdEmpresa = dbo.caj_Caja.IdEmpresa AND dbo.cp_conciliacion_Caja.IdCaja = dbo.caj_Caja.IdCaja ON 
                         dbo.cp_orden_pago.IdEmpresa = dbo.cp_conciliacion_Caja.IdEmpresa_op AND dbo.cp_orden_pago.IdOrdenPago = dbo.cp_conciliacion_Caja.IdOrdenPago_op ON 
                         dbo.cp_catalogo.IdCatalogo = dbo.cp_conciliacion_Caja.IdEstadoCierre
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[85] 4[4] 2[4] 3) )"
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
         Begin Table = "cp_catalogo_tipo"
            Begin Extent = 
               Top = 195
               Left = 335
               Bottom = 307
               Right = 544
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_catalogo"
            Begin Extent = 
               Top = 283
               Left = 506
               Bottom = 470
               Right = 715
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_pago_det"
            Begin Extent = 
               Top = 77
               Left = 747
               Bottom = 295
               Right = 956
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_pago"
            Begin Extent = 
               Top = 0
               Left = 498
               Bottom = 129
               Right = 707
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_conciliacion_Caja"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 394
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "caj_Caja"
            Begin Extent = 
               Top = 1
               Left = 535
               Bottom = 190
               Right = 745
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
      Begin ColumnWidths = 14
         Width = 284
         Wi', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_Conciliacion_Caja';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'dth = 1500
         Width = 2220
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
         Column = 2400
         Alias = 2250
         Table = 1530
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_Conciliacion_Caja';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_Conciliacion_Caja';

