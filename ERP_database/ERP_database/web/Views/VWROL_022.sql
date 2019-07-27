CREATE view web.VWROL_022 as
SELECT        ing_comp.IdEmpresa, ing_comp.IdSucursal, ing_comp.IdNominaTipo, ing_comp.IdNominaTipoLiqui, ing_comp.IdEmpleado, ing_comp.IdArea, ing_comp.IDividion AS IdDivision, ing_comp.IdAreaEmpleado, 
                         area.Descripcion AS AreaEmpleado, ing_comp.IdPeriodo, ing_comp.IdRubro, ing_comp.se_distribuye, ing_comp.Orden, ing_comp.Porcentaje, ing_comp.Valor, ing_comp.rub_visible_reporte, ing_comp.Observacion, 
                         ing_comp.ru_descripcion, ing_comp.pe_FechaIni, pe_FechaFin, ing_comp.ru_tipo, ing_comp.rub_codigo, ing_comp.ru_codRolGen, ing_comp.ca_descripcion, ing_comp.em_codigo, ing_comp.pe_cedulaRuc, 
                         ing_comp.pe_nombreCompleto, ing_comp.IdRol, ing_comp.Descripcion, ing_comp.rub_grupo, ing_comp.Dias
FROM            (SELECT        rol_det.IdEmpresa, rol.IdSucursal, rol.IdNominaTipo, rol.IdNominaTipoLiqui, rol_det.IdEmpleado, emp_div.IdArea, emp_div.IDividion, dbo.ro_empleado.IdArea AS IdAreaEmpleado, rol.IdPeriodo, rol_det.IdRubro, 
                                                    rubr.se_distribuye, rubr.ru_orden AS Orden, emp_div.Porcentaje, 
													
													
													CASE WHEN rubr.ru_tipo = 'I' and rubr.se_distribuye=1 THEN (rol_det.Valor * emp_div.Porcentaje) / 100 ELSE rol_det.Valor END  AS Valor, 
													
													rol_det.rub_visible_reporte, rol_det.Observacion, CASE WHEN (rubr.se_distribuye = 1 AND (rub_cal.IdRubro_sueldo = rol_det.IdRubro OR
                                                    rub_cal.IdRubro_horas_matutina = rol_det.IdRubro OR
                                                    rub_cal.IdRubro_horas_vespertina = rol_det.IdRubro)) THEN 'SUELDOS POR HORA' ELSE rubr.ru_descripcion END AS ru_descripcion, perid.pe_FechaIni, perid.pe_FechaFin, rubr.ru_tipo, rubr.rub_codigo, 
                                                    rubr.ru_codRolGen, CASE WHEN ru_tipo = 'A' THEN '3 - TOTALES' WHEN ru_tipo = 'I' THEN '1 - INGRESOS' ELSE '2 - EGRESOS' END AS ca_descripcion, dbo.ro_empleado.em_codigo, 
                                                    
													 dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_nombreCompleto, rol_det.IdRol, CASE WHEN rubr.ru_tipo in ('I') and rubr.se_distribuye=1 THEN dbo.ro_area.Descripcion + '-' + CAST(emp_div.Porcentaje AS varchar) ELSE '' END Descripcion,
                                                    
													cat.ca_descripcion AS rub_grupo, d .Dias
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
                                                    ro_rubros_calculados rub_cal ON rub_cal.IdEmpresa = dbo.ro_area.IdEmpresa LEFT OUTER JOIN
                                                        (SELECT        det.IdEmpresa, det.IdRol, det.IdEmpleado, det.Valor AS Dias
                                                          FROM            dbo.ro_rol_detalle AS det INNER JOIN
                                                                                    dbo.ro_rubros_calculados AS p ON p.IdEmpresa = det.IdEmpresa AND det.IdRubro = p.IdRubro_dias_trabajados) AS d ON d .IdEmpresa = rol_det.IdEmpresa AND d .IdRol = rol_det.IdRol AND 
                                                    d .IdEmpleado = rol_det.IdEmpleado
                          WHERE        ro_empleado.Tiene_ingresos_compartidos = 1) AS ing_comp LEFT OUTER JOIN
                         ro_area area ON area.IdEmpresa = ing_comp.IdEmpresa AND area.IdArea = ing_comp.IdAreaEmpleado
UNION ALL
SELECT        rol_det.IdEmpresa, rol.IdSucursal, rol.IdNominaTipo, rol.IdNominaTipoLiqui, rol_det.IdEmpleado, dbo.ro_empleado.IdArea, dbo.ro_empleado.IdDivision, dbo.ro_empleado.IdArea, dbo.ro_area.Descripcion, rol.IdPeriodo, 
                         rol_det.IdRubro, rubr.se_distribuye, rubr.ru_orden AS Orden, 0 AS Expr1, rol_det.Valor, rol_det.rub_visible_reporte, rol_det.Observacion, rubr.ru_descripcion, perid.pe_FechaIni, perid.pe_FechaFin, rubr.ru_tipo, rubr.rub_codigo, 
                         rubr.ru_codRolGen, CASE WHEN ru_tipo = 'A' THEN '3 - TOTALES' WHEN ru_tipo = 'I' THEN '1 - INGRESOS' ELSE '2 - EGRESOS' END AS ca_descripcion, dbo.ro_empleado.em_codigo, dbo.tb_persona.pe_cedulaRuc, 
                         dbo.tb_persona.pe_nombreCompleto, rol_det.IdRol, '' Descripcion, cat.ca_descripcion AS rub_grupo, d .Dias
FROM            dbo.ro_rol AS rol INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                         dbo.ro_periodo AS perid ON pe_x_nom.IdEmpresa = perid.IdEmpresa AND pe_x_nom.IdPeriodo = perid.IdPeriodo INNER JOIN
                         dbo.ro_rubro_tipo AS rubr INNER JOIN
                         dbo.ro_rol_detalle AS rol_det ON rubr.IdEmpresa = rol_det.IdEmpresa AND rubr.IdRubro = rol_det.IdRubro ON rol.IdEmpresa = rol_det.IdEmpresa AND rol.IdRol = rol_det.IdRol LEFT OUTER JOIN
                         dbo.ro_area INNER JOIN
                         dbo.ro_empleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona ON dbo.ro_area.IdArea = dbo.ro_empleado.IdArea AND dbo.ro_area.IdEmpresa = dbo.ro_empleado.IdEmpresa ON 
                         rol_det.IdEmpresa = dbo.ro_empleado.IdEmpresa AND rol_det.IdEmpleado = dbo.ro_empleado.IdEmpleado LEFT OUTER JOIN
                         dbo.ro_catalogo AS cat ON rubr.rub_grupo = cat.CodCatalogo LEFT OUTER JOIN
                             (SELECT        det.IdEmpresa, det.IdRol, det.IdEmpleado, det.Valor AS Dias
                               FROM            dbo.ro_rol_detalle AS det INNER JOIN
                                                         dbo.ro_rubros_calculados AS p ON p.IdEmpresa = det.IdEmpresa AND det.IdRubro = p.IdRubro_dias_trabajados) AS d ON d .IdEmpresa = rol_det.IdEmpresa AND d .IdRol = rol_det.IdRol AND 
                         d .IdEmpleado = rol_det.IdEmpleado
WHERE        (dbo.ro_empleado.Tiene_ingresos_compartidos = 0)