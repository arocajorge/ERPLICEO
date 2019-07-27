CREATE view [dbo].[vwfa_notaCreDeb_x_fa_factura_NotaDeb_x_NC]
as
SELECT        dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_nt, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_nt, 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_nt, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdNota_nt, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.secuencia, 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod, 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod, 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.Valor_Aplicado, 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.fecha_cruce, dbo.fa_factura.vt_serie1, dbo.fa_factura.vt_serie2, dbo.fa_factura.vt_NumFactura, dbo.fa_factura.vt_fecha, 
                         dbo.fa_factura.vt_fech_venc, dbo.fa_cliente.IdCliente, dbo.tb_persona.pe_nombreCompleto AS nom_Cliente, dbo.fa_factura.vt_Observacion, 
                         SUM(dbo.fa_factura_det.vt_total) AS vt_total, CASE WHEN fa_factura.vt_serie1 IS NULL THEN fa_factura.vt_tipoDoc + ' # ' + CAST(fa_factura.IdCbteVta AS varchar) 
                         ELSE (fa_factura.vt_tipoDoc + ' # ' + fa_factura.vt_serie1 + '-' + fa_factura.vt_serie2 + '-' + fa_factura.vt_NumFactura) END AS num_doc
FROM            dbo.fa_factura INNER JOIN
                         dbo.tb_persona INNER JOIN
                         dbo.fa_cliente ON dbo.tb_persona.IdPersona = dbo.fa_cliente.IdPersona ON dbo.fa_factura.IdEmpresa = dbo.fa_cliente.IdEmpresa AND 
                         dbo.fa_factura.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                         dbo.fa_factura_det ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_det.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_det.IdSucursal AND 
                         dbo.fa_factura.IdBodega = dbo.fa_factura_det.IdBodega AND dbo.fa_factura.IdCbteVta = dbo.fa_factura_det.IdCbteVta INNER JOIN
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb ON dbo.fa_factura.IdEmpresa = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod AND 
                         dbo.fa_factura.IdSucursal = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod AND 
                         dbo.fa_factura.IdBodega = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod AND 
                         dbo.fa_factura.IdCbteVta = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod AND 
                         dbo.fa_factura.vt_tipoDoc = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc
GROUP BY dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_nt, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_nt, 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_nt, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdNota_nt, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.secuencia, 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod, 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod, 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.Valor_Aplicado, dbo.fa_cliente.IdCliente, 
                         dbo.tb_persona.pe_nombreCompleto, dbo.fa_factura.vt_serie1, dbo.fa_factura.vt_serie2, dbo.fa_factura.vt_NumFactura, dbo.fa_factura.vt_fecha, 
                         dbo.fa_factura.vt_fech_venc, dbo.fa_factura.vt_Observacion, dbo.fa_factura.vt_tipoDoc, dbo.fa_factura.IdCbteVta, 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.fecha_cruce
UNION
SELECT        fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_nt, fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_nt, fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_nt, 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdNota_nt, fa_notaCreDeb_x_fa_factura_NotaDeb.secuencia, 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod, fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod, 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod, fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod, 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc, fa_notaCreDeb_x_fa_factura_NotaDeb.Valor_Aplicado, dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.fecha_cruce,fa_notaCreDeb.Serie1, fa_notaCreDeb.Serie2, 
                         fa_notaCreDeb.NumNota_Impresa, fa_notaCreDeb.no_fecha, fa_notaCreDeb.no_fecha_venc, fa_cliente.IdCliente, tb_persona.pe_nombreCompleto AS nom_Cliente, 
                         fa_notaCreDeb.sc_observacion, SUM(fa_notaCreDeb_det.sc_total) AS sc_total, CASE WHEN fa_notaCreDeb.Serie1 IS NULL 
                         THEN fa_notaCreDeb.CodDocumentoTipo + ' # ' + cast(fa_notaCreDeb.IdNota AS varchar) 
                         ELSE (fa_notaCreDeb.CodDocumentoTipo + ' # ' + fa_notaCreDeb.Serie1 + '-' + fa_notaCreDeb.Serie2 + '-' + fa_notaCreDeb.NumNota_Impresa) 
                         END AS num_doc
FROM            fa_notaCreDeb_x_fa_factura_NotaDeb INNER JOIN
                         tb_persona INNER JOIN
                         fa_cliente ON tb_persona.IdPersona = fa_cliente.IdPersona INNER JOIN
                         fa_notaCreDeb ON fa_cliente.IdCliente = fa_notaCreDeb.IdCliente AND fa_cliente.IdEmpresa = fa_notaCreDeb.IdEmpresa ON 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod = fa_notaCreDeb.IdEmpresa AND 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod = fa_notaCreDeb.IdSucursal AND 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod = fa_notaCreDeb.IdBodega AND 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod = fa_notaCreDeb.IdNota AND 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc = fa_notaCreDeb.CodDocumentoTipo INNER JOIN
                         fa_notaCreDeb_det ON fa_notaCreDeb.IdEmpresa = fa_notaCreDeb_det.IdEmpresa AND fa_notaCreDeb.IdSucursal = fa_notaCreDeb_det.IdSucursal AND 
                         fa_notaCreDeb.IdBodega = fa_notaCreDeb_det.IdBodega AND fa_notaCreDeb.IdNota = fa_notaCreDeb_det.IdNota
GROUP BY fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_nt, fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_nt, fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_nt, 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdNota_nt, fa_notaCreDeb_x_fa_factura_NotaDeb.secuencia, 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod, fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod, 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod, fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod, 
                         fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc, fa_notaCreDeb_x_fa_factura_NotaDeb.Valor_Aplicado, fa_notaCreDeb.Serie1, fa_notaCreDeb.Serie2, 
                         fa_notaCreDeb.NumNota_Impresa, fa_notaCreDeb.no_fecha, fa_notaCreDeb.no_fecha_venc, fa_cliente.IdCliente, tb_persona.pe_nombreCompleto, 
                         fa_notaCreDeb.sc_observacion, fa_notaCreDeb.CodDocumentoTipo, fa_notaCreDeb.IdNota,dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.fecha_cruce