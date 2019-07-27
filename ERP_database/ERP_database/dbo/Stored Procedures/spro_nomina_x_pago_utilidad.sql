
create PROCEDURE [dbo].[spro_nomina_x_pago_utilidad] 

@Idempresa int,
@IdNomina int,
@FechaInicio date,
@FechaFin date
AS
BEGIN

/*
declare
@Idempresa int,
@IdNomina int,
@FechaInicio date,
@FechaFin date

set @Idempresa =1
set @IdNomina =1
set @FechaInicio ='2017/01/01'
set @FechaFin ='2017/12/31'
	
*/
SELECT     emp.IdEmpresa,emp.IdEmpleado,   car.ca_descripcion, per.pe_apellido, per.pe_nombre, per.pe_cedulaRuc, emp.em_status, emp.em_fechaIngaRol, emp.em_fechaIngaRol em_fecha_ingreso, emp.em_fechaSalida,count( cont.FechaInicio) as num_contratos,
ISNULL((select COUNT( c.IdEmpleado) from ro_cargaFamiliar c 
where
( c.IdEmpresa=emp.IdEmpresa 
and c.IdEmpleado=emp.IdEmpleado 
and c.Estado='A'
and DATEDIFF(year, @FechaFin  , c.FechaNacimiento)<18
and c.TipoFamiliar in ('HIJA','T_CFA03'))-- HIJOS
or
(c.FechaNacimiento <@FechaFin
AND c.TipoFamiliar='T_CFA04'-- ESPOSA/ESPOSO
 AND c.Estado='A')
group by c.IdEmpresa,c.IdEmpleado
),0) as num_cargas
FROM            dbo.tb_persona AS per INNER JOIN
                         dbo.ro_empleado AS emp ON per.IdPersona = emp.IdPersona INNER JOIN
                         dbo.ro_cargo AS car ON emp.IdEmpresa = car.IdEmpresa AND emp.IdCargo = car.IdCargo INNER JOIN
                         dbo.ro_contrato AS cont ON emp.IdEmpresa = cont.IdEmpresa AND emp.IdEmpleado = cont.IdEmpleado
		
where 
(cont.FechaInicio<@FechaFin and emp.em_status='EST_ACT')
or (emp.em_fechaSalida between @FechaInicio and @FechaFin)
					 
group by emp.IdEmpresa,emp.IdEmpleado ,car.ca_descripcion, per.pe_apellido, per.pe_nombre, per.pe_cedulaRuc, emp.em_status, emp.em_fechaIngaRol, emp.em_fechaSalida

END
