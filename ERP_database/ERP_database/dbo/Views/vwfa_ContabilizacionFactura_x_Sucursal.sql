create view [dbo].[vwfa_ContabilizacionFactura_x_Sucursal]
as
SELECT     IdEmpresa, IdSucursal, IdBodega, IdCbteVta, SUM(vt_Subtotal) AS Subtotal, SUM(vt_DescUnitario) AS Descuento, SUM(vt_iva) AS iva, SUM(vt_total) 
                      AS Total
FROM         fa_factura_det AS a
GROUP BY IdEmpresa, IdSucursal, IdBodega, IdCbteVta