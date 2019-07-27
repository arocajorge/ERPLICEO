CREATE VIEW web.VWACTF_007
AS
SELECT dbo.Af_Activo_fijo.IdEmpresa, dbo.Af_Activo_fijo.IdActivoFijo, dbo.Af_Activo_fijo.Af_fecha_compra, dbo.Af_Activo_fijo.Af_costo_compra, dbo.Af_Activo_fijo.Estado, dbo.Af_Activo_fijo.Af_observacion, dbo.Af_Activo_fijo.Af_Nombre, 
                  dbo.tb_sucursal.Su_Descripcion, dbo.Af_Activo_fijo_tipo.Af_Descripcion AS NomTipo, dbo.Af_Activo_fijo_Categoria.Descripcion AS NomCategoria, dbo.Af_Departamento.Descripcion AS NomDepartamento, 
                  dbo.tb_persona.pe_nombreCompleto AS NomEncargado, tb_persona_1.pe_nombreCompleto AS NomCustodio, dbo.Af_Activo_fijo.Cantidad
FROM     dbo.Af_Activo_fijo INNER JOIN
                  dbo.Af_Activo_fijo_Categoria ON dbo.Af_Activo_fijo.IdEmpresa = dbo.Af_Activo_fijo_Categoria.IdEmpresa AND dbo.Af_Activo_fijo.IdCategoriaAF = dbo.Af_Activo_fijo_Categoria.IdCategoriaAF INNER JOIN
                  dbo.Af_Activo_fijo_tipo ON dbo.Af_Activo_fijo_Categoria.IdEmpresa = dbo.Af_Activo_fijo_tipo.IdEmpresa AND dbo.Af_Activo_fijo_Categoria.IdActivoFijoTipo = dbo.Af_Activo_fijo_tipo.IdActivoFijoTipo INNER JOIN
                  dbo.Af_Departamento ON dbo.Af_Activo_fijo.IdEmpresa = dbo.Af_Departamento.IdEmpresa AND dbo.Af_Activo_fijo.IdDepartamento = dbo.Af_Departamento.IdDepartamento INNER JOIN
                  dbo.ro_empleado ON dbo.Af_Activo_fijo.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.Af_Activo_fijo.IdEmpresa = dbo.ro_empleado.IdEmpresa AND 
                  dbo.Af_Activo_fijo.IdEmpleadoEncargado = dbo.ro_empleado.IdEmpleado INNER JOIN
                  dbo.ro_empleado AS ro_empleado_1 ON dbo.Af_Activo_fijo.IdEmpresa = ro_empleado_1.IdEmpresa AND dbo.Af_Activo_fijo.IdEmpresa = ro_empleado_1.IdEmpresa AND 
                  dbo.Af_Activo_fijo.IdEmpleadoCustodio = ro_empleado_1.IdEmpleado INNER JOIN
                  dbo.tb_persona AS tb_persona_1 ON ro_empleado_1.IdPersona = tb_persona_1.IdPersona INNER JOIN
                  dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.tb_sucursal ON dbo.Af_Activo_fijo.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.Af_Activo_fijo.IdSucursal = dbo.tb_sucursal.IdSucursal
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWACTF_007';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 1351
               Left = 48
               Bottom = 1514
               Right = 320
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWACTF_007';


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
         Begin Table = "Af_Activo_fijo"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 319
            End
            DisplayFlags = 280
            TopColumn = 27
         End
         Begin Table = "Af_Activo_fijo_Categoria"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 256
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Af_Activo_fijo_tipo"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 305
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Af_Departamento"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 395
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado_1"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 395
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona_1"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWACTF_007';

