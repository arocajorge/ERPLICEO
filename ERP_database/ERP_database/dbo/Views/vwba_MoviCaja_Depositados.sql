CREATE view [dbo].[vwba_MoviCaja_Depositados]
as
SELECT        (CASE WHEN B.IdEmpresa IS NULL THEN G.IdEmpresa ELSE B.IdEmpresa END) AS IdEmpresa, (CASE WHEN B.IdCobro IS NULL THEN G.IdCbteCble ELSE B.IdCobro END) AS IdCobro, 
                         (CASE WHEN B.IdCobro_tipo IS NULL THEN G.CodMoviCaja ELSE B.IdCobro_tipo END) AS IdCobro_tipo, (CASE WHEN B.IdCliente IS NULL 
                         THEN 0 ELSE B.IdCliente END) AS IdCliente, isnull(caj_Caja_Movimiento_det.cr_Valor,0) AS cr_TotalCobro, 
                         (CASE WHEN B.cr_fecha IS NULL THEN G.cm_fecha ELSE B.cr_fecha END) AS cr_fecha, B.cr_fechaCobro, (CASE WHEN B.cr_observacion IS NULL 
                         THEN G.cm_observacion ELSE B.cr_observacion END) AS cr_observacion, B.cr_Banco, B.cr_cuenta, B.cr_Tarjeta, B.cr_NumDocumento, B.cr_estado, B.cr_recibo, 
                         (CASE WHEN D .pe_nombreCompleto IS NULL 
                         THEN D .pe_nombreCompleto ELSE D .pe_nombreCompleto END) AS nCliente, B.cr_fechaDocu, E.IdCaja, E.ca_Descripcion, F.IdTipoMovi, F.tm_descripcion, F.tm_Signo, 
                         B.IdUsuario, G.IdCbteCble AS IdCbteCble_MoviCaja, G.IdTipocbte AS IdTipocbte_MoviCaja, I.mba_IdEmpresa AS IdEmpresa_MoviBan, 
                         I.mba_IdCbteCble AS IdCbteCble_MoviBan, I.mba_IdTipocbte AS IdTipocbte_MoviBan, I.mcj_Secuencia
FROM            dbo.caj_Caja AS E INNER JOIN
                         dbo.caj_Caja_Movimiento AS G ON E.IdEmpresa = G.IdEmpresa AND E.IdCaja = G.IdCaja INNER JOIN
                         dbo.caj_Caja_Movimiento_Tipo AS F ON G.IdTipoMovi = F.IdTipoMovi INNER JOIN
                         dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito AS I ON G.IdEmpresa = I.mcj_IdEmpresa AND G.IdCbteCble = I.mcj_IdCbteCble AND 
                         G.IdTipocbte = I.mcj_IdTipocbte INNER JOIN
                         dbo.caj_Caja_Movimiento_det ON I.mcj_IdEmpresa = dbo.caj_Caja_Movimiento_det.IdEmpresa AND 
                         I.mcj_IdCbteCble = dbo.caj_Caja_Movimiento_det.IdCbteCble AND I.mcj_IdTipocbte = dbo.caj_Caja_Movimiento_det.IdTipocbte AND 
                         I.mcj_Secuencia = dbo.caj_Caja_Movimiento_det.Secuencia LEFT OUTER JOIN
                         dbo.tb_persona AS D INNER JOIN
                         dbo.tb_sucursal AS A INNER JOIN
                         dbo.cxc_cobro AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdSucursal = B.IdSucursal INNER JOIN
                         dbo.fa_cliente AS C ON B.IdCliente = C.IdCliente AND B.IdEmpresa = C.IdEmpresa ON D.IdPersona = C.IdPersona INNER JOIN
                         dbo.cxc_cobro_x_caj_Caja_Movimiento AS H ON B.IdEmpresa = H.cbr_IdEmpresa AND B.IdSucursal = H.cbr_IdSucursal AND B.IdCobro = H.cbr_IdCobro ON 
                         G.IdEmpresa = H.mcj_IdEmpresa AND G.IdCbteCble = H.mcj_IdCbteCble AND G.IdTipocbte = H.mcj_IdTipocbte
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[86] 4[4] 2[6] 3) )"
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
         Begin Table = "E"
            Begin Extent = 
               Top = 360
               Left = 975
               Bottom = 479
               Right = 1177
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "G"
            Begin Extent = 
               Top = 128
               Left = 578
               Bottom = 417
               Right = 760
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "F"
            Begin Extent = 
               Top = 377
               Left = 691
               Bottom = 496
               Right = 859
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "I"
            Begin Extent = 
               Top = 15
               Left = 999
               Bottom = 289
               Right = 1336
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "D"
            Begin Extent = 
               Top = 38
               Left = 0
               Bottom = 157
               Right = 192
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "A"
            Begin Extent = 
               Top = 8
               Left = 233
               Bottom = 127
               Right = 447
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "B"
            Begin Extent = 
               Top = 144
               Left = 97
               Bottom = 263
               Right = 276
            End
            DisplayFlags = 280
            TopColumn = 0
         End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_MoviCaja_Depositados';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'         Begin Table = "C"
            Begin Extent = 
               Top = 0
               Left = 455
               Bottom = 119
               Right = 665
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "H"
            Begin Extent = 
               Top = 179
               Left = 288
               Bottom = 408
               Right = 453
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
      Begin ColumnWidths = 30
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
         Width = 1755
         Width = 1500
         Width = 1500
         Width = 1695
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 3405
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_MoviCaja_Depositados';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_MoviCaja_Depositados';

