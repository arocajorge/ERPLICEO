--exec [web].[SPROL_022]  1,1,2,201904

-- exec [web].[SPROL_022]  5,1,2,201906

CREATE  PROCEDURE [web].[SPROL_022]  
	@idempresa int,
	@idnomina_tipo int,
	@idnomina_Tipo_liq int,
	@idperiodo int
	
AS

BEGIN

--declare
--  @idempresa int,
--	@idnomina_tipo int,
--	@idnomina_Tipo_liq int,
--	@idperiodo int


--set @idempresa =1
--set @idnomina_tipo =1
--set @idnomina_Tipo_liq =2
--set @idperiodo =201901

declare 
@FechaInicio date,
@FechaFin date,
@IdRubroMatutino varchar(50),
@IdRubroVespertino varchar(50),
@IdRubroTotalPagar varchar(50),
@IdRubroPrmariaVespertina varchar(50),
@IdRubroBrigada varchar(50),
@IdRubroHorasAdicionales varchar(50),
@IdRubro_horas_control_salida varchar(50)

delete web.ro_SPROL_022 where IdEmpresa=@idempresa --and IdPeriodo=@idperiodo
select @IdRubroMatutino=IdRubro_horas_matutina, @IdRubroVespertino=IdRubro_horas_vespertina, @IdRubroTotalPagar = IdRubro_tot_pagar,
 @IdRubroPrmariaVespertina=IdRubro_primaria_vespertina,@IdRubroBrigada=IdRubro_horas_brigadas, @IdRubroHorasAdicionales=IdRubro_horas_adicionales, @IdRubro_horas_control_salida = IdRubro_horas_control_salida from ro_rubros_calculados 
 where IdEmpresa=@idempresa 
select @FechaInicio=pe_FechaIni, @FechaFin=pe_FechaFin from ro_periodo where IdEmpresa=IdEmpresa and IdPeriodo=@idperiodo




insert into web.ro_SPROL_022
SELECT e.IdEmpresa, e.IdDivision, e.IdSucursal, nc.IdNomina_TipoLiqui, e.IdArea, e.IdEmpleado, nc.IdJornada, nc.IdNomina_Tipo,  CAST(cast( year(nc.Fecha) as varchar(4))+ RIGHT('00'+CAST(MONTH(nc.Fecha) AS VARCHAR(2)),2) AS INT) IdPeriodo,
case when  j.Descripcion is null then '' else   j.Descripcion+'-'+CAST( nd.CantidadHoras as varchar)+'-'+CAST( nd.Valor / isnull(iif(nd.CantidadHoras = 0,1,nd.CantidadHoras),1) as varchar)  end Descripcion, 
case when  nc.IdJornada is null  then rub.ru_descripcion else   case when ( nd.IdRubro= rub_cal.IdRubro_horas_matutina or nd.IdRubro= rub_cal.IdRubro_horas_vespertina or nd.IdRubro=rub_cal.IdRubro_primaria_vespertina )and nc.IdJornada is not null  then   'SUELDO POR HORA' else    rub.ru_descripcion  end end ru_descripcion,
pe.pe_nombreCompleto AS empleado, 
c.ca_descripcion, rub.ru_tipo,
case when  nc.IdJornada is null then rub.ru_orden else  case when  ( nd.IdRubro= rub_cal.IdRubro_horas_matutina or nd.IdRubro= rub_cal.IdRubro_horas_vespertina or nd.IdRubro=rub_cal.IdRubro_primaria_vespertina ) then '07'     else  rub.ru_orden end  end ru_orden,
nd.Valor,nd.IdRubro
FROM     ro_empleado AS e INNER JOIN
                  ro_empleado_Novedad AS nc ON e.IdEmpresa = nc.IdEmpresa AND e.IdEmpresa = nc.IdEmpresa AND e.IdEmpleado = nc.IdEmpleado AND e.IdEmpleado = nc.IdEmpleado INNER JOIN
                  ro_empleado_novedad_det AS nd ON nc.IdEmpresa = nd.IdEmpresa AND nc.IdNovedad = nd.IdNovedad INNER JOIN
                  ro_rubro_tipo AS rub ON nd.IdEmpresa = rub.IdEmpresa AND nd.IdRubro = rub.IdRubro INNER JOIN
                  ro_rubros_calculados AS rub_cal ON rub.IdEmpresa = rub_cal.IdEmpresa INNER JOIN
                  ro_jornada AS j ON nc.IdEmpresa = j.IdEmpresa AND nc.IdJornada = j.IdJornada inner join
				  tb_persona as pe on pe.IdPersona = e.IdPersona left join
				  ro_catalogo as c on c.CodCatalogo = rub.rub_grupo
WHERE        (rub.ru_tipo = 'I') 
and nd.FechaPago between @FechaInicio and @FechaFin
and nc.IdEmpresa=@idempresa
and nc.IdNomina_Tipo=@idnomina_tipo
and IdNomina_TipoLiqui=@idnomina_Tipo_liq
--and per.pe_nombreCompleto like '%ACEVEDO%'
AND nc.IdJornada is not null
and CAST(cast( year(nc.Fecha) as varchar(4))+ RIGHT('00'+CAST(MONTH(nc.Fecha) AS VARCHAR(2)),2) AS INT) = @idperiodo
and nc.Estado = 'A'
 union all 

 SELECT    r.IdEmpresa,emp.IdDivision,r_dt.IdSucursal, r.IdNominaTipoLiqui,  emp.IdArea,emp.IdEmpleado,1,r.IdNominaTipo,r.IdPeriodo, null, rub.ru_descripcion,pers.pe_apellido+' '+pers.pe_nombre, cate.ca_descripcion, rub.ru_tipo, rub.ru_orden, r_dt.Valor ,
 r_dt.IdRubro  
FROM            dbo.ro_rol AS r INNER JOIN
                         dbo.ro_rol_detalle AS r_dt ON r.IdEmpresa = r_dt.IdEmpresa AND r.IdRol = r_dt.IdRol INNER JOIN
                         dbo.ro_rubro_tipo AS rub ON r_dt.IdEmpresa = rub.IdEmpresa AND r_dt.IdRubro = rub.IdRubro INNER JOIN
						 dbo.ro_catalogo as cate on cate.CodCatalogo = rub.rub_grupo INNER JOIN
                         dbo.ro_empleado AS emp ON r_dt.IdEmpresa = emp.IdEmpresa AND r_dt.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.tb_persona AS pers ON emp.IdPersona = pers.IdPersona
						 
						 WHERE  
						  r_dt.IdEmpresa=@idempresa
						 and r.IdNominaTipo=@idnomina_tipo
						 and r.IdNominaTipoLiqui=@idnomina_Tipo_liq
						 and r.IdPeriodo=@idperiodo
						 and rub.ru_tipo='I'
						 and r_dt.IdRubro not in(@IdRubroMatutino,@IdRubroVespertino,@IdRubroPrmariaVespertina ,@IdRubroHorasAdicionales, @IdRubro_horas_control_salida)
						 and not exists
						 (
						 select * from web.ro_SPROL_022  d

						 where d.IdEmpresa = r_dt.IdEmpresa
						 and d.IdNomina_Tipo = r.IdNominaTipo
						 and d.IdNomina_TipoLiqui = r.IdNominaTipoLiqui
						 and d.IdPeriodo = r.IdPeriodo
						 and d.IdEmpleado = r_dt.IdEmpleado
						 and d.IdRubro = r_dt.IdRubro

						 and d.IdEmpresa = @idempresa
						 and d.IdNomina_Tipo = @idnomina_tipo
						 and d.IdNomina_TipoLiqui = @idnomina_Tipo_liq
						 and d.IdPeriodo = @idperiodo
						 )

						--delete web.ro_SPROL_022 where IdRubro=@IdRubroBrigada and Descripcion is null and IdPeriodo=@idperiodo -- SE comentareo 26/06/2019
						 if(@idempresa<>5)-- se añadio x estas lineas del if
						 Begin
						 	delete web.ro_SPROL_022 where IdRubro=@IdRubroBrigada and Descripcion is null and IdPeriodo=@idperiodo 
						 End
					

						SELECT r.IdEmpresa, r.IdDivision, r.IdSucursal, r.IdNomina_TipoLiqui, r.IdArea, r.IdEmpleado, r.IdJornada, r.IdNomina_Tipo, r.IdPeriodo, r.Descripcion, r.ru_descripcion, r.empleado, r.ca_descripcion, r.ru_tipo, r.ru_orden, r.Valor, r.IdRubro, 
										  ro_Nomina_Tipo.Descripcion AS NomNomina, ro_Nomina_Tipoliqui.DescripcionProcesoNomina AS NomNominaTipo, tb_sucursal.Su_Descripcion, @FechaInicio AS FechaIni, @FechaFin AS FechaFin, ro_Division.Descripcion AS NomDivision, 
										  ro_area.Descripcion AS NomArea
						FROM     web.ro_SPROL_022 AS r LEFT OUTER JOIN
										  ro_Division INNER JOIN
										  ro_area ON ro_Division.IdEmpresa = ro_area.IdEmpresa AND ro_Division.IdDivision = ro_area.IdDivision AND ro_Division.IdEmpresa = ro_area.IdEmpresa AND ro_Division.IdDivision = ro_area.IdDivision ON 
										  r.IdEmpresa = ro_area.IdEmpresa AND r.IdDivision = ro_area.IdDivision AND r.IdArea = ro_area.IdArea LEFT OUTER JOIN
										  tb_sucursal ON r.IdEmpresa = tb_sucursal.IdEmpresa AND r.IdSucursal = tb_sucursal.IdSucursal LEFT OUTER JOIN
										  ro_Nomina_Tipoliqui INNER JOIN
										  ro_Nomina_Tipo ON ro_Nomina_Tipoliqui.IdEmpresa = ro_Nomina_Tipo.IdEmpresa AND ro_Nomina_Tipoliqui.IdNomina_Tipo = ro_Nomina_Tipo.IdNomina_Tipo AND ro_Nomina_Tipoliqui.IdEmpresa = ro_Nomina_Tipo.IdEmpresa AND 
										  ro_Nomina_Tipoliqui.IdNomina_Tipo = ro_Nomina_Tipo.IdNomina_Tipo ON r.IdEmpresa = ro_Nomina_Tipoliqui.IdEmpresa AND r.IdNomina_TipoLiqui = ro_Nomina_Tipoliqui.IdNomina_TipoLiqui AND 
										  r.IdNomina_Tipo = ro_Nomina_Tipoliqui.IdNomina_Tipo
										  WHERE R.IdPeriodo = @idperiodo   
										  -- Se añadio filtro x empresa, y agrupamiento  , para q no salgan repetidos los rubros(brigada) 27/06/2019
										  and  r.IdEmpresa =@idempresa Group By r.IdEmpresa, r.IdDivision, r.IdSucursal, r.IdNomina_TipoLiqui, r.IdArea,IdEmpleado, r.IdJornada,r.IdNomina_Tipo, r.IdPeriodo, r.Descripcion, r.ru_descripcion, r.empleado, r.ca_descripcion, r.ru_tipo, r.ru_orden, r.Valor, r.IdRubro
										    ,ro_Nomina_Tipo.Descripcion, ro_Nomina_Tipoliqui.DescripcionProcesoNomina,tb_sucursal.Su_Descripcion, ro_Division.Descripcion, ro_area.Descripcion
				  --and r.IdEmpleado = 222
			ORDER BY r.empleado

END