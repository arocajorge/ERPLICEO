CREATE VIEW dbo.vwfa_guia_remision_det
AS
SELECT        dbo.fa_guia_remision_det.IdEmpresa, dbo.fa_guia_remision_det.IdSucursal, dbo.fa_guia_remision_det.IdBodega, dbo.fa_guia_remision_det.IdGuiaRemision, dbo.fa_guia_remision_det.Secuencia, 
                         dbo.fa_guia_remision_det.IdProducto, dbo.fa_guia_remision_det.gi_cantidad, dbo.fa_guia_remision_det.gi_detallexItems, dbo.in_Producto.pr_codigo, dbo.in_Producto.pr_descripcion, dbo.in_Producto.pr_descripcion_2, 
                         dbo.fa_guia_remision_det.gi_precio, dbo.fa_guia_remision_det.gi_por_desc, dbo.fa_guia_remision_det.gi_descuentoUni, dbo.fa_guia_remision_det.gi_PrecioFinal, dbo.fa_guia_remision_det.gi_Subtotal, 
                         dbo.fa_guia_remision_det.IdCod_Impuesto, dbo.fa_guia_remision_det.gi_por_iva, dbo.fa_guia_remision_det.gi_Total, dbo.fa_guia_remision_det.gi_Iva
FROM            dbo.fa_guia_remision_det INNER JOIN
                         dbo.in_Producto ON dbo.fa_guia_remision_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.fa_guia_remision_det.IdProducto = dbo.in_Producto.IdProducto

GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_guia_remision_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[50] 4[12] 2[21] 3) )"
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
         Begin Table = "fa_guia_remision_det"
            Begin Extent = 
               Top = 0
               Left = 456
               Bottom = 258
               Right = 633
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 272
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
      Begin ColumnWidths = 9
         Width = 284
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_guia_remision_det';

