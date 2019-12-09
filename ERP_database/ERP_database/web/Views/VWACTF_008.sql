CREATE VIEW web.VWACTF_008
AS
SELECT        TOP (20000) af.IdEmpresa, af.IdActivoFijo, af.Af_Nombre, 'Cant:' + (CONVERT(varchar(5), ISNULL(af.Cantidad, 1)) + ' ' + RTRIM(ISNULL(af.Af_observacion, ''))) AS Af_observacion, af.Estado, af.Af_fecha_compra, 
                         af.Af_costo_compra, af.Af_Vida_Util, af.IdEmpleadoEncargado, af.IdEmpleadoCustodio, enc.pe_nombreCompleto AS EmpleadoEncargado, cusper.pe_nombreCompleto AS EmpleadoCustodio, af.IdDepartamento, 
                         af.IdSucursal, s.Su_Descripcion, dep.Descripcion AS NomDepartamento, af.Cantidad, cat.Descripcion AS NomCategoria, tipo.Af_Descripcion AS NomTipo, dbo.Af_Area.Descripcion AS NomArea, af.IdArea, 
                         Af_Catalogo_1.Descripcion AS NomModelo, af.IdActivoFijoTipo, af.IdCategoriaAF, af.FechaEntrega, af.IdTipoCatalogo_Ubicacion, dbo.Af_Catalogo.Descripcion
FROM            dbo.tb_persona AS cusper INNER JOIN
                         dbo.tb_sucursal AS s INNER JOIN
                         dbo.Af_Activo_fijo AS af INNER JOIN
                         dbo.Af_Activo_fijo_Categoria AS cat ON af.IdEmpresa = cat.IdEmpresa AND af.IdCategoriaAF = cat.IdCategoriaAF INNER JOIN
                         dbo.Af_Activo_fijo_tipo AS tipo ON cat.IdActivoFijoTipo = tipo.IdActivoFijoTipo AND cat.IdEmpresa = tipo.IdEmpresa INNER JOIN
                         dbo.Af_Departamento AS dep ON af.IdDepartamento = dep.IdDepartamento AND af.IdEmpresa = dep.IdEmpresa ON s.IdEmpresa = af.IdEmpresa AND s.IdSucursal = af.IdSucursal ON 
                         cusper.IdPersona = af.IdEmpleadoCustodio INNER JOIN
                         dbo.Af_Area ON dep.IdArea = dbo.Af_Area.IdArea AND dep.IdEmpresa = dbo.Af_Area.IdEmpresa LEFT OUTER JOIN
                         dbo.Af_Catalogo ON af.IdTipoCatalogo_Ubicacion = dbo.Af_Catalogo.IdCatalogo LEFT OUTER JOIN
                         dbo.Af_Catalogo AS Af_Catalogo_1 ON af.IdCatalogo_Modelo = Af_Catalogo_1.IdCatalogo LEFT OUTER JOIN
                         dbo.tb_persona AS enc ON af.IdEmpleadoEncargado = enc.IdPersona
  WHERE        (af.Estado = 'A') -- OJO, AQUI CAMBIAR Y PONER DESDE EL COMBO BOX DEL SISTEMA ESTADOS BY ACUEVA 06/12/2019
ORDER BY af.IdActivoFijo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWACTF_008';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'lumn = 0
         End
         Begin Table = "Af_Catalogo"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 256
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Af_Catalogo_1"
            Begin Extent = 
               Top = 246
               Left = 358
               Bottom = 354
               Right = 547
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "enc"
            Begin Extent = 
               Top = 1351
               Left = 48
               Bottom = 1514
               Right = 322
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
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 2460
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWACTF_008';




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
         Top = -240
         Left = 0
      End
      Begin Tables = 
         Begin Table = "cusper"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 320
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "af"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 319
            End
            DisplayFlags = 280
            TopColumn = 13
         End
         Begin Table = "cat"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 256
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tipo"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 305
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dep"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Af_Area"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 293
            End
            DisplayFlags = 280
            TopCo', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWACTF_008';

