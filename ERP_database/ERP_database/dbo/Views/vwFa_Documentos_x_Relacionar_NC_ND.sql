
CREATE VIEW [dbo].[vwFa_Documentos_x_Relacionar_NC_ND] AS
SELECT        cabfac.IdEmpresa, cabfac.IdSucursal, cabfac.IdBodega, cabfac.vt_tipoDoc, 
                         cabfac.vt_tipoDoc + ' ' + cabfac.vt_serie1 + '-' + cabfac.vt_serie2 + '-' + cabfac.vt_NumFactura AS vt_NunDocumento, cabfac.vt_Observacion AS Referencia, 
                         cabfac.IdCbteVta AS IdComprobante, cabfac.CodCbteVta AS CodComprobante, Sucu.Su_Descripcion, cabfac.IdCliente, cabfac.vt_fecha, ROUND(SUM(detfac.vt_total), 2) AS vt_total, ROUND(SUM(detfac.vt_total) 
                         , 2) 
                         - ROUND(ISNULL(AVG(vwcxc_total_cobros_x_Docu.dc_ValorPago), 0), 2) AS Saldo, ISNULL(AVG(vwcxc_total_cobros_x_Docu.dc_ValorPago), 0) AS TotalxCobrado, 
                         Bod.bo_Descripcion AS Bodega, ROUND(SUM(detfac.vt_Subtotal), 2) AS vt_Subtotal, ROUND(SUM(detfac.vt_iva), 2) AS vt_iva, cabfac.vt_fech_venc, 
                         ROUND(ISNULL(AVG(Cob_RtFu.dc_ValorPago), 0), 2) AS dc_ValorRetFu, ROUND(ISNULL(AVG(Cob_RtIVA.dc_ValorPago), 0), 2) AS dc_ValorRetIva, 
                         Cli.Codigo AS CodCliente, tb_persona.pe_nombreCompleto AS NomCliente, tb_empresa.em_nombre
FROM            fa_factura_det AS detfac INNER JOIN
                         fa_factura AS cabfac ON detfac.IdBodega = cabfac.IdBodega AND detfac.IdSucursal = cabfac.IdSucursal AND detfac.IdEmpresa = cabfac.IdEmpresa AND 
                         detfac.IdCbteVta = cabfac.IdCbteVta INNER JOIN
                         tb_sucursal AS Sucu ON cabfac.IdEmpresa = Sucu.IdEmpresa AND cabfac.IdSucursal = Sucu.IdSucursal INNER JOIN
                         tb_bodega AS Bod ON cabfac.IdEmpresa = Bod.IdEmpresa AND cabfac.IdSucursal = Bod.IdSucursal AND cabfac.IdBodega = Bod.IdBodega AND 
                         Sucu.IdEmpresa = Bod.IdEmpresa AND Sucu.IdSucursal = Bod.IdSucursal INNER JOIN
                         fa_cliente AS Cli ON cabfac.IdEmpresa = Cli.IdEmpresa AND cabfac.IdCliente = Cli.IdCliente INNER JOIN
                         tb_persona ON Cli.IdPersona = tb_persona.IdPersona INNER JOIN
                         tb_empresa ON cabfac.IdEmpresa = tb_empresa.IdEmpresa LEFT OUTER JOIN
                         vwcxc_cobros_x_vta_nota_x_RetIVA_Sumatoria AS Cob_RtIVA ON cabfac.IdEmpresa = Cob_RtIVA.IdEmpresa AND cabfac.IdSucursal = Cob_RtIVA.IdSucursal AND 
                         cabfac.IdBodega = Cob_RtIVA.IdBodega_Cbte AND cabfac.IdCbteVta = Cob_RtIVA.IdCbte_vta_nota LEFT OUTER JOIN
                         vwcxc_cobros_x_vta_nota_x_RetFuente_Sumatoria AS Cob_RtFu ON cabfac.IdEmpresa = Cob_RtFu.IdEmpresa AND cabfac.IdSucursal = Cob_RtFu.IdSucursal AND 
                         cabfac.IdBodega = Cob_RtFu.IdBodega_Cbte AND cabfac.IdCbteVta = Cob_RtFu.IdCbte_vta_nota LEFT OUTER JOIN
                         vwcxc_total_cobros_x_Docu ON cabfac.IdEmpresa = vwcxc_total_cobros_x_Docu.IdEmpresa AND cabfac.IdSucursal = vwcxc_total_cobros_x_Docu.IdSucursal AND 
                         cabfac.IdBodega = vwcxc_total_cobros_x_Docu.IdBodega_Cbte AND cabfac.vt_tipoDoc = vwcxc_total_cobros_x_Docu.dc_TipoDocumento AND 
                         cabfac.IdCbteVta = vwcxc_total_cobros_x_Docu.IdCbte_vta_nota
GROUP BY cabfac.vt_tipoDoc + ' ' + cabfac.vt_serie1 + '-' + cabfac.vt_serie2 + '-' + cabfac.vt_NumFactura, cabfac.IdEmpresa, cabfac.IdSucursal, cabfac.IdBodega, 
                         cabfac.IdCbteVta, cabfac.CodCbteVta, Sucu.Su_Descripcion, cabfac.vt_tipoDoc, cabfac.IdCliente, cabfac.vt_fecha, cabfac.vt_Observacion, Bod.bo_Descripcion, 
                         cabfac.vt_fech_venc, Cli.Codigo, tb_persona.pe_nombreCompleto, tb_empresa.em_nombre
UNION
SELECT        A.IdEmpresa, A.IdSucursal, A.IdBodega, 'NTDB' AS CreDeb, CASE WHEN A.NumNota_Impresa IS NULL THEN 'N/D#' + CAST(A.IdNota AS varchar(20)) 
                         ELSE 'N/D#' + A.Serie1 + '-' + A.Serie2 + '' + A.NumNota_Impresa END AS Documento, A.sc_observacion, A.IdNota, A.CodNota, su.Su_Descripcion, A.IdCliente, 
                         A.no_fecha, ROUND(SUM(B.sc_total), 2) AS sc_total, ROUND((SUM(B.sc_total) 
                        - ISNULL(SUM(CB.dc_ValorPago), 0)), 2) AS Saldo, ISNULL(SUM(CB.dc_ValorPago), 0) AS totalCobrado, 
                         Bo.bo_Descripcion, ROUND(SUM(B.sc_subtotal), 2), ROUND(SUM(B.sc_iva), 2), A.no_fecha_venc, 0 AS RtFT, 0 AS RtIVA, Cli.Codigo AS CodCliente, 
                         tb_persona.pe_nombreCompleto, tb_empresa.em_nombre
FROM            fa_notaCreDeb AS A INNER JOIN
                         fa_notaCreDeb_det AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdSucursal = B.IdSucursal AND A.IdBodega = B.IdBodega AND A.IdNota = B.IdNota INNER JOIN
                         tb_bodega AS Bo ON A.IdEmpresa = Bo.IdEmpresa AND A.IdSucursal = Bo.IdSucursal AND A.IdBodega = Bo.IdBodega INNER JOIN
                         tb_sucursal AS su ON Bo.IdEmpresa = su.IdEmpresa AND Bo.IdSucursal = su.IdSucursal INNER JOIN
                         fa_cliente AS Cli ON A.IdEmpresa = Cli.IdEmpresa AND A.IdCliente = Cli.IdCliente INNER JOIN
                         tb_persona ON Cli.IdPersona = tb_persona.IdPersona INNER JOIN
                         tb_empresa ON A.IdEmpresa = tb_empresa.IdEmpresa LEFT OUTER JOIN
                         vwcxc_total_cobros_x_Docu AS CB ON A.IdEmpresa = CB.IdEmpresa AND A.IdSucursal = CB.IdSucursal AND A.IdBodega = CB.IdBodega_Cbte AND 
                         A.IdNota = CB.IdCbte_vta_nota AND A.CreDeb = CB.dc_TipoDocumento
GROUP BY A.IdEmpresa, A.IdSucursal, A.IdBodega, A.no_fecha, A.CreDeb, A.IdNota, A.Serie1, A.Serie2, A.NumNota_Impresa, A.sc_observacion, A.CodNota, su.Su_Descripcion, 
                         A.IdCliente, Bo.bo_Descripcion, A.no_fecha_venc, Cli.Codigo, tb_persona.pe_nombreCompleto, tb_empresa.em_nombre
HAVING        (A.CreDeb = 'D')
UNION 
SELECT        A.IdEmpresa, A.IdSucursal, A.IdBodega, 'NTCR' AS CreDeb, CASE WHEN A.NumNota_Impresa IS NULL THEN 'N/C#' + CAST(A.IdNota AS varchar(20)) 
                         ELSE 'N/C#' + A.Serie1 + '-' + A.Serie2 + '' + A.NumNota_Impresa END AS Documento, A.sc_observacion, A.IdNota, A.CodNota, su.Su_Descripcion, A.IdCliente, 
                         A.no_fecha, ROUND(SUM(B.sc_total) , 2) AS sc_total, ROUND((SUM(B.sc_total)  - ISNULL(SUM(CB.dc_ValorPago), 0)), 2) AS Saldo, ISNULL(SUM(CB.dc_ValorPago), 0) AS totalCobrado, 
                         Bo.bo_Descripcion, ROUND(SUM(B.sc_subtotal), 2), ROUND(SUM(B.sc_iva), 2), A.no_fecha_venc, 0 AS RtFT, 0 AS RtIVA, Cli.Codigo AS CodCliente, 
                         tb_persona.pe_nombreCompleto, tb_empresa.em_nombre
FROM            fa_notaCreDeb AS A INNER JOIN
                         fa_notaCreDeb_det AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdSucursal = B.IdSucursal AND A.IdBodega = B.IdBodega AND A.IdNota = B.IdNota INNER JOIN
                         tb_bodega AS Bo ON A.IdEmpresa = Bo.IdEmpresa AND A.IdSucursal = Bo.IdSucursal AND A.IdBodega = Bo.IdBodega INNER JOIN
                         tb_sucursal AS su ON Bo.IdEmpresa = su.IdEmpresa AND Bo.IdSucursal = su.IdSucursal INNER JOIN
                         fa_cliente AS Cli ON A.IdEmpresa = Cli.IdEmpresa AND A.IdCliente = Cli.IdCliente INNER JOIN
                         tb_persona ON Cli.IdPersona = tb_persona.IdPersona INNER JOIN
                         tb_empresa ON A.IdEmpresa = tb_empresa.IdEmpresa LEFT OUTER JOIN
                         vwcxc_total_cobros_x_Docu AS CB ON A.IdEmpresa = CB.IdEmpresa AND A.IdSucursal = CB.IdSucursal AND A.IdBodega = CB.IdBodega_Cbte AND 
                         A.IdNota = CB.IdCbte_vta_nota AND A.CreDeb = CB.dc_TipoDocumento
GROUP BY A.IdEmpresa, A.IdSucursal, A.IdBodega, A.no_fecha, A.CreDeb, A.IdNota, A.Serie1, A.Serie2, A.NumNota_Impresa, A.sc_observacion, A.CodNota, su.Su_Descripcion, 
                         A.IdCliente, Bo.bo_Descripcion, A.no_fecha_venc, Cli.Codigo, tb_persona.pe_nombreCompleto, tb_empresa.em_nombre
HAVING        (A.CreDeb = 'C')