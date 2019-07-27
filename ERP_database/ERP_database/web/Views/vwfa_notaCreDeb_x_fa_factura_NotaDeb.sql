CREATE view [web].[vwfa_notaCreDeb_x_fa_factura_NotaDeb]
as
select cruce.IdEmpresa_nt, cruce.IdSucursal_nt, cruce.IdBodega_nt, cruce.IdNota_nt, cruce.secuencia, cruce.vt_tipoDoc, cruce.IdEmpresa_fac_nd_doc_mod, cruce.IdSucursal_fac_nd_doc_mod, cruce.IdBodega_fac_nd_doc_mod,
cruce.IdCbteVta_fac_nd_doc_mod, cruce.Valor_Aplicado, 
iif(cruce.vt_tipoDoc = 'FACT', factura.vt_NumFactura ,debito.NumNotaImpresa) vt_NumFactura,
iif(cruce.vt_tipoDoc = 'FACT', factura.vt_Observacion ,debito.sc_observacion) vt_Observacion,
iif(cruce.vt_tipoDoc = 'FACT', factura.CodCbteVta ,debito.CodNota) CodDoc,
iif(cruce.vt_tipoDoc = 'FACT', factura.vt_Subtotal ,debito.sc_subtotal) vt_Subtotal,
iif(cruce.vt_tipoDoc = 'FACT', factura.vt_iva ,debito.sc_iva) vt_iva,
iif(cruce.vt_tipoDoc = 'FACT', factura.vt_total ,debito.sc_total) vt_total,
isnull(cobro.ValorCobrado,0)ValorCobrado,
isnull(iif(cruce.vt_tipoDoc = 'FACT', factura.vt_total ,debito.sc_total) - isnull(cobro.ValorCobrado,0),0) as saldo,
iif(cruce.vt_tipoDoc = 'FACT', factura.vt_total ,debito.sc_total) - isnull(cobro.ValorCobrado,0) + cruce.Valor_Aplicado as saldo_sin_cobro,
iif(cruce.vt_tipoDoc = 'FACT', factura.vt_fecha ,debito.no_fecha) vt_fecha,
iif(cruce.vt_tipoDoc = 'FACT', factura.vt_fech_venc ,debito.no_fecha_venc) vt_fech_venc,
iif(cruce.vt_tipoDoc = 'FACT', factura.IdCliente ,debito.IdCliente) IdCliente,
iif(cruce.vt_tipoDoc = 'FACT', factura.Nombres ,debito.Nombres) pe_nombreCompleto
 from fa_notaCreDeb_x_fa_factura_NotaDeb as cruce
 left join (
		SELECT        fa_factura.IdEmpresa, fa_factura.IdSucursal, fa_factura.IdBodega, fa_factura.IdCbteVta, fa_factura.vt_tipoDoc,fa_factura.vt_tipoDoc+'-'+ CAST(CAST(fa_factura.vt_NumFactura AS NUMERIC) AS VARCHAR(20)) vt_NumFactura, fa_factura.vt_Observacion, fa_factura.CodCbteVta, SUM(fa_factura_det.vt_Subtotal) 
						AS vt_Subtotal, SUM(fa_factura_det.vt_iva) AS vt_iva, SUM(fa_factura_det.vt_total) AS vt_total, fa_factura.vt_fecha, fa_factura.vt_fech_venc, fa_factura.IdCliente, tb_persona.pe_nombreCompleto Nombres
		FROM            fa_factura LEFT OUTER JOIN
						fa_factura_det ON fa_factura.IdEmpresa = fa_factura_det.IdEmpresa AND fa_factura.IdSucursal = fa_factura_det.IdSucursal AND fa_factura.IdBodega = fa_factura_det.IdBodega AND 
						fa_factura.IdCbteVta = fa_factura_det.IdCbteVta INNER JOIN
						fa_cliente ON fa_cliente.IdEmpresa = fa_factura.IdEmpresa AND fa_cliente.IdCliente = fa_factura.IdCliente INNER JOIN
						tb_persona ON fa_cliente.IdPersona = tb_persona.IdPersona
		GROUP BY fa_factura.IdEmpresa, fa_factura.IdSucursal, fa_factura.IdBodega, fa_factura.IdCbteVta, fa_factura.vt_tipoDoc, fa_factura.vt_NumFactura, fa_factura.vt_Observacion, fa_factura.CodCbteVta, fa_factura.vt_fecha, 
						fa_factura.vt_fech_venc, fa_factura.IdCliente, tb_persona.pe_nombreCompleto
 ) as factura on cruce.IdEmpresa_fac_nd_doc_mod = factura.IdEmpresa and cruce.IdSucursal_fac_nd_doc_mod = factura.IdSucursal and cruce.IdBodega_fac_nd_doc_mod = factura.IdBodega and cruce.IdCbteVta_fac_nd_doc_mod = factura.IdCbteVta
 and cruce.vt_tipoDoc = factura.vt_tipoDoc
 LEFT JOIN (
		SELECT        fa_notaCreDeb.IdEmpresa, fa_notaCreDeb.IdSucursal, fa_notaCreDeb.IdBodega, fa_notaCreDeb.IdNota, fa_notaCreDeb.CodDocumentoTipo, tb_persona.pe_nombreCompleto Nombres, fa_notaCreDeb.no_fecha, 
						fa_notaCreDeb.no_fecha_venc, fa_notaCreDeb.sc_observacion, fa_notaCreDeb.CodNota,fa_notaCreDeb.IdCliente,
						
						fa_notaCreDeb.CodDocumentoTipo +'-'+ IIF(fa_notaCreDeb.NumNota_Impresa IS NOT NULL, CAST(CAST(fa_notaCreDeb.NumNota_Impresa AS NUMERIC) AS VARCHAR(20)),cast( fa_notaCreDeb.IdNota as varchar(20))) as NumNotaImpresa, 
						
						SUM(fa_notaCreDeb_det.sc_subtotal) AS sc_subtotal, SUM(fa_notaCreDeb_det.sc_iva) AS sc_iva, 
						SUM(fa_notaCreDeb_det.sc_total) AS sc_total
		FROM            fa_notaCreDeb LEFT OUTER JOIN
						fa_notaCreDeb_det ON fa_notaCreDeb.IdEmpresa = fa_notaCreDeb_det.IdEmpresa AND fa_notaCreDeb.IdSucursal = fa_notaCreDeb_det.IdSucursal AND fa_notaCreDeb.IdBodega = fa_notaCreDeb_det.IdBodega AND 
						fa_notaCreDeb.IdNota = fa_notaCreDeb_det.IdNota INNER JOIN
						fa_cliente ON fa_cliente.IdEmpresa = fa_notaCreDeb.IdEmpresa AND fa_cliente.IdCliente = fa_notaCreDeb.IdCliente INNER JOIN
						tb_persona ON fa_cliente.IdPersona = tb_persona.IdPersona
		GROUP BY tb_persona.pe_nombreCompleto, fa_notaCreDeb.IdEmpresa, fa_notaCreDeb.IdSucursal, fa_notaCreDeb.IdBodega, fa_notaCreDeb.IdNota, fa_notaCreDeb.CodDocumentoTipo, fa_notaCreDeb.no_fecha, 
						fa_notaCreDeb.no_fecha_venc, fa_notaCreDeb.sc_observacion, fa_notaCreDeb.NumNota_Impresa,fa_notaCreDeb.CodNota,fa_notaCreDeb.IdCliente
 ) as debito on cruce.IdEmpresa_fac_nd_doc_mod = debito.IdEmpresa and cruce.IdSucursal_fac_nd_doc_mod = debito.IdSucursal and cruce.IdBodega_fac_nd_doc_mod = debito.IdBodega and cruce.IdCbteVta_fac_nd_doc_mod = debito.IdNota
 and cruce.vt_tipoDoc = debito.CodDocumentoTipo LEFT OUTER JOIN
                             (SELECT        IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento, SUM(dc_ValorPago) AS ValorCobrado
                               FROM            dbo.cxc_cobro_det AS det
                               WHERE        (estado = 'A')
                               GROUP BY IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento) AS cobro on
							   cruce.IdEmpresa_fac_nd_doc_mod = cobro.IdEmpresa and cruce.IdSucursal_fac_nd_doc_mod = cobro.IdSucursal and cruce.IdBodega_fac_nd_doc_mod = cobro.IdBodega_Cbte and cruce.IdCbteVta_fac_nd_doc_mod = cobro.IdCbte_vta_nota
 and cruce.vt_tipoDoc = cobro.dc_TipoDocumento