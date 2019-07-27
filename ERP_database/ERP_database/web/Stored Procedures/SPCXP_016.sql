--exec [web].[SPCXP_016] 2,'admin',1,'2019/02/01','2019/05/13'
CREATE PROCEDURE [web].[SPCXP_016]
(
@IdEmpresa int,
@IdUsuario varchar(50),
@MostrarSaldo0 bit,
@FechaIni date,
@FechaFin date
)
AS
delete [web].[cp_SPCXP_016] where IdUsuario = @IdUsuario

BEGIN  --INSERTO PROVEEDORES
INSERT INTO [web].[cp_SPCXP_016]
           ([IdEmpresa]
		   ,[IdSucursal]
           ,[IdProveedor]
           ,[IdUsuario]
		   ,[Su_Descripcion]
           ,[pe_CedulaRuc]
           ,[pe_nombreCompleto]
           ,[SaldoInicial]
           ,[Compra]
           ,[Retenciones]
           ,[Pagos]
           ,[Saldo])

     select SU.IdEmpresa, f.IdSucursal, IdProveedor, @IdUsuario, su.Su_Descripcion, per.pe_cedulaRuc, per.pe_nombreCompleto,
	 0,0,0,0,0
	 from cp_proveedor as p inner join 
	 tb_persona as per on p.IdPersona = per.IdPersona inner join
	 tb_sucursal as su on su.IdEmpresa = p.IdEmpresa inner join
	 web.tb_FiltroReportes as f on su.IdEmpresa = f.IdEmpresa and su.IdSucursal = f.IdSucursal
	 where su.IdEmpresa = @IdEmpresa and f.IdUsuario = @IdUsuario
	 and (exists(
	 select * from cp_orden_giro as og
	 where og.IdEmpresa = p.IdEmpresa
	 and og.IdProveedor = p.IdProveedor
	 and og.IdSucursal = f.IdSucursal
	 ) or exists(
	 select * from cp_nota_DebCre as og
	 where og.IdEmpresa = p.IdEmpresa
	 and og.IdProveedor = p.IdProveedor
	 and og.IdSucursal = f.IdSucursal
	 ) or exists(
	 select * from cp_orden_pago as og
	 where og.IdEmpresa = p.IdEmpresa
	 and og.IdEntidad = p.IdProveedor
	 and og.IdSucursal = f.IdSucursal
	 and og.IdTipo_Persona = 'PROVEE'
	 ))
END

BEGIN --ACTUALIZO SALDO INICIAL

UPDATE [web].[cp_SPCXP_016] SET SaldoInicial = B.TOTAL
FROM(
select A.IdEmpresa, A.IdSucursal, A.IdProveedor, SUM(A.Total ) TOTAL
FROM(

SELECT OG.IdEmpresa, OG.IdSucursal, OG.IdProveedor, ROUND(OG.co_total - isnull(RET.re_valor_retencion,0) - isnull(MontoAplicado,0),2) AS Total
FROM cp_orden_giro AS OG LEFT JOIN (
	SELECT c.IdEmpresa_Ogiro, c.IdTipoCbte_Ogiro, c.IdCbteCble_Ogiro, SUM(D.re_valor_retencion)re_valor_retencion
	FROM cp_retencion AS C inner join cp_retencion_det as D
	on c.IdEmpresa = d.IdEmpresa and c.IdRetencion = d.IdRetencion
	where c.Estado = 'A' and c.fecha < @FechaIni
	GROUP BY c.IdEmpresa_Ogiro, c.IdTipoCbte_Ogiro, c.IdCbteCble_Ogiro 
)AS RET ON RET.IdEmpresa_Ogiro = OG.IdEmpresa AND RET.IdTipoCbte_Ogiro = OG.IdTipoCbte_Ogiro AND RET.IdCbteCble_Ogiro = OG.IdCbteCble_Ogiro	LEFT JOIN
(
	SELECT IdEmpresa_cxp, IdTipoCbte_cxp, IdCbteCble_cxp, SUM(MontoAplicado) MontoAplicado 
	FROM cp_orden_pago_cancelaciones pa inner join 
	ct_cbtecble as ct on ct.IdEmpresa = pa.IdEmpresa_pago and ct.IdTipoCbte = pa.IdTipoCbte_pago and ct.IdCbteCble = pa.IdCbteCble_pago
	WHERE pa.IdEmpresa_cxp = @IdEmpresa and ct.cb_Fecha < @FechaIni
	GROUP BY IdEmpresa_cxp, IdTipoCbte_cxp, IdCbteCble_cxp
)AS PAG ON OG.IdEmpresa = PAG.IdEmpresa_cxp AND OG.IdTipoCbte_Ogiro = PAG.IdTipoCbte_cxp AND OG.IdCbteCble_Ogiro = PAG.IdCbteCble_cxp
inner join web.tb_FiltroReportes as f on og.IdEmpresa = f.IdEmpresa and og.IdSucursal = f.IdSucursal
where OG.IdEmpresa = @IdEmpresa AND OG.Estado = 'A' and f.IdUsuario = @IdUsuario
and OG.co_FechaFactura < @FechaIni

UNION ALL

SELECT OG.IdEmpresa, OG.IdSucursal, OG.IdProveedor, ROUND(OG.cn_total - isnull(MontoAplicado,0),2) AS Total
FROM cp_nota_DebCre AS OG LEFT JOIN
(
	SELECT IdEmpresa_cxp, IdTipoCbte_cxp, IdCbteCble_cxp, SUM(MontoAplicado) MontoAplicado 
	FROM cp_orden_pago_cancelaciones pa inner join 
	ct_cbtecble as ct on ct.IdEmpresa = pa.IdEmpresa_pago and ct.IdTipoCbte = pa.IdTipoCbte_pago and ct.IdCbteCble = pa.IdCbteCble_pago
	WHERE pa.IdEmpresa_cxp = @IdEmpresa and ct.cb_Fecha < @FechaIni
	GROUP BY IdEmpresa_cxp, IdTipoCbte_cxp, IdCbteCble_cxp
)AS PAG ON OG.IdEmpresa = PAG.IdEmpresa_cxp AND OG.IdTipoCbte_Nota = PAG.IdTipoCbte_cxp AND OG.IdCbteCble_Nota = PAG.IdCbteCble_cxp
inner join web.tb_FiltroReportes as f on og.IdEmpresa = f.IdEmpresa and og.IdSucursal = f.IdSucursal
where OG.IdEmpresa = @IdEmpresa AND OG.Estado = 'A' and f.IdUsuario = @IdUsuario
and OG.cn_fecha < @FechaIni AND OG.DebCre = 'D'

UNION ALL

SELECT OG.IdEmpresa, OG.IdSucursal, OG.IdEntidad, ROUND(opd.Valor_a_pagar - isnull(MontoAplicado,0),2) AS Total
FROM cp_orden_pago AS OG inner join 
cp_orden_pago_det as opd on opd.IdEmpresa = og.IdEmpresa and og.IdOrdenPago = opd.IdOrdenPago 

LEFT JOIN
(
	SELECT IdEmpresa_cxp, IdTipoCbte_cxp, IdCbteCble_cxp, SUM(MontoAplicado) MontoAplicado 
	FROM cp_orden_pago_cancelaciones pa inner join 
	ct_cbtecble as ct on ct.IdEmpresa = pa.IdEmpresa_pago and ct.IdTipoCbte = pa.IdTipoCbte_pago and ct.IdCbteCble = pa.IdCbteCble_pago
	WHERE pa.IdEmpresa_cxp = @IdEmpresa and ct.cb_Fecha < @FechaIni
	GROUP BY IdEmpresa_cxp, IdTipoCbte_cxp, IdCbteCble_cxp
)AS PAG ON opd.IdEmpresa = PAG.IdEmpresa_cxp AND opd.IdTipoCbte_cxp = PAG.IdTipoCbte_cxp AND opd.IdCbteCble_cxp = PAG.IdCbteCble_cxp
inner join web.tb_FiltroReportes as f on og.IdEmpresa = f.IdEmpresa and og.IdSucursal = f.IdSucursal
where OG.IdEmpresa = @IdEmpresa AND OG.Estado = 'A' and f.IdUsuario = @IdUsuario and OG.Fecha < @FechaIni and og.IdTipo_Persona = 'PROVEE'
AND NOT EXISTS(
SELECT R.IdEmpresa FROM cp_orden_giro R
WHERE R.IdEmpresa = opd.IdEmpresa
AND R.IdTipoCbte_Ogiro = OPD.IdTipoCbte_cxp
AND R.IdCbteCble_Ogiro = OPD.IdCbteCble_cxp
)
AND NOT EXISTS(
SELECT R.IdEmpresa FROM cp_nota_DebCre R
WHERE R.IdEmpresa = opd.IdEmpresa
AND R.IdTipoCbte_Nota = OPD.IdTipoCbte_cxp
AND R.IdCbteCble_Nota = OPD.IdCbteCble_cxp
)
) A
GROUP BY A.IdEmpresa, A.IdSucursal, A.IdProveedor
) B WHERE B.IdEmpresa = [web].[cp_SPCXP_016].IdEmpresa and B.IdSucursal = [web].[cp_SPCXP_016].IdSucursal and B.IdProveedor = [web].[cp_SPCXP_016].IdProveedor and [web].[cp_SPCXP_016].IdUsuario = @IdUsuario
END

BEGIN --ACTUALIZO COMPRAS

UPDATE [web].[cp_SPCXP_016] SET Compra = round(B.TOTAL,2)
FROM(
SELECT A.IdEmpresa,A.IdSucursal,A.IdProveedor,SUM(A.TOTAL) TOTAL FROM(
SELECT OG.IdEmpresa, OG.IdSucursal, OG.IdProveedor, OG.co_total TOTAL
FROM cp_orden_giro OG INNER JOIN
WEB.tb_FiltroReportes AS F ON OG.IdEmpresa = F.IdEmpresa
AND OG.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
WHERE OG.IdEmpresa = @IdEmpresa AND OG.co_FechaFactura BETWEEN @FechaIni AND @FechaFin AND OG.Estado = 'A'
UNION ALL
SELECT OG.IdEmpresa, OG.IdSucursal, OG.IdProveedor, OG.cn_total
FROM cp_nota_DebCre OG INNER JOIN
WEB.tb_FiltroReportes AS F ON OG.IdEmpresa = F.IdEmpresa
AND OG.IdSucursal = F.IdSucursal AND F.IdUsuario = @IdUsuario
WHERE OG.IdEmpresa = @IdEmpresa AND OG.cn_fecha BETWEEN @FechaIni AND @FechaFin AND OG.Estado = 'A'
AND OG.DebCre = 'D'
UNION ALL
SELECT d.IdEmpresa, c.IdSucursal, c.IdEntidad, d.Valor_a_pagar 
FROM cp_orden_pago c inner join 
cp_orden_pago_det d on c.IdEmpresa = d.IdEmpresa and c.IdOrdenPago = d.IdOrdenPago inner join
web.tb_FiltroReportes s on s.IdEmpresa = c.IdEmpresa and s.IdSucursal = c.IdSucursal AND S.IdUsuario = @IdUsuario
where c.IdEmpresa = @IdEmpresa and c.Fecha between @FechaIni and @FechaFin and c.Estado = 'A' and c.IdTipo_Persona = 'PROVEE'
AND NOT EXISTS(
SELECT R.IdEmpresa FROM cp_orden_giro R
WHERE R.IdEmpresa = d.IdEmpresa
AND R.IdTipoCbte_Ogiro = d.IdTipoCbte_cxp
AND R.IdCbteCble_Ogiro = d.IdCbteCble_cxp
)
AND NOT EXISTS(
SELECT R.IdEmpresa FROM cp_nota_DebCre R
WHERE R.IdEmpresa = d.IdEmpresa
AND R.IdTipoCbte_Nota = d.IdTipoCbte_cxp
AND R.IdCbteCble_Nota = d.IdCbteCble_cxp
)) A
GROUP BY A.IdEmpresa, A.IdSucursal, A.IdProveedor
) B
WHERE B.IdEmpresa = [web].[cp_SPCXP_016].IdEmpresa and B.IdSucursal = [web].[cp_SPCXP_016].IdSucursal and B.IdProveedor = [web].[cp_SPCXP_016].IdProveedor and [web].[cp_SPCXP_016].IdUsuario = @IdUsuario
END

BEGIN --ACTUALIZO RETENCIONES


UPDATE [web].[cp_SPCXP_016] SET Retenciones = round(B.TOTAL,2)
FROM(
SELECT R.IdEmpresa, R.IdSucursal, R.IdProveedor, ROUND(SUM(RD.re_valor_retencion),2) TOTAL 
FROM VWCP_RETENCION AS R INNER JOIN 
WEB.tb_FiltroReportes F ON R.IdEmpresa = F.IdEmpresa AND R.IdSucursal = F.IdSucursal LEFT JOIN
cp_retencion_det AS RD ON R.IdEmpresa = RD.IdEmpresa AND R.IdRetencion = RD.IdRetencion
WHERE R.IdEmpresa = @IdEmpresa AND R.fecha BETWEEN @FechaIni AND @FechaFin AND F.IdUsuario = @IdUsuario AND R.Estado = 'A'
GROUP BY R.IdEmpresa, R.IdSucursal, R.IdProveedor
)B WHERE B.IdEmpresa = [web].[cp_SPCXP_016].IdEmpresa and B.IdSucursal = [web].[cp_SPCXP_016].IdSucursal and B.IdProveedor = [web].[cp_SPCXP_016].IdProveedor and [web].[cp_SPCXP_016].IdUsuario = @IdUsuario
END

BEGIN --ACTUALIZO PAGOS

UPDATE [web].[cp_SPCXP_016] SET Pagos = round(B.TOTAL,2)
FROM(
SELECT op.IdEmpresa, op.IdSucursal, op.IdEntidad, round(sum(can.MontoAplicado),2) TOTAL 
FROM cp_orden_pago_cancelaciones can inner join 
cp_orden_pago as op on can.IdEmpresa = op.IdEmpresa and can.IdOrdenPago_op = op.IdOrdenPago inner join
web.tb_FiltroReportes as f on f.IdEmpresa = op.IdEmpresa and f.IdSucursal = op.IdSucursal INNER JOIN 
ct_cbtecble as c on can.IdEmpresa_pago = c.IdEmpresa and can.IdTipoCbte_pago = c.IdTipoCbte and can.IdCbteCble_pago = c.IdCbteCble
WHERE op.IdEmpresa = @IdEmpresa and c.cb_Fecha between @FechaIni and @FechaFin and f.IdUsuario = @IdUsuario and op.Estado = 'A' AND OP.IdTipo_Persona = 'PROVEE'
group by op.IdEmpresa, op.IdSucursal, op.IdEntidad
) B WHERE B.IdEmpresa = [web].[cp_SPCXP_016].IdEmpresa and B.IdSucursal = [web].[cp_SPCXP_016].IdSucursal and B.IdEntidad = [web].[cp_SPCXP_016].IdProveedor and [web].[cp_SPCXP_016].IdUsuario = @IdUsuario

END

UPDATE [web].[cp_SPCXP_016] SET Saldo = ROUND(SaldoInicial + Compra - Retenciones - Pagos,2)
WHERE IdUsuario = @IdUsuario

IF(@MostrarSaldo0 = 0)
BEGIN
	DELETE [web].[cp_SPCXP_016] 
	WHERE IdEmpresa = @IdEmpresa AND IdUsuario = @IdUsuario AND Saldo = 0
END

select [IdEmpresa]		   ,[IdSucursal]           ,[IdProveedor]           ,[IdUsuario]		   ,[Su_Descripcion]           ,[pe_CedulaRuc]           ,[pe_nombreCompleto]
           ,[SaldoInicial]           ,[Compra]           ,[Retenciones]           ,[Pagos]           ,[Saldo] 
from [web].[cp_SPCXP_016]
WHERE IdUsuario = @IdUsuario