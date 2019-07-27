--exec [EntidadRegulatoria].[generarATS] 2,201905,1,8
CREATE  PROCEDURE [EntidadRegulatoria].[generarATS]
@idempresa int,
@idPeriodo int,
@IdSucursalInicio int,
@IdSucursalFin int
AS

--declare 
--@idempresa int,
--@idPeriodo int,
--@IdSucursalInicio int,
--@IdSucursalFin int

--set @idperiodo=201901
--set @idempresa=2
--set @IdSucursalInicio=0
--set @IdSucursalFin=99
BEGIN


--*****************************************************************************************************************************************************************++
--**************************************************************************compras*****************************************************************************+*****

declare
@fecha_inicio date,
@fecha_fin date

delete EntidadRegulatoria.ATS_ventas
select @fecha_inicio=pe_FechaIni, @fecha_fin=pe_FechaFin from ct_periodo where IdEmpresa=@idempresa and IdPeriodo=@idPeriodo
delete EntidadRegulatoria.ATS_ventas where IdEmpresa=@idempresa --and IdPeriodo=@idPeriodo
delete EntidadRegulatoria.ATS_compras where idempresa=@idempresa-- and idperiodo=@idPeriodo
delete EntidadRegulatoria.ATS_retenciones where idempresa=@idempresa-- and idperiodo=@idPeriodo
delete EntidadRegulatoria.ATS_comprobantes_anulados where idempresa=@idempresa-- and idperiodo=@idPeriodo
delete EntidadRegulatoria.ATS_exportaciones where idempresa=@idempresa --and idperiodo=@idPeriodo

insert into EntidadRegulatoria.ATS_compras
(
IdEmpresa,																					IdPeriodo,			
Secuencia,																					codSustento,
tpIdProv,																					idProv,				
tipoComprobante,																			parteRel,
tipoProv,																					denopr,	
fechaRegistro,																				establecimiento,
puntoEmision,																				secuencial,			
fechaEmision,																				autorizacion,
baseNoGraIva,																				baseImponible,		
baseImpGrav,																				baseImpExe,			
montoIce,																					montoIva,			
pagoLocExt,																					denopago,			
paisEfecPago,																				formaPago,
IdSucursal
)
SELECT 

 @idempresa,																				@idPeriodo,				
 ISNULL(ROW_NUMBER()OVER (ORDER BY fac.IdEmpresa), 0)AS Secuencia,							fac.IdOrden_giro_Tipo,
 CASe when perso.IdTipoDocumento='CED' THEN '02' when perso.IdTipoDocumento='PAS' THEN '03'  else '01' end  tpIdProv,					perso.pe_cedulaRuc,
 fac.IdOrden_giro_Tipo tipoComprobante,																		'NO' AS ParteRelacionada,
CASe when perso.pe_Naturaleza='NATU' THEN '01' else '02' end AS tipoProv,					perso.pe_nombreCompleto,			 
 cast(fac.co_fechaOg as date),																SUBSTRING(fac.co_serie, 0, 4) AS establecimiento, 
 SUBSTRING(fac.co_serie, 5, 4) AS puntoEmision,												fac.co_factura, 
 fac.co_FechaFactura,																		fac.Num_Autorizacion,																				 
 isnull(fac.BseImpNoObjDeIva,0.00),															fac.co_subtotal_siniva, 
 fac.co_subtotal_iva,																		isnull(fac.BseImpNoObjDeIva,0.00),
 0 co_Ice_valor,																			fac.co_valoriva,																					
 fac.PagoLocExt,																			f_pago.formas_pago_sri,  
 f_pago.codigo_pago_sri,																	f_pago.codigo_pago_sri, fac.IdSucursal
FROM            dbo.cp_orden_giro AS fac INNER JOIN
                         dbo.cp_proveedor AS prov ON fac.IdEmpresa = prov.IdEmpresa AND fac.IdProveedor = prov.IdProveedor INNER JOIN
                         dbo.tb_persona AS perso ON prov.IdPersona = perso.IdPersona LEFT OUTER JOIN
                         dbo.cp_orden_giro_pagos_sri AS f_pago ON fac.IdEmpresa = f_pago.IdEmpresa AND fac.IdCbteCble_Ogiro = f_pago.IdCbteCble_Ogiro AND fac.IdTipoCbte_Ogiro = f_pago.IdTipoCbte_Ogiro
						 
						 where fac.IdEmpresa=@idempresa
						 and fac.co_fechaOg between @fecha_inicio and @fecha_fin
						 and fac.Estado='A'
						 and fac.IdSucursal>=@IdSucursalInicio
						 and fac.IdSucursal<=@IdSucursalFin
						 



--*****************************************************************************************************************************************************************++
--**************************************************************************VENTAS*****************************************************************************+*****
PRINT 'VENTAS'
insert into EntidadRegulatoria.ATS_ventas
(IdEmpresa,									IdPeriodo,											Secuencia,															tpIdCliente,
idCliente,									parteRel,											tipoCliente,														DenoCli,									
tipoComprobante,							tipoEm,												numeroComprobantes,													baseNoGraIva,
baseImponible,								baseImpGrav,										montoIva,															montoIce,
valorRetIva,								valorRetRenta,										formaPago,															codEstab,
ventasEstab,								ivaComp,IdSucursal)
select 

@idempresa,									@idPeriodo,											ROW_NUMBER()OVER (ORDER BY ventas.IdEmpresa),						ventas.tpIdCliente,
ventas.pe_cedulaRuc,						ventas.parteRel,									ventas.tipoCliente,													ventas.pe_nombreCompleto,
ventas.tipoEmtipoComprobante,				ventas.tipoEm,										count(ventas.IdCbteVta),											sum(ventas.baseNoGraIva),
sum(ventas.baseImponible),					sum(ventas.baseImpGrav),							SUM(ventas.montoIva),												sum(ventas.montoIce),
isnull(sum(cobros.valorRetIva),0.00),		isnull(sum(cobros.valorRetRenta),0.00),				ventas.IdFormaPago,													ventas.vt_serie1,
SUM(ventas.baseImponible+ventas.baseImpGrav),							isnull(sum(ventas.montoIva),0.00),ventas.IdSucursal


from(
SELECT        
fac.IdEmpresa,
fac.IdSucursal,
fac.IdBodega,
fac.IdCbteVta,
fac.IdCliente,

 CASE WHEN per.IdTipoDocumento = 'CED' THEN '05' when per.IdTipoDocumento='PAS' THEN '06' ELSE '04' END AS tpIdCliente,
 per.pe_cedulaRuc, 
 'NO' AS parteRel, 
 CASe when per.pe_Naturaleza='JURI' THEN '02' else '01' end AS tipoCliente, 
 per.pe_nombreCompleto, 
 '18' AS tipoEmtipoComprobante, 
 'F' AS tipoEm,
 0.00 as baseNoGraIva,
CASe when fac_det.IdCod_Impuesto_Iva='IVA0' then SUM( fac_det.vt_Subtotal) else 0.00 end baseImponible,
CASe when fac_det.IdCod_Impuesto_Iva='IVA12' then SUM( fac_det.vt_Subtotal) else 0.00 end baseImpGrav,
sum(fac_det.vt_iva)montoIva,
0.00 montoIce,
fac.vt_serie1,
fac.vt_serie2,
fac.vt_NumFactura,
f_pago.IdFormaPago



FROM            dbo.fa_factura AS fac INNER JOIN
                         dbo.fa_factura_det AS fac_det ON fac.IdEmpresa = fac_det.IdEmpresa AND fac.IdSucursal = fac_det.IdSucursal AND fac.IdBodega = fac_det.IdBodega AND fac.IdCbteVta = fac_det.IdCbteVta INNER JOIN
                         dbo.fa_cliente AS cli ON fac.IdEmpresa = cli.IdEmpresa AND fac.IdCliente = cli.IdCliente INNER JOIN
                         dbo.tb_persona AS per ON cli.IdPersona = per.IdPersona INNER JOIN
                         dbo.fa_formaPago AS f_pago ON cli.FormaPago = f_pago.IdFormaPago
						  where  fac.vt_fecha between @fecha_inicio and @fecha_fin
						 and  fac.Estado='A' 
						 and fac.vt_fecha between @fecha_inicio and @fecha_fin
						 --and per.IdTipoDocumento!='PAS'
						 and fac.IdEmpresa = @idempresa
						 and fac.IdSucursal>=@IdSucursalInicio
						 and fac.IdSucursal<=@IdSucursalFin
GROUP BY per.pe_cedulaRuc, per.pe_nombreCompleto,per.IdTipoDocumento, fac_det.vt_por_iva, fac.IdEmpresa,fac.IdCliente,
fac.IdSucursal,
fac.IdBodega,
fac.IdCbteVta,
fac.vt_serie1,
fac.vt_serie2,
fac.vt_NumFactura,
f_pago.IdFormaPago,
IdCod_Impuesto_Iva, pe_Naturaleza
) ventas
left join
((
select cobro_x_retencion.IdEmpresa,cobro_x_retencion.IdSucursal,cobro_x_retencion.IdBodega_Cbte,
cobro_x_retencion.IdCliente,SUM( cobro_x_retencion.valorRetIva)valorRetIva,SUM( cobro_x_retencion.valorRetRenta)valorRetRenta,cobro_x_retencion.IdCbte_vta_nota
from
(
select  cxc_cobro.idempresa,cxc_cobro.IdSucursal, IdBodega_Cbte,IdCbte_vta_nota, cxc_cobro.idCliente,
isnull( case when SUBSTRING( cxc_cobro_det.IdCobro_tipo,0,5)= 'RTFT'then sum(dc_ValorPago) end,0.00) valorRetRenta,
isnull(case when SUBSTRING( cxc_cobro_det.IdCobro_tipo,0,5)= 'RTIV'then sum(dc_ValorPago) end,0.00) valorRetIva
 from cxc_cobro_det, cxc_cobro_tipo, cxc_cobro
 where cxc_cobro_det.IdCobro_tipo=cxc_cobro_tipo.IdCobro_tipo
 and cxc_cobro_det.IdEmpresa=cxc_cobro.IdEmpresa
 and cxc_cobro_det.IdCobro=cxc_cobro.IdCobro
 and cxc_cobro_tipo.IdMotivo_tipo_cobro='RET'
 and cxc_cobro_det.IdEmpresa=@idempresa
 GROUP by cxc_cobro.idempresa,cxc_cobro.IdSucursal, IdBodega_Cbte,IdCbte_vta_nota,cxc_cobro.IdCliente, cxc_cobro_det.IdCobro_tipo
 ) as 
 cobro_x_retencion
 group by 
 cobro_x_retencion.IdEmpresa,cobro_x_retencion.IdSucursal,cobro_x_retencion.IdBodega_Cbte,
cobro_x_retencion.IdCliente,cobro_x_retencion.IdCbte_vta_nota

 )
 ) as cobros
 on ventas.IdEmpresa=cobros.IdEmpresa
 and ventas.IdSucursal=cobros.IdSucursal
 and ventas.IdBodega=cobros.IdBodega_Cbte
 and ventas.IdCbteVta=cobros.IdCbte_vta_nota
 and ventas.idcliente=cobros.IdCliente
 and ventas.IdEmpresa=@idempresa
 
 group by ventas.IdEmpresa,ventas.tpIdCliente,ventas.parteRel,ventas.tipoCliente,ventas.tipoEmtipoComprobante,ventas.tipoEm,ventas.IdFormaPago,ventas.vt_serie1, ventas.vt_serie2, ventas.IdCliente, ventas.pe_nombreCompleto, ventas.pe_cedulaRuc, ventas.IdSucursal
 





 union all



 --*****************************************************************************************************************************************************************++
--**************************************************************************NOTA CREDITO*****************************************************************************+*****


select 

@idempresa,									@idPeriodo,											ROW_NUMBER()OVER (ORDER BY ventas.IdEmpresa),						ventas.tpIdCliente,
ventas.pe_cedulaRuc,						ventas.parteRel,									ventas.tipoCliente,													ventas.pe_nombreCompleto,
ventas.tipoEmtipoComprobante,				ventas.tipoEm,										count(ventas.IdNota),											sum(ventas.baseNoGraIva),
sum(ventas.baseImponible),					sum(ventas.baseImpGrav),							SUM(ventas.montoIva),												sum(ventas.montoIce),
isnull(sum(cobros.valorRetIva),0.00),		isnull(sum(cobros.valorRetRenta),0.00),				ventas.IdFormaPago,													ventas.Serie1,
SUM(ventas.baseImponible+ventas.baseImpGrav),							isnull(sum(ventas.montoIva),0.00), ventas.IdSucursal


from(
SELECT        
fac.IdEmpresa,
fac.IdSucursal,
fac.IdBodega,
fac.IdNota,
fac.IdCliente,

 CASE WHEN per.IdTipoDocumento = 'CED' THEN '05' ELSE '04' END AS tpIdCliente,
 per.pe_cedulaRuc, 
 'NO' AS parteRel, 
 CASe when per.IdTipoDocumento='CED' THEN '05' else '04' end AS tipoCliente, 
 per.pe_nombreCompleto, 
 '04' AS tipoEmtipoComprobante, 
 'F' AS tipoEm,
 0.00 as baseNoGraIva,
CASe when fac_det.IdCod_Impuesto_Iva='IVA0' then SUM( fac_det.sc_subtotal) else 0.00 end baseImponible,
CASe when fac_det.IdCod_Impuesto_Iva='IVA12' then SUM( fac_det.sc_subtotal) else 0.00 end baseImpGrav,
sum(fac_det.sc_iva)montoIva,
0.00 montoIce,
fac.Serie1,
fac.Serie2,
fac.NumNota_Impresa,
f_pago.IdFormaPago


FROM            dbo.fa_notaCreDeb AS fac INNER JOIN
                         dbo.fa_notaCreDeb_det AS fac_det ON fac.IdEmpresa = fac_det.IdEmpresa AND fac.IdSucursal = fac_det.IdSucursal AND fac.IdBodega = fac_det.IdBodega AND fac.IdNota = fac_det.IdNota INNER JOIN
                         dbo.fa_cliente AS cli ON fac.IdEmpresa = cli.IdEmpresa AND fac.IdCliente = cli.IdCliente INNER JOIN
                         dbo.tb_persona AS per ON cli.IdPersona = per.IdPersona INNER JOIN
                         dbo.fa_formaPago AS f_pago ON cli.FormaPago = f_pago.IdFormaPago
						  where  fac.no_fecha between @fecha_inicio and @fecha_fin
						 and  fac.Estado='A' 
						 AND FAC.NaturalezaNota = 'SRI'
						 and fac.no_fecha between @fecha_inicio and @fecha_fin
						-- and per.IdTipoDocumento!='PAS'
						 and fac.IdEmpresa = @idempresa
						  and fac.IdSucursal>=@IdSucursalInicio
						 and fac.IdSucursal<=@IdSucursalFin
						 AND fac.CreDeb='C'
GROUP BY per.pe_cedulaRuc, per.pe_nombreCompleto,per.IdTipoDocumento, fac_det.vt_por_iva, fac.IdEmpresa,fac.IdCliente,
fac.IdSucursal,
fac.IdBodega,
fac.IdNota,
fac.Serie1,
fac.Serie2,
fac.NumNota_Impresa,
f_pago.IdFormaPago,
IdCod_Impuesto_Iva, fac.IdSucursal
) ventas
left join
((
select cobro_x_retencion.IdEmpresa,cobro_x_retencion.IdSucursal,cobro_x_retencion.IdBodega_Cbte,
cobro_x_retencion.IdCliente,SUM( cobro_x_retencion.valorRetIva)valorRetIva,SUM( cobro_x_retencion.valorRetRenta)valorRetRenta,cobro_x_retencion.IdCbte_vta_nota
from
(
select  cxc_cobro.idempresa,cxc_cobro.IdSucursal, IdBodega_Cbte,IdCbte_vta_nota, cxc_cobro.idCliente,
isnull( case when SUBSTRING( cxc_cobro_det.IdCobro_tipo,0,5)= 'RTFT'then sum(dc_ValorPago) end,0.00) valorRetRenta,
isnull(case when SUBSTRING( cxc_cobro_det.IdCobro_tipo,0,5)= 'RTIV'then sum(dc_ValorPago) end,0.00) valorRetIva
 from cxc_cobro_det, cxc_cobro_tipo, cxc_cobro
 where cxc_cobro_det.IdCobro_tipo=cxc_cobro_tipo.IdCobro_tipo
 and cxc_cobro_det.IdEmpresa=cxc_cobro.IdEmpresa
 and cxc_cobro_det.IdCobro=cxc_cobro.IdCobro
 and cxc_cobro_tipo.IdMotivo_tipo_cobro='RET'
 and cxc_cobro_det.IdEmpresa=@idempresa

 GROUP by cxc_cobro.idempresa,cxc_cobro.IdSucursal, IdBodega_Cbte,IdCbte_vta_nota,cxc_cobro.IdCliente, cxc_cobro_det.IdCobro_tipo
 ) as 
 cobro_x_retencion
 group by 
 cobro_x_retencion.IdEmpresa,cobro_x_retencion.IdSucursal,cobro_x_retencion.IdBodega_Cbte,
cobro_x_retencion.IdCliente,cobro_x_retencion.IdCbte_vta_nota

 )
 ) as cobros
 on ventas.IdEmpresa=cobros.IdEmpresa
 and ventas.IdSucursal=cobros.IdSucursal
 and ventas.IdBodega=cobros.IdBodega_Cbte
 and ventas.IdNota=cobros.IdCbte_vta_nota
 and ventas.idcliente=cobros.IdCliente
 and ventas.IdEmpresa=@idempresa
 
 group by ventas.IdEmpresa,ventas.tpIdCliente,ventas.parteRel,ventas.tipoCliente,ventas.tipoEmtipoComprobante,ventas.tipoEm,ventas.IdFormaPago,ventas.Serie1, ventas.Serie2, ventas.IdCliente, ventas.pe_nombreCompleto, ventas.pe_cedulaRuc, ventas.IdSucursal
 














 --*****************************************************************************************************************************************************************++
--**************************************************************************Retenciones****************************************************************+*****
insert into EntidadRegulatoria.ATS_retenciones(
IdEmpresa,																						IdPeriodo,
Secuencia,																						co_serie,
co_factura,																						Cedula_ruc,
valRetBien10,																					valRetServ20,
valorRetBienes,																					valRetServ50,
valorRetServicios,																				valRetServ100,
codRetAir,																						baseImpAir,
porcentajeAir,																					valRetAir,
estabRetencion1,																				ptoEmiRetencion1,
secRetencion1,																					autRetencion1,
fechaEmiRet1,																					re_tipo_Ret,
denopr,																							IdSucursal
)
SELECT
@idempresa,																						@idPeriodo,
ROW_NUMBER()OVER (ORDER BY fac.IdEmpresa),														fac.co_serie,
fac.co_factura,																					per.pe_cedulaRuc,
ISNULL(CASE WHEN ret_det.re_Porcen_retencion = '10' THEN ret_det.re_valor_retencion END, 0) AS valRetBien10,ISNULL(CASE WHEN ret_det.re_Porcen_retencion = '20' THEN ret_det.re_valor_retencion END, 0) AS valRetServ20,
ISNULL(CASE WHEN ret_det.re_Porcen_retencion = '30' THEN ret_det.re_valor_retencion END, 0) AS valorRetBienes, ISNULL(CASE WHEN ret_det.re_Porcen_retencion = '50' THEN ret_det.re_valor_retencion END, 0) AS valRetServ50, 
ISNULL(CASE WHEN ret_det.re_Porcen_retencion = '70' THEN ret_det.re_valor_retencion END, 0) AS valorRetServicios, ISNULL(CASE WHEN ret_det.re_Porcen_retencion = '100' THEN ret_det.re_valor_retencion END, 0) AS valRetServ100,
ret_det.re_Codigo_impuesto,																			ret_det.re_baseRetencion,
ret_det.re_Porcen_retencion,																	ret_det.re_valor_retencion,
ret.serie1,																						ret.serie2,
ret.NumRetencion,																				CASE WHEN T.es_Documento_Electronico = 1 THEN ret.NAutorizacion ELSE T.NumAutorizacion END AS NumAutorizacion,
cast(ret.fecha as date),																		ret_det.re_tipoRet,
per.pe_nombreCompleto, fac.IdSucursal																					
FROM            dbo.cp_orden_giro AS fac left JOIN
                         dbo.cp_retencion AS ret ON fac.IdEmpresa = ret.IdEmpresa_Ogiro AND fac.IdCbteCble_Ogiro = ret.IdCbteCble_Ogiro AND fac.IdTipoCbte_Ogiro = ret.IdTipoCbte_Ogiro INNER JOIN
                         dbo.cp_retencion_det AS ret_det ON ret.IdEmpresa = ret_det.IdEmpresa AND ret.IdRetencion = ret_det.IdRetencion INNER JOIN
                         dbo.cp_proveedor AS prov ON fac.IdEmpresa = prov.IdEmpresa AND fac.IdProveedor = prov.IdProveedor INNER JOIN
                         dbo.tb_persona AS per ON prov.IdPersona = per.IdPersona LEFT JOIN
						 tb_sis_Documento_Tipo_Talonario AS T ON RET.IdEmpresa = T.IdEmpresa AND RET.CodDocumentoTipo = T.CodDocumentoTipo AND RET.serie1 = T.Establecimiento AND RET.serie2 = T.PuntoEmision AND RET.NumRetencion = T.NumDocumento
						 where fac.co_fechaOg between @fecha_inicio and @fecha_fin
						 and fac.Estado='A'
						 and ret.Estado='A'
						 AND FAC.IdEmpresa = @idempresa

/*

insert into EntidadRegulatoria.ATS_exportaciones
(
IdEmpresa,																							IdPeriodo,
Secuencia,																							tpIdClienteEx,
idClienteEx,																						parteRel,
tipoRegi,																							paisEfecPagoGen,
paisEfecExp,																						exportacionDe,
tipoComprobante,																					fechaEmbarque,
valorFOB,																							valorFOBComprobante,
establecimiento,																					puntoEmision,
secuencial,																							autorizacion,
fechaEmision,																						denoExpCli
)

select
@idempresa,																							@idPeriodo,
ROW_NUMBER()OVER (ORDER BY fac.IdEmpresa),															'21',
per.pe_cedulaRuc,																					'NO',
'01',																								'301',
'301',																								'02',
'01',																								fac.vt_fecha,
sum(f_det.vt_total),																				sum(f_det.vt_total),
fac.vt_serie1,																						fac.vt_serie2,
fac.vt_NumFactura,																					fac.vt_autorizacion,
fac.vt_fecha,																						per.pe_nombreCompleto																				


FROM            dbo.fa_factura_det AS f_det INNER JOIN
                         dbo.fa_factura AS fac ON f_det.IdEmpresa = fac.IdEmpresa AND f_det.IdSucursal = fac.IdSucursal AND f_det.IdBodega = fac.IdBodega AND f_det.IdCbteVta = fac.IdCbteVta INNER JOIN
                         dbo.fa_cliente AS cli ON fac.IdEmpresa = cli.IdEmpresa AND fac.IdCliente = cli.IdCliente INNER JOIN
                         dbo.tb_persona AS per ON cli.IdPersona = per.IdPersona
						 where fac.vt_fecha between @fecha_inicio and @fecha_fin
						 and per.IdTipoDocumento='PAS'
						 and fac.IdEmpresa=@idempresa
						 GROUP BY fac.IdCliente,fac.IdCbteVta, fac.IdEmpresa, per.pe_nombreCompleto,fac.vt_fecha, fac.vt_serie1, fac.vt_serie2, fac.vt_NumFactura, fac.vt_autorizacion,
						 per.pe_cedulaRuc

						 select * from EntidadRegulatoria.ATS_exportaciones








*/




--*****************************************************************************************************************************************************************++
--**************************************************************************comprobantes anulados****************************************************************+*****
	insert into EntidadRegulatoria.ATS_comprobantes_anulados
	(
IdEmpresa,									IdPeriodo,								Secuencia,
tipoComprobante,							Establecimiento,						puntoEmision,
secuencialInicio,							secuencialFin,							Autorización
)


select @idempresa,							@idPeriodo,								ROW_NUMBER()OVER (ORDER BY anulados.IdEmpresa),
anulados.tipoComprobante,					anulados.vt_serie1,						anulados.vt_serie2,
anulados.ini ,								anulados.fin,						    anulados.NumAutorizacion
from(
select  
'01'tipoComprobante,						vt_serie1,								vt_serie2,
vt_NumFactura ini,							vt_NumFactura fin,							tb_sis_Documento_Tipo_Talonario.NumAutorizacion,fa_factura.IdEmpresa												
from fa_factura, tb_sis_Documento_Tipo_Talonario
 where fa_factura.Estado='I'
 and fa_factura.IdEmpresa=tb_sis_Documento_Tipo_Talonario.IdEmpresa
 and fa_factura.vt_serie1=tb_sis_Documento_Tipo_Talonario.Establecimiento
 and fa_factura.vt_serie2=tb_sis_Documento_Tipo_Talonario.PuntoEmision
 and fa_factura.vt_NumFactura=tb_sis_Documento_Tipo_Talonario.NumDocumento
 AND FA_fACTURA.IdEMPRESA = @IdEMPRESA
 and fa_factura.vt_fecha  between @fecha_inicio and @fecha_fin
  AND tb_sis_Documento_Tipo_Talonario.es_Documento_Electronico=0
 union
 select  
'07'tipoComprobante,						serie1,									serie2,
NumRetencion,								NumRetencion,							tb_sis_Documento_Tipo_Talonario.NumAutorizacion,cp_retencion.IdEmpresa												
from cp_retencion, tb_sis_Documento_Tipo_Talonario
 where cp_retencion.Estado='I'
 and cp_retencion.IdEmpresa=tb_sis_Documento_Tipo_Talonario.IdEmpresa
 and cp_retencion.serie1=tb_sis_Documento_Tipo_Talonario.Establecimiento
 and cp_retencion.serie2=tb_sis_Documento_Tipo_Talonario.PuntoEmision
 and cp_retencion.NumRetencion=tb_sis_Documento_Tipo_Talonario.NumDocumento
 AND cp_retencion.IdEmpresa = @idempresa
 and cp_retencion.fecha  between @fecha_inicio and @fecha_fin
 AND tb_sis_Documento_Tipo_Talonario.es_Documento_Electronico=0
 union

 select  
'04'tipoComprobante,						Serie1,								Serie2,
NumNota_Impresa ini,							NumNota_Impresa fin,							tb_sis_Documento_Tipo_Talonario.NumAutorizacion,fa_notaCreDeb.IdEmpresa												
from fa_notaCreDeb, tb_sis_Documento_Tipo_Talonario
 where fa_notaCreDeb.Estado='I'
 and fa_notaCreDeb.IdEmpresa=tb_sis_Documento_Tipo_Talonario.IdEmpresa
 and fa_notaCreDeb.Serie1=tb_sis_Documento_Tipo_Talonario.Establecimiento
 and fa_notaCreDeb.Serie1=tb_sis_Documento_Tipo_Talonario.PuntoEmision
 and fa_notaCreDeb.Serie2=tb_sis_Documento_Tipo_Talonario.NumDocumento
 AND fa_notaCreDeb.IdEMPRESA = @IdEMPRESA
 and fa_notaCreDeb.no_fecha  between @fecha_inicio and @fecha_fin
  AND tb_sis_Documento_Tipo_Talonario.es_Documento_Electronico=0

 )  anulados


END

