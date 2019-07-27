CREATE VIEW vwfa_factura_total_cobrado
AS 
SELECT        dbo.fa_factura.IdEmpresa, dbo.fa_factura.IdSucursal, dbo.fa_factura.IdBodega, dbo.fa_factura.IdCbteVta, SUM(dbo.cxc_cobro_det.dc_ValorPago) AS total_cobrado
FROM            dbo.cxc_cobro_det INNER JOIN
                         dbo.fa_factura ON dbo.cxc_cobro_det.IdEmpresa = dbo.fa_factura.IdEmpresa AND dbo.cxc_cobro_det.IdSucursal = dbo.fa_factura.IdSucursal AND dbo.cxc_cobro_det.IdBodega_Cbte = dbo.fa_factura.IdBodega AND
                          dbo.cxc_cobro_det.IdCbte_vta_nota = dbo.fa_factura.IdCbteVta AND dbo.cxc_cobro_det.dc_TipoDocumento = dbo.fa_factura.vt_tipoDoc
GROUP BY dbo.fa_factura.IdEmpresa, dbo.fa_factura.IdSucursal, dbo.fa_factura.IdBodega, dbo.fa_factura.IdCbteVta