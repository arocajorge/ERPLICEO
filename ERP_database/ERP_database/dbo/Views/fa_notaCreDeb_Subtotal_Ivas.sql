create view fa_notaCreDeb_Subtotal_Ivas
as
SELECT        IdEmpresa, IdSucursal, IdBodega, IdNota, SUM(sc_subtotal) AS sc_subtotal, SUM(sc_iva) AS sc_iva
FROM            fa_notaCreDeb_det
GROUP BY IdEmpresa, IdSucursal, IdBodega, IdNota