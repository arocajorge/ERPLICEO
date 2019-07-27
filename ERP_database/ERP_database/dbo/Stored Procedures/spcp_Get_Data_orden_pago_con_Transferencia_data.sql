-- exec spcp_Get_Data_orden_pago_con_Transferencia_data  1,0,'28/02/2010','30/09/2017', 'admin'

CREATE proc [dbo].[spcp_Get_Data_orden_pago_con_Transferencia_data] 
(
 @i_idempresa int
,@i_IdArchivo numeric(18,0)
,@i_fecha_ini date
,@i_fecha_fin date
,@i_IdUsuario varchar(20)
)
as
begin

delete cp_orden_pago_con_Transferencia_data where IdUsuario = @i_IdUsuario

INSERT INTO cp_orden_pago_con_Transferencia_data 
(			
 IdEmpresa			, IdTipo_op					, Referencia				, Referencia2			, IdOrdenPago
, Secuencia_OP			, IdTipoPersona		, IdPersona					, IdEntidad					, Fecha_OP				
, Observacion			, Valor_a_pagar		, Valor_estimado_a_pagar_OP	, Total_cancelado_OP		, Saldo_x_Pagar_OP		
, IdEstadoAprobacion	, IdFormaPago		, Fecha_Pago				, Cbte_cxp					, Estado				
, IdEmpresa_cxp			, IdTipoCbte_cxp	, IdCbteCble_cxp	
, IdUsuario				,Checked			, IdEmpresa_pago			,IdTipoCbte_pago			, IdCbteCble_pago
, tipo_cbte_pago		,Secuencial_reg_x_proceso,		[Secuencia_ar]				
)

SELECT        
OP_det.IdEmpresa			, OP.IdTipo_op				, NULL AS Referencia		, NULL AS Referencia2		, OP_det.IdOrdenPago
, OP_det.Secuencia			,OP.IdTipo_Persona			, OP.IdPersona				, OP.IdEntidad				, OP.Fecha 
, OP.Observacion			,OP_det.Valor_a_pagar	, OP_det.Valor_a_pagar , isnull(SUM(OP_can.MontoAplicado),0) , SUM(OP_det.Valor_a_pagar) - isnull(SUM(OP_can.MontoAplicado),0 )
, OP_det.IdEstadoAprobacion	, OP_det.IdFormaPago		,OP_det.Fecha_Pago		, OP_det.IdCbteCble_cxp		, OP.Estado
, OP_det.IdEmpresa_cxp		, OP_det.IdTipoCbte_cxp		,OP_det.IdCbteCble_cxp
, @i_IdUsuario,
 1 AS Checked				
 , OP_can.IdEmpresa_pago		, OP_can.IdTipoCbte_pago	, OP_can.IdCbteCble_pago	, ct_cbtecble_tipo.tc_TipoCbte
 , arc_tr.Secuencial_reg_x_proceso,				arc_tr.Secuencia
FROM            ct_cbtecble_tipo RIGHT OUTER JOIN
                         cp_orden_pago_cancelaciones AS OP_can ON ct_cbtecble_tipo.IdEmpresa = OP_can.IdEmpresa_pago AND ct_cbtecble_tipo.IdTipoCbte = OP_can.IdTipoCbte_pago RIGHT OUTER JOIN
                         cp_orden_pago AS OP INNER JOIN
                         cp_orden_pago_det AS OP_det ON OP.IdEmpresa = OP_det.IdEmpresa AND OP.IdOrdenPago = OP_det.IdOrdenPago INNER JOIN
                         ba_Archivo_Transferencia_Det AS arc_tr ON OP_det.IdEmpresa = arc_tr.IdEmpresa_OP AND OP_det.IdOrdenPago = arc_tr.IdOrdenPago AND OP_det.Secuencia = arc_tr.Secuencia_OP ON 
                         OP_can.IdEmpresa_op = OP_det.IdEmpresa AND OP_can.IdOrdenPago_op = OP_det.IdOrdenPago AND OP_can.Secuencia_op = OP_det.Secuencia
WHERE       op.IdEmpresa = @i_idempresa and (OP.Estado = 'A') AND (OP_det.IdEstadoAprobacion = 'APRO') AND (arc_tr.IdArchivo = @i_IdArchivo)
and (exists(
select * from ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo tipo
where tipo.CodTipoCbteBan = 'NDBA'
and OP_can.IdEmpresa_pago = tipo.IdEmpresa
and OP_can.IdTipoCbte_pago = tipo.IdTipoCbteCble
) or  OP_can.IdEmpresa_pago is null
)
--and (OP_can.Observacion like '%'+arc_tr.IdProceso_bancario+'%' OR OP_can.Observacion IS NULL)
GROUP BY OP_det.IdEmpresa, OP.IdTipo_op, OP_det.IdOrdenPago, OP_det.Secuencia, OP.IdTipo_Persona, OP.IdPersona, OP.IdEntidad, OP.Fecha, OP.Observacion,OP_det.Valor_a_pagar, OP_det.IdEstadoAprobacion, OP_det.IdFormaPago, 
                         OP_det.Fecha_Pago, OP_det.IdCbteCble_cxp, OP.Estado, OP_det.IdEmpresa_cxp, OP_det.IdTipoCbte_cxp,arc_tr.IdArchivo,OP_can.IdEmpresa_pago
						, OP_can.IdTipoCbte_pago, OP_can.IdCbteCble_pago, ct_cbtecble_tipo.tc_TipoCbte,arc_tr.Secuencial_reg_x_proceso, arc_tr.Secuencia
having OP_det.IdEmpresa=@i_idempresa 
--and OP.Fecha between @i_fecha_ini and @i_fecha_fin
--and round(SUM(OP_det.Valor_a_pagar) - isnull(SUM(OP_can.MontoAplicado),0),2)>0
union
SELECT        
OP_det.IdEmpresa			, OP.IdTipo_op				, NULL AS Referencia		, NULL AS Referencia2		, OP_det.IdOrdenPago
, OP_det.Secuencia			,OP.IdTipo_Persona			, OP.IdPersona				, OP.IdEntidad				, OP.Fecha 
, OP.Observacion			,OP_det.Valor_a_pagar	, OP_det.Valor_a_pagar , isnull(SUM(OP_can.MontoAplicado),0) , SUM(OP_det.Valor_a_pagar) - isnull(SUM(OP_can.MontoAplicado),0 )

, OP_det.IdEstadoAprobacion	, OP_det.IdFormaPago		,OP_det.Fecha_Pago		, OP_det.IdCbteCble_cxp		, OP.Estado
, OP_det.IdEmpresa_cxp		, OP_det.IdTipoCbte_cxp		,OP_det.IdCbteCble_cxp

, @i_IdUsuario,
0 AS Checked				, NULL						, NULL					, NULL						, NULL
,NULL						,0
FROM            cp_orden_pago AS OP INNER JOIN
                         cp_orden_pago_det AS OP_det ON OP.IdEmpresa = OP_det.IdEmpresa AND OP.IdOrdenPago = OP_det.IdOrdenPago LEFT OUTER JOIN
                         cp_orden_pago_cancelaciones AS OP_can ON OP_det.IdEmpresa = OP_can.IdEmpresa_op AND OP_det.IdOrdenPago = OP_can.IdOrdenPago_op AND OP_det.Secuencia = OP_can.Secuencia_op
WHERE        (OP.IdEmpresa = @i_idempresa) AND (OP.Estado = 'A') AND (OP_det.IdEstadoAprobacion = 'APRO')
and not exists(
select ar_det.IdEmpresa 
from ba_Archivo_Transferencia_Det ar_det inner join ba_Archivo_Transferencia cab
on cab.IdEmpresa = ar_det.IdEmpresa and cab.IdArchivo = ar_det.IdArchivo
where ar_det.IdEmpresa_OP =  OP_det.IdEmpresa
and ar_det.IdOrdenPago = op_det.IdOrdenPago
and ar_det.Secuencia_OP = OP_det.Secuencia
and cab.Estado = 1

)
GROUP BY OP_det.IdEmpresa, OP.IdTipo_op, OP_det.IdOrdenPago, OP_det.Secuencia, OP.IdTipo_Persona, OP.IdPersona, OP.IdEntidad, OP.Fecha, OP.Observacion,OP_det.Valor_a_pagar, OP_det.IdEstadoAprobacion, OP_det.IdFormaPago, 
                         OP_det.Fecha_Pago, OP_det.IdCbteCble_cxp, OP.Estado, OP_det.IdEmpresa_cxp, OP_det.IdTipoCbte_cxp
having OP_det.IdEmpresa=@i_idempresa 
and OP.Fecha between @i_fecha_ini and @i_fecha_fin
and round(OP_det.Valor_a_pagar - isnull(SUM(OP_can.MontoAplicado),0),2)>0


update cp_orden_pago_con_Transferencia_data 
set 
 Nom_Beneficiario=ben.pe_apellido+' '+ben.pe_nombre
,Girar_Cheque_a=ben.pr_girar_cheque_a
,IdCtaCble=ben.IdCtaCble
,Nom_Beneficiario_2=ben.Nombre
,IdTipoCta_acreditacion_cat=ben.IdTipoCta_acreditacion_cat
,num_cta_acreditacion=ben.num_cta_acreditacion
,IdBanco_acreditacion=ben.IdBanco_acreditacion
,ba_descripcion=ban.ba_descripcion
,CodigoLegal=ban.CodigoLegal
FROM            cp_orden_pago_con_Transferencia_data  AS data INNER JOIN
                         vwtb_persona_beneficiario AS ben ON data.IdEmpresa = ben.IdEmpresa AND data.IdTipoPersona = ben.IdTipo_Persona AND data.IdPersona = ben.IdPersona AND data.IdEntidad = ben.IdEntidad LEFT OUTER JOIN
                         tb_banco AS ban ON ben.IdBanco_acreditacion = ban.IdBanco

update cp_orden_pago_con_Transferencia_data 
set Referencia=doc.Codigo+'#' + CAST( CAST( OG.co_factura AS NUMERIC)  AS VARCHAR(20))
,Referencia2=doc.Codigo+'#' + CAST( CAST(OG.co_factura AS NUMERIC) AS VARCHAR(20))
,Fecha_Fa_Prov=OG.co_FechaFactura
,Fecha_Venc_Fac_Prov=OG.co_FechaFactura_vct
FROM            cp_orden_pago_con_Transferencia_data  AS data INNER JOIN
                         cp_orden_giro AS OG ON data.IdEmpresa_cxp = OG.IdEmpresa AND data.IdTipoCbte_cxp = OG.IdTipoCbte_Ogiro AND data.IdCbteCble_cxp = OG.IdCbteCble_Ogiro INNER JOIN
                         cp_TipoDocumento AS doc ON OG.IdOrden_giro_Tipo = doc.CodTipoDocumento
--, , , ND.cn_Nota, 

update cp_orden_pago_con_Transferencia_data  
set Referencia=
CASE WHEN ND.cn_Nota is NULL  or ND.cn_Nota='' THEN  +'ND#:' + cast(ND.IdCbteCble_Nota as varchar(20)) 
else 'ND#' + ND.cn_Nota
END 
,Referencia2=CASE WHEN ND.cn_Nota is NULL  or ND.cn_Nota='' THEN  +'ND#:' + cast(ND.IdCbteCble_Nota as varchar(20)) 
else 'ND#' + ND.cn_Nota
END 

,Fecha_Fa_Prov=ND.cn_fecha
,Fecha_Venc_Fac_Prov=ND.cn_Fecha_vcto

FROM            cp_orden_pago_con_Transferencia_data  AS data INNER JOIN
                         cp_nota_DebCre AS ND ON data.IdEmpresa_cxp = ND.IdEmpresa AND data.IdCbteCble_cxp = ND.IdCbteCble_Nota AND data.IdTipoCbte_cxp = ND.IdTipoCbte_Nota


update cp_orden_pago_con_Transferencia_data  
set Referencia='OP#' + cast(cp_orden_pago_con_Transferencia_data .IdOrdenPago as varchar(20))
,Referencia2='OP#' + cast(cp_orden_pago_con_Transferencia_data .IdOrdenPago as varchar(20))
,fecha_fa_prov=fecha_op
,fecha_venc_fac_prov=fecha_op
WHERE Referencia is null


	SELECT        isnull(ROW_NUMBER() OVER(ORDER BY IdEmpresa),0) AS IdRow
							 ,IdEmpresa, IdTipo_op, Referencia, Referencia2, IdOrdenPago, Secuencia_OP, IdTipoPersona, IdPersona, IdEntidad, Fecha_OP, Fecha_Fa_Prov, Fecha_Venc_Fac_Prov, Observacion, Nom_Beneficiario, 
							 Girar_Cheque_a, Valor_a_pagar, Valor_estimado_a_pagar_OP, Total_cancelado_OP, Saldo_x_Pagar_OP, IdEstadoAprobacion, IdFormaPago, Fecha_Pago, IdCtaCble, IdCentroCosto, IdSubCentro_Costo, Cbte_cxp, 
							 Estado, Nom_Beneficiario_2, IdEmpresa_cxp, IdTipoCbte_cxp, IdCbteCble_cxp,  IdTipoCta_acreditacion_cat, num_cta_acreditacion, IdBanco_acreditacion, ba_descripcion, 
							 CodigoLegal, IdUsuario, Checked, IdEmpresa_pago, IdTipoCbte_pago, IdCbteCble_pago, tipo_cbte_pago, Secuencial_reg_x_proceso,Secuencia_ar
	FROM            cp_orden_pago_con_Transferencia_data 
	WHERE        IdUsuario = @i_IdUsuario

end