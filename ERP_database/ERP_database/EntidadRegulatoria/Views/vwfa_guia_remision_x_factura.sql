CREATE VIEW EntidadRegulatoria.vwfa_guia_remision_x_factura
AS
SELECT        dbo.fa_guia_remision.IdEmpresa, dbo.fa_guia_remision.IdSucursal, dbo.fa_guia_remision.IdBodega, dbo.fa_guia_remision.IdGuiaRemision, dbo.fa_factura.vt_tipoDoc, dbo.fa_factura.vt_serie1, dbo.fa_factura.vt_serie2, 
                         dbo.fa_factura.vt_NumFactura, dbo.fa_factura.Fecha_Autorizacion, dbo.fa_factura.vt_autorizacion
FROM            dbo.fa_factura_x_fa_guia_remision INNER JOIN
                         dbo.fa_guia_remision ON dbo.fa_factura_x_fa_guia_remision.gi_IdEmpresa = dbo.fa_guia_remision.IdEmpresa AND dbo.fa_factura_x_fa_guia_remision.gi_IdSucursal = dbo.fa_guia_remision.IdSucursal AND 
                         dbo.fa_factura_x_fa_guia_remision.gi_IdBodega = dbo.fa_guia_remision.IdBodega AND dbo.fa_factura_x_fa_guia_remision.gi_IdGuiaRemision = dbo.fa_guia_remision.IdGuiaRemision INNER JOIN
                         dbo.fa_factura ON dbo.fa_factura_x_fa_guia_remision.fa_IdEmpresa = dbo.fa_factura.IdEmpresa AND dbo.fa_factura_x_fa_guia_remision.fa_IdSucursal = dbo.fa_factura.IdSucursal AND 
                         dbo.fa_factura_x_fa_guia_remision.fa_IdBodega = dbo.fa_factura.IdBodega AND dbo.fa_factura_x_fa_guia_remision.fa_IdCbteVta = dbo.fa_factura.IdCbteVta
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'EntidadRegulatoria', @level1type = N'VIEW', @level1name = N'vwfa_guia_remision_x_factura';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[78] 4[5] 2[5] 3) )"
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
         Begin Table = "fa_factura_x_fa_guia_remision"
            Begin Extent = 
               Top = 0
               Left = 304
               Bottom = 287
               Right = 525
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_guia_remision"
            Begin Extent = 
               Top = 0
               Left = 742
               Bottom = 379
               Right = 978
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_factura"
            Begin Extent = 
               Top = 8
               Left = 0
               Bottom = 384
               Right = 199
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
', @level0type = N'SCHEMA', @level0name = N'EntidadRegulatoria', @level1type = N'VIEW', @level1name = N'vwfa_guia_remision_x_factura';

