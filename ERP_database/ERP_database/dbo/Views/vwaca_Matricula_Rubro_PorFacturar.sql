CREATE VIEW dbo.vwaca_Matricula_Rubro_PorFacturar
AS
SELECT dbo.aca_Matricula_Rubro.IdEmpresa, dbo.aca_Matricula_Rubro.IdMatricula, dbo.aca_Matricula_Rubro.IdPeriodo, dbo.aca_Matricula_Rubro.IdRubro, dbo.aca_Matricula_Rubro.IdMecanismo, dbo.aca_Matricula_Rubro.IdProducto, 
                  dbo.aca_Matricula_Rubro.EnMatricula, CAST(dbo.aca_Matricula_Rubro.Subtotal AS float) AS Subtotal, dbo.aca_Matricula_Rubro.IdCod_Impuesto_Iva, CAST(dbo.aca_Matricula_Rubro.Porcentaje AS float) AS Porcentaje, 
                  CAST(dbo.aca_Matricula_Rubro.ValorIVA AS float) AS ValorIVA, CAST(dbo.aca_Matricula_Rubro.Total AS float) AS Total, dbo.aca_Matricula_Rubro.IdSucursal, dbo.aca_Matricula_Rubro.IdBodega, dbo.aca_Matricula_Rubro.IdCbteVta, 
                  dbo.aca_Matricula_Rubro.FechaFacturacion, dbo.aca_Matricula.IdAnio, dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdAlumno, dbo.aca_AnioLectivo_Rubro.AplicaProntoPago, dbo.aca_AnioLectivo_Rubro.NomRubro, 
                  dbo.in_Producto.pr_descripcion
FROM     dbo.aca_Matricula INNER JOIN
                  dbo.aca_Matricula_Rubro ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Matricula_Rubro.IdEmpresa AND dbo.aca_Matricula.IdMatricula = dbo.aca_Matricula_Rubro.IdMatricula INNER JOIN
                  dbo.aca_AnioLectivo_Rubro ON dbo.aca_Matricula.IdAnio = dbo.aca_AnioLectivo_Rubro.IdAnio AND dbo.aca_Matricula.IdEmpresa = dbo.aca_AnioLectivo_Rubro.IdEmpresa AND 
                  dbo.aca_Matricula_Rubro.IdRubro = dbo.aca_AnioLectivo_Rubro.IdRubro INNER JOIN
                  dbo.in_Producto ON dbo.aca_AnioLectivo_Rubro.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.aca_AnioLectivo_Rubro.IdProducto = dbo.in_Producto.IdProducto
WHERE  (dbo.aca_Matricula_Rubro.FechaFacturacion IS NULL)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula_Rubro_PorFacturar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'teriaPane = 
      Begin ColumnWidths = 11
         Column = 4704
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula_Rubro_PorFacturar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[30] 4[40] 2[13] 3) )"
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
         Begin Table = "aca_AnioLectivo_Rubro"
            Begin Extent = 
               Top = 0
               Left = 801
               Bottom = 327
               Right = 1045
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Matricula_Rubro"
            Begin Extent = 
               Top = 0
               Left = 231
               Bottom = 396
               Right = 475
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "aca_Matricula"
            Begin Extent = 
               Top = 71
               Left = 531
               Bottom = 306
               Right = 776
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 0
               Left = 1096
               Bottom = 379
               Right = 1371
            End
            DisplayFlags = 280
            TopColumn = 18
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 23
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
   Begin Cri', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula_Rubro_PorFacturar';

