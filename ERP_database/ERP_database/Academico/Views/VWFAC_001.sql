create view academico.VWFAC_001 AS
SELECT d.IdEmpresa, d.IdSucursal, d.IdBodega, d.IdCbteVta, d.Secuencia, d.IdProducto, pro.pr_descripcion, d.vt_cantidad, d.vt_Precio, d.vt_cantidad * d.vt_Precio AS SubtotalSinDscto, d.vt_Subtotal AS SubtotalConDscto, d.vt_iva, 
                  d.vt_Subtotal + d.vt_iva AS vt_Total, c.vt_fecha, c.vt_serie1 + '-' + c.vt_serie2 + '-' + c.vt_NumFactura AS vt_NumFactura, per.pe_nombreCompleto AS cli_Nombre, per.pe_cedulaRuc AS cli_cedulaRuc, con.Direccion AS cli_direccion, 
                  con.Telefono AS cli_Telefonos, con.Correo AS cli_correo, su.Su_Descripcion, su.Su_Telefonos, su.Su_Direccion, cat.Nombre AS FormaDePago, c.IdCatalogo_FormaPago, c.vt_autorizacion, c.Fecha_Autorizacion, c.vt_Observacion, 
                  dbo.fa_factura_resumen.SubtotalIVASinDscto, dbo.fa_factura_resumen.SubtotalSinIVASinDscto, dbo.fa_factura_resumen.SubtotalSinDscto AS T_SubtotalSinDscto, dbo.fa_factura_resumen.Descuento, 
                  dbo.fa_factura_resumen.SubtotalIVAConDscto, dbo.fa_factura_resumen.SubtotalSinIVAConDscto, dbo.fa_factura_resumen.SubtotalConDscto AS T_SubtotalConDscto, dbo.fa_factura_resumen.ValorIVA, dbo.fa_factura_resumen.Total, 
                  dbo.fa_factura_resumen.ValorEfectivo, dbo.fa_factura_resumen.Cambio, d.vt_detallexItems
FROM     dbo.fa_cliente_contactos AS con INNER JOIN
                  dbo.fa_factura AS c ON con.IdEmpresa = c.IdEmpresa AND con.IdCliente = c.IdCliente INNER JOIN
                  dbo.fa_factura_det AS d ON c.IdEmpresa = d.IdEmpresa AND c.IdSucursal = d.IdSucursal AND c.IdBodega = d.IdBodega AND c.IdCbteVta = d.IdCbteVta INNER JOIN
                  dbo.in_Producto AS pro ON d.IdEmpresa = pro.IdEmpresa AND d.IdProducto = pro.IdProducto INNER JOIN
                  dbo.fa_cliente AS cli ON con.IdEmpresa = cli.IdEmpresa AND con.IdCliente = cli.IdCliente INNER JOIN
                  dbo.tb_persona AS per ON cli.IdPersona = per.IdPersona INNER JOIN
                  dbo.tb_sucursal AS su ON c.IdEmpresa = su.IdEmpresa AND c.IdSucursal = su.IdSucursal LEFT OUTER JOIN
                  dbo.fa_factura_resumen ON c.IdEmpresa = dbo.fa_factura_resumen.IdEmpresa AND c.IdSucursal = dbo.fa_factura_resumen.IdSucursal AND c.IdBodega = dbo.fa_factura_resumen.IdBodega AND 
                  c.IdCbteVta = dbo.fa_factura_resumen.IdCbteVta LEFT OUTER JOIN
                  dbo.fa_catalogo AS cat ON c.IdCatalogo_FormaPago = cat.IdCatalogo