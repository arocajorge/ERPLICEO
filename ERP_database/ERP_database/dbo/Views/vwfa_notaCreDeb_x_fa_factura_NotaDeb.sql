CREATE view [dbo].[vwfa_notaCreDeb_x_fa_factura_NotaDeb]
as
SELECT        dbo.fa_factura.IdEmpresa, dbo.fa_factura.IdSucursal, dbo.fa_factura.IdBodega, dbo.fa_factura.IdCbteVta, dbo.fa_factura.vt_tipoDoc, dbo.fa_factura.vt_serie1, 
                         dbo.fa_factura.vt_serie2, dbo.fa_factura.vt_NumFactura, dbo.fa_factura.IdCliente, dbo.tb_persona.pe_nombreCompleto AS nom_Cliente, dbo.fa_factura.vt_fecha, 
                         dbo.fa_factura.vt_fech_venc, dbo.fa_factura.vt_Observacion, SUM(dbo.fa_factura_det.vt_total) AS vt_total, CASE WHEN fa_factura.vt_serie1 IS NULL 
                         THEN fa_factura.vt_tipoDoc + ' # ' + cast(fa_factura.IdCbteVta as varchar) ELSE (fa_factura.vt_tipoDoc + ' # ' + fa_factura.vt_serie1 + '-' + fa_factura.vt_serie2 + '-' + fa_factura.vt_NumFactura) END AS num_doc
FROM            dbo.fa_factura INNER JOIN
                         dbo.fa_cliente ON dbo.fa_factura.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_factura.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                         dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.fa_factura_det ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_det.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_det.IdSucursal AND 
                         dbo.fa_factura.IdBodega = dbo.fa_factura_det.IdBodega AND dbo.fa_factura.IdCbteVta = dbo.fa_factura_det.IdCbteVta
GROUP BY dbo.fa_factura.IdEmpresa, dbo.fa_factura.IdSucursal, dbo.fa_factura.IdBodega, dbo.fa_factura.IdCbteVta, dbo.fa_factura.vt_tipoDoc, dbo.fa_factura.vt_serie1, 
                         dbo.fa_factura.vt_serie2, dbo.fa_factura.vt_NumFactura, dbo.fa_factura.IdCliente, dbo.tb_persona.pe_nombreCompleto, dbo.fa_factura.vt_fecha, 
                         dbo.fa_factura.vt_fech_venc, dbo.fa_factura.vt_Observacion, fa_factura.vt_tipoDoc,fa_factura.IdCbteVta
UNION
SELECT        dbo.fa_notaCreDeb.IdEmpresa, dbo.fa_notaCreDeb.IdSucursal, dbo.fa_notaCreDeb.IdBodega, dbo.fa_notaCreDeb.IdNota, dbo.fa_notaCreDeb.CodDocumentoTipo, 
                         dbo.fa_notaCreDeb.Serie1, dbo.fa_notaCreDeb.Serie2, dbo.fa_notaCreDeb.NumNota_Impresa, dbo.fa_notaCreDeb.IdCliente, dbo.tb_persona.pe_nombreCompleto, 
                         dbo.fa_notaCreDeb.no_fecha, dbo.fa_notaCreDeb.no_fecha_venc, dbo.fa_notaCreDeb.sc_observacion, SUM(dbo.fa_notaCreDeb_det.sc_total) AS Expr1, 
                         CASE WHEN fa_notaCreDeb.Serie1 IS NULL 
                         THEN fa_notaCreDeb.CodDocumentoTipo + ' # ' + cast(fa_notaCreDeb.IdNota as varchar)ELSE (fa_notaCreDeb.CodDocumentoTipo + ' # ' + fa_notaCreDeb.Serie1 + '-' + fa_notaCreDeb.Serie2 + '-' + fa_notaCreDeb.NumNota_Impresa) 
                         END AS num_doc
FROM            dbo.fa_cliente INNER JOIN
                         dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.fa_notaCreDeb ON dbo.fa_cliente.IdEmpresa = dbo.fa_notaCreDeb.IdEmpresa AND dbo.fa_cliente.IdCliente = dbo.fa_notaCreDeb.IdCliente LEFT OUTER JOIN
                         dbo.fa_notaCreDeb_det ON dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_notaCreDeb_det.IdEmpresa AND 
                         dbo.fa_notaCreDeb.IdSucursal = dbo.fa_notaCreDeb_det.IdSucursal AND dbo.fa_notaCreDeb.IdBodega = dbo.fa_notaCreDeb_det.IdBodega AND 
                         dbo.fa_notaCreDeb.IdNota = dbo.fa_notaCreDeb_det.IdNota
WHERE        (dbo.fa_notaCreDeb.CreDeb = 'D')
GROUP BY dbo.fa_notaCreDeb.IdEmpresa, dbo.fa_notaCreDeb.IdSucursal, dbo.fa_notaCreDeb.IdBodega, dbo.fa_notaCreDeb.IdNota, dbo.fa_notaCreDeb.CodDocumentoTipo, 
                         dbo.fa_notaCreDeb.Serie1, dbo.fa_notaCreDeb.Serie2, dbo.fa_notaCreDeb.NumNota_Impresa, dbo.fa_notaCreDeb.IdCliente, dbo.tb_persona.pe_nombreCompleto, 
                         dbo.fa_notaCreDeb.no_fecha, dbo.fa_notaCreDeb.no_fecha_venc, dbo.fa_notaCreDeb.sc_observacion, fa_notaCreDeb.CodDocumentoTipo,
						 fa_notaCreDeb.IdNota