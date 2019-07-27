CREATE VIEW EntidadRegulatoria.vwcp_retencion
AS
SELECT        dbo.cp_retencion.IdEmpresa, dbo.cp_retencion.IdRetencion, dbo.cp_retencion.CodDocumentoTipo, dbo.cp_retencion.serie1, dbo.cp_retencion.serie2, dbo.cp_retencion.NumRetencion, dbo.cp_retencion.NAutorizacion, 
                         dbo.cp_retencion.Fecha_Autorizacion, dbo.cp_retencion.fecha, dbo.cp_retencion.observacion, dbo.cp_orden_giro.IdCbteCble_Ogiro, dbo.cp_orden_giro.IdTipoCbte_Ogiro, dbo.cp_orden_giro.co_fechaOg, 
                         dbo.cp_orden_giro.co_serie, dbo.cp_orden_giro.co_factura, dbo.cp_orden_giro.co_FechaFactura, dbo.cp_orden_giro.Num_Autorizacion, dbo.cp_orden_giro.Num_Autorizacion_Imprenta, dbo.tb_persona.pe_Naturaleza, 
                         dbo.tb_persona.IdTipoDocumento, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_nombreCompleto, dbo.tb_persona.pe_direccion, dbo.tb_persona.pe_telfono_Contacto, dbo.tb_persona.pe_celular, 
                         dbo.cp_proveedor.pr_correo AS pe_correo, dbo.tb_persona.pe_razonSocial, dbo.tb_empresa.em_nombre, dbo.tb_empresa.RazonSocial, dbo.tb_empresa.NombreComercial, dbo.tb_empresa.ContribuyenteEspecial, 
                         'SI' AS ObligadoAllevarConta, dbo.tb_empresa.em_ruc, dbo.tb_empresa.em_direccion, dbo.tb_empresa.em_telefonos, dbo.tb_empresa.em_Email, dbo.cp_orden_giro.IdOrden_giro_Tipo, 
                         dbo.tb_sis_Documento_Tipo_Talonario.es_Documento_Electronico
FROM            dbo.cp_proveedor INNER JOIN
                         dbo.cp_orden_giro ON dbo.cp_proveedor.IdEmpresa = dbo.cp_orden_giro.IdEmpresa AND dbo.cp_proveedor.IdProveedor = dbo.cp_orden_giro.IdProveedor INNER JOIN
                         dbo.cp_retencion ON dbo.cp_orden_giro.IdEmpresa = dbo.cp_retencion.IdEmpresa_Ogiro AND dbo.cp_orden_giro.IdCbteCble_Ogiro = dbo.cp_retencion.IdCbteCble_Ogiro AND 
                         dbo.cp_orden_giro.IdTipoCbte_Ogiro = dbo.cp_retencion.IdTipoCbte_Ogiro INNER JOIN
                         dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.tb_empresa ON dbo.cp_proveedor.IdEmpresa = dbo.tb_empresa.IdEmpresa AND dbo.cp_retencion.Estado = 'A' AND dbo.cp_retencion.aprobada_enviar_sri = 1 INNER JOIN
                         dbo.tb_sis_Documento_Tipo_Talonario ON dbo.cp_retencion.IdEmpresa = dbo.tb_sis_Documento_Tipo_Talonario.IdEmpresa AND 
                         dbo.cp_retencion.CodDocumentoTipo = dbo.tb_sis_Documento_Tipo_Talonario.CodDocumentoTipo AND dbo.cp_retencion.serie2 = dbo.tb_sis_Documento_Tipo_Talonario.PuntoEmision AND 
                         dbo.cp_retencion.serie1 = dbo.tb_sis_Documento_Tipo_Talonario.Establecimiento AND dbo.cp_retencion.NumRetencion = dbo.tb_sis_Documento_Tipo_Talonario.NumDocumento
WHERE        (dbo.cp_retencion.NumRetencion IS NOT NULL) AND (dbo.cp_retencion.Fecha_Autorizacion IS NULL) AND (dbo.tb_sis_Documento_Tipo_Talonario.es_Documento_Electronico = 1)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'EntidadRegulatoria', @level1type = N'VIEW', @level1name = N'vwcp_retencion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'      Width = 1500
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
', @level0type = N'SCHEMA', @level0name = N'EntidadRegulatoria', @level1type = N'VIEW', @level1name = N'vwcp_retencion';












GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[8] 4[24] 2[8] 3) )"
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
         Begin Table = "cp_proveedor"
            Begin Extent = 
               Top = 78
               Left = 977
               Bottom = 208
               Right = 1209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_giro"
            Begin Extent = 
               Top = 52
               Left = 205
               Bottom = 475
               Right = 464
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_retencion"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 439
               Right = 236
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 473
               Left = 722
               Bottom = 932
               Right = 954
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_empresa"
            Begin Extent = 
               Top = 145
               Left = 1159
               Bottom = 595
               Right = 1378
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "tb_sis_Documento_Tipo_Talonario"
            Begin Extent = 
               Top = 23
               Left = 598
               Bottom = 299
               Right = 830
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
      Begin ColumnWidths = 39
         Width = 284
   ', @level0type = N'SCHEMA', @level0name = N'EntidadRegulatoria', @level1type = N'VIEW', @level1name = N'vwcp_retencion';











