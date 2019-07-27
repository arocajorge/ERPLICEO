CREATE VIEW web.vwimp_gastos_no_asignados
AS
SELECT        comp_det.IdEmpresa, comp_det.IdTipoCbte, comp_det.IdCbteCble, comp_det.secuencia, comp_det.IdCtaCble, CASE WHEN comp_det.dc_Valor < 0 THEN comp_det.dc_Valor * - 1 ELSE comp_det.dc_Valor END AS dc_Valor, 
                         comp_det.dc_Observacion, ct_cuentas.pc_Cuenta
FROM            dbo.ct_cbtecble_det AS comp_det INNER JOIN
                         dbo.ct_plancta AS ct_cuentas ON comp_det.IdEmpresa = ct_cuentas.IdEmpresa AND comp_det.IdCtaCble = ct_cuentas.IdCtaCble INNER JOIN
                         dbo.ct_cbtecble AS comp ON comp_det.IdEmpresa = comp.IdEmpresa AND comp_det.IdTipoCbte = comp.IdTipoCbte AND comp_det.IdCbteCble = comp.IdCbteCble
WHERE        (NOT EXISTS
                             (SELECT        IdEmpresa, IdOrdenCompra_ext, IdEmpresa_ct, IdTipoCbte, IdCbteCble, secuencia_ct, IdGasto_tipo
                               FROM            dbo.imp_orden_compra_ext_ct_cbteble_det_gastos AS imp
                               WHERE        (comp_det.IdEmpresa = IdEmpresa) AND (comp_det.IdTipoCbte = IdTipoCbte) AND (comp_det.IdCbteCble = IdCbteCble) AND (comp.cb_Estado = 'A') AND (comp.cb_Observacion NOT LIKE '%REVERSO%') AND 
                                                         (comp_det.dc_Observacion NOT LIKE '%REVERSO%'))) AND (NOT EXISTS
                             (SELECT        IdEmpresa, IdTipoCbte, IdCbteCble, IdEmpresa_Anu, IdTipoCbte_Anu, IdCbteCble_Anu, ip
                               FROM            dbo.ct_cbtecble_Reversado AS rev
                               WHERE        (comp_det.IdEmpresa = IdEmpresa) AND (comp_det.IdTipoCbte = IdTipoCbte) AND (comp_det.IdCbteCble = IdCbteCble) AND (comp.cb_Observacion NOT LIKE '%REVERSO%') AND 
                                                         (comp_det.dc_Observacion NOT LIKE '%REVERSO%'))) AND (NOT EXISTS
                             (SELECT        IdEmpresa
                               FROM            dbo.imp_liquidacion AS rev
                               WHERE        (comp_det.IdEmpresa = IdEmpresa) AND (comp_det.IdTipoCbte = IdTipoCbte_ct) AND (comp_det.IdCbteCble = IdCbteCble_ct) AND (comp.cb_Observacion NOT LIKE '%REVERSO%') AND 
                                                         (comp_det.dc_Observacion NOT LIKE '%REVERSO%')))
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwimp_gastos_no_asignados';


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
         Begin Table = "comp_det"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 301
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_cuentas"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 221
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "comp"
            Begin Extent = 
               Top = 138
               Left = 259
               Bottom = 268
               Right = 441
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwimp_gastos_no_asignados';

