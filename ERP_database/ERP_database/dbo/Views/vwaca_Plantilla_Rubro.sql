CREATE VIEW dbo.vwaca_Plantilla_Rubro
AS
SELECT dbo.aca_Plantilla_Rubro.IdEmpresa, dbo.aca_Plantilla_Rubro.IdAnio, dbo.aca_Plantilla_Rubro.IdPlantilla, dbo.aca_Plantilla_Rubro.IdRubro, dbo.aca_AnioLectivo_Rubro.NomRubro, dbo.aca_Plantilla_Rubro.IdProducto, 
                  dbo.in_Producto.pr_descripcion, dbo.aca_Plantilla_Rubro.Subtotal, dbo.aca_Plantilla_Rubro.IdCod_Impuesto_Iva, dbo.aca_Plantilla_Rubro.Porcentaje, dbo.aca_Plantilla_Rubro.ValorIVA, dbo.aca_Plantilla_Rubro.Total, 
                  dbo.aca_Plantilla_Rubro.TipoDescuento_descuentoDet, dbo.aca_Plantilla_Rubro.Valor_descuentoDet, dbo.aca_Plantilla_Rubro.IdTipoNota_descuentoDet
FROM     dbo.aca_Plantilla_Rubro INNER JOIN
                  dbo.aca_AnioLectivo_Rubro ON dbo.aca_Plantilla_Rubro.IdEmpresa = dbo.aca_AnioLectivo_Rubro.IdEmpresa AND dbo.aca_Plantilla_Rubro.IdAnio = dbo.aca_AnioLectivo_Rubro.IdAnio AND 
                  dbo.aca_Plantilla_Rubro.IdRubro = dbo.aca_AnioLectivo_Rubro.IdRubro INNER JOIN
                  dbo.in_Producto ON dbo.aca_AnioLectivo_Rubro.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.aca_AnioLectivo_Rubro.IdProducto = dbo.in_Producto.IdProducto
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Plantilla_Rubro';


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
         Begin Table = "aca_Plantilla_Rubro"
            Begin Extent = 
               Top = 17
               Left = 21
               Bottom = 306
               Right = 265
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "aca_AnioLectivo_Rubro"
            Begin Extent = 
               Top = 51
               Left = 383
               Bottom = 296
               Right = 627
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 53
               Left = 840
               Bottom = 268
               Right = 1115
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Plantilla_Rubro';



