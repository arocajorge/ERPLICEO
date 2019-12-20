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

declare @w_IdAjuste numeric, @w_Secuencia int

BEGIN --GET ID
	IF(@IdAjuste = 0)
		BEGIN
			select @w_IdAjuste = max(IdAjuste)
			from [dbo].[ro_AjusteImpuestoRenta]
			where IdEmpresa = @IdEmpresa

			SET @IdAjuste = isnull(@w_IdAjuste,0) + 1
		END
END

BEGIN --GET SECUENCIA
	select @w_Secuencia = max(Secuencia)
	from [dbo].[ro_AjusteImpuestoRentaDet]
	where IdEmpresa = @IdEmpresa and IdAjuste = @IdAjuste
	SET @w_Secuencia= ISNULL(@w_Secuencia,0)
END

BEGIN --INSERT O UPDATE DE CABECERA
IF(@w_IdAjuste IS NOT NULL)
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
		   FROM ro_empleado as e inner join 
		   ro_contrato as c on c.IdEmpresa = e.IdEmpresa and c.IdEmpleado = e.IdEmpleado
		   where e.IdEmpresa = @IdEmpresa and e.IdEmpleado between @IdEmpleado and case when @IdEmpleado = 0 then 999999999999 else @IdEmpleado end
		   and e.IdSucursal between @IdSucursal and case when @IdSucursal = 0 then 999999999 else 0 end
		   and e.em_status NOT IN ('EST_LIQ','EST_PLQ')
		   and C.EstadoContrato NOT IN ('ECT_LIQ','ECT_PLQ')
		   AND E.em_estado = 'A'
		   and isnull(c.FechaFin,@FechaCorte) >= @FechaCorte

END

END

/*
EXEC [dbo].[spRo_procesa_AjusteIR]
@IdEmpresa = 1,
@IdAnio = 2019,
@IdAjuste = 0,
@IdEmpleado = 0,
@IdSucursal = 0,
@IdUsuario ='ADMIN',
@Fecha = '01/01/2019',
@FechaCorte ='2019/12/31',
@Observacion =''
*/