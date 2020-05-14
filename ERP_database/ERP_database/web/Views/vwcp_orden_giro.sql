﻿CREATE VIEW [web].[vwcp_orden_giro]
AS
SELECT dbo.cp_orden_giro.IdEmpresa, dbo.cp_orden_giro.IdCbteCble_Ogiro, dbo.cp_orden_giro.IdTipoCbte_Ogiro, dbo.cp_orden_giro.IdOrden_giro_Tipo, dbo.cp_orden_giro.IdProveedor, dbo.cp_orden_giro.co_fechaOg, 
                  dbo.cp_orden_giro.co_serie, dbo.cp_orden_giro.co_factura, dbo.cp_orden_giro.co_FechaFactura, dbo.cp_orden_giro.co_FechaContabilizacion, dbo.cp_orden_giro.co_FechaFactura_vct, dbo.cp_orden_giro.co_plazo, 
                  dbo.cp_orden_giro.co_observacion, dbo.cp_orden_giro.co_subtotal_iva, dbo.cp_orden_giro.co_subtotal_siniva, dbo.cp_orden_giro.co_baseImponible, dbo.cp_orden_giro.co_Por_iva, dbo.cp_orden_giro.co_valoriva, 
                  dbo.cp_orden_giro.IdCod_ICE, dbo.cp_orden_giro.co_total, dbo.cp_orden_giro.co_vaCoa, dbo.cp_orden_giro.IdIden_credito, dbo.cp_orden_giro.IdCod_101, dbo.cp_orden_giro.co_valorpagar, NULL AS IdTipoFlujo, 
                  dbo.cp_orden_giro.IdTipoServicio, dbo.cp_orden_giro.Estado, dbo.cp_orden_giro.IdSucursal, dbo.cp_orden_giro.PagoLocExt, dbo.cp_orden_giro.PaisPago, dbo.cp_orden_giro.ConvenioTributacion, 
                  dbo.cp_orden_giro.PagoSujetoRetencion, dbo.cp_orden_giro.BseImpNoObjDeIva, dbo.cp_orden_giro.fecha_autorizacion, dbo.cp_orden_giro.Num_Autorizacion, dbo.cp_orden_giro.Num_Autorizacion_Imprenta, 
                  dbo.cp_orden_giro.cp_es_comprobante_electronico, dbo.cp_orden_giro.Tipodoc_a_Modificar, dbo.cp_orden_giro.estable_a_Modificar, dbo.cp_orden_giro.ptoEmi_a_Modificar, dbo.cp_orden_giro.num_docu_Modificar, 
                  dbo.cp_orden_giro.aut_doc_Modificar, dbo.cp_orden_giro.IdTipoMovi, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_razonSocial, dbo.tb_persona.pe_nombreCompleto, dbo.tb_persona.pe_cedulaRuc, 
                  dbo.cp_TipoDocumento.Descripcion, dbo.cp_TipoDocumento.ManejaTalonario, dbo.cp_TipoDocumento.FechaInicioTalonario, dbo.BankersRounding(cp_orden_giro.co_total - isnull(can.MontoAplicado,0) - ISNULL(RET.re_valor_retencion,0),0) AS Saldo,
				  case when dbo.BankersRounding(cp_orden_giro.co_total - isnull(can.MontoAplicado,0) - ISNULL(RET.re_valor_retencion,0),0) <= 0 THEN 'CANCELADO' ELSE 'PENDIENTE' END AS EstadoCancelacion
FROM     dbo.cp_orden_giro INNER JOIN
                  dbo.cp_proveedor ON dbo.cp_orden_giro.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.cp_orden_giro.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                  dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.cp_TipoDocumento ON dbo.cp_orden_giro.IdOrden_giro_Tipo = dbo.cp_TipoDocumento.CodTipoDocumento left join
				  (
					  select f.IdEmpresa_cxp, f.IdTipoCbte_cxp, f.IdCbteCble_cxp, sum(f.MontoAplicado) MontoAplicado 
					  from cp_orden_pago_cancelaciones as f
					  group by f.IdEmpresa_cxp, f.IdTipoCbte_cxp, f.IdCbteCble_cxp				  
				  ) as can on cp_orden_giro.IdEmpresa= can.IdEmpresa_cxp and cp_orden_giro.IdTipoCbte_Ogiro = can.IdTipoCbte_cxp and cp_orden_giro.IdCbteCble_Ogiro = can.IdCbteCble_cxp left join
				  (
					select a.IdEmpresa_Ogiro, a.IdTipoCbte_Ogiro, a.IdCbteCble_Ogiro, sum(b.re_valor_retencion) re_valor_retencion
					from cp_retencion as a inner join
					cp_retencion_det as b on a.IdEmpresa = b.IdEmpresa and a.IdRetencion = b.IdRetencion
					where a.Estado = 'A'
					group by a.IdEmpresa_Ogiro, a.IdTipoCbte_Ogiro, a.IdCbteCble_Ogiro
				  ) as ret on cp_orden_giro.IdEmpresa = ret.IdEmpresa_Ogiro and cp_orden_giro.IdTipoCbte_Ogiro = ret.IdTipoCbte_Ogiro and cp_orden_giro.IdCbteCble_Ogiro = ret.IdCbteCble_Ogiro

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[86] 4[3] 2[3] 3) )"
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
         Begin Table = "cp_orden_giro"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 289
               Right = 297
            End
            DisplayFlags = 280
            TopColumn = 46
         End
         Begin Table = "cp_proveedor"
            Begin Extent = 
               Top = 239
               Left = 433
               Bottom = 551
               Right = 665
            End
            DisplayFlags = 280
            TopColumn = 17
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 183
               Left = 725
               Bottom = 467
               Right = 957
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_TipoDocumento"
            Begin Extent = 
               Top = 0
               Left = 484
               Bottom = 225
               Right = 741
            End
            DisplayFlags = 280
            TopColumn = 14
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 67
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
         Width = 1500
         Width = 1500
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcp_orden_giro';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcp_orden_giro';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcp_orden_giro';

