CREATE VIEW [dbo].[vwcp_orden_pago_det]
AS
SELECT        A.IdEmpresa, A.IdOrdenPago, A.Observacion, A.IdTipo_op, A.IdTipo_Persona, A.IdPersona, A.Fecha, A.Estado, B.Secuencia, CASE WHEN D .Descripcion IS NOT NULL
                          THEN substring(D .Descripcion, 1, 5) + '-' + C.co_serie + '-' + C.co_factura + '/' + ' cbtecxp#' + CAST(C.IdCbteCble_Ogiro AS varchar(20)) 
                         ELSE B.Referencia END AS referencia, CASE WHEN C.co_total IS NOT NULL THEN C.co_total ELSE B.Valor_a_pagar END AS Total_total, 
                         CASE WHEN C.co_total IS NOT NULL THEN C.co_valorpagar ELSE B.Valor_a_pagar END AS Total_a_Pagar, B.Valor_a_pagar AS Total_a_pagar_OP, 
                         B.IdEstadoAprobacion, B.IdEmpresa_cxp, B.IdCbteCble_cxp, B.IdTipoCbte_cxp
FROM            dbo.cp_orden_pago AS A INNER JOIN
                         dbo.cp_orden_pago_det AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdOrdenPago = B.IdOrdenPago LEFT OUTER JOIN
                         dbo.cp_TipoDocumento AS D INNER JOIN
                         dbo.cp_orden_giro AS C ON D.CodTipoDocumento = C.IdOrden_giro_Tipo ON B.IdEmpresa_cxp = C.IdEmpresa AND B.IdCbteCble_cxp = C.IdCbteCble_Ogiro AND 
                         B.IdTipoCbte_cxp = C.IdTipoCbte_Ogiro
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[61] 4[18] 2[11] 3) )"
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
         Top = -35
         Left = 0
      End
      Begin Tables = 
         Begin Table = "A"
            Begin Extent = 
               Top = 0
               Left = 42
               Bottom = 334
               Right = 231
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "B"
            Begin Extent = 
               Top = 0
               Left = 266
               Bottom = 333
               Right = 461
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "D"
            Begin Extent = 
               Top = 22
               Left = 670
               Bottom = 141
               Right = 922
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "C"
            Begin Extent = 
               Top = 2
               Left = 506
               Bottom = 240
               Right = 724
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
      Begin ColumnWidths = 20
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
         Width = 4290
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 75
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 8775
         Alias = 2520
         Table = 1170
         Output = 7', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_pago_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'20
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_pago_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_pago_det';

