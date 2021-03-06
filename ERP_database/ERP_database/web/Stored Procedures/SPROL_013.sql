﻿CREATE  PROCEDURE [web].[SPROL_013]
@idempresa int,
@idnomina int,
@IdSucursalInicio int,
@IdSucursalFin int,
@IdEmpleadoInicio numeric,
@IdEmpleadoFin numeric,
@IdDivisionInicio int,
@IdDivisionFin int,
@IdAreaInicion int,
@IdAreafin int,
@fecha_inicio date,
@fecha_fin date
AS


--declare 

--@idempresa int,
--@idnomina int,
--@IdSucursalInicio int,
--@IdSucursalFin int,
--@IdPeriodo int


--set @idempresa= 1
--set @idnomina =1
--set @IdSucursalInicio= 1
--set @IdSucursalFin =1
--set @IdPeriodo=20191211


BEGIN
		 Set nocount on
 select * from ( 
SELECT    dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa, dbo.ro_rol_detalle_x_rubro_acumulado.IdRol, dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpleado , dbo.ro_empleado.IdArea, dbo.ro_empleado.IdDivision, dbo.ro_rubros_calculados.IdRubro_DIII as IdRubro, 
                         [dbo].[BankersRounding](dbo.ro_rol_detalle_x_rubro_acumulado.Valor,2) AS Provision, dbo.ro_rol_detalle_x_rubro_acumulado.Estado, dbo.ro_rol_detalle_x_rubro_acumulado.IdSucursal, dbo.ro_empleado.em_codigo, 
                        dbo.ro_Departamento.de_descripcion, dbo.tb_sucursal.Su_Descripcion, dbo.tb_persona.pe_nombreCompleto, dbo.ro_Division.Descripcion AS Division, dbo.ro_area.Descripcion AS Area,  CONCAT(dbo.ro_periodo.pe_anio, '-', 

                         DATENAME(MONTH, dbo.ro_periodo.pe_FechaIni))  AS Mes, round(isnull(g.ValorAnticipo,0),2) AS Prestamo, [dbo].[BankersRounding](su.Valor,2) AS Sueldo--ROUND( dbo.ro_rol_detalle_x_rubro_acumulado.Valor * 12,2) AS Sueldo
						 ,dbo.ro_periodo.pe_anio,dbo.ro_periodo.pe_mes -- se añadio, para filtrar  27/08/2019 by Acueva
FROM            dbo.ro_rol_detalle_x_rubro_acumulado(nolock) INNER JOIN
                         dbo.ro_rol ON dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa = dbo.ro_rol.IdEmpresa AND dbo.ro_rol_detalle_x_rubro_acumulado.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado ON dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_Division ON dbo.ro_empleado.IdEmpresa = dbo.ro_Division.IdEmpresa AND dbo.ro_empleado.IdDivision = dbo.ro_Division.IdDivision INNER JOIN
                         dbo.ro_area ON dbo.ro_empleado.IdArea = dbo.ro_area.IdArea AND dbo.ro_empleado.IdEmpresa = dbo.ro_area.IdEmpresa INNER JOIN
                         dbo.tb_sucursal ON dbo.ro_rol.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ro_rol.IdSucursal = dbo.tb_sucursal.IdSucursal INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui ON dbo.ro_rol.IdEmpresa = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa AND dbo.ro_rol.IdNominaTipo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_Tipo AND 
                         dbo.ro_rol.IdNominaTipoLiqui = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_TipoLiqui AND dbo.ro_rol.IdPeriodo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo INNER JOIN
                         dbo.ro_periodo ON dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa = dbo.ro_periodo.IdEmpresa AND dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo = dbo.ro_periodo.IdPeriodo INNER JOIN
                         dbo.ro_rubros_calculados ON dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND dbo.ro_rol_detalle_x_rubro_acumulado.IdRubro = dbo.ro_rubros_calculados.IdRubro_prov_DIII INNER JOIN
						 (
							SELECT rd.IdEmpresa, rd.IdRol, rd.IdEmpleado, [dbo].[BankersRounding](SUM(rd.Valor),2) AS Valor, ro_rubro_tipo.ru_tipo, ro_rol.IdNominaTipoLiqui
							FROM     ro_rol_detalle(nolock) AS rd 	
					
							INNER JOIN ro_rubro_tipo ON rd.IdEmpresa = ro_rubro_tipo.IdEmpresa AND rd.IdRubro = ro_rubro_tipo.IdRubro INNER JOIN
							ro_rol ON rd.IdEmpresa = ro_rol.IdEmpresa AND rd.IdRol = ro_rol.IdRol INNER JOIN
							ro_rubros_calculados ON ro_rubro_tipo.IdEmpresa = ro_rubros_calculados.IdEmpresa
							WHERE  (ro_rubro_tipo.ru_tipo = 'I') AND (ro_rol.IdNominaTipoLiqui = 2)
							AND RD.IdRubro NOT IN (ro_rubros_calculados.IdRubro_fondo_reserva, ro_rubros_calculados.IdRubro_DIII, ro_rubros_calculados.IdRubro_DIV)
							AND ro_rubro_tipo.rub_grupo = 'INGRESOS'
							GROUP BY rd.IdEmpresa, rd.IdRol, rd.IdEmpleado, ro_rubro_tipo.ru_tipo, ro_rol.IdNominaTipoLiqui
						

						 ) AS Su on su.IdEmpresa = ro_rol_detalle_x_rubro_acumulado.IdEmpresa
						 and su.IdRol = ro_rol_detalle_x_rubro_acumulado.IdRol
						 and su.IdEmpleado = ro_rol_detalle_x_rubro_acumulado.IdEmpleado LEFT JOIN
						 (
							SELECT IdEmpresa, IdEmpleado, IdRubro, [dbo].[BankersRounding](SUM(Valor),2) ValorAnticipo
							FROM ro_EmpleadoAnticipoBeneficio
							where IdEmpresa = @idempresa
							and (FechaDesde between @fecha_inicio and @fecha_fin
							or FechaHasta between @fecha_inicio and @fecha_fin) and Estado = 1
							group by IdEmpresa, IdEmpleado, IdRubro
						 ) G ON ro_empleado.IdEmpresa = G.IdEmpresa 
						 AND ro_empleado.IdEmpleado = G.IdEmpleado
						 and dbo.ro_rubros_calculados.IdRubro_prov_DIII = g.IdRubro INNER JOIN
						 (
							select a.IdEmpresa, a.IdEmpleado, a.IdContrato, dbo.fxGetIdPeriodo(a.FechaInicio) IdPeriodoIni, dbo.fxGetIdPeriodo(a.FechaFin) IdPeriodoFin
							from(
							select c.idempresa, c.idempleado, c.idcontrato,c.FechaInicio, c.FechaFin,
							ROW_NUMBER() over(Partition by c.IdEmpresa, c.IdEmpleado order by c.IdEmpresa, c.IdEmpleado, fechaInicio desc)IdRow
							from ro_contrato as c inner join 
							ro_empleado as e on c.IdEmpresa = e.IdEmpresa and c.IdEmpleado = e.IdEmpleado
							where c.IdEmpresa = @IdEmpresa 
							and c.FechaInicio <= @Fecha_fin
							and isnull(c.FechaFin,dateadd(year,50,GETDATE())) >= @fecha_fin
							) a where a.IdRow = 1
						 ) AS CA ON CA.IdEmpresa = ro_empleado.IdEmpresa and ca.IdEmpleado = ro_empleado.IdEmpleado 

						 where ro_rol.IdEmpresa=@idempresa
						 and ro_rol.IdSucursal>=@IdSucursalInicio
						 and ro_rol.IdSucursal<=@IdSucursalFin

						 and ro_rol.IdNominaTipo=@idnomina

						 and ro_rol_detalle_x_rubro_acumulado.IdEmpleado>=@IdEmpleadoInicio
						 and ro_rol_detalle_x_rubro_acumulado.IdEmpleado<=@IdEmpleadoFin


						  and ro_empleado.IdArea>=@IdAreaInicion
						 and ro_empleado.IdArea<=@IdAreafin

						  and ro_empleado.IdDivision>=@IdDivisionInicio
						 and ro_empleado.IdDivision<=@IdDivisionFin
						 and ro_periodo.pe_FechaIni between @fecha_inicio and @fecha_fin
						
						and ro_empleado.em_status<>'EST_PLQ' --22/11/2019 by Acueva, por q salian los liquidados
						and ro_empleado.em_status<>'EST_LIQ' --03/12/2019 by Acueva, por q salian los liquidados

						and ro_rol.IdPeriodo between ca.IdPeriodoIni and ca.IdPeriodoFin

union all

SELECT      dbo.ro_rol_detalle.IdEmpresa, dbo.ro_rol_detalle.IdRol, dbo.ro_rol_detalle.IdEmpleado , dbo.ro_empleado.IdArea, dbo.ro_empleado.IdDivision, dbo.ro_rubros_calculados.IdRubro_DIII, 
                         dbo.ro_rol_detalle.Valor AS Provision, '', dbo.ro_rol_detalle.IdSucursal, dbo.ro_empleado.em_codigo, 
                         dbo.ro_Departamento.de_descripcion, dbo.tb_sucursal.Su_Descripcion, dbo.tb_persona.pe_nombreCompleto, dbo.ro_Division.Descripcion AS Division, dbo.ro_area.Descripcion AS Area,  CONCAT(dbo.ro_periodo.pe_anio, '-', 
						
                         DATENAME(MONTH, dbo.ro_periodo.pe_FechaIni))  AS Mes, isnull(G.ValorAnticipo,0) AS Prestamo, [dbo].[BankersRounding](su.Valor,2) AS Sueldo
						 ,dbo.ro_periodo.pe_anio,dbo.ro_periodo.pe_mes -- se añadio, para filtrar  27/08/2019 by Acueva
FROM            dbo.ro_rol_detalle(nolock) INNER JOIN
                         dbo.ro_rol ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_rol.IdEmpresa AND dbo.ro_rol_detalle.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_rol_detalle.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_Division ON dbo.ro_empleado.IdEmpresa = dbo.ro_Division.IdEmpresa AND dbo.ro_empleado.IdDivision = dbo.ro_Division.IdDivision INNER JOIN
                         dbo.ro_area ON dbo.ro_empleado.IdArea = dbo.ro_area.IdArea AND dbo.ro_empleado.IdEmpresa = dbo.ro_area.IdEmpresa INNER JOIN
                         dbo.tb_sucursal ON dbo.ro_rol.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ro_rol.IdSucursal = dbo.tb_sucursal.IdSucursal INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui ON dbo.ro_rol.IdEmpresa = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa AND dbo.ro_rol.IdNominaTipo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_Tipo AND 
                         dbo.ro_rol.IdNominaTipoLiqui = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_TipoLiqui AND dbo.ro_rol.IdPeriodo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo INNER JOIN
                         dbo.ro_periodo ON dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa = dbo.ro_periodo.IdEmpresa AND dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo = dbo.ro_periodo.IdPeriodo INNER JOIN
                         dbo.ro_rubros_calculados ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND dbo.ro_rol_detalle.IdRubro = dbo.ro_rubros_calculados.IdRubro_DIII INNER JOIN
						 (
							SELECT rd.IdEmpresa, rd.IdRol, rd.IdEmpleado, [dbo].[BankersRounding](SUM(rd.Valor),2) AS Valor, ro_rubro_tipo.ru_tipo, ro_rol.IdNominaTipoLiqui
							FROM     ro_rol_detalle AS rd

							 INNER JOIN ro_rubro_tipo ON rd.IdEmpresa = ro_rubro_tipo.IdEmpresa AND rd.IdRubro = ro_rubro_tipo.IdRubro INNER JOIN
							ro_rol ON rd.IdEmpresa = ro_rol.IdEmpresa AND rd.IdRol = ro_rol.IdRol INNER JOIN
							ro_rubros_calculados ON ro_rubro_tipo.IdEmpresa = ro_rubros_calculados.IdEmpresa
							WHERE  (ro_rubro_tipo.ru_tipo = 'I') AND (ro_rol.IdNominaTipoLiqui = 2)
							AND RD.IdRubro NOT IN (ro_rubros_calculados.IdRubro_fondo_reserva, ro_rubros_calculados.IdRubro_DIII, ro_rubros_calculados.IdRubro_DIV)
							AND ro_rubro_tipo.rub_grupo = 'INGRESOS'
							GROUP BY rd.IdEmpresa, rd.IdRol, rd.IdEmpleado, ro_rubro_tipo.ru_tipo, ro_rol.IdNominaTipoLiqui
							
						 ) AS Su on su.IdEmpresa = ro_rol_detalle.IdEmpresa
						 and su.IdRol = ro_rol_detalle.IdRol
						 and su.IdEmpleado = ro_rol_detalle.IdEmpleado
				 -- Linea de abajo para q no filtre repetidos del select principal,by Acueva 28/08/2019, help 
						  and su.IdRol not in
						  (
						  SELECT   dbo.ro_rol_detalle_x_rubro_acumulado.IdRol

						FROM dbo.ro_rol_detalle_x_rubro_acumulado(nolock) INNER JOIN
                         dbo.ro_rol ON dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa = dbo.ro_rol.IdEmpresa AND dbo.ro_rol_detalle_x_rubro_acumulado.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_empleado ON dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_Division ON dbo.ro_empleado.IdEmpresa = dbo.ro_Division.IdEmpresa AND dbo.ro_empleado.IdDivision = dbo.ro_Division.IdDivision INNER JOIN
                         dbo.ro_area ON dbo.ro_empleado.IdArea = dbo.ro_area.IdArea AND dbo.ro_empleado.IdEmpresa = dbo.ro_area.IdEmpresa INNER JOIN
                         dbo.tb_sucursal ON dbo.ro_rol.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ro_rol.IdSucursal = dbo.tb_sucursal.IdSucursal INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui ON dbo.ro_rol.IdEmpresa = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa AND dbo.ro_rol.IdNominaTipo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_Tipo AND 
                         dbo.ro_rol.IdNominaTipoLiqui = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_TipoLiqui AND dbo.ro_rol.IdPeriodo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo INNER JOIN
                         dbo.ro_periodo ON dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa = dbo.ro_periodo.IdEmpresa AND dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo = dbo.ro_periodo.IdPeriodo INNER JOIN
                         dbo.ro_rubros_calculados ON dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND dbo.ro_rol_detalle_x_rubro_acumulado.IdRubro = dbo.ro_rubros_calculados.IdRubro_prov_DIII INNER JOIN
						 (
							SELECT rd.IdEmpresa, rd.IdRol, rd.IdEmpleado, round(SUM(rd.Valor),2) AS Valor, ro_rubro_tipo.ru_tipo, ro_rol.IdNominaTipoLiqui
							FROM     ro_rol_detalle AS rd 	
					
							INNER JOIN ro_rubro_tipo ON rd.IdEmpresa = ro_rubro_tipo.IdEmpresa AND rd.IdRubro = ro_rubro_tipo.IdRubro INNER JOIN
							ro_rol ON rd.IdEmpresa = ro_rol.IdEmpresa AND rd.IdRol = ro_rol.IdRol INNER JOIN
							ro_rubros_calculados ON ro_rubro_tipo.IdEmpresa = ro_rubros_calculados.IdEmpresa
							WHERE  (ro_rubro_tipo.ru_tipo = 'I') AND (ro_rol.IdNominaTipoLiqui = 2)
							AND RD.IdRubro NOT IN (ro_rubros_calculados.IdRubro_fondo_reserva, ro_rubros_calculados.IdRubro_DIII, ro_rubros_calculados.IdRubro_DIV)
							AND ro_rubro_tipo.rub_grupo = 'INGRESOS'
							GROUP BY rd.IdEmpresa, rd.IdRol, rd.IdEmpleado, ro_rubro_tipo.ru_tipo, ro_rol.IdNominaTipoLiqui
						 ) AS Su on su.IdEmpresa = ro_rol_detalle_x_rubro_acumulado.IdEmpresa
						 and su.IdRol = ro_rol_detalle_x_rubro_acumulado.IdRol
						 and su.IdEmpleado = ro_rol_detalle_x_rubro_acumulado.IdEmpleado 
						 
						 where ro_rol.IdEmpresa=@idempresa
						 and ro_rol.IdSucursal>=@IdSucursalInicio
						 and ro_rol.IdSucursal<=@IdSucursalFin
						 and ro_rol.IdNominaTipo=@idnomina
						 and ro_rol_detalle_x_rubro_acumulado.IdEmpleado>=@IdEmpleadoInicio
						 and ro_rol_detalle_x_rubro_acumulado.IdEmpleado<=@IdEmpleadoFin
						  and ro_empleado.IdArea>=@IdAreaInicion
						 and ro_empleado.IdArea<=@IdAreafin
						  and ro_empleado.IdDivision>=@IdDivisionInicio
						 and ro_empleado.IdDivision<=@IdDivisionFin
						 and ro_periodo.pe_FechaIni between @fecha_inicio and @fecha_fin
						  ) -- fin del not in   -- para q no filtre repetidos del select de arriba(PRINCIPAL)
						  LEFT JOIN
						 (
							SELECT IdEmpresa, IdEmpleado, IdRubro, ROUND(SUM(Valor),2) ValorAnticipo
							FROM ro_EmpleadoAnticipoBeneficio
							where IdEmpresa = @idempresa
							and (FechaDesde between @fecha_inicio and @fecha_fin
							or FechaHasta between @fecha_inicio and @fecha_fin) and Estado = 1
							group by IdEmpresa, IdEmpleado, IdRubro
						 ) G ON ro_empleado.IdEmpresa = G.IdEmpresa 
						 AND ro_empleado.IdEmpleado = G.IdEmpleado
						 and dbo.ro_rubros_calculados.IdRubro_prov_DIII = g.IdRubro

						 where ro_rol.IdEmpresa=@idempresa
						 and ro_rol.IdSucursal>=@IdSucursalInicio
						 and ro_rol.IdSucursal<=@IdSucursalFin

						 and ro_rol.IdNominaTipo=@idnomina

						 and ro_rol_detalle.IdEmpleado>=@IdEmpleadoInicio
						 and ro_rol_detalle.IdEmpleado<=@IdEmpleadoFin
						
						  and ro_empleado.IdArea>=@IdAreaInicion
						 and ro_empleado.IdArea<=@IdAreafin

						  and ro_empleado.IdDivision>=@IdDivisionInicio
						 and ro_empleado.IdDivision<=@IdDivisionFin
						 --and ro_empleado.IdEmpleado = 171
						 and ro_periodo.pe_FechaIni between @fecha_inicio and @fecha_fin
						 and ro_empleado.em_status<>'EST_PLQ' --22/11/2019 by Acueva, por q salian los liquidados
						 and ro_empleado.em_status<>'EST_LIQ' --03/12/2019 by Acueva, por q salian los liquidados

) t order by t.pe_anio,t.pe_mes

END

/*

EXEC [web].[SPROL_013]
@idempresa =5,
@idnomina=1,
@IdSucursalInicio=1,
@IdSucursalFin=999999,
@IdEmpleadoInicio=16,
@IdEmpleadoFin=16,
@IdDivisionInicio=1,
@IdDivisionFin=99999,
@IdAreaInicion=1,
@IdAreafin=99999,
@fecha_inicio='01-12-2018',
@fecha_fin='30-11-2019'

select a.IdEmpresa, a.IdEmpleado, a.IdContrato
from(
select c.idempresa, c.idempleado, c.idcontrato,c.FechaFin,
ROW_NUMBER() over(Partition by c.IdEmpresa, c.IdEmpleado order by c.IdEmpresa, c.IdEmpleado, fechaInicio desc)IdRow
from ro_contrato as c inner join 
ro_empleado as e on c.IdEmpresa = e.IdEmpresa and c.IdEmpleado = e.IdEmpleado
where c.IdEmpresa = 5 
and c.FechaInicio <= datefromparts(2019,12,31)
and isnull(c.FechaFin,dateadd(year,50,GETDATE())) >= datefromparts(2019,12,31)
) a where a.IdRow = 1

*/