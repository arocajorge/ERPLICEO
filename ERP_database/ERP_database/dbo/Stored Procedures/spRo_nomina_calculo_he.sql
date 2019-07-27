
CREATE PROCEDURE [dbo].[spRo_nomina_calculo_he] (
@IdEmpresa int,
@IdNomina numeric,
@IdNominaTipo numeric,
@IdPEriodo numeric,
@IdUsuario varchar(50),
@observacion varchar(500)
)
AS

--declare
--@IdEmpresa int,
--@IdNomina numeric,
--@IdNominaTipo numeric,
--@IdPEriodo numeric,
--@IdUsuario varchar(50),
--@observacion varchar(500)
--set @IdEmpresa =1
--set @IdNomina =1
--set @IdNominaTipo =2
--set @IdPEriodo= 201805
--set @IdUsuario ='admin'
--set @observacion= 'prueba'

BEGIN

declare
@Fi date,
@Ff date,
@IdHorasExtras int

----------------------------------------------------------------------------------------------------------------------------------------------
-------------obteniendo fecha del perido------------------- ----------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @Fi= pe_FechaIni, @Ff=pe_FechaFin from ro_periodo where IdEmpresa=@IdEmpresa and IdPeriodo=@IdPEriodo

----------------------------------------------------------------------------------------------------------------------------------------------
-------------preparando la cabecera para la nomina-------- ----------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdHorasExtras= isnull(MAX(IdHorasExtras),0)+1 from ro_nomina_x_horas_extras where IdEmpresa=@IdEmpresa 
if((select  COUNT(IdPeriodo) from ro_nomina_x_horas_extras where IdEmpresa=@IdEmpresa and IdPeriodo=@IdPEriodo and IdNominaTipo=@IdNomina and IdNominaTipoLiqui=@IdNominaTipo)>0)
update ro_nomina_x_horas_extras set IdUsuarioUltMod=@IdUsuario, Fecha_UltMod=GETDATE() where IdEmpresa=@IdEmpresa and IdPeriodo=@IdPEriodo and IdNominaTipo=@IdNomina and IdNominaTipoLiqui=@IdNominaTipo
else
insert into ro_nomina_x_horas_extras
(
IdEmpresa,				IdHorasExtras,				IdNominaTipo,				IdNominaTipoLiqui,				IdPeriodo,				Estado,				observacion,		IdUsuario,
Fecha_Transac,			IdUsuarioUltMod,			Fecha_UltMod,				IdUsuarioUltAnu,				Fecha_UltAnu,			nom_pc,				ip,					MotivoAnulacion

)



values
(@IdEmpresa				,@IdHorasExtras				,@IdNomina				,@IdNominaTipo,					@IdPEriodo				,'A',				@observacion		,@IdUsuario		
,GETDATE()				,null						,null						,null							,null					,null				,null				,null
)

----------------------------------------------------------------------------------------------------------------------------------------------
-------------eliminando detalle--------------------------- ----------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
delete ro_nomina_x_horas_extras_det  
FROM   dbo.ro_nomina_x_horas_extras AS he INNER JOIN
       dbo.ro_nomina_x_horas_extras_det AS he_det ON he.IdEmpresa = he_det.IdEmpresa AND he.IdHorasExtras = he_det.IdHorasExtras 
   	   where he.IdEmpresa=@IdEmpresa and he.IdPeriodo=@IdPEriodo and he.IdNominaTipo=@IdNomina and he.IdNominaTipoLiqui=@IdNominaTipo

----------------------------------------------------------------------------------------------------------------------------------------------
-------------insertando empleados activos y que tiene marcaciones en el periodo y planificacion-----------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdHorasExtras= IdHorasExtras from ro_nomina_x_horas_extras  where IdEmpresa=@IdEmpresa and IdPeriodo=@IdPEriodo and IdNominaTipo=@IdNomina and IdNominaTipoLiqui=@IdNominaTipo
insert into ro_nomina_x_horas_extras_det
(
IdEmpresa,					IdHorasExtras,					IdEmpleado,					IdCalendario,					IdTurno,					IdHorario,
FechaRegistro,				time_entrada1,					time_entrada2,				time_salida1,					time_salida2,				hora_extra25,
hora_extra50,				hora_extra100,					Valor25,					Valor50,						Valor100,					Sueldo_base,					
hora_atraso,				hora_temprano,					hora_trabajada,				es_HorasExtrasAutorizadas
)

select 
@IdEmpresa,					@IdHorasExtras,					emp.IdEmpleado,				marc.IdCalendadrio,				1,							emp.IdHorario,
marc.es_fechaRegistro,		marc.time_entrada1,				marc.time_entrada2,			marc.time_salida1,				marc.time_salida2,			emp.hora_extra25,
emp.hora_extra50			,emp.hora_extra100,				emp.Valor25,				emp.Valor50,					emp.valor100,				emp.Sueldo_base,				
emp.hora_atraso			,hora_temprano					,emp.hora_trabajada			,emp.es_HorasExtrasAutorizadas	


from
(
SELECT        emp.IdEmpresa, emp.IdEmpleado, planif.IdCalendario, planif.IdHorario, emp.em_status, emp.em_fechaSalida, cont.FechaInicio,cont.IdNomina,
			  0 hora_extra25, 0 hora_extra50, 0 hora_extra100,0 Valor25, 0 Valor50, 0 valor100, cont.Sueldo Sueldo_base,
			  0 hora_atraso,0 hora_temprano,0 hora_trabajada, 0 es_HorasExtrasAutorizadas
FROM          dbo.ro_empleado AS emp INNER JOIN
              dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado INNER JOIN
              dbo.ro_horario_planificacion_det AS planif ON emp.IdEmpresa = planif.IdEmpresa AND emp.IdEmpleado = planif.IdEmpleado
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
and emp.IdNomina=@IdNomina
and (emp.em_status='EST_ACT' or emp.em_fechaSalida between @Fi and @Ff)
and marc.es_fechaRegistro between @Fi and @Ff

END