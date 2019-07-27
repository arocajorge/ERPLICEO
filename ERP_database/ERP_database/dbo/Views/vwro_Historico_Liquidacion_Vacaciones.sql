CREATE VIEW vwro_Historico_Liquidacion_Vacaciones as
SELECT        dbo.ro_Solicitud_Vacaciones_x_empleado.IdEmpresa, dbo.ro_Solicitud_Vacaciones_x_empleado.IdEmpleado, dbo.ro_Historico_Liquidacion_Vacaciones.IdLiquidacion, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, 
                         dbo.ro_Historico_Liquidacion_Vacaciones.ValorCancelado, dbo.ro_Historico_Liquidacion_Vacaciones.FechaPago, dbo.ro_Historico_Liquidacion_Vacaciones.Observaciones, 
                         dbo.ro_Solicitud_Vacaciones_x_empleado.Dias_q_Corresponde, dbo.ro_Solicitud_Vacaciones_x_empleado.Dias_a_disfrutar, dbo.ro_Solicitud_Vacaciones_x_empleado.Dias_pendiente, CONVERT(varchar(10), 
                         dbo.ro_Solicitud_Vacaciones_x_empleado.Anio_Desde, 101) + '  AL  ' + CONVERT(varchar(10), dbo.ro_Solicitud_Vacaciones_x_empleado.Anio_Hasta, 103) AS Periodo, dbo.ro_Historico_Liquidacion_Vacaciones.Estado, 
                         dbo.ro_Solicitud_Vacaciones_x_empleado.Anio_Desde, dbo.ro_Solicitud_Vacaciones_x_empleado.Anio_Hasta, dbo.ro_Solicitud_Vacaciones_x_empleado.Fecha_Desde, dbo.ro_Solicitud_Vacaciones_x_empleado.Fecha_Hasta, 
                         dbo.ro_Solicitud_Vacaciones_x_empleado.Fecha_Retorno
FROM            dbo.ro_Historico_Liquidacion_Vacaciones INNER JOIN
                         dbo.ro_empleado ON dbo.ro_Historico_Liquidacion_Vacaciones.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_Historico_Liquidacion_Vacaciones.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_Solicitud_Vacaciones_x_empleado ON dbo.ro_Historico_Liquidacion_Vacaciones.IdEmpresa = dbo.ro_Solicitud_Vacaciones_x_empleado.IdEmpresa AND 
                         dbo.ro_Historico_Liquidacion_Vacaciones.IdEmpleado = dbo.ro_Solicitud_Vacaciones_x_empleado.IdEmpleado AND dbo.ro_Historico_Liquidacion_Vacaciones.IdSolicitud = dbo.ro_Solicitud_Vacaciones_x_empleado.IdSolicitud