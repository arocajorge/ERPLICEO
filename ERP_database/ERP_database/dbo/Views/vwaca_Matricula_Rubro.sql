CREATE VIEW dbo.vwaca_Matricula_Rubro
AS
SELECT dbo.aca_Matricula_Rubro.IdEmpresa, dbo.aca_Matricula_Rubro.IdMatricula, dbo.aca_Matricula_Rubro.IdPeriodo, dbo.aca_AnioLectivo_Periodo.FechaDesde, dbo.aca_Matricula_Rubro.IdRubro, dbo.aca_AnioLectivo_Rubro.NomRubro, 
                  dbo.aca_Matricula_Rubro.IdProducto, dbo.aca_Matricula_Rubro.Subtotal, dbo.aca_Matricula_Rubro.IdCod_Impuesto_Iva, dbo.aca_Matricula_Rubro.Porcentaje, dbo.aca_Matricula_Rubro.ValorIVA, dbo.aca_Matricula_Rubro.Total, 
                  dbo.in_Producto.pr_descripcion, dbo.aca_Matricula_Rubro.IdSucursal, dbo.aca_Matricula_Rubro.IdBodega, dbo.aca_Matricula_Rubro.IdCbteVta, dbo.aca_Matricula_Rubro.FechaFacturacion, dbo.aca_Matricula_Rubro.IdMecanismo, 
                  dbo.aca_Matricula_Rubro.EnMatricula, dbo.aca_Matricula_Rubro.IdAnio, dbo.aca_Matricula_Rubro.IdPlantilla, dbo.aca_Matricula_Rubro.IdSede, dbo.aca_Matricula_Rubro.IdNivel, dbo.aca_Matricula_Rubro.IdJornada, 
                  dbo.aca_Matricula_Rubro.IdCurso, dbo.aca_Matricula_Rubro.IdParalelo
FROM     dbo.aca_AnioLectivo_Rubro INNER JOIN
                  dbo.aca_AnioLectivo_Rubro_Periodo ON dbo.aca_AnioLectivo_Rubro.IdEmpresa = dbo.aca_AnioLectivo_Rubro_Periodo.IdEmpresa AND dbo.aca_AnioLectivo_Rubro.IdAnio = dbo.aca_AnioLectivo_Rubro_Periodo.IdAnio AND 
                  dbo.aca_AnioLectivo_Rubro.IdRubro = dbo.aca_AnioLectivo_Rubro_Periodo.IdRubro INNER JOIN
                  dbo.aca_AnioLectivo_Periodo ON dbo.aca_AnioLectivo_Rubro_Periodo.IdEmpresa = dbo.aca_AnioLectivo_Periodo.IdEmpresa AND dbo.aca_AnioLectivo_Rubro_Periodo.IdPeriodo = dbo.aca_AnioLectivo_Periodo.IdPeriodo INNER JOIN
                  dbo.aca_Matricula_Rubro ON dbo.aca_AnioLectivo_Rubro_Periodo.IdEmpresa = dbo.aca_Matricula_Rubro.IdEmpresa AND dbo.aca_AnioLectivo_Rubro_Periodo.IdPeriodo = dbo.aca_Matricula_Rubro.IdPeriodo AND 
                  dbo.aca_AnioLectivo_Rubro_Periodo.IdRubro = dbo.aca_Matricula_Rubro.IdRubro INNER JOIN
                  dbo.in_Producto ON dbo.aca_Matricula_Rubro.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.aca_Matricula_Rubro.IdProducto = dbo.in_Producto.IdProducto
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula_Rubro';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' = 1200
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula_Rubro';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[52] 4[9] 2[20] 3) )"
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
               Left = 1078
               Bottom = 219
               Right = 1322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Rubro_Periodo"
            Begin Extent = 
               Top = 0
               Left = 459
               Bottom = 202
               Right = 703
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Periodo"
            Begin Extent = 
               Top = 177
               Left = 881
               Bottom = 427
               Right = 1126
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "aca_Matricula_Rubro"
            Begin Extent = 
               Top = 7
               Left = 54
               Bottom = 399
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 228
               Left = 401
               Bottom = 406
               Right = 676
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
      Begin ColumnWidths = 27
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
         Width', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula_Rubro';









