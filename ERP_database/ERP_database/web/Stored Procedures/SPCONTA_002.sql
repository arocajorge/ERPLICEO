
--exec web.SPCONTA_002 1,'0101004001','01/01/2019','01/31/2019'
CREATE PROC [web].[SPCONTA_002]
(
@IdEmpresa int,
@IdSucursalIni int,
@IdSucursalFin int,
@IdCtaCble varchar(20),
@FechaIni datetime,
@FechaFin datetime
)
AS
DECLARE @SaldoInicial float
DECLARE @SignoOperacion int

select @SignoOperacion = g.gc_signo_operacion from ct_grupocble as g inner join ct_plancta as p
on g.IdGrupoCble = p.IdGrupoCble
where IdEmpresa = @IdEmpresa AND p.IdCtaCble = @IdCtaCble



select @SaldoInicial = sum(d.dc_Valor) from ct_cbtecble_det d
inner join ct_cbtecble c
on c.IdEmpresa = d.IdEmpresa and c.IdTipoCbte = d.IdTipoCbte and c.IdCbteCble = d.IdCbteCble
where c.IdEmpresa = @IdEmpresa and d.IdCtaCble = @IdCtaCble and c.cb_Fecha < @FechaIni
and c.IdSucursal between @IdSucursalIni and @IdSucursalFin

SET @SaldoInicial = CASE WHEN @SignoOperacion < 0 THEN @SaldoInicial *-1 ELSE @SaldoInicial END	

SELECT        ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdTipoCbte, ct_cbtecble_det.IdCbteCble, ct_cbtecble_det.secuencia, ct_cbtecble_det.IdCtaCble,ct_cbtecble_det.IdCtaCble+' - '+ ct_plancta.pc_Cuenta pc_Cuenta, ct_cbtecble_det.dc_Valor, 
ISNULL(@SaldoInicial,0) AS SaldoInicial,
CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ct_cbtecble_det.dc_Valor ELSE 0 END AS dc_Valor_Debe,
CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Haber,
isnull(@SaldoInicial,0) + SUM(

CASE WHEN @SignoOperacion < 0 THEN 
				  dbo.ct_cbtecble_det.dc_Valor*-1
				  ELSE dbo.ct_cbtecble_det.dc_Valor
				  END 

) OVER(partition by ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble ORDER BY ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble,ct_cbtecble.cb_Fecha, ct_cbtecble_det.IdTipoCbte, ct_cbtecble_det.IdCbteCble, ct_cbtecble_det.secuencia) as Saldo,
ct_cbtecble.cb_Fecha, ct_cbtecble.cb_Observacion, ct_cbtecble.cb_Estado, ct_cbtecble_tipo.tc_TipoCbte, m.IdMes, m.smes, ct_cbtecble.IdSucursal, Su_Descripcion
FROM            ct_cbtecble INNER JOIN
                         ct_cbtecble_det ON ct_cbtecble.IdEmpresa = ct_cbtecble_det.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_det.IdTipoCbte AND ct_cbtecble.IdCbteCble = ct_cbtecble_det.IdCbteCble INNER JOIN
                         ct_plancta ON ct_cbtecble_det.IdEmpresa = ct_plancta.IdEmpresa AND ct_cbtecble_det.IdCtaCble = ct_plancta.IdCtaCble INNER JOIN
                         ct_cbtecble_tipo ON ct_cbtecble.IdEmpresa = ct_cbtecble_tipo.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_tipo.IdTipoCbte left join
						 tb_mes as m on month(ct_cbtecble.cb_fecha) = m.idMes left join
						 tb_sucursal as su on ct_cbtecble.IdEmpresa = SU.IdEmpresa AND ct_cbtecble.IdSucursal = SU.IdSucursal
where ct_cbtecble_det.IdEmpresa = @IdEmpresa and ct_cbtecble.cb_Fecha between @FechaIni and @FechaFin and ct_cbtecble_det.IdCtaCble = @IdCtaCble
and ct_cbtecble.IdSucursal between @IdSucursalIni and @IdSucursalFin
ORDER BY ct_cbtecble_det.IdEmpresa, ct_cbtecble_det.IdCtaCble, ct_cbtecble.cb_Fecha, ct_cbtecble_det.IdTipoCbte, ct_cbtecble_det.IdCbteCble, ct_cbtecble_det.secuencia