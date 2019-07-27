CREATE view [web].[VWBAN_010]
as
SELECT dbo.ba_Archivo_Transferencia_Det.IdEmpresa, dbo.ba_Archivo_Transferencia_Det.IdArchivo, dbo.ba_Archivo_Transferencia_Det.Secuencia, dbo.ba_Archivo_Transferencia_Det.IdEmpresa_OP, 
                  dbo.ba_Archivo_Transferencia_Det.IdOrdenPago, dbo.ba_Archivo_Transferencia_Det.Secuencia_OP, dbo.ba_Archivo_Transferencia_Det.Estado, dbo.ba_Archivo_Transferencia_Det.Valor, 
                  dbo.ba_Archivo_Transferencia_Det.Secuencial_reg_x_proceso, CAST(0 AS BIT) Contabilizado, GETDATE() Fecha_proceso, dbo.cp_proveedor.IdTipoCta_acreditacion_cat, 
                  dbo.cp_proveedor.num_cta_acreditacion, dbo.cp_proveedor.IdBanco_acreditacion, dbo.cp_proveedor.pr_direccion, dbo.cp_proveedor.pr_correo, dbo.tb_persona.IdTipoDocumento, dbo.tb_persona.pe_cedulaRuc, 
                  dbo.tb_persona.pe_nombreCompleto, dbo.tb_banco.CodigoLegal AS CodigoLegalBanco, dbo.ba_Archivo_Transferencia_Det.Referencia, dbo.cp_orden_pago.IdTipo_Persona, dbo.cp_orden_pago.IdPersona, 
                  dbo.cp_orden_pago.IdEntidad, dbo.ct_cbtecble.cb_Fecha, dbo.tb_banco.ba_descripcion, dbo.ba_Banco_Cuenta.ba_descripcion AS NomCuenta, dbo.tb_banco_procesos_bancarios_x_empresa.NombreProceso, 
                  dbo.ba_Archivo_Transferencia.Fecha, dbo.ba_Archivo_Transferencia.Observacion
FROM     dbo.ba_Archivo_Transferencia INNER JOIN
                  dbo.ba_Archivo_Transferencia_Det INNER JOIN
                  dbo.cp_orden_pago_det ON dbo.ba_Archivo_Transferencia_Det.IdEmpresa_OP = dbo.cp_orden_pago_det.IdEmpresa AND dbo.ba_Archivo_Transferencia_Det.IdOrdenPago = dbo.cp_orden_pago_det.IdOrdenPago AND 
                  dbo.ba_Archivo_Transferencia_Det.Secuencia_OP = dbo.cp_orden_pago_det.Secuencia INNER JOIN
                  dbo.cp_orden_pago ON dbo.cp_orden_pago_det.IdEmpresa = dbo.cp_orden_pago.IdEmpresa AND dbo.cp_orden_pago_det.IdOrdenPago = dbo.cp_orden_pago.IdOrdenPago INNER JOIN
                  dbo.tb_persona INNER JOIN
                  dbo.cp_proveedor ON dbo.tb_persona.IdPersona = dbo.cp_proveedor.IdPersona ON dbo.cp_orden_pago.IdPersona = dbo.cp_proveedor.IdPersona AND dbo.cp_orden_pago.IdEntidad = dbo.cp_proveedor.IdProveedor AND 
                  dbo.cp_orden_pago.IdEmpresa = dbo.cp_proveedor.IdEmpresa ON dbo.ba_Archivo_Transferencia.IdEmpresa = dbo.ba_Archivo_Transferencia_Det.IdEmpresa AND 
                  dbo.ba_Archivo_Transferencia.IdArchivo = dbo.ba_Archivo_Transferencia_Det.IdArchivo INNER JOIN
                  dbo.tb_banco ON dbo.cp_proveedor.IdBanco_acreditacion = dbo.tb_banco.IdBanco INNER JOIN
                  dbo.ct_cbtecble ON dbo.cp_orden_pago_det.IdEmpresa_cxp = dbo.ct_cbtecble.IdEmpresa AND dbo.cp_orden_pago_det.IdTipoCbte_cxp = dbo.ct_cbtecble.IdTipoCbte AND 
                  dbo.cp_orden_pago_det.IdCbteCble_cxp = dbo.ct_cbtecble.IdCbteCble INNER JOIN
                  dbo.ba_Banco_Cuenta ON dbo.ba_Archivo_Transferencia.IdEmpresa = dbo.ba_Banco_Cuenta.IdEmpresa AND dbo.ba_Archivo_Transferencia.IdBanco = dbo.ba_Banco_Cuenta.IdBanco INNER JOIN
                  dbo.tb_banco_procesos_bancarios_x_empresa ON dbo.ba_Archivo_Transferencia.IdEmpresa = dbo.tb_banco_procesos_bancarios_x_empresa.IdEmpresa AND 
                  dbo.ba_Archivo_Transferencia.IdProceso_bancario = dbo.tb_banco_procesos_bancarios_x_empresa.IdProceso
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWBAN_010';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'   Right = 323
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_cbtecble"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 260
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_Banco_Cuenta"
            Begin Extent = 
               Top = 0
               Left = 1333
               Bottom = 163
               Right = 1623
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_banco_procesos_bancarios_x_empresa"
            Begin Extent = 
               Top = 178
               Left = 1329
               Bottom = 427
               Right = 1586
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
      Begin ColumnWidths = 32
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 2040
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWBAN_010';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[57] 4[0] 2[3] 3) )"
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
               Left = 883
               Bottom = 398
               Right = 1107
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_Archivo_Transferencia_Det"
            Begin Extent = 
               Top = 46
               Left = 506
               Bottom = 527
               Right = 771
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_pago_det"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 290
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_pago"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 284
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_proveedor"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_banco"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
            ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWBAN_010';

