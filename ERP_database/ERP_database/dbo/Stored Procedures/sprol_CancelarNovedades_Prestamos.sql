

CREATE procedure [dbo].[sprol_CancelarNovedades_Prestamos] 
@IdEmpresa int,
@IdNomina int,
@IdNominaTipo int,
@IdPeriodo int,
@IdSucursal int,
@IdRol int
as
begin

declare
@FechaInicio date,
@fechaFin date

select @FechaInicio=pe_FechaIni,@fechaFin=pe_FechaFin from ro_periodo where IdPeriodo=@IdPeriodo and @IdEmpresa=@IdEmpresa
update ro_empleado_novedad_det set EstadoCobro='CAN'       
FROM            dbo.ro_empleado_novedad_det INNER JOIN
                         dbo.ro_rol_x_empleado_novedades ON dbo.ro_empleado_novedad_det.IdEmpresa = dbo.ro_rol_x_empleado_novedades.IdEmpresa AND 
                         dbo.ro_empleado_novedad_det.IdEmpresa = dbo.ro_rol_x_empleado_novedades.IdEmpresa_nov AND dbo.ro_empleado_novedad_det.IdNovedad = dbo.ro_rol_x_empleado_novedades.IdNovedad AND 
                         dbo.ro_empleado_novedad_det.Secuencia = dbo.ro_rol_x_empleado_novedades.Secuencia

						 where ro_rol_x_empleado_novedades.IdEmpresa=@IdEmpresa
						 and ro_rol_x_empleado_novedades.IdRol=@IdRol



						 update ro_prestamo_detalle set EstadoPago='CAN'
	FROM            dbo.ro_rol_x_prestamo_detalle INNER JOIN
                         dbo.ro_prestamo_detalle ON dbo.ro_rol_x_prestamo_detalle.IdEmpresa = dbo.ro_prestamo_detalle.IdEmpresa AND dbo.ro_rol_x_prestamo_detalle.IdEmpresa_pre = dbo.ro_prestamo_detalle.IdEmpresa AND 
                         dbo.ro_rol_x_prestamo_detalle.IdPrestamo = dbo.ro_prestamo_detalle.IdPrestamo AND dbo.ro_rol_x_prestamo_detalle.NumCuota = dbo.ro_prestamo_detalle.NumCuota

						 where ro_rol_x_prestamo_detalle.IdEmpresa=@IdEmpresa
						 and ro_rol_x_prestamo_detalle.IdRol=@IdRol

						 update ro_rol set Cerrado='CERRADO' 
						 where IdEmpresa=@IdEmpresa 
						 and  IdNominaTipo=@IdNomina
						 and IdNominaTipoLiqui=@IdNominaTipo
						 and IdPeriodo=@IdPeriodo
						 AND IdRol=@IdRol
end
