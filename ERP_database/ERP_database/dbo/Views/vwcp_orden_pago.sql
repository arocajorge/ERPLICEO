CREATE VIEW dbo.vwcp_orden_pago
AS
SELECT        OP.IdEmpresa, OP.IdOrdenPago, OP.IdTipo_op, OP.IdTipo_Persona, OP.IdPersona, OP.IdEntidad, OP.Fecha, OP.IdEstadoAprobacion, OP.IdFormaPago, OP.Estado, 
                         dbo.tb_persona.pe_nombreCompleto, CAN.Total_OP, ISNULL(PAGO.Total_cancelado, 0) AS Total_cancelado, ROUND(CAN.Total_OP - ISNULL(PAGO.Total_cancelado, 0), 2) AS Saldo, OP.Observacion, null IdTipoFlujo, 
                         '' AS nom_tipoFlujo, CASE WHEN round(CAN.Total_OP - ISNULL(PAGO.Total_cancelado, 0), 2) > 0 THEN 'PENDIENTE' WHEN round(CAN.Total_OP - ISNULL(PAGO.Total_cancelado, 0), 2) 
                         <= 0 THEN 'CANCELADA' END AS EstadoCancelacion, dbo.cp_orden_pago_estado_aprob.Descripcion, OP.IdSucursal, dbo.tb_sucursal.Su_Descripcion
FROM            dbo.cp_orden_pago AS OP INNER JOIN
                         dbo.tb_persona ON OP.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.vwcp_orden_pago_total AS CAN ON OP.IdEmpresa = CAN.IdEmpresa AND OP.IdOrdenPago = CAN.IdOrdenPago INNER JOIN
                         dbo.cp_orden_pago_estado_aprob ON OP.IdEstadoAprobacion = dbo.cp_orden_pago_estado_aprob.IdEstadoAprobacion LEFT OUTER JOIN
                         dbo.tb_sucursal ON OP.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND OP.IdSucursal = dbo.tb_sucursal.IdSucursal LEFT OUTER JOIN
                         
                         dbo.vwcp_orden_pago_Total_Pagado AS PAGO ON CAN.IdEmpresa = PAGO.IdEmpresa_op AND CAN.IdOrdenPago = PAGO.IdOrdenPago_op
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[53] 4[3] 2[15] 3) )"
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
         Begin Table = "OP"
            Begin Extent = 
               Top = 7
               Left = 40
               Bottom = 212
               Right = 226
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 180
               Left = 498
               Bottom = 312
               Right = 690
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CAN"
            Begin Extent = 
               Top = 0
               Left = 305
               Bottom = 228
               Right = 465
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_pago_estado_aprob"
            Begin Extent = 
               Top = 217
               Left = 48
               Bottom = 336
               Right = 279
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_TipoFlujo"
            Begin Extent = 
               Top = 261
               Left = 286
               Bottom = 390
               Right = 495
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PAGO"
            Begin Extent = 
               Top = 6
               Left = 543
               Bottom = 154
               Right = 714
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 184
               Left = 838
               Bottom = 314
               Right = 1068
            End
            D', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_pago';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'isplayFlags = 280
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
         Width = 2805
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
      Begin ColumnWidths = 11
         Column = 3090
         Alias = 2505
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_pago';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_pago';

