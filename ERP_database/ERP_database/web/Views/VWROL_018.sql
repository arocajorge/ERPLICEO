create view web.VWROL_018 as
SELECT        hist_liq.IdEmpresa, hist_liq.IdEmpleado, hist_liq.IdLiquidacion, his_vac.IdPeriodo_Inicio, his_vac.IdPeriodo_Fin, his_vac.FechaIni, his_vac.FechaFin, sol_vac.Dias_q_Corresponde, sol_vac.Dias_a_disfrutar, 
                         sol_vac.Dias_pendiente,SUM( hist_liq_det.Total_Remuneracion)Total_Remuneracion,SUM( hist_liq_det.Total_Vacaciones)Total_Vacaciones,SUM( hist_liq_det.Valor_Cancelar)Valor_Cancelar, per.pe_cedulaRuc, per.pe_nombreCompleto, hist_liq.FechaPago, sol_vac.Fecha_Desde, sol_vac.Fecha_Hasta
FROM            dbo.ro_Historico_Liquidacion_Vacaciones AS hist_liq INNER JOIN
                         dbo.ro_Historico_Liquidacion_Vacaciones_Det AS hist_liq_det ON hist_liq.IdEmpresa = hist_liq_det.IdEmpresa AND hist_liq.IdEmpleado = hist_liq_det.IdEmpleado AND 
                         hist_liq.IdLiquidacion = hist_liq_det.IdLiquidacion INNER JOIN
                         dbo.ro_Solicitud_Vacaciones_x_empleado AS sol_vac ON hist_liq.IdEmpresa = sol_vac.IdEmpresa AND hist_liq.IdEmpleado = sol_vac.IdEmpleado AND hist_liq.IdSolicitud = sol_vac.IdSolicitud INNER JOIN
                         dbo.ro_historico_vacaciones_x_empleado AS his_vac ON sol_vac.IdEmpresa = his_vac.IdEmpresa AND sol_vac.IdEmpleado = his_vac.IdEmpleado AND sol_vac.IdVacacion = his_vac.IdVacacion INNER JOIN
                         dbo.ro_empleado AS emp ON hist_liq.IdEmpresa = emp.IdEmpresa AND hist_liq.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.tb_persona AS per ON emp.IdPersona = per.IdPersona
group by
                                          hist_liq.IdEmpresa, hist_liq.IdEmpleado, hist_liq.IdLiquidacion, his_vac.IdPeriodo_Inicio, his_vac.IdPeriodo_Fin, his_vac.FechaIni, his_vac.FechaFin, sol_vac.Dias_q_Corresponde, sol_vac.Dias_a_disfrutar, 
                         sol_vac.Dias_pendiente, per.pe_cedulaRuc, per.pe_nombreCompleto, hist_liq.FechaPago, sol_vac.Fecha_Desde, sol_vac.Fecha_Hasta


