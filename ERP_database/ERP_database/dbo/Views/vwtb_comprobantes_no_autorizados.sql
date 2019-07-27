CREATE VIEW [web].[vwtb_comprobantes_no_autorizados]
AS
SELECT doc.IdEmpresa, 'FACT' Tipo_documento, doc.IdCbteVta, doc.vt_serie1, doc.vt_serie2, doc.vt_NumFactura DocumentoDoc, doc.vt_serie1 + '-' + doc.vt_serie2 + '-' + doc.vt_NumFactura Documento, doc.vt_fecha, per.pe_nombreCompleto, 
                  doc.vt_Observacion, doc.IdSucursal
FROM     dbo.tb_persona AS per INNER JOIN
                  dbo.fa_cliente AS cont ON per.IdPersona = cont.IdPersona INNER JOIN
                  dbo.fa_factura AS doc ON cont.IdEmpresa = doc.IdEmpresa AND cont.IdCliente = doc.IdCliente AND doc.Estado = 'A' AND 
                  doc.aprobada_enviar_sri = 0
/*where 'RAP'+'-'+'FAC'+'-'+doc.vt_serie1+'-'+doc.vt_serie2+'-'+doc.vt_NumFactura NOT IN (SELEct id_registro from EntidadRegulatoria.fa_elec_registros_generados)*/ UNION
SELECT doc.IdEmpresa, 'RETEN', doc.IdRetencion, doc.serie1, doc.serie2, doc.NumRetencion Documento, doc.serie1 + '-' + doc.serie2 + '-' + doc.NumRetencion, doc.fecha, per.pe_nombreCompleto, doc.observacion,
fp.IdSucursal
FROM     dbo.tb_persona AS per INNER JOIN
                  dbo.cp_proveedor AS cont ON per.IdPersona = cont.IdPersona INNER JOIN
                  dbo.cp_orden_giro AS fp ON cont.IdEmpresa = fp.IdEmpresa AND cont.IdProveedor = fp.IdProveedor INNER JOIN
                  dbo.cp_retencion AS doc ON fp.IdEmpresa = doc.IdEmpresa_Ogiro AND fp.IdCbteCble_Ogiro = doc.IdCbteCble_Ogiro AND fp.IdTipoCbte_Ogiro = doc.IdTipoCbte_Ogiro AND doc.Estado = 'A' AND doc.aprobada_enviar_sri = 0 AND 
                  doc.NumRetencion IS NOT NULL
/* where 'RAP'+'-'+'RET'+'-'+doc.serie1+'-'+doc.serie2+'-'+doc.NumRetencion NOT IN (SELEct id_registro from EntidadRegulatoria.fa_elec_registros_generados)*/ UNION
SELECT cont.IdEmpresa, 'NTCR', doc.IdNota, doc.Serie1, doc.Serie2, doc.NumNota_Impresa Documento, doc.Serie1 + '-' + doc.Serie2 + '-' + doc.NumNota_Impresa, doc.no_fecha, per.pe_nombreCompleto, doc.sc_observacion,
doc.IdSucursal
FROM     dbo.tb_persona AS per INNER JOIN
                  dbo.fa_cliente AS cont ON per.IdPersona = cont.IdPersona INNER JOIN
                  dbo.fa_notaCreDeb AS doc ON cont.IdEmpresa = doc.IdEmpresa AND cont.IdCliente = doc.IdCliente AND doc.Estado = 'A' AND doc.aprobada_enviar_sri = 0 AND 
                  doc.NaturalezaNota = 'SRI'
/*where 'RAP'+'-'+'NTC'+'-'+doc.Serie1+'-'+doc.Serie2+'-'+doc.NumNota_Impresa NOT IN (SELEct id_registro from EntidadRegulatoria.fa_elec_registros_generados)*/ UNION
SELECT doc.IdEmpresa, 'GUIA', doc.IdGuiaRemision, doc.Serie1, doc.Serie2, doc.NumGuia_Preimpresa Documento, doc.Serie1 + '-' + doc.Serie2 + '-' + doc.NumGuia_Preimpresa Documento, doc.gi_fecha, per.pe_nombreCompleto, 
                  doc.gi_Observacion, doc.IdSucursal
FROM     dbo.tb_persona AS per INNER JOIN
                  dbo.fa_cliente AS cont ON per.IdPersona = cont.IdPersona INNER JOIN
                  dbo.fa_guia_remision AS doc ON cont.IdEmpresa = doc.IdEmpresa AND cont.IdCliente = doc.IdCliente AND doc.Estado = 'A' AND doc.aprobada_enviar_sri = 0
						 
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwtb_comprobantes_no_autorizados';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[6] 4[5] 2[80] 3) )"
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
      Begin ColumnWidths = 11
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwtb_comprobantes_no_autorizados';

