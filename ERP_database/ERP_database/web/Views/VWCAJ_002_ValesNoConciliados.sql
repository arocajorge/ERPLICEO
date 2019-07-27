CREATE VIEW web.VWCAJ_002_ValesNoConciliados
AS
SELECT        dbo.cp_conciliacion_Caja_ValesNoConciliados.IdEmpresa, dbo.cp_conciliacion_Caja_ValesNoConciliados.IdConciliacion_Caja, caj.IdTipocbte, caj.IdCbteCble, dbo.cp_conciliacion_Caja_ValesNoConciliados.Valor, 
                         per.pe_nombreCompleto, caj_cab.cm_observacion, caj_cab.cm_fecha, caj_cab.SecuenciaCaja
FROM            dbo.caj_Caja_Movimiento_det AS caj INNER JOIN
                         dbo.caj_Caja_Movimiento AS caj_cab ON caj_cab.IdEmpresa = caj.IdEmpresa AND caj_cab.IdTipocbte = caj.IdTipocbte AND caj_cab.IdCbteCble = caj.IdCbteCble INNER JOIN
                         dbo.tb_persona AS per ON caj_cab.IdPersona = per.IdPersona INNER JOIN
                         dbo.cp_conciliacion_Caja_ValesNoConciliados ON caj.IdEmpresa = dbo.cp_conciliacion_Caja_ValesNoConciliados.IdEmpresa_movcaja AND 
                         caj.IdTipocbte = dbo.cp_conciliacion_Caja_ValesNoConciliados.IdTipocbte_movcaja AND caj.IdCbteCble = dbo.cp_conciliacion_Caja_ValesNoConciliados.IdCbteCble_movcaja
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCAJ_002_ValesNoConciliados';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCAJ_002_ValesNoConciliados';


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
         Begin Table = "caj"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_cab"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 187
               Right = 427
            End
            DisplayFlags = 280
            TopColumn = 17
         End
         Begin Table = "per"
            Begin Extent = 
               Top = 46
               Left = 702
               Bottom = 176
               Right = 934
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_conciliacion_Caja_ValesNoConciliados"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 236
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
         ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCAJ_002_ValesNoConciliados';

