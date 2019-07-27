--exec [web].[SPCXP_015] 2,8,8,3204,3204,'28/02/2019',0
CREATE PROCEDURE [web].[SPCXP_015]
(
@IdEmpresa int,
@IdSucursalIni int,
@IdSucursalFin int,
@IdProveedorIni numeric,
@IdProveedorFin numeric,
@FechaCorte date,
@MostrarSaldo0 bit
)
AS
select * from (
select og.IdEmpresa, og.IdTipoCbte_Ogiro, og.IdCbteCble_Ogiro, og.IdSucursal, su.Su_Descripcion, og.co_factura, og.IdProveedor, per.pe_nombreCompleto, 
og.co_observacion, og.co_FechaFactura, og.co_FechaFactura_vct, tp.Codigo, og.co_subtotal_iva, og.co_subtotal_siniva, og.co_valoriva, og.co_total, 
isnull(ret.re_valor_retencion,0)ValorRetencion, isnull(ValorNC,0)ValorNC, isnull(ab.ValorAbono,0)ValorAbono, 
round(og.co_total - isnull(ret.re_valor_retencion,0) - isnull(ValorNC,0) - isnull(ab.ValorAbono,0),2) as Saldo
from cp_orden_giro as og inner join
cp_proveedor as pro on og.IdEmpresa = pro.IdEmpresa and og.IdProveedor = pro.IdProveedor inner join
tb_persona as per on pro.IdPersona = per.IdPersona left join
cp_TipoDocumento as tp on tp.CodTipoDocumento = og.IdOrden_giro_Tipo left join
tb_sucursal as su on og.IdEmpresa = su.IdEmpresa and og.IdSucursal = su.IdSucursal
/*RETENCION*/
left join (
select c.IdEmpresa_Ogiro, c.IdTipoCbte_Ogiro, c.IdCbteCble_Ogiro, sum(d.re_valor_retencion)re_valor_retencion
from cp_retencion as c
inner join cp_retencion_det as d
on c.IdEmpresa= d.IdEmpresa
and c.IdRetencion = d.IdRetencion
where c.fecha <= @FechaCorte
group by c.IdEmpresa_Ogiro, c.IdTipoCbte_Ogiro, c.IdCbteCble_Ogiro
) ret on og.IdEmpresa = ret.IdEmpresa_Ogiro and og.IdTipoCbte_Ogiro = ret.IdTipoCbte_Ogiro and og.IdCbteCble_Ogiro = ret.IdCbteCble_Ogiro
/*NOTAS DE CREDITO*/
LEFT JOIN (
SELECT f.IdEmpresa_cxp, f.IdTipoCbte_cxp, f.IdCbteCble_cxp, sum(f.MontoAplicado)ValorNC
FROM cp_orden_pago_cancelaciones AS f
where exists(
select nc.IdEmpresa from cp_nota_DebCre as nc
where nc.IdEmpresa = f.IdEmpresa_pago
and nc.IdTipoCbte_Nota = f.IdTipoCbte_pago
and nc.IdCbteCble_Nota = f.IdCbteCble_pago
and nc.cn_fecha <= @FechaCorte
)
group by f.IdEmpresa_cxp, f.IdTipoCbte_cxp, f.IdCbteCble_cxp
) as nc on og.IdEmpresa = nc.IdEmpresa_cxp and og.IdTipoCbte_Ogiro = nc.IdTipoCbte_cxp and og.IdCbteCble_Ogiro = nc.IdCbteCble_cxp
/*ABONOS*/
LEFT JOIN (
SELECT f.IdEmpresa_cxp, f.IdTipoCbte_cxp, f.IdCbteCble_cxp, sum(f.MontoAplicado)ValorAbono
FROM cp_orden_pago_cancelaciones AS f
inner join ct_cbtecble as pa on f.IdEmpresa = pa.IdEmpresa
and f.IdTipoCbte_pago = pa.IdTipoCbte
and f.IdCbteCble_pago = pa.IdCbteCble
where not exists(
select nc.IdEmpresa from cp_nota_DebCre as nc
where nc.IdEmpresa = f.IdEmpresa_pago
and nc.IdTipoCbte_Nota = f.IdTipoCbte_pago
and nc.IdCbteCble_Nota = f.IdCbteCble_pago
) and pa.cb_Fecha <= @FechaCorte
group by f.IdEmpresa_cxp, f.IdTipoCbte_cxp, f.IdCbteCble_cxp
) as ab on og.IdEmpresa = ab.IdEmpresa_cxp and og.IdTipoCbte_Ogiro = ab.IdTipoCbte_cxp and og.IdCbteCble_Ogiro = ab.IdCbteCble_cxp
WHERE og.IdEmpresa = @IdEmpresa and og.IdSucursal between @IdSucursalIni AND @IdSucursalFin and og.co_fechaFactura <= @FechaCorte
and og.IdProveedor between @IdProveedorIni AND @IdProveedorFin and og.Estado = 'A'

UNION ALL

select og.IdEmpresa, og.IdTipoCbte_Nota, og.IdCbteCble_Nota, og.IdSucursal, su.Su_Descripcion, isnull(og.cod_nota, cast(og.IdCbteCble_Nota as varchar(20))), og.IdProveedor, per.pe_nombreCompleto, 
og.cn_observacion, og.cn_fecha, og.cn_Fecha_vcto, 'ND', og.cn_subtotal_iva, og.cn_subtotal_siniva, og.cn_valoriva, og.cn_total, 
0 ValorRetencion, isnull(ValorNC,0)ValorNC, isnull(ab.ValorAbono,0)ValorAbono, 
round(og.cn_total - isnull(ValorNC,0) - isnull(ab.ValorAbono,0),2) as Saldo
from cp_nota_DebCre as og inner join
cp_proveedor as pro on og.IdEmpresa = pro.IdEmpresa and og.IdProveedor = pro.IdProveedor inner join
tb_persona as per on pro.IdPersona = per.IdPersona left join
tb_sucursal as su on og.IdEmpresa = su.IdEmpresa and og.IdSucursal = su.IdSucursal
/*NOTAS DE CREDITO*/
LEFT JOIN (
SELECT f.IdEmpresa_cxp, f.IdTipoCbte_cxp, f.IdCbteCble_cxp, sum(f.MontoAplicado)ValorNC
FROM cp_orden_pago_cancelaciones AS f
where exists(
select nc.IdEmpresa from cp_nota_DebCre as nc
where nc.IdEmpresa = f.IdEmpresa_pago
and nc.IdTipoCbte_Nota = f.IdTipoCbte_pago
and nc.IdCbteCble_Nota = f.IdCbteCble_pago
and nc.cn_fecha <= @FechaCorte
)
group by f.IdEmpresa_cxp, f.IdTipoCbte_cxp, f.IdCbteCble_cxp
) as nc on og.IdEmpresa = nc.IdEmpresa_cxp and og.IdTipoCbte_Nota = nc.IdTipoCbte_cxp and og.IdCbteCble_Nota = nc.IdCbteCble_cxp
/*ABONOS*/
LEFT JOIN (
SELECT f.IdEmpresa_cxp, f.IdTipoCbte_cxp, f.IdCbteCble_cxp, sum(f.MontoAplicado)ValorAbono
FROM cp_orden_pago_cancelaciones AS f
inner join ct_cbtecble as pa on f.IdEmpresa = pa.IdEmpresa
and f.IdTipoCbte_pago = pa.IdTipoCbte
and f.IdCbteCble_pago = pa.IdCbteCble
where not exists(
select nc.IdEmpresa from cp_nota_DebCre as nc
where nc.IdEmpresa = f.IdEmpresa_pago
and nc.IdTipoCbte_Nota = f.IdTipoCbte_pago
and nc.IdCbteCble_Nota = f.IdCbteCble_pago
) and pa.cb_Fecha <= @FechaCorte
group by f.IdEmpresa_cxp, f.IdTipoCbte_cxp, f.IdCbteCble_cxp
) as ab on og.IdEmpresa = ab.IdEmpresa_cxp and og.IdTipoCbte_Nota = ab.IdTipoCbte_cxp and og.IdCbteCble_Nota = ab.IdCbteCble_cxp
WHERE og.IdEmpresa = @IdEmpresa and og.IdSucursal between @IdSucursalIni AND @IdSucursalFin and og.cn_Fecha <= @FechaCorte
and og.IdProveedor between @IdProveedorIni AND @IdProveedorFin and og.Estado = 'A' and og.DebCre = 'D'

UNION ALL

SELECT ISNULL(D.IdEmpresa_cxp,0) IdEmpresa, ISNULL(D.IdTipoCbte_cxp,0) IdTipoCbte, ISNULL(D.IdCbteCble_cxp,0) IdCbteCble, C.IdSucursal, su.Su_Descripcion, cast(c.IdOrdenPago as varchar(20)),
c.IdEntidad, pe_nombreCompleto, c.Observacion, c.Fecha, c.Fecha, 'OP', 0,D.Valor_a_pagar,0,D.Valor_a_pagar,0,
isnull(ValorNC,0), isnull(ValorAbono,0), round(D.Valor_a_pagar - isnull(ValorNC,0) - isnull(ValorAbono,0),2) Saldo
FROM cp_orden_pago_det AS D INNER JOIN
cp_orden_pago AS C ON C.IdEmpresa = D.IdEmpresa AND C.IdOrdenPago = D.IdOrdenPago LEFT JOIN
cp_proveedor AS PRO ON PRO.IdEmpresa = C.IdEmpresa AND PRO.IdProveedor = C.IdEntidad AND C.IdTipo_Persona = 'PROVEE' INNER JOIN
tb_sucursal AS su on su.IdEmpresa = c.IdEmpresa and su.IdSucursal = c.IdSucursal left join
tb_persona as per on pro.IdPersona = per.IdPersona 
/*NOTAS DE CREDITO*/
LEFT JOIN (
SELECT f.IdEmpresa_cxp, f.IdTipoCbte_cxp, f.IdCbteCble_cxp, sum(f.MontoAplicado)ValorNC
FROM cp_orden_pago_cancelaciones AS f
where exists(
select nc.IdEmpresa from cp_nota_DebCre as nc
where nc.IdEmpresa = f.IdEmpresa_pago
and nc.IdTipoCbte_Nota = f.IdTipoCbte_pago
and nc.IdCbteCble_Nota = f.IdCbteCble_pago
and nc.cn_fecha <= @FechaCorte
)
group by f.IdEmpresa_cxp, f.IdTipoCbte_cxp, f.IdCbteCble_cxp
) as nc on C.IdEmpresa = nc.IdEmpresa_cxp and D.IdTipoCbte_cxp = nc.IdTipoCbte_cxp and D.IdCbteCble_cxp = nc.IdCbteCble_cxp

/*ABONOS*/
LEFT JOIN (
SELECT f.IdEmpresa_cxp, f.IdTipoCbte_cxp, f.IdCbteCble_cxp, sum(f.MontoAplicado)ValorAbono
FROM cp_orden_pago_cancelaciones AS f
inner join ct_cbtecble as pa on f.IdEmpresa = pa.IdEmpresa
and f.IdTipoCbte_pago = pa.IdTipoCbte
and f.IdCbteCble_pago = pa.IdCbteCble
where not exists(
select nc.IdEmpresa from cp_nota_DebCre as nc
where nc.IdEmpresa = f.IdEmpresa_pago
and nc.IdTipoCbte_Nota = f.IdTipoCbte_pago
and nc.IdCbteCble_Nota = f.IdCbteCble_pago
) and pa.cb_Fecha <= @FechaCorte

group by f.IdEmpresa_cxp, f.IdTipoCbte_cxp, f.IdCbteCble_cxp
) as AB on d.IdEmpresa = AB.IdEmpresa_cxp and D.IdTipoCbte_cxp = AB.IdTipoCbte_cxp and D.IdCbteCble_cxp = AB.IdCbteCble_cxp
WHERE C.IdEmpresa = @IdEmpresa AND C.IdSucursal BETWEEN @IdSucursalIni AND @IdSucursalFin
AND C.IdTipo_Persona = 'PROVEE' and c.Estado = 'A' and c.IdEntidad between @IdProveedorIni and @IdProveedorFin 
AND NOT EXISTS(
SELECT IdEmpresa FROM cp_orden_giro ne
where ne.IdEmpresa = d.IdEmpresa_cxp
and ne.IdTipoCbte_Ogiro = d.IdTipoCbte_cxp
and ne.IdCbteCble_Ogiro = d.IdCbteCble_cxp
)
AND NOT EXISTS(
SELECT IdEmpresa FROM cp_nota_DebCre ne
where ne.idempresa = d.IdEmpresa_cxp
and ne.IdTipoCbte_Nota = d.IdTipoCbte_cxp
and ne.IdCbteCble_Nota = d.IdCbteCble_cxp
)
and c.fecha <= @FechaCorte
)

a
where a.Saldo > case when @MostrarSaldo0 = 1 then -99999999999999 else 0 end