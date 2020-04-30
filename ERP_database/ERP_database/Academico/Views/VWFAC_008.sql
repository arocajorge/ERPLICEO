CREATE VIEW [Academico].[VWFAC_008]
AS
SELECT dbo.fa_notaCreDeb_det.IdEmpresa, dbo.fa_notaCreDeb_det.IdSucursal, dbo.fa_notaCreDeb_det.IdBodega, dbo.fa_notaCreDeb_det.IdNota, dbo.fa_notaCreDeb_det.Secuencia, dbo.fa_notaCreDeb.Serie1, dbo.fa_notaCreDeb.Serie2, 
                  dbo.fa_notaCreDeb.Serie1 + '-' + dbo.fa_notaCreDeb.Serie2 + '-' + dbo.fa_notaCreDeb.NumNota_Impresa AS NumNota_Impresa, dbo.tb_persona.pe_nombreCompleto, dbo.tb_persona.pe_cedulaRuc, dbo.fa_notaCreDeb.no_fecha, 
                  ISNULL(cr_fa.vt_fecha, cr_nd.no_fecha) AS FechaDocumentoAplica, dbo.fa_notaCreDeb.sc_observacion, dbo.fa_notaCreDeb_det.IdProducto, dbo.in_Producto.pr_codigo, dbo.in_Producto.pr_descripcion, 
                  dbo.fa_notaCreDeb_det.sc_observacion AS DetalleAdicional, dbo.fa_notaCreDeb_det.sc_precioFinal, dbo.fa_cliente_contactos.Telefono, dbo.fa_cliente_contactos.Celular, dbo.fa_cliente_contactos.Correo, 
                  dbo.fa_cliente_contactos.Direccion, CASE WHEN fa_notaCreDeb_det.vt_por_iva > 0 THEN fa_notaCreDeb_det.sc_cantidad * fa_notaCreDeb_det.sc_Precio ELSE 0 END AS SubtotalIva, 
                  CASE WHEN fa_notaCreDeb_det.vt_por_iva = 0 THEN fa_notaCreDeb_det.sc_cantidad * fa_notaCreDeb_det.sc_Precio ELSE 0 END AS SubtotalSinIva, 
                  dbo.fa_notaCreDeb_det.sc_cantidad * dbo.fa_notaCreDeb_det.sc_Precio AS SubtotalAntesDescuento, dbo.fa_notaCreDeb_det.sc_subtotal, dbo.fa_notaCreDeb_det.sc_cantidad * dbo.fa_notaCreDeb_det.sc_descUni AS TotalDescuento, 
                  dbo.fa_notaCreDeb_det.sc_iva, dbo.fa_notaCreDeb_det.sc_total, CASE WHEN cr_fa.vt_NumFactura IS NULL 
                  THEN CASE WHEN cr_nd.NaturalezaNota = 'SRI' THEN cr_nd.Serie1 + '-' + cr_nd.Serie2 + '-' + cr_nd.NumNota_Impresa ELSE ISNULL(cr_nd.CodNota, CAST(cr_nd.IdNota AS varchar(20))) 
                  END ELSE cr_fa.vt_serie1 + '-' + cr_fa.vt_serie2 + '-' + cr_fa.vt_NumFactura END AS DocumentoAplicado, dbo.fa_notaCreDeb.NumAutorizacion, dbo.fa_notaCreDeb.Fecha_Autorizacion, dbo.fa_notaCreDeb.CreDeb, 
                  dbo.fa_notaCreDeb.CodDocumentoTipo, dbo.fa_notaCreDeb_det.sc_cantidad
FROM     dbo.fa_notaCreDeb INNER JOIN
                  dbo.fa_notaCreDeb_det ON dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_notaCreDeb_det.IdEmpresa AND dbo.fa_notaCreDeb.IdSucursal = dbo.fa_notaCreDeb_det.IdSucursal AND 
                  dbo.fa_notaCreDeb.IdBodega = dbo.fa_notaCreDeb_det.IdBodega AND dbo.fa_notaCreDeb.IdNota = dbo.fa_notaCreDeb_det.IdNota INNER JOIN
                  dbo.fa_cliente ON dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_notaCreDeb.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                  dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.fa_notaCreDeb_x_fa_factura_NotaDeb ON dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_nt AND 
                  dbo.fa_notaCreDeb.IdSucursal = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_nt AND dbo.fa_notaCreDeb.IdBodega = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_nt AND 
                  dbo.fa_notaCreDeb.IdNota = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdNota_nt INNER JOIN
                  dbo.in_Producto ON dbo.fa_notaCreDeb_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.fa_notaCreDeb_det.IdProducto = dbo.in_Producto.IdProducto INNER JOIN
                  dbo.fa_cliente_contactos ON 1 = dbo.fa_cliente_contactos.IdContacto AND dbo.fa_notaCreDeb.IdCliente = dbo.fa_cliente_contactos.IdCliente AND 
                  dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_cliente_contactos.IdEmpresa LEFT OUTER JOIN
                  dbo.fa_factura AS cr_fa ON dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod = cr_fa.IdEmpresa AND dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod = cr_fa.IdSucursal AND 
                  dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod = cr_fa.IdBodega AND dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod = cr_fa.IdCbteVta AND 
                  dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc = cr_fa.vt_tipoDoc LEFT OUTER JOIN
                  dbo.fa_notaCreDeb AS cr_nd ON dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod = cr_nd.IdEmpresa AND dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod = cr_nd.IdSucursal AND 
                  dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod = cr_nd.IdBodega AND dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod = cr_nd.IdNota AND 
                  dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc = cr_nd.CodDocumentoTipo