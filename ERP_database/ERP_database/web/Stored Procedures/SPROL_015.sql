
CREATE procedure [web].[SPROL_015]
(
@FechaInicio date,
@FechaFin date,
@IdEmpresa int)

as begin
select emp.IdEmpresa, Valores.IdEmpleado,emp.IdNominaTipo, emp.IdNominaTipoLiqui,emp.pe_cedulaRuc, emp.pe_nombreCompleto, 
sum(Valores.Decimocuarto)/3 Decimocuarto, sum(Valores.DecimoTercero)/3 DecimoTercero,sum( Valores.Vacaciones)/3Vacaciones

from
(
SELECT        r_detalle.IdEmpresa, rol.IdNominaTipo, rol.IdNominaTipoLiqui, rol.IdPeriodo, r_detalle.IdEmpleado, rubros.ru_descripcion, pers.pe_nombreCompleto, pers.pe_cedulaRuc, period.pe_FechaIni, period.pe_FechaFin
FROM            dbo.ro_rol_detalle_x_rubro_acumulado AS r_detalle INNER JOIN
                         dbo.ro_rol AS rol ON r_detalle.IdEmpresa = rol.IdEmpresa AND r_detalle.IdRol = rol.IdRol INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom ON r_detalle.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo AND rol.IdEmpresa = pe_x_nom.IdEmpresa AND rol.IdNominaTipo = pe_x_nom.IdNomina_Tipo AND rol.IdNominaTipoLiqui = pe_x_nom.IdNomina_TipoLiqui AND 
                         rol.IdPeriodo = pe_x_nom.IdPeriodo INNER JOIN
                         dbo.ro_periodo AS period ON pe_x_nom.IdEmpresa = period.IdEmpresa AND pe_x_nom.IdPeriodo = period.IdPeriodo INNER JOIN
                         dbo.ro_rubro_tipo AS rubros ON r_detalle.IdEmpresa = rubros.IdEmpresa AND r_detalle.IdRubro = rubros.IdRubro INNER JOIN
                         dbo.ro_empleado AS emp ON r_detalle.IdEmpresa = emp.IdEmpresa AND r_detalle.IdEmpleado = emp.IdEmpleado AND r_detalle.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.tb_persona AS pers ON emp.IdPersona = pers.IdPersona
WHERE        (r_detalle.IdEmpresa = @IdEmpresa) AND (r_detalle.IdRubro IN (199, 200, 295)) AND (emp.IdEmpresa = @IdEmpresa) AND (period.pe_FechaIni BETWEEN @FechaInicio AND @FechaFin) AND (emp.em_status = 'EST_ACT') AND 
                         (emp.em_estado = 'A')

						 GROUP BY
						  r_detalle.IdEmpresa, rol.IdNominaTipo, rol.IdNominaTipoLiqui, rol.IdPeriodo, r_detalle.IdEmpleado,
rubros.ru_descripcion, pers.pe_nombreCompleto, pers.pe_cedulaRuc, period.pe_FechaIni, period.pe_FechaFin
)
emp
inner join (
(
 SELECT    IdEmpresa,IdEmpleado,IdPeriodo,IdNominaTipo,IdNominaTipoLiqui, ISNULL( [199],0)DecimoTercero ,ISNULL([200],0)Decimocuarto,
 ISNULL([295],0)Vacaciones
FROM (
SELECT        dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa, dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpleado, ro_rol.IdPeriodo, ro_rol.IdNominaTipo, 
                         ro_rol.IdNominaTipoLiqui, dbo.ro_rol_detalle_x_rubro_acumulado.IdRubro, dbo.ro_rol_detalle_x_rubro_acumulado.Valor
FROM            dbo.ro_rol_detalle_x_rubro_acumulado INNER JOIN
                         dbo.ro_rubro_tipo ON dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa = dbo.ro_rubro_tipo.IdEmpresa AND dbo.ro_rol_detalle_x_rubro_acumulado.IdRubro = dbo.ro_rubro_tipo.IdRubro INNER JOIN
                         dbo.ro_rol ON dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa = dbo.ro_rol.IdEmpresa AND dbo.ro_rol_detalle_x_rubro_acumulado.IdRol = dbo.ro_rol.IdRol
WHERE        (dbo.ro_rol_detalle_x_rubro_acumulado.IdEmpresa = @IdEmpresa) AND (dbo.ro_rubro_tipo.IdEmpresa = @IdEmpresa) AND (dbo.ro_rol_detalle_x_rubro_acumulado.IdRubro IN (199, 200, 295))


) as valores
PIVOT
(
   max([Valor])
    FOR [IdRubro] IN ([199],[200],[295])
)AS pvt)
)
 Valores
 on emp.IdEmpresa=Valores.IdEmpresa
 and emp.IdEmpleado=Valores.IdEmpleado
 and emp.IdNominaTipo=Valores.IdNominaTipo
 and emp.IdNominaTipoLiqui=Valores.IdNominaTipoLiqui
 and emp.IdPeriodo=Valores.IdPeriodo


 group by
  emp.IdEmpresa, Valores.IdEmpleado,emp.IdNominaTipo, emp.IdNominaTipoLiqui,emp.pe_cedulaRuc, emp.pe_nombreCompleto

  end 
  