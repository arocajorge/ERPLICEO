
create view  web.VWROL_011 as
SELECT        dbo.ro_nomina_x_horas_extras.IdEmpresa, dbo.ro_nomina_x_horas_extras.IdHorasExtras, dbo.ro_nomina_x_horas_extras.IdNominaTipo, dbo.ro_nomina_x_horas_extras.IdNominaTipoLiqui, 
                         dbo.ro_nomina_x_horas_extras.IdPeriodo, dbo.ro_nomina_x_horas_extras_det.IdEmpleado, dbo.ro_nomina_x_horas_extras_det.FechaRegistro, dbo.ro_nomina_x_horas_extras_det.time_entrada1, 
                         dbo.ro_nomina_x_horas_extras_det.time_entrada2, dbo.ro_nomina_x_horas_extras_det.time_salida1, dbo.ro_nomina_x_horas_extras_det.time_salida2, dbo.ro_nomina_x_horas_extras_det.hora_extra25, 
                         dbo.ro_nomina_x_horas_extras_det.hora_extra50, dbo.ro_nomina_x_horas_extras_det.hora_extra100, dbo.ro_nomina_x_horas_extras_det.Valor25, dbo.ro_nomina_x_horas_extras_det.Valor50, 
                         dbo.ro_nomina_x_horas_extras_det.Valor100, dbo.ro_nomina_x_horas_extras_det.Sueldo_base, dbo.ro_nomina_x_horas_extras_det.hora_atraso, dbo.ro_nomina_x_horas_extras_det.hora_temprano, 
                         dbo.ro_nomina_x_horas_extras_det.hora_trabajada, dbo.ro_periodo.pe_FechaIni, dbo.ro_periodo.pe_FechaFin, dbo.ro_Nomina_Tipo.Descripcion, dbo.ro_Nomina_Tipoliqui.DescripcionProcesoNomina, 
                         dbo.ro_cargo.ca_descripcion, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_nombreCompleto
FROM            dbo.ro_periodo_x_ro_Nomina_TipoLiqui INNER JOIN
                         dbo.ro_nomina_x_horas_extras INNER JOIN
                         dbo.ro_nomina_x_horas_extras_det ON dbo.ro_nomina_x_horas_extras.IdEmpresa = dbo.ro_nomina_x_horas_extras_det.IdEmpresa AND 
                         dbo.ro_nomina_x_horas_extras.IdHorasExtras = dbo.ro_nomina_x_horas_extras_det.IdHorasExtras INNER JOIN
                         dbo.ro_empleado ON dbo.ro_nomina_x_horas_extras_det.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_nomina_x_horas_extras_det.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona ON dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa = dbo.ro_nomina_x_horas_extras.IdEmpresa AND 
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_Tipo = dbo.ro_nomina_x_horas_extras.IdNominaTipo AND 
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_TipoLiqui = dbo.ro_nomina_x_horas_extras.IdNominaTipoLiqui AND dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo = dbo.ro_nomina_x_horas_extras.IdPeriodo INNER JOIN
                         dbo.ro_periodo ON dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa = dbo.ro_periodo.IdEmpresa AND dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo = dbo.ro_periodo.IdPeriodo INNER JOIN
                         dbo.ro_Nomina_Tipoliqui INNER JOIN
                         dbo.ro_Nomina_Tipo ON dbo.ro_Nomina_Tipoliqui.IdEmpresa = dbo.ro_Nomina_Tipo.IdEmpresa AND dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo = dbo.ro_Nomina_Tipo.IdNomina_Tipo ON 
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa = dbo.ro_Nomina_Tipoliqui.IdEmpresa AND dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_Tipo = dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo AND 
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_TipoLiqui = dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui INNER JOIN
                         dbo.ro_cargo ON dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa AND dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo