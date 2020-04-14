CREATE VIEW  [dbo].[vwaca_Matricula_Rubro_PorFacturar]
AS
SELECT dbo.aca_Matricula_Rubro.IdEmpresa, dbo.aca_Matricula_Rubro.IdMatricula, dbo.aca_Matricula_Rubro.IdPeriodo, dbo.aca_Matricula_Rubro.IdRubro, dbo.aca_Matricula_Rubro.IdMecanismo, dbo.aca_Plantilla_Rubro.IdProducto, 
                  dbo.aca_Matricula_Rubro.EnMatricula, CAST(dbo.aca_Plantilla_Rubro.Subtotal AS float) AS Subtotal, dbo.aca_Plantilla_Rubro.IdCod_Impuesto_Iva, CAST(dbo.aca_Plantilla_Rubro.Porcentaje AS float) AS Porcentaje, 
                  CAST(dbo.aca_Plantilla_Rubro.ValorIVA AS float) AS ValorIVA, CAST(dbo.aca_Plantilla_Rubro.Total AS float) AS Total, dbo.aca_Matricula_Rubro.IdSucursal, dbo.aca_Matricula_Rubro.IdBodega, dbo.aca_Matricula_Rubro.IdCbteVta, 
                  dbo.aca_Matricula_Rubro.FechaFacturacion, dbo.aca_Matricula.IdAnio, dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdAlumno, dbo.aca_AnioLectivo_Rubro.AplicaProntoPago, dbo.aca_AnioLectivo_Rubro.NomRubro, 
                  dbo.in_Producto.pr_descripcion, dbo.aca_AnioLectivo_Periodo.FechaDesde, CASE WHEN dbo.aca_AnioLectivo_Rubro.NumeroCuotas = 1 THEN '' ELSE CAST(dbo.aca_AnioLectivo_Rubro_Periodo.Secuencia AS varchar) 
                  + '/' + CAST(dbo.aca_AnioLectivo_Rubro.NumeroCuotas AS varchar) END AS DescripcionCuotas, CASE WHEN aca_AnioLectivo_Rubro.AplicaProntoPago = 1 AND dbo.aca_AnioLectivo_Periodo.FechaProntoPago > CAST(getdate() 
                  AS date) THEN CAST(dbo.aca_Plantilla_Rubro.Subtotal AS float) - (CASE WHEN dbo.aca_Plantilla.AplicaParaTodo = 1 THEN (CASE WHEN dbo.aca_Plantilla.TipoDescuento = '$' THEN CAST(dbo.aca_Plantilla.Valor AS FLOAT) 
                  ELSE ROUND(CAST(dbo.aca_Plantilla_Rubro.Subtotal AS float) * (dbo.aca_Plantilla.Valor / 100), 2) END) 
                  ELSE (CASE WHEN dbo.aca_Plantilla_Rubro.TipoDescuento_descuentoDet = '$' THEN CAST(dbo.aca_Plantilla_Rubro.Valor_descuentoDet AS FLOAT) ELSE ROUND(CAST(dbo.aca_Plantilla_Rubro.Subtotal AS float) 
                  * (dbo.aca_Plantilla_Rubro.Valor_descuentoDet / 100), 2) END) END) ELSE CAST(dbo.aca_Plantilla_Rubro.Subtotal AS float) + CAST(dbo.aca_Plantilla_Rubro.ValorIVA AS FLOAT) 
                  END + dbo.aca_Plantilla_Rubro.ValorIVA AS ValorProntoPago, dbo.aca_AnioLectivo_Periodo.FechaProntoPago, CAST(YEAR(dbo.aca_AnioLectivo_Periodo.FechaHasta) AS VARCHAR) + ' - ' + dbo.tb_mes.smes AS Periodo, 
                  dbo.aca_Plantilla.IdPlantilla
FROM     dbo.aca_Matricula INNER JOIN
                  dbo.aca_Matricula_Rubro ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Matricula_Rubro.IdEmpresa AND dbo.aca_Matricula.IdMatricula = dbo.aca_Matricula_Rubro.IdMatricula INNER JOIN
                  dbo.aca_AnioLectivo_Rubro ON dbo.aca_Matricula.IdAnio = dbo.aca_AnioLectivo_Rubro.IdAnio AND dbo.aca_Matricula.IdEmpresa = dbo.aca_AnioLectivo_Rubro.IdEmpresa AND 
                  dbo.aca_Matricula_Rubro.IdRubro = dbo.aca_AnioLectivo_Rubro.IdRubro INNER JOIN
                  dbo.in_Producto ON dbo.aca_AnioLectivo_Rubro.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.aca_AnioLectivo_Rubro.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                  dbo.aca_AnioLectivo_Periodo ON dbo.aca_Matricula_Rubro.IdEmpresa = dbo.aca_AnioLectivo_Periodo.IdEmpresa AND dbo.aca_Matricula_Rubro.IdPeriodo = dbo.aca_AnioLectivo_Periodo.IdPeriodo INNER JOIN
                  dbo.aca_AnioLectivo_Rubro_Periodo ON dbo.aca_AnioLectivo_Rubro.IdEmpresa = dbo.aca_AnioLectivo_Rubro_Periodo.IdEmpresa AND dbo.aca_AnioLectivo_Rubro.IdAnio = dbo.aca_AnioLectivo_Rubro_Periodo.IdAnio AND 
                  dbo.aca_AnioLectivo_Rubro.IdRubro = dbo.aca_AnioLectivo_Rubro_Periodo.IdRubro AND dbo.aca_AnioLectivo_Periodo.IdEmpresa = dbo.aca_AnioLectivo_Rubro_Periodo.IdEmpresa AND 
                  dbo.aca_AnioLectivo_Periodo.IdPeriodo = dbo.aca_AnioLectivo_Rubro_Periodo.IdPeriodo INNER JOIN
                  dbo.aca_Plantilla ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Plantilla.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.aca_Plantilla.IdAnio AND dbo.aca_Matricula.IdPlantilla = dbo.aca_Plantilla.IdPlantilla INNER JOIN
                  dbo.tb_mes ON dbo.aca_AnioLectivo_Periodo.IdMes = dbo.tb_mes.idMes INNER JOIN
                  dbo.aca_Plantilla_Rubro ON dbo.aca_Matricula_Rubro.IdEmpresa = dbo.aca_Plantilla_Rubro.IdEmpresa AND dbo.aca_Matricula_Rubro.IdAnio = dbo.aca_Plantilla_Rubro.IdAnio AND 
                  dbo.aca_Matricula_Rubro.IdPlantilla = dbo.aca_Plantilla_Rubro.IdPlantilla AND dbo.aca_Matricula_Rubro.IdRubro = dbo.aca_Plantilla_Rubro.IdRubro
WHERE  (dbo.aca_Matricula_Rubro.FechaFacturacion IS NULL)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula_Rubro_PorFacturar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'6
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
         Width = 1200
         Width = 1200
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 4710
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula_Rubro_PorFacturar';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[64] 4[9] 2[8] 3) )"
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
         Begin Table = "aca_Matricula"
            Begin Extent = 
               Top = 186
               Left = 0
               Bottom = 421
               Right = 245
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Matricula_Rubro"
            Begin Extent = 
               Top = 0
               Left = 132
               Bottom = 396
               Right = 376
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Rubro"
            Begin Extent = 
               Top = 7
               Left = 758
               Bottom = 334
               Right = 1002
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 0
               Left = 1249
               Bottom = 379
               Right = 1524
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Periodo"
            Begin Extent = 
               Top = 273
               Left = 503
               Bottom = 549
               Right = 748
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Rubro_Periodo"
            Begin Extent = 
               Top = 234
               Left = 1080
               Bottom = 406
               Right = 1250
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
      Begin ColumnWidths = 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula_Rubro_PorFacturar';





