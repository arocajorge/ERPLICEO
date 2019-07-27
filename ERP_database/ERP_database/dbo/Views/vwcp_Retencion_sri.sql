CREATE VIEW [dbo].[vwcp_Retencion_sri]
AS
SELECT        dbo.cp_retencion.IdEmpresa, dbo.cp_retencion.IdRetencion, dbo.cp_retencion.serie1 +'-' + cp_retencion.serie2 as serie , dbo.cp_retencion.NumRetencion, dbo.cp_retencion.fecha, 
                         dbo.tb_persona.pe_nombreCompleto, dbo.tb_persona.pe_razonSocial, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_correo, dbo.tb_persona.pe_direccion, 
                         dbo.tb_persona.pe_telfono_Contacto, dbo.cp_proveedor.IdProveedor, dbo.cp_orden_giro.co_serie, dbo.cp_orden_giro.co_factura, 
                         dbo.cp_orden_giro.co_FechaFactura, dbo.cp_orden_giro.IdOrden_giro_Tipo, dbo.tb_persona.IdTipoDocumento, dbo.cp_orden_giro.IdSucursal, 
                         dbo.tb_sucursal.Su_Descripcion, dbo.tb_sucursal.Su_Direccion, dbo.tb_empresa.RazonSocial, dbo.tb_empresa.NombreComercial, 
                         dbo.tb_empresa.ContribuyenteEspecial,  'SI'ObligadoAllevarConta, dbo.tb_empresa.em_ruc, dbo.tb_empresa.em_direccion, 
                         dbo.cp_retencion.Estado
FROM            dbo.cp_proveedor INNER JOIN
                         dbo.cp_retencion ON dbo.cp_proveedor.IdEmpresa = dbo.cp_retencion.IdEmpresa INNER JOIN
                         dbo.cp_orden_giro ON dbo.cp_proveedor.IdEmpresa = dbo.cp_orden_giro.IdEmpresa AND dbo.cp_proveedor.IdProveedor = dbo.cp_orden_giro.IdProveedor AND 
                         dbo.cp_retencion.IdEmpresa_Ogiro = dbo.cp_orden_giro.IdEmpresa AND dbo.cp_retencion.IdCbteCble_Ogiro = dbo.cp_orden_giro.IdCbteCble_Ogiro AND 
                         dbo.cp_retencion.IdTipoCbte_Ogiro = dbo.cp_orden_giro.IdTipoCbte_Ogiro INNER JOIN
                         dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.tb_sucursal ON dbo.cp_orden_giro.IdSucursal = dbo.tb_sucursal.IdSucursal AND dbo.cp_retencion.IdEmpresa = dbo.tb_sucursal.IdEmpresa INNER JOIN
                         dbo.tb_empresa ON dbo.cp_retencion.IdEmpresa = dbo.tb_empresa.IdEmpresa
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[86] 4[4] 3[4] 2) )"
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
               Top = 0
               Left = 28
               Bottom = 68
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_retencion"
            Begin Extent = 
               Top = 0
               Left = 322
               Bottom = 152
               Right = 531
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_orden_giro"
            Begin Extent = 
               Top = 8
               Left = 644
               Bottom = 98
               Right = 885
            End
            DisplayFlags = 280
            TopColumn = 41
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 122
               Left = 18
               Bottom = 190
               Right = 227
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 227
               Left = 643
               Bottom = 290
               Right = 873
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "tb_empresa"
            Begin Extent = 
               Top = 177
               Left = 161
               Bottom = 376
               Right = 378
            End
            DisplayFlags = 280
            TopColumn = 11
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 25
         Width = 284
         Width = 1500
     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_Retencion_sri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'    Width = 1500
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
         Width = 1965
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_Retencion_sri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_Retencion_sri';

