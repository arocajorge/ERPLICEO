CREATE VIEW dbo.vwaca_Matricula_Rubro
AS
SELECT mr.IdEmpresa, mr.IdMatricula, mr.IdPeriodo, ap.FechaDesde, mr.IdRubro, ar.NomRubro, mr.IdProducto, mr.Subtotal, mr.IdCod_Impuesto_Iva, mr.Porcentaje, mr.ValorIVA, mr.Total, p.pr_descripcion, mr.IdSucursal, mr.IdBodega, 
                  mr.IdCbteVta, mr.FechaFacturacion, mr.IdMecanismo, mr.EnMatricula, mr.IdAnio, mr.IdPlantilla, mr.IdSede, mr.IdNivel, mr.IdJornada, mr.IdCurso, mr.IdParalelo
FROM     dbo.aca_AnioLectivo_Rubro AS ar WITH (nolock) INNER JOIN
                  dbo.aca_AnioLectivo_Rubro_Periodo AS arp WITH (nolock) ON ar.IdEmpresa = arp.IdEmpresa AND ar.IdAnio = arp.IdAnio AND ar.IdRubro = arp.IdRubro INNER JOIN
                  dbo.aca_AnioLectivo_Periodo AS ap WITH (nolock) ON arp.IdEmpresa = ap.IdEmpresa AND arp.IdPeriodo = ap.IdPeriodo INNER JOIN
                  dbo.aca_Matricula_Rubro AS mr WITH (nolock) ON arp.IdEmpresa = mr.IdEmpresa AND arp.IdPeriodo = mr.IdPeriodo AND arp.IdRubro = mr.IdRubro INNER JOIN
                  dbo.in_Producto AS p WITH (nolock) ON mr.IdEmpresa = p.IdEmpresa AND mr.IdProducto = p.IdProducto
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula_Rubro';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' 1200
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
         Begin Table = "ar"
            Begin Extent = 
               Top = 0
               Left = 1078
               Bottom = 219
               Right = 1322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "arp"
            Begin Extent = 
               Top = 0
               Left = 459
               Bottom = 202
               Right = 703
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ap"
            Begin Extent = 
               Top = 144
               Left = 743
               Bottom = 394
               Right = 988
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "mr"
            Begin Extent = 
               Top = 7
               Left = 54
               Bottom = 399
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "p"
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
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width =', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula_Rubro';











