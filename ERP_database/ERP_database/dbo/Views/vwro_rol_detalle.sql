CREATE VIEW [dbo].[vwro_rol_detalle]
AS
SELECT r.IdEmpresa, r.IdRol, r.IdSucursal, r.IdNominaTipo, r.IdNominaTipoLiqui, r.IdPeriodo, r_det.IdEmpleado, ISNULL(X.IdRubroContabilizacion, r_det.IdRubro) IdRubro,  
CASE WHEN X.IdEmpleado IS NOT NULL AND X.Secuencia = X.CantJornada 
THEN  r_det.Valor - (ROUND(r_det.Valor/X.CantJornada,2) * (X.CantJornada-1)) 
WHEN X.IdEmpleado IS NULL THEN r_det.Valor
ELSE ROUND(r_det.Valor/X.CantJornada,2) 
END AS Valor,
rub.ru_tipo, rub.rub_ctacon, dbo.ct_plancta.pc_Cuenta AS pc_CuentaRubro, emp.IdCtaCble_Emplea, 
                  ct_plancta_1.pc_Cuenta AS pc_CuentaEmple, emp.IdDivision, emp.IdArea, emp.IdDepartamento, ISNULL(RU.ru_descripcion,rub.ru_descripcion)ru_descripcion, per.pe_nombreCompleto, rub.rub_provision, rub.rub_ContPorEmpleado, isnull(emp.IdSucursalContabilizacion, 
                  r.IdSucursal) IdSucursalContabilizacion, su.Su_Descripcion
FROM     dbo.ro_rol AS r INNER JOIN
                  dbo.ro_rol_detalle AS r_det ON r.IdEmpresa = r_det.IdEmpresa AND r.IdRol = r_det.IdRol INNER JOIN
                  dbo.ro_rubro_tipo AS rub ON r_det.IdEmpresa = rub.IdEmpresa AND r_det.IdRubro = rub.IdRubro INNER JOIN
                  dbo.ro_empleado AS emp ON r_det.IdEmpresa = emp.IdEmpresa AND r_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                  dbo.tb_persona AS per ON emp.IdPersona = per.IdPersona LEFT OUTER JOIN
                  dbo.ct_plancta AS ct_plancta_1 ON emp.IdCtaCble_Emplea = ct_plancta_1.IdCtaCble AND emp.IdEmpresa = ct_plancta_1.IdEmpresa LEFT OUTER JOIN
                  dbo.ct_plancta ON rub.rub_ctacon = dbo.ct_plancta.IdCtaCble AND rub.IdEmpresa = dbo.ct_plancta.IdEmpresa LEFT JOIN
                  tb_sucursal AS su ON su.IdEmpresa = emp.IdEmpresa AND su.IdSucursal = isnull(emp.IdSucursalContabilizacion, r.IdSucursal) LEFT JOIN
				  vwro_empleado_x_jornada_x_rubro_cont AS X ON r_det.IdEmpresa = X.IdEmpresa AND r_det.IdEmpleado = X.IdEmpleado AND r_det.IdRubro = X.IdRubro LEFT JOIN
				  ro_rubro_tipo AS RU ON RU.IdEmpresa = X.IdEmpresa AND RU.IdRubro = X.IdRubroContabilizacion
WHERE  (rub.rub_nocontab = 1) AND (emp.Tiene_ingresos_compartidos = 0)
UNION ALL
/* empleados que tienen sueldos compartidos y rubros que se distribuyen*/ SELECT rol.IdEmpresa, rol.IdRol, rol.IdSucursal, rol.IdNominaTipo, rol.IdNominaTipoLiqui, rol.IdPeriodo, rol.IdEmpleado, rol.IdRubro, rol.Valor * (ing_comp.Porcentaje / 100) 
                  AS Expr1, rol.ru_tipo, rol.rub_ctacon, dbo.ct_plancta.pc_Cuenta, rol.IdCtaCble_Emplea, ct_plancta_1.pc_Cuenta AS Expr2, rol.IdDivision, ing_comp.IdArea, rol.IdDepartamento, rol.ru_descripcion, rol.pe_nombreCompleto, rol.rub_provision, 
                  rol.rub_ContPorEmpleado, rol.IdSucursalContabilizacion, rol.Su_Descripcion
FROM     (SELECT r.IdEmpresa, r.IdRol, r.IdSucursal, r.IdNominaTipo, r.IdNominaTipoLiqui, r.IdPeriodo, r_det.IdEmpleado, r_det.IdRubro, r_det.Valor, rub.ru_tipo, rub.rub_ctacon, emp.IdCtaCble_Emplea, emp.IdDivision, emp.IdArea, 
                                    emp.IdDepartamento, rub.ru_descripcion, per.pe_nombreCompleto, rub.rub_provision, rub.rub_ContPorEmpleado, rub.se_distribuye, isnull(emp.IdSucursalContabilizacion, r.IdSucursal) IdSucursalContabilizacion, 
                                    Su_Descripcion
                  FROM      dbo.ro_rol AS r INNER JOIN
                                    dbo.ro_rol_detalle AS r_det ON r.IdEmpresa = r_det.IdEmpresa AND r.IdRol = r_det.IdRol INNER JOIN
                                    dbo.ro_rubro_tipo AS rub ON r_det.IdEmpresa = rub.IdEmpresa AND r_det.IdRubro = rub.IdRubro INNER JOIN
                                    dbo.ro_empleado AS emp ON r_det.IdEmpresa = emp.IdEmpresa AND r_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                                    dbo.tb_persona AS per ON emp.IdPersona = per.IdPersona LEFT JOIN
                                    tb_sucursal AS su ON su.IdEmpresa = emp.IdEmpresa AND su.IdSucursal = isnull(emp.IdSucursalContabilizacion, r.IdSucursal)
                  WHERE   (rub.rub_nocontab = 1) AND (emp.Tiene_ingresos_compartidos = 1) AND (rub.se_distribuye = 1)) AS rol INNER JOIN
                      (SELECT emp_x_are_xrol.IdEmpresa, emp_x_are_xrol.IdRol, emp_x_are_xrol.Secuencia, emp_x_are_xrol.IdEmpleado, emp_x_are_xrol.IDividion, emp_x_are_xrol.IdArea, emp_x_are_xrol.Porcentaje, area.Descripcion, 
                                         dbo.ro_empleado_x_division_x_area.CargaGasto
                       FROM      dbo.ro_empleado_division_area_x_rol AS emp_x_are_xrol INNER JOIN
                                         dbo.ro_area AS area ON emp_x_are_xrol.IdEmpresa = area.IdEmpresa AND emp_x_are_xrol.IDividion = area.IdDivision AND emp_x_are_xrol.IdArea = area.IdArea INNER JOIN
                                         dbo.ro_empleado_x_division_x_area ON area.IdEmpresa = dbo.ro_empleado_x_division_x_area.IdEmpresa AND area.IdDivision = dbo.ro_empleado_x_division_x_area.IDividion AND 
                                         area.IdArea = dbo.ro_empleado_x_division_x_area.IdArea AND emp_x_are_xrol.IdEmpleado = dbo.ro_empleado_x_division_x_area.IdEmpleado) AS ing_comp ON rol.IdEmpresa = ing_comp.IdEmpresa AND 
                  rol.IdRol = ing_comp.IdRol AND rol.IdEmpleado = ing_comp.IdEmpleado LEFT OUTER JOIN
                  dbo.ct_plancta AS ct_plancta_1 ON rol.IdCtaCble_Emplea = ct_plancta_1.IdCtaCble AND rol.IdEmpresa = ct_plancta_1.IdEmpresa LEFT OUTER JOIN
                  dbo.ct_plancta ON rol.rub_ctacon = dbo.ct_plancta.IdCtaCble AND rol.IdEmpresa = dbo.ct_plancta.IdEmpresa
UNION ALL
/* empleados que tienen sueldos compartidos y rubros que no se distribuyen*/ SELECT r.IdEmpresa, r.IdRol, r.IdSucursal, r.IdNominaTipo, r.IdNominaTipoLiqui, r.IdPeriodo, r_det.IdEmpleado, r_det.IdRubro, r_det.Valor, rub.ru_tipo, rub.rub_ctacon, 
                  dbo.ct_plancta.pc_Cuenta, emp.IdCtaCble_Emplea, ct_plancta_1.pc_Cuenta, emp.IdDivision, emp.IdArea, emp.IdDepartamento, rub.ru_descripcion, per.pe_nombreCompleto, rub.rub_provision, rub.rub_ContPorEmpleado, 
                  isnull(emp.IdSucursalContabilizacion, r.IdSucursal) IdSucursalContabilizacion, su.Su_Descripcion
FROM     dbo.ro_rol AS r INNER JOIN
                  dbo.ro_rol_detalle AS r_det ON r.IdEmpresa = r_det.IdEmpresa AND r.IdRol = r_det.IdRol INNER JOIN
                  dbo.ro_rubro_tipo AS rub ON r_det.IdEmpresa = rub.IdEmpresa AND r_det.IdRubro = rub.IdRubro INNER JOIN
                  dbo.ro_empleado AS emp ON r_det.IdEmpresa = emp.IdEmpresa AND r_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                  dbo.tb_persona AS per ON emp.IdPersona = per.IdPersona LEFT OUTER JOIN
                  dbo.ct_plancta AS ct_plancta_1 ON emp.IdEmpresa = ct_plancta_1.IdEmpresa AND emp.IdCtaCble_Emplea = ct_plancta_1.IdCtaCble LEFT OUTER JOIN
                  dbo.ct_plancta ON rub.IdEmpresa = dbo.ct_plancta.IdEmpresa AND rub.rub_ctacon = dbo.ct_plancta.IdCtaCble LEFT JOIN
                  tb_sucursal AS su ON su.IdEmpresa = emp.IdEmpresa AND su.IdSucursal = isnull(emp.IdSucursalContabilizacion, r.IdSucursal)
WHERE  (rub.rub_nocontab = 1) AND (emp.Tiene_ingresos_compartidos = 1) AND (rub.se_distribuye = 0)
UNION ALL
/*Rubros acumulados*/ SELECT r.IdEmpresa, r.IdRol, r.IdSucursal, r.IdNominaTipo, r.IdNominaTipoLiqui, r.IdPeriodo, r_det.IdEmpleado, ISNULL(X.IdRubroContabilizacion, r_det.IdRubro) IdRubro, CASE WHEN X.IdEmpleado IS NOT NULL AND X.Secuencia = X.CantJornada 
THEN  r_det.Valor - (ROUND(r_det.Valor/X.CantJornada,2) * (X.CantJornada-1)) 
WHEN X.IdEmpleado IS NULL THEN r_det.Valor
ELSE ROUND(r_det.Valor/X.CantJornada,2) 
END AS Valor, rub.ru_tipo, rub.rub_ctacon, dbo.ct_plancta.pc_Cuenta AS pc_CuentaRubro, 
                  emp.IdCtaCble_Emplea, ct_plancta_1.pc_Cuenta AS pc_CuentaEmple, emp.IdDivision, emp.IdArea, emp.IdDepartamento, ISNULL(RU.ru_descripcion,rub.ru_descripcion)ru_descripcion, per.pe_nombreCompleto, rub.rub_provision, rub.rub_ContPorEmpleado, 
                  isnull(emp.IdSucursalContabilizacion, r.IdSucursal) IdSucursalContabilizacion, Su_Descripcion
FROM     dbo.ro_rol AS r INNER JOIN
                  dbo.ro_rol_detalle_x_rubro_acumulado AS r_det ON r.IdEmpresa = r_det.IdEmpresa AND r.IdRol = r_det.IdRol INNER JOIN
                  dbo.ro_rubro_tipo AS rub ON r_det.IdEmpresa = rub.IdEmpresa AND r_det.IdRubro = rub.IdRubro INNER JOIN
                  dbo.ro_empleado AS emp ON r_det.IdEmpresa = emp.IdEmpresa AND r_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                  dbo.tb_persona AS per ON emp.IdPersona = per.IdPersona LEFT OUTER JOIN
                  dbo.ct_plancta AS ct_plancta_1 ON emp.IdCtaCble_Emplea = ct_plancta_1.IdCtaCble AND emp.IdEmpresa = ct_plancta_1.IdEmpresa LEFT OUTER JOIN
                  dbo.ct_plancta ON rub.rub_ctacon = dbo.ct_plancta.IdCtaCble AND rub.IdEmpresa = dbo.ct_plancta.IdEmpresa LEFT JOIN
                  tb_sucursal AS su ON su.IdEmpresa = emp.IdEmpresa AND su.IdSucursal = isnull(emp.IdSucursalContabilizacion, r.IdSucursal) LEFT JOIN
				  vwro_empleado_x_jornada_x_rubro_cont AS X ON r_det.IdEmpresa = X.IdEmpresa AND r_det.IdEmpleado = X.IdEmpleado AND r_det.IdRubro = X.IdRubro LEFT JOIN
				  ro_rubro_tipo AS RU ON RU.IdEmpresa = X.IdEmpresa AND RU.IdRubro = X.IdRubroContabilizacion
WHERE  (rub.rub_nocontab = 1) AND (emp.Tiene_ingresos_compartidos = 0)
UNION ALL
/* empleados que tienen sueldos compartidos y rubros que se distribuyen*/ SELECT rol.IdEmpresa, rol.IdRol, rol.IdSucursal, rol.IdNominaTipo, rol.IdNominaTipoLiqui, rol.IdPeriodo, rol.IdEmpleado, rol.IdRubro, rol.Valor * (ing_comp.Porcentaje / 100) 
                  AS Expr1, rol.ru_tipo, rol.rub_ctacon, dbo.ct_plancta.pc_Cuenta, rol.IdCtaCble_Emplea, ct_plancta_1.pc_Cuenta AS Expr2, rol.IdDivision, ing_comp.IdArea, rol.IdDepartamento, rol.ru_descripcion, rol.pe_nombreCompleto, rol.rub_provision, 
                  rol.rub_ContPorEmpleado, rol.IdSucursalContabilizacion, Su_Descripcion
FROM     (SELECT r.IdEmpresa, r.IdRol, r.IdSucursal, r.IdNominaTipo, r.IdNominaTipoLiqui, r.IdPeriodo, r_det.IdEmpleado, r_det.IdRubro, r_det.Valor, rub.ru_tipo, rub.rub_ctacon, emp.IdCtaCble_Emplea, emp.IdDivision, emp.IdArea, 
                                    emp.IdDepartamento, rub.ru_descripcion, per.pe_nombreCompleto, rub.rub_provision, rub.rub_ContPorEmpleado, rub.se_distribuye, isnull(emp.IdSucursalContabilizacion, r.IdSucursal) IdSucursalContabilizacion, 
                                    Su_Descripcion
                  FROM      dbo.ro_rol AS r INNER JOIN
                                    dbo.ro_rol_detalle_x_rubro_acumulado AS r_det ON r.IdEmpresa = r_det.IdEmpresa AND r.IdRol = r_det.IdRol INNER JOIN
                                    dbo.ro_rubro_tipo AS rub ON r_det.IdEmpresa = rub.IdEmpresa AND r_det.IdRubro = rub.IdRubro INNER JOIN
                                    dbo.ro_empleado AS emp ON r_det.IdEmpresa = emp.IdEmpresa AND r_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                                    dbo.tb_persona AS per ON emp.IdPersona = per.IdPersona LEFT JOIN
                                    tb_sucursal AS su ON su.IdEmpresa = emp.IdEmpresa AND su.IdSucursal = isnull(emp.IdSucursalContabilizacion, r.IdSucursal)
                  WHERE   (rub.rub_nocontab = 1) AND (emp.Tiene_ingresos_compartidos = 1) AND (rub.se_distribuye = 1)) AS rol INNER JOIN
                      (SELECT emp_x_are_xrol.IdEmpresa, emp_x_are_xrol.IdRol, emp_x_are_xrol.Secuencia, emp_x_are_xrol.IdEmpleado, emp_x_are_xrol.IDividion, emp_x_are_xrol.IdArea, emp_x_are_xrol.Porcentaje, area.Descripcion, 
                                         dbo.ro_empleado_x_division_x_area.CargaGasto
                       FROM      dbo.ro_empleado_division_area_x_rol AS emp_x_are_xrol INNER JOIN
                                         dbo.ro_area AS area ON emp_x_are_xrol.IdEmpresa = area.IdEmpresa AND emp_x_are_xrol.IDividion = area.IdDivision AND emp_x_are_xrol.IdArea = area.IdArea INNER JOIN
                                         dbo.ro_empleado_x_division_x_area ON area.IdEmpresa = dbo.ro_empleado_x_division_x_area.IdEmpresa AND area.IdDivision = dbo.ro_empleado_x_division_x_area.IDividion AND 
                                         area.IdArea = dbo.ro_empleado_x_division_x_area.IdArea AND emp_x_are_xrol.IdEmpleado = dbo.ro_empleado_x_division_x_area.IdEmpleado) AS ing_comp ON rol.IdEmpresa = ing_comp.IdEmpresa AND 
                  rol.IdRol = ing_comp.IdRol AND rol.IdEmpleado = ing_comp.IdEmpleado LEFT OUTER JOIN
                  dbo.ct_plancta AS ct_plancta_1 ON rol.IdCtaCble_Emplea = ct_plancta_1.IdCtaCble AND rol.IdEmpresa = ct_plancta_1.IdEmpresa LEFT OUTER JOIN
                  dbo.ct_plancta ON rol.rub_ctacon = dbo.ct_plancta.IdCtaCble AND rol.IdEmpresa = dbo.ct_plancta.IdEmpresa
UNION ALL
/* empleados que tienen sueldos compartidos y rubros que no se distribuyen*/ SELECT r.IdEmpresa, r.IdRol, r.IdSucursal, r.IdNominaTipo, r.IdNominaTipoLiqui, r.IdPeriodo, r_det.IdEmpleado, r_det.IdRubro, r_det.Valor, rub.ru_tipo, rub.rub_ctacon, 
                  dbo.ct_plancta.pc_Cuenta, emp.IdCtaCble_Emplea, ct_plancta_1.pc_Cuenta, emp.IdDivision, emp.IdArea, emp.IdDepartamento, rub.ru_descripcion, per.pe_nombreCompleto, rub.rub_provision, rub.rub_ContPorEmpleado, 
                  isnull(emp.IdSucursalContabilizacion, r.IdSucursal) IdSucursalContabilizacion, Su_Descripcion
FROM     dbo.ro_rol AS r INNER JOIN
                  dbo.ro_rol_detalle_x_rubro_acumulado AS r_det ON r.IdEmpresa = r_det.IdEmpresa AND r.IdRol = r_det.IdRol INNER JOIN
                  dbo.ro_rubro_tipo AS rub ON r_det.IdEmpresa = rub.IdEmpresa AND r_det.IdRubro = rub.IdRubro INNER JOIN
                  dbo.ro_empleado AS emp ON r_det.IdEmpresa = emp.IdEmpresa AND r_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                  dbo.tb_persona AS per ON emp.IdPersona = per.IdPersona LEFT OUTER JOIN
                  dbo.ct_plancta AS ct_plancta_1 ON emp.IdEmpresa = ct_plancta_1.IdEmpresa AND emp.IdCtaCble_Emplea = ct_plancta_1.IdCtaCble LEFT OUTER JOIN
                  dbo.ct_plancta ON rub.IdEmpresa = dbo.ct_plancta.IdEmpresa AND rub.rub_ctacon = dbo.ct_plancta.IdCtaCble LEFT JOIN
                  tb_sucursal AS su ON su.IdEmpresa = emp.IdEmpresa AND su.IdSucursal = isnull(emp.IdSucursalContabilizacion, r.IdSucursal)
WHERE  (rub.rub_nocontab = 1) AND (emp.Tiene_ingresos_compartidos = 1) AND (rub.se_distribuye = 0)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_rol_detalle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'd
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_rol_detalle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[86] 4[5] 2[5] 3) )"
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
         Begin Table = "r"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 230
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "r_det"
            Begin Extent = 
               Top = 6
               Left = 268
               Bottom = 136
               Right = 456
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "rub"
            Begin Extent = 
               Top = 56
               Left = 731
               Bottom = 460
               Right = 949
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "emp"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 327
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "per"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 270
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
En', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_rol_detalle';

