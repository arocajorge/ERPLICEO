CREATE VIEW web.VWCXP_003
AS
SELECT dbo.ct_cbtecble_det.IdEmpresa, dbo.ct_cbtecble_det.IdTipoCbte, dbo.ct_cbtecble_det.IdCbteCble, dbo.ct_cbtecble_det.secuencia, ncnd.cn_fecha, ncnd.cn_Fecha_vcto, dbo.ct_cbtecble.cb_Observacion, ncnd.Estado, ncnd.cn_subtotal_iva, 
                  ncnd.cn_subtotal_siniva, ncnd.cn_valoriva, ncnd.cn_total, dbo.ct_cbtecble_det.IdCtaCble, dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.dc_Valor, CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ABS(ct_cbtecble_det.dc_Valor) 
                  ELSE 0 END AS dc_Valor_Debe, CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Haber, dbo.ct_cbtecble_tipo.tc_TipoCbte, dbo.ct_cbtecble_det.dc_Observacion, ncnd.IdProveedor, 
                  per.pe_nombreCompleto, ncnd.DebCre, CASE WHEN ncnd.DebCre = 'C' THEN 'Crédito' ELSE 'Débito' END AS Tipo_doc, ncnd.cn_Nota AS num_documento, dbo.tb_sucursal.Su_Descripcion, dbo.cp_catalogo.Nombre AS NomTipoNota
FROM     dbo.ct_cbtecble INNER JOIN
                  dbo.ct_cbtecble_det ON dbo.ct_cbtecble.IdEmpresa = dbo.ct_cbtecble_det.IdEmpresa AND dbo.ct_cbtecble.IdTipoCbte = dbo.ct_cbtecble_det.IdTipoCbte AND dbo.ct_cbtecble.IdCbteCble = dbo.ct_cbtecble_det.IdCbteCble INNER JOIN
                  dbo.ct_plancta ON dbo.ct_cbtecble_det.IdEmpresa = dbo.ct_plancta.IdEmpresa AND dbo.ct_cbtecble_det.IdCtaCble = dbo.ct_plancta.IdCtaCble INNER JOIN
                  dbo.ct_cbtecble_tipo ON dbo.ct_cbtecble.IdEmpresa = dbo.ct_cbtecble_tipo.IdEmpresa AND dbo.ct_cbtecble.IdTipoCbte = dbo.ct_cbtecble_tipo.IdTipoCbte INNER JOIN
                  dbo.cp_nota_DebCre AS ncnd ON dbo.ct_cbtecble.IdEmpresa = ncnd.IdEmpresa AND dbo.ct_cbtecble.IdEmpresa = ncnd.IdEmpresa AND dbo.ct_cbtecble.IdTipoCbte = ncnd.IdTipoCbte_Nota AND 
                  dbo.ct_cbtecble.IdCbteCble = ncnd.IdCbteCble_Nota INNER JOIN
                  dbo.cp_proveedor AS pro ON ncnd.IdEmpresa = pro.IdEmpresa AND pro.IdProveedor = ncnd.IdProveedor INNER JOIN
                  dbo.tb_persona AS per ON pro.IdPersona = per.IdPersona INNER JOIN
                  dbo.tb_sucursal ON ncnd.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND ncnd.IdSucursal = dbo.tb_sucursal.IdSucursal INNER JOIN
                  dbo.cp_catalogo ON ncnd.IdTipoNota = dbo.cp_catalogo.IdCatalogo

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCXP_003';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'isplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 320
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_catalogo"
            Begin Extent = 
               Top = 761
               Left = 1052
               Bottom = 924
               Right = 1260
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCXP_003';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[71] 4[3] 2[3] 3) )"
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
         Top = -567
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ct_cbtecble"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 260
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_cbtecble_det"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_plancta"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 259
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_cbtecble_tipo"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 256
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ncnd"
            Begin Extent = 
               Top = 579
               Left = 572
               Bottom = 1134
               Right = 838
            End
            DisplayFlags = 280
            TopColumn = 35
         End
         Begin Table = "pro"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "per"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 322
            End
            D', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCXP_003';

