CREATE VIEW vwfa_factura_SubTotal_Iva_total
AS
SELECT        dbo.fa_factura.IdEmpresa, dbo.fa_factura.IdSucursal, dbo.fa_factura.IdBodega, dbo.fa_factura.IdCbteVta, SUM(dbo.fa_factura_det.vt_Subtotal) AS vt_Subtotal, 
                         SUM(dbo.fa_factura_det.vt_iva) AS vt_iva, SUM(dbo.fa_factura_det.vt_total) AS vt_total, MAX(dbo.fa_factura_det.vt_por_iva) AS vt_por_iva
FROM            dbo.fa_factura INNER JOIN
                         dbo.fa_factura_det ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_det.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_det.IdSucursal AND 
                         dbo.fa_factura.IdBodega = dbo.fa_factura_det.IdBodega AND dbo.fa_factura.IdCbteVta = dbo.fa_factura_det.IdCbteVta
GROUP BY dbo.fa_factura.IdEmpresa, dbo.fa_factura.IdSucursal, dbo.fa_factura.IdBodega, dbo.fa_factura.IdCbteVta