CREATE VIEW [dbo].[vwct_cbtecble_Con_Saldo]
AS
SELECT      A.IdEmpresa, A.IdTipoCbte, A.IdCbteCble, AVG(A.dc_Valor) AS dc_Valor, SUM(B.MontoAplicado) AS MontoOG, AVG(A.dc_Valor) - SUM(B.MontoAplicado) 
                      AS SaldoDiario, 'Diarios con pagos' AS Detalle, C.cb_Observacion, C.cb_Fecha
FROM         cp_orden_pago_cancelaciones AS B INNER JOIN
                      vwct_cbtecble_det_TotalDiario AS A ON B.IdEmpresa_op = A.IdEmpresa AND B.IdTipoCbte_pago = A.IdTipoCbte AND B.IdCbteCble_pago = A.IdCbteCble INNER JOIN
                      ct_cbtecble AS C ON A.IdEmpresa = C.IdEmpresa AND A.IdTipoCbte = C.IdTipoCbte AND A.IdCbteCble = C.IdCbteCble
GROUP BY A.IdEmpresa, A.IdTipoCbte, A.IdCbteCble, C.cb_Observacion, C.cb_Fecha
UNION
SELECT      A.IdEmpresa, A.IdTipoCbte, A.IdCbteCble, A.dc_Valor, 0 AS MontoOG, A.dc_Valor AS Saldo, 'Diarios sin pagos' AS Detalle, C.cb_Observacion, C.cb_Fecha
FROM         vwct_cbtecble_det_TotalDiario A INNER JOIN
                      ct_cbtecble AS C ON A.IdEmpresa = C.IdEmpresa AND A.IdTipoCbte = C.IdTipoCbte AND A.IdCbteCble = C.IdCbteCble
WHERE     cast(A.IdEmpresa AS varchar(20)) + '-' + CAST(A.IdTipoCbte AS varchar(20)) + '-' +CAST(A.IdCbteCble AS varchar(20)) NOT IN
                          (SELECT     cast(A.IdEmpresa AS varchar(20)) + '-'+CAST(A.IdTipoCbte_pago AS varchar(20)) + '-' + CAST(A.IdCbteCble_pago AS varchar(20))
                            FROM          cp_orden_pago_cancelaciones A )
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[14] 4[5] 2[64] 3) )"
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwct_cbtecble_Con_Saldo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwct_cbtecble_Con_Saldo';

