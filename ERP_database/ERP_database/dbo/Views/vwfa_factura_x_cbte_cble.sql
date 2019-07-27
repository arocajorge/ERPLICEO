CREATE view vwfa_factura_x_cbte_cble as
SELECT fa_factura.IdEmpresa, fa_factura.IdSucursal,fa_factura.IdBodega, fa_factura.IdCbteVta, tb_sucursal.Su_Descripcion, fa_factura.CodCbteVta, 
                         fa_factura.vt_tipoDoc + '-' + fa_factura.vt_serie1 + '-' + fa_factura.vt_serie2 + '-' + fa_factura.vt_NumFactura AS num_factura, fa_factura.vt_fecha, 
                         fa_factura.vt_Observacion, fa_cliente.IdCliente, tb_persona.pe_nombreCompleto, SUM(fa_factura_det.vt_Subtotal) AS vt_Subtotal, SUM(fa_factura_det.vt_iva) 
                         AS vt_iva, SUM(fa_factura_det.vt_total) AS vt_total, ct_cbtecble.IdCbteCble, ct_cbtecble_tipo.tc_TipoCbte AS nom_tipo_cbte, ct_cbtecble.cb_Fecha, 
                         AVG(ct_cbtecble.cb_Valor) AS cb_Valor, ct_cbtecble.cb_Observacion
FROM            fa_factura_det INNER JOIN
                         fa_factura ON fa_factura_det.IdEmpresa = fa_factura.IdEmpresa AND fa_factura_det.IdSucursal = fa_factura.IdSucursal AND 
                         fa_factura_det.IdBodega = fa_factura.IdBodega AND fa_factura_det.IdCbteVta = fa_factura.IdCbteVta INNER JOIN
                         tb_sucursal ON fa_factura.IdEmpresa = tb_sucursal.IdEmpresa AND fa_factura.IdSucursal = tb_sucursal.IdSucursal INNER JOIN
                         fa_cliente ON fa_factura.IdEmpresa = fa_cliente.IdEmpresa AND fa_factura.IdCliente = fa_cliente.IdCliente INNER JOIN
                         tb_persona ON fa_cliente.IdPersona = tb_persona.IdPersona LEFT OUTER JOIN
                         fa_factura_x_ct_cbtecble INNER JOIN
                         ct_cbtecble ON fa_factura_x_ct_cbtecble.ct_IdEmpresa = ct_cbtecble.IdEmpresa AND fa_factura_x_ct_cbtecble.ct_IdTipoCbte = ct_cbtecble.IdTipoCbte AND 
                         fa_factura_x_ct_cbtecble.ct_IdCbteCble = ct_cbtecble.IdCbteCble INNER JOIN
                         ct_cbtecble_tipo ON ct_cbtecble.IdEmpresa = ct_cbtecble_tipo.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_tipo.IdTipoCbte ON 
                         fa_factura.IdEmpresa = fa_factura_x_ct_cbtecble.vt_IdEmpresa AND fa_factura.IdSucursal = fa_factura_x_ct_cbtecble.vt_IdSucursal AND 
                         fa_factura.IdBodega = fa_factura_x_ct_cbtecble.vt_IdBodega AND fa_factura.IdCbteVta = fa_factura_x_ct_cbtecble.vt_IdCbteVta
GROUP BY ct_cbtecble.IdCbteCble, ct_cbtecble_tipo.tc_TipoCbte, ct_cbtecble.cb_Fecha, ct_cbtecble.cb_Observacion, fa_factura.IdEmpresa, fa_factura.IdSucursal, 
                         fa_factura.IdCbteVta, fa_factura.CodCbteVta, fa_factura.vt_tipoDoc, fa_factura.vt_serie1, fa_factura.vt_serie2, fa_factura.vt_NumFactura, fa_factura.vt_fecha, 
                       fa_factura.vt_Observacion, tb_sucursal.Su_Descripcion, fa_cliente.IdCliente, tb_persona.pe_nombreCompleto,fa_factura.IdBodega
union

SELECT 
fa_notaCreDeb.IdEmpresa, fa_notaCreDeb.IdSucursal,fa_notaCreDeb.IdNota,fa_notaCreDeb.IdBodega
,tb_sucursal.Su_Descripcion,fa_notaCreDeb.CodNota,fa_notaCreDeb.CodDocumentoTipo+'-'+ fa_notaCreDeb.Serie1+'-'+ fa_notaCreDeb.Serie2+'-'+ fa_notaCreDeb.NumNota_Impresa
,fa_notaCreDeb.no_fecha, fa_notaCreDeb.sc_observacion
, fa_cliente.IdCliente, tb_persona.pe_nombreCompleto,
SUM(fa_notaCreDeb_det.sc_subtotal) AS sc_subtotal, 
                         SUM(fa_notaCreDeb_det.sc_iva) AS sc_iva, SUM(fa_notaCreDeb_det.sc_total) AS sc_total, 
ct_cbtecble.IdCbteCble, ct_cbtecble_tipo.tc_TipoCbte AS nom_tipo_cbte, 
                         ct_cbtecble.cb_Fecha, AVG(ct_cbtecble.cb_Valor) AS cb_Valor, ct_cbtecble.cb_Observacion
FROM            tb_persona INNER JOIN
                         fa_cliente ON tb_persona.IdPersona = fa_cliente.IdPersona INNER JOIN
                         fa_notaCreDeb ON fa_cliente.IdEmpresa = fa_notaCreDeb.IdEmpresa AND fa_cliente.IdCliente = fa_notaCreDeb.IdCliente INNER JOIN
                         fa_notaCreDeb_det ON fa_notaCreDeb.IdEmpresa = fa_notaCreDeb_det.IdEmpresa AND fa_notaCreDeb.IdSucursal = fa_notaCreDeb_det.IdSucursal AND 
                         fa_notaCreDeb.IdBodega = fa_notaCreDeb_det.IdBodega AND fa_notaCreDeb.IdNota = fa_notaCreDeb_det.IdNota INNER JOIN
                         tb_sucursal ON fa_notaCreDeb.IdEmpresa = tb_sucursal.IdEmpresa AND fa_notaCreDeb.IdSucursal = tb_sucursal.IdSucursal INNER JOIN
                         fa_notaCreDeb_x_ct_cbtecble ON fa_notaCreDeb.IdEmpresa = fa_notaCreDeb_x_ct_cbtecble.no_IdEmpresa AND 
                         fa_notaCreDeb.IdSucursal = fa_notaCreDeb_x_ct_cbtecble.no_IdSucursal AND fa_notaCreDeb.IdBodega = fa_notaCreDeb_x_ct_cbtecble.no_IdBodega AND 
                         fa_notaCreDeb.IdNota = fa_notaCreDeb_x_ct_cbtecble.no_IdNota INNER JOIN
                         ct_cbtecble INNER JOIN
                         ct_cbtecble_tipo ON ct_cbtecble.IdEmpresa = ct_cbtecble_tipo.IdEmpresa AND ct_cbtecble.IdTipoCbte = ct_cbtecble_tipo.IdTipoCbte ON 
                         fa_notaCreDeb_x_ct_cbtecble.ct_IdEmpresa = ct_cbtecble.IdEmpresa AND fa_notaCreDeb_x_ct_cbtecble.ct_IdEmpresa = ct_cbtecble.IdEmpresa AND 
                         fa_notaCreDeb_x_ct_cbtecble.ct_IdTipoCbte = ct_cbtecble.IdTipoCbte AND fa_notaCreDeb_x_ct_cbtecble.ct_IdTipoCbte = ct_cbtecble.IdTipoCbte AND 
                         fa_notaCreDeb_x_ct_cbtecble.ct_IdCbteCble = ct_cbtecble.IdCbteCble AND fa_notaCreDeb_x_ct_cbtecble.ct_IdCbteCble = ct_cbtecble.IdCbteCble
GROUP BY ct_cbtecble.IdCbteCble, ct_cbtecble_tipo.tc_TipoCbte, ct_cbtecble.cb_Fecha, ct_cbtecble.cb_Observacion, tb_sucursal.Su_Descripcion, fa_cliente.IdCliente, 
                         tb_persona.pe_nombreCompleto, fa_notaCreDeb.IdEmpresa, fa_notaCreDeb.IdSucursal, fa_notaCreDeb.IdBodega, fa_notaCreDeb.IdNota, 
                         fa_notaCreDeb.CodDocumentoTipo, fa_notaCreDeb.Serie1, fa_notaCreDeb.Serie2, fa_notaCreDeb.NumNota_Impresa, fa_notaCreDeb.no_fecha, 
                         fa_notaCreDeb.sc_observacion,fa_notaCreDeb.CodNota,fa_notaCreDeb.IdBodega