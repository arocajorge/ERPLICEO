
CREATE PROCEDURE [dbo].[spRo_Cierre_Rol]
	@IdEmpresa int,
	@IdPeriodo int,
	@IdNomina_Tipo int,
	@IdNomina_TipoLiqui int,
    @IdRol int
	
AS
BEGIN

update ro_periodo_x_ro_Nomina_TipoLiqui set cerrado='S'
where IdEmpresa=@IdEmpresa and IdNomina_Tipo=@IdNomina_Tipo and IdNomina_TipoLiqui=@IdNomina_TipoLiqui
 and IdPeriodo=@IdPeriodo

 update ro_rol set cerrado='CERRADO'
where IdEmpresa=@IdEmpresa and IdNominaTipo=@IdNomina_Tipo and IdNominaTipoLiqui=@IdNomina_TipoLiqui
 and IdPeriodo=@IdPeriodo
 and IdRol=@IdRol

 IF(@IdNomina_TipoLiqui = 2)
 BEGIN
	 UPDATE ro_prestamo_detalle SET EstadoPago = 'CAN'
	 from(
		SELECT P.* FROM ro_prestamo P INNER JOIN
		ro_rol_detalle AS R ON R.IdEmpresa = P.IdEmpresa AND R.IdEmpleado = P.IdEmpleado
		WHERE R.IdEmpresa = @IdEmpresa AND R.IdRol = @IdRol
	 ) A
	 where ro_prestamo_detalle.IdEmpresa = @IdEmpresa AND @IdPeriodo = CAST( CAST(YEAR(FechaPago) AS VARCHAR(4)) + RIGHT('00'+CAST(MONTH(FechaPago) AS VARCHAR(2)),2) AS INT)	 
	 AND ro_prestamo_detalle.IdEmpresa = A.IdEmpresa AND ro_prestamo_detalle.IdPrestamo = A.IdPrestamo


	UPDATE ro_empleado_novedad_det SET EstadoCobro = 'CAN'
	FROM
	(
		SELECT N.* FROM ro_rol_detalle AS R
		INNER JOIN ro_empleado_Novedad AS N
		ON R.IDEMPRESA = N.IdEmpresa
		AND R.IdEmpleado = N.IdEmpleado
		WHERE R.IdEmpresa = @IdEmpresa
		AND R.IdRol = @IdRol
	)A
	WHERE ro_empleado_novedad_det.IdEmpresa = @IdEmpresa AND @IdPeriodo = CAST( CAST(YEAR(FechaPago) AS VARCHAR(4)) + RIGHT('00'+CAST(MONTH(FechaPago) AS VARCHAR(2)),2) AS INT)
	AND ro_empleado_novedad_det.IdEmpresa = A.IdEmpresa AND ro_empleado_novedad_det.IdNovedad = A.IdNovedad
END

IF(@IdNomina_TipoLiqui = 3)
BEGIN
	DECLARE @w_IdRubro_XIII int 
	SELECT @w_IdRubro_XIII = IdRubro_prov_DIII FROM ro_rubros_calculados WHERE IdEmpresa = @IdEmpresa
	UPDATE ro_rol_detalle_x_rubro_acumulado SET Estado = 'CAN'
	FROM(
		SELECT R_DE.IdEmpresa, A.IdRol, P_DE.pe_FechaIni, P_DE.pe_FechaFin, ro_rol_detalle.IdEmpleado
		FROM     ro_rol AS R_DE INNER JOIN
						  ro_periodo AS P_DE ON R_DE.IdEmpresa = P_DE.IdEmpresa AND R_DE.IdPeriodo = P_DE.IdPeriodo INNER JOIN
							  (SELECT R.IdEmpresa, R.IdRol, P.pe_FechaIni, P.pe_FechaFin
							   FROM      ro_rol AS R INNER JOIN
												 ro_periodo AS P ON R.IdEmpresa = P.IdEmpresa AND R.IdPeriodo = P.IdPeriodo
												 and r.IdNominaTipoLiqui not in (3,4)
												 ) AS A ON A.IdEmpresa = P_DE.IdEmpresa AND A.pe_FechaFin BETWEEN P_DE.pe_FechaIni AND P_DE.pe_FechaFin INNER JOIN
						  ro_rol_detalle ON R_DE.IdEmpresa = ro_rol_detalle.IdEmpresa AND R_DE.IdRol = ro_rol_detalle.IdRol 
		WHERE  (R_DE.IdEmpresa = @IdEmpresa) AND (R_DE.IdRol = @IdRol)
		group by R_DE.IdEmpresa, A.IdRol, P_DE.pe_FechaIni, P_DE.pe_FechaFin, ro_rol_detalle.IdEmpleado
	) AS ROLES
	WHERE ro_rol_detalle_x_rubro_acumulado.IdEmpresa = ROLES.IdEmpresa
	AND ro_rol_detalle_x_rubro_acumulado.IdRol = ROLES.IdRol
	AND ro_rol_detalle_x_rubro_acumulado.IdEmpleado = ROLES.IdEmpleado
	AND ro_rol_detalle_x_rubro_acumulado.IdRubro = @w_IdRubro_XIII
END

IF(@IdNomina_TipoLiqui = 4)
BEGIN
	DECLARE @w_IdRubro_XIV int 
	SELECT @w_IdRubro_XIV = IdRubro_prov_DIV FROM ro_rubros_calculados WHERE IdEmpresa = @IdEmpresa
	UPDATE ro_rol_detalle_x_rubro_acumulado SET Estado = 'CAN'
	FROM(
SELECT R_DE.IdEmpresa, A.IdRol, P_DE.pe_FechaIni, P_DE.pe_FechaFin, ro_rol_detalle.IdEmpleado
FROM     ro_rol AS R_DE INNER JOIN
                  ro_periodo AS P_DE ON R_DE.IdEmpresa = P_DE.IdEmpresa AND R_DE.IdPeriodo = P_DE.IdPeriodo INNER JOIN
                      (SELECT R.IdEmpresa, R.IdRol, P.pe_FechaIni, P.pe_FechaFin
                       FROM      ro_rol AS R INNER JOIN
                                         ro_periodo AS P ON R.IdEmpresa = P.IdEmpresa AND R.IdPeriodo = P.IdPeriodo
										 and r.IdNominaTipoLiqui not in (3,4)
										 ) AS A ON A.IdEmpresa = P_DE.IdEmpresa AND A.pe_FechaFin BETWEEN P_DE.pe_FechaIni AND P_DE.pe_FechaFin INNER JOIN
                  ro_rol_detalle ON R_DE.IdEmpresa = ro_rol_detalle.IdEmpresa AND R_DE.IdRol = ro_rol_detalle.IdRol 
WHERE  (R_DE.IdEmpresa = @IdEmpresa) AND (R_DE.IdRol = @IdRol)
group by R_DE.IdEmpresa, A.IdRol, P_DE.pe_FechaIni, P_DE.pe_FechaFin, ro_rol_detalle.IdEmpleado
) ROLES
	WHERE ro_rol_detalle_x_rubro_acumulado.IdEmpresa = ROLES.IdEmpresa
	AND ro_rol_detalle_x_rubro_acumulado.IdRol = ROLES.IdRol
	AND ro_rol_detalle_x_rubro_acumulado.IdEmpleado = ROLES.IdEmpleado
	AND ro_rol_detalle_x_rubro_acumulado.IdRubro = @w_IdRubro_XIV
END

END
