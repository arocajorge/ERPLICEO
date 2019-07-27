CREATE VIEW [web].[VWCXP_009]
AS
SELECT dbo.cp_retencion_det.IdEmpresa, dbo.cp_retencion_det.IdRetencion, dbo.cp_retencion_det.Idsecuencia, dbo.cp_orden_giro.IdCbteCble_Ogiro, dbo.cp_orden_giro.IdTipoCbte_Ogiro, dbo.cp_orden_giro.IdOrden_giro_Tipo, 
                  dbo.cp_orden_giro.IdProveedor, per.pe_nombreCompleto AS nom_proveedor, per.pe_cedulaRuc AS ced_proveedor, pro.pr_direccion AS dir_proveedor, dbo.cp_orden_giro.co_fechaOg, dbo.cp_orden_giro.co_serie, 
                  dbo.cp_orden_giro.co_factura AS num_factura, dbo.cp_orden_giro.co_FechaFactura, dbo.cp_retencion.Estado, dbo.cp_TipoDocumento.Descripcion AS TipoDocumento, dbo.cp_retencion.fecha AS fecha_retencion, 
                  YEAR(dbo.cp_retencion.fecha) AS ejercicio_fiscal, dbo.cp_retencion_det.re_tipoRet AS Impuesto, dbo.cp_retencion_det.re_baseRetencion AS base_retencion, dbo.cp_retencion_det.IdCodigo_SRI, 
                  dbo.cp_codigo_SRI.codigoSRI AS cod_Impuesto_SRI, dbo.cp_codigo_SRI.co_porRetencion AS por_Retencion_SRI, dbo.cp_retencion_det.re_valor_retencion AS valor_Retenido, dbo.cp_retencion.IdEmpresa_Ogiro, 
                  dbo.cp_retencion.serie1 + '' + dbo.cp_retencion.serie2 AS serie, dbo.cp_retencion.NumRetencion, dbo.cp_codigo_SRI.co_descripcion, dbo.cp_codigo_SRI_x_CtaCble.IdCtaCble, 
                  dbo.cp_retencion_x_ct_cbtecble.ct_IdCbteCble AS IdCbteCbleRet, ISNULL(dbo.cp_orden_giro.co_observacion, dbo.cp_retencion.observacion) AS co_observacion, dbo.ct_cbtecble.IdSucursal,
				  su.Su_Descripcion
FROM     dbo.cp_retencion_det INNER JOIN
                  dbo.cp_retencion ON dbo.cp_retencion_det.IdEmpresa = dbo.cp_retencion.IdEmpresa AND dbo.cp_retencion_det.IdRetencion = dbo.cp_retencion.IdRetencion INNER JOIN
                  dbo.cp_codigo_SRI ON dbo.cp_retencion_det.IdCodigo_SRI = dbo.cp_codigo_SRI.IdCodigo_SRI LEFT OUTER JOIN
                  dbo.cp_proveedor AS pro INNER JOIN
                  dbo.cp_orden_giro INNER JOIN
                  dbo.cp_TipoDocumento ON dbo.cp_orden_giro.IdOrden_giro_Tipo = dbo.cp_TipoDocumento.CodTipoDocumento ON pro.IdEmpresa = dbo.cp_orden_giro.IdEmpresa AND pro.IdProveedor = dbo.cp_orden_giro.IdProveedor INNER JOIN
                  dbo.tb_persona AS per ON pro.IdPersona = per.IdPersona ON dbo.cp_retencion.IdCbteCble_Ogiro = dbo.cp_orden_giro.IdCbteCble_Ogiro AND dbo.cp_retencion.IdTipoCbte_Ogiro = dbo.cp_orden_giro.IdTipoCbte_Ogiro AND 
                  dbo.cp_retencion.IdEmpresa_Ogiro = dbo.cp_orden_giro.IdEmpresa LEFT OUTER JOIN
                  dbo.cp_retencion_x_ct_cbtecble LEFT OUTER JOIN
                  dbo.ct_cbtecble ON dbo.cp_retencion_x_ct_cbtecble.ct_IdEmpresa = dbo.ct_cbtecble.IdEmpresa AND dbo.cp_retencion_x_ct_cbtecble.ct_IdTipoCbte = dbo.ct_cbtecble.IdTipoCbte AND 
                  dbo.cp_retencion_x_ct_cbtecble.ct_IdCbteCble = dbo.ct_cbtecble.IdCbteCble ON dbo.cp_retencion.IdEmpresa = dbo.cp_retencion_x_ct_cbtecble.rt_IdEmpresa AND 
                  dbo.cp_retencion.IdRetencion = dbo.cp_retencion_x_ct_cbtecble.rt_IdRetencion LEFT OUTER JOIN
                  dbo.cp_codigo_SRI_x_CtaCble ON dbo.cp_retencion_det.IdEmpresa = dbo.cp_codigo_SRI_x_CtaCble.IdEmpresa AND dbo.cp_retencion_det.IdCodigo_SRI = dbo.cp_codigo_SRI_x_CtaCble.idCodigo_SRI
				  left outer join tb_sucursal as su on ct_cbtecble.IdEmpresa = su.IdEmpresa and ct_cbtecble.IdSucursal = su.IdSucursal
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCXP_009';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'  Right = 242
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCXP_009';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[11] 4[50] 2[20] 3) )"
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
               Top = 7
               Left = 48
               Bottom = 170
               Right = 355
            End
            DisplayFlags = 280
            TopColumn = 38
         End
         Begin Table = "vwcp_ProveedorRuc"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_TipoDocumento"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 366
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_retencion"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 278
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_retencion_det"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 279
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_codigo_SRI"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 268
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_codigo_SRI_x_CtaCble"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
             ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCXP_009';

