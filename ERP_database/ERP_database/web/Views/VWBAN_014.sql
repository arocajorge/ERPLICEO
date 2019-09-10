CREATE VIEW web.VWBAN_014
AS
SELECT dbo.cp_orden_pago_cancelaciones.IdEmpresa_pago, dbo.cp_orden_pago_cancelaciones.IdTipoCbte_pago, dbo.cp_orden_pago_cancelaciones.IdCbteCble_pago, CASE WHEN ba_Archivo_Transferencia.IdArchivo IS NOT NULL 
                  THEN 'ARCHIVO BANCARIO' ELSE ba_Cbte_Ban_tipo.Descripcion END AS TipoPago, dbo.tb_persona.pe_nombreCompleto, dbo.ba_Cbte_Ban.IdBanco, dbo.ba_Banco_Cuenta.ba_descripcion, ISNULL(dbo.ba_Cbte_Ban.cb_Cheque, 
                  CAST(dbo.ba_Cbte_Ban.IdCbteCble AS varchar(20))) AS cb_Cheque, dbo.ba_Cbte_Ban.cb_Observacion AS BAN_Observacion, dbo.cp_orden_pago.IdPersona, ISNULL(dbo.cp_orden_giro.co_FechaFactura, 
                  ISNULL(dbo.cp_nota_DebCre.cn_fecha, dbo.cp_orden_pago.Fecha)) AS CXP_Fecha, ISNULL(dbo.cp_orden_giro.co_factura, ISNULL(dbo.cp_nota_DebCre.cod_nota, 'OP ' + CAST(dbo.cp_orden_pago.IdOrdenPago AS varchar(20)))) 
                  AS CXP_Documento, ISNULL(dbo.cp_orden_giro.co_observacion, ISNULL(dbo.cp_nota_DebCre.cn_observacion, dbo.cp_orden_pago.Observacion)) AS CXP_Observacion, dbo.cp_orden_pago_cancelaciones.MontoAplicado, 
                  dbo.ba_Cbte_Ban.cb_Fecha AS BAN_Fecha, dbo.cp_orden_pago_cancelaciones.IdCbteCble_cxp, dbo.cp_orden_pago_cancelaciones.IdTipoCbte_cxp, dbo.ba_Cbte_Ban.IdSucursal, dbo.tb_sucursal.Su_Descripcion
FROM     dbo.cp_nota_DebCre RIGHT OUTER JOIN
                  dbo.tb_persona INNER JOIN
                  dbo.cp_orden_pago ON dbo.tb_persona.IdPersona = dbo.cp_orden_pago.IdPersona INNER JOIN
                  dbo.ba_Cbte_Ban INNER JOIN
                  dbo.ba_Banco_Cuenta ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ba_Banco_Cuenta.IdEmpresa AND dbo.ba_Cbte_Ban.IdBanco = dbo.ba_Banco_Cuenta.IdBanco INNER JOIN
                  dbo.cp_orden_pago_cancelaciones ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_pago AND dbo.ba_Cbte_Ban.IdCbteCble = dbo.cp_orden_pago_cancelaciones.IdCbteCble_pago AND 
                  dbo.ba_Cbte_Ban.IdTipocbte = dbo.cp_orden_pago_cancelaciones.IdTipoCbte_pago ON dbo.cp_orden_pago.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_op AND 
                  dbo.cp_orden_pago.IdOrdenPago = dbo.cp_orden_pago_cancelaciones.IdOrdenPago_op INNER JOIN
                  dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo INNER JOIN
                  dbo.ba_Cbte_Ban_tipo ON dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.CodTipoCbteBan = dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdEmpresa AND 
                  dbo.ba_Cbte_Ban.IdTipocbte = dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdTipoCbteCble INNER JOIN
                  dbo.tb_sucursal ON dbo.ba_Cbte_Ban.IdSucursal = dbo.tb_sucursal.IdSucursal AND dbo.ba_Cbte_Ban.IdEmpresa = dbo.tb_sucursal.IdEmpresa LEFT OUTER JOIN
                  dbo.cp_orden_giro ON dbo.cp_orden_pago_cancelaciones.IdEmpresa_cxp = dbo.cp_orden_giro.IdEmpresa AND dbo.cp_orden_pago_cancelaciones.IdTipoCbte_cxp = dbo.cp_orden_giro.IdTipoCbte_Ogiro AND 
                  dbo.cp_orden_pago_cancelaciones.IdCbteCble_cxp = dbo.cp_orden_giro.IdCbteCble_Ogiro ON dbo.cp_nota_DebCre.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_cxp AND 
                  dbo.cp_nota_DebCre.IdTipoCbte_Nota = dbo.cp_orden_pago_cancelaciones.IdTipoCbte_cxp AND dbo.cp_nota_DebCre.IdCbteCble_Nota = dbo.cp_orden_pago_cancelaciones.IdCbteCble_cxp LEFT OUTER JOIN
                  dbo.ba_Archivo_Transferencia ON dbo.ba_Cbte_Ban.IdTipocbte = dbo.ba_Archivo_Transferencia.IdTipoCbte AND dbo.ba_Cbte_Ban.IdCbteCble = dbo.ba_Archivo_Transferencia.IdCbteCble AND 
                  dbo.ba_Cbte_Ban.IdEmpresa = dbo.ba_Archivo_Transferencia.IdEmpresa
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWBAN_014';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' = 1178
               Right = 279
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_Cbte_Ban_tipo"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 256
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_giro"
            Begin Extent = 
               Top = 1351
               Left = 48
               Bottom = 1514
               Right = 355
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_nota_DebCre"
            Begin Extent = 
               Top = 1519
               Left = 48
               Bottom = 1682
               Right = 307
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 233
               Left = 1141
               Bottom = 396
               Right = 1413
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
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWBAN_014';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[90] 4[3] 2[3] 3) )"
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
         Begin Table = "ba_Archivo_Transferencia"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 281
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_pago"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 284
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_Cbte_Ban"
            Begin Extent = 
               Top = 0
               Left = 776
               Bottom = 559
               Right = 1037
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_Banco_Cuenta"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 338
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_pago_cancelaciones"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 300
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWBAN_014';

