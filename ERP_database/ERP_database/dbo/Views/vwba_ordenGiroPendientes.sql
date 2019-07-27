CREATE VIEW [dbo].[vwba_ordenGiroPendientes]
AS
SELECT        A.IdProveedor, A.co_fechaOg, A.co_observacion, A.IdEmpresa, A.IdCbteCble_Ogiro, A.IdTipoCbte_Ogiro, AVG(A.co_valorpagar) AS valorAPagar, 
                         ISNULL(SUM(B.MontoAplicado), 0) AS TotalPagado, AVG(A.co_valorpagar) - ISNULL(SUM(B.MontoAplicado), 0) AS saldo, per.pe_nombreCompleto AS Proveedor, 
                         A.co_factura AS NFactura, '' AS GiraCheque, P.IdCtaCble_CXP AS CtaProveedor
FROM            dbo.cp_orden_giro AS A INNER JOIN
                         dbo.cp_proveedor AS P ON A.IdEmpresa = P.IdEmpresa AND A.IdProveedor = P.IdProveedor LEFT OUTER JOIN
                         dbo.cp_orden_pago_cancelaciones AS B ON A.IdEmpresa = B.IdEmpresa_cxp AND A.IdCbteCble_Ogiro = B.IdCbteCble_cxp AND 
                         A.IdTipoCbte_Ogiro = B.IdTipoCbte_cxp
						 inner join tb_persona as per on p.IdPersona = per.IdPersona
WHERE        (A.Estado = 'A')
GROUP BY A.IdProveedor, A.co_fechaOg, A.co_observacion, A.IdEmpresa, A.IdCbteCble_Ogiro, A.IdTipoCbte_Ogiro, A.co_valorpagar, per.pe_nombreCompleto, A.co_factura, 
                          P.IdCtaCble_CXP
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[33] 4[20] 2[33] 3) )"
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
         Begin Table = "A"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 207
               Right = 272
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "P"
            Begin Extent = 
               Top = 110
               Left = 301
               Bottom = 253
               Right = 527
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "B"
            Begin Extent = 
               Top = 6
               Left = 565
               Bottom = 135
               Right = 794
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
      Begin ColumnWidths = 12
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_ordenGiroPendientes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_ordenGiroPendientes';

