create view web.VWROL_006 AS
						 SELECT        dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_cedulaRuc, dbo.ro_empleado.IdEmpresa, dbo.ro_empleado.IdEmpleado, dbo.ro_Solicitud_Vacaciones_x_empleado.IdSolicitud, 
                         dbo.ro_Solicitud_Vacaciones_x_empleado.Anio_Desde, dbo.ro_Solicitud_Vacaciones_x_empleado.Anio_Hasta, dbo.ro_Solicitud_Vacaciones_x_empleado.Dias_pendiente, 
                         dbo.ro_Solicitud_Vacaciones_x_empleado.Dias_a_disfrutar, dbo.ro_Solicitud_Vacaciones_x_empleado.Dias_q_Corresponde, dbo.ro_Solicitud_Vacaciones_x_empleado.AnioServicio, 
                         dbo.ro_Solicitud_Vacaciones_x_empleado.Fecha, dbo.ro_Solicitud_Vacaciones_x_empleado.Fecha_Desde, dbo.ro_Solicitud_Vacaciones_x_empleado.Fecha_Hasta, dbo.ro_Solicitud_Vacaciones_x_empleado.Fecha_Retorno, 
                         dbo.ro_Solicitud_Vacaciones_x_empleado.Observacion, dbo.ro_Departamento.de_descripcion, dbo.ro_Solicitud_Vacaciones_x_empleado.Canceladas, dbo.ro_Solicitud_Vacaciones_x_empleado.Gozadas_Pgadas, 
                         dbo.ro_empleado.em_fechaIngaRol, dbo.ro_cargo.ca_descripcion, dbo.ro_Historico_Liquidacion_Vacaciones.ValorCancelado, dbo.ro_Historico_Liquidacion_Vacaciones.FechaPago, 
                         dbo.ro_Historico_Liquidacion_Vacaciones_Det.Anio, dbo.ro_Historico_Liquidacion_Vacaciones_Det.Mes, dbo.ro_Historico_Liquidacion_Vacaciones_Det.Total_Remuneracion, 
                         dbo.ro_Historico_Liquidacion_Vacaciones_Det.Total_Vacaciones, dbo.ro_Historico_Liquidacion_Vacaciones_Det.Valor_Cancelar, dbo.ro_Historico_Liquidacion_Vacaciones.Iess
FROM            dbo.ro_Solicitud_Vacaciones_x_empleado INNER JOIN
                         dbo.ro_empleado ON dbo.ro_Solicitud_Vacaciones_x_empleado.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_Solicitud_Vacaciones_x_empleado.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_cargo ON dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa AND dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo INNER JOIN
                         dbo.ro_Historico_Liquidacion_Vacaciones ON dbo.ro_Solicitud_Vacaciones_x_empleado.IdEmpresa = dbo.ro_Historico_Liquidacion_Vacaciones.IdEmpresa AND 
                         dbo.ro_Solicitud_Vacaciones_x_empleado.IdSolicitud = dbo.ro_Historico_Liquidacion_Vacaciones.IdSolicitud AND 
                         dbo.ro_Solicitud_Vacaciones_x_empleado.IdEmpleado = dbo.ro_Historico_Liquidacion_Vacaciones.IdEmpleado INNER JOIN
                         dbo.ro_Historico_Liquidacion_Vacaciones_Det ON dbo.ro_Historico_Liquidacion_Vacaciones.IdEmpresa = dbo.ro_Historico_Liquidacion_Vacaciones_Det.IdEmpresa AND 
                         dbo.ro_Historico_Liquidacion_Vacaciones.IdEmpleado = dbo.ro_Historico_Liquidacion_Vacaciones_Det.IdEmpleado AND 
                         dbo.ro_Historico_Liquidacion_Vacaciones.IdLiquidacion = dbo.ro_Historico_Liquidacion_Vacaciones_Det.IdLiquidacion