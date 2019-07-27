CREATE view [dbo].[vwfa_factura_subtotal_iva_0_totales]
as
SELECT        IdEmpresa, IdSucursal, IdBodega, IdCbteVta, ISNULL(SUM(SubTotal_0),0) AS SubTotal_0, ISNULL(SUM(SubTotal_Iva),0) AS SubTotal_Iva, MAX(vt_por_iva) vt_por_iva, SUM(A.vt_iva)vt_iva,
			ISNULL(SUM(SubTotal_0),0) + ISNULL(SUM(SubTotal_Iva),0) + SUM(A.vt_iva) AS vt_total
FROM            (SELECT        IdEmpresa, IdSucursal, IdBodega, IdCbteVta, SUM(vt_Subtotal) AS SubTotal_Iva, NULL AS SubTotal_0, MAX(A.vt_por_iva)vt_por_iva, SUM(A.vt_iva)vt_iva
                          FROM            dbo.fa_factura_det AS A
                          WHERE        (vt_iva > 0)
                          GROUP BY IdEmpresa, IdSucursal, IdBodega, IdCbteVta
                          UNION
                          SELECT        IdEmpresa, IdSucursal, IdBodega, IdCbteVta, NULL AS SubTotal_Iva, SUM(vt_Subtotal) AS SubTotal_0, MAX(A.vt_por_iva)vt_por_iva,SUM(A.vt_iva)vt_iva
                          FROM            dbo.fa_factura_det AS A
                          WHERE        (vt_iva = 0) AND (vt_Subtotal <> 0)
                          GROUP BY IdEmpresa, IdSucursal, IdBodega, IdCbteVta) AS A
GROUP BY IdEmpresa, IdSucursal, IdBodega, IdCbteVta