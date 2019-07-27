--exec [web].[SPBAN_012] 1,1,'2019/06/22','2019/06/30',0,'admin'
CREATE PROCEDURE [web].[SPBAN_012]
(
@IdEmpresa int,
@IdSucursal int,
@FechaIni date,
@FechaFin date,
@MostrarSaldo0 bit,
@IdUsuario varchar(50)
)
AS

DELETE [web].[ba_SPBAN_012] WHERE IdUsuario = @IdUsuario

INSERT INTO [web].[ba_SPBAN_012]
           ([IdEmpresa]
           ,[IdBanco]
           ,[IdTipoFlujo]
           ,[IdUsuario]
           ,[ba_descripcion]
           ,[nom_tipo_flujo]
           ,[SaldoInicial]
           ,[Ingresos]
           ,[Egresos]
           ,[SaldoFinal]
           ,[SaldoFinalBanco])
select s.IdEmpresa, s.IdBanco, t.IdTipoFlujo, @IdUsuario, b.ba_descripcion, t.Descricion, 0,0,0,0,0
from ba_Banco_Cuenta as b inner join 
ba_Banco_Cuenta_x_tb_sucursal as s on b.IdEmpresa = s.IdEmpresa and b.IdBanco = s.IdBanco cross join 
ba_TipoFlujo t 
where s.IdEmpresa = @IdEmpresa and s.IdSucursal = @IdSucursal and t.IdEmpresa = @IdEmpresa


BEGIN --SALDO DE BANCOS

update web.ba_SPBAN_012 set SaldoFinalBanco = A.Valor
FROM(
SELECT b.IdEmpresa, b.IdBanco, round(sum(d.dc_Valor),2) as Valor
FROM ct_cbtecble C INNER JOIN 
ct_cbtecble_det AS D ON C.IdEmpresa = D.IdEmpresa AND C.IdTipoCbte = D.IdTipoCbte AND C.IdCbteCble = D.IdCbteCble INNER JOIN 
ba_Banco_Cuenta AS B ON B.IdEmpresa = D.IdEmpresa AND B.IdCtaCble = D.IdCtaCble INNER JOIN 
ba_Banco_Cuenta_x_tb_sucursal AS S ON S.IdEmpresa = B.IdEmpresa AND S.IdBanco = B.IdBanco
WHERE B.IdEmpresa = @IdEmpresa AND S.IdSucursal = @IdSucursal AND C.cb_Fecha  <= @FechaFin
group by b.IdEmpresa, b.IdBanco
) A
WHERE web.ba_SPBAN_012.IdEmpresa = @IdEmpresa
and web.ba_SPBAN_012.IdUsuario = @IdUsuario
and web.ba_SPBAN_012.IdEmpresa = a.IdEmpresa
and web.ba_SPBAN_012.IdBanco = a.IdBanco
END

BEGIN --SALDO ANTERIOR FLUJO

update web.ba_SPBAN_012 set SaldoInicial = A.Valor
FROM(
	select a.IdEmpresa, a.IdBanco, a.ba_descripcion, a.IdTipoFlujo, a.NomFlujo, sum(round(a.Valor,2)) as Valor
from (
SELECT c.IdEmpresa, 
		b.IdBanco, 
		ba.ba_descripcion, 
		t.IdTipoFlujo, 
		tf.Descricion AS NomFlujo, 
		t.Valor AS ValorAbsoluto, 
		CASE WHEN ltrim(rtrim(tc.CodTipoCbteBan)) IN ('CHEQ', 'NDBA') THEN t .Valor * - 1 ELSE T .Valor END AS Valor
FROM     ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo AS tc INNER JOIN
                  ba_Cbte_Ban AS b INNER JOIN
                  ct_cbtecble AS c ON c.IdEmpresa = b.IdEmpresa AND c.IdTipoCbte = b.IdTipocbte AND c.IdCbteCble = b.IdCbteCble INNER JOIN
                  ba_Banco_Cuenta AS ba ON ba.IdEmpresa = b.IdEmpresa AND ba.IdBanco = b.IdBanco ON tc.IdEmpresa = b.IdEmpresa AND tc.IdTipoCbteCble = b.IdTipocbte INNER JOIN
                  ba_TipoFlujo AS tf INNER JOIN
                  ba_Cbte_Ban_x_ba_TipoFlujo AS t ON tf.IdEmpresa = t.IdEmpresa AND tf.IdTipoFlujo = t.IdTipoFlujo ON c.IdEmpresa = t.IdEmpresa AND c.IdTipoCbte = t.IdTipocbte AND c.IdCbteCble = t.IdCbteCble inner join
				  ba_Banco_Cuenta_x_tb_sucursal as s on ba.IdEmpresa = s.IdEmpresa and ba.IdBanco = s.IdBanco				  
where not exists(
select r.IdEmpresa from ct_cbtecble_Reversado as r
inner join ct_cbtecble as cr on r.IdEmpresa_Anu = cr.IdEmpresa
and r.IdTipoCbte_Anu = cr.IdTipoCbte
and r.IdCbteCble_Anu = cr.IdCbteCble
where r.idempresa = c.idempresa
and r.idtipocbte = c.idtipocbte
and r.idcbtecble = c.idcbtecble
and cr.cb_Fecha < @FechaIni
)
and c.IdEmpresa = @IdEmpresa
and c.cb_Fecha < @FechaIni
and s.IdSucursal = @IdSucursal
--and b.Estado = 'A'
UNION ALL
SELECT ba_TipoFlujo_Movimiento.IdEmpresa, ba_TipoFlujo_Movimiento.IdBanco, ba_Banco_Cuenta.ba_descripcion, ba_TipoFlujo_Movimiento.IdTipoFlujo, ba_TipoFlujo.Descricion, ba_TipoFlujo_Movimiento.Valor, 
                  ba_TipoFlujo_Movimiento.Valor
FROM     ba_TipoFlujo_Movimiento INNER JOIN
                  ba_TipoFlujo ON ba_TipoFlujo_Movimiento.IdEmpresa = ba_TipoFlujo.IdEmpresa AND ba_TipoFlujo_Movimiento.IdTipoFlujo = ba_TipoFlujo.IdTipoFlujo INNER JOIN
                  ba_Banco_Cuenta ON ba_TipoFlujo_Movimiento.IdEmpresa = ba_Banco_Cuenta.IdEmpresa AND ba_TipoFlujo_Movimiento.IdBanco = ba_Banco_Cuenta.IdBanco INNER JOIN
				  ba_Banco_Cuenta_x_tb_sucursal as s on ba_Banco_Cuenta.IdEmpresa = s.IdEmpresa and ba_Banco_Cuenta.IdBanco = s.IdBanco
WHERE  (ba_TipoFlujo_Movimiento.Estado = 1)  AND ba_TipoFlujo_Movimiento.IdEmpresa = @IdEmpresa and s.IdSucursal = @IdSucursal AND ba_TipoFlujo_Movimiento.Fecha < @FechaIni
) a
group by a.IdEmpresa, a.IdBanco, a.ba_descripcion, a.IdTipoFlujo, a.NomFlujo
having ROUND(sum(round(a.Valor,2)),2) != 0
) A WHERE web.ba_SPBAN_012.IdEmpresa = A.IdEmpresa
AND web.ba_SPBAN_012.IdBanco = A.IdBanco
AND web.ba_SPBAN_012.IdTipoFlujo = A.IdTipoFlujo
END

BEGIN --SALDO FINAL FLUJO

update web.ba_SPBAN_012 set SaldoFinal = A.Valor
FROM(
	select a.IdEmpresa, a.IdBanco, a.ba_descripcion, a.IdTipoFlujo, a.NomFlujo, sum(round(a.Valor,2)) as Valor
from (
SELECT c.IdEmpresa, 
		b.IdBanco, 
		ba.ba_descripcion, 
		t.IdTipoFlujo, 
		tf.Descricion AS NomFlujo, 
		t.Valor AS ValorAbsoluto, 
		CASE WHEN ltrim(rtrim(tc.CodTipoCbteBan)) IN ('CHEQ', 'NDBA') THEN t .Valor * - 1 ELSE T .Valor END AS Valor
FROM     ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo AS tc INNER JOIN
                  ba_Cbte_Ban AS b INNER JOIN
                  ct_cbtecble AS c ON c.IdEmpresa = b.IdEmpresa AND c.IdTipoCbte = b.IdTipocbte AND c.IdCbteCble = b.IdCbteCble INNER JOIN
                  ba_Banco_Cuenta AS ba ON ba.IdEmpresa = b.IdEmpresa AND ba.IdBanco = b.IdBanco ON tc.IdEmpresa = b.IdEmpresa AND tc.IdTipoCbteCble = b.IdTipocbte INNER JOIN
                  ba_TipoFlujo AS tf INNER JOIN
                  ba_Cbte_Ban_x_ba_TipoFlujo AS t ON tf.IdEmpresa = t.IdEmpresa AND tf.IdTipoFlujo = t.IdTipoFlujo ON c.IdEmpresa = t.IdEmpresa AND c.IdTipoCbte = t.IdTipocbte AND c.IdCbteCble = t.IdCbteCble inner join
				  ba_Banco_Cuenta_x_tb_sucursal as s on ba.IdEmpresa = s.IdEmpresa and ba.IdBanco = s.IdBanco				  
where not exists(
select r.IdEmpresa from ct_cbtecble_Reversado as r
inner join ct_cbtecble as cr on r.IdEmpresa_Anu = cr.IdEmpresa
and r.IdTipoCbte_Anu = cr.IdTipoCbte
and r.IdCbteCble_Anu = cr.IdCbteCble
where r.idempresa = c.idempresa
and r.idtipocbte = c.idtipocbte
and r.idcbtecble = c.idcbtecble
and cr.cb_Fecha <= @FechaFin
)
and c.IdEmpresa = @IdEmpresa
and c.cb_Fecha <= @FechaFin
and s.IdSucursal = @IdSucursal
--and b.Estado = 'A'
UNION ALL
SELECT ba_TipoFlujo_Movimiento.IdEmpresa, ba_TipoFlujo_Movimiento.IdBanco, ba_Banco_Cuenta.ba_descripcion, ba_TipoFlujo_Movimiento.IdTipoFlujo, ba_TipoFlujo.Descricion, ba_TipoFlujo_Movimiento.Valor, 
                  ba_TipoFlujo_Movimiento.Valor
FROM     ba_TipoFlujo_Movimiento INNER JOIN
                  ba_TipoFlujo ON ba_TipoFlujo_Movimiento.IdEmpresa = ba_TipoFlujo.IdEmpresa AND ba_TipoFlujo_Movimiento.IdTipoFlujo = ba_TipoFlujo.IdTipoFlujo INNER JOIN
                  ba_Banco_Cuenta ON ba_TipoFlujo_Movimiento.IdEmpresa = ba_Banco_Cuenta.IdEmpresa AND ba_TipoFlujo_Movimiento.IdBanco = ba_Banco_Cuenta.IdBanco INNER JOIN
				  ba_Banco_Cuenta_x_tb_sucursal as s on ba_Banco_Cuenta.IdEmpresa = s.IdEmpresa and ba_Banco_Cuenta.IdBanco = s.IdBanco
WHERE  (ba_TipoFlujo_Movimiento.Estado = 1)  AND ba_TipoFlujo_Movimiento.IdEmpresa = @IdEmpresa and s.IdSucursal = @IdSucursal AND ba_TipoFlujo_Movimiento.Fecha <= @FechaFin
) a
group by a.IdEmpresa, a.IdBanco, a.ba_descripcion, a.IdTipoFlujo, a.NomFlujo
having ROUND(sum(round(a.Valor,2)),2) != 0
) A WHERE web.ba_SPBAN_012.IdEmpresa = A.IdEmpresa
AND web.ba_SPBAN_012.IdBanco = A.IdBanco
AND web.ba_SPBAN_012.IdTipoFlujo = A.IdTipoFlujo
END

BEGIN --EGREOS
update web.ba_SPBAN_012 set Egresos = A.Valor
FROM(
	select a.IdEmpresa, a.IdBanco, a.ba_descripcion, a.IdTipoFlujo, a.NomFlujo, sum(round(a.Valor,2)) as Valor
from (
SELECT c.IdEmpresa, 
		b.IdBanco, 
		ba.ba_descripcion, 
		t.IdTipoFlujo, 
		tf.Descricion AS NomFlujo, 
		t.Valor AS ValorAbsoluto, 
		CASE WHEN ltrim(rtrim(tc.CodTipoCbteBan)) IN ('CHEQ', 'NDBA') THEN t .Valor * - 1 ELSE T .Valor END AS Valor
FROM     ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo AS tc INNER JOIN
                  ba_Cbte_Ban AS b INNER JOIN
                  ct_cbtecble AS c ON c.IdEmpresa = b.IdEmpresa AND c.IdTipoCbte = b.IdTipocbte AND c.IdCbteCble = b.IdCbteCble INNER JOIN
                  ba_Banco_Cuenta AS ba ON ba.IdEmpresa = b.IdEmpresa AND ba.IdBanco = b.IdBanco ON tc.IdEmpresa = b.IdEmpresa AND tc.IdTipoCbteCble = b.IdTipocbte INNER JOIN
                  ba_TipoFlujo AS tf INNER JOIN
                  ba_Cbte_Ban_x_ba_TipoFlujo AS t ON tf.IdEmpresa = t.IdEmpresa AND tf.IdTipoFlujo = t.IdTipoFlujo ON c.IdEmpresa = t.IdEmpresa AND c.IdTipoCbte = t.IdTipocbte AND c.IdCbteCble = t.IdCbteCble inner join
				  ba_Banco_Cuenta_x_tb_sucursal as s on ba.IdEmpresa = s.IdEmpresa and ba.IdBanco = s.IdBanco				  
where not exists(
select r.IdEmpresa from ct_cbtecble_Reversado as r
inner join ct_cbtecble as cr on r.IdEmpresa_Anu = cr.IdEmpresa
and r.IdTipoCbte_Anu = cr.IdTipoCbte
and r.IdCbteCble_Anu = cr.IdCbteCble
where r.idempresa = c.idempresa
and r.idtipocbte = c.idtipocbte
and r.idcbtecble = c.idcbtecble
and cr.cb_Fecha <= @FechaFin
)
and c.IdEmpresa = @IdEmpresa
and c.cb_Fecha BETWEEN @FechaIni AND @FechaFin
and s.IdSucursal = @IdSucursal
AND ltrim(rtrim(tc.CodTipoCbteBan)) IN ('CHEQ', 'NDBA')
--and b.Estado = 'A'
UNION ALL
SELECT ba_TipoFlujo_Movimiento.IdEmpresa, ba_TipoFlujo_Movimiento.IdBanco, ba_Banco_Cuenta.ba_descripcion, ba_TipoFlujo_Movimiento.IdTipoFlujo, ba_TipoFlujo.Descricion, ba_TipoFlujo_Movimiento.Valor, 
                  ba_TipoFlujo_Movimiento.Valor
FROM     ba_TipoFlujo_Movimiento INNER JOIN
                  ba_TipoFlujo ON ba_TipoFlujo_Movimiento.IdEmpresa = ba_TipoFlujo.IdEmpresa AND ba_TipoFlujo_Movimiento.IdTipoFlujo = ba_TipoFlujo.IdTipoFlujo INNER JOIN
                  ba_Banco_Cuenta ON ba_TipoFlujo_Movimiento.IdEmpresa = ba_Banco_Cuenta.IdEmpresa AND ba_TipoFlujo_Movimiento.IdBanco = ba_Banco_Cuenta.IdBanco INNER JOIN
				  ba_Banco_Cuenta_x_tb_sucursal as s on ba_Banco_Cuenta.IdEmpresa = s.IdEmpresa and ba_Banco_Cuenta.IdBanco = s.IdBanco
WHERE  (ba_TipoFlujo_Movimiento.Estado = 1)  AND ba_TipoFlujo_Movimiento.IdEmpresa = @IdEmpresa and s.IdSucursal = @IdSucursal AND ba_TipoFlujo_Movimiento.Fecha BETWEEN @FechaIni AND @FechaFin
AND ba_TipoFlujo_Movimiento.Valor < 0
) a
group by a.IdEmpresa, a.IdBanco, a.ba_descripcion, a.IdTipoFlujo, a.NomFlujo
having ROUND(sum(round(a.Valor,2)),2) != 0
) A WHERE web.ba_SPBAN_012.IdEmpresa = A.IdEmpresa
AND web.ba_SPBAN_012.IdBanco = A.IdBanco
AND web.ba_SPBAN_012.IdTipoFlujo = A.IdTipoFlujo
END

BEGIN --INGRESOS
update web.ba_SPBAN_012 set Ingresos = A.Valor
FROM(
	select a.IdEmpresa, a.IdBanco, a.ba_descripcion, a.IdTipoFlujo, a.NomFlujo, sum(round(a.Valor,2)) as Valor
from (
SELECT c.IdEmpresa, 
		b.IdBanco, 
		ba.ba_descripcion, 
		t.IdTipoFlujo, 
		tf.Descricion AS NomFlujo, 
		t.Valor AS ValorAbsoluto, 
		CASE WHEN ltrim(rtrim(tc.CodTipoCbteBan)) IN ('CHEQ', 'NDBA') THEN t .Valor * - 1 ELSE T .Valor END AS Valor
FROM     ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo AS tc INNER JOIN
                  ba_Cbte_Ban AS b INNER JOIN
                  ct_cbtecble AS c ON c.IdEmpresa = b.IdEmpresa AND c.IdTipoCbte = b.IdTipocbte AND c.IdCbteCble = b.IdCbteCble INNER JOIN
                  ba_Banco_Cuenta AS ba ON ba.IdEmpresa = b.IdEmpresa AND ba.IdBanco = b.IdBanco ON tc.IdEmpresa = b.IdEmpresa AND tc.IdTipoCbteCble = b.IdTipocbte INNER JOIN
                  ba_TipoFlujo AS tf INNER JOIN
                  ba_Cbte_Ban_x_ba_TipoFlujo AS t ON tf.IdEmpresa = t.IdEmpresa AND tf.IdTipoFlujo = t.IdTipoFlujo ON c.IdEmpresa = t.IdEmpresa AND c.IdTipoCbte = t.IdTipocbte AND c.IdCbteCble = t.IdCbteCble inner join
				  ba_Banco_Cuenta_x_tb_sucursal as s on ba.IdEmpresa = s.IdEmpresa and ba.IdBanco = s.IdBanco				  
where not exists(
select r.IdEmpresa from ct_cbtecble_Reversado as r
inner join ct_cbtecble as cr on r.IdEmpresa_Anu = cr.IdEmpresa
and r.IdTipoCbte_Anu = cr.IdTipoCbte
and r.IdCbteCble_Anu = cr.IdCbteCble
where r.idempresa = c.idempresa
and r.idtipocbte = c.idtipocbte
and r.idcbtecble = c.idcbtecble
and cr.cb_Fecha <= @FechaFin
)
and c.IdEmpresa = @IdEmpresa
and c.cb_Fecha BETWEEN @FechaIni AND @FechaFin
and s.IdSucursal = @IdSucursal
AND ltrim(rtrim(tc.CodTipoCbteBan)) NOT IN ('CHEQ', 'NDBA')
--and b.Estado = 'A'
UNION ALL
SELECT ba_TipoFlujo_Movimiento.IdEmpresa, ba_TipoFlujo_Movimiento.IdBanco, ba_Banco_Cuenta.ba_descripcion, ba_TipoFlujo_Movimiento.IdTipoFlujo, ba_TipoFlujo.Descricion, ba_TipoFlujo_Movimiento.Valor, 
                  ba_TipoFlujo_Movimiento.Valor
FROM     ba_TipoFlujo_Movimiento INNER JOIN
                  ba_TipoFlujo ON ba_TipoFlujo_Movimiento.IdEmpresa = ba_TipoFlujo.IdEmpresa AND ba_TipoFlujo_Movimiento.IdTipoFlujo = ba_TipoFlujo.IdTipoFlujo INNER JOIN
                  ba_Banco_Cuenta ON ba_TipoFlujo_Movimiento.IdEmpresa = ba_Banco_Cuenta.IdEmpresa AND ba_TipoFlujo_Movimiento.IdBanco = ba_Banco_Cuenta.IdBanco INNER JOIN
				  ba_Banco_Cuenta_x_tb_sucursal as s on ba_Banco_Cuenta.IdEmpresa = s.IdEmpresa and ba_Banco_Cuenta.IdBanco = s.IdBanco
WHERE  (ba_TipoFlujo_Movimiento.Estado = 1)  AND ba_TipoFlujo_Movimiento.IdEmpresa = @IdEmpresa and s.IdSucursal = @IdSucursal AND ba_TipoFlujo_Movimiento.Fecha BETWEEN @FechaIni AND @FechaFin
AND ba_TipoFlujo_Movimiento.Valor > 0
) a
group by a.IdEmpresa, a.IdBanco, a.ba_descripcion, a.IdTipoFlujo, a.NomFlujo
having ROUND(sum(round(a.Valor,2)),2) != 0
) A WHERE web.ba_SPBAN_012.IdEmpresa = A.IdEmpresa
AND web.ba_SPBAN_012.IdBanco = A.IdBanco
AND web.ba_SPBAN_012.IdTipoFlujo = A.IdTipoFlujo
END

update [web].[ba_SPBAN_012] set Egresos = abs(egresos) where IdUsuario = @IdUsuario

IF(@MostrarSaldo0 = 0)
BEGIN
DELETE [web].[ba_SPBAN_012]
where IdUsuario = @IdUsuario AND SaldoFinal = 0 AND SaldoInicial = 0 AND Ingresos = 0 AND Egresos = 0
END

select [IdEmpresa]            ,[IdBanco]           ,[IdTipoFlujo]           ,[IdUsuario]           ,[ba_descripcion]           ,[nom_tipo_flujo]
           ,[SaldoInicial]           ,[Ingresos]           ,[Egresos]           ,[SaldoFinal]           ,[SaldoFinalBanco] from [web].[ba_SPBAN_012]
		   where IdUsuario = @IdUsuario