﻿
--EXEC [web].[SPCONTA_003_balances] 2,2019,'2019/01/01','2019/01/31','ADMIN',6,0,'',0
CREATE PROCEDURE [web].[SPCONTA_003_balances]
(   -- SE  AÑADIO (NOLOCK) Y set nocount on POR QUE SE DEMORA ESTA VIDA Y LA OTRA, Y NO CARGA ESTE RESPORTE 12/11/2020
@IdEmpresa int,
@IdAnio int,
@FechaIni datetime,
@FechaFin datetime,
@IdUsuario varchar(200),
@IdNivel int,
@MostrarSaldo0 bit,
@Balance VARCHAR(2),
@MostrarAcumulado bit
)
AS
set nocount on
delete web.ct_CONTA_003_balances where IdUsuario = @IdUsuario

DECLARE @IdCtaCbleUtilidad varchar(20), @IdTipoCbteCierre int, @IdCbteCbleCierre int
SELECT @IdCtaCbleUtilidad = IdCtaCble FROM ct_anio_fiscal_x_cuenta_utilidad WHERE IdanioFiscal = @IdAnio and IdEmpresa = @IdEmpresa
select @IdTipoCbteCierre = IdTipoCbte_AsientoCierre_Anual from ct_parametro where IdEmpresa = @IdEmpresa
select @IdCbteCbleCierre = IdCbteCble from ct_cbtecble where IdEmpresa = @IdEmpresa and @IdTipoCbteCierre= @IdTipoCbteCierre and year(cb_Fecha) = @IdAnio

BEGIN --INSERTO PLAN DE CUENTAS
INSERT INTO [web].[ct_CONTA_003_balances]
           ([IdUsuario]
           ,[IdEmpresa]
           ,[IdCtaCble]
           ,[pc_Cuenta]
		   ,[IdNivelCta]
           ,[IdCtaCblePadre]
           ,[EsCtaUtilidad]
           ,[IdGrupoCble]
           ,[gc_GrupoCble]
           ,[gc_estado_financiero]
           ,[gc_Orden]
           ,[DebitosSaldoInicial]
           ,[CreditosSaldoInicial]
           ,[SaldoInicial]
           ,[Debitos]
           ,[Creditos]
           ,[SaldoDebitosCreditos]
           ,[SaldoDebitos]
           ,[SaldoCreditos]
           ,[SaldoFinal]
		   ,[EsCuentaMovimiento]
		   ,[Naturaleza]
           ,[SaldoInicialNaturaleza]
           ,[SaldoDebitosCreditosNaturaleza]
           ,[SaldoDebitosNaturaleza]
           ,[SaldoCreditosNaturaleza]
           ,[SaldoFinalNaturaleza])
SELECT @IdUsuario, 
ct_plancta.IdEmpresa, 
ct_plancta.IdCtaCble, 
ct_plancta.pc_Cuenta, 
ct_plancta.IdNivelCta,
ct_plancta.IdCtaCblePadre, 
CASE WHEN ct_anio_fiscal_x_cuenta_utilidad.IdCtaCble = ct_plancta.IdCtaCble THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS EsCtaUtilidad, 
ct_plancta.IdGrupoCble, 
ct_grupocble.gc_GrupoCble, 
ct_grupocble.gc_estado_financiero, 
ct_grupocble.gc_Orden,
0,0,0,0,0,0,0,0,0,0,ct_plancta.pc_Naturaleza,0,0,0,0,0
FROM            ct_anio_fiscal_x_cuenta_utilidad(nolock) RIGHT OUTER JOIN
        ct_plancta(nolock) ON ct_anio_fiscal_x_cuenta_utilidad.IdEmpresa = ct_plancta.IdEmpresa AND ct_anio_fiscal_x_cuenta_utilidad.IdCtaCble = ct_plancta.IdCtaCble LEFT OUTER JOIN
        ct_grupocble(nolock) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble
WHERE        (ISNULL(ct_anio_fiscal_x_cuenta_utilidad.IdanioFiscal, @IdAnio) = @IdAnio) AND ct_plancta.IdEmpresa = @IdEmpresa
END

BEGIN --ACTUALIZO SALDO INICIAL
UPDATE web.ct_CONTA_003_balances SET DebitosSaldoInicial = ROUND(A.DebitosSaldoInicial,2), CreditosSaldoInicial = ROUND(a.CreditosSaldoInicial,2), SaldoInicial = ROUND(a.DebitosSaldoInicial - a.CreditosSaldoInicial,2)
FROM(
	select IdEmpresa, IdCtaCble, sum(DebitosSaldoInicial) DebitosSaldoInicial, sum(CreditosSaldoInicial) CreditosSaldoInicial
	FROM(
			SELECT        ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial, 0 CreditosSaldoInicial
			FROM            ct_cbtecble (nolock) INNER JOIN
							ct_cbtecble_det (nolock) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta (nolock) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble (nolock) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'BG') AND ct_cbtecble.cb_Fecha < @FechaIni and ct_cbtecble_det.dc_Valor > 0
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble
			UNION ALL
			SELECT        ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial, 0
			FROM            ct_cbtecble (nolock) INNER JOIN
							ct_cbtecble_det (nolock) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta (nolock) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble (nolock) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes(NOLOCK) AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'ER') AND ct_cbtecble.cb_Fecha < @FechaIni and ct_cbtecble_det.dc_Valor > 0 AND @IdAnio = YEAR(ct_cbtecble.cb_Fecha)
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble
			UNION ALL
			SELECT        ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble, 0, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial
			FROM            ct_cbtecble(NOLOCK) INNER JOIN
							ct_cbtecble_det(NOLOCK) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta(NOLOCK) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble(NOLOCK) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes(NOLOCK) AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'BG') AND ct_cbtecble.cb_Fecha < @FechaIni and ct_cbtecble_det.dc_Valor < 0
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble
			UNION ALL
			SELECT        ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble, 0, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial
			FROM            ct_cbtecble(NOLOCK) INNER JOIN
							ct_cbtecble_det(NOLOCK) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta(NOLOCK) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble(NOLOCK) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes(NOLOCK) AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'ER') AND ct_cbtecble.cb_Fecha < @FechaIni and ct_cbtecble_det.dc_Valor < 0 AND @IdAnio = YEAR(ct_cbtecble.cb_Fecha)
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble
			UNION ALL
			--UTILIDAD
			SELECT        ct_cbtecble_det.IdEmpresa, @IdCtaCbleUtilidad, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial, 0
			FROM            ct_cbtecble(NOLOCK) INNER JOIN
							ct_cbtecble_det(NOLOCK) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta(NOLOCK) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble(NOLOCK) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes(NOLOCK) AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'ER') AND ct_cbtecble.cb_Fecha < @FechaIni and ct_cbtecble_det.dc_Valor > 0 AND @IdAnio = YEAR(ct_cbtecble.cb_Fecha)
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa
			UNION ALL
			SELECT        ct_cbtecble_det.IdEmpresa, @IdCtaCbleUtilidad, 0, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial
			FROM            ct_cbtecble INNER JOIN
							ct_cbtecble_det(NOLOCK) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta(NOLOCK) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble(NOLOCK) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes(NOLOCK) AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'ER') AND ct_cbtecble.cb_Fecha < @FechaIni and ct_cbtecble_det.dc_Valor < 0 AND @IdAnio = YEAR(ct_cbtecble.cb_Fecha)
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa
			) C
	GROUP BY IdEmpresa, IdCtaCble
) A
where web.ct_CONTA_003_balances.IdEmpresa = a.IdEmpresa and web.ct_CONTA_003_balances.IdCtaCble = a.IdCtaCble and web.ct_CONTA_003_balances.IdUsuario = @IdUsuario
END

BEGIN --ACTUALIZO DEBITOS Y CREDITOS
UPDATE web.ct_CONTA_003_balances SET Debitos = ROUND(A.DebitosSaldoInicial,2), Creditos = ROUND(a.CreditosSaldoInicial,2), SaldoDebitosCreditos = ROUND(a.DebitosSaldoInicial - a.CreditosSaldoInicial,2)
FROM(
	select IdEmpresa, IdCtaCble, sum(DebitosSaldoInicial) DebitosSaldoInicial, sum(CreditosSaldoInicial) CreditosSaldoInicial
	FROM(
			SELECT        ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial, 0 CreditosSaldoInicial
			FROM            ct_cbtecble (nolock) INNER JOIN
							ct_cbtecble_det (nolock) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta  (nolock) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
			 				ct_grupocble (nolock) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'BG') AND ct_cbtecble.cb_Fecha BETWEEN @FechaIni AND @FechaFin and ct_cbtecble_det.dc_Valor > 0
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble
			UNION ALL
			SELECT        ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial, 0
			FROM            ct_cbtecble INNER JOIN
							ct_cbtecble_det(NOLOCK) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta(NOLOCK) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble(NOLOCK) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes(NOLOCK) AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'ER') AND ct_cbtecble.cb_Fecha BETWEEN @FechaIni AND @FechaFin and ct_cbtecble_det.dc_Valor > 0 AND @IdAnio = YEAR(ct_cbtecble.cb_Fecha)
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble
			UNION ALL
			SELECT        ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble, 0, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial
			FROM            ct_cbtecble(NOLOCK) INNER JOIN
							ct_cbtecble_det(NOLOCK) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta(NOLOCK) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble(NOLOCK) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'BG') AND ct_cbtecble.cb_Fecha BETWEEN @FechaIni AND @FechaFin and ct_cbtecble_det.dc_Valor < 0
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble
			UNION ALL
			SELECT        ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble, 0, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial
			FROM            ct_cbtecble INNER JOIN
							ct_cbtecble_det(NOLOCK) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta(NOLOCK) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble(NOLOCK) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes(NOLOCK) AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'ER') AND ct_cbtecble.cb_Fecha BETWEEN @FechaIni AND @FechaFin and ct_cbtecble_det.dc_Valor < 0 AND @IdAnio = YEAR(ct_cbtecble.cb_Fecha)
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble
			UNION ALL
			--UTILIDAD
			SELECT        ct_cbtecble_det.IdEmpresa, @IdCtaCbleUtilidad, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial, 0
			FROM            ct_cbtecble INNER JOIN
							ct_cbtecble_det(NOLOCK) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta(NOLOCK) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble(NOLOCK) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes(NOLOCK) AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'ER') AND ct_cbtecble.cb_Fecha BETWEEN @FechaIni AND @FechaFin and ct_cbtecble_det.dc_Valor > 0 AND @IdAnio = YEAR(ct_cbtecble.cb_Fecha)
			and f.IdUsuario = @IdUsuario
			GROUP BY ct_cbtecble_det.IdEmpresa
			UNION ALL
			SELECT        ct_cbtecble_det.IdEmpresa, @IdCtaCbleUtilidad, 0, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial
			FROM            ct_cbtecble(NOLOCK) INNER JOIN
							ct_cbtecble_det(NOLOCK) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta(NOLOCK) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble(NOLOCK) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes(NOLOCK) AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'ER') AND ct_cbtecble.cb_Fecha BETWEEN @FechaIni AND @FechaFin and ct_cbtecble_det.dc_Valor < 0 AND @IdAnio = YEAR(ct_cbtecble.cb_Fecha)
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa
			) C
	GROUP BY IdEmpresa, IdCtaCble
) A
where web.ct_CONTA_003_balances.IdEmpresa = a.IdEmpresa and web.ct_CONTA_003_balances.IdCtaCble = a.IdCtaCble and web.ct_CONTA_003_balances.IdUsuario = @IdUsuario
END

BEGIN --ACTUALIZO SALDO FINAL
UPDATE web.ct_CONTA_003_balances SET SaldoDebitos = ROUND(A.DebitosSaldoInicial,2), SaldoCreditos = ROUND(a.CreditosSaldoInicial,2), SaldoFinal = ROUND(a.DebitosSaldoInicial - a.CreditosSaldoInicial,2)
FROM(
	select IdEmpresa, IdCtaCble, sum(DebitosSaldoInicial) DebitosSaldoInicial, sum(CreditosSaldoInicial) CreditosSaldoInicial
	FROM(
			SELECT        ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial, 0 CreditosSaldoInicial
			FROM            ct_cbtecble INNER JOIN
							ct_cbtecble_det ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'BG') AND ct_cbtecble.cb_Fecha <= @FechaFin and ct_cbtecble_det.dc_Valor > 0
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble
			UNION ALL
			SELECT        ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial, 0
			FROM            ct_cbtecble(NOLOCK) INNER JOIN
							ct_cbtecble_det(NOLOCK) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta(NOLOCK) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble(NOLOCK) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes(NOLOCK) AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'ER') AND ct_cbtecble.cb_Fecha <= @FechaFin and ct_cbtecble_det.dc_Valor > 0 AND @IdAnio = YEAR(ct_cbtecble.cb_Fecha)
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble
			UNION ALL
			SELECT        ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble, 0, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial
			FROM            ct_cbtecble(NOLOCK) INNER JOIN
							ct_cbtecble_det(NOLOCK) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta(NOLOCK) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble(NOLOCK) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes(NOLOCK) AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'BG') AND ct_cbtecble.cb_Fecha <= @FechaFin and ct_cbtecble_det.dc_Valor < 0
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble
			UNION ALL
			SELECT        ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble, 0, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial
			FROM            ct_cbtecble(NOLOCK) INNER JOIN
							ct_cbtecble_det(NOLOCK) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta(NOLOCK) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble(NOLOCK) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes(NOLOCK) AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal 
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'ER') AND ct_cbtecble.cb_Fecha <= @FechaFin and ct_cbtecble_det.dc_Valor < 0 AND @IdAnio = YEAR(ct_cbtecble.cb_Fecha)
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			
			GROUP BY ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble
			UNION ALL
			--UTILIDAD
			SELECT        ct_cbtecble_det.IdEmpresa, @IdCtaCbleUtilidad, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial, 0
			FROM            ct_cbtecble(NOLOCK) INNER JOIN
							ct_cbtecble_det(NOLOCK) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta(NOLOCK) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble(NOLOCK) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes(NOLOCK) AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'ER') AND cb_Fecha <= @FechaFin and ct_cbtecble_det.dc_Valor > 0 AND @IdAnio = YEAR(ct_cbtecble.cb_Fecha)
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa
			UNION ALL
			SELECT        ct_cbtecble_det.IdEmpresa, @IdCtaCbleUtilidad, 0, ABS(SUM(ct_cbtecble_det.dc_Valor)) AS DebitosSaldoInicial
			FROM            ct_cbtecble(NOLOCK) INNER JOIN
							ct_cbtecble_det(NOLOCK) ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
							ct_plancta(NOLOCK) ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
							ct_grupocble(NOLOCK) ON ct_plancta.IdGrupoCble = ct_grupocble.IdGrupoCble LEFT JOIN
							web.tb_FiltroReportes(NOLOCK) AS F ON ct_cbtecble.IdEmpresa = F.IdEmpresa AND ct_cbtecble.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
			WHERE        ct_cbtecble.IdEmpresa = @IdEmpresa and (ct_grupocble.gc_estado_financiero = 'ER') AND cb_Fecha <= @FechaFin and ct_cbtecble_det.dc_Valor < 0 AND @IdAnio = YEAR(ct_cbtecble.cb_Fecha)
			and f.IdUsuario = @IdUsuario and not exists(
			select f.IdEmpresa from ct_anio_fiscal_x_tb_sucursal as f 
			where f.IdEmpresa = ct_cbtecble.IdEmpresa
			and f.IdTipoCbte = ct_cbtecble.IdTipoCbte
			and f.IdCbteCble = ct_cbtecble.IdCbteCble
			and f.IdanioFiscal = @IdAnio
			)
			GROUP BY ct_cbtecble_det.IdEmpresa
			) C
	GROUP BY IdEmpresa, IdCtaCble
) A
where web.ct_CONTA_003_balances.IdEmpresa = a.IdEmpresa and web.ct_CONTA_003_balances.IdCtaCble = a.IdCtaCble and web.ct_CONTA_003_balances.IdUsuario = @IdUsuario
END

BEGIN --SUMATORIA ASCENDENTE

DECLARE @Contador int

select @Contador = max(IdNivelCta) 
from web.ct_CONTA_003_balances
where IdUsuario = @IdUsuario
and IdEmpresa = @IdEmpresa


	WHILE @Contador > 0
	BEGIN

		UPDATE web.ct_CONTA_003_balances 
		SET [DebitosSaldoInicial] = A.[DebitosSaldoInicial]
           ,[CreditosSaldoInicial] = A.[CreditosSaldoInicial]
           ,[SaldoInicial] = A.[SaldoInicial]
           ,[Debitos] = A.[Debitos]
           ,[Creditos] = A.[Creditos]
           ,[SaldoDebitosCreditos] = A.[SaldoDebitosCreditos]
           ,[SaldoDebitos] = A.[SaldoDebitos]
           ,[SaldoCreditos] = A.[SaldoCreditos]
           ,[SaldoFinal] = A.[SaldoFinal]
		FROM(
		SELECT        IdEmpresa, IdCtaCblePadre
		   ,SUM([DebitosSaldoInicial])[DebitosSaldoInicial]
           ,SUM([CreditosSaldoInicial]) [CreditosSaldoInicial]
           ,SUM([SaldoInicial])[SaldoInicial]
           ,SUM([Debitos])[Debitos]
           ,SUM([Creditos])[Creditos]
           ,SUM([SaldoDebitosCreditos])[SaldoDebitosCreditos]
           ,SUM([SaldoDebitos])[SaldoDebitos]
           ,SUM([SaldoCreditos])[SaldoCreditos]
           ,SUM([SaldoFinal])[SaldoFinal]
		FROM            web.ct_CONTA_003_balances(NOLOCK)
		where web.ct_CONTA_003_balances.IdEmpresa = @IdEmpresa
		and web.ct_CONTA_003_balances.IdUsuario = @IdUsuario
		GROUP BY IdEmpresa, IdCtaCblePadre
		
		) A where web.ct_CONTA_003_balances.IdEmpresa = a.IdEmpresa
		and web.ct_CONTA_003_balances.IdCtaCble = a.IdCtaCblePadre
		and web.ct_CONTA_003_balances.IdUsuario = @IdUsuario
		and web.ct_CONTA_003_balances.IdEmpresa = @IdEmpresa

		SET @Contador = @Contador - 1
	END

END

update web.ct_CONTA_003_balances Set SaldoFinal = round(SaldoFinal,2), SaldoFinalNaturaleza = round(SaldoFinalNaturaleza,2)
where IdUsuario = @IdUsuario
and IdEmpresa = @IdEmpresa

if(@MostrarAcumulado = 0)
BEGIN
	update web.ct_CONTA_003_balances SET SaldoFinal = SaldoDebitosCreditos, SaldoFinalNaturaleza = SaldoDebitosCreditosNaturaleza	
	where IdUsuario = @IdUsuario
	and IdEmpresa = @IdEmpresa

	IF(@MostrarSaldo0 = 0)
	BEGIN
		DELETE web.ct_CONTA_003_balances
		WHERE SaldoDebitosCreditos = 0
		and IdUsuario = @IdUsuario
	END
END
ELSE
IF(@MostrarSaldo0 = 0)
BEGIN

	update web.ct_CONTA_003_balances set EsCuentaMovimiento = 1 
	where IdUsuario = @IdUsuario
	and IdEmpresa = @IdEmpresa
	and not exists(
	select * from web.ct_CONTA_003_balances as f
	where f.IdEmpresa = web.ct_CONTA_003_balances.IdEmpresa
	and f.IdCtaCblePadre = web.ct_CONTA_003_balances.IdCtaCble
	and f.IdEmpresa = @IdEmpresa
	and f.IdUsuario = @IdUsuario
	)

	DELETE web.ct_CONTA_003_balances
	WHERE SaldoFinal = 0
	and IdUsuario = @IdUsuario 
	and not exists(
	select * from web.ct_CONTA_003_balances as f
	where f.IdEmpresa = web.ct_CONTA_003_balances.IdEmpresa
	and f.IdCtaCblePadre = web.ct_CONTA_003_balances.IdCtaCble
	and f.IdEmpresa = @IdEmpresa
	and f.IdUsuario = @IdUsuario
	) and EsCuentaMovimiento = 1

	DELETE web.ct_CONTA_003_balances
	WHERE IdUsuario = @IdUsuario 
	and not exists(
	select * from web.ct_CONTA_003_balances as f
	where f.IdEmpresa = web.ct_CONTA_003_balances.IdEmpresa
	and f.IdCtaCblePadre = web.ct_CONTA_003_balances.IdCtaCble
	and f.IdEmpresa = @IdEmpresa
	and f.IdUsuario = @IdUsuario
	) and EsCuentaMovimiento = 0

	DELETE web.ct_CONTA_003_balances
	WHERE IdUsuario = @IdUsuario 
	and not exists(
	select * from web.ct_CONTA_003_balances as f
	where f.IdEmpresa = web.ct_CONTA_003_balances.IdEmpresa
	and f.IdCtaCblePadre = web.ct_CONTA_003_balances.IdCtaCble
	and f.IdEmpresa = @IdEmpresa
	and f.IdUsuario = @IdUsuario
	) and EsCuentaMovimiento = 0

	DELETE web.ct_CONTA_003_balances
	WHERE IdUsuario = @IdUsuario 
	and not exists(
	select * from web.ct_CONTA_003_balances as f
	where f.IdEmpresa = web.ct_CONTA_003_balances.IdEmpresa
	and f.IdCtaCblePadre = web.ct_CONTA_003_balances.IdCtaCble
	and f.IdEmpresa = @IdEmpresa
	and f.IdUsuario = @IdUsuario
	) and EsCuentaMovimiento = 0
END

delete web.ct_CONTA_003_balances 
where IdNivelCta > @IdNivel
and IdEmpresa = @IdEmpresa
and IdUsuario = @IdUsuario

update web.ct_CONTA_003_balances set EsCuentaMovimiento = 0
where IdUsuario = @IdUsuario

update web.ct_CONTA_003_balances set EsCuentaMovimiento = 1 
where IdUsuario = @IdUsuario
and IdEmpresa = @IdEmpresa
and not exists(
select * from web.ct_CONTA_003_balances as f
where f.IdEmpresa = web.ct_CONTA_003_balances.IdEmpresa
and f.IdCtaCblePadre = web.ct_CONTA_003_balances.IdCtaCble
and f.IdEmpresa = @IdEmpresa
and f.IdUsuario = @IdUsuario
)

UPDATE web.ct_CONTA_003_balances SET 
SaldoInicialNaturaleza = iif(Naturaleza = 'C', SaldoInicial * -1, SaldoInicial),
SaldoDebitosCreditosNaturaleza = iif(Naturaleza = 'C', SaldoDebitosCreditos * -1, SaldoDebitosCreditos),
SaldoDebitosNaturaleza =  ABS(Debitos),
SaldoCreditosNaturaleza = ABS(Creditos),
SaldoFinalNaturaleza = iif(Naturaleza = 'C', SaldoFinal * -1, SaldoFinal)
where web.ct_CONTA_003_balances.IdEmpresa = @IdEmpresa 
and web.ct_CONTA_003_balances.IdUsuario = @IdUsuario

SELECT * FROM web.ct_CONTA_003_balances (nolock)
where gc_estado_financiero LIKE '%'+@Balance+'%'
and [IdNivelCta] <= @IdNivel AND IdUsuario = @IdUsuario
and IdEmpresa = @IdEmpresa
GO

