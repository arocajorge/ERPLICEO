CREATE VIEW [web].[vwfa_factura_x_fa_guia_remision]
AS 
SELECT        dbo.fa_factura.IdEmpresa, dbo.fa_factura.IdSucursal, dbo.fa_factura.IdBodega, dbo.fa_factura.IdCbteVta, dbo.fa_factura.CodCbteVta, dbo.fa_factura.vt_tipoDoc, dbo.fa_factura.vt_serie1, dbo.fa_factura.vt_serie2, 
                         dbo.fa_factura.vt_NumFactura, dbo.fa_factura_x_fa_guia_remision.gi_IdGuiaRemision
FROM            dbo.fa_factura INNER JOIN
                         dbo.fa_factura_x_fa_guia_remision ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_x_fa_guia_remision.fa_IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_x_fa_guia_remision.fa_IdSucursal AND 
                         dbo.fa_factura.IdBodega = dbo.fa_factura_x_fa_guia_remision.fa_IdBodega AND dbo.fa_factura.IdCbteVta = dbo.fa_factura_x_fa_guia_remision.fa_IdCbteVta