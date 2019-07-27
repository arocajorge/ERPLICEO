--[web].[SPCONTA_006] 2,2019,201901,201902,0,'admin',6,0,'ER'
CREATE PROCEDURE [web].[SPCONTA_004]
(
@IdEmpresa int,
@IdAnio int,
@IdPeriodoIni int,
@IdPeriodoFin int,
@MostrarSaldo0 bit,
@IdUsuario varchar(50),
@IdNivel int,
@MostrarAcumulado bit,
@Balance varchar(2)
)
AS
DELETE [web].[ct_CONTA_004] WHERE IdUsuario = @IdUsuario

INSERT INTO [web].[ct_CONTA_004]
           ([IdUsuario]
           ,[IdEmpresa]
           ,[IdCtaCble]
           ,[pc_Cuenta]
           ,[IdCtaCblePadre]
           ,[EsCtaUtilidad]
           ,[IdNivelCta]
           ,[IdGrupoCble]
           ,[gc_GrupoCble]
           ,[gc_estado_financiero]
           ,[gc_Orden]
           ,[EsCuentaMovimiento]
           ,[Naturaleza]
		   ,[Valor1]
		   ,[Valor2]
		   ,[Variacion]
		   ,[Signo])
SELECT @IdUsuario, 
ct_plancta.IdEmpresa, 
ct_plancta.IdCtaCble, 
ct_plancta.pc_Cuenta, 
ct_plancta.IdCtaCblePadre, 
CASE WHEN ct_anio_fiscal_x_cuenta_utilidad.IdCtaCble = ct_plancta.IdCtaCble THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS EsCtaUtilidad, 
ct_plancta.IdNivelCta,
ct_plancta.IdGrupoCble, 
ct_grupocble.gc_GrupoCble, 
ct_grupocble.gc_estado_financiero, 
ct_grupocble.gc_Orden,
0,ct_plancta.pc_Naturaleza,0,0,0,''
FROM            ct_anio_fiscal_x_cuenta_utilidad RIGHT OUTER JOIN
        ct_plancta ON ct_anio_fiscal_x_cuenta_utilidad.IdEmpresa = ct_plancta.IdEmpresa AND ct_anio_fiscal_x_cuenta_utilidad.IdCtaCble = ct_plancta.IdCtaCble LEFT OUTER JOIN
        ct_grupocble ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble
WHERE        (ISNULL(ct_anio_fiscal_x_cuenta_utilidad.IdanioFiscal, @IdAnio) = @IdAnio) AND ct_plancta.IdEmpresa = @IdEmpresa

BEGIN --INSERTO VALORES 1 Y 2
	UPDATE [web].[ct_CONTA_004] SET Valor1 = A.dc_Valor
	from(
	SELECT D.IdEmpresa, D.IdCtaCble, SUM(D.dc_Valor) dc_Valor
	FROM ct_cbtecble AS C INNER JOIN 
	ct_cbtecble_det AS D ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND C.IdCbteCble = D.IdCbteCble INNER JOIN 
	WEB.tb_FiltroReportes AS F ON C.IdEmpresa = F.IdEmpresa AND C.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario 
	WHERE C.IdEmpresa = @IdEmpresa AND YEAR(C.cb_Fecha) between case when @MostrarAcumulado = 1 then 0 else @IdAnio end and @IdAnio
	and cast(cast(year(c.cb_Fecha) as varchar(4)) + right('00'+cast(month(c.cb_Fecha) as varchar(2)),2) as int) = @IdPeriodoIni
	group by D.IdEmpresa, D.IdCtaCble
	)A WHERE [web].[ct_CONTA_004].IdEmpresa = A.IdEmpresa
	AND [web].[ct_CONTA_004].IdCtaCble = A.IdCtaCble
	

	UPDATE [web].[ct_CONTA_004] SET Valor2 = A.dc_Valor
	from(
	SELECT D.IdEmpresa, D.IdCtaCble, SUM(D.dc_Valor) dc_Valor
	FROM ct_cbtecble AS C INNER JOIN 
	ct_cbtecble_det AS D ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND C.IdCbteCble = D.IdCbteCble INNER JOIN 
	WEB.tb_FiltroReportes AS F ON C.IdEmpresa = F.IdEmpresa AND C.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario 
	WHERE C.IdEmpresa = @IdEmpresa AND YEAR(C.cb_Fecha) between case when @MostrarAcumulado = 1 then 0 else @IdAnio end and @IdAnio
	and cast(cast(year(c.cb_Fecha) as varchar(4)) + right('00'+cast(month(c.cb_Fecha) as varchar(2)),2) as int) = @IdPeriodoFin
	group by D.IdEmpresa, D.IdCtaCble
	)A WHERE [web].[ct_CONTA_004].IdEmpresa = A.IdEmpresa
	AND [web].[ct_CONTA_004].IdCtaCble = A.IdCtaCble
	
END


BEGIN --CUENTA UTILIDAD
	UPDATE [web].[ct_CONTA_004] SET Valor1 = a.Valor1, Valor2 = a.Valor2
	FROM(
	SELECT IdEmpresa, sum(Valor1) Valor1, sum(Valor2) Valor2
	FROM [web].[ct_CONTA_004]
	WHERE IdEmpresa = @IdEmpresa AND IdUsuario = @IdUsuario AND gc_estado_financiero = 'ER'
	GROUP BY IdEmpresa
	) A WHERE [web].[ct_CONTA_004].IdEmpresa = A.IdEmpresa
	and [web].[ct_CONTA_004].IdUsuario = @IdUsuario
	AND[web].[ct_CONTA_004].EsCtaUtilidad = 1
END



BEGIN --SUMATORIA ASCENDENTE

DECLARE @Contador int

select @Contador = max(IdNivelCta) 
from web.[ct_CONTA_004]
where IdUsuario = @IdUsuario
and IdEmpresa = @IdEmpresa
	WHILE @Contador > 0
	BEGIN

		UPDATE web.[ct_CONTA_004] 
		SET Valor1 = a.Valor1,
		Valor2 = a.Valor2
		FROM(
		SELECT        IdEmpresa, IdCtaCblePadre
		   ,SUM(Valor1) Valor1
		   ,SUM(Valor2) Valor2
		FROM            web.[ct_CONTA_004]
		where web.[ct_CONTA_004].IdEmpresa = @IdEmpresa
		and web.[ct_CONTA_004].IdUsuario = @IdUsuario
		GROUP BY IdEmpresa, IdCtaCblePadre
		
		) A where web.[ct_CONTA_004].IdEmpresa = a.IdEmpresa
		and web.[ct_CONTA_004].IdCtaCble = a.IdCtaCblePadre
		and web.[ct_CONTA_004].IdUsuario = @IdUsuario
		and web.[ct_CONTA_004].IdEmpresa = @IdEmpresa

		SET @Contador = @Contador - 1
	END

END

UPDATE [web].[ct_CONTA_004] SET 
Valor1 = iif(Naturaleza = 'C', Valor1 * -1, Valor1), 
Valor2 = iif(Naturaleza = 'C', Valor2 * -1, Valor2)
where IdUsuario = @IdUsuario

update [web].[ct_CONTA_004] set EsCuentaMovimiento = 1 
where IdUsuario = @IdUsuario
and IdEmpresa = @IdEmpresa
and not exists(
select * from [web].[ct_CONTA_004] as f
where f.IdEmpresa = [web].[ct_CONTA_004].IdEmpresa
and f.IdCtaCblePadre = [web].[ct_CONTA_004].IdCtaCble
and f.IdEmpresa = @IdEmpresa
and f.IdUsuario = @IdUsuario
)

update [web].[ct_CONTA_004] set Variacion = CASE WHEN Valor1 = 0 THEN 1 ELSE ((Valor2 - Valor1)/Valor1) END WHERE IdUsuario = @IdUsuario
update [web].[ct_CONTA_004] set Signo = CASE WHEN Variacion > 0 THEN '+' WHEN Variacion < 0 THEN '-' ELSE '=' END WHERE IdUsuario = @IdUsuario

IF(@MostrarSaldo0 = 0)
BEGIN
	DELETE [web].[ct_CONTA_004]
	WHERE Valor1 = 0 and Valor2 = 0 and IdUsuario = @IdUsuario
END

select * from [web].[ct_CONTA_004] 
where gc_estado_financiero LIKE '%'+@Balance+'%'
and [IdNivelCta] <= @IdNivel AND IdUsuario = @IdUsuario
and IdEmpresa = @IdEmpresa