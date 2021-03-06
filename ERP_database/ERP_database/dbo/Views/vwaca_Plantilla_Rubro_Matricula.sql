﻿CREATE VIEW [dbo].[vwaca_Plantilla_Rubro_Matricula]
AS
SELECT dbo.aca_Plantilla_Rubro.IdEmpresa, dbo.aca_Plantilla_Rubro.IdAnio, dbo.aca_Plantilla_Rubro.IdPlantilla, dbo.aca_Plantilla_Rubro.IdRubro, dbo.aca_AnioLectivo_Rubro.NomRubro, dbo.aca_AnioLectivo_Rubro_Periodo.IdPeriodo, 
                  dbo.aca_AnioLectivo_Periodo.FechaDesde, dbo.aca_AnioLectivo_Periodo.FechaHasta, dbo.aca_Plantilla_Rubro.IdProducto, dbo.aca_Plantilla_Rubro.Subtotal, dbo.aca_Plantilla_Rubro.IdCod_Impuesto_Iva, 
                  dbo.aca_Plantilla_Rubro.Porcentaje, dbo.aca_Plantilla_Rubro.ValorIVA, dbo.aca_Plantilla_Rubro.Total, dbo.in_Producto.pr_descripcion, dbo.aca_AnioLectivo_Rubro.AplicaProntoPago,
				  
				  CASE WHEN aca_AnioLectivo_Rubro.AplicaProntoPago = 1 AND dbo.aca_AnioLectivo_Periodo.FechaProntoPago > CAST(getdate() AS date) 
					THEN CAST(dbo.aca_Plantilla_Rubro.Subtotal AS float) - (CASE WHEN dbo.aca_Plantilla.AplicaParaTodo = 1 THEN 
						(CASE WHEN dbo.aca_Plantilla.TipoDescuento = '$' THEN CAST(dbo.aca_Plantilla.Valor AS FLOAT) ELSE ROUND(CAST(dbo.aca_Plantilla_Rubro.Subtotal AS float) * (dbo.aca_Plantilla.Valor / 100), 2) END) 
                  ELSE (CASE WHEN dbo.aca_Plantilla_Rubro.TipoDescuento_descuentoDet = '$' THEN CAST(dbo.aca_Plantilla_Rubro.Valor_descuentoDet AS FLOAT) ELSE ROUND(CAST(dbo.aca_Plantilla_Rubro.Subtotal AS float) * (dbo.aca_Plantilla_Rubro.Valor_descuentoDet / 100), 2) END) END) 
				  
				  ELSE CAST(dbo.aca_Plantilla_Rubro.Subtotal AS float) + CAST(dbo.aca_Plantilla_Rubro.ValorIVA AS FLOAT) 
                  END + dbo.aca_Plantilla_Rubro.ValorIVA 
				  
				  AS ValorProntoPago, aca_AnioLectivo_Periodo.FechaProntoPago
FROM     dbo.aca_AnioLectivo_Rubro_Periodo INNER JOIN
                  dbo.aca_Plantilla_Rubro ON dbo.aca_AnioLectivo_Rubro_Periodo.IdEmpresa = dbo.aca_Plantilla_Rubro.IdEmpresa AND dbo.aca_AnioLectivo_Rubro_Periodo.IdAnio = dbo.aca_Plantilla_Rubro.IdAnio AND 
                  dbo.aca_AnioLectivo_Rubro_Periodo.IdRubro = dbo.aca_Plantilla_Rubro.IdRubro INNER JOIN
                  dbo.aca_AnioLectivo_Periodo ON dbo.aca_AnioLectivo_Rubro_Periodo.IdEmpresa = dbo.aca_AnioLectivo_Periodo.IdEmpresa AND dbo.aca_AnioLectivo_Rubro_Periodo.IdPeriodo = dbo.aca_AnioLectivo_Periodo.IdPeriodo INNER JOIN
                  dbo.aca_AnioLectivo_Rubro ON dbo.aca_AnioLectivo_Rubro_Periodo.IdEmpresa = dbo.aca_AnioLectivo_Rubro.IdEmpresa AND dbo.aca_AnioLectivo_Rubro_Periodo.IdAnio = dbo.aca_AnioLectivo_Rubro.IdAnio AND 
                  dbo.aca_AnioLectivo_Rubro_Periodo.IdRubro = dbo.aca_AnioLectivo_Rubro.IdRubro INNER JOIN
                  dbo.in_Producto ON dbo.aca_Plantilla_Rubro.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.aca_Plantilla_Rubro.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                  dbo.aca_Plantilla ON dbo.aca_Plantilla_Rubro.IdEmpresa = dbo.aca_Plantilla.IdEmpresa AND dbo.aca_Plantilla_Rubro.IdAnio = dbo.aca_Plantilla.IdAnio AND dbo.aca_Plantilla_Rubro.IdPlantilla = dbo.aca_Plantilla.IdPlantilla
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Plantilla_Rubro_Matricula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'= 1200
         Width = 1200
         Width = 1056
         Width = 1488
         Width = 1608
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Plantilla_Rubro_Matricula';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[11] 2[15] 3) )"
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
         Begin Table = "aca_AnioLectivo_Rubro_Periodo"
            Begin Extent = 
               Top = 7
               Left = 678
               Bottom = 217
               Right = 922
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "aca_Plantilla_Rubro"
            Begin Extent = 
               Top = 7
               Left = 340
               Bottom = 306
               Right = 584
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Periodo"
            Begin Extent = 
               Top = 3
               Left = 988
               Bottom = 333
               Right = 1233
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Rubro"
            Begin Extent = 
               Top = 221
               Left = 641
               Bottom = 484
               Right = 885
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 129
               Left = 30
               Bottom = 414
               Right = 305
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
      Begin ColumnWidths = 19
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
         Width ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Plantilla_Rubro_Matricula';



