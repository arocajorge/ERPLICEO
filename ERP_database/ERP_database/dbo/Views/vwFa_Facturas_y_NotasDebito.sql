CREATE VIEW [dbo].[vwFa_Facturas_y_NotasDebito]
AS
SELECT      A.IdEmpresa, A.IdSucursal, A.IdBodega, A.Tipo, A.IdNota AS IdCbte, A.Serie1, A.Serie2, A.NumNota_Impresa, A.NumAutorizacion, A.IdCliente, 
                      A.no_fecha AS fecha, A.no_fecha_venc, CASE WHEN A.NumNota_Impresa IS NULL THEN 'N/D#:' + CAST(A.IdNota AS varchar(20)) 
                      ELSE 'N/D#:' + A.Serie1 + '-' + A.Serie2 + '-' + A.NumNota_Impresa + '/' + CAST(A.IdNota AS varchar(20)) END AS Referencia, SUM(fa_notaCreDeb_det.sc_subtotal) 
                      SubTotal, SUM(fa_notaCreDeb_det.sc_iva) Iva, SUM(fa_notaCreDeb_det.sc_total)  AS Total
FROM         vwfa_notaCreDeb AS A INNER JOIN
                      fa_notaCreDeb_det ON A.IdEmpresa = fa_notaCreDeb_det.IdEmpresa AND A.IdSucursal = fa_notaCreDeb_det.IdSucursal AND 
                      A.IdBodega = fa_notaCreDeb_det.IdBodega AND A.IdNota = fa_notaCreDeb_det.IdNota
WHERE     (A.CreDeb = 'D')
GROUP BY A.IdEmpresa, A.IdSucursal, A.IdBodega, A.Tipo, A.IdNota, A.Serie1, A.Serie2, A.NumNota_Impresa, A.NumAutorizacion, A.IdCliente, A.no_fecha, 
                      A.no_fecha_venc
UNION
SELECT      A.IdEmpresa, A.IdSucursal, A.IdBodega, A.vt_tipoDoc, A.IdCbteVta, A.vt_serie1, A.vt_serie2, A.vt_NumFactura, A.vt_autorizacion, A.IdCliente, A.vt_fecha, 
                      A.vt_fech_venc, CASE WHEN A.vt_NumFactura IS NULL THEN A.vt_tipoDoc + CAST(A.IdCbteVta AS varchar(20)) 
                      ELSE A.vt_tipoDoc + A.vt_serie1 + '-' + A.vt_serie2 + '-' + A.vt_NumFactura + '/' + CAST(A.IdCbteVta AS varchar(20)) END AS Referencia, SUM(fa_factura_det.vt_Subtotal)
                       AS Subtotal, SUM(fa_factura_det.vt_iva) AS Iva, SUM(fa_factura_det.vt_total) 
                      AS Total
FROM         fa_factura AS A INNER JOIN
                      fa_factura_det ON A.IdEmpresa = fa_factura_det.IdEmpresa AND A.IdSucursal = fa_factura_det.IdSucursal AND A.IdBodega = fa_factura_det.IdBodega AND 
                      A.IdCbteVta = fa_factura_det.IdCbteVta
GROUP BY A.IdEmpresa, A.IdSucursal, A.IdBodega, A.vt_tipoDoc, A.IdCbteVta, A.vt_serie1, A.vt_serie2, A.vt_NumFactura, A.vt_autorizacion, A.IdCliente, A.vt_fecha, 
                      A.vt_fech_venc
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[9] 4[4] 2[68] 3) )"
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwFa_Facturas_y_NotasDebito';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwFa_Facturas_y_NotasDebito';

