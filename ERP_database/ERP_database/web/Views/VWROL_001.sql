CREATE VIEW web.VWROL_001
AS
SELECT        dbo.ro_rol.IdEmpresa, dbo.ro_rol.IdNominaTipo, dbo.ro_rol.IdNominaTipoLiqui, dbo.ro_rol.Descripcion, dbo.ro_rol.Observacion, dbo.ro_rol.Cerrado, dbo.ro_Departamento.de_descripcion, dbo.ro_rol_detalle.IdEmpleado, 
                         dbo.ro_rol_detalle.IdRubro, dbo.ro_rol_detalle.Orden, dbo.ro_rol_detalle.rub_visible_reporte, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_nombreCompleto, dbo.tb_persona.pe_cedulaRuc, dbo.ro_empleado.em_codigo, 
                         dbo.ro_Departamento.IdDepartamento, dbo.ro_area.IdArea, dbo.ro_Division.IdDivision, dbo.ro_area.Descripcion AS Area, dbo.ro_Division.Descripcion AS Division, dbo.ro_periodo.pe_FechaIni, dbo.ro_periodo.pe_FechaFin, 
                         dbo.ro_periodo.pe_estado, dbo.ro_Nomina_Tipo.Descripcion AS Nomina, dbo.ro_rol.IdPeriodo, dbo.ro_rubro_tipo.ru_codRolGen, dbo.tb_persona.pe_apellido, dbo.ro_rubro_tipo.ru_descripcion, dbo.ro_rubro_tipo.NombreCorto, 
                         dbo.ro_Nomina_Tipoliqui.DescripcionProcesoNomina, dbo.ro_rol_detalle.Valor, dbo.tb_sucursal.Su_Descripcion, dbo.ro_empleado.em_fechaIngaRol, CONVERT(varchar(10), dbo.ro_periodo.pe_FechaIni, 103) 
                         + '  AL ' + CONVERT(varchar(10), dbo.ro_periodo.pe_FechaFin, 103) + '      [ ' + CAST(dbo.ro_periodo.IdPeriodo AS VARCHAR) + ']' AS Periodo, dbo.tb_empresa.em_ruc, dbo.ro_rol.IdSucursal
FROM            dbo.ro_Division INNER JOIN
                         dbo.tb_sucursal INNER JOIN
                         dbo.ro_empleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento ON 
                         dbo.tb_sucursal.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.tb_sucursal.IdSucursal = dbo.ro_empleado.IdSucursal ON dbo.ro_Division.IdEmpresa = dbo.ro_empleado.IdEmpresa AND 
                         dbo.ro_Division.IdDivision = dbo.ro_empleado.IdDivision INNER JOIN
                         dbo.ro_area ON dbo.ro_empleado.IdArea = dbo.ro_area.IdArea AND dbo.ro_empleado.IdEmpresa = dbo.ro_area.IdEmpresa INNER JOIN
                         dbo.tb_empresa ON dbo.ro_empleado.IdEmpresa = dbo.tb_empresa.IdEmpresa RIGHT OUTER JOIN
                         dbo.ro_rubro_tipo RIGHT OUTER JOIN
                         dbo.ro_rol_detalle ON dbo.ro_rubro_tipo.IdRubro = dbo.ro_rol_detalle.IdRubro AND dbo.ro_rubro_tipo.IdEmpresa = dbo.ro_rol_detalle.IdEmpresa ON dbo.ro_empleado.IdEmpresa = dbo.ro_rol_detalle.IdEmpresa AND 
                         dbo.ro_empleado.IdEmpleado = dbo.ro_rol_detalle.IdEmpleado LEFT OUTER JOIN
                         dbo.ro_Nomina_Tipoliqui INNER JOIN
                         dbo.ro_Nomina_Tipo INNER JOIN
                         dbo.ro_rol ON dbo.ro_Nomina_Tipo.IdEmpresa = dbo.ro_rol.IdEmpresa AND dbo.ro_Nomina_Tipo.IdNomina_Tipo = dbo.ro_rol.IdNominaTipo AND dbo.ro_Nomina_Tipo.IdNomina_Tipo = dbo.ro_rol.IdNominaTipo ON 
                         dbo.ro_Nomina_Tipoliqui.IdEmpresa = dbo.ro_rol.IdEmpresa AND dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo = dbo.ro_rol.IdNominaTipo AND dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui = dbo.ro_rol.IdNominaTipoLiqui AND 
                         dbo.ro_Nomina_Tipoliqui.IdEmpresa = dbo.ro_Nomina_Tipo.IdEmpresa AND dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo = dbo.ro_Nomina_Tipo.IdNomina_Tipo INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui INNER JOIN
                         dbo.ro_periodo ON dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa = dbo.ro_periodo.IdEmpresa AND dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo = dbo.ro_periodo.IdPeriodo ON 
                         dbo.ro_rol.IdEmpresa = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa AND dbo.ro_rol.IdNominaTipo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_Tipo AND 
                         dbo.ro_rol.IdNominaTipoLiqui = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_TipoLiqui AND dbo.ro_rol.IdPeriodo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo ON 
                         dbo.ro_rol_detalle.IdEmpresa = dbo.ro_rol.IdEmpresa AND dbo.ro_rol_detalle.IdRol = dbo.ro_rol.IdRol
WHERE        (dbo.ro_rol_detalle.rub_visible_reporte = 1)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[35] 4[35] 2[9] 3) )"
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
         Begin Table = "ro_Division"
            Begin Extent = 
               Top = 666
               Left = 258
               Bottom = 796
               Right = 437
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 1062
               Left = 38
               Bottom = 1192
               Right = 268
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 327
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 534
               Left = 38
               Bottom = 664
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Departamento"
            Begin Extent = 
               Top = 138
               Left = 268
               Bottom = 268
               Right = 447
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_area"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_empresa"
            Begin Extent = 
               Top = 363
               Left = 412
               Bottom = 485
               Right = 631
            End
   ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_001';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'         DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_rubro_tipo"
            Begin Extent = 
               Top = 798
               Left = 38
               Bottom = 928
               Right = 277
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_rol_detalle"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 301
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Nomina_Tipoliqui"
            Begin Extent = 
               Top = 930
               Left = 38
               Bottom = 1060
               Right = 274
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Nomina_Tipo"
            Begin Extent = 
               Top = 666
               Left = 38
               Bottom = 796
               Right = 220
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_rol"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 230
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "ro_periodo_x_ro_Nomina_TipoLiqui"
            Begin Extent = 
               Top = 1194
               Left = 38
               Bottom = 1324
               Right = 235
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_periodo"
            Begin Extent = 
               Top = 6
               Left = 255
               Bottom = 136
               Right = 476
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
      Begin ColumnWidths = 36
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
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
         Output = 1410
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_001';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_001';

