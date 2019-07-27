
--exec [dbo].[spcp_Get_Data_orden_pago_con_cancelacion_data] 1,1,999999,'PROVEE',1,999999,'APRO','admin',1,0
CREATE PROCEDURE [dbo].[spcp_Get_Data_orden_pago_con_cancelacion_data]
(
@IdEmpresa int,
@IdPersona_ini numeric,
@IdPersona_fin numeric,
@IdTipoPersona varchar(10),
@IdEntidad_ini numeric,
@IdEntidad_fin numeric,
@IdEstado_Aprobacion varchar(10),
@IdUsuario varchar(20),
@IdSucursal int,
@mostrar_saldo_0 bit,
@ValidarCuentaBancaria bit
)
AS
BEGIN
delete cp_orden_pago_con_cancelacion_data where IdUsuario = @IdUsuario

INSERT INTO [dbo].[cp_orden_pago_con_cancelacion_data]
           ([IdUsuario]					           ,[IdEmpresa]					           ,[IdTipo_op]						       ,[Referencia]			           ,[Referencia2]
           ,[IdOrdenPago]				           ,[Secuencia_OP]				           ,[IdTipoPersona]						   ,[IdPersona]				           ,[IdEntidad]
           ,[Fecha_OP]					           ,[Fecha_Fa_Prov]				           ,[Fecha_Venc_Fac_Prov]			       ,[Observacion]			           ,[Nom_Beneficiario]
           ,[Girar_Cheque_a]			           ,[Valor_a_pagar]				           ,[Valor_estimado_a_pagar_OP]	           ,[Total_cancelado_OP]	           ,[Saldo_x_Pagar_OP]
           ,[IdEstadoAprobacion]		           ,[IdFormaPago]				           ,[Fecha_Pago]				           ,[IdCtaCble]				           ,[IdCentroCosto]
           ,[IdSubCentro_Costo]			           ,[Cbte_cxp]					           ,[Estado]					           ,[Nom_Beneficiario_2]	           ,[IdEmpresa_cxp]
           ,[IdTipoCbte_cxp]			           ,[IdCbteCble_cxp]			           ,[IdBanco])
select		@IdUsuario, cp_orden_pago.IdEmpresa, cp_orden_pago.IdTipo_op, CASE WHEN cp_orden_pago.IdTipo_op = 'FACT_PROVEE' THEN cp_orden_pago_det.Referencia ELSE cp_orden_pago.Observacion END AS Expr1, 'OP#'+cast(cp_orden_pago.IdOrdenPago as varchar(20)) AS Expr2, cp_orden_pago.IdOrdenPago, cp_orden_pago_det.Secuencia, cp_orden_pago.IdTipo_Persona, cp_orden_pago.IdPersona, cp_orden_pago.IdEntidad, 
                  cp_orden_pago.Fecha, cp_orden_pago.Fecha AS Expr3, cp_orden_pago.Fecha AS Expr4, cp_orden_pago.Observacion, NULL AS Expr5, NULL AS Expr6, cp_orden_pago_det.Valor_a_pagar, cp_orden_pago_det.Valor_a_pagar AS valor_estimado_a_pagar, 
                  ISNULL(SUM(cp_orden_pago_cancelaciones.MontoAplicado),0) AS MontoAplicado, cp_orden_pago_det.Valor_a_pagar - ISNULL(SUM(cp_orden_pago_cancelaciones.MontoAplicado), 0) AS saldo, cp_orden_pago.IdEstadoAprobacion, 
                  cp_orden_pago.IdFormaPago, cp_orden_pago.Fecha Fecha_Pago, NULL AS IdCtaCble, NULL AS Expr7, NULL AS Expr8, NULL AS Expr10, cp_orden_pago.Estado, NULL AS Expr9, cp_orden_pago_det.IdEmpresa_cxp, 
                  cp_orden_pago_det.IdTipoCbte_cxp, cp_orden_pago_det.IdCbteCble_cxp, 0 IdBanco
FROM     cp_orden_pago INNER JOIN
                  cp_orden_pago_det ON cp_orden_pago.IdEmpresa = cp_orden_pago_det.IdEmpresa AND cp_orden_pago.IdOrdenPago = cp_orden_pago_det.IdOrdenPago LEFT JOIN
                  cp_orden_pago_cancelaciones ON cp_orden_pago_det.IdEmpresa = cp_orden_pago_cancelaciones.IdEmpresa_op AND cp_orden_pago_det.IdOrdenPago = cp_orden_pago_cancelaciones.IdOrdenPago_op AND 
                  cp_orden_pago_det.Secuencia = cp_orden_pago_cancelaciones.Secuencia_op 
WHERE cp_orden_pago.IdEmpresa = @IdEmpresa and cp_orden_pago.IdTipo_Persona like '%'+@IdTipoPersona+'%' and cp_orden_pago.IdEntidad between @IdEntidad_ini and @IdEntidad_fin
and cp_orden_pago.IdEstadoAprobacion like '%'+@IdEstado_Aprobacion+'%' and cp_orden_pago.IdPersona between @IdPersona_ini and @IdPersona_fin AND cp_orden_pago.Estado = 'A'
and cp_orden_pago.IdSucursal = @IdSucursal
and not exists(
select f.IdEmpresa from ba_Archivo_Transferencia_Det as f
where f.IdEmpresa_OP = cp_orden_pago_det.IdEmpresa
and f.IdOrdenPago = cp_orden_pago_det.IdOrdenPago
and f.Secuencia_OP = cp_orden_pago_det.Secuencia
) 
GROUP BY cp_orden_pago.IdEmpresa, cp_orden_pago.IdTipo_op,cp_orden_pago_det.Referencia, cp_orden_pago.IdOrdenPago, cp_orden_pago_det.Secuencia, cp_orden_pago.IdTipo_Persona, cp_orden_pago.IdPersona, cp_orden_pago.IdEntidad, cp_orden_pago.Fecha, 
                  cp_orden_pago.Observacion, cp_orden_pago_det.Valor_a_pagar, cp_orden_pago.IdEstadoAprobacion, cp_orden_pago.IdFormaPago, cp_orden_pago.Fecha, cp_orden_pago_det.IdCbteCble_cxp, cp_orden_pago.Estado, 
                  cp_orden_pago_det.IdEmpresa_cxp, cp_orden_pago_det.IdTipoCbte_cxp
--HAVING ROUND(cp_orden_pago_det.Valor_a_pagar - ISNULL(SUM(cp_orden_pago_cancelaciones.MontoAplicado), 0),2) > 0

if(@mostrar_saldo_0 = 0)
BEGIN
	DELETE cp_orden_pago_con_cancelacion_data WHERE @IdUsuario = IdUsuario and Saldo_x_Pagar_OP = 0
END

update [cp_orden_pago_con_cancelacion_data] 
set 
 Nom_Beneficiario=ben.pe_nombreCompleto
,Girar_Cheque_a=ben.pr_girar_cheque_a
,Nom_Beneficiario_2=ben.Nombre
FROM            cp_orden_pago_con_cancelacion_data AS data INNER JOIN
                         vwtb_persona_beneficiario AS ben ON data.IdEmpresa = ben.IdEmpresa AND data.IdTipoPersona = ben.IdTipo_Persona 
						 AND data.IdPersona = ben.IdPersona AND data.IdEntidad = ben.IdEntidad
where data.IdUsuario = @IdUsuario

update [cp_orden_pago_con_cancelacion_data] 
set Referencia=OG.co_observacion--doc.Codigo+'#' + CAST( CAST( OG.co_factura AS NUMERIC)  AS VARCHAR(20))
,Referencia2= doc.Codigo+'#' + CAST( CAST(OG.co_factura AS NUMERIC) AS VARCHAR(20))
,Fecha_Fa_Prov=OG.co_FechaFactura
,Fecha_Venc_Fac_Prov=OG.co_FechaFactura_vct
,Girar_Cheque_a = ISNULL(suc.Su_Descripcion, Girar_Cheque_a)
,Observacion = og.co_observacion
FROM            [cp_orden_pago_con_cancelacion_data]  AS data INNER JOIN
                         cp_orden_giro AS OG ON data.IdEmpresa_cxp = OG.IdEmpresa AND data.IdTipoCbte_cxp = OG.IdTipoCbte_Ogiro AND data.IdCbteCble_cxp = OG.IdCbteCble_Ogiro INNER JOIN
                         cp_TipoDocumento AS doc ON OG.IdOrden_giro_Tipo = doc.CodTipoDocumento  LEFT JOIN tb_sucursal AS Suc
						 on og.IdEmpresa = suc.IdEmpresa and og.IdSucursal_cxp = suc.IdSucursal
where data.IdUsuario = @IdUsuario

update [cp_orden_pago_con_cancelacion_data]  
set Referencia=
CASE WHEN ND.cn_Nota is NULL  or ND.cn_Nota='' THEN  +'ND#:' + cast(ND.IdCbteCble_Nota as varchar(20)) 
else 'ND#' + ND.cn_Nota
END 
,Referencia2=CASE WHEN ND.cn_Nota is NULL  or ND.cn_Nota='' THEN  +'ND#:' + cast(ND.IdCbteCble_Nota as varchar(20)) 
else 'ND#' + ND.cn_Nota
END

,Fecha_Fa_Prov=ND.cn_fecha
,Fecha_Venc_Fac_Prov=ND.cn_Fecha_vcto

FROM            [cp_orden_pago_con_cancelacion_data]  AS data INNER JOIN
                         cp_nota_DebCre AS ND ON data.IdEmpresa_cxp = ND.IdEmpresa AND data.IdCbteCble_cxp = ND.IdCbteCble_Nota AND data.IdTipoCbte_cxp = ND.IdTipoCbte_Nota
where data.IdUsuario = @IdUsuario

update [cp_orden_pago_con_cancelacion_data]  
set Referencia='OP#' + cast([cp_orden_pago_con_cancelacion_data] .IdOrdenPago as varchar(20))
,Referencia2='OP#' + cast([cp_orden_pago_con_cancelacion_data] .IdOrdenPago as varchar(20))
,fecha_fa_prov=fecha_op
,fecha_venc_fac_prov=fecha_op
WHERE Referencia is null and @IdUsuario = IdUsuario


UPDATE cp_orden_pago_con_cancelacion_data
set IdCtaCble = A.IdCtaCble_Acreedora
FROM
(
SELECT IdEmpresa, IdTipoCbte, IdCbteCble, IdCtaCble_Acreedora FROM vwct_cbtecble_con_ctacble_acreedora AS F
WHERE EXISTS(
SELECT R.IdEmpresa FROM cp_orden_pago_con_cancelacion_data AS R
WHERE R.IdUsuario = @IdUsuario
AND R.IdEmpresa = F.IdEmpresa
AND R.IdTipoCbte_cxp = F.IdTipoCbte
AND R.IdCbteCble_cxp = F.IdCbteCble
)
) A
WHERE  cp_orden_pago_con_cancelacion_data.IdEmpresa_cxp = A.IdEmpresa
AND cp_orden_pago_con_cancelacion_data.IdTipoCbte_cxp = A.IdTipoCbte
AND cp_orden_pago_con_cancelacion_data.IdCbteCble_cxp = A.IdCbteCble

if(@ValidarCuentaBancaria = 1)
BEGIN
DELETE cp_orden_pago_con_cancelacion_data 
FROM vwtb_persona_beneficiario AS B
WHERE IdUsuario = @IdUsuario	
AND cp_orden_pago_con_cancelacion_data.IdEmpresa = B.IdEmpresa
AND cp_orden_pago_con_cancelacion_data.IdTipoPersona = B.IdTipo_Persona
AND cp_orden_pago_con_cancelacion_data.IdPersona = B.IdPersona
AND cp_orden_pago_con_cancelacion_data.IdEntidad = B.IdEntidad
AND (B.IdTipoCta_acreditacion_cat IS NULL OR B.num_cta_acreditacion IS NULL OR B.IdBanco_acreditacion IS NULL)
END

SELECT ISNULL(ROW_NUMBER() OVER (ORDER BY IdUsuario),0) AS IdRow, IdUsuario, IdEmpresa, IdTipo_op, Referencia, Referencia2, IdOrdenPago, Secuencia_OP, IdTipoPersona, IdPersona, IdEntidad, Fecha_OP, Fecha_Fa_Prov, Fecha_Venc_Fac_Prov, Observacion, Nom_Beneficiario, Girar_Cheque_a, 
                  Valor_a_pagar, Valor_estimado_a_pagar_OP, Total_cancelado_OP, Saldo_x_Pagar_OP, IdEstadoAprobacion, IdFormaPago, Fecha_Pago, IdCtaCble, IdCentroCosto, IdSubCentro_Costo, Cbte_cxp, Estado, Nom_Beneficiario_2, 
                  IdEmpresa_cxp, IdTipoCbte_cxp, IdCbteCble_cxp, IdBanco
FROM     cp_orden_pago_con_cancelacion_data where IdUsuario = @IdUsuario
END