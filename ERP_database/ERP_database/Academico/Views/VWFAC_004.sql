create view academico.VWFAC_004 AS
SELECT dbo.fa_factura_det.IdEmpresa, dbo.tb_sucursal.Su_CodigoEstablecimiento, dbo.tb_sucursal.Su_Descripcion, dbo.tb_sucursal.Su_Direccion, dbo.tb_sucursal.Su_Telefonos, dbo.fa_factura_det.IdSucursal, dbo.fa_factura.IdCliente, 
                  dbo.tb_persona.pe_nombreCompleto AS nombre_cliente, dbo.tb_persona.pe_cedulaRuc AS ced_ruc_cliente, dbo.tb_persona.pe_direccion AS direccion_cliente, dbo.tb_persona.pe_celular AS celular_cliente, 
                  dbo.tb_persona.pe_telfono_Contacto AS telefono_cliente, dbo.fa_factura_det.IdProforma, dbo.fa_factura_det.Secuencia, dbo.fa_TerminoPago.nom_TerminoPago, dbo.fa_factura.vt_plazo, dbo.fa_factura.CodCbteVta, 
                  dbo.fa_factura.vt_fecha, dbo.fa_factura.Estado, dbo.fa_Vendedor.Codigo, dbo.fa_Vendedor.Ve_Vendedor, dbo.fa_factura_det.vt_cantidad, dbo.fa_factura_det.vt_Precio, dbo.fa_factura_det.vt_PorDescUnitario, 
                  dbo.fa_factura_det.vt_DescUnitario, dbo.fa_factura_det.vt_PrecioFinal, dbo.fa_factura_det.vt_Subtotal, dbo.fa_factura_det.vt_por_iva, dbo.fa_factura_det.vt_iva, dbo.fa_factura_det.vt_Subtotal + dbo.fa_factura_det.vt_iva AS pd_total, 
                  dbo.in_Producto.pr_observacion, dbo.fa_factura_det.IdProducto, dbo.fa_factura.vt_Observacion, dbo.fa_factura_det.IdBodega, dbo.fa_factura_det.IdCbteVta, dbo.in_Producto.pr_descripcion, 
                  dbo.fa_factura.vt_serie1 + '-' + dbo.fa_factura.vt_serie2 + '-' + dbo.fa_factura.vt_NumFactura AS vt_NumFactura, dbo.fa_factura_resumen.SubtotalIVASinDscto, dbo.fa_factura_resumen.SubtotalSinIVASinDscto, 
                  dbo.fa_factura_resumen.SubtotalSinDscto, dbo.fa_factura_resumen.Descuento, dbo.fa_factura_resumen.SubtotalIVAConDscto, dbo.fa_factura_resumen.SubtotalSinIVAConDscto, dbo.fa_factura_resumen.SubtotalConDscto, 
                  dbo.fa_factura_resumen.ValorIVA, dbo.fa_factura_resumen.Total, dbo.fa_factura_resumen.ValorEfectivo, dbo.fa_factura_resumen.Cambio
FROM     dbo.fa_factura INNER JOIN
                  dbo.fa_factura_det ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_det.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_det.IdSucursal AND dbo.fa_factura.IdCbteVta = dbo.fa_factura_det.IdCbteVta AND 
                  dbo.fa_factura.IdBodega = dbo.fa_factura_det.IdBodega INNER JOIN
                  dbo.fa_cliente ON dbo.fa_cliente.IdCliente = dbo.fa_factura.IdCliente AND dbo.fa_factura.IdEmpresa = dbo.fa_cliente.IdEmpresa INNER JOIN
                  dbo.tb_persona ON dbo.tb_persona.IdPersona = dbo.fa_cliente.IdPersona INNER JOIN
                  dbo.fa_Vendedor ON dbo.fa_factura.IdEmpresa = dbo.fa_Vendedor.IdEmpresa AND dbo.fa_factura.IdVendedor = dbo.fa_Vendedor.IdVendedor INNER JOIN
                  dbo.fa_TerminoPago ON dbo.fa_factura.vt_tipo_venta = dbo.fa_TerminoPago.IdTerminoPago LEFT OUTER JOIN
                  dbo.fa_factura_resumen ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_resumen.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_resumen.IdSucursal AND dbo.fa_factura.IdBodega = dbo.fa_factura_resumen.IdBodega AND 
                  dbo.fa_factura.IdCbteVta = dbo.fa_factura_resumen.IdCbteVta LEFT OUTER JOIN
                  dbo.in_Producto ON dbo.fa_factura_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.fa_factura_det.IdProducto = dbo.in_Producto.IdProducto LEFT OUTER JOIN
                  dbo.tb_sucursal ON dbo.tb_sucursal.IdSucursal = dbo.fa_factura_det.IdSucursal AND dbo.tb_sucursal.IdEmpresa = dbo.fa_factura_det.IdEmpresa