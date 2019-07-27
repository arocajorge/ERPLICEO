CREATE procedure [web].[SPROL_025]
@IdEmpresa int,
@IdSucursalIni int,
@IdSucursalFin int,
@IdNomina_Tipo int,
@IdPeriodo int,
@IdDivisionIni int,
@IdDivisionFin int,
@IdAreaIni int,
@IdAreaFin int
as

BEGIN

SELECT ROW_NUMBER() over (order by ro_empleado.IdEmpresa, ro_empleado.IdSucursal) as IdRow, dbo.ro_empleado.IdEmpresa, dbo.ro_empleado.IdEmpleado, dbo.ro_empleado.IdPersona, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_sexo, dbo.ro_contrato.FechaInicio, 
dbo.tb_Catalogo.ca_descripcion, dbo.ro_periodo.pe_FechaIni, dbo.ro_contrato.IdNomina, dbo.ro_Nomina_Tipo.Descripcion ,dbo.ro_periodo.pe_FechaFin, dbo.ro_rol.IdRol, dbo.ro_rol.IdPeriodo, dbo.ro_rol_detalle.IdRubro, dbo.ro_rubro_tipo.ru_descripcion, dbo.ro_rol_detalle.Valor,
case when (dbo.ro_contrato.FechaInicio <= dbo.ro_periodo.pe_FechaIni) then '360'
else ((datediff(MONTH, dbo.ro_contrato.FechaInicio,dbo.ro_periodo.pe_FechaFin )+1)*30)-(day(ro_contrato.FechaInicio))+1
end as dias,
dbo.ro_Division.Descripcion Division,
dbo.ro_area.Descripcion Area
FROM            dbo.ro_periodo_x_ro_Nomina_TipoLiqui INNER JOIN
dbo.ro_rol ON dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa = dbo.ro_rol.IdEmpresa AND dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_Tipo = dbo.ro_rol.IdNominaTipo AND 
dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_TipoLiqui = dbo.ro_rol.IdNominaTipoLiqui AND dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo = dbo.ro_rol.IdPeriodo INNER JOIN
dbo.ro_periodo ON dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa = dbo.ro_periodo.IdEmpresa AND dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo = dbo.ro_periodo.IdPeriodo INNER JOIN
dbo.ro_rol_detalle ON dbo.ro_rol.IdEmpresa = dbo.ro_rol_detalle.IdEmpresa AND dbo.ro_rol.IdRol = dbo.ro_rol_detalle.IdRol INNER JOIN
dbo.ro_empleado 
INNER JOIN dbo.ro_Division on dbo.ro_Division.IdEmpresa = dbo.ro_empleado.IdEmpresa and dbo.ro_Division.IdDivision = dbo.ro_empleado.IdDivision
INNER JOIN dbo.ro_area on dbo.ro_area.IdEmpresa = dbo.ro_empleado.IdEmpresa and dbo.ro_area.IdArea = dbo.ro_empleado.IdArea
INNER JOIN
dbo.ro_contrato ON dbo.ro_empleado.IdEmpresa = dbo.ro_contrato.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_contrato.IdEmpleado INNER JOIN
dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
dbo.tb_Catalogo ON dbo.tb_persona.pe_sexo = dbo.tb_Catalogo.CodCatalogo ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_rol_detalle.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
dbo.ro_rubro_tipo ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_rubro_tipo.IdEmpresa AND dbo.ro_rol_detalle.IdRubro = dbo.ro_rubro_tipo.IdRubro INNER JOIN
dbo.ro_rubros_calculados ON dbo.ro_rubro_tipo.IdEmpresa = dbo.ro_rubros_calculados.IdEmpresa
inner join dbo.ro_Nomina_Tipo on dbo.ro_Nomina_Tipo.IdEmpresa = dbo.ro_contrato.IdEmpresa and dbo.ro_Nomina_Tipo.IdNomina_Tipo = dbo.ro_contrato.IdNomina 
WHERE  dbo.ro_empleado.em_status = 'EST_ACT' and  dbo.ro_empleado.em_estado = 'A' 
and dbo.ro_rubros_calculados.IdRubro_DIV = dbo.ro_rol_detalle.IdRubro
and dbo.ro_empleado.IdEmpresa = @IdEmpresa
and dbo.ro_rol.IdSucursal >= @IdSucursalIni
and dbo.ro_rol.IdSucursal <= @IdSucursalFin
and dbo.ro_contrato.IdNomina = @IdNomina_Tipo
and dbo.ro_rol.IdPeriodo = @IdPeriodo
and dbo.ro_empleado.IdDivision >=  @IdDivisionIni
and dbo.ro_empleado.IdDivision <=  @IdDivisionFin
and dbo.ro_empleado.IdArea >= @IdAreaIni
and dbo.ro_empleado.IdArea <= @IdAreaFin
order by dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre asc
end