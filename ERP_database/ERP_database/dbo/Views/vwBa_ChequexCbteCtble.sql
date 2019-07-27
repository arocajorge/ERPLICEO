CREATE VIEW dbo.vwBa_ChequexCbteCtble
AS
SELECT        dbo.ba_Cbte_Ban.IdEmpresa, dbo.ba_Cbte_Ban.IdCbteCble, dbo.ba_Cbte_Ban.IdTipocbte, dbo.ba_Cbte_Ban.Cod_Cbtecble, dbo.ba_Cbte_Ban.cb_Observacion, 
                         0 cb_secuencia, dbo.ba_Cbte_Ban.cb_Valor, dbo.ba_Cbte_Ban.cb_Cheque, 'N' cb_ChequeImpreso, 
                         GETDATE() cb_FechaCheque, dbo.ba_Cbte_Ban.Fecha_Transac, dbo.ba_Cbte_Ban.Estado, dbo.ba_Cbte_Ban.cb_giradoA, dbo.ba_Cbte_Ban.cb_ciudadChq, 
                         dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan, dbo.ba_Cbte_Ban.cb_Fecha, dbo.ct_cbtecble.cb_Fecha AS con_Fecha, dbo.ct_cbtecble.cb_Valor AS con_Valor, 
                         dbo.ct_cbtecble.cb_Observacion AS con_Observacion, dbo.ct_cbtecble.IdCbteCble AS con_IdCbteCble, dbo.ct_cbtecble_det.IdCtaCble, dbo.ct_plancta.pc_Cuenta, 
                         dbo.ct_cbtecble_det.dc_Valor, dbo.ba_Cbte_Ban.ValorEnLetras, dbo.tb_banco.IdBanco, dbo.tb_banco.ba_descripcion
FROM            dbo.ct_cbtecble_det INNER JOIN
                         dbo.ct_cbtecble ON dbo.ct_cbtecble_det.IdEmpresa = dbo.ct_cbtecble.IdEmpresa AND dbo.ct_cbtecble_det.IdTipoCbte = dbo.ct_cbtecble.IdTipoCbte AND 
                         dbo.ct_cbtecble_det.IdCbteCble = dbo.ct_cbtecble.IdCbteCble INNER JOIN
                         dbo.ba_Cbte_Ban INNER JOIN
                         dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdEmpresa AND 
                         dbo.ba_Cbte_Ban.IdTipocbte = dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdTipoCbteCble INNER JOIN
                         dbo.ba_Cbte_Ban_tipo ON dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.CodTipoCbteBan = dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan ON 
                         dbo.ct_cbtecble.IdEmpresa = dbo.ba_Cbte_Ban.IdEmpresa AND dbo.ct_cbtecble.IdTipoCbte = dbo.ba_Cbte_Ban.IdTipocbte AND 
                         dbo.ct_cbtecble.IdCbteCble = dbo.ba_Cbte_Ban.IdCbteCble INNER JOIN
                         dbo.ct_plancta ON dbo.ct_cbtecble_det.IdEmpresa = dbo.ct_plancta.IdEmpresa AND dbo.ct_cbtecble_det.IdCtaCble = dbo.ct_plancta.IdCtaCble INNER JOIN
                         dbo.ba_Banco_Cuenta ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ba_Banco_Cuenta.IdEmpresa AND 
                         dbo.ba_Cbte_Ban.IdBanco = dbo.ba_Banco_Cuenta.IdBanco INNER JOIN
                         dbo.tb_banco ON dbo.ba_Banco_Cuenta.IdBanco_Financiero = dbo.tb_banco.IdBanco
WHERE        (dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan = 'CHEQ')
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[39] 4[25] 2[21] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[25] 3) )"
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
         Configuration = "(H (1[45] 2) )"
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
      ActivePaneConfig = 2
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ct_cbtecble_det"
            Begin Extent = 
               Top = 2
               Left = 44
               Bottom = 131
               Right = 307
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "ct_cbtecble"
            Begin Extent = 
               Top = 6
               Left = 339
               Bottom = 135
               Right = 548
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_Cbte_Ban"
            Begin Extent = 
               Top = 6
               Left = 578
               Bottom = 135
               Right = 787
            End
            DisplayFlags = 280
            TopColumn = 31
         End
         Begin Table = "ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo"
            Begin Extent = 
               Top = 3
               Left = 817
               Bottom = 132
               Right = 1026
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "ba_Cbte_Ban_tipo"
            Begin Extent = 
               Top = 51
               Left = 1057
               Bottom = 180
               Right = 1266
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_plancta"
            Begin Extent = 
               Top = 100
               Left = 421
               Bottom = 229
               Right = 630
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
      Begin ColumnWidths = 25
         Width = ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwBa_ChequexCbteCtble';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'284
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
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwBa_ChequexCbteCtble';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwBa_ChequexCbteCtble';

