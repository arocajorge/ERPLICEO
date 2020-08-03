/*
EXEC [dbo].[spRo_procesa_AjusteIR]
@IdEmpresa = 1,
@IdAnio = 2019,
@IdAjuste = 1,
@IdEmpleado = 0,
@IdSucursal = 1,
@IdUsuario ='ADMIN',
@Fecha = '2019/01/01',
@FechaCorte ='2019/11/30',
@Observacion =''
*/

CREATE PROCEDURE [dbo].[spRo_procesa_AjusteIR]
(
@IdEmpresa int,
@IdAnio int,
@IdAjuste numeric,
@IdEmpleado numeric,
@IdSucursal int,
@IdUsuario varchar(50),
@Fecha date,
@FechaCorte date,
@Observacion varchar(max)
)
AS
BEGIN

declare @w_IdAjuste numeric = 0, @w_Secuencia int, @IdPeriodoIni int, @IdPeriodoFin int, @MesesHastaDiciembre int, @IdRubroAportePatronal int, @IdRubro_IR int
set @IdPeriodoIni = dbo.fxGetIdPeriodo(DATEFROMPARTS(YEAR(@FechaCorte),1,1))
set @IdPeriodoFin = dbo.fxGetIdPeriodo(@FechaCorte)
set @MesesHastaDiciembre = 12 - month(@FechaCorte)
select @IdRubroAportePatronal = IdRubro_iess_perso, @IdRubro_IR = IdRubro_IR from ro_rubros_calculados where IdEmpresa = @IdEmpresa

PRINT 'GET ID'
BEGIN --GET ID
	IF(@IdAjuste = 0)
		BEGIN
			select @w_IdAjuste = max(IdAjuste)
			from [dbo].[ro_AjusteImpuestoRenta]
			where IdEmpresa = @IdEmpresa
			SET @w_IdAjuste = isnull(@w_IdAjuste,0)+1
			SET @IdAjuste = @w_IdAjuste
		END
END

PRINT 'GET SECUENCIA'
BEGIN --GET SECUENCIA
	select @w_Secuencia = max(Secuencia)
	from [dbo].[ro_AjusteImpuestoRentaDet]
	where IdEmpresa = @IdEmpresa and IdAjuste = @IdAjuste
	SET @w_Secuencia= ISNULL(@w_Secuencia,0)
END
PRINT 'INSERT O UPDATE DE CABECERA'
BEGIN --INSERT O UPDATE DE CABECERA
IF(@w_IdAjuste > 0)
	BEGIN
		INSERT INTO [dbo].[ro_AjusteImpuestoRenta]
           ([IdEmpresa]
           ,[IdAjuste]
           ,[IdAnio]
           ,[Fecha]
           ,[FechaCorte]
           ,[IdSucursal]
           ,[Observacion]
           ,[Estado]
           ,[IdUsuarioCreacion]
           ,[FechaCreacion])
     VALUES
           (@IdEmpresa
           ,@IdAjuste
           ,@IdAnio
           ,@Fecha
           ,@FechaCorte
           ,case when @IdSucursal = 0 THEN NULL ELSE @IdSucursal END
           ,@Observacion
           ,1
           ,@IdUsuario
           ,GETDATE())
	END
	ELSE
	BEGIN
		UPDATE [dbo].[ro_AjusteImpuestoRenta]
		SET 
		[FechaCorte] = @FechaCorte
		,[IdSucursal] = case when @IdSucursal = 0 THEN NULL ELSE @IdSucursal END
		,[Observacion] = @Observacion
		,[IdUsuarioModificacion] = @IdUsuario
		,[FechaModificacion] = GETDATE()
		WHERE IdEmpresa = @IdEmpresa and IdAjuste = @IdAjuste
	END
END
PRINT 'INSERT DEL DETALLE'
BEGIN --INSERT DEL DETALLE

	DELETE [dbo].[ro_AjusteImpuestoRentaDet] WHERE IdEmpresa = @IdEmpresa and IdAjuste = @IdAjuste and IdEmpleado between @IdEmpleado and case when @IdEmpleado = 0 then 999999999999 else @IdEmpleado end

	INSERT INTO [dbo].[ro_AjusteImpuestoRentaDet]
           ([IdEmpresa]
           ,[IdAjuste]
           ,[Secuencia]
           ,[IdEmpleado]
           ,[SueldoFechaCorte]
           ,[SueldoProyectado]
           ,[OtrosIngresos]
		   ,[IngresosLiquidos]
           ,[GastosPersonales]
           ,[AporteFechaCorte]
           ,[BaseImponible]
           ,[FraccionBasica]
           ,[Excedente]
           ,[ImpuestoFraccionBasica]
           ,[ImpuestoRentaCausado]
           ,[DescontadoFechaCorte]
           ,[LiquidacionFinal])
     SELECT
            e.IdEmpresa
           ,@IdAjuste
           ,ROW_NUMBER() over(order by e.IdEmpresa) + @w_Secuencia
           ,e.IdEmpleado
           ,0
           ,0
           ,0
           ,0
		   ,0
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0
		   FROM ro_empleado as e inner join 
		   ro_contrato as c on c.IdEmpresa = e.IdEmpresa and c.IdEmpleado = e.IdEmpleado
		   where e.IdEmpresa = @IdEmpresa and e.IdEmpleado between @IdEmpleado and case when @IdEmpleado = 0 then 999999999999 else @IdEmpleado end
		   and isnull(e.IdSucursal, @IdSucursal) between @IdSucursal and case when @IdSucursal = 0 then 999999999 else @IdSucursal end
		   and e.em_status NOT IN ('EST_LIQ','EST_PLQ')
		   and C.EstadoContrato NOT IN ('ECT_LIQ','ECT_PLQ')
		   AND E.em_estado = 'A'
		   and c.IdNomina = 1
		   and isnull(c.FechaFin,@FechaCorte) >= @FechaCorte

END

PRINT 'INSERT DEL DETALLE DE OTROS INGRESOS'
BEGIN --INSERT DETALLE DE OTROS INGRESOS
	IF(@w_IdAjuste > 0)
	BEGIN		
		INSERT INTO [dbo].[ro_AjusteImpuestoRentaDetOI]
				([IdEmpresa]
				,[IdAjuste]
				,[IdEmpleado]
				,[Secuencia]
				,[DescripcionOI]
				,[Valor])
		select A.IdEmpresa, @IdAjuste, A.IdEmpleado, ROW_NUMBER() OVER(PARTITION BY A.IdEmpresa, A.IdEmpleado order by A.IdEmpresa) Secuencia, b.DescripcionOI, B.Valor
		from(
			select d.IdEmpresa, d.IdEmpleado, ROW_NUMBER() over(partition by d.IdEmpresa, d.IdEmpleado order by d.IdEmpresa, d.IdEmpleado, d.IdAjuste desc) IdRow, d.IdAjuste
			from ro_AjusteImpuestoRentaDet d inner join 
			ro_AjusteImpuestoRenta c on d.IdEmpresa = c.IdEmpresa and d.IdAjuste = c.IdAjuste inner join
			ro_empleado as e on d.IdEmpresa = e.IdEmpresa and d.IdEmpleado = e.IdEmpleado
			WHERE c.Estado = 1 and c.IdEmpresa = @IdEmpresa and d.IdEmpleado between @IdEmpleado and case when @IdEmpleado = 0 then 9999999999 else @IdEmpleado end
			and e.IdSucursal between @IdSucursal and case when @IdSucursal = 0 then 9999999999 else @IdSucursal end
		) a INNER JOIN 
		ro_AjusteImpuestoRentaDetOI AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdAjuste = B.IdAjuste AND A.IdEmpleado = B.IdEmpleado
		where a.IdRow = 1
	END
END

PRINT 'UPDATE DE SUELDO A LA FECHA'
BEGIN --UPDATE DE SUELDO A LA FECHA
	UPDATE [dbo].[ro_AjusteImpuestoRentaDet] SET SueldoFechaCorte = a.Valor
	FROM(
		SELECT rd.IdEmpresa, rd.IdEmpleado, [dbo].[BankersRounding](SUM(rd.Valor),2) AS Valor, ro_rubro_tipo.ru_tipo, ro_rol.IdNominaTipoLiqui
		FROM     ro_rol_detalle(nolock) AS rd 	
		INNER JOIN ro_rubro_tipo ON rd.IdEmpresa = ro_rubro_tipo.IdEmpresa AND rd.IdRubro = ro_rubro_tipo.IdRubro INNER JOIN
		ro_rol ON rd.IdEmpresa = ro_rol.IdEmpresa AND rd.IdRol = ro_rol.IdRol INNER JOIN
		ro_rubros_calculados ON ro_rubro_tipo.IdEmpresa = ro_rubros_calculados.IdEmpresa
		WHERE  ro_rol.IdEmpresa = @IdEmpresa
		AND (ro_rubro_tipo.ru_tipo = 'I') AND (ro_rol.IdNominaTipoLiqui = 2) and ro_rol.IdNominaTipo = 1
		AND ro_rol.IdPeriodo BETWEEN @IdPeriodoIni and @IdPeriodoFin
		AND RD.IdRubro NOT IN (ro_rubros_calculados.IdRubro_fondo_reserva, ro_rubros_calculados.IdRubro_DIII, ro_rubros_calculados.IdRubro_DIV)
		AND ro_rubro_tipo.rub_grupo = 'INGRESOS' 
		and rd.IdEmpleado between @IdEmpleado and case when @IdEmpleado = 0 then 9999999999 else @IdEmpleado end
		GROUP BY rd.IdEmpresa, rd.IdEmpleado, ro_rubro_tipo.ru_tipo, ro_rol.IdNominaTipoLiqui 
	) A
	WHERE [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = @IdEmpresa
	AND [dbo].[ro_AjusteImpuestoRentaDet].IdAjuste = @IdAjuste
	and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = a.IdEmpresa
	and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpleado = a.IdEmpleado
END


PRINT 'UPDATE SUELDO DESDE FECHA CORTE A DICIEMBRE'
BEGIN --UPDATE SUELDO DESDE FECHA CORTE A DICIEMBRE
	IF(@IdPeriodoFin != CAST(CAST(@IdAnio AS VARCHAR)+'11' AS INT))
	BEGIN
		UPDATE [dbo].[ro_AjusteImpuestoRentaDet] SET SueldoProyectado = a.Valor * @MesesHastaDiciembre
		FROM(
			SELECT rd.IdEmpresa, rd.IdEmpleado, [dbo].[BankersRounding](SUM(rd.Valor),2) AS Valor, ro_rubro_tipo.ru_tipo, ro_rol.IdNominaTipoLiqui
			FROM     ro_rol_detalle(nolock) AS rd 	
			INNER JOIN ro_rubro_tipo ON rd.IdEmpresa = ro_rubro_tipo.IdEmpresa AND rd.IdRubro = ro_rubro_tipo.IdRubro INNER JOIN
			ro_rol ON rd.IdEmpresa = ro_rol.IdEmpresa AND rd.IdRol = ro_rol.IdRol INNER JOIN
			ro_rubros_calculados ON ro_rubro_tipo.IdEmpresa = ro_rubros_calculados.IdEmpresa
			WHERE  ro_rol.IdEmpresa = @IdEmpresa
			AND (ro_rubro_tipo.ru_tipo = 'I') AND (ro_rol.IdNominaTipoLiqui = 2) and ro_rol.IdNominaTipo = 1
			AND ro_rol.IdPeriodo = @IdPeriodoFin
			AND RD.IdRubro NOT IN (ro_rubros_calculados.IdRubro_fondo_reserva, ro_rubros_calculados.IdRubro_DIII, ro_rubros_calculados.IdRubro_DIV)
			AND ro_rubro_tipo.rub_grupo = 'INGRESOS' 
			and rd.IdEmpleado between @IdEmpleado and case when @IdEmpleado = 0 then 9999999999 else @IdEmpleado end
			GROUP BY rd.IdEmpresa, rd.IdEmpleado, ro_rubro_tipo.ru_tipo, ro_rol.IdNominaTipoLiqui 
		) A
		WHERE [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = @IdEmpresa
		AND [dbo].[ro_AjusteImpuestoRentaDet].IdAjuste = @IdAjuste
		and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = a.IdEmpresa
		and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpleado = a.IdEmpleado
	END
	ELSE
	BEGIN
		UPDATE [dbo].[ro_AjusteImpuestoRentaDet] SET SueldoProyectado = a.Valor * @MesesHastaDiciembre
		FROM(
			SELECT rd.IdEmpresa, rd.IdEmpleado, [dbo].[BankersRounding](SUM(rd.Valor),2) AS Valor, ro_rubro_tipo.ru_tipo, ro_rol.IdNominaTipoLiqui
			FROM     ro_rol_detalle(nolock) AS rd 	
			INNER JOIN ro_rubro_tipo ON rd.IdEmpresa = ro_rubro_tipo.IdEmpresa AND rd.IdRubro = ro_rubro_tipo.IdRubro INNER JOIN
			ro_rol ON rd.IdEmpresa = ro_rol.IdEmpresa AND rd.IdRol = ro_rol.IdRol INNER JOIN
			ro_rubros_calculados ON ro_rubro_tipo.IdEmpresa = ro_rubros_calculados.IdEmpresa
			WHERE  ro_rol.IdEmpresa = @IdEmpresa
			AND (ro_rubro_tipo.ru_tipo = 'I') AND (ro_rol.IdNominaTipoLiqui = 2) and ro_rol.IdNominaTipo = 1
			AND ro_rol.IdPeriodo = CAST(CAST(@IdAnio AS VARCHAR)+'12' AS INT)
			AND RD.IdRubro NOT IN (ro_rubros_calculados.IdRubro_fondo_reserva, ro_rubros_calculados.IdRubro_DIII, ro_rubros_calculados.IdRubro_DIV)
			AND ro_rubro_tipo.rub_grupo = 'INGRESOS' 
			and rd.IdEmpleado between @IdEmpleado and case when @IdEmpleado = 0 then 9999999999 else @IdEmpleado end
			GROUP BY rd.IdEmpresa, rd.IdEmpleado, ro_rubro_tipo.ru_tipo, ro_rol.IdNominaTipoLiqui 
		) A
		WHERE [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = @IdEmpresa
		AND [dbo].[ro_AjusteImpuestoRentaDet].IdAjuste = @IdAjuste
		and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = a.IdEmpresa
		and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpleado = a.IdEmpleado
	END
END

PRINT 'UPDATE OTROS INGRESOS'
BEGIN --UPDATE OTROS INGRESOS
	UPDATE [dbo].[ro_AjusteImpuestoRentaDet] SET OtrosIngresos = dbo.BankersRounding(a.Valor,2)
	FROM(
		SELECT A.IdEmpresa, A.IdEmpleado, SUM(A.Valor) Valor
		FROM ro_AjusteImpuestoRentaDetOI AS A
		WHERE A.IdEmpresa = @IdEmpresa
		and a.IdAjuste = @IdAjuste
		AND A.IdEmpleado BETWEEN @IdEmpleado AND CASE WHEN @IdEmpleado = 0 THEN 9999999999 ELSE @IdEmpleado END
		group by A.IdEmpresa, A.IdEmpleado
	) A
	WHERE [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = @IdEmpresa
	AND [dbo].[ro_AjusteImpuestoRentaDet].IdAjuste = @IdAjuste
	and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = a.IdEmpresa
	and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpleado = a.IdEmpleado
END

PRINT 'UPDATE INGRESOS LIQUIDOS'
BEGIN --UPDATE INGRESOS LIQUIDOS
	UPDATE ro_AjusteImpuestoRentaDet SET IngresosLiquidos = SueldoFechaCorte + SueldoProyectado + OtrosIngresos 
	WHERE IdEmpresa = @IdEmpresa AND IdAjuste = @IdAjuste
	AND IdEmpleado BETWEEN @IdEmpleado AND CASE WHEN @IdEmpleado = 0 THEN 9999999999 ELSE @IdEmpleado END
END

PRINT 'UPDATE GASTOS PERSONALES'
BEGIN --UPDATE GASTOS PERSONALES
	UPDATE [dbo].[ro_AjusteImpuestoRentaDet] SET GastosPersonales = dbo.BankersRounding(a.Valor,2)
	FROM(
		SELECT A.IdEmpresa, A.IdEmpleado, SUM(A.Valor) Valor 
		FROM ro_empleado_gatos_x_anio A
		WHERE A.IdEmpresa = @IdEmpresa
		AND A.AnioFiscal = @IdAnio
		AND A.IdEmpleado BETWEEN @IdEmpresa AND CASE WHEN @IdEmpleado = 0 THEN 999999999 ELSE @IdEmpleado END
		group by A.IdEmpresa, A.IdEmpleado
	) A
	WHERE [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = @IdEmpresa
	AND [dbo].[ro_AjusteImpuestoRentaDet].IdAjuste = @IdAjuste
	and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = a.IdEmpresa
	and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpleado = a.IdEmpleado
END

PRINT 'UPDATE APORTE PATRONAL'
BEGIN --UPDATE APORTE PATRONAL
	UPDATE [dbo].[ro_AjusteImpuestoRentaDet] SET AporteFechaCorte = dbo.BankersRounding((SueldoFechaCorte + SueldoProyectado)*0.0945,2)
	WHERE [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = @IdEmpresa
	AND [dbo].[ro_AjusteImpuestoRentaDet].IdAjuste = @IdAjuste	
	and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpleado between @IdEmpresa AND CASE WHEN @IdEmpleado = 0 THEN 999999999 ELSE @IdEmpleado END
END

PRINT 'UPDATE BASE IMPONIBLE'
BEGIN --UPDATE BASE IMPONIBLE
	UPDATE [dbo].[ro_AjusteImpuestoRentaDet] SET BaseImponible = dbo.BankersRounding(IngresosLiquidos - GastosPersonales - AporteFechaCorte,2)
	WHERE [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = @IdEmpresa
	AND [dbo].[ro_AjusteImpuestoRentaDet].IdAjuste = @IdAjuste	
	and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpleado between @IdEmpresa AND CASE WHEN @IdEmpleado = 0 THEN 999999999 ELSE @IdEmpleado END
END

PRINT 'UPDATE DATOS TABLA DE IMPUESTO A LA RENTA'
BEGIN --UPDATE DATOS TABLA DE IMPUESTO A LA RENTA
UPDATE [dbo].[ro_AjusteImpuestoRentaDet] SET FraccionBasica = dbo.BankersRounding(a.FraccionBasica,2), Excedente = dbo.BankersRounding(a.Excedente,2), ImpuestoFraccionBasica = a.ImpFraccionBasica, ImpuestoRentaCausado = dbo.BankersRounding(A.Excedente + ImpFraccionBasica,2)
	FROM(
		SELECT a.IdEmpresa, a.IdEmpleado, A.BaseImponible - B.FraccionBasica FraccionBasica, (A.BaseImponible - B.FraccionBasica) * (B.Por_ImpFraccion_Exce /100) Excedente, Por_ImpFraccion_Exce, b.ImpFraccionBasica
		FROM ro_AjusteImpuestoRentaDet A INNER JOIN 
		ro_tabla_Impu_Renta B ON A.BaseImponible BETWEEN B.FraccionBasica AND B.ExcesoHasta
		WHERE B.AnioFiscal = @IdAnio
		AND A.IdEmpresa = @IdEmpresa
		AND A.IdAjuste = @IdAjuste
		and a.IdEmpleado between @IdEmpresa AND CASE WHEN @IdEmpleado = 0 THEN 999999999 ELSE @IdEmpleado END
		) A
	WHERE [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = @IdEmpresa
	AND [dbo].[ro_AjusteImpuestoRentaDet].IdAjuste = @IdAjuste
	and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = a.IdEmpresa
	and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpleado = a.IdEmpleado
END

BEGIN --UPDATE IMPUESTO A LA RENTA DESCONTADO
IF(MONTH(@FechaCorte) > 1)
BEGIN

if(cast(getdate() as date) <= @FechaCorte)
begin
	set @IdPeriodoFin = dbo.fxGetIdPeriodo(DATEADD(month,-1, @FechaCorte))
end

	UPDATE [dbo].[ro_AjusteImpuestoRentaDet] SET DescontadoFechaCorte = dbo.BankersRounding(a.Valor,2)
	FROM(
		SELECT A.IdEmpresa, B.IdEmpleado, SUM(B.Valor) Valor
		FROM ro_rol AS A INNER JOIN 
		ro_rol_detalle AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdRol = B.IdRol
		WHERE A.IdEmpresa = @IdEmpresa AND A.IdNominaTipo = 1 AND A.IdNominaTipoLiqui = 2 
		AND B.IdRubro = @IdRubro_IR AND A.IdPeriodo BETWEEN @IdPeriodoIni AND @IdPeriodoFin
		AND B.IdEmpleado BETWEEN @IdEmpleado AND CASE WHEN @IdEmpleado = 0 THEN 9999999999 ELSE @IdEmpleado END
		GROUP BY A.IdEmpresa, B.IdEmpleado
	) A
	WHERE [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = @IdEmpresa
	AND [dbo].[ro_AjusteImpuestoRentaDet].IdAjuste = @IdAjuste
	and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = a.IdEmpresa
	and [dbo].[ro_AjusteImpuestoRentaDet].IdEmpleado = a.IdEmpleado
END
ELSE
BEGIN
	UPDATE [dbo].[ro_AjusteImpuestoRentaDet] SET DescontadoFechaCorte = 0
	WHERE [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = @IdEmpresa
	AND [dbo].[ro_AjusteImpuestoRentaDet].IdAjuste = @IdAjuste
END
	UPDATE [dbo].[ro_AjusteImpuestoRentaDet] SET LiquidacionFinal = ImpuestoRentaCausado - DescontadoFechaCorte
	WHERE [dbo].[ro_AjusteImpuestoRentaDet].IdEmpresa = @IdEmpresa
	AND [dbo].[ro_AjusteImpuestoRentaDet].IdAjuste = @IdAjuste
END
END