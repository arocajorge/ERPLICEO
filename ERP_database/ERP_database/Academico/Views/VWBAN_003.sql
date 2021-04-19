CREATE VIEW Academico.VWBAN_003
AS
SELECT dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan, dbo.ba_Cbte_Ban.IdEmpresa, dbo.ba_Cbte_Ban.IdCbteCble, dbo.ba_Cbte_Ban.IdTipocbte, dbo.ba_Cbte_Ban.IdBanco, dbo.ba_Banco_Cuenta.ba_descripcion, dbo.ba_Cbte_Ban.cb_Fecha, 
                  dbo.ba_Cbte_Ban.cb_Observacion, dbo.ba_Cbte_Ban.Estado, dbo.ba_Cbte_Ban.IdTipoNota, dbo.tb_persona.pe_nombreCompleto, dbo.ct_cbtecble_det.IdCtaCble, dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.dc_Valor, 
                  CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ct_cbtecble_det.dc_Valor ELSE 0 END AS dc_Valor_Debe, CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Haber, 
                  dbo.ba_Cbte_Ban.cb_Cheque, dbo.ba_Cbte_Ban.cb_giradoA, dbo.seg_usuario.Nombre AS NombreUsuario, dbo.tb_sucursal.Su_Descripcion
FROM     dbo.ba_Cbte_Ban_tipo WITH (nolock) INNER JOIN
                  dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo WITH (nolock) ON dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan = dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.CodTipoCbteBan INNER JOIN
                  dbo.ba_Cbte_Ban WITH (nolock) ON dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdEmpresa = dbo.ba_Cbte_Ban.IdEmpresa AND dbo.ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo.IdTipoCbteCble = dbo.ba_Cbte_Ban.IdTipocbte INNER JOIN
                  dbo.ba_Banco_Cuenta WITH (nolock) ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ba_Banco_Cuenta.IdEmpresa AND dbo.ba_Cbte_Ban.IdBanco = dbo.ba_Banco_Cuenta.IdBanco INNER JOIN
                  dbo.ct_cbtecble_det WITH (nolock) ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.ct_cbtecble_det.IdEmpresa AND dbo.ba_Cbte_Ban.IdTipocbte = dbo.ct_cbtecble_det.IdTipoCbte AND 
                  dbo.ba_Cbte_Ban.IdCbteCble = dbo.ct_cbtecble_det.IdCbteCble INNER JOIN
                  dbo.ct_plancta WITH (nolock) ON dbo.ct_cbtecble_det.IdCtaCble = dbo.ct_plancta.IdCtaCble AND dbo.ct_cbtecble_det.IdEmpresa = dbo.ct_plancta.IdEmpresa LEFT OUTER JOIN
                  dbo.tb_sucursal WITH (nolock) ON dbo.ba_Cbte_Ban.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ba_Cbte_Ban.IdSucursal = dbo.tb_sucursal.IdSucursal LEFT OUTER JOIN
                  dbo.tb_persona WITH (nolock) INNER JOIN
                  dbo.cp_orden_pago WITH (nolock) ON dbo.tb_persona.IdPersona = dbo.cp_orden_pago.IdPersona INNER JOIN
                  dbo.cp_orden_pago_cancelaciones WITH (nolock) ON dbo.cp_orden_pago.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_op AND dbo.cp_orden_pago.IdOrdenPago = dbo.cp_orden_pago_cancelaciones.IdOrdenPago_op ON 
                  dbo.ba_Cbte_Ban.IdEmpresa = dbo.cp_orden_pago_cancelaciones.IdEmpresa_pago AND dbo.ba_Cbte_Ban.IdCbteCble = dbo.cp_orden_pago_cancelaciones.IdCbteCble_pago AND 
                  dbo.ba_Cbte_Ban.IdTipocbte = dbo.cp_orden_pago_cancelaciones.IdTipoCbte_pago LEFT OUTER JOIN
                  dbo.seg_usuario WITH (nolock) ON dbo.ba_Cbte_Ban.IdUsuario = dbo.seg_usuario.IdUsuario
WHERE  (dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan = 'CHEQ')
GROUP BY dbo.ba_Cbte_Ban_tipo.CodTipoCbteBan, dbo.ba_Cbte_Ban.IdEmpresa, dbo.ba_Cbte_Ban.IdCbteCble, dbo.ba_Cbte_Ban.IdTipocbte, dbo.ba_Cbte_Ban.IdBanco, dbo.ba_Banco_Cuenta.ba_descripcion, dbo.ba_Cbte_Ban.cb_Fecha, 
                  dbo.ba_Cbte_Ban.cb_Observacion, dbo.ba_Cbte_Ban.Estado, dbo.ba_Cbte_Ban.IdTipoNota, dbo.tb_persona.pe_nombreCompleto, dbo.ct_cbtecble_det.IdCtaCble, dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.dc_Valor, 
                  dbo.ba_Cbte_Ban.cb_Cheque, dbo.ba_Cbte_Ban.cb_giradoA, dbo.ct_cbtecble_det.secuencia, dbo.seg_usuario.Nombre, dbo.tb_sucursal.Su_Descripcion
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWBAN_003';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'    Right = 336
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 338
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_pago"
            Begin Extent = 
               Top = 1351
               Left = 48
               Bottom = 1514
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_pago_cancelaciones"
            Begin Extent = 
               Top = 1519
               Left = 48
               Bottom = 1682
               Right = 316
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "seg_usuario"
            Begin Extent = 
               Top = 1687
               Left = 48
               Bottom = 1850
               Right = 358
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
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
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
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWBAN_003';


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
         Begin Table = "ba_Cbte_Ban_tipo"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_Cbte_Ban"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 325
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_Banco_Cuenta"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 354
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_cbtecble_det"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_plancta"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
           ', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWBAN_003';

