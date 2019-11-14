CREATE VIEW web.VWBAN_010
AS
SELECT     dbo.ba_Archivo_Transferencia_Det.IdEmpresa, dbo.ba_Archivo_Transferencia_Det.IdArchivo, dbo.ba_Archivo_Transferencia_Det.Secuencia, 
                      dbo.ba_Archivo_Transferencia_Det.IdEmpresa_OP, dbo.ba_Archivo_Transferencia_Det.IdOrdenPago, dbo.ba_Archivo_Transferencia_Det.Secuencia_OP, 
                      dbo.ba_Archivo_Transferencia_Det.Estado, dbo.ba_Archivo_Transferencia_Det.Valor, dbo.ba_Archivo_Transferencia_Det.Secuencial_reg_x_proceso, CAST(0 AS BIT) 
                      AS Contabilizado, GETDATE() AS Fecha_proceso, CASE WHEN cp_proveedor_detalle.IdTipoCta_acreditacion_cat IS NULL THEN isnull(opd.IdTipoCta_acreditacion_cat, 
                      dbo.cp_proveedor.IdTipoCta_acreditacion_cat) ELSE cp_proveedor_detalle.IdTipoCta_acreditacion_cat END AS IdTipoCta_acreditacion_cat, 
                      CASE WHEN cp_proveedor_detalle.num_cta_acreditacion IS NULL THEN isnull(opd.num_cta_acreditacion, dbo.cp_proveedor.num_cta_acreditacion) 
                      ELSE cp_proveedor_detalle.num_cta_acreditacion END AS num_cta_acreditacion, CASE WHEN cp_proveedor_detalle.IdBanco_acreditacion IS NULL 
                      THEN isnull(opd.IdBanco_acreditacion, dbo.cp_proveedor.IdBanco_acreditacion) ELSE cp_proveedor_detalle.IdBanco_acreditacion END AS IdBanco_acreditacion, 
                      CASE WHEN cp_proveedor_detalle.pr_correo IS NULL THEN isnull(opd.pr_correo, dbo.cp_proveedor.pr_correo) 
                      ELSE cp_proveedor_detalle.pr_correo END AS pr_correo, CASE WHEN cp_proveedor_detalle.NombreAdicional IS NULL THEN isnull(opd.NombreAdicional, 
                      dbo.tb_persona.pe_nombreCompleto) ELSE cp_proveedor_detalle.NombreAdicional END AS pe_nombreCompleto, dbo.cp_proveedor.pr_direccion, 
                      dbo.tb_persona.IdTipoDocumento, dbo.tb_persona.pe_cedulaRuc, dbo.tb_banco.CodigoLegal AS CodigoLegalBanco, dbo.ba_Archivo_Transferencia_Det.Referencia, 
                      dbo.cp_orden_pago.IdTipo_Persona, dbo.cp_orden_pago.IdPersona, dbo.cp_orden_pago.IdEntidad, dbo.ct_cbtecble.cb_Fecha, dbo.tb_banco.ba_descripcion, 
                      dbo.ba_Banco_Cuenta.ba_descripcion AS NomCuenta, dbo.tb_banco_procesos_bancarios_x_empresa.NombreProceso, dbo.ba_Archivo_Transferencia.Fecha, 
                      dbo.ba_Archivo_Transferencia.Observacion
FROM         dbo.cp_proveedor_detalle AS opd RIGHT OUTER JOIN
                      dbo.ba_Archivo_Transferencia INNER JOIN
                      dbo.ba_Archivo_Transferencia_Det INNER JOIN
                      dbo.cp_orden_pago_det ON dbo.ba_Archivo_Transferencia_Det.IdEmpresa_OP = dbo.cp_orden_pago_det.IdEmpresa AND 
                      dbo.ba_Archivo_Transferencia_Det.IdOrdenPago = dbo.cp_orden_pago_det.IdOrdenPago AND 
                      dbo.ba_Archivo_Transferencia_Det.Secuencia_OP = dbo.cp_orden_pago_det.Secuencia INNER JOIN
                      dbo.cp_orden_pago ON dbo.cp_orden_pago_det.IdEmpresa = dbo.cp_orden_pago.IdEmpresa AND 
                      dbo.cp_orden_pago_det.IdOrdenPago = dbo.cp_orden_pago.IdOrdenPago INNER JOIN
                      dbo.tb_persona INNER JOIN
                      dbo.cp_proveedor ON dbo.tb_persona.IdPersona = dbo.cp_proveedor.IdPersona ON dbo.cp_orden_pago.IdPersona = dbo.cp_proveedor.IdPersona AND 
                      dbo.cp_orden_pago.IdEntidad = dbo.cp_proveedor.IdProveedor AND dbo.cp_orden_pago.IdEmpresa = dbo.cp_proveedor.IdEmpresa ON 
                      dbo.ba_Archivo_Transferencia.IdEmpresa = dbo.ba_Archivo_Transferencia_Det.IdEmpresa AND 
                      dbo.ba_Archivo_Transferencia.IdArchivo = dbo.ba_Archivo_Transferencia_Det.IdArchivo INNER JOIN
                      dbo.ct_cbtecble ON dbo.cp_orden_pago_det.IdEmpresa_cxp = dbo.ct_cbtecble.IdEmpresa AND 
                      dbo.cp_orden_pago_det.IdTipoCbte_cxp = dbo.ct_cbtecble.IdTipoCbte AND dbo.cp_orden_pago_det.IdCbteCble_cxp = dbo.ct_cbtecble.IdCbteCble INNER JOIN
                      dbo.ba_Banco_Cuenta ON dbo.ba_Archivo_Transferencia.IdEmpresa = dbo.ba_Banco_Cuenta.IdEmpresa AND 
                      dbo.ba_Archivo_Transferencia.IdBanco = dbo.ba_Banco_Cuenta.IdBanco INNER JOIN
                      dbo.tb_banco_procesos_bancarios_x_empresa ON dbo.ba_Archivo_Transferencia.IdEmpresa = dbo.tb_banco_procesos_bancarios_x_empresa.IdEmpresa AND 
                      dbo.ba_Archivo_Transferencia.IdProceso_bancario = dbo.tb_banco_procesos_bancarios_x_empresa.IdProceso LEFT OUTER JOIN
                      dbo.tb_banco ON dbo.cp_proveedor.IdBanco_acreditacion = dbo.tb_banco.IdBanco ON opd.IdEmpresa = dbo.cp_orden_pago.IdEmpresa AND 
                      opd.IdProveedor = dbo.cp_orden_pago.IdEntidad AND opd.Secuencia = dbo.cp_orden_pago.SecuenciaProveedor LEFT OUTER JOIN
                      dbo.cp_proveedor_detalle RIGHT OUTER JOIN
                      dbo.cp_orden_giro ON dbo.cp_proveedor_detalle.IdEmpresa = dbo.cp_orden_giro.IdEmpresa AND 
                      dbo.cp_proveedor_detalle.IdProveedor = dbo.cp_orden_giro.IdProveedor AND dbo.cp_proveedor_detalle.Secuencia = dbo.cp_orden_giro.SecuenciaProveedor ON 
                      dbo.cp_orden_pago_det.IdEmpresa_cxp = dbo.cp_orden_giro.IdEmpresa AND dbo.cp_orden_pago_det.IdTipoCbte_cxp = dbo.cp_orden_giro.IdTipoCbte_Ogiro AND 
                      dbo.cp_orden_pago_det.IdCbteCble_cxp = dbo.cp_orden_giro.IdCbteCble_Ogiro
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWBAN_010';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'ight = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_banco"
            Begin Extent = 
               Top = 1030
               Left = 373
               Bottom = 1193
               Right = 648
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct_cbtecble"
            Begin Extent = 
               Top = 1236
               Left = 452
               Bottom = 1399
               Right = 664
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
         Begin Table = "cp_proveedor_detalle"
            Begin Extent = 
               Top = 40
               Left = 326
               Bottom = 148
               Right = 538
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_giro"
            Begin Extent = 
               Top = 192
               Left = 433
               Bottom = 300
               Right = 668
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
         Configuration = "(H (1[85] 4[4] 3[4] 2) )"
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
         Top = -960
         Left = 0
      End
      Begin Tables = 
         Begin Table = "opd"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 250
            End
            DisplayFlags = 280
            TopColumn = 0
         End
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
               Top = 338
               Left = 213
               Bottom = 501
               Right = 455
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
               Top = 663
               Left = 341
               Bottom = 826
               Right = 615
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_proveedor"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1111
               R', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWBAN_010';



