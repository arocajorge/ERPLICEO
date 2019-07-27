CREATE view [dbo].[vwfa_notaCreDeb_det_subtotal_iva_0_totales]
as
SELECT        IdEmpresa, IdSucursal, IdBodega, IdNota, ROUND(ISNULL(SUM(SubTotal_0), 0), 2) AS SubTotal_0, ROUND(ISNULL(SUM(SubTotal_Iva), 0), 2) AS SubTotal_Iva,
                         ROUND(ISNULL(SUM(sc_iva), 0), 2) AS sc_iva, ROUND(ISNULL(SUM(SubTotal_0), 0), 2)+ ROUND(ISNULL(SUM(SubTotal_Iva), 0), 2)+ROUND(ISNULL(SUM(sc_iva), 0), 2) sc_total
FROM            (SELECT        IdEmpresa, IdSucursal, IdBodega, IdNota, SUM(sc_subtotal) AS SubTotal_Iva, NULL AS SubTotal_0, SUM(sc_iva) AS sc_iva
                          FROM            dbo.fa_notaCreDeb_det AS A
                          WHERE        (sc_iva > 0)
                          GROUP BY IdEmpresa, IdSucursal, IdBodega, IdNota
                          UNION
                          SELECT        IdEmpresa, IdSucursal, IdBodega, IdNota, NULL AS SubTotal_Iva, SUM(sc_subtotal) AS SubTotal_0, SUM(sc_iva) AS sc_iva
                          FROM            dbo.fa_notaCreDeb_det AS A
                          WHERE        (sc_iva = 0) AND (sc_subtotal <> 0)
                          GROUP BY IdEmpresa, IdSucursal, IdBodega, IdNota) AS A
GROUP BY IdEmpresa, IdSucursal, IdBodega, IdNota