CREATE VIEW [dbo].[vwfa_creditos_debitos_con_saldo]
AS
SELECT        NT.IdEmpresa, NT.IdSucursal, NT.IdBodega, CASE NT.CreDeb WHEN 'C' THEN 'NTCR' WHEN 'D' THEN 'NTDB' END AS Tipo, NT.IdNota, NT.CreDeb, NT.Serie1, 
                         NT.Serie2, NT.NumNota_Impresa, cli.IdCliente, sucu.Su_Descripcion AS NomSucursal, Bod.bo_Descripcion AS Nom_Bodega, NT.no_fecha, NT.no_fecha_venc, 
                         NT.sc_observacion, pe.pe_nombreCompleto AS Nom_Cliente, tipNt.No_Descripcion AS Motivo_Nota, CASE WHEN NT.NumNota_Impresa IS NOT NULL 
                         THEN 'NT' + NT.CreDeb + '-' + NT.Serie1 + NT.Serie2 + CAST(NT.NumNota_Impresa AS varchar(20)) + '/' + CAST(NT.IdNota AS varchar(25)) 
                         ELSE 'NT' + NT.CreDeb + '-' + CAST(NT.IdNota AS varchar(25)) END AS Referencia, SUM(NT_det.sc_total) AS sc_total, SUM(NT_det.sc_total) -
                             ((SELECT        CASE WHEN SUM(notCxc.Valor_cobro) IS NULL THEN 0 ELSE SUM(notCxc.Valor_cobro) END AS Expr1
                                 FROM            dbo.fa_notaCreDeb_x_cxc_cobro AS notCxc
                                 WHERE        (IdEmpresa_nt = NT.IdEmpresa) AND (IdSucursal_nt = NT.IdSucursal) AND (IdNota_nt = NT.IdNota) AND (IdBodega_nt = NT.IdBodega)) +
                             (SELECT        ISNULL(ROUND(SUM(dc_ValorPago), 2), 0) AS Expr1
                               FROM            dbo.cxc_cobro_det AS cobro
                               WHERE        (IdEmpresa = NT.IdEmpresa) AND (IdSucursal = NT.IdSucursal) AND (IdBodega_Cbte = NT.IdBodega) AND (IdCbte_vta_nota = NT.IdNota) AND 
                                                         (dc_TipoDocumento = (CASE NT.CreDeb WHEN 'C' THEN 'NTCR' ELSE 'NTDB' END)))) AS Saldo, 'NT_CR_DB' AS IdTipoConciliacion, tipNt.IdTipoNota
                      
FROM            dbo.fa_notaCreDeb AS NT INNER JOIN
                         dbo.fa_notaCreDeb_det AS NT_det ON NT.IdEmpresa = NT_det.IdEmpresa AND NT.IdSucursal = NT_det.IdSucursal AND NT.IdBodega = NT_det.IdBodega AND 
                         NT.IdNota = NT_det.IdNota INNER JOIN
                         dbo.fa_TipoNota AS tipNt ON NT.IdTipoNota = tipNt.IdTipoNota INNER JOIN
                         dbo.fa_cliente AS cli ON NT.IdEmpresa = cli.IdEmpresa AND NT.IdCliente = cli.IdCliente INNER JOIN
                         dbo.tb_persona AS pe ON cli.IdPersona = pe.IdPersona INNER JOIN
                         dbo.tb_bodega AS Bod ON NT.IdEmpresa = Bod.IdEmpresa AND NT.IdSucursal = Bod.IdSucursal AND NT.IdBodega = Bod.IdBodega INNER JOIN
                         dbo.tb_sucursal AS sucu ON Bod.IdEmpresa = sucu.IdEmpresa AND Bod.IdSucursal = sucu.IdSucursal LEFT OUTER JOIN
                         dbo.cxc_cobro_det AS cobro ON cobro.IdEmpresa = NT.IdEmpresa AND cobro.IdSucursal = NT.IdSucursal AND cobro.IdBodega_Cbte = NT.IdBodega AND 
                         cobro.IdCbte_vta_nota = NT.IdNota AND cobro.dc_TipoDocumento = (CASE NT.CreDeb WHEN 'C' THEN 'NTCR' ELSE 'NTDB' END)
GROUP BY NT.IdEmpresa, NT.IdSucursal, NT.IdBodega, NT.IdNota, NT.CodNota, NT.CreDeb, NT.Serie1, NT.Serie2, NT.NumNota_Impresa, NT.NumAutorizacion, cli.IdCliente, 
                         sucu.Su_Descripcion, NT.no_fecha, NT.no_fecha_venc, NT.sc_observacion, pe.pe_nombreCompleto, tipNt.No_Descripcion, Bod.bo_Descripcion, tipNt.IdTipoNota
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[28] 4[19] 2[39] 3) )"
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
         Begin Table = "NT"
            Begin Extent = 
               Top = 7
               Left = 306
               Bottom = 126
               Right = 500
            End
            DisplayFlags = 280
            TopColumn = 34
         End
         Begin Table = "NT_det"
            Begin Extent = 
               Top = 0
               Left = 600
               Bottom = 119
               Right = 779
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tipNt"
            Begin Extent = 
               Top = 0
               Left = 53
               Bottom = 119
               Right = 237
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cli"
            Begin Extent = 
               Top = 93
               Left = 515
               Bottom = 212
               Right = 741
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pe"
            Begin Extent = 
               Top = 62
               Left = 714
               Bottom = 181
               Right = 922
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Bod"
            Begin Extent = 
               Top = 366
               Left = 38
               Bottom = 485
               Right = 252
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "sucu"
            Begin Extent = 
               Top = 486
               Left = 38
               Bottom = 605
               Right = 268
            End
            DisplayFlags = 280
            TopColumn = 0
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_creditos_debitos_con_saldo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
         End
         Begin Table = "cobro"
            Begin Extent = 
               Top = 6
               Left = 960
               Bottom = 135
               Right = 1185
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
      Begin ColumnWidths = 22
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_creditos_debitos_con_saldo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_creditos_debitos_con_saldo';

