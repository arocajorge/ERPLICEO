CREATE VIEW [web].[vwcxc_cobro_det]
AS
SELECT        dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdCobro, dbo.cxc_cobro_det.secuencial, dbo.cxc_cobro_det.dc_TipoDocumento, dbo.cxc_cobro_det.IdBodega_Cbte, 
                         dbo.cxc_cobro_det.IdCbte_vta_nota, dbo.cxc_cobro_det.dc_ValorPago, dbo.cxc_cobro_det.IdCobro_tipo, dbo.cxc_cobro_det.dc_TipoDocumento + '-' + CAST(CAST(ISNULL(dbo.fa_factura.vt_NumFactura, 
                         dbo.fa_notaCreDeb.NumNota_Impresa) AS int) AS VARCHAR(20)) AS vt_NumFactura, ISNULL(dbo.fa_factura.vt_Observacion, dbo.fa_notaCreDeb.sc_observacion) AS vt_Observacion, ISNULL(dbo.fa_factura.CodCbteVta, 
                         dbo.fa_notaCreDeb.CodNota) AS CodDoc, ISNULL(SUM(dbo.fa_factura_det.vt_Subtotal), 0) + ISNULL(SUM(dbo.fa_notaCreDeb_det.sc_subtotal), 0) AS vt_Subtotal, ISNULL(SUM(dbo.fa_factura_det.vt_iva), 0) 
                         + ISNULL(SUM(dbo.fa_notaCreDeb_det.sc_iva), 0) AS vt_iva, ISNULL(SUM(dbo.fa_factura_det.vt_total), 0) + ISNULL(SUM(dbo.fa_notaCreDeb_det.sc_total), 0) AS vt_total, ISNULL(cobro.ValorCobrado, 0) AS ValorCobrado, 
                         ISNULL(SUM(dbo.fa_factura_det.vt_total), 0) + ISNULL(SUM(dbo.fa_notaCreDeb_det.sc_total), 0) - ISNULL(cobro.ValorCobrado, 0) AS saldo, ISNULL(SUM(dbo.fa_factura_det.vt_total), 0) 
                         + ISNULL(SUM(dbo.fa_notaCreDeb_det.sc_total), 0) - ISNULL(cobro.ValorCobrado, 0) + dbo.cxc_cobro_det.dc_ValorPago AS saldo_sin_cobro, ISNULL(dbo.fa_factura.vt_fecha, dbo.fa_notaCreDeb.no_fecha) AS vt_fecha, 
                         ISNULL(dbo.fa_factura.vt_fech_venc, dbo.fa_notaCreDeb.no_fecha_venc) AS vt_fech_venc, ISNULL(dbo.fa_factura.IdCliente, dbo.fa_notaCreDeb.IdCliente) AS IdCliente,
						 ISNULL(fa_cliente_contactos.Nombres,tb_persona.pe_nombreCompleto) pe_nombreCompleto
FROM            dbo.tb_persona INNER JOIN
                         dbo.fa_notaCreDeb_det INNER JOIN
                         dbo.fa_notaCreDeb ON dbo.fa_notaCreDeb_det.IdEmpresa = dbo.fa_notaCreDeb.IdEmpresa AND dbo.fa_notaCreDeb_det.IdSucursal = dbo.fa_notaCreDeb.IdSucursal AND 
                         dbo.fa_notaCreDeb_det.IdBodega = dbo.fa_notaCreDeb.IdBodega AND dbo.fa_notaCreDeb_det.IdNota = dbo.fa_notaCreDeb.IdNota INNER JOIN
                         dbo.fa_cliente ON dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_notaCreDeb.IdCliente = dbo.fa_cliente.IdCliente ON dbo.tb_persona.IdPersona = dbo.fa_cliente.IdPersona RIGHT OUTER JOIN
                         dbo.cxc_cobro_det ON dbo.fa_notaCreDeb.IdEmpresa = dbo.cxc_cobro_det.IdEmpresa AND dbo.fa_notaCreDeb.IdSucursal = dbo.cxc_cobro_det.IdSucursal AND 
                         dbo.fa_notaCreDeb.IdBodega = dbo.cxc_cobro_det.IdBodega_Cbte AND dbo.fa_notaCreDeb.IdNota = dbo.cxc_cobro_det.IdCbte_vta_nota AND 
                         dbo.fa_notaCreDeb.CodDocumentoTipo = dbo.cxc_cobro_det.dc_TipoDocumento LEFT OUTER JOIN
                         dbo.fa_cliente_contactos INNER JOIN
                         dbo.fa_factura INNER JOIN
                         dbo.fa_factura_det ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_det.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_det.IdSucursal AND dbo.fa_factura.IdBodega = dbo.fa_factura_det.IdBodega AND 
                         dbo.fa_factura.IdCbteVta = dbo.fa_factura_det.IdCbteVta ON dbo.fa_cliente_contactos.IdEmpresa = dbo.fa_factura.IdEmpresa AND dbo.fa_cliente_contactos.IdCliente = dbo.fa_factura.IdCliente 
                         ON dbo.cxc_cobro_det.IdEmpresa = dbo.fa_factura.IdEmpresa AND dbo.cxc_cobro_det.IdSucursal = dbo.fa_factura.IdSucursal AND 
                         dbo.cxc_cobro_det.IdBodega_Cbte = dbo.fa_factura.IdBodega AND dbo.cxc_cobro_det.IdCbte_vta_nota = dbo.fa_factura.IdCbteVta AND dbo.cxc_cobro_det.dc_TipoDocumento = dbo.fa_factura.vt_tipoDoc LEFT OUTER JOIN
                             (SELECT        IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento, SUM(dc_ValorPago) AS ValorCobrado
                               FROM            dbo.cxc_cobro_det AS det
                               WHERE        (estado = 'A')
                               GROUP BY IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento) AS cobro ON cobro.IdEmpresa = dbo.cxc_cobro_det.IdEmpresa AND cobro.IdSucursal = dbo.cxc_cobro_det.IdSucursal AND 
                         cobro.IdBodega_Cbte = dbo.cxc_cobro_det.IdBodega_Cbte AND cobro.IdCbte_vta_nota = dbo.cxc_cobro_det.IdCbte_vta_nota AND cobro.dc_TipoDocumento = dbo.cxc_cobro_det.dc_TipoDocumento
GROUP BY dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdCobro, dbo.cxc_cobro_det.secuencial, dbo.cxc_cobro_det.dc_TipoDocumento, dbo.cxc_cobro_det.IdBodega_Cbte, 
                         dbo.cxc_cobro_det.IdCbte_vta_nota, dbo.cxc_cobro_det.dc_ValorPago, dbo.cxc_cobro_det.IdCobro_tipo, dbo.fa_factura.vt_NumFactura, dbo.fa_notaCreDeb.NumNota_Impresa, dbo.fa_factura.vt_Observacion, 
                         dbo.fa_notaCreDeb.sc_observacion, dbo.fa_factura.CodCbteVta, dbo.fa_notaCreDeb.CodNota, dbo.fa_factura.vt_fecha, dbo.fa_notaCreDeb.no_fecha, dbo.fa_factura.vt_fech_venc, dbo.fa_notaCreDeb.no_fecha_venc, 
                         dbo.fa_factura.IdCliente, dbo.fa_notaCreDeb.IdCliente, dbo.cxc_cobro_det.dc_ValorPago, cobro.ValorCobrado, fa_cliente_contactos.Nombres,tb_persona.pe_nombreCompleto