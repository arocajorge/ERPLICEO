
CREATE procedure [web].[SPROL_012]
@IdEmpresa int,
@Fecha_desde date,
@Fecha_hasta date,
@IdRubro varchar(50)
as begin
select 
a.IdEmpresa, a.IdDepartamento, a.IdEmpleado, a.IdPrestamo, a.pe_cedulaRuc, 
                         a.pe_apellido, a.pe_nombre, '' EstadoPago, a.de_descripcion,a.Total_Prestamo, 
						sum(a.Total_Cancelado)Total_Cancelado,
						sum(a.Total_Pendiente_pago)Total_Pendiente_pago,
                          a.Observacion,a.Fecha_PriPago, getdate() Fecha_Transac,a.IdSucursal, a.IdRubro, a.ru_descripcion

from (

SELECT        dbo.ro_empleado.IdEmpresa, dbo.ro_Departamento.IdDepartamento, dbo.ro_empleado.IdEmpleado, dbo.ro_prestamo_detalle.IdPrestamo, dbo.tb_persona.pe_cedulaRuc, 
                         dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.ro_prestamo_detalle.EstadoPago, dbo.ro_Departamento.de_descripcion, dbo.ro_prestamo.MontoSol AS Total_Prestamo, 
						CASE WHEN dbo.ro_prestamo_detalle.EstadoPago = 'CAN' THEN SUM(dbo.ro_prestamo_detalle.TotalCuota) ELSE 0 END Total_Cancelado,
						CASE WHEN dbo.ro_prestamo_detalle.EstadoPago = 'PEN' THEN SUM(dbo.ro_prestamo_detalle.TotalCuota) ELSE 0 END Total_Pendiente_pago,
                          dbo.ro_prestamo.Observacion,ro_prestamo.Fecha_PriPago, ro_prestamo.Fecha_Transac, ro_empleado.IdSucursal, ro_prestamo.IdRubro, ru.ru_descripcion

FROM            dbo.ro_prestamo_detalle INNER JOIN
                         dbo.ro_prestamo ON dbo.ro_prestamo_detalle.IdEmpresa = dbo.ro_prestamo.IdEmpresa AND dbo.ro_prestamo_detalle.IdPrestamo = dbo.ro_prestamo.IdPrestamo INNER JOIN
                         dbo.ro_empleado ON dbo.ro_prestamo.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_prestamo.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_prestamo.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona
						 left join ro_rubro_tipo as ru on ru.IdEmpresa = ro_prestamo.IdEmpresa
						 AND RU.IdRubro = ro_prestamo.IdRubro
						 	  where CAST( dbo.ro_prestamo.Fecha_Transac as date) between @Fecha_desde and @Fecha_hasta
							  and   dbo.ro_prestamo.IdEmpresa=@IdEmpresa 
							  and ro_prestamo.IdRubro like case when len(ltrim(rtrim(@IdRubro))) = 0 or @IdRubro is null then '%%' else @IdRubro end 
						 group by
						 dbo.ro_empleado.IdEmpresa,  dbo.ro_Departamento.IdDepartamento, dbo.ro_empleado.IdEmpleado, dbo.ro_prestamo_detalle.IdPrestamo, dbo.tb_persona.pe_cedulaRuc, 
                         dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.ro_prestamo_detalle.EstadoPago, dbo.ro_Departamento.de_descripcion,  
                          dbo.ro_prestamo.Observacion,ro_prestamo.Fecha_PriPago, ro_prestamo.Fecha_Transac, ro_empleado.IdSucursal,ro_prestamo.IdRubro, ru.ru_descripcion, dbo.ro_prestamo.MontoSol

						    )a

							group by 

							a.IdEmpresa, a.IdDepartamento, a.IdEmpleado, a.IdPrestamo, a.pe_cedulaRuc, 
                         a.pe_apellido, a.pe_nombre, a.de_descripcion,a.Total_Prestamo, 
                          a.Observacion,a.Fecha_PriPago,a.IdSucursal, a.IdRubro, a.ru_descripcion

						  end