CREATE VIEW [web].[vwfa_notaCreDeb_x_fa_factura_NotaDeb]
AS
SELECT        cruce.IdEmpresa_nt, cruce.IdSucursal_nt, cruce.IdBodega_nt, cruce.IdNota_nt, cruce.secuencia, cruce.vt_tipoDoc, cruce.IdEmpresa_fac_nd_doc_mod, cruce.IdSucursal_fac_nd_doc_mod, cruce.IdBodega_fac_nd_doc_mod, 
                         cruce.IdCbteVta_fac_nd_doc_mod, cruce.Valor_Aplicado, iif(cruce.vt_tipoDoc = 'FACT', factura.vt_NumFactura, debito.NumNotaImpresa) vt_NumFactura, iif(cruce.vt_tipoDoc = 'FACT', factura.vt_Observacion, 
                         debito.sc_observacion) vt_Observacion, iif(cruce.vt_tipoDoc = 'FACT', factura.CodCbteVta, debito.CodNota) CodDoc, iif(cruce.vt_tipoDoc = 'FACT', factura.vt_Subtotal, debito.sc_subtotal) vt_Subtotal, iif(cruce.vt_tipoDoc = 'FACT', 
                         factura.vt_iva, debito.sc_iva) vt_iva, iif(cruce.vt_tipoDoc = 'FACT', factura.vt_total, debito.sc_total) vt_total, isnull(cobro.ValorCobrado, 0) ValorCobrado, isnull(iif(cruce.vt_tipoDoc = 'FACT', factura.vt_total, debito.sc_total) 
                         - isnull(cobro.ValorCobrado, 0), 0) AS saldo, iif(cruce.vt_tipoDoc = 'FACT', factura.vt_total, debito.sc_total) - isnull(cobro.ValorCobrado, 0) + cruce.Valor_Aplicado AS saldo_sin_cobro, iif(cruce.vt_tipoDoc = 'FACT', factura.vt_fecha, 
                         debito.no_fecha) vt_fecha, iif(cruce.vt_tipoDoc = 'FACT', factura.vt_fech_venc, debito.no_fecha_venc) vt_fech_venc, iif(cruce.vt_tipoDoc = 'FACT', factura.IdCliente, debito.IdCliente) IdCliente, iif(cruce.vt_tipoDoc = 'FACT', 
                         factura.Nombres, debito.Nombres) pe_nombreCompleto, cruce.fecha_cruce, cruce.ValorProntoPago
FROM            fa_notaCreDeb_x_fa_factura_NotaDeb AS cruce LEFT JOIN
                             (SELECT        fa_factura.IdEmpresa, fa_factura.IdSucursal, fa_factura.IdBodega, fa_factura.IdCbteVta, fa_factura.vt_tipoDoc, fa_factura.vt_tipoDoc + '-' + CAST(CAST(fa_factura.vt_NumFactura AS NUMERIC) AS VARCHAR(20)) 
                                                         vt_NumFactura, fa_factura.vt_Observacion, fa_factura.CodCbteVta, SUM(fa_factura_det.vt_Subtotal) AS vt_Subtotal, SUM(fa_factura_det.vt_iva) AS vt_iva, SUM(fa_factura_det.vt_total) AS vt_total, 
                                                         fa_factura.vt_fecha, fa_factura.vt_fech_venc, fa_factura.IdCliente, tb_persona.pe_nombreCompleto Nombres
                               FROM            fa_factura LEFT OUTER JOIN
                                                         fa_factura_det ON fa_factura.IdEmpresa = fa_factura_det.IdEmpresa AND fa_factura.IdSucursal = fa_factura_det.IdSucursal AND fa_factura.IdBodega = fa_factura_det.IdBodega AND 
                                                         fa_factura.IdCbteVta = fa_factura_det.IdCbteVta INNER JOIN
                                                         fa_cliente ON fa_cliente.IdEmpresa = fa_factura.IdEmpresa AND fa_cliente.IdCliente = fa_factura.IdCliente INNER JOIN
                                                         tb_persona ON fa_cliente.IdPersona = tb_persona.IdPersona
                               GROUP BY fa_factura.IdEmpresa, fa_factura.IdSucursal, fa_factura.IdBodega, fa_factura.IdCbteVta, fa_factura.vt_tipoDoc, fa_factura.vt_NumFactura, fa_factura.vt_Observacion, fa_factura.CodCbteVta, fa_factura.vt_fecha, 
                                                         fa_factura.vt_fech_venc, fa_factura.IdCliente, tb_persona.pe_nombreCompleto) AS factura ON cruce.IdEmpresa_fac_nd_doc_mod = factura.IdEmpresa AND 
                         cruce.IdSucursal_fac_nd_doc_mod = factura.IdSucursal AND cruce.IdBodega_fac_nd_doc_mod = factura.IdBodega AND cruce.IdCbteVta_fac_nd_doc_mod = factura.IdCbteVta AND cruce.vt_tipoDoc = factura.vt_tipoDoc LEFT JOIN
                             (SELECT        fa_notaCreDeb.IdEmpresa, fa_notaCreDeb.IdSucursal, fa_notaCreDeb.IdBodega, fa_notaCreDeb.IdNota, fa_notaCreDeb.CodDocumentoTipo, tb_persona.pe_nombreCompleto Nombres, fa_notaCreDeb.no_fecha, 
                                                         fa_notaCreDeb.no_fecha_venc, fa_notaCreDeb.sc_observacion, fa_notaCreDeb.CodNota, fa_notaCreDeb.IdCliente, fa_notaCreDeb.CodDocumentoTipo + '-' + IIF(fa_notaCreDeb.NumNota_Impresa IS NOT NULL, 
                                                         CAST(CAST(fa_notaCreDeb.NumNota_Impresa AS NUMERIC) AS VARCHAR(20)), cast(fa_notaCreDeb.IdNota AS varchar(20))) AS NumNotaImpresa, SUM(fa_notaCreDeb_det.sc_subtotal) AS sc_subtotal, 
                                                         SUM(fa_notaCreDeb_det.sc_iva) AS sc_iva, SUM(fa_notaCreDeb_det.sc_total) AS sc_total
                               FROM            fa_notaCreDeb LEFT OUTER JOIN
                                                         fa_notaCreDeb_det ON fa_notaCreDeb.IdEmpresa = fa_notaCreDeb_det.IdEmpresa AND fa_notaCreDeb.IdSucursal = fa_notaCreDeb_det.IdSucursal AND 
                                                         fa_notaCreDeb.IdBodega = fa_notaCreDeb_det.IdBodega AND fa_notaCreDeb.IdNota = fa_notaCreDeb_det.IdNota INNER JOIN
                                                         fa_cliente ON fa_cliente.IdEmpresa = fa_notaCreDeb.IdEmpresa AND fa_cliente.IdCliente = fa_notaCreDeb.IdCliente INNER JOIN
                                                         tb_persona ON fa_cliente.IdPersona = tb_persona.IdPersona
                               GROUP BY tb_persona.pe_nombreCompleto, fa_notaCreDeb.IdEmpresa, fa_notaCreDeb.IdSucursal, fa_notaCreDeb.IdBodega, fa_notaCreDeb.IdNota, fa_notaCreDeb.CodDocumentoTipo, fa_notaCreDeb.no_fecha, 
                                                         fa_notaCreDeb.no_fecha_venc, fa_notaCreDeb.sc_observacion, fa_notaCreDeb.NumNota_Impresa, fa_notaCreDeb.CodNota, fa_notaCreDeb.IdCliente) AS debito ON 
                         cruce.IdEmpresa_fac_nd_doc_mod = debito.IdEmpresa AND cruce.IdSucursal_fac_nd_doc_mod = debito.IdSucursal AND cruce.IdBodega_fac_nd_doc_mod = debito.IdBodega AND 
                         cruce.IdCbteVta_fac_nd_doc_mod = debito.IdNota AND cruce.vt_tipoDoc = debito.CodDocumentoTipo LEFT OUTER JOIN
                             (SELECT        IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento, SUM(dc_ValorPago) AS ValorCobrado
                               FROM            dbo.cxc_cobro_det AS det
                               WHERE        (estado = 'A')
                               GROUP BY IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento) AS cobro ON cruce.IdEmpresa_fac_nd_doc_mod = cobro.IdEmpresa AND cruce.IdSucursal_fac_nd_doc_mod = cobro.IdSucursal AND 
                         cruce.IdBodega_fac_nd_doc_mod = cobro.IdBodega_Cbte AND cruce.IdCbteVta_fac_nd_doc_mod = cobro.IdCbte_vta_nota AND cruce.vt_tipoDoc = cobro.dc_TipoDocumento
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwfa_notaCreDeb_x_fa_factura_NotaDeb';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwfa_notaCreDeb_x_fa_factura_NotaDeb';

