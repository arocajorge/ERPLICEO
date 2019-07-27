

CREATE PROCEDURE [dbo].[spRo_procesa_Rol_anticipo] (
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

--declare
--@IdEmpresa int,
--@IdNomina numeric,
--@IdNominaTipo numeric,
--@IdPEriodo numeric,
--@IdUsuario varchar(50),
--@observacion varchar(500),
--@IdRol int,
--@IdSucursal int,
--@IdSucursalInicio int,
--@IdSucursalFin int

--set @IdEmpresa =1
--set @IdNomina =1
--set @IdNominaTipo =1
--set @IdPEriodo= 20190201
--set @IdUsuario ='admin'
--set @observacion= '1ERA QUINCENA FEBRERO'
--set @IdRol =6
--set @IdSucursal=1
--set @IdSucursalInicio=1
--set @IdSucursalFin=1
BEGIN

declare
@Fi date,
@Ff date,
@IdRubro_calculado varchar(50),
@Dias_trabajados int,
@Anio float,
@IdRubroTotalING varchar(50),
@IdRubroTotalEGR varchar(50),
@Porcentaje_anticipo float,
@IdRubroMatutina varchar(50),
@IdRubroVespertina varchar(50)
----------------------------------------------------------------------------------------------------------------------------------------------
-------------obteniendo fecha del perido------------------- ----------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @Fi= pe_FechaIni, @Ff=pe_FechaFin, @Anio=pe_anio from ro_periodo where IdEmpresa=@IdEmpresa and IdPeriodo=@IdPEriodo
select @Porcentaje_anticipo=Porcentaje_anticipo from ro_Parametros where IdEmpresa=@IdEmpresa
----------------------------------------------------------------------------------------------------------------------------------------------
-------------preparando la cabecera del rol general-------- ----------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
if((select  COUNT(idrol) from ro_rol where IdEmpresa=@IdEmpresa and @IdRol=IdRol)>0)
update ro_rol set UsuarioModifica=@IdUsuario, FechaModifica=GETDATE() where IdEmpresa=@IdEmpresa and @IdRol=IdRol
else
insert into ro_rol
(IdEmpresa,	IdRol,IdSucursal,	IdNominaTipo,		IdNominaTipoLiqui,		IdPeriodo,			Descripcion,				Observacion,				Cerrado,			FechaIngresa,
UsuarioIngresa,	FechaModifica,		UsuarioModifica,		FechaAnula,			UsuarioAnula,				MotivoAnula,				UsuarioCierre,		FechaCierre,
IdCentroCosto)
values
(@IdEmpresa	, @IdRol, case when @IdSucursalInicio=0then NULL else @IdSucursalInicio end	,@IdNomina			,@IdNominaTipo			,@IdPEriodo			,@observacion				,@observacion				,'ABIERTO'				,GETDATE()
,@IdUsuario		,null				,null					,null				,null						,null						,null				,null
,null)

----------------------------------------------------------------------------------------------------------------------------------------------
-------------eliminando detalle--------------------------- ----------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
delete ro_rol_detalle_x_rubro_acumulado  where IdEmpresa=@IdEmpresa and IdRol=@IdRol
delete ro_rol_detalle where ro_rol_detalle.IdEmpresa=@IdEmpresa and @IdRol=IdRol


----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando dias trabajados por empleado-----------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------

select @IdRubro_calculado= IdRubro_dias_trabajados from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros

insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion			)

select 

@IdEmpresa				,@IdRol				,emp.IdSucursal			,cont.IdEmpleado		,@IdRubro_calculado	,'0' ,dbo.calcular_dias_trabajados(@Fi,@Ff,emp.em_fechaIngaRol, emp.em_status, emp.em_fechaSalida)
,1						,'Días trabajados'		
FROM            dbo.ro_contrato AS cont INNER JOIN
                dbo.ro_empleado AS emp ON cont.IdEmpresa = emp.IdEmpresa AND cont.IdEmpleado = emp.IdEmpleado
where cont.IdEmpresa=@IdEmpresa 
and cont.IdNomina=@IdNomina
and cont.EstadoContrato!='ECT_LIQ'
and (emp.em_status!='EST_LIQ')
and (emp.em_status!='EST_LIQ' and isnull( emp.em_fechaSalida, @Ff) between @Fi and @Ff )
and emp.IdSucursal = @IdSucursalFin
----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando sueldo al personal que no se les paga por horas-------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdRubro_calculado= IdRubro_sueldo from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros

insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select 
@IdEmpresa				,@IdRol,		emp.IdSucursal			,cont.IdEmpleado		,@IdRubro_calculado	,'1' ,ISNULL( ROUND( cont.sueldo/30* ((dbo.calcular_dias_trabajados(@Fi,@Ff,emp.em_fechaIngaRol, emp.em_status, emp.em_fechaSalida)))*2*emp.em_AnticipoSueldo,2),0)
,1						,'Sueldo base'		
FROM            dbo.ro_contrato AS cont INNER JOIN
                dbo.ro_empleado AS emp ON cont.IdEmpresa = emp.IdEmpresa AND cont.IdEmpleado = emp.IdEmpleado
where cont.IdEmpresa=@IdEmpresa 
and cont.IdNomina=@IdNomina
and cont.EstadoContrato!='ECT_LIQ'
and (emp.em_status!='EST_LIQ')
and CAST( cont.FechaInicio as date)<=@Ff
and (emp.em_status!='EST_LIQ' and isnull( emp.em_fechaSalida, @Ff) between @Fi and @Ff )
and emp.IdSucursal = @IdSucursalFin
and emp.Pago_por_horas=0





----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando sueldo por días trabajados al personal que  se les paga por horas-------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdRubro_calculado= IdRubro_sueldo, @IdRubroMatutina=IdRubro_horas_matutina, @IdRubroVespertina=IdRubro_horas_vespertina from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros

insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select 
@IdEmpresa				,@IdRol,		emp.IdSucursal			,cont.IdEmpleado		,@IdRubro_calculado	,'1' ,ISNULL( ROUND( SUM( rol_det.valor)*emp.em_AnticipoSueldo,2),0)
,1						,'Sueldo base'		
FROM            dbo.ro_rol AS rol INNER JOIN
                         dbo.ro_rol_detalle AS rol_det ON rol.IdEmpresa = rol_det.IdEmpresa AND rol.IdRol = rol_det.IdRol INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                         dbo.ro_periodo AS peri ON pe_x_nom.IdEmpresa = peri.IdEmpresa AND pe_x_nom.IdPeriodo = peri.IdPeriodo INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado
						 and rol_det.idrubro in(@IdRubroMatutina,@IdRubroVespertina)
						 and emp.Pago_por_horas=1
						 and peri.pe_anio=case when DATEPART(MONTH, @Fi) =1 then DATEPART(year, @Fi)-1 else  DATEPART(year, peri.pe_FechaIni) end
						 and peri.pe_mes=case when DATEPART(MONTH, @Fi)=1 then 12 else  DATEPART(MONTH, @Fi)-1 end
						 and pe_x_nom.IdNomina_TipoLiqui=2
where cont.IdEmpresa=@IdEmpresa 
and cont.IdNomina=@IdNomina
and cont.EstadoContrato!='ECT_LIQ'
and CAST( cont.FechaInicio as date)<=@Ff
and (emp.em_status!='EST_LIQ' and isnull( emp.em_fechaSalida, @Ff) between @Fi and @Ff )
and emp.IdSucursal = @IdSucursalFin
and emp.Pago_por_horas=1
group by emp.IdSucursal, cont.IdEmpleado, emp.em_AnticipoSueldo

----------------------------------------------------------------------------------------------------------------------------------------------
-------------buscando novedades del periodo e insertando al rol detalle-----------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------


insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select 
@IdEmpresa				,@IdRol				,emp.IdSucursal							,novc.IdEmpleado		,nov.IdRubro		,rub.ru_orden	,sum(ROUND( nov.Valor,2))
,1						,rub.ru_descripcion		
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
and (emp.em_status!='EST_LIQ')
and CAST( emp.em_fechaIngaRol as date)<=@Ff
and emp.IdSucursal = @IdSucursalFin
group by novc.IdEmpresa,novc.IdEmpleado,nov.IdRubro,rub.ru_orden,rub.ru_descripcion, emp.IdSucursal

----------------------------------------------------------------------------------------------------------------------------------------------
-------------buscando cuota de prestamos e insertando al rol detalle-------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select
@IdEmpresa				,@IdRol				,emp.IdSucursal			,pre.IdEmpleado		,pre.IdRubro		,rub.ru_orden	,ROUND( pred.TotalCuota,2)
,1						,pred.Observacion_det
FROM            dbo.ro_prestamo AS pre INNER JOIN
                         dbo.ro_prestamo_detalle AS pred ON pre.IdEmpresa = pred.IdEmpresa AND pre.IdPrestamo = pred.IdPrestamo INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON pre.IdEmpresa = rub.IdEmpresa AND pre.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_empleado AS emp ON pre.IdEmpresa = emp.IdEmpresa AND pre.IdEmpleado = emp.IdEmpleado
and pre.IdEmpresa=@IdEmpresa
and emp.IdEmpresa=@IdEmpresa
and pred.IdNominaTipoLiqui=@IdNominaTipo
and pred.FechaPago between @Fi and @Ff
and pred.Estado=1
and pred.EstadoPago='PEN'
and (emp.em_status!='EST_LIQ')
and CAST( emp.em_fechaIngaRol as date)<=@Ff
AND (emp.em_fechaSalida IS NULL OR emp.em_fechaSalida BETWEEN @Fi and @Ff)
and emp.IdSucursal = @IdSucursalFin
----------------------------------------------------------------------------------------------------------------------------------------------
-------------buscando rubros fijos e insertando al rol detalle-------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------

insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,			IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select
@IdEmpresa				,@IdRol				,emp.IdSucursal			,emp.IdEmpleado		,rub_fij.IdRubro	,rub.ru_orden	,ROUND( rub_fij.Valor,2)
,1						,rub.ru_descripcion	
FROM            dbo.ro_rubro_tipo AS rub INNER JOIN
                         dbo.ro_empleado_x_ro_rubro AS rub_fij ON rub.IdEmpresa = rub_fij.IdEmpresa AND rub.IdRubro = rub_fij.IdRubro INNER JOIN
                         dbo.ro_empleado AS emp ON rub_fij.IdEmpresa = emp.IdEmpresa AND rub_fij.IdEmpleado = emp.IdEmpleado
and rub_fij.IdEmpresa=@IdEmpresa
and emp.IdEmpresa=@IdEmpresa
and rub_fij.IdNomina_tipo=@IdNomina
and rub_fij.IdNomina_TipoLiqui=@IdNominaTipo
and (rub_fij.es_indifinido=1 or ( @Fi between rub_fij.FechaFin and rub_fij.FechaFin and @Ff between rub_fij.FechaFin and rub_fij.FechaFin))
--and rub_fij.Estado='A'
and (emp.em_status!='EST_LIQ')
and CAST( emp.em_fechaIngaRol as date)<=@Ff
AND (emp.em_fechaSalida IS NULL OR emp.em_fechaSalida BETWEEN @Fi and @Ff)
and emp.IdSucursal= @IdSucursalFin
----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando total ingreso por empleado-------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------

select @IdRubro_calculado= IdRubro_tot_ing from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)


select
@IdEmpresa				,@IdRol				,emp.IdSucursal							,rol_det.IdEmpleado		,@IdRubro_calculado	,'100'			,sum(ROUND( rol_det.Valor,2))
,1						,'Total ingresos'	
FROM            dbo.ro_rol_detalle AS rol_det INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON rol_det.IdEmpresa = rub.IdEmpresa AND rol_det.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_rol ON rol_det.IdEmpresa = dbo.ro_rol.IdEmpresa AND rol_det.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado
where rol_det.IdEmpresa=@IdEmpresa
and ro_rol.IdNominaTipo=@IdNomina
and ro_rol.IdNominaTipoLiqui=@IdNominaTipo
and ro_rol.IdPeriodo=@IdPEriodo
and ro_rol.IdSucursal= @IdSucursalFin
and rub.ru_tipo='I'
group by rol_det.IdEmpresa,rol_det.IdEmpleado,ro_rol.IdNominaTipo,ro_rol.IdNominaTipoLiqui,ro_rol.IdPeriodo, emp.IdSucursal


----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculando total egreso por empleado--------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------

select @IdRubro_calculado= IdRubro_tot_egr from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,			IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select
@IdEmpresa				,@IdRol				,emp.IdSucursal							,rol_det.IdEmpleado		,@IdRubro_calculado	,'200'			,sum( ROUND( rol_det.Valor,2))
,1						,'Total Egreso'	
FROM            dbo.ro_rol_detalle AS rol_det INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON rol_det.IdEmpresa = rub.IdEmpresa AND rol_det.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_rol ON rol_det.IdEmpresa = dbo.ro_rol.IdEmpresa AND rol_det.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado

where rol_det.IdEmpresa=@IdEmpresa
and ro_rol.IdNominaTipo=@IdNomina
and ro_rol.IdNominaTipoLiqui=@IdNominaTipo
and ro_rol.IdPeriodo=@IdPEriodo
and rol_det.IdRol=@IdRol
and rol_det.IdSucursal = @IdSucursalFin
and rub.ru_tipo='E'
group by rol_det.IdEmpresa,rol_det.IdEmpleado,ro_rol.IdNominaTipo,ro_rol.IdNominaTipoLiqui,ro_rol.IdPeriodo, emp.IdSucursal






----------------------------------------------------------------------------------------------------------------------------------------------
-------------calculandoliquido a recibir--------------------------------------------------------------------------------------------<
----------------------------------------------------------------------------------------------------------------------------------------------
select @IdRubroTotalEGR=IdRubro_tot_egr, @IdRubroTotalING=IdRubro_tot_ing from ro_rubros_calculados where IdEmpresa=@IdEmpresa
select @IdRubro_calculado= IdRubro_tot_pagar from ro_rubros_calculados where IdEmpresa=@IdEmpresa-- obteniendo el idrubro desde parametros
insert into ro_rol_detalle
(IdEmpresa,				IdRol,			IdSucursal,						IdEmpleado,			IdRubro,			Orden,			Valor
,rub_visible_reporte,	Observacion)

select
@IdEmpresa				,@IdRol				,IdSucursal			,IdEmpleado		,@IdRubro_calculado	,'1000'			,(ISNULL( [22],0) -ISNULL( [23],0))
,1						,'Liquido a recibir'	
FROM (
    SELECT 
        rol_det.IdEmpresa,emp.IdEmpleado, emp.IdSucursal,IdNominaTipo,IdNominaTipoLiqui ,IdPeriodo ,rol_det.IdRubro, Valor
FROM            dbo.ro_rol_detalle AS rol_det INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON rol_det.IdEmpresa = rub.IdEmpresa AND rol_det.IdRubro = rub.IdRubro INNER JOIN
                         dbo.ro_rol ON rol_det.IdEmpresa = dbo.ro_rol.IdEmpresa AND rol_det.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado AS emp ON rol_det.IdEmpresa = emp.IdEmpresa AND rol_det.IdEmpleado = emp.IdEmpleado
	 where rol_det.IdEmpresa=@IdEmpresa
	 and IdNominaTipo=@IdNomina
	 and IdNominaTipoLiqui=@IdNominaTipo
	 and IdPeriodo=@IdPEriodo
and rol_det.IdSucursal = @IdSucursalFin
	 and rol_det.IdRol=@IdRol
) as s
PIVOT
(
   max([Valor])
    FOR [IdRubro] IN ([22],[23])
)AS pvt






update ro_periodo_x_ro_Nomina_TipoLiqui set Procesado='S' where IdEmpresa=@IdEmpresa and IdNomina_Tipo=@IdNomina and IdNomina_TipoLiqui=@IdNominaTipo and IdPeriodo=@IdPEriodo

END