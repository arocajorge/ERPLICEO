
--EXEC [dbo].[spRo_procesa_Rol] 1,1,2,201904,'ADMIN','',24,1,1

CREATE PROCEDURE [dbo].[spRo_procesa_Rol] (
@IdEmpresa int,
@IdNomina numeric,
@IdNominaTipo numeric,
@IdPEriodo numeric,
@IdUsuario varchar(50),
@Observacion varchar(500),
@IdRol int,
@IdSucursalInicio int,
@IdSucursalFin int
)
AS
begin
--declare
--@IdEmpresa int,
--@IdNomina numeric,
--@IdNominaTipo numeric,
--@IdPEriodo numeric,
--@IdUsuario varchar(50),
--@observacion varchar(500),
--@IdRol int,
--@IdSucursalFin int,
--@IdSucursalInicio int

--set @IdEmpresa =1
--set @IdNomina =1
--set @IdNominaTipo =2
--set @IdPEriodo= 201902
--set @IdUsuario ='admin'
--set @observacion= 'PERIODO'+CAST( @IdPEriodo AS varchar(15))
--set @IdRol =1
--set @IdSucursalFin=1
--set @IdSucursalInicio=1


BEGIN -- variables
declare
@Fi date,
@Ff date,
@IdRubro_calculado varchar(50),
@Dias_trabajados int,
@Anio float,
@SueldoBasico float,
@Por_apor_pers_iess float,
@por_apor_per_patr float,
@por_apor_fnd float,
@IdSucursal int,
@IdRubro_Provision varchar(50),
@IdRubro_total_ingreso varchar(50),
@IdRubro_total_egreso varchar(50),
@IdRubro_total_pagar varchar(50),
@IdRubro_anticipo varchar(50),
@PorAportePersonal float,
@SalarioBasico float,

@IdRubro_DIII varchar(50),
@IdRubro_DIV varchar(50),
@IdRubro_ProvDIII varchar(50),
@IdRubro_ProvDIV varchar(50),
@IdRubro_FondoReserva varchar(50),
@IdRubro_PagoCheque varchar(50)

end

select @SueldoBasico= Sueldo_basico,@Por_apor_pers_iess= Porcentaje_aporte_pers, @por_apor_per_patr=Porcentaje_aporte_patr from ro_Parametros where IdEmpresa=@IdEmpresa
----------------------------------------------------------------------------------------------------------------------------------------------
-------------obteniendo fecha del perido------------------- ----------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @Fi= pe_FechaIni, @Ff=pe_FechaFin, @Anio=pe_anio from ro_periodo where IdEmpresa=@IdEmpresa and IdPeriodo=@IdPEriodo
-------------obteniendo aporte personal------------------- ----------------------------------------------------------------------------------<
select @PorAportePersonal = Porcentaje_aporte_pers, @SalarioBasico = Sueldo_basico from ro_Parametros where IdEmpresa=@IdEmpresa
----------------------------------------------------------------------------------------------------------------------------------------------
-------------preparando la cabecera del rol general-------- ----------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
if((select  COUNT(idrol) from ro_rol where IdEmpresa=@IdEmpresa and @IdRol=IdRol)>0)
update ro_rol set PorAportePersonal=@PorAportePersonal, SalarioBasico=@SalarioBasico, UsuarioModifica=@IdUsuario, FechaModifica=GETDATE() where IdEmpresa=@IdEmpresa and @IdRol=IdRol
else
insert into ro_rol
(IdEmpresa,	IdRol, IdSucursal,	IdNominaTipo,		IdNominaTipoLiqui,		IdPeriodo,			Descripcion,				Observacion,				Cerrado,			FechaIngresa,
UsuarioIngresa,	FechaModifica,		UsuarioModifica,		FechaAnula,			UsuarioAnula,				MotivoAnula,				UsuarioCierre,		FechaCierre,
IdCentroCosto, PorAportePersonal, SalarioBasico)
select
 @IdEmpresa	, @IdRol, case when @IdSucursalInicio=0then NULL else @IdSucursalInicio end	,@IdNomina			,@IdNominaTipo			,@IdPEriodo			,@observacion				,@observacion				,'ABIERTO'				,GETDATE()
,@IdUsuario		,null				,null					,null				,null						,null						,null				,null
,null, @PorAportePersonal, @SalarioBasico

----------------------------------------------------------------------------------------------------------------------------------------------
-------------eliminando detalle--------------------------- ----------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
delete ro_rol_detalle_x_rubro_acumulado  where IdEmpresa=@IdEmpresa and IdRol=@IdRol
delete ro_rol_x_empleado_novedades where IdEmpresa=@IdEmpresa and IdRol=@IdRol
delete ro_rol_x_prestamo_detalle where IdEmpresa=@IdEmpresa and IdRol=@IdRol
delete ro_rol_detalle where ro_rol_detalle.IdEmpresa=@IdEmpresa and @IdRol=IdRol
delete ro_empleado_division_area_x_rol where IdEmpresa=@IdEmpresa and IdRol= @IdRol 

----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando dias trabajados por empleado-----------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------

select @IdRubro_calculado= IdRubro_dias_trabajados from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros

insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion			)

select 

@IdEmpresa				,@IdRol				,emp.IdSucursal			,cont.IdEmpleado		,@IdRubro_calculado	,'0' ,  dbo.calcular_dias_trabajados(@Fi,@Ff,emp.em_fechaIngaRol, emp.em_status, emp.em_fechaSalida)
,1						,'Días trabajados'		
FROM            dbo.ro_contrato AS cont INNER JOIN
                dbo.ro_empleado AS emp ON cont.IdEmpresa = emp.IdEmpresa AND cont.IdEmpleado = emp.IdEmpleado
where cont.IdEmpresa=@IdEmpresa 
and cont.IdNomina=@IdNomina
and cont.EstadoContrato<>'ECT_LIQ'
and (emp.em_status<>'EST_LIQ' and isnull( emp.em_fechaSalida, @Ff) between @Fi and @Ff )
and CAST( cont.FechaInicio as date)<=@Ff
and emp.IdSucursal =  @IdSucursalFin


PRINT 'calculando sueldo por días trabajados'
----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando sueldo por días trabajados-------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdRubro_calculado= IdRubro_sueldo from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros

insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select 
@IdEmpresa				,@IdRol,		emp.IdSucursal			,cont.IdEmpleado		,@IdRubro_calculado	,'1' ,
case when emp.Pago_por_horas=0 then (cont.Sueldo/30)* (dbo.calcular_dias_trabajados(@Fi,@Ff,emp.em_fechaIngaRol, emp.em_status, CONT.FechaFin)) 
else 0 
end
,1						,'Sueldo base'		
FROM            dbo.ro_contrato AS cont INNER JOIN
                dbo.ro_empleado AS emp ON cont.IdEmpresa = emp.IdEmpresa AND cont.IdEmpleado = emp.IdEmpleado
where cont.IdEmpresa=@IdEmpresa 
and cont.IdNomina=@IdNomina
and cont.EstadoContrato<>'ECT_LIQ'

and (emp.em_status<>'EST_LIQ' and isnull( emp.em_fechaSalida, @Ff) between @Fi and @Ff )
and CAST( cont.FechaInicio as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin


PRINT 'buscando novedades del periodo e insertando al rol detalle'
----------------------------------------------------------------------------------------------------------------------------------------------
-------------buscando novedades del periodo e insertando al rol detalle-----------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------


insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select 
@IdEmpresa				,@IdRol				,emp.IdSucursal							,novc.IdEmpleado		,nov.IdRubro		,rub.ru_orden	,sum(nov.Valor)
,1						,rub.ru_descripcion +'('+ CAST(  convert(decimal(10, 2), (SUM(ISNULL( nov.CantidadHoras,0)))) as varchar(10))   +')'		
FROM   dbo.ro_empleado AS emp INNER JOIN
dbo.ro_empleado_Novedad AS novc ON emp.IdEmpresa = novc.IdEmpresa AND emp.IdEmpleado = novc.IdEmpleado INNER JOIN
dbo.ro_empleado_novedad_det AS nov ON novc.IdEmpresa = nov.IdEmpresa AND novc.IdNovedad = nov.IdNovedad AND novc.IdEmpleado = novc.IdEmpleado INNER JOIN
dbo.ro_rubro_tipo AS rub ON nov.IdEmpresa = rub.IdEmpresa AND nov.IdRubro = rub.IdRubro
and nov.IdEmpresa=@IdEmpresa
and emp.IdEmpresa=@IdEmpresa
and novc.IdNomina_tipo=@IdNomina
and novc.IdNomina_TipoLiqui=@IdNominaTipo
and nov.FechaPago between @Fi and @Ff
and novc.Estado='A'
and nov.EstadoCobro='PEN'
and (emp.em_status<>'EST_LIQ')
and CAST( emp.em_fechaIngaRol as date)<=@Ff
and ISNULL( emp.em_fechaSalida, @Fi) between @Fi and @Ff
and emp.IdSucursal = @IdSucursalFin
group by novc.IdEmpresa,novc.IdEmpleado,nov.IdRubro,rub.ru_orden,rub.ru_descripcion, emp.IdSucursal, nov.CantidadHoras


PRINT 'buscando cuota de prestamos e insertando al rol detalle'
----------------------------------------------------------------------------------------------------------------------------------------------
-------------buscando cuota de prestamos e insertando al rol detalle-------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select
@IdEmpresa				,@IdRol				,emp.IdSucursal			,pre.IdEmpleado		,pre.IdRubro		,rub.ru_orden	,sum(pred.TotalCuota)
,1						,rub.ru_descripcion
FROM            dbo.ro_prestamo AS pre INNER JOIN
                         dbo.ro_prestamo_detalle AS pred ON pre.IdEmpresa = pred.IdEmpresa AND pre.IdPrestamo = pred.IdPrestamo INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON pre.IdEmpresa = rub.IdEmpresa AND pre.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_empleado AS emp ON pre.IdEmpresa = emp.IdEmpresa AND pre.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado
and pre.IdEmpresa=@IdEmpresa
and emp.IdEmpresa=@IdEmpresa
and pred.IdNominaTipoLiqui=@IdNominaTipo
and CAST(pred.FechaPago AS DATE) between @Fi and @Ff
and pred.Estado=1
and pred.EstadoPago='PEN'
and (emp.em_status <>'EST_LIQ' and isnull( emp.em_fechaSalida, @Ff) between @Fi and @Ff )
and CAST( emp.em_fechaIngaRol as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin
and pred.FechaPago between  @Fi and @Ff 
and pred.IdNominaTipoLiqui=@IdNominaTipo
and cont.IdNomina=@IdNomina
and cont.EstadoContrato <>'ECT_LIQ'
group by pred.IdEmpresa,pre.IdEmpleado,emp.IdSucursal, pre.IdRubro, rub.ru_orden, rub.ru_descripcion
----------------------------------------------------------------------------------------------------------------------------------------------
-------------buscando rubros fijos e insertando al rol detalle-------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------

insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,			IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select
@IdEmpresa				,@IdRol				,emp.IdSucursal			,emp.IdEmpleado		,rub_fij.IdRubro	,rub.ru_orden	,rub_fij.Valor
,1						,rub.ru_descripcion	
FROM            dbo.ro_rubro_tipo AS rub INNER JOIN
                         dbo.ro_empleado_x_ro_rubro AS rub_fij ON rub.IdEmpresa = rub_fij.IdEmpresa AND rub.IdRubro = rub_fij.IdRubro INNER JOIN
                         dbo.ro_empleado AS emp ON rub_fij.IdEmpresa = emp.IdEmpresa AND rub_fij.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado
and rub_fij.IdEmpresa=@IdEmpresa
and emp.IdEmpresa=@IdEmpresa
and rub_fij.IdNomina_tipo=@IdNomina
and rub_fij.IdNomina_TipoLiqui=@IdNominaTipo
--and (rub_fij.es_indifinido=1 or ( @Fi between rub_fij.FechaFin and rub_fij.FechaFin and @Ff between rub_fij.FechaFin and rub_fij.FechaFin))
--and rub_fij.Estado='A'
and (emp.em_status<>'EST_LIQ' and isnull( emp.em_fechaSalida, @Ff) between @Fi and @Ff )
and CAST( emp.em_fechaIngaRol as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin
and cont.IdNomina=@IdNomina
and cont.EstadoContrato<>'ECT_LIQ'
----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando aporte personal------------------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdRubro_calculado= IdRubro_iess_perso from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select
@IdEmpresa				,@IdRol				,emp.IdSucursal							,rol_det.IdEmpleado		,@IdRubro_calculado	,'510'			, sum(rol_det.Valor)*@Por_apor_pers_iess 
,1						,'Aporte personal'	
FROM            dbo.ro_rol_detalle AS rol_det INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON rol_det.IdEmpresa = rub.IdEmpresa AND rol_det.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_rol ON rol_det.IdEmpresa = dbo.ro_rol.IdEmpresa AND rol_det.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado

where rol_det.IdEmpresa=@IdEmpresa
and ro_rol.IdNominaTipo=@IdNomina
and ro_rol.IdNominaTipoLiqui=@IdNominaTipo
and ro_rol.IdPeriodo=@IdPEriodo
and rub.ru_tipo='I' 
and rub.rub_aplica_IESS=1
and cont.IdNomina=1
and cont.EstadoContrato<>'ECT_LIQ'
and rol_det.IdRol=@IdRol
and emp.IdSucursal = @IdSucursalFin
AND cont.IdNomina=@IdNomina
group by rol_det.IdEmpresa,rol_det.IdEmpleado,ro_rol.IdNominaTipo,ro_rol.IdNominaTipoLiqui,ro_rol.IdPeriodo, emp.IdSucursal





----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando anticipo de quincena------------------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdRubro_calculado= IdRubro_tot_pagar, @IdRubro_anticipo=IdRubro_anticipo,@IdRubro_PagoCheque=IdRubro_novedad_proceso from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select
@IdEmpresa				,@IdRol				,emp.IdSucursal							,rol_det.IdEmpleado		,@IdRubro_anticipo	,'501'			,sum(rol_det.Valor)
,1						,'Anticipo 25 primera quincena'	
FROM            dbo.ro_rol_detalle AS rol_det INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON rol_det.IdEmpresa = rub.IdEmpresa AND rol_det.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_rol ON rol_det.IdEmpresa = dbo.ro_rol.IdEmpresa AND rol_det.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado

where rol_det.IdEmpresa=@IdEmpresa
and ro_rol.IdNominaTipo=@IdNomina
and ro_rol.IdNominaTipoLiqui=1
and ro_rol.IdPeriodo=CAST( @IdPEriodo as varchar)+'01'
and rol_det.IdRubro in(@IdRubro_calculado,@IdRubro_PagoCheque)
and emp.IdSucursal = @IdSucursalFin
and cont.IdNomina=@IdNomina
and cont.EstadoContrato<>'ECT_LIQ'
and rol_det.Valor != 0
group by rol_det.IdEmpresa,rol_det.IdEmpleado,ro_rol.IdNominaTipo,ro_rol.IdNominaTipoLiqui,ro_rol.IdPeriodo, emp.IdSucursal



update ro_rol_detalle set Valor = round(Valor,2) where IdEmpresa = @IdEmpresa and IdRol = @IdRol

PRINT 'calculando fondo de reserva'
----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando fondo de reserva----------------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdRubro_calculado= IdRubro_fondo_reserva,@IdRubro_Provision=IdRubro_fondo_reserva from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,			IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)
select
@IdEmpresa				,@IdRol			,emp.IdSucursal		,rol_det.IdEmpleado		,@IdRubro_calculado	,20			,
--CASE WHEN emp.Pago_por_horas = 1 then 
ISNULL( round( (sum(rol_det.Valor) /30) * dbo.calcular_dias_fondos_reserva(@Fi,@Ff,emp.em_fechaIngaRol, emp.em_status, ISNULL(EMP.em_fechaSalida,DATEADD(YEAR,50,GETDATE())), cont.FechaAcumulacion)*0.0833 ,2),0)
--else ISNULL(round( sum(rol_det.Valor)*0.0833 ,2),0) end


,1						,'Fondos de reserva'	
FROM            dbo.ro_rol_detalle AS rol_det INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON rol_det.IdEmpresa = rub.IdEmpresa AND rol_det.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_rol ON rol_det.IdEmpresa = dbo.ro_rol.IdEmpresa AND rol_det.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado left join
						 (
						 
select con.IdEmpresa,con.IdEmpleado, case when  sum(Dias) is null then dateadd(year,1,CON.FechaInicio) else DATEADD(DAY,(365 - case when isnull(sum(Dias),0) > 365 then isnull(sum(Dias),0) else isnull(sum(Dias),0) end), CON.FechaInicio ) end FechaAcumulacion
from ro_contrato as con left join (
SELECT IdEmpresa, IdEmpleado, DATEDIFF(DAY,FechaInicio,FechaFin)+1 Dias FROM ro_contrato C WHERE C.EstadoContrato = 'ECT_LIQ' ) a 
on con.IdEmpresa = a.IdEmpresa and con.IdEmpleado = a.IdEmpleado 
WHERE con.EstadoContrato<>'ECT_LIQ' --and con.IdEmpresa = 5 and con.IdEmpleado = 31
group by con.IdEmpresa,con.IdEmpleado, con.FechaInicio
)

as cont on emp.IdEmpresa = cont.IdEmpresa and emp.IdEmpleado = cont.IdEmpleado

where rol_det.IdEmpresa=@IdEmpresa
and ro_rol.IdNominaTipo=@IdNomina
and ro_rol.IdNominaTipoLiqui=@IdNominaTipo
and ro_rol.IdPeriodo=@IdPEriodo
and ro_rol.IdRol=@IdRol
and ro_rol.IdRol=rol_det.IdRol
and rub.ru_tipo='I' and rub.rub_aplica_IESS=1
AND CONT.FechaAcumulacion <= @FF
and  not exists(select acum.IdEmpleado from ro_empleado_x_rubro_acumulado acum 
where acum.IdEmpresa= @IdEmpresa
and acum.IdEmpresa=emp.IdEmpresa
and acum.IdEmpleado=emp.IdEmpleado
and acum.IdRubro=@IdRubro_Provision)
and CAST( emp.em_fechaIngaRol as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin
group by rol_det.IdEmpresa,rol_det.IdEmpleado,ro_rol.IdNominaTipo,ro_rol.IdNominaTipoLiqui,ro_rol.IdPeriodo, emp.IdSucursal, emp.em_fechaIngaRol, emp.em_fechaSalida, emp.em_status,emp.Pago_por_horas,cont.FechaAcumulacion

----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando decimo tercer sueldo-------------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdRubro_calculado= IdRubro_DIII, @IdRubro_Provision=IdRubro_DIII from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)
select
@IdEmpresa				,@IdRol				,emp.IdSucursal						,rol_det.IdEmpleado		,@IdRubro_calculado	,'52'			, ROUND((sum(rol_det.Valor)/360)*30 ,2)
/*dbo.calcular_dias_trabajados(@Fi,@Ff,emp.em_fechaIngaRol, emp.em_status, emp.em_fechaSalida),2)*/
,1						,'Decimo tercer sueldo'	
FROM            dbo.ro_rol_detalle AS rol_det INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON rol_det.IdEmpresa = rub.IdEmpresa AND rol_det.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_rol ON rol_det.IdEmpresa = dbo.ro_rol.IdEmpresa AND rol_det.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado
where rol_det.IdEmpresa=@IdEmpresa
and ro_rol.IdNominaTipo=@IdNomina
and ro_rol.IdNominaTipoLiqui=@IdNominaTipo
and ro_rol.IdPeriodo=@IdPEriodo
and rub.ru_tipo='I' and rub.rub_aplica_IESS=1
and rol_det.IdRol=@IdRol
and rol_det.IdEmpleado not in(
select acum.IdEmpleado from ro_empleado_x_rubro_acumulado acum 
where acum.IdEmpresa= emp.IdEmpresa
and acum.IdEmpleado=emp.IdEmpleado
and acum.IdRubro=@IdRubro_Provision
and acum.IdEmpresa=@IdEmpresa
and emp.IdEmpresa=@IdEmpresa
)
and emp.IdSucursal = @IdSucursalFin
and cont.IdNomina=@IdNomina
and cont.EstadoContrato<>'ECT_LIQ'
and ISNULL( emp.em_fechaSalida, @Fi) between @Fi and @Ff

group by rol_det.IdEmpresa,rol_det.IdEmpleado,ro_rol.IdNominaTipo,ro_rol.IdNominaTipoLiqui,ro_rol.IdPeriodo, emp.IdSucursal,emp.em_fechaIngaRol, emp.em_status, emp.em_fechaSalida


----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando decimo cuarto sueldo-------------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdRubro_calculado= IdRubro_DIV, @IdRubro_Provision=IdRubro_DIV from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)
select
@IdEmpresa				,@IdRol				,emp.IdSucursal						,emp.IdEmpleado		,@IdRubro_calculado	,'51'			,ROUND((@SueldoBasico/360)* 
dbo.calcular_dias_trabajados(@Fi,@Ff,emp.em_fechaIngaRol, emp.em_status, emp.em_fechaSalida),2) 
,1						,'Decimo cuarto sueldo'	
FROM  dbo.ro_empleado emp, ro_contrato cont
where emp.IdEmpresa=cont.IdEmpresa
and emp.IdEmpleado=cont.IdEmpleado
and cont.EstadoContrato<>'ECT_LIQ'
and (emp.em_status<>'EST_LIQ')
AND cont.IdNomina=@IdNomina
and emp.IdEmpleado not in(select acum.IdEmpleado from ro_empleado_x_rubro_acumulado acum 
where acum.IdEmpresa= emp.IdEmpresa
and acum.IdEmpresa=emp.IdEmpresa
and acum.IdEmpleado=emp.IdEmpleado
and acum.IdRubro=@IdRubro_Provision
and acum.IdEmpresa=@IdEmpresa
and emp.IdEmpresa=@IdEmpresa)
AND emp.IdEmpresa=@IdEmpresa
and CAST( emp.em_fechaIngaRol as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin
and ISNULL( emp.em_fechaSalida, @Fi) between @Fi and @Ff
group by emp.IdEmpresa,emp.IdEmpleado, emp.em_fechaSalida, cont.FechaInicio, cont.FechaFin, emp.em_status, emp.IdSucursal, emp.em_status, emp.em_fechaSalida, emp.em_fechaIngaRol, emp.Pago_por_horas


/*
----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando bono por antiguedada----------------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdRubro_calculado= IdRubro_bono_x_antiguedad from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,			IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)
select
@IdEmpresa				,@IdRol			,emp.IdSucursal		,emp.IdEmpleado		,@IdRubro_calculado	,90			,CASe when( round(DATEDIFF(YEAR ,emp.em_fechaIngaRol,@Ff)*1 ,2))>5 then  round(DATEDIFF(YEAR ,emp.em_fechaIngaRol,@Ff)*1 ,2) -5 else  round(DATEDIFF(YEAR ,emp.em_fechaIngaRol,@Ff)*1 ,2) end
,1						,'BONO POR ANTIGÜEDAD'	

from                         dbo.ro_empleado AS emp 
where emp.IdEmpresa=@IdEmpresa

and DATEDIFF(YEAR ,emp.em_fechaIngaRol,@Ff)>=5
and CAST( emp.em_fechaIngaRol as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin
group by emp.IdEmpresa,emp.IdEmpleado, emp.IdSucursal, emp.em_fechaIngaRol

*/
----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando total ingreso por empleado-------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------

select @IdRubro_calculado= IdRubro_tot_ing from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)


select
@IdEmpresa				,@IdRol				,emp.IdSucursal							,rol_det.IdEmpleado		,@IdRubro_calculado	,'500'			,round( sum(round(rol_det.Valor,2)),2)
,1						,'Total ingresos'	
FROM            dbo.ro_rol_detalle AS rol_det INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON rol_det.IdEmpresa = rub.IdEmpresa AND rol_det.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_rol ON rol_det.IdEmpresa = dbo.ro_rol.IdEmpresa AND rol_det.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado
where rol_det.IdEmpresa=@IdEmpresa
and ro_rol.IdNominaTipo=@IdNomina
and ro_rol.IdNominaTipoLiqui=@IdNominaTipo
and ro_rol.IdPeriodo=@IdPEriodo
and rub.ru_tipo='I'
and emp.IdSucursal = @IdSucursalFin
	and cont.IdNomina=@IdNomina
	and cont.EstadoContrato<>'ECT_LIQ'
group by rol_det.IdEmpresa,rol_det.IdEmpleado,ro_rol.IdNominaTipo,ro_rol.IdNominaTipoLiqui,ro_rol.IdPeriodo, emp.IdSucursal

/*
----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculandoliquido impuesto a la renta--------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------

select @IdRubro_calculado= IdRubro_IR from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle
(IdEmpresa,				IdRol,	IdSucursal,					IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select
@IdEmpresa				,@IdRol, emp.IdSucursal				,emp.IdEmpleado		,@IdRubro_calculado	,'600'			, ISNULL( dbo.base_impuesto_renta(@IdEmpresa,@IdNomina,@IdNominaTipo,@IdPEriodo,emp.idempleado,@Anio),0),
1						,'Provisión impuesto a la renta'	

FROM            dbo.ro_empleado AS emp INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado LEFT OUTER JOIN
                         dbo.ro_empleado_proyeccion_gastos AS gast ON emp.IdEmpresa = gast.IdEmpresa AND emp.IdEmpleado = gast.IdEmpleado

where emp.IdEmpresa=@IdEmpresa
and cont.IdNomina=@IdNomina
and emp.IdSucursal = @IdSucursalFin
and cont.IdNomina=@IdNomina
and cont.EstadoContrato!='ECT_LIQ'
*/
----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando total egreso por empleado--------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------

select @IdRubro_calculado= IdRubro_tot_egr from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,			IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select
@IdEmpresa				,@IdRol				,emp.IdSucursal							,rol_det.IdEmpleado		,@IdRubro_calculado	,'1000'			,round( sum(round(rol_det.Valor,2)),2)
,1						,'Total Egreso'	
FROM            dbo.ro_rol_detalle AS rol_det INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON rol_det.IdEmpresa = rub.IdEmpresa AND rol_det.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_rol ON rol_det.IdEmpresa = dbo.ro_rol.IdEmpresa AND rol_det.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado

where rol_det.IdEmpresa=@IdEmpresa
and ro_rol.IdNominaTipo=@IdNomina
and ro_rol.IdNominaTipoLiqui=@IdNominaTipo
and ro_rol.IdPeriodo=@IdPEriodo
and (rub.ru_tipo='E' or rol_det.IdRubro=56 )
and rol_det.IdRol=@IdRol
and emp.IdSucursal = @IdSucursalFin
and cont.IdNomina=@IdNomina
and cont.EstadoContrato<>'ECT_LIQ'
group by rol_det.IdEmpresa,rol_det.IdEmpleado,ro_rol.IdNominaTipo,ro_rol.IdNominaTipoLiqui,ro_rol.IdPeriodo, emp.IdSucursal






----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculandoliquido a recibir--------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------

select @IdRubro_calculado= IdRubro_tot_pagar from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select
@IdEmpresa				,@IdRol				,IdSucursal			,IdEmpleado		,@IdRubro_calculado	,'1500'			, cast( (ISNULL( [22],0) -ISNULL( [23],0)) as numeric(12,2))
,1						,'Liquido a recibir'	
FROM (
    SELECT 
        rol_det.IdEmpresa,emp.IdEmpleado, emp.IdSucursal,IdNominaTipo,IdNominaTipoLiqui ,IdPeriodo ,rol_det.IdRubro, Valor
FROM            dbo.ro_rol_detalle AS rol_det INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON rol_det.IdEmpresa = rub.IdEmpresa AND rol_det.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_rol ON rol_det.IdEmpresa = dbo.ro_rol.IdEmpresa AND rol_det.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado
	 where rol_det.IdEmpresa=@IdEmpresa
	 and IdNominaTipo=@IdNomina
	 and IdNominaTipoLiqui=@IdNominaTipo
	 and IdPeriodo=@IdPEriodo
	 and rol_det.IdRol=@IdRol
and emp.IdSucursal = @IdSucursalFin
and cont.IdNomina=@IdNomina
and cont.EstadoContrato<>'ECT_LIQ'
) as s
PIVOT
(
   max([Valor])
    FOR [IdRubro] IN ([22],[23])
)AS pvt










----------------------------------------------------------------------------------------------------------------------------------------------
-------------INSERTANDO PROVISIONES ACUMULADAS-----------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------


----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando fondo de reserva----------------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdRubro_calculado= IdRubro_prov_FR,@IdRubro_Provision=IdRubro_fondo_reserva from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle_x_rubro_acumulado
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,						Valor, Estado
)
select
@IdEmpresa				,@IdRol				,emp.IdSucursal				,rol_det.IdEmpleado		,@IdRubro_calculado	,			

--CASE WHEN emp.Pago_por_horas = 1 then 
ISNULL( round( (sum(rol_det.Valor) /30) * dbo.calcular_dias_fondos_reserva(@Fi,@Ff,emp.em_fechaIngaRol, emp.em_status, ISNULL(EMP.em_fechaSalida,DATEADD(YEAR,50,GETDATE())), contI.FechaAcumulacion)*0.0833 ,2),0)
--else ISNULL(round( sum(rol_det.Valor)*0.0833 ,2),0) end

--CAST( ( (sum(rol_det.Valor)/30)*  dbo.calcular_dias_fondos_reserva(@Fi,@Ff,emp.em_fechaIngaRol, emp.em_status, CONT.FechaFin,contI.FechaAcumulacion))*0.0833 as numeric(10,2))
,'PEN'

FROM            dbo.ro_rol_detalle AS rol_det INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON rol_det.IdEmpresa = rub.IdEmpresa AND rol_det.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_rol ON rol_det.IdEmpresa = dbo.ro_rol.IdEmpresa AND rol_det.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado INNER JOIN
                         dbo.ro_empleado_x_rubro_acumulado AS emp_x_rub_acum ON emp.IdEmpresa = emp_x_rub_acum.IdEmpresa AND emp.IdEmpleado = emp_x_rub_acum.IdEmpleado left join
						 (
						 
select con.IdEmpresa,con.IdEmpleado, case when  sum(Dias) is null then dateadd(year,1,CON.FechaInicio) else DATEADD(DAY,(365 - case when isnull(sum(Dias),0) > 365 then isnull(sum(Dias),0) else isnull(sum(Dias),0) end), CON.FechaInicio ) end FechaAcumulacion
from ro_contrato as con left join (
SELECT IdEmpresa, IdEmpleado, DATEDIFF(DAY,FechaInicio,FechaFin)+1 Dias FROM ro_contrato C WHERE C.EstadoContrato = 'ECT_LIQ' ) a 
on con.IdEmpresa = a.IdEmpresa and con.IdEmpleado = a.IdEmpleado 
WHERE con.EstadoContrato<>'ECT_LIQ' --and con.IdEmpresa = 5 and con.IdEmpleado = 31
group by con.IdEmpresa,con.IdEmpleado, CON.FechaInicio
)

as contI on emp.IdEmpresa = contI.IdEmpresa and emp.IdEmpleado = contI.IdEmpleado
where rol_det.IdEmpresa=@IdEmpresa
and ro_rol.IdNominaTipo=@IdNomina
and ro_rol.IdNominaTipoLiqui=@IdNominaTipo
and ro_rol.IdPeriodo=@IdPEriodo
and rub.ru_tipo='I' and rub.rub_aplica_IESS=1
--and DATEDIFF(day ,emp.em_fechaIngaRol,@Ff)>360
AND CONTI.FechaAcumulacion <= @Ff
and rol_det.IdRol=@IdRol
and CAST( emp.em_fechaIngaRol as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin
and cont.IdNomina=@IdNomina
and cont.EstadoContrato<>'ECT_LIQ'

and (emp.em_status<>'EST_LIQ' and isnull( emp.em_fechaSalida, @Ff) between @Fi and @Ff )
and CAST( cont.FechaInicio as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin

and emp_x_rub_acum.IdRubro=@IdRubro_Provision

group by rol_det.IdEmpresa,rol_det.IdEmpleado,ro_rol.IdNominaTipo,ro_rol.IdNominaTipoLiqui,ro_rol.IdPeriodo, emp.IdSucursal, emp.em_fechaIngaRol,emp.em_fechaSalida,emp.Pago_por_horas, CONT.FechaFin,emp.em_status,contI.FechaAcumulacion

----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando decimo tercer sueldo-------------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdRubro_calculado= IdRubro_prov_DIII,@IdRubro_Provision=IdRubro_DIII from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle_x_rubro_acumulado
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,						Valor, Estado
)
select
@IdEmpresa				,@IdRol				,emp.IdSucursal				,rol_det.IdEmpleado		,@IdRubro_calculado				,round((sum(rol_det.Valor)/360)*30,2)
/*dbo.calcular_dias_trabajados(@Fi,isnull(emp.em_fechaSalida, cast(@Ff as date)),cont.FechaInicio, emp.em_status, emp.em_fechaSalida) as numeric(10,2))*/
,'PEN'
FROM            dbo.ro_rol_detalle AS rol_det INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON rol_det.IdEmpresa = rub.IdEmpresa AND rol_det.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_rol ON rol_det.IdEmpresa = dbo.ro_rol.IdEmpresa AND rol_det.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado INNER JOIN
                         dbo.ro_empleado_x_rubro_acumulado AS emp_x_rub_acum ON emp.IdEmpresa = emp_x_rub_acum.IdEmpresa AND emp.IdEmpleado = emp_x_rub_acum.IdEmpleado
where rol_det.IdEmpresa=@IdEmpresa
and ro_rol.IdNominaTipo=@IdNomina
and ro_rol.IdNominaTipoLiqui=@IdNominaTipo
and ro_rol.IdPeriodo=@IdPEriodo
and rub.ru_tipo='I' and rub.rub_aplica_IESS=1
and rol_det.IdRol=@IdRol
and CAST( emp.em_fechaIngaRol as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin
and cont.IdNomina=@IdNomina
and cont.EstadoContrato <> 'ECT_LIQ'
and (emp.em_status<>'EST_LIQ' and isnull( emp.em_fechaSalida, @Ff) between @Fi and @Ff )
and CAST( cont.FechaInicio as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin

and emp_x_rub_acum.IdRubro=@IdRubro_Provision
group by rol_det.IdEmpresa,rol_det.IdEmpleado,ro_rol.IdNominaTipo,ro_rol.IdNominaTipoLiqui,ro_rol.IdPeriodo, emp.IdSucursal,emp.em_fechaSalida,cont.FechaInicio, emp.em_status, emp.em_fechaSalida


----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando decimo cuarto sueldo-------------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdRubro_calculado= IdRubro_prov_DIV, @IdRubro_Provision=IdRubro_DIV from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle_x_rubro_acumulado
(IdEmpresa,				IdRol,			idSucursal,						IdEmpleado,			IdRubro,						Valor,	Estado
)
select
@IdEmpresa				,@IdRol				,emp.IdSucursal			,emp.IdEmpleado		,@IdRubro_calculado				,round( (@SueldoBasico/360)* 
dbo.calcular_dias_trabajados(@Fi,isnull(emp.em_fechaSalida, cast(@Ff as date)),cont.FechaInicio, emp.em_status, emp.em_fechaSalida) ,2)
,'PEN'
FROM            dbo.ro_empleado AS emp INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado INNER JOIN
                         dbo.ro_empleado_x_rubro_acumulado AS emp_x_rub_acum ON emp.IdEmpresa = emp_x_rub_acum.IdEmpresa AND emp.IdEmpleado = emp_x_rub_acum.IdEmpleado
where emp.IdEmpresa=cont.IdEmpresa
and emp.IdEmpleado=cont.IdEmpleado

and (emp.em_status<>'EST_LIQ' and isnull( emp.em_fechaSalida, @Ff) between @Fi and @Ff )
and CAST( cont.FechaInicio as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin
and cont.EstadoContrato <> 'ECT_LIQ'
and emp.IdSucursal = @IdSucursalFin
and cont.IdNomina=@IdNomina
and emp_x_rub_acum.IdRubro=@IdRubro_Provision
and emp.IdEmpresa=@IdEmpresa
group by emp.IdEmpresa,emp.IdEmpleado, emp.em_fechaSalida, cont.FechaInicio, cont.FechaFin, emp.em_status, emp.IdSucursal, emp.Pago_por_horas


----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando provision de vacaciones----------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdRubro_calculado= IdRubro_prov_vac from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle_x_rubro_acumulado
(IdEmpresa,				IdRol,			idSucursal,						IdEmpleado,			IdRubro,						Valor,						Estado
)
select
@IdEmpresa				,@IdRol				,emp.IdSucursal			,rol_det.IdEmpleado		,@IdRubro_calculado				,CAST( sum(rol_det.Valor)/24 as numeric(10,2)), 'PEN'

FROM            dbo.ro_rol_detalle AS rol_det INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON rol_det.IdEmpresa = rub.IdEmpresa AND rol_det.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_rol ON rol_det.IdEmpresa = dbo.ro_rol.IdEmpresa AND rol_det.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado
where rol_det.IdEmpresa=@IdEmpresa
and ro_rol.IdNominaTipo=@IdNomina
and ro_rol.IdNominaTipoLiqui=@IdNominaTipo
and ro_rol.IdPeriodo=@IdPEriodo
and rub.ru_tipo='I' and rub.rub_aplica_IESS=1
and rol_det.IdRol=@IdRol
and CAST( emp.em_fechaIngaRol as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin
and cont.IdNomina=@IdNomina

and (emp.em_status<>'EST_LIQ' and isnull( emp.em_fechaSalida, @Ff) between @Fi and @Ff )
and CAST( cont.FechaInicio as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin

group by rol_det.IdEmpresa,rol_det.IdEmpleado,ro_rol.IdNominaTipo,ro_rol.IdNominaTipoLiqui,ro_rol.IdPeriodo, emp.IdSucursal, emp.Pago_por_horas




----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando aporte patronal------------------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdRubro_calculado= IdRubro_aporte_patronal from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle_x_rubro_acumulado
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,						Valor, Estado
)
select
@IdEmpresa				,@IdRol				,emp.IdSucursal				,rol_det.IdEmpleado		,@IdRubro_calculado	,			round( sum(rol_det.Valor)*0.1215 ,2),'PEN'

FROM            dbo.ro_rol_detalle AS rol_det INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON rol_det.IdEmpresa = rub.IdEmpresa AND rol_det.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_rol ON rol_det.IdEmpresa = dbo.ro_rol.IdEmpresa AND rol_det.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado --INNER JOIN
                         --dbo.ro_empleado_x_rubro_acumulado AS emp_x_rub_acum ON emp.IdEmpresa = emp_x_rub_acum.IdEmpresa AND emp.IdEmpleado = emp_x_rub_acum.IdEmpleado

where rol_det.IdEmpresa=@IdEmpresa
and ro_rol.IdNominaTipo=@IdNomina
and ro_rol.IdNominaTipoLiqui=@IdNominaTipo
and ro_rol.IdPeriodo=@IdPEriodo
and rub.ru_tipo='I' 
and rub.rub_aplica_IESS=1
and cont.IdNomina=1
and rol_det.IdRol=@IdRol
and emp.IdSucursal = @IdSucursalFin
and cont.IdNomina=@IdNomina
and cont.EstadoContrato <> 'ECT_LIQ'
and (emp.em_status<>'EST_LIQ' and isnull( emp.em_fechaSalida, @Ff) between @Fi and @Ff )
and CAST( cont.FechaInicio as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin

group by rol_det.IdEmpresa,rol_det.IdEmpleado,ro_rol.IdNominaTipo,ro_rol.IdNominaTipoLiqui,ro_rol.IdPeriodo, emp.IdSucursal



----------------------------------------------------------------------------------------------------------------------------------------------
-------------insertado gastos distrbuidos----------------------------------------------------------------------------------------------<
------------------------------------------------------------------------------------------------------------------------------------------------

insert into  ro_empleado_division_area_x_rol

select ro_empleado_x_division_x_area.IdEmpresa,@IdRol, ROW_NUMBER() OVER(ORDER BY ro_empleado.idempresa ASC) AS Row ,ro_empleado.IdEmpleado,ro_empleado_x_division_x_area.IDividion,ro_empleado_x_division_x_area.IdArea,Porcentaje, Observacion 
FROM            dbo.ro_empleado_x_division_x_area INNER JOIN
                         dbo.ro_empleado ON dbo.ro_empleado_x_division_x_area.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_empleado_x_division_x_area.IdEmpleado = dbo.ro_empleado.IdEmpleado
WHERE ro_empleado_x_division_x_area.IdEmpresa=@IdEmpresa
and ro_empleado.IdSucursal=@IdSucursalFin


----------------------------------------------------------------------------------------------------------------------------------------------
-------------insertado gastos decimo III si no existe----------------------------------------------------------------------------------------------<
------------------------------------------------------------------------------------------------------------------------------------------------
declare 
@IdEmpleado int

SELECT @IdEmpleado=min(IdEmpleado)  FROM ro_rol_detalle where IdEmpresa=@IdEmpresa and IdRol=@IdRol

select @IdRubro_DIII=IdRubro_DIII,@IdRubro_DIV=IdRubro_DIV,@IdRubro_ProvDIII=IdRubro_prov_DIII,@IdRubro_ProvDIV=IdRubro_prov_DIV, @IdRubro_FondoReserva=IdRubro_fondo_reserva from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros

if((select COUNT(IdEmpresa) from ro_rol_detalle where IdEmpresa=@IdEmpresa  and IdRol=@IdRol and IdRubro=@IdRubro_DIII)=0)
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)


select
@IdEmpresa				,@IdRol			,@IdSucursalInicio				,@IdEmpleado		,@IdRubro_DIII	,'52'			,0
,1						,'Decimo tercero'	

if((select COUNT(IdEmpresa) from ro_rol_detalle where IdEmpresa=@IdEmpresa  and IdRol=@IdRol and IdRubro=@IdRubro_DIV)=0)
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)


select
@IdEmpresa				,@IdRol				,@IdSucursalInicio				,@IdEmpleado		,@IdRubro_DIV	,'52'			,0
,1						,'Decimo cuarto'	

if((select COUNT(IdEmpresa) from ro_rol_detalle where IdEmpresa=@IdEmpresa  and IdRol=@IdRol and IdRubro=@IdRubro_FondoReserva)=0)
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)


select
@IdEmpresa				,@IdRol				,@IdSucursalInicio				,@IdEmpleado		,@IdRubro_FondoReserva	,'20'			,0
,1						,'Fondos de Reserva'	


update ro_rol_detalle set Valor=CAST(Valor as numeric(18,2)) where IdEmpresa=@IdEmpresa and IdRol=@IdRol
update ro_rol_detalle_x_rubro_acumulado set Valor=CAST(Valor as numeric(18,2)) where IdEmpresa=@IdEmpresa and IdRol=@IdRol
update ro_periodo_x_ro_Nomina_TipoLiqui set Procesado='S' where IdEmpresa=@IdEmpresa and IdNomina_Tipo=@IdNomina and IdNomina_TipoLiqui=@IdNominaTipo and IdPeriodo=@IdPEriodo




----------------------------------------------------------------------------------------------------------------------------------------------
-------------INSERTANDO CUOTAS DE PRESTAMO CONSIDERADAS EN ESTE ROL---------------------------------------------------------------------------<
------------------------------------------------------------------------------------------------------------------------------------------------
insert into ro_rol_x_prestamo_detalle
(IdEmpresa,				IdRol,Secuencia,			IdEmpresa_pre,						IdPrestamo,			NumCuota,				Observacion)

select
@IdEmpresa				,@IdRol,ROW_NUMBER() over (order by emp.IdEmpresa)			,pre.IdEmpresa						,pre.IdPrestamo		,pred.NumCuota,			pred.Observacion_det
FROM            dbo.ro_prestamo AS pre INNER JOIN
                         dbo.ro_prestamo_detalle AS pred ON pre.IdEmpresa = pred.IdEmpresa AND pre.IdPrestamo = pred.IdPrestamo INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON pre.IdEmpresa = rub.IdEmpresa AND pre.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_empleado AS emp ON pre.IdEmpresa = emp.IdEmpresa AND pre.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado
and pre.IdEmpresa=@IdEmpresa
and emp.IdEmpresa=@IdEmpresa
and pred.IdNominaTipoLiqui=@IdNominaTipo
and CAST(pred.FechaPago AS DATE) between @Fi and @Ff
and pred.Estado=1
and pred.EstadoPago='PEN'
and (emp.em_status <>'EST_LIQ' and isnull( emp.em_fechaSalida, @Ff) between @Fi and @Ff )
and CAST( emp.em_fechaIngaRol as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin
and pred.FechaPago between  @Fi and @Ff 
and pred.IdNominaTipoLiqui=@IdNominaTipo
and cont.IdNomina=@IdNomina





----------------------------------------------------------------------------------------------------------------------------------------------
-------------INSERTANDO NOVEDADES QUE SE CONSIDERARON EN ESTE ROL----------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------


insert into ro_rol_x_empleado_novedades
(IdEmpresa,				IdRol,	Secuencia,		IdEmpresa_nov,						IdNovedad, Secuencia_nov,			Observacion)

select 
@IdEmpresa				,@IdRol,ROW_NUMBER() over (order by emp.IdEmpresa),novc.IdEmpresa	,		novc.IdNovedad, nov.Secuencia, novc.Observacion		
FROM   dbo.ro_empleado AS emp INNER JOIN
dbo.ro_empleado_Novedad AS novc ON emp.IdEmpresa = novc.IdEmpresa AND emp.IdEmpleado = novc.IdEmpleado INNER JOIN
dbo.ro_empleado_novedad_det AS nov ON novc.IdEmpresa = nov.IdEmpresa AND novc.IdNovedad = nov.IdNovedad AND novc.IdEmpleado = novc.IdEmpleado INNER JOIN
dbo.ro_rubro_tipo AS rub ON nov.IdEmpresa = rub.IdEmpresa AND nov.IdRubro = rub.IdRubro
and nov.IdEmpresa=@IdEmpresa
and emp.IdEmpresa=@IdEmpresa
and novc.IdNomina_tipo=@IdNomina
and novc.IdNomina_TipoLiqui=@IdNominaTipo
and nov.FechaPago between @Fi and @Ff
and novc.Estado='A'
and nov.EstadoCobro='PEN'
and (emp.em_status<>'EST_LIQ')
and CAST( emp.em_fechaIngaRol as date)<=@Ff
and ISNULL( emp.em_fechaSalida, @Fi) between @Fi and @Ff
and emp.IdSucursal = @IdSucursalFin


END