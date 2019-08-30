CREATE VIEW [dbo].[vwba_Archivo_Transferencia_Det]
AS
SELECT d.IdEmpresa, d.IdArchivo, d.Secuencia, d.IdEmpresa_OP, d.IdOrdenPago, d.Secuencia_OP, d.Estado, d.Valor, d.Secuencial_reg_x_proceso, 

isnull(pd.IdTipoCta_acreditacion_cat, dbo.cp_proveedor.IdTipoCta_acreditacion_cat) IdTipoCta_acreditacion_cat, 
isnull(pd.num_cta_acreditacion, dbo.cp_proveedor.num_cta_acreditacion)num_cta_acreditacion, 
isnull(pd.IdBanco_acreditacion, dbo.cp_proveedor.IdBanco_acreditacion) IdBanco_acreditacion, 
				  dbo.cp_proveedor.pr_direccion, isnull(pd.pr_correo, dbo.cp_proveedor.pr_correo)pr_correo, dbo.tb_persona.IdTipoDocumento, dbo.tb_persona.pe_cedulaRuc, 
				  isnull(pd.NombreAdicional, dbo.tb_persona.pe_nombreCompleto)pe_nombreCompleto, 
                  dbo.tb_banco.CodigoLegal AS CodigoLegalBanco, d.Referencia, dbo.cp_orden_pago.IdTipo_Persona, dbo.cp_orden_pago.IdPersona, dbo.cp_orden_pago.IdEntidad, dbo.ct_cbtecble.cb_Fecha, ct.IdCtaCble_Acreedora, 
                  pc.IdCtaCble + ' - ' + pc.pc_Cuenta AS pc_Cuenta, dbo.cp_orden_pago_det.IdEmpresa_cxp, dbo.cp_orden_pago_det.IdTipoCbte_cxp, dbo.cp_orden_pago_det.IdCbteCble_cxp
FROM     dbo.ba_Archivo_Transferencia INNER JOIN
                  dbo.ba_Archivo_Transferencia_Det AS d INNER JOIN
                  dbo.cp_orden_pago_det ON d.IdEmpresa_OP = dbo.cp_orden_pago_det.IdEmpresa AND d.IdOrdenPago = dbo.cp_orden_pago_det.IdOrdenPago AND d.Secuencia_OP = dbo.cp_orden_pago_det.Secuencia INNER JOIN
                  dbo.cp_orden_pago ON dbo.cp_orden_pago_det.IdEmpresa = dbo.cp_orden_pago.IdEmpresa AND dbo.cp_orden_pago_det.IdOrdenPago = dbo.cp_orden_pago.IdOrdenPago INNER JOIN
                  dbo.tb_persona INNER JOIN
                  dbo.cp_proveedor ON dbo.tb_persona.IdPersona = dbo.cp_proveedor.IdPersona ON dbo.cp_orden_pago.IdPersona = dbo.cp_proveedor.IdPersona AND dbo.cp_orden_pago.IdEntidad = dbo.cp_proveedor.IdProveedor AND 
                  dbo.cp_orden_pago.IdEmpresa = dbo.cp_proveedor.IdEmpresa ON dbo.ba_Archivo_Transferencia.IdEmpresa = d.IdEmpresa AND dbo.ba_Archivo_Transferencia.IdArchivo = d.IdArchivo 
				  left join 
				  cp_orden_giro as og on cp_orden_pago_det.IdEmpresa_cxp = og.IdEmpresa and cp_orden_pago_det.IdTipoCbte_cxp = og.IdTipoCbte_Ogiro and cp_orden_pago_det.IdCbteCble_cxp = og.IdCbteCble_Ogiro left join
				  cp_proveedor_detalle as pd on pd.IdEmpresa = og.IdEmpresa and pd.IdProveedor = og.IdProveedor and pd.Secuencia = og.SecuenciaProveedor
				  INNER JOIN
                  dbo.tb_banco ON dbo.tb_banco.IdBanco = ISNULL(pd.IdBanco_acreditacion, dbo.cp_proveedor.IdBanco_acreditacion) INNER JOIN
                  dbo.ct_cbtecble ON dbo.cp_orden_pago_det.IdEmpresa_cxp = dbo.ct_cbtecble.IdEmpresa AND dbo.cp_orden_pago_det.IdTipoCbte_cxp = dbo.ct_cbtecble.IdTipoCbte AND 
                  dbo.cp_orden_pago_det.IdCbteCble_cxp = dbo.ct_cbtecble.IdCbteCble LEFT OUTER JOIN
                  dbo.vwct_cbtecble_con_ctacble_acreedora AS ct ON ct.IdEmpresa = dbo.cp_orden_pago_det.IdEmpresa AND ct.IdTipoCbte = dbo.cp_orden_pago_det.IdTipoCbte_cxp AND 
                  ct.IdCbteCble = dbo.cp_orden_pago_det.IdCbteCble_cxp LEFT OUTER JOIN
                  dbo.ct_plancta AS pc ON ct.IdEmpresa = pc.IdEmpresa AND ct.IdCtaCble_Acreedora = pc.IdCtaCble
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_Archivo_Transferencia_Det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'     Right = 1255
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "ct_cbtecble"
            Begin Extent = 
               Top = 323
               Left = 784
               Bottom = 596
               Right = 966
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct"
            Begin Extent = 
               Top = 7
               Left = 1122
               Bottom = 170
               Right = 1357
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pc"
            Begin Extent = 
               Top = 7
               Left = 1405
               Bottom = 170
               Right = 1616
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
      Begin ColumnWidths = 23
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
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 4392
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_Archivo_Transferencia_Det';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[28] 4[60] 2[3] 3) )"
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
               Top = 0
               Left = 339
               Bottom = 300
               Right = 531
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_Archivo_Transferencia_Det"
            Begin Extent = 
               Top = 0
               Left = 9
               Bottom = 407
               Right = 233
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_pago_det"
            Begin Extent = 
               Top = 156
               Left = 668
               Bottom = 509
               Right = 874
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_pago"
            Begin Extent = 
               Top = 313
               Left = 340
               Bottom = 609
               Right = 531
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 285
               Left = 1019
               Bottom = 415
               Right = 1251
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_proveedor"
            Begin Extent = 
               Top = 55
               Left = 842
               Bottom = 185
               Right = 1074
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_banco"
            Begin Extent = 
               Top = 425
               Left = 1021
               Bottom = 555
          ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_Archivo_Transferencia_Det';







