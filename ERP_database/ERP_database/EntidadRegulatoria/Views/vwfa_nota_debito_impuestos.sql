
create view [EntidadRegulatoria].[vwfa_nota_debito_impuestos] as 
SELECT        IdEmpresa, IdSucursal, IdBodega, IdNota, CAST(SUM(sc_subtotal) AS numeric(10, 2)) AS Base_imponible, CAST(SUM(sc_iva) AS numeric(10, 2)) AS impuesto, vt_por_iva
FROM            dbo.fa_notaCreDeb_det
GROUP BY IdEmpresa, IdSucursal, IdBodega, IdNota, vt_por_iva