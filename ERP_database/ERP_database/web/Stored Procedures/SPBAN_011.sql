--EXEC web.SPBAN_011 1,'admin',1,'2019/04/01','2019/05/31'
CREATE PROCEDURE web.SPBAN_011
(
@IdEmpresa int,
@IdUsuario varchar(50),
@IdSucursal int,
@FechaIni date,
@FechaFin date
)
AS
DELETE [web].[ba_SPBAN_011] WHERE IdUsuario = @IdUsuario

INSERT INTO [web].[ba_SPBAN_011]
           ([IdEmpresa]
           ,[IdBanco]
           ,[IdUsuario]
           ,[IdCtaCble]
           ,[Descripcion]
		   ,[Su_Descripcion]
           ,[SaldoAnterior]
           ,[Ingreso]
           ,[Egreso]
           ,[SaldoFinal])
SELECT  b.IdEmpresa, b.IdBanco, @IdUsuario, b.IdCtaCble, b.ba_descripcion, su.Su_Descripcion, 0,0,0,0 
FROM ba_Banco_Cuenta b inner join ba_Banco_Cuenta_x_tb_sucursal as s 
on s.IdEmpresa = b.IdEmpresa and s.IdBanco = b.IdBanco inner join tb_sucursal as su
on su.IdEmpresa = s.IdEmpresa and su.IdSucursal = s.IdSucursal
WHERE s.IdEmpresa = @IdEmpresa and s.IdSucursal = @IdSucursal

BEGIN --SALDO INICIAL
	update web.ba_SPBAN_011 set SaldoAnterior = A.dc_Valor
	from(
	select d.IdEmpresa, b.IdBanco, b.IdCtaCble, sum(d.dc_Valor) dc_Valor 
	from ct_cbtecble_det as d inner join 
	ct_cbtecble as c on c.IdEmpresa = d.IdEmpresa and c.IdTipoCbte = d.IdTipoCbte and c.IdCbteCble = d.IdCbteCble inner join 
	web.ba_SPBAN_011 as b on d.IdEmpresa = b.IdEmpresa and d.IdCtaCble = b.IdCtaCble
	where b.IdEmpresa = @IdEmpresa and b.IdUsuario = @IdUsuario and c.cb_Fecha < @FechaIni
	group by d.IdEmpresa, b.IdBanco, b.IdCtaCble) A WHERE A.IdEmpresa = web.ba_SPBAN_011.IdEmpresa AND A.IdBanco = web.ba_SPBAN_011.IdBanco AND A.IdCtaCble = web.ba_SPBAN_011.IdCtaCble AND web.ba_SPBAN_011.IdUsuario = @IdUsuario
END

BEGIN --INGRESO
	update web.ba_SPBAN_011 set Ingreso = A.dc_Valor
	from(
	select d.IdEmpresa, b.IdBanco, b.IdCtaCble, sum(d.dc_Valor) dc_Valor 
	from ct_cbtecble_det as d inner join 
	ct_cbtecble as c on c.IdEmpresa = d.IdEmpresa and c.IdTipoCbte = d.IdTipoCbte and c.IdCbteCble = d.IdCbteCble inner join 
	web.ba_SPBAN_011 as b on d.IdEmpresa = b.IdEmpresa and d.IdCtaCble = b.IdCtaCble
	where b.IdEmpresa = @IdEmpresa and b.IdUsuario = @IdUsuario and c.cb_Fecha BETWEEN @FechaIni AND @FechaFin AND D.dc_Valor > 0
	group by d.IdEmpresa, b.IdBanco, b.IdCtaCble) A WHERE A.IdEmpresa = web.ba_SPBAN_011.IdEmpresa AND A.IdBanco = web.ba_SPBAN_011.IdBanco AND A.IdCtaCble = web.ba_SPBAN_011.IdCtaCble AND web.ba_SPBAN_011.IdUsuario = @IdUsuario
END

BEGIN --EGRESO
	update web.ba_SPBAN_011 set Egreso = A.dc_Valor
	from(
	select d.IdEmpresa, b.IdBanco, b.IdCtaCble, sum(d.dc_Valor) dc_Valor 
	from ct_cbtecble_det as d inner join 
	ct_cbtecble as c on c.IdEmpresa = d.IdEmpresa and c.IdTipoCbte = d.IdTipoCbte and c.IdCbteCble = d.IdCbteCble inner join 
	web.ba_SPBAN_011 as b on d.IdEmpresa = b.IdEmpresa and d.IdCtaCble = b.IdCtaCble
	where b.IdEmpresa = @IdEmpresa and b.IdUsuario = @IdUsuario and c.cb_Fecha BETWEEN @FechaIni AND @FechaFin AND D.dc_Valor < 0
	group by d.IdEmpresa, b.IdBanco, b.IdCtaCble) A WHERE A.IdEmpresa = web.ba_SPBAN_011.IdEmpresa AND A.IdBanco = web.ba_SPBAN_011.IdBanco AND A.IdCtaCble = web.ba_SPBAN_011.IdCtaCble AND web.ba_SPBAN_011.IdUsuario = @IdUsuario
END

BEGIN --SALDO FINAL
	update web.ba_SPBAN_011 set SaldoFinal = A.dc_Valor
	from(
	select d.IdEmpresa, b.IdBanco, b.IdCtaCble, sum(d.dc_Valor) dc_Valor 
	from ct_cbtecble_det as d inner join 
	ct_cbtecble as c on c.IdEmpresa = d.IdEmpresa and c.IdTipoCbte = d.IdTipoCbte and c.IdCbteCble = d.IdCbteCble inner join 
	web.ba_SPBAN_011 as b on d.IdEmpresa = b.IdEmpresa and d.IdCtaCble = b.IdCtaCble
	where b.IdEmpresa = @IdEmpresa and b.IdUsuario = @IdUsuario and c.cb_Fecha <= @FechaFin 
	group by d.IdEmpresa, b.IdBanco, b.IdCtaCble) A WHERE A.IdEmpresa = web.ba_SPBAN_011.IdEmpresa AND A.IdBanco = web.ba_SPBAN_011.IdBanco AND A.IdCtaCble = web.ba_SPBAN_011.IdCtaCble AND web.ba_SPBAN_011.IdUsuario = @IdUsuario
END

select IdEmpresa, IdBanco, IdUsuario, IdCtaCble, Descripcion, Su_Descripcion, SaldoAnterior, Ingreso, Egreso, SaldoFinal 
from [web].[ba_SPBAN_011] 
where IdEmpresa = @IdEmpresa and IdUsuario = @IdUsuario