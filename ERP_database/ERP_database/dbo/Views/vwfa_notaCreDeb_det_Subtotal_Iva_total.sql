CREATE view [dbo].[vwfa_notaCreDeb_det_Subtotal_Iva_total]
as
SELECT        IdEmpresa, IdSucursal, IdBodega, IdNota, SUM(sc_subtotal) AS sc_subtotal, SUM(sc_iva) AS sc_iva, SUM(sc_total) AS sc_total, MAX(vt_por_iva) AS vt_por_iva
FROM            dbo.fa_notaCreDeb_det
GROUP BY IdEmpresa, IdSucursal, IdBodega, IdNota