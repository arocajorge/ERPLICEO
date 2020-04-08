/*order by a.IdEmpresa*/
CREATE VIEW dbo.vwcp_orden_giro_LiquidacionDeCompras
AS
SELECT a.IdEmpresa, a.IdTipoCbte_Ogiro, a.IdCbteCble_Ogiro, a.co_FechaFactura, a.co_factura, d.pe_nombreCompleto, a.Num_Autorizacion, b.Codigo, a.Estado, a.co_total, a.co_observacion, a.IdSucursal, b.Descripcion, 
                  a.fecha_autorizacion
FROM     dbo.cp_orden_giro AS a INNER JOIN
                  dbo.cp_TipoDocumento AS b ON a.IdOrden_giro_Tipo = b.CodTipoDocumento INNER JOIN
                  dbo.cp_proveedor AS c ON a.IdEmpresa = c.IdEmpresa AND a.IdProveedor = c.IdProveedor INNER JOIN
                  dbo.tb_persona AS d ON c.IdPersona = d.IdPersona INNER JOIN
                  dbo.tb_sis_Documento_Tipo_Talonario AS e ON a.IdEmpresa = e.IdEmpresa AND a.co_serie = e.Establecimiento + '-' + e.PuntoEmision AND e.NumDocumento = a.co_factura AND b.Codigo = e.CodDocumentoTipo
WHERE  (b.ManejaTalonario = 1) AND (e.es_Documento_Electronico = 1) AND (YEAR(a.co_FechaFactura) >= 2020)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_giro_LiquidacionDeCompras';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_giro_LiquidacionDeCompras';




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
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 239
               Right = 355
            End
            DisplayFlags = 280
            TopColumn = 38
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 664
               Left = 404
               Bottom = 975
               Right = 722
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 270
               Left = 992
               Bottom = 433
               Right = 1266
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "e"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 321
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_orden_giro_LiquidacionDeCompras';



