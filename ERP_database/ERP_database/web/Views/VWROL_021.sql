CREATE VIEW web.VWROL_021
AS
SELECT        data.IdEmpresa, data.IdSucursal, data.IdNominaTipo, data.IdNominaTipoLiqui, data.IdEmpleado, data.IdArea, data.IDividion, data.IdAreaEmpleado, data.IdDivisionEmpleado, data.IdPeriodo, data.IdRubro, data.se_distribuye, 
                         data.Orden, data.Porcentaje, data.Valor, data.rub_visible_reporte, data.Observacion, data.ru_descripcion, data.pe_FechaIni, data.pe_FechaFin, data.ru_tipo, data.rub_codigo, data.ru_codRolGen, data.ca_descripcion, 
                         data.em_codigo, data.pe_cedulaRuc, data.pe_nombreCompleto, data.IdRol, data.Descripcion, data.rub_grupo, data.Dias, nom_t.Descripcion AS NominaTipo, nom_tip.DescripcionProcesoNomina AS NominaTipoLiqui, 
                         area.Descripcion AS AreaEmpleado, div.Descripcion AS Division, su.Su_Descripcion
FROM            (SELECT        rol_det.IdEmpresa, rol.IdSucursal, rol.IdNominaTipo, rol.IdNominaTipoLiqui, rol_det.IdEmpleado, CASE WHEN rubr.se_distribuye = 1 THEN emp_div.IdArea ELSE NULL END AS IdArea, 
                                                    CASE WHEN rubr.se_distribuye = 1 THEN emp_div.IDividion ELSE NULL END AS IDividion, dbo.ro_empleado.IdArea AS IdAreaEmpleado, dbo.ro_empleado.IdDivision AS IdDivisionEmpleado, rol.IdPeriodo, 
                                                    rol_det.IdRubro, rubr.se_distribuye, rubr.ru_orden AS Orden, CASE WHEN rubr.se_distribuye = 1 THEN emp_div.Porcentaje ELSE NULL END AS Porcentaje, 
                                                    CASE WHEN rubr.se_distribuye = 1 THEN CASE WHEN rubr.ru_tipo = 'I' THEN (rol_det.Valor * (emp_div.Porcentaje / 100)) ELSE rol_det.Valor END ELSE rol_det.Valor END AS Valor, rol_det.rub_visible_reporte, 
                                                    rol_det.Observacion, CASE WHEN (rubr.se_distribuye = 1 AND (rub_cal.IdRubro_sueldo = rol_det.IdRubro OR
                                                    rub_cal.IdRubro_horas_matutina = rol_det.IdRubro OR
                                                    rub_cal.IdRubro_horas_vespertina = rol_det.IdRubro)) THEN 'SUELDO COMPARTIDO' ELSE rubr.ru_descripcion END AS ru_descripcion, perid.pe_FechaIni, perid.pe_FechaFin, rubr.ru_tipo, rubr.rub_codigo, 
                                                    rubr.ru_codRolGen, CASE WHEN ru_tipo = 'A' THEN '3 - TOTALES' WHEN ru_tipo = 'I' THEN '1 - INGRESOS' ELSE '2 - EGRESOS' END AS ca_descripcion, dbo.ro_empleado.em_codigo, 
                                                    dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_nombreCompleto, rol_det.IdRol, CASE WHEN rubr.ru_tipo = 'I' THEN dbo.ro_area.Descripcion + '-' + CAST(emp_div.Porcentaje AS varchar) 
                                                    ELSE '' END AS Descripcion, cat.ca_descripcion AS rub_grupo, d.Dias
                          FROM            dbo.ro_area INNER JOIN
                                                    dbo.ro_empleado_division_area_x_rol AS emp_div ON dbo.ro_area.IdEmpresa = emp_div.IdEmpresa AND dbo.ro_area.IdArea = emp_div.IdArea RIGHT OUTER JOIN
                                                    dbo.ro_rol AS rol INNER JOIN
                                                    dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND
                                                     rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                                                    dbo.ro_periodo AS perid ON pe_x_nom.IdEmpresa = perid.IdEmpresa AND pe_x_nom.IdPeriodo = perid.IdPeriodo INNER JOIN
                                                    dbo.ro_rubro_tipo AS rubr INNER JOIN
                                                    dbo.ro_rol_detalle AS rol_det ON rubr.IdEmpresa = rol_det.IdEmpresa AND rubr.IdRubro = rol_det.IdRubro ON rol.IdEmpresa = rol_det.IdEmpresa AND rol.IdRol = rol_det.IdRol LEFT OUTER JOIN
                                                    dbo.ro_empleado INNER JOIN
                                                    dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona ON rol_det.IdEmpresa = dbo.ro_empleado.IdEmpresa AND rol_det.IdEmpleado = dbo.ro_empleado.IdEmpleado ON 
                                                    emp_div.IdEmpresa = rol_det.IdEmpresa AND emp_div.IdEmpleado = rol_det.IdEmpleado AND emp_div.IdRol = rol_det.IdRol INNER JOIN
                                                    dbo.ro_catalogo AS cat ON rubr.rub_grupo = cat.CodCatalogo LEFT OUTER JOIN
                                                    dbo.ro_rubros_calculados AS rub_cal ON rub_cal.IdEmpresa = dbo.ro_area.IdEmpresa LEFT OUTER JOIN
                                                        (SELECT        det.IdEmpresa, det.IdRol, det.IdEmpleado, det.Valor AS Dias
                                                          FROM            dbo.ro_rol_detalle AS det INNER JOIN
                                                                                    dbo.ro_rubros_calculados AS p ON p.IdEmpresa = det.IdEmpresa AND det.IdRubro = p.IdRubro_dias_trabajados) AS d ON d.IdEmpresa = rol_det.IdEmpresa AND d.IdRol = rol_det.IdRol AND 
                                                    d.IdEmpleado = rol_det.IdEmpleado
                          WHERE        (dbo.ro_empleado.Tiene_ingresos_compartidos = 1) AND (rubr.se_distribuye = 1)
                          UNION ALL
                          SELECT        rol_det.IdEmpresa, rol.IdSucursal, rol.IdNominaTipo, rol.IdNominaTipoLiqui, rol_det.IdEmpleado, NULL AS IdArea, NULL AS IDividion, ro_empleado_2.IdArea AS IdAreaEmpleado, 
                                                   ro_empleado_2.IdDivision AS IdDivisionEmpleado, rol.IdPeriodo, rol_det.IdRubro, rubr.se_distribuye, rubr.ru_orden AS Orden, NULL AS Porcentaje, rol_det.Valor, rol_det.rub_visible_reporte, rol_det.Observacion, 
                                                   CASE WHEN (rubr.se_distribuye = 1 AND (rub_cal.IdRubro_sueldo = rol_det.IdRubro OR
                                                   rub_cal.IdRubro_horas_matutina = rol_det.IdRubro OR
                                                   rub_cal.IdRubro_horas_vespertina = rol_det.IdRubro)) THEN 'SUELDOS POR HORA' ELSE rubr.ru_descripcion END AS ru_descripcion, perid.pe_FechaIni, perid.pe_FechaFin, rubr.ru_tipo, rubr.rub_codigo, 
                                                   rubr.ru_codRolGen, CASE WHEN ru_tipo = 'A' THEN '3 - TOTALES' WHEN ru_tipo = 'I' THEN '1 - INGRESOS' ELSE '2 - EGRESOS' END AS ca_descripcion, ro_empleado_2.em_codigo, tb_persona_2.pe_cedulaRuc, 
                                                   tb_persona_2.pe_nombreCompleto, rol_det.IdRol, '' AS Descripcion, cat.ca_descripcion AS rub_grupo, d_2.Dias
                          FROM            dbo.ro_area AS ro_area_1 INNER JOIN
                                                   dbo.ro_rol AS rol INNER JOIN
                                                   dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                                                   rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                                                   dbo.ro_periodo AS perid ON pe_x_nom.IdEmpresa = perid.IdEmpresa AND pe_x_nom.IdPeriodo = perid.IdPeriodo INNER JOIN
                                                   dbo.ro_rubro_tipo AS rubr INNER JOIN
                                                   dbo.ro_rol_detalle AS rol_det ON rubr.IdEmpresa = rol_det.IdEmpresa AND rubr.IdRubro = rol_det.IdRubro ON rol.IdEmpresa = rol_det.IdEmpresa AND rol.IdRol = rol_det.IdRol LEFT OUTER JOIN
                                                   dbo.ro_empleado AS ro_empleado_2 INNER JOIN
                                                   dbo.tb_persona AS tb_persona_2 ON ro_empleado_2.IdPersona = tb_persona_2.IdPersona ON rol_det.IdEmpresa = ro_empleado_2.IdEmpresa AND rol_det.IdEmpleado = ro_empleado_2.IdEmpleado ON 
                                                   ro_area_1.IdEmpresa = ro_empleado_2.IdEmpresa AND ro_area_1.IdArea = ro_empleado_2.IdArea LEFT OUTER JOIN
                                                   dbo.ro_catalogo AS cat ON rubr.rub_grupo = cat.CodCatalogo LEFT OUTER JOIN
                                                   dbo.ro_rubros_calculados AS rub_cal ON ro_area_1.IdEmpresa = rub_cal.IdEmpresa LEFT OUTER JOIN
                                                       (SELECT        det.IdEmpresa, det.IdRol, det.IdEmpleado, det.Valor AS Dias
                                                         FROM            dbo.ro_rol_detalle AS det INNER JOIN
                                                                                   dbo.ro_rubros_calculados AS p ON p.IdEmpresa = det.IdEmpresa AND det.IdRubro = p.IdRubro_dias_trabajados) AS d_2 ON d_2.IdEmpresa = rol_det.IdEmpresa AND d_2.IdRol = rol_det.IdRol AND 
                                                   d_2.IdEmpleado = rol_det.IdEmpleado
                          WHERE        (ro_empleado_2.Tiene_ingresos_compartidos = 1) AND (rubr.se_distribuye = 0)
                          UNION ALL
                          SELECT        rol_det.IdEmpresa, rol.IdSucursal, rol.IdNominaTipo, rol.IdNominaTipoLiqui, rol_det.IdEmpleado, NULL AS Expr2, NULL AS Expr3, ro_empleado_1.IdArea, ro_empleado_1.IdDivision, rol.IdPeriodo, rol_det.IdRubro, 
                                                   rubr.se_distribuye, rubr.ru_orden AS Orden, 0 AS Expr1, rol_det.Valor, rol_det.rub_visible_reporte, rol_det.Observacion, rubr.ru_descripcion, perid.pe_FechaIni, perid.pe_FechaFin, rubr.ru_tipo, rubr.rub_codigo, 
                                                   rubr.ru_codRolGen, CASE WHEN ru_tipo = 'A' THEN '3 - TOTALES' WHEN ru_tipo = 'I' THEN '1 - INGRESOS' ELSE '2 - EGRESOS' END AS ca_descripcion, ro_empleado_1.em_codigo, tb_persona_1.pe_cedulaRuc, 
                                                   tb_persona_1.pe_nombreCompleto, rol_det.IdRol, '' AS Descripcion, cat.ca_descripcion AS rub_grupo, d_1.Dias
                          FROM            dbo.ro_rol AS rol INNER JOIN
                                                   dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                                                   rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                                                   dbo.ro_periodo AS perid ON pe_x_nom.IdEmpresa = perid.IdEmpresa AND pe_x_nom.IdPeriodo = perid.IdPeriodo INNER JOIN
                                                   dbo.ro_rubro_tipo AS rubr INNER JOIN
                                                   dbo.ro_rol_detalle AS rol_det ON rubr.IdEmpresa = rol_det.IdEmpresa AND rubr.IdRubro = rol_det.IdRubro ON rol.IdEmpresa = rol_det.IdEmpresa AND rol.IdRol = rol_det.IdRol LEFT OUTER JOIN
                                                   dbo.ro_empleado AS ro_empleado_1 INNER JOIN
                                                   dbo.tb_persona AS tb_persona_1 ON ro_empleado_1.IdPersona = tb_persona_1.IdPersona ON rol_det.IdEmpresa = ro_empleado_1.IdEmpresa AND 
                                                   rol_det.IdEmpleado = ro_empleado_1.IdEmpleado LEFT OUTER JOIN
                                                   dbo.ro_catalogo AS cat ON rubr.rub_grupo = cat.CodCatalogo LEFT OUTER JOIN
                                                       (SELECT        det.IdEmpresa, det.IdRol, det.IdEmpleado, det.Valor AS Dias
                                                         FROM            dbo.ro_rol_detalle AS det INNER JOIN
                                                                                   dbo.ro_rubros_calculados AS p ON p.IdEmpresa = det.IdEmpresa AND det.IdRubro = p.IdRubro_dias_trabajados) AS d_1 ON d_1.IdEmpresa = rol_det.IdEmpresa AND d_1.IdRol = rol_det.IdRol AND 
                                                   d_1.IdEmpleado = rol_det.IdEmpleado
                          WHERE        (ro_empleado_1.Tiene_ingresos_compartidos = 0)) AS data LEFT OUTER JOIN
                         dbo.ro_area AS area ON area.IdEmpresa = data.IdEmpresa AND area.IdArea = data.IdAreaEmpleado LEFT OUTER JOIN
                         dbo.ro_Division AS div ON div.IdEmpresa = data.IdEmpresa AND div.IdDivision = data.IdDivisionEmpleado LEFT OUTER JOIN
                         dbo.tb_sucursal AS su ON su.IdEmpresa = data.IdEmpresa AND su.IdSucursal = data.IdSucursal LEFT OUTER JOIN
                         dbo.ro_Nomina_Tipo AS nom_t ON nom_t.IdEmpresa = data.IdEmpresa AND nom_t.IdNomina_Tipo = data.IdNominaTipo LEFT OUTER JOIN
                         dbo.ro_Nomina_Tipoliqui AS nom_tip ON nom_tip.IdEmpresa = data.IdEmpresa AND nom_tip.IdNomina_Tipo = data.IdNominaTipo AND nom_tip.IdNomina_TipoLiqui = data.IdNominaTipoLiqui
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_021';






GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[8] 4[3] 2[84] 3) )"
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
         Top = -223
         Left = 0
      End
      Begin Tables = 
         Begin Table = "area"
            Begin Extent = 
               Top = 229
               Left = 278
               Bottom = 359
               Right = 457
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "div"
            Begin Extent = 
               Top = 361
               Left = 38
               Bottom = 491
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "su"
            Begin Extent = 
               Top = 493
               Left = 38
               Bottom = 623
               Right = 268
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "nom_t"
            Begin Extent = 
               Top = 361
               Left = 255
               Bottom = 491
               Right = 437
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "nom_tip"
            Begin Extent = 
               Top = 625
               Left = 38
               Bottom = 755
               Right = 274
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "data"
            Begin Extent = 
               Top = 229
               Left = 38
               Bottom = 359
               Right = 240
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
      Begin ColumnWidths = 31
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_021';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'500
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
         Width = 1380
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_021';

