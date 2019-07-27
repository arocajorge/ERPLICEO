CREATE VIEW web.VWFAC_012
AS
SELECT d.IdEmpresa, d.IdSucursal, d.IdBodega, d.IdCambio, d.Secuencia, d.IdCbteVta, d.SecuenciaFact, c.Fecha, c.Observacion, c.Estado, c.IdMovi_inven_tipo, c.IdNumMovi, pf.pr_descripcion AS pr_descripcionFact, 
                  pc.pr_descripcion AS pr_descripcionCambio, d.CantidadFact, d.CantidadCambio, fc.vt_NumFactura, fcc.Nombres AS NombreCliente, su.Su_Descripcion, bo.bo_Descripcion
FROM     fa_factura AS fc INNER JOIN
                  fa_CambioProducto AS c INNER JOIN
                  fa_CambioProductoDet AS d ON c.IdEmpresa = d.IdEmpresa AND c.IdSucursal = d.IdSucursal AND c.IdBodega = d.IdBodega AND c.IdCambio = d.IdCambio INNER JOIN
                  fa_factura_det AS fd ON d.IdEmpresa = fd.IdEmpresa AND d.IdSucursal = fd.IdSucursal AND d.IdBodega = fd.IdBodega AND d.IdCbteVta = fd.IdCbteVta AND d.SecuenciaFact = fd.Secuencia ON fc.IdEmpresa = fd.IdEmpresa AND 
                  fc.IdSucursal = fd.IdSucursal AND fc.IdBodega = fd.IdBodega AND fc.IdCbteVta = fd.IdCbteVta LEFT OUTER JOIN
                  tb_sucursal AS su INNER JOIN
                  tb_bodega AS bo ON su.IdEmpresa = bo.IdEmpresa AND su.IdSucursal = bo.IdSucursal ON d.IdEmpresa = bo.IdEmpresa AND d.IdSucursal = bo.IdSucursal AND d.IdBodega = bo.IdBodega LEFT OUTER JOIN
                  fa_cliente_contactos AS fcc ON fc.IdEmpresa = fcc.IdEmpresa AND fc.IdCliente = fcc.IdCliente LEFT OUTER JOIN
                  in_Producto AS pc ON d.IdEmpresa = pc.IdEmpresa AND d.IdProductoCambio = pc.IdProducto LEFT OUTER JOIN
                  in_Producto AS pf ON d.IdEmpresa = pf.IdEmpresa AND d.IdProductoFact = pf.IdProducto