﻿--EXEC web.SPBAN_015 'admin','2019/08/31','2019/08/31','IGLESIA',0
CREATE PROCEDURE [web].[SPBAN_015]
(
@IdUsuario varchar(50),
@FechaIni date,
@FechaFin date,
@Reporte VARCHAR(50),
@MostrarSaldo0 bit
)
AS
UPDATE ba_TipoFlujo SET cod_flujo = '' WHERE cod_flujo IS NULL

BEGIN --DECLARACION DE VARIABLES
	DECLARE @w_IdEmpresa1 int = case when @Reporte = 'IGLESIA' then 2 ELSE 1 END
	DECLARE @w_IdEmpresa2 int = case when @Reporte = 'IGLESIA' then 2 ELSE 3 END
	DECLARE @w_IdEmpresa3 int = case when @Reporte = 'IGLESIA' then 2 ELSE 5 END
	DECLARE @w_IdEmpresa4 int = case when @Reporte = 'IGLESIA' then 2 ELSE 0 END

	DECLARE @w_IdSucursalIni1 int = case when @Reporte = 'IGLESIA' then 8 ELSE 1 END
	DECLARE @w_IdSucursalIni2 int = case when @Reporte = 'IGLESIA' then 2 ELSE 1 END
	DECLARE @w_IdSucursalIni3 int = case when @Reporte = 'IGLESIA' then 1 ELSE 1 END
	DECLARE @w_IdSucursalIni4 int = case when @Reporte = 'IGLESIA' then 3 ELSE 0 END

	DECLARE @w_IdSucursalFin1 int = case when @Reporte = 'IGLESIA' then 8 ELSE 1 END
	DECLARE @w_IdSucursalFin2 int = case when @Reporte = 'IGLESIA' then 2 ELSE 1 END
	DECLARE @w_IdSucursalFin3 int = case when @Reporte = 'IGLESIA' then 1 ELSE 1 END
	DECLARE @w_IdSucursalFin4 int = case when @Reporte = 'IGLESIA' then 7 ELSE 0 END

	DECLARE @w_Secuencia int
END

DELETE web.ba_spBAN_015 WHERE IdUsuario = @IdUsuario


BEGIN --INSERT DE DATOS INICIALES
INSERT INTO [web].[ba_spBAN_015]
           ([IdUsuario]
           ,[Reporte]
           ,[Secuencia]
           ,[Tipo]
           ,[Codigo]
           ,[Descripcion]
           ,[SaldoE1]
           ,[SaldoE2]
           ,[SaldoE3]
           ,[SaldoE4]
           ,[Total])
SELECT A.IdUsuario, a.Reporte, a.Secuencia, a.Tipo, a.IdBanco, ba_descripcion,
round(sum(a.SaldoE1),2), round(sum(a.SaldoE2),2),round(sum(a.SaldoE3),2),round(sum(a.SaldoE4),2), round(sum(a.Total),2)
FROM(
SELECT @IdUsuario IdUsuario, @Reporte Reporte, b2.IdBanco Secuencia, 'BANCOS' Tipo, b2.IdBanco, b2.ba_descripcion,
CASE WHEN C.IdEmpresa = @w_IdEmpresa1 AND C.IdSucursal BETWEEN @w_IdSucursalIni1 and @w_IdSucursalFin1 then d.dc_Valor else 0 end as SaldoE1,
CASE WHEN C.IdEmpresa = @w_IdEmpresa2 AND C.IdSucursal BETWEEN @w_IdSucursalIni2 and @w_IdSucursalFin2 then d.dc_Valor else 0 end as SaldoE2,
CASE WHEN C.IdEmpresa = @w_IdEmpresa3 AND C.IdSucursal BETWEEN @w_IdSucursalIni3 and @w_IdSucursalFin3 then d.dc_Valor else 0 end as SaldoE3,
CASE WHEN C.IdEmpresa = @w_IdEmpresa4 AND C.IdSucursal BETWEEN @w_IdSucursalIni4 and @w_IdSucursalFin4 then d.dc_Valor else 0 end as SaldoE4,
d.dc_Valor Total
FROM ct_cbtecble AS C INNER JOIN 
ct_cbtecble_det AS D ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND C.IdCbteCble = D.IdCbteCble INNER JOIN
ba_Banco_Cuenta AS B1 ON B1.IdEmpresa = D.IdEmpresa AND B1.IdCtaCble = D.IdCtaCble INNER JOIN
tb_banco AS B2 ON B1.IdBanco_Financiero = B2.IdBanco
WHERE C.cb_Fecha <= @FechaFin AND C.IdEmpresa IN (@w_IdEmpresa1, @w_IdEmpresa2, @w_IdEmpresa3, @w_IdEmpresa4)
) A
GROUP BY A.IdUsuario, a.Reporte, a.Secuencia, a.Tipo, a.IdBanco, ba_descripcion
END

select @w_Secuencia = max(secuencia) from web.ba_spBAN_015 WHERE IdUsuario = @IdUsuario

BEGIN --INSERTO FLUJOS
	INSERT INTO [web].[ba_spBAN_015]
			   ([IdUsuario]
			   ,[Reporte]
			   ,[Secuencia]
			   ,[Tipo]
			   ,[Codigo]
			   ,[Descripcion]
			   ,[SaldoE1]
			   ,[SaldoE2]
			   ,[SaldoE3]
			   ,[SaldoE4]
			   ,[Total])
	SELECT A.IdUsuario, A.Reporte, isnull(@w_Secuencia,0) + ROW_NUMBER() OVER(ORDER BY a.Descricion) Secuencia, 'FLUJOS', a.cod_flujo, a.Descricion,
	round(sum(a.SaldoE1),2), round(sum(a.SaldoE2),2),round(sum(a.SaldoE3),2),round(sum(a.SaldoE4),2), round(sum(a.Total),2)
	 FROM(
		select @IdUsuario IdUsuario, @Reporte Reporte, f.cod_flujo,f.Descricion,
		CASE WHEN C.IdEmpresa = @w_IdEmpresa1 AND C.IdSucursal BETWEEN @w_IdSucursalIni1 and @w_IdSucursalFin1 then ROUND(f1.Valor,2) * case when x.CodTipoCbteBan in ('CHEQ','NDBA') THEN -1 ELSE 1 end else 0 end as SaldoE1,
		CASE WHEN C.IdEmpresa = @w_IdEmpresa2 AND C.IdSucursal BETWEEN @w_IdSucursalIni2 and @w_IdSucursalFin2 then ROUND(f1.Valor,2) * case when x.CodTipoCbteBan in ('CHEQ','NDBA') THEN -1 ELSE 1 end else 0 end as SaldoE2,
		CASE WHEN C.IdEmpresa = @w_IdEmpresa3 AND C.IdSucursal BETWEEN @w_IdSucursalIni3 and @w_IdSucursalFin3 then ROUND(f1.Valor,2) * case when x.CodTipoCbteBan in ('CHEQ','NDBA') THEN -1 ELSE 1 end else 0 end as SaldoE3,
		CASE WHEN C.IdEmpresa = @w_IdEmpresa4 AND C.IdSucursal BETWEEN @w_IdSucursalIni4 and @w_IdSucursalFin4 then ROUND(f1.Valor,2) * case when x.CodTipoCbteBan in ('CHEQ','NDBA') THEN -1 ELSE 1 end else 0 end as SaldoE4,
		f1.Valor  * case when x.CodTipoCbteBan in ('CHEQ','NDBA') THEN -1 ELSE 1 end Total
		from ba_TipoFlujo as f inner join
		ba_Cbte_Ban_x_ba_TipoFlujo as f1 on f1.IdEmpresa = f.IdEmpresa and f1.IdTipoFlujo = f.IdTipoFlujo inner join
		ct_cbtecble as c on f1.IdEmpresa = c.IdEmpresa and f1.IdTipocbte = c.IdTipoCbte and f1.IdCbteCble = c.IdCbteCble inner join
		ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo as x on x.IdEmpresa = c.IdEmpresa and x.IdTipoCbteCble = c.IdTipoCbte 
		where C.cb_Fecha <= @FechaFin AND C.IdEmpresa IN (@w_IdEmpresa1, @w_IdEmpresa2, @w_IdEmpresa3, @w_IdEmpresa4)
		AND not exists(
		select r.IdEmpresa from ct_cbtecble_Reversado as r
		inner join ct_cbtecble as cr on r.IdEmpresa_Anu = cr.IdEmpresa
		and r.IdTipoCbte_Anu = cr.IdTipoCbte
		and r.IdCbteCble_Anu = cr.IdCbteCble
		where r.idempresa = c.idempresa
		and r.idtipocbte = c.idtipocbte
		and r.idcbtecble = c.idcbtecble
		and cr.cb_Fecha <= @FechaFin
		)
		UNION ALL
		SELECT @IdUsuario IdUsuario, @Reporte Reporte, f.cod_flujo,f.Descricion,
		CASE WHEN m.IdEmpresa = @w_IdEmpresa1 AND m.IdSucursal BETWEEN @w_IdSucursalIni1 and @w_IdSucursalFin1 then m.Valor else 0 end as SaldoE1,
		CASE WHEN m.IdEmpresa = @w_IdEmpresa2 AND m.IdSucursal BETWEEN @w_IdSucursalIni2 and @w_IdSucursalFin2 then m.Valor else 0 end as SaldoE2,
		CASE WHEN m.IdEmpresa = @w_IdEmpresa3 AND m.IdSucursal BETWEEN @w_IdSucursalIni3 and @w_IdSucursalFin3 then m.Valor else 0 end as SaldoE3,
		CASE WHEN m.IdEmpresa = @w_IdEmpresa4 AND m.IdSucursal BETWEEN @w_IdSucursalIni4 and @w_IdSucursalFin4 then m.Valor else 0 end as SaldoE4,
		m.Valor Total
		FROM ba_TipoFlujo_Movimiento AS m INNER JOIN 
		ba_TipoFlujo AS F on m.IdEmpresa = f.IdEmpresa and m.IdTipoFlujo = f.IdTipoFlujo
		where m.Fecha <= @FechaFin AND m.IdEmpresa IN (@w_IdEmpresa1, @w_IdEmpresa2, @w_IdEmpresa3, @w_IdEmpresa4)
		AND M.Estado = 1
		) A
		GROUP BY A.IdUsuario, A.Reporte,a.cod_flujo,a.Descricion
END

DECLARE @Valor float
		select @Valor = sum(SaldoE1) from web.ba_spBAN_015 where IdUsuario = @IdUsuario and Tipo = 'FLUJOS'
		IF(ROUND(ISNULL(@Valor,0),2) = 0)
		BEGIN
			UPDATE web.ba_spBAN_015 SET SaldoE1 = ROUND(ISNULL(A.SaldoE1,0),2)
			FROM (
			SELECT SUM(SaldoE1) SaldoE1 
			FROM web.ba_spBAN_015
			WHERE IdUsuario = @IdUsuario
			AND Tipo = 'BANCOS'
			) A
			WHERE web.ba_spBAN_015.IdUsuario = @IdUsuario AND web.ba_spBAN_015.Codigo = 'ADM' AND TIPO = 'FLUJOS'
		END

		select @Valor = sum(SaldoE2) from web.ba_spBAN_015 where IdUsuario = @IdUsuario and Tipo = 'FLUJOS'
		IF(ROUND(ISNULL(@Valor,0),2) = 0)
		BEGIN
			UPDATE web.ba_spBAN_015 SET SaldoE2 = ROUND(ISNULL(A.SaldoE2,0),2)
			FROM (
			SELECT SUM(SaldoE2) SaldoE2 
			FROM web.ba_spBAN_015
			WHERE IdUsuario = @IdUsuario
			AND Tipo = 'BANCOS'
			) A
			WHERE web.ba_spBAN_015.IdUsuario = @IdUsuario AND web.ba_spBAN_015.Codigo = 'ADM' AND TIPO = 'FLUJOS'
		END

		select @Valor = sum(SaldoE3) from web.ba_spBAN_015 where IdUsuario = @IdUsuario and Tipo = 'FLUJOS'
		IF(ROUND(ISNULL(@Valor,0),2) = 0)
		BEGIN
			UPDATE web.ba_spBAN_015 SET SaldoE3 = ROUND(ISNULL(A.SaldoE3,0),2)
			FROM (
			SELECT SUM(SaldoE3) SaldoE3
			FROM web.ba_spBAN_015
			WHERE IdUsuario = @IdUsuario
			AND Tipo = 'BANCOS'
			) A
			WHERE web.ba_spBAN_015.IdUsuario = @IdUsuario AND web.ba_spBAN_015.Codigo = 'ADM' AND TIPO = 'FLUJOS'
		END

		select @Valor = sum(SaldoE4) from web.ba_spBAN_015 where IdUsuario = @IdUsuario and Tipo = 'FLUJOS'
		IF(ROUND(ISNULL(@Valor,0),2) = 0)
		BEGIN
			UPDATE web.ba_spBAN_015 SET SaldoE4 = ROUND(ISNULL(A.SaldoE4,0),2)
			FROM (
			SELECT SUM(SaldoE4) SaldoE4
			FROM web.ba_spBAN_015
			WHERE IdUsuario = @IdUsuario
			AND Tipo = 'BANCOS'
			) A
			WHERE web.ba_spBAN_015.IdUsuario = @IdUsuario AND web.ba_spBAN_015.Codigo = 'ADM' AND TIPO = 'FLUJOS'
		END

		UPDATE web.ba_spBAN_015 SET Total = SaldoE1 + SaldoE2 + SaldoE3 + SaldoE4

IF(@MostrarSaldo0 = 0)
BEGIN
	DELETE web.ba_spBAN_015 WHERE IdUsuario = @IdUsuario AND Total = 0
END

SELECT * FROM web.ba_spBAN_015 WHERE IdUsuario = @IdUsuario