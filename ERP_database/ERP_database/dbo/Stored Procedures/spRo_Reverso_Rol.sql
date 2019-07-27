

CREATE PROCEDURE [dbo].[spRo_Reverso_Rol]
	@IdEmpresa int,
	@IdNomina_Tipo int,
	@IdNomina_TipoLiqui int,
	@IdPeriodo int,
	@IdRol int,
	@TipoReverso int
	
	
AS

BEGIN
/*
declare

	@IdEmpresa int,
	@IdNomina_Tipo int,
	@IdNomina_TipoLiqui int,
	@IdPeriodo int


	set @IdEmpresa=1
	set @IdNomina_Tipo=1
	set @IdNomina_TipoLiqui=2
	set @IdPeriodo=201704
	*/
DECLARE 

@fechai date,
@fechaf date


-- eleiminando comprobante contable detalle 
if(@TipoReverso=1)
delete ct_cbtecble_det
FROM            dbo.ro_Comprobantes_Contables INNER JOIN
                         dbo.ct_cbtecble_det ON dbo.ro_Comprobantes_Contables.IdEmpresa = dbo.ct_cbtecble_det.IdEmpresa AND dbo.ro_Comprobantes_Contables.IdTipoCbte = dbo.ct_cbtecble_det.IdTipoCbte AND 
                         dbo.ro_Comprobantes_Contables.IdCbteCble = dbo.ct_cbtecble_det.IdCbteCble
						 where ro_Comprobantes_Contables.IdEmpresa=@IdEmpresa
						and IdNomina=@IdNomina_Tipo
						and IdNominaTipo=@IdNomina_TipoLiqui
						and IdPeriodo=@IdPeriodo


-- eliminando comprobante contable cabecera 
if(@TipoReverso=1)
delete ct_cbtecble
FROM            dbo.ro_Comprobantes_Contables AS comp_rol INNER JOIN
                  dbo.ct_cbtecble ON comp_rol.IdEmpresa = dbo.ct_cbtecble.IdEmpresa AND comp_rol.IdTipoCbte = dbo.ct_cbtecble.IdTipoCbte AND comp_rol.IdCbteCble = dbo.ct_cbtecble.IdCbteCble
			where comp_rol.IdEmpresa=@IdEmpresa
			and comp_rol.IdNomina=@IdNomina_Tipo
			and IdNominaTipo=@IdNomina_TipoLiqui
			and comp_rol.IdPeriodo=@IdPeriodo


-- eliminando comprobante contable rol
if(@TipoReverso=1)
delete ro_Comprobantes_Contables 
			where IdEmpresa=@IdEmpresa
			and ro_Comprobantes_Contables.IdNomina=@IdNomina_Tipo
			and ro_Comprobantes_Contables.IdNominaTipo=@IdNomina_TipoLiqui
			and IdPeriodo=@IdPeriodo




update ro_empleado_novedad_det set EstadoCobro='PEND'       
FROM            dbo.ro_empleado_novedad_det INNER JOIN
                         dbo.ro_rol_x_empleado_novedades ON dbo.ro_empleado_novedad_det.IdEmpresa = dbo.ro_rol_x_empleado_novedades.IdEmpresa AND 
                         dbo.ro_empleado_novedad_det.IdEmpresa = dbo.ro_rol_x_empleado_novedades.IdEmpresa_nov AND dbo.ro_empleado_novedad_det.IdNovedad = dbo.ro_rol_x_empleado_novedades.IdNovedad AND 
                         dbo.ro_empleado_novedad_det.Secuencia = dbo.ro_rol_x_empleado_novedades.Secuencia

						 where ro_rol_x_empleado_novedades.IdEmpresa=@IdEmpresa
						 and ro_rol_x_empleado_novedades.IdRol=@IdRol



						 update ro_prestamo_detalle set EstadoPago='PEND'
	FROM            dbo.ro_rol_x_prestamo_detalle INNER JOIN
                         dbo.ro_prestamo_detalle ON dbo.ro_rol_x_prestamo_detalle.IdEmpresa = dbo.ro_prestamo_detalle.IdEmpresa AND dbo.ro_rol_x_prestamo_detalle.IdEmpresa_pre = dbo.ro_prestamo_detalle.IdEmpresa AND 
                         dbo.ro_rol_x_prestamo_detalle.IdPrestamo = dbo.ro_prestamo_detalle.IdPrestamo AND dbo.ro_rol_x_prestamo_detalle.NumCuota = dbo.ro_prestamo_detalle.NumCuota

						 where ro_rol_x_prestamo_detalle.IdEmpresa=@IdEmpresa
						 and ro_rol_x_prestamo_detalle.IdRol=@IdRol
if(@TipoReverso=1)
						 update ro_rol set Cerrado='CERRADO' 
						 where IdEmpresa=@IdEmpresa 
						 AND IdRol=@IdRol


if(@TipoReverso=0)
						 update ro_rol set Cerrado='ABIERTO' 
						 where IdEmpresa=@IdEmpresa 
						 AND IdRol=@IdRol


END
