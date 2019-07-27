CREATE PROCEDURE web.SPCXP_010
(  @IdEmpresa int, @IdProveedorIni numeric, @IdProveedorFin numeric, @FechaIni date, @FechaFin date, @MostrarAnulados bit
)
AS
SELECT ISNULL(REP.IdEmpresa,0) IdEmpresa, ISNULL(REP.IdProveedor,0) IdProveedor, REP.pe_nombreCompleto, REP.Fecha, REP.Estado, REP.Tipo, REP.Referencia, REP.co_observacion, REP.Debito, REP.Credito, 
SUM(REP.VALOR) OVER(PARTITION BY REP.IdProveedor ORDER BY Rep.IdEmpresa, Rep.IdProveedor, Rep.Fecha,Rep.Secuencia, Rep.Referencia, Rep.Debito, REP.Credito) as Saldo, 
SUM(REP.VALOR) OVER(ORDER BY Rep.IdEmpresa, Rep.IdProveedor, Rep.Fecha,Rep.Secuencia, Rep.Referencia, Rep.Debito, REP.Credito) as SaldoModulo, 
SECUENCIA as Secuencia
FROM(


select a.IdEmpresa, a.IdProveedor, a.pe_nombreCompleto, dateadd(day,-1,@FechaIni) Fecha, 'A' Estado, 'S.I.' Tipo, '' Referencia, 'SALDO INICIAL' co_observacion, 0 as Debito, 0 as Credito, sum(Valor) as Valor, -1 as Secuencia
from (
		SELECT        cp_orden_giro.IdEmpresa, cp_orden_giro.IdProveedor, tb_persona.pe_nombreCompleto, cp_orden_giro.co_fechaOg, cp_orden_giro.Estado, cp_TipoDocumento.Descripcion Tipo, 
								 cp_orden_giro.co_serie + '-' + cp_orden_giro.co_factura AS Referencia, cp_orden_giro.co_observacion, 0 AS Debito, cp_orden_giro.co_total AS Credito, 						 
								 case when cp_orden_giro.Estado = 'A' THEN cp_orden_giro.co_total ELSE 0 END AS Valor, 0 AS Secuencia
		FROM            cp_orden_giro INNER JOIN
								 cp_proveedor ON cp_orden_giro.IdEmpresa = cp_proveedor.IdEmpresa AND cp_orden_giro.IdProveedor = cp_proveedor.IdProveedor INNER JOIN
								 tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona INNER JOIN
								 cp_TipoDocumento ON cp_orden_giro.IdOrden_giro_Tipo = cp_TipoDocumento.CodTipoDocumento
		where cp_orden_giro.IdEmpresa = @IdEmpresa and cp_orden_giro.IdProveedor between @IdProveedorIni and @IdProveedorFin and cp_orden_giro.co_fechaOg < @FechaIni and cp_orden_giro.Estado ='A'
		UNION ALL
		SELECT        cp_nota_DebCre.IdEmpresa, cp_nota_DebCre.IdProveedor, tb_persona.pe_nombreCompleto, cp_nota_DebCre.cn_fecha, cp_nota_DebCre.Estado, 
								 CASE WHEN cp_nota_DebCre.DebCre = 'C' THEN 'Nota de crédito' ELSE 'Nota de débito' END AS Tipo, 
								 CASE WHEN cp_nota_DebCre.IdTipoNota = 'T_TIP_NOTA_INT' THEN 'INT-' + CAST(cp_nota_DebCre.IdCbteCble_Nota AS VARCHAR(20)) 
								 ELSE cp_nota_DebCre.cn_serie1 + '-' + cp_nota_DebCre.cn_serie2 + '-' + cp_nota_DebCre.cn_Nota END AS referencia, cp_nota_DebCre.cn_observacion, 
								 CASE WHEN cp_nota_DebCre.DebCre = 'C' THEN cp_nota_DebCre.cn_total ELSE 0 END AS Debito, CASE WHEN cp_nota_DebCre.DebCre = 'D' THEN cp_nota_DebCre.cn_total ELSE 0 END AS Credito, 
								 CASE WHEN cp_nota_DebCre.DebCre = 'D' THEN cp_nota_DebCre.cn_total ELSE cp_nota_DebCre.cn_total * - 1 END AS Valor, 0 AS Secuencia
		FROM            cp_proveedor INNER JOIN
								 cp_nota_DebCre ON cp_proveedor.IdEmpresa = cp_nota_DebCre.IdEmpresa AND cp_proveedor.IdProveedor = cp_nota_DebCre.IdProveedor INNER JOIN
								 tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona
		where cp_nota_DebCre.IdEmpresa = @IdEmpresa and cp_nota_DebCre.IdProveedor between @IdProveedorIni and @IdProveedorFin and cp_nota_DebCre.cn_fecha <  @FechaIni and cp_nota_DebCre.Estado = 'A'
		UNION ALL
		SELECT        cp_orden_giro.IdEmpresa, cp_orden_giro.IdProveedor, tb_persona.pe_nombreCompleto, cp_retencion.fecha, cp_retencion.Estado, 'Retención' AS Tipo, cp_retencion.serie1+'-'+cp_retencion.serie2+'-'+ cp_retencion.NumRetencion, cp_retencion.observacion,
				det.re_valor_retencion as Debito, 0 as Credito, det.re_valor_retencion *-1 as Valor, 2
		FROM            cp_proveedor INNER JOIN
								 cp_orden_giro ON cp_proveedor.IdEmpresa = cp_orden_giro.IdEmpresa AND cp_proveedor.IdProveedor = cp_orden_giro.IdProveedor INNER JOIN
								 tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona INNER JOIN
								 cp_retencion ON cp_orden_giro.IdEmpresa = cp_retencion.IdEmpresa_Ogiro AND cp_orden_giro.IdCbteCble_Ogiro = cp_retencion.IdCbteCble_Ogiro AND cp_orden_giro.IdTipoCbte_Ogiro = cp_retencion.IdTipoCbte_Ogiro
								 LEFT OUTER JOIN(
										SELECT        cp_retencion.IdEmpresa, cp_retencion.IdRetencion, SUM(cp_retencion_det.re_valor_retencion) AS re_valor_retencion
										FROM            cp_retencion_det INNER JOIN
										cp_retencion ON cp_retencion_det.IdEmpresa = cp_retencion.IdEmpresa AND cp_retencion_det.IdRetencion = cp_retencion.IdRetencion LEFT OUTER JOIN
										cp_proveedor INNER JOIN
										cp_orden_giro ON cp_proveedor.IdEmpresa = cp_orden_giro.IdEmpresa AND cp_proveedor.IdProveedor = cp_orden_giro.IdProveedor INNER JOIN
										tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona ON cp_retencion.IdEmpresa_Ogiro = cp_orden_giro.IdEmpresa AND cp_retencion.IdCbteCble_Ogiro = cp_orden_giro.IdCbteCble_Ogiro AND 
										cp_retencion.IdTipoCbte_Ogiro = cp_orden_giro.IdTipoCbte_Ogiro
										where cp_orden_giro.IdEmpresa = @IdEmpresa and cp_orden_giro.IdProveedor between @IdProveedorIni and @IdProveedorFin and cp_retencion.fecha < @FechaIni and cp_orden_giro.Estado = 'A'
										GROUP BY cp_retencion.IdEmpresa, cp_retencion.IdRetencion
								 ) AS det on cp_retencion.IdEmpresa = det.IdEmpresa and det.IdRetencion = cp_retencion.IdRetencion						 
		where cp_orden_giro.IdEmpresa = @IdEmpresa and cp_orden_giro.IdProveedor between @IdProveedorIni and @IdProveedorFin and cp_retencion.fecha < @FechaIni and cp_orden_giro.Estado = 'A' and det.re_valor_retencion > 0
		UNION ALL
		SELECT        cp_orden_pago.IdEmpresa, cp_orden_pago.IdEntidad, tb_persona.pe_nombreCompleto, ba_Cbte_Ban.cb_Fecha, ba_Cbte_Ban.Estado, ct_cbtecble_tipo.tc_TipoCbte, ISNULL(ba_Cbte_Ban.cb_Cheque,'INT-'+ cast(ba_Cbte_Ban.IdCbteCble as varchar(20))), ba_Cbte_Ban.cb_Observacion, 
								  cp_orden_pago_cancelaciones.MontoAplicado as Debito, 0 as Credito, cp_orden_pago_cancelaciones.MontoAplicado *-1 as Valor, cp_orden_pago_cancelaciones.Secuencia
		FROM            cp_proveedor INNER JOIN
								 tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona INNER JOIN
								 cp_orden_pago_cancelaciones INNER JOIN
								 cp_orden_pago_det ON cp_orden_pago_cancelaciones.IdEmpresa_op = cp_orden_pago_det.IdEmpresa AND cp_orden_pago_cancelaciones.IdOrdenPago_op = cp_orden_pago_det.IdOrdenPago AND 
								 cp_orden_pago_cancelaciones.Secuencia_op = cp_orden_pago_det.Secuencia INNER JOIN
								 cp_orden_pago ON cp_orden_pago_det.IdEmpresa = cp_orden_pago.IdEmpresa AND cp_orden_pago_det.IdOrdenPago = cp_orden_pago.IdOrdenPago ON cp_proveedor.IdEmpresa = cp_orden_pago.IdEmpresa AND 
								 cp_proveedor.IdProveedor = cp_orden_pago.IdEntidad INNER JOIN
								 ba_Cbte_Ban ON cp_orden_pago_cancelaciones.IdEmpresa_pago = ba_Cbte_Ban.IdEmpresa AND cp_orden_pago_cancelaciones.IdCbteCble_pago = ba_Cbte_Ban.IdCbteCble AND 
								 cp_orden_pago_cancelaciones.IdTipoCbte_pago = ba_Cbte_Ban.IdTipocbte INNER JOIN
								 ct_cbtecble_tipo ON ba_Cbte_Ban.IdEmpresa = ct_cbtecble_tipo.IdEmpresa AND ba_Cbte_Ban.IdTipocbte = ct_cbtecble_tipo.IdTipoCbte
		WHERE        (cp_orden_pago.IdTipo_Persona = 'PROVEE') AND cp_orden_pago.IdEmpresa = @IdEmpresa and cp_orden_pago.IdEntidad between @IdProveedorIni and @IdProveedorFin and ba_Cbte_Ban.cb_Fecha < @FechaIni and ba_Cbte_Ban.Estado = 'A'
) A group by a.IdEmpresa, a.IdProveedor, a.pe_nombreCompleto
UNION ALL

SELECT        cp_orden_giro.IdEmpresa, cp_orden_giro.IdProveedor, tb_persona.pe_nombreCompleto, cp_orden_giro.co_fechaOg, cp_orden_giro.Estado, cp_TipoDocumento.Descripcion Tipo, 
                         cp_orden_giro.co_serie + '-' + cp_orden_giro.co_factura AS Referencia, cp_orden_giro.co_observacion, 0 AS Debito, cp_orden_giro.co_total AS Credito, 						 
						 case when cp_orden_giro.Estado = 'A' THEN cp_orden_giro.co_total ELSE 0 END AS Valor, 0 AS Secuencia
FROM            cp_orden_giro INNER JOIN
                         cp_proveedor ON cp_orden_giro.IdEmpresa = cp_proveedor.IdEmpresa AND cp_orden_giro.IdProveedor = cp_proveedor.IdProveedor INNER JOIN
                         tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona INNER JOIN
                         cp_TipoDocumento ON cp_orden_giro.IdOrden_giro_Tipo = cp_TipoDocumento.CodTipoDocumento
where cp_orden_giro.IdEmpresa = @IdEmpresa and cp_orden_giro.IdProveedor between @IdProveedorIni and @IdProveedorFin and cp_orden_giro.co_fechaOg between @FechaIni and @FechaFin and cp_orden_giro.Estado like '%'+case when @MostrarAnulados = 1 then '' else 'A' end+'%'
UNION ALL
SELECT        cp_nota_DebCre.IdEmpresa, cp_nota_DebCre.IdProveedor, tb_persona.pe_nombreCompleto, cp_nota_DebCre.cn_fecha, cp_nota_DebCre.Estado, 
                         CASE WHEN cp_nota_DebCre.DebCre = 'C' THEN 'Nota de crédito' ELSE 'Nota de débito' END AS Tipo, 
                         CASE WHEN cp_nota_DebCre.IdTipoNota = 'T_TIP_NOTA_INT' THEN 'INT-' + CAST(cp_nota_DebCre.IdCbteCble_Nota AS VARCHAR(20)) 
                         ELSE cp_nota_DebCre.cn_serie1 + '-' + cp_nota_DebCre.cn_serie2 + '-' + cp_nota_DebCre.cn_Nota END AS referencia, cp_nota_DebCre.cn_observacion, 
                         CASE WHEN cp_nota_DebCre.DebCre = 'C' THEN cp_nota_DebCre.cn_total ELSE 0 END AS Debito, CASE WHEN cp_nota_DebCre.DebCre = 'D' THEN cp_nota_DebCre.cn_total ELSE 0 END AS Credito, 
                         CASE WHEN cp_nota_DebCre.DebCre = 'D' THEN cp_nota_DebCre.cn_total ELSE cp_nota_DebCre.cn_total * - 1 END AS Valor, 0 AS Secuencia
FROM            cp_proveedor INNER JOIN
                         cp_nota_DebCre ON cp_proveedor.IdEmpresa = cp_nota_DebCre.IdEmpresa AND cp_proveedor.IdProveedor = cp_nota_DebCre.IdProveedor INNER JOIN
                         tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona
where cp_nota_DebCre.IdEmpresa = @IdEmpresa and cp_nota_DebCre.IdProveedor between @IdProveedorIni and @IdProveedorFin and cp_nota_DebCre.cn_fecha between @FechaIni and @FechaFin and cp_nota_DebCre.Estado like '%'+case when @MostrarAnulados = 1 then '' else 'A' end+'%'
UNION ALL
SELECT        cp_orden_giro.IdEmpresa, cp_orden_giro.IdProveedor, tb_persona.pe_nombreCompleto, cp_retencion.fecha, cp_retencion.Estado, 'Retención' AS Tipo, cp_retencion.serie1+'-'+cp_retencion.serie2+'-'+ cp_retencion.NumRetencion, cp_retencion.observacion,
		det.re_valor_retencion as Debito, 0 as Credito, det.re_valor_retencion *-1 as Valor, 2
FROM            cp_proveedor INNER JOIN
                         cp_orden_giro ON cp_proveedor.IdEmpresa = cp_orden_giro.IdEmpresa AND cp_proveedor.IdProveedor = cp_orden_giro.IdProveedor INNER JOIN
                         tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona RIGHT OUTER JOIN
                         cp_retencion ON cp_orden_giro.IdEmpresa = cp_retencion.IdEmpresa_Ogiro AND cp_orden_giro.IdCbteCble_Ogiro = cp_retencion.IdCbteCble_Ogiro AND cp_orden_giro.IdTipoCbte_Ogiro = cp_retencion.IdTipoCbte_Ogiro
						 LEFT OUTER JOIN(
								SELECT        cp_retencion.IdEmpresa, cp_retencion.IdRetencion, SUM(cp_retencion_det.re_valor_retencion) AS re_valor_retencion
								FROM            cp_retencion_det INNER JOIN
								cp_retencion ON cp_retencion_det.IdEmpresa = cp_retencion.IdEmpresa AND cp_retencion_det.IdRetencion = cp_retencion.IdRetencion LEFT OUTER JOIN
								cp_proveedor INNER JOIN
								cp_orden_giro ON cp_proveedor.IdEmpresa = cp_orden_giro.IdEmpresa AND cp_proveedor.IdProveedor = cp_orden_giro.IdProveedor INNER JOIN
								tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona ON cp_retencion.IdEmpresa_Ogiro = cp_orden_giro.IdEmpresa AND cp_retencion.IdCbteCble_Ogiro = cp_orden_giro.IdCbteCble_Ogiro AND 
								cp_retencion.IdTipoCbte_Ogiro = cp_orden_giro.IdTipoCbte_Ogiro
								where cp_orden_giro.IdEmpresa = @IdEmpresa and cp_orden_giro.IdProveedor between @IdProveedorIni and @IdProveedorFin and cp_retencion.fecha between @FechaIni and @FechaFin and cp_orden_giro.Estado like '%'+case when @MostrarAnulados = 1 then '' else 'A' end+'%'
								GROUP BY cp_retencion.IdEmpresa, cp_retencion.IdRetencion
						 ) AS det on cp_retencion.IdEmpresa = det.IdEmpresa and det.IdRetencion = cp_retencion.IdRetencion						 
where cp_orden_giro.IdEmpresa = @IdEmpresa and cp_orden_giro.IdProveedor between @IdProveedorIni and @IdProveedorFin and cp_retencion.fecha between @FechaIni and @FechaFin and cp_orden_giro.Estado like '%'+case when @MostrarAnulados = 1 then '' else 'A' end+'%' and det.re_valor_retencion > 0
UNION ALL
SELECT        cp_orden_pago.IdEmpresa, cp_orden_pago.IdEntidad, tb_persona.pe_nombreCompleto, ba_Cbte_Ban.cb_Fecha, ba_Cbte_Ban.Estado, ct_cbtecble_tipo.tc_TipoCbte, ISNULL(ba_Cbte_Ban.cb_Cheque,'INT-'+ cast(ba_Cbte_Ban.IdCbteCble as varchar(20))), ba_Cbte_Ban.cb_Observacion, 
                          cp_orden_pago_cancelaciones.MontoAplicado as Debito, 0 as Credito, cp_orden_pago_cancelaciones.MontoAplicado *-1 as Valor, cp_orden_pago_cancelaciones.Secuencia
FROM            cp_proveedor INNER JOIN
                         tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona INNER JOIN
                         cp_orden_pago_cancelaciones INNER JOIN
                         cp_orden_pago_det ON cp_orden_pago_cancelaciones.IdEmpresa_op = cp_orden_pago_det.IdEmpresa AND cp_orden_pago_cancelaciones.IdOrdenPago_op = cp_orden_pago_det.IdOrdenPago AND 
                         cp_orden_pago_cancelaciones.Secuencia_op = cp_orden_pago_det.Secuencia INNER JOIN
                         cp_orden_pago ON cp_orden_pago_det.IdEmpresa = cp_orden_pago.IdEmpresa AND cp_orden_pago_det.IdOrdenPago = cp_orden_pago.IdOrdenPago ON cp_proveedor.IdEmpresa = cp_orden_pago.IdEmpresa AND 
                         cp_proveedor.IdProveedor = cp_orden_pago.IdEntidad INNER JOIN
                         ba_Cbte_Ban ON cp_orden_pago_cancelaciones.IdEmpresa_pago = ba_Cbte_Ban.IdEmpresa AND cp_orden_pago_cancelaciones.IdCbteCble_pago = ba_Cbte_Ban.IdCbteCble AND 
                         cp_orden_pago_cancelaciones.IdTipoCbte_pago = ba_Cbte_Ban.IdTipocbte INNER JOIN
                         ct_cbtecble_tipo ON ba_Cbte_Ban.IdEmpresa = ct_cbtecble_tipo.IdEmpresa AND ba_Cbte_Ban.IdTipocbte = ct_cbtecble_tipo.IdTipoCbte
WHERE        (cp_orden_pago.IdTipo_Persona = 'PROVEE') AND cp_orden_pago.IdEmpresa = @IdEmpresa and cp_orden_pago.IdEntidad between @IdProveedorIni and @IdProveedorFin and ba_Cbte_Ban.cb_Fecha between @FechaIni and @FechaFin and ba_Cbte_Ban.Estado like '%'+case when @MostrarAnulados = 1 then '' else 'A' end+'%'
) REP ORDER BY Rep.IdEmpresa, Rep.IdProveedor, Rep.Fecha,Rep.Secuencia, Rep.Referencia, Rep.Debito, REP.Credito