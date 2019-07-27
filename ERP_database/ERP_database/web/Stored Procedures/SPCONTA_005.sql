CREATE PROCEDURE [web].[SPCONTA_005]
(
@IdEmpresa int,
@IdPunto_cargo_grupo int,
@IdUsuario varchar(50),
@FechaIni date,
@FechaFin date,
@MostrarSaldo0 bit
)
AS
DELETE [web].[ct_SPCONTA_005] where IdUsuario = @IdUsuario

INSERT INTO [web].[ct_SPCONTA_005]
           ([IdEmpresa]
           ,[IdPunto_cargo]
           ,[IdUsuario]
           ,[IdPunto_cargo_grupo]
           ,[nom_punto_cargo]
           ,[nom_punto_cargo_grupo]
           ,[SaldoAnterior]
           ,[Debitos]
           ,[Creditos]
           ,[SaldoFinal])

SELECT d.IdEmpresa, d.IdPunto_cargo, @IdUsuario, d.IdPunto_cargo_grupo, d.nom_punto_cargo, c.nom_punto_cargo_grupo,0,0,0,0
FROM ct_punto_cargo AS d inner join ct_punto_cargo_grupo as c on c.IdEmpresa = d.IdEmpresa and c.IdPunto_cargo_grupo = d.IdPunto_cargo_grupo

BEGIN --SALDO INICIAL
	UPDATE [web].[ct_SPCONTA_005] set SaldoAnterior = A.Valor
	from(
	SELECT d.IdEmpresa, d.IdPunto_cargo, round(sum(d.dc_Valor),2) Valor
	FROM ct_cbtecble AS C INNER JOIN 
	ct_cbtecble_det AS D ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND C.IdCbteCble = D.IdCbteCble
	INNER JOIN WEB.tb_FiltroReportes as f on c.IdEmpresa = f.IdEmpresa and c.IdSucursal = f.IdSucursal and f.IdUsuario = @IdUsuario
	where c.IdEmpresa = @IdEmpresa and d.IdPunto_cargo_grupo = @IdPunto_cargo_grupo and c.cb_Fecha < @FechaIni 
	group by d.IdEmpresa, d.IdPunto_cargo
	) a
	where [web].[ct_SPCONTA_005].IdEmpresa = a.IdEmpresa
	and [web].[ct_SPCONTA_005].IdPunto_cargo = a.IdPunto_cargo
	and [web].[ct_SPCONTA_005].IdUsuario = @IdUsuario
END

BEGIN --DEBITOS
	UPDATE [web].[ct_SPCONTA_005] set Debitos = A.Valor
	from(
	SELECT d.IdEmpresa, d.IdPunto_cargo, round(sum(d.dc_Valor),2) Valor
	FROM ct_cbtecble AS C INNER JOIN 
	ct_cbtecble_det AS D ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND C.IdCbteCble = D.IdCbteCble
	INNER JOIN WEB.tb_FiltroReportes as f on c.IdEmpresa = f.IdEmpresa and c.IdSucursal = f.IdSucursal and f.IdUsuario = @IdUsuario
	where c.IdEmpresa = @IdEmpresa and d.IdPunto_cargo_grupo = @IdPunto_cargo_grupo and c.cb_Fecha BETWEEN @FechaIni AND @FechaFin AND D.dc_Valor > 0
	group by d.IdEmpresa, d.IdPunto_cargo
	) a
	where [web].[ct_SPCONTA_005].IdEmpresa = a.IdEmpresa
	and [web].[ct_SPCONTA_005].IdPunto_cargo = a.IdPunto_cargo
	and [web].[ct_SPCONTA_005].IdUsuario = @IdUsuario
END

BEGIN --CREDITOS
	UPDATE [web].[ct_SPCONTA_005] set Creditos = ABS(A.Valor)
	from(
	SELECT d.IdEmpresa, d.IdPunto_cargo, round(sum(d.dc_Valor),2) Valor
	FROM ct_cbtecble AS C INNER JOIN 
	ct_cbtecble_det AS D ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND C.IdCbteCble = D.IdCbteCble
	INNER JOIN WEB.tb_FiltroReportes as f on c.IdEmpresa = f.IdEmpresa and c.IdSucursal = f.IdSucursal and f.IdUsuario = @IdUsuario
	where c.IdEmpresa = @IdEmpresa and d.IdPunto_cargo_grupo = @IdPunto_cargo_grupo and c.cb_Fecha BETWEEN @FechaIni AND @FechaFin AND D.dc_Valor < 0
	group by d.IdEmpresa, d.IdPunto_cargo
	) a
	where [web].[ct_SPCONTA_005].IdEmpresa = a.IdEmpresa
	and [web].[ct_SPCONTA_005].IdPunto_cargo = a.IdPunto_cargo
	and [web].[ct_SPCONTA_005].IdUsuario = @IdUsuario
END

BEGIN --SALDO FINAL
	UPDATE [web].[ct_SPCONTA_005] set SaldoFinal = A.Valor
	from(
	SELECT d.IdEmpresa, d.IdPunto_cargo, round(sum(d.dc_Valor),2) Valor
	FROM ct_cbtecble AS C INNER JOIN 
	ct_cbtecble_det AS D ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND C.IdCbteCble = D.IdCbteCble
	INNER JOIN WEB.tb_FiltroReportes as f on c.IdEmpresa = f.IdEmpresa and c.IdSucursal = f.IdSucursal and f.IdUsuario = @IdUsuario
	where c.IdEmpresa = @IdEmpresa and d.IdPunto_cargo_grupo = @IdPunto_cargo_grupo and c.cb_Fecha <= @FechaFin 
	group by d.IdEmpresa, d.IdPunto_cargo
	) a
	where [web].[ct_SPCONTA_005].IdEmpresa = a.IdEmpresa
	and [web].[ct_SPCONTA_005].IdPunto_cargo = a.IdPunto_cargo
	and [web].[ct_SPCONTA_005].IdUsuario = @IdUsuario
END

IF(@MostrarSaldo0 = 0)
begin
delete [web].[ct_SPCONTA_005] where IdUsuario = @IdUsuario and SaldoFinal = 0
end

select [IdEmpresa]           ,[IdPunto_cargo]           ,[IdUsuario]           ,[IdPunto_cargo_grupo]           ,[nom_punto_cargo]           ,[nom_punto_cargo_grupo]
           ,[SaldoAnterior]           ,[Debitos]           ,[Creditos]           ,[SaldoFinal] from [web].[ct_SPCONTA_005]
		   where IdUsuario = @IdUsuario