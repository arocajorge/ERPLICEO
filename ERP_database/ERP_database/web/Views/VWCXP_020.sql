CREATE VIEW [web].[VWCXP_020]
AS
SELECT a.IdEmpresa, a.IdTipoCbte_Ogiro, a.IdCbteCble_Ogiro, c.Descripcion AS NomDocumento, a.co_serie, a.co_factura, a.Num_Autorizacion, a.fecha_autorizacion, f.Su_Descripcion, f.Su_Direccion, g.pe_nombreCompleto, g.pe_cedulaRuc, 
                  a.co_FechaFactura, a.co_observacion, i.IdFormaPago, i.nom_FormaPago, a.co_subtotal_iva, a.co_subtotal_siniva, a.co_subtotal_iva + a.co_subtotal_siniva AS co_subtotal, a.co_total, a.co_valoriva, j.pr_descripcion, j.pr_codigo, b.Subtotal, 
                  b.DescuentoUni * b.Cantidad AS Descuento, b.ValorIva, b.Total AS TotalDetalle, e.pr_direccion, e.pr_correo, b.Cantidad, b.CostoUni
FROM     dbo.cp_orden_giro AS a INNER JOIN
                  dbo.cp_orden_giro_det AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdTipoCbte_Ogiro = b.IdTipoCbte_Ogiro AND a.IdCbteCble_Ogiro = b.IdCbteCble_Ogiro INNER JOIN
                  dbo.cp_TipoDocumento AS c ON a.IdOrden_giro_Tipo = c.CodTipoDocumento INNER JOIN
                  dbo.tb_sis_Documento_Tipo_Talonario AS d ON c.Codigo = d.CodDocumentoTipo AND a.co_serie = d.Establecimiento + '-' + d.PuntoEmision AND a.co_factura = d.NumDocumento AND a.IdEmpresa = d.IdEmpresa INNER JOIN
                  dbo.cp_proveedor AS e ON e.IdEmpresa = a.IdEmpresa AND e.IdProveedor = a.IdProveedor INNER JOIN
                  dbo.tb_sucursal AS f ON a.IdEmpresa = f.IdEmpresa AND a.IdSucursal = f.IdSucursal INNER JOIN
                  dbo.tb_persona AS g ON e.IdPersona = g.IdPersona LEFT OUTER JOIN
                  dbo.cp_orden_giro_pagos_sri AS h ON a.IdEmpresa = h.IdEmpresa AND a.IdTipoCbte_Ogiro = h.IdTipoCbte_Ogiro AND a.IdCbteCble_Ogiro = h.IdCbteCble_Ogiro LEFT OUTER JOIN
                  dbo.fa_formaPago AS i ON h.codigo_pago_sri = i.IdFormaPago INNER JOIN
                  dbo.in_Producto AS j ON b.IdEmpresa = j.IdEmpresa AND b.IdProducto = j.IdProducto