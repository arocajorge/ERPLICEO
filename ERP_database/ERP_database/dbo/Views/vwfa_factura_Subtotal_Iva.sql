create view vwfa_factura_Subtotal_Iva
as
SELECT        IdEmpresa, IdSucursal, IdBodega, IdCbteVta, SUM(vt_Subtotal) AS vt_Subtotal, SUM(vt_iva) AS vt_iva
FROM            fa_factura_det
GROUP BY IdEmpresa, IdSucursal, IdBodega, IdCbteVta