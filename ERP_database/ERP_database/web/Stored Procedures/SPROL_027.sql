

CREATE procedure [web].[SPROL_027]
@IdEmpresa int,
@IdSucursalIni int,
@IdSucursalFin int,
@IdNomina_Tipo int,
@IdDivisionIni int,
@IdDivisionFin int,
@IdAreaIni int,
@IdAreaFin int,
@FechaInicio date,
@FechaFin date
as

BEGIN

select a.IdEmpresa, a.IdSucursal, a.Nomina, a.Area, a.pe_nombre, a.pe_apellido,a.pe_cedulaRuc, a.em_codigo, a.IdEmpleado,a.pe_nombreCompleto,ROUND( sum(a.Valor),2)Valor, 0 Descuento,ROUND( sum( a.Valor),2)Total,
       a.IdDepartamento, IdDivision,a.IdArea,a.de_descripcion,a.Su_Descripcion
 from (
SELECT        dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa, dbo.ro_rol_detalle_x_rubro_acumulado.IdRol, dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpleado, dbo.ro_rol_detalle_x_rubro_acumulado.IdRubro, 
                         dbo.ro_rol_detalle_x_rubro_acumulado.Valor, dbo.ro_rol_detalle_x_rubro_acumulado.Estado, dbo.ro_rol_detalle_x_rubro_acumulado.IdSucursal, dbo.ro_empleado.em_codigo, dbo.tb_persona.pe_apellido, 
                         dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_nombreCompleto, dbo.tb_persona.pe_cedulaRuc, dbo.ro_area.Descripcion AS Area, dbo.ro_Division.Descripcion AS Division, dbo.ro_Nomina_Tipo.Descripcion AS Nomina, 
                         dbo.ro_periodo.pe_FechaIni, dbo.ro_periodo.pe_FechaFin, dbo.ro_empleado.IdArea, dbo.ro_empleado.IdDivision, dbo.ro_empleado.IdDepartamento, dbo.ro_Departamento.de_descripcion, dbo.tb_sucursal.Su_Descripcion
FROM            dbo.ro_rol_detalle_x_rubro_acumulado INNER JOIN
                         dbo.ro_rol ON dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa = dbo.ro_rol.IdEmpresa AND dbo.ro_rol_detalle_x_rubro_acumulado.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui ON dbo.ro_rol.IdEmpresa = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa AND dbo.ro_rol.IdNominaTipo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_Tipo AND 
                         dbo.ro_rol.IdNominaTipoLiqui = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_TipoLiqui AND dbo.ro_rol.IdPeriodo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo INNER JOIN
                         dbo.ro_periodo ON dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa = dbo.ro_periodo.IdEmpresa AND dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo = dbo.ro_periodo.IdPeriodo INNER JOIN
                         dbo.ro_empleado ON dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_Division ON dbo.ro_empleado.IdEmpresa = dbo.ro_Division.IdEmpresa AND dbo.ro_empleado.IdDivision = dbo.ro_Division.IdDivision INNER JOIN
                         dbo.ro_area ON dbo.ro_empleado.IdEmpresa = dbo.ro_area.IdEmpresa AND dbo.ro_empleado.IdArea = dbo.ro_area.IdArea INNER JOIN
                         dbo.ro_Nomina_Tipo ON dbo.ro_rol.IdEmpresa = dbo.ro_Nomina_Tipo.IdEmpresa AND dbo.ro_rol.IdNominaTipo = dbo.ro_Nomina_Tipo.IdNomina_Tipo INNER JOIN
                         dbo.ro_rubros_calculados ON dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa AND 
                         dbo.ro_rol_detalle_x_rubro_acumulado.IdRubro = dbo.ro_rubros_calculados.IdRubro_prov_DIII INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento INNER JOIN
                         dbo.tb_sucursal ON dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ro_rol_detalle_x_rubro_acumulado.IdSucursal = dbo.tb_sucursal.IdSucursal AND 
                         dbo.ro_rol.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ro_rol.IdSucursal = dbo.tb_sucursal.IdSucursal AND dbo.ro_empleado.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND 
                         dbo.ro_empleado.IdSucursal = dbo.tb_sucursal.IdSucursal
where ro_periodo.pe_FechaFin between @FechaInicio and @FechaFin
and ro_periodo.pe_FechaIni between @FechaInicio and @FechaFin
and ro_rol.IdNominaTipo=@IdNomina_Tipo
and ro_rol.IdEmpresa=@IdEmpresa
and ro_empleado.IdDivision>=@IdDivisionIni
and ro_empleado.IdDivision<=@IdDivisionFin

and ro_empleado.IdArea>=@IdAreaIni
and ro_empleado.IdArea<=@IdAreaFin

and ro_rol.IdSucursal>=@IdSucursalIni
and ro_rol.IdSucursal<=@IdSucursalFin



		)a	
		
		group by a.IdEmpresa, a.IdSucursal, a.Nomina, a.Area, a.pe_nombre, a.pe_apellido,a.pe_cedulaRuc, a.em_codigo, a.IdEmpleado,a.pe_nombreCompleto,
		 a.IdDepartamento, IdDivision,a.IdArea,a.de_descripcion, a.Su_Descripcion
				
end