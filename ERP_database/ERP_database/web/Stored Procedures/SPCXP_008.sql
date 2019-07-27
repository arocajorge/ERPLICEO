--exec [web].[SPCXP_008] 2,'3/10/2019',2,2,1,9999999
CREATE PROCEDURE [web].[SPCXP_008]
  @idempresa int ,
  @fecha datetime ,
  @IdSucursalIni int,
  @IdSucursalFin int,
  @idProveedorIni decimal,
  @idProveedorFIn decimal
AS
BEGIN


         
SELECT ISNULL(ROW_NUMBER() OVER(ORDER BY VW.IdEmpresa),0) as IdRow, 
* 
from(         
Select 
        A.IdEmpresa, A.IdCbteCble_Ogiro, A.IdTipoCbte_Ogiro, A.IdOrden_giro_Tipo, A.Documento, A.nom_tipo_doc, A.cod_tipo_doc,
        A.IdProveedor, A.nom_proveedor, A.Valor_a_pagar
        
        , ISNULL(b.MontoAplicado, 0) AS MontoAplicado, round((round(A.Valor_a_pagar,2) - round(ISNULL(b.MontoAplicado, 0),2)),2)  as Saldo
        , A.Observacion,
         A.Ruc_Proveedor, A.representante_legal, A.Tipo_cbte, A.co_plazo Plazo_fact, 
          A.co_fechaOg,  co_FechaFactura_vct, A.Dias_Vcto,@fecha as Fecha_corte,
        (Case when datediff(day,co_FechaFactura_vct,@fecha) <=0 then (A.Valor_a_pagar - ISNULL(b.MontoAplicado, 0)) else 0 end) x_Vencer,
		(Case when datediff(day,co_FechaFactura_vct,@fecha) >0 then (A.Valor_a_pagar - ISNULL(b.MontoAplicado, 0)) else 0 end) Vencido,
        (Case when datediff(day,co_FechaFactura_vct,@fecha) >=1  and datediff(day,co_FechaFactura_vct,@fecha) <=30 then (A.Valor_a_pagar - ISNULL(b.MontoAplicado, 0)) else 0 end) Vencido_1_30,
        (Case when datediff(day,co_FechaFactura_vct,@fecha) >=31  and datediff(day,co_FechaFactura_vct,@fecha) <=60 then (A.Valor_a_pagar - ISNULL(b.MontoAplicado, 0)) else 0 end) Vencido_31_60,
		(Case when datediff(day,co_FechaFactura_vct,@fecha) >=61  and datediff(day,co_FechaFactura_vct,@fecha) <=90 then (A.Valor_a_pagar - ISNULL(b.MontoAplicado, 0)) else 0 end) Vencido_60_90,
        (Case when datediff(day,co_FechaFactura_vct,@fecha) >=91    then (A.Valor_a_pagar - ISNULL(b.MontoAplicado, 0)) else 0 end) Vencido_mayor_90, isnull(a.en_conciliacion,0) as en_conciliacion, Su_Descripcion
 from
 (
 
			select og.IdEmpresa,og.IdCbteCble_Ogiro,og.IdTipoCbte_Ogiro,og.IdOrden_giro_Tipo,og.co_factura Documento, td.Descripcion as nom_tipo_doc,td.Codigo as cod_tipo_doc, og.IdProveedor, per.pe_nombreCompleto as nom_proveedor,
			og.co_total as Valor_a_pagar, og.co_observacion Observacion,per.pe_cedulaRuc Ruc_Proveedor, per.pe_nombreCompleto as representante_legal, 'CBTE_CXP' as Tipo_cbte, og.co_plazo, co_fechaOg, co_FechaFactura_vct,
			case when conci.IdEmpresa_OGiro is null then cast(0 as bit) else cast(1 as bit) end as en_conciliacion,
			DATEDIFF(DAY,og.co_FechaFactura_vct,@fecha) as Dias_Vcto, Su_Descripcion
			from cp_orden_giro as og inner join cp_TipoDocumento as td on og.IdOrden_giro_Tipo = td.CodTipoDocumento inner join cp_proveedor AS pro on pro.IdEmpresa = og.IdEmpresa
			and pro.IdProveedor = og.IdProveedor inner join tb_persona as per on per.IdPersona = pro.IdPersona inner join tb_sucursal as su on og.IdEmpresa = su.IdEmpresa and og.IdSucursal = su.IdSucursal
			left join (
					select cp_conciliacion_Caja_det.IdEmpresa_OGiro,cp_conciliacion_Caja_det.IdTipoCbte_Ogiro,cp_conciliacion_Caja_det.IdCbteCble_Ogiro 
					from cp_conciliacion_Caja_det 
					group by cp_conciliacion_Caja_det.IdEmpresa_OGiro,cp_conciliacion_Caja_det.IdTipoCbte_Ogiro,cp_conciliacion_Caja_det.IdCbteCble_Ogiro 
					) conci on og.IdEmpresa = conci.IdEmpresa_OGiro and og.IdTipoCbte_Ogiro = conci.IdTipoCbte_Ogiro and og.IdCbteCble_Ogiro = conci.IdCbteCble_Ogiro
			WHERE og.Estado = 'A'
			AND OG.IdEmpresa = @idempresa AND OG.IdSucursal BETWEEN @IdSucursalIni AND @IdSucursalFin AND OG.co_FechaFactura <= @fecha
			AND OG.IDPROVEEDOR BETWEEN @idProveedorIni AND @idProveedorFIn
			union all
			select og.IdEmpresa,og.IdCbteCble_nota,og.IdTipoCbte_Nota,'05',og.cod_nota, 'Nota de débito' as nom_tipo_doc,'ND' as cod_tipo_doc, og.IdProveedor, per.pe_nombreCompleto as nom_proveedor,
			og.cn_total as Valor_a_pagar, og.cN_observacion Observacion,per.pe_cedulaRuc Ruc_Proveedor, per.pe_nombreCompleto as representante_legal, 'CBTE_CXP' as Tipo_cbte, 0, cn_fecha, cn_Fecha_vcto,0,
			DATEDIFF(DAY,og.cn_Fecha_vcto,@fecha) as Dias_Vcto, Su_Descripcion
			from cp_nota_DebCre as og inner join cp_proveedor AS pro on pro.IdEmpresa = og.IdEmpresa
			and pro.IdProveedor = og.IdProveedor inner join tb_persona as per on per.IdPersona = pro.IdPersona inner join tb_sucursal as su on og.IdEmpresa = su.IdEmpresa and og.IdSucursal = su.IdSucursal
			where og.DebCre = 'D' AND Og.Estado = 'A'
			AND OG.IdEmpresa = @idempresa AND OG.IdSucursal BETWEEN @IdSucursalIni AND @IdSucursalFin AND OG.cn_fecha <= @fecha
			AND OG.IDPROVEEDOR BETWEEN @idProveedorIni AND @idProveedorFIn
			UNION ALL
			select og.IdEmpresa,CAST( d.IdCbteCble_cxp AS INT), CAST(d.IdTipoCbte_cxp AS DECIMAL),'00',cast(og.IdOrdenPago as varchar(20)), 'Orden de pago' as nom_tipo_doc,'OP' as cod_tipo_doc, og.IdEntidad, per.pe_nombreCompleto as nom_proveedor,
			d.Valor_a_pagar as Valor_a_pagar, og.Observacion Observacion,per.pe_cedulaRuc Ruc_Proveedor, per.pe_nombreCompleto as representante_legal, 'CBTE_CXP' as Tipo_cbte, 0, OG.Fecha, OG.Fecha,0,
			DATEDIFF(DAY,og.Fecha,@fecha) as Dias_Vcto, Su_Descripcion
			from cp_orden_pago as og inner join cp_proveedor AS pro on pro.IdEmpresa = og.IdEmpresa
			and pro.IdProveedor = og.IdEntidad inner join tb_persona as per on per.IdPersona = pro.IdPersona
			INNER JOIN cp_orden_pago_det as d on og.IdEmpresa = d.IdEmpresa
			and og.IdOrdenPago = d.IdOrdenPago inner join tb_sucursal as su on og.IdEmpresa = su.IdEmpresa and og.IdSucursal = su.IdSucursal
			WHERE OG.IdTipo_Persona = 'PROVEE' AND OG.IdTipo_op <> 'FACT_PROVEE' AND OG.Estado = 'A'
			AND OG.IdEmpresa = @idempresa AND OG.IdSucursal BETWEEN @IdSucursalIni AND @IdSucursalFin AND OG.Fecha <= @fecha
			AND OG.IdEntidad BETWEEN @idProveedorIni AND @idProveedorFIn
) as A
 left join (
 Select IdEmpresa_cxp,IdTipoCbte_cxp ,IdCbteCble_cxp,sum(MontoAplicado)MontoAplicado
 from 
 ( 
		SELECT cp_orden_pago_cancelaciones.IdEmpresa_cxp, 
		cp_orden_pago_cancelaciones.IdTipoCbte_cxp, 
		cp_orden_pago_cancelaciones.IdCbteCble_cxp, 
		cp_orden_pago_cancelaciones.MontoAplicado
FROM     cp_orden_pago_cancelaciones INNER JOIN
                  ct_cbtecble ON cp_orden_pago_cancelaciones.IdEmpresa_pago = ct_cbtecble.IdEmpresa AND cp_orden_pago_cancelaciones.IdTipoCbte_pago = ct_cbtecble.IdTipoCbte AND 
                  cp_orden_pago_cancelaciones.IdCbteCble_pago = ct_cbtecble.IdCbteCble
			where ct_cbtecble.cb_Fecha <= @fecha
UNION ALL
		SELECT cp_orden_giro.IdEmpresa, 
				cp_orden_giro.IdTipoCbte_Ogiro, 
				cp_orden_giro.IdCbteCble_Ogiro, 
				cp_retencion_det.re_valor_retencion
		FROM     cp_orden_giro INNER JOIN
						  cp_retencion ON cp_orden_giro.IdEmpresa = cp_retencion.IdEmpresa_Ogiro AND cp_orden_giro.IdCbteCble_Ogiro = cp_retencion.IdCbteCble_Ogiro AND cp_orden_giro.IdTipoCbte_Ogiro = cp_retencion.IdTipoCbte_Ogiro INNER JOIN
						  cp_retencion_det ON cp_retencion.IdEmpresa = cp_retencion_det.IdEmpresa AND cp_retencion.IdRetencion = cp_retencion_det.IdRetencion
		WHERE cp_retencion.fecha <= @fecha
) as VISTA 
group by IdEmpresa_cxp,IdTipoCbte_cxp ,IdCbteCble_cxp ) as 
b on
 a.IdEmpresa = b.IdEmpresa_cxp and a.IdCbteCble_Ogiro = b.IdCbteCble_cxp and a.IdTipoCbte_Ogiro = b.IdTipoCbte_cxp 
 ) VW
 where vw.Saldo > 0
END