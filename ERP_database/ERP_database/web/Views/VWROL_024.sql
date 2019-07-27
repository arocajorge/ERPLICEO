CREATE VIEW web.VWROL_024
AS
SELECT        dbo.ro_rol_detalle.IdEmpresa, dbo.ro_rol_detalle.IdRol, dbo.ro_rol_detalle.IdEmpleado, dbo.ro_rol.IdNominaTipoLiqui, dbo.ro_rol.IdNominaTipo, dbo.ro_Nomina_Tipo.Descripcion AS NomNomina, 
                         dbo.ro_Nomina_Tipoliqui.DescripcionProcesoNomina AS NomNominaTipo, dbo.tb_persona.pe_nombreCompleto, dbo.ro_rol.IdPeriodo, dbo.ro_periodo.pe_FechaIni, dbo.ro_periodo.pe_FechaFin, SUM(dbo.ro_rol_detalle.Valor) 
                         AS Valor, dbo.ro_rol_detalle.IdSucursal, dbo.tb_sucursal.Su_Descripcion, dbo.tb_persona.pe_cedulaRuc, ISNULL(D.Valor, 0) AS Dias
FROM            dbo.ro_rol INNER JOIN
                         dbo.ro_rol_detalle ON dbo.ro_rol.IdEmpresa = dbo.ro_rol_detalle.IdEmpresa AND dbo.ro_rol.IdRol = dbo.ro_rol_detalle.IdRol INNER JOIN
                         dbo.ro_Nomina_Tipoliqui ON dbo.ro_rol.IdEmpresa = dbo.ro_Nomina_Tipoliqui.IdEmpresa AND dbo.ro_rol.IdNominaTipo = dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo AND 
                         dbo.ro_rol.IdNominaTipoLiqui = dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui INNER JOIN
                         dbo.ro_Nomina_Tipo ON dbo.ro_Nomina_Tipoliqui.IdEmpresa = dbo.ro_Nomina_Tipo.IdEmpresa AND dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo = dbo.ro_Nomina_Tipo.IdNomina_Tipo INNER JOIN
                         dbo.ro_empleado ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_rol_detalle.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_periodo ON dbo.ro_rol.IdEmpresa = dbo.ro_periodo.IdEmpresa AND dbo.ro_rol.IdPeriodo = dbo.ro_periodo.IdPeriodo INNER JOIN
                         dbo.tb_sucursal ON dbo.ro_rol_detalle.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ro_rol_detalle.IdSucursal = dbo.tb_sucursal.IdSucursal INNER JOIN
                         dbo.ro_rubro_tipo ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_rubro_tipo.IdEmpresa AND dbo.ro_rol_detalle.IdRubro = dbo.ro_rubro_tipo.IdRubro LEFT OUTER JOIN
                             (SELECT        ro_rol_detalle_1.IdEmpresa, ro_rol_detalle_1.IdRol, ro_rol_detalle_1.IdEmpleado, ro_rol_detalle_1.Valor
                               FROM            dbo.ro_rol_detalle AS ro_rol_detalle_1 INNER JOIN
                                                         dbo.ro_rubros_calculados AS ro_rubros_calculados_1 ON ro_rol_detalle_1.IdEmpresa = ro_rubros_calculados_1.IdEmpresa AND 
                                                         ro_rol_detalle_1.IdRubro = ro_rubros_calculados_1.IdRubro_dias_trabajados INNER JOIN
                                                         dbo.ro_rol AS ro_rol_1 ON ro_rol_detalle_1.IdEmpresa = ro_rol_1.IdEmpresa AND ro_rol_detalle_1.IdRol = ro_rol_1.IdRol) AS D ON D.IdEmpresa = dbo.ro_rol_detalle.IdEmpresa AND 
                         D.IdEmpleado = dbo.ro_rol_detalle.IdEmpleado AND D.IdRol = dbo.ro_rol_detalle.IdRol
WHERE        (dbo.ro_rubro_tipo.ru_tipo = 'I') AND (dbo.ro_rubro_tipo.rub_aplica_IESS = 1)
GROUP BY dbo.ro_rol_detalle.IdEmpresa, dbo.ro_rol_detalle.IdRol, dbo.ro_rol_detalle.IdEmpleado, dbo.ro_rol.IdNominaTipoLiqui, dbo.ro_rol.IdNominaTipo, dbo.ro_Nomina_Tipo.Descripcion, 
                         dbo.ro_Nomina_Tipoliqui.DescripcionProcesoNomina, dbo.tb_persona.pe_nombreCompleto, dbo.ro_rol.IdPeriodo, dbo.ro_periodo.pe_FechaIni, dbo.ro_periodo.pe_FechaFin, dbo.ro_rol_detalle.IdSucursal, 
                         dbo.tb_sucursal.Su_Descripcion, dbo.tb_persona.pe_cedulaRuc, D.Valor
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_024';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 930
               Left = 38
               Bottom = 1060
               Right = 284
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_rubro_tipo"
            Begin Extent = 
               Top = 1062
               Left = 38
               Bottom = 1192
               Right = 272
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "D"
            Begin Extent = 
               Top = 6
               Left = 284
               Bottom = 136
               Right = 470
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
      Begin ColumnWidths = 12
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_024';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[22] 4[5] 2[56] 3) )"
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
         Begin Table = "ro_rol"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 246
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_rol_detalle"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Nomina_Tipoliqui"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 290
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Nomina_Tipo"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 236
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 534
               Left = 38
               Bottom = 664
               Right = 343
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 666
               Left = 38
               Bottom = 796
               Right = 286
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_periodo"
            Begin Extent = 
               Top = 798
               Left = 38
               Bottom = 928
               Right = 275
            End', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_024';

