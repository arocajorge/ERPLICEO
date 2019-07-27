CREATE VIEW web.vwfa_notaCreDeb
AS
SELECT        dbo.fa_notaCreDeb.IdEmpresa, dbo.fa_notaCreDeb.IdSucursal, dbo.fa_notaCreDeb.IdBodega, dbo.fa_notaCreDeb.IdNota, dbo.fa_notaCreDeb.CreDeb, dbo.fa_notaCreDeb.NumNota_Impresa, dbo.fa_notaCreDeb.no_fecha, 
                         dbo.fa_cliente_contactos.Nombres, SUM(dbo.fa_notaCreDeb_det.sc_subtotal) AS sc_subtotal, SUM(dbo.fa_notaCreDeb_det.sc_iva) AS sc_iva, SUM(dbo.fa_notaCreDeb_det.sc_total) AS sc_total, dbo.fa_notaCreDeb.Estado
FROM            dbo.fa_notaCreDeb INNER JOIN
                         dbo.fa_cliente_contactos ON dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_cliente_contactos.IdEmpresa AND dbo.fa_notaCreDeb.IdCliente = dbo.fa_cliente_contactos.IdCliente  LEFT OUTER JOIN
                         dbo.fa_notaCreDeb_det ON dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_notaCreDeb_det.IdEmpresa AND dbo.fa_notaCreDeb.IdSucursal = dbo.fa_notaCreDeb_det.IdSucursal AND 
                         dbo.fa_notaCreDeb.IdBodega = dbo.fa_notaCreDeb_det.IdBodega AND dbo.fa_notaCreDeb.IdNota = dbo.fa_notaCreDeb_det.IdNota
GROUP BY dbo.fa_notaCreDeb.IdEmpresa, dbo.fa_notaCreDeb.IdSucursal, dbo.fa_notaCreDeb.IdBodega, dbo.fa_notaCreDeb.IdNota, dbo.fa_notaCreDeb.CreDeb, dbo.fa_notaCreDeb.NumNota_Impresa, dbo.fa_notaCreDeb.no_fecha, 
                         dbo.fa_cliente_contactos.Nombres, dbo.fa_notaCreDeb.Estado