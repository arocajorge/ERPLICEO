--exec [web].[SPROL_013] 1,1,1,1,17,17,1,99999,1,99999,'01/01/2019','31/12/2019'

CREATE  PROCEDURE [web].[SPROL_013]
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

SELECT        dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa, dbo.ro_rol_detalle_x_rubro_acumulado.IdRol, dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpleado , dbo.ro_empleado.IdArea, dbo.ro_empleado.IdDivision, dbo.ro_rubros_calculados.IdRubro_DIII as IdRubro, 
                         dbo.ro_rol_detalle_x_rubro_acumulado.Valor AS Provision, dbo.ro_rol_detalle_x_rubro_acumulado.Estado, dbo.ro_rol_detalle_x_rubro_acumulado.IdSucursal, dbo.ro_empleado.em_codigo, 
                         dbo.ro_Departamento.de_descripcion, dbo.tb_sucursal.Su_Descripcion, dbo.tb_persona.pe_nombreCompleto, dbo.ro_Division.Descripcion AS Division, dbo.ro_area.Descripcion AS Area,  CONCAT(dbo.ro_periodo.pe_anio, '-', 
                         DATENAME(MONTH, dbo.ro_periodo.pe_FechaIni))  AS Mes, 0.00 AS Prestamo, su.Valor AS Sueldo--ROUND( dbo.ro_rol_detalle_x_rubro_acumulado.Valor * 12,2) AS Sueldo
FROM            dbo.ro_rol_detalle_x_rubro_acumulado INNER JOIN
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
							SELECT rd.IdEmpresa, rd.IdRol, rd.IdEmpleado, SUM(rd.Valor) AS Valor, ro_rubro_tipo.ru_tipo, ro_rol.IdNominaTipoLiqui
							FROM     ro_rol_detalle AS rd INNER JOIN
							ro_rubro_tipo ON rd.IdEmpresa = ro_rubro_tipo.IdEmpresa AND rd.IdRubro = ro_rubro_tipo.IdRubro INNER JOIN
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

union all

SELECT        dbo.ro_rol_detalle.IdEmpresa, dbo.ro_rol_detalle.IdRol, dbo.ro_rol_detalle.IdEmpleado , dbo.ro_empleado.IdArea, dbo.ro_empleado.IdDivision, dbo.ro_rubros_calculados.IdRubro_DIII, 
                         dbo.ro_rol_detalle.Valor AS Provision, '', dbo.ro_rol_detalle.IdSucursal, dbo.ro_empleado.em_codigo, 
                         dbo.ro_Departamento.de_descripcion, dbo.tb_sucursal.Su_Descripcion, dbo.tb_persona.pe_nombreCompleto, dbo.ro_Division.Descripcion AS Division, dbo.ro_area.Descripcion AS Area,  CONCAT(dbo.ro_periodo.pe_anio, '-', 
                         DATENAME(MONTH, dbo.ro_periodo.pe_FechaIni))  AS Mes, 0.00 AS Prestamo, su.Valor AS Sueldo
FROM            dbo.ro_rol_detalle INNER JOIN
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
							SELECT rd.IdEmpresa, rd.IdRol, rd.IdEmpleado, SUM(rd.Valor) AS Valor, ro_rubro_tipo.ru_tipo, ro_rol.IdNominaTipoLiqui
							FROM     ro_rol_detalle AS rd INNER JOIN
							ro_rubro_tipo ON rd.IdEmpresa = ro_rubro_tipo.IdEmpresa AND rd.IdRubro = ro_rubro_tipo.IdRubro INNER JOIN
							ro_rol ON rd.IdEmpresa = ro_rol.IdEmpresa AND rd.IdRol = ro_rol.IdRol INNER JOIN
							ro_rubros_calculados ON ro_rubro_tipo.IdEmpresa = ro_rubros_calculados.IdEmpresa
							WHERE  (ro_rubro_tipo.ru_tipo = 'I') AND (ro_rol.IdNominaTipoLiqui = 2)
							AND RD.IdRubro NOT IN (ro_rubros_calculados.IdRubro_fondo_reserva, ro_rubros_calculados.IdRubro_DIII, ro_rubros_calculados.IdRubro_DIV)
							AND ro_rubro_tipo.rub_grupo = 'INGRESOS'
							GROUP BY rd.IdEmpresa, rd.IdRol, rd.IdEmpleado, ro_rubro_tipo.ru_tipo, ro_rol.IdNominaTipoLiqui
						 ) AS Su on su.IdEmpresa = ro_rol_detalle.IdEmpresa
						 and su.IdRol = ro_rol_detalle.IdRol
						 and su.IdEmpleado = ro_rol_detalle.IdEmpleado
						 
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
						 and ro_periodo.pe_FechaIni between @fecha_inicio and @fecha_fin

END