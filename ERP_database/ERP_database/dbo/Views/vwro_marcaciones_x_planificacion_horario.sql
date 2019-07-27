create view vwro_marcaciones_x_planificacion_horario as 
select emp.IdEmpresa,emp.IdEmpleado, emp.IdSucursal,emp.IdHorario, marc.IdCalendadrio,emp.Sueldo, emp.IdNomina, emp.pe_nombre, emp.pe_apellido, emp.pe_nombreCompleto,emp.pe_cedulaRuc, emp.em_codigo,  marc.time_entrada1, marc.time_salida1, marc.es_fechaRegistro, emp.HoraIni, emp.HoraFin

from
(
SELECT      emp.IdEmpresa,  hor.HoraIni, hor.HoraFin, emp.em_codigo, per.pe_nombreCompleto, per.pe_apellido, per.pe_nombre,per.pe_cedulaRuc, hor_pla.IdPlanificacion, hor_pla.IdEmpleado, hor_pla.IdCalendario, hor_pla.fecha, emp.IdSucursal,
hor.IdHorario, cont.Sueldo, cont.IdNomina
FROM            dbo.tb_persona AS per INNER JOIN
                         dbo.ro_empleado AS emp ON per.IdPersona = emp.IdPersona INNER JOIN
                         dbo.ro_horario_planificacion_det AS hor_pla INNER JOIN
                         dbo.ro_horario AS hor ON hor_pla.IdEmpresa = hor.IdEmpresa AND hor_pla.IdHorario = hor.IdHorario ON emp.IdEmpresa = hor_pla.IdEmpresa AND emp.IdEmpleado = hor_pla.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado
						 			  and cont.EstadoContrato='ECT_ACT' AND cont.Estado='A'
)
emp
inner join (
(
 SELECT  IdEmpresa,IdEmpleado,IdCalendadrio, es_fechaRegistro, ISNULL( [IN1],'00:00:00')time_entrada1 ,ISNULL([IN2],'00:00:00')time_entrada2,
 ISNULL([OUT1],'00:00:00')time_salida1,ISNULL([OUT2],'00:00:00')time_salida2
FROM (
    SELECT 
        IdEmpresa,IdEmpleado,IdCalendadrio,es_fechaRegistro,IdTipoMarcaciones, es_Hora
    FROM ro_marcaciones_x_empleado
) as s
PIVOT
(
   max([es_Hora])
    FOR [IdTipoMarcaciones] IN ([IN1],[IN2],[OUT1],[OUT2])
)AS pvt)
)
 marc
 on emp.IdEmpresa=marc.IdEmpresa
 and emp.IdEmpleado=marc.IdEmpleado
 AND emp.IdCalendario=marc.IdCalendadrio
--and emp.IdNomina=@IdNomina
--and (emp.em_status='EST_ACT' or emp.em_fechaSalida between @Fi and @Ff)
--and marc.es_fechaRegistro between @Fi and @Ff
