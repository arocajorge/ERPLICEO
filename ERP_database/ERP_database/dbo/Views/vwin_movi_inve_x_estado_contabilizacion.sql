CREATE VIEW [dbo].[vwin_movi_inve_x_estado_contabilizacion]
AS
SELECT dbo.in_Ing_Egr_Inven_det.IdEmpresa, dbo.in_Ing_Egr_Inven_det.IdSucursal, dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven_det.IdNumMovi, dbo.tb_sucursal.Su_Descripcion AS nom_sucursal, 
                  dbo.tb_bodega.bo_Descripcion AS nom_bodega, dbo.in_movi_inven_tipo.tm_descripcion AS nom_tipo_movi, dbo.in_movi_inve.cm_fecha, dbo.in_movi_inve.cm_tipo, dbo.in_movi_inve.cm_observacion, 
                  dbo.in_movi_inve_x_ct_cbteCble.IdEmpresa_ct, dbo.in_movi_inve_x_ct_cbteCble.IdTipoCbte, dbo.in_movi_inve_x_ct_cbteCble.IdCbteCble, CASE WHEN in_movi_inve_x_ct_cbteCble.IdEmpresa_ct IS NULL 
                  THEN 'NO CONTABILIZADO' ELSE 'CONTABILIZADO' END AS Estado_contabilizacion, abs(ROUND(SUM(dbo.in_Ing_Egr_Inven_det.dm_cantidad * dbo.in_Ing_Egr_Inven_det.mv_costo),2)) AS TotalModulo,
				  isnull(ct.TotalContabilidad,0) TotalContabilidad, round(abs(ROUND(SUM(dbo.in_Ing_Egr_Inven_det.dm_cantidad * dbo.in_Ing_Egr_Inven_det.mv_costo),2)) - isnull(ct.TotalContabilidad,0),2) as Diferencia
FROM     dbo.in_Ing_Egr_Inven_det INNER JOIN
                  dbo.in_movi_inve INNER JOIN
                  dbo.in_movi_inven_tipo ON dbo.in_movi_inve.IdEmpresa = dbo.in_movi_inven_tipo.IdEmpresa AND dbo.in_movi_inve.IdMovi_inven_tipo = dbo.in_movi_inven_tipo.IdMovi_inven_tipo ON 
                  dbo.in_Ing_Egr_Inven_det.IdEmpresa_inv = dbo.in_movi_inve.IdEmpresa AND dbo.in_Ing_Egr_Inven_det.IdSucursal_inv = dbo.in_movi_inve.IdSucursal AND dbo.in_Ing_Egr_Inven_det.IdBodega_inv = dbo.in_movi_inve.IdBodega AND 
                  dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo_inv = dbo.in_movi_inve.IdMovi_inven_tipo AND dbo.in_Ing_Egr_Inven_det.IdNumMovi_inv = dbo.in_movi_inve.IdNumMovi LEFT OUTER JOIN
                  dbo.in_movi_inve_x_ct_cbteCble ON dbo.in_movi_inve.IdEmpresa = dbo.in_movi_inve_x_ct_cbteCble.IdEmpresa AND dbo.in_movi_inve.IdSucursal = dbo.in_movi_inve_x_ct_cbteCble.IdSucursal AND 
                  dbo.in_movi_inve.IdBodega = dbo.in_movi_inve_x_ct_cbteCble.IdBodega AND dbo.in_movi_inve.IdMovi_inven_tipo = dbo.in_movi_inve_x_ct_cbteCble.IdMovi_inven_tipo AND 
                  dbo.in_movi_inve.IdNumMovi = dbo.in_movi_inve_x_ct_cbteCble.IdNumMovi LEFT OUTER JOIN
                  dbo.tb_sucursal INNER JOIN
                  dbo.tb_bodega ON dbo.tb_sucursal.IdEmpresa = dbo.tb_bodega.IdEmpresa AND dbo.tb_sucursal.IdSucursal = dbo.tb_bodega.IdSucursal ON dbo.in_movi_inve.IdEmpresa = dbo.tb_bodega.IdEmpresa AND 
                  dbo.in_movi_inve.IdSucursal = dbo.tb_bodega.IdSucursal AND dbo.in_movi_inve.IdBodega = dbo.tb_bodega.IdBodega
				  left join (
				  select ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdTipoCbte, ct_cbtecble_det.IdCbteCble, sum(ct_cbtecble_det.dc_Valor) TotalContabilidad
				  from ct_cbtecble_det
				  where ct_cbtecble_det.dc_Valor > 0
				  group by ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdTipoCbte, ct_cbtecble_det.IdCbteCble 
				  ) as ct on in_movi_inve_x_ct_cbteCble.IdEmpresa_ct = ct.IdEmpresa and in_movi_inve_x_ct_cbteCble.IdTipoCbte = ct.IdTipoCbte
				  and in_movi_inve_x_ct_cbteCble.IdCbteCble = ct.IdCbteCble
WHERE  (dbo.in_movi_inve.Estado = 'A') AND (dbo.in_movi_inven_tipo.Genera_Diario_Contable = 1) 
GROUP BY dbo.tb_sucursal.Su_Descripcion, dbo.tb_bodega.bo_Descripcion, dbo.in_movi_inven_tipo.tm_descripcion, dbo.in_movi_inve.cm_fecha, dbo.in_movi_inve.cm_tipo, dbo.in_movi_inve.cm_observacion, 
                  dbo.in_movi_inve_x_ct_cbteCble.IdEmpresa_ct, dbo.in_movi_inve_x_ct_cbteCble.IdTipoCbte, dbo.in_movi_inve_x_ct_cbteCble.IdCbteCble, dbo.in_Ing_Egr_Inven_det.IdEmpresa, dbo.in_Ing_Egr_Inven_det.IdSucursal, 
                  dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven_det.IdNumMovi,ct.TotalContabilidad
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[4] 4[46] 2[3] 3) )"
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
         Begin Table = "in_movi_inve_x_ct_cbteCble"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_movi_inve"
            Begin Extent = 
               Top = 0
               Left = 411
               Bottom = 311
               Right = 674
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_movi_inven_tipo"
            Begin Extent = 
               Top = 8
               Left = 0
               Bottom = 137
               Right = 215
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 189
               Left = 15
               Bottom = 318
               Right = 245
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_bodega"
            Begin Extent = 
               Top = 203
               Left = 0
               Bottom = 332
               Right = 261
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Ing_Egr_Inven_det"
            Begin Extent = 
               Top = 0
               Left = 768
               Bottom = 292
               Right = 1077
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
      Begin ColumnWidths = 18
         Width = 284
     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_movi_inve_x_estado_contabilizacion';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'    Width = 1200
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
         Width = 2100
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_movi_inve_x_estado_contabilizacion';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_movi_inve_x_estado_contabilizacion';

