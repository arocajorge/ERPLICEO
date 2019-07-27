CREATE VIEW [dbo].[vwRo_HorasExtrasXEmpleado]
AS

select 1 as uno
/*
SELECT        dbo.ro_nomina_x_horas_extras.IdEmpresa, dbo.ro_nomina_x_horas_extras_det.IdEmpleado, dbo.ro_nomina_x_horas_extras.IdNominaTipo, 
                         dbo.ro_nomina_x_horas_extras.IdNominaTipoLiqui, dbo.ro_nomina_x_horas_extras.IdPeriodo, dbo.ro_nomina_x_horas_extras_det.IdCalendario, 
                         dbo.ro_nomina_x_horas_extras.IdTurno, dbo.ro_nomina_x_horas_extras.IdHorario, dbo.ro_nomina_x_horas_extras.FechaRegistro, 
                         dbo.ro_nomina_x_horas_extras.time_entrada1, dbo.ro_nomina_x_horas_extras.time_entrada2, dbo.ro_nomina_x_horas_extras.time_salida1, 
                         dbo.ro_nomina_x_horas_extras.time_salida2, dbo.ro_nomina_x_horas_extras.hora_extra25, dbo.ro_nomina_x_horas_extras.hora_extra50, 
                         dbo.ro_nomina_x_horas_extras.hora_extra100, dbo.ro_nomina_x_horas_extras.hora_atraso, dbo.ro_nomina_x_horas_extras.hora_temprano, 
                         dbo.ro_nomina_x_horas_extras.hora_trabajada, dbo.vwro_empleadoXdepXcargo.cargo, dbo.vwro_empleadoXdepXcargo.departamento, 
                         dbo.vwro_empleadoXdepXcargo.em_estado AS EstadoEmpleado, dbo.vwro_empleadoXdepXcargo.NomCompleto AS NombreCompleto, 
                         dbo.vwro_empleadoXdepXcargo.pe_cedulaRuc AS CedulaRuc, dbo.vwro_empleadoXdepXcargo.IdDivision, dbo.vwro_empleadoXdepXcargo.Apellido, 
                         dbo.vwro_empleadoXdepXcargo.Nombre, dbo.vwro_empleadoXdepXcargo.Sucursal, dbo.vwro_empleadoXdepXcargo.IdSucursal, 
                         dbo.ro_horario.Descripcion AS DescripcionHorario, dbo.ro_nomina_x_horas_extras.es_HorasExtrasAutorizadas
FROM            dbo.vwro_empleadoXdepXcargo INNER JOIN
                         dbo.ro_nomina_x_horas_extras ON dbo.vwro_empleadoXdepXcargo.IdEmpresa = dbo.ro_nomina_x_horas_extras.IdEmpresa AND 
                         dbo.vwro_empleadoXdepXcargo.IdEmpleado = dbo.ro_nomina_x_horas_extras.IdEmpleado INNER JOIN
                         dbo.ro_horario ON dbo.ro_nomina_x_horas_extras.IdEmpresa = dbo.ro_horario.IdEmpresa AND dbo.ro_nomina_x_horas_extras.IdHorario = dbo.ro_horario.IdHorario
						 */