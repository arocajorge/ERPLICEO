
CREATE  PROCEDURE  [EntidadRegulatoria].[spROL_DecimosCSV] 
 @IdEmpresa int,
 @IdRol int,
 @IdRubro Int
 AS
 
 

 --declare

 --@IdEmpresa int,
 --@IdRol int,
 --@IdRubro Int
 --set @IdEmpresa =1
 --set @IdRol =12
 --set @IdRubro=24
	
	
BEGIN
	
	declare 
	@IdPeriodo int,
	@FechaI date,
	@FechaF date

	select @IdPeriodo= IdPEriodo from ro_rol where IdEmpresa=@IdEmpresa and IdRol=@IdRol
	
	select @FechaI= pe_FechaIni, @FechaF=pe_FechaFin from ro_periodo where IdEmpresa=@IdEmpresa and IdPeriodo=@IdPeriodo
   
select Tab_Valores_Decimos_x_Empleado.*,tab_DiasTrabajados_x_Empleado.DiasTrabajados as Dias_Decimo,tab_DiasTrabajados_x_Empleado.DiasTrabajados as DiasA_considerar_Decimo 
from 
(
			select A.IdEmpresa, A.IdEmpleado, A.pe_apellido,A.pe_nombre,A.pe_cedulaRuc,A.CodigoSectorial,A.ca_descripcion, sum(A.valor) as Valor, A.pe_sexo,A.Estado,A.em_fechaIngaRol
			from 
			(
SELECT        dbo.ro_empleado.IdEmpresa, dbo.ro_empleado.IdEmpleado, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_cedulaRuc, dbo.ro_empleado.CodigoSectorial, dbo.ro_cargo.ca_descripcion, 
                         dbo.ro_empleado.em_status, dbo.ro_periodo.pe_FechaIni, dbo.ro_periodo.pe_FechaFin, dbo.ro_rol_detalle.Valor, dbo.tb_persona.pe_sexo, dbo.ro_catalogo.ca_descripcion AS Estado, dbo.ro_empleado.em_fechaSalida, 
                         dbo.ro_empleado.em_fechaIngaRol
FROM            dbo.ro_empleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_cargo ON dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa AND dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo INNER JOIN
                         dbo.ro_rol_detalle ON dbo.ro_empleado.IdEmpresa = dbo.ro_rol_detalle.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_rol_detalle.IdEmpleado INNER JOIN
                         dbo.ro_catalogo ON dbo.ro_empleado.em_status = dbo.ro_catalogo.CodCatalogo INNER JOIN
                         dbo.ro_rol ON dbo.ro_rol_detalle.IdEmpresa = dbo.ro_rol.IdEmpresa AND dbo.ro_rol_detalle.IdRol = dbo.ro_rol.IdRol INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui ON dbo.ro_rol.IdEmpresa = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa AND dbo.ro_rol.IdNominaTipo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_Tipo AND 
                         dbo.ro_rol.IdNominaTipoLiqui = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_TipoLiqui AND dbo.ro_rol.IdPeriodo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo INNER JOIN
                         dbo.ro_periodo ON dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa = dbo.ro_periodo.IdEmpresa AND dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo = dbo.ro_periodo.IdPeriodo
					       where ro_empleado.IdEmpresa=@IdEmpresa
						  -- and ro_empleado.em_status='EST_ACT'
						   and ro_empleado.em_estado='A'
						   and ro_rol_detalle.IdRubro= @IdRubro 
						  and dbo.ro_rol.IdRol=@IdRol
						

			) A
			group by A.IdEmpresa,A.IdEmpleado, A.pe_apellido,A.pe_nombre,A.pe_cedulaRuc,A.CodigoSectorial,A.ca_descripcion,A.pe_sexo,A.Estado,A.em_fechaIngaRol

) 
Tab_Valores_Decimos_x_Empleado  

left join
(
select CON.IdEmpresa,CON.IdEmpleado, 


case when (CON.FechaInicio <= @FechaI) then '360'
else ((datediff(MONTH, CON.FechaInicio,@FechaF )+1)*30)-(day(CON.FechaInicio))+1
end as DiasTrabajados


from ro_contrato as CON
where CON.EstadoContrato='ECT_ACT' and CON.Estado='A'and  CON.IdEmpresa=@IdEmpresa) tab_DiasTrabajados_x_Empleado
on tab_DiasTrabajados_x_Empleado.IdEmpresa=Tab_Valores_Decimos_x_Empleado.IdEmpresa and tab_DiasTrabajados_x_Empleado.IdEmpleado=Tab_Valores_Decimos_x_Empleado.IdEmpleado


ORDER BY pe_apellido asc
end