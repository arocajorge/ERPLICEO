CREATE VIEW dbo.vwcaj_Caja_Movimiento_x_Conciliar
AS
SELECT        mov_caj.IdEmpresa, mov_caj.IdCbteCble, mov_caj.IdTipocbte, mov_caj.cm_Signo, mov_caj.IdCaja, mov_caj.cm_observacion, mov_caj.cm_fecha, 
                         mov_caj.IdPeriodo, ISNULL(SUM(mov_caj_det.cr_Valor), 0) AS Total_movi, ISNULL(dbo.vwcp_conciliacion_Caja_det_Ing_Caja_total_aplicado.Total_aplicado, 0) 
                         AS Total_aplicado, ROUND(ISNULL(ISNULL(SUM(mov_caj_det.cr_Valor), 0) - ISNULL(dbo.vwcp_conciliacion_Caja_det_Ing_Caja_total_aplicado.Total_aplicado, 0), 0), 
                         2) AS Saldo
FROM            dbo.caj_Caja_Movimiento AS mov_caj INNER JOIN
                         dbo.caj_Caja_Movimiento_det AS mov_caj_det ON mov_caj.IdEmpresa = mov_caj_det.IdEmpresa AND mov_caj.IdCbteCble = mov_caj_det.IdCbteCble AND 
                         mov_caj.IdTipocbte = mov_caj_det.IdTipocbte LEFT OUTER JOIN
                         dbo.vwcp_conciliacion_Caja_det_Ing_Caja_total_aplicado ON 
                         mov_caj.IdEmpresa = dbo.vwcp_conciliacion_Caja_det_Ing_Caja_total_aplicado.IdEmpresa_movcaj AND 
                         mov_caj.IdCbteCble = dbo.vwcp_conciliacion_Caja_det_Ing_Caja_total_aplicado.IdCbteCble_movcaj AND 
                         mov_caj.IdTipocbte = dbo.vwcp_conciliacion_Caja_det_Ing_Caja_total_aplicado.IdTipocbte_movcaj
GROUP BY mov_caj.IdEmpresa, mov_caj.IdCbteCble, mov_caj.IdTipocbte, mov_caj.cm_Signo, mov_caj.cm_observacion, mov_caj.cm_fecha, mov_caj.IdPeriodo, 
                         dbo.vwcp_conciliacion_Caja_det_Ing_Caja_total_aplicado.Total_aplicado, mov_caj.IdCaja, mov_caj.Estado
HAVING        (mov_caj.cm_Signo = '+') AND (mov_caj.Estado = 'A')
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[6] 4[19] 2[5] 3) )"
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
         Begin Table = "mov_caj"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 191
               Right = 263
            End
            DisplayFlags = 280
            TopColumn = 18
         End
         Begin Table = "mov_caj_det"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 267
               Right = 317
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "vwcp_conciliacion_Caja_det_Ing_Caja_total_aplicado"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 399
               Right = 263
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
      Begin ColumnWidths = 13
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
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcaj_Caja_Movimiento_x_Conciliar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcaj_Caja_Movimiento_x_Conciliar';

