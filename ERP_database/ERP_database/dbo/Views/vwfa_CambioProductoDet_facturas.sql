CREATE VIEW [dbo].[vwfa_CambioProductoDet_facturas]
AS
SELECT dbo.fa_factura_det.IdEmpresa, dbo.fa_factura_det.IdSucursal, dbo.fa_factura_det.IdBodega, dbo.fa_factura_det.IdCbteVta, dbo.fa_factura_det.Secuencia, dbo.fa_factura.vt_fecha, dbo.fa_factura_det.IdProducto, 
                  dbo.in_Producto.pr_descripcion, dbo.fa_factura_det.vt_cantidad - ISNULL(Cambios.CantidadCambio, 0) AS vt_cantidad, dbo.fa_factura.vt_NumFactura, tb_persona.pe_nombreCompleto AS NombreCliente, dbo.fa_factura.Estado
FROM     dbo.fa_factura INNER JOIN
                  dbo.fa_factura_det ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_det.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_det.IdSucursal AND dbo.fa_factura.IdBodega = dbo.fa_factura_det.IdBodega AND 
                  dbo.fa_factura.IdCbteVta = dbo.fa_factura_det.IdCbteVta INNER JOIN
                  dbo.in_Producto ON dbo.fa_factura_det.IdEmpresa = dbo.in_Producto.IdEmpresa AND dbo.fa_factura_det.IdProducto = dbo.in_Producto.IdProducto LEFT OUTER JOIN
                      (SELECT D.IdEmpresa, D.IdSucursal, D.IdBodega, D.IdCbteVta, D.SecuenciaFact, SUM(D.CantidadCambio) AS CantidadCambio
                       FROM      dbo.fa_CambioProductoDet AS D INNER JOIN
                                         dbo.fa_CambioProducto AS c ON c.IdEmpresa = D.IdEmpresa AND c.IdSucursal = D.IdSucursal AND c.IdBodega = D.IdBodega AND c.IdCambio = D.IdCambio
										 WHERE C.Estado = 1
                       GROUP BY D.IdEmpresa, D.IdSucursal, D.IdBodega, D.IdCbteVta, D.SecuenciaFact) AS Cambios ON dbo.fa_factura_det.IdEmpresa = Cambios.IdEmpresa AND dbo.fa_factura_det.IdSucursal = Cambios.IdSucursal AND 
                  dbo.fa_factura_det.IdBodega = Cambios.IdBodega AND dbo.fa_factura_det.IdCbteVta = Cambios.IdCbteVta AND dbo.fa_factura_det.Secuencia = Cambios.SecuenciaFact inner join
				  fa_cliente on fa_factura.IdEmpresa = fa_cliente.IdEmpresa and fa_cliente.IdCliente = fa_factura.IdCliente inner join 
				  tb_persona ON tb_persona.IdPersona = fa_cliente.IdPersona				  
WHERE  (dbo.fa_factura.Estado = 'A') AND (dbo.fa_factura_det.vt_cantidad - ISNULL(Cambios.CantidadCambio, 0) > 0)